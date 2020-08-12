using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ChalanWeb.Models
{
    public class Direccion
    {
        public Int32 Id { get; set; }
        public String Calle { get; set; }
        public String Numero_Ext { get; set; }
        public String Numero_Int { get; set; }
        public String Colonia { get; set; }
        public String CP { get; set; }
        public String Municipio { get; set; }
        public String Estado { get; set; }
        public String Longitud { get; set; }
        public String Latitud { get; set; }
        public Int32 Activo { get; set; }
        public Int32 Id_Cliente { get; set; }
        

    }
}