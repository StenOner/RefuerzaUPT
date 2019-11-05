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
        private Cuestionario Cuestionario = new Cuestionario();
        private Pregunta Pregunta = new Pregunta();
        private Alternativa Alternativa = new Alternativa();

        /**
         * 
         * 
         * 
         */
        public ActionResult Index()
        {
            return View(Cuestionario.Listar());
        }

        /**
         *
         * 
         * 
         */
        public ActionResult Ver(int _id)
        {
            return View(Cuestionario.Obtener(_id));
        }

        /**
         * 
         * 
         * 
         * 
         */
        public ActionResult ResolverCuestionario(int _id)
        {

            return View(Cuestionario.Obtener(_id));
        }

        /**
         *
         * 
         *
         */
        public ActionResult AgregarEditar(int _id)
        {
            return View(
                _id == 0 ? new Cuestionario()
                : Cuestionario.Obtener(_id));
        }

        /**
         * 
         * 
         * 
         * 
         */
        public ActionResult Guardar(Cuestionario _cuestionario)
        {
            string[] allKeys = Request.Form.AllKeys;
            //obtener valores de forms de igual nombre
            string[] values = Request.Form.GetValues("Pregunta1");
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
            Cuestionario.cuestionarioID = _id;
            Cuestionario.Eliminar();
            return Redirect("~/Cuestionario");
        }

        /**
         * 
         * 
         * 
         * 
         */
        public ActionResult AgregarPregunta()
        {
            return PartialView("_PreguntaCuestionario", new Pregunta());
        }

        /**
         * 
         * 
         * 
         * 
         */
        public ActionResult EliminarPregunta(int _id = 0)
        {
            if (_id > 0)
            {
                Pregunta.preguntaID = _id;
                Pregunta.Eliminar();
            }
            return Json("Eliminado");
        }

        /**
         * 
         * 
         * 
         * 
         */
        public ActionResult AgregarAlternativa()
        {
            return PartialView("_AlternativaPregunta", new Alternativa());
        }

        /**
         * 
         * 
         * 
         * 
         */
        public ActionResult EliminarAlternativa(int _id = 0)
        {
            if (_id > 0)
            {
                Alternativa.alternativaID = _id;
                Alternativa.Eliminar();
            }
            return Json("Eliminado");
        }
    }
}