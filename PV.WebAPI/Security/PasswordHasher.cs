using System.Security.Cryptography;

namespace PV.WebAPI.Security
{
    public class PasswordHasher : IPasswordHasher
    {

        private const int SaltSize = 128 / 8;
        private const int KeySize = 256 / 8;
        private const int Iterations = 10000;
        private static readonly HashAlgorithmName _hashAlgorithmName = HashAlgorithmName.SHA256;
        private static char Delimeter = ';';
        public string Hash(string passowrd)
        {
            var salt = RandomNumberGenerator.GetBytes(SaltSize);
            var hash = Rfc2898DeriveBytes.Pbkdf2(passowrd, salt, Iterations, _hashAlgorithmName, KeySize);
            return string.Join(Delimeter, Convert.ToBase64String(salt), Convert.ToBase64String(hash));
        }

        public bool verify(string passwordHash, string inputPassowrd)
        {
            throw new NotImplementedException();
        }
    }
}
