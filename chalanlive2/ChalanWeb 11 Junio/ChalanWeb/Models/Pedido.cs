using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ChalanWeb.Models
{
    public class Pedido
    {
        public Int32 Id { get; set; }
        public DateTime Fecha_Pedido { get; set; }
        public DateTime Fecha_Aceptado { get; set; }
        public DateTime Fecha_Estimada { get; set; }
        public DateTime Fecha_Entrega { get; set; }
        public Int32 Id_Direccion { get; set; }
        public String Calle { get; set; }
        public String Numero_Ext { get; set; }
        public String Numero_Int { get; set; }
        public String Colonia { get; set; }
        public String Municipio { get; set; }
        public String Estado { get; set; }
        public String CP { get; set; }
        public String Nota_Cliente { get; set; }
        public String Nota_User { get; set; }
        public Int32 Id_Repartidor { get; set; }
        public String Nombre_Repartidor { get; set; }
        public String Telefono_Repartidor { get; set; }
        public Int32 Id_Cliente { get; set; }
        public String Nombre_Cliente { get; set; }
        public String Telefono_Cliente { get; set; }
        public Int32 Id_Listado { get; set; }
        public String Nombre_Listado { get; set; }
        public Int32 Id_User_Acepta { get; set; }
        public Int32 Id_User_Finaliza { get; set; }
        public Decimal Costo_Total { get; set; }
        public Decimal Costo_Envio { get; set; }
        public String Metodo_Pago { get; set; }
        public String Latitud { get; set; }
        public String Longitud { get; set; }
        public Int32 Id_Sucursal { get; set; }
        public Int32 Status { get; set; }
        public DateTime Fecha_Cancelado { get; set; }
        public List<DetallePedido> Detalles { get; set; }
        
        public String Token { get; set; }
        public String HoraEntrega { get; set; }
        public String Dispositivo { get; set; }
        public DateTime FechaAgendado { get; set; }
        public string OracionStatus { get; set; }
        public string ColorStatus { get; set; }
    }
}