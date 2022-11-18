using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumDZ.Paiges;
using SeleniumExtras.PageObjects;
using SeleniumExtras.WaitHelpers;

namespace SeleniumDZ.Paige;

public class PersonalData : TestBase
{
    private WebDriver _driver;
    private WebDriverWait _wait;

    public PersonalData(WebDriver driver, WebDriverWait wait)
    {
        _driver = driver;
        _wait = wait;
    }
    
    
    public PersonalData ClearEmailField()
    {
        _wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//input[@name='row[email]']")));
        _driver.FindElement(By.XPath("//input[@name='row[email]']")).Clear();
        return this;
    }
    
    public PersonalData SendEmailField(string email)
    {
        _wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//input[@name='row[email]']")));
        _driver.FindElement(By.XPath("//input[@name='row[email]']")).SendKeys(email + "gmail.com");
        return this;
    }

    public PersonalData AccountInformationSave()
    {
        _driver.FindElement(By.XPath("//input[@type='submit']")).Click();
        return this;
    }
    
    public ChangePasswordPage GetChangePassword()
    {
        _driver.FindElement(By.XPath("//a[@href='/cp/personal/password']")).Click();
        return new ChangePasswordPage(_driver,_wait);
    }
    
    public PersonalData SetSecondNameField(string SecondName)
    {
        _driver.FindElement(By.XPath("//input[@name='row[f]']")).SendKeys(SecondName);
        _driver.FindElement(By.XPath("//input[@type='submit']")).Click();
        return this;
    }
    
    public PersonalData ClearSecondNameField()
    {
        _driver.FindElement(By.XPath("//input[@name='row[f]']")).Clear();
        return this;
    }

    public PersonalData GetErorrMessage()
    {
        _wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//div[@class='cp2_notice cp2_notice__error']")));
        return this;
    }

    public PersonalData SavePersonalInformation()
    {
        _driver.FindElement(By.XPath("//input[@type='submit']")).Click();
        
        return this;
    }


}