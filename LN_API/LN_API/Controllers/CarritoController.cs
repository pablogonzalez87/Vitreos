using LN_API.Entities;
using LN_API.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace LN_API.Controllers
{
    public class CarritoController : ApiController
    {
        [HttpGet]
        [Route("api/ConsultaVidreoCarrito")]
        public IHttpActionResult ConsultaVidreoCarrito(long q)
        {
            using (var bd = new Tienda_VidreosEntities())
            {
                var datos = (from x in bd.VidreoCarrito
                             join y in bd.Vidreo on x.IdVidreo equals y.IdVidreo
                             where x.IdUsuario == q
                             select new
                             {
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
                            Precio = item.Precio,
                            Nombre = item.Nombre,
                            Impuesto = item.Precio * 0.13M
                        });
                    }
                    return Ok(res);
                }
                return Ok(new List<CarritoEnt>());
            }
        }

        [HttpGet]
        [Route("api/ConsultaVidreoUsuario")]
        public IHttpActionResult ConsultaVidreoUsuario(long q)
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
                                 x.FechaPago,
                                 y.Nombre
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
                            PrecioPago = item.PrecioPago,
                            Nombre = item.Nombre,
                            FechaPago = item.FechaPago,
                            Impuesto = item.PrecioPago * 0.13M
                        });
                    }
                    return Ok(res);
                }
                return Ok(new List<CarritoEnt>());
            }
        }

        [HttpPost]
        [Route("api/AgregarVidreoCarrito")]
        public IHttpActionResult AgregarVidreoCarrito(CarritoEnt entidad)
        {
            using (var bd = new Tienda_VidreosEntities())
            {
                var vidrio = bd.Vidreo.FirstOrDefault(v => v.IdVidreo == entidad.IdVidreo);

                if (vidrio == null)
                {
                    return NotFound();
                }

                if (vidrio.CantidadStock <= 0)
                {
                    return BadRequest("No hay suficiente stock para este producto.");
                }

                VidreoCarrito tabla = new VidreoCarrito
                {
                    IdUsuario = entidad.IdUsuario,
                    IdVidreo = entidad.IdVidreo,
                    FechaCarrito = entidad.FechaCarrito,
                    Cantidad = 1
                };

                bd.VidreoCarrito.Add(tabla);
                vidrio.CantidadStock--;
                bd.SaveChanges();

                return Ok();
            }
        }

        [HttpDelete]
        [Route("api/RemoverVidreoCarrito")]
        public IHttpActionResult RemoverVidreoCarrito(long q)
        {
            using (var bd = new Tienda_VidreosEntities())
            {
                var carrito = (from cc in bd.VidreoCarrito
                               where cc.IdVidreoCarrito == q
                               select cc).FirstOrDefault();

                if (carrito == null)
                {
                    return NotFound();
                }

                var vidrio = bd.Vidreo.FirstOrDefault(v => v.IdVidreo == carrito.IdVidreo);
                if (vidrio != null)
                {
                    vidrio.CantidadStock += carrito.Cantidad;
                }

                bd.VidreoCarrito.Remove(carrito);
                bd.SaveChanges();

                return Ok();
            }
        }

        [HttpPost]
        [Route("api/ImagenComprobante")]
        public async Task<IHttpActionResult> ImagenComprobante()
        {
            if (!Request.Content.IsMimeMultipartContent())
            {
                return BadRequest("El tipo de contenido no es multipart/form-data");
            }

            var provider = new MultipartMemoryStreamProvider();
            await Request.Content.ReadAsMultipartAsync(provider);

            var file = provider.Contents.FirstOrDefault();
            if (file == null)
            {
                return BadRequest("No se ha subido ningún archivo");
            }

            var fileName = file.Headers.ContentDisposition.FileName.Trim('\"');
            var buffer = await file.ReadAsByteArrayAsync();
            var filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Uploads", fileName);

            Directory.CreateDirectory(Path.GetDirectoryName(filePath));
            File.WriteAllBytes(filePath, buffer);

            using (var bd = new Tienda_VidreosEntities())
            {
                var nuevaEntrada = new VidreoUsuario
                {
                    ImagenComprobante = filePath
                };

                bd.VidreoUsuario.Add(nuevaEntrada);
                bd.SaveChanges();

                return Ok(nuevaEntrada.IdVidreoUsuario); // Asegúrate de devolver el ID correcto
            }
        }

        [HttpPost]
        [Route("api/PagarVidreoCarrito")]
        public IHttpActionResult PagarVidreoCarrito(CarritoEnt entidad)
        {
            using (var bd = new Tienda_VidreosEntities())
            {
                var datos = (from cc in bd.VidreoCarrito
                             join c in bd.Vidreo on cc.IdVidreo equals c.IdVidreo
                             where cc.IdUsuario == entidad.IdUsuario
                             select new
                             {
                                 cc.IdVidreo,
                                 cc.IdUsuario,
                                 cc.Cantidad,
                                 c.Precio,
                                 c.CantidadStock,
                             }).ToList();

                if (datos.Count > 0)
                {
                    foreach (var item in datos)
                    {
                        if (item.CantidadStock < item.Cantidad)
                        {
                            return BadRequest("No hay suficiente stock para completar la compra.");
                        }

                        VidreoUsuario cu = new VidreoUsuario
                        {
                            IdVidreo = item.IdVidreo,
                            IdUsuario = item.IdUsuario,
                            FechaPago = DateTime.Now,
                            PrecioPago = item.Precio,
                        };

                        bd.VidreoUsuario.Add(cu);

                        var vidrio = bd.Vidreo.FirstOrDefault(v => v.IdVidreo == item.IdVidreo);
                        if (vidrio != null)
                        {
                            vidrio.CantidadStock -= item.Cantidad;
                        }
                    }

                    var carritoActual = (from cc in bd.VidreoCarrito
                                         where cc.IdUsuario == entidad.IdUsuario
                                         select cc).ToList();

                    foreach (var item in carritoActual)
                    {
                        bd.VidreoCarrito.Remove(item);
                    }

                    bd.SaveChanges();
                    return Ok();
                }

                return BadRequest("No hay artículos en el carrito para pagar.");
            }
        }
    }
}
