namespace newSHA256
{
using System;
using System.Security.Cryptography;
using System.Text;

public class PasswordHasher
{
    public static string GenerateSalt()
    {
        byte[] saltBytes = new byte[16];
        using (var rng = new RNGCryptoServiceProvider())
        {
            rng.GetBytes(saltBytes);
        }
        return Convert.ToBase64String(saltBytes);
    }

    public static string GenerateHash(string password, string salt)
    {
        byte[] passwordBytes = Encoding.UTF8.GetBytes(password);
        byte[] saltBytes = Convert.FromBase64String(salt);

        byte[] combinedBytes = new byte[passwordBytes.Length + saltBytes.Length];
        Buffer.BlockCopy(passwordBytes, 0, combinedBytes, 0, passwordBytes.Length);
        Buffer.BlockCopy(saltBytes, 0, combinedBytes, passwordBytes.Length, saltBytes.Length);

        using (var sha256 = SHA256.Create())
        {
            byte[] hashBytes = sha256.ComputeHash(combinedBytes);

            return Convert.ToBase64String(hashBytes);
        }
    }
}

public class Program
{
    public static void Main()
    {
        Console.WriteLine("Inserisci la password:");
        string password = Console.ReadLine();

        Console.WriteLine("Inserisci il salt:");
        string salt = Console.ReadLine();

        string hashedPassword = PasswordHasher.GenerateHash(password, salt);

        Console.WriteLine("Password hash generato:");
        Console.WriteLine(hashedPassword);
    }
}
}

