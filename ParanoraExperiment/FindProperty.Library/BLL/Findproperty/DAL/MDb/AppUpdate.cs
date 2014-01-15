using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;
using FindProperty.Lib.BLL.Findproperty.IDAL;

namespace FindProperty.Lib.BLL.Findproperty.DAL.MDb
{
    public class AppUpdate :IAppUpdate
    {
        public string GetApkUpdateInfo(string filepath)
        {
            string filecontent = string.Empty;
            if (File.Exists(filepath))
            {
                using (FileStream fs = new FileStream(filepath, FileMode.Open, FileAccess.Read))
                {
                    StreamReader sr = new StreamReader(fs);
                    try
                    {
                        filecontent = sr.ReadToEnd();
                    }
                    catch
                    {
                        filecontent = string.Empty;
                    }
                    finally
                    {
                        sr.Close();
                    }
                }
            }
            return filecontent;
        }


        public string GetApkNews(string filepath)
        {
            string filecontent = string.Empty;
            if (File.Exists(filepath))
            {
                using (FileStream fs = new FileStream(filepath, FileMode.Open, FileAccess.Read))
                {
                    StreamReader sr = new StreamReader(fs);
                    try
                    {
                        filecontent = sr.ReadToEnd();
                    }
                    catch
                    {
                        filecontent = string.Empty;
                    }
                    finally
                    {
                        sr.Close();
                    }
                }
            }
            return filecontent;
        }
    }
}
