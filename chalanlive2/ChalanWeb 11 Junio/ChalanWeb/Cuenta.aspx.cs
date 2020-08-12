using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ChalanWeb.Models;
using ChalanWeb.DAO;
namespace ChalanWeb
{
    public partial class Cuenta : System.Web.UI.Page
    {
        protected string Telefono { get; set; }
        protected string Nombre { get; set; }
        protected string ApellidoP { get; set; }
        protected string ApellidoM { get; set; }
        protected string TarjetaChal { get; set; }
        protected string Email { get; set; }
        protected string Pass { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            //PintarDirecciones();

            if (!Page.IsPostBack)
            {
                Cliente cliente = (Cliente)Session["cliente"];
                this.Telefono = cliente.Telefono;
                this.Nombre = cliente.Nombre;
                this.ApellidoP = cliente.Apellido_P;
                this.ApellidoM = cliente.Apellido_M;
                this.TarjetaChal = cliente.TarjetaChalan;
                this.Email = cliente.Correo;
                this.Pass = cliente.Pass;
            }
            
        }

        private void PintarDirecciones()
        {
            DAODireccion dire = new DAODireccion();

            Cliente cliente = (Cliente)Session["cliente"];

            //textApellidoMat.Value = cliente.Apellido_M;
            //textApellidoPat.Value = cliente.Apellido_P;
            //textNombre.Value = cliente.Nombre;
            //textemail.Value = cliente.Correo;
            ////Fecha.Value = cliente.Fecha_Nacimiento.ToString("dd/MM/yyyy");
            ////Telefono.Value = cliente.Telefono;
            //Tarjeta.Value = cliente.TarjetaChalan;
            //textcontrasena.Value = cliente.Pass;
        }

        protected void ButtonActualizar_Click(object sender, EventArgs e)
        {
            try
            {
                DAOClient update = new DAOClient();
                Cliente cliente = (Cliente)Session["cliente"];

                Cliente cli = new Cliente();
                cli.Id = cliente.Id;
                cli.Apellido_M = Request.Form["textApellidoMat"].ToString();
                cli.Apellido_P = Request.Form["textApellidoPat"].ToString();
                cli.Nombre = Request.Form["textNombre"].ToString();
                cli.Correo = Request.Form["textemail"].ToString();
                cli.Telefono = Request.Form["Telefono"].ToString();
                //if (!string.IsNullOrEmpty(textcontrasena.Value))
                //    cli.Pass = textcontrasena.Value;
                cli.Fecha_Nacimiento = cliente.Fecha_Nacimiento;
                if (!string.IsNullOrEmpty(Request.Form["Tarjeta"].ToString()))
                    cli.TarjetaChalan = Request.Form["Tarjeta"].ToString();

                bool cambio = update.UpdateCliente(cli);
                if (cambio)
                {
                    cliente.Nombre = cli.Nombre;
                    cliente.Apellido_M = cli.Apellido_M;
                    cliente.Apellido_P = cli.Apellido_P;
                    cliente.Telefono = cli.Telefono;
                    cliente.Correo = cli.Correo;
                    cliente.Fecha_Nacimiento = cli.Fecha_Nacimiento;
                    cliente.TarjetaChalan = cli.TarjetaChalan;
                    Session["cliente"] = cliente;
                    Response.Redirect("Cuenta.aspx#cuenta");
                }

                else
                    Response.Redirect("Cuenta.aspx#notupdate");
            }
            catch (Exception ex) { string error = ex.Message; }
        }
    }
}