using LN_WEB.Entities;
using LN_WEB.Model;
using LN_WEB.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LN_WEB.Controllers
{
    public class CarritoController : Controller
    {
        CarritoModel model = new CarritoModel();// esto es una instancia 
        VidreoModel modelVidreo = new VidreoModel();
        CarritoModel modelCarrito = new CarritoModel();

        [HttpGet]
        public ActionResult AgregarVidreoCarrito(long q)
        {
            CarritoEnt entidad = new CarritoEnt();
            entidad.FechaCarrito = DateTime.Now;
            entidad.IdVidreo = q;
            entidad.IdUsuario = long.Parse(Session["IdUsuario"].ToString());
            var respuesta = model.AgregarVidreoCarrito(entidad);
            //ActualizarDatosSesion();
            model.AgregarVidreoCarrito(entidad);

            var datos = model.ConsultaVidreoCarrito(long.Parse(Session["IdUsuario"].ToString()));
            Session["CantidadVidreo"] = datos.Count();
            Session["SubTotalVidreo"] = datos.Sum(x => x.Precio);
            Session["TotalVidreo"] = datos.Sum(x => x.Precio) + (datos.Sum(x => x.Precio) * 0.13M);

            return RedirectToAction("Vidrio", "Home");
        }


        //public void ActualizarDatosSesion()
        //{
        //    var datos = modelCarrito.ConsultaVidreoCarrito(long.Parse(Session["IdUsuario"].ToString()));
        //    Session["CantidadVidreo"] = datos.Count();
        //    Session["SubTotalVidreo"] = datos.Sum(x => x.Precio);
        //    Session["TotalVidreo"] = datos.Sum(x => x.Precio) + (datos.Sum(x => x.Precio) * 0.13M);
        //}


        [HttpGet]
        public ActionResult VerCarrito()
        {

            var datos = modelCarrito.ConsultaVidreoCarrito(long.Parse(Session["IdUsuario"].ToString()));
            return View(datos);
        }


        [HttpPost]
        public ActionResult ConfirmarPago()
        {
            CarritoEnt entidad = new CarritoEnt();
            entidad.IdVidreo = (long.Parse(Session["IdUsuario"].ToString()));

            model.PagarVidreoCarrito(entidad);
            return RedirectToAction("Inicio", "Home");
        }
    }
}