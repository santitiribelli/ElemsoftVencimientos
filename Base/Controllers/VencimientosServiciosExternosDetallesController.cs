//using BusinessLogic;
//using Entities;
//using GestionProcesos.Models;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Web;
//using System.Web.Mvc;

//namespace GestionProcesos.Controllers
//{
//    public class VVencimientosServiciosExternosDetallesController : BaseController
//    {
//        //
//        // GET: /VencimientosServiciosExternosDetalles/

//        #region Vehiculos
//        [Autorizacion(Page = "Vehiculos")]
//        public ActionResult Buscador()
//        {
//            getPermisoUsuario("Vehiculos");
//            return View();
//        }

//        [Autorizacion(Page = "Vehiculos")]
//        public ActionResult BuscadorGrilla()
//        {
//            try
//            {
//                var list = VencimientosServiciosExternosDetallesAdmin.GetListJson();

//                return Json(list, JsonRequestBehavior.AllowGet);
//            }
//            catch (Exception ex)
//            {
//                return Json(new { success = false, msg = ex.Message }, JsonRequestBehavior.AllowGet);
//            }
//        }



//        [Autorizacion(Page = "Vehiculos")]
//        public ActionResult Index(int? id)
//        {
//            getPermisoUsuario("Vehiculos");
//            if (id != null)
//            {
//                var objeto = VencimientosServiciosExternosDetallesAdmin.Get((int)id);

//                return View(objeto);
//            }
//            return View();
//        }
//        [Autorizacion(Page = "Vehiculos")]
//        public ActionResult VencimientosServiciosExternos(int? id)
//        {
//            getPermisoUsuario("Vehiculos");
//            if (id != null)
//            {
//                var P = VencimientosServiciosExternosDetallesAdmin.Get((int)id);
//                return PartialView(P);
//            }
//            return PartialView();
//        }
//        [HttpPost]
//        [Autorizacion(Page = "Vehiculos")]
//        public ActionResult VencimientosServiciosExternosDetalles(VencimientosServiciosExternosDetalles model)
//        {
//            try
//            {
//                VencimientosServiciosExternosDetallesAdmin.InsertOrUpdate(model);

//                return Json(new { success = true, msg = model.VenSerExt_Id }, JsonRequestBehavior.AllowGet);
//            }
//            catch (Exception ex)
//            {
//                return Json(new { success = false, msg = ex.Message }, JsonRequestBehavior.AllowGet);
//            }
//        }


//        [Autorizacion(Page = "Vehiculos")]
//        [HttpPost]
//        public JsonResult VencimientosServiciosExternosDetallesDelete(int id)
//        {
//            try
//            {
//                VencimientosServiciosExternosDetallesAdmin.Delete(id);

//                return Json(new { success = true, msg = "" }, JsonRequestBehavior.AllowGet);
//            }
//            catch (Exception ex)
//            {
//                return Json(new { success = false, msg = ex.Message }, JsonRequestBehavior.AllowGet);
//            }
//        }

//        #endregion
//    }

//}
