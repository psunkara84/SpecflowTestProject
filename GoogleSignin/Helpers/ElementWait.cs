using AFrame.Web.Controls;
using System.Configuration;
using System.Threading;

namespace GoogleSignin.Helpers
{
    public static class ElementWait
    {
        public static int SEARCH_ELEMENT_TIMES = int.Parse(ConfigurationManager.AppSettings["SEARCH_ELEMENT_TIMES"]);
        public static int TIMEOUT_TIME_SECOND = int.Parse(ConfigurationManager.AppSettings["TIMEOUT_TIME_SECOND"]);

        public static bool IsElementExisted(WebControl ElementName)
        {
            for (var i = 0; i < SEARCH_ELEMENT_TIMES; i++)
            {
                if (ElementName.Exists) return true;
                Thread.Sleep(TIMEOUT_TIME_SECOND * 1000);
            }

            return false;
        }
    }
}
