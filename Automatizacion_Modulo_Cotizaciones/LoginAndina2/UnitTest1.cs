using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using OpenQA.Selenium.DevTools;
using OpenQA.Selenium.DevTools.V138.Network;
using Newtonsoft.Json.Linq;
using System.Text;




namespace LoginAndina2
{
    public class Tests1
    {
        private ChromeDriver driver;
        private WebDriverWait wait;
        private DevToolsSession devToolsSession;
        private OpenQA.Selenium.DevTools.V138.DevToolsSessionDomains devToolsDomains;

        [SetUp]
        public void Setup()
        {
            driver = new ChromeDriver();
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            driver.Manage().Window.Maximize();
        }

        [Test]
        //Prueba de login Happy Path
        public async Task LoginHappyPath()
        {
            // --- Configurar DevTools Network ---
            devToolsSession = ((ChromeDriver)driver).GetDevToolsSession();
            devToolsDomains = devToolsSession.GetVersionSpecificDomains<OpenQA.Selenium.DevTools.V138.DevToolsSessionDomains>();
            await devToolsDomains.Network.Enable(new EnableCommandSettings());

            // Esperar el OTP en segundo plano
            var tcsOtp = new TaskCompletionSource<string>();

            var otpRequestIds = new HashSet<string>();

            devToolsDomains.Network.ResponseReceived += (sender, e) =>
            {
                Console.WriteLine($"↪️ {e.Response.Url}");
                if (e.Response.Url.Contains("/otp/send-otp"))
                {
                    otpRequestIds.Add(e.RequestId);
                }
            };
            
                       devToolsDomains.Network.LoadingFinished += async (sender, e) =>
                      {
                           if (otpRequestIds.Contains(e.RequestId))
                           {
                               try
                               {
                                   var body = await devToolsDomains.Network.GetResponseBody(
                                       new GetResponseBodyCommandSettings { RequestId = e.RequestId });

                                   string jsonText = body.Base64Encoded
                                       ? Encoding.UTF8.GetString(Convert.FromBase64String(body.Body))
                                       : body.Body;

                                   var json = JObject.Parse(jsonText);
                                   var otp = json["otp"]?.ToString();
                                   if (!string.IsNullOrEmpty(otp))
                                   {
                                       tcsOtp.TrySetResult(otp);
                                       Console.WriteLine($"🔔 OTP capturado: {otp}");
                                   }
                               }
                               catch (Exception ex)
                               {
                                   Console.WriteLine($"Error al obtener cuerpo OTP: {ex.Message}");
                               }
                           }
                       };
            // --- Flujo UI ---
            driver.Navigate().GoToUrl("https://andinavidasegurosdev.linktic.com/");

            var inputUsuario = wait.Until(ExpectedConditions.ElementIsVisible(
                By.XPath("//input[@aria-label='Inserte usuario']")));
            inputUsuario.SendKeys("automatizacion");

            var inputContrasena = wait.Until(ExpectedConditions.ElementIsVisible(
                By.XPath("//input[@aria-label='Inserte clave']")));
            inputContrasena.SendKeys("Hola2025*");

            var botonIngresar = wait.Until(ExpectedConditions.ElementToBeClickable(
                By.XPath("//span[contains(text(), 'Ingresar')]")));
            botonIngresar.Click();
           
                    // --- Esperar el OTP de la red ---
            var otpCode = await Task.WhenAny(tcsOtp.Task, Task.Delay(10000)) == tcsOtp.Task
                ? tcsOtp.Task.Result
                : throw new TimeoutException("OTP no recibido");

            // --- Insertar el OTP en el modal ---
            var inputOtp = wait.Until(ExpectedConditions.ElementIsVisible(
                By.XPath("//input[@placeholder='Inserte código' and @maxlength='6']")));
            inputOtp.SendKeys(otpCode);
         

            var botonEnviar = wait.Until(ExpectedConditions.ElementToBeClickable(
                By.XPath("(//span[contains(text(),'Validar')])[1]")));
            botonEnviar.Click();
            Thread.Sleep(1000);
            //Validar inicio de sesion exitoso
            var bienvenida = wait.Until(ExpectedConditions.ElementIsVisible(
                By.XPath("(//span[normalize-space()='Inicio'])[1]")));

           StringAssert.Contains("Inicio", bienvenida.Text);
      
        }
        [Test]
        //Prueba de Login con credenciales erroneas
        public void LoginCredencialesErroneas()
        {
            driver.Navigate().GoToUrl("https://andinavidasegurosdev.linktic.com/");

            var inputUsuario = wait.Until(ExpectedConditions.ElementIsVisible(
                By.XPath("//input[@aria-label='Inserte usuario']")));
            inputUsuario.SendKeys("automatizacion");

            var inputContrasena = wait.Until(ExpectedConditions.ElementIsVisible(
                By.XPath("//input[@aria-label='Inserte clave']")));
            inputContrasena.SendKeys("Hola2035*");

            var botonIngresar = wait.Until(ExpectedConditions.ElementToBeClickable(
                By.XPath("//span[contains(text(), 'Ingresar')]")));
            botonIngresar.Click();

            var textoError = wait.Until(ExpectedConditions.ElementIsVisible(
                By.XPath("(//div[@class='q-banner__content col text-body2'])[1]")));
            StringAssert.Contains("El usuario y/o contraseña son incorrectos, intente nuevamente", textoError.Text);
        }
        [Test]
        //Prueba de login OTP incorrecta
        public void LoginErrorOTPIncorrecta()
        {
            driver.Navigate().GoToUrl("https://andinavidasegurosdev.linktic.com/");

            var inputUsuario = wait.Until(ExpectedConditions.ElementIsVisible(
                By.XPath("//input[@aria-label='Inserte usuario']")));
            inputUsuario.SendKeys("automatizacion");

            var inputContrasena = wait.Until(ExpectedConditions.ElementIsVisible(
                By.XPath("//input[@aria-label='Inserte clave']")));
            inputContrasena.SendKeys("Hola2025*");

            var botonIngresar = wait.Until(ExpectedConditions.ElementToBeClickable(
                By.XPath("//span[contains(text(), 'Ingresar')]")));
            botonIngresar.Click();

            var inputOtp = wait.Until(ExpectedConditions.ElementIsVisible(
                By.XPath("//input[@placeholder='Inserte código' and @maxlength='6']")));
            inputOtp.SendKeys("123456");

            var botonEnviar = wait.Until(ExpectedConditions.ElementToBeClickable(
                By.XPath("(//span[contains(text(),'Validar')])[1]")));
            botonEnviar.Click();

            var textoError = wait.Until(ExpectedConditions.ElementIsVisible(
                By.XPath("//div[@class='q-notification__message col']")));
            StringAssert.Contains("Error en la validación del OTP", textoError.Text);
        }
        [Test]
        //autorizacion con otp incompleta
        public void LoginErrorOTPIncompleta()
        {
            driver.Navigate().GoToUrl("https://andinavidasegurosdev.linktic.com/");

            var inputUsuario = wait.Until(ExpectedConditions.ElementIsVisible(
                By.XPath("//input[@aria-label='Inserte usuario']")));
            inputUsuario.SendKeys("automatizacion");

            var inputContrasena = wait.Until(ExpectedConditions.ElementIsVisible(
                By.XPath("//input[@aria-label='Inserte clave']")));
            inputContrasena.SendKeys("Hola2025*");

            var botonIngresar = wait.Until(ExpectedConditions.ElementToBeClickable(
                By.XPath("//span[contains(text(), 'Ingresar')]")));
            botonIngresar.Click();

            var inputOtp = wait.Until(ExpectedConditions.ElementIsVisible(
                By.XPath("//input[@placeholder='Inserte código' and @maxlength='6']")));
            inputOtp.SendKeys("123");

            var botonEnviar = wait.Until(ExpectedConditions.ElementToBeClickable(
                By.XPath("(//span[contains(text(),'Validar')])[1]")));
            botonEnviar.Click();

            var textoError = wait.Until(ExpectedConditions.ElementIsVisible(
                By.XPath("//div[@class='q-notification__message col']")));
            StringAssert.Contains("El Código OTP debe tener 6 digitos", textoError.Text);
        }
        /*    [Test]
            //Probar 5 veces la otp
            public void OTPIntentos()
            {
                driver.Navigate().GoToUrl("https://andinavidasegurosdev.linktic.com/");

                var inputUsuario = wait.Until(ExpectedConditions.ElementIsVisible(
                    By.XPath("//input[@aria-label='Inserte usuario']")));
                inputUsuario.SendKeys("automatizacion");

                var inputContrasena = wait.Until(ExpectedConditions.ElementIsVisible(
                    By.XPath("//input[@aria-label='Inserte clave']")));
                inputContrasena.SendKeys("Hola2025*");

                var botonIngresar = wait.Until(ExpectedConditions.ElementToBeClickable(
                    By.XPath("//span[contains(text(), 'Ingresar')]")));
                botonIngresar.Click();

                var inputOtp = wait.Until(ExpectedConditions.ElementIsVisible(
                    By.XPath("//input[@placeholder='Inserte código' and @maxlength='6']")));
                inputOtp.SendKeys("12346");

                var botonEnviar = wait.Until(ExpectedConditions.ElementToBeClickable(
                    By.XPath("(//span[contains(text(),'Validar')])[1]")));
                botonEnviar.Click();

                var inputOtp = wait.Until(ExpectedConditions.ElementIsVisible(
                    By.XPath("//input[@placeholder='Inserte código' and @maxlength='6']")));
                inputOtp.SendKeys("12346");
                Thread.Sleep(2000)
                  var textoError = wait.Until(ExpectedConditions.ElementIsVisible(
                      By.XPath("//div[@class='q-notification__message col']")));
                  StringAssert.Contains("El Código OTP debe tener 6 digitos", textoError.Text);
            }*/
        [TearDown]
        public void Teardown()
        {
            devToolsSession?.Dispose();
            driver.Quit();
            driver?.Dispose();
        }
    }
}
