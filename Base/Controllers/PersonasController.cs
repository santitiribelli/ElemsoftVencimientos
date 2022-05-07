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
    public class PersonasController : BaseController
    {
        #region Personas

        [Autorizacion(Page = "Personas")]
        public ActionResult Buscador()
        {
            //ViewBag.Tipo = tipo;
            getPermisoUsuario("Personas");
            return View();
        }

        [Autorizacion(Page = "Personas")]
        public ActionResult BuscadorGrilla(/*string tipo*/)
        {
            try
            {
                var list = PersonasAdmin.GetListJson(Server);

                return Json(list, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { success = false, msg = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        [Autorizacion(Page = "Personas")]
        public ActionResult IndexAlta()
        {
            getPermisoUsuario("Personas");
            return View();
        }

        [HttpPost]
        [Autorizacion(Page = "Personas")]
        public JsonResult PersonaAlta(Personas Personas,Proveedores Proveedores)
        {
            try
            {
                Personas existeCUIT = null;
                if (Personas.Per_Cuil_Doc != null)
                    existeCUIT = PersonasAdmin.Get(Personas.Cuit);

                var existeNombre = PersonasAdmin.GetList(Personas.Per_PersonaFisica ? Personas.Per_SoloApe + " " + Personas.Per_SoloNom : Personas.Per_SoloApe);

                //verifico si el cuit encontrado es de la persona ya cargada
                if (existeCUIT != null)
                    throw new Exception("El CUIL ya fue registrado para " + existeCUIT.NombreCompleto);

                PersonasAdmin.InsertAlta(Personas, Proveedores);

                //return Json(new { success = true, msg = perfis.Per_Id }, JsonRequestBehavior.AllowGet);
                return Json(new
                {
                    success = true,
                    msg = Personas.Per_Id,
                    msgExisteNombre = existeNombre.Count > 0 ? "El nombre " + (Personas.Per_PersonaFisica ? Personas.Per_SoloApe + " " + Personas.Per_SoloNom : Personas.Per_SoloApe) + " se encontraba registrado." : ""
                }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { success = false, msg = ex.Message }, JsonRequestBehavior.AllowGet);
            }

        }

        [Autorizacion(Page = "Personas")]
        public ActionResult Index(int id, string tab)
        {
            getPermisoUsuario("Personas");

            ViewBag.Tab = tab;
            ViewBag.path = System.IO.File.Exists(Server.MapPath("~/Uploads/Personas/" + id + ".jpg")) ? "../Uploads/Personas/" + id + ".jpg" : "";
            var objeto = PersonasAdmin.Get(id);
            if (objeto != null)
                return View(objeto);
            else
                return View();
        }

        [Autorizacion(Page = "Personas")]
        public ActionResult Personas(int id)
        {
            getPermisoUsuario("Personas");
            var usuario = (SeguridadUsuarios)Session["UserLogged"];
            ViewBag.EsAdmin = usuario.SeguridadRoles.SegRol_Descr.ToUpper() == "ADMINISTRADOR";
            var P = PersonasAdmin.Get((int)id);
            //ViewBag.Foto = System.IO.File.Exists(Server.MapPath("~/Uploads/Personas/" + id + ".jpg"));
            return PartialView(P);
        }

        [HttpPost]
        [Autorizacion(Page = "Personas")]
        public ActionResult Personas(Personas model)
        {
            try
            {
                PersonasAdmin.InsertOrUpdate(model);

                return Json(new { success = true, msg = model.Per_Id }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { success = false, msg = ex.Message }, JsonRequestBehavior.AllowGet);
            }

        }

        [HttpPost]
        [Autorizacion(Page = "Personas")]
        public ActionResult Verificar(int id)
        {
            try
            {

                PersonasAdmin.Verificar(id);

                return Json(new { success = true, msg = "" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { success = false, msg = ex.Message }, JsonRequestBehavior.AllowGet);
            }

        }

        [Autorizacion(Page = "Personas")]
        public ActionResult IndexAltaComodin()
        {
            getPermisoUsuario("Personas");
            return View();
        }

        [HttpPost]
        [Autorizacion(Page = "Personas")]
        public ActionResult PersonaAltaComodin(Personas Personas)
        {
            try
            {
                PersonasAdmin.InsertOrUpdate(Personas);

                return Json(new { success = true, msg = Personas.Per_Id }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { success = false, msg = ex.Message }, JsonRequestBehavior.AllowGet);
            }

        }
        [Autorizacion(Page = "Personas")]
        [HttpPost]
        public JsonResult PersonasDelete(int id)
        {
            try
            {
                PersonasAdmin.Delete(id);

                return Json(new { success = true, msg = "" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { success = false, msg = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }


        #endregion
        
        #region  auxiliares
        public JsonResult GetPersonaFisicas(String q)
        {
            try
            {
                var list = PersonasAdmin.GetList(q, true).Where(x => x.Per_PersonaFisica);

                var auxList =
                    list.Select(per => new
                    {
                        id = per.Per_Id,
                        text = per.DocumentoNombre,
                        cuit = per.Per_Cuil_Doc != null

                    })
                        .ToList();

                return Json(auxList, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { success = false, msg = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult GetPaciente(String q)
        {
            try
            {
                var list = PersonasAdmin.GetList(q);

                var auxList =
                    list.Select(per => new
                    {
                        id = per.Per_Id,
                        text = per.DocumentoNombre,
                        cuit = per.Per_Cuil_Doc != null,
                    })
                        .ToList();

                return Json(auxList, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { success = false, msg = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult GetPacienteById(int id)
        {
            try
            {
                var list = PersonasAdmin.GetList(id);

                var auxList = list.Select(per => new
                {
                    id = per.Per_Id,
                    text = per.DocumentoNombre,
                    cuit = per.Per_Cuil_Doc != null
                }).ToList();

                return Json(auxList, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { success = false, msg = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        public object ValidarCUIT(string cuit, int? sexo, int? id)
        {
            Personas existeCUIT;
            var codigo = 0;
            var control = 0;
            var numeroOriginal = cuit.Substring(3, 8);
            var suma = 0;

            if (sexo == null)
            {
                existeCUIT = PersonasAdmin.Get(cuit);

                //verifico si el cuit encontrado es de la persona ya cargada
                if (existeCUIT != null && existeCUIT.Per_Id == id)
                    existeCUIT = null;

                return Json(new
                {
                    success = false,
                    msg = "Debe asignar el sexo primero",
                    msgExisteId = existeCUIT != null ? existeCUIT.Per_Id : 0,
                    msgExisteNombre = existeCUIT != null ? existeCUIT.NombreCompleto : ""
                }, JsonRequestBehavior.AllowGet);
            }

            var clasificadorSexo = ClasificadoresAdmin.Get((int)sexo);

            codigo = clasificadorSexo.Clas_Abrev == "MASC" ? 20 : 27;

            suma += Convert.ToInt32(codigo.ToString().Substring(0, 1)) * 5;//10
            suma += Convert.ToInt32(codigo.ToString().Substring(1, 1)) * 4;//0
            suma += Convert.ToInt32(numeroOriginal.Substring(0, 1)) * 3;//9
            suma += Convert.ToInt32(numeroOriginal.Substring(1, 1)) * 2;//8
            suma += Convert.ToInt32(numeroOriginal.Substring(2, 1)) * 7;//0
            suma += Convert.ToInt32(numeroOriginal.Substring(3, 1)) * 6;//0
            suma += Convert.ToInt32(numeroOriginal.Substring(4, 1)) * 5;//25
            suma += Convert.ToInt32(numeroOriginal.Substring(5, 1)) * 4;//0
            suma += Convert.ToInt32(numeroOriginal.Substring(6, 1)) * 3;//12
            suma += Convert.ToInt32(numeroOriginal.Substring(7, 1)) * 2;//10
            var division = suma / 11; //6
            var resto = suma - (division * 11); //8
            if (resto == 1)
            {
                if (clasificadorSexo.Clas_Abrev == "MASC")
                {
                    control = 9;
                    codigo = 23;
                }
                else
                {
                    control = 4;
                    codigo = 23;
                }

            }
            else if (resto > 1)
            {
                control = 11 - resto; //3n 
            }

            existeCUIT = PersonasAdmin.Get(cuit);

            //verifico si el cuit encontrado es de la persona ya cargada
            if (existeCUIT != null && existeCUIT.Per_Id == id)
                existeCUIT = null;

            return Json(new
            {
                success = (cuit == (codigo + "-" + numeroOriginal + "-" + control) || cuit.Substring(0, 2) == "24" || cuit.Substring(0, 2) == "23"),
                msg = "Validacion - Personas - CUIT Incorrecto" + " " + (codigo + "-" + numeroOriginal + "-" + control),
                msgExisteId = existeCUIT != null ? existeCUIT.Per_Id : 0,
                msgExisteNombre = existeCUIT != null ? existeCUIT.NombreCompleto : ""
            }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetPersonasOffline()
        {
            try
            {
                var list = PersonasAdmin.GetList();

                var auxList =
                    list.Select(
                            per =>
                                new
                                {
                                    id = per.Per_Id,
                                    text = per.NombreCompleto,
                                })
                        .ToList();

                return Json(auxList, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { success = false, msg = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public ActionResult UploadFoto(int id)
        {
            var bannerImage = Request.Files[0] as HttpPostedFileBase;
            if (bannerImage != null && bannerImage.ContentLength > 0)
            {
                if (!Directory.Exists(Server.MapPath("~/Uploads/Personas/"))) //If not exists directory add
                    Directory.CreateDirectory(Server.MapPath("~/Uploads/Personas/"));

                var path = Server.MapPath("~/Uploads/Personas/" + id + ".jpg");
                bannerImage.SaveAs(path);

                return
                    Json(
                        new
                        {
                            success = true,
                            message = "Image has been updated successfully"
                        });
            }
            else
            {
                return Json(new { success = false, message = "File is empty" });
            }
        }


        public JsonResult GetDatosPersonas(int id)
        {
            try
            {
                var p = PersonasAdmin.Get(id);
                return Json(new
                {
                    success = true,
                    apellido = p.Per_SoloApe ?? "",
                    nombre = p.Per_SoloNom ?? "",
                    alias = p.Per_Alias ?? "",
                    cuil = p.Cuit ?? "",
                    docExt = p.Per_Doc_Extranjero ?? "",
                    domicilio = p.Per_Direccion ?? "",
                    sexo = p.Clas_Sexo_Id.HasValue ? p.Clas_Sexo_Id.Value.ToString() : "",
                    telefono = p.Per_TE_Celular ?? "",
                    email = p.Per_MAIL ?? "",
                    desde = p.Per_FecDes.HasValue ? p.Per_FecDes.Value.ToString("dd/MM/yyyy") : "",
                    hasta = p.Per_FecHas.HasValue ? p.Per_FecHas.Value.ToString("dd/MM/yyyy") : "",
                    observaciones = p.Per_Obs ?? "",
                    localidadId = p.Clas_Localidad_Id.HasValue ? p.Clas_Localidad_Id.Value.ToString() : "",
                    nacionalidadId = p.Clas_Nacionalidad_Id.HasValue ? p.Clas_Nacionalidad_Id.Value.ToString():"",
                    datosCompletos = p.Per_DatosCompletos == true ? "True":"False",
                    datosVerificados = p.Per_DatosVerificados == true ? "True" : "False",
                    perFis = p.Per_PersonaFisica == true ? "True" : "False",


                }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { success = false, msg = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
        #endregion
    }
}
