using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System.Data;

namespace TestProject5
{
    public class Tests
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
        public void Test1()
        {
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            string SearchQuery = "QA";

            var searchButton = driver.FindElement(By.CssSelector(".search-icon"));
            searchButton.Click();

            Console.WriteLine("Search button was clicked");

            var searchInput = wait.Until(drv =>
                {
                    var expectedElement = drv.FindElement(By.ClassName("header-search__input"));
                    return expectedElement.Displayed ? expectedElement : null;    
                });

            searchInput.SendKeys("QA");
            searchInput.SendKeys(Keys.Enter);
            
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
            var resultListItems =  driver.FindElements(By.ClassName("search-results__description"));
                 
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