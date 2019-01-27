using DiagnosticCenterBillMgtWebApp.Manager;
using DiagnosticCenterBillMgtWebApp.Model;
using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;

namespace DiagnosticCenterBillMgtWebApp.UI
{
    public partial class Test : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ShowTests();
                ShowTestTypes();
            }
        }

        protected void addButton_Click(object sender, EventArgs e)
        {
            if (new TestManager().IsExistsByName(testTextBox.Text) > 0)
            {
                messageBox.InnerHtml = GetMessage("Test record exists. Try another.", "danger");
            }
            else
            {
                if (new TestManager().AddTest(new TestModel(0, testTextBox.Text.Trim().ToString(),
                                            Convert.ToDecimal(feeTextBox.Text),
                                            Convert.ToInt32(testTypeDropdown.SelectedItem.Value),
                                            testTypeDropdown.SelectedItem.Text)) > 0)
                {
                    messageBox.InnerHtml = GetMessage("Test added successfully!", "success");
                    testTextBox.Text = feeTextBox.Text = string.Empty;
                    testTypeDropdown.ClearSelection();
                    ShowTests();
                }
                else
                {
                    messageBox.InnerHtml = GetMessage("Test adding failed!", "danger");
                }
            }
        }

        public void ShowTests()
        {
            List<TestModel> tests = new TestManager().GetTests();
            if (tests == null)
            {
                GridView.DataSource = null;
                GridView.EmptyDataText = GetMessage("No Test Found!", "info");
            }
            else
            {
                GridView.DataSource = tests;
            }
            GridView.DataBind();
        }

        public void ShowTestTypes()
        {
            List<TestTypeModel> testTypes = new TestTypeManager().GetTestTypes();
            if (testTypes == null)
            {
                testTypeDropdown.DataSource = null;
            }
            else
            {
                testTypeDropdown.DataSource = testTypes;
                testTypeDropdown.DataTextField = "TypeName";
                testTypeDropdown.DataValueField = "TypeID";
            }
            testTypeDropdown.DataBind();
            testTypeDropdown.Items.Insert(0, new ListItem("--SELECT--", string.Empty));
        }

        private string GetMessage(string message, string type)
        {
            return string.Format("<div class='alert alert-{0}'>{1}</div>", type, message);
        }
    }
}