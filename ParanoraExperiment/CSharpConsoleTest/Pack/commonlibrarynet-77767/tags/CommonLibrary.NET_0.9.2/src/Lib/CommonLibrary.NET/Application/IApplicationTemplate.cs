using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.IO;


namespace CommonLibrary
{
    /// <summary>
    /// Interface for any console/batch application.
    /// </summary>
    public interface IApplicationTemplate
    {
        /// <summary>
        /// Get the command line arguments that were actually supplied.
        /// </summary>
        string[] Options{ get; }


        /// <summary>
        /// The configuration for this application.
        /// </summary>
        IConfigSource Conf { get; set;  }


        /// <summary>
        /// The logger for this application.
        /// </summary>
        ILogMulti Log { get; set; }


        /// <summary>
        /// The emailer for this application.
        /// </summary>
        IEmailService Emailer { get; set; }


        /// <summary>
        /// Get the definition of command line options that
        /// are acceptable for this application.
        /// </summary>
        List<ArgAttribute> OptionsSupported { get; }


        /// <summary>
        /// The result of the execution.
        /// </summary>
        BoolMessageItem Result { get; set; }


        /// <summary>
        /// Get a list of examples that show how to launch this application.
        /// </summary>
        List<string> OptionsExamples { get; }


        /// <summary>
        /// Determine if the string[] arguments (command line arguments) can be accepted.
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        BoolMessageItem AcceptArgs(string[] args);


        /// <summary>
        /// Determine if the string[] arguments (command line arguments) can be accepted.
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        bool Accept(string[] args);


        /// <summary>
        /// Initialize the application.
        /// </summary>
        void Init();


        /// <summary>
        /// Initialize with some contextual data.
        /// </summary>
        /// <param name="context"></param>
        void Init(object context);


        /// <summary>
        /// Perform some post initialization processing.
        /// and before execution begins.
        /// </summary>
        void InitComplete();
        

        /// <summary>
        /// Execute the application without any arguments.
        /// </summary>
        BoolMessageItem Execute();


        /// <summary>
        /// Execute the application with context data.
        /// </summary>
        /// <param name="context"></param>
        BoolMessageItem Execute(object context);


        /// <summary>
        /// Used for performing some post execution processing before
        /// shutting down the application.
        /// </summary>
        void ExecuteComplete();


        /// <summary>
        /// Shutdown the application.
        /// </summary>
        void ShutDown();


        /// <summary>
        /// Send an email after the application execution completed.
        /// </summary>
        void SendEmail();


        /// <summary>
        /// Display the start of the application.
        /// </summary>
        void DisplayStart();


        /// <summary>
        /// Display the end of the application.
        /// </summary>
        void DisplayEnd();
    }
}
