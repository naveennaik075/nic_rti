<%@ Page Title="" Language="C#" MasterPageFile="~/Master_employee.master" AutoEventWireup="true" CodeFile="changepass.aspx.cs" Inherits="Employee_changepass" %>

<%@ Register Src="~/controls/changepasswordaall.ascx" TagPrefix="uc1" TagName="changepasswordaall" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <uc1:changepasswordaall runat="server" ID="changepasswordaall" />
</asp:Content>

