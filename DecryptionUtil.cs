using Fiddler;
using System;
using System.Security.Cryptography;
using System.Text;

namespace Util
{
    class DecryptionUtil
    {
       private static readonly string iv = "替换为自己的iv";
        private static readonly string key = "替换为自己的key";
        public static string DecryptSDKBody(String bodyContent)
        {
            return AESDecryption(bodyContent, key, iv);
        }

        // AES解密
        public static string AESDecryption(string text, string AesKey, string AesIV)
        {
            try
            {
                // 16进制数据转换成byte
                byte[] encryptedData = HexStrToBytes(text);
                Console.WriteLine(encryptedData.Length + "");
                RijndaelManaged rijndaelCipher = new RijndaelManaged
                {
                    Key = Encoding.UTF8.GetBytes(AesKey),
                    IV = Encoding.UTF8.GetBytes(AesIV),
                    Mode = CipherMode.CBC,
                    Padding = PaddingMode.None
                };
                ICryptoTransform transform = rijndaelCipher.CreateDecryptor();
                byte[] plainText = transform.TransformFinalBlock(encryptedData, 0, encryptedData.Length);
                string result = Encoding.UTF8.GetString(plainText);
                FiddlerApplication.Log.LogString(result);
                return result;
            }
            catch (Exception ex)
            {
                // Console.WriteLine(ex.ToString());
                FiddlerApplication.Log.LogString(ex.ToString());
                return null;
            }
        }

        private static byte[] HexStrToBytes(string hexString)
        {
            hexString = hexString.Replace(" ", "");
            if ((hexString.Length % 2) != 0)
                hexString += " ";
            byte[] returnBytes = new byte[hexString.Length / 2];
            for (int i = 0; i < returnBytes.Length; i++)
                returnBytes[i] = Convert.ToByte(hexString.Substring(i * 2, 2).Trim(), 16);
            return returnBytes;
        }
    }
}
