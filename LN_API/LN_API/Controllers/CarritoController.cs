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
        [Route("api/ConsultaVidreoCarrito")]
        public List<CarritoEnt> ConsultaVidreoCarrito(long q)
        {
            using (var bd = new Tienda_VidreosEntities())
            {            
                    var datos = (from x in bd.VidreoCarrito
                                 join y in bd.Vidreo on x.IdVidreo equals y.IdVidreo
                                 where x.IdUsuario == q
                                 select new {
                                     x.IdVidreoCarrito,
                                     x.IdUsuario,
                                     x.IdVidreo,
                                     y.Precio,
                                     y.Nombre                      
                                 }).ToList();

                    if (datos.Count > 0)
                    {
                        List<CarritoEnt> res = new List<CarritoEnt>();
                        foreach (var item in datos)
                        {
                            res.Add(new CarritoEnt
                            {
                                IdVidreoCarrito = item.IdVidreoCarrito,
                                IdUsuario = item.IdUsuario,
                                IdVidreo = item.IdVidreo,
                                Precio  = item.Precio,
                                Nombre = item.Nombre,
                                Impuesto = item.Precio * 0.13M
                            });
                        }
                        return res;
                    }
                    return new List<CarritoEnt>();
                    {
                       
                    }
                }

            }
        



        [HttpGet]
        [Route("api/ConsultaVidreoUsuario")]
        public List<CarritoEnt> ConsultaVidreoUsuario(long q)
        {
            using (var bd = new Tienda_VidreosEntities())
            {
                var datos = (from x in bd.VidreoUsuario
                             join y in bd.Vidreo on x.IdVidreo equals y.IdVidreo
                             where x.IdUsuario == q
                             select new
                             {
                                 x.IdVidreoUsuario,
                                 x.IdVidreo,
                                 x.IdUsuario,
                                 x.PrecioPago,
                                 y.Nombre,

                             }).ToList();

                if (datos.Count > 0)
                {
                    List<CarritoEnt> res = new List<CarritoEnt>();
                    foreach (var item in datos)
                    {
                        res.Add(new CarritoEnt
                        {
                            IdVidreoCarrito = item.IdVidreoUsuario,
                            IdVidreo = item.IdVidreo,
                            IdUsuario = item.IdUsuario,
                            Precio = item.PrecioPago,
                            Nombre = item.Nombre,
                            Impuesto = item.PrecioPago * 0.13M
                        });
                    }

                    return res;
                }

                return new List<CarritoEnt>();
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


        //remover


        [HttpPost]
        [Route("api/PagarVidreoCarrito")]
        public int PagarVidreoCarrito(CarritoEnt entidad)
        {
            using (var bd = new Tienda_VidreosEntities())
            {
                //Busco el carrito para pasarlo a la tabla de usuarios
                var datos = (from cc in bd.VidreoCarrito
                             join c in bd.Vidreo on cc.IdVidreo equals c.IdVidreo
                             where cc.IdUsuario == entidad.IdUsuario
                             select new
                             {
                                 cc.IdVidreo,
                                 cc.IdUsuario,
                                 c.Precio
                             }).ToList();

                if (datos.Count > 0)
                {
                    foreach (var item in datos)
                    {
                        VidreoUsuario cu = new VidreoUsuario();
                        cu.IdVidreo = item.IdVidreo;
                        cu.IdUsuario = item.IdUsuario;
                        cu.FechaPago = DateTime.Now;
                        cu.PrecioPago = item.Precio;
                        bd.VidreoUsuario.Add(cu);
                    }

                    //Busco el carrito para limpiarlo
                    var carritoActual = (from cc in bd.VidreoCarrito
                                         where cc.IdUsuario == entidad.IdUsuario
                                         select cc).ToList();

                    foreach (var item in carritoActual)
                    {
                        bd.VidreoCarrito.Remove(item);
                    }

                    return bd.SaveChanges();
                }

                return 0;
            }
        }
    }
}



