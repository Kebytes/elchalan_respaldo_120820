using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ChalanWeb.Models
{
    public class TarjetaOP
    {
        public string id { get; set; }
        public string type { get; set; }
        public string brand { get; set; }
        public string card_number { get; set; }
        public string holder_name { get; set; }
        public string expiration_year { get; set; }
        public string expiration_month { get; set; }
        public bool allows_charges { get; set; }
        public bool allows_payouts { get; set; }
        public DateTime creation_date { get; set; }
        public string bank_name { get; set; }
        public string customer_id { get; set; }
        public string bank_code { get; set; }
        public string imagen { get; set; }

        public string TARJETATEXTO
        {
            get
            {
                return TIPO + " " + ULTIMOS4;
            }
        }

        public string ULTIMOS4 { get { return "**** " + card_number.Substring(card_number.Length - 4); } }

        public string TIPO
        {
            get
            {
                string tipo = "";
                if (type.Equals("debit"))
                {
                    tipo = "Débito";
                }
                else
                {
                    tipo = "Crédito";
                }
                return tipo;
            }
        }
    }
}