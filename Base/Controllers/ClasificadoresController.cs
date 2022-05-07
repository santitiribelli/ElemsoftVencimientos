using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BusinessLogic;
using Entities;
using GestionProcesos.Models;

namespace GestionProcesos.Controllers
{
    public class ClasificadoresController : BaseController
    {
        [Autorizacion(Page = "Clasificadores")]
        public ActionResult Index()
        {
            getPermisoUsuario("Clasificadores");

            return View();
        }

        [HttpGet]
        [Autorizacion(Page = "Clasificadores")]
        public JsonResult Grilla(string tabla)
        {
            try
            {
                var list = ClasificadoresAdmin.GenerarMenuPadre(tabla);

                return Json(list, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { success = false, msg = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        [Autorizacion(Page = "Clasificadores")]
        public JsonResult Index(Clasificadores model)
        {
            try
            {
                ClasificadoresAdmin.InsertOrUpdate(model);

                return Json(new { success = true, msg = "" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { success = false, msg = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult ValidateSelect2Multi(int id)
        {
            try
            {
                var item = ClasificadoresAdmin.Get(id);

                return Json(new { success = true, msg = item.Clas_DatoOblig });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, msg = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult GetClasificador(String q, int? id, string tabla, int? idPropio)
        {
            try
            {
                var list = ClasificadoresAdmin.GetClasificador(tabla, id, q, idPropio);

                return Json(list, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { success = false, msg = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult GetClasificadorById(int id)
        {
            try
            {
                var list = ClasificadoresAdmin.GetListCascade(id);

                return Json(list, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { success = false, msg = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult GetClasificadorByCBU(string abrev, string tabla)
        {
            try
            {
                var lista = ClasificadoresAdmin.GetClasificadorByCBU(abrev, tabla);

                return Json(lista, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { success = false, msg = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
        //public ActionResult ExportExcel2(string tabla)
        //{

        //    GenerarMenuPadre(tabla);
        //    var dt = ConvertToDataTable(listaClas);

        //    //var dt = new DataTable();
        //    var fileName = string.Format("Clasificadores{0}.xls", tabla);
        //    new ExcelExport().ExportExcelHtml(this.HttpContext, dt, fileName);

        //    return View();

        //}

        //[HttpPost]
        //public JsonResult ExportExcel(string tabla)
        //{
        //    try
        //    {
        //        GenerarMenuPadre(tabla);
        //        var dt = ConvertToDataTable(listaClas);
        //        var dir = Server.MapPath("~/Uploads/tmp/");
        //        var fileName = string.Format("Clasificadores{0}.xlsx", tabla);
        //        var file = new ExcelExport().WriteDataTableToExcel(dir, dt, "Clasificadores", "Clasificadores" + tabla, tabla);//

        //        return Json(new { success = true, msg = fileName }, JsonRequestBehavior.AllowGet);
        //    }
        //    catch (Exception ex)
        //    {
        //        return Json(new { success = false, msg = ex.Message }, JsonRequestBehavior.AllowGet);
        //    }

        //}
    }
}
