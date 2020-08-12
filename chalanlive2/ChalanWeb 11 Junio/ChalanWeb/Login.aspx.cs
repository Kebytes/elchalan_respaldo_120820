using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ChalanWeb.Models;
namespace ChalanWeb
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            Page.Validate("one");
            try
            {
                string email = Request.Form["email"].ToString();
                string password = Request.Form["password"].ToString();

                DAO.DAOClient daocliente = new DAO.DAOClient();
                Cliente cliente = daocliente.GetCliente(email, password);
                
                if(cliente !=null)
                {
                    Session["cliente"] = cliente;
                    Response.Redirect("Default.aspx#logged");
                }
                else
                {
                    Response.Redirect("Login.aspx#loggnot");
                }

            }
            catch
            { }

        }

        protected void ButtonRegistro_Click(object sender, EventArgs e)
        {
            Page.Validate("two");
            try
            {
                string password = Request.Form["passwordR"].ToString();
                string confirmacion = Request.Form["passwordC"].ToString();

                if (!password.Equals(confirmacion))
                    Response.Redirect("Login.aspx#notpass");

                else
                {
                    DAO.DAOClient cliente = new DAO.DAOClient();
                    Cliente cli = new Cliente()
                    {
                        Nombre = Request.Form["nombre"].ToString(),
                        Apellido_P = Request.Form["ApellidoP"].ToString(),
                        Apellido_M = Request.Form["ApellidoM"].ToString(),
                        Telefono = Request.Form["Telefono"].ToString(),
                        Fecha_Nacimiento = Convert.ToDateTime(Request.Form["Fecha"].ToString()),
                        TarjetaChalan = Request.Form["Tarjeta"].ToString(),
                        Correo = Request.Form["emailR"].ToString(),
                        Pass = password
                    };

                    bool registro = cliente.Insert(cli);
                    if (registro)
                    {
                        Session["cliente"] = cli;
                        Response.Redirect("Default.aspx#registro");
                    }
                    else
                        Response.Redirect("Login.aspx#notreg");
                }
            }

            catch { }
        }
    }
}