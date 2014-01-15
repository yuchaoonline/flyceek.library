using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.IO;
using System.Collections;
using System.Collections.Specialized;
using System.Net.Mail;


namespace CommonLibrary
{
    /// <summary>
    /// Base class for the Batch application.
    /// </summary>
    public class ApplicationTemplate : IApplicationTemplate, IDisposable
    {
        /// <summary>
        /// Boiler plate code to run an application.
        /// 1. Accept / validate command line arguments.
        /// 2. Initialize the application
        /// 3. Execute the application.
        /// 4. Shutdown the application.
        /// </summary>
        /// <param name="app">The application to run.</param>
        /// <param name="args">Command line arguments.</param>
        public static BoolMessageItem Run(IApplicationTemplate app, string[] args)
        {
            // Validation.
            if (app == null) throw new ArgumentNullException("ApplicationTemplate to run was not supplied.");
            BoolMessageItem result = null;

            try
            {
                // Validate the arguments.
                result = app.AcceptArgs(args);
                if (!result.Success) return result;

                // Initalize.
                app.Init();
                app.InitComplete();

                // Execute.
                result = app.Execute();
                app.ExecuteComplete();
            }
            catch (Exception ex)
            {
                ExecuteHelper.HandleException(ex);
            }
            finally
            {            
                app.ShutDown();
            }            
            return result;
        }


        #region Constructors
        /// <summary>
        /// Default construction.
        /// </summary>
        public ApplicationTemplate()
        {
            Settings = new ApplicationTemplateSettings();
            Settings.StartTime = DateTime.Now;
        }
        #endregion


        #region IBatchApplication Members
        /// <summary>
        /// Get the arguments that were supplied on the command line.
        /// </summary>
        public string[] Options
        {
            get { return Settings.CommandLineArgs; }
        }


        /// <summary>
        /// The configuration for this application.
        /// </summary>
        public IConfigSource Conf
        {
            get { return _config; }
            set { _config = value; }
        }


        /// <summary>
        /// The instance of the logger to use for the application.
        /// </summary>
        public ILogMulti Log
        {
            get { return _log; }
            set { _log = value; }
        }


        /// <summary>
        /// The instance of the email service.
        /// </summary>
        public IEmailService Emailer
        {
            get { return _emailer; }
            set { _emailer = value; }
        }


        /// <summary>
        /// The result of the execution.
        /// </summary>
        public BoolMessageItem Result
        {
            get { return _result; }
            set { _result = value; }
        }


        /// <summary>
        /// By default only supports the --pause option.
        /// </summary>
        public virtual List<ArgAttribute> OptionsSupported
        {
            get
            {
                // Get all arguments supported from argument reciever's attributes.
                if (Settings.ArgsRequired && Settings.ArgsReciever != null)                
                    return ArgsHelper.GetArgsFromReciever(Settings.ArgsReciever);

                // Return default settings supplied.
                return new List<ArgAttribute>()
                {
                    new ArgAttribute("pause", "Pause the application to attach debugger", typeof(bool), false, false, "true|false", false, false, true),
                };
            }
        }


        /// <summary>
        /// By default run the application without any arguments.
        /// </summary>
        public virtual List<string> OptionsExamples
        {
            get 
            {
                string appName = string.Empty;
                if (!string.IsNullOrEmpty(Settings.AppName))
                    appName = Settings.AppName;
                else
                {
                    FileInfo fileInfo = new FileInfo(Assembly.GetEntryAssembly().Location);
                    appName = fileInfo.Name;
                }
                return ArgsUsage.BuildSampleRuns(appName, OptionsSupported); 
            }
        }


        /// <summary>
        /// Determine if the arguments can be accepted.
        /// </summary>
        /// <param name="args"></param>
        /// <returns>True if success. False otherwise.</returns>        
        public virtual BoolMessageItem AcceptArgs(string[] args)
        {
            Settings.CommandLineArgs = args;
            
            // Are the args required?
            if (args == null || args.Length == 0 && !Settings.ArgsRequired)
                return new BoolMessageItem(new Args(), true, "Arguments not required.");

            /// Pause the execution of sojara.
            /// This allows us to attach the debugger to sojara.commodities.exe
            /// and start debugging from the very beginning.
            if (args != null && args.Length > 0 && args[0].StartsWith("-pause:"))
            {
                Console.WriteLine("Paused...");
                Console.ReadKey();
            }

            // Determine if the arguments are specified correctly.
            BoolMessageItem<Args> result = ArgsHelper.Accept(args, "-", ":", 1, OptionsSupported, OptionsExamples);

            // Successful ? Set apply the args to the reciever if applicable
            if (result.Success && Settings.ArgsReciever != null)
            {
                ArgsHelper.Accept(args, "-", ":", Settings.ArgsReciever);
            }

            return result;
        }


        /// <summary>
        /// Determine if the arguments can be accepted.
        /// </summary>
        /// <param name="args"></param>
        /// <returns>True if success. False otherwise.</returns>        
        public virtual bool Accept(string[] args)
        {
            return AcceptArgs(args).Success;
        }


        /// <summary>
        /// Initialize the application.
        /// </summary>        
        public virtual void Init()
        {
            Init(null);
        }


        /// <summary>
        /// Initialize with contextual data.
        /// </summary>
        /// <param name="context"></param>
        public virtual void Init(object context)
        {
            // Nothing to do in the base class yet.
        }


        /// <summary>
        /// On initialization complete and before execution begins.
        /// </summary>
        public virtual void InitComplete()
        {
            DisplayStart();
        }


        /// <summary>
        /// Execute the application without any arguments.
        /// </summary>
        public virtual BoolMessageItem Execute()
        {
            return Execute(null);
        }


        /// <summary>
        /// Execute the application with context data.
        /// </summary>
        /// <param name="context"></param>
        public virtual BoolMessageItem Execute(object context)
        {
            return new BoolMessageItem(0, true, string.Empty);
        }


        /// <summary>
        /// Used to perform some post execution processing before
        /// shutting down.
        /// </summary>
        public virtual void ExecuteComplete()
        {
        }


        /// <summary>
        /// Shutdown the application.
        /// </summary>
        public virtual void ShutDown()
        {
        }


        /// <summary>
        /// Send an email notification.
        /// </summary>
        public virtual void SendEmail()
        {
            // Check for null.
            var msg = new NotificatonMessage(new Dictionary<string, string>(), "", "", "", "");            
            msg.To = Conf.Get<string>("EmailSettings", "emailTo");
            msg.From = Conf.Get<string>("EmailSettings", "emailFrom");
            msg.Subject = Conf.Get<string>("EmailSettings", "emailSubject");
            SendEmail(msg);
        }


        /// <summary>
        /// Display information at the start of the application.
        /// </summary>
        public virtual void DisplayStart()
        {
            Display(true, new OrderedDictionary());
        }


        /// <summary>
        /// Display information at the end of the application.
        /// </summary>
        public virtual void DisplayEnd()
        {
            Display(false, new OrderedDictionary());
        }


        /// <summary>
        /// Send an email at the end of the completion of the application.
        /// </summary>
        /// <param name="msg"></param>
        public virtual void SendEmail(NotificatonMessage msg)
        {
            bool sendEmail = Conf.GetDefault<bool>("EmailSettings", "enableEmails", false);
            bool sendEmailOnlyOnFailure = Conf.GetDefault<bool>("EmailSettings", "enableEmailsOnlyOnFailures", true);
            if ((sendEmail && !sendEmailOnlyOnFailure) ||
                (sendEmail && sendEmailOnlyOnFailure && !_result.Success))
            {
                _emailer.Send(msg);
            }
        }


        /// <summary>
        /// Display information about the application.
        /// </summary>
        /// <param name="isStart"></param>
        /// <param name="summaryInfo">The key/value pairs can be supplied
        /// if this is derived and the derived class wants to add additional
        /// summary information.</param>
        public virtual void Display(bool isStart, IDictionary summaryInfo)
        {
            if(summaryInfo == null) summaryInfo = new OrderedDictionary();
            FileInfo fileInfo = new FileInfo(Assembly.GetEntryAssembly().Location);
            string header = isStart ? "Application Start" : "Application End";
            string envName = EnvironmentCurrent.Selected == null ? "" : EnvironmentCurrent.Name;
            string envPath = EnvironmentCurrent.Selected == null ? "" : EnvironmentCurrent.ConfigPath;
            string envType = EnvironmentCurrent.Selected == null ? "" : EnvironmentCurrent.EnvType.ToString();
            
            // Version/Machine/Evnrionment summary.
            summaryInfo["Location"] = fileInfo.Directory.FullName;
            summaryInfo["Version"] = this.GetType().Assembly.GetName().Version.ToString();;
            summaryInfo["Machine"] = Environment.MachineName;
            summaryInfo["User"] = AuthService.UserName;
            summaryInfo["StartTime"] = Settings.StartTime.ToString();
            summaryInfo["EndTime"] = DateTime.Now.ToString();
            summaryInfo["Duration"] = (DateTime.Now - Settings.StartTime).ToString();
            summaryInfo["Diagnostics"] = "Diagnostics.log";
            summaryInfo["Args"] = Environment.CommandLine;
            summaryInfo["Env Type"] = envType;
            summaryInfo["Env Name"] = envName;
            summaryInfo["Config"] = envPath;

            // Get the log summary.
            List<string> logSummary = Logger.GetLogInfo();
            for (int ndx = 0; ndx < logSummary.Count; ndx++) 
                summaryInfo["Log " + ndx + 1] = logSummary[ndx];

            // Get arguments that were applied to the argument reciever.
            if (Settings.ArgsReciever != null
                && Settings.ArgsRequired && Settings.TransferArgsToReciever)
                ArgsHelper.GetArgValues(summaryInfo, Settings.ArgsReciever);

            // Print the summary.
            // - Version          : 1.0.0.0
            // - Machine          : KISHORE1
            // - User             : kishore1\kishore
            Log.Info("--------------------------------------------------- ", null, null);
            Log.Info(header + " Information ----------------------------: ", null, null);
            StringHelpers.DoFixedLengthPrinting(summaryInfo, 4, (key, val) => Log.Info(" - " + key + " : " + val, null, null));
            Log.Info("--------------------------------------------------- ", null, null);
        }

        #endregion


        #region Public Properties
        /// <summary>
        /// Settings.
        /// </summary>
        public ApplicationTemplateSettings Settings { get; set; }


        /// <summary>
        /// Determines whether or not the argument reciever is capable of recieving the arguments.
        /// </summary>
        public bool IsArgumentRecieverApplicable
        {
            get { return Settings.ArgsReciever != null && Settings.ArgsRequired && Settings.TransferArgsToReciever; }
        }
        #endregion


        #region IDisposable Members
        /// <summary>
        /// Currently disposing.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }


        /// <summary>
        /// Overloaded dispose method indicating if dispose was 
        /// called explicitly.
        /// </summary>
        /// <param name="disposing"></param>
        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                ShutDown();
            }
        }


        /// <summary>
        /// Finalization.
        /// </summary>
        ~ApplicationTemplate()
        {
            // Simply call Dispose(false).
            Dispose(false);
        }
        #endregion


        #region Private Data
        protected ILogMulti _log;
        protected IConfigSource _config;        
        protected IEmailService _emailer;
        protected BoolMessageItem _result = BoolMessageItem.False;
        #endregion
    }
}
