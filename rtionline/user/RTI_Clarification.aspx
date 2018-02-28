<%@ Page Title="" Language="C#" MasterPageFile="~/UserMaster.master" AutoEventWireup="true" CodeFile="RTI_Clarification.aspx.cs" Inherits="user_RTI_Clarification" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script type="text/javascript">
        function setTarget() {
            document.forms[0].target = "_blank";
        }

 
        var count1 = "500";
        function limiterRTI() {

            document.getElementById("ContentPlaceHolder1_limit").innerHTML = count1;
            var text = document.getElementById("ContentPlaceHolder1_txt_RequestApplicationText").value;
            var lent = text.length;
            if (lent > count1) {
                text = text.substring(0, count1);
                document.getElementById("ContentPlaceHolder1_txt_RequestApplicationText").value = text;
                return false;
            }
            document.getElementById("ContentPlaceHolder1_limit").innerHTML = count1 - lent;
        }

    </script>

    <style type="text/css">
        /*td, th {
            padding: 2px !important;
        }*/
    </style>
    <div class="form-horizontal ">
        <div class="panel panel-success panel-group">
            <div class="panel-heading ">
                <div class="panel-title center">
                    <h3>
                        <asp:Label ID="lbl_rti_Id" runat="server" CssClass=" text-primary"><%=Resources.Resource.RTIDetails %>  </asp:Label>
                    </h3>
                </div>
            </div>
            <div class="panel-body">
                <div class="panel panel-info">
                    <div class="panel-heading">
                        <h4 class="panel-title">
                            <a data-toggle="collapse" href="#Detailapplicatn" aria-expanded="true"><%=Resources.Resource.DetailOfApplicant %> </a>
                        </h4>
                    </div>
                    <div id="Detailapplicatn" class="panel-collapse collapse in " aria-expanded="true">
                        <div class="panel-body">
                            <div class="form-group">
                                <div class="col-sm-1"></div>
                                <div class="col-sm-2  text-primary">
                                    <%=Resources.Resource.Name %> 
                                </div>
                                <div class="col-sm-3">
                                    <asp:Label ID="lbl_applicant_name" runat="server" CssClass=""></asp:Label>
                                </div>
                                <div class="col-sm-1"></div>
                                <div class="col-sm-2  text-primary">
                                    <%=Resources.Resource.Gender %> 
                                </div>
                                <div class="col-sm-3">
                                    <asp:Label ID="Lbl_gender" runat="server" CssClass=""></asp:Label>
                                </div>
                            </div>
                            <div class="form-group">
                                <div class="col-sm-1"></div>
                                <div class="col-sm-2  text-primary">
                                     <%=Resources.Resource.MobileNo %>
                                </div>
                                <div class="col-sm-3">
                                    <asp:Label ID="lbl_mobile" runat="server" CssClass=""></asp:Label>
                                </div>
                                <div class="col-sm-1"></div>
                                <div class="col-sm-2  text-primary">
                                    <%=Resources.Resource.EmailID %>
                                </div>
                                <div class="col-sm-3">
                                    <asp:Label ID="lbl_email" runat="server" CssClass=""></asp:Label>
                                </div>
                            </div>
                            <div class="form-group">
                                <div class="col-sm-1"></div>
                                <div class="col-sm-2  text-primary">
                                     <%=Resources.Resource.Address %>
                                </div>
                                <div class="col-sm-9">
                                    <asp:Label ID="Lbl_address" runat="server" CssClass=""></asp:Label>
                                </div>
                            </div>

                            <div class="form-group" id="div_state" runat="server">
                                <div class="col-sm-1"></div>
                                <div class="col-sm-2  text-primary">
                                     <%=Resources.Resource.District %>
                                </div>
                                <div class="col-sm-3">
                                    <asp:Label ID="lbl_district" runat="server" CssClass=""></asp:Label>
                                </div>
                                <div class="col-sm-1"></div>
                                <div class="col-sm-2  text-primary">
                                     <%=Resources.Resource.State %>
                                </div>
                                <div class="col-sm-3">
                                    <asp:Label ID="lbl_state" runat="server" CssClass=""></asp:Label>
                                </div>
                            </div>
                            <div class="form-group">
                                <div class="col-sm-1"></div>
                                <div class="col-sm-2  text-primary">
                                     <%=Resources.Resource.Country %>
                                </div>
                                <div class="col-sm-3">
                                    <asp:Label ID="Lbl_country" runat="server" CssClass=""></asp:Label>
                                </div>
                                <div class="col-sm-1"></div>
                                <div class="col-sm-2  text-primary">
                                     <%=Resources.Resource.Pincode %>
                                </div>
                                <div class="col-sm-3">
                                    <asp:Label ID="lbl_pincode" runat="server" CssClass=""></asp:Label>
                                </div>
                            </div>
                            <div class="form-group">
                                <div class="col-sm-1"></div>
                                <div class="col-sm-2  text-primary">
                                     <%=Resources.Resource.Subject %>
                                </div>
                                <div class="col-sm-3">
                                    <asp:Label ID="lbl_subject" runat="server" CssClass=""></asp:Label>
                                </div>
                                <div class="col-sm-1"></div>
                                <div class="col-sm-2 text-primary">
                                     <%=Resources.Resource.RTIStatus %> 
                                </div>
                                <div class="col-sm-3">
                                    <asp:Label ID="lbl_status" runat="server"></asp:Label>
                                </div>
                            </div>

                            <div class="form-group">
                                <div class="col-sm-1"></div>
                                <div class="col-sm-2  text-primary">
                                   <%=Resources.Resource.Description %>  
                                </div>
                                <div class="col-sm-9">
                                    <asp:Label ID="lbl_detail" runat="server" CssClass=""></asp:Label>
                                </div>
                            </div>
                            <div class="form-group" runat="server" id="divfile">
                                <div class="col-sm-1"></div>
                                <div class="col-sm-2  text-primary">
                                   <%=Resources.Resource.FileDescription %>    
                                </div>
                                <div class="col-sm-3">
                                    <asp:Label ID="lbl_file_dtl" runat="server" CssClass=""></asp:Label>
                                </div>
                                <div class="col-sm-1"></div>
                                <div class="col-sm-2  text-primary">
                                   <%=Resources.Resource.SupportingFile %>    
                                </div>
                                <div class="col-sm-3">
                                    <asp:LinkButton ID="lnk_file" runat="server" Text="" OnClientClick="setTarget();"  OnClick="lnk_file_Click"></asp:LinkButton>
                                    
                                </div>
                            </div>
                            
                        </div>
                    </div>
                </div>
                <div class="panel panel-info " id="pnl_result" runat="server">
                    <div class="panel-heading">
                        <h4 class="panel-title">
                            <a data-toggle="collapse" href="#Dtlgrid" aria-expanded="true"><%=Resources.Resource.ResultOfRTI %>  </a>
                        </h4>
                    </div>
                    <div id="Dtlgrid" class="panel-collapse collapse in">
                       
                        <div class="panel-body">

                            <div class="form-horizontal">
                                <div class="form-group">
                                    <div class="col-sm-1"></div>
                                    <div class="col-sm-2 text-primary">
                                       <%=Resources.Resource.ClarificationNeed %> 
                                    </div>
                                    <div class="col-sm-9">
                                        <asp:Label ID="lbl_resultdesc" runat="server" BorderWidth="1px"></asp:Label>
                                        <asp:HiddenField ID="h_RTI_ID" runat="server" />
                                    </div>
                                </div>
                                <div class="form-group">
                                    <div class="col-sm-1"></div>
                                    <div class="col-sm-2 text-primary">
                                       <%=Resources.Resource.ClarificationDescription %> 
                                    </div>
                                    <div class="col-sm-9">
                                        <asp:TextBox ID="txt_RequestApplicationText" runat="server" ValidationGroup="vg" MaxLength="500" Rows="5" TextMode="MultiLine" onkeyup="limiterRTI();" CssClass=" form-control" placeholder="<%$Resources:Resource, Ex_ApplicationDetailsHere %>"></asp:TextBox>
                                        <asp:Label ID="lblChar" runat="server" CssClass="text-danger" Text="Characters left."></asp:Label>
                                        <asp:Label ID="limit" runat="server" CssClass="text-danger" Text="500"></asp:Label>
                                        <asp:RequiredFieldValidator ID="RFV_RequestApplicationText" runat="server" ControlToValidate="txt_RequestApplicationText" Display="Dynamic" ErrorMessage="Please Enter Details of RTI" ValidationGroup="vg" CssClass="text-danger"></asp:RequiredFieldValidator>
                                    </div>
                                </div>
                            </div>
                      
                             <div class="form-group">
                                    <div class="col-sm-12 center">
                                        <asp:Button ID="btn_submit" runat="server" OnClick="btn_submit_Click" Text="<%$Resources:Resource, Submit %>" ValidationGroup="vg" />
                                    </div>
                                </div>
                            
                        </div>
                        <div class="form-group">
                           
                        </div>
                    </div>
                    
                </div>
            </div>
        </div>
    </div>

</asp:Content>
