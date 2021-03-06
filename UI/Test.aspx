﻿<%@ Page Title="" Language="C#" MasterPageFile="~/UI/Diagnostic.Master" AutoEventWireup="true" CodeBehind="Test.aspx.cs" Inherits="DiagnosticCenterBillMgtWebApp.UI.Test" %>

<asp:Content ID="Content_head" ContentPlaceHolderID="ContentPlaceHolder_head" runat="server">
</asp:Content>
<asp:Content ID="Content_body" ContentPlaceHolderID="ContentPlaceHolder_body" runat="server">
    <div class="display-table full-width-height margin-top-50">
        <div class="display-table-cell">
            <asp:Panel ID="Panel" GroupingText="Test Setup" runat="server">
                <div class="row">
                    <div class="col-sm-6 col-sm-offset-3">
                        <div id="messageBox" runat="server"></div>
                        <div class="form-group">
                            <div class="row">
                                <div class="col-sm-4 text-left">
                                    <label for="testTextBox">Test Name:</label></div>
                                <div class="col-sm-8">
                                    <asp:TextBox CssClass="form-control" required="required" ID="testTextBox" ClientIDMode="Static" runat="server"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                        <div class="form-group">
                            <div class="form-group">
                                <div class="row">
                                    <div class="col-sm-4 text-left">
                                        <label for="feeTextBox">Fee:</label></div>
                                    <div class="col-sm-8">
                                        <div class="input-group">
                                            <asp:TextBox CssClass="form-control" required="required" ID="feeTextBox" ClientIDMode="Static" runat="server" TextMode="Number" min="1" step="0.1"></asp:TextBox>
                                            <span class="input-group-addon">BDT</span>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="form-group">
                            <div class="row">
                                <div class="col-sm-4 text-left">
                                    <label for="testTypeDropdown">Test Type:</label></div>
                                <div class="col-sm-8">
                                    <asp:DropDownList CssClass="form-control" required="required" ID="testTypeDropdown" ClientIDMode="Static" runat="server"></asp:DropDownList>
                                </div>
                            </div>
                        </div>
                        <div class="form-group">
                            <asp:Button CssClass="btn btn-block btn-success" ID="addButton" ClientIDMode="Static" runat="server" Text="ADD" OnClick="addButton_Click" />
                        </div>
                    </div>
                    <div class="col-sm-8 col-sm-offset-2">
                        <asp:GridView ID="GridView" ClientIDMode="Static" CssClass="table table-striped table-bordered table-hover table-responsive" AutoGenerateColumns="false" runat="server">
                            <Columns>
                                <asp:TemplateField HeaderText="SL No." HeaderStyle-CssClass="text-center" HeaderStyle-Font-Bold="true">
                                    <ItemTemplate>
                                        <%# Container.DataItemIndex + 1 %>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Test Name" HeaderStyle-CssClass="text-center" HeaderStyle-Font-Bold="true" ItemStyle-CssClass="text-left">
                                    <ItemTemplate>
                                        <%#Eval("TestName") %>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Test Fee" HeaderStyle-CssClass="text-center" HeaderStyle-Font-Bold="true" ItemStyle-CssClass="text-right">
                                    <ItemTemplate>
                                        <%# Math.Round((decimal)Eval("TestFee"), 2) %>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Test Type" HeaderStyle-CssClass="text-center" HeaderStyle-Font-Bold="true" ItemStyle-CssClass="text-left">
                                    <ItemTemplate>
                                        <%#Eval("TypeName") %>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </div>
                </div>
            </asp:Panel>
        </div>
    </div>
</asp:Content>

<asp:Content ID="Content_footer" ContentPlaceHolderID="ContentPlaceHolder_footer" runat="server">
</asp:Content>