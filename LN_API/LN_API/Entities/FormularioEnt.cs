using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LN_API.Entities
{
    public class FormularioEnt
    {
        public long IdFormulario { get; set; }
        public long IdUsuario { get; set; }
        public string CorreoElectronico { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
    }
}