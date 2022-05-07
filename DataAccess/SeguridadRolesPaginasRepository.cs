using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities;

namespace DataAccess
{
    public static class SeguridadRolesPaginasRepository
    {
        public static List<SeguridadRolesPaginas> GetListByRol(int id)
        {
            using (var context = Utiles.ContextoLocal())
            {
                return (from p in context.SeguridadRolesPaginas
                        .Include("SeguridadPaginas")
                        where p.SegRol_Id == id
                        select p).ToList();
            }
        }

        public static bool GetPermisoVerByRol(int id, string pagina)
        {
            using (var context = Utiles.ContextoLocal())
            {
                var algo = (from p in context.SeguridadRolesPaginas
                            where p.SegRol_Id == id &&
                            p.SeguridadPaginas.SegPag_Nom == pagina && p.SegRolPag_Ver
                            select p).FirstOrDefault();

                return algo != null;
            }
        }

        //public static SeguridadRolesPaginas GetPermisoByRol(int rolId, string pagina)
        //{
        //    using (var context = Utiles.ContextoLocal())
        //    {
        //        return (from p in context.SeguridadRolesPaginas
        //                    where p.SegRol_Id == rolId &&
        //                    p.SeguridadPaginas.SegPag_Nom == pagina
        //                    select p).FirstOrDefault();
        //    }
        //}

        public static SeguridadRolesPaginas Get(int id)
        {
            using (var context = Utiles.ContextoLocal())
            {
                return (from p in context.SeguridadRolesPaginas
                        where p.SegRolPag_Id == id
                        select p).FirstOrDefault();
            }
        }

        public static void Update(SeguridadRolesPaginas varia)
        {
            try
            {
                using (var context = Utiles.ContextoLocal())
                {
                    var viejo =
                        context.SeguridadRolesPaginas.Attach(
                            context.SeguridadRolesPaginas.Single(i => i.SegRolPag_Id == varia.SegRolPag_Id));

                    context.Entry(viejo).CurrentValues.SetValues(varia);

                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Hubo un inconveniente no se pudo realizar la modificación.");
            }
        }
    }
}
