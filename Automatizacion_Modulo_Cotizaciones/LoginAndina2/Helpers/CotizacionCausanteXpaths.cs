namespace LoginAndina2.Helpers
{
    public static class CotizacionCausanteXpaths
    {
        public static readonly Dictionary<string, string> DatosCausante = new()
        {
            ["TipoIdentificacion"] = "/html[1]/body[1]/div[1]/div[1]/div[3]/main[1]/div[3]/div[2]/div[1]/div[1]/div[1]/div[1]/div[1]/div[1]/div[2]/form[1]/div[1]/div[2]/div[1]/label[1]/label[1]/div[1]/div[1]/div[1]/div[1]",
            ["Identificacion"] = "/html[1]/body[1]/div[1]/div[1]/div[3]/main[1]/div[3]/div[2]/div[1]/div[1]/div[1]/div[1]/div[1]/div[1]/div[2]/form[1]/div[1]/div[3]/div[1]/label[1]/label[1]/div[1]/div[1]/div[1]/input[1]",
            ["PrimerNombre"] = "/html[1]/body[1]/div[1]/div[1]/div[3]/main[1]/div[3]/div[2]/div[1]/div[1]/div[1]/div[1]/div[1]/div[1]/div[2]/form[1]/div[1]/div[5]/div[1]/label[1]/label[1]/div[1]/div[1]/div[1]/input[1]",
            ["SegundoNombre"] = "/html[1]/body[1]/div[1]/div[1]/div[3]/main[1]/div[3]/div[2]/div[1]/div[1]/div[1]/div[1]/div[1]/div[1]/div[2]/form[1]/div[1]/div[6]/div[1]/label[1]/label[1]/div[1]/div[1]/div[1]/input[1]",
            ["PrimerApellido"] = "/html[1]/body[1]/div[1]/div[1]/div[3]/main[1]/div[3]/div[2]/div[1]/div[1]/div[1]/div[1]/div[1]/div[1]/div[2]/form[1]/div[1]/div[7]/div[1]/label[1]/label[1]/div[1]/div[1]/div[1]/input[1]",
            ["SegundoApellido"] = "/html[1]/body[1]/div[1]/div[1]/div[3]/main[1]/div[3]/div[2]/div[1]/div[1]/div[1]/div[1]/div[1]/div[1]/div[2]/form[1]/div[1]/div[8]/div[1]/label[1]/label[1]/div[1]/div[1]/div[1]/input[1]",
            ["FechaExpedicionDocumento"] = "/html[1]/body[1]/div[1]/div[1]/div[3]/main[1]/div[3]/div[2]/div[1]/div[1]/div[1]/div[1]/div[1]/div[1]/div[2]/form[1]/div[1]/div[4]/div[1]/div[1]/label[1]/label[1]/div[1]/div[1]/div[2]/input[1]",
            ["FechaNacimiento"] = "/html[1]/body[1]/div[1]/div[1]/div[3]/main[1]/div[3]/div[2]/div[1]/div[1]/div[1]/div[1]/div[1]/div[1]/div[2]/form[1]/div[1]/div[9]/div[1]/div[1]/label[1]/label[1]/div[1]/div[1]/div[2]/input[1]",
            ["Sexo"] = "/html[1]/body[1]/div[1]/div[1]/div[3]/main[1]/div[3]/div[2]/div[1]/div[1]/div[1]/div[1]/div[1]/div[1]/div[2]/form[1]/div[1]/div[10]/div[1]/label[1]/label[1]/div[1]/div[1]/div[1]/div[1]",
            ["EstadoCivil"] = "/html[1]/body[1]/div[1]/div[1]/div[3]/main[1]/div[3]/div[2]/div[1]/div[1]/div[1]/div[1]/div[1]/div[1]/div[2]/form[1]/div[1]/div[11]/div[1]/label[1]/label[1]/div[1]/div[1]/div[1]/div[1]",
            ["EstadoCausante"] = "/html[1]/body[1]/div[1]/div[1]/div[3]/main[1]/div[3]/div[2]/div[1]/div[1]/div[1]/div[1]/div[1]/div[1]/div[2]/form[1]/div[1]/div[12]/div[1]/label[1]/label[1]/div[1]/div[1]/div[1]/div[1]",
            ["EPS"] = "/html[1]/body[1]/div[1]/div[1]/div[3]/main[1]/div[3]/div[2]/div[1]/div[1]/div[1]/div[1]/div[1]/div[1]/div[2]/form[1]/div[1]/div[13]/div[1]/label[1]/label[1]/div[1]/div[1]/div[1]/div[1]",
            ["ResidenteExterior"] = "/html[1]/body[1]/div[1]/div[1]/div[3]/main[1]/div[3]/div[2]/div[1]/div[1]/div[1]/div[1]/div[1]/div[1]/div[2]/form[1]/div[1]/div[14]/div[1]/label[1]/label[1]/div[1]/div[1]/div[1]/div[1]"
        };

        public static readonly Dictionary<string, string> DatosPresentacion = new()
        {
            ["SemanasCotizadas"] = "/html[1]/body[1]/div[1]/div[1]/div[3]/main[1]/div[3]/div[2]/div[1]/div[1]/div[1]/div[1]/div[2]/div[1]/div[2]/form[1]/div[1]/div[8]/div[1]/label[1]/label[1]/div[1]/div[1]/div[1]/input",
            ["IBL"] = "/html[1]/body[1]/div[1]/div[1]/div[3]/main[1]/div[3]/div[2]/div[1]/div[1]/div[1]/div[1]/div[2]/div[1]/div[2]/form[1]/div[1]/div[9]/label[1]/label[1]/div[1]/div[1]/div[1]/input[1]",
            ["PorcentajePCLDerecho"] = "/html[1]/body[1]/div[1]/div[1]/div[3]/main[1]/div[3]/div[2]/div[1]/div[1]/div[1]/div[1]/div[2]/div[1]/div[2]/form[1]/div[1]/div[1]/div[1]/label[1]/label[1]/div[1]/div[1]/div[1]/input[1]",
            ["PorcentajePCLActual"] = "/html[1]/body[1]/div[1]/div[1]/div[3]/main[1]/div[3]/div[2]/div[1]/div[1]/div[1]/div[1]/div[2]/div[1]/div[2]/form[1]/div[1]/div[2]/div[1]/label[1]/label[1]/div[1]/div[1]/div[1]/input[1]",
            ["FechaCausacionDerecho"] = "/html[1]/body[1]/div[1]/div[1]/div[3]/main[1]/div[3]/div[2]/div[1]/div[1]/div[1]/div[1]/div[2]/div[1]/div[2]/form[1]/div[1]/div[3]/div[1]/div[1]/label[1]/label[1]/div[1]/div[1]/div[2]/input[1]",
            ["FechaMuerte"] = "/html[1]/body[1]/div[1]/div[1]/div[3]/main[1]/div[3]/div[2]/div[1]/div[1]/div[1]/div[1]/div[2]/div[1]/div[2]/form[1]/div[1]/div[4]/div[1]/div[1]/label[1]/label[1]/div[1]",
            ["OrigenInicialPrestacion"] = "/html[1]/body[1]/div[1]/div[1]/div[3]/main[1]/div[3]/div[2]/div[1]/div[1]/div[1]/div[1]/div[2]/div[1]/div[2]/form[1]/div[1]/div[6]/div[1]/label[1]/label[1]/div[1]/div[1]/div[1]/div[1]"
        };

        public static readonly Dictionary<string, string> DatosBancarios = new()
        {
            ["PrimerNombre"] = "/html[1]/body[1]/div[1]/div[1]/div[3]/main[1]/div[3]/div[2]/div[1]/div[1]/div[1]/div[1]/div[3]/div[1]/div[2]/form[1]/div[1]/div[3]/div[1]/label[1]/label[1]/div[1]/div[1]/div[1]/input[1]",
            ["SegundoNombre"] = "/html[1]/body[1]/div[1]/div[1]/div[3]/main[1]/div[3]/div[2]/div[1]/div[1]/div[1]/div[1]/div[3]/div[1]/div[2]/form[1]/div[1]/div[4]/div[1]/label[1]/label[1]/div[1]/div[1]/div[1]/input[1]",
            ["PrimerApellido"] = "/html[1]/body[1]/div[1]/div[1]/div[3]/main[1]/div[3]/div[2]/div[1]/div[1]/div[1]/div[1]/div[3]/div[1]/div[2]/form[1]/div[1]/div[5]/div[1]/label[1]/label[1]/div[1]/div[1]/div[1]/input[1]",
            ["SegundoApellido"] = "/html[1]/body[1]/div[1]/div[1]/div[3]/main[1]/div[3]/div[2]/div[1]/div[1]/div[1]/div[1]/div[3]/div[1]/div[2]/form[1]/div[1]/div[6]/div[1]/label[1]/label[1]/div[1]/div[1]/div[1]/input[1]",
            ["IdentificacionTerceroPago"] = "/html[1]/body[1]/div[1]/div[1]/div[3]/main[1]/div[3]/div[2]/div[1]/div[1]/div[1]/div[1]/div[3]/div[1]/div[2]/form[1]/div[1]/div[7]/div[1]/label[1]/label[1]/div[1]/div[1]/div[1]/input[1]",
            ["NumeroCuentaBancaria"] = "/html[1]/body[1]/div[1]/div[1]/div[3]/main[1]/div[3]/div[2]/div[1]/div[1]/div[1]/div[1]/div[3]/div[1]/div[2]/form[1]/div[1]/div[11]/label[1]/label[1]/div[1]/div[1]/div[1]/input[1]",
            ["TipoTerceroPago"] = "/html[1]/body[1]/div[1]/div[1]/div[3]/main[1]/div[3]/div[2]/div[1]/div[1]/div[1]/div[1]/div[3]/div[1]/div[2]/form[1]/div[1]/div[1]/div[1]/label[1]/label[1]/div[1]/div[1]/div[1]",
            ["TipoIdentificacionTerceroPago"] = "/html[1]/body[1]/div[1]/div[1]/div[3]/main[1]/div[3]/div[2]/div[1]/div[1]/div[1]/div[1]/div[3]/div[1]/div[2]/form[1]/div[1]/div[2]/div[1]/label[1]/label[1]/div[1]/div[1]/div[1]",
            ["TipoCuenta"] = "/html[1]/body[1]/div[1]/div[1]/div[3]/main[1]/div[3]/div[2]/div[1]/div[1]/div[1]/div[1]/div[3]/div[1]/div[2]/form[1]/div[1]/div[10]/div[1]/label[1]/label[1]/div[1]/div[1]/div[1]/div[1]",
            ["EntidadBancaria"] = "/html[1]/body[1]/div[1]/div[1]/div[3]/main[1]/div[3]/div[2]/div[1]/div[1]/div[1]/div[1]/div[3]/div[1]/div[2]/form[1]/div[1]/div[9]/div[1]/label[1]/label[1]/div[1]/div[1]/div[1]/div[1]",
            ["FormaPago"] = "/html[1]/body[1]/div[1]/div[1]/div[3]/main[1]/div[3]/div[2]/div[1]/div[1]/div[1]/div[1]/div[3]/div[1]/div[2]/form[1]/div[1]/div[8]/div[1]/label[1]/label[1]/div[1]/div[1]/div[1]/div[1]"
        };

        public static readonly string BtnContinuar = "//span[normalize-space()='Continuar']";

        public static readonly Dictionary<string, string> DatosContacto = new()
        {
            ["DepartamentoNacimiento"] = "/html[1]/body[1]/div[1]/div[1]/div[3]/main[1]/div[3]/div[2]/div[1]/div[1]/div[1]/div[1]/div[4]/div[1]/div[2]/form[1]/div[1]/div[1]/div[1]/label[1]/label[1]/div[1]/div[1]/div[1]/div[1]",
            ["CiudadNacimiento"] = "/html[1]/body[1]/div[1]/div[1]/div[3]/main[1]/div[3]/div[2]/div[1]/div[1]/div[1]/div[1]/div[4]/div[1]/div[2]/form[1]/div[1]/div[2]/div[1]/label[1]/label[1]/div[1]/div[1]/div[1]/div[1]",
            ["PaisResidencia"] = "/html[1]/body[1]/div[1]/div[1]/div[3]/main[1]/div[3]/div[2]/div[1]/div[1]/div[1]/div[1]/div[4]/div[1]/div[2]/form[1]/div[1]/div[4]/div[1]/label[1]/label[1]/div[1]/div[1]/div[1]/div[1]",
            ["DepartamentoResidencia"] = "/html[1]/body[1]/div[1]/div[1]/div[3]/main[1]/div[3]/div[2]/div[1]/div[1]/div[1]/div[1]/div[4]/div[1]/div[2]/form[1]/div[1]/div[5]/div[1]/label[1]/label[1]/div[1]/div[1]/div[1]",
            ["CiudadResidencia"] = "/html[1]/body[1]/div[1]/div[1]/div[3]/main[1]/div[3]/div[2]/div[1]/div[1]/div[1]/div[1]/div[4]/div[1]/div[2]/form[1]/div[1]/div[6]/div[1]/label[1]/label[1]/div[1]/div[1]/div[1]/div[1]",
            ["DireccionYBarrio"] = "/html[1]/body[1]/div[1]/div[1]/div[3]/main[1]/div[3]/div[2]/div[1]/div[1]/div[1]/div[1]/div[4]/div[1]/div[2]/form[1]/div[1]/div[3]/div[1]/label[1]/label[1]/div[1]/div[1]/div[1]/input[1]",
            ["TelefonoFijo"] = "/html[1]/body[1]/div[1]/div[1]/div[3]/main[1]/div[3]/div[2]/div[1]/div[1]/div[1]/div[1]/div[4]/div[1]/div[2]/form[1]/div[1]/div[7]/div[1]/label[1]/label[1]/div[1]/div[1]/div[1]/input[1]",
            ["Celular"] = "/html[1]/body[1]/div[1]/div[1]/div[3]/main[1]/div[3]/div[2]/div[1]/div[1]/div[1]/div[1]/div[4]/div[1]/div[2]/form[1]/div[1]/div[8]/div[1]/label[1]/label[1]/div[1]/div[1]/div[1]/input[1]",
            ["CorreoElectronico"] = "/html[1]/body[1]/div[1]/div[1]/div[3]/main[1]/div[3]/div[2]/div[1]/div[1]/div[1]/div[1]/div[4]/div[1]/div[2]/form[1]/div[1]/div[9]/div[1]/label[1]/label[1]/div[1]/div[1]/div[1]/input[1]"
        };
    }
}
