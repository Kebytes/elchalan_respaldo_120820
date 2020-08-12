using ChalanWeb.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace ChalanWeb.Controllers
{
    public class Tarjeta
    {
        public static async Task/*<List<string>>*/ AgregarTarjeta(Cliente cliente, CreditCard card)
        {
            cliente.Pedidos = null;
            cliente.Direcciones = null;
            string clienteNuevo = JsonConvert.SerializeObject(cliente);
            string tarjeta = "{\"card_number\":\"" + card.NumeroTarjeta + "\",\"holder_name\":\"" + card.Dueño + "\",\"expiration_year\":\"" + card.Expira_Anio + "\",\"expiration_month\":\"" + card.Expira_Mes + "\",\"cvv2\":\"" + card.CVV + "\"}";

            //List<string> respuesta = await ConexionApi.ActualizarClienteToken(clienteNuevo, tarjeta);

            //Mensaje mensaje = JsonConvert.DeserializeObject<Mensaje>(respuesta[0]);

            //return respuesta;

            //if (mensaje.Titulo.Equals("OK"))
            //{
                
            //}
            //else
            //{

            //}
        }

        //public async Task EliminarTarjetaOP(string idTarjeta, string token)
        //{
           
        //    string respuesta = await ConexionApi.EliminarTarjetaOP(idTarjeta, token);
        //    if (!String.IsNullOrEmpty(respuesta))
        //    {
        //        ErrorOP error = JsonConvert.DeserializeObject<ErrorOP>(respuesta);
        //        //await DisplayAlert("ERROR", error.DescripcionError, "Aceptar");
        //    }
        //    else
        //    {

        //    }
        //}

        //public async Task<List<TarjetaOP>> ObtenerTarjetas(Cliente cliente)
        //{
        //    List<TarjetaOP> tarjetaOPs = await ConexionApi.ObtenerTarjetasOP(cliente.TOKENOP);

        //    return tarjetaOPs;
        //}
    }
}