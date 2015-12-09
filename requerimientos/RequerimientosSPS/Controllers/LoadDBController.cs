using RequerimientosSPS.Models;
using RequerimientosSPS.Helpers;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using System.Text;

namespace RequerimientosSPS.Controllers
{
    public class LoadDBController : Controller
    {
        private spsrequerimientosEntities db = new spsrequerimientosEntities();

        private string loadTable(string tableName, string fieldName, int valueIdx)
        {
            var file = System.IO.File.OpenRead(Path.Combine(Server.MapPath("~/archbase/"), "Query-Todas-sps.csv"));
            var reader = new StreamReader(file, Encoding.Default);
            for (int i = 1; i <= 7; i++)
            {
                /* Salto las primeras 8 lineas que no tienen informacion relevante */
                reader.ReadLine();
            }
            while (!reader.EndOfStream)
            {
                var line = reader.ReadLine();
                var values = line.Split(';');
                var data = values[valueIdx].Trim();
                string sql = String.Format("INSERT {0} ({1}) " +
                                                " SELECT '{2}'  " +
                                                " WHERE '{2}' NOT IN (SELECT {1} FROM {0}) AND '{2}' <> '-'"
                                , tableName, fieldName, data);
                db.Database.ExecuteSqlCommand(sql);
            }
            return "ok";
        }
        public string LoadAll()
        {
            this.loadTable("severidad", "descripcion", IndicesArchivoSps.INDICE_SEVERIDAD);
            this.loadTable("estadoSPS", "descripcion", IndicesArchivoSps.INDICE_ESTADO);
            this.loadTable("clase", "descripcion", IndicesArchivoSps.INDICE_CLASE);
            this.loadTable("origen", "descripcion", IndicesArchivoSps.INDICE_ORIGEN);
            this.loadTable("prioridad", "descripcion", IndicesArchivoSps.INDICE_PRIORIDAD);
            this.loadTable("area", "descripcion", IndicesArchivoSps.INDICE_AREA);
            this.loadTable("impuesto", "descripcion", IndicesArchivoSps.INDICE_IMPUESTO);
            return this.loadTable("analista", "nombre", IndicesArchivoSps.INDICE_ANALISTA);
             
        }
        public string loadSPS()
        {
            var file = System.IO.File.OpenRead(Path.Combine(Server.MapPath("~/archbase/"), "Query-Todas-sps.csv"));
            var reader = new StreamReader(file);
            for (int i = 1; i <= 8; i++)
            {
                /* Salto las primeras 8 lineas que no tienen informacion relevante */
                reader.ReadLine();
            }
            while (!reader.EndOfStream)
            {
                var line = reader.ReadLine();
                var values = line.Split(';');
                var val_analista = values[IndicesArchivoSps.INDICE_ANALISTA].Trim();
                val_analista = (val_analista != "-") ? val_analista : null;
                var val_severidad = values[IndicesArchivoSps.INDICE_SEVERIDAD].Trim();
                val_severidad = (val_severidad != "-") ? val_severidad : null;
                var val_impuesto = values[IndicesArchivoSps.INDICE_IMPUESTO].Trim();
                val_impuesto = (val_impuesto != "-") ? val_impuesto : null;
                var val_clase = values[IndicesArchivoSps.INDICE_CLASE].Trim();
                val_clase = (val_clase != "-") ? val_clase : null;
                var val_prioridad = values[IndicesArchivoSps.INDICE_PRIORIDAD].Trim();
                val_prioridad = (val_prioridad != "-") ? val_prioridad : null;
                var val_area = values[IndicesArchivoSps.INDICE_AREA].Trim();
                val_area = (val_area != "-") ? val_area : null;
                var val_estadoSps = values[IndicesArchivoSps.INDICE_ESTADO].Trim();
                var val_origen = values[IndicesArchivoSps.INDICE_ORIGEN].Trim();
                var val_fechaOrigen = values[IndicesArchivoSps.INDICE_FECHAORIGEN].Trim();
                var val_fechaComunicada = values[IndicesArchivoSps.INDICE_FECHACOMUNICADA].Trim();
                val_fechaComunicada = (val_fechaComunicada != "-") ? val_fechaComunicada : "1900-01-01";
                sps sps = new sps()
                {
                    analista = db.analista.FirstOrDefault(a => a.nombre == val_analista),
                    severidad = db.severidad.FirstOrDefault(a => a.descripcion == val_severidad),
                    impuesto = db.impuesto.FirstOrDefault(a => a.descripcion == val_impuesto),
                    estadoSps = db.estadoSps.FirstOrDefault(a => a.descripcion == val_estadoSps),
                    clase = db.clase.FirstOrDefault(a => a.descripcion == val_clase),
                    origen = db.origen.FirstOrDefault(a => a.descripcion == val_origen),
                    prioridad = db.prioridad.FirstOrDefault(a => a.descripcion == val_prioridad),
                    area = db.area.FirstOrDefault(a => a.descripcion == val_area),
                    fechaOrigen = DateTime.ParseExact(val_fechaOrigen.Substring(0,10), "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture),
                    fechaComunicada = DateTime.ParseExact(val_fechaComunicada, "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture),
                    caratula = values[IndicesArchivoSps.INDICE_CARATULA].Trim(),
                    tipo = values[IndicesArchivoSps.INDICE_TIPO].Trim(),
                    referencia = values[IndicesArchivoSps.INDICE_REFERENCIA].Trim(),
                    vuser = values[IndicesArchivoSps.INDICE_VUSER].Trim(),
                };
                db.sps.Add(sps);
        }
        db.SaveChanges();
            return "ok";
        }

        public string prueba()
        {
            return "Prueba prueba oejfiokjsd";
        }        

        // GET: Load
        public ActionResult Index()
        {
            return View();
        }

        // GET: Load/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Load/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Load/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Load/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Load/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Load/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Load/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
