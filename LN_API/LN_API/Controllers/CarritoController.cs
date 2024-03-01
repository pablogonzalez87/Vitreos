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

        [HttpGet]
        [Route("api/ConsultarVidreoCarrito")]
        public List<CarritoEnt> ConsultarVidreoCarrito(long q)
        {
            using (var bd = new Tienda_VidreosEntities())
            {
                {
                    var datos = (from x in bd.VidreoCarrito
                                 join y in bd.Vidreo on x.IdVidreo equals y.idVidreo
                                 where x.IdUsuario == q
                                 select new {
                                     x.IdVidreoCarrito,
                                     x.IdUsuario,
                                     x.IdVidreo,
                                     y.Precio
                                 
                                 }).ToList();

                    if (datos.Count > 0)
                    {
                        List<CarritoEnt> res = new List<CarritoEnt> ();
                        foreach (var item in datos)
                        {
                            res.Add(new CarritoEnt
                            {
                                IdVidreoCarrito = item.IdVidreoCarrito,
                                IdUsuario = item.IdUsuario,
                                IdVidreo = item.IdVidreo,
                                Precio  = item.Precio
                            });
                        }
                        return res;
                    }
                    return new List<CarritoEnt>();
                    {
                       
                    }
                }

            }
        }

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


