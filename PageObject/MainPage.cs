using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace PageObject
{
    public class MainPage
    {
        private readonly IWebDriver driver;

        private WebDriverWait wait => new WebDriverWait(driver, TimeSpan.FromSeconds(10));

        public MainPage(IWebDriver driver)
        {
            this.driver = driver;
        }
        public IWebElement searchButton => driver.FindElement(By.CssSelector(".search-icon"));

        public SearchResultPage SendKeysToSearchField(String Query)
        {
            var searchInput = wait.Until(drv =>
            {
                var expectedElement = drv.FindElement(By.ClassName("header-search__input"));
                return expectedElement.Displayed ? expectedElement : null;
            });

            searchInput.SendKeys(Query);
            searchInput.SendKeys(Keys.Enter);
            return new SearchResultPage(driver);
;        }  
    }
}
