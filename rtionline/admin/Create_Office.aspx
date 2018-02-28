<%@ Page Title="" Language="C#" MasterPageFile="~/admin_master.master" AutoEventWireup="true" CodeFile="Create_Office.aspx.cs" Inherits="dio_Create_Office" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CPH_MasterLogin" runat="Server">
    <style type="text/css">
        td, th {
            padding: 2px !important;
        }

        .pager {
            background-color: #646464;
            font-family: Arial;
            color: White;
            height: 30px;
            text-align: left;
        }

        .mydatagrid a /** FOR THE PAGING ICONS  **/ {
            background-color: #4682b4;
            padding: 5px 5px 5px 5px;
            color: #fff;
            text-decoration: none;
            font-weight: bold;
        }

        .header {
            background-color: #646464;
            font-family: Arial;
            color: White;
            border: none 0px transparent;
            height: 25px;
            text-align: center;
            font-size: 16px;
        }

        .mydatagrid span /** FOR THE PAGING ICONS CURRENT PAGE INDICATOR **/ {
            background-color: #c9c9c9;
            color: #000;
            padding: 5px 5px 5px 5px;
        }

        .bold {
            font-family: Arial;
            font-size: larger;
            font-weight: 800;
        }

        .lol {
            resize: none;
        }
    </style>
    <script type="text/javascript">
        var count1 = "100";
        function limiter() {
            document.getElementById("CPH_MasterLogin_limit").innerHTML = count1;
            var text = document.getElementById("CPH_MasterLogin_txt_address").value;
            var lent = text.length;
            if (lent > count1) {
                text = text.substring(0, count1);
                document.getElementById("CPH_MasterLogin_txt_address").value = text;
                return false;
            }
            document.getElementById("CPH_MasterLogin_limit").innerHTML = count1 - lent;
        }
        //$(document).ready(function () {
        //    limiter();
        //});       
    </script>
    <div class="panel panel-info">
        <div class="panel-heading">
            <div class="panel-title ">
                <label class="text-primary"><%=Resources.Resource.OfficeAddingForm %> </label>
            </div>
        </div>
        <div class="panel-body">
            <div class="form-horizontal">
                <asp:UpdatePanel ID="udp" runat="server">
                    <ContentTemplate>
                        <div class="form-group">
                            <div class="col-sm-6" id="district" runat="server">
                                <div class="col-sm-4">
                                    <asp:Label ID="lbl_office" runat="server" Text="<%$Resources:Resource, District %> "></asp:Label><span class="text-danger">*</span>
                                </div>
                                <div class="col-sm-8">
                                    <asp:DropDownList ID="ddl_district" runat="server" ValidationGroup="vg" CssClass="form-control" OnSelectedIndexChanged="ddl_district_SelectedIndexChanged" AppendDataBoundItems="true" AutoPostBack="true"></asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="rfv_district" runat="server" ValidationGroup="vg" ControlToValidate="ddl_district" InitialValue="0"
                                        CssClass="alert-danger" ErrorMessage="District is Required" Display="Dynamic" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                </div>
                            </div>
                            <div class="col-sm-6" id="dept" runat="server">
                                <div class="col-sm-4">
                                    <asp:Label ID="Label1" runat="server" Text="<%$Resources:Resource, Department %>"></asp:Label><span class="text-danger">*</span>
                                </div>
                                <div class="col-sm-8">
                                    <asp:DropDownList ID="ddl_department" runat="server" ValidationGroup="vg" CssClass="form-control" OnSelectedIndexChanged="ddl_department_SelectedIndexChanged" AppendDataBoundItems="true" AutoPostBack="true"></asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="rfv_ddl_dept" runat="server" ValidationGroup="vg" ControlToValidate="ddl_department" InitialValue="0"
                                        CssClass="alert-danger" ErrorMessage="Department is Required" Display="Dynamic" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                </div>
                            </div>
                        </div>
                        <div class="form-group">
                            <div class="col-sm-6" id="ofc_leval" runat="server">
                                <div class="col-sm-4">
                                    <asp:Label ID="Label2" runat="server" Text="<%$Resources:Resource, OfficeLevel %>"></asp:Label><span class="text-danger">*</span>
                                </div>
                                <div class="col-sm-8">
                                    <asp:DropDownList ID="ddl_Ofc_Lvl" runat="server" ValidationGroup="vg" CssClass="form-control"
                                        OnSelectedIndexChanged="ddl_Ofc_Lvl_SelectedIndexChanged" AppendDataBoundItems="true" AutoPostBack="true">
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="rfv_ofclvl" runat="server" ValidationGroup="vg" ControlToValidate="ddl_Ofc_Lvl" InitialValue="0"
                                        CssClass="alert-danger" ErrorMessage="Office Level is Required" Display="Dynamic" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                </div>
                            </div>
                            <div class="col-sm-6">
                                <div class="col-sm-4">
                                    <asp:Label ID="Label3" runat="server" Text="<%$Resources:Resource, OfficeCategory %>"></asp:Label>
                                </div>
                                <div class="col-sm-8">
                                    <asp:DropDownList ID="ddl_category" runat="server" ValidationGroup="vg" CssClass="form-control" AppendDataBoundItems="true" OnSelectedIndexChanged="ddl_category_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="rfv_category" runat="server" ValidationGroup="vg" ControlToValidate="ddl_Ofc_Lvl" InitialValue="0"
                                        CssClass="alert-danger" ErrorMessage="Office Category is Required" Display="Dynamic" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                </div>
                            </div>
                        </div>
                        <div class="form-group">
                            <div class="col-sm-6" id="ofc_nm" runat="server">
                                <div class="col-sm-4">
                                    <asp:Label ID="Label4" runat="server" Text="<%$Resources:Resource, OfficeName %>"></asp:Label>
                                    <span class="text-danger">*</span>
                                </div>
                                <div class="col-sm-8">
                                    <asp:TextBox ID="txt_office" runat="server" ValidationGroup="vg" CssClass="form-control" placeholder="<%$Resources:Resource, Ex_OfficeName %>" MaxLength="100"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="rfv_office" runat="server" ValidationGroup="vg" SetFocusOnError="true" CssClass="alert-danger" ControlToValidate="txt_office" ErrorMessage="Office Name Is Required"></asp:RequiredFieldValidator>
                                </div>
                            </div>
                            <div class="col-sm-6" id="ofc_address" runat="server">
                                <div class="col-sm-4">
                                    <asp:Label ID="Label5" runat="server" Text="<%$Resources:Resource, OfficeAddress %> "></asp:Label>
                                    <span class="text-danger">*</span>
                                </div>
                                <div class="col-sm-8">
                                    <asp:TextBox ID="txt_address" runat="server" ValidationGroup="vg" placeholder="<%$Resources:Resource, Ex_OfficeAddress%>" CssClass="form-control lol" MaxLength="255" TextMode="MultiLine" onkeyup="limiter()"></asp:TextBox>
                                    <asp:Label ID="lblChar" runat="server" CssClass="text-danger" Text="Characters left."></asp:Label>
                                    <asp:Label ID="limit" runat="server" CssClass="text-danger" Text="100"></asp:Label>
                                    <asp:RequiredFieldValidator ID="rfv_ofc_add" runat="server" ValidationGroup="vg" SetFocusOnError="true" CssClass="alert-danger" ControlToValidate="txt_address" ErrorMessage="Office Address Is Required"></asp:RequiredFieldValidator>
                                </div>
                            </div>
                        </div>
                        <div class="form-group">
                            <div class="col-sm-6">
                                <div class="col-sm-4">
                                    <asp:Label ID="Label6" runat="server" Text="<%$Resources:Resource, ContactNumber %> "></asp:Label>
                                </div>
                                <div class="col-sm-8">
                                    <asp:TextBox ID="txt_contact" runat="server" ValidationGroup="vg" CssClass="form-control" MaxLength="10" placeholder="<%$Resources:Resource, Ex_Mobile %>"></asp:TextBox>
                                    <asp:RegularExpressionValidator ID="rev_mobile" runat="server" ControlToValidate="txt_contact" CssClass="alert-danger" Display="Dynamic" ErrorMessage="Please Enter a valid Number" SetFocusOnError="True" ValidationExpression="^[1-9][0-9]{9}$" ValidationGroup="vg"></asp:RegularExpressionValidator>
                                </div>
                            </div>
                            <div class="col-sm-6">
                                <div class="col-sm-4">
                                    <asp:Label ID="Label7" runat="server" Text="<%$Resources:Resource, FaxNumber %> "></asp:Label>
                                </div>
                                <div class="col-sm-8">
                                    <asp:TextBox ID="txt_fax" runat="server" ValidationGroup="vg" MaxLength="11" placeholder="<%$Resources:Resource, Ex_Fax %>" CssClass="form-control"></asp:TextBox>
                                    <asp:RegularExpressionValidator ID="rev_fax" runat="server" ControlToValidate="txt_fax" CssClass="alert-danger" Display="Dynamic" ErrorMessage="Please Enter a valid FAX Number" SetFocusOnError="True" ValidationExpression="^[0-9]{11}$" ValidationGroup="vg"></asp:RegularExpressionValidator>
                                </div>
                            </div>
                        </div>
                        <div class="form-group">
                            <div class="col-sm-6">
                                <div class="col-sm-4">
                                    <asp:Label ID="Label8" runat="server" Text="<%$Resources:Resource, OfficeEmail %>"></asp:Label>
                                </div>
                                <div class="col-sm-8">
                                    <asp:TextBox ID="txt_email" runat="server" CssClass="form-control" placeholder="<%$Resources:Resource, Ex_Email %>" ValidationGroup="vg" MaxLength="60"></asp:TextBox>
                                    <asp:RegularExpressionValidator ID="rev_email" runat="server" ControlToValidate="txt_email" CssClass="alert-danger" Display="Dynamic" ErrorMessage="Please Enter Valid Email ID" SetFocusOnError="True" ValidationGroup="vg" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"></asp:RegularExpressionValidator>
                                </div>
                            </div>
                            <div class="col-sm-6">
                                <div class="col-sm-4">
                                    <asp:Label ID="Label9" runat="server" Text="<%$Resources:Resource, OfficeURL %>"></asp:Label>
                                </div>
                                <div class="col-sm-8">
                                    <asp:TextBox ID="txt_url" runat="server" ValidationGroup="vg" placeholder="<%$Resources:Resource, Ex_URL %>" CssClass="form-control" MaxLength="50"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
                <div class="form-group center">
                    <asp:Button ID="btn_create" runat="server" ValidationGroup="vg" OnClick="btn_create_Click" Text="<%$Resources:Resource, Submit %>" CssClass=" btn btn-primary btn-info" />
                </div>
            </div>
            <asp:Panel runat="server" CssClass="panel panel-info" ScrollBars="Both">
                <div class="row center">
                    <asp:Label ID="lbl_count" runat="server" CssClass="text-primary bold"></asp:Label>
                </div>
                <asp:GridView ID="grd_Office" runat="server" AllowPaging="true" PageSize="20" AutoGenerateColumns="false" ShowHeaderWhenEmpty="true"
                    CssClass="table table-striped table-bordered  table-hover table-grid mydatagrid" OnPageIndexChanging="grd_Office_PageIndexChanging">
                    <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                    <HeaderStyle BackColor="#507CD1" HorizontalAlign="left" VerticalAlign="Middle" Font-Bold="True" ForeColor="White" />
                    <AlternatingRowStyle BackColor="White" />
                    <Columns>
                        <asp:TemplateField HeaderText="<%$Resources:Resource, SNo %>">
                            <ItemTemplate>
                                <%# Container.DataItemIndex+1 %>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="left" />
                            <ControlStyle Width="10px" />
                            <HeaderStyle HorizontalAlign="left" />
                        </asp:TemplateField>
                        <asp:BoundField HeaderText="<%$Resources:Resource, OfficeName %>" DataField="ofice">
                            <ItemStyle HorizontalAlign="left" />
                            <HeaderStyle HorizontalAlign="left" />
                        </asp:BoundField>
                        <asp:BoundField DataField="address" HeaderText="<%$Resources:Resource, Address %> ">
                            <ItemStyle HorizontalAlign="left" />
                            <HeaderStyle HorizontalAlign="left" />
                        </asp:BoundField>
                        <asp:BoundField DataField="mobile" HeaderText="<%$Resources:Resource, ContactNumber %>">
                            <ItemStyle HorizontalAlign="left" />
                            <HeaderStyle HorizontalAlign="left" />
                        </asp:BoundField>
                        <asp:BoundField DataField="email" HeaderText="<%$Resources:Resource, EmailID %>">
                            <ItemStyle HorizontalAlign="left" />
                            <HeaderStyle HorizontalAlign="left" />
                        </asp:BoundField>
                        <asp:BoundField DataField="ofc_url" HeaderText="<%$Resources:Resource, OfficeURL %>">
                            <ItemStyle HorizontalAlign="left" />
                            <HeaderStyle HorizontalAlign="left" />
                        </asp:BoundField>
                        <asp:BoundField DataField="basedept" HeaderText="<%$Resources:Resource, BaseDepartmentName %>">
                            <ItemStyle HorizontalAlign="left" />
                            <HeaderStyle HorizontalAlign="left" />
                        </asp:BoundField>
                        <asp:BoundField DataField="ofclvl" HeaderText="<%$Resources:Resource, OfficeLevel %>">
                            <ItemStyle HorizontalAlign="left" />
                            <HeaderStyle HorizontalAlign="left" />
                        </asp:BoundField>
                        <asp:BoundField DataField="district" HeaderText="<%$Resources:Resource, District %>">
                            <ItemStyle HorizontalAlign="left" />
                            <HeaderStyle HorizontalAlign="left" />
                        </asp:BoundField>
                        <asp:BoundField DataField="address" HeaderText="<%$Resources:Resource, OfficeAddress %>">
                            <ItemStyle HorizontalAlign="left" />
                            <HeaderStyle HorizontalAlign="left" />
                        </asp:BoundField>
                    </Columns>
                    <EmptyDataTemplate><%=Resources.Resource.NoRecordsAvailable %></EmptyDataTemplate>
                    <PagerSettings Mode="NumericFirstLast" />
                    <PagerStyle BackColor="White" ForeColor="Black" HorizontalAlign="left" CssClass="cssPager" />
                </asp:GridView>
            </asp:Panel>
        </div>
    </div>
</asp:Content>
