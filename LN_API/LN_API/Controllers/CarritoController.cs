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
    public class CarritoController : ApiController
    {

        [HttpPost]
        [Route("api/AgregarVidreoCarrito")]
        public int AgregarVidreoCarrito(CarritoEnt entidad)
        {
            using (var bd = new Tienda_VidreosEntities())
            {
    //var datos = (from x in bd.Usuario
    //                         select x).ToList();
                VidreoCarrito tabla = new VidreoCarrito();
                tabla.IdUsuario = entidad.IdUsuario;
                tabla.IdVidreo = entidad.IdVidreo;
                tabla.FechaCarrito = entidad.FechaCarrito;


                bd.VidreoCarrito.Add(tabla);
                return bd.SaveChanges();
            }
           
        }
    }
}

