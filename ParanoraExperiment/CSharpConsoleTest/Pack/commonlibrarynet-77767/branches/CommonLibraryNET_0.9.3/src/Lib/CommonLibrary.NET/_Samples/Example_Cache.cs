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
using ComLib.Caching;
using ComLib.Entities;
using ComLib.Membership;


namespace ComLib.Samples
{
    /// <summary>
    /// Example of ActiveRecord Initialization/Configuration.
    /// </summary>
    public class Example_Cache : App
    {
        /// <summary>
        /// Initialize.
        /// </summary>
        /// <param name="args"></param>
        public Example_Cache()
        {
        }


        /// <summary>
        /// Run the application.
        /// </summary>
        public override BoolMessageItem Execute()
        {
            // Using default ASP.NET Cache.
            
            Cacher.Insert("my_site", "http://www.knowledgedrink.com");
            Cacher.Insert("my_show", "ufc: ultimate fighting");
            Cacher.Insert("my_place", "bahamas", 360, true);

            Console.WriteLine("====================================================");
            Console.WriteLine("CACHE ");
            Console.WriteLine("Obtained from cache : '" + Cacher.Get<string>("my_site") + "'");
            Console.WriteLine("Contains cache for 'my_show' : " + Cacher.Contains("my_show"));
            Console.WriteLine(Environment.NewLine);
            return BoolMessageItem.True;
        }


        /// <summary>
        /// Clear the cache on shutdown of the application.
        /// </summary>
        public override void ShutDown()
        {
            Cacher.Clear();
        }
    }
}
