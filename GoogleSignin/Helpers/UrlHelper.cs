using System.Configuration;

namespace GoogleSignin.Helpers
{
    public static class UrlHelper 
    {
        public static string HomeUrl = ConfigurationManager.AppSettings["websiteUrl"];
    }
}
