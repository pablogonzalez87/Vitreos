using Tienda_Vidreos.Entities;
using LN_WEB.Entities;
using LN_WEB.Models;
using Tienda_Vidreos.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LN_WEB.Model;
using LN_WEB.Models;
using static System.Collections.Specialized.BitVector32;

namespace Tienda_Vidreos.Controllers
{
    public class HomeController : Controller
    {
        UsuarioModel model = new UsuarioModel();
        VidreoModel modelVidreos = new VidreoModel();
        CarritoModel modelCarrito = new CarritoModel();
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult IniciarSesion(UsuarioEnt entidad)
        {
            var resp = model.IniciarSesion(entidad);

            if (resp != null)
            {
                Session["IdUsuario"] = resp.IdUsuario;
                Session["CorreoUsuario"] = resp.CorreoElectronico;
                Session["NombreUsuario"] = resp.Nombre;
                Session["NombreRolUsuario"] = resp.NombreRol;
                Session["RolUsuario"] = resp.IdRol;
                Session["Token"] = resp.Token;
               

                var datos = modelCarrito.ConsultarVidreoCarrito(long.Parse(Session["IdUsuario"].ToString()));
                Session["CantidadVidreos"] = datos.Count();
                Session["subTotalVidreos"] = datos.Sum(x => x.Precio);
                return RedirectToAction("Inicio", "Home");
            }
            else
            {
                ViewBag.MsjPantalla = "No se ha podido validar su información";
                return View("Login");
            }
        }



        [HttpGet]
        public ActionResult Registro()
        {
            return View();
        }



        [HttpPost]
        public ActionResult RegistrarUsuario(UsuarioEnt entidad)
        {
            entidad.IdRol = 2;
            entidad.Estado = true;

            var resp = model.RegistrarUsuario(entidad);

            if (resp > 0)
                return RedirectToAction("Login", "Home");
            else
            {
                ViewBag.MsjPantalla = "No se ha podido registrar su información";
                return View("Registro");
            }
        }



        [HttpGet]
        public ActionResult Recuperar()
        {
            return View();
        }

        [HttpPost]
        public ActionResult RecuperarClave(UsuarioEnt entidad)
        {
            var resp = model.RecuperarClave(entidad);

            if (resp)
                return RedirectToAction("Login", "Home");
            else
            {
                ViewBag.MsjPantalla = "No se ha podido recuperar su acceso";
                return View("Recuperar");
            }

        }

        [HttpPost]
        public ActionResult ConsultaVidrio(List<VidrioEnt> entidad)
        {
            var resp = model2.ConsultaVidrio(entidad);

            if (resp != null)
                return RedirectToAction("Vidrio", "Home");
            else
            {
                ViewBag.MsjPantalla = "No se ha podido cargar la informacion";
                return View("Registro");
            }
        }

        [HttpGet]
        public ActionResult Inicio()
        {

            return View();
        }

        [HttpGet]
        public ActionResult Contacto()
        {
            return View();
        }

        [HttpGet]
        public ActionResult AgregarMenu()
        {
            return View();
        }

       

        [HttpGet]
        public ActionResult Carrito()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Reportes()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Vidrio()
        {
            var datos = modelCarrito.ConsultarVidreoCarrito(long.Parse(Session["IdUsuario"].ToString()));
            Session["CantidadVidreo"] = datos.Count();
            Session["SubTotalVidreo"] = datos.Sum(x => x.Precio);
            Session["TotalVidreo"] = datos.Sum(x  => x.Precio) + (datos.Sum(x => x.Precio) * 0.13M);
            var vidreos = modelVidreos.ConsultaVidreos();
            return View(vidreos);
        }



        [HttpGet]
        public ActionResult CerrarSesion()
        {
            Session.Clear();
            return RedirectToAction("Login", "Home");
        }



        [HttpGet]
        public ActionResult Cambiar()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CambiarClave(UsuarioEnt entidad)
        {
            entidad.CorreoElectronico = Session["CorreoUsuario"].ToString();
            entidad.IdUsuario = long.Parse(Session["IdUsuario"].ToString());
            var respValidarClave = model.IniciarSesion(entidad);

            if (respValidarClave == null)
            {
                ViewBag.MsjPantalla = "La actual no coincide con su registro en la base de datos";
                return View("Cambiar");
            }

            if (entidad.ContrasennaNueva != entidad.ConfirmarContrasennaNueva)
            {
                ViewBag.MsjPantalla = "Las nueva contraseña no coincide con su confirmación";
                return View("Cambiar");
            }

            var respCambiarClave = model.CambiarClave(entidad);

            if (respCambiarClave > 0)
                return RedirectToAction("Inicio", "Home");
            else
            {
                ViewBag.MsjPantalla = "No se ha podido cambiar su contraseña actual";
                return View("Cambiar");
            }

        }

    }
}