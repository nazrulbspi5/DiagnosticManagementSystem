<%@ Page Title="" Language="C#" MasterPageFile="~/UI/Diagnostic.Master" AutoEventWireup="true" CodeBehind="Payment.aspx.cs" Inherits="DiagnosticCenterBillMgtWebApp.UI.Payment" %>

<asp:Content ID="Content_head" ContentPlaceHolderID="ContentPlaceHolder_head" runat="server">
</asp:Content>
<asp:Content ID="Content_body" ContentPlaceHolderID="ContentPlaceHolder_body" runat="server">
    <div class="display-table full-width-height margin-top-50">
        <div class="display-table-cell">
            <asp:Panel ID="Panel" GroupingText="Payment" runat="server">
                <div class="row">
                    <div class="col-sm-6 col-sm-offset-3 panel panel-default padding-10">
                        <div id="messageBox" runat="server"></div>
                        <div class="form-group">
                            <div class="input-group">
                                <span class="input-group-addon">Search By:</span>
                                <asp:TextBox CssClass="form-control" placeholder="Bill no..." ID="billNoTextBox" ClientIDMode="Static" runat="server"></asp:TextBox>
                                <span class="input-group-addon">OR:</span>
                                <asp:TextBox CssClass="form-control" placeholder="Mobile no..." ID="mobileNoTextBox" ClientIDMode="Static" runat="server"></asp:TextBox>
                                <span class="input-group-btn">
                                    <asp:Button CssClass="btn btn-block btn-success" ID="searchButton" ClientIDMode="Static" runat="server" Text="SEARCH" OnClick="searchButton_Click" />
                                </span>
                            </div>
                        </div>
                    </div>
                </div>

                <div class="row">
                    <div class="col-sm-6 col-sm-offset-3 panel panel-default padding-10">
                        <asp:Panel ID="recordPanel" Visible="false" runat="server">
                            <div class="form-group">
                                <div class="row">
                                    <div class="col-sm-4 text-left">Amount:</div>
                                    <div class="col-sm-8">
                                        <div class="input-group">
                                            <asp:TextBox CssClass="form-control text-right" placeholder="Amount" Enabled="false" ID="amountTextBox" ClientIDMode="Static" runat="server"></asp:TextBox>
                                            <span class="input-group-addon">BDT</span>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="form-group">
                                <div class="row">
                                    <div class="col-sm-4 text-left">Due Date:</div>
                                    <div class="col-sm-8">
                                        <asp:TextBox CssClass="form-control" placeholder="Due date" Enabled="false" ID="dueDateTextBox" ClientIDMode="Static" runat="server"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                            <div class="form-group">
                                <asp:HiddenField ID="patientIdHiddenField" runat="server" />
                                <asp:Button CssClass="btn btn-block btn-success" ID="payButton" ClientIDMode="Static" runat="server" Text="PAY" OnClick="payButton_Click" />
                            </div>
                        </asp:Panel>
                    </div>
                </div>
            </asp:Panel>
        </div>
    </div>
</asp:Content>

<asp:Content ID="Content_footer" ContentPlaceHolderID="ContentPlaceHolder_footer" runat="server">
</asp:Content>