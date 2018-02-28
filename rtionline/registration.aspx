<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="registration.aspx.cs" Inherits="reg_istration" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="MSCaptcha" Namespace="MSCaptcha" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <style type="text/css">
        .lol {
            resize: none;
        }
    </style>

    <script type="text/javascript">

        //var count1 = "100";
        //function limiter() {
        //    document.getElementById("ContentPlaceHolder2_limit").innerHTML = count1 ;
        //    var text = document.getElementById("ContentPlaceHolder2_txt_Address").value;
        //    var lent = text.length;
        //    if (lent > count1) {
        //        text = text.substring(0, count1);
        //        document.getElementById("ContentPlaceHolder2_txt_Address").value = text;
        //        return false;
        //    }
        //    document.getElementById("ContentPlaceHolder2_limit").innerHTML = count1 - lent;
        //}
        //$(document).ready(function () {
        //    limiter();
        //});

        //// attach the event binding function to every partial update
        //Sys.WebForms.PageRequestManager.getInstance().add_endRequest(function (evt, args) {
        //    limiter();
        //});

        var count = "100";
        var len = 0;
        function limiter() {
            document.getElementById("ContentPlaceHolder2limit").innerHTML = count - len;
            var tex = document.getElementById("ContentPlaceHolder2_txt_Address").value;
            len = tex.length;
            if (len > count) {
                tex = tex.substring(0, count);
                document.getElementById("ContentPlaceHolder2_txt_Address").value = tex;
                return false
            }
            document.getElementById("ContentPlaceHolder2_limit").innerHTML = count - len;
        }
    </script>

    <div class="panel-collapse panel panel-success">
        <div class="panel-heading">
            <div class="panel-title">
               <%=Resources.Resource.Registration %> 
            </div>
        </div>
        <span class="text-danger "> <%=Resources.Resource.MandatoryField %>  </span>
        <br />
        <span class="text-danger"> <%=Resources.Resource.PasswordPolicy %>  </span>
        <div class="panel-body">
            <div class="form-horizontal">
                <div class="panel panel-info">
                    <div class="panel-heading">
                        <div class="panel-title"> <%=Resources.Resource.UserDetails %> </div>
                    </div>
                    <div class="panel-body">
                        <div class="form-group">
                            <div class="col-sm-2 text-primary ">
                                <asp:Label ID="lbl_First_name" runat="server" Text="<%$Resources:Resource, Name %>"></asp:Label>
                                <span class="text-danger">*</span>
                            </div>
                            <div class="col-sm-4">
                                <asp:TextBox ID="txt_first_name" runat="server" placeholder="<%$Resources:Resource, Ex_Name %>" ValidationGroup="vg" MaxLength="50" CssClass="form-control"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="rfv_first_name" runat="server" ControlToValidate="txt_first_name" ErrorMessage="Name is Required" Display="Dynamic" CssClass="alert-danger" SetFocusOnError="true" ValidationGroup="vg"></asp:RequiredFieldValidator>
                            </div>
                            <div class="col-sm-2 text-primary">
                                <asp:Label ID="lbl_gender" runat="server" Text="<%$Resources:Resource, Gender %>"></asp:Label>
                                <span class="text-danger">*</span>
                            </div>
                            <div class="col-sm-4">
                                <asp:DropDownList ID="ddl_gender" CssClass="form-control" ValidationGroup="vg" runat="server" >
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="rfv_gendera" ValidationGroup="vg" InitialValue="0" runat="server" ControlToValidate="ddl_gender" 
                                    ErrorMessage="please Select a gender" CssClass="alert-danger" SetFocusOnError="true"></asp:RequiredFieldValidator>
                            </div>
                        </div>
                        <div class="form-group">
                            <div class="col-sm-2 text-primary">
                                <asp:Label ID="lbl_mobile" runat="server" Text= "<%$Resources:Resource, MobileNo %>"></asp:Label>
                                <span class="text-danger">*</span>
                            </div>
                            <div class="col-sm-4">
                                <asp:TextBox ID="txt_mobile" runat="server" ValidationGroup="vg" CssClass=" form-control" MaxLength="10" placeholder="<%$Resources:Resource, Ex_Mobile %>"></asp:TextBox>
                                <asp:RegularExpressionValidator ID="rev_mobile" runat="server" ControlToValidate="txt_mobile" CssClass="alert-danger" Display="Dynamic" ErrorMessage="Please Enter a valid Number" SetFocusOnError="True" ValidationExpression="^[1-9][0-9]{9}$" ValidationGroup="vg"></asp:RegularExpressionValidator>
                                <asp:RequiredFieldValidator ID="rfv_mobile" runat="server" ValidationGroup="vg" Display="Dynamic" CssClass="alert-danger" SetFocusOnError="true" ControlToValidate="txt_mobile" ErrorMessage="Mobile Number Is Required"></asp:RequiredFieldValidator>
                            </div>
                            <div class="col-sm-2 text-primary">
                                <asp:Label ID="lbl_email" runat="server" Text= "<%$Resources:Resource, EmailID %>"></asp:Label><span class="text-danger">*</span>
                            </div>
                            <div class="col-sm-4">
                                <asp:TextBox ID="txt_email" runat="server" ValidationGroup="vg" CssClass="form-control" placeholder="<%$Resources:Resource, Ex_Email %>"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="ref_email" runat="server" ControlToValidate="txt_email" ValidationGroup="vg" Display="Dynamic" CssClass="alert-danger" SetFocusOnError="true" ErrorMessage="Please Enter Email ID"></asp:RequiredFieldValidator>
                                <asp:RegularExpressionValidator ID="rev_email" runat="server" ControlToValidate="txt_email" CssClass="alert-danger" Display="Dynamic" ErrorMessage="Please Enter Valid Email ID" SetFocusOnError="True" ValidationGroup="vg" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"></asp:RegularExpressionValidator>
                            </div>
                        </div>
                        <asp:UpdatePanel ID="udp_farm" runat="server">
                            <ContentTemplate>
                                <div class="form-group">
                                    <div class="col-sm-2 text-primary">
                                        <asp:Label ID="lbl_address" runat="server" Text= "<%$Resources:Resource, Address %>"> </asp:Label><span class="text-danger">*</span>
                                    </div>
                                    <div class="col-sm-4">
                                        <asp:TextBox ID="txt_Address" runat="server" TextMode="MultiLine" CssClass="form-control lol" placeholder="<%$Resources:Resource, Ex_Address%>" ValidationGroup="vg" onkeyup="limiter()"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="rfv_address" runat="server" ControlToValidate="txt_Address" ValidationGroup="vg" Display="Dynamic" CssClass="alert-danger" SetFocusOnError="true" ErrorMessage="Please Enter Address"></asp:RequiredFieldValidator>
                                        <asp:Label ID="limit" runat="server" CssClass="text-danger" Text="100"></asp:Label>
                                        <asp:Label ID="lblChar" runat="server" CssClass="text-danger" Text="Characters left."></asp:Label>
                                    </div>
                                    <div class="col-sm-2 text-primary">
                                        <asp:Label ID="lbl_country" runat="server" Text="<%$Resources:Resource, Country %>" ></asp:Label><span class="text-danger">*</span>
                                    </div>
                                    <div class="col-sm-4">
                                        <asp:DropDownList ID="ddl_country" runat="server" ValidationGroup="vg" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddl_country_SelectedIndexChanged">
                                            <asp:ListItem Value="0">Choose Country</asp:ListItem>
                                            <asp:ListItem Value="091">India</asp:ListItem>
                                            <asp:ListItem Value="999">Other</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfv_country" runat="server" ControlToValidate="ddl_country" CssClass="alert-danger" Display="Dynamic" ErrorMessage=" Please Select Country" SetFocusOnError="true" ValidationGroup="vg" InitialValue="0"></asp:RequiredFieldValidator>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <div class="col-sm-2 text-primary">
                                        <asp:Label ID="lbl_state" Text= "<%$Resources:Resource, State %>" runat="server"></asp:Label><span class="text-danger">*</span>
                                    </div>
                                    <div class="col-sm-4" id="ddl" runat="server" visible="true">
                                        <asp:DropDownList ID="ddl_state" runat="server" OnSelectedIndexChanged="ddl_state_SelectedIndexChanged" AutoPostBack="true" AppendDataBoundItems="true" ValidationGroup="vg" CssClass="form-control">
                                        </asp:DropDownList>
                                        <asp:TextBox ID="txt_state_name" runat="server" Visible="False" CssClass="form-control" MaxLength="30" placeholder="<%$Resources:Resource, State %>"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="rfv_state_name" Enabled="false" runat="server" ValidationGroup="vg" ControlToValidate="txt_state_name" CssClass="alert-danger" ErrorMessage="State Name is Required" Display="Dynamic" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                        <asp:RequiredFieldValidator ID="rfv_ddl_state" InitialValue="0" runat="server" SetFocusOnError="true" CssClass="alert-danger" ValidationGroup="vg" ControlToValidate="ddl_state" ErrorMessage="State Name Is Required"></asp:RequiredFieldValidator>
                                    </div>
                                    <div class="col-sm-4" runat="server" visible="false" id="txt">
                                        <asp:TextBox ID="txtState_Other" runat="server" Visible="true" CssClass="form-control" MaxLength="50"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="rfv_state" runat="server" SetFocusOnError="true" CssClass="alert-danger" ValidationGroup="vg" ControlToValidate="txtState_Other" ErrorMessage="Country Name Is Required"></asp:RequiredFieldValidator>
                                    </div>
                                    <div class="col-sm-2 text-primary" id="ctc">
                                        <asp:Label ID="lbl_district" runat="server" Text="<%$Resources:Resource, District %>" ></asp:Label>
                                        <span class="text-danger">*</span>
                                    </div>
                                    <div class="col-sm-4" id="ctc1">
                                        <asp:DropDownList ID="ddl_district" runat="server" AppendDataBoundItems="true" ValidationGroup="vg" CssClass="form-control" OnSelectedIndexChanged="ddl_district_SelectedIndexChanged" AutoPostBack="true">
                                            <asp:ListItem Value="0">Select District</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:TextBox ID="txt_Districtname" runat="server" Visible="False" CssClass="form-control" MaxLength="30" placeholder="<%$Resources:Resource, District %>"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="rfv_district" runat="server" ValidationGroup="vg" ControlToValidate="ddl_district" InitialValue="0" CssClass="alert-danger" ErrorMessage="District is Required" Display="Dynamic" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                        <asp:RequiredFieldValidator ID="rfv_dis_nam" Enabled="false" runat="server" ValidationGroup="vg" ControlToValidate="txt_Districtname" CssClass="alert-danger" ErrorMessage="District Name is Required" Display="Dynamic" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                    </div>
                                </div>

                                <div class="form-group">
                                    <div class="col-sm-2 text-primary">
                                        <asp:Label ID="lbl_pincode" runat="server" Text="<%$Resources:Resource, Pincode %>" ></asp:Label><span class="text-danger">*</span>
                                    </div>
                                    <div class="col-sm-4">
                                        <asp:TextBox ID="txt_pincode" runat="server" ValidationGroup="vg" MaxLength="6" CssClass="form-control" placeholder="<%$Resources:Resource, Pincode %>"></asp:TextBox>
                                        <asp:RegularExpressionValidator ID="rev_pincode" runat="server" ControlToValidate="txt_pincode" CssClass="alert-danger" Display="Dynamic" ErrorMessage="Please Enter Correct Pincode" SetFocusOnError="True" ValidationExpression="\d{6}" ValidationGroup="vg"></asp:RegularExpressionValidator>
                                        <asp:RequiredFieldValidator ID="rfv_pincode" runat="server" ControlToValidate="txt_pincode" CssClass="alert-danger" Display="Dynamic" ErrorMessage="Pincode is required" SetFocusOnError="True" ValidationGroup="vg"></asp:RequiredFieldValidator>
                                    </div>
                                </div>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                </div>
                <div class="panel panel-info">
                    <div class="panel-heading">
                        <div class="panel-title">
                            Login Details                          
                        </div>
                    </div>
                    <div class="panel-body">
                        <div class="form-group">
                            <div class="col-sm-2 text-primary">
                                <asp:Label ID="lbl_usertype" runat="server" Text="<%$Resources:Resource, UserType %>" ></asp:Label><span class="text-danger">*</span>
                            </div>
                            <div class="col-sm-4">
                                <asp:DropDownList ID="ddl_usertype" runat="server" CssClass="form-control" AppendDataBoundItems="true" ValidationGroup="vg">
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="rfv_usertype" runat="server" ControlToValidate="ddl_usertype" ValidationGroup="vg" Display="Dynamic" CssClass="alert-danger" SetFocusOnError="true" ErrorMessage="Please Select User type" InitialValue="0"></asp:RequiredFieldValidator>
                            </div>
                            <div class="col-sm-2 text-primary">
                                <asp:Label ID="lbl_Userid" runat="server" Text= "<%$Resources:Resource, CreateUserID %>" ></asp:Label>
                                <span class="text-danger">*</span>
                            </div>
                            <asp:UpdatePanel ID="Udp2" runat="server"><ContentTemplate> 
                            <div class="col-sm-4">
                                <asp:TextBox ID="txt_Userid" runat="server" ValidationGroup="vg" CssClass=" form-control" OnTextChanged="txt_Userid_TextChanged" AutoPostBack="true" MaxLength="20"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="rfv_userid" runat="server" ControlToValidate="txt_Userid" ValidationGroup="vg" Display="Dynamic" CssClass="alert-danger" SetFocusOnError="true" ErrorMessage="Please Create User Id"></asp:RequiredFieldValidator>
                            </div>
                               </ContentTemplate> </asp:UpdatePanel> 
                        </div>
                        <div class="form-group">
                            <div class="col-sm-2 text-primary">
                                <asp:Label ID="lbl_password" runat="server" Text="<%$Resources:Resource, CreatePassword %>" > </asp:Label>
                                <span class="text-danger">*</span>
                            </div>
                            <div class="col-sm-4">
                                <asp:TextBox ID="txt_password" runat="server" ValidationGroup="vg" TextMode="Password" CssClass="form-control" MaxLength="15" ToolTip="Minimum 8 and Maximum 15 characters atleast 1 UpperCase Alphabet, 1 LowerCase Alphabet, 1 Number and 1 Special Character"></asp:TextBox>
                                <asp:RegularExpressionValidator ID="Rev_password" runat="server" ValidationGroup="vg" ControlToValidate="txt_password" ValidationExpression="^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?!@$%^&*-]).{8,15}$" Display="Dynamic" ErrorMessage="Password must contain: Minimum 8 and Maximum 15 characters atleast 1 UpperCase Alphabet, 1 LowerCase Alphabet, 1 Number and 1 Special Character" CssClass="text-danger" ToolTip=" Minimum 8 and Maximum 15 characters atleast 1 UpperCase Alphabet, 1 LowerCase Alphabet, 1 Number and 1 Special Character"></asp:RegularExpressionValidator>
                                <%--<asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ValidationGroup="vg" ControlToValidate="txt_password" ValidationExpression="^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[$#@$^!%*?&])[A-Za-z\d$@#$^)(!%*?&]{8,15}" Display="Dynamic" ErrorMessage="Password must contain: Minimum 8 and Maximum 15 characters atleast 1 UpperCase Alphabet, 1 LowerCase Alphabet, 1 Number and 1 Special Character" CssClass="text-danger" ToolTip=" Minimum 8 and Maximum 15 characters atleast 1 UpperCase Alphabet, 1 LowerCase Alphabet, 1 Number and 1 Special Character"></asp:RegularExpressionValidator>--%>
                                <asp:RequiredFieldValidator ID="rfv_Password" runat="server" ControlToValidate="txt_password" ValidationGroup="vg" Display="Dynamic" CssClass="alert-danger" SetFocusOnError="true" ErrorMessage="Please Create Password"></asp:RequiredFieldValidator>
                                <asp:PasswordStrength ID="pwdStrength" TargetControlID="txt_password" StrengthIndicatorType="Text" PrefixText="Strength:" HelpStatusLabelID="lblhelp" PreferredPasswordLength="8"
                                    MinimumNumericCharacters="1" MinimumSymbolCharacters="1" TextStrengthDescriptions="Very Poor;Weak;Average;Good;Excellent" TextStrengthDescriptionStyles="VeryPoorStrength;WeakStrength;
                        AverageStrength;GoodStrength;ExcellentStrength"
                                    DisplayPosition="BelowLeft" RequiresUpperAndLowerCaseCharacters="true" MinimumLowerCaseCharacters="1" MinimumUpperCaseCharacters="1" runat="server" HelpHandlePosition="BelowLeft" TextCssClass="alert-danger" />
                            </div>
                            <div class="col-sm-2 text-primary ">
                                <asp:Label ID="lbl_repass" runat="server" Text="<%$Resources:Resource, ConfirmPassword %>" ></asp:Label>
                                <span class="text-danger">*</span>
                            </div>
                            <div class="col-sm-4">
                                <asp:TextBox ID="txt_repassword" runat="server" TextMode="Password" ValidationGroup="vg" MaxLength="15" CssClass=" form-control"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="rfv_repass" runat="server" ControlToValidate="txt_repassword" ValidationGroup="vg" Display="Dynamic" CssClass="alert-danger" SetFocusOnError="true" ErrorMessage="Please Re Enter Your Password"></asp:RequiredFieldValidator>
                                <asp:CompareValidator ID="comp_repass" runat="server" ControlToCompare="txt_password" ControlToValidate="txt_repassword" Operator="Equal" CssClass="alert-danger" SetFocusOnError="true" ErrorMessage="Passsword Mismatch Please Check Your Password That You Entered" Display="Dynamic"></asp:CompareValidator>
                            </div>
                        </div>
                        <div class="form-group">
                            <div class="col-sm-2 text-primary"><%=Resources.Resource.EnterSecurityCode %>  <span class="text-danger">*</span> </div>
                            <div class="col-sm-4">
                                <asp:TextBox ID="txtCaptcha" runat="server" CssClass="form-control"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="rfv_captcha" runat="server" ControlToValidate="txtCaptcha" ValidationGroup="vg" Display="Dynamic" CssClass="alert-danger" SetFocusOnError="true" ErrorMessage="Please enter security code"></asp:RequiredFieldValidator>
                            </div>
                            <div class="col-sm-3">
                                <cc1:CaptchaControl ID="Captcha1" runat="server" CaptchaBackgroundNoise="Low" CaptchaLength="5"
                                    CaptchaHeight="40" CaptchaWidth="270" CaptchaMinTimeout="10" CaptchaMaxTimeout="240"
                                    FontColor="#D20B0C" NoiseColor="#B1B1B1" />
                            </div>
                            <div class="col-sm-1">
                                <asp:ImageButton ID="imgbtn_refresh" ImageUrl="~/refresh.png" runat="server" />
                            </div>
                        </div>
                        <div class="form-group">
                            <div class="col-sm-5"></div>
                            <div class="col-sm-4">
                                <asp:Button ID="btn_submit" runat="server" Text="<%$Resources:Resource, Submit %>"  CssClass="button button-3d button-black nomargin btn btn-primary" OnClick="btn_submit_Click" ValidationGroup="vg" />
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
