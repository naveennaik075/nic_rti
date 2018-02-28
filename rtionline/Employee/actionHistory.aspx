<%@ Page Title="" Language="C#" MasterPageFile="~/Master_employee.master" AutoEventWireup="true" CodeFile="actionHistory.aspx.cs" Inherits="actionHistory" EnableEventValidation="false" MaintainScrollPositionOnPostback="true"%>

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

                <div class="form-group center">
                    <asp:Button ID="btnClose" runat="server" Text="<%$Resources:Resource, Close %>" ValidationGroup="vg" CssClass="btn button-blue" ForeColor="White" OnClick="btnClose_Click" />
                </div>
                

            </div>
        </div>
    </div>
</asp:Content>
