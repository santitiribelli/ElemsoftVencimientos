using BusinessLogic;
using Entities;
using GestionProcesos.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GestionProcesos.Controllers
{
    public class VencimientosServiciosExternosController : BaseController
    {
        //
        // GET: /VencimientosServiciosExternos/

        #region Vencimientos

        [Autorizacion(Page = "Vehiculos")]
        public ActionResult Buscador()
        {
            getPermisoUsuario("Vehiculos");
            return View();
        }

        [Autorizacion(Page = "Vehiculos")]
        public ActionResult BuscadorGrilla()
        {
            try
            {
                var list = VencimientosServiciosExternosAdmin.GetListJson();

                return Json(list, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { success = false, msg = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }



        [Autorizacion(Page = "Vehiculos")]
        public ActionResult Index(int? id)
        {
            getPermisoUsuario("Vehiculos");
            if (id != null)
            {
                var objeto = VencimientosServiciosExternosAdmin.Get((int)id);

                return View(objeto);
            }
            return View();
        }
        [Autorizacion(Page = "Vehiculos")]
        public ActionResult VencimientosServiciosExternos(int? id)
        {
            getPermisoUsuario("Vehiculos");
            if (id != null)
            {
                var P = VencimientosServiciosExternosAdmin.Get((int)id);
                return PartialView(P);
            }
            return PartialView();
        }
        [HttpPost]
        [Autorizacion(Page = "Vehiculos")]
        public ActionResult VencimientosServiciosExternos(VencimientosServiciosExternos model)
        {
            try
            {
                VencimientosServiciosExternosAdmin.InsertOrUpdate(model);

                return Json(new { success = true, msg = model.VenSerExt_Id }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { success = false, msg = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
        

        [Autorizacion(Page = "Vehiculos")]
        [HttpPost]
        public JsonResult VencimientosServiciosExternosDelete(int id)
        {
            try
            {
                VencimientosServiciosExternosAdmin.Delete(id);

                return Json(new { success = true, msg = "" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { success = false, msg = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        #endregion

        #region Solapa 

        [Autorizacion(Page = "Vehiculos Licencias")]
        public ActionResult Detalles(int id)
        {
            getPermisoUsuario("Vehiculos Licencias");

            return PartialView();
         }
            [Autorizacion(Page = "Vehiculos Licencias")]
        
            public JsonResult DetallesGrilla(int id)
        {
            try
            {
                var list = VencimientosServiciosExternosDetallesAdmin.GetListByDetalleJson(id);

                return Json(list, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { success = false, msg = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }



        [HttpPost]
        [Autorizacion(Page = "Vehiculos")]
        public ActionResult Detalles(VencimientosServiciosExternosDetalles model)
        {
            try
            {
                VencimientosServiciosExternosDetallesAdmin.InsertOrUpdate(model);

                return Json(new { success = true, msg = model.VenSerExt_Id }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { success = false, msg = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }


        [Autorizacion(Page = "Vehiculos")]
        [HttpPost]
        public JsonResult DetallesDelete(int id)
        {
            try
            {
                VencimientosServiciosExternosDetallesAdmin.Delete(id);

                return Json(new { success = true, msg = "" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { success = false, msg = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        #endregion
    }

}
