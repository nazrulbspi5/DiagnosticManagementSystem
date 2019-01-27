using DiagnosticCenterBillMgtWebApp.Manager;
using DiagnosticCenterBillMgtWebApp.Model;
using Microsoft.Reporting.WebForms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI.WebControls;

namespace DiagnosticCenterBillMgtWebApp.UI
{
    public partial class TestWiseReport : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected void searchBox_Click(object sender, EventArgs e)
        {
            DateTime startDate, endDate;
            if (!DateTime.TryParse(fromDateTextBox.Text, out startDate))
            {
                messageBox.InnerHtml = GetMessage("From date not correctly formated as mm/dd/yyyy", "danger");
                fromDateTextBox.Focus();
            }
            else if (!DateTime.TryParse(toDateTextBox.Text, out endDate))
            {
                messageBox.InnerHtml = GetMessage("To date not correctly formated as mm/dd/yyyy", "danger");
                toDateTextBox.Focus();
            }
            else
            {
                if (endDate < startDate)
                {
                    messageBox.InnerHtml = GetMessage("To date must be date after from date.", "danger");
                    toDateTextBox.Focus();
                }
                else
                {
                    List<ReportModel> report = new TestManager().GetReport(startDate, endDate);
                    if (report == null)
                    {
                        messageBox.InnerHtml = GetMessage("No records found!", "info");
                        recordPanel.Visible = false;
                    }
                    else
                    {
                        messageBox.InnerHtml = GetMessage("Records found!", "success");
                        totalTextBox.Text = Math.Round(report.AsEnumerable().Sum(row => row.TotalAmount), 2).ToString();
                        recordPanel.Visible = true;
                        ViewState["report"] = report;
                    }
                    GridView.DataSource = report;
                    GridView.DataBind();
                }
            }
        }

        protected void pdfButton_Click(object sender, EventArgs e)
        {
            List<ReportModel> report = (List<ReportModel>)ViewState["report"];
            ReportViewer.Visible = true;
            ReportViewer.ProcessingMode = ProcessingMode.Local;
            ReportViewer.LocalReport.ReportPath = Server.MapPath("~/Reports/TestWiseReport.rdlc");

            ReportDataSource datasource = new ReportDataSource("ReportDataSet", report);
            ReportViewer.LocalReport.DataSources.Clear();
            ReportViewer.LocalReport.DataSources.Add(datasource);

            // Variables
            Warning[] warnings;
            string[] streamIds;
            string mimeType = string.Empty;
            string encoding = string.Empty;
            string extension = string.Empty;

            byte[] bytes = ReportViewer.LocalReport.Render("PDF", null, out mimeType, out encoding, out extension, out streamIds, out warnings);

            // Now that you have all the bytes representing the PDF report, buffer it and send it to the client.
            Response.Buffer = true;
            Response.Clear();
            Response.ContentType = mimeType;
            Response.AddHeader("content-disposition", "attachment; filename=" + "Test-Wise-Report" + "." + extension);
            Response.BinaryWrite(bytes); // create the file
            Response.Flush(); // send it to the client to download
        }

        private string GetMessage(string message, string type)
        {
            return string.Format("<div class='alert alert-{0}'>{1}</div>", type, message);
        }
    }
}