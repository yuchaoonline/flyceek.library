/*
 * Author: Kishore Reddy
 * Url: http://commonlibrarynet.codeplex.com/
 * Title: CommonLibrary.NET
 * Copyright: � 2009 Kishore Reddy
 * License: LGPL License
 * LicenseUrl: http://commonlibrarynet.codeplex.com/license
 * Description: A C# based .NET 3.5 Open-Source collection of reusable components.
 * Usage: Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */
using System;
using System.Security.Cryptography;
using System.Text; 
using System.Configuration;

namespace CommonLibrary
{

    /// <summary>
    /// Cryptography service to encrypt and decrypt strings.
    /// </summary>
    public class SymmetricCryptoService : ISymmetricCryptoService
	{
        private EncryptionSettings _encryptionOptions;

        /// <summary>
        /// Default options
        /// </summary>
        public SymmetricCryptoService()
        {
            _encryptionOptions = new EncryptionSettings(true);
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="SymmetricCryptoService"/> class.
        /// </summary>
        /// <param name="options">The options.</param>
        public SymmetricCryptoService(EncryptionSettings options)
        {
            _encryptionOptions = options;
        }


        /// <summary>
        /// Options for encryption.
        /// </summary>
        /// <value></value>
        public EncryptionSettings Settings
        {
            get { return _encryptionOptions; }
        }


		/// <summary>
		/// Encrypts the plaintext using an internal private key.
		/// </summary>
		/// <param name="plaintext">The text to encrypt.</param>
		/// <returns>An encrypted string in base64 format.</returns>
		public string Encrypt( string plaintext )
		{
            if(!_encryptionOptions.Encrypt)
                return plaintext;

            string base64Text = SymmetricCrypto.EncryptTripleDES(plaintext, _encryptionOptions.InternalKey);
			return base64Text;
		}


		/// <summary>
		/// Decrypts the base64key using an internal private key.
		/// </summary>
		/// <param name="base64Text">The encrypted string in base64 format.</param>
		/// <returns>The plaintext string.</returns>
        public string Decrypt(string base64Text)
		{
            if(!_encryptionOptions.Encrypt)
                return base64Text;

            string plaintext = SymmetricCrypto.DecryptTripleDES(base64Text, _encryptionOptions.InternalKey);
			return plaintext;
		}
	}



    /// <summary>
    /// Simple Cryptographic Services
    /// </summary>
    internal class SymmetricCrypto
    {

        /// <summary>
        /// Generates a cryptographic Hash Key for the provided text data.
        /// Basically a digital fingerprint
        /// </summary>
        /// <param name="dataToHash">text to hash</param>
        /// <returns>Unique hash representing string</returns>
        public static String GetMD5Hash(String dataToHash)
        {
            String hexResult = "";
            string[] tabStringHex = new string[16];
            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] data = Encoding.ASCII.GetBytes(dataToHash);
            byte[] result = md5.ComputeHash(data);
            for (int i = 0; i < result.Length; i++)
            {
                tabStringHex[i] = (result[i]).ToString("x");
                hexResult += tabStringHex[i];
            }
            return hexResult;
        }


        /// <summary>
        /// Encrypts text with Triple DES encryption using the supplied key
        /// </summary>
        /// <param name="plaintext">The text to encrypt</param>
        /// <param name="key">Key to use for encryption</param>
        /// <returns>The encrypted string represented as base 64 text</returns>
        public static string EncryptTripleDES(string plaintext, string key)
        {
            TripleDESCryptoServiceProvider DES =
                new TripleDESCryptoServiceProvider();
            MD5CryptoServiceProvider hashMD5 = new MD5CryptoServiceProvider();
            DES.Key = hashMD5.ComputeHash(ASCIIEncoding.ASCII.GetBytes(key));
            DES.Mode = CipherMode.ECB;
            ICryptoTransform DESEncrypt = DES.CreateEncryptor();
            byte[] Buffer = ASCIIEncoding.ASCII.GetBytes(plaintext);
            return Convert.ToBase64String(
                DESEncrypt.TransformFinalBlock(Buffer, 0, Buffer.Length));
        }


        /// <summary>
        /// Decrypts supplied Triple DES encrypted text using the supplied key
        /// </summary>
        /// <param name="base64Text">Triple DES encrypted base64 text</param>
        /// <param name="key">Decryption Key</param>
        /// <returns>The decrypted string</returns>
        public static string DecryptTripleDES(string base64Text, string key)
        {
            TripleDESCryptoServiceProvider DES =
                new TripleDESCryptoServiceProvider();
            MD5CryptoServiceProvider hashMD5 = new MD5CryptoServiceProvider();
            DES.Key = hashMD5.ComputeHash(ASCIIEncoding.ASCII.GetBytes(key));
            DES.Mode = CipherMode.ECB;
            ICryptoTransform DESDecrypt = DES.CreateDecryptor();
            byte[] Buffer = Convert.FromBase64String(base64Text);
            return ASCIIEncoding.ASCII.GetString(
                DESDecrypt.TransformFinalBlock(Buffer, 0, Buffer.Length));
        }

    } // class
}
