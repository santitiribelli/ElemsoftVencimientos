using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Entities;

namespace DataAccess
{
    public static class Utiles
    {

        private static string DencriptPassword(string name)
        {

            // Assume failure.

            string returnValue = null;



            returnValue = name;



            //Busca la clave y lo descencripta

            if (!string.IsNullOrEmpty(returnValue))
            {



                string[] keys = returnValue.Split(';');

                string cs = "";

                string sep = "";



                foreach (string s in keys)
                {

                    if (s.ToUpper().IndexOf("PASSWORD") >= 0)
                    {

                        //Arregla los casos que la password termine justo donde termina la cadena con una ". Para los casos donde password este en el medio no hace nada.

                        string clave = s.Substring(s.IndexOf("=") + 1);

                        if (!string.IsNullOrEmpty(clave) && clave.LastOrDefault() == 34)
                        {

                            clave = clave.Substring(0, clave.Length - 1);

                            cs += sep + "Password=" + new Helper().DecryptFromString64(clave) + "\"";

                        }

                        else

                            cs += sep + "Password=" + new Helper().DecryptFromString64(clave);

                    }

                    else

                        cs += sep + s;





                    sep = ";";

                }



                returnValue = cs;

            }



            return returnValue;

        }

        public static GestionProcesosEntities ContextoLocal()
        {

            var cont = new GestionProcesosEntities();

            cont.Configuration.ProxyCreationEnabled = false;

            //Descemcripta la clave de la cadena de conexion

            cont.Database.Connection.ConnectionString = DencriptPassword(cont.Database.Connection.ConnectionString);

            return cont;

        }

        public static string ConnectionString()
        {

            var cont = new GestionProcesosEntities();

            return Utiles.DencriptPassword(cont.Database.Connection.ConnectionString);

        }

        public static SqlConnection GetConnection()
        {
            var cont = new GestionProcesosEntities();

            cont.Database.Connection.ConnectionString = DencriptPassword(cont.Database.Connection.ConnectionString);

            return cont.Database.Connection as SqlConnection;
        }

    }





    public class Helper
    {

        /// <summary>

        /// clave fija para encriptar/desencriptar

        /// </summary>

        private string PassFrase = "huguitoelem";



        // Use DES CryptoService with Private key pair

        byte[] key;  // we are going to pass in the key portion in our method calls

        byte[] IV = new byte[] { 0x12, 0x34, 0x56, 0x78, 0x90, 0xAB, 0xCD, 0xEF };



        /// <summary>

        /// Desencripta una cadena utilizando una clave recibida por parametro

        /// </summary>

        /// <param name="stringToDecrypt">cadena a desencriptar</param>

        /// <param name="sEncryptionKey">clave de desencriptacion</param>

        /// <returns>cadena desencriptada</returns>

        public string DecryptFromString64(string stringToDecrypt, string sEncryptionKey)
        {

            byte[] inputbyteArray = new byte[stringToDecrypt.Length];

            //string Cadena = new string();

            try
            {

                key = System.Text.Encoding.UTF8.GetBytes(sEncryptionKey.Substring(0, 8));

                DESCryptoServiceProvider des = new DESCryptoServiceProvider();

                inputbyteArray = Convert.FromBase64String(stringToDecrypt);

                MemoryStream ms = new MemoryStream();

                CryptoStream cs = new CryptoStream(ms, des.CreateDecryptor(key, IV), CryptoStreamMode.Write);

                cs.Write(inputbyteArray, 0, inputbyteArray.Length);

                cs.FlushFinalBlock();

                System.Text.Encoding encoding = System.Text.Encoding.UTF8;



                return encoding.GetString(ms.ToArray());

            }

            catch (InvalidCastException e)
            {

                throw e;

            }

        }



        /// <summary>

        /// Desencripta una cadena utilizando una clave fija

        /// </summary>

        /// <param name="stringToDecrypt">cadena a desencriptar</param>        

        /// <returns>cadena desencriptada</returns>

        public string DecryptFromString64(string stringToDecrypt)
        {

            try
            {

                return this.DecryptFromString64(stringToDecrypt, this.PassFrase);

            }

            catch //(Exception ex)
            {

                return "";

                //throw ex;

            }

        }



        /// <summary>

        /// Encripta una cadena utilizando una clave fija

        /// </summary>

        /// <param name="stringToDecrypt">cadena a encriptar</param>

        /// <returns>cadena encriptada</returns>

        public string EncryptToBase64String(string stringToDecrypt)
        {

            try
            {

                string s = this.EncryptToBase64String(stringToDecrypt, this.PassFrase);



                if (s.IndexOf(";") >= 0)

                    new Exception("Invalid string");



                return s;

            }

            catch (Exception ex)
            {

                throw ex;

            }

        }



        /// <summary>

        /// Encripta una cadena utilizando una clave recibida por parametro

        /// </summary>

        /// <param name="stringToDecrypt">cadena a encriptar</param>        

        /// <param name="SEncryptionKey">clave de encriptacion</param>

        /// <returns>cadena encriptada</returns>

        public string EncryptToBase64String(string stringToEncrypt, string SEncryptionKey)
        {

            //MyString Cadena = new MyString();

            try
            {

                key = System.Text.Encoding.UTF8.GetBytes(SEncryptionKey.Substring(0, 8));

                var des = new DESCryptoServiceProvider();

                byte[] inputbyteArray = Encoding.UTF8.GetBytes(stringToEncrypt);

                var ms = new MemoryStream();

                var cs = new CryptoStream(ms, des.CreateEncryptor(key, IV), CryptoStreamMode.Write);

                cs.Write(inputbyteArray, 0, inputbyteArray.Length);

                cs.FlushFinalBlock();

                return Convert.ToBase64String(ms.ToArray());



            }

            catch (InvalidCastException e)
            {

                throw e;

            }

        }



    }
}
