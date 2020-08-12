using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ChalanWeb.Models
{
    public class CreditCard
    {
        public string NumeroTarjeta { get; set; }
        public string Dueño { get; set; }
        public string Expira_Anio { get; set; }
        public string Expira_Mes { get; set; }
        public string CVV { get; set; }
    }
}