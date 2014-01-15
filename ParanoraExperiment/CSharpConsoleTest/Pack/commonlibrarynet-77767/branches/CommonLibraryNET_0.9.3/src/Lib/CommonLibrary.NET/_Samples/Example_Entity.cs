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
using ComLib.Entities;
using ComLib.Application;


namespace ComLib.Samples
{
    /// <summary>
    /// Example of ActiveRecord Initialization/Configuration.
    /// </summary>
    public class Example_Entity : App
    {
        /// <summary>
        /// Initialize.
        /// </summary>
        /// <param name="args"></param>
        public Example_Entity()
        {
        }


        public class Person : Entity
        {
            public string Name { get; set; }
        }


        /// <summary>
        /// Run the application.
        /// </summary>
        public override BoolMessageItem Execute()
        {
            Console.WriteLine("====================================================");
            Console.WriteLine("Entity ");

            IEntity entity = new Person() { Name = "kishore" };

            Console.WriteLine("Id, Persistant and Audit fields.");
            Console.WriteLine("These fields are set by EntityService if using CommonLibrary DomainModel Services.");

            Console.WriteLine("Id            ", entity.Id);
            Console.WriteLine("IsPersistant  ", entity.IsPersistant());            
            Console.WriteLine("CreateDate    ", entity.CreateDate);
            Console.WriteLine("CreateUser    ", entity.CreateUser);
            Console.WriteLine("UpdateDate    ", entity.UpdateDate);
            Console.WriteLine("UpdateUser    ", entity.UpdateUser);
            Console.WriteLine("UpdateComment ", entity.UpdateComment);
            return BoolMessageItem.True;
        }
    }
}
