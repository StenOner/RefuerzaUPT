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
        private Cuestionario objetoCuestionario = new Cuestionario();
        private Pregunta objetoPregunta = new Pregunta();
        private Alternativa objetoAlternativa = new Alternativa();

        /**
         * 
         * 
         * 
         */
        public ActionResult Index()
        {
            return View(objetoCuestionario.Listar());
        }

        /**
         *
         * 
         * 
         */
        public ActionResult Ver(int _id)
        {
            return View(objetoCuestionario.Obtener(_id));
        }

        /**
         * 
         * 
         * 
         * 
         */
        public ActionResult ResolverCuestionario(int _id)
        {

            return View(objetoCuestionario.Obtener(_id));
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
                : objetoCuestionario.Obtener(_id));
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
            objetoCuestionario.cuestionarioID = _id;
            objetoCuestionario.Eliminar();
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
                objetoPregunta.preguntaID = _id;
                objetoPregunta.Eliminar();
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
                objetoAlternativa.alternativaID = _id;
                objetoAlternativa.Eliminar();
            }
            return Json("Eliminado");
        }
    }
}