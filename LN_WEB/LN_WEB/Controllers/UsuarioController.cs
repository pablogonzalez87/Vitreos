using Tienda_Vidreos.Entities;
using Tienda_Vidreos.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Tienda_Vidreos.Controllers
{
    public class UsuarioController : Controller
    {
        UsuarioModel modelUsuario = new UsuarioModel();

        [HttpGet]
        public ActionResult ConsultaUsuarios()
        {
            var resp = modelUsuario.ConsultaUsuarios();
            return View(resp);
        }
        [HttpGet]
        public ActionResult Perfil()
        {
            long q = long.Parse(Session["IdUsuario"].ToString());
            var datos = modelUsuario.ConsultaUsuario(q);
            Session["Nombre"] = datos.Nombre;
            ViewBag.Direcciones = modelUsuario;
            return View(datos);
        }







        [HttpGet]
        public ActionResult CambiarEstado(long q)
        {
            UsuarioEnt entidad = new UsuarioEnt();
            entidad.IdUsuario = q;

            var resp = modelUsuario.CambiarEstado(entidad);

            if (resp > 0)
                return RedirectToAction("ConsultaUsuarios", "Usuario");
            else
            {
                ViewBag.MsjPantalla = "No se ha podido actualizar el estado del usuario";
                return View("ConsultaUsuarios");
            }
        }

        [HttpGet]
        public ActionResult Editar(long q)
        {
            var resp = modelUsuario.ConsultaUsuario(q);
            var respRoles = modelUsuario.ConsultaRoles();

            var roles = new List<SelectListItem>();
            foreach (var item in respRoles)
            {
                roles.Add(new SelectListItem { Value = item.IdRol.ToString(), Text = item.NombreRol.ToString() });
            }

            ViewBag.ComboRoles = roles;
            return View(resp);
        }

        [HttpPost]
        public ActionResult EditarUsuario(UsuarioEnt entidad)
        {
            var resp = modelUsuario.EditarUsuario(entidad);

            if (resp > 0)
                return RedirectToAction("ConsultaUsuarios", "Usuario");
            else
            {
                ViewBag.MsjPantalla = "No se ha podido actualizar la información del usuario";
                return View("ConsultaUsuarios");
            }
        }

    }
}