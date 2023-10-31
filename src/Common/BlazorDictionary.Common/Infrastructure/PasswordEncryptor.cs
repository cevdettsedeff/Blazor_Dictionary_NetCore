using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace BlazorDictionary.Common.Infrastructure
{
    public class PasswordEncryptor
    {
        public static string Encrypt(string password)
        {
            using var md5 = MD5.Create();

            byte[] inputTypes = Encoding.ASCII.GetBytes(password);
            byte[] hashBytes = md5.ComputeHash(inputTypes);

            return Convert.ToHexString(hashBytes);

            // bu metod sayesinde içerisine gönderilmiş rastgele bir şifreyi MD5 kriptografi ile geriye şifreli hale döndürürüz.
        }
    }
}
