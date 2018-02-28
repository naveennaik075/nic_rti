<%@ Page Title="" Language="C#" MasterPageFile="~/UserMaster.master" AutoEventWireup="true" CodeFile="UserWelcome.aspx.cs" Inherits="user_UserWelcome" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <style type="text/css">
        /*.padd {
            padding-right: 30px;
            padding-left: 30px;
        }*/
         

        td, th {
            padding: 2px !important;
        }
    </style>

    <div class="panel panel-info">
        <div class="panel-heading panel-collapse">
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
        </div>
        <div class="panel-body">
            <div class="panel panel-success ">
                <div class="panel-heading panel-collapse">
                    <h4 class="panel-title">
                        <a data-toggle="collapse" href="#Detailapplicatn" aria-expanded="true"><%=Resources.Resource.your_submitted_rti %></a>
                    </h4>
                </div>
                <div id="Detailapplicatn" class="panel-collapse collapse in" aria-expanded="true">
                     <div class="col-sm-12">
                        <asp:Label ID="lbl_count" runat="server" CssClass="text-primary bold"></asp:Label>
                            </div>
                    <div class="panel-body">
                        

                        <asp:GridView ID="GridView1" runat="server" AllowPaging="true" PageSize="10" AutoGenerateColumns="False" ShowHeaderWhenEmpty="True"
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
                                <asp:TemplateField HeaderText="<%$Resources:Resource, RTIRequestID %>">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lnk_id" OnClick="lnk_id_Click" runat="server" CausesValidation="false" CommandArgument='<%# Eval("RequestID","{0}") %>' Text='<%# Bind("RequestID") %>'></asp:LinkButton>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="left" />
                                </asp:TemplateField>
                                <%--  <asp:BoundField HeaderText="RTI Request ID"  DataField="RequestID" >
                                            <ItemStyle HorizontalAlign="left"  />
                                            <HeaderStyle HorizontalAlign="left" />
                                        </asp:BoundField>--%>
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
                        <%-- <div class="col-sm-4">
                    <label class="control-label">Number Of Rti Submitted :</label>
                </div>
                <div class="col-sm-4">
                    <asp:LinkButton ID="lnk_rti_number" runat="server" ></asp:LinkButton>
                </div>--%>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>

