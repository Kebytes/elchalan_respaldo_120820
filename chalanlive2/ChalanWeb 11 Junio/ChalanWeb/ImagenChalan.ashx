<%@ WebHandler Language="C#" Class="ImagenChalan" %>

using System;
using System.Web;
using System.Data.SqlClient;
using System;
using System.Configuration;
public class ImagenChalan : IHttpHandler
{

    public bool IsReusable { get { return true; } }

    public void ProcessRequest(HttpContext ctx)
    {
        try
        {
            string id = ctx.Request.QueryString["id"];
             
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["ChalanConString"].ConnectionString);
            SqlCommand cmd = new SqlCommand("SELECT IMAGEN FROM PRODUCTOS WHERE idPRODUCTOS = @Id", con);
            cmd.Parameters.AddWithValue("@Id", id);
            con.Open();
            byte[] pict = (byte[])cmd.ExecuteScalar();
            con.Close();

            ctx.Response.ContentType = "image/jpg";
            ctx.Response.OutputStream.Write(pict,0,pict.Length);
        }
        catch { }


    }
}