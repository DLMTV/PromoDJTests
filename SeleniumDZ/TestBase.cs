using Newtonsoft.Json.Linq;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;

namespace SeleniumDZ;

public class TestBase
{

    protected WebDriver driver;
    protected WebDriverWait wait;
    protected JObject TestData;
    protected IJavaScriptExecutor executor;

    [OneTimeSetUp]

    public void OneTimeSetup()
    {

        var options = new ChromeOptions();
        options.AddExcludedArgument("--enable-automation");
        options.AddAdditionalChromeOption("useAutomationExtension", false);

        driver = new ChromeDriver(options);
        wait = new WebDriverWait(driver, TimeSpan.FromSeconds(300));
        executor = driver;

        var path = Utils.GetFilePathByFileName("TestData.json");
        TestData = JObject.Parse(File.ReadAllText(path));

    }

    [OneTimeTearDown]
    public void OneTearDown()
    {
        driver.Quit();
    }

    protected void PromoDjEnterProfileCookie(string login, string password)
    {
        driver.FindElement(By.XPath("//a[@href='https://promodj.com/login']")).Click();
        wait.Until(ExpectedConditions.PresenceOfAllElementsLocatedBy(By.XPath("//*[@id='login']")));
        driver.FindElement(By.XPath("//*[@id='login']")).SendKeys(login);
        driver.FindElement(By.XPath("//*[@id='password']")).SendKeys(password);
        driver.Manage().Cookies
            .AddCookie(new Cookie("l1am", "g0168c7b43c793ecdb5576bd5679970a1197b975579405f4620e497b7ff15cf9"));
        driver.Manage().Cookies.AddCookie(new Cookie("digestive",
            "aSwmknYSWu2ptpyZt6ig3ZBlypspKIEptpyg4Sgl3GkA3GxmPvys3UyJyMYSzUy0yMTSznmjOroAin7DIU7JznHLzpyJyMwXONYSOIyptpwoHeVskZLkPmsjkewjz17CKpQvznmkPSyJyM4fi1msyvol4vih4ZBnt62lPUwZK1HAiIVmkNuptpyQtGTp3Z2S4Gwv4G4vONif3N3l4vYXivYpOrBZiG4v4pw7"));
        driver.Manage().Cookies.AddCookie(new Cookie("_ym_uid", "1660159733514662942"));
        driver.Manage().Cookies.AddCookie(new Cookie("lvu", "190521%2C3868311"));
        driver.Manage().Cookies.AddCookie(new Cookie("pdjsid", "502fa6189f51ce0954f81a0addb2dfb5"));
        driver.Manage().Cookies.AddCookie(new Cookie("bid", "2036")); // Проверять перед тестами
        driver.Navigate().GoToUrl("https://promodj.com");
    }
    
    protected void ScreenShot()
    {
        Screenshot screenshot = driver.GetScreenshot();
        screenshot.SaveAsFile(@"C:\Users\domkr\RiderProjects\API\SeleniumDZ\SeleniumDZ\Screens\SeleniumDZ_" + Utils.GetrandomName() + ".png");
    }
    
   /*  protected void ScreenShotElement()
    {
          IWebElement element = driver.FindElement(By.XPath());
          Screenshot screenElement = ((ITakesScreenshot)element).GetScreenshot();
          screenElement.SaveAsFile(@"C:\Users\domkr\RiderProjects\API\SeleniumDZ\SeleniumDZ\Screens\SeleniumDZ_ScreenShotElement_" + Utils.GetrandomName()+ "png");

    }
    */

}