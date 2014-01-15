using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Data;
using System.Data.Common;


using ComLib.Entities;
using ComLib.Membership;
using ComLib;
using ComLib.Application;


namespace ComLib.Samples
{
    /// <summary>
    /// Example of ActiveRecord Initialization/Configuration.
    /// </summary>
    public class Example_ActiveRecord : App
    {
        /// <summary>
        /// Initialize.
        /// </summary>
        /// <param name="args"></param>
        public Example_ActiveRecord()
        {
        }


        /// <summary>
        /// Configure the ActiveRecord for Accounts
        /// NOTES: - All service objects can be constructed with a repository, validator, settings object.            
        ///        - All service objects can be constructed with a fake repository ( in memory ) for testing.
        /// 
        /// POSSIBLE CONFIGURATIONS:
        /// 1. Singleton account service as is.            
        /// 2. Singleton account service and enable repository connection configuration.
        /// 3. Factory method for creating account service and enable repository connection configuration.
        /// 4. Factory methods for creating service, repository, validator and enabling repository connection configuration.
        /// </summary>
        /// <param name="context"></param>
        public override void Init(object context)
        {
            string config = (context == null) ? "factory_service" : (string)context;
            string columnsToIndex = "Id,UserName,UserNameLowered,Email,EmailLowered,Password";
            IEntityRepository<Account> repository = new EntityRepositoryInMemory<Account>(columnsToIndex);

            switch (config)
            {
                case "singleton_service":
                    Accounts.Init(new AccountService(repository, new AccountValidator(), new AccountSettings()));
                    break;
                case "singleton_service_db":
                    Accounts.Init(new AccountService(repository, new AccountValidator(), new AccountSettings()), true);
                    break;
                case "factory_service":
                    Accounts.Init(() => new AccountService(repository, new AccountValidator(), new AccountSettings()), true);
                    break;
                case "factory_all":
                    Accounts.Init(() => new AccountService(), () => new EntityRepositoryInMemory<Account>(columnsToIndex), () => new AccountValidator(), true);
                    break;
                default: // "factory_service"
                    Accounts.Init(() => new AccountService(repository, new AccountValidator(), new AccountSettings()), true);
                    break;
            }
        }


        /// <summary>
        /// Run the application.
        /// </summary>
        public override BoolMessageItem  Execute()
        {
            // 1. Create.
            Account account = new Account("kdog", "password", "kishore@abc.com", true);
            Accounts.Create(account);
            
            // 2. Retrieve.
            Account justCreated = Accounts.Get(account.Id).Item;
            Console.WriteLine("Created {0}, Retrieved {1}", account.UserName, justCreated.UserName);

            // 3. Update
            justCreated.UpdateComment = "testing update";
            Accounts.Update(justCreated);
            Account afterUpdate = Accounts.Get(justCreated.Id).Item;
            Console.WriteLine(afterUpdate.UpdateComment);
            
            // 4. Validate.
            // Create another account with same data. 
            // Will get error that same username/email exists.
            BoolResult<Account> result = Accounts.Create(justCreated);
            Console.WriteLine(result.Message);

            // 5. Delete
            Accounts.Delete(justCreated.Id);
            if(Accounts.Get(justCreated.Id).Item == null) 
                Console.WriteLine("deleted");

            return BoolMessageItem.True;
        }
    }
}
