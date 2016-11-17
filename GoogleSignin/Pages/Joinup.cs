using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AFrame.Web.Controls;
using GoogleSignin.AFrameExtensions;
using AFrame.Web;
using OpenQA.Selenium;
using GoogleSignin.Helpers;


namespace GoogleSignin.Pages
{
    public class Joinup :WebControl
    {

        internal WebControl JoinupButton
        {
            get { return CreateControl(".gmail-nav__nav-link__sign-in"); }
        }

        internal WebControl JoinupEmail
        {
            get { return CreateControl("#Email"); }
        }
        public void ClickJoinupButton()
        {
            JoinupButton.Click();
        }

        public bool IsEmailDisplayed()
        {
            return ElementWait.IsElementExisted(JoinupEmail);
            //if (JoinupEmail.Exists)  return true;
            //else
            //    return false;

        }

        public Joinup(WebContext context) : base(context)
        {
        }

    }
}
