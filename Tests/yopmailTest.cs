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
    public class yopmailTest
    {
        private IWebDriver driver;
        private WebDriverWait wait;

        [TestInitialize]
        public void TestInitialize()
        {
            driver = BrowserFactory.CreateChromeDriver();
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
        }


        [TestMethod]
        public void EnviarMailTest()
        {
            //variables de test
            string testMail = "gabriel1235";
            string testSubject = "email de prueba";
            string testBodyMail = "Mensaje de qa desde Selenium";
            
            
            driver.Navigate().GoToUrl("https://yopmail.com/es/");

            
            
            // Enter the email address
            IWebElement mailTextBox = wait.Until(ExpectedConditions.ElementIsVisible(By.Id("login")));
            mailTextBox.SendKeys(testMail);

            // Click the "Check Inbox" button
            IWebElement checkInboxButton = wait.Until(ExpectedConditions.ElementToBeClickable(By.Id("refreshbut")));
            checkInboxButton.Click();

            // Verify the email address in the mailbox
            IWebElement mailNameLabel = wait.Until(ExpectedConditions.ElementIsVisible(By.ClassName("bname")));
            Assert.AreEqual(testMail+"@yopmail.com", mailNameLabel.Text, "Email address is different!");

            // Click the "New Email" button
            IWebElement newMailButton = wait.Until(ExpectedConditions.ElementToBeClickable(By.Id("newmail")));
            newMailButton.Click();

            // Switch to the email content frame
            IWebElement mailFrame = wait.Until(ExpectedConditions.ElementIsVisible(By.Id("ifmail")));
            driver.SwitchTo().Frame(mailFrame);

            // Perform actions within the email content frame            
            IWebElement destinationMail = wait.Until(ExpectedConditions.ElementIsVisible(By.Id("msgto")));
            destinationMail.SendKeys(testMail);

            // Type the subject
            IWebElement subjectText = wait.Until(ExpectedConditions.ElementIsVisible(By.Id("msgsubject")));
            subjectText.SendKeys(testSubject);

            //Type the body
            IWebElement messageBodyText = wait.Until(ExpectedConditions.ElementIsVisible(By.Id("msgbody")));
            messageBodyText.SendKeys(testBodyMail);

            // Click the SendMail button
            IWebElement sendMailButton = wait.Until(ExpectedConditions.ElementToBeClickable(By.Id("msgsend")));
            sendMailButton.Click();

            // Verify the succes notification
            IWebElement succesAlert = wait.Until(ExpectedConditions.ElementIsVisible(By.Id("msgpopmsg")));
            Assert.AreEqual("Tu mensaje ha sido enviado", succesAlert.Text, "Error en confirmacion");

            // Switch back to the default content
            driver.SwitchTo().DefaultContent();

            // click the refresh button for inbox
            IWebElement refreshButton = wait.Until(ExpectedConditions.ElementIsVisible(By.Id("refresh")));
            refreshButton.Click();

            // Switch to the inbox content frame
            IWebElement inboxFrame = wait.Until(ExpectedConditions.ElementIsVisible(By.Id("ifinbox")));
            driver.SwitchTo().Frame(inboxFrame);

            //Verify the new mail is received in the inbox    
            IWebElement newRecMail = wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//div[@class='mctn']//div[@class='m'][1]//div[@class='lms']")));
            Assert.AreEqual(testSubject, newRecMail.Text, "Error en contenido de mail");

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