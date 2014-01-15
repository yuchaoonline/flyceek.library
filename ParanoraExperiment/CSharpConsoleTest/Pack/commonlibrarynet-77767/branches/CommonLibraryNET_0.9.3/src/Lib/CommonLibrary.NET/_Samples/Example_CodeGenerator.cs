using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Data;
using System.Data.Common;
using System.Security.Cryptography;

using ComLib;
using ComLib.Membership;
using ComLib.Entities;
using ComLib.Collections;
using ComLib.Database;
using ComLib.Application;
using ComLib.LocationSupport;
using ComLib.ValidationSupport;
using ComLib.Models;
using ComLib.CodeGeneration;



namespace ComLib.Samples
{
    /// <summary>
    /// Example of ActiveRecord Initialization/Configuration.
    /// </summary>
    public class Example_CodeGenerator : App
    {
        /// <summary>
        /// Initialize.
        /// </summary>
        /// <param name="args"></param>
        public Example_CodeGenerator()
        {
        }


        /// <summary>
        /// Run the application.
        /// </summary>
        public override BoolMessageItem Execute()
        {
            ModelContainer models = GetModelContainer();
            ModelContext ctx = new ModelContext() { AllModels = models };
            IList<ICodeBuilder> builders = new List<ICodeBuilder>()
            {
                // This generates the Database tables in SQL - Server.
                // You connection string in ModelBuilderSettings.Connection must be set.
                // new CodeBuilderDb(ctx.AllModels.Settings.Connection),
                new CodeBuilderDomain(),              
            };
            BoolMessage message = CodeBuilder.Process(ctx, builders);
            Console.WriteLine("Code generation Sucess : {0} - {1}", message.Success, message.Message);
            return BoolMessageItem.True;
        }



        private ModelContainer GetModelContainer()
        {
            // Settings for the Code model builders.
            ModelBuilderSettings settings = new ModelBuilderSettings()
            {
                ModelCodeLocation = @"Generated\src",
                ModelInstallLocation = @"Generated\install",
                ModelCodeLocationTemplate = @"..\..\..\..\lib\CommonLibrary.NET\CodeGen\Templates\Default",
                ModelDbStoredProcTemplates = @"..\..\..\..\lib\CommonLibrary.NET\CodeGen\Templates\DefaultSql",
                DbAction_Create = DbCreateType.Create,
                Connection = new ConnectionInfo("Server=myserver1;Database=mydb;User=user1;Password=password;", "System.Data.SqlClient"),
                AssemblyName = "CommonLibrary.Extensions"
            };

            ModelContainer models = new ModelContainer()
            {
                Settings = settings,
                ExtendedSettings = new Dictionary<string, object>() { },
                
                // Model definition.
                AllModels = new List<Model>()
                {
                    new Model("ModelBase")
                    {
                        NameSpace = "CommonLibrary.WebModules",
                        GenerateCode = false,
                        GenerateTable = false,
                        GenerateOrMap = false,
                        PropertiesSortOrder = 1,
                        Properties = new List<PropertyInfo>()
                        {
                            new PropertyInfo( "Id",            typeof(int)      ) { IsRequired = true, ColumnName = "Id", IsKey = true },
                            new PropertyInfo( "CreateDate",    typeof(DateTime) ) { IsRequired = true },
                            new PropertyInfo( "UpdateDate",    typeof(DateTime) ) { IsRequired = true },
                            new PropertyInfo( "CreateUser",    typeof(string)   ) { IsRequired = true, MaxLength = "20" },
                            new PropertyInfo( "UpdateUser",    typeof(string)   ) { IsRequired = true, MaxLength = "20" },
                            new PropertyInfo( "UpdateComment", typeof(string)   ) { IsRequired = false, MaxLength = "150" },
                            new PropertyInfo( "Version",       typeof(int)      ) { IsRequired = true, DefaultValue = 1 },
                            new PropertyInfo( "IsActive",      typeof(bool)     ) { IsRequired = true, DefaultValue = 0 }
                        }
                    }, 
                    new Model("RatingPostBase")
                    {
                        NameSpace = "CommonLibrary.WebModules",
                        Inherits = "ModelBase",
                        GenerateCode = false,
                        GenerateTable = false,
                        GenerateOrMap = false,
                        PropertiesSortOrder = 100,
                        Properties = new List<PropertyInfo>()
                        {
                            new PropertyInfo( "AverageRating",     typeof(double) ) { IsRequired = false },
                            new PropertyInfo( "TotalLiked",        typeof(int)    ),
                            new PropertyInfo( "TotalDisLiked",     typeof(int)    ),
                            new PropertyInfo( "TotalBookMarked",   typeof(int)    ),
                            new PropertyInfo( "TotalAbuseReports", typeof(int)    )
                        }
                    },
                    new Model("Address")
                    {
                        Properties = new List<PropertyInfo>()
                        {
                            new PropertyInfo( "Street",    typeof(string)) { MaxLength = "40" },
                            new PropertyInfo( "City",      typeof(string)) { MaxLength = "40" },
                            new PropertyInfo( "State",     typeof(string)) { MaxLength = "20" },
                            new PropertyInfo( "Country",   typeof(string)) { MaxLength = "20", DefaultValue = "U.S." },
                            new PropertyInfo( "Zip",       typeof(string)) { MaxLength = "10" },
                            new PropertyInfo( "CityId",    typeof(int) )   {  },
                            new PropertyInfo( "StateId",   typeof(int) )   {  },
                            new PropertyInfo( "CountryId", typeof(int) )   {  },
                            new PropertyInfo( "IsOnline",  typeof(bool))   { DefaultValue = false }
                        }
                    },
                    new Model("Event")
                    {
                        TableName = "Events",
                        NameSpace = "CommonLibrary.WebModules.Events",
                        GenerateCode = true, GenerateTests = false, 
                        GenerateUI = false, GenerateRestApi = false, GenerateFeeds = false, GenerateOrMap = false,
                        PropertiesSortOrder = 50,
                        Inherits = "ModelBase",
                        Includes     = new List<Include>()         { new Include("RatingPostBase") },
                        ComposedOf   = new List<Composition>()     { new Composition("Address") },
                        Validations  = new List<ValidationItem>()  { new ValidationItem("Address", typeof(SimpleAddressValidator)) { IsStatic = false} },
                        DataMassages = new List<DataMassageItem>() { new DataMassageItem("Address", typeof(SimpleAddressDataMassager), Massage.AfterValidation) { IsStatic = false} },
                        RepositoryType = "RepositorySql",
                        ExcludeFiles = "Repository.cs,Feeds.cs,ImportExport.cs,Serializer.cs",
                        Properties = new List<PropertyInfo>()
                        {
                            new PropertyInfo("Title",       typeof(string))     { ColumnName = "Title", MinLength = "10", MaxLength = "150", IsRequired = true },
                            new PropertyInfo("Summary",     typeof(string))     { MaxLength = "200", IsRequired = true },
                            new PropertyInfo("Description", typeof(StringClob)) { MinLength = "10", MaxLength = "-1", IsRequired = true },
                            new PropertyInfo("StartDate",   typeof(DateTime))   { IsRequired = true },
                            new PropertyInfo("EndDate",     typeof(DateTime))   { IsRequired = true},
                            new PropertyInfo("StartTime",   typeof(DateTime)),
                            new PropertyInfo("EndTime",     typeof(DateTime)),
                            new PropertyInfo("IsOnline",    typeof(bool))       { DefaultValue = false },
                            new PropertyInfo("Email",       typeof(string))     { IsRequired = false, MaxLength = "30",  RegEx = "RegexPatterns.Email", IsRegExConst = true },
                            new PropertyInfo("Phone",       typeof(string))     { IsRequired = false, MaxLength = "20",  RegEx = "RegexPatterns.PhoneUS", IsRegExConst = true },
                            new PropertyInfo("Url",         typeof(string))     { IsRequired = false, MaxLength = "150", RegEx = "RegexPatterns.Url", IsRegExConst = true },
                            new PropertyInfo("Keywords",    typeof(string))     { MaxLength = "100"}
                        },                        
                    }
                }            
            };
            return models;
        }        
    }



    public class SimpleAddressValidator : ValidatorBase
    {
        /// <summary>
        /// Do some basic validation. This is just a DUMMY validator
        /// to show how to incorporation the validator into the code generator.
        /// </summary>
        /// <param name="validationEvent"></param>
        /// <returns></returns>
        protected override bool ValidateInternal(ValidationEvent validationEvent)
        {
            Address address = validationEvent.Target as Address;
            IValidationResults errors = validationEvent.Results;
            int initialErrorCount = errors.Count;

            if (string.IsNullOrEmpty(address.City))
                errors.Add("City is empty");

            if (string.IsNullOrEmpty(address.State))
                errors.Add("State is empty");

            if (string.IsNullOrEmpty(address.Country))
                errors.Add("Country is emtpy");
            else if (address.Country.Trim().ToLower() != "usa")
                errors.Add("Only supports 'usa' for now.");


            return initialErrorCount == errors.Count;
        }
    }


    public class SimpleAddressDataMassager : IEntityMassager
    {
        #region IEntityMassager Members

        /// <summary>
        /// Massage the data after validation by setting the id's 
        /// of the city/state/country.
        /// 
        /// This is just a dummy example to show how to incorporate a datamassager
        /// automatically into the code generation.
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="action"></param>
        public void Massage(object entity, EntityAction action)
        {
            Address address = entity as Address;
            address.CityId = 1; // queens.
            address.CountryId = 230; // USA
            address.StateId = 7; // NY
        }

        #endregion
    }
}
