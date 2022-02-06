namespace nXs.Web.Helpers
{
    public class Auth0Response
    {
        public string access_token { get; set; }

        public string token_type { get; set; }

        public static string Token { get; set; }
    }
}