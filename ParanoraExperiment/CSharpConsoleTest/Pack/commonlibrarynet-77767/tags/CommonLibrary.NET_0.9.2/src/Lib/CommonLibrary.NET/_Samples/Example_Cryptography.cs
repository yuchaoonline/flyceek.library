using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Data;
using System.Data.Common;
using System.Security.Cryptography;
using CommonLibrary;
using CommonLibrary.DomainModel;
using CommonLibrary.Membership;


namespace CommonLibrary.Samples
{
    /// <summary>
    /// Example of ActiveRecord Initialization/Configuration.
    /// </summary>
    public class Example_Cryptography : ApplicationTemplate
    {
        /// <summary>
        /// Initialize.
        /// </summary>
        /// <param name="args"></param>
        public Example_Cryptography()
        {
        }


        /// <summary>
        /// Run the application.
        /// </summary>
        public override BoolMessageItem Execute()
        {
            // 1. Encrypt using default provider. ( Symmetric TripleDes )
            string plainText = "www.knowledgedrink.com";
            string encrypted = Cryptographer.Encrypt(plainText);
            string decrypted = Cryptographer.Decrypt(encrypted);

            Console.WriteLine("====================================================");
            Console.WriteLine("CRYPTOGRAPHY ");
            Console.WriteLine("Encrypted : " + plainText + " to " + encrypted);
            Console.WriteLine("Decrypted : " + encrypted + " to " + decrypted);
            Console.WriteLine(Environment.NewLine);

            // 2. Use non-static encryption provider.
            ICryptographyService crypto = new CryptographerServiceHash("commonlib.net", new MD5CryptoServiceProvider());
            string hashed = crypto.Encrypt("my baby - 2002 honda accord ex coupe");
            Console.WriteLine(hashed);

            // 3. Change the crypto provider on the static helper.
            ICryptographyService crypto2 = new CryptographerServiceSymmetric("new key", new TripleDESCryptoServiceProvider());
            Cryptographer.Init(crypto2);
            string encryptedWithNewKey = Cryptographer.Encrypt("www.knowledgedrink.com");
            Console.WriteLine(string.Format("Encrypted text : using old key - {0}, using new key - {1}", encrypted, encryptedWithNewKey));
            return BoolMessageItem.True;
        }
    }
}
