<%@ Page Title="" Language="C#" MasterPageFile="~/UI/Diagnostic.Master" AutoEventWireup="true" CodeBehind="TestType.aspx.cs" Inherits="DiagnosticCenterBillMgtWebApp.UI.TestType" %>

<asp:Content ID="Content_head" ContentPlaceHolderID="ContentPlaceHolder_head" runat="server">
</asp:Content>
<asp:Content ID="Content_body" ContentPlaceHolderID="ContentPlaceHolder_body" runat="server">
    <div class="display-table full-width-height">
        <div class="display-table-cell">
            <asp:Panel ID="Panel" GroupingText="Test Type Setup" runat="server">
                <div class="row">
                    <div class="col-sm-6 col-sm-offset-3">
                        <div id="messageBox" runat="server"></div>
                        <div class="form-group">
                            <div class="input-group">
                                <span class="input-group-addon">Test Type:</span>
                                <asp:TextBox CssClass="form-control" required="required" ID="typeNameTextBox" ClientIDMode="Static" runat="server"></asp:TextBox>
                                <span class="input-group-btn">
                                    <asp:Button CssClass="btn btn-success" ID="addButton" ClientIDMode="Static" runat="server" Text="ADD" OnClick="addButton_Click" />
                                </span>
                            </div>
                        </div>
                    </div>
                    <div class="col-sm-6 col-sm-offset-3">
                        <asp:GridView ID="GridView" ClientIDMode="Static" CssClass="table table-striped table-bordered table-hover table-responsive" AutoGenerateColumns="false" runat="server">
                            <Columns>
                                <asp:TemplateField HeaderText="SL No." HeaderStyle-CssClass="text-center" HeaderStyle-Font-Bold="true">
                                    <ItemTemplate>
                                        <%# Container.DataItemIndex + 1 %>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Type Name" HeaderStyle-CssClass="text-center" HeaderStyle-Font-Bold="true" ItemStyle-CssClass="text-left">
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