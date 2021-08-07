namespace ChatMe.Domain.Users
{
    using System;
    using System.Security.Cryptography;
    using System.Text;

    public class Password
    {
        public Password(string value)
        {
            Value = HashPassword(value);
        }

        public string Value { get; }

        public static implicit operator Password(string value) => new(value);

        private string HashPassword(string password)
        {
            byte[] bytes = Encoding.Unicode.GetBytes(password);
            byte[] inArray = HashAlgorithm.Create("SHA1").ComputeHash(bytes);

            return Convert.ToBase64String(inArray);
        }
    }
}
