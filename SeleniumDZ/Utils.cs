using System.Diagnostics.CodeAnalysis;
using System.Reflection.Metadata.Ecma335;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;

namespace SeleniumDZ;

public abstract class Utils
{
    private WebDriver driver;
    private WebDriverWait wait;
    public static string GetRandomEmail()
    {
        DateTime dateTime = DateTime.Now;
        return dateTime.Ticks.ToString() + "@gmail.com";
    }

    public static string GetRandomPassword()
    {
        var rand = new Random();
        string pass = string.Empty;
        for (int i = 0; i < 6; i++)
        {
            pass += (char)rand.Next('a', 'z');
        }

        return pass;
    }

    public static string GetrandomName()
    {
        var rand = new Random();
        string name = string.Empty;
        for (int i = 0; i < 10; i++)
        {
            name += (char)rand.Next('a', 'z');
        }

        return name;
    }
    
    public static string GetFilePathByFileName(string fileName)
    {
        string directory = AppDomain.CurrentDomain.BaseDirectory;
        string sFile = System.IO.Path.Combine(directory, "../../../" + fileName);
        string sFilePath = Path.GetFullPath(sFile);
        return sFilePath;
    }

    
}
