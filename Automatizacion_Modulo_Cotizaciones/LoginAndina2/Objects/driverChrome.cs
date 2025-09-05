using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;


namespace LoginAndina2.Object
{

    public class DriverChrome
    {
        protected IWebDriver driver;
        public IWebDriver Driver => driver; // ✅ Esto permite accederlo desde LoginTest


        public void SetUp()
        {

            var options = new ChromeOptions();
           // var driverService = ChromeDriverService.CreateDefaultService(@"C:\Users\Usuario 01\Documents\ProyectoAndina\CoreAndina\bin\Debug\net8.0\chromedriver.exe");
            // Crear un objeto ChromeOptions para agregar opciones al navegador

            // Iniciar maximizado
            options.AddArguments("--start-maximized");

            // Inicializar el WebDriver con las opciones configuradas
            driver = new ChromeDriver(options);
            Thread.Sleep(500);
            driver.Navigate().GoToUrl("https://andinavidasegurosdev.linktic.com/");
            // Alternativa para establecer zoom usando JavaScript
           // Thread.Sleep(2000);
            SetZoomLevel(0.8); // 80%
        }

        internal void TearDown(object v)
        {
            throw new NotImplementedException();
        }

        internal void TearDown()
        {
            throw new NotImplementedException();
        }

        private void SetZoomLevel(double zoomLevel)
        {
            IJavaScriptExecutor js = (IJavaScriptExecutor)driver;
            js.ExecuteScript($"document.body.style.zoom='{zoomLevel}'");
        }
    }
}