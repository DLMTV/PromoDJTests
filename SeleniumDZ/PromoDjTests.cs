using Newtonsoft.Json.Linq;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.DevTools.V104.SystemInfo;
using OpenQA.Selenium.DevTools.V85.CSS;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using Size = System.Drawing.Size;

namespace SeleniumDZ;

[TestFixture]

public class PromoDj : TestBase

{
    private JObject TestUser;
    private JObject Familia;

    [OneTimeSetUp]

    public void OneTimeSetUp()
    {
        driver.Navigate().GoToUrl("https://promodj.com");
        TestUser = (JObject)TestData["TestUser"];
        Familia = (JObject)TestData["Familia"];
        
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
        var element = driver.FindElement(By.XPath("//a[@href='/cp']"));
         Assert.That(element.Text, Is.EqualTo("ЦУП"));
    }

    [Test]

    public void PromoDjProfileTesting()
    {
        driver.FindElement(By.XPath("//a[@href='/cp']")).Click();
        Assert.That(driver.Url, Is.EqualTo("https://promodj.com/cp"));
        
    }

    [Test]
    [TestCase("+-+/+;';")]
    [TestCase("вапарвмма")]

    public void PromoDjProfileEditEmailTesting(string data)
    {
        PromoDjProfileEditEmail(data);
        bool isDisplayed = wait.Until( e => e.FindElement(By.XPath("//div[@class='cp2_notice cp2_notice__error']"))
            .Displayed);
        Assert.That(isDisplayed, Is.True);
        ScreenShot();
    }

    [Test]
    [TestCase("")]
    [TestCase("12345")]
    [TestCase("*/+-.")]

    public void PromoDjProfileEditPasswordTesting(string data)
    {
        PromoDjEnterOldPassword();
        PromoDjProfileEditPassword(data);
        bool isDisplayed = wait.Until( e => e.FindElement(By.XPath("//div[@class='cp2_notice cp2_notice__error']"))
            .Displayed);
        Assert.That(isDisplayed, Is.True);
        ClearNewPasswordField();
    }

    
    
    [Test]
    [TestCase("998898989898989898")]
    [TestCase("*/+<>:]")]
    [TestCase("ljljfdslkfjlkdjflkdsjflksjdfslkfjldsfjlsdkfdslkfjsldkfjslkfdjs")]
    [TestCase("Dolmatov")]
    [TestCase("Долматов")]

        public void PromoDjProfileEditFamiliaTesting(string data)
        {
            PromoDjProfileEditFamilia(data);
            bool isDisplayed = wait.Until( e => e.FindElement(By.XPath("//div[@class='cp2_notice cp2_notice__info']"))
            .Displayed);
            Assert.That(isDisplayed, Is.True);
            ScreenShot();
            ClearFamiliaField();
         }
        


        [Test]
        public void PromoDjPlayerTesting()
        {
            Player(); //Alexey Progress - Fashion Sound #041 #41
            bool isDisplayed = wait.Until(e => e.FindElement(By.XPath("//*[@class='playerr_bigplaybutton playerr_bigpausebutton']"))).Displayed;
            Assert.That(isDisplayed, Is.True);
            ScreenShot();
        }
        
        
        [Test]
        [TestCase(300, 400)]
        [TestCase(800, 600)]
        [TestCase(400, 800)]
        [TestCase(740, 1360)]
        
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