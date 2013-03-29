namespace HelloWebApi
{
    public interface IAuthenticationService
    {
        bool Authenticate(string username, string password);
    }

    class AuthenticationService : IAuthenticationService
    {
        public bool Authenticate(string username, string password)
        {
            return username == "ema" && password == "pwd";
        }
    }
}