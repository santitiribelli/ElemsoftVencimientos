using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using BusinessLogic;
using Entities;

namespace GestionProcesos.Models
{
    public class Autorizacion : AuthorizeAttribute
    {
        //Property to allow array instead of single string.
        private string[] _authorizedRoles;
        public string[] AuthorizedRoles
        {
            get { return _authorizedRoles ?? new string[0]; }
            set { _authorizedRoles = value; }
        }


        private string _page;
        public string Page
        {
            get { return _page; }
            set { _page = value; }
        }
        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            base.OnAuthorization(filterContext);

            ////If its an unauthorized/timed out ajax request go to top window and redirect to logon.
            //if (filterContext.Result is HttpUnauthorizedResult && filterContext.HttpContext.Request.IsAjaxRequest())
            //            filterContext.Result = new JavaScriptResult() { Script = top.location = '/Account/LogOn?Expired=1'; };

            //If authorization results in HttpUnauthorizedResult, redirect to error page instead of Logon page.
            if (filterContext.Result is HttpUnauthorizedResult)
                filterContext.Result = new RedirectResult("~/Account/Login");
        }


        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            if (httpContext == null)
                throw new ArgumentNullException("httpContext");

            if (!httpContext.User.Identity.IsAuthenticated)
            {
                httpContext.Response.Redirect("~/");
                return false;
            }

            //Bypass role check if user is Admin, prevents having to add Admin role across the whole project.
            //if(httpContext.User.IsInRole("Admin"))
            //    return true;

            //var user = new SeguridadUsuarios().PreLogin(httpContext.User.Identity.Name);
            var user = new SeguridadUsuarios();
            if (httpContext.Session != null && httpContext.Session.Count > 0)
            {
                user = (SeguridadUsuarios)httpContext.Session["UserLogged"];
            }
            else
            {
                if (httpContext.Request.IsAjaxRequest())
                {
                    httpContext.Response.StatusCode = 403;
                    httpContext.Response.End();
                }
                else
                {
                    httpContext.Response.Redirect("~/Account/Login", true);
                }
                return false;
            }
            //AuthorizedRoles = new[] { "pasquini" };
            ////If no roles are supplied to the attribute just check that the user is logged in.
            //if (AuthorizedRoles.Length == 0)
            //    return true;

            //AGREGAR LA VALIDACION POR PAGE SI LO CONTIENE EL ROL ASIGNADO EN EL USUARIO
            if (user.SegRol_Id != null)
            {
                if (SeguridadRolesPaginasAdmin.GetPermisoVerByRol((int)user.SegRol_Id, Page))
                    return true;

            }


            //AGREGAR LA VALIDACION POR PAGE SI LO CONTIENE ASIGNADO LA PERSONA
            if (SeguridadUsuariosPaginasAdmin.GetPermisoVerByUsuario((int)user.SegUsu_Id, Page))
                return true;


            //Check to see if any of the authorized roles fits into any assigned roles only if roles have been supplied.
            //if (AuthorizedRoles.Any(httpContext.User.IsInRole))
            //    return true;

            return false;
        }

        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            filterContext.Result = new RedirectToRouteResult(
                        new RouteValueDictionary(
                            new
                            {
                                controller = "Seguridad",
                                action = "AccesoDenegado"
                            })
                        );
        }
    }
}