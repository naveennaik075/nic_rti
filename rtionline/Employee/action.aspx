<%@ Page Title="" Language="C#" MasterPageFile="~/Master_employee.master" AutoEventWireup="true" CodeFile="action.aspx.cs" Inherits="action" EnableEventValidation="false" MaintainScrollPositionOnPostback="true"%>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="~/controls/RTIDataForApplicant.ascx" TagPrefix="uc1" TagName="RTIDataForApplicant" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">


    <script type="text/javascript">

        Sys.WebForms.PageRequestManager.getInstance().add_endRequest(function (evt, args) {

            hidemeet();
            hidefees();
            hide_dispose_file();
            //hide_action_file();

        });

        $(document).ready(function () {
            hidemeet();
            hidefees();
            hide_dispose_file();
            // hide_action_file();

        });
        function hide_dispose_file() {
            $('#ContentPlaceHolder1_ddl_rti_file').change(function () {

                var bar = $('#ContentPlaceHolder1_ddl_rti_file').val();


                if (bar == "Y") {
                    $("#ContentPlaceHolder1_div_file_rti").show();
                    ValidatorEnable($("[id$='ContentPlaceHolder1_rfv_fileuploadrti']")[0], true);
                    ValidatorEnable($("[id$='ContentPlaceHolder1_rfv_txt_desc_rti']")[0], true);

                }
                else {
                    $("#ContentPlaceHolder1_div_file_rti").hide();
                    ValidatorEnable($("[id$='ContentPlaceHolder1_rfv_fileuploadrti']")[0], false);
                    ValidatorEnable($("[id$='ContentPlaceHolder1_rfv_txt_desc_rti']")[0], false);
                }

            });
        }
        function hide_action_file() {
            $('#ContentPlaceHolder1_ddl_fileup').change(function () {
                var fil = $("#ContentPlaceHolder1_ddl_fileup").val();
                if (fil == "Y") {
                    $("#ContentPlaceHolder1_div_file_upload").show();
                    ValidatorEnable($("[id$='ContentPlaceHolder1_rfv_fu']")[0], true);
                    ValidatorEnable($("[id$='ContentPlaceHolder1_rfv_file_desc']")[0], true);
                }
                else {
                    $("#ContentPlaceHolder1_div_file_upload").hide();
                    ValidatorEnable($("[id$='ContentPlaceHolder1_rfv_fu']")[0], false);
                    ValidatorEnable($("[id$='ContentPlaceHolder1_rfv_file_desc']")[0], false);

                }
            })
        }
        function hidefees() {
            $('#ContentPlaceHolder1_rbl_additional_fees_rti').change(function () {

                var value = $('#ContentPlaceHolder1_rbl_additional_fees_rti input:checked').val();

                if (value == "Y") {
                    $("#ContentPlaceHolder1_div_additional_fees").show();
                    ValidatorEnable($("[id$='ContentPlaceHolder1_rfv_txt_amount']")[0], true);
                    //   pageclient$("#ContentPlaceHolder1_rfv_meeting").enable();

                }
                if (value == "N") {
                    $("#ContentPlaceHolder1_div_additional_fees").hide();
                    //$("#ContentPlaceHolder1_rfv_meeting").();
                    ValidatorEnable($("[id$='ContentPlaceHolder1_rfv_txt_amount']")[0], false);
                }
            });
        }
        function hidemeet() {
            $('#ContentPlaceHolder1_RadioMeeting').change(function () {

                var value = $('#ContentPlaceHolder1_RadioMeeting input:checked').val();

                if (value == "Y") {
                    $("#ContentPlaceHolder1_div_meet_date").show();
                    ValidatorEnable($("[id$='ContentPlaceHolder1_rfv_meeting']")[0], true);
                    //   pageclient$("#ContentPlaceHolder1_rfv_meeting").enable();
                }
                if (value == "N") {
                    $("#ContentPlaceHolder1_div_meet_date").hide();
                    //$("#ContentPlaceHolder1_rfv_meeting").();
                    ValidatorEnable($("[id$='ContentPlaceHolder1_rfv_meeting']")[0], false);
                }
            });
        }
        //var count = "1000";
        //function limiter() {
        //    alert("Hi1");
        //    document.getElementById("ContentPlaceHolder1_lblcounter").innerHTML = count;
        //    var tex = document.getElementById("ContentPlaceHolder1_txt_description").value;
        //    var len = tex.length;
        //    if (len > count) {
        //        tex = tex.substring(0, count);
        //        document.getElementById("ContentPlaceHolder1_txt_description").value = tex;
        //        return false
        //    }
        //    document.getElementById("ContentPlaceHolder1_lblcounter").innerHTML = count - len;
        //}
        var count1 = "1000";
        function limiter1() {

            document.getElementById("ContentPlaceHolder1_Label1").innerHTML = count1;
            var tex1 = document.getElementById("ContentPlaceHolder1_txt_result").value;
            var lent = tex1.length;
            if (lent > count1) {
                tex1 = tex1.substring(0, count1);
                document.getElementById("ContentPlaceHolder1_txt_result").value = tex1;
                return false
            }
            document.getElementById("ContentPlaceHolder1_Label1").innerHTML = count1 - lent;
        }
    </script>
    <script type="text/javascript">
        $(document).keydown(function () {
            //alert("Hi1");
            //attach change event to div
            $('#ContentPlaceHolder1_HtmlEditorExtender1_ExtenderContentEditable').bind("DOMSubtreeModified", function () {
                //call function to calcualate count on change event of div
               
                textCounter();
            });
        });
        function textCounter() {
          
            //Get the text count
            var divlength = $('#ContentPlaceHolder1_HtmlEditorExtender1_ExtenderContentEditable').text().length;
            //check if count greater than 1000
            if (divlength > 1000) {
                //Trim the text
                var trimmedtext = $('#ContentPlaceHolder1_HtmlEditorExtender1_ExtenderContentEditable').text().substring(0, 1000);
                //assign it to editor
                $('#ContentPlaceHolder1_HtmlEditorExtender1_ExtenderContentEditable').html(trimmedtext);
            }
            else {
                //reduce the allowed no of characters in textbox
               // $('#ContentPlaceHolder1_lblcounter2').val(1000 - divlength);
                document.getElementById("ContentPlaceHolder1_lblcounter").innerHTML = 1000 - divlength;
            }
        }
 </script>
    <style type="text/css">
        .lol {
            resize: none;
        }

        .control-label {
            text-align: right;
        }
        .text-primary{
             padding: 1px 9px 2px;
             margin: 1px 0px;
        }
    </style>
    <div class="form-horizontal">
        <asp:HiddenField ID="h_IsRtiData" runat="server" Value="N" />
        <div class="panel panel-success">
            <div class="panel-heading ">
                <h5 class="panel-title"><%=Resources.Resource.ApplicationDetail %>:
               <a data-toggle="collapse" href="#DetailUser" aria-expanded="true"></a>
                    <asp:Label ID="lbl_UserName" runat="server" Text="Label" CssClass="text-primary control-label"></asp:Label>
                </h5>

            </div>
            <div id="DetailUser" class="panel-collapse collapse in" aria-expanded="true">
                <div class="panel-body">
                    <div class="form-group">
                        <div class="col-sm-3">
                            <label class="text-primary"><%=Resources.Resource.ApplicationNo %>:</label>
                        </div>
                        <div class="col-sm-3">
                            <asp:Label ID="txt_applicationNo" runat="server"></asp:Label>
                        </div>
                        <div class="col-sm-3">
                            <label class="text-primary"><%=Resources.Resource.RTISubmissionDate %>:</label>
                        </div>
                        <div class="col-sm-3">
                            <asp:Label ID="txt_date" runat="server"></asp:Label>
                        </div>
                    </div>
                    <div class="form-group">
                        <div class="col-sm-3">
                            <label class="text-primary"><%=Resources.Resource.ApplicantName %>:</label>
                        </div>
                        <div class="col-sm-3">
                            <asp:Label ID="txt_applicantName" runat="server"></asp:Label>
                        </div>
                        <div class="col-sm-3">
                            <label class="text-primary"><%=Resources.Resource.MobileNo %>:</label>
                        </div>
                        <div class="col-sm-3">
                            <asp:Label ID="txt_mobileNo" runat="server"></asp:Label>
                        </div>
                    </div>
                    <div class="form-group">
                        <div class="col-sm-3">
                            <label class="text-primary"><%=Resources.Resource.ApplicantAddress %>: </label>
                        </div>
                        <div class="col-sm-3">
                            <asp:Label ID="txt_applicantAddress" runat="server"></asp:Label>
                        </div>
                        <div class="col-sm-3">
                            <label runat="server" class="text-primary"><%=Resources.Resource.ApplicationStatus %>:</label>
                        </div>
                        <div class="col-sm-3">
                            <asp:Label ID="txt_application_status" runat="server"></asp:Label>
                        </div>
                    </div>
                    <div class="form-group">
                        <div class="col-sm-3">
                            <label runat="server" class="text-primary"><%=Resources.Resource.Subject %>:</label>
                        </div>
                        <div class="col-sm-3">
                            <asp:Label ID="txt_subject" runat="server"></asp:Label>
                        </div>
                        <div class="col-sm-3"></div>
                        <asp:LinkButton ID="lnk_rti_file" Visible="false" runat="server"><%=Resources.Resource.FileUpload %> </asp:LinkButton>
                    </div>
                </div>
            </div>
        </div>
        <div class="panel panel-success">
            <div class="panel-heading ">
                <h5 class="panel-title"><b><%=Resources.Resource.ActionDetail %>
                </b></h5>
            </div>
            <div class="panel-body">
                <div class="form-group">
                    <asp:GridView ID="grd_Action" runat="server" AllowPaging="true" PageSize="20" AutoGenerateColumns="false" ShowHeaderWhenEmpty="true" DataKeyNames="action_id,rti_id"
                        CssClass="table table-striped table-bordered  table-hover table-grid mydatagrid " OnRowDataBound="grd_Action_RowDataBound" OnPageIndexChanging="grd_Action_PageIndexChanging">
                        <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                        <HeaderStyle BackColor="#507CD1" HorizontalAlign="left" VerticalAlign="Middle" Font-Bold="True" ForeColor="White" />
                        <AlternatingRowStyle BackColor="White" />
                        <Columns>
                            <asp:TemplateField HeaderText="<%$Resources:Resource, SNo %>" ItemStyle-Width="70px">
                                <ItemTemplate>
                                    <%# Container.DataItemIndex+1 %>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="left" />
                                <ControlStyle Width="10px" />
                                <HeaderStyle HorizontalAlign="left" />
                            </asp:TemplateField>
                            <asp:BoundField HeaderText="<%$Resources:Resource, ActionDetail %>" DataField="detail" ItemStyle-Width="200px">
                                <ItemStyle HorizontalAlign="left" />
                                <HeaderStyle HorizontalAlign="left" />
                            </asp:BoundField>
                            <asp:BoundField DataField="date" HeaderText="<%$Resources:Resource, ActionDate %>" ItemStyle-Width="100px">
                                <ItemStyle HorizontalAlign="left" />
                                <HeaderStyle HorizontalAlign="left" />
                            </asp:BoundField>
                            <asp:BoundField DataField="status" HeaderText="<%$Resources:Resource, ActionStatus %> " ItemStyle-Width="100px">
                                <ItemStyle HorizontalAlign="left" />
                                <HeaderStyle HorizontalAlign="left" />
                            </asp:BoundField>
                            <asp:BoundField DataField="name" HeaderText="<%$Resources:Resource, OfficerName %>" ItemStyle-Width="100px">
                                <ItemStyle HorizontalAlign="left" />
                                <HeaderStyle HorizontalAlign="left" />
                            </asp:BoundField>
                            <asp:BoundField DataField="OfficeName" HeaderText="<%$Resources:Resource, OfficeName %>" ItemStyle-Width="100px">
                                <ItemStyle HorizontalAlign="left" />
                                <HeaderStyle HorizontalAlign="left" />
                            </asp:BoundField>
                            <asp:BoundField DataField="Designation_Name" HeaderText="<%$Resources:Resource, DesignationName %>" ItemStyle-Width="130px">
                                <ItemStyle HorizontalAlign="left" />
                                <HeaderStyle HorizontalAlign="left" />
                            </asp:BoundField>
                            <asp:BoundField DataField="deptName" HeaderText="<%$Resources:Resource, Department %>" ItemStyle-Width="100px">
                                <ItemStyle HorizontalAlign="left" />
                                <HeaderStyle HorizontalAlign="left" />
                            </asp:BoundField>
                            <asp:TemplateField HeaderText="<%$Resources:Resource, FileProvided %>" ItemStyle-Width="60px">
                                <ItemTemplate>
                                    <asp:LinkButton ID="lnk_file" runat="server" CommandArgument='<%#Eval("fileid") %>' Text="view" CommandName="download" OnClick="lnk_file_Click"></asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="f_desc" HeaderText="<%$Resources:Resource, FileDescription %>" ItemStyle-Width="100px">
                                <ItemStyle HorizontalAlign="left" />
                                <HeaderStyle HorizontalAlign="left" />
                            </asp:BoundField>
                        </Columns>
                        <EmptyDataTemplate><%=Resources.Resource.NoActionHasTakenYet %></EmptyDataTemplate>
                        <PagerSettings Mode="NumericFirstLast" />
                        <PagerStyle BackColor="White" ForeColor="Black" HorizontalAlign="left" CssClass="cssPager" />
                    </asp:GridView>
                </div>

                <div class="form-group">
                    <div class="col-sm-3">
                        <label class="text-primary"><%=Resources.Resource.ActionRemarks %></label>
                    </div>
                    <div class="col-sm-9">
                        <asp:TextBox ID="txt_description" CssClass="form-control lol " Height="150px" MaxLength="1000" runat="server" TextMode="MultiLine"  ValidationGroup="vg"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="rfv_description" runat="server" ControlToValidate="txt_description" Display="Dynamic"
                            ErrorMessage="Description is required" ValidationGroup="vg" CssClass="alert-danger" Font-Bold="True"></asp:RequiredFieldValidator>
                        <asp:Label ID="lblcounter" runat="server" CssClass="text-danger" Text="1000"></asp:Label>
                        <asp:Label ID="lblcounter2" CssClass="text-danger" runat="server">character left</asp:Label>
                        <asp:HtmlEditorExtender ID="HtmlEditorExtender1" runat="server" TargetControlID="txt_description" EnableSanitization="false" >
                        </asp:HtmlEditorExtender>
                    </div>
                </div>
                
                <%--<asp:UpdatePanel ID="udp" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>--%>
              
                        <div class="form-group">
                            <div class="col-sm-3">
                                <label class="text-primary"><%=Resources.Resource.RTIAction %> :</label>
                            </div>
                            <div class="col-sm-3">
                                <asp:DropDownList ID="ddl_status" runat="server" AutoPostBack="true" ValidationGroup="vg" CssClass="form-control" AppendDataBoundItems="true" OnSelectedIndexChanged="ddl_status_SelectedIndexChanged">
                                    <asp:ListItem Value="0">Select</asp:ListItem>
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="rfv_status" runat="server" ValidationGroup="vg" ControlToValidate="ddl_status" InitialValue="0"
                                    CssClass="alert-danger" ErrorMessage="Status is Required" Display="Dynamic" SetFocusOnError="true"></asp:RequiredFieldValidator>
                            </div>

                            <div class="col-sm-3">
                                <label class="text-primary"><%=Resources.Resource.ActionDate %></label>
                            </div>
                            <div class="col-sm-2">
                                <asp:TextBox ID="Txt_Action" runat="server" ValidationGroup="vg" CssClass="form-control" InitialValue="0"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="rfv_Action" runat="server" ErrorMessage="choose a Right Action date " Display="Dynamic" ControlToValidate="Txt_Action" Font-Bold="True" CssClass="alert-danger" ValidationGroup="vg">Action date required</asp:RequiredFieldValidator>
                                <asp:CompareValidator ID="comp_date" runat="server" ControlToValidate="Txt_Action" CssClass="alert-danger" ErrorMessage="Date Should Not Greater then Today" Display="Dynamic" Operator="LessThanEqual" SetFocusOnError="True" Type="Date" ValidationGroup="vg"></asp:CompareValidator>
                                <asp:CalendarExtender ID="Txt_Action_CalendarExtender" runat="server" Format="dd-MM-yyyy" ClearTime="True" Enabled="True" TargetControlID="Txt_Action">
                                </asp:CalendarExtender>
                            </div>
                            <div class="col-sm-1">
                                <asp:ImageButton ID="imgbtn" runat="server" ImageAlign="Middle" Height="30" Width="30" ImageUrl="~/images/calendar-icon.png" />
                                <asp:CalendarExtender ID="calex_from" runat="server" Enabled="True" Format="dd-MM-yyyy" PopupButtonID="imgbtn" FirstDayOfWeek="Monday" PopupPosition="BottomRight" TargetControlID="Txt_Action" TodaysDateFormat="dd/MM/yyyy"></asp:CalendarExtender>
                            </div>
                        </div>

                        <div class="form-group">

                            <div class="col-sm-3">
                                <label class="text-primary"><%=Resources.Resource.DoYouWantToUploadFile %> :</label>
                            </div>
                            <div class="col-sm-3">
                                <asp:DropDownList ID="ddl_fileup" runat="server" CssClass="form-control" ValidationGroup="vg" OnSelectedIndexChanged="ddl_fileup_SelectedIndexChanged" AutoPostBack="true">
                                    <asp:ListItem Value="0" Text="Select"></asp:ListItem>
                                    <asp:ListItem Value="Y" Text="Yes"></asp:ListItem>
                                    <asp:ListItem Value="N" Text="No"></asp:ListItem>
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="rfv_fil_up" runat="server" ControlToValidate="ddl_fileup" Display="Dynamic"
                                    ErrorMessage=" File Upload required" ValidationGroup="vg" CssClass="alert-danger" InitialValue="0" Font-Bold="True"></asp:RequiredFieldValidator>
                            </div>
                             <%--</ContentTemplate>
                </asp:UpdatePanel>--%>
                            <div id="forward_auth" runat="server" visible="false">
                                <div class="col-sm-3">
                                    <label class="text-primary"><%=Resources.Resource.SendTo %> :</label>
                                </div>
                                <div class="col-sm-3">
                                    <asp:DropDownList ID="ddl_forward_auth" runat="server" CssClass="form-control" ValidationGroup="vg">
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="rfv_ddl_forward_auth" runat="server" ControlToValidate="ddl_forward_auth" InitialValue="0" CssClass="alert-danger" ErrorMessage="Please Select Send To Action"></asp:RequiredFieldValidator>
                                </div>
                            </div>
                        </div>
                        <div class="form-group" id="div_file_upload" runat="server" visible="false">
                            <div class="col-sm-3">
                                <label class="text-primary"><%=Resources.Resource.InsertFile %>:</label>
                                <label class="text-danger" style="font-size: small"><%=Resources.Resource.OnlyPdfIsAcceptable %> </label>
                            </div>
                            <div class="col-sm-3">
                                <asp:FileUpload ID="fu_action" runat="server" EnableViewState="True" ViewStateMode="Enabled" />
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
                <asp:UpdatePanel ID="udp" runat="server" >
                    <ContentTemplate>
                        <div id="div_forward" runat="server" visible="false">
                            <div class="form-group" id="div_radio_forward" runat="server">
                                <div class="col-sm-3">
                                    <label class="text-primary"><%=Resources.Resource.ForwardTo %>:</label>
                                </div>
                            </div>
                            <%--    <div class="col-sm-8">
                                    <asp:RadioButtonList ID="RadioArea" CssClass="RadioButtonWidth" RepeatDirection="Horizontal" AutoPostBack="true" OnSelectedIndexChanged="RadioArea_SelectedIndexChanged" runat="server" RepeatLayout="Flow" AppendDataBoundItems="true">
                                        <asp:ListItem Text="Within office" Value="WO"></asp:ListItem>
                                        <asp:ListItem Text="Outside office" Value="OSO"></asp:ListItem>
                                    </asp:RadioButtonList>
                                    <asp:RequiredFieldValidator ID="rfv_RadioArea" runat="server" ValidationGroup="vg" ControlToValidate="RadioArea" CssClass="alert-danger" ErrorMessage="Field Required"></asp:RequiredFieldValidator>
                                </div>
                            </div>--%>
                            <%--<div runat="server" id="inside_ofc">
                                <div class="form-group">
                                    <div class="col-sm-3">
                                        <label class="text-primary">Select Officer :</label>
                                    </div>
                                    <div class="col-sm-3">
                                        <asp:DropDownList runat="server" ID="ddl_with_ofc" CssClass="form-control" AppendDataBoundItems="true">
                                            <asp:ListItem Value="0" Text="---select-------"></asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfv_with_ofc" runat="server" ValidationGroup="vg" ControlToValidate="ddl_with_ofc" InitialValue="0"
                                            CssClass="alert-danger" ErrorMessage="Officer is Required" Display="Dynamic" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                    </div>
                                </div>
                            </div>--%>
                    
                    
                            <div id="out_office" runat="server" visible="true">
                                <div class="form-group">
                                    <div class="col-sm-3">
                                        <label class="text-primary"><%=Resources.Resource.Department %>:</label>
                                    </div>
                                    <div class="col-sm-3">
                                        <asp:DropDownList ID="ddl_department" runat="server" AutoPostBack="true" ValidationGroup="vg" CssClass="form-control" OnSelectedIndexChanged="ddl_department_SelectedIndexChanged"></asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfv_dept" runat="server" ValidationGroup="vg" ControlToValidate="ddl_department" InitialValue="0"
                                            CssClass="alert-danger" ErrorMessage="Department  is Required" Display="Dynamic" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                    </div>
                                    <div class="col-sm-3">
                                        <label class="text-primary"><%=Resources.Resource.District %>:</label>
                                    </div>
                                    <div class="col-sm-3">
                                        <asp:DropDownList ID="ddl_district" runat="server" AutoPostBack="true" ValidationGroup="vg" CssClass="form-control"
                                            OnSelectedIndexChanged="ddl_district_SelectedIndexChanged">
                                            <asp:ListItem Value="0" Text=" Select "></asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfv_district" runat="server" ValidationGroup="vg" ControlToValidate="ddl_district" InitialValue="0"
                                            CssClass="alert-danger" ErrorMessage="District  is Required" Display="Dynamic" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <div class="col-sm-3">
                                        <label class="text-primary"><%=Resources.Resource.OfficeLevel %>:</label>
                                    </div>
                                    <div class="col-sm-3">
                                        <asp:DropDownList ID="ddl_officeLevel" runat="server" CssClass="form-control" OnSelectedIndexChanged="ddl_officeLevel_SelectedIndexChanged" AutoPostBack="true" AppendDataBoundItems="true">
                                            <asp:ListItem Value="0" Text=" Select "></asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfv_officeLevel" runat="server" ControlToValidate="ddl_officeLevel"
                                            ErrorMessage=" Office level is Required" InitialValue="0" ValidationGroup="vg" CssClass="text-danger" SetFocusOnError="true"> </asp:RequiredFieldValidator>
                                    </div>
                                    <div class="col-sm-3">
                                        <label class="text-primary"><%=Resources.Resource.OfficeCategory %>:</label>
                                    </div>
                                    <div class="col-sm-3">
                                        <asp:DropDownList ID="ddl_officeCategory" runat="server" AutoPostBack="true" ValidationGroup="vg" CssClass="form-control"
                                            OnSelectedIndexChanged="ddl_officeCategory_SelectedIndexChanged">
                                            <asp:ListItem Value="0" Text=" Select "></asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfv_ofc_cat" runat="server" ValidationGroup="vg" ControlToValidate="ddl_officeCategory" InitialValue="0"
                                            CssClass="alert-danger" ErrorMessage="Category  is Required" Display="Dynamic" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <div class="col-sm-3">
                                        <label class="text-primary"><%=Resources.Resource.Office %>:</label>
                                    </div>
                                    <div class="col-sm-3">
                                        <asp:DropDownList ID="ddl_office" runat="server" AutoPostBack="true"
                                            OnSelectedIndexChanged="ddl_office_SelectedIndexChanged" ValidationGroup="vg" CssClass="form-control">
                                            <asp:ListItem Value="0" Text=" Select "></asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfv_office" runat="server" ValidationGroup="vg" ControlToValidate="ddl_office" InitialValue="0"
                                            CssClass="alert-danger" ErrorMessage="Office is Required" Display="Dynamic" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                    </div>
                                    <div class="col-sm-3">
                                        <label class="text-primary"><%=Resources.Resource.DesignationName %>:</label>
                                    </div>
                                    <div class="col-sm-3">
                                        <asp:DropDownList ID="ddl_designation" runat="server" AutoPostBack="true" ValidationGroup="vg" CssClass="form-control" OnSelectedIndexChanged="ddl_designation_SelectedIndexChanged">
                                            <asp:ListItem Value="0" Text=" Select "></asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfv_ddl_designation" runat="server" ValidationGroup="vg" ControlToValidate="ddl_designation" InitialValue="0"
                                            CssClass="alert-danger" ErrorMessage="Designation is Required" Display="Dynamic" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <div class="col-sm-3">
                                        <label class="text-primary"><%=Resources.Resource.RTIOfficer %>:</label>
                                    </div>
                                    <div class="col-sm-3">
                                        <asp:DropDownList ID="ddl_officer" CssClass="form-control" runat="server">
                                            <asp:ListItem Value="0" Text=" Select "></asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfv_ddl_officer" runat="server" ValidationGroup="vg" ControlToValidate="ddl_officer" InitialValue="0"
                                            CssClass="alert-danger" ErrorMessage="Officer is Required" Display="Dynamic" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div id="div_reject" runat="server" visible="false">
                            <div class="form-group">
                                <div class="col-sm-3">
                                    <label class="text-primary"><%=Resources.Resource.Reason %></label>
                                </div>
                                <div class="col-sm-3">
                                    <asp:DropDownList ID="ddl_reject" runat="server" CssClass=" form-control">
                                        <%--<asp:ListItem Text="------Select----- " Value="0"></asp:ListItem>--%>
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="rfv_ddl_reject" runat="server" ValidationGroup="vg" ControlToValidate="ddl_reject" InitialValue="0"
                                        CssClass="alert-danger" ErrorMessage="Reason is Required" Display="Dynamic" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                </div>
                                <%--<div class="col-sm-3">
                                    <label class="text-primary">Remark1: </label>
                                </div>
                                <div class="col-sm-3">
                                    <asp:TextBox ID="txt_reject" runat="server" TextMode="MultiLine" CssClass=" form-control lol">
                                    </asp:TextBox>
                                    <asp:RequiredFieldValidator ID="rfv_txt_reject" runat="server" ControlToValidate="txt_reject" ValidationGroup="vg"
                                        Display="Dynamic" ErrorMessage="Required field" SetFocusOnError="true" CssClass="alert-danger"></asp:RequiredFieldValidator>
                                </div>--%>
                            </div>
                        </div>
                        
                        <%--                        <div id="div_review" runat="server" visible="false">
                            <div class="form-group">
                                <div class="col-sm-3">
                                    <label class="text-primary">Review For:</label>
                                </div>
                                <div class="col-sm-3">
                                    <asp:DropDownList ID="ddl_review" runat="server" CssClass=" form-control">
                                        <asp:ListItem Text="------Select----- " Value="0"></asp:ListItem>
                                        <asp:ListItem Text="Not relevant 1" Value="1" />
                                        <asp:ListItem Text="Review reason 2" Value="2" />
                                        <asp:ListItem Text="Review reason 3" Value="3" />
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="rfv_ddl_review" runat="server" ValidationGroup="vg" ControlToValidate="ddl_review" InitialValue="0"
                                        CssClass="alert-danger" ErrorMessage="Officer is Required" Display="Dynamic" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                </div>
                                <div class="col-sm-3">
                                    <label class="text-primary">Remark2: </label>
                                </div>
                                <div class="col-sm-3">
                                    <asp:TextBox ID="txt_review" runat="server" TextMode="MultiLine" CssClass=" form-control lol">
                                    </asp:TextBox>
                                    <asp:RequiredFieldValidator ID="rfv_txt_review" runat="server" ControlToValidate="txt_review" CssClass="alert-danger" Display="Dynamic"
                                        ErrorMessage="Required field" ValidationGroup="vg" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                </div>
                            </div>
                        </div>--%>
                   
                        <div id="div_dispose" runat="server" visible="false">
                            <div class="panel panel-info">
                                <div class="panel-heading">
                                    <div class="panel-title">
                                       <%=Resources.Resource.DataToBeProvidedForApplicant %> 
                                    </div>
                                </div>
                                <div class="panel-body">
                                    <div class="form-group">
                                        <div class="col-sm-3">
                                            <label class="text-primary"><%=Resources.Resource.RTIInformation %> </label>
                                        </div>
                                        <div class="col-sm-9">
                                            <asp:TextBox ID="txt_result" CssClass="form-control  " Height="150px" MaxLength="1000" runat="server" TextMode="MultiLine" OnkeyUp="limiter1()" ValidationGroup="vg"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="rfv_txt_result" runat="server" ControlToValidate="txt_result" Display="Dynamic"
                                                ErrorMessage="Description is required" ValidationGroup="vg" CssClass="alert-danger"></asp:RequiredFieldValidator>
                                            <asp:Label ID="Label1" runat="server" CssClass="text-danger" Text="1000"></asp:Label>
                                            <asp:Label ID="Label2" CssClass="text-danger" runat="server">character left</asp:Label>
                                            <%-- <asp:HtmlEditorExtender ID="HtmlEditorExtender2" runat="server" TargetControlID="txt_result" EnableSanitization="false">
                                            </asp:HtmlEditorExtender>--%>
                                        </div>
                                    </div>
                                   
                                    <div id="div_meeting" runat="server" visible="false">
                                        <div class="form-group">
                                            <div id="div_meet" runat="server">
                                                <div class="col-sm-4">
                                                    <label class="text-primary"><%=Resources.Resource.DoYouWantMeetingWithApplicant %></label>
                                                </div>
                                                <div class="col-sm-2">
                                                    <asp:RadioButtonList ID="RadioMeeting" CssClass="RadioButtonWidth" AutoPostBack="true" OnSelectedIndexChanged="RadioMeeting_SelectedIndexChanged" RepeatDirection="Horizontal" runat="server" RepeatLayout="Flow" AppendDataBoundItems="true">
                                                        <asp:ListItem Text="YES" Value="Y"></asp:ListItem>
                                                        <asp:ListItem Text="NO" Value="N"></asp:ListItem>
                                                    </asp:RadioButtonList>
                                                    <asp:RequiredFieldValidator ID="rfv_radiomeeting" runat="server" ValidationGroup="vg" ControlToValidate="RadioMeeting" ErrorMessage="Field Is Required" CssClass="alert-danger" Display="Dynamic"></asp:RequiredFieldValidator>
                                                </div>
                                            </div>
                                            <%--<div id="div_meet_date" runat="server" style="display: none">--%>
                                            <div id="div_meet_date" runat="server" visible="false">
                                                <div class="col-sm-3">
                                                    <label class="text-primary"><%=Resources.Resource.MeetingDate %></label>
                                                </div>
                                                <div class="col-sm-2">
                                                    <asp:TextBox ID="txt_meeting" runat="server" ValidationGroup="vg" CssClass="form-control"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="rfv_meeting" runat="server" ErrorMessage="choose a Right Meeting date " Display="Dynamic" ControlToValidate="txt_meeting" Font-Bold="True" CssClass="alert-danger" ValidationGroup="vg">Meeting date required</asp:RequiredFieldValidator>
                                                    <asp:CompareValidator ID="comval1" runat="server" ControlToValidate="txt_meeting" CssClass="alert-danger" ErrorMessage="Date Should Not Greater then Today" Display="Dynamic" Operator="GreaterThanEqual" SetFocusOnError="True" Type="Date" ValidationGroup="vg"></asp:CompareValidator>
                                                </div>
                                                <div class="col-sm-1">
                                                    <asp:ImageButton ID="ImgBtn1" runat="server" ImageAlign="Middle" Height="30" Width="30" ImageUrl="~/images/calendar-icon.png" />
                                                    <asp:CalendarExtender ID="CalendarExtender2" runat="server" Enabled="True" Format="dd-MM-yyyy" PopupButtonID="ImgBtn1" FirstDayOfWeek="Monday" PopupPosition="BottomRight" TargetControlID="txt_meeting" TodaysDateFormat="dd/MM/yyyy"></asp:CalendarExtender>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <div class="col-sm-4">
                                            <label class="text-primary"><%=Resources.Resource.NeedAdditionalFees %></label>
                                        </div>
                                        <div class="col-sm-2">
                                            <asp:RadioButtonList ID="rbl_additional_fees_rti" CssClass="RadioButtonWidth" AutoPostBack="true" OnSelectedIndexChanged="rbl_additional_fees_SelectedIndexChanged1" RepeatDirection="Horizontal" runat="server" RepeatLayout="Flow">
                                                <asp:ListItem Text="YES" Value="Y"></asp:ListItem>
                                                <asp:ListItem Text="NO" Value="N"></asp:ListItem>
                                            </asp:RadioButtonList>
                                            <asp:RequiredFieldValidator ID="rfv_rbl_additionalfee" runat="server" ValidationGroup="vg" ControlToValidate="rbl_additional_fees_rti" ErrorMessage="Field Is Required" CssClass="alert-danger" Display="Dynamic"></asp:RequiredFieldValidator>
                                        </div>
                                        <%--<div id="div_additional_fees" runat="server" style="display: none">--%>
                                        <div id="div_additional_fees" runat="server" visible="false">
                                            <div class="col-sm-3">
                                                <label class="text-primary"><%=Resources.Resource.FeesAmount %></label>
                                            </div>
                                            <div class="col-sm-3">
                                                <asp:TextBox ID="txt_amount" runat="server" ValidationGroup="vg" CssClass="form-control" MaxLength="10" placeholder="Only Amount"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="rfv_txt_amount" runat="server" ValidationGroup="vg" ControlToValidate="txt_amount" ErrorMessage="Amount is required" CssClass="alert-danger" Display="Dynamic"></asp:RequiredFieldValidator>
                                                <%--<asp:RegularExpressionValidator ID="rev_txt_amount" runat="server" ValidationExpression="((\d+)+(\.\d+))$" ErrorMessage="Please enter valid decimal number" ControlToValidate="txt_amount" CssClass="alert-danger" Display="Dynamic" />--%>
                                                <asp:RegularExpressionValidator ID="rev_txt_amount" runat="server" ValidationExpression="((\d+)((\.\d{1,2})?))$" ErrorMessage="Please enter valid decimal number" ControlToValidate="txt_amount" ValidationGroup="vg" CssClass="alert-danger" Display="Dynamic" />
                                            </div>
                                        </div>
                                    </div>
                                    
                                     <div class="form-group">
                                        <div class="col-sm-3">
                                            <label class="text-primary"><%=Resources.Resource.IsThereAnyFileToProvideApplicant %> :</label>
                                        </div>
                                        <div class="col-sm-3">
                                            <asp:DropDownList ID="ddl_rti_file" runat="server" AutoPostBack="true" CssClass="form-control" ValidationGroup="vg" OnSelectedIndexChanged="ddl_rti_file_SelectedIndexChanged">
                                                <asp:ListItem Value="0" Text="Select"></asp:ListItem>
                                                <asp:ListItem Value="Y" Text="Yes"></asp:ListItem>
                                                <asp:ListItem Value="N" Text="No"></asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="rfv_rti_file" runat="server" ControlToValidate="ddl_rti_file" Display="Dynamic"
                                                ErrorMessage=" File Upload required" ValidationGroup="vg" CssClass="alert-danger" InitialValue="0" Font-Bold="True"></asp:RequiredFieldValidator>
                                        </div>
                                        <div class="col-sm-3"></div>
                                        <%--<div class="col-sm-3" id="div_rti_file" runat ="server" visible="false">--%>
                                        <asp:LinkButton ID="lnk_file_rti" runat="server" Visible="false" Text="View RTI File" OnClick="lnk_file_rti_Click"></asp:LinkButton>
                                        <%--</div>--%>
                                    </div>
                                    
                                    <%--<div class="form-group" id="div_file_rti" runat="server" style="display: none">--%>
                                    <div class="form-group" id="div_file_rti" runat="server" visible="false">
                                        <div class="col-sm-3">
                                            <label class="text-primary"><%=Resources.Resource.InsertFile %>:</label>
                                            <label class="text-danger" style="font-size: small"><%=Resources.Resource.OnlyPdf_WordIsAcceptable %> </label>
                                        </div>
                                        <div class="col-sm-3">
                                            <asp:FileUpload ID="FileUploadRTI" runat="server" ViewStateMode="Enabled" />
                                            <asp:RequiredFieldValidator ID="rfv_fileuploadrti" runat="server" ValidationGroup="vg" Display="Dynamic" ControlToValidate="FileUploadRTI" ErrorMessage="File is Required" CssClass="alert-danger"></asp:RequiredFieldValidator>
                                        </div>
                                        <div class="col-sm-3">
                                            <label class="text-primary"><%=Resources.Resource.FileDescription %> :</label>
                                        </div>
                                        <div class="col-sm-3">
                                            <asp:TextBox ID="txt_desc_rti" runat="server" ValidationGroup="vg" CssClass="form-control"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="rfv_txt_desc_rti" runat="server" ValidationGroup="vg" ControlToValidate="txt_desc_rti" ErrorMessage="File Discription is required" CssClass="alert-danger" Display="Dynamic"></asp:RequiredFieldValidator>
                                        </div>
                                    </div>

                                </div>
                            </div>
                        </div>
                  </ContentTemplate>
                </asp:UpdatePanel>
                <div class="form-group center">
                    <asp:Button ID="btnSubmit" runat="server" Text="<%$Resources:Resource, Submit %>" ValidationGroup="vg" CssClass="btn button-blue" ForeColor="White" OnClick="btnSubmit_Click" />
                </div>
                <asp:Label ID="lbl_status" runat="server" Text="" Visible="false"></asp:Label>

            </div>
        </div>
    </div>
</asp:Content>
