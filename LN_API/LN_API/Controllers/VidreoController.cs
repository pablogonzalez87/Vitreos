using LN_API.Entities;
using LN_API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.UI.WebControls;

namespace LN_API.Controllers
{
    public class VidreoController : ApiController
    {
        [HttpGet]
        [Route("api/ConsultaVidreos")]
        public List<VidreoEnt> ConsultaVidreos()
        {
            using (var bd = new Tienda_VidreosEntities())
            {
                var datos = (from x in bd.Vidreo
                             select x).ToList();

                if (datos.Count > 0)
                {
                    List<VidreoEnt> res = new List<VidreoEnt>();
                    foreach (var item in datos)
                    {
                        res.Add(new VidreoEnt
                        {
                            IdVidreo = item.IdVidreo,
                            Nombre = item.Nombre,
                            Descripcion = item.Descripcion,
                            Precio = item.Precio,
                            Imagen = item.Imagen,

                        });
                    }
                    return res;
                }
                else
                {
                    return new List<VidreoEnt>();
                }
            }
        }


        [HttpGet]
        [Route("api/ConsultarVidreo")]
        public VidreoEnt ConsultarVidreo(long q)
        {
            using (var bd = new Tienda_VidreosEntities())
            {
                var datos = (from x in bd.Vidreo
                             where x.IdVidreo == q
                             select x).FirstOrDefault();

                if (datos != null)
                {

                    VidreoEnt resp = new VidreoEnt();
                    resp.IdVidreo = datos.IdVidreo;
                    resp.Nombre = datos.Nombre;
                    resp.Descripcion = datos.Descripcion;
                    resp.Precio = datos.Precio;
                    resp.Imagen = datos.Imagen;

                    return resp;
                }
                return null;
            }
        }



        [HttpGet]
        [Route("api/ConsultaVidreo")]
        public VidreoEnt ConsultaVidreo(long q)
        {
            using (var bd = new Tienda_VidreosEntities())
            {
                var datos = (from x in bd.Vidreo
                             where x.IdVidreo == q
                             select x).FirstOrDefault();

                if (datos != null)
                {
                    VidreoEnt res = new VidreoEnt();
                    res.IdVidreo = datos.IdVidreo;
                    res.Nombre = datos.Nombre;
                    res.Descripcion = datos.Descripcion;
                    res.Precio = datos.Precio;
                    res.Imagen = datos.Imagen;
                    return res;
                }

                return null;
            }
        }


        [HttpPost]
        [Route("api/RegistrarVidreo")]
        public long RegistrarVidreo(VidreoEnt entidad)
        {
            using (var bd = new Tienda_VidreosEntities())
            {
                Vidreo tabla = new Vidreo();
                tabla.Nombre = entidad.Nombre;
                tabla.Descripcion = entidad.Descripcion;
                tabla.Precio = entidad.Precio;
                tabla.Imagen = entidad.Imagen;

                bd.Vidreo.Add(tabla);
                bd.SaveChanges();

                return tabla.IdVidreo;
            }
        }
        [HttpPut]
        [Route("api/ActualizarRuta")]
        public void ActualizarRuta(VidreoEnt entidad)
        {
            using (var bd = new Tienda_VidreosEntities())
            {
                var datos = (from x in bd.Vidreo
                             where x.IdVidreo == entidad.IdVidreo
                             select x).FirstOrDefault();

                if (datos != null)
                {
                    datos.Imagen = entidad.Imagen;
                    bd.SaveChanges();
                }
            }
        }



        [HttpPut]
        [Route("api/ActualizarVidreo")]
        public int ActualizarVidreo(VidreoEnt entidad)
        {
            using (var bd = new Tienda_VidreosEntities())
            {
                var datos = (from x in bd.Vidreo
                             where x.IdVidreo == entidad.IdVidreo
                             select x).FirstOrDefault();

                if (datos != null)
                {
                    datos.Nombre = entidad.Nombre;
                    datos.Descripcion = entidad.Descripcion;
                    datos.Precio = entidad.Precio;
                    bd.SaveChanges();
                }
                return 0;
            }
        }
    }

}