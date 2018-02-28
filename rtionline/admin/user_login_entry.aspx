<%@ Page Title="" Language="C#" MasterPageFile="~/admin_master.master" AutoEventWireup="true" CodeFile="user_login_entry.aspx.cs" Inherits="user_login_entry" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CPH_MasterLogin" runat="Server">
    <style type="text/css">
        td, th {
            padding: 2px !important;
        }
    </style>

    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="col-sm-12">

                <div class="panel panel-default">
                    <div class="panel-heading">
                        <h3 class="panel-title"><%=Resources.Resource.CreateNewUserIdandPassword %> Create New User Id and Password</h3>
                        <h6><%=Resources.Resource.PasswordPolicy %> </h6>
                    </div>

                    <div class="panel-body">

                        <div class="form-horizontal">
                            <div class="form-group">
                                <div class="col-sm-4">
                                    <asp:HiddenField runat="server" ID="h_roll_id" />
                                    <label><%=Resources.Resource.District %>  <span class="text-danger">*</span> </label>
                                    <asp:DropDownList ID="DDL_District" OnSelectedIndexChanged="DDL_District_SelectedIndexChanged" AutoPostBack="true" CssClass="form-control" runat="server"></asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="rfv_district" ControlToValidate="DDL_District" InitialValue="0" ValidationGroup="vg" CssClass="alert-danger" runat="server" ErrorMessage="required"></asp:RequiredFieldValidator>
                                </div>

                                <div class="col-sm-4">
                                    <label><%=Resources.Resource.BaseDepartmentName %> <span class="text-danger">*</span> </label>
                                    <asp:DropDownList ID="DDL_Department" OnSelectedIndexChanged="DDL_Department_SelectedIndexChanged" AutoPostBack="true" CssClass="form-control" runat="server"></asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="rfv_Department" ControlToValidate="DDL_Department" InitialValue="0" ValidationGroup="vg" CssClass="alert-danger" runat="server" ErrorMessage="required"></asp:RequiredFieldValidator>
                                </div>

                                <div class="col-sm-4">
                                    <label><%=Resources.Resource.Office %><span class="text-danger">*</span> </label>
                                    <asp:DropDownList ID="DDL_Office" OnSelectedIndexChanged="DDL_Office_SelectedIndexChanged" AutoPostBack="true" CssClass="form-control" runat="server"></asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="rfv_Office" ControlToValidate="DDL_Office" InitialValue="0" ValidationGroup="vg" CssClass="alert-danger" runat="server" ErrorMessage="required"></asp:RequiredFieldValidator>
                                </div>

                            </div>

                            <div class="form-group">
                                <div class="col-sm-4">
                                    <label> <%=Resources.Resource.EmployeeName %>  <span class="text-danger">*</span> </label>
                                    <asp:DropDownList ID="ddl_employee"  AutoPostBack="true" CssClass="form-control" runat="server"></asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ControlToValidate="ddl_employee" InitialValue="0" ValidationGroup="vg" CssClass="alert-danger" runat="server" ErrorMessage="required"></asp:RequiredFieldValidator>
                                </div>
                                <div class="col-sm-4">
                                    <label><%=Resources.Resource.UserID %>  <span class="text-danger">*</span> </label>
                                    <asp:TextBox ID="txt_user_name" CssClass="form-control" runat="server" MaxLength="30"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" Display="Dynamic" ValidationGroup="vg" ControlToValidate="txt_user_name" CssClass="alert-danger" runat="server" ErrorMessage="required"></asp:RequiredFieldValidator>
                                </div>

                                <div class="col-sm-4">
                                    <label><%=Resources.Resource.Password %> <span class="text-danger">*</span> </label>
                                    <asp:TextBox ID="txt_password" TextMode="Password" CssClass="form-control " runat="server" MaxLength="30"></asp:TextBox>

                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator7" Display="Dynamic" ValidationGroup="vg" ControlToValidate="txt_password" CssClass="alert-danger" runat="server" ErrorMessage="required"></asp:RequiredFieldValidator>
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator1" Display="Dynamic" ValidationGroup="vg" CssClass="alert-danger" ControlToValidate="txt_password" runat="server"
                                         ValidationExpression="^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?!@$%^&*-]).{8,15}$" ErrorMessage="Minimum 8 characters at least 1 Uppercase Alphabet, 1 Lowercase Alphabet, 1 Number and 1 Special Character:"></asp:RegularExpressionValidator>
                                </div>

                            </div>

                            <div class="clear"></div>
                            <div class="form-group">
                                <div class="col-sm-4">
                                    <label><%=Resources.Resource.ConfirmPassword %>    <span class="text-danger">*</span> </label>
                                    <asp:TextBox ID="txt_confirm_pass" TextMode="Password" CssClass="form-control" runat="server" MaxLength="30"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator8" Display="Dynamic" ValidationGroup="vg" ControlToValidate="txt_confirm_pass" CssClass="alert-danger" runat="server" ErrorMessage="required"></asp:RequiredFieldValidator>

                                    <asp:CompareValidator ID="CompareValidator1" ControlToValidate="txt_confirm_pass" ValidationGroup="vg" ControlToCompare="txt_password" runat="server" ErrorMessage="CompareValidator"></asp:CompareValidator>

                                </div>
                                <div class="col-sm-4">
                                    <label><%=Resources.Resource.Active %>   <span class="text-danger">*</span> </label>
                                    <asp:DropDownList ID="ddl_active" CssClass="form-control" runat="server">
                                        <asp:ListItem Value="Y">Active</asp:ListItem>
                                        <asp:ListItem Value="N">Deactive</asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator5" ControlToValidate="ddl_active" InitialValue="0" ValidationGroup="vg" CssClass="alert-danger" runat="server" ErrorMessage="required"></asp:RequiredFieldValidator>
                                </div>
                                <div class="col-sm-4">
                                    <label><%=Resources.Resource.ChangePassword %> <span class="text-danger">*</span> </label>
                                    <asp:DropDownList ID="ddl_change_pass" CssClass="form-control" runat="server">
                                        <asp:ListItem Value="Y">Yes</asp:ListItem>
                                        <asp:ListItem Value="N">No</asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ControlToValidate="ddl_change_pass" InitialValue="0" ValidationGroup="vg" CssClass="alert-danger" runat="server" ErrorMessage="required"></asp:RequiredFieldValidator>
                                </div>
                            </div>


                            <div class="clear"></div>

                            <div class="form-group">
                        
                                
                                <div class="col-sm-4 text-right">
                                    <br />


                                    <asp:Button ID="btn_save" CssClass="button button-3d button-black nomargin" OnClick="btn_save_Click" ValidationGroup="vg" runat="server" Text="<%$Resources:Resource, Save %>" />
                                  
                                     </div>
                            </div>



                        </div>
                    </div>
                </div>
            </div>
            <div class="col-sm-12">

              
                    <asp:Label ID="lbl_count" runat="server" CssClass="text-primary bold"></asp:Label>
              
                <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="false" Width="100%" CssClass="table table-condensed table-hover"
                    EmptyDataText="No Record Found." EmptyDataRowStyle-CssClass="text-center"  DataKeyNames="LoginID" AllowPaging="true" PageSize="10"
                    OnPageIndexChanging="GridView1_PageIndexChanging">
                     <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                            <HeaderStyle BackColor="#507CD1" HorizontalAlign="Center" VerticalAlign="Middle" Font-Bold="True" ForeColor="White" />
                    <Columns>
                        <asp:TemplateField HeaderText="<%$Resources:Resource, SNo %>">
                            <ItemTemplate>
                                <%#Container.DataItemIndex+1%>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="left" />
                            <HeaderStyle HorizontalAlign="Left" />
                        </asp:TemplateField>
                      
                      <%--  <asp:BoundField DataField="LoginID" HeaderText=" Login Id" SortExpression="LoginID" />--%>
                       
                        <asp:BoundField DataField="Name_en" HeaderText="<%$Resources:Resource, EmployeeName %>" SortExpression="Name_en" />
                           <asp:BoundField DataField="UserID" HeaderText="<%$Resources:Resource, UserID %>" SortExpression="UserID" />
                        <asp:BoundField DataField="PasswordChange" HeaderText="<%$Resources:Resource, ChangePassword %>" SortExpression="PasswordChange" />
                        <asp:BoundField DataField="Active" HeaderText="<%$Resources:Resource, Active %>" SortExpression="Active" />
                       
                      
                    </Columns>
                </asp:GridView>
            </div>
        </ContentTemplate>

    </asp:UpdatePanel>





</asp:Content>

