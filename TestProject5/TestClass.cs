using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using PageObject;
using System.Data;

namespace TestProject5
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
            var MainPage = new MainPage(driver);
            var SearchQuery = "EPAM";

            //Act
            MainPage.searchButton.Click();
            var resultListItems = MainPage.SendKeysToSearchField(SearchQuery)
                .resultListItems;

            //Assert
            Assert.IsTrue(
                resultListItems.Select(element => element.Text).ToList().All(el => el.Contains(SearchQuery)));

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