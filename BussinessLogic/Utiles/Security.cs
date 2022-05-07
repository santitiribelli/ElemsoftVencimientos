using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Web.Routing;

namespace BusinessLogic.Utiles
{
    public class Security
    {
        public static string GetSHA256Hash(string text)
        {
            SHA256 sha = new SHA256CryptoServiceProvider();
            byte[] inputbytes = UTF8Encoding.UTF8.GetBytes(text);
            byte[] hash = sha.ComputeHash(inputbytes);

            StringBuilder sb = new StringBuilder();

            for (int i = 0; i < hash.Length; i++)
            {
                sb.Append(hash[i].ToString("X2"));
            }

            return sb.ToString();
        }

        public static string GetSHA512WithSalt(string strPassword, string strSalt)
        {
            string _strPasswordSalt = strPassword + strSalt;
            SHA512Managed _objSha512 = new SHA512Managed();
            byte[] _objTemporal = null;
            string Retorno = string.Empty;
            StringBuilder sb = new StringBuilder();

            try
            {
                _objTemporal = System.Text.Encoding.UTF8.GetBytes(_strPasswordSalt);
                _objTemporal = _objSha512.ComputeHash(_objTemporal);
                for (int i = 0; i < _objTemporal.Length; i++)
                {
                    sb.Append(_objTemporal[i].ToString("X2"));
                }
            }
            finally { _objSha512.Clear(); }
            return sb.ToString(); ;
        }

        /// <summary>
        /// Generates a cryptographically secure block of data suitable for salting hashes.
        /// </summary>
        /// <returns>A cryptographically secure block of data suitable for salting hashes.</returns>
        public static int GenerateSalt()
        {
            const int MinSaltSize = 1;
            const int MaxSaltSize = 2;

            // Generate a random number to determine the salt size.
            Random random = new Random();
            int saltSize = random.Next(MinSaltSize, MaxSaltSize);

            // Allocate a byte array, to hold the salt.
            byte[] saltBytes = new byte[saltSize];

            // Initialize the cryptographically secure random number generator.
            RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider();

            // Fill the salt with cryptographically strong byte values.
            rng.GetNonZeroBytes(saltBytes);

            string sSalt = string.Empty;
            int iSalt = 0;

            for (int i = 0; i < saltBytes.Count(); i++)
            {
                sSalt = String.Concat(sSalt, saltBytes[i].ToString());
            }

            iSalt = Convert.ToInt32(sSalt);

            return iSalt;
        }

        public static string EncriptarUrl(string actionName, string controllerName, object routeValues)
        {
            string queryString = string.Empty;
            if (routeValues != null)
            {
                RouteValueDictionary d = new RouteValueDictionary(routeValues);
                for (int i = 0; i < d.Keys.Count; i++)
                {
                    if (i > 0)
                    {
                        queryString += "?";
                    }
                    queryString += d.Keys.ElementAt(i) + "=" + d.Values.ElementAt(i);
                }
            }

            //What is Entity Framework??
            string ancor = "";

            if (controllerName != string.Empty)
            {
                ancor += "/Clientes/" + controllerName;
            }

            //if (actionName != "Index")
            //{
                ancor += "/" + actionName;
            //}
            if (queryString != string.Empty)
            {
                ancor += "?q=" + Encrypt(queryString);
            }

            return ancor;
        }

        private static string Encrypt(string plainText)
        {
            string key = "jdsg432387#";
            byte[] EncryptKey = { };
            byte[] IV = { 55, 34, 87, 64, 87, 195, 54, 21 };
            EncryptKey = System.Text.Encoding.UTF8.GetBytes(key.Substring(0, 8));
            DESCryptoServiceProvider des = new DESCryptoServiceProvider();
            byte[] inputByte = Encoding.UTF8.GetBytes(plainText);
            MemoryStream mStream = new MemoryStream();
            CryptoStream cStream = new CryptoStream(mStream, des.CreateEncryptor(EncryptKey, IV), CryptoStreamMode.Write);
            cStream.Write(inputByte, 0, inputByte.Length);
            cStream.FlushFinalBlock();
            return Convert.ToBase64String(mStream.ToArray());
        }
    }
}
