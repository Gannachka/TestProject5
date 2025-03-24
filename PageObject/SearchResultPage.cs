using OpenQA.Selenium;

namespace PageObject
{
    public class SearchResultPage
    {
        private readonly IWebDriver driver;
        public SearchResultPage(IWebDriver driver)
        {
            this.driver = driver;
        }
        
        public IList<IWebElement> resultListItems => driver.FindElements(By.ClassName("search-results__description"));

    }
}
