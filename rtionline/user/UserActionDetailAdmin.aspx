<%@ Page Title="" Language="C#" MasterPageFile="~/admin_master.master" AutoEventWireup="true" CodeFile="UserActionDetailAdmin.aspx.cs" Inherits="user_UserActionDetailAdmin" %>

<%@ Register Src="~/controls/ShowRTIActionDetails.ascx" TagPrefix="uc1" TagName="ShowRTIActionDetails" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CPH_MasterLogin" Runat="Server">
    <uc1:ShowRTIActionDetails runat="server" ID="ShowRTIActionDetails" />
</asp:Content>

