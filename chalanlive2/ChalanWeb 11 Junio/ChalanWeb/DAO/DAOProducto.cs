using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using System.Data.SqlClient;
using ChalanWeb.Models;

namespace ChalanWeb.DAO
{
    public class DAOProducto
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["ChalanConString"].ConnectionString);
        private static string insert_prod_pedido = "INSERT INTO DETALLEPEDIDO (idPEDIDO, idPRODUCTO, NOMBREPRODUCTO, PRECIO, CANTIDAD, COSTO) VALUES(@idPEDIDO, @idPRODUCTO, @NOMBREPRODUCTO, @PRECIO, @CANTIDAD, @COSTO); UPDATE PRODUCTOS SET CANTIDAD = CANTIDAD - @CANTIDAD WHERE idPRODUCTOS = @idPRODUCTO";
        private static string GET_PRODUCTOS = "SELECT p.idPRODUCTOS, p.NOMBRE, p.PRECIO, p.FOTOGRAFIA, 'LICORES', p.FAVORITO, p.DESCRIPCION, p.PROMO, p.IMAGEN FROM PRODUCTOS p WHERE p.VISIBLE = 1 AND  PRECIO <> 0.00 AND p.FAVORITO IN (0,1) AND p.CATEGORIA = 'BOTELLAS' UNION  SELECT p.idPRODUCTOS, p.NOMBRE, p.PRECIO, p.FOTOGRAFIA, 'TOP', p.FAVORITO, p.DESCRIPCION, p.PROMO, p.IMAGEN FROM PRODUCTOS p WHERE p.VISIBLE = 1 AND  PRECIO <> 0.00 AND p.FAVORITO = 1 UNION SELECT p.idPRODUCTOS, p.NOMBRE, p.PRECIO, p.FOTOGRAFIA, p.CATEGORIA, p.FAVORITO, p.DESCRIPCION, p.PROMO, p.IMAGEN FROM PRODUCTOS p WHERE p.VISIBLE = 1 AND PRECIO <> 0.00 AND p.FAVORITO IN (0,1) ORDER BY(p.NOMBRE) ASC";
        private static string GET_PRODUCTOS_PEDIDO = "SELECT idDETALLEPEDIDO, idPEDIDO, idPRODUCTO, NOMBREPRODUCTO, PRECIO, CANTIDAD, COSTO FROM DETALLEPEDIDO WHERE idPEDIDO = @IdPedido";

        //private static string GET_PRODUCTOS = "SELECT p.idPRODUCTOS, p.NOMBRE, p.PRECIO, p.FOTOGRAFIA, 'LICORES', p.FAVORITO, p.DESCRIPCION, p.PROMO, p.IMAGEN, p.PRECIOSIMBOLICO FROM PRODUCTOS p WHERE p.VISIBLE = 1 AND  PRECIO <> 0.00 AND p.FAVORITO IN (0,1) AND p.CATEGORIA = 'BOTELLAS' UNION  SELECT p.idPRODUCTOS, p.NOMBRE, p.PRECIO, p.FOTOGRAFIA, 'TOP', p.FAVORITO, p.DESCRIPCION, p.PROMO, p.IMAGEN, p.PRECIOSIMBOLICO FROM PRODUCTOS p WHERE p.VISIBLE = 1 AND  PRECIO <> 0.00 AND p.FAVORITO = 1 UNION SELECT p.idPRODUCTOS, p.NOMBRE, p.PRECIO, p.FOTOGRAFIA, p.CATEGORIA, p.FAVORITO, p.DESCRIPCION, p.PROMO, p.IMAGEN, p.PRECIOSIMBOLICO FROM PRODUCTOS p WHERE p.VISIBLE = 1 AND PRECIO <> 0.00 AND p.FAVORITO IN (0,1) ORDER BY(p.NOMBRE) ASC";

        public void Insert(DetallePedido new_prod_pedido)
        {
            try
            {

                SqlCommand cmd = new SqlCommand(insert_prod_pedido, con);
                //@idPEDIDO, @idPRODUCTO, @NOMBREPRODUCTO, @PRECIO, @CANTIDAD, @COSTO
                cmd.Parameters.AddWithValue("@idPEDIDO", new_prod_pedido.Id_Pedido);
                cmd.Parameters.AddWithValue("@idPRODUCTO", new_prod_pedido.Id_Producto);
                cmd.Parameters.AddWithValue("@NOMBREPRODUCTO", new_prod_pedido.Nombre);
                cmd.Parameters.AddWithValue("@PRECIO", new_prod_pedido.Precio);
                cmd.Parameters.AddWithValue("@CANTIDAD", new_prod_pedido.Cantidad);
                cmd.Parameters.AddWithValue("@COSTO", new_prod_pedido.Costo);
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


        public Producto GetProducto(string id)
        {
            try
            {
                List<Producto> productos = new List<Producto>();

                string GET_PRODUCTO = "SELECT p.idPRODUCTOS, p.NOMBRE, p.PRECIO, p.FOTOGRAFIA, 'LICORES', p.FAVORITO, p.DESCRIPCION, p.PROMO, p.IMAGEN FROM PRODUCTOS p WHERE p.idPRODUCTOS = @IDPRODUCTO";

                SqlCommand cmd = new SqlCommand(GET_PRODUCTO, con);

                cmd.Parameters.AddWithValue("@IDPRODUCTO", id);
       
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        //int dds = dr.GetInt32(7);
                        Producto prod = new Producto()
                        {
                            Id = dr[0] != DBNull.Value ? dr.GetInt32(0) : 0,
                            Nombre = dr[1] != DBNull.Value ? dr.GetString(1) : string.Empty,
                            Precio = dr[2] != DBNull.Value ? dr.GetDecimal(2) : 0,
                            Fotografia = dr[3] != DBNull.Value ? dr.GetString(3) : string.Empty,
                            Categoria = dr[4] != DBNull.Value ? dr.GetString(4) : string.Empty,
                            Descripcion = dr[6] != DBNull.Value ? dr.GetString(6) : string.Empty,
                            PROMO = dr[7] != DBNull.Value ? Convert.ToBoolean(dr.GetByte(7)) : false

                        };
                        if (dr[8] != DBNull.Value)
                            prod.Imagen = "http://elchalan.mx/ImagenChalan.ashx?id=" + Convert.ToString(dr.GetInt32(0));

                        else
                            prod.Imagen = string.Empty;
       

                        return prod;
                    }
                 
                }
                return null;
            }
            catch (Exception ex)
            {
                //Log
                string err = ex.ToString();
                return null;
            }
            finally
            {
                con.Close();
            }
        }


        public List<Producto> GetProductosWhere(string condicion)
        {
            string GET_PRODUCTO = "SELECT p.idPRODUCTOS, p.NOMBRE, p.PRECIO, p.FOTOGRAFIA, p.CATEGORIA, p.DESCRIPCION, p.PROMO, p.IMAGEN FROM PRODUCTOS p WHERE p.NOMBRE LIKE @Condicion;";
            try
            {
                List<Producto> productos = new List<Producto>();
                SqlCommand cmd = new SqlCommand(GET_PRODUCTO, con);
                cmd.Parameters.AddWithValue("@Condicion", "%" + condicion + "%");
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        //int dds = dr.GetInt32(7);
                        Producto prod = new Producto()
                        {
                            Id = dr[0] != DBNull.Value ? dr.GetInt32(0) : 0,
                            Nombre = dr[1] != DBNull.Value ? dr.GetString(1) : string.Empty,
                            Precio = dr[2] != DBNull.Value ? dr.GetDecimal(2) : 0,
                            Fotografia = dr[3] != DBNull.Value ? dr.GetString(3) : string.Empty,
                            Categoria = dr[4] != DBNull.Value ? dr.GetString(4) : string.Empty,
                            Descripcion = dr[5] != DBNull.Value ? dr.GetString(5) : string.Empty,
                            PROMO = dr[6] != DBNull.Value ? Convert.ToBoolean(dr.GetByte(6)) : false

                        };
                        if (dr[7] != DBNull.Value)
                            prod.Imagen = "http://elchalan.mx/ImagenChalan.ashx?id=" + Convert.ToString(dr.GetInt32(0));

                        else
                            prod.Imagen = string.Empty;
                        productos.Add(prod);
                    }
                    return productos;
                }
                return null;
            }
            catch (Exception ex)
            {
                //Log
                string err = ex.ToString();
                return null;
            }
            finally
            {
                con.Close();
            }
        }



        //public List<Producto> GetProductosWhere(string condicion)
        //{
        //    try
        //    {
        //        List<Producto> productos = new List<Producto>();


        //        string GET_PRODUCTO = "SELECT p.idPRODUCTOS, p.NOMBRE, p.PRECIO, p.FOTOGRAFIA, p.CATEGORIA, p.FAVORITO, p.DESCRIPCION, p.PROMO, p.IMAGEN FROM PRODUCTOS p WHERE p.NOMBRE like @condicion OR  p.CATEGORIA like @condicion     ";

        //        SqlCommand cmd = new SqlCommand(GET_PRODUCTO, con);

        //        cmd.Parameters.AddWithValue("@condicion", "%" + condicion + "%");
        //        con.Open();
        //        SqlDataReader dr = cmd.ExecuteReader();
        //        if (dr.HasRows)
        //        {
        //            while (dr.Read())
        //            {
        //                //int dds = dr.GetInt32(7);
        //                Producto prod = new Producto()
        //                {
        //                    Id = dr[0] != DBNull.Value ? dr.GetInt32(0) : 0,
        //                    Nombre = dr[1] != DBNull.Value ? dr.GetString(1) : string.Empty,
        //                    Precio = dr[2] != DBNull.Value ? dr.GetDecimal(2) : 0,
        //                    Fotografia = dr[3] != DBNull.Value ? dr.GetString(3) : string.Empty,
        //                    Categoria = dr[4] != DBNull.Value ? dr.GetString(4) : string.Empty,
        //                    Descripcion = dr[6] != DBNull.Value ? dr.GetString(6) : string.Empty,
        //                    PROMO = dr[7] != DBNull.Value ? Convert.ToBoolean(dr.GetByte(7)) : false

        //                };
        //                if (dr[8] != DBNull.Value)
        //                    prod.Imagen = "http://elchalan.mx/ImagenChalan.ashx?id=" + Convert.ToString(dr.GetInt32(0));

        //                else
        //                    prod.Imagen = string.Empty;
        //                productos.Add(prod);
        //            }
        //            return productos;
        //        }
        //        return null;
        //    }
        //    catch (Exception ex)
        //    {
        //        //Log
        //        string err = ex.ToString();
        //        return null;
        //    }
        //    finally
        //    {
        //        con.Close();
        //    }
        //}




        public List<Producto> GetProductos()
        {
            try
            {
                //http://localhost:64661/ImagenChalan.ashx?id=
                List<Producto> productos = new List<Producto>();
                SqlCommand cmd = new SqlCommand(GET_PRODUCTOS, con);
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        //int dds = dr.GetInt32(7);
                        Producto prod = new Producto()
                        {
                            Id = dr[0] != DBNull.Value ? dr.GetInt32(0) : 0,
                            Nombre = dr[1] != DBNull.Value ? dr.GetString(1) : string.Empty,
                            Precio = dr[2] != DBNull.Value ? dr.GetDecimal(2) : 0,
                            Fotografia = dr[3] != DBNull.Value ? dr.GetString(3) : string.Empty,
                            Categoria = dr[4] != DBNull.Value ? dr.GetString(4) : string.Empty,
                            Descripcion = dr[6] != DBNull.Value ? dr.GetString(6) : string.Empty,
                            PROMO = dr[7] != DBNull.Value ? Convert.ToBoolean(dr.GetByte(7)) : false
                        };
                        if (dr[8] != DBNull.Value)
                            prod.Imagen = "http://elchalan.mx/ImagenChalan.ashx?id=" + Convert.ToString(dr.GetInt32(0));

                        
                       
                        else
                            prod.Imagen = string.Empty;

                        //if (dr[9] != DBNull.Value)
                        //    prod.Precio_Simbolico = dr.GetDecimal(9);

                        productos.Add(prod);
                    }
                    return productos;
                }
                return null;
            }
            catch (Exception ex)
            {
                //Log
                string err = ex.ToString();
                return null;
            }
            finally
            {
                con.Close();
            }
        }

        public List<DetallePedido> GetProductosPedido(Int32 idPedido)
        {
            try
            {
                List<DetallePedido> productos = new List<DetallePedido>();
                SqlCommand cmd = new SqlCommand(GET_PRODUCTOS_PEDIDO, con);
                cmd.Parameters.AddWithValue("@IdPedido", idPedido);
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        DetallePedido prod = new DetallePedido()
                        {
                            Id = dr[0] != DBNull.Value ? dr.GetInt32(0) : 0,
                            Id_Pedido = dr[1] != DBNull.Value ? dr.GetInt32(1) : 0,
                            Id_Producto = dr[2] != DBNull.Value ? dr.GetInt32(2) : 0,
                            Nombre = dr[3] != DBNull.Value ? dr.GetString(3) : string.Empty,
                            Precio = dr[4] != DBNull.Value ? dr.GetDecimal(4) : 0,
                            Cantidad = dr[5] != DBNull.Value ? dr.GetInt32(5) : 0
                            
                        };
                        productos.Add(prod);
                    }
                    return productos;
                }
                return null;
            }
            catch (Exception ex)
            {
                //Log
                string err = ex.ToString();
                return null;
            }
            finally
            {
                con.Close();
            }
        }
    }
}