using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Common;
using System.IO;

using ComLib;

using ComLib.Models;


namespace ComLib.CodeGeneration
{

    /// <summary>
    /// Builds the ORM Mapping.
    /// </summary>
    public class CodeBuilderORMHibernate : CodeBuilderBase, ICodeBuilder
    {
        #region ICodeBuilder Members
        /// <summary>
        /// Create the ORM mappings in xml file.
        /// </summary>
        /// <param name="ctx"></param>
        /// <returns></returns>
        public BoolMessageItem<ModelContainer> Process(ModelContext ctx)
        {
            StringBuilder buffer = new StringBuilder();
            
            // If only generating the file completely instead of replace parts of it.
            if (!ctx.AllModels.Settings.OrmGenerationDef.Replace)
                buffer.Append("<hibernate-mapping xmlns=\"urn:nhibernate-mapping-2.0\">");

            IncrementIndent();
            foreach(Model model in ctx.AllModels.AllModels)
            {
                if (model.GenerateOrMap)
                {
                    // Get inheritance chain to correct create properties in proper order.
                    List<Model> modelChain = ModelUtils.GetModelInheritancePath(ctx, model.Name);

                    // Inheritance order doesn't matter.
                    // Sort based on the importance of properties.
                    ModelUtils.Sort(modelChain);
                                        
                    BuildMappingForModel(ctx, model, modelChain, buffer);
                }
            }
            DecrementIndent();

            // If only generating the file completely instead of replace parts of it.
            if (!ctx.AllModels.Settings.OrmGenerationDef.Replace)
                buffer.Append("</hibernate-mapping>");
            
            string mappings = buffer.ToString();
            
            // Write to file.
            string filePath = ctx.AllModels.Settings.ModelOrmLocation;
            if (ctx.AllModels.Settings.OrmGenerationDef.Replace)
            {
                string ormMap = File.ReadAllText(ctx.AllModels.Settings.ModelOrmLocation);
                int ndxTextBefore = (ormMap.IndexOf(ctx.AllModels.Settings.OrmGenerationDef.StartTag) + ctx.AllModels.Settings.OrmGenerationDef.StartTag.Length);
                int ndxTextAfter = ormMap.IndexOf(ctx.AllModels.Settings.OrmGenerationDef.EndTag);
                string textBefore = ormMap.Substring(0, ndxTextBefore);
                string textAfter = ormMap.Substring(ndxTextAfter);
                mappings = textBefore + mappings + textAfter;
            }
            File.WriteAllText(filePath, mappings);
            return new BoolMessageItem<ModelContainer>(ctx.AllModels, true, string.Empty);
        }

        #endregion


        /// <summary>
        /// Build the mapping for the entire model.
        /// </summary>
        /// <param name="ctx"></param>
        /// <param name="model"></param>
        /// <param name="buffer"></param>
        /// <returns></returns>
        private void BuildMappingForModel(ModelContext ctx, Model model, List<Model> modelChain, StringBuilder buffer)
        {
            // <class name="CommonLibrary.ConfigItem, CommonLibrary" table="wk_ConfigSettings" lazy="false" >
            string template = GetIndent() + "<class name=\"{0}, {1}\" table=\"{2}\" lazy=\"false\" >" + Environment.NewLine;
            string assemblyName = string.IsNullOrEmpty(ctx.AllModels.Settings.AssemblyName) ? model.AssemblyName : ctx.AllModels.Settings.AssemblyName;
            string fullName = model.NameSpace + "." + model.Name;
            template = string.Format(template, fullName, assemblyName, model.TableName);

            buffer.Append(Environment.NewLine + template);
            // Generate prop mapping for each one.
            // <property name="Title" type="string" AllowNull="false" />
            IncrementIndent();

            // Build mappings for inherited models.
            foreach (Model currentModel in modelChain)
            {
                BuildMappingForProperties(ctx, currentModel, buffer, false);
                if (currentModel.ComposedOf != null && currentModel.ComposedOf.Count > 0)
                {
                    // Build the properties for each composed model
                    foreach (Composition composite in currentModel.ComposedOf)
                    {
                        // Build mappings for the composite.
                        if (composite.GenerateOrMap)
                        {
                            Model compositeOf = ctx.AllModels.ModelMap[composite.Name];

                            // Build <component name="Address">
                            buffer.Append(GetIndent() + "<component name=\"" + compositeOf.Name + "\">" + Environment.NewLine);
                            IncrementIndent(1);
                            BuildMappingForProperties(ctx, compositeOf, buffer, false);
                            DecrementIndent(1);
                            buffer.Append(GetIndent() + "</component>" + Environment.NewLine);
                        }
                    }
                }
                // Build the properties for each included model.                    
                if (currentModel.Includes != null && currentModel.Includes.Count > 0)
                {
                    foreach (Include include in currentModel.Includes)
                    {
                        Model includedModel = ctx.AllModels.ModelMap[include.Name];
                        BuildMappingForProperties(ctx, includedModel, buffer, false);
                    }
                }
            }             
            DecrementIndent();
            buffer.Append(GetIndent() + "</class>" + Environment.NewLine);            
        }


        /// <summary>
        /// Build mappings for properties.
        /// </summary>
        /// <param name="ctx"></param>
        /// <param name="currentModel"></param>
        /// <param name="buffer"></param>
        /// <returns></returns>
        private void BuildMappingForProperties(ModelContext ctx, Model currentModel, StringBuilder buffer, bool prefixUsingModelName)
        {
           var props = from p in currentModel.Properties where p.CreateColumn == true select p;

           foreach (PropertyInfo prop in props)
            {
                string propMapping = string.Empty;
                if (prop.IsKey)
                    propMapping = BuildMappingForPropetyId(ctx, currentModel, prop, prefixUsingModelName);
                else
                    propMapping = BuildMappingForProperty(ctx, currentModel, prop, prefixUsingModelName);
                buffer.Append(propMapping);
            }
        }


        /// <summary>
        /// Build the id mapping.
        /// </summary>
        /// <param name="ctx"></param>
        /// <param name="current"></param>
        /// <param name="prop"></param>
        /// <returns></returns>
        private string BuildMappingForPropetyId(ModelContext ctx, Model current, PropertyInfo prop, bool prefixWithModelName)
        {
            // <id name="Id" type="int" unsaved-value="0">
            string propName = prefixWithModelName ? current.Name + "." + prop.Name : prop.Name;
            string template = GetIndent() + "<id name=\"{0}\" type=\"{1}\" unsaved-value=\"0\">";
            string dataType = prop.DataType.Name;
            
            template = string.Format(template, propName, dataType);
            IncrementIndent();
            string columnMapping = BuildMappingForColumn(ctx, current, prop);
            template = template + Environment.NewLine
                         + columnMapping
                         + GetIndent() + "<generator class=\"native\" />" + Environment.NewLine;
            DecrementIndent();
            template += GetIndent() + "</id>" + Environment.NewLine;
            return template;
        }


        /// <summary>
        /// Build a regular property mapping.
        /// </summary>
        /// <param name="ctx"></param>
        /// <param name="current"></param>
        /// <param name="prop"></param>
        /// <returns></returns>
        private string BuildMappingForProperty(ModelContext ctx, Model current, PropertyInfo prop, bool prefixWithModelName)
        {
            // <property name="Title" type="String">
            //      <column name="Title" length="100" sql-type="nvarchar" not-null="false"/>
            // </property>
            string propName = prefixWithModelName ? current.Name + "." + prop.Name : prop.Name;
            string template = GetIndent() + "<property name=\"{0}\" type=\"{1}\" >";            
            template = string.Format(template, propName, prop.DataType.Name);
            IncrementIndent();
            string columnMapping = BuildMappingForColumn(ctx, current, prop);
            template = template + Environment.NewLine + columnMapping;
            DecrementIndent();
            template += GetIndent() + "</property>" + Environment.NewLine;

            return template;
        }



        /// <summary>
        /// Build column mapping.
        /// </summary>
        /// <param name="ctx"></param>
        /// <param name="current"></param>
        /// <param name="prop"></param>
        /// <returns></returns>
        private string BuildMappingForColumn(ModelContext ctx, Model current, PropertyInfo prop)
        {
            // <column name="Title" length="100" sql-type="nvarchar" not-null="false"/>            
            string columnString = GetIndent() + "<column name=\"" + prop.ColumnName + "\"";
            
            // Set the length.
            if (prop.DataType == typeof(string))
            {
                columnString += " length=\"" + prop.MaxLength + "\"";
                columnString += " sql-type=\"nvarchar\"";
            }
            else
            {
                string dataType = TypeMap.Get<string>(TypeMap.SqlServer, prop.DataType.Name);
                columnString += " sql-type=\"" + dataType + "\"";
            }
            // Unique.
            if (prop.IsUnique)
            {
                columnString += " unique=\"true\"";
            }
            columnString += " not-null=\"" + prop.IsRequired.ToString().ToLower() + "\" />";
            return columnString + Environment.NewLine;
        }      
    }
}
