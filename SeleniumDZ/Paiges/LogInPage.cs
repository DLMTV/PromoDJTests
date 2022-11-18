using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumDZ.Paiges;
using SeleniumExtras.WaitHelpers;

namespace SeleniumDZ.Paige;

public class LogInPage
{
    private WebDriver _driver;
    private WebDriverWait _wait;

    public LogInPage(WebDriver driver, WebDriverWait wait)
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

    public LogInPage SetEmail(string login)
    {
        _wait.Until(ExpectedConditions.VisibilityOfAllElementsLocatedBy(By.XPath("//*[@id='login']")));
        _driver.FindElement(By.XPath("//*[@id='login']")).SendKeys(login);
        return this;
    }
    
    public LogInPage SetPassword(string password)
    {
        _driver.FindElement(By.XPath("//*[@id='password']")).SendKeys(password);
        return this;
    }
    
    public LogInPage ReCaptcha()
    {
        _wait.Until(ExpectedConditions.FrameToBeAvailableAndSwitchToIt(
            By.XPath("//iframe[@title='reCAPTCHA']")));
        _wait.Until(ExpectedConditions.ElementToBeClickable(By.Id("recaptcha-anchor"))).Click();
        _driver.SwitchTo().DefaultContent();
        return this;
    }
    
    public LogInPage SubmitLogIn()
    {
       _wait.Until(ExpectedConditions.VisibilityOfAllElementsLocatedBy(By.XPath("//*[@class='form_button_input']")));
        _driver.FindElement(By.XPath("//*[@class='form_button_input']")).Click();
        return new LogInPage(this._driver, _wait);
    }
    
    public RegistrationPage GetRegistrationPage()
    {
        IWebElement registration = _driver.FindElement(By.LinkText("Регистрация"));
        registration.Click();
        return new RegistrationPage(_driver, _wait);
    }
}