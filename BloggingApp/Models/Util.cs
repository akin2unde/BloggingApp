using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System.Security.Cryptography;
using System.Text;

namespace BloggingApp.Models
{
    public class Util
    {
        public static string LoggedInUser { get; set; } = "LoggedInUser";

        const int keySize = 64;
        const int iterations = 350000;
        public static string HashPasword(string password)
        {
            byte[] encoded = SHA256.Create().ComputeHash(Encoding.UTF8.GetBytes(password));
            var value = (BitConverter.ToUInt32(encoded, 0) % 1000000).ToString();
            return value;
        }
    }
}
