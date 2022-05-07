using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using DataAccess;
using Entities;
using BusinessLogic.Utiles;

namespace BusinessLogic
{
    public static class PersonasAdmin
    {
        #region Web Central
        public static List<Personas> GetList()
        {
            try
            {
                return PersonasRepository.GetList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static List<Personas> GetList(bool perFisica)
        {
            try
            {
                return PersonasRepository.GetList(perFisica);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static object GetListJson(HttpServerUtilityBase server)
        {
            try
            {
                return PersonasRepository.GetList().Select(per =>
                    new
                    {
                        id = per.Per_Id,
                        nombreCompleto = per.Per_Ape,
                        documento = per.Documento,
                        telefonoCelular = per.Per_TE_Celular ?? "",
                        mail = per.Per_MAIL ?? "",
                        prov = per.Proveedores.Count == 0 ? "danger" : "success",
                        provId = per.Proveedores.Count == 0 ? (int?)null : per.Proveedores.FirstOrDefault().Prov_Id,
                        provVerificado = per.Proveedores.Count == 0 ? true : per.Proveedores.FirstOrDefault().Prov_DatosVerificados,
                        perFis = per.Per_PersonaFisica ? "True" : "False",
                        datosVef = per.Per_DatosVerificados, //#ffea00 amarillo
                        comodin = per.Per_RegistroComodin ? "True" : "False", //#1488c5 celeste
                        foto = System.IO.File.Exists(server.MapPath("~/Uploads/Personas/" + per.Per_Id + ".jpg")) ? "True" : "False",
                    }).OrderBy(x => x.comodin).ThenBy(x => x.datosVef).ThenBy(i => i.nombreCompleto).ToList();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public static List<Personas> GetList(int id)
        {
            try
            {
                return PersonasRepository.GetList(id);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static List<Personas> GetList(string q)
        {
            try
            {
                return PersonasRepository.GetList(q);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static List<Personas> GetList(string q, bool fisica)
        {
            try
            {
                return PersonasRepository.GetList(q, fisica);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static Personas Get(int id)
        {
            try
            {
                return PersonasRepository.Get(id);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static Personas Get(string cuit)
        {
            try
            {
                var codigo = cuit.Substring(0, 2);
                var numero = cuit.Substring(3, 8);
                var control = cuit.Substring(12, 1);
                return PersonasRepository.Get(Convert.ToInt32(codigo), Convert.ToInt32(numero), Convert.ToInt32(control));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static void InsertOrUpdate(Personas model)
        {
            try
            {
                model.AudFecha = DateTime.Now;
                model.AudUser = ((SeguridadUsuarios)HttpContext.Current.Session["UserLogged"]).SegUsu_Id;
                if (model.Per_PersonaFisica)
                    model.Per_Ape = model.Per_SoloApe.Trim() + " " + model.Per_SoloNom.Trim();
                else
                {
                    model.Per_Ape = model.Per_SoloNom.Trim();
                    model.Per_SoloApe = "";
                }

                if (model.Per_Id > 0)
                {
                    var original = PersonasRepository.GetLimpio(model.Per_Id);
                    model.Per_DatosVerificados = true;
                   
                    PersonasRepository.Update(model);
                }
                else
                {
                    model.Per_DatosVerificados = true;
                    PersonasRepository.Insert(model);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static void InsertAlta(Personas Personas, Proveedores Proveedores)
        {
            try
            {
                SeguridadUsuarios usuarioEmpleado = null;
                Personas.AudFecha = DateTime.Now;
                Personas.AudUser = ((SeguridadUsuarios)HttpContext.Current.Session["UserLogged"]).SegUsu_Id;
                if (Personas.Per_PersonaFisica)
                    Personas.Per_Ape = Personas.Per_SoloApe.Trim() + " " + Personas.Per_SoloNom.Trim();
                else
                {
                    Personas.Per_Ape = Personas.Per_SoloApe.Trim();
                    Personas.Per_SoloApe = "";
                }

                Personas.Per_SoloApe = Personas.Per_SoloApe.ToUpper();
                if (Personas.Per_SoloNom != null)
                    Personas.Per_SoloNom = Personas.Per_SoloNom.ToUpper();
                Personas.Per_Ape = Personas.Per_Ape.ToUpper();
                Personas.Per_FecHas = null;
                Personas.Per_Direccion = null;
                Personas.Clas_Localidad_Id = null;
                Personas.Per_TE_Celular = null;
                Personas.Per_MAIL = null;
                Personas.Per_DatosCompletos = false;

                if (Proveedores != null && !String.IsNullOrEmpty(Proveedores.Prov_Codigo))
                {
                    Proveedores.AudFecha = DateTime.Now;
                    Proveedores.AudUser = ((SeguridadUsuarios)HttpContext.Current.Session["UserLogged"]).SegUsu_Id;
                    Proveedores.Per_Id = Personas.Per_Id;
                    Proveedores.Clas_ProEst_Id = 16;
                    //Proveedores.Prov_DatosVerificados = ((SeguridadUsuarios)HttpContext.Current.Session["UserLogged"]).SeguridadRoles.SegRol_Descr == "Administrador";
                    Proveedores.Prov_DatosVerificados = true;
                    //ProveedoresRepository.Insert(Proveedores);
                }
                
                PersonasRepository.Insert(Personas, usuarioEmpleado, Proveedores);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static void Delete(int id)
        {
            try
            {
                PersonasRepository.Delete(id);
            }
            catch (Exception ex)
            {
                throw new Exception("Hubo un inconveniente no se pudo realizar la modificación.");
            }
        }

        public static void Verificar(int id)
        {
            try
            {
                var persona = PersonasRepository.Get(id);
                persona.Per_DatosVerificados = true;
                PersonasRepository.Update(persona);
            }
            catch (Exception ex)
            {

                throw new Exception("Hubo un inconveniente no se pudo realizar la modificación.");
            }
        }
        #endregion
        #region Web Clientes
        public static object GetListJsonWebClientes(HttpServerUtilityBase server)
        {
            try
            {
                var user = ((SeguridadUsuarios)HttpContext.Current.Session["ClientesUserLogged"]).Personas1.Per_Id ;
                
                return PersonasRepository.GetListByCli(user).Select(per =>
                    new
                    {
                        id = per.Per_Id,
                        nombreCompleto = per.Per_Ape,
                        documento = per.Documento,
                        telefonoCelular = per.Per_TE_Celular ?? "",
                        mail = per.Per_MAIL ?? "",
                        perFis = per.Per_PersonaFisica ? "True" : "False",
                        datosVef = per.Per_DatosVerificados, //#ffea00 amarillo
                        comodin = per.Per_RegistroComodin ? "True" : "False", //#1488c5 celeste
                        foto = System.IO.File.Exists(server.MapPath("~/Uploads/Personas/" + per.Per_Id + ".jpg")) ? "True" : "False",
                        query = Security.EncriptarUrl("Index", "Personas", new { id = per.Per_Id, tab= "Personas" }),
                        queryEmpleados = Security.EncriptarUrl("Index", "Empleados", new { id = per.Per_Id, tab = "Empleados" }),
                        queryTripulantes = Security.EncriptarUrl("Index", "Tripulantes", new { id = per.Per_Id, tab = "Tripulantes" }),
                    }).OrderBy(x => x.comodin).ThenBy(x => x.datosVef).ThenBy(i => i.nombreCompleto).ToList();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public static void InsertAltaWebClientes(Personas Personas, Proveedores Proveedores)
        {
            try
            {
                SeguridadUsuarios usuarioEmpleado = null;
                Personas.AudFecha = DateTime.Now;
                Personas.AudUser = ((SeguridadUsuarios)HttpContext.Current.Session["ClientesUserLogged"]).SegUsu_Id;
                if (Personas.Per_PersonaFisica)
                    Personas.Per_Ape = Personas.Per_SoloApe.Trim() + " " + Personas.Per_SoloNom.Trim();
                else
                {
                    Personas.Per_Ape = Personas.Per_SoloApe.Trim();
                    Personas.Per_SoloApe = "";
                }

                Personas.Per_SoloApe = Personas.Per_SoloApe.ToUpper();
                if (Personas.Per_SoloNom != null)
                    Personas.Per_SoloNom = Personas.Per_SoloNom.ToUpper();
                Personas.Per_Ape = Personas.Per_Ape.ToUpper();
                Personas.Per_FecHas = null;
                Personas.Per_Direccion = null;
                Personas.Clas_Localidad_Id = null;
                Personas.Per_TE_Celular = null;
                Personas.Per_MAIL = null;
                Personas.Per_DatosCompletos = false;
                Personas.Per_DatosVerificados = false;
                
                PersonasRepository.Insert(Personas, usuarioEmpleado, Proveedores);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static void InsertOrUpdateWebClientes(Personas model)
        {
            try
            {
                model.AudFecha = DateTime.Now;
                model.AudUser = ((SeguridadUsuarios)HttpContext.Current.Session["ClientesUserLogged"]).SegUsu_Id;
                if (model.Per_PersonaFisica)
                    model.Per_Ape = model.Per_SoloApe.Trim() + " " + model.Per_SoloNom.Trim();
                else
                {
                    model.Per_Ape = model.Per_SoloNom.Trim();
                    model.Per_SoloApe = "";
                }

                if (model.Per_Id > 0)
                {
                    var original = PersonasRepository.GetLimpio(model.Per_Id);
                    model.Per_DatosVerificados = false;

                    PersonasRepository.Update(model);
                }
                else
                {
                    model.Per_DatosVerificados = false;
                    PersonasRepository.Insert(model);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

    }
}
