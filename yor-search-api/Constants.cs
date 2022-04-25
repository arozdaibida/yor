namespace yor_search_api
{
    public class Constants
    {
        public const string AppName = "yor-search-api";
        
        public static class EntityFrameworkOptions
        {
            public const string ConnectionName = "DefaultConnection";
        }

        public static class JwtOptions
        {
            public const string JwtKeys = "JsonWebTokenKeys";
        }

        public static class JwtToken
        {
            public static class Claims
            {
                public const string UserIdClaim = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier";
            }
        }
    }
}
