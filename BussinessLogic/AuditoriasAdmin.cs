using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess;

namespace BusinessLogic
{
    public class AuditoriasAdmin
    {
        public DataTable GetList(DateTime fechaDesde, DateTime fechaHasta, string tabla, string usuario)
        {
            return new AuditoriasRepository().GetList(fechaDesde, fechaHasta, tabla, usuario);
        }
    }
}
