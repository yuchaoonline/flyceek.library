using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Common;
using System.IO;


using ComLib;
using ComLib.Orm;
using ComLib.Reflection;
using ComLib.Collections;
using ComLib.Models;



namespace ComLib.CodeGeneration
{
    /// <summary>
    /// Code Builder Domain Model.
    /// </summary>
    public class CodeBuilderDomain : CodeBuilderBase, ICodeBuilder
    {
        #region ICodeBuilder Members
        /// <summary>
        /// Builds the Domain model code which includes :
        /// 1. Entity.
        /// 2. ActiveRecord
        /// 3. Service 
        /// 4. Repository
        /// 5. Settings
        /// 6. Validator
        /// </summary>
        /// <param name="ctx"></param>
        /// <returns></returns>
        public virtual BoolMessageItem<ModelContainer> Process(ModelContext ctx)
        {
            string codeTemplatePath = ctx.AllModels.Settings.ModelCodeLocationTemplate;
            Dictionary<string, CodeFile> fileMap = CodeBuilderUtils.GetFiles(ctx, string.Empty, codeTemplatePath);
            
            foreach (Model currentModel in ctx.AllModels.AllModels)
            {
                // Pre condition.
                if (currentModel.GenerateCode)
                {
                    // Create the database table for all the models.
                    List<Model> modelChain = ModelUtils.GetModelInheritancePath(ctx, currentModel.Name, true);

                    fileMap = CodeFileHelper.GetFilteredDomainModelFiles(currentModel, fileMap);
                    string generatedCodePath = (string)ctx.AllModels.Settings.ModelCodeLocation;
                    generatedCodePath += "/" + currentModel.Name;                    
                    IDictionary<string, string> subs = new Dictionary<string, string>();
                    BuildSubstitutions(ctx, currentModel, modelChain, subs);

                    foreach (string fileName in fileMap.Keys)
                    {
                        string templateFile = codeTemplatePath + "/" + fileName;
                        string generated = generatedCodePath + "/" + fileMap[fileName].QualifiedName;
                        CodeFileHelper.Write(templateFile, generated, generatedCodePath, subs);
                    }
                }
            }
            return new BoolMessageItem<ModelContainer>(ctx.AllModels, true, string.Empty);
        }

        #endregion


        /// <summary>
        /// Build all the substutions.
        /// </summary>
        /// <param name="ctx">The entire context of all the models.</param>
        /// <param name="currentModel">The current model being code-generated.</param>
        /// <param name="modelChain">The inheritance chain of the model.</param>
        /// <param name="subs">The dictionary of substutions to populate.</param>
        public virtual void BuildSubstitutions(ModelContext ctx, Model currentModel, List<Model> modelChain, IDictionary<string, string> subs)
        {
            // 1. Validation code.
            // 2. Row Mapping code.
            // 3. Properties on the Object.
            // 4. Edit roles for the model.
            // 5. Data Massagers.
            CodeBuilderValidation validationBuilder = new CodeBuilderValidation();
            CodeBuilderDbRowMapper codeBuilderRowMapper = new CodeBuilderDbRowMapper();
            codeBuilderRowMapper.Tab = "\t\t\t";
            // TO_DO: This is using sql by default.
            OrmSqlStaticBuilder sql = OrmSqlStaticBuilderFactory.GetBuilder(ctx.AllModels.Settings.Connection.ProviderName);
            sql.Init(ctx, currentModel.Name, currentModel.Name + "s", 3);
            Tuple2<string, string> massagersCode = BuildAllMassagers(ctx, currentModel);
            string properties = BuildPropertiesForModel(ctx, currentModel);
            string moderators = BuildEditRoles(currentModel);
            string validationCode = validationBuilder.BuildValidationForModel(ctx, currentModel);
            string rowMapperCode = codeBuilderRowMapper.BuildRowMapper(ctx, currentModel, modelChain);

            subs["<%= model.NameSpace %>"] = currentModel.NameSpace;
            subs["<%= model.Name %>"] = currentModel.Name;
            subs["<%= model.Properties %>"] = properties;
            subs["<%= model.ValidationCode %>"] = validationCode;
            subs["<%= model.BeforeValidationMassagers %>"] = massagersCode.First;
            subs["<%= model.AfterValidationMassagers %>"] = massagersCode.Second;
            subs["<%= model.RowMappingCode %>"] = rowMapperCode;
            subs["<%= model.EditRoles %>"] = moderators;
            subs["<%= model.SqlInsert %>"] = sql.Insert();
            subs["<%= model.SqlUpdate %>"] = sql.Update();
        }


        /// <summary>
        /// Build the properties.
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public virtual string BuildPropertiesForModel(ModelContext ctx, Model model)
        {
            IncrementIndent(2);            
            StringBuilder buffer = new StringBuilder();
            // Build properties that belog directly to the model.
            BuildProperties(ctx, model, buffer);

            ModelIterator iterator = new ModelIterator();            
            iterator.OnCompositeProcess += (mctx, m, composition) =>
            {
                BuildProperty(composition.ModelRef, new PropertyInfo() { Name = composition.ModelRef.Name }, false, buffer);
                return true;
            };
            iterator.OnIncludeProcess += (mctx, m, include) =>
            {
                BuildProperties(ctx, include.ModelRef, buffer);
                return true;
            };
            iterator.Process(ctx, model.Name);
            DecrementIndent(2);
            string props = buffer.ToString();
            return props;
        }


        /// <summary>
        /// Build properties
        /// </summary>
        /// <param name="ctx"></param>
        /// <param name="model"></param>
        /// <param name="buffer"></param>
        public void BuildProperties(ModelContext ctx, Model model, StringBuilder buffer)
        {
            foreach (PropertyInfo prop in model.Properties)
            {
                if (prop.CreateCode) 
                    BuildProperty(model, prop, true, buffer);
            }
        }


        /// <summary>
        /// Builds a comma delimited string of roles that can moderate
        /// instances of the model specified.
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public string BuildEditRoles(Model model)
        {
            if (model.ManagedBy == null || model.ManagedBy.Count == 0)
                return string.Empty;

            // Only 1 role.
            if (model.ManagedBy.Count == 1) return model.ManagedBy[0];

            // Build comma delimited list.
            string moderators = model.ManagedBy[0];
            
            for(int ndx = 1; ndx < model.ManagedBy.Count; ndx++)
            {
                moderators += "," + model.ManagedBy[ndx];
            }
            return moderators;
        }
      

        /// <summary>
        /// Build properties.
        /// </summary>
        /// <param name="model"></param>
        /// <param name="buffer"></param>
        /// <returns></returns>
        public void BuildProperty(Model model, PropertyInfo prop, bool usePropType, StringBuilder buffer)
        {
            string indent = GetIndent();    
            string netType = string.Empty;
            if (usePropType && TypeMap.IsBasicNetType(prop.DataType))
            {
                netType = TypeMap.Get<string>(TypeMap.NetFormatToCSharp, prop.DataType.Name);
                buffer.Append(indent + "/// <summary>" + Environment.NewLine);
                buffer.Append(indent + "/// Get/Set " + prop.Name + Environment.NewLine);
                buffer.Append(indent + "/// </summary>" + Environment.NewLine);
                buffer.Append(indent + "public " + netType + " " + prop.Name + " { get; set; }" + Environment.NewLine + Environment.NewLine + Environment.NewLine);
            }
            else
            {
                netType = model.Name;
                // private Address _address = new Address();
                buffer.Append(indent + "private " + netType + " _" + prop.Name + " = new " + netType + "();" + Environment.NewLine);
                buffer.Append(indent + "/// <summary>" + Environment.NewLine);
                buffer.Append(indent + "/// Get/Set " + prop.Name + Environment.NewLine);
                buffer.Append(indent + "/// </summary>" + Environment.NewLine);
                buffer.Append(indent + "public " + netType + " " + prop.Name + Environment.NewLine
                    + indent + " { " + Environment.NewLine
                    + indent + " get { return _" + prop.Name + ";  }" + Environment.NewLine
                    + indent + " set { _" + prop.Name + " = value; }" + Environment.NewLine
                    + indent + " }" + Environment.NewLine + Environment.NewLine + Environment.NewLine);
            }
        }


        public void BuildValidationForStringProperty(Model model, PropertyInfo prop, bool usePropType, StringBuilder buffer)
        {
            string isRequired = prop.IsRequired ? "true" : "false";
            string checkMinLength = prop.CheckMinLength ? "true" : "false";
            string checkMaxLength = prop.CheckMaxLength ? "true" : "false";
            string propName =  "target." + prop.Name;

            // String length.
            string validationCode = "Validation.IsStringLengthMatch({0}, {1}, {2}, {3}, {4}, {5}, {6}, {7} );" + Environment.NewLine;
            string codeLine = string.Format(validationCode, propName, isRequired, checkMinLength, checkMaxLength, prop.MinLength, prop.MaxLength, "errors", prop.Name);
            buffer.Append(codeLine);
        }



        public void BuildValidationForIntProperty(Model model, PropertyInfo prop, bool usePropType, StringBuilder buffer)
        {
            string isRequired = prop.IsRequired ? "true" : "false";
            string checkMinLength = prop.CheckMaxLength ? "true" : "false";
            string checkMaxLength = prop.CheckMaxLength ? "true" : "false";
            string propName = "target." + prop.Name;

            string methodName = "IsStringRegExMatch";
            //if(prop.RegEx == RegexPatterns.Email)
            
            //Validation.IsStringRegExMatch("", isRequired, prop.RegEx, "errors", prop.Name);
            // String length.
            string validationCode = "Validation.IsStringLengthMatch({0}, {1}, {2}, {3}, {4}, {5}, {6}, {7} );" + Environment.NewLine;
            string codeLine = string.Format(validationCode, propName, "true", checkMinLength, checkMaxLength, prop.MinLength, prop.MaxLength, "errors", prop.Name);
            buffer.Append(codeLine);
        }


        /// <summary>
        /// Build all the messagers before and after validation.
        /// </summary>
        /// <param name="ctx"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        private Tuple2<string, string> BuildAllMassagers(ModelContext ctx, Model model)
        {
            if (CollectionUtils.IsEmpty<DataMassageItem>(model.DataMassages))
                return new Tuple2<string, string>(string.Empty, string.Empty);

            string beforeValidationMassagers = BuildMassagers(model, Massage.BeforeValidation);
            string afterValidationMassagers = BuildMassagers(model, Massage.AfterValidation);

            return new Tuple2<string, string>(beforeValidationMassagers, afterValidationMassagers);
        }



        /// <summary>
        /// Build massagers
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        private string BuildMassagers(Model model, Massage massageSequence)
        {
            var massagers = from m in model.DataMassages where m.Sequence == massageSequence select m;
            string code = string.Empty;

            // No need to proceed further.
            if (massagers.Count() == 0) return string.Empty;

            string entityCode = model.Name + " entity = ctx.Item as " + model.Name + ";" + Environment.NewLine;
                
            foreach (DataMassageItem item in massagers)
            {
                string massagerVarName = item.DataMassager.Name.ToLower();
                string massagerName = item.DataMassager.Name;
                // Instance massager
                code = "IEntityMassager " + massagerVarName + " = new " + massagerName + "();";
                code += massagerVarName + ".Massage(entity." + item.PropertyToMassage + ", entityAction);";

                // Static massager ?
                if (item.IsStatic)
                    code = massagerName + ".Massage(entity." + item.PropertyToMassage + ", entityAction);";
            }
            code = entityCode + code + Environment.NewLine;
            return code;
        }
    }
}
