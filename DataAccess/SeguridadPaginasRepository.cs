using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities;

namespace DataAccess
{
    public static class SeguridadPaginasRepository
    {
        public static List<SeguridadPaginas> GetList()
        {
            using (var context = Utiles.ContextoLocal())
            {

                return (from p in context.SeguridadPaginas
                        select p).ToList();
            }
        }

        public static SeguridadPaginas GetByPagina(string pagina)
        {
            using (var context = Utiles.ContextoLocal())
            {

                return (from p in context.SeguridadPaginas
                        where p.SegPag_Nom == pagina
                        select p).FirstOrDefault();
            }
        }
    }
}
