<%@ Page Title="" Language="C#" MasterPageFile="~/Master_employee.master" AutoEventWireup="true" CodeFile="filterReport.aspx.cs" Inherits="report" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <style>
        .cssPager td {
            padding-left: 4px;
            padding-right: 4px;
        }
    </style>

    <div class="panel panel-success">
        <div class="panel-heading">
            <div class="panel-title">
             <%=Resources.Resource.Report %> 
                   <%-- <asp:Label ID="lbl_UserName" runat="server" Text="Label" CssClass="text-primary"></asp:Label>--%>
            </div>
            <div class="left">
            </div>

        </div>

        <div class="panel-body">


            <div class="form-group">
                <div class="col-sm-2">
                    <asp:Label ID="lbl_state" runat="server" Text="<%$Resources:Resource, State %>"></asp:Label><span class="text-danger">*</span>
                </div>
                <div class="col-sm-4">
                    <asp:DropDownList ID="ddl_state" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddl_state_SelectedIndexChanged" ValidationGroup="v" CssClass="form-control">
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator ID="rfv_state" runat="server" ControlToValidate="ddl_state" CssClass="alert-danger" Display="Dynamic" ErrorMessage=" Please Select State" SetFocusOnError="true" ValidationGroup="vg" InitialValue="0"></asp:RequiredFieldValidator>
                </div>
                <div class="col-sm-2">
                    <asp:Label ID="lbl_district" Text="<%$Resources:Resource, District %> " runat="server"></asp:Label><span class="text-danger">*</span>
                </div>
                <div class="col-sm-4">
                    <asp:DropDownList ID="ddl_district" runat="server" AutoPostBack="true" ValidationGroup="v" CssClass="form-control" OnSelectedIndexChanged="ddl_district_SelectedIndexChanged">
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator ID="rfv_district" runat="server" ValidationGroup="v" ControlToValidate="ddl_district" CssClass="alert-danger" Display="Dynamic" ErrorMessage=" Please Select District" SetFocusOnError="true" InitialValue="0"></asp:RequiredFieldValidator>
                </div>

            </div>

            <div class="clearfix"></div>
            <br />
            <div class="form-group">
                <div class="col-sm-2">
                    <asp:Label ID="Label1" Text="<%$Resources:Resource, Department %>" runat="server"></asp:Label><span class="text-danger">*</span>
                </div>
                <div class="col-sm-4">
                    <asp:DropDownList ID="ddl_department" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddl_department_SelectedIndexChanged" ValidationGroup="v" CssClass="form-control">
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator ID="rfv_department" runat="server" ValidationGroup="v" ControlToValidate="ddl_department" CssClass="alert-danger" Display="Dynamic" ErrorMessage=" Please Select Department" SetFocusOnError="true" InitialValue="0"></asp:RequiredFieldValidator>
                </div>
                <div class="col-sm-2">
                    <asp:Label ID="lbl_office" runat="server" Text="<%$Resources:Resource, Office %>"></asp:Label><span class="text-danger">*</span>
                </div>
                <div class="col-sm-4">
                    <asp:DropDownList ID="ddl_office" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddl_office_SelectedIndexChanged" ValidationGroup="v" CssClass="form-control">
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator ID="rfv_office" runat="server" ValidationGroup="v" ControlToValidate="ddl_office" CssClass="alert-danger" Display="Dynamic" ErrorMessage=" Please Select Office" SetFocusOnError="true" InitialValue="0"></asp:RequiredFieldValidator>
                </div>
            </div>

            <div class="clearfix"></div>
            <br />
            <div class="form-group">
                <div class="col-sm-2">

                    <asp:Label ID="lbl_designation" runat="server" Text="<%$Resources:Resource, DesignationName %> "></asp:Label>
                    <span class="text-danger">*</span>
                </div>
                <div class="col-sm-4">

                    <asp:DropDownList ID="ddl_designation" runat="server" AutoPostBack="true" ValidationGroup="v" CssClass="form-control" OnSelectedIndexChanged="ddl_designation_SelectedIndexChanged">
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator ID="RFV_designation" runat="server" ValidationGroup="v" ControlToValidate="ddl_designation" CssClass="alert-danger" Display="Dynamic" ErrorMessage=" Please Select Designation" SetFocusOnError="true" InitialValue="0"></asp:RequiredFieldValidator>
                </div>
                <div class="col-sm-2"></div>
                <div class="col-sm-4">
                    <asp:LinkButton ID="lnk_addoffice" runat="server" Text="<%$Resources:Resource, AddOffice %> " CssClass="text-primary" OnClick="lnk_addoffice_Click"></asp:LinkButton>

                </div>
            </div>
            <div class="clearfix"></div>
            <br />
            <div id="div_office" runat="server" visible="false">
                <div class="form-group" id="div_radio_forward" runat="server">
                    <div class="col-sm-3">
                        <label class="text-primary center"> <%=Resources.Resource.ForAddNewOffice %> </label>
                    </div>
                </div>
                <div class="clearfix"></div>
                <br />
                <div class="panel-body">
                    <div class="form-horizontal">

                        <asp:UpdatePanel ID="udp" runat="server" Visible="false">
                            <ContentTemplate>
                                <div class="form-group">
                                    <div class="col-sm-6" id="district" runat="server">
                                        <div class="col-sm-4">
                                            <asp:Label ID="Label2" runat="server" Text="<%$Resources:Resource, District %>"></asp:Label><span class="text-danger">*</span>
                                        </div>
                                        <div class="col-sm-8">
                                            <asp:DropDownList ID="ddl_district1" runat="server" OnSelectedIndexChanged="ddl_district1_SelectedIndexChanged" ValidationGroup="vg" CssClass="form-control" AppendDataBoundItems="true" AutoPostBack="true"></asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ValidationGroup="vg" runat="server" ControlToValidate="ddl_district1" InitialValue="0"
                                                CssClass="alert-danger" ErrorMessage="District is Required" Display="Dynamic" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                        </div>
                                    </div>
                                    <div class="col-sm-6" id="dept" runat="server">
                                        <div class="col-sm-4">
                                            <asp:Label ID="Label3" runat="server" Text="<%$Resources:Resource, Department %>"></asp:Label><span class="text-danger">*</span>
                                        </div>
                                        <div class="col-sm-8">
                                            <asp:DropDownList ID="ddl_department1" runat="server" OnSelectedIndexChanged="ddl_department1_SelectedIndexChanged" ValidationGroup="vg" CssClass="form-control" AppendDataBoundItems="true" AutoPostBack="true"></asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="rfv_ddl_dept" runat="server" ValidationGroup="vg" ControlToValidate="ddl_department1" InitialValue="0"
                                                CssClass="alert-danger" ErrorMessage="Department is Required" Display="Dynamic" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                        </div>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <div class="col-sm-6" id="ofc_leval" runat="server">
                                        <div class="col-sm-4">
                                            <asp:Label ID="Label4" runat="server" Text="<%$Resources:Resource, OfficeLevel %>"></asp:Label><span class="text-danger">*</span>
                                        </div>
                                        <div class="col-sm-8">
                                            <asp:DropDownList ID="ddl_Ofc_Lvl" runat="server" OnSelectedIndexChanged="ddl_Ofc_Lvl_SelectedIndexChanged" ValidationGroup="vg" CssClass="form-control"
                                                AppendDataBoundItems="true" AutoPostBack="true">
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="rfv_ofclvl" runat="server" ValidationGroup="vg" ControlToValidate="ddl_Ofc_Lvl" InitialValue="0"
                                                CssClass="alert-danger" ErrorMessage="Office Level is Required" Display="Dynamic" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                        </div>
                                    </div>
                                    <div class="col-sm-6">
                                        <div class="col-sm-4">
                                            <asp:Label ID="Label5" runat="server" Text="<%$Resources:Resource, OfficeCategory %>"></asp:Label><span class="text-danger">*</span>

                                        </div>
                                        <div class="col-sm-8">
                                            <asp:DropDownList ID="ddl_category" runat="server" ValidationGroup="vg" OnSelectedIndexChanged="ddl_category_SelectedIndexChanged" CssClass="form-control" AppendDataBoundItems="true" AutoPostBack="true"></asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="rfv_category" runat="server" ValidationGroup="vg" ControlToValidate="ddl_category" InitialValue="0"
                                                CssClass="alert-danger" ErrorMessage="Office Category is Required" Display="Dynamic" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                        </div>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <div class="col-sm-6" id="ofc_nm" runat="server">
                                        <div class="col-sm-4">
                                            <asp:Label ID="Label6" runat="server" Text="<%$Resources:Resource, OfficeName %> "></asp:Label>
                                            <span class="text-danger">*</span>
                                        </div>
                                        <div class="col-sm-8">
                                            <asp:TextBox ID="txt_office" runat="server" ValidationGroup="vg" CssClass="form-control" placeholder="100 characters" MaxLength="100"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ValidationGroup="vg" SetFocusOnError="true" CssClass="alert-danger" ControlToValidate="txt_office" ErrorMessage="Office Name Is Required"></asp:RequiredFieldValidator>
                                        </div>
                                    </div>
                                    <div class="col-sm-6" id="ofc_address" runat="server">
                                        <div class="col-sm-4">
                                            <asp:Label ID="Label7" runat="server" Text="<%$Resources:Resource, OfficeAddress %> "></asp:Label>
                                            <span class="text-danger">*</span>
                                        </div>
                                        <div class="col-sm-8">
                                            <asp:TextBox ID="txt_address" runat="server" ValidationGroup="vg" CssClass="form-control lol" MaxLength="255" TextMode="MultiLine" onkeyup="limiter()"></asp:TextBox>
                                            <asp:Label ID="lblChar" runat="server" CssClass="text-danger" Text="Characters left."></asp:Label>
                                            <asp:Label ID="limit" runat="server" CssClass="text-danger" Text="100"></asp:Label>
                                            <asp:RequiredFieldValidator ID="rfv_ofc_add" runat="server" ValidationGroup="vg" SetFocusOnError="true" CssClass="alert-danger" ControlToValidate="txt_address" ErrorMessage="Office Address Is Required"></asp:RequiredFieldValidator>
                                        </div>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <div class="col-sm-6">
                                        <div class="col-sm-4">
                                            <asp:Label ID="Label8" runat="server" Text="<%$Resources:Resource, ContactNumber %>"></asp:Label>
                                        </div>
                                        <div class="col-sm-8">
                                            <asp:TextBox ID="txt_contact" runat="server" ValidationGroup="vg" CssClass="form-control" MaxLength="10" placeholder="Only 10 Numbers "></asp:TextBox>
                                            <asp:RegularExpressionValidator ID="rev_mobile" runat="server" ControlToValidate="txt_contact" CssClass="alert-danger" Display="Dynamic" ErrorMessage="Please Enter a valid Number" SetFocusOnError="True" ValidationExpression="^[1-9][0-9]{9}$" ValidationGroup="vg"></asp:RegularExpressionValidator>
                                        </div>
                                    </div>
                                    <div class="col-sm-6">
                                        <div class="col-sm-4">
                                            <asp:Label ID="Label9" runat="server" Text="<%$Resources:Resource, FaxNumber %> "></asp:Label>
                                        </div>
                                        <div class="col-sm-8">
                                            <asp:TextBox ID="txt_fax" runat="server" ValidationGroup="vg" MaxLength="11" placeholder="only numbers allow" CssClass="form-control"></asp:TextBox>
                                            <asp:RegularExpressionValidator ID="rev_fax" runat="server" ControlToValidate="txt_fax" CssClass="alert-danger" Display="Dynamic" ErrorMessage="Please Enter a valid FAX Number" SetFocusOnError="True" ValidationExpression="^[0-9]{11}$" ValidationGroup="vg"></asp:RegularExpressionValidator>
                                        </div>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <div class="col-sm-6">
                                        <div class="col-sm-4">
                                            <asp:Label ID="Label10" runat="server" Text="<%$Resources:Resource, OfficeEmail %> "></asp:Label>
                                        </div>
                                        <div class="col-sm-8">
                                            <asp:TextBox ID="txt_email" runat="server" CssClass="form-control" ValidationGroup="vg" MaxLength="60"></asp:TextBox>
                                            <asp:RegularExpressionValidator ID="rev_email" runat="server" ControlToValidate="txt_email" CssClass="alert-danger" Display="Dynamic" ErrorMessage="Please Enter Valid Email ID" SetFocusOnError="True" ValidationGroup="vg" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"></asp:RegularExpressionValidator>
                                        </div>
                                    </div>
                                    <div class="col-sm-6">
                                        <div class="col-sm-4">
                                            <asp:Label ID="Label11" runat="server" Text="<%$Resources:Resource, OfficeURL %>"></asp:Label>
                                        </div>
                                        <div class="col-sm-8">
                                            <asp:TextBox ID="txt_url" runat="server" ValidationGroup="vg" CssClass="form-control" MaxLength="50"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                                <div class="form-group center">
                                    <asp:Button ID="btn_create" runat="server" ValidationGroup="vg" OnClick="btn_create_Click" Text="<%$Resources:Resource, Submit %>" CssClass=" btn btn-primary btn-info" />
                                </div>
                            </ContentTemplate>
                        </asp:UpdatePanel>

                    </div>
                </div>
            </div>
            <div class="clearfix"></div>
            <br />
            <asp:Panel runat="server" ID="pnl_grid" ScrollBars="Both" CssClass="panel panel-info">
                <div class="row center">
                    <asp:Label ID="lbl_count" runat="server" CssClass="text-primary bold"></asp:Label>
                </div>
                <asp:GridView ID="GridView1" runat="server" AllowPaging="True" PageSize="20"
                    AutoGenerateColumns="False" ShowHeaderWhenEmpty="True" DataKeyNames="id"
                    OnPageIndexChanging="GridView1_PageIndexChanging"
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
                        <asp:BoundField HeaderText="<%$Resources:Resource, EmployeeId %>" DataField="id" ReadOnly="true" Visible="false" />

                        <asp:TemplateField HeaderText="<%$Resources:Resource, EmployeeName %>">
                            <ItemTemplate>
                                <asp:Label ID="lbl_empName" runat="server" Text='<%#Eval("empname") %>'></asp:Label>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:TextBox ID="txt_Emp_Name" runat="server" MaxLength="20" Text='<%#Eval("empname") %>'></asp:TextBox>
                                <asp:RequiredFieldValidator ID="rfv_emp_name" runat="server" ControlToValidate="txt_Emp_Name" CssClass="text-danger" ErrorMessage="Emp Name is Required" Display="Dynamic" SetFocusOnError="true"></asp:RequiredFieldValidator>
                            </EditItemTemplate>
                        </asp:TemplateField>
                         <asp:TemplateField HeaderText="<%$Resources:Resource, MobileNo %>">
                                    <ItemTemplate>
                                        <asp:Label ID="lbl_mob_num" runat="server" Text='<%#Eval("mblname") %>'></asp:Label>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:TextBox ID="txt_mob_num" runat="server" MaxLength="10" Text='<%#Eval("mblname") %>'></asp:TextBox>
                                        <asp:RegularExpressionValidator ID="rev_mobile_num" runat="server" ControlToValidate="txt_mob_num" CssClass="alert-danger" Display="Dynamic" ErrorMessage="Please Enter a valid Number" SetFocusOnError="True" ValidationExpression="^[1-9][0-9]{9}$"></asp:RegularExpressionValidator>

                                        <asp:RequiredFieldValidator ID="rfv_mob_num" runat="server" ControlToValidate="txt_mob_num" CssClass="text-danger" ErrorMessage="Mobile Number is Required" Display="Dynamic" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                    </EditItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="<%$Resources:Resource, EmailID %>">
                                    <ItemTemplate>
                                        <asp:Label ID="lbl_email" runat="server" Text='<%#Eval("email") %>'></asp:Label>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:TextBox ID="txt_email" runat="server" MaxLength="60" Text='<%#Eval("email") %>'></asp:TextBox>
                                        <asp:RegularExpressionValidator ID="rev_email_id" runat="server" ControlToValidate="txt_email" CssClass="alert-danger" Display="Dynamic" ErrorMessage="Please Enter Valid Email ID" SetFocusOnError="True" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"></asp:RegularExpressionValidator>
                                        <asp:RequiredFieldValidator ID="rfv_email" runat="server" ControlToValidate="txt_email" CssClass="text-danger" ErrorMessage="email is Required" Display="Dynamic" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                    </EditItemTemplate>
                                </asp:TemplateField>
                                  <asp:TemplateField HeaderText="<%$Resources:Resource, District %>">
                                    <ItemTemplate>
                                        <asp:Label ID="lbl_District" runat="server" Text='<%#Eval("dist") %>'></asp:Label>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:DropDownList ID="ddl_district" runat="server"  AutoPostBack="true"></asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfv_grid_district" runat="server" ControlToValidate="ddl_district" CssClass="text-danger" Display="Dynamic" ErrorMessage=" Please Select district" SetFocusOnError="true" InitialValue="0"></asp:RequiredFieldValidator>
                                    </EditItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="<%$Resources:Resource, Department %>">
                                    <ItemTemplate>
                                        <asp:Label ID="lbl_Department" runat="server" Text='<%#Eval("depnm") %>'></asp:Label>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:DropDownList ID="ddl_department_grid" runat="server"  AutoPostBack="true"></asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfv_grid_dept" runat="server" ControlToValidate="ddl_department_grid" CssClass="text-danger" Display="Dynamic" ErrorMessage=" Please Select State" SetFocusOnError="true" InitialValue="0"></asp:RequiredFieldValidator>
                                    </EditItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="<%$Resources:Resource, Office %>">
                                    <ItemTemplate>
                                        <asp:Label ID="lbl_Office" runat="server" Text='<%#Eval("office") %>'></asp:Label>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:DropDownList ID="ddl_office" runat="server"></asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfv_grid_ofc" runat="server" ControlToValidate="ddl_office" CssClass="text-danger" Display="Dynamic" ErrorMessage=" Please Select State" SetFocusOnError="true" InitialValue="0"></asp:RequiredFieldValidator>
                                    </EditItemTemplate>
                                </asp:TemplateField>
                    </Columns>
                    <EmptyDataTemplate><%=Resources.Resource.NoRecordsAvailable %></EmptyDataTemplate>
                    <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="left" CssClass="cssPager" />
                </asp:GridView>
            </asp:Panel>

        </div>
    </div>

</asp:Content>

