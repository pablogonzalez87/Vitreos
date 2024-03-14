using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LN_API.Entities
{
    public class CarritoEnt
    {
        public  long IdVidreoCarrito {  get; set; }
        public long IdUsuario { get; set; }
        public long IdVidreo { get; set; }
        public decimal Impuesto { get; set; }
        public decimal Precio { get; set; }
        public DateTime FechaCarrito { get; set; }
        public string Nombre { get; set; }
    }
}