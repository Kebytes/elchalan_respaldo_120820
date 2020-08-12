using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ChalanWeb.DAO;
using ChalanWeb.Models;


using System.Web.Services;

namespace ChalanWeb
{
    public partial class Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                DAO.DAOProducto dAOProducto = new DAO.DAOProducto();

                List<Producto> prods = dAOProducto.GetProductos();
                

                List<Producto> favoritos = prods.Where(x => x.Categoria == "TOP").ToList();

                RepetidorFavoritos.DataSource = favoritos;
                RepetidorFavoritos.DataBind();
            }
        }

        [System.Web.Services.WebMethod()]
        [System.Web.Script.Services.ScriptMethod()]
        public static void AddCart(int i)
        {
            string Name = i.ToString();
            //return Name;
            try
            {

                if (HttpContext.Current.Session["carro"] == null)
                {

                    //DetallePedido ped = new DetallePedido();
                    //DAOProducto pro = new DAOProducto();
                    //Producto prod = pro.GetProducto(i);
                    //ped.Cantidad = 1;
                    //ped.Precio = prod.Precio;
                    //ped.Nombre = prod.Nombre;
                    //ped.PROMO = prod.PROMO;

                    //ped.Id_Producto = prod.Id;
                    //ped.Imagen = prod.Imagen;

                    //ped.Costo = ped.Precio * ped.Cantidad;

                    //List<DetallePedido> carrito = new List<DetallePedido>();






                    //List<DetallePedido> carrito = (List<DetallePedido>)HttpContext.Current.Session["carro"];

                    //Producto prod = pro.GetProducto(i);
                    //DetallePedido ped = new DetallePedido();
                    //ped.Cantidad = 1;
                    //ped.Precio = prod.Precio;
                    //ped.Nombre = prod.Nombre;
                    //ped.PROMO = prod.PROMO;

                    //ped.Id_Producto = prod.Id;
                    //ped.Imagen = prod.Imagen;

                    //ped.Costo = ped.Precio * ped.Cantidad;


                    //carrito.Add(ped);

                    //HttpContext.Current.Session["carro"] = carrito;
                }
                else
                {
                    List<DetallePedido> carrito = (List<DetallePedido>)HttpContext.Current.Session["carro"];




                    //carrito = carrito.First(x => (x.Cantidad = x.Cantidad +1) == 100).ToList();



                    foreach (var detallep in carrito.Where(w => w.Id == i))
                    {
                        detallep.Cantidad = detallep.Cantidad + 1;
                    }


                }

            }
            catch { }
            // return true;

        }

        protected void Button1_Click(object sender, EventArgs e)
        {

        }

        protected void RepetidorFavoritos_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            string id = e.CommandArgument.ToString();

            //Si el carro está vacio
            if (Session["carro"] == null)
            {
                List<DetallePedido> carrito = new List<DetallePedido>();


                DAOProducto pro = new DAOProducto();
                Producto prod = pro.GetProducto(id);
                DetallePedido ped = new DetallePedido();

                ped.Cantidad = 1;
                ped.Precio = prod.Precio;
                ped.Nombre = prod.Nombre;
                ped.PROMO = prod.PROMO;
                ped.Costo = prod.Precio;

                ped.Id_Producto = prod.Id;
                ped.Imagen = prod.Imagen;


                carrito.Add(ped);
                Session["carro"] = carrito;
                //List<DetallePedido> carrito = (List<DetallePedido>)HttpContext.Current.Session["carro"];

                //Producto prod = pro.GetProducto(i);
                //DetallePedido ped = new DetallePedido();
                //ped.Cantidad = 1;
                //ped.Precio = prod.Precio;
                //ped.Nombre = prod.Nombre;
                //ped.PROMO = prod.PROMO;

                //ped.Id_Producto = prod.Id;
                //ped.Imagen = prod.Imagen;

                //ped.Costo = ped.Precio * ped.Cantidad;


                //carrito.Add(ped);

                //HttpContext.Current.Session["carro"] = carrito;

            }


            else  //si tengo carrito
            {
                List<DetallePedido> carrito = (List<DetallePedido>)HttpContext.Current.Session["carro"];

                if (carrito.Exists(x => x.Id_Producto == Convert.ToInt32(id)))
                {
                    foreach (var detallep in carrito.Where(w => w.Id_Producto == Convert.ToInt32(id)))
                    {
                        detallep.Cantidad = detallep.Cantidad + 1;
                    }
                }
                else
                {

                    DAOProducto pro = new DAOProducto();
                    Producto prod = pro.GetProducto(id);

                    DetallePedido ped = new DetallePedido();

                    ped.Cantidad = 1;
                    ped.Precio = prod.Precio;
                    ped.Nombre = prod.Nombre;
                    ped.PROMO = prod.PROMO;
                    ped.Costo = prod.Precio;

                    ped.Id_Producto = prod.Id;
                    ped.Imagen = prod.Imagen;
                    
                    carrito.Add(ped);
                    Session["carro"] = carrito;

                }
            }
            Response.Redirect(Request.RawUrl);
        }
    }
}