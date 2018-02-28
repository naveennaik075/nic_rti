<%@ Page Title="" Language="C#" MasterPageFile="~/admin_master.master" AutoEventWireup="true" CodeFile="Approve.aspx.cs" Inherits="_Approve"%>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CPH_MasterLogin" Runat="Server">
    <style>
        .cssPager td {
            padding-left: 4px;
            padding-right: 4px;
        }
    </style>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div style="text-align: left; padding-left: 10px;">
                
            </div>
            <div class="panel-body" style="background-color: #f9f8f8;">
     <div class="form-group">
                                   
                                    <div class="col-sm-3">
                                        <label class="text-primary">District:</label>
                                    </div>
                                    <div class="col-sm-3">
                                        <asp:DropDownList ID="ddl_district" runat="server" AutoPostBack="true" ValidationGroup="vg" CssClass="form-control"
                                            OnSelectedIndexChanged="ddl_district_SelectedIndexChanged">
                                            <asp:ListItem Value="0" Text=" Select "></asp:ListItem>
                                        </asp:DropDownList>
                                    
                                         </div>
      
                                </div>
     
            <div class="panel-body ">
                  //<asp:Panel runat="server" ID="pnl_grid" ScrollBars="Both" CssClass="panel panel-info">
                <div class="form-group">
                     <asp:Label ID="lbl_count" runat="server" CssClass="text-primary bold"></asp:Label>
                    <asp:GridView ID="grd_Office" runat="server" AllowPaging="true" PageSize="20" AutoGenerateColumns="false" ShowHeaderWhenEmpty="true" DataKeyNames="NewOfficeCode"
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
                         <asp:TemplateField HeaderText="NewOfficeCode" Visible="false">
                                <ItemTemplate>
                                    <%#Eval("NewOfficeCode")%>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="left" />
                                <HeaderStyle HorizontalAlign="Left" />
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
                         <asp:BoundField DataField="Fax" HeaderText="Fax  ">
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
                         <asp:BoundField DataField="OfficeCategory" HeaderText="Office Category">
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
                         <asp:TemplateField HeaderText="All">
                                <HeaderTemplate>
                                    <asp:CheckBox ID="chkAllSelect" Text="All" runat="server" AutoPostBack="true" OnCheckedChanged="chkAllSelect_CheckedChanged" />

                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:CheckBox ID="chk_approve" runat="server" Text="Approve" />
                                </ItemTemplate>
                            </asp:TemplateField>

                       
                    </Columns>
                    <EmptyDataTemplate> Records Not Available</EmptyDataTemplate>
                    <PagerSettings Mode="NumericFirstLast" />
                    <PagerStyle BackColor="White" ForeColor="Black" HorizontalAlign="left" CssClass="cssPager" />
                </asp:GridView>
                </div>
//</asp:Panel>
              
          </div>
       
     <div class="form-group center">
         <asp:Label runat="server" Text="" Visible="false" ID="NewOfficeCode"></asp:Label>
                            <asp:Button ID="Button1" CssClass="button button-small button-dark button-rounded" runat="server" Text="Approve" OnClick="btnapprove_Click" />
                </div>
                </ContentTemplate>
        </asp:UpdatePanel>
</asp:Content>

