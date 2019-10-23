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
        private Usuario Usuario = new Usuario();

        [NoLogin]
        public ActionResult Index()
        {
            return View();
        }

        public JsonResult Validar(string _Correo, string _Clave)
        {
            var rm = Usuario.ValidarLogin(_Correo, _Clave);
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