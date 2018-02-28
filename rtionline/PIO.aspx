<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="PIO.aspx.cs" Inherits="report" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CPH_MasterLogin" Runat="Server">
       <style>
        .cssPager td {
            padding-left: 4px;
            padding-right: 4px;
        }
    </style>
   
     <asp:Panel runat="server" ID="pnl_grid" ScrollBars="Both" CssClass="panel panel-info">
                        <div class="row center" >
                            <asp:Label ID="lbl_count" runat="server"  CssClass="text-primary bold"></asp:Label>
                        </div>
        <asp:GridView ID="GV_PIO" runat="server"  AllowPaging="true" PageSize="10" AutoGenerateColumns="false" ShowHeaderWhenEmpty="true"
            CssClass="table table-striped table-bordered  table-hover table-grid " OnPageIndexChanging="GV_PIO_PageIndexChanging">
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
                <asp:BoundField HeaderText=" Name of PIO"  DataField="Name_en">
                    <ItemStyle HorizontalAlign="left" />
                    <HeaderStyle HorizontalAlign="left" />
                </asp:BoundField>
                
            </Columns>
            <EmptyDataTemplate>Action  Records Not Available</EmptyDataTemplate>
            <PagerSettings Mode="NumericFirstLast" />
            <PagerStyle BackColor="White" ForeColor="Black" HorizontalAlign="left" CssClass="cssPager" />
        </asp:GridView>


         </asp:Panel>
</asp:Content>

