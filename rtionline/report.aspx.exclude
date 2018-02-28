<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="report.aspx.cs" Inherits="report" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CPH_MasterLogin" Runat="Server">
       <style>
        .cssPager td {
            padding-left: 4px;
            padding-right: 4px;
        }
    </style>
     <div class="form-group">
        <div class="col-sm-2">

            <asp:Label ID="lbl_designation" runat="server" Text="Designation Name:"></asp:Label>
            <span class="text-danger">*</span>
        </div>
        <div class="col-sm-4">

            <asp:DropDownList ID="ddl_designation" runat="server" AutoPostBack="true" ValidationGroup="v" CssClass="form-control" OnSelectedIndexChanged="ddl_designation_SelectedIndexChanged" >
            </asp:DropDownList>
            <asp:RequiredFieldValidator ID="RFV_designation" runat="server" ValidationGroup="v" ControlToValidate="ddl_designation" CssClass="alert-danger" Display="Dynamic" ErrorMessage=" Please Select Designation" SetFocusOnError="true"  InitialValue="0"></asp:RequiredFieldValidator>
        </div>
         </div>
    <div class="clearfix"></div>
    <br />
 
     <asp:Panel runat="server" ID="pnl_grid" ScrollBars="Both" CssClass="panel panel-info">
                        <div class="row center" >
                            <asp:Label ID="lbl_count" runat="server"  CssClass="text-primary bold"></asp:Label>
                        </div>
      <asp:GridView ID="GridView1" runat="server" AllowPaging="True" PageSize="15" 
                            AutoGenerateColumns="False" ShowHeaderWhenEmpty="True" DataKeyNames="id"
                            OnPageIndexChanging="GridView1_PageIndexChanging"   
                            CssClass="table table-striped table-bordered  table-hover table-grid" >
                            <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                            <HeaderStyle BackColor="#507CD1" HorizontalAlign="Center" VerticalAlign="Middle" Font-Bold="True" ForeColor="White" />
                            <AlternatingRowStyle BackColor="White" />
                            <Columns>
                                <asp:TemplateField HeaderText="S.No. ">
                                    <ItemTemplate>
                                        <%#Container.DataItemIndex+1%>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="left" />
                                    <HeaderStyle HorizontalAlign="Left" />
                                </asp:TemplateField>
                                <asp:BoundField HeaderText="Employee Id" DataField="id" ReadOnly="true" Visible="false" />

                                <asp:TemplateField HeaderText="Employee Name">
                                    <ItemTemplate>
                                        <asp:Label ID="lbl_empName" runat="server" Text='<%#Eval("empname") %>'></asp:Label>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:TextBox ID="txt_Emp_Name" runat="server" MaxLength="20" Text='<%#Eval("empname") %>'></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="rfv_emp_name" runat="server" ControlToValidate="txt_Emp_Name" CssClass="text-danger" ErrorMessage="Emp Name is Required" Display="Dynamic" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                    </EditItemTemplate>
                                </asp:TemplateField>
                                  <asp:TemplateField HeaderText="Mobile No">
                                    <ItemTemplate>
                                        <asp:Label ID="lbl_mob_num" runat="server" Text='<%#Eval("mblname") %>'></asp:Label>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:TextBox ID="txt_mob_num" runat="server" MaxLength="10" Text='<%#Eval("mblname") %>'></asp:TextBox>
                                        <asp:RegularExpressionValidator ID="rev_mobile_num" runat="server" ControlToValidate="txt_mob_num" CssClass="alert-danger" Display="Dynamic" ErrorMessage="Please Enter a valid Number" SetFocusOnError="True" ValidationExpression="^[1-9][0-9]{9}$"></asp:RegularExpressionValidator>

                                        <asp:RequiredFieldValidator ID="rfv_mob_num" runat="server" ControlToValidate="txt_mob_num" CssClass="text-danger" ErrorMessage="Mobile Number is Required" Display="Dynamic" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                    </EditItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Email Id">
                                    <ItemTemplate>
                                        <asp:Label ID="lbl_email" runat="server" Text='<%#Eval("email") %>'></asp:Label>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:TextBox ID="txt_email" runat="server" MaxLength="60" Text='<%#Eval("email") %>'></asp:TextBox>
                                        <asp:RegularExpressionValidator ID="rev_email_id" runat="server" ControlToValidate="txt_email" CssClass="alert-danger" Display="Dynamic" ErrorMessage="Please Enter Valid Email ID" SetFocusOnError="True" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"></asp:RegularExpressionValidator>
                                        <asp:RequiredFieldValidator ID="rfv_email" runat="server" ControlToValidate="txt_email" CssClass="text-danger" ErrorMessage="email is Required" Display="Dynamic" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                    </EditItemTemplate>
                                </asp:TemplateField>
                                  <asp:TemplateField HeaderText="District">
                                    <ItemTemplate>
                                        <asp:Label ID="lbl_District" runat="server" Text='<%#Eval("dist") %>'></asp:Label>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:DropDownList ID="ddl_district" runat="server"  AutoPostBack="true"></asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfv_grid_district" runat="server" ControlToValidate="ddl_district" CssClass="text-danger" Display="Dynamic" ErrorMessage=" Please Select district" SetFocusOnError="true" InitialValue="0"></asp:RequiredFieldValidator>
                                    </EditItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Department">
                                    <ItemTemplate>
                                        <asp:Label ID="lbl_Department" runat="server" Text='<%#Eval("depnm") %>'></asp:Label>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:DropDownList ID="ddl_department_grid" runat="server"  AutoPostBack="true"></asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfv_grid_dept" runat="server" ControlToValidate="ddl_department_grid" CssClass="text-danger" Display="Dynamic" ErrorMessage=" Please Select State" SetFocusOnError="true" InitialValue="0"></asp:RequiredFieldValidator>
                                    </EditItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Office">
                                    <ItemTemplate>
                                        <asp:Label ID="lbl_Office" runat="server" Text='<%#Eval("office") %>'></asp:Label>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:DropDownList ID="ddl_office" runat="server"></asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfv_grid_ofc" runat="server" ControlToValidate="ddl_office" CssClass="text-danger" Display="Dynamic" ErrorMessage=" Please Select State" SetFocusOnError="true" InitialValue="0"></asp:RequiredFieldValidator>
                                    </EditItemTemplate>
                                </asp:TemplateField>
                               </Columns>
           <EmptyDataTemplate>No Records Available</EmptyDataTemplate>
                            <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="left" CssClass="cssPager" />
          </asp:GridView>
         </asp:Panel>
</asp:Content>

