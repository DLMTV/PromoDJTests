using System.Diagnostics;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;

namespace SeleniumDZ.Paiges;

public class ChangePasswordPage
{
    private WebDriver _driver;
    private WebDriverWait _wait;

    public ChangePasswordPage (WebDriver driver, WebDriverWait wait)
    {
        _driver = driver;
        _wait = wait;
    }

    public ChangePasswordPage InputOldPassword(string oldpassword)
    {
        _wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//*[@name='row[oldpassword]']")));
        _driver.FindElement(By.XPath("//*[@name='row[oldpassword]']")).SendKeys(oldpassword);
        return this;
    }
    

    public ChangePasswordPage InputNewPassword_1(string data)
    {
        _driver.FindElement(By.XPath("//input[@id='password1']")).SendKeys(data);
        return this;
    }
    
    public ChangePasswordPage InputNewPassword_2(string data)
    {
        _driver.FindElement(By.XPath("//input[@id='password2']")).SendKeys(data);
        return this;
    }

    public ChangePasswordPage SaveNewPassword()
    {
        _driver.FindElement(By.XPath("//*[@id='dos1']")).Click();
        return this;
    }

    public ChangePasswordPage GetErorrMessagePassword()
    {
        _wait.Until(ExpectedConditions.VisibilityOfAllElementsLocatedBy(By.XPath("//div[@class='cp2_notice cp2_notice__error']")));
        return this;
    }

    public ChangePasswordPage ClearNewPasswordField()
    {
        _driver.FindElement(By.XPath("//input[@id='password1']")).Clear();
        _driver.FindElement(By.XPath("//input[@id='password2']")).Clear();
        return this;
    }

    public ChangePasswordPage ClosePasswordChangeWindow()
    {
        _driver.FindElement(By.XPath("//*[@id='dos2']")).Click();
        return this;
    }



}