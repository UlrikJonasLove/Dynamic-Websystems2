namespace Api.Models
{
    public interface ITokenManager
    {
        string TokenGenerator(User user);
        string GetToken();
        void RemoveToken();
    }
}
