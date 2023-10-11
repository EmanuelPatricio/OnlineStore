namespace WebUI.Helpers;

public class ApiRoutes
{
    private const string api = "api";

    public struct Login
    {
        public const string login = api + "/Login";
    }

    public struct Users
    {
        private const string _ = api + "/User";
        public const string AddToolToCart = _ + "/add-tool";
        public const string Create = _;
    }

    public struct Tool
    {
        private const string _ = api + "/Tool";
        public const string Get = _;
    }
}
