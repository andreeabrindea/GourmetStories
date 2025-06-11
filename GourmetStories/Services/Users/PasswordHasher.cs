using System.Security.Cryptography;
public class PasswordHasher : IPasswordHasher
{
    private const int Salt = 16;
    private const int HashSize = 32;
    private const int Iterations = 100000;
    private static readonly HashAlgorithmName Algorithm = HashAlgorithmName.SHA512;
    public string Hash(string password)
    {
        byte[] salt = RandomNumberGenerator.GetBytes(Salt);
        byte[] hash = Rfc2898DeriveBytes.Pbkdf2(password, salt, Iterations, Algorithm, HashSize);

        return $"{Convert.ToHexString(salt)}-{Convert.ToHexString(hash)}";
    }

    public bool Verify(string password, string passwordHash)
    {
        string[] parts = passwordHash.Split("-");
        byte[] hash = Convert.FromHexString(parts[1]);
        byte[] salt = Convert.FromHexString(parts[0]);
        byte[] inputHash = Rfc2898DeriveBytes.Pbkdf2(password, salt, Iterations, Algorithm, HashSize);
        return CryptographicOperations.FixedTimeEquals(hash, inputHash);
    }
}