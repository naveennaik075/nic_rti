<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="NoticeAll.aspx.cs" Inherits="user_NoticeAll" EnableEventValidation="false" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">
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
                                <%=Resources.Resource.NoticeBoard %>
                                <asp:Button runat="server" CssClass="button-blue lol" ID="Button1" OnClick="a123_Click"
                                    Text="<%$Resources:Resource, AdvanceSearch %>"></asp:Button>
                            </h3>
                        </div>
                        <asp:Panel ID="pp" runat="server" Visible="false">
                            <div class="panel-body">
                                <div class="form-group">
                                    <div class="col-sm-4 ">
                                        <label class="text-primary">
                                           <%=Resources.Resource.NoticeID %></label>
                                        <asp:TextBox ID="txt_notice_id" runat="server" CssClass=" form-control" Text=""></asp:TextBox>
                                    </div>
                                    <div class="col-sm-4 ">
                                        <label class="text-primary">
                                           <%=Resources.Resource.Subject %> 
                                        </label>
                                        <asp:TextBox ID="txt_Subject" runat="server" CssClass=" form-control" Text=""></asp:TextBox>
                                    </div>
                                    <div class="col-sm-4">
                                        <label class="text-primary">
                                           <%=Resources.Resource.Keyword %> 
                                        </label>
                                        <asp:TextBox ID="txt_search" runat="server" Text="" CssClass=" form-control"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <div class="col-sm-4">
                                     <label class="text-primary">
                                            <%=Resources.Resource.Department %></label>
                                        <asp:DropDownList ID="ddl_department" runat="server" class="input" Height="30px"
                                            Width="93%" OnSelectedIndexChanged="ddl_department_SelectedIndexChanged" AutoPostBack="true"
                                            CssClass="text_box">
                                        </asp:DropDownList>
                                    </div>
                                    <div class="col-sm-4">
                                        <label class="text-primary">
                                             <%=Resources.Resource.District %></label>
                                        <asp:DropDownList ID="ddl_district" runat="server" class="input" Height="30px" Width="93%"
                                            OnSelectedIndexChanged="ddlDistrict_SelectedIndexChanged" AutoPostBack="true">
                                        </asp:DropDownList>
                                    </div>
                                    <div class="col-sm-4">
                                        <label id="Label3" class="text-primary">
                                             <%=Resources.Resource.Office %>
                                        </label>
                                        <asp:DropDownList ID="ddl_office" runat="server" class="input" Height="30px" Width="93%">
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <div class="col-sm-4 ">                                     
                                            <label class="text-primary">
                                              <%=Resources.Resource.NoticeFrom %>
                                            </label>                                       
                                        <div class="row">
                                            <div class="col-sm-9">
                                                <asp:TextBox ID="txt_FromDate" runat="server" Text="" placeholder="DD/MM/YYYY" CssClass=" form-control"> </asp:TextBox>
                                                <asp:CalendarExtender ID="CalendarExtender2" runat="server" Enabled="True" Format="dd-MM-yyyy"
                                                    PopupButtonID="ImgBtn1" FirstDayOfWeek="Monday" PopupPosition="BottomRight" TargetControlID="txt_FromDate"
                                                    TodaysDateFormat="dd/MM/yyyy">
                                                </asp:CalendarExtender>
                                                 <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txt_FromDate" ValidationGroup="a" Display="Dynamic" ErrorMessage="Enter Correct Date" SetFocusOnError="true" CssClass="alert-danger" ValidationExpression="(((((0[1-9])|(1\d)|(2[0-8]))\/((0[1-9])|(1[0-2])))|((31\/((0[13578])|(1[02])))|((29|30)\/((0[1,3-9])|(1[0-2])))))\/((20[0-9][0-9])|(19[0-9][0-9])))|((29\/02\/(19|20)(([02468][048])|([13579][26]))))$"></asp:RegularExpressionValidator>
                                                
                                            </div>
                                            <div class="col-sm-3">
                                                <asp:ImageButton ID="ImgBtn1" runat="server" Height="30" Width="30" ImageUrl="~/images/calendar-icon.png" />
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-4">
                                   
                                            <label class="text-primary">
                                                <%=Resources.Resource.NoticeTo %> 
                                            </label>

                                        <div class="row">
                                            <div class="col-sm-9">
                                              <asp:TextBox ID="txt_ToDate" runat="server" ValidationGroup="a" Text="" CssClass=" form-control"  placeholder="DD/MM/YYYY"> </asp:TextBox>
                                <asp:CalendarExtender ID="CalendarExtender1" runat="server" Enabled="True" Format="dd-MM-yyyy"
                                        PopupButtonID="ImageButton1" FirstDayOfWeek="Monday" PopupPosition="BottomRight"
                                        TargetControlID="txt_ToDate" TodaysDateFormat="dd/MM/yyyy">
                                    </asp:CalendarExtender>
                                       <asp:RegularExpressionValidator ID="reg_date" runat="server" ControlToValidate="txt_ToDate" ValidationGroup="a" Display="Dynamic" ErrorMessage="Enter Correct Date" SetFocusOnError="true" CssClass="alert-danger" ValidationExpression="(((((0[1-9])|(1\d)|(2[0-8]))\/((0[1-9])|(1[0-2])))|((31\/((0[13578])|(1[02])))|((29|30)\/((0[1,3-9])|(1[0-2])))))\/((20[0-9][0-9])|(19[0-9][0-9])))|((29\/02\/(19|20)(([02468][048])|([13579][26]))))$"></asp:RegularExpressionValidator>
                                    <asp:CompareValidator ID="CompareValidator1" runat="server" CultureInvariantValues="false"
                                        ErrorMessage="Date should be greater then From Date" ControlToCompare="txt_FromDate"
                                        ControlToValidate="txt_ToDate" Display="Dynamic" Operator="GreaterThan" Type="Date"></asp:CompareValidator>
                                    </div>
                                    <div class="col-sm-3">
                                        <asp:ImageButton ID="ImageButton1" runat="server" ImageAlign="Middle" Height="30"
                                            Width="30" ImageUrl="~/images/calendar-icon.png" />
                                    </div>
                                   
                                </div>
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
                    <%-- <div class="gridtitle" style="vertical-align: middle !important; border: solid 0px black; text-align: center;">
                <div class="mytitle">
                    <h4>Notice </h4>
                </div>
            </div>--%>
                    <asp:Label ID="lbl_count" runat="server"></asp:Label>
                    <%--<asp:GridView ID="GridView1" CssClass="table table-striped table-bordered  table-hover table-grid" runat="server"
            EmptyDataText="N o       A c t i v e        C i r c u l a r "
            AutoGenerateColumns="False" ShowFooter="True" Width="100%" ForeColor="ButtonShadow" Font-Names="Arial"
            Font-Size="Smaller" AllowPaging="True" PageSize="15"
            OnPageIndexChanging="GridView1_PageIndexChanging">
            <HeaderStyle />
                    --%>
                    <asp:GridView ID="GridView1" runat="server" CssClass="table table-striped table-bordered  table-hover table-grid"
                        AllowPaging="true" PageSize="15" Width="100%" ShowHeaderWhenEmpty="True" AlternatingRowStyle-CssClass="alt"
                        ClientIDMode="Static" AutoGenerateColumns="False" OnPageIndexChanging="GridView1_PageIndexChanging"
                        EmptyDataText="No Record Found." OnRowDataBound="GridView1_RowDataBound" datakeys="notice_id">
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
                            <%--<asp:HyperLinkField HeaderText="Notice No." DataNavigateUrlFields="notice_id" DataTextField="notice_id" Target="_blank">
                                <ItemStyle HorizontalAlign="Center" ForeColor="ActiveBorder" />
                                <HeaderStyle HorizontalAlign="Center" />
                            </asp:HyperLinkField>--%>
                            <%--<asp:TemplateField HeaderText="Show File" ItemStyle-Width="60px">--%>
                            <asp:TemplateField HeaderText="<%$Resources:Resource, Subject %> ">
                                <ItemTemplate>
                                    <asp:LinkButton ID="lnk_file" runat="server" CommandArgument='<%#Eval("File_Id") %>'
                                        Text='<%#Eval("Header") %>' CommandName="download" OnClick="lnk_file_Click"
                                        OnClientClick="setTarget();"></asp:LinkButton>
                                    <asp:HiddenField ID="h_file_name" runat="server" Value='<%#Eval("filename") %>' />
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
                            <asp:BoundField DataField="publishdate" HeaderText="<%$Resources:Resource, PublishDate %>">
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
