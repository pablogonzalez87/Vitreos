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
            ActualizarDatosSesion();

            return RedirectToAction("Vidrio", "Home");
        }



        [HttpGet]
        public ActionResult RemoverVidreoCarrito(long q)
        {
            var respuesta = model.RemoverVidreoCarrito(q);
            ActualizarDatosSesion();

            if (respuesta > 0)
            {
                return RedirectToAction("VerCarrito", "Carrito");
            }
            else
            {
                ViewBag.MsjPantalla = "No se pudo remover  de su carrito";
                return View("VerCarrito");
            }
        }



        public void ActualizarDatosSesion()
        {
            var datos = model.ConsultaVidreoCarrito(long.Parse(Session["IdUsuario"].ToString()));
            Session["CantidadVidreos"] = datos.Count();
            Session["SubTotalVidreos"] = datos.Sum(x => x.Precio);
            Session["TotalVidreos"] = datos.Sum(x => x.Precio) + (datos.Sum(x => x.Precio) * 0.13M);
        }


        [HttpGet]
        public ActionResult VerCarrito()
        {

            var datos = model.ConsultaVidreoCarrito(long.Parse(Session["IdUsuario"].ToString()));
            ActualizarDatosSesion();

            return View(datos);
        }


        [HttpPost]
        public ActionResult ConfirmarPago()
        {
            CarritoEnt entidad = new CarritoEnt();
            entidad.IdUsuario = (long.Parse(Session["IdUsuario"].ToString()));
            ActualizarDatosSesion();
            model.PagarVidreoCarrito(entidad);
            return RedirectToAction("Inicio", "Home");
        }
    }
}