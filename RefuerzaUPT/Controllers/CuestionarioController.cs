using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RefuerzaUPT.Models;

namespace RefuerzaUPT.Controllers
{
    public class CuestionarioController : Controller
    {
        private Cuestionario objCuestionario = new Cuestionario();

        /**
         * 
         * 
         */
        public ActionResult Index()
        {
            return View(objCuestionario.Listar());
        }

        /**
         * 
         * 
         */
        public ActionResult Ver(int _id)
        {
            return View(objCuestionario.Obtener(_id));
        }

        /**
         * 
         * 
         * 
         * 
         */
        public ActionResult ResolverCuestionario(int _id)
        {

            return View(objCuestionario.Obtener(_id));
        }

        /**
         *
         *
         */
        public ActionResult AgregarEditar(int _id)
        {
            return View(
                _id == 0 ? new Cuestionario()
                : objCuestionario.Obtener(_id));
        }

        /**
         * 
         * 
         * 
         * 
         */
        public ActionResult Guardar(Cuestionario _cuestionario)
        {
            if (ModelState.IsValid)
            {
                _cuestionario.Guardar();
                return Redirect("~/Cuestionario");
            }
            else
            {
                return View("~/Views/Cuestionario/AgregarEditar.cshtml", _cuestionario);
            }
        }

        /**
         * 
         * 
         * 
         * 
         */
        public ActionResult Eliminar(int _id)
        {
            objCuestionario.cuestionarioID = _id;
            objCuestionario.Eliminar();
            return Redirect("~/Cuestionario");
        }
    }
}