using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Data;
using System.Data.Common;


using ComLib.Entities;
using ComLib.Membership;
using ComLib;
using ComLib.Arguments;
using ComLib.Application;


namespace ComLib.Samples
{
    /// <summary>
    /// Example of ActiveRecord Initialization/Configuration.
    /// </summary>
    public class Example_Arguments : App
    {
        /// <summary>
        /// Initialize.
        /// </summary>
        /// <param name="args"></param>
        public Example_Arguments(string[] args)
        {            
        }


        /// <summary>
        /// Run the application.
        /// </summary>
        public override BoolMessageItem  Execute()
        {
            // Sample command line args.
            string[] argList = new string[] { "-config:prod.xml", "-date:${t-1}", "-readonly:true", "myApplicationId" };

            // Option 1. Parse using -, : as prefix/separator.
            Args args = Args.Parse(argList, "-", ":").Item;
            Console.WriteLine("Config : {0},  BusinessDate : {1}, [0] : {2}", args.Named["config"], args.Named["date"], args.Positional[0]);

            // Option 2. Parse args and apply them on an object.
            StartupArgs reciever = new StartupArgs();
            bool accepted = Args.Accept(argList, "-", ":", reciever);
            Console.WriteLine(string.Format("Accepted config : {0}, date : {1}, readonly : {2}, settingsId: {3}",
                              reciever.Config, reciever.BusinessDate, reciever.ReadonlyMode, reciever.DefaultSettingsId));

            // Display -help text. 
            // ( NOTE: -help is automatically interpreted to display args usage).
            ArgsUsage.ShowUsingReciever(Settings.ArgsReciever);
            return BoolMessageItem.True;
        }



        /// <summary>
        /// Sample object that should recieve the arguments.
        /// </summary>
        public class StartupArgs
        {
            [Arg("config", "config file for environment", typeof(string), true, "", "dev.xml")]
            public string Config { get; set; }


            [Arg("date", "The business date", typeof(int), true, "${today}", "${today} | 05/12/2009", true, false, false)]
            public DateTime BusinessDate { get; set; }


            [Arg("readonly", "readonly mode", typeof(bool), false, false, "true | false")]
            public bool ReadonlyMode { get; set; }


            [Arg(1, "Number of categories to display", typeof(int), false, 1, "1 | 2 | 3 etc.")]
            public int CategoriesToDisplay { get; set; }


            [Arg(0, "settings id to load on startup", typeof(string), true, "settings_01", "settings_01")]
            public string DefaultSettingsId { get; set; }
        }
    }
}
