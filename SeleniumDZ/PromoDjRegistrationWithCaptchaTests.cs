using Newtonsoft.Json.Linq;
using OpenQA.Selenium;

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
        PromoDjRegistrationRandomUserWithCaptcha();
        var element = driver.FindElement(By.XPath("//a[@href='/cp']"));
        Assert.That(element.Text, Is.EqualTo("ЦУП"));
    }


    [Test]

    public void PromoDjAutorizationWithCaptchaTesting()
    {
        PromoDjAutorizationWithCaptcha(TestUser["login"].ToString(), TestUser["password"].ToString());
        var element = driver.FindElement(By.XPath("//a[@href='/cp']"));
        Assert.That(element.Text, Is.EqualTo("ЦУП"));
    }

    [Test]
    [TestCase("")]
    [TestCase("Вававававава")]
    [TestCase("+-/-++/")]

    public void PromoDjEMailFieldRegistrationTesting(string data)
    {
        PromoDjEmailField(data);
        bool isDisplayed = wait.Until( e => e.FindElement(By.LinkText("Пожалуйста, введите ваш e-mail"))
            .Displayed);
        Assert.That(isDisplayed, Is.True);
        ScreenShot();
    }




}