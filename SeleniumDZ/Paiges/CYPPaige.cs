using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumDZ.Paiges;
using SeleniumExtras.PageObjects;
using SeleniumExtras.WaitHelpers;

namespace SeleniumDZ.Paige;

public class CYPPaige

{
    private WebDriver _driver;
    private WebDriverWait _wait;

    public CYPPaige(WebDriver driver, WebDriverWait wait)
    {
        _driver = driver;
        _wait = wait;
    }
    
    public CYPPaige GetCYP()
    {
        _wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//*[@href='/cp']")));
        _driver.FindElement(By.XPath("//*[@href='/cp']")).Click();
        
        return this;
    }
    
    public PersonalData GetPersonalData()
    {
        _wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//a[@href='https://promodj.com/cp/personal']")));
        _driver.FindElement(By.XPath("//a[@href='https://promodj.com/cp/personal']")).Click();
        return new PersonalData(_driver, _wait);
    }
    
    public ChangePasswordPage GetChangePassword()
    {
        _wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//a[@href='/cp/personal/password']")));
         _driver.FindElement(By.XPath("//a[@href='/cp/personal/password']")).Click();
        return new ChangePasswordPage(_driver, _wait);
    }

   

}