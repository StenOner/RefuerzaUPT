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
            _cuestionario.temaID = 1;
            _cuestionario.duracion = 60;
            _cuestionario.intentos = 3;
            _cuestionario.estado = true;
            //string[] allKeys = Request.Form.AllKeys;
            string[] guids = Request.Form.GetValues("Pregunta.Index");
            try
            {
                _cuestionario.Guardar();
                foreach (string guid in guids)
                {
                    GuardarPregunta(_cuestionario, guid);
                }
                return Redirect("~/Cuestionario");
            }
            catch (Exception)
            {
                return View("~/Views/Cuestionario/AgregarEditar.cshtml", _cuestionario);
            }
        }

        /**
         * 
         * 
         */
        private void GuardarPregunta(Cuestionario _cuestionario, string _guid)
        {
            string preguntaID = $"Pregunta[{_guid}].preguntaID";
            string tipoPreguntaID = $"Pregunta[{_guid}].tipoPreguntaID";
            string enunciadoPregunta = $"Pregunta[{_guid}].enunciadoPregunta";
            Pregunta nuevaPregunta = new Pregunta();
            nuevaPregunta.preguntaID = Convert.ToInt32(Request.Form[preguntaID]);
            nuevaPregunta.cuestionarioID = _cuestionario.cuestionarioID;
            nuevaPregunta.tipoPreguntaID = Convert.ToInt32(Request.Form[tipoPreguntaID]);
            nuevaPregunta.enunciadoPregunta = Request.Form[enunciadoPregunta];
            nuevaPregunta.imagen = null;
            nuevaPregunta.estado = true;
            nuevaPregunta.Guardar();

            GuardarAlternativa(nuevaPregunta, _guid);

        }

        /**
         * 
         * 
         */
        private void GuardarAlternativa(Pregunta _nuevaPregunta, string _guid)
        {
            string alternativaID = $"Pregunta[{_guid}].alternativaID";
            string enunciadoAlternativa = $"Pregunta[{_guid}].enunciadoAlternativa";
            Alternativa nuevaAlternativa = new Alternativa();
            nuevaAlternativa.alternativaID = Convert.ToInt32(Request.Form[alternativaID]);
            nuevaAlternativa.preguntaID = _nuevaPregunta.preguntaID;
            nuevaAlternativa.enunciadoAlternativa = Request.Form[enunciadoAlternativa];
            nuevaAlternativa.respuestaCorrecta = Convert.ToBoolean(Request.Form[alternativaID]);
            nuevaAlternativa.estado = true;
            nuevaAlternativa.Guardar();
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