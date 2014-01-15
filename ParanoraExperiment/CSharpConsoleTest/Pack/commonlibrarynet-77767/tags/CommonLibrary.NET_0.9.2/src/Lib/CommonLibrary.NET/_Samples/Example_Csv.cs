using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Data;
using System.Data.Common;
using System.Security.Cryptography;
using CommonLibrary;
using CommonLibrary.DomainModel;
using CommonLibrary.Membership;


namespace CommonLibrary.Samples
{
    /// <summary>
    /// Example of ActiveRecord Initialization/Configuration.
    /// </summary>
    public class Example_Csv : ApplicationTemplate
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
            CsvDocument doc = new CsvDocument(csv, false);
            doc.ParseLists();

            Logger.Info("====================================================");
            Logger.Info("CSV FILES ");
            Logger.Info("Columns : " + doc.Columns.ToString());
            Logger.Info("Row 1 : " + doc.RecordsList[0].AsDelimited<string>(","));
            Logger.Info("Row 2 : " + doc.RecordsList[1].AsDelimited<string>(","));
            Logger.Info("Row 3['Name'] : " + doc.RecordsList[2][1]);
            Logger.Info(Environment.NewLine);
            return BoolMessageItem.True;
        }
    }
}
