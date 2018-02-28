<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="DeptUserManual.aspx.cs" Inherits="user_DeptUserManual" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" Runat="Server">

     <style type="text/css">
        .cssPager td
        {
            padding-left: 4px;
            padding-right: 4px;
        }
        
        .dd_chk_select
        {
            padding-top: 6px !important;
            padding-bottom: 6px !important;
            padding-left: 30px !important;
            height: 34px !important;
        }
        
        /* Icon when the collapsible content is shown */
        .btn:before
        {
            font-family: "Glyphicons Halflings";
            content: "\e114";
        }
        
        /* Icon when the collapsible content is hidden */
        .btn.collapsed:before
        {
            content: "\e080";
        }
        
        .lol
        {
            float: right;
        }
    </style>
    <script type="text/javascript">

        function div_expand() {
            document.getElementById('demo').classList.add('in');
        };

        function setTarget() {
            document.forms[0].target = "_blank";

        }

    </script>
    <div class="container-fluid">
        <asp:UpdatePanel ID="udp" runat="server">
            <ContentTemplate>
                <div class="form-horizontal">
                    <div class="panel panel-primary">
                        <div class="panel-heading">
                            <h3 class="panel-title">
                                <%=Resources.Resource.DeptUserManual %>
                                <asp:Button runat="server" CssClass="button-blue lol" ID="Button1" OnClick="a123_Click"
                                    Text="<%$Resources:Resource, AdvanceSearch %>"></asp:Button>
                            </h3>
                        </div>
                        <asp:Panel ID="pp" runat="server" Visible="false">
                            <div class="panel-body">
                               
                                <div class="form-group">
                                    <div class="col-sm-3">
                                     <label id="lblDepartment" class="text-primary">
                                            <%=Resources.Resource.Department %></label>
                                        <asp:DropDownList ID="ddl_department" runat="server" class="input" Height="30px"
                                            Width="93%" OnSelectedIndexChanged="ddl_department_SelectedIndexChanged" AutoPostBack="true"
                                            CssClass="text_box">
                                        </asp:DropDownList>
                                    </div>
                                    <div class="col-sm-3">
                                        <label id="lblDistrict" class="text-primary">
                                             <%=Resources.Resource.District %></label>
                                        <asp:DropDownList ID="ddl_district" runat="server" class="input" Height="30px" Width="93%"
                                            OnSelectedIndexChanged="ddlDistrict_SelectedIndexChanged" AutoPostBack="true">
                                        </asp:DropDownList>
                                    </div>
                                    <div class="col-sm-3">
                                        <label id="lblOffice" class="text-primary">
                                             <%=Resources.Resource.Office %>
                                        </label>
                                        <asp:DropDownList ID="ddl_office" runat="server" class="input" Height="30px" Width="93%">
                                        </asp:DropDownList>
                                    </div>
                                    <div class="col-sm-3">
                                        <label id="lblYear" class="text-primary">
                                             <%=Resources.Resource.YearofIssue %>
                                        </label>
                                        <asp:TextBox ID="txtYear" runat="server" class="input" Height="30px" Width="93%">
                                        </asp:TextBox>
                                    </div>
                                </div>
                                
                                <div class="form-group center">
                                  
                                        <asp:Button ID="btn_search" runat="server" Text="<%$Resources:Resource, Submit %>" OnClick="btn_search_click"
                                            ValidationGroup="a" CssClass="button button-blue button-3d nomargin btn" />
                                 
                                </div>
                            </div>
                        </asp:Panel>
                    </div>
                </div>
                <br />
                <div class="form-group">
                    
                    <asp:Label ID="lbl_count" runat="server"></asp:Label>
                   
                    <asp:GridView ID="GridView1" runat="server" CssClass="table table-striped table-bordered  table-hover table-grid"
                        AllowPaging="true" PageSize="15" Width="100%" ShowHeaderWhenEmpty="True" AlternatingRowStyle-CssClass="alt"
                        ClientIDMode="Static" AutoGenerateColumns="False" OnPageIndexChanging="GridView1_PageIndexChanging"
                        EmptyDataText="No Record Found." OnRowDataBound="GridView1_RowDataBound" datakeys="Dept_User_Manual_ID">
                        <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                        <HeaderStyle BackColor="#507CD1" HorizontalAlign="Center" VerticalAlign="Middle"
                            Font-Bold="True" ForeColor="White" />
                        <Columns>
                            <asp:TemplateField HeaderText="<%$Resources:Resource, SNo %>">
                                <ItemTemplate>
                                    <%# Container.DataItemIndex+1 %>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                                <%--<ControlStyle Width="10px" />--%>
                                <HeaderStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                            
                            <asp:TemplateField HeaderText="<%$Resources:Resource, FileDescription %> ">
                                <ItemTemplate>
                                    <asp:LinkButton ID="lnk_file" runat="server" CommandArgument='<%#Eval("Dept_User_Manual_ID") %>'
                                        Text='<%#Eval("file_description") %>' CommandName="download" OnClick="lnk_file_Click"
                                        OnClientClick="setTarget();"></asp:LinkButton>
                                    <asp:HiddenField ID="h_file_name" runat="server" Value='<%#Eval("file_name") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="dist_name" HeaderText="<%$Resources:Resource, District %>">
                                <ItemStyle HorizontalAlign="Center" />
                                <HeaderStyle HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="dept_name" HeaderText="<%$Resources:Resource, Department %>">
                                <ItemStyle HorizontalAlign="Center" />
                                <HeaderStyle HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="ofc_name" HeaderText="<%$Resources:Resource, OfficeName %>">
                                <ItemStyle HorizontalAlign="Center" />
                                <HeaderStyle HorizontalAlign="Center" />
                            </asp:BoundField>
                           <%-- <asp:BoundField DataField="Subject" HeaderText="Subject        ">
                                <ItemStyle HorizontalAlign="Center" />
                                <HeaderStyle HorizontalAlign="Center" />
                            </asp:BoundField>--%>
                            <asp:BoundField DataField="Year_Issue" HeaderText="<%$Resources:Resource, YearofIssue %>">
                                <ItemStyle HorizontalAlign="Center" />
                                <HeaderStyle HorizontalAlign="Center" />
                            </asp:BoundField>
                        </Columns>
                        <%--<EmptyDataRowStyle Font-Bold="True" Font-Names="Bookman Old Style" Font-Size="Large"
                 ForeColor="#C00000" HorizontalAlign="Center" VerticalAlign="Top" />--%>
                        <EmptyDataRowStyle Font-Bold="True" Font-Names="Bookman Old Style" Font-Size="Large"
                            HorizontalAlign="Center" VerticalAlign="Top" />
                        <PagerSettings Mode="NumericFirstLast" />
                        <%--<PagerStyle ForeColor="#8C4510" HorizontalAlign="Center" CssClass="cssPager" />--%>
                        <PagerStyle HorizontalAlign="Center" CssClass="cssPager" />
                    </asp:GridView>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>


</asp:Content>

