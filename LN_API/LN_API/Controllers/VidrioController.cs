using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using LN_API.Entities;
using LN_API.Models;

namespace LN_API.Controllers
{
    public class VidrioController : ApiController
    {

        [HttpPost]
        [Route("api/CrearVidrio")]
        [AllowAnonymous]
        public int CrearVidrio(VidrioEnt entidad)
        {
            using (var bd = new Tienda_VidreosEntities())
            {
                Vidrio tabla = new Vidrio();
                tabla.Nombre = entidad.Nombre;
                tabla.Descripcion = entidad.Descripcion;
                tabla.Cantidad = entidad.Cantidad;

                bd.Vidrio.Add(tabla);
                return bd.SaveChanges();
            }
        }

        [HttpGet]
        [Route("api/ConsultaVidrio")]
        public List<VidrioEnt> ConsultaVidrio()
        {
            using (var bd = new Tienda_VidreosEntities())
            {
                var datos = (from x in bd.Vidrio
                             select x).ToList();

                if (datos.Count > 0)
                {
                    var resp = new List<VidrioEnt>();
                    foreach (var item in datos)
                    {
                        resp.Add(new VidrioEnt
                        {
                            Nombre = item.Nombre,
                            Descripcion = item.Descripcion,
                            Cantidad = item.Cantidad
                        });
                    }
                    return resp;
                }
                else
                {
                    return new List<VidrioEnt>();
                }
            }
        }

        [HttpPut]
        [Route("api/EditarVidrio")]
        public int EditarVidrio(VidrioEnt entidad)
        {
            using (var bd = new Tienda_VidreosEntities())
            {
                var datos = (from x in bd.Vidrio
                             where x.IdVidrio == entidad.IdVidrio
                             select x).FirstOrDefault();

                if (datos != null)
                {
                    datos.Nombre = entidad.Nombre;
                    datos.Descripcion = entidad.Descripcion;
                    datos.Cantidad = entidad.Cantidad;
                    return bd.SaveChanges();
                }

                return 0;
            }
        }

        [HttpDelete]
        [Route("api/EliminarVidrio")]
        public int EliminarVidrio(VidrioEnt entidad)
        {
            using (var bd = new Tienda_VidreosEntities())
            {
                var datos = (from x in bd.Vidrio
                             where x.IdVidrio == entidad.IdVidrio
                             select x).FirstOrDefault();

                if (datos != null)
                {
                    bd.Vidrio.Remove(datos);
                    return bd.SaveChanges();
                }

                return 0;
            }
        }


    }
}
