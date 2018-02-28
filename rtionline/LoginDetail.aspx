<%@ Page Language="C#" AutoEventWireup="true" CodeFile="LoginDetail.aspx.cs" Inherits="LoginDetail" MasterPageFile="~/UserMaster.master" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <style type="text/css">
        .logintime {
            text-align: right;
        }
    </style>
    <p class="logintime">
        <%--  Last Successfull Login
        <asp:Label  id="lbl_LoginTime" runat="server" Text="Label"></asp:Label>--%>
    </p>
    <div class="form-horizontal">
        <div class="portlet box blue-dark">
            <div class="portlet-title">
                <div class="panel-heading">
                    <h5 class="panel-title text-center">
                        <b>
                            <asp:Label ID="lbt_reg_ttl" runat="server" CssClass="text-primary" Text="User Details"></asp:Label>
                        </b>
                    </h5>
                </div>
                <div class="tools">
                    <a class="collapse" href="javascript:;" data-original-title="" title=""></a>
                </div>
            </div>
            <div class="portlet-body " style="display: block;">
                <div class="form-group">
                    <div class="col-sm-3">
                        <asp:Label ID="lbl_userid" runat="server" CssClass="text-primary" Text="User ID :"></asp:Label>
                    </div>
                    <div class="col-sm-3">
                        <asp:TextBox ID="txt_userid" runat="server" CssClass="form-control" ReadOnly="true"></asp:TextBox>
                    </div>
                    <div class="col-sm-3">
                        <asp:Label ID="lbl_lastlogin" runat="server" CssClass="text-primary" Text="Last Successful login:"></asp:Label>
                    </div>
                    <div class="col-sm-3">
                        <asp:TextBox ID="txt_last_login" runat="server" CssClass="form-control" ReadOnly="true"></asp:TextBox>
                    </div>
                </div>
                <div class="form-group">
                    <div class="col-sm-3">
                        <asp:Label ID="lbl_First_name" CssClass="text-primary" runat="server" Text="Name:"></asp:Label>
                    </div>
                    <div class="col-sm-3">
                        <asp:TextBox ID="txt_first_name" runat="server" CssClass="form-control" ReadOnly="true"></asp:TextBox>
                    </div>
                    <div class="col-sm-3">
                        <asp:Label ID="lbl_gender" runat="server" CssClass="text-primary" Text="Gender:"></asp:Label>
                    </div>
                    <div class="col-sm-3">
                        <asp:TextBox ID="txt_gender" runat="server" CssClass="form-control" ReadOnly="true"></asp:TextBox>
                    </div>
                </div>
                <div class="form-group">
                    <div class="col-sm-3">
                        <asp:Label ID="lbl_mobile" runat="server" CssClass="text-primary" Text="Mobile No: "></asp:Label>
                    </div>
                    <div class="col-sm-3">
                        <asp:TextBox ID="txt_mobile" runat="server" CssClass="form-control" ReadOnly="true"></asp:TextBox>
                    </div>
                    <div class="col-sm-3">
                        <asp:Label ID="lbl_email" runat="server" CssClass="text-primary" Text="Email Id:"></asp:Label>
                    </div>
                    <div class="col-sm-3">
                        <asp:TextBox ID="txt_email" runat="server" CssClass="form-control" ReadOnly="true"></asp:TextBox>
                    </div>
                </div>
                <div class="form-group">
                    <div class="col-sm-3">
                        <asp:Label ID="lbl_address" runat="server" CssClass="text-primary" Text="Address:"></asp:Label>
                    </div>
                    <div class="col-sm-3">
                        <asp:TextBox ID="txt_Address" runat="server" CssClass="form-control" ReadOnly="true"></asp:TextBox>
                    </div>
                    <div class="col-sm-3">
                        <asp:Label ID="lbl_pin_code" runat="server" CssClass="text-primary" Text="Pin Code:"></asp:Label>
                    </div>
                    <div class="col-sm-3">
                        <asp:TextBox ID="txt_pin_code" runat="server" CssClass="form-control" ReadOnly="true"></asp:TextBox>
                    </div>
                </div>
                <div class="form-group">
                    <div class="col-sm-3">
                        <asp:Label ID="lbl_reg_year" runat="server" CssClass="text-primary" Text="Registration Year:"></asp:Label>
                    </div>
                    <div class="col-sm-3">
                        <asp:TextBox ID="txt_reg_year" runat="server" CssClass="form-control" ReadOnly="true"></asp:TextBox>
                    </div>
                    <div class="col-sm-3">
                        <asp:Label ID="lbl_password" runat="server" CssClass="text-primary" Text="Password :" Visible="false"> </asp:Label>
                    </div>
                    <div class="col-sm-3">
                        <asp:TextBox ID="txt_Password" runat="server" CssClass="form-control" Visible="false" TextMode="Password"></asp:TextBox>
                         <asp:RequiredFieldValidator ID="Rfv_PAss" runat="server" ControlToValidate="txt_Password" ValidationGroup="vg" Display="Dynamic" CssClass="alert-danger" Enabled="false" SetFocusOnError="true" ErrorMessage="Please Select Password" ></asp:RequiredFieldValidator>
                                    
                    </div>
                </div>
                <div class="form-group" id="div_Password" runat="server" visible="false">
                    <div class="col-sm-3">
                        <asp:Label ID="lbl_newpass" runat="server" CssClass="text-primary" Text="Create New Password :"> </asp:Label>
                    </div>
                    <div class="col-sm-3">
                        <asp:TextBox ID="txt_new_pass" runat="server" CssClass="form-control" TextMode="Password"></asp:TextBox>
                         <asp:RegularExpressionValidator ID="Rev_password" runat="server" ValidationGroup="vg" ControlToValidate="txt_new_pass" ValidationExpression="^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[$#@$^!%*?&])[A-Za-z\d$@#$^)(!%*?&]{8,15}" Display="Dynamic" ErrorMessage="Password must contain: Minimum 8 and Maximum 15 characters atleast 1 UpperCase Alphabet, 1 LowerCase Alphabet, 1 Number and 1 Special Character" CssClass="text-danger" ToolTip=" Minimum 8 and Maximum 15 characters atleast 1 UpperCase Alphabet, 1 LowerCase Alphabet, 1 Number and 1 Special Character" Enabled="false"></asp:RegularExpressionValidator>
                                    <asp:RequiredFieldValidator ID="rfv_Password" runat="server" ControlToValidate="txt_new_pass" ValidationGroup="vg" Display="Dynamic" CssClass="alert-danger" SetFocusOnError="true" ErrorMessage="Please Select Password" Enabled="false"></asp:RequiredFieldValidator>
                                    <asp:PasswordStrength ID="pwdStrength" TargetControlID="txt_new_pass" StrengthIndicatorType="Text" PrefixText="Strength:" HelpStatusLabelID="lblhelp" PreferredPasswordLength="8"
                                        MinimumNumericCharacters="1" MinimumSymbolCharacters="1" TextStrengthDescriptions="Very Poor;Weak;Average;Good;Excellent" TextStrengthDescriptionStyles="VeryPoorStrength;WeakStrength;
                        AverageStrength;GoodStrength;ExcellentStrength"
                                        DisplayPosition="BelowLeft" RequiresUpperAndLowerCaseCharacters="true" MinimumLowerCaseCharacters="1" MinimumUpperCaseCharacters="1" runat="server" HelpHandlePosition="BelowLeft" TextCssClass="alert-danger" />
                               
                    </div>
                    <div class="col-sm-3">
                        <asp:Label ID="lbl_repass" runat="server" CssClass="text-primary" Text="Repeat New Password :"> </asp:Label>
                    </div>
                    <div class="col-sm-3">
                        <asp:TextBox ID="txt_repass" runat="server" CssClass="form-control" TextMode="Password"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="rfv_repass" runat="server" ControlToValidate="txt_repass" ValidationGroup="vg" Display="Dynamic" CssClass="alert-danger" SetFocusOnError="true" ErrorMessage="Please Re Enter Your Password" Enabled="false"></asp:RequiredFieldValidator>
                        <asp:CompareValidator ID="comp_repass" runat="server" ControlToCompare="txt_new_pass" ControlToValidate="txt_repass" Operator="Equal" CssClass="alert-danger" SetFocusOnError="true" ErrorMessage="Passsword Mismatch With New Password  Please Check Your Password That You Enter As New Password" Display="Dynamic" Enabled="false"></asp:CompareValidator>
                    </div>
                </div>
                <div class="row">
                    <div class="col-sm-3"></div>
                    <div class="col-sm-2">
                        <asp:Button ID="btn_update" runat="server" Text="Update Detail" ValidationGroup="vg" CssClass="btn btn-circle" OnClick="btn_update_Click" Visible="false" />
                    </div>
                    <div class="col-sm-4">
                    </div>
                    <div class="col-sm-2">
                        <asp:Button ID="Btn_Password" runat="server" Text="Change Password" ValidationGroup="vg" CssClass="btn btn-circle" OnClick="Btn_Password_Click" />
                    </div>
                </div>
            </div>
        </div>

      <%--  <br />
        <div class="col-sm-3"></div>
        <div class="panel panel-primary col-sm-6">
            <div class="panel-body">
                <div class="col-sm-3"></div>
                <div class="col-sm-8">
                    Request/Appeal Status as on
                <asp:Label ID="lbl_RequestApealStatus" runat="server" Text="Date"></asp:Label>
                </div>
                <div class="col-sm-3"></div>
            </div>
        </div>
        <div class="col-sm-3"></div>
        <br />
        <br />
        <br />
        <br />
        <br />

        <div class="col-sm-2"></div>
        <div class="panel panel-primary col-sm-8">
            <div class="form-group">
                <div class="col-sm-6">
                    <span>Requests </span>
                    <ul class="list-group">
                        <li class="list-group-item">
                            <span class="badge">
                                <asp:Label ID="lbl_Request_Registered" runat="server" Text="0"></asp:Label></span>
                            Registered
                        </li>
                        <li class="list-group-item">
                            <span class="badge">
                                <asp:Label ID="lbl_Request_DisposedOf" runat="server" Text="0"></asp:Label></span>
                            Disposed of
                        </li>
                        <li class="list-group-item">
                            <span class="badge">
                                <asp:Label ID="lbl_Request_Pending" runat="server" Text="0"></asp:Label></span>
                            Pending
                        </li>

                    </ul>
                </div>

                <div class="col-sm-6">
                    <span>Appeals </span>
                    <ul class="list-group">
                        <li class="list-group-item">
                            <span class="badge">
                                <asp:Label ID="lbl_Appeal_Registered" runat="server" Text="0"></asp:Label></span>
                            Registered
                        </li>
                        <li class="list-group-item">
                            <span class="badge">
                                <asp:Label ID="lbl_Appeal_DisposedOf" runat="server" Text="0"></asp:Label></span>
                            Disposed of
                        </li>
                        <li class="list-group-item">
                            <span class="badge">
                                <asp:Label ID="lbl_Appeal_Pending" runat="server" Text="0"></asp:Label></span>
                            Pending
                        </li>
                    </ul>
                </div>
            </div>
        </div>

        </>
    <div class="col-sm-3"></div>
        <br />--%>
    </div>
</asp:Content>
