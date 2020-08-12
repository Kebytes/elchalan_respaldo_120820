using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ChalanWeb.DAO;
using ChalanWeb.Models;
namespace ChalanWeb
{
    public partial class Product : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                try
                {
                    string query = null;


                    if (Request.QueryString.AllKeys.Contains("id"))
                        query = Request.QueryString["id"].ToUpper();
                    DAOProducto daoprod = new DAOProducto();
                    Producto prod = daoprod.GetProducto(query);

                    List<Producto> listado = new List<Producto>();
                    listado.Add(prod);

                    repetidorProducto.DataSource = listado;
                    repetidorProducto.DataBind();

                }
                catch
                {

                }
            }

        }


        protected void repetidorProducto_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            int cantidad = Convert.ToInt32(Request.Form["cantidadProducto"].ToString());

            string id = e.CommandArgument.ToString();

            if (Session["carro"] == null)
            {
                List<DetallePedido> carrito = new List<DetallePedido>();
                DAOProducto prod = new DAOProducto();

                Producto producto = prod.GetProducto(id);
                DetallePedido detalle = new DetallePedido();

                detalle.Cantidad = cantidad;
                detalle.Precio = producto.Precio;
                detalle.Nombre = producto.Nombre;
                detalle.PROMO = producto.PROMO;
                detalle.Id_Producto = producto.Id;
                detalle.Imagen = producto.Imagen;


                carrito.Add(detalle);
                Session["carro"] = carrito;
            }

            else
            {
                List<DetallePedido> carrito = (List<DetallePedido>)HttpContext.Current.Session["carro"];
                if (carrito.Exists(x => x.Id_Producto == Convert.ToInt32(id)))
                {
                    foreach (var detallep in carrito.Where(w => w.Id_Producto == Convert.ToInt32(id)))
                    {
                        detallep.Cantidad = detallep.Cantidad + cantidad;
                    }
                }

                else
                {
                    DAOProducto pro = new DAOProducto();
                    Producto prod = pro.GetProducto(id);
                    DetallePedido ped = new DetallePedido();

                    ped.Cantidad = cantidad;
                    ped.Precio = prod.Precio;
                    ped.Nombre = prod.Nombre;
                    ped.PROMO = prod.PROMO;

                    ped.Id_Producto = prod.Id;
                    ped.Imagen = prod.Imagen;

                    carrito.Add(ped);
                    Session["carro"] = carrito;
                }
            }
            Response.Redirect("Default.aspx");
        }
    }
}