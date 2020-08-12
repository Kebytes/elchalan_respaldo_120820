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
    public partial class Modificar_Direccion : System.Web.UI.Page
    {
        protected Decimal lati { get; set; }
        protected Decimal lon { get; set; }
        protected int id { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                string hash = Seguridad.DesEncriptar(Request.QueryString["id"]);
                id = Convert.ToInt32(hash);
                PintarDireccion(id);
            }
        }

        private void PintarDireccion(int id)
        {
            if(Session["cliente"] != null)
            {
                Direccion dir = null;
                Cliente cli = (Cliente) Session["cliente"];
                List<Direccion> direcciones = cli.Direcciones;
                for(int i=0; i<direcciones.Count; i++)
                {
                    if (direcciones[i].Id == id)
                    {
                        dir = direcciones[i];
                        break;
                    }

                    else
                    {
                        Response.Redirect("Default.aspx#NotDir");
                    }
                }

                if (dir != null)
                {
                    Calle.Value = dir.Calle;
                    NumeroE.Value = dir.Numero_Ext;
                    NumeroI.Value = dir.Numero_Int;
                    Colonia.Value = dir.Colonia;
                    CodPos.Value = dir.CP;
                    Municipio.Value = dir.Municipio;
                    Estado.Value = dir.Estado;
                    longitud.Value = dir.Longitud;
                    latitud.Value = dir.Latitud;
                    this.lati = Convert.ToDecimal(dir.Latitud);
                    this.lon = Convert.ToDecimal(dir.Longitud);
                }
            }
        }

        protected void ButtonModificarDireccion_Click(object sender, EventArgs e)
        {
            id = Convert.ToInt32(Request.QueryString["id"]);
            Cliente cli = null;
            Direccion direccion = new Direccion();
            DAO.DAODireccion actualizarDireccion = new DAO.DAODireccion();
            
            if (Session["cliente"] != null)
            {
                cli = (Cliente) Session["cliente"];
            }

            direccion.Calle = Calle.Value;
            direccion.Numero_Ext = NumeroE.Value;
            direccion.Numero_Int = NumeroI.Value;
            direccion.Colonia = Colonia.Value;
            direccion.CP = CodPos.Value;
            direccion.Municipio = Municipio.Value;
            direccion.Estado = Estado.Value;
            direccion.Longitud = longitud.Value;
            direccion.Latitud = latitud.Value;
            direccion.Id_Cliente = cli.Id;
            direccion.Id = id;

            bool actualizacion = actualizarDireccion.UpdateDireccionCliente(direccion);

            if (actualizacion)
            {
                Response.Redirect("DireccionesUsuario.aspx#UpdateDir");
            }

            else
            {

            }
        }
    }
}