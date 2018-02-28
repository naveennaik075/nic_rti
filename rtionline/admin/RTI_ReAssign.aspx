<%@ Page Title="" Language="C#" MasterPageFile="~/admin_master.master" AutoEventWireup="true" CodeFile="RTI_ReAssign.aspx.cs" Inherits="RTI_ReAssign" enableEventValidation="false" %>

<%@ Register Assembly="MSCaptcha" Namespace="MSCaptcha" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CPH_MasterLogin" runat="Server">
    <style>
        .cssPager td {
            padding-left: 4px;
            padding-right: 4px;
        }
    </style>
    <style type="text/css">
        td, th {
            padding: 2px !important;
        }
    </style>
    <div class=" panel panel-info">
        <div class="panel-heading">
            <h5 class="panel-title">
                <asp:Label ID="lbl_title" CssClass="text-primary" runat="server" Text="<%$Resources:Resource, ReAssignRTI %>"></asp:Label>
            </h5>
        </div>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <div class="panel-body" style="background-color: #f9f8f8;">
                    <div class="form-horizontal">
                       
                        <div class="form-group">
                            <div class="col-sm-2">
                                <asp:Label ID="lbl_state" runat="server" Text="<%$Resources:Resource, State %>"></asp:Label>
                            </div>
                            <div class="col-sm-4">
                                <asp:DropDownList ID="ddl_state" runat="server" Enabled="false" AutoPostBack="true" OnSelectedIndexChanged="ddl_state_SelectedIndexChanged"  CssClass="form-control">
                                </asp:DropDownList>
                                
                            </div>
                            <div class="col-sm-2">
                                <asp:Label ID="lbl_district" Text="<%$Resources:Resource, District %> " runat="server"></asp:Label>
                            </div>
                            <div class="col-sm-4">
                                <asp:DropDownList ID="ddl_district" runat="server" AutoPostBack="true"  CssClass="form-control" OnSelectedIndexChanged="ddl_district_SelectedIndexChanged">
                                </asp:DropDownList>
                                
                            </div>
                            
                        </div>
                        <div class="form-group">
                            <div class="col-sm-2">
                                <asp:Label ID="Label1" Text="<%$Resources:Resource, Department %> " runat="server"></asp:Label>
                            </div>
                            <div class="col-sm-4">
                                <asp:DropDownList ID="ddl_department" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddl_department_SelectedIndexChanged"  CssClass="form-control">
                                </asp:DropDownList>
                                
                            </div>
                            <div class="col-sm-2">
                                <asp:Label ID="lbl_office" runat="server" Text="<%$Resources:Resource, Office %> "></asp:Label>
                            </div>
                            <div class="col-sm-4">
                                <asp:DropDownList ID="ddl_office" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddl_office_SelectedIndexChanged"  CssClass="form-control">
                                </asp:DropDownList>
                                
                            </div>
                        </div>
                        <div class="form-group">
                            <div class="col-sm-2"> 
                                <asp:Label ID="lbl_Officer" Text="<%$Resources:Resource, OfficerName %>" runat="server"></asp:Label>
                            </div>
                            <div class="col-sm-4"> 
                                <asp:DropDownList ID="ddl_officer" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddl_officer_SelectedIndexChanged"  CssClass="form-control">
                                    </asp:DropDownList>
                            </div>
                            <div class="col-sm-2"> 
                                <asp:Label ID="lbl_status" Text="<%$Resources:Resource, Status %>" runat="server"></asp:Label>
                            </div>
                            <div class="col-sm-4"> 
                                <asp:DropDownList ID="ddl_status" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddl_status_SelectedIndexChanged"  CssClass="form-control">
                                    </asp:DropDownList>
                            </div>
                        </div>
                        <%--<asp:HiddenField ID="empid" runat="server" />--%>
                  </div>
                    <asp:Panel runat="server" ID="pnl_grid" ScrollBars="Both" CssClass="panel panel-info">
                        <div>
                            <asp:Label ID="lbl_count" runat="server" CssClass="text-primary bold"></asp:Label>
                        </div>
                        <asp:GridView ID="GridView1" runat="server" AllowPaging="True" PageSize="20" OnRowUpdating="GridView1_RowUpdating" OnDataBound="OnDataBound"
                            AutoGenerateColumns="False" ShowHeaderWhenEmpty="True" DataKeyNames="rti_id,dist_id,dept_id,ofc_id,ofc_map_id"
                            OnPageIndexChanging="GridView1_PageIndexChanging" OnRowDataBound="RowDataBound" OnRowEditing="GridView1_RowEditing" OnRowCancelingEdit="GridView1_RowCancelingEdit"
                            CssClass="table table-striped table-bordered  table-hover table-grid" AutoGenerateEditButton="True">
                            <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                            <HeaderStyle BackColor="#507CD1" HorizontalAlign="Center" VerticalAlign="Middle" Font-Bold="True" ForeColor="White" />
                            <AlternatingRowStyle BackColor="White" />
                            <Columns>
                                <%--<asp:TemplateField>
                                    <HeaderTemplate>
                                        <tr >
                                          <th style="width:0px"></th>
                                          <th colspan="5">RTI Information</th>                        
                                          <th colspan="5">Officer Assignment Information</th>
                                        </tr>
                                     </HeaderTemplate> 
                                </asp:TemplateField >--%>
                               
                                <asp:TemplateField HeaderText="<%$Resources:Resource, SNo %>">

                                    <ItemTemplate>
                                        <%#Container.DataItemIndex+1%>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" />
                                    <HeaderStyle HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:BoundField HeaderText="<%$Resources:Resource, RTI_ID %>" DataField="rti_id" ReadOnly="true"  />
                                <asp:BoundField HeaderText="<%$Resources:Resource, ApplicantName %>" DataField="Ap_Name" ReadOnly="true"  />
                                <asp:BoundField HeaderText="<%$Resources:Resource, Subject %>" DataField="subject" ReadOnly="true"  />
                                <%-- Up RTI part Down Assignment part --%>
                                                              
                                <asp:TemplateField HeaderText="<%$Resources:Resource, District %>">
                                    <ItemTemplate>
                                        <asp:Label ID="lbl_District" runat="server" Text='<%#Eval("dist") %>'></asp:Label>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:DropDownList ID="ddl_district_grid" runat="server" OnSelectedIndexChanged="ddl_grid_district_SelectedIndexChanged" AutoPostBack="true" ></asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfv_grid_district" runat="server" ControlToValidate="ddl_district_grid" CssClass="text-danger" Display="Dynamic" ErrorMessage=" Please Select district" SetFocusOnError="true" InitialValue="0"></asp:RequiredFieldValidator>
                                    </EditItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="<%$Resources:Resource, Department %>">
                                    <ItemTemplate>
                                        <asp:Label ID="lbl_Department" runat="server" Text='<%#Eval("dept_name") %>'></asp:Label>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:DropDownList ID="ddl_department_grid" runat="server" OnSelectedIndexChanged="ddl_grid_department_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfv_grid_dept" runat="server" ControlToValidate="ddl_department_grid" CssClass="text-danger" Display="Dynamic" ErrorMessage=" Please Select department" SetFocusOnError="true" InitialValue="0"></asp:RequiredFieldValidator>
                                    </EditItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="<%$Resources:Resource, Office %>">
                                    <ItemTemplate>
                                        <asp:Label ID="lbl_Office" runat="server" Text='<%#Eval("ofc_name") %>'></asp:Label>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:DropDownList ID="ddl_office_grid" runat="server" OnSelectedIndexChanged="ddl_office_grid_SelectedIndexChanged" AutoPostBack="true" ></asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfv_grid_ofc" runat="server" ControlToValidate="ddl_office_grid" CssClass="text-danger" Display="Dynamic" ErrorMessage=" Please Select Office" SetFocusOnError="true" InitialValue="0"></asp:RequiredFieldValidator>
                                    </EditItemTemplate>
                                </asp:TemplateField>
                                 <asp:TemplateField HeaderText="<%$Resources:Resource, OfficerName %>">
                                    <ItemTemplate>
                                        <asp:Label ID="lbl_Officer" runat="server" Text='<%#Eval("emp_name") %>'></asp:Label>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:DropDownList ID="ddl_officer_grid" runat="server" ></asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfv_grid_ofcr" runat="server" ControlToValidate="ddl_officer_grid" CssClass="text-danger" Display="Dynamic" ErrorMessage=" Please Select Officer" SetFocusOnError="true" InitialValue="0"></asp:RequiredFieldValidator>
                                    </EditItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="<%$Resources:Resource, Status %>">
                                    <ItemTemplate>
                                        <asp:Label ID="lbl_dept_status" runat="server" Text='<%#Eval("dept_status") %>'></asp:Label>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:DropDownList ID="ddl_dept_status" runat="server"></asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfv_grid_dept_status" runat="server" ControlToValidate="ddl_dept_status" CssClass="text-danger" Display="Dynamic" ErrorMessage=" Please Select Status" SetFocusOnError="true" InitialValue="0"></asp:RequiredFieldValidator>
                                    </EditItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                            <EmptyDataTemplate><%=Resources.Resource.NoRecordsAvailable %></EmptyDataTemplate>
                            <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="left" CssClass="cssPager" />
                        </asp:GridView>
                    </asp:Panel>
                </div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
</asp:Content>

