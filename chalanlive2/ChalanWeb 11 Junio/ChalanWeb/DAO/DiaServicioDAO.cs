using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using System.Data.SqlClient;
using ChalanWeb.Models;

namespace ChalanWeb.DAO
{
    public class DiaServicioDAO
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["ChalanConString"].ConnectionString);
        private static string GET_DIA = "SELECT * FROM DIASDESERVICIO WHERE DIA = @DIA";

        public DiaDeServicio Get_Dia_Servicio(DateTime dia_solicitud)
        {
            try
            {
                SqlCommand cmd = new SqlCommand(GET_DIA, con);
                cmd.Parameters.AddWithValue("@DIA", dia_solicitud);
                //cmd.Parameters.AddWithValue("@Id", 1);
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    dr.Read();
                    DiaDeServicio dia = new DiaDeServicio()
                    {
                        Id = dr[0] != DBNull.Value ? dr.GetInt32(0) : 0,
                        Fecha = dr.GetDateTime(1),
                        HoraDeInicio = dr.GetTimeSpan(2),
                        HoraDeFinalizacion = dr.GetTimeSpan(3),
                        Habil = dr[4] != DBNull.Value ? Convert.ToBoolean(dr.GetByte(4)) : false
                    };
                    return dia;
                }
                return null;
            }
            catch (Exception ex)
            {
                string sldd = ex.ToString();
                return null;
            }
            finally { con.Close(); }
        }
    }
}