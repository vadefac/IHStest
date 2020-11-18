using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;

namespace IHSTestProject
{
    [TestClass]
    public class TestBrowser
    {
        [TestMethod]
        public void TestOutput()
        {
            IWebDriver driver = new ChromeDriver();
            driver.Manage().Window.Maximize();
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
            driver.Url = "https://dotnetfiddle.net/";
            driver.FindElement(By.CssSelector("#run-button")).Click();            
            var output = driver.FindElement(By.XPath("//*[@id='output']")).Text;
            Assert.AreEqual(output,"Hello World");
            driver.Quit();
        }

        [TestMethod]
        public void TestPackage()
        {
            IWebDriver driver = new ChromeDriver();
            driver.Manage().Window.Maximize();            
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            driver.Url = "https://dotnetfiddle.net/";

            driver.FindElement(By.CssSelector("#CodeForm > div.main.docked > div.sidebar.unselectable > div:nth-child(5) > div > div > input")).SendKeys("nunit");        

            var element = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.Id("ui-id-1")));

            Actions action = new Actions(driver);
            action.MoveToElement(element).Perform();

            System.Threading.Thread.Sleep(4000);

            

            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.ClassName("ui-menu-item")));
            var versions = driver.FindElements(By.ClassName("ui-menu-item"));


            foreach (var version in versions)
            {
                
                if(version.Text == "3.12.0")
                {
                    try
                    {
                        version.FindElement(By.CssSelector("#menu > li:nth-child(1) > ul > li:nth-child(1) > a > i")); // Check if installed icon exists, if it does then assert true. If the icon is not present exception is thrown and assert fail
                        Assert.IsTrue(true);
                    }
                    catch
                    {
                        Assert.Fail();
                    }
                    
                                  
                }
            }
           
            driver.Quit();
        }
    }
}
