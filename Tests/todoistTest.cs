using System.Threading;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium.Chrome;
using yopmailQA.Utils;
using System;
using SeleniumExtras.WaitHelpers;



namespace yopmailQA.Tests
{
    [TestClass]
    public class todoistTest
    {
        private IWebDriver driver;
        private WebDriverWait wait;

        //variables de test
        public string account = "gillanes@aimonkey.io";
        public string password = "*12345678*";
        public string urlTodoist = "https://todoist.com";

        public string projectName = "Test project 001";

        [TestInitialize]
        public void TestInitialize()
        {
            driver = BrowserFactory.CreateChromeDriver();
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(20));

            // Go to login page
            driver.Navigate().GoToUrl(urlTodoist + "/auth/login" );

            // Enter the user accout
            IWebElement accountTextBox = wait.Until(ExpectedConditions.ElementIsVisible(By.Id("element-0")));
            accountTextBox.SendKeys(account);

            // Enter the user's password 
            IWebElement passwordTextBox = wait.Until(ExpectedConditions.ElementIsVisible(By.Id("element-3")));
            passwordTextBox.SendKeys(password);

            // Click the login button
            IWebElement loginButton = wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//button[@type='submit']")));
            loginButton.Click();

            // Verify the user is redirected to main dashboard
            wait.Until(ExpectedConditions.UrlContains("/app/today"));
            Assert.IsTrue(driver.Url.Contains("/app/today"), "Error en Login");


            
        }


        [TestMethod]
        public void VerificarTareasProyecto()
        {
            
            // Create project

            // Hover to make visible the add button
            IWebElement menuProject = wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//a[@href='/app/projects']")));
            menuProject.Click();

            // Click Add button
            IWebElement addButton = wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//button[@aria-label='Añadir proyecto']")));
            addButton.Click();
            
            //Type project name
            IWebElement projectNameTextBox = wait.Until(ExpectedConditions.ElementIsVisible(By.Id("edit_project_modal_field_name")));
            projectNameTextBox.SendKeys(projectName);
            projectNameTextBox.SendKeys(Keys.Enter);

            //Verify project creation
            IWebElement projectItem = wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//ul/li[@data-type='project_list_item'][last()]//a/span[last()]")));
            Assert.AreEqual(projectName, projectItem.Text, "No coincide nombre de proyecto" );
            projectItem.Click();

            //Cick actions menu
            IWebElement actionMenu = wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//*[@id='projects_list']/li[last()]/div/div/div/button")));            
            actionMenu.Click();
            
            IWebElement actionEdit = wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//ul[@role='menu']/li//div[contains(text(), 'Editar')]")));            
            actionEdit.Click();
            
            //Type new project name
            // IWebElement projectNameEditTextBox = wait.Until(ExpectedConditions.ElementIsVisible(By.Id("edit_project_modal_field_name")));
            // projectNameEditTextBox.SendKeys(projectName + "modified");
            // projectNameEditTextBox.SendKeys(Keys.Enter);
            
            // Wait for 3 seconds (just for demonstration purposes)
            System.Threading.Thread.Sleep(3000);
        }

        

        

        [TestCleanup]
        public void TestCleanup()
        {
            driver.Quit();
            driver.Dispose();
        }
    }
}