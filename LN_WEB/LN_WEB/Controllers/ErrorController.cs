using System;
using System.Web.Mvc;

public class ErrorController : Controller
{
    private readonly ApplicationDbContext _context;

    public ErrorController()
    {
        _context = new ApplicationDbContext();
    }

    // GET: Error/Reportar
    public ActionResult Reportar()
    {
        return View();
    }

    // POST: Error/Reportar
    [HttpPost]
    [ValidateAntiForgeryToken]
    public ActionResult Reportar([Bind(Include = "Id,Descripcion,Usuario,Tipo")] Error error)
    {
        if (ModelState.IsValid)
        {
            _context.Errores.Add(error);
            _context.SaveChanges();
            return RedirectToAction("Reportar", "Home"); // Redirigir a donde desees
        }

        return View(error);
    }
}