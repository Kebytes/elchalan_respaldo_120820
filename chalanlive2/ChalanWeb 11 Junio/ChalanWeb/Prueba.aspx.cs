using ChalanWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ChalanWeb
{
    public partial class Prueba : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                DAO.DAODireccion direcc = new DAO.DAODireccion();
                List<Direccion> direcciones = direcc.GetDireccionesCliente(199);
                rptMarkers.DataSource = direcciones;
                rptMarkers.DataBind();
                rptMaps.DataSource = direcciones;
                rptMaps.DataBind();
            }
        }
    }
}