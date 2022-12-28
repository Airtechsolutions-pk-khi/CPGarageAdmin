using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using System.Security.Cryptography;
using System.IO;

namespace BAL.Repositories
{
    public class clsCryption
    {
        //Log l = new Log();
        string ErrorCode = "1002";
        //members of the Cryption 

        //algorithm type in my case it�s RijndaelManaged

        private RijndaelManaged Algorithm;
        //memory stream

        private MemoryStream memStream;
        //ICryptoTransform interface

        private ICryptoTransform EncryptorDecryptor;
        //CryptoStream

        private CryptoStream crStream;
        //Stream writer and Stream reader

        private StreamWriter strWriter;
        private StreamReader strReader;
        //internal members

        private string m_key;
        private string m_iv;
        //the Key and the Inicialization Vector

        private byte[] key;
        private byte[] iv;
        //password view

        private string pwd_str;
        private byte[] pwd_byte;
        //Constructor

        public clsCryption()
        {
            try
            {
                key = new byte[32];
                iv = new byte[32];

                int i;
                m_key = "H2SO4ieSuphuricAcid";
                m_iv = "EDPRIS";
                //key calculation, depends on first constructor parameter

                for (i = 0; i < m_key.Length; i++)
                {
                    key[i] = Convert.ToByte(m_key[i]);
                }
                //IV calculation, depends on second constructor parameter

                for (i = 0; i < m_iv.Length; i++)
                {
                    iv[i] = Convert.ToByte(m_iv[i]);
                }
            }
            catch (Exception ex)
            {
               // l.ErrorLogs(ErrorCode, ex.Message, System.Reflection.MethodBase.GetCurrentMethod().ToString(), HttpContext.Current.GetType().Name.ToString(), ex.StackTrace.Substring(ex.StackTrace.Length - 67, 67), Convert.ToInt32(HttpContext.Current.Session["UserID"]), HttpContext.Current.Session["CompanyCode"].ToString());
                throw;
            }
        }

        public string EncryptDecrypt(string sText, string action)
        {
            try
            {
                sText = sText.Replace("|", "+");
                string passPhrase = "Pas5pr@se";        // can be any string
                string saltValue = "s@1tValue";        // can be any string
                string hashAlgorithm = "SHA1";             // can be "MD5"
                int passwordIterations = 2;                  // can be any number
                string initVector = "@1B2c3D4e5F6g7H8"; // must be 16 bytes
                int keySize = 128;                // can be 192 or 128

                if (action.ToLower().Equals("encrypt"))
                {
                    return
                    crytpto.Encrypt(sText, passPhrase, saltValue, hashAlgorithm, passwordIterations, initVector,
                                          keySize);
                }
                if (action.ToLower().Equals("decrypt"))
                {
                    return
                    crytpto.Decrypt(sText, passPhrase, saltValue, hashAlgorithm, passwordIterations, initVector,
                                          keySize);
                }
                return sText;
            }
            catch (Exception ex)
            {
               // l.ErrorLogs(ErrorCode, ex.Message, System.Reflection.MethodBase.GetCurrentMethod().ToString(), HttpContext.Current.GetType().Name.ToString(), ex.StackTrace.Substring(ex.StackTrace.Length - 67, 67), Convert.ToInt32(HttpContext.Current.Session["UserID"]), HttpContext.Current.Session["CompanyCode"].ToString());
                return "0";
            }

        }



        //Encrypt method implementation
        public string Encrypt(string s)
        {
            try
            {
                //new instance of algorithm creation

                Algorithm = new RijndaelManaged();

                //setting algorithm bit size

                Algorithm.BlockSize = 256;
                Algorithm.KeySize = 256;

                //creating new instance of Memory stream

                memStream = new MemoryStream();

                //creating new instance of the Encryptor

                EncryptorDecryptor = Algorithm.CreateEncryptor(key, iv);

                //creating new instance of CryptoStream

                crStream = new CryptoStream(memStream, EncryptorDecryptor,
                CryptoStreamMode.Write);

                //creating new instance of Stream Writer

                strWriter = new StreamWriter(crStream);

                //cipher input string

                strWriter.Write(s);

                //clearing buffer for currnet writer and writing any 

                //buffered data to //the underlying device

                strWriter.Flush();
                crStream.FlushFinalBlock();

                //storing cipher string as byte array 

                pwd_byte = new byte[memStream.Length];
                memStream.Position = 0;
                memStream.Read(pwd_byte, 0, (int)pwd_byte.Length);

                //storing cipher string as Unicode string

                pwd_str = new UnicodeEncoding().GetString(pwd_byte);

                return pwd_str;
            }
            catch (Exception ex)
            {
                //l.ErrorLogs(ErrorCode, ex.Message, System.Reflection.MethodBase.GetCurrentMethod().ToString(), HttpContext.Current.GetType().Name.ToString(), ex.StackTrace.Substring(ex.StackTrace.Length - 67, 67), Convert.ToInt32(HttpContext.Current.Session["UserID"]), HttpContext.Current.Session["CompanyCode"].ToString());
                throw;
            }
        }

        //Decrypt method implementation 
        public string Decrypt(string s)
        {
            try
            {
                //new instance of algorithm creation

                Algorithm = new RijndaelManaged();

                //setting algorithm bit size

                Algorithm.BlockSize = 256;
                Algorithm.KeySize = 256;

                //creating new Memory stream as stream for input string      

                MemoryStream memStream = new MemoryStream(
                    new UnicodeEncoding().GetBytes(s));

                //Decryptor creating 

                ICryptoTransform EncryptorDecryptor =
                    Algorithm.CreateDecryptor(key, iv);

                //setting memory stream position

                memStream.Position = 0;

                //creating new instance of Crupto stream

                CryptoStream crStream = new CryptoStream(
                    memStream, EncryptorDecryptor, CryptoStreamMode.Read);

                //reading stream

                strReader = new StreamReader(crStream);

                //returnig decrypted string
                ////////////////////////////////////////////////////
                return strReader.ReadToEnd();
            }
            catch (Exception ex)
            {
               // l.ErrorLogs(ErrorCode, ex.Message, System.Reflection.MethodBase.GetCurrentMethod().ToString(), HttpContext.Current.GetType().Name.ToString(), ex.StackTrace.Substring(ex.StackTrace.Length - 67, 67), Convert.ToInt32(HttpContext.Current.Session["UserID"]), HttpContext.Current.Session["CompanyCode"].ToString());
                throw;
            }
        }

        private byte[] Key = { 89, 83, 45, 236, 140, 228, 180, 79, 209, 164, 231, 131, 28, 7, 110, 73, 140, 235, 118, 52, 225, 46, 202, 118 };
        private byte[] IV = { 161, 200, 187, 207, 22, 92, 119, 227 };

        public string Encrypt2(string inputString)
        {
            try
            {
                //Step #4
                byte[] buffer = Encoding.ASCII.GetBytes(inputString);

                TripleDESCryptoServiceProvider tripleDes = new TripleDESCryptoServiceProvider()
                {
                    Key = Key,
                    IV = IV
                };
                //Step #4,5,6
                ICryptoTransform ITransform = tripleDes.CreateEncryptor();
                return Convert.ToBase64String(ITransform.TransformFinalBlock(buffer, 0, buffer.Length));
            }
            catch (Exception ex)
            {
                //l.ErrorLogs(ErrorCode, ex.Message, System.Reflection.MethodBase.GetCurrentMethod().ToString(), HttpContext.Current.GetType().Name.ToString(), ex.StackTrace.Substring(ex.StackTrace.Length - 67, 67), Convert.ToInt32(HttpContext.Current.Session["UserID"]), HttpContext.Current.Session["CompanyCode"].ToString());
                throw;
            }
        }
        public string Decrypt2(string inputString)
        {
            try
            {
                byte[] buffer = Convert.FromBase64String(inputString);
                TripleDESCryptoServiceProvider tripleDes = new TripleDESCryptoServiceProvider()
                {
                    Key = Key,
                    IV = IV
                };
                ICryptoTransform ITransform = tripleDes.CreateDecryptor();
                return Encoding.ASCII.GetString(ITransform.TransformFinalBlock(buffer, 0, buffer.Length));
            }
            catch (Exception ex)
            {
              //  l.ErrorLogs(ErrorCode, ex.Message, System.Reflection.MethodBase.GetCurrentMethod().ToString(), HttpContext.Current.GetType().Name.ToString(), ex.StackTrace.Substring(ex.StackTrace.Length - 67, 67), Convert.ToInt32(HttpContext.Current.Session["UserID"]), HttpContext.Current.Session["CompanyCode"].ToString());
                throw;
            }
        }
    }
}