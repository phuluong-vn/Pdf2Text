using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace Pdf2Text
{
    public static class GeneratedCode
    {

        private static readonly Random random = new Random();

        public static string RandomCode(int lenght)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            var code = new string(Enumerable.Repeat(chars, lenght)
                .Select(s => s[random.Next(s.Length)]).ToArray());
            return code;
        }
       
        public static string hashwithtime(string input)
        {
            //MD5CryptoServiceProvider --> right click quick action
            //and select using System.Security.CrytoService
            MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();

            input = DateTime.Now.ToString() + input;
            byte[] originalBytes = ASCIIEncoding.Default.GetBytes(input);
            byte[] encodedBytes = md5.ComputeHash(originalBytes);

            return BitConverter.ToString(encodedBytes).Replace("-", "").Replace("y", "e").Replace("r", "c");
        }
    }
}