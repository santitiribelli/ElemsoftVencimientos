using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess;
using Entities;

namespace BusinessLogic
{
    public static class SeguridadUsuariosPaginasAdmin
    {
        public static List<SeguridadUsuariosPaginas> GetListByUser(int id)
        {
            return SeguridadUsuariosPaginasRepository.GetListByUser(id);
        }

        public static object GetJsonListByUser(int id)
        {
            List<SeguridadRolesPaginas> rolesPermisos = new List<SeguridadRolesPaginas>();
            var usuario = SeguridadUsuariosRepository.Get(id);
            if (usuario.SegRol_Id != null)
                rolesPermisos = SeguridadRolesPaginasRepository.GetListByRol((int)usuario.SegRol_Id);

            return SeguridadUsuariosPaginasRepository.GetListByUser(id).Select(per =>
                    new
                    {
                        id = per.SegUsuPag_Id,
                        nombre = per.SeguridadPaginas.SegPag_Nom,
                        desc = per.SeguridadPaginas.SegPag_NomDesc,
                        ver = per.SegPagUsu_Ver,
                        exportar = per.SegPagUsu_Expo,
                        alta = per.SegPagUsu_Alta,
                        heredadoAlta = rolesPermisos.Count>0 ? rolesPermisos.Where(x => x.SegPag_Id == per.SegPag_Id).FirstOrDefault().SegRolPag_Alta : false,
                        heredadoVer = rolesPermisos.Count > 0 ? rolesPermisos.Where(x => x.SegPag_Id == per.SegPag_Id).FirstOrDefault().SegRolPag_Ver : false
                    }).ToList();
        }

        public static bool GetPermisoVerByUsuario(int id, string pagina)
        {
            return SeguridadUsuariosPaginasRepository.GetPermisoVerByUsuario(id,pagina);
        }

        public static SeguridadUsuariosPaginas GetPermisoByUsuario(int id, string pagina)
        {
            return SeguridadUsuariosPaginasRepository.GetPermisoByUsuario(id,pagina);
        }

        public static SeguridadUsuariosPaginas GetPermisoByUsuarioPagina(int usuarioId, int paginaId)
        {
            return SeguridadUsuariosPaginasRepository.GetPermisoByUsuarioPagina(usuarioId,paginaId);
        }

        public static SeguridadUsuariosPaginas Get(int id)
        {
            return SeguridadUsuariosPaginasRepository.Get(id);
        }

        public static void Update(SeguridadUsuariosPaginas varia)
        {
            try
            {
                SeguridadUsuariosPaginasRepository.Update(varia);
            }
            catch (Exception ex)
            {
                
                throw ex;
            }
        }

        public static void AsignarPermisos(int id, string campo, string mod)
        {
            try
            {
                var item = SeguridadUsuariosPaginasRepository.Get(id);

                switch (campo)
                {
                    case "A":
                        if (mod == "A")
                        {
                            item.SegPagUsu_Alta = true;
                            item.SegPagUsu_Expo = true;
                            item.SegPagUsu_Ver = true;
                        }
                        else
                        {
                            item.SegPagUsu_Alta = false;
                        }
                        break;
                    case "E":
                        if (mod == "A")
                        {
                            item.SegPagUsu_Expo = true;
                            item.SegPagUsu_Ver = true;
                        }
                        else
                        {
                            item.SegPagUsu_Expo = false;
                        }
                        break;
                    case "V":
                        if (mod == "A")
                        {
                            item.SegPagUsu_Ver = true;
                        }
                        else
                        {
                            item.SegPagUsu_Ver = false;
                            item.SegPagUsu_Expo = false;
                            item.SegPagUsu_Alta = false;
                        }
                        break;
                }

                SeguridadUsuariosPaginasRepository.Update(item);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
