using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ChalanWeb.Models;
using ChalanWeb.DAO;
using Newtonsoft.Json;
using System.Threading.Tasks;
using ChalanWeb.Controllers;
using System.Text;

namespace ChalanWeb
{
    public partial class Envio : System.Web.UI.Page
    {
        protected int Id_Direccion = 0;
        protected Decimal total { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                try
                {
                    PintarDirecciones();
                    PintarCarro();
                    PintarTarjetasAsync();
                }
                catch (Exception ex)
                {
                    Response.Redirect("Default#notsession");
                }

            }
        }

        private void PintarDirecciones()
        {
            DAODireccion dire = new DAODireccion();

            Cliente cliente = (Cliente)Session["cliente"];

            if (cliente != null)
            {
                List<Direccion> direcciones = dire.GetDireccionesCliente(cliente.Id);

                repetidorDirecciones.DataSource = direcciones;
                repetidorDirecciones.DataBind();
            }

            else
            {
                Response.Redirect("Default.aspx#notsession");
            }
        }

        private async void PintarTarjetasAsync()
        {
            Cliente cliente = (Cliente)Session["cliente"];

            if (cliente != null)
            {
                Controllers.ConexionApi conexion = new Controllers.ConexionApi();

                List<TarjetaOP> tarjetas = await conexion.ObtenerTarjetasOP(cliente.TOKENOP);

                if (tarjetas != null && tarjetas.Any())
                {
                    repetidorTarjetas.DataSource = tarjetas;
                    repetidorTarjetas.DataBind();
                }
            }

            else
            {
                Response.Redirect("Default.aspx#notsession");
            }
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


        private void PintarCarro()
        {
            List<DetallePedido> carrito = new List<DetallePedido>();

            try
            {
                carrito = (List<DetallePedido>)HttpContext.Current.Session["carro"];
            }
            catch { }

            repetidorCarro.DataSource = carrito;
            repetidorCarro.DataBind();

            total = carrito.Select(x => x.Costo).Sum();
            int cantidad = carrito.Count;
            decimal envio = Convert.ToDecimal(29);
            Decimal totalPedido = total + envio;


            literalCantidad.Text = cantidad.ToString();
            literalTotalCompra.Text = string.Concat("Total de tu compra: $", totalPedido.ToString("#,##0.00"));
        }

        private bool ChecarPedidos(Cliente cliente)
        {
            bool bandera = true;

            if (cliente.Pedidos == null)
                return bandera;

            if (cliente.Pedidos.Count > 0)
            {
                for (int i = 0; i < cliente.Pedidos.Count; i++)
                {
                    if (cliente.Pedidos[i].Status == 0 || cliente.Pedidos[i].Status == 1)
                        bandera = false;
                }
            }
            return bandera;
        }

        protected async void ButtonFinalizar_Click(object sender, EventArgs e)
        {
            Cliente cliente = (Cliente)Session["cliente"];

            int idDireccion = 0;
            string metodoPago = "";
            string card = "";
            string idSession = "";
            string notas = "";

            bool bandera = ChecarPedidos(cliente);

            if (bandera)
            {
                if (Request.Form["payment-method"] != null)
                {
                    metodoPago = Request.Form["payment-method"].ToString();
                }

                //foreach (RepeaterItem rptItem in repetidorDirecciones.Items)
                //{
                //    TextBox txtQty = (TextBox)rptItem.FindControl("idDireccion");
                //    if (txtQty != null)
                //    { string ass = txtQty.Text;
                //        string pedo = ass;
                //    }
                //}

                //if (Request.Form["idDireccion"] != null)
                //{
                //    string selectedGender = Request.Form["idDireccion"].ToString();
                //}

                if (Request.Form["DireccionAEnviar"] != null)
                {
                    idDireccion = Convert.ToInt32(Request.Form["DireccionAEnviar"].ToString());
                }

                if (Request.Form["TarjetaSeleccionada"] != null)
                {
                    card = Request.Form["TarjetaSeleccionada"].ToString();
                }

                if (Request.Form["sessionId"] != null)
                {
                    idSession = Request.Form["sessionId"].ToString();
                }

                if(Request.Form["Notas"] != null)
                {
                    notas = Request.Form["Notas"].ToString();
                }

                DAOPedido new_Pedido = new DAOPedido();

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
                pedido.FechaAgendado = DateTime.Now;
                pedido.Id_Direccion = dir.Id;
                pedido.Calle = dir.Calle;
                pedido.Numero_Ext = dir.Numero_Ext;
                pedido.Numero_Int = dir.Numero_Int;
                pedido.Colonia = dir.Colonia;
                pedido.Municipio = dir.Municipio;
                pedido.Estado = dir.Estado;
                pedido.CP = dir.CP;
                pedido.Nota_Cliente = notas;
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

                //string tarjetaSeleccionada = "";
                //List<TarjetaOP> tarjetas = await ObtenerTarjetas();

                //if(tarjetas!=null && tarjetas.Any())
                //{
                //    for (int i = 0; i < tarjetas.Count; i++)
                //    {
                //        if (tarjetas[i].id.Equals(card))
                //        {
                //            tarjetaSeleccionada = JsonConvert.SerializeObject(tarjetas[i]);
                //            break;
                //        }
                //    }
                //}

                ConexionApi conexion = new ConexionApi();
                //Mensaje respuesta = await conexion.HacerPedido(pedidoJson, idSession, card);
                List<string> jsons = await conexion.HacerPedido(pedidoJson, idSession, card);

                Mensaje respuesta = JsonConvert.DeserializeObject<Mensaje>(jsons[0]);

                if (respuesta.Titulo.Equals("OK")) //Se agregó y se generó el cobro correctamente
                {
                    //Llenar objeto pedido
                    Pedido ped = new Pedido();
                    ped.Id = 0;
                    ped.Id_Cliente = cliente.Id;
                    ped.Nombre_Cliente = cliente.Nombre;
                    ped.Telefono_Cliente = cliente.Telefono;
                    ped.Id_Listado = cliente.Id_Lista;
                    ped.Nombre_Listado = cliente.Nombre_Listado;
                    ped.Detalles = new List<DetallePedido>();
                    Session["carro"] = ped.Detalles;
                    Cliente cli = JsonConvert.DeserializeObject<Cliente>(jsons[1]);
                    Pedido ultimo = cli.Pedidos.OrderByDescending(x => x.Id).FirstOrDefault();
                    cli.DetallesUltimoPedido = ultimo.Detalles;

                    Session["cliente"] = cli;

                    Response.Redirect("DetallePedidoNuevo.aspx#Compra");
                }

                else
                {
                    switch (respuesta.Contenido)
                    {
                        case "Pedido no creado. Sin productos":
                            respuesta.Contenido = "1Pedido no creado. Sin productos";
                            break;
                        case "Pedido no creado.":
                            respuesta.Contenido = "2Pedido no creado.";
                            break;
                    }
                    string advertencia = Seguridad.Encriptar(respuesta.Contenido).Substring(0, 10);
                    Response.Redirect(string.Concat("Envio.aspx#", advertencia));
                }
            }

            else
            {
                Response.Redirect("Default.aspx#OpenOrder", false);
            }



            //if (bandera)
            //{
            //    if (Request.Form["payment-method"] != null)
            //    {
            //        metodoPago = Request.Form["payment-method"].ToString();
            //    }

            //    //foreach (RepeaterItem rptItem in repetidorDirecciones.Items)
            //    //{
            //    //    TextBox txtQty = (TextBox)rptItem.FindControl("idDireccion");
            //    //    if (txtQty != null)
            //    //    { string ass = txtQty.Text;
            //    //        string pedo = ass;
            //    //    }
            //    //}

            //    //if (Request.Form["idDireccion"] != null)
            //    //{
            //    //    string selectedGender = Request.Form["idDireccion"].ToString();
            //    //}

            //    if (Request.Form["DireccionAEnviar"] != null)
            //    {
            //        idDireccion = Convert.ToInt32(Request.Form["DireccionAEnviar"].ToString());
            //    }

            //    if (Request.Form["TarjetaSeleccionada"] != null)
            //    {
            //        card = Request.Form["TarjetaSeleccionada"].ToString();
            //    }

            //    if (Request.Form["sessionId"] != null)
            //    {
            //        idSession = Request.Form["sessionId"].ToString();
            //    }

            //    DAOPedido new_Pedido = new DAOPedido();

            //    Direccion dir = null;
            //    for (int i = 0; i < cliente.Direcciones.Count; i++)
            //    {
            //        if (cliente.Direcciones[i].Id == idDireccion)
            //        {
            //            dir = cliente.Direcciones[i];
            //            break;
            //        }
            //    }

            //    Pedido pedido = new Pedido();
            //    pedido.Fecha_Pedido = DateTime.Now;
            //    pedido.FechaAgendado = DateTime.Now;
            //    pedido.Id_Direccion = dir.Id;
            //    pedido.Calle = dir.Calle;
            //    pedido.Numero_Ext = dir.Numero_Ext;
            //    pedido.Numero_Int = dir.Numero_Int;
            //    pedido.Colonia = dir.Colonia;
            //    pedido.Municipio = dir.Municipio;
            //    pedido.Estado = dir.Estado;
            //    pedido.CP = dir.CP;
            //    pedido.Nota_Cliente = "";
            //    pedido.Id_Cliente = cliente.Id;
            //    pedido.Nombre_Cliente = cliente.Nombre;
            //    pedido.Telefono_Cliente = cliente.Telefono;
            //    pedido.Id_Listado = 1;
            //    pedido.Nombre_Listado = "CLIENTE";
            //    pedido.Costo_Envio = 29;
            //    pedido.Metodo_Pago = metodoPago.ToUpper().Substring(0, 1);
            //    pedido.Latitud = dir.Latitud;
            //    pedido.Longitud = dir.Longitud;
            //    pedido.Dispositivo = "W";


            //    List<DetallePedido> carrito = (List<DetallePedido>)HttpContext.Current.Session["carro"];
            //    Decimal total = carrito.Select(x => x.Costo).Sum();
            //    pedido.Costo_Total = total; //cantidad*precio //total

            //    pedido.Detalles = carrito;

            //    string pedidoJson = JsonConvert.SerializeObject(pedido);

            //    //string tarjetaSeleccionada = "";
            //    //List<TarjetaOP> tarjetas = await ObtenerTarjetas();

            //    //if(tarjetas!=null && tarjetas.Any())
            //    //{
            //    //    for (int i = 0; i < tarjetas.Count; i++)
            //    //    {
            //    //        if (tarjetas[i].id.Equals(card))
            //    //        {
            //    //            tarjetaSeleccionada = JsonConvert.SerializeObject(tarjetas[i]);
            //    //            break;
            //    //        }
            //    //    }
            //    //}

            //    ConexionApi conexion = new ConexionApi();
            //    //Mensaje respuesta = await conexion.HacerPedido(pedidoJson, idSession, card);
            //    List<string> jsons = await conexion.HacerPedido(pedidoJson, idSession, card);

            //    Mensaje respuesta = JsonConvert.DeserializeObject<Mensaje>(jsons[0]);

            //    if (respuesta.Titulo.Equals("OK")) //Se agregó y se generó el cobro correctamente
            //    {
            //        //Llenar objeto pedido
            //        Pedido ped = new Pedido();
            //        ped.Id = 0;
            //        ped.Id_Cliente = cliente.Id;
            //        ped.Nombre_Cliente = cliente.Nombre;
            //        ped.Telefono_Cliente = cliente.Telefono;
            //        ped.Id_Listado = cliente.Id_Lista;
            //        ped.Nombre_Listado = cliente.Nombre_Listado;
            //        ped.Detalles = new List<DetallePedido>();
            //        Session["carro"] = ped.Detalles;
            //        Cliente cli = JsonConvert.DeserializeObject<Cliente>(jsons[1]);
            //        Pedido ultimo = cli.Pedidos.OrderByDescending(x => x.Id).FirstOrDefault();
            //        cli.DetallesUltimoPedido = ultimo.Detalles;

            //        Session["cliente"] = cli;

            //        Response.Redirect("DetallePedidoNuevo.aspx#Compra");
            //    }

            //    else
            //    {
            //        Response.Redirect("Envio.aspx#PurchaseError");
            //    }
            //}

            //else
            //{
            //    Response.Redirect("Default.aspx#OpenOrder");
            //}
        }
    }
}