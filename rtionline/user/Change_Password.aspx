<%@ Page Title="" Language="C#" MasterPageFile="~/UserMaster.master" AutoEventWireup="true" CodeFile="Change_Password.aspx.cs" Inherits="user_Change_Password" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="~/controls/changepasswordaall.ascx" TagPrefix="uc1" TagName="changepasswordaall" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
   <%-- <div class="form-horizontal">
        <div class="panel  panel-info">
            <div class="panel-heading">
                <div class="panel-title">
                  Change Password
                </div>
                </div>
             <span class="text-danger"> &nbsp &nbsp&nbsp&nbsp&nbsp Password Policy: Password must have atleast 8 characters  maximum 15 characters. It must contain one capital alphabate, one small alphabate, one number and one special character. </span>
           
            <div class="panel-body">
                <div class="form-group">
                    <div class="col-sm-4">
                        <asp:Label ID="lbl_password" runat="server" CssClass="lbltxt" Text="Password :"> </asp:Label>
                    </div>
                    <div class="col-sm-4">
                        <asp:TextBox ID="txt_Password" runat="server" CssClass="form-control" Visible="true" TextMode="Password" MaxLength="15"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="Rfv_PAss" runat="server" ControlToValidate="txt_Password" ValidationGroup="vg" Display="Dynamic" CssClass="alert-danger" SetFocusOnError="true" ErrorMessage="Please Select Password"></asp:RequiredFieldValidator>
                    </div>
                </div>
                <div class="form-group">
                    <div class="col-sm-4">
                        <asp:Label ID="lbl_newpass" runat="server" CssClass="lbltxt" Text="Create New Password :"> </asp:Label>
                    </div>
                    <div class="col-sm-4">
                        <asp:TextBox ID="txt_new_pass1" runat="server" CssClass="form-control" TextMode="Password" MaxLength="15"></asp:TextBox>
                        <asp:RegularExpressionValidator ID="Rev_password" runat="server" ValidationGroup="vg" ControlToValidate="txt_new_pass1" ValidationExpression="^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[$#@$^!%*?&])[A-Za-z\d$@#$^)(!%*?&]{8,15}" Display="Dynamic" ErrorMessage="Password must contain: Minimum 8 and Maximum 15 characters atleast 1 UpperCase Alphabet, 1 LowerCase Alphabet, 1 Number and 1 Special Character" CssClass="text-danger" ToolTip=" Minimum 8 and Maximum 15 characters atleast 1 UpperCase Alphabet, 1 LowerCase Alphabet, 1 Number and 1 Special Character"></asp:RegularExpressionValidator>
                        <asp:RequiredFieldValidator ID="rfv_Password" runat="server" ControlToValidate="txt_new_pass1" ValidationGroup="vg" Display="Dynamic" CssClass="alert-danger" SetFocusOnError="true" ErrorMessage="Please Select Password"></asp:RequiredFieldValidator>
                        <asp:PasswordStrength ID="pwdStrength" TargetControlID="txt_new_pass1" StrengthIndicatorType="Text" PrefixText="Strength:" HelpStatusLabelID="lblhelp" PreferredPasswordLength="8"
                            MinimumNumericCharacters="1" MinimumSymbolCharacters="1" TextStrengthDescriptions="Very Poor;Weak;Average;Good;Excellent" TextStrengthDescriptionStyles="VeryPoorStrength;WeakStrength;
                        AverageStrength;GoodStrength;ExcellentStrength"
                            DisplayPosition="BelowLeft" RequiresUpperAndLowerCaseCharacters="true" MinimumLowerCaseCharacters="1" MinimumUpperCaseCharacters="1" runat="server" HelpHandlePosition="BelowLeft" TextCssClass="alert-danger" />
                    </div>
                </div>
                <div class="form-group">
                    <div class="col-sm-4">
                        <asp:Label ID="lbl_repass" runat="server" CssClass="lbltxt" Text="Repeat New Password :"> </asp:Label>
                    </div>
                    <div class="col-sm-4">
                        <asp:TextBox ID="txt_repass" runat="server" CssClass="form-control" TextMode="Password" MaxLength="15"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="rfv_repass" runat="server" ControlToValidate="txt_repass" ValidationGroup="vg" Display="Dynamic" CssClass="alert-danger" SetFocusOnError="true" ErrorMessage="Please Re Enter Your Password"></asp:RequiredFieldValidator>
                        <asp:CompareValidator ID="comp_repass" runat="server" ControlToCompare="txt_new_pass1" ControlToValidate="txt_repass" Operator="Equal" CssClass="alert-danger" SetFocusOnError="true" ErrorMessage="Passsword Mismatch With New Password  Please Check Your Password That You Enter As New Password" Display="Dynamic"></asp:CompareValidator>
                    </div>
                </div>
            </div>
                 <div class="form-group"></div>
            <div class="row ">
                <center>
                <asp:Button ID="btn_pass" runat="server" ValidationGroup="vg" Text="submit" CssClass="btn-success" OnClick="btn_pass_Click" />
            </center>
            </div>
        </div>
    </div>--%>
    <uc1:changepasswordaall runat="server" ID="changepassword" />
</asp:Content>

