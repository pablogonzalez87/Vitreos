using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Tienda_Vidreos.Models;
using LN_WEB.Models;
using Tienda_Vidreos.Entities;
using LN_WEB.Entities;

namespace LN_WEB.Controllers
{
    public class VidrioController : Controller
    {
        VidrioModel model = new VidrioModel();

        [HttpGet]
        public ActionResult ConsultaUsuarios()
        {
            var resp = model.ConsultaVidrio();
            return View(resp);
        }
        [HttpGet]
        public ActionResult Perfil()
        {
            return View();
        }

        [HttpPost]
        public ActionResult EditarVidrio(VidrioEnt entidad)
        {
            var resp = model.EditarVidrio(entidad);

            if (resp > 0)
                return RedirectToAction("ConsultaVidrio", "Vidrio");
            else
            {
                ViewBag.MsjPantalla = "No se ha podido actualizar la información del vidrio";
                return View("ConsultaVidrio");
            }
        }

        [HttpDelete]
        public ActionResult EliminarVidrio(VidrioEnt entidad)
        {
            var resp = model.EliminarVidrio(entidad);

            if (resp > 0)
                return RedirectToAction("EliminarVidrio", "Vidrio");
            else
            {
                ViewBag.MsjPantalla = "No se ha podido eliminar la información del vidrio";
                return View("ConsultaVidrio");
            }
        }
    }
}