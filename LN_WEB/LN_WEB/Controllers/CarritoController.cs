using LN_WEB.Entities;
using LN_WEB.Model;
using LN_WEB.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LN_WEB.Controllers
{
    public class CarritoController : Controller
    {
        CarritoModel model = new CarritoModel(); // instancia
        VidreoModel modelVidreo = new VidreoModel();
        CarritoModel modelCarrito = new CarritoModel();

        [HttpGet]
        public ActionResult AgregarVidreoCarrito(long q)
        {
            CarritoEnt entidad = new CarritoEnt
            {
                FechaCarrito = DateTime.Now,
                IdVidreo = q,
                IdUsuario = long.Parse(Session["IdUsuario"].ToString())
            };

            var respuesta = model.AgregarVidreoCarrito(entidad);
            ActualizarDatosSesion();

            return RedirectToAction("Vidrio", "Home");
        }

        [HttpGet]
        public ActionResult Tarjeta()
        {
            var datos = model.ConsultaVidreoCarrito(long.Parse(Session["IdUsuario"].ToString()));
            ActualizarDatosSesion();
            return View(datos);
        }

        [HttpGet]
        public ActionResult RemoverVidreoCarrito(long q)
        {
            var respuesta = model.RemoverVidreoCarrito(q);
            ActualizarDatosSesion();

            if (respuesta > 0)
            {
                return RedirectToAction("VerCarrito");
            }
            else
            {

                var datos = model.ConsultaVidreoCarrito(long.Parse(Session["IdUsuario"].ToString()));
                return View("VerCarrito", datos);
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


        [HttpPost]
        public ActionResult ConfirmarPago()
        {
            CarritoEnt entidad = new CarritoEnt
            {
                IdUsuario = long.Parse(Session["IdUsuario"].ToString())
            };
            ActualizarDatosSesion();
            model.PagarVidreoCarrito(entidad);
            return RedirectToAction("Inicio", "Home");
        }

        [HttpGet]
        public ActionResult Factura()
        {
            var resp = model.ConsultaVidreoUsuario(long.Parse(Session["IdUsuario"].ToString()));
            return View(resp);
        }
    }
}
