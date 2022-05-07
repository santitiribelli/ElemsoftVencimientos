using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BusinessLogic.Utiles
{
    public class ExcelExport
    {

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

        public void ExportExcelHtml(System.Web.HttpContextBase context, System.Data.DataTable data, string fileName)
        {
            var grid = new GridView();
            grid.DataSource = data;
            grid.DataBind();

            context.Response.ClearContent();
            context.Response.Buffer = true;
            context.Response.AddHeader("content-disposition", string.Format("attachment; filename={0}", fileName));
            context.Response.ContentType = "application/ms-excel";

            context.Response.Charset = "";
            StringWriter sw = new StringWriter();
            HtmlTextWriter htw = new HtmlTextWriter(sw);

            grid.RenderControl(htw);

            context.Response.Output.Write(sw.ToString());
            context.Response.Flush();
            context.Response.End();

        }

        public void ExportExcelHtml(System.Web.HttpContextBase context, System.Data.DataSet data, string fileName)
        {
            var grid = new GridView();
            grid.DataSource = data;
            grid.DataBind();

            context.Response.ClearContent();
            context.Response.Buffer = true;
            context.Response.AddHeader("content-disposition", string.Format("attachment; filename={0}", fileName));
            context.Response.ContentType = "application/ms-excel";

            context.Response.Charset = "";
            StringWriter sw = new StringWriter();
            HtmlTextWriter htw = new HtmlTextWriter(sw);

            grid.RenderControl(htw);

            context.Response.Output.Write(sw.ToString());
            context.Response.Flush();
            context.Response.End();

        }


        //public void ExportarEsto()
        //{
        //    MemoryStream stream = SpreadsheetReader.Create();
        //    SpreadsheetDocument doc = SpreadsheetDocument.Open(stream, true);
        //    var worksheetPart = SpreadsheetReader.GetWorksheetPartByName(doc, "Sheet1");
        //    WorksheetWriter writer = new WorksheetWriter(doc, worksheetPart);

        //    writer.PasteText("B2", "Hello World");

        //    //Save to the memory stream
        //    SpreadsheetWriter.Save(doc);

        //    //Write to response stream
        //    HttpContext.Current.Response.Clear();
        //    HttpContext.Current.Response.AddHeader("content-disposition", String.Format("attachment;filename={0}", "example1.xlsx"));
        //    HttpContext.Current.Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";

        //    stream.WriteTo(HttpContext.Current.Response.OutputStream);
        //    HttpContext.Current.Response.End();
        //}
        /// <summary>
        /// FUNCTION FOR EXPORT TO EXCEL
        /// </summary>
        /// <param name="dataTable"></param>
        /// <param name="worksheetName"></param>
        /// <param name="saveAsLocation"></param>
        /// <returns></returns>
        public string WriteDataTableToExcel(string dir, System.Data.DataTable dataTable, string worksheetName, string saveAsLocation, string ReporType)
        {
            foreach (var file in new DirectoryInfo(dir).GetFiles())
            {
                file.Delete();
            }


            //Microsoft.Office.Interop.Excel.Application excel;
            //Microsoft.Office.Interop.Excel.Workbook excelworkBook;
            //Microsoft.Office.Interop.Excel._Worksheet excelSheet;
            Microsoft.Office.Interop.Excel.Range excelCellrange;

            // Start Excel and get Application object.
            var excel = new Microsoft.Office.Interop.Excel.Application();

            // for making Excel visible
            //excel.Visible = false;
            //excel.DisplayAlerts = false;

            // Creation a new Workbook
            excel.Workbooks.Add();
            //excelworkBook = excel.Workbooks.Add(Type.Missing);

            // Workk sheet
            Microsoft.Office.Interop.Excel._Worksheet excelSheet = excel.ActiveSheet;
            //excelSheet = (Microsoft.Office.Interop.Excel.Worksheet)excelworkBook.ActiveSheet;
            //excelSheet.Name = worksheetName;

            string fileName = string.Format(@"{0}" + saveAsLocation, dir);

            try
            {
                excelSheet.Cells[1, 1] = ReporType;
                excelSheet.Cells[1, 2] = "Date : " + DateTime.Now.ToShortDateString();

                // loop through each row and add values to our sheet
                int rowcount = 2;

                foreach (DataRow datarow in dataTable.Rows)
                {
                    rowcount += 1;
                    for (int i = 1; i <= dataTable.Columns.Count; i++)
                    {
                        // on the first iteration we add the column headers
                        if (rowcount == 3)
                        {
                            excelSheet.Cells[2, i] = dataTable.Columns[i - 1].ColumnName;
                            excelSheet.Cells.Font.Color = System.Drawing.Color.Black;

                        }

                        excelSheet.Cells[rowcount, i] = datarow[i - 1].ToString();

                        //for alternate rows
                        if (rowcount > 3)
                        {
                            if (i == dataTable.Columns.Count)
                            {
                                if (rowcount % 2 == 0)
                                {
                                    excelCellrange = excelSheet.Range[excelSheet.Cells[rowcount, 1], excelSheet.Cells[rowcount, dataTable.Columns.Count]];
                                    FormattingExcelCells(excelCellrange, "#CCCCFF", System.Drawing.Color.Black, false);
                                }

                            }
                        }

                    }

                }

                // now we resize the columns
                excelCellrange = excelSheet.Range[excelSheet.Cells[1, 1], excelSheet.Cells[rowcount, dataTable.Columns.Count]];
                excelCellrange.EntireColumn.AutoFit();
                Microsoft.Office.Interop.Excel.Borders border = excelCellrange.Borders;
                border.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous;
                border.Weight = 2d;


                excelCellrange = excelSheet.Range[excelSheet.Cells[1, 1], excelSheet.Cells[2, dataTable.Columns.Count]];
                FormattingExcelCells(excelCellrange, "#000099", System.Drawing.Color.White, true);

                //now save the workbook and exit Excel
                excelSheet.SaveAs(fileName);
                //excelworkBook.SaveAs(fileName); ;
                //excelworkBook.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                excel.Quit();

                // Release COM objects (very important!)
                //System.Runtime.InteropServices.Marshal.ReleaseComObject(excel);

                //if (excelSheet != null)
                //System.Runtime.InteropServices.Marshal.ReleaseComObject(excelSheet);

                // Force garbage collector cleaning
                //GC.Collect();

                // Save this data as a file
                //HttpContext.Current.Response.ClearContent();
                //HttpContext.Current.Response.Buffer = true;
                //HttpContext.Current.Response.ContentType = "application/x-msexcel";
                //HttpContext.Current.Response.AppendHeader("Content-Disposition", "attachment; filename=" + saveAsLocation + ".xlsx");
                //HttpContext.Current.Response.TransmitFile(fileName);
                //HttpContext.Current.Response.Flush();

                //System.IO.File.Delete(fileName);

                //HttpContext.Current.Response.End();



                //excelSheet = null;
                excelCellrange = null;
                //excelworkBook = null;
            }

            return fileName;

        }

        /// <summary>
        /// FUNCTION FOR FORMATTING EXCEL CELLS
        /// </summary>
        /// <param name="range"></param>
        /// <param name="HTMLcolorCode"></param>
        /// <param name="fontColor"></param>
        /// <param name="IsFontbool"></param>
        public void FormattingExcelCells(Microsoft.Office.Interop.Excel.Range range, string HTMLcolorCode, System.Drawing.Color fontColor, bool IsFontbool)
        {
            range.Interior.Color = System.Drawing.ColorTranslator.FromHtml(HTMLcolorCode);
            range.Font.Color = System.Drawing.ColorTranslator.ToOle(fontColor);
            if (IsFontbool == true)
            {
                range.Font.Bold = IsFontbool;
            }
        }






    }
}
