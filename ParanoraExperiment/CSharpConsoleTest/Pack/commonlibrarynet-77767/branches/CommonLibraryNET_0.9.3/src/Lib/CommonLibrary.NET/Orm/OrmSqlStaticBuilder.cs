using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using ComLib.Models;


namespace ComLib.Orm
{
    /// <summary>
    /// Orm sql builder.
    /// </summary>
    public abstract class OrmSqlStaticBuilder
    {
        protected string _databaseType;
        protected ModelContext _ctx;
        protected string _modelName;
        protected string _tableName;
        protected int _tabCount;
        protected string _tab;


        /// <summary>
        /// Unitialized instance.
        /// </summary>
        public OrmSqlStaticBuilder() { }


        /// <summary>
        /// Initialize
        /// </summary>
        /// <param name="databaseType"></param>
        /// <param name="ctx"></param>
        /// <param name="modelName"></param>
        public OrmSqlStaticBuilder(ModelContext ctx, string modelName, string tableName, int tabCount)
        {
            Init( ctx, modelName, tableName, tabCount);
        }            
            
        
        /// <summary>
        /// Initialize
        /// </summary>
        /// <param name="ctx"></param>
        /// <param name="modelName"></param>
        /// <param name="tableName"></param>
        public void Init(ModelContext ctx, string modelName, string tableName, int tabCount)
        {
            _tabCount = tabCount;
            _ctx = ctx;
            _modelName = modelName;
            _tableName = string.IsNullOrEmpty(tableName) ? modelName + "s" : tableName;

            for (int ndx = 0; ndx < _tabCount; ndx++)
                _tab += "\t";
        }


        /// <summary>
        /// Generate the sql for insert.
        /// </summary>
        /// <returns></returns>
        public virtual string Insert()
        {
            StringBuilder buffer = new StringBuilder();
            List<string> columnNames = new List<string>();
            List<string> columnValues = new List<string>();
            SetupColumns(columnNames, columnValues);
            
            string sql = "\"insert into " + _tableName + " ( " + columnNames[0];
            for (int ndx = 1; ndx < columnNames.Count; ndx++)
            {
                sql += ", " + columnNames[ndx];
                if (ndx % 5 == 0)
                {
                    sql += "\" + " + Environment.NewLine + _tab + "\"";
                }
            }            
            sql += ") \" + " + Environment.NewLine + " \"VALUES (\" + " + columnValues[0];
            for (int ndx = 1; ndx < columnValues.Count; ndx++)
            {
                sql += " + \",\" + " + columnValues[ndx];
                if (ndx % 4 == 0) sql +=  Environment.NewLine + _tab;
            }
            sql += "+ \");" + IdentityRetrievalStatement + "\"";

            return sql;
        }


        /// <summary>
        /// Provide the statement for getting the inserted entity id(sql identity).
        /// </summary>
        public abstract string IdentityRetrievalStatement { get; }


        /// <summary>
        /// Generate the sql for insert.
        /// </summary>
        /// <returns></returns>
        public virtual string Update()
        {
            StringBuilder buffer = new StringBuilder();
            List<string> columnNames = new List<string>();
            List<string> columnValues = new List<string>();

            SetupColumns(columnNames, columnValues);

            string sql = "\"update " + _tableName + string.Format(" set {0} = \" + {1} + ", columnNames[0], columnValues[0]);
            for (int ndx = 1; ndx < columnValues.Count; ndx++)
            {
                sql += string.Format("\", {0} = \" + {1} + ", columnNames[ndx], columnValues[ndx]);
                if (ndx % 5 == 0) sql += Environment.NewLine + _tab;
            }

            sql += " \" where Id = \" + entity.Id;";
            return sql;
        }


        /// <summary>
        /// Setup the columns.
        /// </summary>
        /// <param name="columnNames"></param>
        /// <param name="columnValues"></param>
        public void SetupColumns(List<string> columnNames, List<string> columnValues)
        {
            Process((objProp, model, prop) =>
            {
                columnNames.Add(prop.ColumnName);
                //string objPropName = string.IsNullOrEmpty(objProp) ? "entity." : "entity." + objProp + ".";
                columnValues.Add(GetValue("entity.", prop));
            });
        }


        /// <summary>
        /// Process the model, by handling all it's properties, compositions, and includes and calling
        /// the delegate given  to handle the properties.
        /// </summary>
        /// <param name="propHandler"></param>
        public virtual void Process(Action<string, Model, PropertyInfo> propHandler)
        {
            ModelIterator iterator = new ModelIterator();
            
            // Now add the columns of the model itself.
            iterator.OnPropertyProcess += new Func<ModelContext, Model, PropertyInfo, bool>((ctx, model, prop) =>
            {
                string objProp = string.Empty;
                if (model.Name != _modelName)
                    objProp = model.Name;

                if (prop.Name != "Id")
                {
                    propHandler(objProp, model, prop);
                }
                return true;
            });
            
            // Now add the columns of the composite model
            iterator.OnCompositeProcess += new Func<ModelContext, Model, Composition, bool>((ctx, model, composition) =>
            {
                iterator.ProcessModel(ctx, composition.ModelRef);
                return true;
            });

            // Now add the columns of the composite model
            iterator.OnIncludeProcess += new Func<ModelContext, Model, Include, bool>((ctx, model, include) =>
            {
                iterator.ProcessModel(ctx, include.ModelRef);
                return true;
            });
            iterator.Process(_ctx, _modelName);
        }


        /// <summary>
        /// Get the value.
        /// </summary>
        /// <param name="objectPropName"></param>
        /// <param name="prop"></param>
        /// <returns></returns>
        public virtual string GetValue(string objectPropName, PropertyInfo prop)
        {
            if (prop.DataType == typeof(string))
                return string.Format("\"'\" + DataUtils.Encode({0}) + \"'\"", objectPropName + prop.Name);

            else if (prop.DataType == typeof(DateTime))
                return "\"'\" + " + objectPropName + prop.Name + ".ToString(\"yyyy-MM-dd\") + \"'\"";

            else if (prop.DataType == typeof(bool))
                return " Convert.ToSByte(" + objectPropName + prop.Name + ") ";

            else if (prop.DataType == typeof(int) || prop.DataType == typeof(long)
                    || prop.DataType == typeof(float) || prop.DataType == typeof(double))
                return " " + objectPropName + prop.Name + " ";

            return " " + objectPropName + prop.Name + " ";
        }
    }



    public class OrmSqlStaticBuilderFactory
    {
        public static OrmSqlStaticBuilder GetBuilder(string providerName)
        {
            return new OrmSqlStaticBuilderSqlServer();
        }
    }



    /// <summary>
    /// Sql builder for Sql Server.
    /// </summary>
    public class OrmSqlStaticBuilderSqlServer : OrmSqlStaticBuilder
    {
        /// <summary>
        /// Unitialized instance.
        /// </summary>
        public OrmSqlStaticBuilderSqlServer() { } 


        /// <summary>
        /// Initialize
        /// </summary>
        /// <param name="databaseType"></param>
        /// <param name="ctx"></param>
        /// <param name="modelName"></param>
        public OrmSqlStaticBuilderSqlServer(ModelContext ctx, string modelName, string tableName, int tabCount)
            : base(ctx, modelName, tableName, tabCount)
        {
            _databaseType = "System.Data.SqlClient";
        }


        /// <summary>
        /// Provide the statement for getting the inserted entity id(sql identity).
        /// </summary>
        public override string IdentityRetrievalStatement
        {
            get { return "select scope_identity();"; }
        }        
    }
}
