<%@ Page Title="" Language="C#" MasterPageFile="~/UserMaster.master" AutoEventWireup="true" CodeFile="User_Mobile_Varification.aspx.cs" Inherits="User_Mobile_Varification" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel ID="udp" runat="server">
        <ContentTemplate>
            <div class="panel">
                <div class="panel-heading text-center">
                    <h3><%=Resources.Resource.MobileVarification %>  </h3>
                </div>
                <div class="form-horizontal">
                    <div class="form-group">
                        <div class="col-sm-2"></div>
                        <div class="col-sm-4">
                            <label class="control-label text-center"> <%=Resources.Resource.YourMobileNumber %>  <span class="text-danger">*</span></label>
                        </div>
                        <div class="col-sm-4">
                            <asp:TextBox ID="txt_mobile" runat="server" MaxLength="10" CssClass="form-control" ValidationGroup="vg" ReadOnly="true"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfv_mobile" runat="server" CssClass="text-danger" ControlToValidate="txt_mobile" SetFocusOnError="true" ErrorMessage="Please Enter Mobile Number" Display="Dynamic" ValidationGroup="vg"></asp:RequiredFieldValidator>
                            <asp:RegularExpressionValidator ID="rev_mobile" runat="server" ControlToValidate="txt_mobile" CssClass="alert-danger" Display="Dynamic" ErrorMessage="Please Enter a valid Number" SetFocusOnError="True" ValidationExpression="^[1-9][0-9]{9}$" ValidationGroup="vg"></asp:RegularExpressionValidator>
                        </div>

                    </div>
                    <div class="form-group">
                        <div class="col-sm-4">
                        </div>
                        <div class="col-sm-4">
                            <asp:Button ID="btn_send" runat="server" CssClass="button button-3d button-black nomargin" Text="<%$Resources:Resource, Send %>" ValidationGroup="vg" OnClick="btn_send_Click" />
                        </div>
                    </div>
                    <div id="div_otp" runat="server" visible="false">
                        <div class="form-group">
                            <div class="col-sm-12 text-center">
                            </div>
                            <div class="col-sm-2"></div>
                            <div class="col-sm-5">
                                <label class="control-label text-center"><%=Resources.Resource.MobileMessage%></label>
                                <span class="text-danger">*</span>
                            </div>
                            <%--<div class="col-sm-4">
                        <label class="form-control text-center">Enter OTP</label>
                    </div>--%>
                            <div class="col-sm-3">
                                <asp:TextBox ID="txt_otp" runat="server" CssClass="form-control" ValidationGroup="vg1"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="rfv_otp" runat="server" CssClass="text-danger" ValidationGroup="vg1" ControlToValidate="txt_otp" ErrorMessage="Please Enter OTP" SetFocusOnError="true"></asp:RequiredFieldValidator>
                            </div>
                        </div>
                        <div class="form-group">
                            <div class="col-sm-4"></div>
                            <div class="col-sm-6">
                                <asp:Button ID="btn_otp" runat="server" ValidationGroup="vg1" Text="<%$Resources:Resource, Submit %>" CssClass="button button-3d button-black nomargin" OnClick="btn_otp_Click" />
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

