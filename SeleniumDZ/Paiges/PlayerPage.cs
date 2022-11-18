using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;

namespace SeleniumDZ.Paiges;

public class PlayerPage
{
    private WebDriver _driver;
    private WebDriverWait _wait;
    
    public PlayerPage (WebDriver driver, WebDriverWait wait)
    {
        _driver = driver;
        _wait = wait;
    }

    public PlayerPage PlayFindTrack()
    {
        _wait.Until(ExpectedConditions.VisibilityOfAllElementsLocatedBy(By.XPath("//*[@class='playerr_bigplaybutton']")));
        _driver.FindElement(By.XPath("//img[@class='playerr_bigplaybutton']")).Click();
        _wait.Until(ExpectedConditions.VisibilityOfAllElementsLocatedBy(By.XPath("//img[@class='playerr_bigplaybutton playerr_bigpausebutton']")));
        return this;
    }
}