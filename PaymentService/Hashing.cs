using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace PaymentService
{
    public class Hashing
    {
        public static string GetSHA512Hashed(string input)
        {
            System.Security.Cryptography.SHA512 sha1 = System.Security.Cryptography.SHA512.Create();

            byte[] hash = sha1.ComputeHash(Encoding.UTF8.GetBytes(input.ToString()));

            StringBuilder output = new StringBuilder();

            for (int i = 0; i < hash.Length; i++)
            {
                output.Append(hash[i].ToString("X2"));
            }
            return output.ToString();
        }

        public static string GetMd5Hash(string input)
        {
            MD5 md5Hash = MD5.Create();
            // Convert the input string to a byte array and compute the hash. 
            byte[] data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(input));

            // Create a new Stringbuilder to collect the bytes 
            // and create a string.
            StringBuilder sBuilder = new StringBuilder();

            // Loop through each byte of the hashed data  
            // and format each one as a hexadecimal string. 
            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }

            // Return the hexadecimal string. 
            return sBuilder.ToString();
        }

        // Verify a hash against a string. 
        public static bool VerifyMd5Hash(string input, string hash, int maxHashLength)
        {
            // Hash the input. 
            string hashOfInput = GetMd5Hash(input);

            if (maxHashLength > 0)
            {
                hashOfInput = hashOfInput.Substring(0, maxHashLength);
            }

            // Create a StringComparer an compare the hashes.
            StringComparer comparer = StringComparer.OrdinalIgnoreCase;

            if (0 == comparer.Compare(hashOfInput, hash))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

    }
}