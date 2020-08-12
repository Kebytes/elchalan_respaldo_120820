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
    public partial class NuevaDireccion : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                ViewState["RefUrl"] = Request.UrlReferrer.ToString();
            }
        }

        protected void ButtonAgregarDireccion_Click(object sender, EventArgs e)
        {
            Cliente cli = (Cliente)HttpContext.Current.Session["cliente"];
            DAO.DAODireccion altaDir = new DAO.DAODireccion();
            Direccion direccion = new Direccion()
            {
                Calle = Calle.Value,
                Numero_Ext = NumeroE.Value,
                Numero_Int = string.IsNullOrEmpty(NumeroI.Value) ? "" : NumeroI.Value,
                Colonia = Colonia.Value,
                CP = CodPos.Value,
                Municipio = Municipio.Value,
                Estado = string.IsNullOrEmpty(Estado.Value) ? "" : Estado.Value,
                Longitud = longitud.Value,
                Latitud = latitud.Value,
                Id_Cliente = cli.Id,
                Activo = 1
            };

            bool insercion = altaDir.Insert(direccion);
            if (insercion)
            {
                Response.Redirect("Default.aspx#AltaDir");
            }
            else
            {
                Response.Redirect("Prueba.aspx#Error");
            }
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