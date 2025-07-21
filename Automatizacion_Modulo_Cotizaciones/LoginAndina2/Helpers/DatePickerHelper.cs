/*using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using System;

namespace LoginAndina2.Helpers
{
    public static class DatePickerHelper
    {
        /// <summary>
        /// Selecciona una fecha en un QDate de Quasar Framework simulando clicks humanos.
        /// </summary>
        /// <param name="driver">WebDriver de Selenium</param>
        /// <param name="qdateLabelXPath">XPath del label o contenedor principal del QDate</param>
        /// <param name="fecha">Fecha a seleccionar (DateTime o string en formato dd/MM/yyyy, yyyy-MM-dd, etc.)</param>
        /// <param name="timeoutSegundos">Timeout en segundos para esperas explícitas</param>
        public static void SelectDate(IWebDriver driver, string qdateLabelXPath, string fecha, int timeoutSegundos = 5)
        {
            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(timeoutSegundos));
            // Parsear la fecha
            DateTime dt = DateTime.TryParse(fecha, out var parsed) ? parsed : throw new ArgumentException($"Fecha inválida: {fecha}");
            string dia = dt.Day.ToString();

            // Encontrar el label/contenedor del QDate
            var label = driver.FindElement(By.XPath(qdateLabelXPath));
            label.Click();
            Thread.Sleep(1000);
            // Seleccionar el año
            // 1. Click en el botón para abrir la selección de año
            var anioSelectorBtn = wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("/html[1]/body[1]/div[3]/div[1]/div[1]/div[2]/div[1]/div[1]/div[1]/div[5]/div[1]/button[1]/span[2]/span[1]")));
            anioSelectorBtn.Click();

            // 2. Buscar el año deseado o navegar con flechas
            string anio = dt.Year.ToString();
            bool encontrado = false;
            int maxIntentos = 20; // Evita loops infinitos
            while (!encontrado && maxIntentos-- > 0)
            {
                var aniosVisibles = driver.FindElements(By.XPath("//div[contains(@class,'q-date__years-item')]//span[@class='block']"));
                var textos = aniosVisibles.Select(e => e.Text).ToList();
                Console.WriteLine($"Años visibles: {string.Join(", ", textos)}");
                // ¿Está el año deseado visible?
                var anioBtn = aniosVisibles.FirstOrDefault(e => e.Text == anio);
                if (anioBtn != null)
                {
                    anioBtn.Click();
                    encontrado = true;
                    break;
                }
                // Si no está, navegar con flechas
                int min = textos.Select(t => int.Parse(t)).Min();
                int max = textos.Select(t => int.Parse(t)).Max();
                if (int.Parse(anio) < min)
                {
                    // Click flecha izquierda
                    var flechaIzq = driver.FindElement(By.XPath("//button[@aria-label='Anterior 20 años']//span[@class='q-btn__content text-center col items-center q-anchor--skip justify-center row']"));
                    flechaIzq.Click();
                    Thread.Sleep(200);
                }
                else if (int.Parse(anio) > max)
                {
                    // Click flecha derecha
                    var flechaDer = driver.FindElement(By.XPath("//button[@aria-label='Siguiente 20 años']//span[@class='q-btn__content text-center col items-center q-anchor--skip justify-center row']"));
                    flechaDer.Click();
                    Thread.Sleep(200);
                }
                else
                {
                    throw new Exception($"No se pudo encontrar el año {anio} en el calendario.");
                }
            }
            if (!encontrado) throw new Exception($"No se encontró el año {anio} después de varios intentos.");

            // Seleccionar el mes
            var mesSelectorBtn = wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("/html[1]/body[1]/div[3]/div[1]/div[1]/div[2]/div[1]/div[1]/div[1]/div[2]/div[1]/button[1]/span[2]/span[1]")));
            mesSelectorBtn.Click();
            string[] meses = { "Ene", "Feb", "Mar", "Abr", "May", "Jun", "Jul", "Ago", "Sep", "Oct", "Nov", "Dic" };
            string mesAbrev = meses[dt.Month - 1];
            Console.WriteLine($"Seleccionando mes: {mesAbrev}");
            var mesBtn = wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath($"//span[@class='block' and text()='{mesAbrev}']")));
            mesBtn.Click();

            // Seleccionar el día (igual que antes)
            var day = wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath($"//div[contains(@class,'q-date__calendar-item')]//button[.//span[@class='block' and text()='{dia}']]")));
            Console.WriteLine($"El dia es {dia}");
            try
            {
                day.Click();
            }
            catch (ElementClickInterceptedException)
            {
                Console.WriteLine($"[DatePickerHelper] Click interceptado al seleccionar el día {dia}, intentando con JavaScript...");
                ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].click();", day);
            }
        }
    }
}*/