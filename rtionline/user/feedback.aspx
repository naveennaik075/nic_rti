<%@ Page Title="" Language="C#" MasterPageFile="~/UserMaster.master" AutoEventWireup="true" CodeFile="feedback.aspx.cs" Inherits="user_feedback" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="MSCaptcha" Namespace="MSCaptcha" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script type="text/javascript">
    
        var count1 = "500";
        var lent=0;
        function countChars() {
            document.getElementById("ContentPlaceHolder1_charcount").innerHTML = count1-lent;
            var text = document.getElementById("ContentPlaceHolder1_txt_feedback").value;
            lent = text.length;
            if (lent > count1) {
                text = text.substring(0, count1);
                document.getElementById("ContentPlaceHolder1_txt_feedback").value = text;
                return false;
            }
            document.getElementById("ContentPlaceHolder1_charcount").innerHTML = count1 - lent;
        }

        
    </script>

    <div class="form-horizontal">
        <div class="panel panel-info">
            <div class="panel-heading panel-collapse">
                <div class="title-border-color text-center">
                    <h3><%=Resources.Resource.FeedBack %></h3>
                </div>
            </div>
            <div class="panel panel-body ">
                <div class="container">
                    <div class=" form-group">
                        <div class=" col-sm-12 text-primary">
                            <label class=" text-primary"><%=Resources.Resource.FeedBackText %> </label>
                        </div>
                        <div class="col-sm-4 text-left text-warning ">
                            <%=Resources.Resource.MandatoryField %>
                        </div>
                    </div>
                </div>
                <div class="container">
                    <asp:UpdatePanel ID="udp" runat="server">
                        <ContentTemplate>
                            <div class="form-group">
                        <div class="col-sm-3">
                            <%=Resources.Resource.SubjectOfFeedback %>  <span class="text-danger">*</span>
                        </div>
                        <div class="col-sm-6">
                            <asp:DropDownList ID="ddl_subject" runat="server" CssClass="form-control" OnSelectedIndexChanged="ddl_subject_SelectedIndexChanged" AutoPostBack="true">
                                <asp:ListItem Value="0" Text="<%$Resources:Resource, SelectSubject %>"> </asp:ListItem>
                                <asp:ListItem Value="1" Text="<%$Resources:Resource, WebSiteContent %>">  </asp:ListItem>
                                <asp:ListItem Value="2" Text="<%$Resources:Resource, FormFilling %>"> </asp:ListItem>
                                <asp:ListItem Value="3" Text="<%$Resources:Resource, NavigationInWebsite %>"> </asp:ListItem>
                                <asp:ListItem Value="4" Text="<%$Resources:Resource, Other %>"> </asp:ListItem>
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="rfv_ddl" runat="server" ValidationGroup="vg" SetFocusOnError="true"
                                 ControlToValidate="ddl_subject" ErrorMessage="Subject Is Required" Display="Dynamic" InitialValue="0"
                                 CssClass="alert-danger" ></asp:RequiredFieldValidator>
                        </div>
                    </div>
                    <div class="form-group" id="div_sub" runat="server" visible="false">
                    <div class="col-sm-3">Please Specify</div>
                    <div class="col-sm-6">
                        <asp:TextBox ID="txt_sub" runat="server" CssClass="form-control" ValidationGroup="vg"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="rfv_txt_sub" runat="server" ControlToValidate="txt_sub"  ValidationGroup="vg"
                             SetFocusOnError="true" ErrorMessage="Subject Is Required" Display="Dynamic" CssClass="alert-danger" Enabled="false" />
                    </div></div>
                 
                  
                       <div class="form-group">
                        <div class="col-sm-3">
                            <%=Resources.Resource.YourValuableFeedback %>  <span class="text-danger">*</span>
                        </div>
                        <div class="col-sm-6    ">
                            <asp:TextBox ID="txt_feedback" runat="server" ValidationGroup="vg" TextMode="MultiLine" CssClass="form-control" onkeyup="countChars();" placeholder="<%$Resources:Resource, MessagePlaceHolder %>" ></asp:TextBox>
                            <asp:Label runat="server" id="charcount" CssClass="text-danger" Text="500"></asp:Label>  <%=Resources.Resource.CharRemain %> 
                         <asp:RequiredFieldValidator ID="rfv_first_name" runat="server" ControlToValidate="txt_feedback" ErrorMessage="Feedback is Required" Display="Dynamic" CssClass="alert-danger" SetFocusOnError="true" ValidationGroup="vg"></asp:RequiredFieldValidator>
                        </div>
                        <div class="col-sm-3"></div>
                    </div>
                    <div class="form-group">
                        <div class="col-sm-3 "> <%=Resources.Resource.SecurityCode %>  <span class="text-danger">*</span> </div>
                        <div class="col-sm-3">
                            <asp:TextBox ID="txtCaptcha" runat="server" CssClass="form-control" ValidationGroup="vg"></asp:TextBox>
                             <asp:RequiredFieldValidator ID="rfv_captcha" runat="server" ControlToValidate="txtCaptcha" ErrorMessage="Security code is required is Required" Display="Dynamic" CssClass="alert-danger" SetFocusOnError="true" ValidationGroup="vg"></asp:RequiredFieldValidator>
                       
                        </div>
                        <div class="col-sm-3">
                            <cc1:CaptchaControl ID="Captcha1" runat="server" CaptchaBackgroundNoise="Low" CaptchaLength="5"
                                CaptchaHeight="40" CaptchaWidth="270" CaptchaMinTimeout="5" CaptchaMaxTimeout="240"
                                FontColor="#D20B0C" NoiseColor="#B1B1B1" />
                        </div>
                        <div class="col-sm-3">
                            <asp:ImageButton ID="imgbtn_refresh" ImageUrl="~/refresh.png" runat="server" />
                        </div>
                    </div>
                    <div class="form-group">
                        <div class="col-sm-4">
                        </div>
                        <div class="col-sm-4 aligncenter">
                            <asp:Button ID="btn_submit" OnClick="btn_submit_Click" runat="server" ValidationGroup="vg" Text="<%$Resources:Resource, Submit %>" CssClass="button button-3d button-black nomargin btn btn-primary"/>

                            
                       

                   
                        </div>
                              </ContentTemplate>
                    </asp:UpdatePanel>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>

