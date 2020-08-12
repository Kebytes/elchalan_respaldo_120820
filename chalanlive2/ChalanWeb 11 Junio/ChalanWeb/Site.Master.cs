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
    public partial class SiteMaster : MasterPage
    {
        protected string valor { get; set; }
        protected void Page_Load(object sender, EventArgs e)

        {
            if(Session["carro"] != null)
            {
                PintarCarro();
            }
            if (Session["cliente"] != null)
            {
                linkCuenta.Visible = true;
                linkLogOut.Visible = true;
            }
            else
            {
                linkIniciarSesion.Visible = true;
                //Response.Redirect("Default.aspx#notsession");
            }
        }


        private void PintarCarro()
        {
            List<DetallePedido> carrito = (List<DetallePedido>)HttpContext.Current.Session["carro"];

            RepetidorCarrito.DataSource = carrito;
            RepetidorCarrito.DataBind();

            Decimal total = carrito.Select(x => x.Costo).Sum();


            LiteralTotal.Text = total.ToString("#,##0.00");
            LitnumArt.Text = carrito.Count.ToString();
            LiteralCuentaIcono.Text = carrito.Count.ToString();
        }

        protected void PopUp_Click(object sender, EventArgs e)
        {

        }
    }
}