namespace LoginAndina2.Helpers
{
    public static class CotizacionBeneficiarioXpaths
    {
        public static readonly Dictionary<string, string> DatosBeneficiario = new()
        {
            // Ejemplo de campos, ajusta los XPaths según tu formulario real
            ["TipoIdentificacion"] = "/html[1]/body[1]/div[1]/div[1]/div[3]/main[1]/div[3]/div[2]/div[1]/div[1]/div[1]/div[1]/section[1]/div[1]/div[1]/div[2]/form[1]/div[1]/div[1]/div[1]/label[1]/label[1]/div[1]/div[1]/div[1]/div[1]",
            ["Identificacion"] = "/html[1]/body[1]/div[1]/div[1]/div[3]/main[1]/div[3]/div[2]/div[1]/div[1]/div[1]/div[1]/section[1]/div[1]/div[1]/div[2]/form[1]/div[1]/div[2]/div[1]/label[1]/label[1]/div[1]/div[1]/div[1]/input[1]",
            ["PrimerNombre"] = "/html[1]/body[1]/div[1]/div[1]/div[3]/main[1]/div[3]/div[2]/div[1]/div[1]/div[1]/div[1]/section[1]/div[1]/div[1]/div[2]/form[1]/div[1]/div[4]/div[1]/label[1]/label[1]/div[1]/div[1]/div[1]/input[1]",
            ["SegundoNombre"] = "/html[1]/body[1]/div[1]/div[1]/div[3]/main[1]/div[3]/div[2]/div[1]/div[1]/div[1]/div[1]/section[1]/div[1]/div[1]/div[2]/form[1]/div[1]/div[5]/div[1]/label[1]/label[1]/div[1]/div[1]/div[1]/input[1]",
            ["PrimerApellido"] = "/html[1]/body[1]/div[1]/div[1]/div[3]/main[1]/div[3]/div[2]/div[1]/div[1]/div[1]/div[1]/section[1]/div[1]/div[1]/div[2]/form[1]/div[1]/div[6]/div[1]/label[1]/label[1]/div[1]/div[1]/div[1]/input[1]",
            ["SegundoApellido"] = "/html[1]/body[1]/div[1]/div[1]/div[3]/main[1]/div[3]/div[2]/div[1]/div[1]/div[1]/div[1]/section[1]/div[1]/div[1]/div[2]/form[1]/div[1]/div[7]/div[1]/label[1]/label[1]/div[1]/div[1]/div[1]/input[1]",
            ["Parentesco"] = "/html[1]/body[1]/div[1]/div[1]/div[3]/main[1]/div[3]/div[2]/div[1]/div[1]/div[1]/div[1]/section[1]/div[1]/div[1]/div[2]/form[1]/div[1]/div[9]/div[1]/label[1]/label[1]/div[1]/div[1]/div[1]/div[1]",
            ["Sexo"] = "/html[1]/body[1]/div[1]/div[1]/div[3]/main[1]/div[3]/div[2]/div[1]/div[1]/div[1]/div[1]/section[1]/div[1]/div[1]/div[2]/form[1]/div[1]/div[10]/div[1]/label[1]/label[1]/div[1]/div[1]/div[1]/div[1]",
            ["EstadoCivil"] = "/html[1]/body[1]/div[1]/div[1]/div[3]/main[1]/div[3]/div[2]/div[1]/div[1]/div[1]/div[1]/section[1]/div[1]/div[1]/div[2]/form[1]/div[1]/div[11]/div[1]/label[1]/label[1]/div[1]/div[1]/div[1]",
            ["EstadoBeneficiario"] = "/html[1]/body[1]/div[1]/div[1]/div[3]/main[1]/div[3]/div[2]/div[1]/div[1]/div[1]/div[1]/section[1]/div[1]/div[1]/div[2]/form[1]/div[1]/div[12]/div[1]/label[1]/label[1]/div[1]/div[1]/div[1]/div[1]",
            ["EPS"] = "/html[1]/body[1]/div[1]/div[1]/div[3]/main[1]/div[3]/div[2]/div[1]/div[1]/div[1]/div[1]/section[1]/div[1]/div[1]/div[2]/form[1]/div[1]/div[13]/div[1]/label[1]/label[1]/div[1]/div[1]/div[1]",
            ["ResidenteExterior"] = "/html[1]/body[1]/div[1]/div[1]/div[3]/main[1]/div[3]/div[2]/div[1]/div[1]/div[1]/div[1]/section[1]/div[1]/div[1]/div[2]/form[1]/div[1]/div[14]/div[1]/label[1]/label[1]/div[1]/div[1]/div[1]/div[1]",
            ["FechaExpedicionDocumento"] = "/html[1]/body[1]/div[1]/div[1]/div[3]/main[1]/div[3]/div[2]/div[1]/div[1]/div[1]/div[1]/section[1]/div[1]/div[1]/div[2]/form[1]/div[1]/div[3]/div[1]/div[1]/label[1]/label[1]/div[1]/div[1]/div[2]/input[1]",
            ["FechaNacimiento"] = "/html[1]/body[1]/div[1]/div[1]/div[3]/main[1]/div[3]/div[2]/div[1]/div[1]/div[1]/div[1]/section[1]/div[1]/div[1]/div[2]/form[1]/div[1]/div[8]/div[1]/div[1]/label[1]/label[1]/div[1]/div[1]/div[2]/input[1]",
            ["AFP"] = "/html[1]/body[1]/div[1]/div[1]/div[3]/main[1]/div[3]/div[2]/div[1]/div[1]/div[1]/div[1]/section[1]/div[2]/div[1]/div[2]/form[1]/div[1]/div[1]/div[1]/label[1]/label[1]/div[1]/div[1]/div[1]/div[1]",
            ["Temporalidad"] = "/html[1]/body[1]/div[1]/div[1]/div[3]/main[1]/div[3]/div[2]/div[1]/div[1]/div[1]/div[1]/section[1]/div[2]/div[1]/div[2]/form[1]/div[1]/div[2]/div[1]/label[1]/label[1]/div[1]/div[1]/div[1]/div[1]",
            ["DerechoAPension"] = "/html[1]/body[1]/div[1]/div[1]/div[3]/main[1]/div[3]/div[2]/div[1]/div[1]/div[1]/div[1]/section[1]/div[2]/div[1]/div[2]/form[1]/div[1]/div[3]/div[1]/label[1]/label[1]/div[1]/div[1]/div[1]/div[1]",
            ["PorcentajePension"] = "/html[1]/body[1]/div[1]/div[1]/div[3]/main[1]/div[3]/div[2]/div[1]/div[1]/div[1]/div[1]/section[1]/div[2]/div[1]/div[2]/form[1]/div[1]/div[4]/div[1]/label[1]/label[1]/div[1]/div[1]/div[1]/input[1]",

            // Campos bancarios
            ["EntidadBancaria"] = "/html[1]/body[1]/div[1]/div[1]/div[3]/main[1]/div[3]/div[2]/div[1]/div[1]/div[1]/div[1]/section[1]/div[3]/div[1]/div[2]/form[1]/div[1]/div[9]/div[1]/label[1]/label[1]/div[1]/div[1]/div[1]/div[1]",
            ["TipoCuenta"] = "/html[1]/body[1]/div[1]/div[1]/div[3]/main[1]/div[3]/div[2]/div[1]/div[1]/div[1]/div[1]/section[1]/div[3]/div[1]/div[2]/form[1]/div[1]/div[10]/div[1]/label[1]/label[1]/div[1]/div[1]/div[1]/div[1]",
            ["NumeroCuentaBancaria"] = "/html[1]/body[1]/div[1]/div[1]/div[3]/main[1]/div[3]/div[2]/div[1]/div[1]/div[1]/div[1]/section[1]/div[3]/div[1]/div[2]/form[1]/div[1]/div[11]/label[1]/label[1]/div[1]/div[1]/div[1]/input[1]",
            ["FormaPago"] = "/html[1]/body[1]/div[1]/div[1]/div[3]/main[1]/div[3]/div[2]/div[1]/div[1]/div[1]/div[1]/section[1]/div[3]/div[1]/div[2]/form[1]/div[1]/div[8]/div[1]/label[1]/label[1]/div[1]/div[1]/div[1]/div[1]",
            ["TipoTerceroPago"] = "/html[1]/body[1]/div[1]/div[1]/div[3]/main[1]/div[3]/div[2]/div[1]/div[1]/div[1]/div[1]/section[1]/div[3]/div[1]/div[2]/form[1]/div[1]/div[1]/div[1]/label[1]/label[1]/div[1]/div[1]/div[1]/div[1]",
            ["TipoIdentificacionTerceroPago"] = "/html[1]/body[1]/div[1]/div[1]/div[3]/main[1]/div[3]/div[2]/div[1]/div[1]/div[1]/div[1]/section[1]/div[3]/div[1]/div[2]/form[1]/div[1]/div[2]/div[1]/label[1]/label[1]/div[1]/div[1]/div[1]/div[1]",
            ["IdentificacionTerceroPago"] = "/html[1]/body[1]/div[1]/div[1]/div[3]/main[1]/div[3]/div[2]/div[1]/div[1]/div[1]/div[1]/section[1]/div[3]/div[1]/div[2]/form[1]/div[1]/div[3]/div[1]/label[1]/label[1]/div[1]/div[1]/div[1]/input[1]",
            ["PrimerNombreBanco"] = "/html[1]/body[1]/div[1]/div[1]/div[3]/main[1]/div[3]/div[2]/div[1]/div[1]/div[1]/div[1]/section[1]/div[3]/div[1]/div[2]/form[1]/div[1]/div[4]/div[1]/label[1]/label[1]/div[1]/div[1]/div[1]/input[1]",
            ["SegundoNombreBanco"] = "/html[1]/body[1]/div[1]/div[1]/div[3]/main[1]/div[3]/div[2]/div[1]/div[1]/div[1]/div[1]/section[1]/div[3]/div[1]/div[2]/form[1]/div[1]/div[5]/div[1]/label[1]/label[1]/div[1]/div[1]/div[1]/input[1]",
            ["PrimerApellidoBanco"] = "/html[1]/body[1]/div[1]/div[1]/div[3]/main[1]/div[3]/div[2]/div[1]/div[1]/div[1]/div[1]/section[1]/div[3]/div[1]/div[2]/form[1]/div[1]/div[6]/div[1]/label[1]/label[1]/div[1]/div[1]/div[1]/input[1]",
            ["SegundoApellidoBanco"] = "/html[1]/body[1]/div[1]/div[1]/div[3]/main[1]/div[3]/div[2]/div[1]/div[1]/div[1]/div[1]/section[1]/div[3]/div[1]/div[2]/form[1]/div[1]/div[7]/div[1]/label[1]/label[1]/div[1]/div[1]/div[1]/input[1]",



            // Campos de contacto
            ["DepartamentoNacimiento"] = "/html[1]/body[1]/div[1]/div[1]/div[3]/main[1]/div[3]/div[2]/div[1]/div[1]/div[1]/div[1]/section[1]/div[4]/div[1]/div[2]/form[1]/div[1]/div[1]/div[1]/label[1]/label[1]/div[1]/div[1]/div[1]/div[1]",
            ["CiudadNacimiento"] = "/html[1]/body[1]/div[1]/div[1]/div[3]/main[1]/div[3]/div[2]/div[1]/div[1]/div[1]/div[1]/section[1]/div[4]/div[1]/div[2]/form[1]/div[1]/div[2]/div[1]/label[1]/label[1]/div[1]/div[1]/div[1]/div[1]",
            ["PaisResidencia"] = "/html[1]/body[1]/div[1]/div[1]/div[3]/main[1]/div[3]/div[2]/div[1]/div[1]/div[1]/div[1]/section[1]/div[4]/div[1]/div[2]/form[1]/div[1]/div[4]/div[1]/label[1]/label[1]/div[1]/div[1]/div[1]/div[1]",
            ["DepartamentoResidencia"] = "/html[1]/body[1]/div[1]/div[1]/div[3]/main[1]/div[3]/div[2]/div[1]/div[1]/div[1]/div[1]/section[1]/div[4]/div[1]/div[2]/form[1]/div[1]/div[5]/div[1]/label[1]/label[1]/div[1]/div[1]/div[1]/div[1]",
            ["CiudadResidencia"] = "/html[1]/body[1]/div[1]/div[1]/div[3]/main[1]/div[3]/div[2]/div[1]/div[1]/div[1]/div[1]/section[1]/div[4]/div[1]/div[2]/form[1]/div[1]/div[6]/div[1]/label[1]/label[1]/div[1]/div[1]/div[1]/div[1]",
            ["DireccionYBarrio"] = "/html[1]/body[1]/div[1]/div[1]/div[3]/main[1]/div[3]/div[2]/div[1]/div[1]/div[1]/div[1]/section[1]/div[4]/div[1]/div[2]/form[1]/div[1]/div[3]/div[1]/label[1]/label[1]/div[1]/div[1]/div[1]/input[1]",
            ["TelefonoFijo"] = "/html[1]/body[1]/div[1]/div[1]/div[3]/main[1]/div[3]/div[2]/div[1]/div[1]/div[1]/div[1]/section[1]/div[4]/div[1]/div[2]/form[1]/div[1]/div[7]/div[1]/label[1]/label[1]/div[1]/div[1]/div[1]/input[1]",
            ["Celular"] = "/html[1]/body[1]/div[1]/div[1]/div[3]/main[1]/div[3]/div[2]/div[1]/div[1]/div[1]/div[1]/section[1]/div[4]/div[1]/div[2]/form[1]/div[1]/div[8]/div[1]/label[1]/label[1]/div[1]/div[1]/div[1]/input[1]",
            ["CorreoElectronico"] = "/html[1]/body[1]/div[1]/div[1]/div[3]/main[1]/div[3]/div[2]/div[1]/div[1]/div[1]/div[1]/section[1]/div[4]/div[1]/div[2]/form[1]/div[1]/div[9]/div[1]/label[1]/label[1]/div[1]/div[1]/div[1]/input[1]"

           

        };
        // Agrega aquí otros XPaths relevantes para el flujo de beneficiarios
    }
}
