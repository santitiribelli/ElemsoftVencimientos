using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessLogic.Utiles;

namespace GestionProcesos.Models
{
    public class Helpers
    {
        /// <summary>
        /// FUNCTION FOR EXPORT TO EXCEL
        /// </summary>
        /// <param name="dataTable"></param>
        /// <param name="worksheetName"></param>
        /// <param name="saveAsLocation"></param>
        /// <returns></returns>
        public DataTable ConvertToDataTable<T>(IList<T> data)
        {
            PropertyDescriptorCollection properties =
               TypeDescriptor.GetProperties(typeof(T));
            var table = new DataTable();
            foreach (PropertyDescriptor prop in properties)
            {
                if (prop.PropertyType.Name.Contains("String")
                    || prop.PropertyType.Name.Contains("DateTime")
                    || prop.PropertyType.Name.Contains("Boolean")
                     || prop.PropertyType.Name.Contains("Int32"))
                    table.Columns.Add(prop.Name, Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType);
            }
            foreach (T item in data)
            {
                DataRow row = table.NewRow();
                foreach (PropertyDescriptor prop in properties)
                {
                    if (prop.PropertyType.Name.Contains("String")
                    || prop.PropertyType.Name.Contains("DateTime")
                    || prop.PropertyType.Name.Contains("Boolean")
                     || prop.PropertyType.Name.Contains("Int32"))
                        row[prop.Name] = prop.GetValue(item) ?? DBNull.Value;
                }
                table.Rows.Add(row);
            }
            return table;

        }

        /// <summary>
        /// FUNCTION FOR EXPORT TO EXCEL
        /// </summary>
        /// <param name="dataTable"></param>
        /// <param name="worksheetName"></param>
        /// <param name="saveAsLocation"></param>
        /// <returns></returns>
        public string ConvertDataToHtml(System.Data.DataTable data)
        {
            var sb = new StringBuilder();
            sb.Append("<table>");
            sb.Append("<th>");

            foreach (DataColumn col in data.Columns)
            {
                sb.Append("<td>");
                sb.Append(col.ColumnName);
                sb.Append("</td>");
            }

            sb.Append("</th>");


            for (int i = 0; i < data.Rows.Count; i++)
            {

                sb.Append("<tr>");
                foreach (DataColumn col in data.Columns)
                {
                    sb.Append("<td>");

                    sb.Append(data.Rows[i][col].ToString());

                    sb.Append("</td>");
                }
                sb.Append("</tr>");
            }
            sb.Append("</table>");
            return sb.ToString();
        }

        /// <summary>
        /// FUNCTION FOR EXPORT TO EXCEL
        /// </summary>
        /// <param name="dataTable"></param>
        /// <param name="worksheetName"></param>
        /// <param name="saveAsLocation"></param>
        /// <returns></returns>
        public void ExportExcelHtml(System.Web.HttpContextBase context, System.Data.DataTable data, string fileName)
        {
            var grid = new GridView {DataSource = data};
            grid.DataBind();

            context.Response.ClearContent();
            context.Response.Buffer = true;
            context.Response.AddHeader("content-disposition", string.Format("attachment; filename={0}", fileName));
            context.Response.ContentType = "application/ms-excel";

            context.Response.Charset = "";
            var sw = new StringWriter();
            var htw = new HtmlTextWriter(sw);

            grid.RenderControl(htw);

            context.Response.Output.Write(sw.ToString());
            context.Response.Flush();
            context.Response.End();

        }

        /// <summary>
        /// FUNCTION FOR EXPORT TO EXCEL
        /// </summary>
        /// <param name="dataTable"></param>
        /// <param name="worksheetName"></param>
        /// <param name="saveAsLocation"></param>
        /// <returns></returns>
        public void ExportExcelHtml(System.Web.HttpContextBase context, System.Data.DataSet data, string fileName)
        {
            var grid = new GridView {DataSource = data};
            grid.DataBind();

            context.Response.ClearContent();
            context.Response.Buffer = true;
            context.Response.AddHeader("content-disposition", string.Format("attachment; filename={0}", fileName));
            context.Response.ContentType = "application/ms-excel";

            context.Response.Charset = "";
            var sw = new StringWriter();
            var htw = new HtmlTextWriter(sw);

            grid.RenderControl(htw);

            context.Response.Output.Write(sw.ToString());
            context.Response.Flush();
            context.Response.End();

        }

        public DataSet GetComboData(string query)
        {
            try
            {
                var con = UtilesAdmin.GetConnection();
                con.Open();

                var dadapter = new SqlDataAdapter();
                dadapter.SelectCommand = new SqlCommand(query, con);
                var data = new DataSet();
                dadapter.Fill(data);
                con.Close();

                data.Tables[0].Columns[1].ColumnName = "Text";
                data.Tables[0].Columns[0].ColumnName = "Id";

                return data;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}