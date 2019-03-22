using Educacion.BL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Educacion.WebAdmin.Controllers
{
    public class NotasDetalleController : Controller
    {
        NotasBL _notasBL;
        MateriasBL _materiasBL;

        public NotasDetalleController()
        {
            _notasBL = new NotasBL();
            _materiasBL = new MateriasBL();
        }

        public ActionResult Index(int id)
        {
            var notas = _notasBL.ObtenerNotas(id);
            notas .ListadeNotasDetalle = _notasBL.ObtenerNotasDetalle(id);

            return View(notas);
        }


        public ActionResult Crear(int id)
        {
            var nuevaNotasDetalle = new NotasDetalle();
             nuevaNotasDetalle.NotaId = id ;

            var materias= _materiasBL.ObtenerMaterias();
            ViewBag.MateriasId = new SelectList(materias , "Id");

            return View(nuevaNotasDetalle);

        }


        [HttpPost]
        public ActionResult Crear(NotasDetalle NotasDetalle)
        {
            if (ModelState.IsValid)
            {
                if (NotasDetalle.MateriaId == 0)
                {
                    ModelState.AddModelError("Materia", "Seleccione una MAteria");
                    return View(NotasDetalle);
                }

                _notasBL.GuardarNotasDetalle(NotasDetalle);
                return RedirectToAction("Index", new { id = NotasDetalle.NotaId  });
            }

            var materias = _materiasBL.ObtenerMaterias();
            ViewBag.MateriaId = new SelectList(materias , "Id");

            return View(NotasDetalle);
        }
        public ActionResult Eliminar(int id)
        {
            var NotasDetalle = _notasBL.ObtenerNotasDetallePorId(id);

            return View(NotasDetalle);
        }

        [HttpPost]
        public ActionResult Eliminar(NotasDetalle NotasDetalle)
        {
            _notasBL.EliminarNotasDetalle(NotasDetalle.Id);

            return RedirectToAction("Index", new { id = NotasDetalle.NotaId  });
        }

    }
}