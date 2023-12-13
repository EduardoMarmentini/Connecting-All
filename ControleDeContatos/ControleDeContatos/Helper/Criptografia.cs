using System.Globalization;
using System.Security.Cryptography;
using System.Text;

namespace ControleDeContatos.Helper
{
    public static class Criptografia
    {
        public static string GerarHash(this string value) 
        {
            // Variavel que recebe o objeto de hash SHA1
            var hash = SHA1.Create();
            
            var encode = new ASCIIEncoding();
            //Array que ira receber os bites da string passada 
            var array = encode.GetBytes(value);

            //Aqui ele gera o Hash de cripitografia da string
            array = hash.ComputeHash(array);
            
            var strHexa = new StringBuilder();

            foreach ( var item in array ) 
            {
                strHexa.Append(item.ToString("x2"));
            }

            return strHexa.ToString();
        }
    }
}
