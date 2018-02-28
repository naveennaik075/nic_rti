<%@ Page Title="" Language="C#" MasterPageFile="~/admin_master.master" AutoEventWireup="true" CodeFile="change_permission.aspx.cs" Inherits="dio_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CPH_MasterLogin" runat="Server">
    <div class="form-horizontal">
        <div class="panel-info">
            <div class="panel-heading">
                <div class="panel-title">
                    Change Permission Provided To User
                </div>
            </div>
            <div class="panel-body">
                <asp:UpdatePanel ID="udp" runat="server">
                    <ContentTemplate>
                        <div class="form-group">
                            <div class="col-sm-2">
                                <asp:HiddenField runat="server" ID="h_district_id" />
                                <asp:HiddenField runat="server" ID="h_offfice_code" />
                                <asp:HiddenField runat="server" ID="h_department_code" />
                                <asp:HiddenField runat="server" ID="h_roll_id" />
                                <asp:Label ID="lbl_department1" runat="server" Text="Department Name:"></asp:Label>
                                <span class="text-danger">*</span>
                            </div>
                            <div class="col-sm-4">
                                <asp:DropDownList ID="ddl_department1" runat="server" AutoPostBack="true" ValidationGroup="vg" CssClass="form-control" OnSelectedIndexChanged="ddl_department1_SelectedIndexChanged">
                                    <asp:ListItem Value="0" Text="-----Select Department------"></asp:ListItem>
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="rfv_department1" runat="server" ControlToValidate="ddl_department1" CssClass="text-danger" Display="Dynamic" ErrorMessage=" Please Select Department" SetFocusOnError="true" ValidationGroup="vg" InitialValue="0"></asp:RequiredFieldValidator>
                            </div>
                            <div class="col-sm-2">
                                <asp:Label ID="lbl_district1" runat="server" Text="District:"></asp:Label>
                                <span class="text-danger">*</span>
                            </div>
                            <div id="ddl_dist_view" runat="server">
                                <div class="col-sm-4">
                                    <asp:DropDownList ID="ddl_district1" runat="server" AutoPostBack="true" ValidationGroup="vg" CssClass="form-control" OnSelectedIndexChanged="ddl_district1_SelectedIndexChanged">
                                        <asp:ListItem Value="0" Text="-------Select District------"></asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="rfv_district1" runat="server" ControlToValidate="ddl_district1" ErrorMessage="District is Required" Display="Dynamic" CssClass="alert-danger" SetFocusOnError="true" ValidationGroup="vg" InitialValue="0"></asp:RequiredFieldValidator>
                                </div>
                            </div>
                            <div id="txt_dist_view" runat="server">
                                <div class="col-sm-4">
                                    <asp:TextBox ID="txt_district1" runat="server" CssClass="form-control" ReadOnly="True" Visible="False"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                        <div class="form-group">
                            <div class="col-sm-2">
                                <asp:Label ID="lbl_Employee1" runat="server" Text="Employee:"></asp:Label>
                                <span class="text-danger">*</span>
                            </div>
                            <div class="col-sm-4">
                                <asp:DropDownList ID="ddl_employee1" runat="server" AutoPostBack="true" ValidationGroup="vg" CssClass="form-control" OnSelectedIndexChanged="ddl_employee1_SelectedIndexChanged">
                                    <asp:ListItem Value="0" Text="------Select Employee------"></asp:ListItem>
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="rfv_employee1" runat="server" InitialValue="0" ControlToValidate="ddl_employee1" ErrorMessage="Employee is Required" Display="Dynamic" CssClass="alert-danger" SetFocusOnError="true" ValidationGroup="vg"></asp:RequiredFieldValidator>
                            </div>
                        </div>
                        <div id="div_permission" runat="server" visible="false">
                            <div class="panel panel-success">
                                <div class="panel-heading">
                                    <div class="panel-title">PERMISSION </div>
                                </div>
                                <div class="panel-body">
                                    <div class="form-group">
                                        <div class="col-sm-3">
                                            <asp:Label ID="lbl_office" runat="server" Text="Permission for Dispose :"></asp:Label> 
                                            
                                        </div>
                                        <div class="col-sm-3">
                                            <asp:RadioButtonList ID="rdb_dispose" runat="server" CssClass="form-control" RepeatDirection="Horizontal" ValidationGroup="vg">
                                                <asp:ListItem Value="Y" Text="Yes"></asp:ListItem>
                                                <asp:ListItem Value="N" Text="No"></asp:ListItem>
                                            </asp:RadioButtonList>
                                            <asp:RequiredFieldValidator ID="rfv_dispose" runat="server" ValidationGroup="vg" ErrorMessage="Select Permission" Display="Dynamic" ControlToValidate="rdb_dispose" CssClass="alert-danger"></asp:RequiredFieldValidator>
                                        </div>
                                        <div class="col-sm-3">
                                            <asp:Label ID="Label1" runat="server" Text="Permission for Reject:"></asp:Label> 
                                            
                                        </div>
                                        <div class="col-sm-3">
                                            <asp:RadioButtonList runat="server" ID="rdb_reject" CssClass="form-control" RepeatDirection="Horizontal" ValidationGroup="vg">
                                                <asp:ListItem Text="Yes " Value="Y" />
                                                <asp:ListItem Text="No" Value="N" />
                                            </asp:RadioButtonList>
                                            <asp:RequiredFieldValidator ID="rfv_reject" runat="server" ValidationGroup="vg" ErrorMessage="Select Permission" Display="Dynamic" ControlToValidate="rdb_reject" CssClass="alert-danger"></asp:RequiredFieldValidator>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <div class="col-sm-3">
                                             <asp:Label ID="Label2" runat="server" Text="Permission for Review:"></asp:Label> 
                                            
                                        </div>
                                        <div class="col-sm-3">
                                            <asp:RadioButtonList runat="server" ID="rdb_review" CssClass="form-control" RepeatDirection="Horizontal" ValidationGroup="vg">
                                                <asp:ListItem Text="Yes " Value="Y" />
                                                <asp:ListItem Text="No" Value="N" />
                                            </asp:RadioButtonList>
                                            <asp:RequiredFieldValidator ID="rfv_review" runat="server" ValidationGroup="vg" ErrorMessage="Select Permission" Display="Dynamic" ControlToValidate="rdb_review" CssClass="alert-danger"></asp:RequiredFieldValidator>
                                        </div>
                                        <div class="col-sm-3">
                                                <asp:Label ID="Label3" runat="server" Text="Permission for Forward:"></asp:Label> 
                                            
                                        </div>
                                        <div class="col-sm-3">
                                            <asp:RadioButtonList ID="rdb_forward" runat="server" CssClass="form-control" RepeatDirection="Horizontal" ValidationGroup="vg">
                                                <asp:ListItem Value="Y" Text="Yes"></asp:ListItem>
                                                <asp:ListItem Value="N" Text="No"></asp:ListItem>
                                            </asp:RadioButtonList>
                                            <asp:RequiredFieldValidator ID="rfv_forward" runat="server" ValidationGroup="vg" ErrorMessage="Select Permission" Display="Dynamic" ControlToValidate="rdb_forward" CssClass="alert-danger"></asp:RequiredFieldValidator>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <div class="col-sm-3">
                                              <asp:Label ID="Label4" runat="server" Text="Permission For Approve:"></asp:Label> 
                                           
                                        </div>
                                        <div class="col-sm-3">
                                            <asp:RadioButtonList ID="rdb_approve" runat="server" CssClass="form-control" RepeatDirection="Horizontal" ValidationGroup="vg">
                                                <asp:ListItem Value="Y" Text="Yes"></asp:ListItem>
                                                <asp:ListItem Value="N" Text="No"></asp:ListItem>
                                            </asp:RadioButtonList>
                                            <asp:RequiredFieldValidator ID="rfv_approve" runat="server" ValidationGroup="vg" ErrorMessage="Select Permission" Display="Dynamic" ControlToValidate="rdb_approve" CssClass="alert-danger"></asp:RequiredFieldValidator>
                                        </div>
                                    </div>
                                </div>
                                <div class="center">
                                    <asp:Button ID="btn_submit" runat="server" Text="Update" ValidationGroup="vg" CssClass="button button-3d button-black nomargin btn btn-primary" OnClick="btn_submit_Click" />
                                </div>
                            </div>
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>
    </div>
</asp:Content>

