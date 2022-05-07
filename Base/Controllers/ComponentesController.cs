using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GestionProcesos.Models;

namespace GestionProcesos.Controllers
{
    public class ComponentesController : BaseController
    {
        public ActionResult Eliminar()
        {
            return PartialView();
        }

        public ActionResult Editar()
        {
            return PartialView();
        }

        public ActionResult Clasificadores()
        {
            return PartialView();
        }

    }
}
