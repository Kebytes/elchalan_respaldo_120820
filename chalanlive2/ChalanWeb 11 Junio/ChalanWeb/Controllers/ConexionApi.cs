using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using ChalanWeb.Models;
using System.Diagnostics;
using Newtonsoft.Json;
using System.Configuration;
using System.Text;

namespace ChalanWeb.Controllers
{
    public class ConexionApi
    {
        private static readonly string urlbase = "http://apichalanprueba.developmxhost.com/";
        static string MerchantId = ConfigurationManager.AppSettings["merchant_id"];
        static string OPURL = ConfigurationManager.AppSettings["PruebasOP"];
        static string API_KEY = ConfigurationManager.AppSettings["api_key"];
        static Uri urlOpenPay = new Uri(OPURL +"/v1"+ "/" + MerchantId + "/");

        public async Task<List<string>> ActualizarClienteToken(string cliente, string tarjeta)
        {
            List<string> resultados = new List<string>();
            try
            {
                HttpClient client = new HttpClient();
                var request = new HttpRequestMessage(HttpMethod.Put, new Uri(urlbase + "api/Cliente/updateCliToken"));
                request.Headers.Add("ClienteUpdate", cliente);
                request.Headers.Add("tarjeta", tarjeta);

                var response = await client.SendAsync(request);
                string Json = response.Content.ReadAsStringAsync().Result;

                Mensaje mensaje = JsonConvert.DeserializeObject<Mensaje>(Json);

                if (mensaje?.Titulo != null)
                {
                    List<string> lista = new List<string>();
                    string respuesta = JsonConvert.SerializeObject(mensaje);
                    lista.Add(respuesta);
                    lista.Add(Json);
                    return lista;
                }

                else
                {
                    List<string> lista = new List<string>();
                    Mensaje mens = new Mensaje { Titulo = "OK", Contenido = "OK" };
                    string x = JsonConvert.SerializeObject(mens);
                    lista.Add(x);
                    lista.Add(Json);
                    return lista;
                }



                //if (Conversion.TryParseJSON(Json))
                //{
                //    //string error = JsonConvert.SerializeObject(Json);
                //    resultados.Add(Json);
                //    return resultados;
                //}

                //else
                //{
                //    Mensaje mens = new Mensaje { Titulo = "OK", Contenido = "OK" };
                //    string ok = JsonConvert.SerializeObject(mens);
                //    resultados.Add(ok);
                //    resultados.Add(Json);
                //    return resultados;
                //}

                //Mensaje mensaje = JsonConvert.DeserializeObject<Mensaje>(Json);

                //if (mensaje?.Titulo != null)
                //{
                //    string error = JsonConvert.SerializeObject(mensaje);
                //    resultados.Add(error);
                //    return resultados;
                //}
                    
                //Application.Current.Properties["Cliente"] = Json;
                //Debug.WriteLine(Json);
                //return new Mensaje { Titulo = "OK", Contenido = "OK" };
                //Mensaje mens = new Mensaje { Titulo = "OK", Contenido = "OK" };
                //string ok = JsonConvert.SerializeObject(mens);
                //resultados.Add(ok);
                //resultados.Add(Json);
                //return resultados;
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Error al hacer el put del cliente: " + ex.ToString());
                Mensaje x = new Mensaje { Titulo = "Advertencia", Contenido = "Ha ocurrido un error, intentalo más tarde." };
                string error = JsonConvert.SerializeObject(x);
                resultados.Add(error);
                return resultados;
            }
        }

        public async Task<string> EliminarTarjetaOP(string idTarjeta, string TOKENOP)
        {
            string tarjetaOP = "";
            HttpClient clientOP = new HttpClient();
            string authUserData = String.Format("{0}:{1}",API_KEY,"");
            string authHeaderVal = Convert.ToBase64String(Encoding.UTF8.GetBytes(authUserData));

            try
            {
                var request = new HttpRequestMessage(HttpMethod.Delete, new Uri(urlOpenPay + "customers/" + TOKENOP + "/cards/" + idTarjeta));


                clientOP.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", authHeaderVal);

                var response = await clientOP.SendAsync(request);
                string Json = response.Content.ReadAsStringAsync().Result;


                //if (!response.IsSuccessStatusCode)
                //    return Json;


                tarjetaOP = Json;

                Debug.WriteLine(Json);
                return tarjetaOP;
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Error al hacer el put del cliente: " + ex.ToString());
                return tarjetaOP;
            }
        }

        public async Task<List<TarjetaOP>> ObtenerTarjetasOP(string TokenOP)
        {
            List<TarjetaOP> tarjetaOP;
            HttpClient clientOP = new HttpClient();
            string authUserData = String.Format("{0}:{1}", API_KEY, "");
            string authHeaderVal = Convert.ToBase64String(Encoding.UTF8.GetBytes(authUserData));

            try
            {
                var request = new HttpRequestMessage(HttpMethod.Get, new Uri(urlOpenPay + "customers/" + TokenOP + "/cards"));

                clientOP.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", authHeaderVal);

                var response = await clientOP.SendAsync(request);
                string Json = response.Content.ReadAsStringAsync().Result;

                if (!response.IsSuccessStatusCode)
                    return null;

                tarjetaOP = JsonConvert.DeserializeObject<List<TarjetaOP>>(Json);

                Debug.WriteLine(Json);
                return tarjetaOP;
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Error al hacer el put del cliente: " + ex.ToString());
                return null;
            }
        }

        public async Task<List<string>> HacerPedido(string json, string deviceSessionID, string tarjeta)
        {
            string Json = "";
            try
            {
                HttpClient client = new HttpClient();
                //antigua forma
                //var httpContent = new StringContent(json, Encoding.UTF8, "application/json");
                //var response = await client.PostAsync(urlbase + "Pedidos/newPedido", httpContent);

                Cliente cliente = new Cliente();

                //string chida = "http://localhost:57824/";

                //nueva forma
                var request = new HttpRequestMessage(HttpMethod.Post, new Uri(urlbase + "api/Pedidos/newPedido/"));
                request.Headers.Add("sesion", deviceSessionID);
                request.Headers.Add("tarjeta", tarjeta);
                request.Content = new StringContent(json, Encoding.UTF8, "application/json");
                var response = await client.SendAsync(request);

                Json = response.Content.ReadAsStringAsync().Result;

                Mensaje mensaje = JsonConvert.DeserializeObject<Mensaje>(Json);

                if (mensaje?.Titulo != null)
                {
                    List<string> lista = new List<string>();
                    string respuesta = JsonConvert.SerializeObject(mensaje);
                    lista.Add(respuesta);
                    return lista;
                }
                    
                else
                {
                    List<string> lista = new List<string>();
                    Mensaje mens = new Mensaje { Titulo = "OK", Contenido = "OK" };
                    string x = JsonConvert.SerializeObject(mens);
                    lista.Add(x);
                    lista.Add(Json);
                    return lista;
                }

                //cliente = JsonConvert.DeserializeObject<Cliente>(Json);

                ////Llenar objeto pedido
                //Pedido pedido = new Pedido();
                //pedido.Id = 0;
                //pedido.Id_Cliente = cliente.Id;
                //pedido.Nombre_Cliente = cliente.Nombre;
                //pedido.Telefono_Cliente = cliente.Telefono;
                //pedido.Id_Listado = cliente.Id_Lista;
                //pedido.Nombre_Listado = cliente.Nombre_Listado;
                //pedido.Detalles = new List<DetallePedido>();

                ////Application.Current.Properties["Carrito"] = JsonConvert.SerializeObject(pedido);

                ////Application.Current.Properties["Cliente"] = Json;
                ////await Application.Current.SavePropertiesAsync();
                ////Debug.WriteLine(Json);
                //return new Mensaje { Titulo = "OK", Contenido = "OK" };
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Error al hacer el post de pedidos: " + ex.ToString());

                try
                {
                    Mensaje mensaje = JsonConvert.DeserializeObject<Mensaje>(Json);
                    if(mensaje != null)
                        mensaje = new Mensaje { Titulo = "Advertencia", Contenido = "Ha ocurrido un error, intentalo más tarde." };
                    List<string> lista = new List<string>();
                    lista.Add(JsonConvert.SerializeObject(mensaje));
                    return lista;
                }
                catch (Exception ex2)
                {
                    List<string> lista = new List<string>();
                    Mensaje mensaje = new Mensaje{ Titulo = "Advertencia", Contenido = "Ha ocurrido un error, intentalo más tarde." };
                    lista.Add(JsonConvert.SerializeObject(mensaje));
                    return lista;
                }
            }
        }

        

        public static async Task<List<string>> CancelarPedido(int idpedido, int idcliente, string tokenPago)
        {
            try
            {
                List<string> resultado = new List<string>();
                string chida = "http://localhost:57824/";
                HttpClient client = new HttpClient();
                var request = new HttpRequestMessage(HttpMethod.Put, new Uri(urlbase + "api/pedidos")); //urlbase
                request.Headers.Add("IdPedido", idpedido.ToString());
                request.Headers.Add("IdCli", idcliente.ToString());
                request.Headers.Add("tokenPago", tokenPago);


                Cliente cliente = new Cliente();

                var response = await client.SendAsync(request);
                string Json = response.Content.ReadAsStringAsync().Result;
                cliente = JsonConvert.DeserializeObject<Cliente>(Json);

                Mensaje mensaje = new Mensaje { Titulo = "OK", Contenido = "OK" };
                resultado.Add(JsonConvert.SerializeObject(mensaje));
                resultado.Add(Json);
                //Application.Current.Properties["Cliente"] = Json;
                //await Application.Current.SavePropertiesAsync();
                //Debug.WriteLine(Json);
                //return true;
                return resultado;
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Error al hacer al cancelar pedidos: " + ex.ToString());
                List<string> resultado = new List<string>();
                Mensaje mensaje = new Mensaje { Titulo = "Advertencia", Contenido = "Ha ocurrido un error, intentalo más tarde." };
                resultado.Add(JsonConvert.SerializeObject(mensaje));
                //return false;
                return resultado;
            }
        }
    }
}