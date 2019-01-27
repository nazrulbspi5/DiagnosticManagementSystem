<%@ Page Title="" Language="C#" MasterPageFile="~/UI/Diagnostic.Master" AutoEventWireup="true" CodeBehind="TestWiseReport.aspx.cs" Inherits="DiagnosticCenterBillMgtWebApp.UI.TestWiseReport" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=12.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>

<asp:Content ID="Content_head" ContentPlaceHolderID="ContentPlaceHolder_head" runat="server">
</asp:Content>
<asp:Content ID="Content_body" ContentPlaceHolderID="ContentPlaceHolder_body" runat="server">
    <div class="display-table full-width-height margin-top-50">
        <div class="display-table-cell">
            <asp:Panel ID="Panel" GroupingText="Test Wise Report" runat="server">
                <div class="row">
                    <div class="col-sm-6 col-sm-offset-3">
                        <div id="messageBox" runat="server"></div>
                        <div class="form-group">
                            <div class="input-group">
                                <span class="input-group-addon">Date From</span>
                                <asp:TextBox CssClass="form-control datepicker-field" placeholder="MM\DD\YYYY" required="required" ID="fromDateTextBox" ClientIDMode="Static" runat="server"></asp:TextBox>
                                <span class="input-group-addon">To</span>
                                <asp:TextBox CssClass="form-control datepicker-field" placeholder="MM\DD\YYYY" required="required" ID="toDateTextBox" ClientIDMode="Static" runat="server"></asp:TextBox>
                                <span class="input-group-btn">
                                    <asp:Button CssClass="btn btn-block btn-success" ID="searchBox" ClientIDMode="Static" runat="server" Text="SEARCH" OnClick="searchBox_Click" />
                                </span>
                            </div>
                        </div>
                    </div>

                    <div class="col-sm-6 col-sm-offset-3">
                        <asp:Panel ID="recordPanel" runat="server" Visible="false">
                            <asp:GridView ID="GridView" ClientIDMode="Static" CssClass="table table-striped table-bordered table-hover table-responsive" AutoGenerateColumns="false" runat="server">
                                <Columns>
                                    <asp:TemplateField HeaderText="S/L No" HeaderStyle-CssClass="text-center" HeaderStyle-Font-Bold="true">
                                        <ItemTemplate>
                                            <%# Container.DataItemIndex + 1 %>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Test Name" HeaderStyle-CssClass="text-center" HeaderStyle-Font-Bold="true" ItemStyle-CssClass="text-left">
                                        <ItemTemplate>
                                            <%#Eval("Name") %>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Total Test" HeaderStyle-CssClass="text-center" HeaderStyle-Font-Bold="true">
                                        <ItemTemplate>
                                            <%#Eval("TotalCount") %>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Total Amount" HeaderStyle-CssClass="text-center" HeaderStyle-Font-Bold="true" ItemStyle-CssClass="text-right">
                                        <ItemTemplate>
                                            <%#Math.Round((decimal)Eval("TotalAmount"),2) %>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                            <div class="row">
                                <div class="col-sm-6 col-sm-push-6 text-center">
                                    <div class="input-group">
                                        <span class="input-group-addon">Total</span>
                                        <asp:TextBox CssClass="form-control text-right" Enabled="false" ClientIDMode="Static" ID="totalTextBox" runat="server"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-sm-6 col-sm-pull-6 text-center">
                                    <asp:Button CssClass="btn btn-block btn-primary" ID="pdfButton" ClientIDMode="Static" runat="server" Text="PDF" OnClick="pdfButton_Click" />
                                </div>
                            </div>
                        </asp:Panel>
                    </div>
                    <div class="col-sm-12 text-center">
                        <asp:ScriptManager ID="ScriptManager" runat="server"></asp:ScriptManager>
                        <rsweb:ReportViewer ID="ReportViewer" CssClass="width100" runat="server" ClientIDMode="Static" Visible="false" Font-Names="Verdana" Font-Size="8pt" WaitMessageFont-Names="Verdana" WaitMessageFont-Size="14pt">
                            <LocalReport ReportPath="Reports\TestWiseReport.rdlc"></LocalReport>
                        </rsweb:ReportViewer>
                    </div>
                </div>
            </asp:Panel>
        </div>
    </div>
</asp:Content>

<asp:Content ID="Content_footer" ContentPlaceHolderID="ContentPlaceHolder_footer" runat="server">
</asp:Content>