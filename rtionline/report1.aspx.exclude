<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="report1.aspx.cs" Inherits="report" %>

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
       <asp:GridView ID="GV_Department"  runat="server" AllowPaging="True" PageSize="15"
            AutoGenerateColumns="False" ShowHeaderWhenEmpty="True" DataKeyNames="dept_id"
            OnPageIndexChanging="GV_Department_PageIndexChanging"
            CssClass="table table-striped table-bordered  table-hover table-grid">
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
                <asp:BoundField HeaderText="Department Id" DataField="dept_id" />
                <asp:BoundField HeaderText="Department Name" DataField="dept_name" />

            </Columns>

            <EmptyDataTemplate>No Records Available</EmptyDataTemplate>
            <PagerSettings Mode="NumericFirstLast" />
            <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="left" CssClass="cssPager" />

        </asp:GridView>

         </asp:Panel>
</asp:Content>

