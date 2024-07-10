using LN_API.Entities;
using LN_API.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

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
                var datos = bd.Vidreo.ToList();
                return datos.Select(item => new VidreoEnt
                {
                    IdVidreo = item.IdVidreo,
                    Nombre = item.Nombre,
                    Descripcion = item.Descripcion,
                    Precio = item.Precio,
                    CantidadStock = item.CantidadStock,
                    Imagen = item.Imagen
                }).ToList();
            }
        }

        [HttpGet]
        [Route("api/ConsultarVidreo")]
        public VidreoEnt ConsultarVidreo(long q)
        {
            using (var bd = new Tienda_VidreosEntities())
            {
                var datos = bd.Vidreo.FirstOrDefault(x => x.IdVidreo == q);
                if (datos == null) return null;

                return new VidreoEnt
                {
                    IdVidreo = datos.IdVidreo,
                    Nombre = datos.Nombre,
                    Descripcion = datos.Descripcion,
                    Precio = datos.Precio,
                    CantidadStock = datos.CantidadStock,
                    Imagen = datos.Imagen
                };
            }
        }

        [HttpPost]
        [Route("api/RegistrarVidreo")]
        public long RegistrarVidreo(VidreoEnt entidad)
        {
            using (var bd = new Tienda_VidreosEntities())
            {
                var tabla = new Vidreo
                {
                    Nombre = entidad.Nombre,
                    Descripcion = entidad.Descripcion,
                    Precio = entidad.Precio,
                    Imagen = entidad.Imagen,
                    CantidadStock = entidad.CantidadStock
                };

                bd.Vidreo.Add(tabla);
                bd.SaveChanges();

                return tabla.IdVidreo;
            }
        }

        //[HttpPost]
        //[Route("api/ImagenComprobante")]
        //public long ImagenComprobante(VidreoEnt entidad)
        //{
        //    using (var bd = new Tienda_VidreosEntities())
        //    {
        //        var tabla = new VidreoUsuario
        //        {
        //           ImagenComprobante = entidad.ImagenComprobante,
        //        };
        //        bd.VidreoUsuario.Add(tabla);
        //        bd.SaveChanges();
        //        return tabla.IdVidreo;
        //    }
        //}



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

















        [HttpPut]
        [Route("api/ActualizarRuta")]
        public void ActualizarRuta(VidreoEnt entidad)
        {
            using (var bd = new Tienda_VidreosEntities())
            {
                var datos = bd.Vidreo.FirstOrDefault(x => x.IdVidreo == entidad.IdVidreo);
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
                var datos = bd.Vidreo.FirstOrDefault(x => x.IdVidreo == entidad.IdVidreo);
                if (datos != null)
                {
                    datos.Nombre = entidad.Nombre;
                    datos.Descripcion = entidad.Descripcion;
                    datos.Precio = entidad.Precio;
                    datos.CantidadStock = entidad.CantidadStock;
                    bd.SaveChanges();
                }
                return 0;
            }
        }


    }
}
