using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using TP06___Repaso___ToDo_List.Models;


public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

   public IActionResult Index()
    {
        return View();
    }
    public IActionResult VerTareas()
    {
        string idUsuarioStr = HttpContext.Session.GetString("IdUsuario");
        if (string.IsNullOrEmpty(idUsuarioStr))
            return RedirectToAction("Login", "Account");

        int idUsuario = int.Parse(idUsuarioStr);
        var tareas = BD.DevolverTareas(idUsuario);
        return View(tareas);
    }

    public IActionResult CrearTarea()
    {
        string idUsuarioStr = HttpContext.Session.GetString("IdUsuario");
        if (string.IsNullOrEmpty(idUsuarioStr))
            return RedirectToAction("Login", "Account");

        return View();
    }

    [HttpPost]
    public IActionResult CrearTareaGuardar(Tarea tarea)
    {
        string idUsuarioStr = HttpContext.Session.GetString("IdUsuario");
        if (string.IsNullOrEmpty(idUsuarioStr))
            return RedirectToAction("Login", "Account");

        tarea.IdUsuario = int.Parse(idUsuarioStr);
        BD.CrearTarea(tarea);
        return RedirectToAction("VerTareas");
    }

    public IActionResult ModificarTarea(int id)
    {
        string idUsuarioStr = HttpContext.Session.GetString("IdUsuario");
        if (string.IsNullOrEmpty(idUsuarioStr))
            return RedirectToAction("Login", "Account");

        var tarea = BD.DevolverTarea(id);
        return View(tarea);
    }

    [HttpPost]
    public IActionResult ModificarTareaGuardar(Tarea tarea)
    {
        string idUsuarioStr = HttpContext.Session.GetString("IdUsuario");
        if (string.IsNullOrEmpty(idUsuarioStr))
            return RedirectToAction("Login", "Account");

        tarea.IdUsuario = int.Parse(idUsuarioStr); 
        BD.EditarTarea(tarea);
        return RedirectToAction("VerTareas");
    }

    public IActionResult EliminarTarea(int id)
    {
        string idUsuarioStr = HttpContext.Session.GetString("IdUsuario");
        if (string.IsNullOrEmpty(idUsuarioStr))
            return RedirectToAction("Login", "Account");

        BD.EliminarTarea(id);
        return RedirectToAction("VerTareas");
    }

    public IActionResult FinalizarTarea(int id)
    {
        string idUsuarioStr = HttpContext.Session.GetString("IdUsuario");
        if (string.IsNullOrEmpty(idUsuarioStr))
            return RedirectToAction("Login", "Account");

        BD.FinalizarTarea(id);
        return RedirectToAction("VerTareas");
    }
}