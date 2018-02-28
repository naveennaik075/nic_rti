<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="employee_erole.aspx.cs" Inherits="employee_erole" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">
    <div class="panel panel-success">
        <div class="panel-heading">
            <div class="panel-title">
                <div class="col-sm-6">
                <%=Resources.Resource.SelectYourDesignation %> </div>
                 <div class="col-sm-4"></div>
                              <asp:Button ID="btnlogout" runat="server" CssClass="btn btn-link text-primary" OnClick="btnlogout_Click" Text="Logout" Font-Bold="true" />
            </div>
          
        </div>
        <asp:UpdatePanel ID="upd1" runat="server">
            <ContentTemplate>
                <div class="panel-body">
                    <div class="form-horizontal">
                        <div class="form-group">
                            <div id="div_roll" runat="server" visible="false">
                                <div class="col-sm-3">
                                    <asp:Label ID="lbl_title" runat="server" Text="<%$Resources:Resource, SelectYourRole %>"></asp:Label>
                                    
                                </div>
                                <div class="col-sm-3">
                                    <asp:DropDownList ID="ddl_role" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddl_role_SelectedIndexChanged" ValidationGroup="vg" CssClass="form-control">
                                        <asp:ListItem Value="0" Text="----Select----"></asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="rfv_role" runat="server" Display="Dynamic" ControlToValidate="ddl_role" SetFocusOnError="true" ValidationGroup="vg" CssClass="alert-danger" ErrorMessage="Please Select Roll"></asp:RequiredFieldValidator>
                                </div>
                            </div>
                            <div class="col-sm-3">
                                <asp:Label ID="lbl_designation" runat="server" Text="<%$Resources:Resource, SelectDesignation %>">
                              
                                </asp:Label>
                            </div>
                            <div class="col-sm-3">
                                <asp:DropDownList ID="ddl_designatiom" runat="server" ValidationGroup="vg" CssClass="form-control">
                                    <asp:ListItem Value="0" Text="----Select----"></asp:ListItem>
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="rfv_designation" runat="server" Display="Dynamic" ControlToValidate="ddl_designatiom" SetFocusOnError="true" ValidationGroup="vg" CssClass="alert-danger" ErrorMessage="Please Select Designation"></asp:RequiredFieldValidator>
                            </div>
                        </div>
                    </div>

                    <div class="row center">
                        <asp:Button ID="btn_submit" runat="server" OnClick="btn_submit_Click" ValidationGroup="vg" Text="<%$Resources:Resource, Submit %>" CssClass="button button-3d button-black nomargin btn btn-primary" />
                    </div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
</asp:Content>
