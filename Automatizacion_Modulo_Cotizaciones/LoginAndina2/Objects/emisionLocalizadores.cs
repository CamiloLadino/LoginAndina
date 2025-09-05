using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using WaitHerlpers.Helpers;
using System.Collections.ObjectModel;

namespace LoginAndina2.Object
{
    public class EmisionL
    {
        private readonly IWebDriver driver;
        private readonly WebDriverWait wait;
        private readonly WebDriverWait longWait;
        private readonly WaitHelper waitHelper;

        // Timeouts
        private const int TIMEOUT_PREDETERMINADO_SEGUNDOS = 15;
        private const int TIMEOUT_LARGO_SEGUNDOS = 30;

        public EmisionL(IWebDriver driver)
        {
            this.driver = driver;
            this.wait = new WebDriverWait(driver, TimeSpan.FromSeconds(TIMEOUT_PREDETERMINADO_SEGUNDOS));
            this.longWait = new WebDriverWait(driver, TimeSpan.FromSeconds(TIMEOUT_LARGO_SEGUNDOS));
            this.waitHelper = new WaitHelper(driver);
        }

        // Localizadores 
        private By HomeLocator => By.XPath("//section[contains(@class, 'home-page')]");
        private By EmiPageLocator => By.XPath("//h3[@class='table__title' and text()='Listado de pólizas']");
        private By TablaPolizasLocator => By.XPath("//table | //div[contains(@class, 'table')] | //div[contains(@class, 'q-table')]");
        private By BtnEmisionLocator => By.XPath("//p[.//i[text()='article'] and contains(., 'Emisión')]");
        private By BtnEmiPolLocator => By.XPath("//div[@class='item']/div[contains(text(), 'Emisión de Pólizas')]");
        private By CheckPolLocator => By.XPath("//div[contains(@class, 'q-checkbox') and @role='checkbox']");
        private By EmitPolLocator => By.XPath("/html[1]/body[1]/div[1]/div[1]/div[3]/main[1]/div[1]/div[2]/button[3]/span[2]/span[1]");

        private By BtnSiLocator => By.XPath("//button[@type='button'][span//span[text()='Sí']]");

        private By MensajeEmiInicioLocator => By.XPath("//span[contains(text(), 'Emisión completada')]");

        private By MensajeEmiCompleteLocator => By.XPath("//span[contains(text(), 'Emisión completada')]");

        public void IngresarEmision()
        {
            try
            {
                Console.WriteLine("🔄 Navegando a la sección de Emisión...");

                wait.Until(ExpectedConditions.ElementIsVisible(HomeLocator));
                ClickearElemento(BtnEmisionLocator, "Botón de Emisión");
                ClickearElemento(BtnEmiPolLocator, "Botón de Emisión de Pólizas");

                Console.WriteLine("✅ Navegación a Emisión completada.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ Error al ingresar a la sección de Emisión: {ex.Message}");
                throw;
            }
        }

        public void EmitirMasPol()
        {
            try
            {
                Console.WriteLine("🔄 Iniciando emisión múltiple de pólizas...");
                
                EsperarCargaPaginaEmision();
                ClickearElemento(CheckPolLocator, "Checkbox general");
                EmitirPolizas();
                
                Console.WriteLine("✅ Múltiples pólizas emitidas exitosamente.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ Error durante la emisión múltiple de pólizas: {ex.Message}");
                throw;
            }
        }

        public void EmitirOnePol(string numeroCotizacion)
        {
            ValidarNumeroCotizacion(numeroCotizacion);

            try
            {
                Console.WriteLine($"🔄 Iniciando emisión de póliza para cotización: {numeroCotizacion}");

                EsperarCargaPaginaEmision();

                if (!ExisteCotizacion(numeroCotizacion))
                {
                    MostrarCotizacionesDisponibles();
                    throw new NoSuchElementException($"Cotización {numeroCotizacion} no encontrada");
                }

                // CAMBIO PRINCIPAL: Usar JavaScript para hacer clic más confiable
                IWebElement checkBoxPol = EncontrarCheckboxCotizacion(numeroCotizacion);

                // Verificar si ya está seleccionado
                if (!EstaCheckboxSeleccionado(checkBoxPol))
                {
                    HacerClicConJavaScript(checkBoxPol);
                    Console.WriteLine($"✅ Checkbox para la cotización {numeroCotizacion} seleccionado.");

                    // Esperar un momento para que se procese la selección
                    Thread.Sleep(1000);

                    // Verificar que efectivamente quedó seleccionado
                    if (!EstaCheckboxSeleccionado(checkBoxPol))
                    {
                        throw new Exception("El checkbox no se pudo seleccionar correctamente");
                    }
                }

                EmitirPolizas();
                Console.WriteLine("✅ Póliza única emitida exitosamente.");

                // Dar clic en el botón "Si" del popup
                wait.Until(ExpectedConditions.ElementToBeClickable(BtnSiLocator));
                driver.FindElement(BtnSiLocator).Click();

                wait.Until(ExpectedConditions.ElementIsVisible(MensajeEmiInicioLocator));
                Console.WriteLine("🟡 Iniciando emisión detectado...");
                //Verificar mensajes de emisión
                try
                {

                    wait.Until(ExpectedConditions.InvisibilityOfElementLocated(MensajeEmiInicioLocator));
                    Console.WriteLine("✅ Mensaje 'Iniciando emisión...' desapareció.");

                    wait.Until(ExpectedConditions.ElementIsVisible(MensajeEmiCompleteLocator));
                    Console.WriteLine("🎉 Emisión completada detectada.");

                    try
                    {

                        wait.Until(ExpectedConditions.InvisibilityOfElementLocated(MensajeEmiCompleteLocator));
                        Console.WriteLine("✅ Emisión completada exitosamente.");

                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("⚠️ Timeout esperando algún mensaje: " + ex.Message);
                    }

                }
                catch(Exception ex)
                {
                        Console.WriteLine("⚠️ Timeout esperando algún mensaje: " + ex.Message);

                }

            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ Error al emitir la póliza para la cotización {numeroCotizacion}: {ex.Message}");
                MostrarCotizacionesDisponibles();
                throw;
            }
        }

        // Métodos auxiliares simplificados y optimizados
        private void ClickearElemento(By localizador, string descripcion)
        {
            var elemento = wait.Until(ExpectedConditions.ElementToBeClickable(localizador));
            elemento.Click();
            Console.WriteLine($"✅ {descripcion} clickeado.");
        }

        private void EmitirPolizas()
        {
            Console.WriteLine("🔄 Esperando que el botón de emisión se habilite...");
            
            // Esperar hasta que el botón esté habilitado Y visible
            longWait.Until(driver => 
            {
                try
                {
                    var boton = driver.FindElement(EmitPolLocator);
                    return boton.Enabled && boton.Displayed;
                }
                catch
                {
                    return false;
                }
            });
            
            // Usar JavaScript para hacer clic más confiable
            var botonEmitir = driver.FindElement(EmitPolLocator);
            HacerClicConJavaScript(botonEmitir);
            Console.WriteLine("✅ Botón de emisión clickeado.");
        }

        private void HacerClicConJavaScript(IWebElement elemento)
        {
            ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].click();", elemento);
        }

        private bool EstaCheckboxSeleccionado(IWebElement checkbox)
        {
            try
            {
                // Para checkboxes de Quasar, verificar el atributo aria-checked
                var ariaChecked = checkbox.GetAttribute("aria-checked");
                if (!string.IsNullOrEmpty(ariaChecked))
                {
                    return ariaChecked.Equals("true", StringComparison.OrdinalIgnoreCase);
                }
                
                // Fallback para checkboxes normales
                return checkbox.Selected;
            }
            catch
            {
                return false;
            }
        }

        private void ValidarNumeroCotizacion(string numeroCotizacion)
        {
            if (string.IsNullOrWhiteSpace(numeroCotizacion))
            {
                throw new ArgumentException("El número de cotización no puede ser nulo o vacío", nameof(numeroCotizacion));
            }
        }

        private IWebElement EncontrarCheckboxCotizacion(string numeroCotizacion)
        {
            var estrategias = new[]
            {
                By.XPath($"//div[contains(@class, 'q-checkbox') and ancestor::tr[contains(., '{numeroCotizacion}')]]"),
                By.XPath($"//tr[contains(., '{numeroCotizacion}')]//div[contains(@class, 'q-checkbox')]"),
                By.XPath($"//input[@type='checkbox' and ancestor::tr[contains(., '{numeroCotizacion}')]]")
            };

            foreach (var estrategia in estrategias)
            {
                try
                {
                    Console.WriteLine($"🔄 Intentando localizar checkbox con: {estrategia}");
                    var elemento = wait.Until(ExpectedConditions.ElementToBeClickable(estrategia));
                    Console.WriteLine($"✅ Checkbox encontrado.");
                    return elemento;
                }
                catch (WebDriverTimeoutException)
                {
                    continue;
                }
            }

            throw new NoSuchElementException($"No se pudo encontrar el checkbox para la cotización {numeroCotizacion}");
        }

        private bool ExisteCotizacion(string numeroCotizacion)
        {
            try
            {
                driver.FindElement(By.XPath($"//*[contains(text(), '{numeroCotizacion}')]"));
                return true;
            }
            catch (NoSuchElementException)
            {
                return false;
            }
        }

        private void EsperarCargaTablaPolizas()
        {
            Console.WriteLine("🔄 Esperando que la tabla de pólizas se cargue...");
            longWait.Until(ExpectedConditions.ElementIsVisible(TablaPolizasLocator));
            Thread.Sleep(2000); // Reducido de 3000ms
            Console.WriteLine("✅ Tabla de pólizas cargada.");
        }

        private void MostrarCotizacionesDisponibles()
        {
            try
            {
                Console.WriteLine("🔍 Cotizaciones disponibles en la tabla:");
                
                var posiblesCotizaciones = driver.FindElements(By.XPath("//td[contains(text(), 'COT-') or contains(text(), 'cot-')]"));
                
                if (posiblesCotizaciones.Count > 0)
                {
                    foreach (var cotizacion in posiblesCotizaciones.Take(10))
                    {
                        Console.WriteLine($"   - {cotizacion.Text.Trim()}");
                    }
                }
                else
                {
                    Console.WriteLine("   No se encontraron cotizaciones en la tabla.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ Error al mostrar cotizaciones disponibles: {ex.Message}");
            }
        }

        public bool PuedeEmitirPoliza()
        {
            try
            {
                var boton = wait.Until(ExpectedConditions.ElementIsVisible(EmitPolLocator));
                return boton.Enabled;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ Error al verificar el estado del botón de emisión: {ex.Message}");
                return false;
            }
        }

        public void EsperarCargaPaginaEmision()
        {
            try
            {
                longWait.Until(ExpectedConditions.ElementIsVisible(EmiPageLocator));
                EsperarCargaTablaPolizas();
                Console.WriteLine("✅ Página de emisión cargada exitosamente.");
            }
            catch (WebDriverTimeoutException ex)
            {
                Console.WriteLine($"❌ Tiempo de espera agotado esperando que la página de emisión se cargue: {ex.Message}");
                throw;
            }
        }
    }
}