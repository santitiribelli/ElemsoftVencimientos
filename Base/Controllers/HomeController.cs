using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BusinessLogic;
using Entities;
using GestionProcesos.Models;

namespace GestionProcesos.Controllers
{
    public class HomeController : BaseController
    {
        //
        // GET: /Home/
        public ActionResult Index()
        {
            var user = (SeguridadUsuarios)Session["UserLogged"];
            var permisoClasificadores = ((List<SeguridadUsuariosPaginas>)Session["PermisosPagina"]).Where(x => x.SeguridadPaginas.SegPag_Nom == "Clasificadores").FirstOrDefault();

            if (user.SegRol_Id != null)
            {
                var permisoRolClasificadores =
                    ((List<SeguridadRolesPaginas>)Session["PermisosRol"]).Where(
                        x => x.SegRol_Id == user.SegRol_Id && x.SeguridadPaginas.SegPag_Nom == "Clasificadores")
                        .FirstOrDefault();

                ViewBag.ClasAlta = permisoRolClasificadores.SegRolPag_Alta || permisoClasificadores.SegPagUsu_Alta;
            }
            else
            {
                ViewBag.ClasAlta = permisoClasificadores.SegPagUsu_Alta;
            }

            return View();
        }

    }
}
