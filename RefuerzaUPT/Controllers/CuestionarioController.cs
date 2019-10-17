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
        public ActionResult Ver(int id)
        {
            return View(objCuestionario.Obtener(id));
        }

        /**
         *
         *
         */
        public ActionResult AgregarEditar(int id)
        {
            return View(
                id == 0 ? new Cuestionario()
                : objCuestionario.Obtener(id));
        }

        /**
         * 
         * 
         * 
         * 
         */
        public ActionResult Guardar(Cuestionario cuestionario)
        {
            if (ModelState.IsValid)
            {
                cuestionario.Guardar();
                return Redirect("~/Cuestionario");
            }
            else
            {
                return View("~/Views/Cuestionario/AgregarEditar.cshtml", cuestionario);
            }
        }

        /**
         * 
         * 
         * 
         * 
         */
        public ActionResult Eliminar(int id)
        {
            objCuestionario.cuestionarioID = id;
            objCuestionario.Eliminar();
            return Redirect("~/Cuestionario");
        }
    }
}