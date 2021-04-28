using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Interactions;
using NUnit.Framework;
using System.Net.Http;
using System;
using System.Net.Http.Headers;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace AmarillisUnit
{

    public class Tests
    {
        const string cityname = "Minsk";
        const string latt = "53.893009";
        const string @long = "27.567444";
        
        private static readonly HttpClient client = new HttpClient();
        

        IWebDriver driver;

        [SetUp]
        public void Setup()
        {
            driver = new ChromeDriver();
        }

        private void ResizeWindow()
        {
            driver.Manage().Window.Size = new System.Drawing.Size(1920, 1080);
            driver.Manage().Window.Position = new System.Drawing.Point(0, 0);
        }

        [Test, Order(1)]
        public void LanguageAndCurrencySelectorTest()
        {
            driver.Navigate().GoToUrl("https://www.booking.com/index.ru.html");
            ResizeWindow();
            driver.FindElement(By.CssSelector(".bui-avatar__image")).Click();
            driver.FindElement(By.CssSelector(".bui-group__item:nth-child(1) > .bui-group:nth-child(1) .bui-grid__column:nth-child(2) .bui-inline-container:nth-child(1)")).Click();
            {
                var element = driver.FindElement(By.CssSelector(".bui-button__text > span:nth-child(1)"));
                Actions builder = new Actions(driver);
                builder.MoveToElement(element).Perform();
            }
            driver.FindElement(By.CssSelector(".bui-button__text > span:nth-child(1)")).Click();
            {
                var element = driver.FindElement(By.TagName("body"));
                Actions builder = new Actions(driver);
                builder.MoveToElement(element).Perform();
            }
            driver.FindElement(By.CssSelector(".bui-group__item:nth-child(1) .bui-group__item:nth-child(1) .bui-grid__column:nth-child(1) .bui-inline-container__main:nth-child(1)")).Click();
        }



        [Test, Order(2)]
        public void AirTicketsTest() {
            driver.Navigate().GoToUrl("https://www.booking.com/index.ru.html");
            ResizeWindow(); 
            driver.FindElement(By.CssSelector(".bui-tab__item:nth-child(2) .bui-tab__text")).Click();
            Assert.True(driver.Url.StartsWith("https://booking.kayak.com/"));
        }

        [TearDown]
        public void TearDown()
        {
            driver.Quit();
        }

        [Test, Order(3)]
        public void LocationTest()
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("https://www.metaweather.com/api/");
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage response = client.GetAsync("location/search/?query=minsk").Result;
            Assert.IsTrue(response.IsSuccessStatusCode);
            List<Location> locations = JsonConvert.DeserializeObject<List<Location>>(response.Content.ReadAsStringAsync().Result);
            Location minsk = locations.Find(x => x.title == cityname);
        }
    }
}