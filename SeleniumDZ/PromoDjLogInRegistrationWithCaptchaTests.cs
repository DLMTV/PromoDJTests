using Newtonsoft.Json.Linq;
using OpenQA.Selenium;
using SeleniumDZ.Paige;

namespace SeleniumDZ;

/// Мой IP уже заблокирован, думаю с твоего один раз должно запуститься ))) 

[TestFixture]

public class PromoDjRegistrationWithCaptchaTests : TestBase
{

    private JObject TestUser;
    private JObject Familia;

    [OneTimeSetUp]

    public void OneTimeSetUp()
    {
        driver.Navigate().GoToUrl("https://promodj.com");
        TestUser = (JObject)TestData["TestUser"];
    }
    

    [OneTimeTearDown]

    public void TearDown()
    {
        driver.Close();
    } 
    
    
    [Test]

    public void PromoDjRegistrationRandomUserWithCaptchaTesting()
    {
        var mainPage = new MainPage(driver, wait);
        mainPage
            .GetRegistrationPage()
            .SetRandomEmail(Utils.GetRandomEmail())
            .SetRandomPassword()
            .SetRandomName()
            .ReCaptcha()
            .SubmitRegistration();
        
        bool isDisplayed = wait.Until( e => e.FindElement(By.XPath("//a[@href='/cp']"))
            .Displayed);
        Assert.IsTrue(isDisplayed);
    }


    [Test]

    public void PromoDjAutorizationWithCaptchaTesting()
    {
        var mainPage = new MainPage(driver, wait);
        mainPage
            .GetLogInPage()
            .SetEmail(TestUser["login"].ToString())
            .SetPassword(TestUser["password"].ToString())
            .ReCaptcha()
            .SubmitLogIn();
        
        bool isDisplayed = wait.Until( e => e.FindElement(By.XPath("//a[@href='/cp']"))
            .Displayed);
        Assert.IsTrue(isDisplayed);
    }

    
}