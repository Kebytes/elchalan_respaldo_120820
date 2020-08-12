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
    public partial class Categoria : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string query = null;
            string termino = null;

            if (Request.QueryString.AllKeys.Contains("cat"))
                query = Request.QueryString["cat"].ToUpper();

            if (Request.QueryString.AllKeys.Contains("term"))
                termino = Request.QueryString["term"].ToUpper();

            if (!Page.IsPostBack)
            {
                if (query != null)
                {
                    DAO.DAOProducto dAOProducto = new DAO.DAOProducto();

                    List<Producto> prods = dAOProducto.GetProductos();

                    litNombreCategoria.Text = query;
                    List<Producto> favoritos = prods.Where(x => x.Categoria == query).ToList();

                    repetidorCategoria.DataSource = favoritos;
                    repetidorCategoria.DataBind();
                }

                if (termino != null)
                {
                    DAO.DAOProducto dAOProducto = new DAO.DAOProducto();

                    List<Producto> prods = dAOProducto.GetProductosWhere(termino);

                    if (prods != null)
                    {
                        litNombreCategoria.Text = termino;

                        repetidorCategoria.DataSource = prods;
                        repetidorCategoria.DataBind();
                    }

                    else
                    {
                        litNombreCategoria.Text = "SIN RESULTADOS PARA: " + termino;
                    }
                }

                //OpcionesOrdenacion.Items.Insert(0, new ListItem("Ordenar", ""));
                //OpcionesOrdenacion.Items[0].Selected = true;
                //OpcionesOrdenacion.Items[0].Attributes["disabled"] = "disabled";
            }
        }

        protected void repetidorCategoria_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            string id = e.CommandArgument.ToString();

            if (Session["carro"] == null)
            {
                List<DetallePedido> carrito = new List<DetallePedido>();
                DAOProducto prod = new DAOProducto();

                Producto producto = prod.GetProducto(id);
                DetallePedido detalle = new DetallePedido();

                detalle.Cantidad = 1;
                detalle.Precio = producto.Precio;
                detalle.Nombre = producto.Nombre;
                detalle.PROMO = producto.PROMO;
                detalle.Id_Producto = producto.Id;
                detalle.Imagen = producto.Imagen;
                detalle.Costo = producto.Precio;


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

                    ped.Id_Producto = prod.Id;
                    ped.Imagen = prod.Imagen;
                    ped.Costo = prod.Precio;

                    carrito.Add(ped);
                    Session["carro"] = carrito;
                }
            }
            Response.Redirect(Request.RawUrl);
        }

        protected void OpcionesOrdenacion_SelectedIndexChanged(object sender, EventArgs e)
        {
            string query = null;
            string termino = null;

            if (OpcionesOrdenacion.SelectedItem.Value.Equals("asc"))
            {
                if (Request.QueryString.AllKeys.Contains("cat"))
                    query = Request.QueryString["cat"].ToUpper();

                if (Request.QueryString.AllKeys.Contains("term"))
                    termino = Request.QueryString["term"].ToUpper();


                if (query != null)
                {
                    DAO.DAOProducto dAOProducto = new DAO.DAOProducto();

                    List<Producto> prods = dAOProducto.GetProductos();

                    litNombreCategoria.Text = query;
                    List<Producto> favoritos = prods.Where(x => x.Categoria == query).ToList().OrderBy(x => x.Precio).ToList();

                    repetidorCategoria.DataSource = favoritos;
                    repetidorCategoria.DataBind();
                }

                if (termino != null)
                {
                    DAO.DAOProducto dAOProducto = new DAO.DAOProducto();

                    List<Producto> prods = dAOProducto.GetProductosWhere(termino).OrderBy(x => x.Precio).ToList();

                    if (prods != null)
                    {
                        litNombreCategoria.Text = termino;

                        repetidorCategoria.DataSource = prods;
                        repetidorCategoria.DataBind();
                    }
                }
            }

            else if (OpcionesOrdenacion.SelectedItem.Value.Equals("desc"))
            {
                if (Request.QueryString.AllKeys.Contains("cat"))
                    query = Request.QueryString["cat"].ToUpper();

                if (Request.QueryString.AllKeys.Contains("term"))
                    termino = Request.QueryString["term"].ToUpper();


                if (query != null)
                {
                    DAO.DAOProducto dAOProducto = new DAO.DAOProducto();

                    List<Producto> prods = dAOProducto.GetProductos();

                    litNombreCategoria.Text = query;
                    List<Producto> favoritos = prods.Where(x => x.Categoria == query).ToList().OrderByDescending(x => x.Precio).ToList();

                    repetidorCategoria.DataSource = favoritos;
                    repetidorCategoria.DataBind();
                }

                if (termino != null)
                {
                    DAO.DAOProducto dAOProducto = new DAO.DAOProducto();

                    List<Producto> prods = dAOProducto.GetProductosWhere(termino).OrderByDescending(x => x.Precio).ToList();

                    if (prods != null)
                    {
                        litNombreCategoria.Text = termino;

                        repetidorCategoria.DataSource = prods;
                        repetidorCategoria.DataBind();
                    }
                }
            }
        }
    }
}