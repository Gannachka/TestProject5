using NUnit.Framework.Constraints;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using PageObject;
using System.Data;

namespace SpecFlowProject1
{
    public class TestClass
    {
        IWebDriver driver;        

        [SetUp]
        public void Setup()
        {
            driver = new ChromeDriver();
            driver.Manage().Window.Maximize();
            driver.Navigate().GoToUrl("https://www.epam.com/");
            var acceptCookiesButton = driver.FindElement(By.Id("onetrust-accept-btn-handler"));
            acceptCookiesButton.Click();
        }

        [Test]
        public void SearchFunctionalityTest()
        {
            //Arrange
            var mainPage = new MainPage(driver);
            var SearchQuery = "EPAM";

            //Act
            mainPage.searchButton.Click();
            var resultListItems = mainPage.SendKeysToSearchField(SearchQuery)
                .resultListItems;

            //Assert
            Assert.Contains(
                SearchQuery, resultListItems.Select(element => element.Text).ToList());

        }

        [TearDown]
        public void TearDown()
        {
            Screenshot screenshot = ((ITakesScreenshot)driver).GetScreenshot();
            string screenshotPath = Path.Combine(Directory.GetCurrentDirectory(), "CareersPageScreenshot.png");
            screenshot.SaveAsFile("ScreenshotImageFormat.Png");

            driver.Quit();
        }
    }
}