using DiagnosticCenterBillMgtWebApp.Manager;
using DiagnosticCenterBillMgtWebApp.Model;
using Microsoft.Reporting.WebForms;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web.UI.WebControls;

namespace DiagnosticCenterBillMgtWebApp.UI
{
    public partial class TestRequest : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                GetTests();
            }
        }

        public void GetTests()
        {
            List<TestModel> tests = new TestManager().GetTests();
            testDropdown.DataSource = tests;
            testDropdown.DataTextField = "TestName";
            testDropdown.DataValueField = "TestId";
            testDropdown.DataBind();
            testDropdown.Items.Insert(0, new ListItem("--Select--", ""));
        }

        protected void testDropdown_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (testDropdown.SelectedItem.Value != string.Empty)
            {
                TestModel test = new TestManager().GetTestById(Convert.ToInt32(testDropdown.SelectedItem.Value));
                feeTextBox.Text = Math.Round(test.TestFee, 2).ToString();
            }
            else
            {
                messageBox.InnerHtml = GetMessage("You have to select a test", "danger");
                feeTextBox.Text = string.Empty;
            }
        }

        protected void addButton_Click(object sender, EventArgs e)
        {
            ClearMessageBox();
            if (new PatientManager().GetPatientsByContact(mobileTextBox.Text) != null)
            {
                messageBox.InnerHtml = GetMessage("Contact number exists. Try another", "danger");
                mobileTextBox.Focus();
                return;
            }

            PatientModel patient = null;
            List<TestModel> tests = new List<TestModel>();
            if (ViewState["patient"] != null)
            {
                patient = (PatientModel)ViewState["patient"];
                tests = patient.Tests;
            }

            if (testDropdown.SelectedItem.Value != string.Empty)
            {
                if (tests.Count > 0)
                {
                    List<TestModel> isExists = tests.Where(row => row.TestName == testDropdown.SelectedItem.Text).ToList();
                    if (isExists.Count > 0)
                    {
                        messageBox.InnerHtml = GetMessage("Test already selected. Try another.", "info");
                        return;
                    }
                }

                tests.Add(new TestModel(Convert.ToInt32(testDropdown.SelectedItem.Value),
                                        testDropdown.SelectedItem.Text,
                                        Convert.ToDecimal(feeTextBox.Text)));
                testDropdown.ClearSelection();
            }

            /*
             * Patient name, mobile/contact number and date of birth always
             * take from the form instead on getting from view state
             * (if exists than also. Assuming, last view state may has spelling/
             * wrong info
             */
            ViewState["patient"] = new PatientModel(patientNameTextBox.Text,
                                                    mobileTextBox.Text,
                                                    Convert.ToDateTime(dateOfBirthTextBox.Text),
                                                    tests);

            GetSelectedTests();
        }

        protected void saveButton_Click(object sender, EventArgs e)
        {
            if (ViewState["patient"] != null)
            {
                long patientId = new PatientManager().AddPatient((PatientModel)ViewState["patient"]);
                if (patientId > 0)
                {
                    messageBox.InnerHtml = GetMessage("Patient test request added.", "success");

                    patientNameTextBox.Text =
                        dateOfBirthTextBox.Text =
                        mobileTextBox.Text =
                        feeTextBox.Text =
                        totalTextBox.Text = string.Empty;

                    testDropdown.ClearSelection();
                    ViewState["patient"] = null;
                    recordPanel.Visible = false;

                    ReportParameter param = new ReportParameter("PatientID", patientId.ToString());

                    ReportViewer.ProcessingMode = ProcessingMode.Local;
                    ReportViewer.LocalReport.ReportPath = Server.MapPath("~/Reports/TestRequestBillReport.rdlc");
                    ReportViewer.LocalReport.SetParameters(param);
                    ReportViewer.LocalReport.Refresh();

                    PatientDataSetTableAdapters.vPatientTestDetailsTableAdapter ptdAdapter = new PatientDataSetTableAdapters.vPatientTestDetailsTableAdapter();
                    PatientDataSet.vPatientTestDetailsDataTable patient = ptdAdapter.GetData(patientId);

                    ReportDataSource datasource = new ReportDataSource("PatientDataSet", (DataTable)patient);
                    ReportViewer.LocalReport.DataSources.Clear();
                    ReportViewer.LocalReport.DataSources.Add(datasource);
                    //ReportViewer.Visible = true;

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
                    Response.AddHeader("content-disposition", "attachment; filename=" + "Test-Request-Bill" + "." + extension);
                    Response.BinaryWrite(bytes); // create the file
                    Response.Flush(); // send it to the client to download
                }
                else
                {
                    messageBox.InnerHtml = GetMessage("Patient test request failed.", "danger");
                }
            }
            GetSelectedTests();
        }

        public void GetSelectedTests()
        {
            if (ViewState["patient"] != null)
            {
                PatientModel patient = (PatientModel)ViewState["patient"];
                recordPanel.Visible = true;

                totalTextBox.Text = patient.Tests.AsEnumerable().Sum(row => row.TestFee).ToString();

                GridView.DataSource = patient.Tests;
            }
            else
                GridView.DataSource = null;

            GridView.DataBind();
        }

        private string GetMessage(string message, string type)
        {
            return string.Format("<div class='alert alert-{0}'>{1}</div>", type, message);
        }

        private void ClearMessageBox()
        {
            messageBox.InnerHtml = string.Empty;
        }
    }
}