using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ChalanWeb.Models
{
    public class Producto
    {
        public Int32 Id { get; set; }
        public String Nombre { get; set; }
        public String Descripcion { get; set; }
        public String Fotografia { get; set; }
        public String Imagen { get; set; }
        public Int32 Visible { get; set; }
        public Int32 Cantidad { get; set; }
        public Decimal Precio { get; set; }
        public String Categoria { get; set; }
        public Boolean PROMO { get; set; }
        public Decimal Precio_Simbolico { get; set; }
        public string Shown { get; set; }
    }
    public class DetallePedido : Producto
    {
        public Int32 Id_Pedido { get; set; }
        public Int32 Id_Producto { get; set; }
        public Decimal Costo
        {
            get
            {
                Decimal cost = 0;
                try
                {
                    cost = Cantidad * Precio;
                }
                catch
                {

                }

                return cost;
           }

            set { }
        }
    }
}