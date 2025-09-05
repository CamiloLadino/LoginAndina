
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using WaitHerlpers.Helpers;

namespace LoginAndina2.Object
{
    public class cotizacionLocalizador
    {
        private readonly IWebDriver driver;
        private readonly WebDriverWait wait;
        private readonly WebDriverWait longWait;
        private readonly WaitHelper waitHelper;

        // Timeouts
        private const int TIMEOUT_PREDETERMINADO_SEGUNDOS = 15;
        private const int TIMEOUT_LARGO_SEGUNDOS = 30;

        public cotizacionLocalizador(IWebDriver driver)
        {
            this.driver = driver;
            this.wait = new WebDriverWait(driver, TimeSpan.FromSeconds(TIMEOUT_PREDETERMINADO_SEGUNDOS));
            this.longWait = new WebDriverWait(driver, TimeSpan.FromSeconds(TIMEOUT_LARGO_SEGUNDOS));
            this.waitHelper = new WaitHelper(driver);
        }

        // Localizadores

        public static By MenuCotLocator => By.XPath("//p[i[text()='account_balance_wallet'] and contains(.,'Suscripción y cotización')]");

        public static By BtonCrearCot => By.XPath("//div[@class='item'][.//div[normalize-space(.)='Crear Cotización']]");

        public static By PageCrearCot => By.XPath("//i[contains(@class,'material-symbols-rounded') and @style='font-size: 32px;' and text()='account_balance_wallet']");

        public static By BtonGenerarCot => By.XPath("//span[contains(@class,'q-btn__content')][.//span[normalize-space(.)='Generar cotización']]");

        public static By BtonCargueCot => By.XPath("//div[@class='q-item__label' and normalize-space(text())='Cargue cotización']");

        public static By SelecArchivoCot => By.XPath("//div[contains(@class,'q-uploader__header')]//p[normalize-space(.)='Clic aquí para cargar su archivo']");

    }
}