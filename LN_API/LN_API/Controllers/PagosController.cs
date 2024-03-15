using LN_API.Entities;
using LN_API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace LN_API.Controllers
{
    public class PagosController : ApiController
    {
        [HttpPost]
        [Route("api/AgregarPago")]
        public int AgregarPago(PagosEnt entidad)
        {
            using (var bd = new Tienda_VidreosEntities())
            {
                Pagos tabla = new Pagos();
                tabla.IdPago = entidad.IdPago;
                tabla.IdUsuario = entidad.IdUsuario;
                tabla.CantidadArt = entidad.CantidadArt;
                tabla.SubTotal = entidad.SubTotal;
                tabla.Total = entidad.Total;
                tabla.Fecha = entidad.Fecha;

                bd.Pagos.Add(tabla);
                return bd.SaveChanges();
            }

        }

    }
}
