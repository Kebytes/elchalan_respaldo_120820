using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ChalanWeb.Models
{
    public class Cliente
    {
        public Int32 Id { get; set; }
        public String Nombre { get; set; }
        public String Apellido_P { get; set; }
        public String Apellido_M { get; set; }
        public DateTime Fecha_Nacimiento { get; set; }
        public String Sexo { get; set; }
        public String Telefono { get; set; }
        public String Correo { get; set; }
        public String Pass { get; set; }
        public Int32 Id_Lista { get; set; }
        public String Nombre_Listado { get; set; }
        public Int32 Edad { get; set; }
        public List<Direccion> Direcciones { get; set; }
        public List<Pedido> Pedidos { get; set; }
        public String Token_FB { get; set; }
        public String TokenUser { get; set; }
        public String TarjetaChalan { get; set; }
        public String TOKENOP { get; set; }
        public String SoloNombre { get; set; }
        public List<DetallePedido> DetallesUltimoPedido { get; set; }
    }
}