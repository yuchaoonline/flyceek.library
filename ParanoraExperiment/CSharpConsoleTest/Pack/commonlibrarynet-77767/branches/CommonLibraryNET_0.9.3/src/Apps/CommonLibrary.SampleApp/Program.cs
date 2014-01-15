using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Data;
using System.Data.Common;
using ComLib;
using ComLib.Application;
using ComLib.Logging;
using ComLib.CsvParse;


namespace ComLib.Samples
{
    public class SampleAppRunner
    {        
        /// <summary>
        /// Sample application runner.
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            string text = "'Id', 'Name',   'Desc'" + Environment.NewLine
                        + "'0',  'Art',    'Art class'" + Environment.NewLine
                        + "'1',  'Sports', 'all sports'";
            var cols = new List<string>(){ "Id", "Name", "Desc" };
            var data = new List<List<string>>(){ new List<string>() { "0",  "Baseball", "MLB"},
                                                          new List<string>() { "1",  "Football", "NFL"} };

            Csv.Write("StockData.csv", data, ";", cols);



            IApp app = new Example_Logging();
            try
            {                
                if (!app.Accept(args)) return;

                app.Init();
                app.Execute();
                app.ShutDown();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error : " + ex.Message);
                Console.WriteLine("Error : " + ex.StackTrace);
            }

            Console.WriteLine("Finished... Press enter to exit.");
            Console.ReadKey();
        }


        /// <summary>
        /// Sample application runner.
        /// Does pretty much the same thing as the above.
        /// But the above is shown just to display the API/Usage of ApplicationTemplate.
        /// </summary>
        /// <param name="args"></param>
        static void RunWithTemplateCall(IApp app, string[] args)
        {
            App.Run(app, args);
        }
    }
}
