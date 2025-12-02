namespace MentorHub.API.Utilities;

public interface IHashHandler
{
    string GenerateHash(string password);
    bool ValidateHash(string password, string hash);
}

public class HashHandler : IHashHandler
{
    private string GenerateSalt()
    {
        return BCrypt.Net.BCrypt.GenerateSalt(12); // 10
    }
    public string GenerateHash(string password)
    {
        return BCrypt.Net.BCrypt.HashPassword(password, GenerateSalt());
    }

    public bool ValidateHash(string password, string hash)
    {
        return BCrypt.Net.BCrypt.Verify(password, hash);
    }
}
