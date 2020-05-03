using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.PageObjects;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using System;
using RestSharp;
using RestSharp.Authenticators;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Collections.ObjectModel;

namespace selenium_tests
{
    [TestFixture]
    public class Test
    {
        IWebDriver driver;
        [OneTimeSetUp] // вызывается перед началом запуска всех тестов
        public void OneTimeSetUp()
        {
            driver = new FirefoxDriver("E:\\WORK\\QA\\selenium");
            driver.Url = "https://docs.microsoft.com/ru-ru/";
            driver.Manage().Window.Maximize();
        }

        [OneTimeTearDown] //вызывается после завершения всех тестов
        public void OneTimeTearDown()
        {
            
        }

        [SetUp] // вызывается перед каждым тестом
        public void SetUp()
        {
            
        }

        [TearDown] // вызывается после каждого теста
        public void TearDown()
        {
            driver.Close();
        }

        [Test]
        public void TEST_1()
        {
            IWebElement searcher = driver.FindElement(By.XPath(".//*[@id='searchFormDesktop']"));

            Actions act = new Actions(driver);

            act.MoveToElement(searcher).Click().Perform();
            act.MoveToElement(searcher).SendKeys("LINQ");
            act.MoveToElement(searcher).SendKeys(Keys.Enter).Perform();
            Thread.Sleep(2000);
            for (int i = 1; i < 5; i++)
            {

                IWebElement mainPanel = driver.FindElement(By.Id("main"));
                IWebElement resultsPanel = mainPanel.FindElement(By.XPath(".//div[@id='search-results']"));
                ReadOnlyCollection<IWebElement> searchResults = resultsPanel.FindElements(By.XPath(".//li"));              
                
                foreach (IWebElement result in searchResults)
                {
                    Console.WriteLine("----------------------------------------------------------------");
                    string s1 = result.Text.ToString();
                    string s2 = "linq";
                    bool b = s1.ToLower().Contains(s2);
                    if(b == true)
                    {
                        Console.WriteLine($"Result: {b} \n Search result contains sought-for word");
                    }
                    else
                    {
                        Console.WriteLine($"Result: {b} \n Search result not contains sought-for word");
                    }
                    //Console.WriteLine(result.Text);
                    Console.WriteLine("----------------------------------------------------------------");
                }
                
                IWebElement pagePanel = mainPanel.FindElement(By.XPath(".//*[contains(@class,'pagination')]"));
                Actions action = new Actions(driver);
                action.SendKeys(Keys.End);
                action.Perform();
                IWebElement pager = pagePanel.FindElement(By.XPath(".//a[@href='/ru-ru/search/?search=LINQ&category=All&skip="+i+"0']"));
                pager.Click();
                Thread.Sleep(2000);
            }
        }
    }
}
