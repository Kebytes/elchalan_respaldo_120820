using ChalanWeb.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ChalanWeb.Controllers
{
    public class Conversion
    {
        public static bool TryParseJSON(string json)
        {
            try
            {
                
                return true;
            }
            catch
            {
                return false;
            }
        }

        public static bool ParsearJSON(string json)
        {
            try
            {
                Mensaje mensaje = JsonConvert.DeserializeObject<Mensaje>(json);
                if (mensaje?.Titulo != null)
                    return false;
                return false;
            } catch(JsonReaderException ex)
            {
                return false;
            }

            catch(Exception ex)
            {
                return false;
            }
        }
    }
}