using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using ChalanWeb.Models;
using System.Data.SqlClient;

namespace ChalanWeb.DAO
{
    public class DAOPedido
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["ChalanConString"].ConnectionString);
        private DAOProducto DAO_Prod = new DAOProducto();
        private string insert_pedido = "INSERT INTO PEDIDOS (FECHAPEDIDO, idDIRECCION, CALLE, NUMEROEXT, NUMEROINT, COLONIA, MUNICIPIO, ESTADO, CP, NOTASCLIENTE, idCLIENTE, NOMBRECLIENTE, TELEFONOCLIENTE, idLISTADO, NOMBRELISTADO, COSTOTOTAL, COSTOENVIO, METODOPAGO, LATITUD, LONGITUD, STATUS, TOKEN, DISPOSITIVO) "
                                     + "VALUES(@FECHAPEDIDO, @idDIRECCION, @CALLE, @NUMEROEXT, @NUMEROINT, @COLONIA, @MUNICIPIO, @ESTADO, @CP, @NOTASCLIENTE, @idCLIENTE, @NOMBRECLIENTE, @TELEFONOCLIENTE, @idLISTADO, @NOMBRELISTADO, @COSTOTOTAL, @COSTOENVIO, @METODOPAGO, @LATITUD, @LONGITUD, 0, @TOKEN, @DISPOSITIVO)  SELECT SCOPE_IDENTITY() as idPedido;";

        //private string insert_pedido = "INSERT INTO PEDIDOS (FECHAPEDIDO, idDIRECCION, CALLE, NUMEROEXT, NUMEROINT, COLONIA, MUNICIPIO, ESTADO, CP, NOTASCLIENTE, idCLIENTE, NOMBRECLIENTE, TELEFONOCLIENTE, idLISTADO, NOMBRELISTADO, COSTOTOTAL, COSTOENVIO, METODOPAGO, LATITUD, LONGITUD, STATUS) "
       //                         + "VALUES(@FECHAPEDIDO, @idDIRECCION, @CALLE, @NUMEROEXT, @NUMEROINT, @COLONIA, @MUNICIPIO, @ESTADO, @CP, @NOTASCLIENTE, @idCLIENTE, @NOMBRECLIENTE, @TELEFONOCLIENTE, @idLISTADO, @NOMBRELISTADO, @COSTOTOTAL, @COSTOENVIO, @METODOPAGO, @LATITUD, @LONGITUD, 0)  SELECT SCOPE_IDENTITY() as idPedido;";

        private string get_pedidos = "SELECT idPEDIDOS, FECHAPEDIDO, FECHAACEPTADO, FECHAESTIMADA, idDIRECCION, CALLE, NUMEROEXT, NUMEROINT, COLONIA, MUNICIPIO, ESTADO, CP, NOTASCLIENTE, NOTASUSER, NOMBREREPARTIDOR, TELEFONOREPARTIDOR, COSTOTOTAL, COSTOENVIO, METODOPAGO, STATUS, LATITUD, LONGITUD, TOKENPAGO FROM PEDIDOS WHERE idCLIENTE = @IdCli";
        private string get_pedido_actual = "SELECT TOP 1 idPEDIDOS, FECHAPEDIDO, FECHAACEPTADO, FECHAESTIMADA, FECHAENTREGA, idDIRECCION, CALLE, NUMEROEXT, NUMEROINT, COLONIA, MUNICIPIO, ESTADO, CP, NOTASCLIENTE, NOTASUSER, NOMBREREPARTIDOR, TELEFONOREPARTIDOR, COSTOTOTAL, COSTOENVIO, METODOPAGO, STATUS, LATITUD, LONGITUD, TOKEN, FORMAT(FECHAESTIMADA, 'hh:mm tt') AS HoraEntrega FROM PEDIDOS WHERE idCLIENTE = @IdCli AND STATUS IN(0, 1) ORDER BY(idPEDIDOS) DESC";

        private string cancelar_pedido = "UPDATE PEDIDOS SET STATUS = 3 WHERE idPEDIDOS = @idPEDIDOS and idPEDIDOS <> 0";

        public Pedido Insert(Pedido new_pedido)
        {
            try
            {

                SqlCommand cmd = new SqlCommand(insert_pedido, con);
                cmd.Parameters.AddWithValue("@FECHAPEDIDO", new_pedido.Fecha_Pedido); //DateTime.Now.AddHours(-6)
                cmd.Parameters.AddWithValue("@idDIRECCION", new_pedido.Id_Direccion);
                cmd.Parameters.AddWithValue("@CALLE", new_pedido.Calle);
                cmd.Parameters.AddWithValue("@NUMEROEXT", new_pedido.Numero_Ext);
                cmd.Parameters.AddWithValue("@NUMEROINT", new_pedido.Numero_Int);
                cmd.Parameters.AddWithValue("@COLONIA", new_pedido.Colonia);
                cmd.Parameters.AddWithValue("@MUNICIPIO", new_pedido.Municipio);
                cmd.Parameters.AddWithValue("@ESTADO", new_pedido.Estado);
                cmd.Parameters.AddWithValue("@CP", new_pedido.CP);
                cmd.Parameters.AddWithValue("@NOTASCLIENTE", new_pedido.Nota_Cliente);
                cmd.Parameters.AddWithValue("@idCLIENTE", new_pedido.Id_Cliente);
                cmd.Parameters.AddWithValue("@NOMBRECLIENTE", new_pedido.Nombre_Cliente);
                cmd.Parameters.AddWithValue("@TELEFONOCLIENTE", new_pedido.Telefono_Cliente);
                cmd.Parameters.AddWithValue("@idLISTADO", new_pedido.Id_Listado);
                cmd.Parameters.AddWithValue("@NOMBRELISTADO", new_pedido.Nombre_Listado);
                cmd.Parameters.AddWithValue("@COSTOTOTAL", new_pedido.Costo_Total);
                cmd.Parameters.AddWithValue("@COSTOENVIO", 29);
                cmd.Parameters.AddWithValue("@METODOPAGO", new_pedido.Metodo_Pago);
                cmd.Parameters.AddWithValue("@LATITUD", new_pedido.Latitud);
                cmd.Parameters.AddWithValue("@LONGITUD", new_pedido.Longitud);
                if (string.IsNullOrEmpty(new_pedido.Token))
                    new_pedido.Token = "";
                if (string.IsNullOrEmpty(new_pedido.Dispositivo))
                    new_pedido.Dispositivo = "";
                cmd.Parameters.AddWithValue("@TOKEN", new_pedido.Token);
                cmd.Parameters.AddWithValue("@DISPOSITIVO", new_pedido.Dispositivo);

                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                dr.Read();
                int id_pedido_nvo = int.Parse(dr["idPedido"].ToString());
                if (id_pedido_nvo != 0)
                {
                    new_pedido.Id = id_pedido_nvo; //Id del pedido
                    if (new_pedido.Detalles != null)
                    {
                        foreach (DetallePedido item in new_pedido.Detalles)
                        {
                            item.Id_Pedido = id_pedido_nvo;
                            DAO_Prod.Insert(item);
                        }
                        return new_pedido;
                    }
                    return null;
                }
                return null;
            }
            catch (Exception ex)
            {
                string ds = ex.ToString();

                //Meter al log de excepciones
                return null;
            }
            finally { con.Close(); }
        }

        public List<Pedido> GetPedidosCliente(Int32 id_cli)
        {
            try
            {

                SqlCommand cmd = new SqlCommand(get_pedidos, con);
                cmd.Parameters.AddWithValue("@IdCli", id_cli);
                List<Pedido> pedidos = new List<Pedido>();
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                if(dr.HasRows)
                {
                    while(dr.Read())
                    {
                        Pedido item = new Pedido()
                        {
                            Id = dr[0] != DBNull.Value ? dr.GetInt32(0) : 0,
                            Fecha_Pedido = dr.GetDateTime(1),
                            //Fecha_Aceptado = dr.GetDateTime(2),
                            //Fecha_Estimada = dr.GetDateTime(3),
                            Id_Direccion = dr[4] != DBNull.Value ? dr.GetInt32(4) : 0,
                            Calle = dr["CALLE"] != DBNull.Value ? Convert.ToString(dr["CALLE"]) : string.Empty,                            
                            Numero_Ext = dr[6] != DBNull.Value ? dr.GetString(6) : string.Empty,
                            Numero_Int = dr[7] != DBNull.Value ? dr.GetString(7) : string.Empty,
                            Colonia = dr[8] != DBNull.Value ? dr.GetString(8) : string.Empty,
                            Municipio = dr[9] != DBNull.Value ? dr.GetString(9) : string.Empty,
                            Estado = dr[10] != DBNull.Value ? dr.GetString(10) : string.Empty,
                            CP = dr[11] != DBNull.Value ? dr.GetString(11) : string.Empty,
                            Nota_Cliente = dr[12] != DBNull.Value ? dr.GetString(12) : string.Empty,
                            Nota_User = dr[13] != DBNull.Value ? dr.GetString(13) : string.Empty,
                            Nombre_Repartidor = dr[14] != DBNull.Value ? dr.GetString(14) : string.Empty,
                            Telefono_Repartidor = dr[15] != DBNull.Value ? dr.GetString(15) : string.Empty,
                            Costo_Total = dr[16] != DBNull.Value ? dr.GetDecimal(16) : 0,
                            Costo_Envio = dr[17] != DBNull.Value ? dr.GetDecimal(17) : 0,
                            Metodo_Pago = dr[18] != DBNull.Value ? dr.GetString(18) : string.Empty,
                            Status = dr[19] != DBNull.Value ? dr.GetInt32(19) : 0,
                            Latitud = dr[20] != DBNull.Value ? dr.GetString(20) : string.Empty,
                            Longitud = dr[21] != DBNull.Value ? dr.GetString(21) : string.Empty,
                            Token = dr[22] != DBNull.Value ? dr.GetString(22) : string.Empty
                        };
                        item.Detalles = DAO_Prod.GetProductosPedido(item.Id);
                        pedidos.Add(item);
                    }
                    return pedidos;
                }
                return null;
               
            }
            catch (Exception ex)
            {
                string ds = ex.ToString();
                //Meter al log de excepciones
                return null;
            }
            finally { con.Close(); }
        }

        public Pedido GetPedidoActual(Int32 id_cli)
        {
            try
            {

                SqlCommand cmd = new SqlCommand(get_pedido_actual, con);
                cmd.Parameters.AddWithValue("@IdCli", id_cli);
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    dr.Read();
                        Pedido pedido_actual = new Pedido()
                        {
                            Id = dr[0] != DBNull.Value ? dr.GetInt32(0) : 0,
                            Fecha_Pedido = dr.GetDateTime(1),
                            Fecha_Aceptado = dr[2] != DBNull.Value ? dr.GetDateTime(2) : DateTime.MinValue,
                            Fecha_Estimada = dr[3] != DBNull.Value ? dr.GetDateTime(3) : DateTime.MinValue,
                            Fecha_Entrega = dr[4] != DBNull.Value ? dr.GetDateTime(4) : DateTime.MinValue,
                            Id_Direccion = dr[5] != DBNull.Value ? dr.GetInt32(5) : 0,
                            Calle = dr["CALLE"] != DBNull.Value ? Convert.ToString(dr["CALLE"]) : string.Empty,
                            Numero_Ext = dr[7] != DBNull.Value ? dr.GetString(7) : string.Empty,
                            Numero_Int = dr[8] != DBNull.Value ? dr.GetString(8) : string.Empty,
                            Colonia = dr[9] != DBNull.Value ? dr.GetString(9) : string.Empty,
                            Municipio = dr[10] != DBNull.Value ? dr.GetString(10) : string.Empty,
                            Estado = dr[11] != DBNull.Value ? dr.GetString(11) : string.Empty,
                            CP = dr[12] != DBNull.Value ? dr.GetString(12) : string.Empty,
                            Nota_Cliente = dr[13] != DBNull.Value ? dr.GetString(13) : string.Empty,
                            Nota_User = dr[14] != DBNull.Value ? dr.GetString(14) : string.Empty,
                            Nombre_Repartidor = dr[15] != DBNull.Value ? dr.GetString(15) : string.Empty,
                            Telefono_Repartidor = dr[16] != DBNull.Value ? dr.GetString(16) : string.Empty,
                            Costo_Total = dr[17] != DBNull.Value ? dr.GetDecimal(17) : 0,
                            Costo_Envio = dr[18] != DBNull.Value ? dr.GetDecimal(18) : 0,
                            Metodo_Pago = dr[19] != DBNull.Value ? dr.GetString(19) : string.Empty,
                            Status = dr[20] != DBNull.Value ? dr.GetInt32(20) : 0,
                            Latitud = dr[21] != DBNull.Value ? dr.GetString(21) : string.Empty,
                            Longitud = dr[22] != DBNull.Value ? dr.GetString(22) : string.Empty,
                            Token = dr[23] != DBNull.Value ? dr.GetString(23) : string.Empty,
                            HoraEntrega = dr[24] != DBNull.Value ? dr.GetString(24) : "No hay info"
                        };
                    pedido_actual.Detalles = DAO_Prod.GetProductosPedido(pedido_actual.Id);                    
                    return pedido_actual;
                }
                return null;
            }
            catch (Exception ex)
            {
                string ds = ex.ToString();
                //Meter al log de excepciones
                return null;
            }
            finally { con.Close(); }
        }

        public void CancelarPedido(Int32 idPEDIDOS)
        {
            try
            {

                SqlCommand cmd = new SqlCommand(cancelar_pedido, con);
                cmd.Parameters.AddWithValue("@idPEDIDOS", idPEDIDOS);
                con.Open();
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                string ds = ex.ToString();
                //Meter al log de excepciones
            }
            finally { con.Close(); }
        }
    }
}