using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LN_WEB.Entities
{
    public class DatosTarjetaEnt
    {
        public long IdTarjeta { get; set; }
        public string NombreTitular { get; set; }
        public int NumeroTarjeta { get; set; }
        public string FechaExpiracion { get; set; }
        public short Codigo { get; set; }
    }
}