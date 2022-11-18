using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumDZ.Paige;
using SeleniumExtras.WaitHelpers;

namespace SeleniumDZ.Paiges;

public class RegistrationPage
{
    private WebDriver _driver;
    private WebDriverWait _wait;

    public RegistrationPage(WebDriver driver, WebDriverWait wait)
    {
        _driver = driver;
        _wait = wait;
    }

    public RegistrationPage SetRandomEmail(string email)
    {
        _wait.Until(ExpectedConditions.VisibilityOfAllElementsLocatedBy(By.XPath("//*[@id='yessss']")));
        _driver.FindElement(By.XPath("//*[@id='yessss']")).SendKeys(email); // рандомный мэйл ввод
        return this;
    }
    
    public RegistrationPage SetRandomPassword()
    {
        string pass = Utils.GetRandomPassword();
        _driver.FindElement(By.Name("row[password1]")).SendKeys(pass); // рандомный пароль ввод
        return this;
    }
    
    public RegistrationPage SetRandomName()
    {
        string name = Utils.GetrandomName();
        _driver.FindElement(By.Name("row[i]")).SendKeys(name); // рандомное имя 
        return this;
    }

    public RegistrationPage ReCaptcha()
    {
        _wait.Until(ExpectedConditions.FrameToBeAvailableAndSwitchToIt(
            By.XPath("//iframe[@title='reCAPTCHA']")));
        _wait.Until(ExpectedConditions.ElementToBeClickable(By.Id("recaptcha-anchor"))).Click();
        _driver.SwitchTo().DefaultContent();
        return this;
    }

    public RegistrationPage SubmitRegistration()
    {
        _wait.Until(ExpectedConditions.PresenceOfAllElementsLocatedBy(
            (By.XPath("//*[@id='register_submit']"))));
        _driver.FindElement(By.XPath("//*[@id='register_submit']")).Click();
        return new RegistrationPage(this._driver, _wait);
    }

    public LogInPage GetLogInPage()
    {
        IWebElement enter = _driver.FindElement(By.XPath("//*[@href='https://promodj.com/login']"));
        enter.Click();
        return new LogInPage(_driver, _wait);
    }

}