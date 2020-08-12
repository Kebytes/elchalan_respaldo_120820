using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ChalanWeb.Controllers
{
    public class ErrorOP
    {
        public string category { get; set; }
        public string description { get; set; }
        public int http_code { get; set; }
        public int error_code { get; set; }
        public string request_id { get; set; }
        public List<string> fraud_rules { get; set; }

        public string DescripcionError
        {
            get
            {
                string error = "Ocurrió un error";
                switch (error_code)
                {
                    case (1000):
                        error = "Ocurrió un error interno";
                        break;
                    case (2004):
                        error = "El dígito de verificación del número de tarjeta no es válido";
                        break;
                    case (2005):
                        error = "La fecha de expiración de la tarjeta es anterior a la fecha actual";
                        break;
                    case (2006):
                        error = "El código de seguridad de la tarjeta (CVV2) no fue proporcionado";
                        break;
                    case (2009):
                        error = "El código de seguridad de la tarjeta (CVV2) no es válido.";
                        break;
                    case (3001):
                        error = "La tarjeta fue declinada";
                        break;
                    case (3002):
                        error = "La tarjeta ha expirado";
                        break;
                    case (3003):
                        error = "La tarjeta no tiene fondos suficientes";
                        break;
                    case (3004):
                        error = "La tarjeta ha sido identificada como una tarjeta robada";
                        break;
                    case (3005):
                        error = "La tarjeta ha sido identificada como una tarjeta fraudulenta";
                        break;
                    case (3008):
                        error = "La tarjeta no es soportada en transacciones en linea";
                        break;
                    case (3009):
                        error = "La tarjeta fue reportada como perdida";
                        break;
                    case (3010):
                        error = "El banco ha restringido la tarjeta";
                        break;
                    case (3011):
                        error = "El banco ha solicitado que la tarjeta sea retenida. Contacte al banco";
                        break;
                    case (3012):
                        error = "Se requiere solicitar al banco autorización para realizar este pago";
                        break;
                }
                return error;
            }
        }
    }
}