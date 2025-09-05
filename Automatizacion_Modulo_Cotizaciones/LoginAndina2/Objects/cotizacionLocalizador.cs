
using OpenQA.Selenium;

namespace LoginAndina2.Object
{
    public class cotizacionLocalizador
    {
        private IWebDriver driver;

        public cotizacionLocalizador(IWebDriver driver)
        {
            this.driver = driver;
        }

        // Localizadores
        public static By HomeLocator => By.XPath("//section[contains(@class, 'home-page')]");

        public static By MenuCotLocator => By.XPath("//p[i[text()='account_balance_wallet'] and contains(.,'Suscripción y cotización')]");

        public static By BtonCrearCot => By.XPath("//div[@class='item'][.//div[normalize-space(.)='Crear Cotización']]");

        public static By PageCrearCot => By.XPath("//i[contains(@class,'material-symbols-rounded') and @style='font-size: 32px;' and text()='account_balance_wallet']");

        public static By BtonGenerarCot => By.XPath("//span[contains(@class,'q-btn__content')][.//span[normalize-space(.)='Generar cotización']]");

        public static By BtonCargueCot => By.XPath("//div[@class='q-item__label' and normalize-space(text())='Cargue cotización']");

        public static By SelecArchivoCot => By.XPath("//div[contains(@class,'q-uploader__header')]//p[normalize-space(.)='Clic aquí para cargar su archivo']");

    }
}