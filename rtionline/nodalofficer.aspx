<%@ Page Title="" Language="C#" MasterPageFile="~/admin_master.master" AutoEventWireup="true" CodeFile="nodalofficer.aspx.cs" Inherits="nodalofficer" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CPH_MasterLogin" Runat="Server">
     <div class="form-horizontal">
        <div class="col-sm-2"></div>
        <div class="col_two_third">
            <div class="panel panel-collapse ">
                <div class="panel-title bgpanel">
                        <div class="panel-heading">
                        <h3> Welcome <asp:Label ID="lbl_name" runat="server" CssClass="text-primary"></asp:Label> </h3>
                        </div>
                    </div>
                </div>
            </div>
        </div>
</asp:Content>

