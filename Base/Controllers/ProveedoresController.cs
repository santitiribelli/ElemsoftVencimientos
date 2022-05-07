using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BusinessLogic;
using Entities;
using GestionProcesos.Models;

namespace GestionProcesos.Controllers
{
    public class ProveedoresController : BaseController
    {
        #region Proveedores
        [Autorizacion(Page = "Proveedores")]
        public ActionResult Buscador()
        {
            getPermisoUsuario("Proveedores");
            return View();
        }

        [HttpGet]
        [Autorizacion(Page = "Proveedores")]
        public JsonResult BuscadorGrilla()
        {
            try
            {
                var lista = ProveedoresAdmin.GetListJson();

                return Json(lista, JsonRequestBehavior.AllowGet);

            }
            catch (Exception ex)
            {
                return Json(new { success = false, msg = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        [Autorizacion(Page = "Proveedores")]
        public ActionResult Index(int id, string tab)
        {
            getPermisoUsuario("Proveedores");
            ViewBag.Tab = tab;
            ViewBag.Per_Id = id;
            ViewBag.path = System.IO.File.Exists(Server.MapPath("~/Uploads/Personas/" + id + ".jpg")) ? "../Uploads/Personas/" + id + ".jpg" : "/images/default_avatar_male.jpg";
            var persona = PersonasAdmin.Get(id);
            ViewBag.PerFis = persona.Per_PersonaFisica;
            ViewBag.Documento = persona.Documento;
            ViewBag.NombreCompleto = persona.NombreCompleto;
            var objeto = ProveedoresAdmin.GetByPerId((int)id);
            if (objeto != null)
                return View(objeto);
            else
                return View();
        }

        [Autorizacion(Page = "Proveedores")]
        public ActionResult Proveedores(int? id)
        {
            getPermisoUsuario("Proveedores");
            if (id != null)
            {
                var usuario = (SeguridadUsuarios)Session["UserLogged"];
                ViewBag.EsAdmin = usuario.SeguridadRoles.SegRol_Descr.ToUpper() == "ADMINISTRADOR";
                var P = ProveedoresAdmin.GetByPerId((int)id);
                if (P != null)
                {
                    return PartialView(P);
                }
                else
                {
                    return PartialView();
                }
            }
            return PartialView();
        }

        [HttpPost]
        [Autorizacion(Page = "Proveedores")]
        public ActionResult Proveedores(Proveedores model)
        {
            try
            {
                ProveedoresAdmin.InsertOrUpdate(model);

                return Json(new { success = true, msg = model.Prov_Id }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { success = false, msg = ex.Message }, JsonRequestBehavior.AllowGet);
            }

        }

        #endregion

        public JsonResult GetProveedores(String q)
        {
            try
            {
                var list = ProveedoresAdmin.GetList();

                var auxList =
                    list.Select(
                        per =>
                            new
                            {
                                id = per.Prov_Id,
                                text = per.Personas.NombreCompleto,
                            })
                        .ToList();

                return Json(auxList, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { success = false, msg = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

    }
}
