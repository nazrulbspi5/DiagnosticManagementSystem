using DiagnosticCenterBillMgtWebApp.Manager;
using DiagnosticCenterBillMgtWebApp.Model;
using System;
using System.Linq;

namespace DiagnosticCenterBillMgtWebApp.UI
{
    public partial class Payment : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected void searchButton_Click(object sender, EventArgs e)
        {
            if (billNoTextBox.Text == string.Empty && mobileNoTextBox.Text == string.Empty)
            {
                messageBox.InnerHtml = GetMessage("You have to enter bill number and/or mobile number", "danger");
                billNoTextBox.Focus();
            }
            else
            {
                PatientModel patient = null;
                if (billNoTextBox.Text != string.Empty)
                {
                    patient = new PatientManager().GetPatientsByBillNo(billNoTextBox.Text);
                }
                else
                {
                    patient = new PatientManager().GetPatientsByContact(mobileNoTextBox.Text);
                }
                if (patient != null)
                {
                    amountTextBox.Text = Math.Round((decimal)patient.Tests.AsEnumerable().Sum(row => row.TestFee), 2).ToString();
                    dueDateTextBox.Text = patient.TestDate.ToString();
                    recordPanel.Visible = true;
                    messageBox.InnerHtml = GetMessage("Patient record found!", "success");
                    patientIdHiddenField.Value = patient.ID.ToString();

                    if (patient.PaymentStatus > 0)
                    {
                        payButton.Enabled = false;
                        payButton.CssClass = "btn btn-block btn-default";
                        payButton.Text = "P A I D";
                    }
                    else
                    {
                        payButton.Enabled = true;
                        payButton.CssClass = "btn btn-block btn-success";
                        payButton.Text = "P A Y";
                    }
                }
                else
                {
                    recordPanel.Visible = false;
                    messageBox.InnerHtml = GetMessage("No patient record found for given bill/contact number", "info");
                }
            }
        }

        protected void payButton_Click(object sender, EventArgs e)
        {
            if (new PatientManager().UpdatePayment(Convert.ToInt64(patientIdHiddenField.Value)) > 0)
            {
                messageBox.InnerHtml = GetMessage("Patient's payment updated successfully!", "success");
                payButton.Enabled = false;
                payButton.CssClass = "btn btn-block btn-default";
                payButton.Text = "P A I D";
            }
            else
            {
                messageBox.InnerHtml = GetMessage("Patient's payment update failed!", "danger");
                payButton.Enabled = true;
                payButton.CssClass = "btn btn-block btn-success";
                payButton.Text = "P A Y";
            }
        }

        private string GetMessage(string message, string type)
        {
            return string.Format("<div class='alert alert-{0}'>{1}</div>", type, message);
        }
    }
}