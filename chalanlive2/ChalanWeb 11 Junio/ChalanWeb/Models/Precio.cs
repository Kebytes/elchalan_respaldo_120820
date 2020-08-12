using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ChalanWeb.Models
{
    public class Precio
    {
        public Int32 Id { get; set; }
        public Decimal Costo { get; set; }
        public Int32 Id_Producto { get; set; }
        public Int32 Id_Listado { get; set; }
    }
}