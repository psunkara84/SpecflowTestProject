using System;
using TechTalk.SpecFlow;
using GoogleSignin.Pages;
using GoogleSignin.Helpers;
using AFrame.Web;
using OpenQA.Selenium;
using System.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BoDi;

namespace GoogleSignin.StepDef
{
    [Binding]
    public class JoinupSteps

    {
        private WebContext _webContext;
        public IWebDriver WebDriver;

         public WebContext WebContext
        {
            get
            {
                if (this._webContext == null)
                {
                    this._webContext = new WebContext(WebDriver);
                }

                return this._webContext;
            }
        }
        public JoinupSteps(IObjectContainer objectContainer)
        {
            WebDriver = objectContainer.Resolve<IWebDriver>();
        }

        [Given(@"I am on (.*) website")]
        public void GivenIAmOnWebsite(string jurisduction)
        {
            switch (jurisduction)

            {
                case "India":
                    WebContext.NavigateTo<Joinup>(UrlHelper.HomeUrl);
                    break;
                case "Australia":
                    WebContext.NavigateTo<Joinup>(UrlHelper.HomeUrl);
                    break;
                
            }


        }
                         
                
        [When(@"I click on joinup button")]
        public void WhenIClickOnJoinupButton()
        {
            var JoinupPage = WebContext.As<Joinup>();
            JoinupPage.ClickJoinupButton();
        }
        
        [Then(@"i should see the joinup form")]
        public void ThenIShouldSeeTheJoinupForm()
        {
            var JoinupPage = WebContext.As<Joinup>();

            Assert.IsTrue(JoinupPage.IsEmailDisplayed());
        }
    }
}
