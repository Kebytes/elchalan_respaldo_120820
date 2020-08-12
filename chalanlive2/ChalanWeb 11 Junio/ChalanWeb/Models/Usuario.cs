using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ChalanWeb.Models
{
    public class Usuario
    {
        public Int32 Id { get; set; }
        public String Nombre { get; set; }
        public String Email { get; set; }
        public String Password { get; set; }
        public Int32 Id_Sucursal { get; set; }
}
}