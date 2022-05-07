using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Security;
using BusinessLogic.Utiles;
using DataAccess;
using Entities;

namespace BusinessLogic
{
    public static class SeguridadUsuariosAdmin
    {
        public static bool Login(int pUsername, string pPassword)
        {
            try
            {
                //VERIFICO SI EL USUARIO EXISTE
                var oUser = SeguridadUsuariosRepository.Login(pUsername);
                if (oUser != null)
                {
                    //VERIFICO SI EL USUARIO FUE DADO DE BAJA
                    if (oUser.SegUsu_FecBaja != null && oUser.SegUsu_FecBaja < DateTime.Now)
                    {
                        throw new Exception("Usuario deshabilitado.");
                    }

                    //VERIFICO QUE LA CONTRASEÑA SEA CORRECTA
                    if (oUser.SegUsu_Pass == Security.GetSHA512WithSalt(pPassword, oUser.Salt))
                    {
                        HttpContext.Current.Session["PermisosPagina"] =
                                        SeguridadUsuariosPaginasAdmin.GetListByUser(oUser.SegUsu_Id);

                        if (oUser.SegRol_Id != null)
                            HttpContext.Current.Session["PermisosRol"] =
                                SeguridadRolesPaginasAdmin.GetListByRol((int)oUser.SegRol_Id);

                        HttpContext.Current.Session["UserLogged"] = oUser;
                        FormsAuthentication.SetAuthCookie(pUsername.ToString(), false);
                        
                        if (oUser.Personas1.Per_Cuil_Doc != null)
                            return Security.GetSHA512WithSalt(oUser.Personas1.Per_Cuil_Doc.ToString(), oUser.Salt) != oUser.SegUsu_Pass;
                        else
                            return Security.GetSHA512WithSalt(oUser.Personas1.Per_Doc_Extranjero, oUser.Salt) != oUser.SegUsu_Pass;
                    }
                    throw new Exception("Contraseña o usuario incorrecto.");
                }
                throw new Exception("Usuario inexistente.");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static void LoginWindows(int pUsername, string pPassword)
        {
            try
            {
                //VERIFICO SI EL USUARIO EXISTE
                var oUser = SeguridadUsuariosRepository.Login(pUsername);
                if (oUser != null)
                {
                    //VERIFICO SI EL USUARIO FUE DADO DE BAJA
                    if (oUser.SegUsu_FecBaja != null && oUser.SegUsu_FecBaja < DateTime.Now)
                    {
                        throw new Exception("Usuario deshabilitado.");
                    }

                    //VERIFICO QUE LA CONTRASEÑA SEA CORRECTA
                    if (oUser.SegUsu_Pass == Security.GetSHA512WithSalt(pPassword, oUser.Salt))
                    {
                        
                    }else
                        throw new Exception("Contraseña o usuario incorrecto.");
                }else
                    throw new Exception("Usuario inexistente.");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static void InsertOrUpdate(SeguridadUsuarios model)
        {
            try
            {
                if (model.SegUsu_Id > 0)
                {
                    var usuMod = SeguridadUsuariosRepository.Get(model.SegUsu_Id);
                    if (!string.IsNullOrEmpty(model.SegUsu_Pass))
                    {
                        usuMod.Salt = Security.GenerateSalt().ToString();
                        usuMod.SegUsu_Pass = Security.GetSHA512WithSalt(model.SegUsu_Pass.Trim(), usuMod.Salt);
                    }

                    usuMod.SegRol_Id = model.SegRol_Id;
                    usuMod.SegUsu_FecBaja = model.SegUsu_FecBaja;

                    SeguridadUsuariosRepository.Update(usuMod);
                }
                else
                {
                    var persona = PersonasRepository.Get((int)model.Per_Id);
                    model.SegUsu_FecAlta = DateTime.Now;
                    model.Salt = Security.GenerateSalt().ToString();

                    if (persona.Per_Cuil_Doc != null)
                        model.SegUsu_Pass = Security.GetSHA512WithSalt(persona.Per_Cuil_Doc.ToString(), model.Salt);
                    else
                        model.SegUsu_Pass = Security.GetSHA512WithSalt(persona.Per_Doc_Extranjero, model.Salt);

                    SeguridadUsuariosRepository.Insert(model);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static List<SeguridadUsuarios> GetList()
        {
            return SeguridadUsuariosRepository.GetList();
        }

        public static object GetJsonList()
        {
            try
            {
                return SeguridadUsuariosRepository.GetList().Select(per =>
                    new
                    {
                        id = per.SegUsu_Id,
                        usuario = per.Personas1.Documento + " - " + per.Personas1.NombreCompleto,
                        rol = per.SeguridadRoles != null ? per.SeguridadRoles.SegRol_Descr : "",
                        rolId = per.SeguridadRoles != null ? per.SegRol_Id.ToString() : "",
                        fechaAlta = per.SegUsu_FecAlta.ToString("dd/MM/yyyy"),
                        fechaBaja = per.SegUsu_FecBaja.HasValue ? per.SegUsu_FecBaja.Value.ToString("dd/MM/yyyy") : "",
                    }).ToList();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public static SeguridadUsuarios Get(int id)
        {
            return SeguridadUsuariosRepository.Get(id);
        }

        public static void ResetearContraseña(int id)
        {
            try
            {
                var usuario = SeguridadUsuariosRepository.Get(id);
                usuario.Salt = Security.GenerateSalt().ToString();
                if (usuario.Personas1.Per_Cuil_Doc != null)
                    usuario.SegUsu_Pass = Security.GetSHA512WithSalt(usuario.Personas1.Per_Cuil_Doc.ToString(), usuario.Salt);
                else
                    usuario.SegUsu_Pass = Security.GetSHA512WithSalt(usuario.Personas1.Per_Doc_Extranjero, usuario.Salt);

                SeguridadUsuariosRepository.Update(usuario);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #region Sitio Clientes

        public static bool LoginClientes(string pUsername, string pPassword, int idiomaId)
        {
            try
            {
                var oUser = SeguridadUsuariosRepository.LoginClientes(pUsername);
                if (oUser != null)
                {
                    if (oUser.SegUsu_Pass == Security.GetSHA512WithSalt(pPassword, oUser.Salt))
                    {
                        HttpContext.Current.Session["ClientesUserLogged"] = oUser;
                        FormsAuthentication.SetAuthCookie(pUsername, false);

                        return Security.GetSHA512WithSalt(oUser.Personas1.Per_Cuil_Doc.ToString(), oUser.Salt) == oUser.SegUsu_Pass
                               || Security.GetSHA512WithSalt(oUser.Personas1.Per_Doc_Extranjero, oUser.Salt) == oUser.SegUsu_Pass;
                    }
                    throw new Exception("Error Contraseña");
                }
                throw new Exception("ERROR");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static void UpdatePassword(SeguridadUsuarios model)
        {
            try
            {
                model.Salt = Security.GenerateSalt().ToString();
                model.SegUsu_Pass = Security.GetSHA512WithSalt(model.SegUsu_Pass, model.Salt);

                SeguridadUsuariosRepository.Update(model);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion
    }
}
