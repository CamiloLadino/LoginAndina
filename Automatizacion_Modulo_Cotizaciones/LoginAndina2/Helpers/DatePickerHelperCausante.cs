using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using System;
using System.Linq;
using System.Threading;

namespace LoginAndina2.Helpers
{
    /// <summary>
    /// Helper flexible para seleccionar fechas en QDate de Quasar con XPaths configurables.
    /// Permite usar distintos XPaths para los botones internos del datepicker según el caso.
    /// </summary>
    public static class DatePickerHelperCausante
    {
        public class DatePickerXpaths
        {
            public string YearSelectorBtn { get; set; }
            public string YearItems { get; set; }
            public string PrevYearsBtn { get; set; }
            public string NextYearsBtn { get; set; }
            public string MonthSelectorBtn { get; set; }
            public string MonthItems { get; set; }
            public string DayItems { get; set; }
        }

        /// <summary>
        /// Selecciona una fecha en un QDate usando XPaths personalizados para cada botón.
        /// </summary>
        public static void SelectDate(IWebDriver driver, string qdateLabelXPath, string fecha, DatePickerXpaths xpaths, int timeoutSegundos = 5)
        {
            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(timeoutSegundos));
            DateTime dt = DateTime.TryParse(fecha, out var parsed) ? parsed : throw new ArgumentException($"Fecha inválida: {fecha}");
            string dia = dt.Day.ToString();

            // 1. Click en el label para abrir el datepicker
            var label = driver.FindElement(By.XPath(qdateLabelXPath));
            label.Click();
            Thread.Sleep(100);

            // 2. Seleccionar el año
            var anioSelectorBtn = wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath(xpaths.YearSelectorBtn)));
            anioSelectorBtn.Click();
            Thread.Sleep(100);

            string anio = dt.Year.ToString();
            bool encontrado = false;
            int maxIntentos = 20;
            while (!encontrado && maxIntentos-- > 0)
            {
                var aniosVisibles = driver.FindElements(By.XPath(xpaths.YearItems));
                var textos = aniosVisibles.Select(e => e.Text).ToList();
                //Console.WriteLine($"Años visibles (raw): {string.Join(", ", textos.Select(x => $"'{x}'"))}");
                var spanAnios = aniosVisibles.SelectMany(e => e.Text.Split('\n'))
                    .Select(t => t.Trim())
                    .Where(t => int.TryParse(t, out _))
                    .ToList();
                // ¿Está el año buscado en los visibles?
                if (spanAnios.Contains(anio))
                {
                    // Buscar el elemento <span> que contiene el año y hacer click
                    var anioBtn = aniosVisibles.SelectMany(e => e.FindElements(By.XPath(".//span")))
                        .FirstOrDefault(s => s.Text.Trim() == anio);
                    if (anioBtn != null)
                    {
                        try { anioBtn.Click(); } catch { ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].click();", anioBtn); }
                        encontrado = true;
                        break;
                    }
                    // Fallback: click JS sobre el contenedor si solo hay uno
                    if (aniosVisibles.Count == 1)
                    {
                        ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].click();", aniosVisibles[0]);
                        encontrado = true;
                        break;
                    }
                }
                // Si no está, usar flechas para navegar
                if (spanAnios.Any())
                {
                    int min = spanAnios.Select(t => int.Parse(t)).Min();
                    int max = spanAnios.Select(t => int.Parse(t)).Max();
                    if (int.Parse(anio) < min)
                    {
                        var flechaIzq = driver.FindElement(By.XPath(xpaths.PrevYearsBtn));
                        flechaIzq.Click();
                        Thread.Sleep(100);
                    }
                    else if (int.Parse(anio) > max)
                    {
                        var flechaDer = driver.FindElement(By.XPath(xpaths.NextYearsBtn));
                        flechaDer.Click();
                        Thread.Sleep(100);
                    }
                    else
                    {
                        throw new Exception($"No se pudo encontrar el año {anio} en el calendario (visible pero no clickable).");
                    }
                }
                else
                {
                    throw new Exception("No se encontraron años numéricos visibles en el calendario.");
                }
            }
            if (!encontrado) throw new Exception($"No se encontró el año {anio} después de varios intentos.");

            // 3. Seleccionar el mes
            var mesSelectorBtn = wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath(xpaths.MonthSelectorBtn)));
            mesSelectorBtn.Click();
            string[] meses = { "Ene", "Feb", "Mar", "Abr", "May", "Jun", "Jul", "Ago", "Sep", "Oct", "Nov", "Dic" };
            string mesAbrev = meses[dt.Month - 1];
            var mesBtn = wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath(xpaths.MonthItems + $"[text()='{mesAbrev}']")));
            mesBtn.Click();

            // 4. Seleccionar el día
            var day = wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath(xpaths.DayItems + $"[text()='{dia}']")));
            try
            {
                day.Click();
            }
            catch (ElementClickInterceptedException)
            {
                ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].click();", day);
            }
        }
    }
}
