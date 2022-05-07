using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Utiles
{
    public static class UtilesAdmin
    {
        public static SqlConnection GetConnection()
        {
            return DataAccess.Utiles.GetConnection();
        }
    }

    public class HelperAdmin
    {
        public string DecryptFromString64(string text)
        {
            return new DataAccess.Helper().DecryptFromString64(text);
        }
    }
}
