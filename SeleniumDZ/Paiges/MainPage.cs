using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumDZ.Paiges;
using SeleniumExtras.PageObjects;
using SeleniumExtras.WaitHelpers;

namespace SeleniumDZ.Paige;

public class MainPage
{
    private WebDriver _driver;
    private WebDriverWait _wait;
    
    public MainPage(WebDriver driver, WebDriverWait wait)
    {
        _driver = driver;
        _wait = wait;
    }
    
    public LogInPage GetLogInPage()
    {
        IWebElement enter = _driver.FindElement(By.XPath("//*[@href='https://promodj.com/login']"));
        enter.Click();
        return new LogInPage(_driver, _wait);
    }

    public RegistrationPage GetRegistrationPage()
    {
        IWebElement registration = _driver.FindElement(By.XPath("//[@href='https://promodj.com/register']"));
        registration.Click();
        return new RegistrationPage(_driver, _wait);
    }

    public MainPage MainPageSearchInput(string data)
    {
        IWebElement search = _driver.FindElement(By.Id("mainmenu_searchfor"));
        search.SendKeys(data);
        return this;
    }
    
    public SearchPage SearchResult(string toString)
    {
        _driver.FindElement(By.XPath("//span[@class='mainmenu_search_button mainmenu_search_button__black']")).Click();
        return new SearchPage(_driver, _wait);
    }
    
    
}
