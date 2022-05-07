using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess;
using Entities;

namespace BusinessLogic
{
    public static class SeguridadRolesPaginasAdmin
    {
        public static List<SeguridadRolesPaginas> GetListByRol(int id)
        {
            return SeguridadRolesPaginasRepository.GetListByRol(id);
        }

        public static object GetJsonListByRol(int id)
        {
            return SeguridadRolesPaginasRepository.GetListByRol(id).Select(per =>
                    new
                    {
                        id = per.SegRolPag_Id,
                        nombre = per.SeguridadPaginas.SegPag_Nom,
                        desc = per.SeguridadPaginas.SegPag_NomDesc,
                        ver = per.SegRolPag_Ver,
                        exportar = per.SegRolPag_Expo,
                        alta = per.SegRolPag_Alta
                    }).ToList();
        }

        public static bool GetPermisoVerByRol(int id, string pagina)
        {
            try
            {
                return SeguridadRolesPaginasRepository.GetPermisoVerByRol(id, pagina);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static SeguridadRolesPaginas Get(int id)
        {
            return SeguridadRolesPaginasRepository.Get(id);
        }

        public static void Update(SeguridadRolesPaginas varia)
        {
            try
            {
                SeguridadRolesPaginasRepository.Update(varia);
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
                var item = SeguridadRolesPaginasRepository.Get(id);

                switch (campo)
                {
                    case "A":
                        if (mod == "A")
                        {
                            item.SegRolPag_Alta = true;
                            item.SegRolPag_Expo = true;
                            item.SegRolPag_Ver = true;
                        }
                        else
                        {
                            item.SegRolPag_Alta = false;
                        }
                        break;
                    case "E":
                        if (mod == "A")
                        {
                            item.SegRolPag_Expo = true;
                            item.SegRolPag_Ver = true;
                        }
                        else
                        {
                            item.SegRolPag_Expo = false;
                        }
                        break;
                    case "V":
                        if (mod == "A")
                        {
                            item.SegRolPag_Ver = true;
                        }
                        else
                        {
                            item.SegRolPag_Ver = false;
                            item.SegRolPag_Expo = false;
                            item.SegRolPag_Alta = false;
                        }
                        break;
                }

                SeguridadRolesPaginasRepository.Update(item);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
