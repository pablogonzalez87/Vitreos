using LN_WEB.Entities;
using LN_WEB.Model;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
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


            entidad.Imagen ="/images/" + entidad.IdVidreo + extension;
            modelVidreos.ActualizarRuta(entidad);


            return RedirectToAction("ConsultarMantVidreos", "Vidreo");
        }
    }
}


