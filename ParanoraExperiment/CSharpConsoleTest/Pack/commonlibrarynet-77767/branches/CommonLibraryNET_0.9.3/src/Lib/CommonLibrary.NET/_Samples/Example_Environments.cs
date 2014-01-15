using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Data;
using System.Data.Common;
using System.Security.Cryptography;
using ComLib;
using ComLib.Application;
using ComLib.Logging;
using ComLib.Environments;


namespace ComLib.Samples
{
    /// <summary>
    /// Example of ActiveRecord Initialization/Configuration.
    /// </summary>
    public class Example_Environments : App
    {
        /// <summary>
        /// Initialize.
        /// </summary>
        /// <param name="args"></param>
        public Example_Environments()
        {
        }


        /// <summary>
        /// Run the application.
        /// </summary>
        public override BoolMessageItem Execute()
        {
            //// Setup notification on Environment changed.
            Env.OnChange += (sender, args) => Console.Write("New Env: " + Env.Name);

            // Use 1: Set to "prod"(PRODUCTION) with default available envs as "prod,uat,qa,dev".
            Envs.Set("prod");
            PrintEnvironment();

            // Use 2: Set to "qa"(QA) with default available envs as "prod,qa,dev".
            Envs.Set("qa", "prod,qa,dev");
            PrintEnvironment();

            // Use 3: Set to "dev"(DEVELOPMENT) with default available envs as "prod,uat,qa,dev".
            //        Also set the "dev" env RefPath to "dev.config".
            Envs.Set("dev", "prod,uat,qa,dev", "dev.config");
            PrintEnvironment();

            // Use 4: Env Set up & Configuration File Setup with Inheritance
            //        - Set env to "newyork.prod"(PRODUCTION) with default available envs as "newyork.prod,london.prod,qa,dev".
            //        - Also set the "newyork.prod" env RefPath to use 3 config files "ny.prod.config,prod.config,dev.config.
            //        - Config file sequence is an inheritance of files ny.prod.config (inherits) prod.config (inherits) dev.config
            Envs.Set("newyork.prod", "newyork.prod,london.prod,qa,dev", "newyork.prod.config,prod.config,dev.config");
            PrintEnvironment();

            // Use 5: Set up the environment using Built objects. 
            Envs.Set("Dev2", GetSampleEvironments(), "");
            PrintEnvironment();


            // Use 6: Change the environment from the last one ("Dev2").
            Env.Change("Qa");
            PrintEnvironment();
            return BoolMessageItem.True;
        }


        private void PrintEnvironment()
        {
            Logger.Info("====================================================");
            Logger.Info("ENVIRONMENTS ");
            Logger.Info("Environment name: "     + Env.Name);
            Logger.Info("Environment type: "     + Env.EnvType);
            Logger.Info("Environments #  : "     + Env.Count);
            Logger.Info("Environment inherits: " + Env.Inherits);
            Logger.Info("Environment file: "     + Env.RefPath);
            Logger.Info("Environment IsProd: "   + Env.IsProd);
            Logger.Info("Environment IsQa: "     + Env.IsProd);
            Logger.Info("Environment IsDev: "    + Env.IsProd);
            Logger.Info(Environment.NewLine);
        }


        /// <summary>
        /// This builds a datastructure of all the environments supported
        /// and the links to the config files for each environment 
        /// and how they are inherited.
        /// 
        /// THIS CAN BE LOADED FROM AN XML, JSON, YAML, INI file or whatever.
        /// </summary>
        /// <returns></returns>
        public static List<EnvItem> GetSampleEvironments()
        {
            List<EnvItem> envs = new List<EnvItem>()
            {
                new EnvItem(){ Name = "Dev",    RefPath =@"${rootfolder}\dev.config",    InheritsDeeply = true,  EnvType = EnvType.Dev,       Inherits = "" },
                new EnvItem(){ Name = "Dev2",   RefPath =@"${rootfolder}\dev2.config",   InheritsDeeply = true,  EnvType = EnvType.Dev,       Inherits = "" },
                new EnvItem(){ Name = "Qa",     RefPath =@"${rootfolder}\qa.config",     InheritsDeeply = true,  EnvType = EnvType.Qa,        Inherits = "Dev" },
                new EnvItem(){ Name = "Prod",   RefPath =@"${rootfolder}\prod.config",   InheritsDeeply = true,  EnvType = EnvType.Prod,      Inherits = "Qa" },
                new EnvItem(){ Name = "Custom", RefPath =@"${rootfolder}\custom.config", InheritsDeeply = true,  EnvType = EnvType.MixedProd, Inherits = "Prod,Dev2" }
            };
            return envs;
        }
    }
}
