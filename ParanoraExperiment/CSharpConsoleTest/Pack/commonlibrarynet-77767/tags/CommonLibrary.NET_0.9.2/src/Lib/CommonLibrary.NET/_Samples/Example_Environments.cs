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
    public class Example_Environments : ApplicationTemplate
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
            // Setup notification on Environment changed.
            EnvironmentCurrent.OnChange += (sender, args) => Console.Write("New Env: " + EnvironmentCurrent.Name);

            // 1. Non-Strict setup indicating current environment is Production.
            Environments.Init("prod", null);
            PrintEnvironment();

            // 2. Non-Strict setup indicating current environment and config references.
            Environments.Init("prod", "prod.config");
            PrintEnvironment();

            // 3. Non-Strict setup indicating current environment and config references.
            Environments.Init("dev", "dev1.config,dev2.config");
            PrintEnvironment();
            
            // 4. Non-Strict setup indicating current environment inheritance and config inheritance.
            Environments.Init("prod,qa,dev", "prod.config,qa.config,dev.config");
            PrintEnvironment();
            EnvironmentCurrent.Change("qa");
            PrintEnvironment();

            // 5. Strict setup indicating current environments selected from the possible environments.
            Environments.Init(GetSampleEvironments(), "Prod");
            PrintEnvironment();

            return BoolMessageItem.True;
        }


        private void PrintEnvironment()
        {
            Logger.Info("====================================================");
            Logger.Info("ENVIRONMENTS ");
            Logger.Info("Environment name: " + EnvironmentCurrent.Name);
            Logger.Info("Environment type: " + EnvironmentCurrent.EnvType);
            Logger.Info("Environment inherits: " + EnvironmentCurrent.InheritancePath);
            Logger.Info("Environment file: " + EnvironmentCurrent.ConfigPath);
            Logger.Info("Environment prod: " + EnvironmentCurrent.IsProd);
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
        public static EnvironmentContext GetSampleEvironments()
        {
            List<EnvItem> env = new List<EnvItem>()
            {
                new EnvItem(){ Name = "Dev",    Source = "File", Path = @"${rootfolder}\dev.config",    DeepInherit = true,  EnvType = EnvironmentType.Dev,       InheritPath = "" },
                new EnvItem(){ Name = "Dev2",   Source = "File", Path = @"${rootfolder}\dev2.config",   DeepInherit = true,  EnvType = EnvironmentType.Dev,       InheritPath = "" },
                new EnvItem(){ Name = "Qa",     Source = "File", Path = @"${rootfolder}\qa.config",     DeepInherit = true,  EnvType = EnvironmentType.Qa,        InheritPath = "Dev" },
                new EnvItem(){ Name = "Prod",   Source = "File", Path = @"${rootfolder}\prod.config",   DeepInherit = true,  EnvType = EnvironmentType.Prod,      InheritPath = "Qa" },
                new EnvItem(){ Name = "Custom", Source = "File", Path = @"${rootfolder}\custom.config", DeepInherit = true,  EnvType = EnvironmentType.MixedProd, InheritPath = "Prod,Dev2" }
            };
            Dictionary<string, string> subs = new Dictionary<string, string>() 
            { 
                { "rootfolder", @"..\..\config" } 
            };
            EnvironmentContext ctx = new EnvironmentContext();
            ctx.Envs = env;
            ctx.Substitutions = subs;
            return ctx;
        }
    }
}
