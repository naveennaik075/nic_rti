<%@ Page Title="" Language="C#" MasterPageFile="~/UserMaster.master" AutoEventWireup="true" CodeFile="UserDashBoard.aspx.cs" Inherits="UserDashBoard" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
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
                    <%-- <div class="panel-heading">
                        <h3><%=Resources.Resource.welcome %> <asp:Label ID="lbl_rolename" runat="server" ReadOnly="true"></asp:Label>  </h3>
                    </div>--%>
                    <div class="row">
                        <div class="col-sm-6  text-left">
                            <div class="row padd">
                                <div class="col-sm-12">
                                    <label><%=Resources.Resource.YourIPAddress %> </label>
                                    <asp:Label ID="lbl_ip" runat="server" Font-Bold="true" CssClass="text-uppercase" ForeColor="SandyBrown"></asp:Label>
                                </div>
                            </div>
                        </div>
                        <div class="col-sm-6 text-right padd">
                            <h3><%=Resources.Resource.welcome %>
                                <asp:Label ID="lbl_username" runat="server" Font-Bold="True" CssClass="text-uppercase" ForeColor="SandyBrown"></asp:Label>
                            </h3>
                        </div>
                    </div>

                    <div class="col-sm-6" style="border: 1px solid #808080; border-radius: 8px;">
                        <div class="Panel-body  " style="display: block;">
                            <div class="form-group"></div>
                            <div class="form-group">
                                <div class="col-sm-6">
                                    <asp:Label ID="lbl_TotalRTI" runat="server" CssClass="lbltxt" Text="<%$Resources:Resource, TotalRTI %>"></asp:Label>
                                </div>
                                <div class="col-sm-6">

                                    <asp:LinkButton ID="LB_TotalRTI" runat="server" PostBackUrl="#GridView1" OnClick="ShowTotalRTI_Click">
                                        <asp:Label ID="lbl_TotalRTI_count" runat="server" ReadOnly="true"></asp:Label>
                                    </asp:LinkButton>
                                </div>
                            </div>
                            <div class="form-group">
                                <div class="col-sm-6">
                                    <asp:Label ID="lbl_CompletedRTI" runat="server" CssClass="lbltxt" Text="<%$Resources:Resource, TotalCompletedRTI %>"></asp:Label>
                                </div>
                                <div class="col-sm-6">
                                    <asp:LinkButton ID="LB_CompletedRTI" runat="server" PostBackUrl="#GridView1" OnClick="ShowCompletedRTI_Click">
                                        <asp:Label ID="lbl_CompletedRTI_count" runat="server" ReadOnly="true"></asp:Label>
                                    </asp:LinkButton>
                                </div>
                            </div>

                            <div class="form-group">
                                <div class="col-sm-6">
                                    <asp:Label ID="lbl_UnderProcessRTI" runat="server" CssClass="lbltxt" Text="<%$Resources:Resource, TotalUnderProcessRTI %>"></asp:Label>
                                </div>
                                <div class="col-sm-6">
                                    <asp:LinkButton ID="LB_UnderProcessRTI" runat="server" PostBackUrl="#GridView1" OnClick="ShowUnderProcessRTI_Click">
                                        <asp:Label ID="lbl_UnderProcessRTI_count" runat="server" ReadOnly="true"></asp:Label>
                                    </asp:LinkButton>
                                </div>
                            </div>
                            <div class="form-group">
                                <div class="col-sm-6">
                                    <asp:Label ID="lbl_RejectedRTI" runat="server" CssClass="lbltxt" Text="<%$Resources:Resource, TotalRejectedRTI %>"></asp:Label>
                                </div>
                                <div class="col-sm-6">
                                    <asp:LinkButton ID="LB_RejectedRTI" runat="server" PostBackUrl="#GridView1" OnClick="ShowRejectedRTI_Click">
                                        <asp:Label ID="lbl_RejectedRTI_count" runat="server" ReadOnly="true"></asp:Label>
                                    </asp:LinkButton>
                                </div>
                            </div>

                            <div class="form-group">
                                <div class="col-sm-6">
                                    <asp:Label ID="lbl_RTIForClarification" runat="server" CssClass="lbltxt" Text="<%$Resources:Resource, TotalRTIForClarification %>"></asp:Label>
                                </div>
                                <div class="col-sm-6">
                                    <asp:LinkButton ID="LB_RTIForClarification" runat="server" PostBackUrl="#GridView1" OnClick="ShowRTIForClarification_Click">
                                        <asp:Label ID="lbl_RTIForClarification_count" runat="server" ReadOnly="true"></asp:Label>
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
            <asp:HiddenField ID="h_gridtype" runat="server" value="0" />
        </div>

        <asp:GridView ID="GridView1" Visible="false" runat="server" AllowPaging="true" PageSize="10" AutoGenerateColumns="False" ShowHeaderWhenEmpty="True"
            CssClass="table table-striped table-bordered  table-hover table-grid" OnPageIndexChanging="GridView1_PageIndexChanging">
            <FooterStyle BackColor="#507CD1" HorizontalAlign="left" VerticalAlign="Middle" Font-Bold="True" ForeColor="White" />
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
                <%--<asp:BoundField HeaderText="<%$Resources:Resource, RTIRequestID %>" DataField="RequestID">
                    <ItemStyle HorizontalAlign="left" />
                    <HeaderStyle HorizontalAlign="left" />
                </asp:BoundField>--%>
                <asp:TemplateField HeaderText="<%$Resources:Resource, RTIRequestID %>">
                    <ItemTemplate>
                        <asp:LinkButton ID="lnk_id" OnClick="lnk_id_Click" runat="server" CausesValidation="false" CommandArgument='<%# Eval("RequestID","{0}") %>' Text='<%# Bind("RequestID") %>'></asp:LinkButton>
                    </ItemTemplate>
                    <ItemStyle HorizontalAlign="left" />
                </asp:TemplateField>
                
                <asp:BoundField HeaderText="<%$Resources:Resource, ApplicantName %>" DataField="ApplicantName">
                    <ItemStyle HorizontalAlign="left" />
                    <HeaderStyle HorizontalAlign="left" />
                </asp:BoundField>
                <asp:BoundField DataField="subject" HeaderText="<%$Resources:Resource, SubjectOfRTI %>">
                    <ItemStyle HorizontalAlign="left" />
                    <HeaderStyle HorizontalAlign="left" />
                </asp:BoundField>
                <asp:BoundField DataField="status" HeaderText="<%$Resources:Resource, Status %>">
                    <ItemStyle HorizontalAlign="left" />
                    <HeaderStyle HorizontalAlign="left" />
                </asp:BoundField>
                <asp:BoundField DataField="office" HeaderText="<%$Resources:Resource, OfficeName %>  ">
                    <ItemStyle HorizontalAlign="left" />
                    <HeaderStyle HorizontalAlign="left" />
                </asp:BoundField>
                <asp:BoundField DataField="officer" HeaderText="<%$Resources:Resource, OfficerName %>">
                    <ItemStyle HorizontalAlign="left" />
                    <HeaderStyle HorizontalAlign="left" />
                </asp:BoundField>
                <asp:BoundField DataField="department" HeaderText="<%$Resources:Resource, DepartmentName %>  ">
                    <ItemStyle HorizontalAlign="left" />
                    <HeaderStyle HorizontalAlign="left" />
                </asp:BoundField>
            </Columns>
            <EmptyDataTemplate><%=Resources.Resource.NoRecordsAvailable %> </EmptyDataTemplate>
            <PagerSettings Mode="NumericFirstLast" PageButtonCount="6" />
            <PagerStyle BackColor="White" ForeColor="Black" HorizontalAlign="left" CssClass="cssPager" />
        </asp:GridView>
    </div>
</asp:Content>

