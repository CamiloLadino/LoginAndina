using LoginAndina2;
using OpenQA.Selenium;
using LoginAndina2.Object;

namespace LoginAndina2.Models;

public class EmisionModel
{
    private IWebDriver driver;
    private emisionLocalizadores emisionL = null!;

    public EmisionModel(IWebDriver driver)
    {
        this.driver = driver;
        this.emisionL = new emisionLocalizadores(driver);
    }

    public void EmisionPol()
    {
        try
        {
            Console.WriteLine("🔑 Desplegando SubMenu de Emisión...");
            emisionL.IngresarEmision();
            Console.WriteLine("✅ Menu de emisión desplegado exitosamente");
            
            // Esperar a que la página se cargue completamente
            emisionL.EsperarCargaPaginaEmision();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"❌ Error durante la navegación al menú de emisión: {ex.Message}");
            throw;
        }
    }

    public void EmisionPolMas()
    {
        try
        {
            Console.WriteLine("🔄 Seleccionando múltiples pólizas...");
            emisionL.EmitirMasPol();
            Console.WriteLine("✅ Múltiples pólizas seleccionadas y emitidas exitosamente");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"❌ Error en la selección de múltiples pólizas: {ex.Message}");
            throw;
        }
    }

    public void EmisionPolOne(string numeroCotizacion = "COT-00002023")
    {
        try
        {
            Console.WriteLine($"🔄 Seleccionando póliza individual: {numeroCotizacion}...");
            emisionL.EmitirOnePol(numeroCotizacion);
            Console.WriteLine($"✅ Póliza {numeroCotizacion} seleccionada y emitida exitosamente");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"❌ Error durante la selección de la cotización {numeroCotizacion}: {ex.Message}");
            throw;
        }
    }

    /// <summary>
    /// Método para probar con diferentes números de cotización
    /// </summary>
    // public void EmisionPolOneConReintento(string numeroCotizacion = "COT-00002024")
    // {
    //     const int maxReintentos = 3;
        
    //     for (int intento = 1; intento <= maxReintentos; intento++)
    //     {
    //         try
    //         {
    //             Console.WriteLine($"🔄 Intento {intento}/{maxReintentos}: Seleccionando póliza {numeroCotizacion}...");
    //             emisionL.EmitirOnePol(numeroCotizacion);
    //             Console.WriteLine($"✅ Póliza {numeroCotizacion} emitida exitosamente en el intento {intento}");
    //             return; // Éxito, salir del loop
    //         }
    //         catch (Exception ex)
    //         {
    //             Console.WriteLine($"❌ Intento {intento} falló: {ex.Message}");
                
    //             if (intento == maxReintentos)
    //             {
    //                 Console.WriteLine($"❌ Todos los intentos fallaron para la cotización {numeroCotizacion}");
    //                 throw;
    //             }
                
    //             // Esperar antes del siguiente intento
    //             Thread.Sleep(2000);
    //         }
    //     }
    // }
}