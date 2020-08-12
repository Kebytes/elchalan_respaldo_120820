using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ChalanWeb.Models
{
    public class DiaDeServicio
    {
        public int Id { get; set; }
        public DateTime Fecha { get; set; }
        public TimeSpan HoraDeInicio { get; set; }
        public TimeSpan HoraDeFinalizacion { get; set; }
        public bool Habil { get; set; }
    }
}