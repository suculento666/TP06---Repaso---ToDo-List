using Microsoft.AspNetCore.Mvc;
using TP06___Repaso___ToDo_List.Models;

public class AccountController : Controller
{
    public IActionResult Login()
    {
        if (!string.IsNullOrEmpty(HttpContext.Session.GetString("IdUsuario")))
            return RedirectToAction("VerTareas", "Home");
        return View();
    }

    [HttpPost]
    public IActionResult LoginGuardar(string username, string password)
    {
        var usuario = BD.BuscarUsuario(username, password);
        if (usuario != null)
        {
            HttpContext.Session.SetString("IdUsuario", usuario.Id.ToString());
            BD.ActualizarUltimoLogin(usuario.Id);
            return RedirectToAction("VerTareas", "Home");
        }
        ViewBag.Mensaje = "Usuario o contraseña incorrectos.";
        return View("Login");
    }

    public IActionResult CerrarSesion()
    {
        HttpContext.Session.Remove("IdUsuario");
        return RedirectToAction("Login");
    }

    public IActionResult Registro()
    {
        return View();
    }

    [HttpPost]
    public IActionResult RegistroGuardar(Usuario usuario)
    {
        if (BD.ExisteUsername(usuario.Username))
        {
            ViewBag.Mensaje = "El nombre de usuario ya existe.";
            return View("Registro");
        }

        usuario.UltimoLogin = DateTime.Now;
        BD.AgregarUsuario(usuario);
        return RedirectToAction("Login");
    }
}