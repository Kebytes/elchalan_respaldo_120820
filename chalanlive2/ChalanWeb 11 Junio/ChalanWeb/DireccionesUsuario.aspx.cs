using ChalanWeb.Controllers;
using ChalanWeb.DAO;
using ChalanWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ChalanWeb
{
    public partial class DireccionesUsuario : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                if (Session["cliente"] != null)
                {
                    ViewState["RefUrl"] = Request.UrlReferrer.ToString();
                    PintarDirecciones();
                }

                else
                    Response.Redirect("Default.aspx#nosession");
            }
        }

        private void PintarDirecciones()
        {
            DAODireccion dire = new DAODireccion();

            Cliente cliente = (Cliente)Session["cliente"];

            List<Direccion> direcciones = dire.GetDireccionesCliente(cliente.Id);

            repetidorDirecciones.DataSource = direcciones;
            repetidorDirecciones.DataBind();
        }

        protected void repetidorDirecciones_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            string id = e.CommandArgument.ToString();
            string hash = Seguridad.Encriptar(id);
            string page = "Modificar_Direccion.aspx?id=";
            Response.Redirect(string.Concat(page,hash));
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