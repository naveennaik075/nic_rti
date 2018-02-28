<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Forget_password.aspx.cs" Inherits="WEBHOME_Forget_password" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="MSCaptcha" Namespace="MSCaptcha" TagPrefix="cc1" %>
<%--<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>--%>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="container">
        <div class="form-horizontal">
        <div class="panel panel-warning">
            <div class="panel panel-heading">
                <div class="panel-title">
                    <h3 class="text-center"><%=Resources.Resource.ForgetPassword %>
                    </h3>
                </div>
            </div>
            <div class="panel panel-body">
                <div class="form-group ">
                    <label class="control-label">
                         <%=Resources.Resource.MandatoryField %>
                    </label>
                </div>
                <div class="form-group"></div>
                <div id="div2" runat="server" visible="true">
                    <div class="form-group">
                        <div class="col-sm-5">
                            <label class="control-label"><%=Resources.Resource.UserName %> : <span class="text-danger">*</span></label>
                        </div>
                        <div class="col-sm-4">
                            <asp:TextBox ID="txt_username" runat="server" CssClass="form-control" ValidationGroup="vg" MaxLength="15"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfv_username" runat="server" ControlToValidate="txt_username" ValidationGroup="vg" Display="Dynamic" CssClass="alert-danger" SetFocusOnError="true" ErrorMessage="Please Enter User Name"></asp:RequiredFieldValidator>
                        </div>
                    </div>
                    <div class="form-group center">
                        <div class="col-sm-12">
                            <label class="control-label">
                                <%=Resources.Resource.Please %>
                            <asp:LinkButton ID="lnkbtn_otp" runat="server" ValidationGroup="vg" Text="<%$Resources:Resource, ClickHere %>" OnClick="lnkbtn_otp_Click"></asp:LinkButton>
                                <%=Resources.Resource.ToGetVCOnYourRegisteredMobile %></label>
                        </div>
                    </div>
                </div>
                <div id="div1" runat="server" visible="false">
                    <div class="form-group">
                        <div class="col-sm-5">
                            <label class="control-label"><%=Resources.Resource.EnterTheOTPThatHasBeenSentToYourMobile %></label>
                        </div>
                        <div class="col-sm-4">
                            <asp:TextBox runat="server" ID="txt_otp" CssClass="form-control" ValidationGroup="vg1"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfv_otp" runat="server" ControlToValidate="txt_otp" ValidationGroup="vg1" Display="Dynamic" CssClass="alert-danger" SetFocusOnError="true" ErrorMessage="Please Enter OTP"></asp:RequiredFieldValidator>
                        </div>
                        <div class="col-sm-4"></div>
                    </div>
                    <div class="form-group">
                        <div class="col-sm-5">
                            <label class="control-label">
                                <%=Resources.Resource.CreateNewPassword %> <span class="text-danger">*</span>
                            </label>
                        </div>
                        <div class="col-sm-4">
                            <asp:TextBox ID="txt_new_pass" runat="server" CssClass="form-control" ValidationGroup="vg1" TextMode="Password" MaxLength="15"></asp:TextBox>
                            <asp:RegularExpressionValidator ID="Rev_password" runat="server" ValidationGroup="vg" ControlToValidate="txt_new_pass" ValidationExpression="^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?!@$%^&*-]).{8,}$" Display="Dynamic" ErrorMessage="Password must contain: Minimum 8 and Maximum 15 characters atleast 1 UpperCase Alphabet, 1 LowerCase Alphabet, 1 Number and 1 Special Character" CssClass="text-danger" ToolTip=" Minimum 8 and Maximum 15 characters atleast 1 UpperCase Alphabet, 1 LowerCase Alphabet, 1 Number and 1 Special Character" Enabled="false"></asp:RegularExpressionValidator>
                            <asp:RequiredFieldValidator ID="rfv_Password" runat="server" ControlToValidate="txt_new_pass" ValidationGroup="vg1" Display="Dynamic" CssClass="alert-danger" SetFocusOnError="true" ErrorMessage="Please Select Password" ></asp:RequiredFieldValidator>
                            <asp:PasswordStrength ID="pwdStrength" TargetControlID="txt_new_pass" StrengthIndicatorType="Text" PrefixText="Strength:" HelpStatusLabelID="lblhelp" PreferredPasswordLength="8"
                                MinimumNumericCharacters="1" MinimumSymbolCharacters="1" TextStrengthDescriptions="Very Poor;Weak;Average;Good;Excellent" TextStrengthDescriptionStyles="VeryPoorStrength;WeakStrength;
                        AverageStrength;GoodStrength;ExcellentStrength"
                                DisplayPosition="BelowLeft" RequiresUpperAndLowerCaseCharacters="true" MinimumLowerCaseCharacters="1" MinimumUpperCaseCharacters="1" runat="server" HelpHandlePosition="BelowLeft" TextCssClass="alert-danger" />
                        </div>
                    </div>
                    <div class="form-group">
                        <div class="col-sm-5">
                            <label class="control-label"><%=Resources.Resource.RepeatNewPassword %><span class="text-danger">*</span> </label>
                        </div>
                        <div class="col-sm-4">
                            <asp:TextBox ID="txt_repass" runat="server" CssClass="form-control" TextMode="Password" MaxLength="15" ValidationGroup="vg1"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfv_repass" runat="server" ControlToValidate="txt_repass" ValidationGroup="vg1" Display="Dynamic" CssClass="alert-danger" SetFocusOnError="true" ErrorMessage="Please Re Enter Your Password" ></asp:RequiredFieldValidator>
                            <asp:CompareValidator ID="comp_repass" runat="server" ControlToCompare="txt_new_pass" ControlToValidate="txt_repass" Operator="Equal" CssClass="alert-danger" SetFocusOnError="true" ErrorMessage="Passsword Mismatch With New Password  Please Check Your Password That You Enter As New Password" Display="Dynamic" Enabled="false"></asp:CompareValidator>
                        </div>
                    </div>
                    <div class="form-group">
                        <div class="col-sm-5">
                            <label class="control-label"><%=Resources.Resource.SecurityCode %> <span class="text-danger">*</span></label>
                        </div>
                        <div class="col-sm-3">
                            <asp:TextBox ID="txtCaptcha" runat="server" CssClass="form-control"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RFV_ScurityCode" runat="server" ControlToValidate="txtCaptcha"
                                        Display="Dynamic" ErrorMessage="Security code Required" CssClass="alert-danger" ValidationGroup="vg1"></asp:RequiredFieldValidator>
                        </div>
                        <div class="col-sm-3">
                            <cc1:CaptchaControl ID="Captcha1" runat="server" CaptchaBackgroundNoise="Low" CaptchaLength="5"
                                CaptchaHeight="40" CaptchaWidth="270" CaptchaMinTimeout="5" CaptchaMaxTimeout="240"
                                FontColor="#D20B0C" NoiseColor="#B1B1B1" />
                        </div>
                        <div class="col-sm-1">
                            <asp:ImageButton ID="imgbtn_refresh" ImageUrl="~/refresh.png" runat="server" />
                        </div>
                    </div>
                    <div class="form-group center">
                        <asp:Button ID="btn_update" runat="server" ValidationGroup="vg1" OnClick="btn_update_Click" CssClass="button button-3d button-black nomargin" Text="<%$Resources:Resource, Submit %>" />
                    </div>
                   <%-- <div class="form-group">
                        <div class="col-sm-2 text-primary">Enter Scurity Code <span class="text-danger">*</span> </div>
                        <div class="col-sm-4">
                            <asp:TextBox ID="TextBox1" runat="server" CssClass="form-control"></asp:TextBox>
                        </div>
                        <div class="col-sm-3">
                            <cc1:CaptchaControl ID="CaptchaControl1" runat="server" CaptchaBackgroundNoise="Low" CaptchaLength="5"
                                CaptchaHeight="40" CaptchaWidth="270" CaptchaMinTimeout="5" CaptchaMaxTimeout="240"
                                FontColor="#D20B0C" NoiseColor="#B1B1B1" />
                        </div>
                        <div class="col-sm-1">
                            <asp:ImageButton ID="ImageButton1" ImageUrl="~/refresh.png" runat="server" />
                            <asp:RequiredFieldValidator ID="RFV_ScurityCode" runat="server" ControlToValidate="txtCaptcha" Display="Dynamic" ErrorMessage="Security code Required" ForeColor="Red" ValidationGroup="vg"></asp:RequiredFieldValidator>
                        </div>
                    </div>--%>
                </div>
            </div>
        </div>
    </div>
    </div>
    
</asp:Content>

