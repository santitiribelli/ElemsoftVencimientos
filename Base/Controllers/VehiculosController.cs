using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using BusinessLogic;
using Entities;
using GestionProcesos.Models;

namespace GestionProcesos.Controllers
{
    public class VehiculosController : BaseController
    {
        #region Vehiculos
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
                var list = VehiculosAdmin.GetListJson();

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
                var objeto = VehiculosAdmin.Get((int)id);

                return View(objeto);
            }
            return View();
        }

        [Autorizacion(Page = "Vehiculos")]
        public ActionResult Vehiculos(int? id)
        {
            getPermisoUsuario("Vehiculos");
            if (id != null)
            {
                var P = VehiculosAdmin.Get((int)id);
                return PartialView(P);
            }
            return PartialView();
        }

        [HttpPost]
        [Autorizacion(Page = "Vehiculos")]
        public ActionResult Vehiculos(Vehiculos model)
        {
            try
            {
                VehiculosAdmin.InsertOrUpdate(model);

                return Json(new { success = true, msg = model.Veh_Id }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { success = false, msg = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
        [HttpPost]
        [Autorizacion(Page = "Vehiculos")]
        public ActionResult VehiculosVerificar(int id)
        {
            try
            {

                VehiculosAdmin.Verificar(id);

                return Json(new { success = true, msg = "" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { success = false, msg = ex.Message }, JsonRequestBehavior.AllowGet);
            }

        }

        [Autorizacion(Page = "Vehiculos")]
        [HttpPost]
        public JsonResult VehiculosDelete(int id)
        {
            try
            {
                VehiculosAdmin.Delete(id);

                return Json(new { success = true, msg = "" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { success = false, msg = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        #endregion

        #region Vehiculos Documentos
        [Autorizacion(Page = "Vehiculos Documentos")]
        public ActionResult Documentos(int id)
        {
            getPermisoUsuario("Vehiculos Documentos");
            return PartialView();
        }

        [HttpPost]
        [Autorizacion(Page = "Vehiculos Documentos")]
        public ActionResult Documentos(VehiculosDocumentos model)
        {
            try
            {
                VehiculosDocumentosAdmin.InsertOrUpdate(model);

                return Json(new { success = true, msg = model.VehDoc_Id }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { success = false, msg = ex.Message }, JsonRequestBehavior.AllowGet);
            }

        }
        [Autorizacion(Page = "Vehiculos Documentos")]
        [HttpGet]
        public JsonResult DocumentosGrilla(int id)
        {
            try
            {
                var list = VehiculosDocumentosAdmin.GetListByVehiculoJson(id);

                return Json(list, JsonRequestBehavior.AllowGet);

            }
            catch (Exception ex)
            {
                return Json(new { success = false, msg = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        [Autorizacion(Page = "Vehiculos Documentos")]
        [HttpPost]
        public JsonResult DocumentosDelete(int id)
        {
            try
            {
                VehiculosDocumentosAdmin.Delete(id);

                return Json(new { success = true, msg = "" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { success = false, msg = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
        [HttpPost]
        [Autorizacion(Page = "Vehiculos Documentos")]
        public ActionResult DocumentosVerificar(int id)
        {
            try
            {

                VehiculosDocumentosAdmin.Verificar(id);

                return Json(new { success = true, msg = "" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { success = false, msg = ex.Message }, JsonRequestBehavior.AllowGet);
            }

        }

        [HttpPost]
        public ActionResult UploadArchivo(int id)
        {
            try
            {
                var bannerImage = Request.Files[0] as HttpPostedFileBase;
                if (bannerImage != null && bannerImage.ContentLength > 0)
                {
                    var doc = VehiculosDocumentosAdmin.Get(id);
                    if (!Directory.Exists(Server.MapPath("~/Uploads/VehiculosDocumentos/"))) //If not exists directory add
                        Directory.CreateDirectory(Server.MapPath("~/Uploads/VehiculosDocumentos/"));

                    if (!String.IsNullOrEmpty(doc.VehDoc_ArchivoAdjunto) && System.IO.File.Exists(Server.MapPath("~/Uploads/VehiculosDocumentos/" + doc.VehDoc_ArchivoAdjunto)))
                    {
                        System.IO.File.Delete(Server.MapPath("~/Uploads/VehiculosDocumentos/" + doc.VehDoc_ArchivoAdjunto));
                    }

                    var fileName = Path.GetFileName(bannerImage.FileName);
                    fileName = Regex.Replace(fileName, @"\s|\$|\#\%", "");
                    var path = String.Empty;

                    path = Server.MapPath("~/Uploads/VehiculosDocumentos/" + doc.VehDoc_Id + " " + bannerImage.FileName);
                    bannerImage.SaveAs(path);

                    doc.VehDoc_ArchivoAdjunto = doc.VehDoc_Id + " " + bannerImage.FileName;
                    VehiculosDocumentosAdmin.UpdateVehiculosDocumentosArchivos(doc);

                    return
                        Json(
                            new
                            {
                                success = true,
                                msg = ""
                            });
                }
                //else
                //{
                //    return Json(new { success = false, msg = "File is empty" });
                //}

                return Json(new { success = true, msg = "" });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, msg = ex.Message });
            }
        }

        #endregion

        #region Vehiculos Licencias

        /// <summary>
        /// Solapa Vehiculos Licencias
        /// </summary>
        [Autorizacion(Page = "Vehiculos Licencias")]
        public ActionResult Licencias(int id)
        {
            getPermisoUsuario("Vehiculos Licencias");

            return PartialView();
        }

        /// <summary>
        /// Listado de Vehiculos Licencias : Grilla
        /// </summary>
        [HttpGet]
        [Autorizacion(Page = "Vehiculos Licencias")]
        public JsonResult LicenciasGrilla(int id)
        {
            try
            {
                var list = VehiculosLicenciasAdmin.GetListByVehiculoJson(id);

                return Json(list, JsonRequestBehavior.AllowGet);

            }
            catch (Exception ex)
            {
                return Json(new { success = false, msg = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        /// <summary>
        /// Insert / Edit
        /// </summary>
        [HttpPost]
        [Autorizacion(Page = "Vehiculos Licencias")]
        public ActionResult Licencias(VehiculosLicencias model)
        {
            try
            {
                VehiculosLicenciasAdmin.InsertOrUpdate(model);

                return Json(new { success = true, msg = "" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { success = false, msg = ex.Message }, JsonRequestBehavior.AllowGet);
            }

        }

        /// <summary>
        /// Eliminar
        /// </summary>
        [Autorizacion(Page = "Vehiculos Licencias")]
        [HttpPost]
        public JsonResult LicenciasDelete(int id)
        {
            try
            {
                VehiculosLicenciasAdmin.Delete(id);

                return Json(new { success = true, msg = "" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { success = false, msg = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }


        [HttpPost]
        [Autorizacion(Page = "Vehiculos Licencias")]
        public JsonResult ValidarFechasLicencias(DateTime desde, DateTime? hasta, int? idPropio, int vehiculoId)
        {
            try
            {
                VehiculosLicenciasAdmin.SuperposicionFechas(desde, hasta, idPropio, vehiculoId);

                return Json(new { success = true, msg = "" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { success = false, msg = ex.Message }, JsonRequestBehavior.AllowGet);
            }

        }

        [HttpPost]
        [Autorizacion(Page = "Vehiculos Licencias")]
        public ActionResult LicenciasVerificar(int id)
        {
            try
            {

                VehiculosLicenciasAdmin.Verificar(id);

                return Json(new { success = true, msg = "" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { success = false, msg = ex.Message }, JsonRequestBehavior.AllowGet);
            }

        }

        #endregion

        public JsonResult GetVehiculos()
        {
            try
            {
                var list = VehiculosAdmin.GetListVerificados();

                var auxList =
                    list.Select(
                            per =>
                                new
                                {
                                    id = per.Veh_Id,
                                    text = per.Veh_Patente,
                                    licencias = per.VehiculosLicencias.Select(m => new
                                    {
                                        fechaDesde = m.VehLic_FechaDesde.ToString("dd/MM/yyyy"),
                                        fechaHasta = m.VehLic_FechaHasta.HasValue ? m.VehLic_FechaHasta.Value.ToString("dd/MM/yyyy") : "",
                                        tipo = m.Clasificadores.Clas_Desc
                                    }).ToList(),
                                })
                        .ToList();

                return Json(new { success = true, msg = auxList }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { success = false, msg = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult GetVehiculosMesaOperativa()
        {
            try
            {
                var list = VehiculosAdmin.GetListVerificados();

                var auxList =
                    list.Select(
                            per =>
                                new
                                {
                                    id = per.Veh_Id,
                                    text = per.Veh_Patente + (string.IsNullOrEmpty(per.Veh_Obs) ? "" : " - " + per.Veh_Obs),
                                    cantidad = per.Veh_PasajerosCantidad,
                                    licencias = per.VehiculosLicencias.Select(m => new
                                    {
                                        fechaDesde = m.VehLic_FechaDesde.ToString("dd/MM/yyyy"),
                                        fechaHasta = m.VehLic_FechaHasta.HasValue ? m.VehLic_FechaHasta.Value.ToString("dd/MM/yyyy") : "",
                                        tipo = m.Clasificadores.Clas_Desc
                                    }).ToList(),
                                })
                        .ToList();

                return Json(new { success = true, msg = auxList }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { success = false, msg = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
    }
}
