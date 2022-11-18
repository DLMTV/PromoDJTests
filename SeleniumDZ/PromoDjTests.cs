using System.Diagnostics;
using Newtonsoft.Json.Linq;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.DevTools.V104.ServiceWorker;
using OpenQA.Selenium.DevTools.V104.SystemInfo;
using OpenQA.Selenium.DevTools.V85.CSS;
using OpenQA.Selenium.Support.UI;
using SeleniumDZ.Paige;
using SeleniumDZ.Paiges;
using SeleniumExtras.PageObjects;
using SeleniumExtras.WaitHelpers;
using Size = System.Drawing.Size;

namespace SeleniumDZ;

[TestFixture]

public class PromoDj : TestBase

{
    private JObject TestUser;
    private JObject FindTrack;

    [OneTimeSetUp]

    public void OneTimeSetUp()
    {
        driver.Navigate().GoToUrl("https://promodj.com");
        TestUser = (JObject)TestData["TestUser"];
        FindTrack = (JObject)TestData["FindTrack"];


        PromoDjEnterProfileCookie(TestUser["login"].ToString(), TestUser["password"].ToString());
    }
    
    [OneTimeTearDown]

    public void TearDown()
    {
        driver.Close();
    }
    
    

    [Test]

    public void PromoDjAutorizationWithOutCaptchaTesting()
    {
        
        bool isDisplayed = wait.Until( e => e.FindElement(By.XPath("//a[@href='/cp']"))
            .Displayed);
        Assert.IsTrue(isDisplayed,$"Авторизация не прошла!");
    }

    [Test]

    public void PromoDjCYPTesting()
    {
        var cypPaige = new CYPPaige(driver, wait);
        cypPaige
            .GetCYP();
        Assert.AreEqual(driver.Url, "https://promodj.com/cp", $"Ожидалось{driver.Url}, получили {"https://promodj.com/cp"}");
        
    }

    [Test]
    [TestCase("")]
    [TestCase("+-+/+;';")]
    [TestCase("вапарвмма")]

    public void PromoDjProfileEditEmailNegativeTesting(string data)
    { 
        CYPPaige cypPaige = new CYPPaige(driver, wait);
        cypPaige
            .GetCYP()
            .GetPersonalData()
            .ClearEmailField()
            .SendEmailField(data)
            .AccountInformationSave()
            .GetErorrMessage();
       bool isDisplayed = wait.Until(e => e.FindElement(By.XPath("//div[@class='cp2_notice cp2_notice__error']"))
            .Displayed); 
        Assert.IsTrue(isDisplayed, $"Email соттветсвует требованиям.");
            ScreenShot();
        
    }

    
    // Под Debug тест отрабатывает отлично, при запуске всех тестов падает на проверке второго значения
    
    
    [Test]
    [TestCase("")]
    [TestCase("+->,")]
    [TestCase("12345")]

    public void PromoDjProfileEditPasswordNegativeTesting(string data)
    {
        CYPPaige cypPaige = new CYPPaige(driver, wait);
        cypPaige
           .GetCYP()
           .GetPersonalData()
           .GetChangePassword()
           .InputOldPassword(TestUser["password"].ToString())
           .InputNewPassword_1(data)
           .InputNewPassword_2(data)
           .SaveNewPassword()
           .GetErorrMessagePassword();
       bool isDisplayed = wait.Until(e => e.FindElement(By.XPath("//div[@class='cp2_notice cp2_notice__error']")).Displayed);
        Assert.IsTrue(isDisplayed, $"Пароль больше 6 символов.");
    }

    [Test]
      [TestCase("998898989898989898")]
      [TestCase("*/+<>:]")]
      [TestCase("ljljfdslkfjlkdjflkdsjflksjdfslkfjldsfjlsdkfdslkfjsldkfjslkfdjs")]
      [TestCase("Долматов")]
      [TestCase("Dolmatov")]

        public void PromoDjProfileEditSecondNameNegativeTesting(string data)
        {
            var cypPaige = new CYPPaige(driver, wait);
            cypPaige
                .GetCYP()
                .GetPersonalData()
                .ClearSecondNameField()
                .SetSecondNameField(data)
                .SavePersonalInformation();
            bool isDisplayed = wait.Until( e => e.FindElement(By.XPath("//div[@class='cp2_notice cp2_notice__info']"))
            .Displayed);
            Assert.IsTrue(isDisplayed, $"Фамилия не соответствует требованиям!");
        }
        

        [Test]
        public void PromoDjPlayerTesting()
        {
            var mainPage = new MainPage(driver, wait);
            mainPage
                .MainPageSearchInput(FindTrack["findTrack"].ToString())
                .SearchResult(FindTrack["findTrack"].ToString())
                .GetPlayer()
                .PlayFindTrack();
            bool isDisplayed = wait.Until(e => e.FindElement(By.XPath("//img[@class='playerr_bigplaybutton playerr_bigpausebutton']")))
                .Displayed;
            Assert.IsTrue(isDisplayed,$"");
                ScreenShot();
        }
        
        
        [Test]
        [TestCase(300, 400)]
        [TestCase(800, 600)]
        [TestCase(400, 800)]
        [TestCase(1000, 3000)]

        public void PromoDjCheckSizeWindowsJsTesting(int height, int weight)
        {
            driver.Manage().Window.Size = new Size(height, weight);
            IJavaScriptExecutor executor = driver;
            driver.Navigate().GoToUrl("https://promodj.com");
            Boolean heightScroll =
                (Boolean)executor.ExecuteScript(
                    "return document.documentElement.scrollHeight > document.documentElement.clientHeight");
            Boolean weightScroll =
                (Boolean)executor.ExecuteScript(
                    "return document.documentElement.scrollWidth > document.documentElement.clientWidth");
            Assert.True(weightScroll);
            Assert.True(heightScroll);
                ScreenShot();
        }
        
    
        

}