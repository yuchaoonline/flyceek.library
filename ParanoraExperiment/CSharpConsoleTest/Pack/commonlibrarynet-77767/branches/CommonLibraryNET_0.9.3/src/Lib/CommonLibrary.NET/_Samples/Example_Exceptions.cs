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
using ComLib.Application;
using ComLib.Errors;


namespace ComLib.Samples
{
    /// <summary>
    /// Example of ActiveRecord Initialization/Configuration.
    /// </summary>
    public class Example_Exceptions : App
    {
        /// <summary>
        /// Initialize.
        /// </summary>
        /// <param name="args"></param>
        public Example_Exceptions()
        {
        }


        /// <summary>
        /// Run the application.
        /// </summary>
        public override BoolMessageItem Execute()
        {
            ErrorManager.Register("myErrorManager", false, new CustomExceptionHandler("myErrorManager"));
            Console.WriteLine("====================================================");
            Console.WriteLine("EXCEPTION HANDLING ");

            try
            {
                throw new ArgumentException("exception handling testing");
            }
            catch (Exception ex)
            {
                // Option 1. Use default error handler.
                ErrorManager.Handle("Default error handling.", ex);

                // Option 2. Use custom named error handler "myErrorManager"
                ErrorManager.Handle("Example with custom NAMED error handler.", ex, "myErrorManager");
            }
            Console.WriteLine(Environment.NewLine);
            return BoolMessageItem.True;
        }
    }
}
