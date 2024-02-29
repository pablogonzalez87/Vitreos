using LN_WEB.Entities;
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
      
        [HttpGet]
        public ActionResult AgregarVidreoCarrito(long q)
        {
            CarritoEnt entidad = new CarritoEnt();
            entidad.FechaCarrito = DateTime.Now;
            entidad.IdVidreo = q;
            entidad.IdUsuario = long.Parse(Session["IdUsuario"].ToString());

          model.AgregarVidreoCarrito(entidad);


            return RedirectToAction("Vidrio", "Home");
        }
    }
}