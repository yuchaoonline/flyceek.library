﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.IO;
using System.Collections;
using CommonLibrary;


namespace CommonLibrary.StockMarketApp
{
    /// <summary>
    /// Object that should recieve the command line arguments.
    /// </summary>
    public class StockMarketAppArgs
    {
        /// <summary>
        /// What environment to run this in.
        /// </summary>
        /// <remarks>This can also be inherited.
        /// e.g. prod,qa,dev. ( Prod inherits from Qa inherits from Dev ).</remarks>
        [Arg("env", "Environment to run in", typeof(string), true, "dev", "dev", "dev | qa | prod | prod,qa,dev")]
        public string Envrionment { get; set; }


        /// <summary>
        /// The list of configuration files to load.
        /// 1. Load single config: prod.config
        /// 2. Load config inheritance: prod.config,qa.config,dev.config.
        /// </summary>
        [Arg("config", "Config file for environment", typeof(string), true, @"config\prod.config", "dev.config", "dev.config | qa.config | prod.config")]
        public string Config { get; set; }


        /// <summary>
        /// This is NOT required because if logfile is empty, the logfile
        /// file will be defaulted to {environment}.log.
        /// e.g. dev.log qa.log where "dev" and "qa" is the environment name above.
        /// </summary>
        [Arg("log", "Log file to write to", typeof(string), false, "{env}.log", "app.log", "app.log | myapp.log")]
        public string LogFile { get; set; }


        /// <summary>
        /// The business date to get stock market data for.
        /// </summary>
        [Arg("date", "The business date", typeof(int), true, "${today}", "${today}", "${today} | 05/12/2009", true, false, false)]
        public DateTime BusinessDate { get; set; }


        /// <summary>
        /// Whether or not to store the stock market data into internal business systems.
        /// This effectively is useful for testing purposes.
        /// </summary>
        [Arg("dryrun", "Flag to store", typeof(bool), false, false, "false", "true | false")]
        public bool DryRun { get; set; }


        /// <summary>
        /// Indicates where to get the market data from.
        /// Either "Bloomberg" or "Reuters"
        /// </summary>
        [Arg("source", "Data source to get market data from.", typeof(string), false, "Bloomberg", "Bloomberg", "Bloomberg | Reuters")]
        public string DataSource { get; set; }


        /// <summary>
        /// How many symbols to process each time. e.g. 10 symbols per batch.
        /// </summary>
        [Arg(0, "Number of symbols to process at one time.", typeof(int), true, 10, "10", "10 | 20 | 30 etc.")]
        public string BatchSize { get; set; }
    }



    /// <summary>
    /// Full sample application using the CommonLibrary.NET framework.
    /// </summary>
    public class StockMarketApplication : ApplicationTemplate
    {
        /// <summary>
        /// Run the application via it's interface ( Init - Execute - Shutdown )
        /// using the static Run utility method.
        /// </summary>
        /// <param name="args">command line arguments.
        /// e.g. -env:Prod,Dev -date:${today-1} -config:config\prod.config,config\dev.config -source:Reuters 10</param>
        static int Main(string[] args)
        {
            string decorators = "log,diagnostics,email";
            return Run(new ApplicationTemplateDecorator(decorators, new StockMarketApplication()), args).AsExitCode();
        }


        /// <summary>
        /// Set the settings to indictate that 
        /// 1. Command line arguments are required
        /// 2. Command line args should be transferred to args reciever.
        /// </summary>
        public StockMarketApplication()
        {
            Settings.ArgsReciever = new StockMarketAppArgs();
            Settings.ArgsRequired = true;
            Settings.TransferArgsToReciever = true;
        }


        /// <summary>
        /// INHERITANCE
        /// 1. -env:Prod,Dev -date:${today}   -config:prod.config,dev.config -log:{env}.log -source:Bloomberg 10
        /// 2. -env:Prod,Dev -date:${today-1} -config:prod.config,dev.config -log:{env}.log -source:Reuters 10
        /// </summary>
        public override void Init()
        {
            StockMarketAppArgs args = Settings.ArgsReciever as StockMarketAppArgs;

            // 1. Initialize the environment. prod, prod.config
            Environments.Init(args.Envrionment, args.Config);
            
            // Initialize log file path e.g. Prod.log.
            if (string.IsNullOrEmpty(args.LogFile)) 
                args.LogFile = string.Format("{0}.log", EnvironmentCurrent.EnvType);

            // 2. Append the file based logger.
            Logger.Default.Append(new LoggerFileBased("env_log", args.LogFile));

            // 3. Initialize config inheritance from environment inheritance.
            Config.Init(EnvironmentUtils.LoadConfigs(EnvironmentCurrent.Inheritance));
         
            // 4. Set the config and logger, and emailer instances on the application.
            _config = Config.Current;
            _log = Logger.Default;
            _emailer = new EmailService(_config, "EmailServiceSettings");
        }


        /// <summary>
        /// Dislay the startup / end information.
        /// </summary>
        /// <remarks>Just overriding to show how to extend the summary display with 
        /// some additional key/values.
        /// </remarks>
        /// <param name="isStart"></param>
        public override void Display(bool isStart, IDictionary summaryInfo)
        {
            // The base method displays important information such as starttime, endtime, duration, etc.
            summaryInfo["RunType"] = "batch_mode";
            base.Display(isStart, summaryInfo);
        }


        /// <summary>
        /// Execute the core logic of the application.
        /// </summary>
        /// <remarks>Note this does not need to be inside of a try-catch 
        /// if using the ApplicationDecorator.</remarks>
        public override BoolMessageItem Execute()
        {
            Log.Info("Executing application : " + Conf.Get<string>("Global", "ApplicationName"), null, null);                        
            _result = BoolMessageItem.True;            
            return _result;
        }


        /// <summary>
        /// Shutdown the application.
        /// </summary>
        public override void ShutDown()
        {
            Log.Info("Shutting down", null, null);
        }


        /// <summary>
        /// Just showing how to override and populate any other sub-stitution
        /// values for the template.
        /// </summary>
        /// <param name="msg"></param>
        public override void SendEmail(NotificatonMessage msg)
        {
            // Allow the base class to send the email.
            msg.Values["application"] = Conf.Get<string>("Application", "name");
            msg.Subject = _result.Success ? "StockMarketApp Successful" : "StockMarketApp FAILED";
            msg.Body = _result.Success ? "Success" : _result.Message;               
            base.SendEmail(msg);
        }
    }
}
