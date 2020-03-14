namespace SoftRazborki.WebApi.Services
{
    using System;
    using System.Security.Cryptography;
    using System.Text;

    public class Sha256HashingService : IHashingService
    {
        public string Hash(string input)
        {
            if (string.IsNullOrEmpty(input))
            {
                throw new ArgumentException("Argument cannot be empty.", nameof(input));
            }

            using (SHA256 sha = SHA256.Create())
            {
                byte[] raw = Encoding.UTF8.GetBytes(input);
                byte[] hash = sha.ComputeHash(raw);

                return Convert.ToBase64String(hash);
            }
        }
    }
}
