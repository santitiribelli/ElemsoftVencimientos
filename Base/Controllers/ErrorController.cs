using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GestionProcesos.Controllers
{
    public class ErrorController : Controller
    {
        //
        // GET: /Error/
        public ActionResult Index()
        {
            return View();
        }

        //[HttpPost]
        //public JsonResult SolicitudCambios(Pedidos_cambio_sis model)
        //{
        //    try
        //    {
        //        model.OP_PIDE = HttpContext.User.Identity.Name;
        //        model.fh_pedido = DateTime.Now;
        //        model.pantalla_cambio = HttpContext.Request.UrlReferrer.PathAndQuery;
        //        new Pedidos_cambio_sis().Insert(model);

        //        return Json(new { success = true, msg = "" }, JsonRequestBehavior.AllowGet);
        //    }
        //    catch (Exception ex)
        //    {
        //        return Json(new { success = false, msg = ex.Message }, JsonRequestBehavior.AllowGet);
        //    }
        //}
    }
}
