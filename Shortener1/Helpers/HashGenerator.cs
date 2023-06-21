using System.Security.Cryptography;
using System.Text;

namespace Shortener1.Helpers;

public static class HashGenerator
{
    
    public static (string Password, string ShortenedUrl) GenerateHash(string stringForHashing) 
    {
        using (SHA256 sha256Hash = SHA256.Create())
        {
            byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(stringForHashing));
            StringBuilder builder = new StringBuilder();
            for (int i = 0; i < bytes.Length; i++)
            {
                builder.Append(bytes[i].ToString("x2"));
            }
            return (builder.ToString(), builder.ToString()[..10]);
        }
    }
}