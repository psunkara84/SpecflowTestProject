using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using System.Web;
using AFrame.Web.Controls;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GoogleSignin.AFrameExtensions
{
    public static class AFrameExtensions
    {
        public static WebControl ClickWhenAvailable(this WebControl webControl)
        {
            WaitForAllTransisitions(webControl);

            webControl.Click();

            return webControl;

        }

        public static bool CheckDisplayStateWhenAvailable(this WebControl webControl)
        {
            WaitForAllTransisitions(webControl);

            return webControl.Displayed;
        }

        public static WebControl WaitUntilAvailable(this WebControl webControl)
        {
            WaitForAllTransisitions(webControl);

            return webControl;
        }

        public static string GetAttributeWhenAvailable(this WebControl webControl, string attributeString)
        {
            WaitForAllTransisitions(webControl);

            return webControl.GetAttribute(attributeString);
        }

        public static string TextCollectWhenAvailable(this WebControl webControl)
        {
            WaitForAllTransisitions(webControl);

            return webControl.Text;
        }

        public static WebControl CreateControlWhenAvailable(this WebControl webControl, string jqueryString)
        {
            WaitForAllTransisitions(webControl);

            return webControl.CreateControl(jqueryString);

        }

        public static IEnumerable<WebControl> CreateControlsWhenAvailable(this WebControl webControl, string jqueryString)
        {
            WaitForAllTransisitions(webControl);

            return webControl.CreateControls(jqueryString);
        }

        // Private Methods ---------------------------------------------

        private static void WaitForAllTransisitions(WebControl webControl)
        {
            // NOTE!
            // add ALL additional "wait" type methods here to ensure that all actions 
            // wait for the appropriate transisitons to complete

            WaitForAnimations(webControl);
            WaitUntilAjaxComplete(webControl);
        }

        private static void WaitForAnimations(WebControl webControl)
        {
            //TODO: Expand to log the actual animated elements that have caused a timeout to occur

            var sw = Stopwatch.StartNew();

            while ((bool)webControl.ExecuteScript("return $(':animated').length > 0"))
            {
                // if we are waiting for more than 3 seconds then halt all processing as the
                // animation is taking too long.
                if ((sw.ElapsedMilliseconds <= 10000))
                {
                    Thread.Sleep(10);
                }
                else
                {
                    Assert.Fail("ClickWhenAvailable: Timed out waiting for animation(s) to complete.");
                }
            }
        }

        private static long WaitUntilAjaxComplete(this WebControl webControl)
        {
            long timeoutMs = 8000;
            int retrySleepMs = 50;

            var sw = Stopwatch.StartNew();
            while (sw.ElapsedMilliseconds < timeoutMs)
            {

                if ((Boolean)webControl.ExecuteScript("return jQuery.active == 0"))
                {
                    return sw.ElapsedMilliseconds;
                }

                Thread.Sleep(retrySleepMs);
            }

            Assert.Fail("Failed to complete ajax requests within {0} seconds", timeoutMs / 1000);
            return sw.ElapsedMilliseconds;
        }
        
    }
}
