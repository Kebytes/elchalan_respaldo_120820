using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using System.Data.SqlClient;
using ChalanWeb.Models;
using System.Diagnostics;
using System.Text;

namespace ChalanWeb.DAO
{
    public class DAOClient
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["ChalanConString"].ConnectionString);
        private DAODireccion DAO_Dir = new DAODireccion();
        private DAOPedido DAO_Ped = new DAOPedido();

        //private static string INSERT_CLIENT = "INSERT INTO CLIENTES(NOMBRE, APELLIDOPATERNO, APELLIDOMATERNO, FECHANACIMIENTO, SEXO, TELEFONO, CORREO, PASS, TOKENFB, TOKENFB) VALUES(@NOMBRE, @APELLIDOPATERNO, @APELLIDOMATERNO, @FECHANACIMIENTO, @SEXO, @TELEFONO, @CORREO, @PASS, @TOKENFB) SELECT SCOPE_IDENTITY() as idCli";

        private static string INSERT_CLIENT = "INSERT INTO CLIENTES(NOMBRE, APELLIDOPATERNO, APELLIDOMATERNO, FECHANACIMIENTO, SEXO, TELEFONO, CORREO, PASS, TOKENFB, TOKENUSER, TARJETACHALAN) VALUES(@NOMBRE, @APELLIDOPATERNO, @APELLIDOMATERNO, @FECHANACIMIENTO, @SEXO, @TELEFONO, @CORREO, @PASS, @TOKENFB, @TOKENUSER, @TARJETACHALAN) SELECT SCOPE_IDENTITY() as idCli";
        private static string INSERT_CLIENT_FB = "INSERT INTO CLIENTES(NOMBRE, FECHANACIMIENTO, SEXO, TELEFONO, CORREO, TOKENFB) VALUES(@NOMBRE, @FECHANACIMIENTO, @SEXO, @TELEFONO, @CORREO, @TOKENFB) SELECT SCOPE_IDENTITY() as idCli";
        private static string LOGIN_CLIENT = "SELECT idClientes, CONCAT(c.NOMBRE, ' ', APELLIDOPATERNO, ' ', APELLIDOMATERNO) as fullname, DATEDIFF(hour,FECHANACIMIENTO,GETDATE())/8766 AS Edad, SEXO, TELEFONO, CORREO, idLISTA, FECHANACIMIENTO, l.NOMBRE, TOKENUSER, TARJETACHALAN, c.NOMBRE, c.APELLIDOPATERNO, c.APELLIDOMATERNO,TOKENOP FROM CLIENTES c, LISTADOS l WHERE CORREO = @CORREO AND PASS = @PASS AND c.idLISTA = l.idLISTADOS";
        private static string LOGIN_CLIENT_FB = "SELECT idClientes, CONCAT(c.NOMBRE, ' ', APELLIDOPATERNO, ' ', APELLIDOMATERNO) as fullname, DATEDIFF(hour,FECHANACIMIENTO,GETDATE())/8766 AS Edad, SEXO, TELEFONO, CORREO, idLISTA, FECHANACIMIENTO, l.NOMBRE, c.APELLIDOPATERNO, c.APELLIDOMATERNO, TOKENUSER, TARJETACHALAN, c.NOMBRE FROM CLIENTES c, LISTADOS l WHERE TOKENFB = @TOKENFB AND c.idLISTA = l.idLISTADOS";
        private static string GET_CLIENTE_BY_ID = "SELECT idClientes, CONCAT(c.NOMBRE, ' ', APELLIDOPATERNO, ' ', APELLIDOMATERNO) as fullname, DATEDIFF(hour,FECHANACIMIENTO,GETDATE())/8766 AS Edad, SEXO, TELEFONO, CORREO, idLISTA, FECHANACIMIENTO, l.NOMBRE, TOKENUSER, TARJETACHALAN, c.NOMBRE, c.APELLIDOPATERNO, c.APELLIDOMATERNO FROM CLIENTES c, LISTADOS l WHERE c.idLISTA = l.idLISTADOS AND idClientes = @Id_Cli";
        private static string GET_CLIENTE_BY_MAIL = "SELECT idClientes, CONCAT(c.NOMBRE, ' ', APELLIDOPATERNO, ' ', APELLIDOMATERNO) as fullname, DATEDIFF(hour,FECHANACIMIENTO,GETDATE())/8766 AS Edad, SEXO, TELEFONO, CORREO, idLISTA, FECHANACIMIENTO, l.NOMBRE, TOKENUSER, TARJETACHALAN, c.NOMBRE, c.APELLIDOPATERNO, c.APELLIDOMATERNO FROM CLIENTES c, LISTADOS l WHERE c.idLISTA = l.idLISTADOS AND CORREO = @CORREO";

        private static string SET_TOKENUSER = "UPDATE CLIENTES SET TOKENUSER = @TOKENUSER WHERE idCLIENTES = @Id";
        private static string UPDATE_CLIENTE = "UPDATE CLIENTES SET NOMBRE = @NOMBRE, APELLIDOPATERNO = @APELLIDOPATERNO, APELLIDOMATERNO = @APELLIDOMATERNO, FECHANACIMIENTO = @FECHANACIMIENTO, CORREO = @CORREO, TELEFONO = @TELEFONO, TARJETACHALAN = @TARJETACHALAN WHERE idClientes = @idClientes";
        //private static string LOGIN_CLIENT = "SELECT idClientes, CONCAT(NOMBRE, ' ', APELLIDOPATERNO, ' ', APELLIDOMATERNO) as fullname, DATEDIFF(hour,FECHANACIMIENTO,GETDATE())/8766 AS Edad, SEXO, TELEFONO, CORREO, idLISTA FROM CLIENTES WHERE idClientes = @Id";

        public Boolean Insert(Cliente new_cli)
        {
            try
            {

                SqlCommand cmd = new SqlCommand(INSERT_CLIENT, con);
                cmd.Parameters.AddWithValue("@NOMBRE", new_cli.Nombre);
                cmd.Parameters.AddWithValue("@APELLIDOPATERNO", new_cli.Apellido_P);
                cmd.Parameters.AddWithValue("@APELLIDOMATERNO", new_cli.Apellido_M);
                cmd.Parameters.AddWithValue("@FECHANACIMIENTO", new_cli.Fecha_Nacimiento);
                //cmd.Parameters.AddWithValue("@SEXO", new_cli.Sexo);
                cmd.Parameters.AddWithValue("@TELEFONO", new_cli.Telefono);
                cmd.Parameters.AddWithValue("@CORREO", new_cli.Correo);
                cmd.Parameters.AddWithValue("@PASS", new_cli.Pass);
                
                if (string.IsNullOrEmpty(new_cli.TokenUser))
                    new_cli.TokenUser = "";
                cmd.Parameters.AddWithValue("@TOKENUSER", new_cli.TokenUser);
                if (string.IsNullOrEmpty(new_cli.Sexo))
                    new_cli.Sexo = "";
                cmd.Parameters.AddWithValue("@SEXO", new_cli.Sexo);
                if (string.IsNullOrEmpty(new_cli.TarjetaChalan))
                    new_cli.TarjetaChalan = "";
                cmd.Parameters.AddWithValue("@TARJETACHALAN", new_cli.TarjetaChalan);
                if (string.IsNullOrEmpty(new_cli.Token_FB))
                    new_cli.Token_FB = "";
                cmd.Parameters.AddWithValue("@TOKENFB", new_cli.Token_FB);
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                dr.Read();
                int id_cli_insertado = int.Parse(dr["idCli"].ToString());
                if(id_cli_insertado != 0)
                {
                    if(new_cli.Direcciones != null)
                    {
                        foreach (Direccion new_dir in new_cli.Direcciones)
                        {
                            new_dir.Id_Cliente = id_cli_insertado;
                            DAO_Dir.Insert(new_dir);
                        }
                        return true;
                    }
                    return true;                    
                }
                return false;
        }
            catch(Exception ex)
            {
                string ds = ex.ToString();
                
                //Meter al log de excepciones
                return false;
            }
            finally { con.Close(); }
        }
        public bool UpdateCliente(Cliente cli_update)
        {
            //NOMBRE = @NOMBRE, APELLIDOPATERNO = @APELLIDOPATERNO, APELLIDOMATERNO = @APELLIDOMATERNO, FECHANACIMIENTO = @FECHANACIMIENTO, CORREO = @CORREO, TELEFONO = @TELEFONO WHERE idClientes = @idClientes
            try
            {
                SqlCommand cmd = new SqlCommand(UPDATE_CLIENTE, con);
                cmd.Parameters.AddWithValue("@NOMBRE", cli_update.Nombre);
                cmd.Parameters.AddWithValue("@APELLIDOPATERNO", cli_update.Apellido_P);
                cmd.Parameters.AddWithValue("@APELLIDOMATERNO", cli_update.Apellido_M);
                cmd.Parameters.AddWithValue("@FECHANACIMIENTO", cli_update.Fecha_Nacimiento);
                cmd.Parameters.AddWithValue("@CORREO", cli_update.Correo);
                cmd.Parameters.AddWithValue("@TELEFONO", cli_update.Telefono);
                cmd.Parameters.AddWithValue("@idClientes", cli_update.Id);
                if (string.IsNullOrEmpty(cli_update.TarjetaChalan))
                    cli_update.TarjetaChalan = "";
                cmd.Parameters.AddWithValue("@TARJETACHALAN", cli_update.TarjetaChalan);
                con.Open();
                cmd.ExecuteNonQuery();
                return true;
            }
            catch (Exception ex)
            {
                string sldd = ex.ToString();
                return false;
            }
            finally { con.Close(); }
        }

        public Boolean InsertWithFB(Cliente new_cli)
        {
            try
            {
                //@NOMBRE, @FECHANACIMIENTO, @SEXO, @TELEFONO, @CORREO, @TOKENFB
                SqlCommand cmd = new SqlCommand(INSERT_CLIENT_FB, con);
                cmd.Parameters.AddWithValue("@NOMBRE", new_cli.Nombre);
                cmd.Parameters.AddWithValue("@APELLIDOPATERNO", new_cli.Apellido_P);
                //cmd.Parameters.AddWithValue("@APELLIDOMATERNO", new_cli.Apellido_M);
                //cmd.Parameters.AddWithValue("@FECHANACIMIENTO", new_cli.Fecha_Nacimiento);
                cmd.Parameters.AddWithValue("@SEXO", new_cli.Sexo);
                cmd.Parameters.AddWithValue("@TELEFONO", new_cli.Telefono);
                cmd.Parameters.AddWithValue("@CORREO", new_cli.Correo);
                cmd.Parameters.AddWithValue("@TOKENFB", new_cli.Token_FB);
                //cmd.Parameters.AddWithValue("@PASS", new_cli.Pass);
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                dr.Read();
                int id_cli_insertado = int.Parse(dr["idCli"].ToString());
                if (id_cli_insertado != 0)
                {
                    if (new_cli.Direcciones != null)
                    {
                        foreach (Direccion new_dir in new_cli.Direcciones)
                        {
                            new_dir.Id_Cliente = id_cli_insertado;
                            DAO_Dir.Insert(new_dir);
                        }
                        return true;
                    }
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                string ds = ex.ToString();

                //Meter al log de excepciones
                return false;
            }
            finally { con.Close(); }
        }

        public static string CreateMD5(string input)
        {
            // Use input string to calculate MD5 hash
            using (System.Security.Cryptography.MD5 md5 = System.Security.Cryptography.MD5.Create())
            {
                byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(input);
                byte[] hashBytes = md5.ComputeHash(inputBytes);

                // Convert the byte array to hexadecimal string
                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < hashBytes.Length; i++)
                {
                    sb.Append(hashBytes[i].ToString("X2"));
                }
                return sb.ToString();
            }
        }

        public Cliente GetCliente(String Mail, String Pass)
        {
            try
            {
                SqlCommand cmd = new SqlCommand(LOGIN_CLIENT, con);
                cmd.Parameters.AddWithValue("@CORREO", Mail);
                cmd.Parameters.AddWithValue("@PASS", CreateMD5(Pass));
                //cmd.Parameters.AddWithValue("@Id", 1);
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                if(dr.HasRows)
                {
                    dr.Read();
                    Cliente cli = new Cliente()
                    {
                        Id = dr[0] != DBNull.Value ? dr.GetInt32(0) : 0,
                        Nombre = dr[1] != DBNull.Value ? dr.GetString(1) : string.Empty,
                        Edad = dr[2] != DBNull.Value ? dr.GetInt32(2) : 0,
                        Sexo = dr[3] != DBNull.Value ? dr.GetString(3) : string.Empty,
                        Telefono = dr[4] != DBNull.Value ? dr.GetString(4) : string.Empty,
                        Correo = dr[5] != DBNull.Value ? dr.GetString(5) : string.Empty,
                        Id_Lista = dr[6] != DBNull.Value ? dr.GetInt32(6) : 0,
                        Fecha_Nacimiento = dr.GetDateTime(7),
                        Nombre_Listado = dr[8] != DBNull.Value ? dr.GetString(8) : string.Empty,
                        TokenUser = dr[9] != DBNull.Value ? dr.GetString(9) : string.Empty,
                        TarjetaChalan = dr[10] != DBNull.Value ? dr.GetString(10) : string.Empty,
                        SoloNombre = dr[11] != DBNull.Value ? dr.GetString(11) : string.Empty,
                        Apellido_P = dr[12] != DBNull.Value ? dr.GetString(12) : string.Empty,
                        Apellido_M = dr[13] != DBNull.Value ? dr.GetString(13) : string.Empty,
                        TOKENOP = dr[14] != DBNull.Value ? dr.GetString(14) : string.Empty,
                        Direcciones = DAO_Dir.GetDireccionesCliente(dr.GetInt32(0))
                    };
                    cli.Pedidos = DAO_Ped.GetPedidosCliente(cli.Id);
                    return cli;
                }
                return null;
            }
            catch(Exception ex)
            {
                string sldd = ex.ToString();
                return null;
            }
            finally { con.Close(); }
        }

        public Cliente GetClienteById(Int32 id_cliente)
        {
            try
            {
                SqlCommand cmd = new SqlCommand(GET_CLIENTE_BY_ID, con);
                cmd.Parameters.AddWithValue("@Id_Cli", id_cliente);
                //cmd.Parameters.AddWithValue("@Id", 1);
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    dr.Read();
                    Cliente cli = new Cliente()
                    {
                        Id = dr[0] != DBNull.Value ? dr.GetInt32(0) : 0,
                        Nombre = dr[1] != DBNull.Value ? dr.GetString(1) : string.Empty,
                        Edad = dr[2] != DBNull.Value ? dr.GetInt32(2) : 0,
                        Sexo = dr[3] != DBNull.Value ? dr.GetString(3) : string.Empty,
                        Telefono = dr[4] != DBNull.Value ? dr.GetString(4) : string.Empty,
                        Correo = dr[5] != DBNull.Value ? dr.GetString(5) : string.Empty,
                        Id_Lista = dr[6] != DBNull.Value ? dr.GetInt32(6) : 0,
                        Fecha_Nacimiento = dr.GetDateTime(7),
                        Nombre_Listado = dr[8] != DBNull.Value ? dr.GetString(8) : string.Empty,
                        TokenUser = dr[9] != DBNull.Value ? dr.GetString(9) : string.Empty,
                        TarjetaChalan = dr[10] != DBNull.Value ? dr.GetString(10) : string.Empty,
                        SoloNombre = dr[11] != DBNull.Value ? dr.GetString(11) : string.Empty,
                        Apellido_P = dr[12] != DBNull.Value ? dr.GetString(12) : string.Empty,
                        Apellido_M = dr[13] != DBNull.Value ? dr.GetString(13) : string.Empty
                    };
                    cli.Direcciones = DAO_Dir.GetDireccionesCliente(cli.Id);
                    cli.Pedidos = DAO_Ped.GetPedidosCliente(cli.Id);
                    return cli;
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


        public Cliente GetClienteWithFB(String FB_Id)
        {
            try
            {
                SqlCommand cmd = new SqlCommand(LOGIN_CLIENT_FB, con);
                cmd.Parameters.AddWithValue("@TOKENFB", FB_Id);
                //cmd.Parameters.AddWithValue("@Id", 1);
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    dr.Read();
                    Cliente cli = new Cliente()
                    {
                        Id = dr[0] != DBNull.Value ? dr.GetInt32(0) : 0,
                        Nombre = dr[1] != DBNull.Value ? dr.GetString(1) : string.Empty,
                        Edad = dr[2] != DBNull.Value ? dr.GetInt32(2) : 0,
                        Sexo = dr[3] != DBNull.Value ? dr.GetString(3) : string.Empty,
                        Telefono = dr[4] != DBNull.Value ? dr.GetString(4) : string.Empty,
                        Correo = dr[5] != DBNull.Value ? dr.GetString(5) : string.Empty,
                        Id_Lista = dr[6] != DBNull.Value ? dr.GetInt32(6) : 0,
                        Fecha_Nacimiento = dr.GetDateTime(7),
                        Nombre_Listado = dr[8] != DBNull.Value ? dr.GetString(8) : string.Empty,
                        Apellido_P = dr[9] != DBNull.Value ? dr.GetString(9) : string.Empty,
                        Apellido_M = dr[10] != DBNull.Value ? dr.GetString(10) : string.Empty,
                        TokenUser = dr[11] != DBNull.Value ? dr.GetString(11) : string.Empty,
                        TarjetaChalan = dr[12] != DBNull.Value ? dr.GetString(12) : string.Empty,
                        SoloNombre = dr[13] != DBNull.Value ? dr.GetString(13) : string.Empty,
                        Direcciones = DAO_Dir.GetDireccionesCliente(dr.GetInt32(0))
                    };
                    cli.Pedidos = DAO_Ped.GetPedidosCliente(cli.Id);
                    return cli;
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

        public Cliente GetClientePorCorreo(String CORREO)
        {
            try
            {
                SqlCommand cmd = new SqlCommand(GET_CLIENTE_BY_MAIL, con);
                cmd.Parameters.AddWithValue("@CORREO", CORREO);
                //cmd.Parameters.AddWithValue("@Id", 1);
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    dr.Read();
                    Cliente cli = new Cliente()
                    {
                        Id = dr[0] != DBNull.Value ? dr.GetInt32(0) : 0,
                        Nombre = dr[1] != DBNull.Value ? dr.GetString(1) : string.Empty,
                        Edad = dr[2] != DBNull.Value ? dr.GetInt32(2) : 0,
                        Sexo = dr[3] != DBNull.Value ? dr.GetString(3) : string.Empty,
                        Telefono = dr[4] != DBNull.Value ? dr.GetString(4) : string.Empty,
                        Correo = dr[5] != DBNull.Value ? dr.GetString(5) : string.Empty,
                        Id_Lista = dr[6] != DBNull.Value ? dr.GetInt32(6) : 0,
                        Fecha_Nacimiento = dr.GetDateTime(7),
                        Nombre_Listado = dr[8] != DBNull.Value ? dr.GetString(8) : string.Empty,
                        TokenUser = dr[9] != DBNull.Value ? dr.GetString(9) : string.Empty,
                        Direcciones = DAO_Dir.GetDireccionesCliente(dr.GetInt32(0)),
                        TarjetaChalan = dr[10] != DBNull.Value ? dr.GetString(10) : string.Empty,
                        SoloNombre = dr[11] != DBNull.Value ? dr.GetString(11) : string.Empty,
                        Apellido_P = dr[12] != DBNull.Value ? dr.GetString(12) : string.Empty,
                        Apellido_M = dr[13] != DBNull.Value ? dr.GetString(13) : string.Empty
                    };
                    cli.Pedidos = DAO_Ped.GetPedidosCliente(cli.Id);
                    return cli;
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

        public void SetTokenUser(Int32 idUser, String tokenUser)
        {
            try
            {
                SqlCommand cmd = new SqlCommand(SET_TOKENUSER, con);
                cmd.Parameters.AddWithValue("@Id", idUser);
                cmd.Parameters.AddWithValue("@TOKENUSER", tokenUser);                
                con.Open();
                cmd.ExecuteNonQuery();                
            }
            catch (Exception ex)
            {
                string sldd = ex.ToString();
            }
            finally { con.Close(); }
        }


    }
}