<%@ Page Title="" Language="C#" MasterPageFile="~/admin_master.master" AutoEventWireup="true" CodeFile="LoginAdmin.aspx.cs" Inherits="LoginAdmin" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="CPH_MasterLogin" runat="Server">
     <style type="text/css">
         
        td, th {
            padding: 2px !important;
        }
    </style>
    <style>
        .cssPager td {
            padding-left: 4px;
            padding-right: 4px;
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


    <div class="form-horizontal">
        <div class="col-sm-2"></div>
        <div class="col_two_third">
            <div class="panel panel-collapse ">
                <div class="panel-title bgpanel">
                    <div class="panel-heading">
                        <h3><%=Resources.Resource.welcome %> <asp:Label ID="lbl_rolename" runat="server" ReadOnly="true"></asp:Label>  </h3>
                    </div>
                    
                    <div class="col-sm-6" style="border: 1px solid #808080; border-radius: 8px;">
                        <div class="Panel-body  " style="display: block;">
                            <div class="form-group"></div>
                            <div class="form-group">
                                <div class="col-sm-6">
                                    <asp:Label ID="lbl_employee" runat="server" CssClass="lbltxt" Text="<%$Resources:Resource, TotalEmployee %>"></asp:Label>
                                </div>
                                <div class="col-sm-6">

                                    <asp:LinkButton ID="LB_Employee" runat="server" PostBackUrl="#GridView1" OnClick="ShowEmployee_Click">
                                        <asp:Label ID="lbl_employee_count" runat="server" ReadOnly="true"></asp:Label>
                                    </asp:LinkButton>
                                </div>
                            </div>
                            <div class="form-group">
                                <div class="col-sm-6">
                                    <asp:Label ID="lbl_department" runat="server" CssClass="lbltxt" Text="<%$Resources:Resource, TotalDepartment %>"></asp:Label>
                                </div>
                                <div class="col-sm-6">
                                    <asp:LinkButton ID="LB_Department" runat="server" PostBackUrl="#GV_Department" OnClick="ShowDepartment_Click">
                                        <asp:Label ID="lbl_department_count" runat="server" ReadOnly="true"></asp:Label>
                                    </asp:LinkButton>
                                </div>
                            </div>

                            <div class="form-group">
                                <div class="col-sm-6">
                                    <asp:Label ID="lbl_office" runat="server" CssClass="lbltxt" Text="<%$Resources:Resource, TotalOffice %>"></asp:Label>
                                </div>
                                <div class="col-sm-6">
                                    <asp:LinkButton ID="LB_Office" runat="server" PostBackUrl="#GV_Office" OnClick="ShowOffice_Click">
                                        <asp:Label ID="lbl_office_count" runat="server" ReadOnly="true"></asp:Label>
                                    </asp:LinkButton>
                                </div>
                            </div>
                            <div class="form-group">
                                <div class="col-sm-6">
                                    <asp:Label ID="lbl_mapEmp" runat="server" CssClass="lbltxt" Text="<%$Resources:Resource, TotalMappedEmployee %>"></asp:Label>
                                </div>
                                <div class="col-sm-6">
                                    <asp:LinkButton ID="LB_MapEmp" runat="server" PostBackUrl="#GV_MapEmp" OnClick="ShowMapEmp_Click">
                                        <asp:Label ID="lbl_mapEmp_count" runat="server" ReadOnly="true"></asp:Label>
                                    </asp:LinkButton>
                                </div>
                            </div>

                        </div>
                    </div>

                </div>
            </div>
        </div>
    </div>


    <div class="form-group">
        <div class="col-sm-12">
            <asp:Label ID="lbl_count" runat="server" CssClass="lbltxt" Visible="false"> </asp:Label>
        </div>
        
        <asp:GridView ID="GridView1" Visible="false" runat="server" AllowPaging="True" PageSize="10"
            AutoGenerateColumns="False" ShowHeaderWhenEmpty="True" DataKeyNames="id,state,dist,depnm,office"
            OnPageIndexChanging="GridView1_PageIndexChanging"
            CssClass="table table-striped table-bordered  table-hover table-grid ">
            <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
            <HeaderStyle BackColor="#507CD1" HorizontalAlign="Center" VerticalAlign="Middle" Font-Bold="True" ForeColor="White" />


            <AlternatingRowStyle BackColor="White" />
            <Columns>
                <asp:TemplateField HeaderText="<%$Resources:Resource, SNo %>">
                    <ItemTemplate>
                        <%#Container.DataItemIndex+1%>
                    </ItemTemplate>
                    <ItemStyle HorizontalAlign="left" />
                    <HeaderStyle HorizontalAlign="Left" />
                </asp:TemplateField>
                <asp:BoundField HeaderText="<%$Resources:Resource, EmployeeId %>" DataField="id" />
                <asp:BoundField HeaderText="<%$Resources:Resource, EmployeeName %>" DataField="empname" />
                <asp:BoundField HeaderText="<%$Resources:Resource, MobileNo %>" DataField="mblname" />
                <asp:BoundField HeaderText="<%$Resources:Resource, EmailID %>" DataField="email" />
                <asp:TemplateField HeaderText="<%$Resources:Resource, State %>">
                    <ItemTemplate>
                        <asp:Label ID="lbl_State" runat="server" Text='<%#Eval("state") %>'></asp:Label>

                    </ItemTemplate>
                    <EditItemTemplate>

                        <asp:DropDownList ID="ddl_state" runat="server"></asp:DropDownList>
                    </EditItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="<%$Resources:Resource, District %>">
                    <ItemTemplate>
                        <asp:Label ID="lbl_District" runat="server" Text='<%#Eval("dist") %>'></asp:Label>

                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:DropDownList ID="ddl_district" runat="server"></asp:DropDownList>
                    </EditItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="<%$Resources:Resource, Office %>">
                    <ItemTemplate>
                        <asp:Label ID="lbl_Office" runat="server" Text='<%#Eval("office") %>'></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:DropDownList ID="ddl_office" runat="server"></asp:DropDownList>
                    </EditItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="<%$Resources:Resource, Department %>">
                    <ItemTemplate>
                        <asp:Label ID="lbl_Department" runat="server" Text='<%#Eval("depnm") %>'></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:DropDownList ID="ddl_department" runat="server"></asp:DropDownList>
                    </EditItemTemplate>
                </asp:TemplateField>

            </Columns>
            <EmptyDataTemplate><%=Resources.Resource.NoRecordsAvailable %></EmptyDataTemplate>
            <PagerSettings Mode="NumericFirstLast" />
            <PagerStyle BackColor="White" ForeColor="Black" HorizontalAlign="left" CssClass="cssPager" />
        </asp:GridView>
    </div>

    <div class="form-group">
        <%--<div class="col-sm-12">
      <asp:Label ID="Label1" runat="server" CssClass="lbltxt" Visible="false"> </asp:Label>
            </div>--%>
        <asp:GridView ID="GV_MapEmp" Visible="false" runat="server" AllowPaging="True" PageSize="10"
            AutoGenerateColumns="False" ShowHeaderWhenEmpty="True" DataKeyNames="mapping_id"
            OnPageIndexChanging="GV_MapEmp_PageIndexChanging"
            CssClass="table table-striped table-bordered  table-hover table-grid">
            <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
            <HeaderStyle BackColor="#507CD1" HorizontalAlign="Center" VerticalAlign="Middle" Font-Bold="True" ForeColor="White" />


            <AlternatingRowStyle BackColor="White" />
            <Columns>
                <asp:TemplateField HeaderText="<%$Resources:Resource, SNo %>">
                    <ItemTemplate>
                        <%#Container.DataItemIndex+1%>
                    </ItemTemplate>
                    <ItemStyle HorizontalAlign="left" />
                    <HeaderStyle HorizontalAlign="Left" />
                </asp:TemplateField>
                <asp:BoundField HeaderText="<%$Resources:Resource, MappingID %>" DataField="mapping_id" />
                <asp:BoundField HeaderText="<%$Resources:Resource, EmployeeCode %>" DataField="EmployeeCode" />
                <asp:BoundField HeaderText="<%$Resources:Resource, EmployeeName %>" DataField="EmpName" />
                <asp:BoundField HeaderText="<%$Resources:Resource, OfficeName %>" DataField="officeName" />
                <asp:BoundField HeaderText="<%$Resources:Resource, Department %>" DataField="DeptName" />
                <asp:BoundField HeaderText="<%$Resources:Resource, OfficeLevel %>" DataField="OfficeLevelName" />
                <asp:BoundField HeaderText="<%$Resources:Resource, District %>" DataField="DistName_En" />
                <asp:BoundField HeaderText="<%$Resources:Resource, OfficeCategory %>" DataField="OfficeCategoryName" />
                <asp:BoundField HeaderText="<%$Resources:Resource, UserID %>" DataField="UserID" />
                <asp:BoundField HeaderText="<%$Resources:Resource, ChargeType %>" DataField="ChangeTypeName" />
                <asp:BoundField HeaderText="<%$Resources:Resource, IsActive %>" DataField="activeStatus" />
            </Columns>

            <EmptyDataTemplate>Action  Records Not Available</EmptyDataTemplate>
            <PagerSettings Mode="NumericFirstLast" />
            <PagerStyle BackColor="White" ForeColor="Black" HorizontalAlign="left" CssClass="cssPager" />
        </asp:GridView>
    </div>
    <div class="form-group">
        <%--<div class="col-sm-12">
      <asp:Label ID="Label1" runat="server" CssClass="lbltxt" Visible="false"> </asp:Label>
            </div>--%>
        <asp:GridView ID="GV_Department" Visible="false" runat="server" AllowPaging="True" PageSize="10"
            AutoGenerateColumns="False" ShowHeaderWhenEmpty="True" DataKeyNames="dept_id"
            OnPageIndexChanging="GV_Department_PageIndexChanging"
            CssClass="table table-striped table-bordered  table-hover table-grid">
            <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
            <HeaderStyle BackColor="#507CD1" HorizontalAlign="Center" VerticalAlign="Middle" Font-Bold="True" ForeColor="White" />


            <AlternatingRowStyle BackColor="White" />
            <Columns>
                <asp:TemplateField HeaderText="<%$Resources:Resource, SNo %>">
                    <ItemTemplate>
                        <%#Container.DataItemIndex+1%>
                    </ItemTemplate>
                    <ItemStyle HorizontalAlign="left" />
                    <HeaderStyle HorizontalAlign="Left" />
                </asp:TemplateField>
                <asp:BoundField HeaderText="<%$Resources:Resource, DepartmentId %>" DataField="dept_id" />
                <asp:BoundField HeaderText="<%$Resources:Resource, DepartmentName %>" DataField="dept_name" />

            </Columns>

            <EmptyDataTemplate>No Records Available</EmptyDataTemplate>
            <PagerSettings Mode="NumericFirstLast" />
            <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="left" CssClass="cssPager" />

        </asp:GridView>

    </div>

    <div class="form-group">
        
        <asp:GridView ID="GV_Office" runat="server" Visible="false" AllowPaging="true" PageSize="10" AutoGenerateColumns="false" ShowHeaderWhenEmpty="true"
            CssClass="table table-striped table-bordered  table-hover table-grid " OnPageIndexChanging="GV_Office_PageIndexChanging">
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
                <asp:BoundField HeaderText="<%$Resources:Resource, OfficeName %>" DataField="office">
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
                <asp:BoundField DataField="district" HeaderText="<%$Resources:Resource, District %> ">
                    <ItemStyle HorizontalAlign="left" />
                    <HeaderStyle HorizontalAlign="left" />
                </asp:BoundField>
                <%--<asp:BoundField DataField="address" HeaderText="Address Of Office">
                    <ItemStyle HorizontalAlign="left" />
                    <HeaderStyle HorizontalAlign="left" />
                </asp:BoundField>--%>
            </Columns>
            <EmptyDataTemplate><%=Resources.Resource.NoRecordsAvailable %></EmptyDataTemplate>
            <PagerSettings Mode="NumericFirstLast" />
            <PagerStyle BackColor="White" ForeColor="Black" HorizontalAlign="left" CssClass="cssPager" />
        </asp:GridView>
    </div>

</asp:Content>

