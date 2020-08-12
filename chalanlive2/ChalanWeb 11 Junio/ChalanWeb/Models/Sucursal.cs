using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ChalanWeb.Models
{
    public class Sucursal
    {
        public Int32 Id { get; set; }
        public String Nombre { get; set; }
        public String Latitud { get; set; }
        public String Longitud { get; set; }
    }
}