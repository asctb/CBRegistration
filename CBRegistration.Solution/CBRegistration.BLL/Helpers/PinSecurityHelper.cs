using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace CBRegistration.BLL.Helpers
{
    internal static class PinSecurityHelper
    {
        private const int SaltSize = 16;
        private const int Iterations = 100000;
        private const int HashSize = 32;

        public static string HashPin(string pin)
        {
            byte[] salt = RandomNumberGenerator.GetBytes(SaltSize);

            var pbkdf2 = new Rfc2898DeriveBytes(pin, salt, Iterations, HashAlgorithmName.SHA256);
            byte[] hash = pbkdf2.GetBytes(HashSize);

            byte[] hashBytes = new byte[SaltSize + HashSize];
            Array.Copy(salt, 0, hashBytes, 0, SaltSize);
            Array.Copy(hash, 0, hashBytes, SaltSize, HashSize);

            return Convert.ToBase64String(hashBytes);
        }

        public static bool VerifyPin(string pin, string hashedPin)
        {
            byte[] hashBytes = Convert.FromBase64String(hashedPin);

            byte[] salt = new byte[SaltSize];
            Array.Copy(hashBytes, 0, salt, 0, SaltSize);

            var pbkdf2 = new Rfc2898DeriveBytes(pin, salt, Iterations, HashAlgorithmName.SHA256);
            byte[] hash = pbkdf2.GetBytes(HashSize);

            for (int i = 0; i < HashSize; i++)
            {
                if (hashBytes[i + SaltSize] != hash[i])
                {
                    return false;
                }
            }
            return true;
        }
    }
}
