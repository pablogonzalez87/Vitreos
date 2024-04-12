//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Web;
//using System.Web.Mvc;
//using LN_WEB.Models;

//namespace TuProyecto.Controllers
//{
//    public class ErroresController : Controller
//    {
//        //private ApplicationDbContext db = new ApplicationDbContext();

//        // GET: Errores
//        public ActionResult Index()
//        {
//            return View(db.Errores.ToList());
//        }

//        // GET: Errores/Crear
//        public ActionResult Crear()
//        {
//            return View();
//        }

//        [HttpPost]
//        [ValidateAntiForgeryToken]
//        public ActionResult Crear(Error error)
//        {
//            if (ModelState.IsValid)
//            {
//                error.FechaCreacion = DateTime.Now;
//                db.Errores.Add(error);
//                db.SaveChanges();
//                return RedirectToAction("Index");
//            }

//            return View(error);
//        }

//        // Otros métodos de controlador para Editar, Eliminar, etc.
//    }
//}