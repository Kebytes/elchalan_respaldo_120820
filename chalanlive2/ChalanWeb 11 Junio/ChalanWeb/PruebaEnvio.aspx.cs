using ChalanWeb.DAO;
using ChalanWeb.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ChalanWeb
{
    public partial class PruebaEnvio : System.Web.UI.Page
    {
    
        protected void Page_Load(object sender, EventArgs e)
        {
            
        }

        private async Task<List<TarjetaOP>> ObtenerTarjetas()
        {
            if (Session["cliente"] != null)
            {
                Cliente cliente = (Cliente)Session["cliente"];

                Controllers.ConexionApi conexion = new Controllers.ConexionApi();

                List<TarjetaOP> tarjetas = await conexion.ObtenerTarjetasOP(cliente.TOKENOP);

                return tarjetas;
            }

            return new List<TarjetaOP>();
        }

        protected async void ButtonFinalizar_Click(object sender, EventArgs e)
        {
            int idDireccion = 0;
            string metodoPago = "";
            string card = "";

            if (Request.Form["payment-method"] != null)
            {
                metodoPago = Request.Form["payment-method"].ToString();
            }

            if (Request.Form["DireccionAEnviar"] != null)
            {
                idDireccion = Convert.ToInt32(Request.Form["DireccionAEnviar"].ToString());
            }

            if (Request.Form["TarjetaSeleccionada"] != null)
            {
                card = Request.Form["TarjetaSeleccionada"].ToString();
            }

            string id = Aber.Value;

            DAOPedido new_Pedido = new DAOPedido();
            Cliente cliente = (Cliente)Session["cliente"];
            Direccion dir = null;
            for (int i = 0; i < cliente.Direcciones.Count; i++)
            {
                if (cliente.Direcciones[i].Id == idDireccion)
                {
                    dir = cliente.Direcciones[i];
                    break;
                }
            }

            Pedido pedido = new Pedido();
            pedido.Fecha_Pedido = DateTime.Now;
            pedido.Id_Direccion = dir.Id;
            pedido.Calle = dir.Calle;
            pedido.Numero_Ext = dir.Numero_Ext;
            pedido.Numero_Int = dir.Numero_Int;
            pedido.Colonia = dir.Colonia;
            pedido.Municipio = dir.Municipio;
            pedido.Estado = dir.Estado;
            pedido.CP = dir.CP;
            pedido.Nota_Cliente = "";
            pedido.Id_Cliente = cliente.Id;
            pedido.Nombre_Cliente = cliente.Nombre;
            pedido.Telefono_Cliente = cliente.Telefono;
            pedido.Id_Listado = 1;
            pedido.Nombre_Listado = "CLIENTE";
            pedido.Costo_Envio = 29;
            pedido.Metodo_Pago = metodoPago.ToUpper().Substring(0, 1);
            pedido.Latitud = dir.Latitud;
            pedido.Longitud = dir.Longitud;
            pedido.Dispositivo = "W";


            List<DetallePedido> carrito = (List<DetallePedido>)HttpContext.Current.Session["carro"];
            Decimal total = carrito.Select(x => x.Costo).Sum();
            pedido.Costo_Total = total; //cantidad*precio //total

            pedido.Detalles = carrito;

            string pedidoJson = JsonConvert.SerializeObject(pedido);
            

            string tarjetaSeleccionada = "";
            List<TarjetaOP> tarjetas = await ObtenerTarjetas();
            for (int i = 0; i < tarjetas.Count; i++)
            {
                if (tarjetas[i].id.Equals(card))
                {
                    tarjetaSeleccionada = JsonConvert.SerializeObject(tarjetas[i]);
                    break;
                }
            }

            Pedido insert = new_Pedido.Insert(pedido);

            if (insert != null)
            {
                Session["DetalleNuevo"] = pedido;
                Response.Redirect("DetallePedidoNuevo.aspx#Compra");
            }
            else
            {
                Response.Redirect("Envio.aspx#CompraError");
            }
        }
    }
}