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

        /**
         * 
         */
        public ActionResult Index()
        {
            return View(objetoCuestionario.Listar());
        }

        /**
         * 
         */
        public ActionResult Ver(int _id = 0)
        {
            return View(objetoCuestionario.Obtener(_id));
        }

        /**
         * 
         */
        public ActionResult ResolverCuestionario(int _id)
        {

            return View(objetoCuestionario.Obtener(_id));
        }

        /**
         * 
         */
        public ActionResult AgregarEditar(int _id = 0)
        {
            return View(
                _id == 0 ? new Cuestionario()
                : objetoCuestionario.Obtener(_id));
        }

        /**
         * 
         */
        public ActionResult Guardar([Bind(Exclude = "Pregunta")]Cuestionario _cuestionario)
        {
            _cuestionario.temaID = 1;
            _cuestionario.estado = true;
            string[] guidPreguntas = Request.Form.GetValues("Pregunta.Index");
            try
            {
                _cuestionario.Guardar();
                //int counterCuestionario = 0;
                //using (IEnumerator<Pregunta> iteradorCuestionario = _cuestionario.Pregunta.GetEnumerator())
                //{
                //    while (iteradorCuestionario.MoveNext())
                //    {
                //        Pregunta actualPregunta = iteradorCuestionario.Current;
                //        GuardarAlternativa(actualPregunta, guids[counterCuestionario]);
                //        counterCuestionario++;
                //    }
                //}
                foreach (string guid in guidPreguntas)
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
         * param @_cuestionario
         * param @_guid
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
            try
            {
                GuardarAlternativa(nuevaPregunta, _guid);
            }
            catch (Exception) { }
        }

        /**
         * param @_nuevaPregunta
         * param @_guid
         */
        private void GuardarAlternativa(Pregunta _nuevaPregunta, string _guid)
        {
            List<string> alternativaIDsEnunciados = new List<string>();
            alternativaIDsEnunciados.AddRange(Request.Form.GetValues($"Pregunta[{_guid}].alternativaID"));
            alternativaIDsEnunciados.AddRange(Request.Form.GetValues($"Pregunta[{_guid}].enunciadoAlternativa"));

            List<string> respuestaCorrecta = Request.Form.GetValues($"Pregunta[{_guid}].respuestaCorrecta").ToList();
            for (int i = 0; i < respuestaCorrecta.Count; i++)
            {
                alternativaIDsEnunciados.Add(respuestaCorrecta[i]);
                if (respuestaCorrecta[i] == "true")
                    i++;
            }

            int n = 3;

            for (int i = 0; i < (alternativaIDsEnunciados.Count / n); i++)
            {
                Alternativa nuevaAlternativa = new Alternativa();
                nuevaAlternativa.alternativaID = Convert.ToInt32(alternativaIDsEnunciados[i]);
                nuevaAlternativa.preguntaID = _nuevaPregunta.preguntaID;
                nuevaAlternativa.enunciadoAlternativa = alternativaIDsEnunciados[(alternativaIDsEnunciados.Count / n) + i];
                nuevaAlternativa.respuestaCorrecta = Convert.ToBoolean(alternativaIDsEnunciados[(alternativaIDsEnunciados.Count / n) * 2 + i]);
                nuevaAlternativa.estado = true;
                nuevaAlternativa.Guardar();
            }
        }

        /**
         * 
         */
        public ActionResult Eliminar(int _id)
        {
            //objetoCuestionario.cuestionarioID = _id;
            //objetoCuestionario.Eliminar();
            if (_id > 0)
            {
                Cuestionario cuestionario = new Cuestionario().Obtener(_id);
                cuestionario.estado = false;
                cuestionario.Guardar();
            }
            return Redirect("~/Cuestionario");
        }

        /**
         * 
         */
        public ActionResult AgregarPregunta()
        {
            return PartialView("_PreguntaCuestionario", new Pregunta());
        }

        /**
         * 
         */
        public ActionResult EliminarPregunta(int _id = 0)
        {
            //if (_id > 0)
            //{
            //    objetoPregunta.preguntaID = _id;
            //    objetoPregunta.Eliminar();
            //}
            if (_id > 0)
            {
                Pregunta pregunta = new Pregunta().Obtener(_id);
                pregunta.estado = false;
                pregunta.Guardar();
            }
            return Json("Eliminado");
        }

        /**
         * 
         */
        public ActionResult AgregarAlternativa()
        {
            return PartialView("_AlternativaPregunta", new Alternativa());
        }

        /**
         * 
         */
        public ActionResult EliminarAlternativa(int _id = 0)
        {
            //if (_id > 0)
            //{
            //    objetoAlternativa.alternativaID = _id;
            //    objetoAlternativa.Eliminar();
            //}
            if (_id > 0)
            {
                Alternativa alternativa = new Alternativa().Obtener(_id);
                alternativa.estado = false;
                alternativa.Guardar();
            }
            return Json("Eliminado");
        }
    }
}