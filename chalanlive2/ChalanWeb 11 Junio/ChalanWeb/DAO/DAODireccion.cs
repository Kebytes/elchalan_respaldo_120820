using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Configuration;
using ChalanWeb.Models;

namespace ChalanWeb.DAO
{
    public class DAODireccion
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["ChalanConString"].ConnectionString);
        private static string INSERT_DIRECCION = "INSERT INTO DIRECCIONES(CALLE, NUMEROEXT, NUMEROINT, COLONIA, CP, MUNICIPIO, ESTADO, LONGITUD, LATITUD, idCLIENTE) VALUES(@CALLE, @NUMEROEXT, @NUMEROINT, @COLONIA, @CP, @MUNICIPIO, @ESTADO, @LONGITUD, @LATITUD, @idCLIENTE)";
        private static string GET_DIRECCIONES_CLIENTE = "SELECT idDIRECCIONES, CALLE, NUMEROEXT, NUMEROINT, COLONIA, CP, MUNICIPIO, ESTADO, LONGITUD, LATITUD, ACTIVO FROM DIRECCIONES WHERE idCLIENTE = @IdCli AND ACTIVO = 1";
        private static string SET_STATUS_ACTIVO = "UPDATE DIRECCIONES SET ACTIVO = @Bandera WHERE idDIRECCIONES = @Id_Dir";
        private static string UPDATE_DIRECCION_CLIENTE = "UPDATE DIRECCIONES SET CALLE = @CALLE, NUMEROEXT = @NUMEROEXT, NUMEROINT = @NUMEROINT, COLONIA = @COLONIA, CP = @CP, MUNICIPIO = @MUNICIPIO, ESTADO = @ESTADO, LONGITUD = @LONGITUD, LATITUD = @LATITUD WHERE idCLIENTE = @idCLIENTE AND idDIRECCIONES = @idDIRECCIONES";

        public bool Insert(Direccion new_dir)
        {
            try
            {

                SqlCommand cmd = new SqlCommand(INSERT_DIRECCION, con);
                cmd.Parameters.AddWithValue("@CALLE", new_dir.Calle);
                cmd.Parameters.AddWithValue("@NUMEROEXT", new_dir.Numero_Ext);
                cmd.Parameters.AddWithValue("@NUMEROINT", new_dir.Numero_Int);
                cmd.Parameters.AddWithValue("@COLONIA", new_dir.Colonia);
                cmd.Parameters.AddWithValue("@CP", new_dir.CP);
                cmd.Parameters.AddWithValue("@MUNICIPIO", new_dir.Municipio);
                cmd.Parameters.AddWithValue("@ESTADO", new_dir.Estado);
                cmd.Parameters.AddWithValue("@LONGITUD", new_dir.Longitud);
                cmd.Parameters.AddWithValue("@LATITUD", new_dir.Latitud);
                cmd.Parameters.AddWithValue("@idCLIENTE", new_dir.Id_Cliente);
                con.Open();
                cmd.ExecuteNonQuery();
                return true;
            }
            catch
            {
                return false;
                //Meter un log con excepción.
            }
            finally { con.Close(); }
        }

        public void SetStatusActivo(Int32 id_direccion, Int32 banderaActivo)
        {
            try
            {

                SqlCommand cmd = new SqlCommand(SET_STATUS_ACTIVO, con);
                cmd.Parameters.AddWithValue("@Id_Dir", id_direccion);
                cmd.Parameters.AddWithValue("@Bandera", banderaActivo);
                con.Open();
                cmd.ExecuteNonQuery();
            }
            catch
            {
                //Meter un log con excepción.
            }
            finally { con.Close(); }
        }

        public bool UpdateDireccionCliente(Direccion direccion)
        {
            try
            {
                SqlCommand cmd = new SqlCommand(UPDATE_DIRECCION_CLIENTE, con);
                cmd.Parameters.AddWithValue("@CALLE", direccion.Calle);
                cmd.Parameters.AddWithValue("@NUMEROEXT", direccion.Numero_Ext);
                cmd.Parameters.AddWithValue("@NUMEROINT", direccion.Numero_Int);
                cmd.Parameters.AddWithValue("@COLONIA", direccion.Colonia);
                cmd.Parameters.AddWithValue("@CP", direccion.CP);
                cmd.Parameters.AddWithValue("@MUNICIPIO", direccion.Municipio);
                cmd.Parameters.AddWithValue("@ESTADO", direccion.Estado);
                cmd.Parameters.AddWithValue("@LONGITUD", direccion.Longitud);
                cmd.Parameters.AddWithValue("@LATITUD", direccion.Latitud);
                cmd.Parameters.AddWithValue("@idCLIENTE", direccion.Id_Cliente);
                cmd.Parameters.AddWithValue("@idDIRECCIONES", direccion.Id);
                con.Open();
                cmd.ExecuteNonQuery();
                return true;
            }
            catch (Exception e) { return false; }
            finally { con.Close(); }
        }

        public List<Direccion> GetDireccionesCliente(Int32 Id_Cliente)
        {
            try
            {
                List<Direccion> direcciones_cli = new List<Direccion>();
                SqlCommand cmd = new SqlCommand(GET_DIRECCIONES_CLIENTE, con);
                cmd.Parameters.AddWithValue("@IdCli", Id_Cliente);
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    while(dr.Read())
                    {
                        Direccion dir = new Direccion()
                        {
                            Id = dr[0] != DBNull.Value ? dr.GetInt32(0) : 0,
                            Calle = dr[1] != DBNull.Value ? dr.GetString(1) : string.Empty,
                            Numero_Ext = dr[2] != DBNull.Value ? dr.GetString(2) : string.Empty,
                            Numero_Int = dr[3] != DBNull.Value ? dr.GetString(3) : string.Empty,
                            Colonia = dr[4] != DBNull.Value ? dr.GetString(4) : string.Empty,
                            CP = dr[5] != DBNull.Value ? dr.GetString(5) : string.Empty,
                            Municipio = dr[6] != DBNull.Value ? dr.GetString(6) : string.Empty,
                            Estado = dr[7] != DBNull.Value ? dr.GetString(7) : string.Empty,
                            Longitud = dr[8] != DBNull.Value ? dr.GetString(8) : string.Empty,
                            Latitud = dr[9] != DBNull.Value ? dr.GetString(9) : string.Empty,
                            Activo= dr[10] != DBNull.Value ? dr.GetInt32(10) : 0,
                        };
                        direcciones_cli.Add(dir);
                    }
                    return direcciones_cli;
                }
                return null;
            }
            catch(Exception ex)
            {
                string ekd = ex.ToString();
                //Un log o qué?
                return null;
            }
            finally { con.Close(); }
        }
    }
}