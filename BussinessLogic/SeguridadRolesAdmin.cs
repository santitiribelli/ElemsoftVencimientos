using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess;
using Entities;

namespace BusinessLogic
{
    public static class SeguridadRolesAdmin
    {
        public static List<SeguridadRoles> GetList()
        {
            return SeguridadRolesRepository.GetList();
        }

        public static object GetJsonList()
        {
            try
            {
                return SeguridadRolesRepository.GetList().Select(per =>
                    new
                    {
                        id = per.SegRol_Id,
                        desc = per.SegRol_Descr
                    }).ToList();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public static SeguridadRoles Get(int id)
        {
            return SeguridadRolesRepository.Get(id);
        }

        public static void InsertOrUpdate(SeguridadRoles model)
        {
            try
            {
                if (model.SegRol_Id > 0)
                {
                    SeguridadRolesRepository.Update(model);
                }
                else
                {
                    SeguridadRolesRepository.Insert(model);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
