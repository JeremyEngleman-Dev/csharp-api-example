using System.Security.Cryptography;

namespace APIExample.Authentication
{
    public static class PasswordHasher
    {
        private const int SaltSize = 16;   // 128-bit
        private const int KeySize = 32;    // 256-bit
        private const int Iterations = 100_000;

        public static string Hash(string password)
        {
            // Generate random salt
            var salt = RandomNumberGenerator.GetBytes(SaltSize);

            // Derive key using the new static Pbkdf2 method
            var key = Rfc2898DeriveBytes.Pbkdf2(
                password,
                salt,
                Iterations,
                HashAlgorithmName.SHA256,
                KeySize
            );

            // Format: {iterations}.{salt}.{hash}
            return $"{Iterations}.{Convert.ToBase64String(salt)}.{Convert.ToBase64String(key)}";
        }

        public static bool Verify(string password, string hashedPassword)
        {
            var parts = hashedPassword.Split('.', 3);
            if (parts.Length != 3) return false;

            var iterations = int.Parse(parts[0]);
            var salt = Convert.FromBase64String(parts[1]);
            var expectedKey = Convert.FromBase64String(parts[2]);

            // Use the static Pbkdf2 method to derive the key for verification
            var actualKey = Rfc2898DeriveBytes.Pbkdf2(
                password,
                salt,
                iterations,
                HashAlgorithmName.SHA256,
                expectedKey.Length
            );

            return CryptographicOperations.FixedTimeEquals(actualKey, expectedKey);
        }
    }
}