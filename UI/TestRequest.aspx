<%@ Page Title="" Language="C#" MasterPageFile="~/UI/Diagnostic.Master" AutoEventWireup="true" CodeBehind="TestRequest.aspx.cs" Inherits="DiagnosticCenterBillMgtWebApp.UI.TestRequest" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=12.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>

<asp:Content ID="Content_head" ContentPlaceHolderID="ContentPlaceHolder_head" runat="server">
</asp:Content>
<asp:Content ID="Content_body" ContentPlaceHolderID="ContentPlaceHolder_body" runat="server">
    <div class="display-table full-width-height margin-top-50">
        <div class="display-table-cell">
            <asp:Panel ID="Panel" GroupingText="Request Test" runat="server">
                <div class="row">
                    <div class="col-sm-6 col-sm-offset-3">
                        <div id="messageBox" runat="server"></div>
                        <div class="form-group">
                            <div class="row">
                                <div class="col-sm-4 text-left">Patient Name:</div>
                                <div class="col-sm-8">
                                    <asp:TextBox CssClass="form-control" placeholder="Patient Name" required="required" ID="patientNameTextBox" ClientIDMode="Static" runat="server"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                        <div class="form-group">
                            <div class="row">
                                <div class="col-sm-4 text-left">Date of Birth:</div>
                                <div class="col-sm-8">
                                    <asp:TextBox CssClass="form-control" placeholder="mm/dd/yyyy" required="required" ID="dateOfBirthTextBox" ClientIDMode="Static" runat="server"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                        <div class="form-group">
                            <div class="row">
                                <div class="col-sm-4 text-left">Contact No:</div>
                                <div class="col-sm-8">
                                    <asp:TextBox CssClass="form-control" placeholder="Mobile" pattern="[0-9]{11}" MaxLength="11" required="required" ID="mobileTextBox" ClientIDMode="Static" runat="server"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                        <div class="form-group">
                            <div class="row">
                                <div class="col-sm-4 text-left">Select Test:</div>
                                <div class="col-sm-8">
                                    <div class="row">
                                        <div class="col-sm-8">
                                            <asp:DropDownList CssClass="form-control" ID="testDropdown" ClientIDMode="Static" runat="server" AutoPostBack="true" OnSelectedIndexChanged="testDropdown_SelectedIndexChanged"></asp:DropDownList>
                                        </div>
                                        <div class="col-sm-4">
                                            <asp:TextBox CssClass="form-control text-right" Enabled="false" placeholder="Fee" ID="feeTextBox" ClientIDMode="Static" runat="server"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="form-group">
                            <asp:Button CssClass="btn btn-block btn-success" ID="addButton" ClientIDMode="Static" runat="server" Text="ADD" OnClientClick="javascript:return ValidateTestSelection();" OnClick="addButton_Click" />
                        </div>
                    </div>
                    <div class="col-sm-8 col-sm-offset-2">
                        <asp:Panel ID="recordPanel" runat="server" Visible="false">
                            <asp:GridView ID="GridView" ClientIDMode="Static" CssClass="table table-striped table-bordered table-hover table-responsive" AutoGenerateColumns="false" runat="server">
                                <Columns>
                                    <asp:TemplateField HeaderText="SL No." HeaderStyle-Font-Bold="true" HeaderStyle-CssClass="text-center">
                                        <ItemTemplate>
                                            <%# Container.DataItemIndex + 1 %>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Test" HeaderStyle-Font-Bold="true" HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-left">
                                        <ItemTemplate>
                                            <%#Eval("TestName") %>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Fee" HeaderStyle-Font-Bold="true" HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-right">
                                        <ItemTemplate>
                                            <%#Eval("TestFee") %>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                            <div class="row">
                                <div class="col-sm-4 col-sm-offset-8 text-right">
                                    <div class="input-group">
                                        <span class="input-group-addon">Total</span>
                                        <asp:TextBox CssClass="form-control text-right" Enabled="false" ClientIDMode="Static" ID="totalTextBox" runat="server"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-sm-4 col-sm-offset-8">
                                    <asp:Button CssClass="btn btn-block btn-primary" ID="saveButton" ClientIDMode="Static" runat="server" Text="Save" OnClick="saveButton_Click" />
                                </div>
                            </div>
                        </asp:Panel>
                    </div>
                    <div class="col-sm-12 text-center">
                        <asp:ScriptManager ID="ScriptManager" runat="server"></asp:ScriptManager>
                        <rsweb:ReportViewer ID="ReportViewer" CssClass="width100" runat="server" ClientIDMode="Static" Visible="false" Font-Names="Verdana" Font-Size="8pt" WaitMessageFont-Names="Verdana" WaitMessageFont-Size="14pt">
                            <LocalReport ReportPath="Reports\Report.rdlc"></LocalReport>
                        </rsweb:ReportViewer>
                    </div>
                </div>
            </asp:Panel>
        </div>
    </div>
</asp:Content>

<asp:Content ID="Content_footer" ContentPlaceHolderID="ContentPlaceHolder_footer" runat="server">
    <script type="text/javascript">
        function ValidateTestSelection() {
            if ($("#testDropdown option:selected").val() == "") {
                alert("You have to select a test first.");
                return false;
            }
            return true;
        }
    </script>
</asp:Content>