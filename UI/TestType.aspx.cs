using DiagnosticCenterBillMgtWebApp.Manager;
using DiagnosticCenterBillMgtWebApp.Model;
using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;

namespace DiagnosticCenterBillMgtWebApp.UI
{
    public partial class TestType : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
                ShowTestTypes();
        }

        protected void addButton_Click(object sender, EventArgs e)
        {
            if (new TestTypeManager().IsExistsByName(typeNameTextBox.Text) > 0)
            {
                messageBox.InnerHtml = GetMessage("Test Type Exists. Try again.", "danger");
            }
            else
            {
                if (new TestTypeManager().AddTestType(new TestTypeModel(0, typeNameTextBox.Text.Trim().ToString())) > 0)
                {
                    messageBox.InnerHtml = GetMessage("Test type added successfully!", "success");
                    typeNameTextBox.Text = string.Empty;
                }
                else
                {
                    messageBox.InnerHtml = GetMessage("Test type adding failed!", "danger");
                }
            }

            ShowTestTypes();
        }

        public void ShowTestTypes()
        {
            List<TestTypeModel> testTypes = new TestTypeManager().GetTestTypes();
            if (testTypes == null)
            {
                GridView.DataSource = null;
                GridView.EmptyDataText = GetMessage("No Test Type Found!", "info");
            }
            else
            {
                GridView.DataSource = testTypes;
            }
            GridView.DataBind();
        }

        private string GetMessage(string message, string type)
        {
            return string.Format("<div class='alert alert-{0}'>{1}</div>", type, message);
        }
    }
}