using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Common;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using ComLib;
using ComLib.Logging;
using ComLib.Models;
using ComLib.Database;


namespace ComLib.CodeGeneration
{
    public class CodeBuilderDb : CodeBuilderBase, ICodeBuilder    
    {
        private ConnectionInfo _conn;
        

        /// <summary>
        /// Default setup.
        /// </summary>
        public CodeBuilderDb() 
        {
            _conn = ConnectionInfo.Default;
        }


        /// <summary>
        /// Initialize using connection.
        /// </summary>
        /// <param name="conn"></param>
        public CodeBuilderDb(ConnectionInfo conn)
        {
            _conn = conn;
        }


        /// <summary>
        /// Creates the models in the database.
        /// </summary>
        /// <param name="ctx"></param>
        /// <returns></returns>
        public BoolMessageItem<ModelContainer> Process(ModelContext ctx)
        {
            StringBuilder bufferSql = new StringBuilder();
            StringBuilder bufferTbl = new StringBuilder();
            StringBuilder bufferProcs = new StringBuilder();

            foreach (Model currentModel in ctx.AllModels.AllModels)
            {
                // Pre condition.
                if (currentModel.GenerateTable)
                {
                    // Create the database table for all the models.
                    List<Model> modelInheritanceChain = ModelUtils.GetModelInheritancePath(ctx, currentModel.Name);
                    
                    // Sort the models to create the columns/properties in a specific order.
                    // For the database, the inheritance chain doesn't really matter.
                    ModelUtils.Sort(modelInheritanceChain);

                    // Create table.
                    DataTable modelTable = ConvertModelChainToTable(currentModel, modelInheritanceChain, ctx);

                    string sql = BuildTableSchema(ctx, modelInheritanceChain, currentModel);
                    
                    // Actually create the model in the database.
                    ExecuteSqlInDb(sql, ctx, currentModel);
                    
                    // Create the stored procedures
                    BoolMessageItem<List<string>> result = CreateStoredProcs(ctx, currentModel);
                    if (result.Success)
                    {
                        ExecuteSqlProcsInDb(result, ctx, currentModel);
                        bufferProcs.Append(result.Item + Environment.NewLine);
                    }
                    
                    bufferSql.Append(sql);                    
                }
            }
            string sqlText = bufferSql.ToString();
            string sqlProcsText = bufferProcs.ToString();
            string sqlFile = ctx.AllModels.Settings.ModelInstallLocation + "install.sql";
            string procsFile = ctx.AllModels.Settings.ModelInstallLocation + "install_procs.sql";
            File.WriteAllText(sqlFile, sqlText);
            File.WriteAllText(procsFile, sqlProcsText);
            return null;
        }


        /// <summary>
        /// Create stored procedures
        /// </summary>
        /// <param name="ctx"></param>
        /// <param name="currentModel"></param>
        private BoolMessageItem<List<string>> CreateStoredProcs(ModelContext ctx, Model currentModel)
        {
            string codeTemplatePath = ctx.AllModels.Settings.ModelDbStoredProcTemplates;
            string[] files = Directory.GetFiles(codeTemplatePath);
            if (files == null || files.Length == 0)
                return new BoolMessageItem<List<string>>(null, false, string.Empty);

            List<FileInfo> fileInfos = new List<FileInfo>();
            Dictionary<string, string> fileMap = new Dictionary<string, string>();

            files.ForEach(f => fileInfos.Add(new FileInfo(f)));

            StringBuilder buffer = new StringBuilder();
            List<string> procs = new List<string>();

            // Get each stored proc and do substitutions.
            foreach (FileInfo file in fileInfos)
            {
                string fileContent = File.ReadAllText(file.FullName);
                
                // Determine stored proc name.
                // 01234567890
                //    cde_ad]
                string nameCheck = @"CREATE PROCEDURE [dbo].[<%= model.TableName %>_";
                int ndxProcName = fileContent.IndexOf(nameCheck);
                ndxProcName = ndxProcName + nameCheck.Length;
                int nextBracket = fileContent.IndexOf("]", ndxProcName);
                string procName = fileContent.Substring(ndxProcName , (nextBracket - ndxProcName));

                fileContent = fileContent.Replace("<%= model.NameSpace %>", currentModel.NameSpace);
                fileContent = fileContent.Replace("<%= model.Name %>", currentModel.Name);
                fileContent = fileContent.Replace("<%= model.TableName %>", currentModel.TableName);

                string dropCommand = "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[<%= model.TableName %>_" + procName + "]') AND type in (N'P', N'PC'))" + Environment.NewLine
                    + "DROP PROCEDURE [dbo].[<%= model.TableName %>_" + procName + "]";

                dropCommand = dropCommand.Replace("<%= model.TableName %>", currentModel.TableName);

                procs.Add(dropCommand);
                procs.Add(fileContent);
                buffer.Append(fileContent + Environment.NewLine);
            }
            return new BoolMessageItem<List<string>>(procs, true, string.Empty);

        }


        /// <summary>
        /// Convert the model chain to database table.
        /// </summary>
        /// <param name="modelInheritanceChain">The list of models representing the inheritance chain, this includes the model
        /// being created. </param>
        /// <param name="modelToCreate">The model being created.</param>
        public static DataTable ConvertModelChainToTable(Model modelToCreate, List<Model> modelInheritanceChain, ModelContext ctx)
        {
            DataTable table = new DataTable();
            List<DataColumn> primaryKeyColumns = new List<DataColumn>();

            // Add all the properties of each inherited model as columns in the table.
            foreach (Model model in modelInheritanceChain)
            {
                BuildTableColumns(model, table, primaryKeyColumns);
            }
            if (modelToCreate.ComposedOf != null && modelToCreate.ComposedOf.Count > 0)
            {
                // Add properties of each composed model as columns in the table.
                foreach (Composition compositionInfo in modelToCreate.ComposedOf)
                {
                    Model model = ctx.AllModels.ModelMap[compositionInfo.Name];
                    BuildTableColumns(model, table, primaryKeyColumns);
                }
            }
            table.PrimaryKey = primaryKeyColumns.ToArray();
            table.TableName = modelToCreate.TableName;
            return table;
        }


        /// <summary>
        /// Prefix to use for Table creation.
        /// </summary>
        public virtual string CreateTablePrefix(Model model)
        {
            return
            "SET ANSI_NULLS ON" + Environment.NewLine +
            //"GO" + Environment.NewLine +
            "SET QUOTED_IDENTIFIER ON" + Environment.NewLine +
            //"GO" + Environment.NewLine +
            "SET ANSI_PADDING ON" + Environment.NewLine;// +
            //"GO" + Environment.NewLine;
        }


        /// <summary>
        /// Prefix to use for Table creation.
        /// </summary>
        public virtual string CreateTableSuffix(Model model)
        {
            int clobPropertyCount = model.Properties.Count<PropertyInfo>( (p) => p.DataType == typeof(StringClob));

            //var clobProperties = from p in model.Properties where p.DataType == typeof(StringClob) select p;
            string textImageOn = clobPropertyCount > 0 ? "TEXTIMAGE_ON [PRIMARY]" : string.Empty;

            string suffix = "," + Environment.NewLine +
            " CONSTRAINT [PK_" + model.TableName + "] PRIMARY KEY CLUSTERED " + Environment.NewLine +
            "( [Id] ASC" + Environment.NewLine +
            " )WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]" + Environment.NewLine +
            " ) ON [PRIMARY] " + textImageOn + Environment.NewLine;// +
            //"GO" + Environment.NewLine + Environment.NewLine + Environment.NewLine + Environment.NewLine;
            return suffix;
        }

        
        private string BuildTableSchema(ModelContext ctx, List<Model> modelInheritanceChain, Model modelToCreate)
        {
            StringBuilder buffer = new StringBuilder();
            buffer.Append(CreateTablePrefix(modelToCreate));
            buffer.Append("CREATE TABLE [dbo].[" + modelToCreate.TableName + "]( " + Environment.NewLine);

            List<PropertyInfo> allProps = new List<PropertyInfo>();
            modelInheritanceChain.ForEach(m => allProps.AddRange(m.Properties));
            
            if( modelToCreate.ComposedOf != null)
                modelToCreate.ComposedOf.ForEach( c => allProps.AddRange(ctx.AllModels.ModelMap[c.Name].Properties));

            if (modelToCreate.Includes != null)
                modelToCreate.Includes.ForEach(m => allProps.AddRange(ctx.AllModels.ModelMap[m.Name].Properties));
            
            // Add all the properties of each model as columns in the table.
            for(int ndx = 0; ndx < allProps.Count; ndx++)
            {
                PropertyInfo prop = allProps[ndx];
                if (prop.CreateColumn)
                {
                    string columnInfo = string.Empty;

                    // First column.
                    if (ndx == 0 && !prop.IsKey)
                    {
                        columnInfo = BuildColumnInfo(prop);
                    }
                    else if (prop.IsKey)
                    {
                        columnInfo = BuildColumnInfoForKey(prop);
                        if (ndx != 0)
                            columnInfo = "," + Environment.NewLine + columnInfo;
                    }
                    else
                    {
                        columnInfo = "," + Environment.NewLine + BuildColumnInfo(prop);
                    }
                    buffer.Append(columnInfo);
                }
            }
            buffer.Append(CreateTableSuffix(modelToCreate));
            string sql = buffer.ToString();
            return sql;
        }


        public virtual string BuildColumnInfo(PropertyInfo prop)
        {
            string columnInfo = "[{0}] [{1}]{2} {3}";
            string sqlType = TypeMap.Get<string>(TypeMap.SqlServer, prop.DataType.Name);
            string length = prop.DataType == typeof(string) ? "(" + prop.MaxLength + ")" : string.Empty;
            string nullOption = prop.IsRequired ? "NOT NULL" : "NULL";
            columnInfo = string.Format(columnInfo, prop.ColumnName, sqlType, length, nullOption);
            return columnInfo;
        }


        public virtual string BuildColumnInfoForKey(PropertyInfo prop)
        {
            // [Id] [bigint] IDENTITY(1,1) NOT NULL
            string columnInfo = "[{0}] [{1}] {2} {3}";
            string sqlType = TypeMap.Get<string>(TypeMap.SqlServer, prop.DataType.Name);
            string indentity = "IDENTITY(1,1)";
            columnInfo = string.Format(columnInfo, prop.ColumnName, sqlType, indentity, "NOT NULL");
            return columnInfo;
        }


        /// <summary>
        /// Create table in the database.
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="ctx"></param>
        /// <param name="currentModel"></param>
        public virtual void ExecuteSqlInDb(string sql, ModelContext ctx, Model currentModel)
        {
            DbCreateType createType = ctx.AllModels.Settings.DbAction_Create;
            DBSchema helper = new DBSchema(_conn);
            try
            {
                if (createType == DbCreateType.DropCreate)
                {
                    helper.DropTable(currentModel.TableName);
                }
                helper.ExecuteNonQuery(sql, CommandType.Text, null);                
            }
            catch (Exception ex)
            {
                Logger.Error("Error creating tables for model : " + currentModel.Name + " table name : " + currentModel.TableName);
                Logger.Error(ex.Message);
            }
        }


        /// <summary>
        /// Create table in the database.
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="ctx"></param>
        /// <param name="currentModel"></param>
        public virtual void ExecuteSqlProcsInDb(BoolMessageItem<List<string>> procs, ModelContext ctx, Model currentModel)
        {
            DbCreateType createType = ctx.AllModels.Settings.DbAction_Create;
            DBSchema helper = new DBSchema(_conn);
            try
            {
                foreach (string sql in procs.Item)
                {
                    helper.ExecuteNonQuery(sql, CommandType.Text, null);
                }
            }
            catch (Exception ex)
            {
                Logger.Error("Error creating tables for model : " + currentModel.Name + " table name : " + currentModel.TableName);
                Logger.Error(ex.Message);
            }
        }


        private static void BuildTableColumns(Model model, DataTable table, List<DataColumn> primaryKeyColumns)
        {
            foreach (PropertyInfo prop in model.Properties)
            {
                DataColumn column = new DataColumn();

                // Right now only handle simple datatypes.
                if (TypeMap.IsBasicNetType(prop.DataType))
                {
                    column.ColumnName = string.IsNullOrEmpty(prop.ColumnName) ? prop.Name : prop.ColumnName;
                    column.DataType = prop.DataType;
                    column.AllowDBNull = !prop.IsRequired;
                    column.Unique = prop.IsUnique;
                    if (prop.IsKey)
                    {
                        primaryKeyColumns.Add(column);
                    }
                    table.Columns.Add(column);
                }
            }
        }
    }



    public class CodeBuilderDbRowMapper
    {
        /// <summary>
        /// The tabs to use.
        /// </summary>
        public string Tab { get; set; }


        /// <summary>
        /// Build the entire row mapper.
        /// </summary>
        /// <param name="ctx"></param>
        /// <param name="model"></param>
        /// <param name="buffer"></param>
        /// <param name="modelInheritanceChain"></param>
        /// <returns></returns>
        public string BuildRowMapper(ModelContext ctx, Model model, List<Model> modelInheritanceChain)
        {
            // Job entity = Jobs.New();
            StringBuilder buffer = new StringBuilder();
            string code = string.Empty;
            
            // Build property mapping for each inherited model.
            foreach (Model inheritedModel in modelInheritanceChain)
            {
                code = BuildRowMapperProperties(ctx, "entity", model, inheritedModel.Properties);
                buffer.Append(code);
            }

            // Build property mapping for each inherited model.
            if (model.Includes != null && model.Includes.Count > 0)
            {
                foreach (Include include in model.Includes)
                {
                    Model includedModel = ctx.AllModels.ModelMap[include.Name];
                    code = BuildRowMapperProperties(ctx, "entity", model, includedModel.Properties);
                    buffer.Append(code);
                }
            }

            // Build property mapping for each Composed of model.
            if (model.ComposedOf != null && model.ComposedOf.Count > 0)
            {
                foreach (Composition composedModelName in model.ComposedOf)
                {
                    Model composedModel = ctx.AllModels.ModelMap[composedModelName.Name];
                    code = string.Format("entity.{0} = new {1}();", composedModelName.Name, composedModelName.Name);
                    buffer.Append(code + Environment.NewLine);

                    // entity.Address
                    code = BuildRowMapperProperties(ctx, "entity." + composedModelName.Name, composedModel, composedModel.Properties);
                    buffer.Append(code);
                }
            }
            string mappingCode = buffer.ToString();
            return mappingCode;
        }


        /// <summary>
        /// Build row mapper.
        /// </summary>
        /// <param name="ctx"></param>
        /// <param name="model"></param>
        /// <param name="buffer"></param>
        /// <param name="props"></param>
        /// <returns></returns>
        public string BuildRowMapperProperties(ModelContext ctx, string entityName, Model model, List<PropertyInfo> props)
        {
            // Job entity = Jobs.New();
            string code = string.Empty;
            StringBuilder buffer = new StringBuilder();
            buffer.Append(code);

            // Create mapping for each property
            foreach (PropertyInfo prop in props)
            {
                if (prop.DataType == typeof(string))
                    code = BuildMappingCode(prop, entityName, "{0}.{1} = reader[\"{2}\"] == DBNull.Value ? string.Empty : reader[\"{3}\"].ToString();");
                
                else if (prop.DataType == typeof(int))
                    code = BuildMappingCode(prop, entityName, "{0}.{1} = reader[\"{2}\"] == DBNull.Value ? 0 : (int)reader[\"{3}\"];");

                else if (prop.DataType == typeof(bool))
                    code = BuildMappingCode(prop, entityName, "{0}.{1} = reader[\"{2}\"] == DBNull.Value ? false : (bool)reader[\"{3}\"];");

                else if (prop.DataType == typeof(DateTime))
                    code = BuildMappingCode(prop, entityName, "{0}.{1} = reader[\"{2}\"] == DBNull.Value ? DateTime.MinValue : (DateTime)reader[\"{2}\"];");

                else if (prop.DataType == typeof(long))
                    code = BuildMappingCode(prop, entityName, "{0}.{1} = reader[\"{2}\"] == DBNull.Value ? 0 : (long)reader[\"{2}\"];");

                else if (prop.DataType == typeof(double))
                    code = BuildMappingCode(prop, entityName, "{0}.{1} = reader[\"{2}\"] == DBNull.Value ? 0 : Convert.ToDouble(reader[\"{2}\"]);");


                buffer.Append(code + Environment.NewLine);
            }
            return buffer.ToString();
        }


        private string BuildMappingCode(PropertyInfo prop, string entityName, string codeLine)
        {
            // reader["Description"] == DBNull.Value ? string.Empty : reader["Description"].ToString();
            string code = string.Format(codeLine, entityName, prop.Name, prop.ColumnName, prop.ColumnName);
            return Tab + code;
        }
    }
}
