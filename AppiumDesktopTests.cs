using OpenQA.Selenium.Appium.Windows;
using OpenQA.Selenium.Appium;
using System.Xml.Linq;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace AppiumDesktopTests
{
    public class AppiumDesktopTests
    {

        private const string AppiumUrl = "http://127.0.0.1:4723/wd/hub";
        private const string TaskboardUrl = "https://taskboard--kameliya-m.repl.co/api";
        private const string appLocation = @"C:\Users\Emoto\Desktop\RetakeExamQAFrontEnd\TaskBoard.DesktopClient.Net6\TaskBoard.DesktopClient.exe";

        private WindowsDriver<WindowsElement> driver;
        private AppiumOptions options;


        [SetUp]
        public void Setup()
        {
            options = new AppiumOptions() { PlatformName = "Windows" };
            options.AddAdditionalCapability("app", appLocation);

            driver = new WindowsDriver<WindowsElement>(new Uri(AppiumUrl), options);
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);
        }

        [Test]
        public void Test_AssertOpen()
        {
            var urlField = driver.FindElementByAccessibilityId("textBoxApiUrl");
            urlField.Clear();
            urlField.SendKeys(TaskboardUrl);

            var connectButton = driver.FindElementByAccessibilityId("buttonConnect");
            connectButton.Click();

            string windowName = driver.WindowHandles[0];
            driver.SwitchTo().Window(windowName);

            var textBoxSearch = driver.FindElementByAccessibilityId("textBoxSearchText");
            textBoxSearch.Clear();
            textBoxSearch.SendKeys("open");

            var buttonSearch = driver.FindElementByAccessibilityId("buttonSearch");
            buttonSearch.Click();

          
            var tasksList = driver.FindElementByXPath("//Group[@Name=\"Open\"]");
            
         
            Assert.That(tasksList.Text, Is.EqualTo("Open"));
            
        }


        [TearDown]
        public void CloseApp()
        {
            driver.Quit();
        }
    }
}