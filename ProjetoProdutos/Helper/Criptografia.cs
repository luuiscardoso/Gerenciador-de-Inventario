using System.Security.Cryptography;
using System.Text;

namespace ProjetoProdutos.Helper
{
    public static class Criptografia
    {
        public static string GerarHash(this string valor)
        {
            SHA1 sHA1 = SHA1.Create();
            SHA1 hash = sHA1;
            var encoding  = new ASCIIEncoding();
            var array = encoding.GetBytes(valor);

            array = hash.ComputeHash(array);

            var strHexa = new StringBuilder();

            foreach(var item in array)
            {
                strHexa.Append(item.ToString("x2"));
            }

            return strHexa.ToString();
        }
    }
}
