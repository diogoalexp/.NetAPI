using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleModel.Helper
{
    public static class Security
    {
        public static string key = "diogo@jAi8m&o1JH8nJ";

        public static string Encrypt(string value)
        {
            if (string.IsNullOrEmpty(value)) return string.Empty;
            value += key;
            var valueBytes = Encoding.UTF8.GetBytes(value);

            return Convert.ToBase64String(valueBytes);
        }

        public static string Decrypt(string value)
        {
            if (string.IsNullOrEmpty(value)) return string.Empty;
            var base64EncodedBytes = Convert.FromBase64String(value);
            var result = Encoding.UTF8.GetString(base64EncodedBytes);
            result = result.Substring(0, result.Length - key.Length);
            return result;
        }

    }
}
