using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using FindProperty.Lib.Common.ValueConvert.Interface;

namespace FindProperty.Lib.Common.ValueConvert
{
    public class DataTableToModeList<TResult> : IDataTableToListModelConverter<TResult> where TResult:class,new()
    {
        private ValueConverter<string> _converter = new ValueConverter<string>(ValueConverterFactoryCreator<string>.Create());

        public DataTableToModeList()
        {

        }

        public List<TResult> Convert(DataTable param)
        {
            List<TResult> result = new List<TResult>();
            Type objType = typeof(object);
            object convertValue = null;
            try
            {
                foreach (DataRow row in param.Rows)
                {
                    TResult item = new TResult();
                    foreach (DataColumn col in param.Columns)
                    {
                        PropertyInfo pinfo = item.GetType().GetProperty(col.ColumnName);
                        string value = row[col.ColumnName].ToString();
                        if (pinfo != null && !string.IsNullOrEmpty(value))
                        {
                            convertValue = _converter.Convert(value, pinfo.PropertyType);
                            if (convertValue != null)
                            {
                                pinfo.SetValue(item, convertValue);
                            }
                        }
                    }
                    result.Add(item);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return result;
        }
    }
}
