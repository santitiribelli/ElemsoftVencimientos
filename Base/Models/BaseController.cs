using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BusinessLogic;
using BusinessLogic.Utiles;
using Entities;

namespace GestionProcesos.Models
{
    public abstract class BaseController : Controller
    {
        protected override void Initialize(System.Web.Routing.RequestContext requestContext)
        {
            base.Initialize(requestContext);

            var user = (SeguridadUsuarios)HttpContext.Session["UserLogged"];

            ViewBag.Usuario = user != null ? user.Personas1.NombreCompleto : "";

            ViewBag.Produccion = UtilesAdmin.GetConnection().Database.Contains("Prod");
        }

        protected virtual void getPermisoUsuario(string pagina)
        {
            var user = (SeguridadUsuarios)Session["UserLogged"];

            var seguridadUsuario = ((List<SeguridadUsuariosPaginas>)Session["PermisosPagina"]).Where(x => x.SeguridadPaginas.SegPag_Nom == pagina).FirstOrDefault();
            var permisoClasificadores = ((List<SeguridadUsuariosPaginas>)Session["PermisosPagina"]).Where(x => x.SeguridadPaginas.SegPag_Nom == "Clasificadores").FirstOrDefault();
            if (user.SegRol_Id != null)
            {
                var seguridadRol = ((List<SeguridadRolesPaginas>)Session["PermisosRol"]).Where(x => 
                
                
           x.SegRol_Id == user.SegRol_Id && x.SeguridadPaginas.SegPag_Nom == pagina).FirstOrDefault();

                ViewBag.Exportar = seguridadRol.SegRolPag_Expo || seguridadUsuario.SegPagUsu_Expo;
                ViewBag.Alta = seguridadRol.SegRolPag_Alta || seguridadUsuario.SegPagUsu_Alta;
                ViewBag.PaginaId = seguridadRol.SegPag_Id;
                //ViewBag.EsAdmin = permiso.Where(x => x.SegRol_Id == user.SegRol_Id && x.SeguridadPaginas.SegPag_Nom == "Usuario").FirstOrDefault().SegRolPag_Alta || permiso2.Where(x => x.SeguridadPaginas.SegPag_Nom == "Usuario").FirstOrDefault().SegPagUsu_Alta;
                var permisoRolClasificadores = ((List<SeguridadRolesPaginas>)Session["PermisosRol"]).Where(x => x.SegRol_Id == user.SegRol_Id && x.SeguridadPaginas.SegPag_Nom == "Clasificadores").FirstOrDefault();

                ViewBag.ClasAlta = permisoRolClasificadores.SegRolPag_Alta || permisoClasificadores.SegPagUsu_Alta;
            }
            else
            {
                ViewBag.Exportar = seguridadUsuario.SegPagUsu_Expo;
                ViewBag.Alta = seguridadUsuario.SegPagUsu_Alta;
                ViewBag.PaginaId = seguridadUsuario.SegPag_Id;
                //ViewBag.EsAdmin = permiso2.Where(x => x.SeguridadPaginas.SegPag_Nom == "Usuario").FirstOrDefault().SegPagUsu_Alta;
                if (permisoClasificadores != null)
                    ViewBag.ClasAlta = permisoClasificadores.SegPagUsu_Alta;
            }
        }
    }
}