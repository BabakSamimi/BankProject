using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;

namespace Common
{
    public static class Crypto
    {
        public static string GetSHA256FromString(string raw)
        {
            SHA256 sha = new SHA256Managed();
            byte[] rawBytes = Encoding.UTF8.GetBytes(raw);
            byte[] hash = sha.ComputeHash(rawBytes);

            return GetStringFromHash(hash);

        }

        private static string GetStringFromHash(byte[] hash)
        {
            StringBuilder sb = new StringBuilder();
            for(int i = 0; i < hash.Length; ++i)
            {
                sb.Append(hash[i].ToString("X2"));
            }
            return sb.ToString();
        }

    }
}
