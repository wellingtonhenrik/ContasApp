using System.Security.Cryptography;
using System.Text;

namespace ContasApp.Presentation.Helpers
{
    public static class MD5Helper
    {
        public static string Encrypt(string senha)
        {
            using (MD5 md5 = MD5.Create())
            {
                var inputBytes = Encoding.UTF8.GetBytes(senha);
                var hashBytes = md5.ComputeHash(inputBytes); //criptografia

                var sp = new StringBuilder();

                foreach(var item in hashBytes)
                    sp.Append(item.ToString("x2")); //hexadecimal

                return sp.ToString();
            }
        }
    }
}
