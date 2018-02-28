<%@ Control Language="C#" AutoEventWireup="true" CodeFile="RTIDataForApplicant.ascx.cs" Inherits="controls_RTIDataForApplicant" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<script type="text/javascript">
    var count = "500";
    function limiter() {
        document.getElementById("ContentPlaceHolder1_RTIDataForApplicant_lblcounter").innerHTML = count;
        var tex = document.getElementById("ContentPlaceHolder1_RTIDataForApplicant_txt_description").value;
        var len = tex.length;
        if (len > count) {
            tex = tex.substring(0, count);
            document.getElementById("ContentPlaceHolder1_RTIDataForApplicant_txt_description").value = tex;
            return false
        }
        document.getElementById("ContentPlaceHolder1_RTIDataForApplicant_lblcounter").innerHTML = count - len;
    }
</script>


<div class="panel-body" style="background-color: #f9f8f8;">
    <div class="form-horizontal">
        <div class="panel-heading ">
            <h5 class="panel-title text-center"><%=Resources.Resource.RTIDataforApplicant %>   </h5>
        </div>
        <div class="form-group">
            <div class="col-sm-3">
                <label class="text-primary"><%=Resources.Resource.ActionDescription %> </label>
            </div>
            <div class="col-sm-9">
                <asp:TextBox ID="txt_description" CssClass="form-control lol " Height="150px" MaxLength="1000" runat="server" TextMode="MultiLine" OnkeyUp="limiter()" ValidationGroup="vg"></asp:TextBox>
                <asp:RequiredFieldValidator ID="rfv_description" runat="server" ControlToValidate="txt_description" Display="Dynamic"
                    ErrorMessage="Description is required" ValidationGroup="vg" CssClass="alert-danger" Font-Bold="True"></asp:RequiredFieldValidator>
                <asp:Label ID="lblcounter" runat="server" CssClass="text-danger" Text="500"></asp:Label>
                <asp:Label ID="lblcounter2" CssClass="text-danger" runat="server">character left</asp:Label>
            </div>
        </div>

        <asp:UpdatePanel ID="udp" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                <div class="form-group">
                    <div class="col-sm-3">
                        <label class="text-primary"><%=Resources.Resource.DoYouWantToUploadFile %> :</label>
                    </div>
                    <div class="col-sm-3">
                        <asp:DropDownList ID="ddl_fileup" runat="server" CssClass="form-control" AutoPostBack="true" ValidationGroup="vg" OnSelectedIndexChanged="ddl_fileup_SelectedIndexChanged">
                            <asp:ListItem Value="0" Text="Select"></asp:ListItem>
                            <asp:ListItem Value="Y" Text="Yes"></asp:ListItem>
                            <asp:ListItem Value="N" Text="No"></asp:ListItem>
                        </asp:DropDownList>
                        <asp:RequiredFieldValidator ID="rfv_fil_up" runat="server" ControlToValidate="ddl_fileup" Display="Dynamic"
                            ErrorMessage=" File Upload required" ValidationGroup="vg" CssClass="alert-danger" InitialValue="0" Font-Bold="True"></asp:RequiredFieldValidator>
                    </div>
                </div>

                <div class="form-group" id="div_file_upload" runat="server" visible="false">
                    <div class="col-sm-3">
                        <label class="text-primary"><%=Resources.Resource.InsertFile %>:</label>
                        <label class="text-danger" style="font-size: small"><%=Resources.Resource.OnlyPdf_WordIsAcceptable %>  </label>
                    </div>
                    <div class="col-sm-3">
                        <asp:FileUpload ID="fu_action" runat="server" />
                        <asp:RequiredFieldValidator ID="rfv_fu" runat="server" ValidationGroup="vg" Display="Dynamic" ControlToValidate="fu_action" ErrorMessage="File is Required" CssClass="alert-danger"></asp:RequiredFieldValidator>
                    </div>
                    <div class="col-sm-3">
                        <label class="text-primary"><%=Resources.Resource.FileDescription %> :</label>
                    </div>
                    <div class="col-sm-3">
                        <asp:TextBox ID="txt_file_desc" runat="server" ValidationGroup="vg" CssClass="form-control"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="rfv_file_desc" runat="server" ValidationGroup="vg" ControlToValidate="txt_file_desc" ErrorMessage="File Discription is required" CssClass="alert-danger" Display="Dynamic"></asp:RequiredFieldValidator>
                    </div>
                </div>

                <%--    </ContentTemplate>
        </asp:UpdatePanel>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>--%>
                <%--<div id="div_meeting" runat="server" visible="true">--%>

                <%--<div class="form-group" id="div1" runat="server">--%>
                <div class="form-group">
                    <div class="col-sm-4">
                        <label class="text-primary"><%=Resources.Resource.DoYouWantMeetingWithApplicant %> </label>

                    </div>
                    <div class="col-sm-3">
                        <asp:RadioButtonList ID="RadioMeeting" CssClass="RadioButtonWidth" RepeatDirection="Horizontal" AutoPostBack="true" OnSelectedIndexChanged="RadioMeeting_SelectedIndexChanged" runat="server" RepeatLayout="Flow">
                            <asp:ListItem Text="YES" Value="Y"></asp:ListItem>
                            <asp:ListItem Text="NO" Value="N"></asp:ListItem>
                        </asp:RadioButtonList>
                    </div>
                    <%--</div>--%>
                    <div id="div2" runat="server" visible="false">
                        <div class="col-sm-2">
                            <label class="text-primary"><%=Resources.Resource.MeetingDate %></label>
                        </div>
                        <div class="col-sm-2">
                            <asp:TextBox ID="txt_meeting" runat="server" ValidationGroup="vg" CssClass="form-control" Enabled="false"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfv_meeting" runat="server" ErrorMessage="choose a Right Meeting date " Display="Dynamic" ControlToValidate="txt_meeting" Font-Bold="True" CssClass="alert-danger" ValidationGroup="vg">Meeting date required</asp:RequiredFieldValidator>
                            <asp:CompareValidator ID="comval1" runat="server" ControlToValidate="txt_meeting" CssClass="alert-danger" ErrorMessage="Date Should Not less then Today" 
                                Display="Dynamic" Operator="GreaterThanEqual" SetFocusOnError="True" Type="Date" ValidationGroup="vg" ></asp:CompareValidator>
                            
                        </div>
                        <div class="col-sm-1">
                            <asp:ImageButton ID="ImgBtn1" runat="server" ImageAlign="Middle" Height="30" Width="30" ImageUrl="~/images/calendar-icon.png" />
                            <asp:CalendarExtender ID="CalendarExtender2" runat="server" Enabled="True" Format="dd-MM-yyyy" PopupButtonID="ImgBtn1" FirstDayOfWeek="Monday" PopupPosition="BottomRight" TargetControlID="txt_meeting" TodaysDateFormat="dd/MM/yyyy"></asp:CalendarExtender>

                        </div>
                    </div>
                </div>

                <div class="form-group">
                    <div class="col-sm-4">
                        <label class="text-primary"><%=Resources.Resource.PayAdditionalFees %></label>

                    </div>
                    <div class="col-sm-3">
                        <asp:RadioButtonList ID="rbl_additional_fees" CssClass="RadioButtonWidth" RepeatDirection="Horizontal" AutoPostBack="true" OnSelectedIndexChanged="Radioadditional_fees_SelectedIndexChanged" runat="server" RepeatLayout="Flow">
                            <asp:ListItem Text="YES" Value="Y"></asp:ListItem>
                            <asp:ListItem Text="NO" Value="N"></asp:ListItem>
                        </asp:RadioButtonList>
                    </div>
                    <div id="div_additional_fees" runat="server" visible="false">
                        <div class="col-sm-2">
                            <label class="text-primary"><%=Resources.Resource.FeesAmount %> </label>
                        </div>
                        <div class="col-sm-3">
                            <asp:TextBox ID="txt_amount" runat="server" ValidationGroup="vg" CssClass="form-control" MaxLength="10"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfv_txt_amount" runat="server" ValidationGroup="vg" ControlToValidate="txt_amount" ErrorMessage="Amount is required" CssClass="alert-danger" Display="Dynamic"></asp:RequiredFieldValidator>
                            <%--<asp:RegularExpressionValidator ID="rev_txt_amount" runat="server" ValidationExpression="((\d+)+(\.\d+))$" ErrorMessage="Please enter valid decimal number" ControlToValidate="txt_amount" CssClass="alert-danger" Display="Dynamic" />--%>
                            <asp:RegularExpressionValidator ID="rev_txt_amount" runat="server" ValidationExpression="((\d+)((\.\d{1,2})?))$" ErrorMessage="Please enter valid decimal number" ControlToValidate="txt_amount" CssClass="alert-danger" Display="Dynamic" />
                        </div>
                    </div>

                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
        <div class="form-group center">
            <asp:Button ID="btnSubmit" runat="server" Text="<%$Resources:Resource, Submit %>" ValidationGroup="vg" CssClass="button button-3d button-black nomargin btn btn-primary" ForeColor="White" OnClick="btnSubmit_Click" />
        </div>
    </div>
</div>
