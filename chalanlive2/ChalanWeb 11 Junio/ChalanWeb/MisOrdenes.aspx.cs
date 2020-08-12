using ChalanWeb.Controllers;
using ChalanWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ChalanWeb
{
    public partial class MisOrdenes : System.Web.UI.Page
    {
        private static List<Pedido> pedidos = new List<Pedido>();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                if (Session["cliente"] != null)
                {
                    PintarOrdenes();
                    ViewState["RefUrl"] = Request.UrlReferrer.ToString();
                }

                else
                    Response.Redirect("Default.aspx#nosession");
            }
        }

        private void PintarOrdenes()
        {
            DAO.DAOPedido ped = new DAO.DAOPedido();
            Cliente cliente = (Cliente)Session["cliente"];
            pedidos = ped.GetPedidosCliente(cliente.Id);

            for(int i=0; i<pedidos.Count; i++)
            {
                switch (pedidos[i].Status)
                {
                    case 0:
                        pedidos[i].OracionStatus = "Pendiente.";
                        pedidos[i].ColorStatus = "Yellow";
                        break;
                    case 1:
                        pedidos[i].OracionStatus = "Aceptado.";
                        pedidos[i].ColorStatus = "Black";
                        break;
                    case 2:
                        pedidos[i].OracionStatus = "Entregado.";
                        pedidos[i].ColorStatus = "Green";
                        break;
                    case 3:
                        pedidos[i].OracionStatus = "Cancelado.";
                        pedidos[i].ColorStatus = "Red";
                        break;
                }
            }

            rptMarkers.DataSource = pedidos;
            rptMarkers.DataBind();

            repetidorOrdenes.DataSource = pedidos;
            repetidorOrdenes.DataBind();
        }

        protected void repetidorOrdenes_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            int id = Convert.ToInt32(e.CommandArgument.ToString());
            string hash = Seguridad.Encriptar(id.ToString());
            string ir = "DetalleOrden.aspx?id=";
            Response.Redirect(string.Concat(ir, hash));
            //for (int i=0; i<pedidos.Count; i++)
            //{
            //    if(pedidos[i].Id == id)
            //    {
            //        Pedido ped = pedidos[i];
            //        Session["DetallePedido"] = ped;
            //        Response.Redirect("DetalleOrden.aspx");
            //    }
            //}
        }

        protected void ButtonRegresar_Click(object sender, EventArgs e)
        {
            if (ViewState["RefUrl"] != null)
            {
                var url = ViewState["RefUrl"];
                Response.Redirect((string)url);
            }
        }
    }
}