using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using OpenQA.Selenium.DevTools;
using OpenQA.Selenium.DevTools.V138.Network;
using Newtonsoft.Json.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoginAndina2
{
    public class LoginC
    {
        private IWebDriver driver;
        private WebDriverWait wait;

        public LoginC(IWebDriver driver, WebDriverWait wait)
        {
            this.driver = driver;
            this.wait = wait;
        }

        public async Task obtenerOTP()
        {

        }
       
        
        
        
        
        
        public async Task Login()
        {
            // --- Configurar DevTools Network ---
            DevToolsSession devToolsSession = ((ChromeDriver)driver).GetDevToolsSession();
            var devToolsDomains = devToolsSession.GetVersionSpecificDomains<OpenQA.Selenium.DevTools.V138.DevToolsSessionDomains>();
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
                if (!otpRequestIds.Contains(e.RequestId))
                {
                    try
                    {
                        var body = await devToolsDomains.Network.GetResponseBody(new GetResponseBodyCommandSettings { RequestId = e.RequestId });
                        string jsonText = body.Base64Encoded ? Encoding.UTF8.GetString(Convert.FromBase64String(body.Body)) : body.Body;
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
                        //Console.WriteLine($"Error al obtener cuerpo OTP: {ex.Message}");
                    }
                }
            };

            // --- Flujo UI ---
            driver.Navigate().GoToUrl("https://andinavidasegurosdev.linktic.com/");

            var inputUsuario = wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//input[@aria-label='Inserte usuario']")));
            inputUsuario.SendKeys("automatizacion");

            var inputContrasena = wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//input[@aria-label='Inserte clave']")));
            inputContrasena.SendKeys("Hola2025**");

            var botonIngresar = wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//span[contains(text(), 'Ingresar')]")));
            botonIngresar.Click();
            Thread.Sleep(50);

            var inputOtp = wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//input[@placeholder='Inserte código' and @maxlength='6']")));
            var otp = await tcsOtp.Task;
            inputOtp.SendKeys(otp);
            Thread.Sleep(50);
            var botonEnviar = wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("(//span[contains(text(),'Validar')])[1]")));
            botonEnviar.Click();

            // Esperar a que cargue la página después del login
            wait.Until(driver => ((IJavaScriptExecutor)driver).ExecuteScript("return document.readyState").Equals("complete"));
        }
    }
}