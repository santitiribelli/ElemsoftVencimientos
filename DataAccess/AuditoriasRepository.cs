using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class AuditoriasRepository
    {
        public DataTable GetList(DateTime fechaDesde, DateTime fechaHasta, string tabla, string usuario)
        {
            using (var conn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["LaCaja2"].ConnectionString))
            {
                var sqlComm = new SqlCommand("sp_Aud_" + tabla + "", conn);
                sqlComm.Parameters.AddWithValue("@FecDes", fechaDesde);
                sqlComm.Parameters.AddWithValue("@FecHas", fechaHasta);
                sqlComm.Parameters.AddWithValue("@User", string.IsNullOrEmpty(usuario) ? null : usuario);

                sqlComm.CommandType = CommandType.StoredProcedure;

                var da = new SqlDataAdapter();
                da.SelectCommand = sqlComm;
                var data = new DataSet();
                da.Fill(data);

                return data.Tables[0];
            }
        }
    }
}
