using Newtonsoft.Json.Linq;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;

namespace SeleniumDZ.Paiges;

public class SearchPage
{
    
    private WebDriver _driver;
    private WebDriverWait _wait;

    public SearchPage(WebDriver driver, WebDriverWait wait)
    {
        _driver = driver;
        _wait = wait;
    }

    public SearchPage SearchResult(string result)
    {
        _wait.Until(ExpectedConditions.VisibilityOfAllElementsLocatedBy(By.LinkText(result)));
        return this;
    }

    public PlayerPage GetPlayer()
    {
        _wait.Until(ExpectedConditions.VisibilityOfAllElementsLocatedBy(By.XPath("//*[@class='playerr_bigplaybutton']")));
        return new PlayerPage(_driver, _wait);
    }
    

}