using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RefuerzaUPT.Models;
using RefuerzaUPT.Filters;

namespace RefuerzaUPT.Controllers
{
    public class LoginController : Controller
    {
        private Usuario objetoUsuario = new Usuario();

        [NoLogin]
        public ActionResult Index()
        {
            return View();
        }

        public JsonResult Validar(string _correo, string _clave)
        {
            var rm = objetoUsuario.ValidarLogin(_correo, _clave);
            if (rm.response)
            {
                rm.href = Url.Content("/Cuestionario");
            }
            return Json(rm);
        }

        public ActionResult Logout()
        {
            SessionHelper.DestroyUserSession();
            return Redirect("~/Login");
        }
    }
}