<%@ Page Title="" Language="C#" MasterPageFile="~/Master_employee.master" AutoEventWireup="true" CodeFile="RTIPrepareData.aspx.cs" Inherits="Employee_RTIPrepareData" %>


<%@ Register Src="~/controls/RTIDataForApplicant.ascx" TagPrefix="uc1" TagName="RTIDataForApplicant" %>



<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
   
    <uc1:RTIDataForApplicant runat="server" ID="RTIDataForApplicant" />

</asp:Content>

