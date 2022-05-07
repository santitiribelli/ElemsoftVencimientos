using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BusinessLogic;
using BusinessLogic.Utiles;
using Entities;
using GestionProcesos.Models;

namespace GestionProcesos.Controllers
{
    public class SeguridadController : BaseController
    {
        [Autorizacion(Page = "Rol")]
        public ActionResult Roles()
        {
            getPermisoUsuario("Rol");
            return View();
        }

        [HttpGet]
        [Autorizacion(Page = "Rol")]
        public JsonResult RolesGrilla()
        {
            try
            {
                var list = SeguridadRolesAdmin.GetJsonList();

                return Json(list, JsonRequestBehavior.AllowGet);

            }
            catch (Exception ex)
            {
                return Json(new { success = false, msg = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        [Autorizacion(Page = "Rol")]
        public JsonResult Roles(SeguridadRoles model)
        {
            try
            {
                SeguridadRolesAdmin.InsertOrUpdate(model);

                return Json(new { success = true, msg = "" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { success = false, msg = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        [Autorizacion(Page = "Usuario")]
        public ActionResult Usuarios()
        {
            getPermisoUsuario("Usuario");
            return View();
        }

        [HttpGet]
        [Autorizacion(Page = "Usuario")]
        public JsonResult UsuariosGrilla()
        {
            try
            {
                var list = SeguridadUsuariosAdmin.GetJsonList();

                return Json(list, JsonRequestBehavior.AllowGet);

            }
            catch (Exception ex)
            {
                return Json(new { success = false, msg = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        [Autorizacion(Page = "Usuario")]
        public JsonResult Usuarios(SeguridadUsuarios model)
        {
            try
            {
                SeguridadUsuariosAdmin.InsertOrUpdate(model);

                return Json(new { success = true, msg = "" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { success = false, msg = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult PermisosUsuario(int id)
        {
            var model = SeguridadUsuariosAdmin.Get(id);
            return View(model);
        }

        public ActionResult PermisosRoles(int id)
        {
            var model = SeguridadRolesAdmin.Get(id);
            return View(model);
        }

        public ActionResult AccesoDenegado()
        {
            return View();
        }

        [HttpGet]
        [Autorizacion(Page = "Rol")]
        public JsonResult GetPaginasRoles(int id)
        {
            try
            {
                var list = SeguridadRolesPaginasAdmin.GetJsonListByRol(id);

                return Json(list, JsonRequestBehavior.AllowGet);

            }
            catch (Exception ex)
            {
                return Json(new { success = false, msg = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpGet]
        [Autorizacion(Page = "Usuario")]
        public JsonResult GetPaginasUsuarios(int id)
        {
            try
            {
                var list = SeguridadUsuariosPaginasAdmin.GetJsonListByUser(id);

                return Json(list, JsonRequestBehavior.AllowGet);

            }
            catch (Exception ex)
            {
                return Json(new { success = false, msg = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        [Autorizacion(Page = "Rol")]
        public JsonResult AsignarPermisos(int id, string campo, string mod)
        {
            try
            {
                SeguridadRolesPaginasAdmin.AsignarPermisos(id, campo, mod);

                return Json(new { success = true, msg = mod }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { success = false, msg = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        [Autorizacion(Page = "Usuario")]
        public JsonResult AsignarPermisosUsuario(int id, string campo, string mod)
        {
            try
            {
                SeguridadUsuariosPaginasAdmin.AsignarPermisos(id, campo, mod);

                return Json(new { success = true, msg = mod }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { success = false, msg = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        [Autorizacion(Page = "Usuario")]
        public JsonResult ResetearContraseña(int id)
        {
            try
            {
                SeguridadUsuariosAdmin.ResetearContraseña(id);

                return Json(new { success = true, msg = "" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { success = false, msg = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
    }
}
