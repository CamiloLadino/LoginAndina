using System;
using System.Collections.Generic;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using System.Threading;

namespace LoginAndina2.Helpers
{
    public static class FormHelper
    {
        public static void LlenarCampo(IWebDriver driver, WebDriverWait wait, string xpath, string valor, string nombreCampo)
        {
            if (string.IsNullOrEmpty(valor)) return;
            Console.WriteLine($"[LlenarCampo] Intentando llenar campo '{nombreCampo}' con valor: '{valor}'");
            try
            {
                wait.Until(ExpectedConditions.ElementIsVisible(By.XPath(xpath)));
                var elemento = wait.Until(drv =>
                {
                    try
                    {
                        var element = drv.FindElement(By.XPath(xpath));
                        return (element.Displayed && element.Enabled) ? element : null;
                    }
                    catch { return null; }
                });
                Console.WriteLine($"[LlenarCampo] Elemento encontrado y visible para el campo '{nombreCampo}'. Llenando valor...");
                ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].scrollIntoView({behavior: 'smooth', block: 'center', inline: 'center'});", elemento);
                elemento.Clear();
                elemento.SendKeys(valor);
                Thread.Sleep(100);
                Console.WriteLine($"[LlenarCampo] Campo '{nombreCampo}' llenado correctamente.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[LlenarCampo][ERROR] Falló llenando campo '{nombreCampo}'. Error: {ex.Message}");
                throw;
            }
        }

        public static void SeleccionarOpcion(IWebDriver driver, WebDriverWait wait, string xpath, string valor, string nombreCampo)
        {
            if (string.IsNullOrEmpty(valor)) return;
            var campo = wait.Until(drv =>
            {
                try
                {
                    var element = drv.FindElement(By.XPath(xpath));
                    
                    return (element.Displayed && element.Enabled) ? element : null;
                }
                catch { return null; }
            });
            ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].click();", campo);
            Thread.Sleep(100);
            var opcionXPath = $"//div[contains(@class, 'q-item__label') and normalize-space()='" + valor + "']";
            IWebElement opcion = null;
            int intentos = 0;
            while (intentos < 30)
            {
                try
                {
                    opcion = driver.FindElement(By.XPath(opcionXPath));
                    if (opcion.Displayed && opcion.Enabled)
                        break;
                }
                catch
                {
                    bool hizoScroll = false;
                    try
                    {
                        var contenedor = campo.FindElement(By.XPath("ancestor::div[contains(@class,'q-menu')]"));
                        ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].scrollTop = arguments[0].scrollTop + 400;", contenedor);
                        Console.WriteLine($"[SeleccionarOpcion] Scroll en ancestro q-menu (intento {intentos+1}) para el campo '{nombreCampo}' con valor '{valor}'");
                        hizoScroll = true;
                    }
                    catch {}
                    if (!hizoScroll)
                    {
                        try
                        {
                            var menus = driver.FindElements(By.XPath("//div[contains(@class,'q-menu') and not(contains(@style,'display: none'))]"));
                            foreach (var menu in menus)
                            {
                                ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].scrollTop = arguments[0].scrollTop + 400;", menu);
                                Console.WriteLine($"[SeleccionarOpcion] Scroll en q-menu global (intento {intentos+1}) para el campo '{nombreCampo}' con valor '{valor}'");
                                hizoScroll = true;
                            }
                        }
                        catch {}
                    }
                }
                Thread.Sleep(100);
                intentos++;
            }
            if (opcion == null || !opcion.Displayed)
                throw new Exception($"No se pudo encontrar la opción '{valor}' en el selector del campo '{nombreCampo}' después de hacer scroll.");
            ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].click();", opcion);
            Thread.Sleep(100);
        }
    }
}
