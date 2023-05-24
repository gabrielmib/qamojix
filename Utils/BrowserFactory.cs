using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace yopmailQA.Utils
{
    public static class BrowserFactory
    {
         public static IWebDriver CreateChromeDriver()
        {
            ChromeOptions options = new ChromeOptions();
            // options.AddArguments("--headless"); // Opcional: ejecutar Chrome en modo headless (sin interfaz gráfica)
            options.AddArguments("--no-sandbox"); // Opcional: para evitar errores de sandbox en Linux

            ChromeDriverService service = ChromeDriverService.CreateDefaultService("Drivers");
            ChromeDriver driver = new ChromeDriver(service, options);

            return driver;
        }
    }
}