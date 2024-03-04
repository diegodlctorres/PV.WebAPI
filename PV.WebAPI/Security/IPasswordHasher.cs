namespace PV.WebAPI.Security
{
    public interface IPasswordHasher
    {
        string Hash(string passowrd);

        bool verify(string passwordHash, string inputPassowrd);
    }
}
