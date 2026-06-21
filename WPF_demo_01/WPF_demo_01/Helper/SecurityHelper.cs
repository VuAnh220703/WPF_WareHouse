using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
// add using SHA 256
using System.Security.Cryptography;

//add Brcypt --> dowload thư viện brypt = Install-Package BCrypt.Net-Next
using BCrypt.Net;


namespace WPF_demo_01.Helper
{
   public static class SecurityHelper
    {
        //hash password
        public static string HashPassword(string password)
        {
            return BCrypt.Net.BCrypt.HashPassword(password,workFactor:12);
        }

        //verify password
        public static bool VerifyHashPassword(string inputPassword, string storeHash)
        {
            return BCrypt.Net.BCrypt.Verify(inputPassword, storeHash);
        }

        // hash input (cccd, sdt, email) --> SHA 256
        public static string HashSHA256(string inputHash)
        {
           inputHash = inputHash.Trim();
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] bytes = Encoding.UTF8.GetBytes(inputHash);
                byte[] hash = sha256.ComputeHash(bytes);

                StringBuilder sb = new StringBuilder();
                foreach (byte b in hash)
                {
                    sb.Append(b.ToString("x2"));
                }

                return sb.ToString();
            }
        }
    }
}
