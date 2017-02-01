using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace RelatorioFinanceiroV5.SharedKernel.Helpers
{
    public class StringHelper
    {
        public static string Encrypt(string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                return "";
            }
            value += "|7a2b562f-7276-4b76-8747-ee0e0606aed9";

            StringBuilder hash = new StringBuilder();
            MD5CryptoServiceProvider md5Provider = new MD5CryptoServiceProvider();
            byte[] data = md5Provider.ComputeHash(new UTF8Encoding().GetBytes(value));

            for (int i = 0; i < data.Length; i++)
            {
                hash.Append(data[i].ToString("x2"));
            }
            return hash.ToString();
        }
    }
}
