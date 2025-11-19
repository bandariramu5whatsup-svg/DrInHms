using System.Security.Cryptography;

namespace HanuMediSoftCore.Helpers
{
    public class PasswordHasher
    {
        private const int SaltSize = 16;
        private const int HashSize = 32;
        private const int Iterations = 100000;

        public static (byte[] hash, byte[] salt, int iterations) HashPassword(string password)
        {
            byte[] salt = new byte[SaltSize];
            using (var rng = RandomNumberGenerator.Create())
                rng.GetBytes(salt);

            using var pbkdf2 = new Rfc2898DeriveBytes(password, salt, Iterations, HashAlgorithmName.SHA256);
            byte[] hash = pbkdf2.GetBytes(HashSize);

            return (hash, salt, Iterations);
        }

        public static bool VerifyPassword(string password, byte[] storedHash, byte[] storedSalt, int iterations)
        {
            using var pbkdf2 = new Rfc2898DeriveBytes(password, storedSalt, iterations, HashAlgorithmName.SHA256);
            byte[] computed = pbkdf2.GetBytes(HashSize);

            return CryptographicOperations.FixedTimeEquals(computed, storedHash);
        }
    }
}
