<%@ Page Title="" Language="C#" MasterPageFile="~/admin_master.master" AutoEventWireup="true" CodeFile="dispatch_receive_create.aspx.cs" Inherits="dio_dispatch_receive_create" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CPH_MasterLogin" runat="Server">
    <style type="text/css">
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
            <div class="panel-title">
                Recieve and Dispatch
            </div>
        </div>
        <div class="panel-body">
            <asp:UpdatePanel ID="udp" runat="server">
                <ContentTemplate>
                    <div class="form-horizontal">
                        <div class="form-group">
                            <div class="col-sm-6" id="district" runat="server">
                                <div class="col-sm-4">
                                    <label class="text-primary">District : <span class="text-danger">*</span></label>
                                </div>
                                <div class="col-sm-8">
                                    <asp:DropDownList ID="ddl_district" runat="server" ValidationGroup="vg" CssClass="form-control" OnSelectedIndexChanged="ddl_district_SelectedIndexChanged" AppendDataBoundItems="true" AutoPostBack="true"></asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="rfv_district" runat="server" ValidationGroup="vg" ControlToValidate="ddl_district" InitialValue="0"
                                        CssClass="alert-danger" ErrorMessage="District is Required" Display="Dynamic" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                </div>
                            </div>
                            <div class="col-sm-6" id="dept" runat="server">
                                <div class="col-sm-4">
                                    <label class="text-primary">Department :<span class="text-danger">*</span> </label>
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
                                    <label class="text-primary">Office Level:<span class="text-danger">*</span></label>
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
                                    <label class="text-primary">Office Category</label>
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
                                    <label class="text-primary">Office Name :<span class="text-danger">*</span></label>
                                </div>
                                <div class="col-sm-8">
                                    <asp:DropDownList ID="ddl_office" runat="server" ValidationGroup="vg" CssClass="form-control" AppendDataBoundItems="true" OnSelectedIndexChanged="ddl_office_SelectedIndexChanged" AutoPostBack="true">
                                        <asp:ListItem Value="0" Text="Choose Office" ></asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="rfv_office" runat="server" ValidationGroup="vg" ControlToValidate="ddl_office" InitialValue="0"
                                        CssClass="alert-danger" ErrorMessage="Office is Required" Display="Dynamic" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                </div>
                            </div>
                            <div class="col-sm-6" id="employee" runat="server">
                                <div class="col-sm-4">
                                    <label class="text-primary">Employee Name :<span class="text-danger">*</span></label>
                                </div>
                                <div class="col-sm-8">
                                    <asp:DropDownList ID="ddl_employee" runat="server" ValidationGroup="vg" CssClass="form-control" AppendDataBoundItems="true" OnSelectedIndexChanged="ddl_employee_SelectedIndexChanged" AutoPostBack="true">
                                        <asp:ListItem Value="0" Text="Choose Employee" ></asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="rfv_employee" runat="server" ValidationGroup="vg" ControlToValidate="ddl_employee" InitialValue="0"
                                        CssClass="alert-danger" ErrorMessage="Employee is Required" Display="Dynamic" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                </div>
                            </div>
                        </div>
                        <div class="form-group" >
                            <div class="col-sm-6" id="chargetyp"> 
                                <div class="col-sm-4">
                                    <label class="text-primary">
                                        Charge Type : <span class="text-danger">*</span>
                                    </label>
                                </div>
                                <div class="col-sm-8">
                                    <asp:DropDownList ID="ddl_charge" runat="server" ValidationGroup="vg" CssClass="form-control" AppendDataBoundItems="true">
                                        <asp:ListItem Value="0" Text="Choose Charge Type"></asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="rfv_charge" runat="server" ValidationGroup="vg" ControlToValidate="ddl_charge" InitialValue="0"
                                        CssClass="alert-danger" ErrorMessage="Charge is Required" Display="Dynamic" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                </div>
                            </div>
                            <div class="col-sm-6 " id="active">
                                <div class="col-sm-4">
                                    <label class="text-primary">Status<span class="text-danger">*</span></label>
                                </div>
                                <div class="col-sm-8">
                                    <asp:DropDownList ID="ddl_active" runat="server" ValidationGroup="vg" CssClass="form-control" AppendDataBoundItems="true">
                                        <asp:ListItem Value="0" Text="SELECT "></asp:ListItem>
                                        <asp:ListItem Value="Y" Text="ACTIVE"></asp:ListItem>
                                        <asp:ListItem Value="N" Text="NOT ACTIVE"></asp:ListItem>
                                    </asp:DropDownList>
                                      <asp:RequiredFieldValidator ID="rfv_active" runat="server" ValidationGroup="vg" ControlToValidate="ddl_active" InitialValue="0"
                                        CssClass="alert-danger" ErrorMessage="Status Required" Display="Dynamic" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                </div>
                            </div>
                        </div>
                   

                            <div class="form-group">   
                                <div class="col-sm-6">                             
                                    <div class="col-sm-4">
                                     <label class="text-primary">  joining  from:<span class="text-danger"></span></label>
                                    </div>
                                <div class="col-sm-6"> 
                                    <asp:TextBox ID="txt_from_date" palceholder="DD/MM/YYYY" MaxLength="10" runat="server" ValidationGroup="vg" CssClass="form-control" ></asp:TextBox>
                                     <asp:CalendarExtender ID="calex_from" runat="server" Enabled="True" Format="dd/MM/yyyy" PopupButtonID="imgbtn" FirstDayOfWeek="Monday" PopupPosition="BottomRight" TargetControlID="txt_from_date" TodaysDateFormat="dd/MM/yyyy"></asp:CalendarExtender>
                                      <asp:CalendarExtender ID="calex__from" runat="server" Enabled="True" Format="dd/MM/yyyy" PopupButtonID="txt_from_date" FirstDayOfWeek="Monday" PopupPosition="BottomRight" TargetControlID="txt_from_date" TodaysDateFormat="dd/MM/yyyy"></asp:CalendarExtender>
                                    <asp:RequiredFieldValidator ID="rfv_dt_From" ControlToValidate="txt_from_date" runat="server" ValidationGroup="vg"  CssClass="alert-danger" ErrorMessage="from field Date Required" Display="Dynamic" SetFocusOnError="true"></asp:RequiredFieldValidator> 
                                    <asp:CompareValidator ID="comval_txt_date_join" runat="server" Type="Date"  ControlToValidate="txt_from_date" CssClass="text-danger" Display="Dynamic"
                                         ErrorMessage="Please enter Correct Date" Operator="LessThanEqual" SetFocusOnError="True"  ValidationGroup="vg"></asp:CompareValidator>                                                                          

                                </div>
                            <div class="col-sm-2"><asp:ImageButton id="imgbtn" runat="server"  ImageAlign="Middle" Height="40" Width="40" ImageUrl="~/images/calendar-icon.png" />                                    
                            </div>
                                                                    </div>
                                <div class="col-sm-6">
                                                                    <div class="col-sm-4">
                                    <label class="text-primary">To:</label>
                                </div>
                                <div class="col-sm-6">
                                    <asp:TextBox ID="txt_to_date" MaxLength="10" palceholder="DD/MM/YYYY" runat="server" ValidationGroup="vg" CssClass="form-control"></asp:TextBox>
                                   <asp:CalendarExtender ID="Calex_2" runat="server" Enabled="True" Format="dd/MM/yyyy" PopupButtonID="imgbtn2" FirstDayOfWeek="Monday" PopupPosition="BottomRight" TargetControlID="txt_to_date" TodaysDateFormat="dd/MM/yyyy"></asp:CalendarExtender>
                                      <asp:CalendarExtender ID="CalendarExtender2" runat="server" Enabled="True" Format="dd/MM/yyyy" PopupButtonID="txt_to_date" FirstDayOfWeek="Monday" PopupPosition="BottomRight" TargetControlID="txt_to_date" TodaysDateFormat="dd/MM/yyyy"></asp:CalendarExtender>
                                     <asp:CompareValidator ID="comval_to" runat="server" Type="Date"  ControlToCompare="txt_from_date" ControlToValidate="txt_to_date" CssClass="text-danger" Display="Dynamic"
                                         ErrorMessage="Please enter Correct Date" Operator="GreaterThan" SetFocusOnError="True"  ValidationGroup="vg"></asp:CompareValidator>                                                                          </div>
                            <div class="col-sm-2">
                                <asp:ImageButton id="imgbtn2" runat="server"  ImageAlign="Middle" Height="40" Width="40" ImageUrl="~/images/calendar-icon.png" />                                    
                            </div></div>
                        </div>

                        <div class="form-group center">
                            <asp:Button ID="btn_create" runat="server" ValidationGroup="vg" OnClick="btn_create_Click" Text="Submit" />
                        </div>
                        <div class="form-group">
                            <div class="col-sm-12">
                                <asp:Label ID="lbl_count" runat="server" CssClass="text-primary bold"></asp:Label>
                            </div>
                        </div>

         <asp:GridView ID="grd_Office" runat="server" AllowPaging="true" PageSize="20" AutoGenerateColumns="false" ShowHeaderWhenEmpty="true"
                            CssClass="table table-striped table-bordered  table-hover table-grid mydatagrid" OnPageIndexChanging="grd_Office_PageIndexChanging">
                            <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                            <HeaderStyle BackColor="#507CD1" HorizontalAlign="left" VerticalAlign="Middle" Font-Bold="True" ForeColor="White" />
                            <AlternatingRowStyle BackColor="White" />
                            <Columns>
                                <asp:TemplateField HeaderText="S.No.">
                                    <ItemTemplate>
                                        <%# Container.DataItemIndex+1 %>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="left" />
                                    <ControlStyle Width="10px" />
                                    <HeaderStyle HorizontalAlign="left" />
                                </asp:TemplateField>
                                <asp:BoundField HeaderText="Office Name" DataField="ofice">
                                    <ItemStyle HorizontalAlign="left" />
                                    <HeaderStyle HorizontalAlign="left" />
                                </asp:BoundField>
                                <asp:BoundField DataField="address" HeaderText="Address ">
                                    <ItemStyle HorizontalAlign="left" />
                                    <HeaderStyle HorizontalAlign="left" />
                                </asp:BoundField>
                                <asp:BoundField DataField="mobile" HeaderText="Contact  ">
                                    <ItemStyle HorizontalAlign="left" />
                                    <HeaderStyle HorizontalAlign="left" />
                                </asp:BoundField>
                                <asp:BoundField DataField="email" HeaderText="Email ID  ">
                                    <ItemStyle HorizontalAlign="left" />
                                    <HeaderStyle HorizontalAlign="left" />
                                </asp:BoundField>
                                <asp:BoundField DataField="ofc_url" HeaderText="Office url  ">
                                    <ItemStyle HorizontalAlign="left" />
                                    <HeaderStyle HorizontalAlign="left" />
                                </asp:BoundField>
                                <asp:BoundField DataField="basedept" HeaderText="Base Department">
                                    <ItemStyle HorizontalAlign="left" />
                                    <HeaderStyle HorizontalAlign="left" />
                                </asp:BoundField>
                                <asp:BoundField DataField="ofclvl" HeaderText="Office Level">
                                    <ItemStyle HorizontalAlign="left" />
                                    <HeaderStyle HorizontalAlign="left" />
                                </asp:BoundField>
                                <asp:BoundField DataField="district" HeaderText=" Office District">
                                    <ItemStyle HorizontalAlign="left" />
                                    <HeaderStyle HorizontalAlign="left" />
                                </asp:BoundField>
                                <asp:BoundField DataField="address" HeaderText="Address Of Office">
                                    <ItemStyle HorizontalAlign="left" />
                                    <HeaderStyle HorizontalAlign="left" />
                                </asp:BoundField>
                            </Columns>
                            <EmptyDataTemplate>Action  Records Not Available</EmptyDataTemplate>
                            <PagerSettings Mode="NumericFirstLast" />
                            <PagerStyle BackColor="White" ForeColor="Black" HorizontalAlign="left" CssClass="cssPager" />
                        </asp:GridView>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>
</asp:Content>

