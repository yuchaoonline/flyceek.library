using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CommonLibrary
{
    /// <summary>
    /// Settings for the application template.
    /// </summary>
    public class ApplicationTemplateSettings
    {
        public string AppName { get; set; }
        public string[] CommandLineArgs { get; set; }
        public DateTime StartTime { get; set; }
        public object ArgsReciever { get; set; }
        public bool TransferArgsToReciever { get; set; }
        public bool ArgsRequired { get; set; }
        public bool SendEmailOnCompletion { get; set; }
    }
}
