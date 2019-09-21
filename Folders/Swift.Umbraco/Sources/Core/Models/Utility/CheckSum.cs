using System.Security.Cryptography;

namespace Swift.Umbraco.$safeprojectname$.Utility
{
    public class CheckSum
    {
        /// <summary>
        /// Provided by Consultix
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static string GenerateChecksum(string data)
        {
            byte[] buffer = new byte[data.Length];
            for (int i = 0; i < data.Length; ++i)
            {
                buffer[i] = (byte)(data[i]);
            }

            MD5 md5 = MD5.Create();

            byte[] code = md5.ComputeHash(buffer);

            string str = "";
            for (int j = 0; j < code.Length; ++j)
            {
                str += ToHex(code[j] >> 4);
                str += ToHex(code[j] & 0x0F);
            }
            return str;
        }

        private static char ToHex(int i)
        {
            char c;
            if (i < 10)
                c = (char)(48 + i);
            else
                c = (char)(87 + i);

            return c;
        }
    }
}
