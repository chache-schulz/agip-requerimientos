using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RequerimientosSPS.Models;
using System.IO;

namespace RequerimientosSPS.Controllers
{
    public class BuscadorController : Controller
    {
        private spsrequerimientosEntities db = new spsrequerimientosEntities();
        // GET: Test
        public ActionResult Index()
        {
            ViewBag.analistas = new MultiSelectList(db.analista, "id", "nombre");
            ViewBag.areas = new MultiSelectList(db.area, "id", "descripcion");
            ViewBag.prioridades = new MultiSelectList(db.prioridad, "id", "descripcion");
            ViewBag.origenes = new MultiSelectList(db.origen, "id", "descripcion");
            ViewBag.clases = new MultiSelectList(db.clase, "id", "descripcion");
            ViewBag.impuestos = new MultiSelectList(db.impuesto, "id", "descripcion");
            ViewBag.estados = new MultiSelectList(db.estadoSps, "id", "descripcion");
            ViewBag.severidades = new MultiSelectList(db.severidad, "id", "descripcion");
            ViewBag.vusers = new MultiSelectList(db.sps.Select(s => s.vuser).Distinct());
            ViewBag.referencias = new MultiSelectList(db.sps.Select(s => s.referencia).Distinct());
            ViewBag.tipos = new MultiSelectList(db.sps.Select(s => s.tipo).Distinct());
            ViewBag.caratulas = new MultiSelectList(db.sps.Select(s => s.caratula).Distinct());
            return View();
        }

        [HttpPost]
        public ActionResult Buscar(DateTime? fechaDesde, DateTime? fechaHasta, List<int> analistas, List<string> vusers, List<int> prioridades,
            List<int> areas, List<int> origenes, List<int> clases, List<int> impuestos, List<int> estados, List<int> severidades, 
            List<int> referencias, List<int> tipos, List<int> caratulas)
        {
            IQueryable<sps> resultados = db.sps;

            if (fechaDesde != null) resultados = resultados.Where(sps => sps.fechaOrigen >= fechaDesde);
            if (fechaHasta != null) resultados = resultados.Where(sps => sps.fechaOrigen <= fechaDesde);
            if (analistas != null) resultados = resultados.Join(analistas, sps => sps.analista_Id, id => id, (sps, id) => sps);
            if (vusers != null) resultados = resultados.Where(r => vusers.Contains(r.vuser));






            //if (prioridades != null) resultados = resultados.Join(eventos, inc => inc.id_evento, id => id, (inc, id) => inc);
            //if (areas != null) resultados = resultados.Join(reparticiones, inc => inc.id_reparticion, id => id, (inc, id) => inc);
            //if (origenes != null) resultados = resultados.Join(impuestos, inc => inc.id_impuesto, id => id, (inc, id) => inc);
            //if (clases != null) resultados = resultados.Join(programadores, inc => inc.id_programador, id => id, (inc, id) => inc);
            //if (impuestos != null) resultados = resultados.Join(sistemas, inc => inc.id_sistema, id => id, (inc, id) => inc);
            //if (estados != null) resultados = resultados.Join(errores, inc => inc.id_error, id => id, (inc, id) => inc);
            //if (severidades != null) resultados = resultados.Join(errores, inc => inc.id_error, id => id, (inc, id) => inc);
            //if (referencias != null) resultados = resultados.Join(errores, inc => inc.id_error, id => id, (inc, id) => inc);
            //if (tipos != null) resultados = resultados.Join(errores, inc => inc.id_error, id => id, (inc, id) => inc);
            //if (caratulas != null) resultados = resultados.Join(errores, inc => inc.id_error, id => id, (inc, id) => inc);
            return View(resultados.ToList());
        }

    }
}
