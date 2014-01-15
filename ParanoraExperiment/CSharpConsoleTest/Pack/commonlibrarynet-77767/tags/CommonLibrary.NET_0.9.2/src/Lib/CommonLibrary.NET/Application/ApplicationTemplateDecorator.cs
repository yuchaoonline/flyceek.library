using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.IO;
using System.Collections;


namespace CommonLibrary
{    
    /// <summary>
    /// Base class for the Batch application.
    /// </summary>
    public class ApplicationTemplateDecorator : IApplicationTemplate
    {
        private IApplicationTemplate _instance;
        private ApplicationTemplateDecorationHelper _helper;
        private string _decorators;
        private BoolMessageItem _result;


        /// <summary>
        /// Initialize the underlying instance.
        /// </summary>
        /// <param name="instance"></param>
        public ApplicationTemplateDecorator(string delimitedDecorators, IApplicationTemplate instance)
        {
            _instance = instance;
            _decorators = delimitedDecorators;
            InitDecorations();
        }


        /// <summary>
        /// Initialize
        /// </summary>
        public void InitDecorations()
        {
            _helper = new ApplicationTemplateDecorationHelper(_instance.GetType(), _decorators);
        }


        #region IApplicationTemplate Members
        /// <summary>
        /// Get all the options.
        /// </summary>
        public string[] Options
        {
            get { return _instance.Options; }
        }
        

        /// <summary>
        /// Get all the options that are supported.
        /// </summary>
        public List<ArgAttribute> OptionsSupported
        {
            get { return _instance.OptionsSupported; }
        }


        /// <summary>
        /// Get examples of the command line options.
        /// </summary>
        public List<string> OptionsExamples
        {
            get { return _instance.OptionsExamples; }
        }


        /// <summary>
        /// The config source for the application.
        /// </summary>
        public IConfigSource Conf
        {
            get { return _instance.Conf; }
            set { _instance.Conf = value; }
        }


        /// <summary>
        /// The logger for the application.
        /// </summary>
        public ILogMulti Log
        {
            get { return _instance.Log; }
            set { _instance.Log = value; }
        }


        /// <summary>
        /// Result of the execution.
        /// </summary>
        public BoolMessageItem Result
        {
            get { return _instance.Result; }
            set { _instance.Result = value; }
        }


        /// <summary>
        /// The Emailer for this application.
        /// </summary>
        public IEmailService Emailer
        {
            get { return _instance.Emailer; }
            set { _instance.Emailer = value; }
        }


        /// <summary>
        /// Accept arguments.
        /// </summary>
        /// <param name="args">Command line args.</param>
        /// <returns></returns>
        public BoolMessageItem AcceptArgs(string[] args)
        {
            return _instance.AcceptArgs(args);
        }


        /// <summary>
        /// Accept args.
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        public bool Accept(string[] args)
        {
            return _instance.Accept(args);
        }


        /// <summary>
        /// Initialize application.
        /// </summary>
        public void Init()
        {
            _helper.Execute("Init()", "", false, () => _instance.Init());
        }


        /// <summary>
        /// Initialize 
        /// </summary>
        /// <param name="context"></param>
        public void Init(object context)
        {
            _helper.Execute("Init(context)", "", false, () => _instance.Init(context));
        }

        
        /// <summary>
        /// Initialization complete.
        /// </summary>
        public void InitComplete()
        {
            _helper.Execute("InitComplete()", "", false, () => _instance.InitComplete());
        }


        /// <summary>
        /// Execute the core logic.
        /// </summary>
        public BoolMessageItem Execute()
        {
            return ExecuteInternal(() => _instance.Execute());
        }


        /// <summary>
        /// Execute the core logic.
        /// </summary>
        /// <param name="context"></param>
        public BoolMessageItem Execute(object context)
        {
            return ExecuteInternal(() => _instance.Execute(context));
        }


        /// <summary>
        /// Execute Complete.
        /// </summary>
        public void ExecuteComplete()
        {
            _helper.Execute("ExecuteComplete", "", false, () => _instance.ExecuteComplete());
        }


        /// <summary>
        /// Shutdown.
        /// </summary>
        public void ShutDown()
        {
            // Shutdown services and application.
            _helper.Execute("ShutDown()", "", true, () =>
            {
                RunDiagnostics();
                _instance.ShutDown();
                _instance.DisplayEnd();
                SendEmail();
            });            
            ShutdownServices();
        }


        /// <summary>
        /// Send emails only if the decoration is enabled.
        /// </summary>
        public void SendEmail()
        {
            if (_helper.IsDecoratedWith("email"))
                _helper.Execute("SendEmail()", "Emailing", true, () => _instance.SendEmail());
        }


        /// <summary>
        /// Display start.
        /// </summary>
        public void DisplayStart()
        {
            _helper.Execute("DisplayStart()", "", true, () => _instance.DisplayStart());
        }


        /// <summary>
        /// Display end.
        /// </summary>
        public void DisplayEnd()
        {
            _helper.Execute("DisplayEnd()", "", true, () => _instance.DisplayEnd());
        }
        #endregion


        /// <summary>
        /// Executes the application.execute method via a lamda and logs the success/failure.
        /// </summary>
        /// <param name="action"></param>
        /// <returns></returns>
        protected BoolMessageItem ExecuteInternal(Func<BoolMessageItem> action)
        {
            string methodName = _instance.GetType().FullName + ".Execute";
            _helper.Execute("Execute(context)", "", () =>
            {
                _result = ExecuteHelper.TryCatchLogGetBoolMessageItem("Error in " + methodName, Logger.Default, action);            
            });
            
            // Handle possiblity of applicationTemplate returning null for result.
            if (_result == null)
                _result = new BoolMessageItem(null, false, _instance.GetType().Name + " returned null result, converting this to a failure result.");
            
            string message = _result.Success ? "Successful" : "Failed : " + _result.Message;
            Logger.Info(methodName + " : " + message);
            _instance.Result = _result;
            return _result;
        }


        /// <summary>
        /// Run Diagnostics.
        /// </summary>
        protected void RunDiagnostics()
        {
            if (_helper.IsDecoratedWith("diagnostics"))
                Diagnostics.WriteInfo("MachineInfo,AppDomain,Env_System,Env_User", "Diagnostics.txt");
        }


        /// <summary>
        /// Shutdown various Services.
        /// </summary>
        protected void ShutdownServices()
        {
            Logger.ShutDown();
            Cacher.Clear();
        }
    }
}
