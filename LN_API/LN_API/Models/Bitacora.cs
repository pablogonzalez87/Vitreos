//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace LN_API.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Bitacora
    {
        public long IdBitacora { get; set; }
        public System.DateTime FechaHora { get; set; }
        public string Mensaje { get; set; }
        public string Origen { get; set; }
        public long IdUsuario { get; set; }
        public string DireccionIP { get; set; }
    
        public virtual Usuario Usuario { get; set; }
    }
}
