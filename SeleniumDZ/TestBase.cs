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

    protected void PromoDjRegistrationRandomUserWithCaptcha()
    {
        IWebElement registr = driver.FindElement(By.LinkText("Регистрация"));
        registr.Click();
        wait.Until(ExpectedConditions.VisibilityOfAllElementsLocatedBy(By.XPath("//*[@id='yessss']")));
        string email = Utils.GetRandomEmail();
        driver.FindElement(By.XPath("//*[@id='yessss']")).SendKeys(email); // рандомный мэйл ввод
        string pass = Utils.GetRandomPassword();
        driver.FindElement(By.Name("row[password1]")).SendKeys(pass); // рандомный пароль ввод
        string name = Utils.GetrandomName();
        driver.FindElement(By.Name("row[i]")).SendKeys(name); // рандомное имя 
        ReCaptcha();
        wait.Until(ExpectedConditions.PresenceOfAllElementsLocatedBy(
            (By.XPath("//*[@id='register_submit']"))));
        driver.FindElement(By.XPath("//*[@id='register_submit']")).Click();
    }

    protected void PromoDjAutorizationWithCaptcha(string login,string password)
    {
        IWebElement enter = driver.FindElement(By.XPath("//*[@href='https://promodj.com/login']"));
        enter.Click();
        wait.Until(ExpectedConditions.VisibilityOfAllElementsLocatedBy(By.XPath("//*[@id='login']")));
        driver.FindElement(By.XPath("//*[@id='login']")).SendKeys(login);
        driver.FindElement(By.XPath("//*[@id='password']")).SendKeys(password);
        ReCaptcha();
        wait.Until(ExpectedConditions.VisibilityOfAllElementsLocatedBy(By.XPath("//*[@class='form_button_input']")));
        driver.FindElement(By.XPath("//*[@class='form_button_input']")).Click();
    }

    protected void PromoDjEmailField(string data)
    {
        IWebElement registr = driver.FindElement(By.LinkText("Регистрация"));
        registr.Click();
        wait.Until(ExpectedConditions.VisibilityOfAllElementsLocatedBy(By.XPath("//*[@id='yessss']")));
        driver.FindElement(By.XPath("//*[@id='yessss']")).SendKeys(data+"gmail.com");
        ReCaptcha();
        wait.Until(ExpectedConditions.VisibilityOfAllElementsLocatedBy(By.XPath("//*[@class='form_button_input']")));
        driver.FindElement(By.XPath("//*[@class='form_button_input']")).Click();
    }

    
    protected void ReCaptcha()
    {
        wait.Until(ExpectedConditions.FrameToBeAvailableAndSwitchToIt(
            By.XPath("//iframe[@title='reCAPTCHA']")));
        wait.Until(ExpectedConditions.ElementToBeClickable(By.Id("recaptcha-anchor"))).Click();
        driver.SwitchTo().DefaultContent();
    }

    protected void PromoDjEnterProfileCookie(string login,string password)
    {
        driver.FindElement(By.XPath("//a[@href='https://promodj.com/login']")).Click();
        wait.Until(ExpectedConditions.PresenceOfAllElementsLocatedBy(By.XPath("//*[@id='login']")));
        driver.FindElement(By.XPath("//*[@id='login']")).SendKeys(login);
        driver.FindElement(By.XPath("//*[@id='password']")).SendKeys(password);
        driver.Manage().Cookies.AddCookie(new Cookie("l1am", "g0168c7b43c793ecdb5576bd5679970a1197b975579405f4620e497b7ff15cf9"));  
        driver.Manage().Cookies.AddCookie(new Cookie("digestive", "aSwmknYSWu2ptpyZt6ig3ZBlypspKIEptpyg4Sgl3GkA3GxmPvys3UyJyMYSzUy0yMTSznmjOroAin7DIU7JznHLzpyJyMwXONYSOIyptpwoHeVskZLkPmsjkewjz17CKpQvznmkPSyJyM4fi1msyvol4vih4ZBnt62lPUwZK1HAiIVmkNuptpyQtGTp3Z2S4Gwv4G4vONif3N3l4vYXivYpOrBZiG4v4pw7"));
        driver.Manage().Cookies.AddCookie(new Cookie("_ym_uid", "1660159733514662942"));
        driver.Manage().Cookies.AddCookie(new Cookie("lvu", "190521%2C3868311"));
        driver.Manage().Cookies.AddCookie(new Cookie("pdjsid", "502fa6189f51ce0954f81a0addb2dfb5"));
        driver.Manage().Cookies.AddCookie(new Cookie("bid", "2074")); // Проверять перед тестами
        driver.Navigate().GoToUrl("https://promodj.com");
    }

    protected void ScreenShot()
    {
        Screenshot screenshot = driver.GetScreenshot();
        screenshot.SaveAsFile(@"C:\Users\domkr\RiderProjects\API\SeleniumDZ\SeleniumDZ\Screens\SeleniumDZ_" + Utils.GetrandomName() + ".png");
    }
    
    protected void ScreenShotElement()
    {
        //IWebElement element = driver.FindElement(By.XPath());
        //Screenshot screenElement = ((ITakesScreenshot)element).GetScreenshot();
        //screenElement.SaveAsFile(@"C:\Users\domkr\RiderProjects\API\SeleniumDZ\SeleniumDZ\Screens\SeleniumDZ_ScreenShotElement_" + Utils.GetrandomName()+ "png");

    }

    protected void PromoDjProfileEditFamilia(string familia)
    {
        driver.FindElement(By.LinkText("ЦУП")).Click();
        wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//a[@href='https://promodj.com/cp/personal']")));
        driver.FindElement(By.XPath("//a[@href='https://promodj.com/cp/personal']")).Click();
        driver.FindElement(By.XPath("//input[@name='row[f]']")).SendKeys(familia);
        driver.FindElement(By.XPath("//input[@type='submit']")).Click();
    }
    
    protected void ClearFamiliaField()
    {
        driver.FindElement(By.XPath("//input[@name='row[f]']")).Clear();
        driver.FindElement(By.XPath("//input[@type='submit']")).Click();
    }

    protected void PromoDjProfileEditEmail(string email)
    {
        driver.FindElement(By.LinkText("ЦУП")).Click();
        wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//a[@href='https://promodj.com/cp/personal']")));
        driver.FindElement(By.XPath("//a[@href='https://promodj.com/cp/personal']")).Click();
        driver.FindElement(By.XPath("//input[@name='row[email]']")).Clear();
        driver.FindElement(By.XPath("//input[@name='row[email]']")).SendKeys(email+"gmail.com");
        driver.FindElement(By.XPath("//input[@type='submit']")).Click();
    }
    
    protected void PromoDjEnterOldPassword()
    {
        driver.FindElement(By.LinkText("ЦУП")).Click();
        wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//a[@href='https://promodj.com/cp/personal']")));
        driver.FindElement(By.XPath("//a[@href='https://promodj.com/cp/personal']")).Click();
        driver.FindElement(By.XPath("//a[@href='/cp/personal/password']")).Click();
        wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//*[@name='row[oldpassword]']")));
        driver.FindElement(By.XPath("//*[@name='row[oldpassword]']")).SendKeys("DLmtv8917");
    }
    
    protected void PromoDjProfileEditPassword(string data)
    {
        driver.FindElement(By.XPath("//input[@id='password1']")).SendKeys(data);
        driver.FindElement(By.XPath("//input[@id='password2']")).SendKeys(data);
        driver.FindElement(By.XPath("//*[@id='dos1']")).Click();
        wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//div[@class='cp2_notice cp2_notice__error']")));
    }

    protected void ClearNewPasswordField()
    {
        driver.FindElement(By.XPath("//input[@id='password1']")).Clear();
        driver.FindElement(By.XPath("//input[@id='password2']")).Clear();
    }
    
    protected void Player()
    
    {
        driver.FindElement(By.Id("mainmenu_searchfor")).SendKeys("Alexey Progress - Fashion Sound #041 #41");
        driver.FindElement(By.XPath("//span[@class='mainmenu_search_button mainmenu_search_button__black']")).Click();
        wait.Until(ExpectedConditions.VisibilityOfAllElementsLocatedBy(By.LinkText("Alexey Progress - Fashion Sound #041 #41")));
        driver.FindElement(By.XPath("//img[@class='playerr_bigplaybutton']")).Click();
        wait.Until(ExpectedConditions.VisibilityOfAllElementsLocatedBy(By.XPath("//*[@class='playerr_bigplaybutton playerr_bigpausebutton']")));  
         
    }
    

}