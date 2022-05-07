using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities;

namespace DataAccess
{
    public static class SeguridadUsuariosPaginasRepository
    {
        public static List<SeguridadUsuariosPaginas> GetListByUser(int id)
        {
            using (var context = Utiles.ContextoLocal())
            {
                return (from p in context.SeguridadUsuariosPaginas.Include("SeguridadPaginas") where p.SegUsu_Id == id select p).ToList();
            }
        }

        public static bool GetPermisoVerByUsuario(int id, string pagina)
        {
            using (var context = Utiles.ContextoLocal())
            {
                var algo = (from p in context.SeguridadUsuariosPaginas
                            where p.SegUsu_Id == id &&
                            p.SeguridadPaginas.SegPag_Nom == pagina && p.SegPagUsu_Ver
                            select p).FirstOrDefault();

                return algo != null;
            }
        }

        public static SeguridadUsuariosPaginas GetPermisoByUsuario(int id, string pagina)
        {
            using (var context = Utiles.ContextoLocal())
            {
                return (from p in context.SeguridadUsuariosPaginas
                        where p.SegUsu_Id == id &&
                              p.SeguridadPaginas.SegPag_Nom == pagina
                        select p).FirstOrDefault();
            }
        }

        public static SeguridadUsuariosPaginas GetPermisoByUsuarioPagina(int usuarioId, int paginaId)
        {
            using (var context = Utiles.ContextoLocal())
            {
                return (from p in context.SeguridadUsuariosPaginas
                        where p.SegUsu_Id == usuarioId &&
                              p.SegPag_Id == paginaId
                        select p).FirstOrDefault();
            }
        }

        public static SeguridadUsuariosPaginas Get(int id)
        {
            using (var context = Utiles.ContextoLocal())
            {
                return (from p in context.SeguridadUsuariosPaginas
                        where p.SegUsuPag_Id == id
                        select p).FirstOrDefault();
            }
        }

        public static void Update(SeguridadUsuariosPaginas varia)
        {
            try
            {
                using (var context = Utiles.ContextoLocal())
                {
                    var viejo =
                        context.SeguridadUsuariosPaginas.Attach(
                            context.SeguridadUsuariosPaginas.Single(i => i.SegUsuPag_Id == varia.SegUsuPag_Id));

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
