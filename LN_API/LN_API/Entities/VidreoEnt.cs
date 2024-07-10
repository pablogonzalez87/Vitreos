using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LN_API.Entities
{
    public class VidreoEnt
    {
        public long IdVidreo { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public decimal Precio { get; set; }
        public string Imagen { get; set; }
        public int CantidadStock { get; set; }
        public string ImagenComprobante { get; set; }

        

    }

}