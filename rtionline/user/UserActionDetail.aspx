<%@ Page Title="" Language="C#" MasterPageFile="~/UserMaster.master" AutoEventWireup="true" CodeFile="UserActionDetail.aspx.cs" Inherits="user_UserActionDetail" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="~/controls/ShowRTIActionDetails.ascx" TagPrefix="uc1" TagName="useractiondetails" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
     <uc1:useractiondetails runat="server" ID="showuseractiondetails" />
     
</asp:Content>

