using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LN_WEB.Entities
{
    public class VidrioEnt
    {
        public long IdVidrio { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public int Cantidad { get; set; }
    }
}