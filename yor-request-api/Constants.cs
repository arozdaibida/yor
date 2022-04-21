namespace yor_request_api
{
    public static class Constants
    {
        public const string AppName = "yor-request-api";

        public static class EntityFrameworkOptions
        {
            public const string ConnectionName = "DefaultConnection";
        }

        public static class JwtOptions
        {
            public const string JwtKeys = "JsonWebTokenKeys";
        }

        public static class State
        {
            public const string Accepted = "Accepted";
            public const string Rejected = "Rejected";
        }
    }
}
