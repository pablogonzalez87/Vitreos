using LN_WEB.Entities;
using LN_WEB.Model;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace LN_WEB.Controllers
{
    public class VidreoController : Controller
    {
        VidreoModel modelVidreos = new VidreoModel();
        [HttpGet]
        public ActionResult ConsultarMantVidreos()
        {
            var vidreos = modelVidreos.ConsultaVidreos();
            return View(vidreos);

        }
        [HttpGet]
        public ActionResult Agregar()
        {

            return View();
        }

        [HttpPost]
        public ActionResult Agregar(VidreoEnt entidad, HttpPostedFileBase ImagenVidreo)
        {
            entidad.Imagen = string.Empty;
            var resp = modelVidreos.RegistrarVidreo(entidad);

            string extension = Path.GetExtension(Path.GetFileName(ImagenVidreo.FileName));
            string ruta = @"C:\Vitreos\Vitreos\LN_WEB\LN_WEB\images\" + resp + extension;
            ImagenVidreo.SaveAs(ruta);
            entidad.Imagen = "/images/" + resp + extension;
            entidad.IdVidreo = resp;
            modelVidreos.ActualizarRuta(entidad);



            return RedirectToAction("ConsultarMantVidreos", "Vidreo");
        }


        public ActionResult SubirComprobante()
        {
            return View();
        }
        [HttpPost]
        public ActionResult SubirComprobante(HttpPostedFileBase imagenComprobante)
        {
            try
            {
                if (imagenComprobante != null && imagenComprobante.ContentLength > 0)
                {
                    string extension = Path.GetExtension(imagenComprobante.FileName);
                    string fileName = Path.GetFileNameWithoutExtension(imagenComprobante.FileName) + "_" + DateTime.Now.ToString("yyyyMMddHHmmss") + extension;
                    string path = Path.Combine(Server.MapPath("~/ImagenComprobante"), fileName);

                    var directory = Path.GetDirectoryName(path);
                    if (!Directory.Exists(directory))
                    {
                        Directory.CreateDirectory(directory);
                    }
                    imagenComprobante.SaveAs(path);
                    ViewBag.Message = "Comprobante subido exitosamente.";
                }
                else
                {
                    ViewBag.Message = "Por favor seleccione un archivo.";
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al subir la imagen del comprobante: " + ex.Message);
            }

            return View();
        }
   


       [HttpGet]
        public ActionResult Editar(long q)
        {
            var datos = modelVidreos.ConsultarVidreo(q);
            return View(datos);
        }

        [HttpPost]
        public ActionResult Editar(VidreoEnt entidad, HttpPostedFileBase ImagenVidreo)
        {


            entidad.Imagen = string.Empty;
            modelVidreos.ActualizarVidreo(entidad);

            string extension = Path.GetExtension(Path.GetFileName(ImagenVidreo.FileName));
            string ruta = @"C:\Vitreos\Vitreos\LN_WEB\LN_WEB\images\" + entidad.IdVidreo + extension;
            ImagenVidreo.SaveAs(ruta);


            entidad.Imagen = "/images/" + entidad.IdVidreo + extension;
            modelVidreos.ActualizarRuta(entidad);


            return RedirectToAction("ConsultarMantVidreos", "Vidreo");
        }
    }
}


