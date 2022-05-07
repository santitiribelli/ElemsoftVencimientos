using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities;

namespace DataAccess
{
    public static class SeguridadRolesRepository
    {
        public static List<SeguridadRoles> GetList()
        {
            using (var context = Utiles.ContextoLocal())
            {

                return (from p in context.SeguridadRoles
                        select p).ToList();
            }
        }

        public static SeguridadRoles Get(int id)
        {
            using (var context = Utiles.ContextoLocal())
            {
                return (from p in context.SeguridadRoles
                        where p.SegRol_Id == id
                        select p).FirstOrDefault();
            }
        }

        public static SeguridadRoles GetByDesc(string desc)
        {
            using (var context = Utiles.ContextoLocal())
            {
                return (from p in context.SeguridadRoles
                    where p.SegRol_Descr == desc
                    select p).FirstOrDefault();
            }
        }

        public static void Insert(SeguridadRoles varia)
        {
            try
            {
                using (var context = Utiles.ContextoLocal())
                {
                    context.SeguridadRoles.Add(varia);

                    var paginas = SeguridadPaginasRepository.GetList();
                    foreach (var item in paginas)
                    {
                        var algo = new SeguridadRolesPaginas
                        {
                            SegRolPag_Alta = false,
                            SegRolPag_Expo = false,
                            SegRolPag_Ver = false,
                            SegPag_Id = item.SegPag_Id,
                            SeguridadRoles = varia
                        };
                        context.SeguridadRolesPaginas.Add(algo);
                    }

                    context.SaveChanges();
                }
            }
            catch (DbEntityValidationException ex)
            {
                throw new Exception(ErrorHelper.dbError(ex));
            }
            catch (Exception ex)
            {
                if (ex.InnerException.InnerException.Message.Contains("uniqueRoles"))
                    throw new Exception("Ya existe un rol con ese nombre.");
                throw new Exception("Hubo un inconveniente no se pudo realizar la modificación.");
            }
        }

        public static void Update(SeguridadRoles varia)
        {
            try
            {
                using (var context = Utiles.ContextoLocal())
                {
                    var viejo =
                        context.SeguridadRoles.Attach(
                            context.SeguridadRoles.Single(i => i.SegRol_Id == varia.SegRol_Id));

                    context.Entry(viejo).CurrentValues.SetValues(varia);

                    context.SaveChanges();
                }
            }
            catch (DbEntityValidationException ex)
            {
                throw new Exception(ErrorHelper.dbError(ex));
            }
            catch (Exception ex)
            {
                if (ex.InnerException.InnerException.Message.Contains("uniqueRoles"))
                    throw new Exception("Ya existe un rol con ese nombre.");
                throw new Exception("Hubo un inconveniente no se pudo realizar la modificación.");
            }
        }
    }
}
