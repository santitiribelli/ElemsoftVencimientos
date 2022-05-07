using System;
using System.Web.Mvc;
using System.Web.Security;
using BusinessLogic;
using BusinessLogic.Utiles;
using Entities;
using GestionProcesos.Models;
using System.Collections.Generic;
using System.Linq;

namespace GestionProcesos.Controllers
{
    public class AccountController : Controller
    {
        public ActionResult Login()
        {
            ViewBag.Produccion = UtilesAdmin.GetConnection().Database.Contains("Prod");

            //var algo = MovimientosComentariosAdmin.EncriptarClave();
            FormsAuthentication.SignOut();
            Session.Abandon();
            return View();
        }

        [HttpPost]
        public JsonResult Login(int user, string password)
        {
            try
            {
                var oUser = SeguridadUsuariosAdmin.Login(user, password);

                if (MockHelper.GetInstance.IsEnabled)
                {
                    HttpContext.Session["Idiomas"] = MockHelper.GetInstance.Idiomas;
                }

                return Json(new { success = true, msg = oUser }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { success = false, msg = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult CambiarPassword()
        {

            return View();
        }

        [HttpPost]
        public JsonResult CambiarPassword(string pass, string passActual)
        {
            try
            {
                var usuario = (SeguridadUsuarios)Session["UserLogged"];

                if (usuario.SegUsu_Pass != Security.GetSHA512WithSalt(passActual, usuario.Salt))
                    throw new Exception("Usuarios - Cambiar Contraseña - Error Contraseña Actual");

                usuario.SegUsu_Pass = pass;
                SeguridadUsuariosAdmin.InsertOrUpdate(usuario);

                Session["UserLogged"] = usuario;

                return Json(new { success = true, msg = "" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { success = false, msg = ex.Message }, JsonRequestBehavior.AllowGet);
                //log the error
                //SaveTechLog("Home", SancorLevelError.ERROR, ex);
                //return RedirectToAction("Index", "Error");
            }
        }

        [HttpPost]
        public ActionResult LogOff()
        {
            return RedirectToAction("Login", "Account");
        }

    }
}
