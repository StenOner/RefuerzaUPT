using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RefuerzaUPT.Models;
using RefuerzaUPT.Filters;

namespace RefuerzaUPT.Controllers
{
    [Autenticado]
    public class CuestionarioController : Controller
    {
        private Cuestionario objetoCuestionario = new Cuestionario();

        /**
         * 
         */
        public ActionResult Index()
        {
            Session["Tema"] = null;
            ViewBag.misCursos = new Curso().ListarMisCursos(SessionHelper.GetUser());
            ViewBag.cursos = new Curso().Listar();
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
        public ActionResult AgregarEditar(int _id = 0, int _cursoID = 0)
        {
            ViewBag.temas = new Tema().ListarporCurso(_cursoID);
            return View(
                _id == 0 ? new Cuestionario()
                : objetoCuestionario.Obtener(_id));
        }

        /**
         * 
         */
        public ActionResult Intento(int _id = 0)
        {
            if (_id == 0)
                return Redirect("~/Cuestionario");
            return View(objetoCuestionario.Obtener(_id));
        }

        /**
         * 
         */
        public ActionResult GuardarIntento(Cuestionario _cuestionario)
        {
            string[] guidPreguntas = Request.Form.GetValues("Pregunta.Index");
            IntentoCuestionario nuevoIntentoCuestionario = new IntentoCuestionario
            {
                cuestionarioID = _cuestionario.cuestionarioID,
                usuarioID = SessionHelper.GetUser(),
                nota = 0,
                tiempoResolucion = _cuestionario.duracion - Convert.ToInt32(Request.Form["tiempoRestante"]),
                estado = true
            };
            nuevoIntentoCuestionario.Guardar();
            try
            {
                foreach (string guid in guidPreguntas)
                {
                    GuardarRespuesta(nuevoIntentoCuestionario, guid);
                }
            }catch (Exception) { }
            return Redirect("~/Cuestionario");
        }

        /**
         * 
         */
        public void GuardarRespuesta(IntentoCuestionario _nuevoIntentoCuestionario, string _guid)
        {
            string preguntaID = $"Pregunta[{_guid}].preguntaID";
            List<string> alternativaIDsEnunciados = new List<string>();
            alternativaIDsEnunciados.AddRange(Request.Form.GetValues($"Pregunta[{_guid}].alternativaID"));

            List<string> respuestaCorrecta = Request.Form.GetValues($"Pregunta[{_guid}].respuestaCorrecta").ToList();
            for (int i = 0; i < respuestaCorrecta.Count; i++)
            {
                if (respuestaCorrecta[i] == "true")
                    alternativaIDsEnunciados.Add(respuestaCorrecta[i]);
            }
            Respuesta nuevaRespuesta = new Respuesta()
            {
                cuestionarioID = _nuevoIntentoCuestionario.cuestionarioID,
                intentoCuestionarioID = _nuevoIntentoCuestionario.intentoCuestionarioID,
                preguntaID = Convert.ToInt32(Request.Form[preguntaID]),
                alternativaID = 0,
                usuarioID = _nuevoIntentoCuestionario.usuarioID,
                estado = true
            };
            nuevaRespuesta.Guardar();
        }

        /**
         * 
         */
        public ActionResult Guardar([Bind(Exclude = "Pregunta")]Cuestionario _cuestionario)
        {
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
         * 
         */
        private void GuardarPregunta(Cuestionario _cuestionario, string _guid)
        {
            string preguntaID = $"Pregunta[{_guid}].preguntaID";
            string tipoPreguntaID = $"Pregunta[{_guid}].tipoPreguntaID";
            string enunciadoPregunta = $"Pregunta[{_guid}].enunciadoPregunta";
            Pregunta nuevaPregunta = new Pregunta
            {
                preguntaID = Convert.ToInt32(Request.Form[preguntaID]),
                cuestionarioID = _cuestionario.cuestionarioID,
                tipoPreguntaID = Convert.ToInt32(Request.Form[tipoPreguntaID]),
                enunciadoPregunta = Request.Form[enunciadoPregunta],
                imagen = null,
                estado = true
            };
            nuevaPregunta.Guardar();
            try
            {
                GuardarAlternativa(nuevaPregunta, _guid);
            }
            catch (Exception) { }
        }

        /**
         * 
         */
        public void GuardarAlternativa(Pregunta _nuevaPregunta, string _guid)
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

            //lista unidimensional con id, enunciado y respuesta
            int n = 3;
            for (int i = 0; i < (alternativaIDsEnunciados.Count / n); i++)
            {
                Alternativa nuevaAlternativa = new Alternativa
                {
                    alternativaID = Convert.ToInt32(alternativaIDsEnunciados[i]),
                    preguntaID = _nuevaPregunta.preguntaID,
                    enunciadoAlternativa = alternativaIDsEnunciados[(alternativaIDsEnunciados.Count / n) + i],
                    respuestaCorrecta = Convert.ToBoolean(alternativaIDsEnunciados[(alternativaIDsEnunciados.Count / n) * 2 + i]),
                    estado = true
                };
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

        /**
        * 
        */
        public ActionResult GuardarTemaEnSession(int _temaID)
        {
            Session["Tema"] = _temaID;
            return Json("temaID: " + _temaID);
        }
    }
}