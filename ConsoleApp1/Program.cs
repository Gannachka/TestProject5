See https://aka.ms/new-console-template for more information
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;

Console.WriteLine("Hello, World!");
1.Initialize the ChromeDriver
IWebDriver driver = new ChromeDriver();

try
{
    2.Navigate to EPAM's website
    driver.Navigate().GoToUrl("https://www.epam.com");
    Console.WriteLine("Opened website: https://www.epam.com");

    Maximize the browser window
    driver.Manage().Window.Maximize();

    3.Accept cookies(if present)
        try
        {
            var acceptCookiesButton = driver.FindElement(By.Id("onetrust-accept-btn-handler")); // Cookie consent popup ID
            acceptCookiesButton.Click();
            Console.WriteLine("Accepted cookies.");
        }
        catch (NoSuchElementException)
        {
            Console.WriteLine("Cookies banner not displayed.");
        }

    4.Interact with the search functionality
    var searchIcon = driver.FindElement(By.CssSelector(".header-search__button")); // Search Icon
    searchIcon.Click();

    var searchInput = driver.FindElement(By.CssSelector(".header-search__input")); // Search Input Field
    searchInput.SendKeys("QA Engineer");
    searchInput.SendKeys(Keys.Enter); // Press Enter to trigger the search

    Wait for search results to load

   WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(20));
    IWebElement element = wait.Until(drv =>
    {
        IWebElement expectedElement = drv.FindElement(By.CssSelector(".search-results__items")); // Update with your actual ID
        return expectedElement.Displayed ? expectedElement : null;
    });

    Console.WriteLine("Search results loaded successfully.");

    5.Navigate to the Careers section
    var careersLink = driver.FindElement(By.LinkText("Careers"));
    careersLink.Click();

    Wait for the Careers page to load

   element = wait.Until(drv =>
   {
       IWebElement expectedElement = drv.FindElement(By.Id("new_form_job_search-keyword")); // Update with your actual ID
       return expectedElement.Displayed ? expectedElement : null;
   });

    Console.WriteLine("Navigated to Careers page.");

    6.Interact with dropdowns(e.g., location selectors on Careers page)
    try
    {
        var locationDropDown = new SelectElement(driver.FindElement(By.CssSelector("div.os-content  li.select2-results__option strong"))); // Location dropdown
        locationDropDown.SelectByText("United States"); // Select United States as the location
        Console.WriteLine("Selected location: United States.");
    }
    catch (NoSuchElementException)
    {
        Console.WriteLine("Location dropdown not found.");
    }

    7.Take a screenshot of the Careers page
    Screenshot screenshot = ((ITakesScreenshot)driver).GetScreenshot();
    string screenshotPath = Path.Combine(Directory.GetCurrentDirectory(), "CareersPageScreenshot.png");
    screenshot.SaveAsFile("ScreenshotImageFormat.Png");
    Console.WriteLine($"Screenshot of Careers page saved at: {screenshotPath}");

    8.Back to the homepage
    driver.Navigate().Back();
    element = wait.Until(drv =>
    {
        IWebElement expectedElement = drv.FindElement(By.CssSelector(".header__logo-link")); // Update with your actual ID
        return expectedElement.Displayed ? expectedElement : null;
    });
    Console.WriteLine("Navigated back to the homepage.");
}
