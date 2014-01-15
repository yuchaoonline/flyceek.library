using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Data;
using System.Data.Common;
using System.Security.Cryptography;

using ComLib.Entities;
using ComLib.Membership;
using ComLib;
using ComLib.CsvParse;
using ComLib.Application;
using ComLib.Logging;


namespace ComLib.Samples
{
    /// <summary>
    /// Example of ActiveRecord Initialization/Configuration.
    /// </summary>
    public class Example_Csv : App
    {
        /// <summary>
        /// Initialize.
        /// </summary>
        /// <param name="args"></param>
        public Example_Csv()
        {
        }


        /// <summary>
        /// Run the application.
        /// </summary>
        public override BoolMessageItem Execute()
        {
            // This is a LEXICAL parser.
            // So this can handle quoted and non-quoted values
            // where the separator "," can be inside the quotes such as in 'Art, Classes'
            string csv = "Id, Name,      'Desc'" + Environment.NewLine
                       + "0,  Art,       'Art classes'" + Environment.NewLine
                       + "1,  Painting,  'Any type of painting' " + Environment.NewLine
                       + "2,  Sports,    'Sports classes'" + Environment.NewLine
                       + "3,  Boxing,	 'Boxing classes'";
            CsvDoc doc = new CsvDoc(csv, false);
            
            Logger.Info("====================================================");
            Logger.Info("CSV FILES ");
            Logger.Info("Columns : " + doc.Columns.ToString());
            Logger.Info("Row 1 - Col 1 : " + doc.Data[0][0]);
            Logger.Info("Row 2 - Col 2: " + doc.Data[1][1]);
            Logger.Info("Row 3['Name'] : " + doc.Data[2]["name"].ToString());
            Logger.Info(Environment.NewLine);
            
            // Write out the csv doc to a file.
            // doc.Write(@"C:\temp\test.csv", ",");

            return BoolMessageItem.True;
        }
    }
}
