using AFrame.Core;
using BoDi;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.Extensions;
using System;
using System.Configuration;
using System.Diagnostics;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using TechTalk.SpecFlow;

namespace GoogleSignin
{
    [Binding]
    public class BeforeAndAfter
    {
        protected IWebDriver WebDriver;
        private readonly IObjectContainer _objectContainer;
        string browser = ConfigurationManager.AppSettings["BROWSER_TYPE"];
        int SEARCH_TIME_SECOND = int.Parse(ConfigurationManager.AppSettings["SEARCH_TIME_SECOND"]);
        public BeforeAndAfter(IObjectContainer objectContainer)
        {
            _objectContainer = objectContainer;
        }
        
        [BeforeScenario("UITest"]
        public void BeforeScenario()
        {
          
            // remove processes from previous runs
            var procs = Process.GetProcesses().Where(x => x.ProcessName.Contains("chromedriver")).ToArray();
            foreach (var proc in procs)
            {
                proc.Kill();
            }

            //open browser
            if (browser.Equals("firefox"))
            {
                WebDriver = new FirefoxDriver();
            }
            else if (browser.Equals("internetexplorer"))
            {
                WebDriver = new OpenQA.Selenium.IE.InternetExplorerDriver();
            }
            else  // default to chrome
            {
                WebDriver = new OpenQA.Selenium.Chrome.ChromeDriver();
            }


            WebDriver.Manage().Window.Maximize();
            WebDriver.Manage().Cookies.DeleteAllCookies();
            //Aframe defaults.
            Playback.SearchTimeout = 1000 * SEARCH_TIME_SECOND;
            Playback.HighlightOnFind = bool.Parse(ConfigurationManager.AppSettings["highlight_on_find"]);
            _objectContainer.RegisterInstanceAs(WebDriver);

        }

        [AfterScenario("UITest")]
        public void AfterScenario()
        {
            if (ScenarioContext.Current.TestError != null)
            {
                try
                {
                    var filePath =
                        $"{DateTime.Now:yyyy-MM-dd-HH-mm-ss-fff}-{ScenarioContext.Current.ScenarioInfo.Title}.png";
                    var fileInfo = new FileInfo(filePath);
                    WebDriver.TakeScreenshot().SaveAsFile(filePath, ImageFormat.Png);
                //   // Log.Information("Finished {Feature}.{Scenario}: {Result} {ScreenShotPath}",
                //        FeatureContext.Current.FeatureInfo.Title, ScenarioContext.Current.ScenarioInfo.Title, "FAILED",
                //        $@"\\{GetMachineName()}\{fileInfo.FullName.Replace("C:", "C$")}");
                }
                catch (Exception ex)
                {
                //   // Log.Error(ex, "Finished {Feature}.{Scenario}: {Result} {ScreenShotPath}",
                //        FeatureContext.Current.FeatureInfo.Title, ScenarioContext.Current.ScenarioInfo.Title, "FAILED",
                //        "");
                }
            }
            else
            {
               // Log.Information("Finished {Feature}.{Scenario}: {Result} {ScreenShotPath}", FeatureContext.Current.FeatureInfo.Title, ScenarioContext.Current.ScenarioInfo.Title, "SUCCESS", "");
            }

           WebDriver?.Close();
            WebDriver?.Dispose();

        }
            
            }

}
