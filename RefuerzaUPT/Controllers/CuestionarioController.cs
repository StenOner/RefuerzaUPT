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
            if (_id > 0)
                ViewBag.temas = new Tema().ListarporCurso(objetoCuestionario.Obtener(_id).Tema.cursoID);
            else
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
            try
            {
                IntentoCuestionario nuevoIntentoCuestionario = new IntentoCuestionario
                {
                    cuestionarioID = _cuestionario.cuestionarioID,
                    usuarioID = SessionHelper.GetUser(),
                    nota = 0,
                    tiempoResolucion = _cuestionario.duracion - Convert.ToInt32(Request.Form["tiempoRestante"]),
                    estado = true
                };
                nuevoIntentoCuestionario.Guardar();
                foreach (string guid in guidPreguntas)
                {
                    GuardarRespuesta(nuevoIntentoCuestionario, guid);
                }
                nuevoIntentoCuestionario.nota = CalcularNota(nuevoIntentoCuestionario);
                nuevoIntentoCuestionario.Guardar();
            }catch (Exception) { }
            return Redirect("~/Cuestionario");
        }

        /**
         * 
         */
        public double CalcularNota(IntentoCuestionario _nuevoIntentoCuestionario)
        {
            double nota = 0;
            double puntajePregunta = 0;
            double notaMaxima = 20;
            puntajePregunta = CalcularPuntajePregunta(_nuevoIntentoCuestionario, notaMaxima);
            IntentoCuestionario intentoCuestionario = new IntentoCuestionario().Obtener(_nuevoIntentoCuestionario.intentoCuestionarioID);
            using (IEnumerator<Respuesta> iteradorRespuesta = intentoCuestionario.Respuesta.GetEnumerator())
            {
                while (iteradorRespuesta.MoveNext())
                {
                    double puntajeAlternativa = 0;
                    Respuesta respuesta = iteradorRespuesta.Current;
                    puntajeAlternativa = (puntajePregunta / (new Respuesta().ListarPorPregunta(respuesta.preguntaID, respuesta.intentoCuestionarioID).Count));
                    Alternativa alternativa = new Alternativa().Obtener((int)respuesta.alternativaID);
                    if (respuesta.alternativaRespuesta == alternativa.respuestaCorrecta)
                        nota += puntajeAlternativa;
                    else
                        nota -= puntajeAlternativa;
                }
            }
            if (nota < 0)
                nota = 0;
            return Math.Round(nota, 2);
        }

        /**
         * 
         */
        private double CalcularPuntajePregunta(IntentoCuestionario _nuevoIntentoCuestionario, double _notaMaxima)
        {
            int contadorPreguntas = 0;
            Cuestionario cuestionario = new Cuestionario().Obtener(_nuevoIntentoCuestionario.cuestionarioID);
            using (IEnumerator<Pregunta> iteradorPregunta = cuestionario.Pregunta.GetEnumerator())
            {
                while (iteradorPregunta.MoveNext())
                {
                    Pregunta pregunta = iteradorPregunta.Current;
                    var listaRespuesta = new Respuesta().ListarPorPregunta(pregunta.preguntaID, _nuevoIntentoCuestionario.intentoCuestionarioID);
                    if (listaRespuesta != null && listaRespuesta.Count > 0)
                    {
                        contadorPreguntas++;
                    }
                }
                return (_notaMaxima / contadorPreguntas);
            }
        }

        /**
         * 
         */
        public void GuardarRespuesta(IntentoCuestionario _nuevoIntentoCuestionario, string _guid)
        {
            string preguntaID = $"Pregunta[{_guid}].preguntaID";
            List<string> alternativaIDsRespuestas = new List<string>();
            alternativaIDsRespuestas.AddRange(Request.Form.GetValues($"Pregunta[{_guid}].alternativaID"));

            List<string> respuestaCorrecta = Request.Form.GetValues($"Pregunta[{_guid}].respuestaCorrecta").ToList();
            for (int i = 0; i < respuestaCorrecta.Count; i++)
            {
                alternativaIDsRespuestas.Add(respuestaCorrecta[i]);
                if (respuestaCorrecta[i] == "true")
                    i++;
            }

            //lista unidimensional con id y respuesta
            int n = 2;
            for (int i = 0; i < (alternativaIDsRespuestas.Count / n); i++)
            {
                Respuesta nuevaRespuesta = new Respuesta()
                {
                    cuestionarioID = _nuevoIntentoCuestionario.cuestionarioID,
                    intentoCuestionarioID = _nuevoIntentoCuestionario.intentoCuestionarioID,
                    preguntaID = Convert.ToInt32(Request.Form[preguntaID]),
                    alternativaID = Convert.ToInt32(alternativaIDsRespuestas[i]),
                    alternativaRespuesta = Convert.ToBoolean(alternativaIDsRespuestas[(alternativaIDsRespuestas.Count / n) + i]),
                    usuarioID = _nuevoIntentoCuestionario.usuarioID,
                    estado = true
                };
                nuevaRespuesta.Guardar();
            }
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
        public ActionResult GuardarTemporizadorEnSession(string _temporizador)
        {
            Session["Temporizador"] = _temporizador;
            return Json("Temporizador: " + _temporizador);
        }
    }
}