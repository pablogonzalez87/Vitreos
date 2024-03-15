using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LN_WEB.Entities
{
    public class PagosEnt
    {
        public long IdPago { get; set; }
        public long IdUsuario { get; set; }
        public int CantidadArt { get; set; }
        public int SubTotal { get; set; }
        public int Total { get; set; }
        public DateTime Fecha { get; set; }
    }
}