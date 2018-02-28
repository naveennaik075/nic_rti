<%@ Page Title="" Language="C#" MasterPageFile="~/admin_master.master" AutoEventWireup="true" CodeFile="NoticeEdit_Form.aspx.cs" Inherits="usr_NoticeEdit_Form" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="CPH_MasterLogin" runat="Server">

    <link rel="stylesheet" type="text/css" href="../msdropdown/dd.css" />
    <script type="text/javascript" src="../msdropdown/js/jquery-1.6.1.min.js"></script>
    <%--<script type="text/javascript" src="../msdropdown/js/jquery-1.9.1.min.js"></script>--%>
    <script type="text/javascript" src="../msdropdown/js/jquery.dd.js"></script>

    <style type="text/css">
        .Grid tr td {
            padding-right: 5px;
        }

        .form_error {
            color: red;
        }

        .table tbody tr:first-child td {
            background-color: #F2F5A9;
        }

        .text-danger {
            color: red;
            font-size: large;
        }

        textarea {
            resize: none;
        }

        cal {
            width: 40px;
        }

        .RadioButtonWidth label {
            margin-right: 20px;
            margin-left: 10px;
        }

        .lol {
            padding-right: 20px;
        }
    </style>
    <style type="text/css">
        #CPH_MasterLogin_ddlimage_child img {
            width: 30px;
            height: 30px;
            border: 0;
            overflow: hidden;
            outline: none;
        }

        #CPH_MasterLogin_blinkimage img {
            width: auto;
            height: 30px;
            border: 0;
            overflow: hidden;
            outline: none;
        }

        .image1size {
            width: 30px !important;
        }
    </style>

    <script type="text/javascript">
        $(document).ready(function (e) {
            //------------------------------new----------------------------------------
            $(".dd").addClass("form-control");
            try {
                $("#CPH_MasterLogin_ddlimage").msDropDown();
            } catch (e) {
                alert(e.message);
            }

            $("#CPH_MasterLogin_ddlimage").change(function () {
                $("#CPH_MasterLogin_Image1").addClass("image1size");
            });


            //--------------------------------------------------------------------------

            //try {
            //    $("#ContentPlaceHolder1_ddlimage").msDropDown();
            //    $("#ContentPlaceHolder1_ddlimage").change(function () {
            //        var io = $("#ContentPlaceHolder1_ddlimage option:selected").val();

            //        if (io != "")
            //            document.getElementById("ContentPlaceHolder1_Image1").innerHTML = "<div><img src=" + io + "></div>";


            //    }).change();

            //    $('#ContentPlaceHolder1_ddlimage').trigger('change');



            //    $("#ContentPlaceHolder1_txtHeader").keyup(function () {
            //        $('#ContentPlaceHolder1_ddlimage').trigger('change');

            //    });


            //} catch (e) {
            //    alert(e.message);
            //}


         <%--  var ra = document.getElementById('<%=RequiredFieldValidator10.ClientID %>');
            ra.ValidationGroup = "a";
            ValidatorEnable(ra, true); 

            var ra1 = document.getElementById('<%=RequiredFieldValidator9.ClientID %>');
            ra1.ValidationGroup = "a";
            ValidatorEnable(ra1, true); --%>

            var ra2 = document.getElementById('<%=RegularExpressionValidator10.ClientID %>');
            ra2.ValidationGroup = "a";
            ValidatorEnable(ra2, true);

            var ra3 = document.getElementById('<%=RegularExpressionValidator17.ClientID %>');
            ra3.ValidationGroup = "a";
            ValidatorEnable(ra3, true);

            var ra4 = document.getElementById('<%=RequiredFieldValidator7.ClientID %>');
            ra4.ValidationGroup = "x";
            ValidatorEnable(ra4, false);

            var ra5 = document.getElementById('<%=RegularExpressionValidator1.ClientID %>');
            ra5.ValidationGroup = "x";
            ValidatorEnable(ra5, false);
        });
        function Color_Changed(sender) {
            sender.get_element().value = "#" + sender.get_selectedColor();
            var color = document.getElementById("CPH_MasterLogin_color").value;
            document.getElementById("CPH_MasterLogin_lblpreview").style.color = sender.get_element().value;
            document.getElementById("<%=HiddenField3.ClientID%>").value = color;
        }

        var prmInstance = Sys.WebForms.PageRequestManager.getInstance();
        prmInstance.add_endRequest(function () {

            //-------------------------------------------------------image---------------------

            //try {
            //    $("#ContentPlaceHolder1_ddlimage").msDropDown();
            //    // 

            //    preview();

            //    $("#ContentPlaceHolder1_ddlimage").change(function () {
            //        var io = $("#ContentPlaceHolder1_ddlimage option:selected").val();
            //        if (io != "")
            //            document.getElementById("ContentPlaceHolder1_Image1").innerHTML = "<div><img src=" + io + "></div>";

            //    }).change();
            //    $('#ContentPlaceHolder1_ddlimage').trigger('change');
            //} catch (e) {
            //    alert(e.message);
            //}
        });
    </script>
    <%--   //---------------------------------------------------------new--------------------%>
    <script type="text/javascript">
        $(function () {
            $('#CPH_MasterLogin_ddlimage').change(function () {
                var selectedText = $(this).find("option:selected").text();
                var selectedValue = $(this).val();
                //alert(selectedText);
                if (selectedText == "Select")
                    $('#CPH_MasterLogin_Image1').hide();
                else {
                    $('#CPH_MasterLogin_Image1').show();
                    $("#CPH_MasterLogin_Image1").attr("src", selectedText);
                    document.getElementById("<%=HiddenField1.ClientID%>").value = selectedText;

                }
            })
        })
    </script>


    <script type="text/javascript">

        function AppendTime(sender, args) {
            var selectedDate = new Date();
            selectedDate = sender.get_selectedDate();
            var now = new Date();
            sender.get_element().value = selectedDate.format("dd/MM/yyyy") + " " + now.format("HH:mm:ss");
        }

        //---------------------------------------------------------function to count letters in a text box----------------------------------------------------
        var count = "100";   //Example: var count = "175";

        $(document).ready(function () {
            document.getElementById("CPH_MasterLogin_limit1").innerHTML = count;
        });
        function limiter() {
            document.getElementById("CPH_MasterLogin_limit1").innerHTML = count;
            var tex = document.getElementById("CPH_MasterLogin_txtHeader").value;
            var len = tex.length;
            if (len > count) {
                tex = tex.substring(0, count);
                document.getElementById("CPH_MasterLogin_txtHeader").value = tex;
                return false;
            }
            document.getElementById("CPH_MasterLogin_limit1").innerHTML = count - len;
        }
        //show hide datefrom/dateto
        function Get() {
            var rb = document.getElementById("<%=ddlPermanent.ClientID%>");
            if (rb.value == "Y") {
                $('#divPublishDate').show();
                $('#divFrom').hide();
                $('#divTo').hide();
                var ra = document.getElementById('<%=RequiredFieldValidator10.ClientID %>');
                ra.ValidationGroup = "x";
                ValidatorEnable(ra, false);

                var ra1 = document.getElementById('<%=RequiredFieldValidator9.ClientID %>');
                ra1.ValidationGroup = "x";
                ValidatorEnable(ra1, false);

                var ra2 = document.getElementById('<%=RegularExpressionValidator10.ClientID %>');
                ra2.ValidationGroup = "x";
                ValidatorEnable(ra2, false);

                var ra3 = document.getElementById('<%=RegularExpressionValidator17.ClientID %>');
                ra3.ValidationGroup = "x";
                ValidatorEnable(ra3, false);

                var ra4 = document.getElementById('<%=RequiredFieldValidator7.ClientID %>');
                ra4.ValidationGroup = "a";
                ValidatorEnable(ra4, true);

                var ra5 = document.getElementById('<%=RegularExpressionValidator1.ClientID %>');
                ra5.ValidationGroup = "a";
                ValidatorEnable(ra5, true);

            }
            else {
                $('#divPublishDate').hide();
                $('#divFrom').show();
                $('#divTo').show();
                var ra = document.getElementById('<%=RequiredFieldValidator10.ClientID %>');
                ra.ValidationGroup = "a";
                ValidatorEnable(ra, true);

                var ra1 = document.getElementById('<%=RequiredFieldValidator9.ClientID %>');
                ra1.ValidationGroup = "a";
                ValidatorEnable(ra1, true);

                var ra2 = document.getElementById('<%=RegularExpressionValidator10.ClientID %>');
                ra2.ValidationGroup = "a";
                ValidatorEnable(ra2, true);

                var ra3 = document.getElementById('<%=RegularExpressionValidator17.ClientID %>');
                ra3.ValidationGroup = "a";
                ValidatorEnable(ra3, true);

                var ra4 = document.getElementById('<%=RequiredFieldValidator7.ClientID %>');
                ra4.ValidationGroup = "x";
                ValidatorEnable(ra4, false);

                var ra5 = document.getElementById('<%=RegularExpressionValidator1.ClientID %>');
                ra5.ValidationGroup = "x";
                ValidatorEnable(ra5, false);

            }
        };
        //show hide external/internal
        function Gettype() {
            var rb = document.getElementById("<%=ddlHyperlink.ClientID%>");
            if (rb.value == "Y") {
                $('#divType').show();


                var ra = document.getElementById('<%=RequiredFieldValidator3.ClientID %>');
                ra.ValidationGroup = "a";
                ValidatorEnable(ra, true);

            }
            else {
                $('#divType').hide();
                $('#divFileUpload').hide();
                $('#divUrl').hide();
                var ra = document.getElementById('<%=RequiredFieldValidator3.ClientID %>');
                ra.ValidationGroup = "x";
                ValidatorEnable(ra, false);
                var ra1 = document.getElementById('<%=RequiredFieldValidator6.ClientID %>');
                ra1.ValidationGroup = "x";
                ValidatorEnable(ra1, false);

                var ra2 = document.getElementById('<%=RequiredFieldValidator4.ClientID %>');
                ra2.ValidationGroup = "x";
                ValidatorEnable(ra2, false);

                var ra3 = document.getElementById('<%=regUrl.ClientID %>');
                ra3.ValidationGroup = "x";
                ValidatorEnable(ra3, false);
            }
        };
        //show hide external/internal
        function Get2() {
            var rb = document.getElementById("<%=ddlType.ClientID%>");
            if (rb.value == "I") {
                $('#divFileUpload').show();
                $('#divUrl').hide();
                //alert("l");
                var ra = document.getElementById('<%=RequiredFieldValidator6.ClientID %>');
                ra.ValidationGroup = "a";
                ValidatorEnable(ra, true);

                var ra1 = document.getElementById('<%=RequiredFieldValidator4.ClientID %>');
                ra1.ValidationGroup = "x";
                ValidatorEnable(ra1, false);

                var ra2 = document.getElementById('<%=regUrl.ClientID %>');
                ra2.ValidationGroup = "x";
                ValidatorEnable(ra2, false);
            }
            else if (rb.value == "E") {
                $('#divFileUpload').hide();
                $('#divUrl').show();

                var ra = document.getElementById('<%=RequiredFieldValidator6.ClientID %>');
                ra.ValidationGroup = "x";
                ValidatorEnable(ra, false);

                var ra1 = document.getElementById('<%=RequiredFieldValidator4.ClientID %>');
                ra1.ValidationGroup = "a";
                ValidatorEnable(ra1, true);

                var ra2 = document.getElementById('<%=regUrl.ClientID %>');
                ra2.ValidationGroup = "a";
                ValidatorEnable(ra2, true);
            }
    };
    //--------on click preview------------ 
    function preview() {
        var rb = document.getElementById("<%=ddlPermanent.ClientID%>");
        if (rb.value == "Y") {
            $('#divPublishDate').show();
            $('#divFrom').hide();
            $('#divTo').hide();
            var ra = document.getElementById('<%=RequiredFieldValidator10.ClientID %>');
            ra.ValidationGroup = "x";
            ValidatorEnable(ra, false);

            var ra1 = document.getElementById('<%=RequiredFieldValidator9.ClientID %>');
           ra1.ValidationGroup = "x";
           ValidatorEnable(ra1, false);

           var ra2 = document.getElementById('<%=RegularExpressionValidator10.ClientID %>');
            ra2.ValidationGroup = "x";
            ValidatorEnable(ra2, false);

            var ra3 = document.getElementById('<%=RegularExpressionValidator17.ClientID %>');
            ra3.ValidationGroup = "x";
            ValidatorEnable(ra3, false);

            var ra4 = document.getElementById('<%=RequiredFieldValidator7.ClientID %>');
            ra4.ValidationGroup = "a";
            ValidatorEnable(ra4, true);

            var ra5 = document.getElementById('<%=RegularExpressionValidator1.ClientID %>');
            ra5.ValidationGroup = "a";
            ValidatorEnable(ra5, true);

        }
        else {
            $('#divPublishDate').hide();
            $('#divFrom').show();
            $('#divTo').show();
            var ra = document.getElementById('<%=RequiredFieldValidator10.ClientID %>');
           ra.ValidationGroup = "a";
           ValidatorEnable(ra, true);

           var ra1 = document.getElementById('<%=RequiredFieldValidator9.ClientID %>');
            ra1.ValidationGroup = "a";
            ValidatorEnable(ra1, true);

            var ra2 = document.getElementById('<%=RegularExpressionValidator10.ClientID %>');
            ra2.ValidationGroup = "a";
            ValidatorEnable(ra2, true);

            var ra3 = document.getElementById('<%=RegularExpressionValidator17.ClientID %>');
            ra3.ValidationGroup = "a";
            ValidatorEnable(ra3, true);

            var ra4 = document.getElementById('<%=RequiredFieldValidator7.ClientID %>');
            ra4.ValidationGroup = "x";
            ValidatorEnable(ra4, false);

            var ra5 = document.getElementById('<%=RegularExpressionValidator1.ClientID %>');
                ra5.ValidationGroup = "x";
                ValidatorEnable(ra5, false);

            }


            var rb = document.getElementById("<%=ddlHyperlink.ClientID%>");
        if (rb.value == "Y") {
            $('#divType').show();


            var ra = document.getElementById('<%=RequiredFieldValidator3.ClientID %>');
            ra.ValidationGroup = "a";
            ValidatorEnable(ra, true);

        }
        else {
            $('#divType').hide();
            $('#divFileUpload').hide();
            $('#divUrl').hide();
            var ra = document.getElementById('<%=RequiredFieldValidator3.ClientID %>');
           ra.ValidationGroup = "x";
           ValidatorEnable(ra, false);
           var ra1 = document.getElementById('<%=RequiredFieldValidator6.ClientID %>');
            ra1.ValidationGroup = "x";
            ValidatorEnable(ra1, false);

            var ra2 = document.getElementById('<%=RequiredFieldValidator4.ClientID %>');
            ra2.ValidationGroup = "x";
            ValidatorEnable(ra2, false);

            var ra3 = document.getElementById('<%=regUrl.ClientID %>');
            ra3.ValidationGroup = "x";
            ValidatorEnable(ra3, false);
        }

        //show hide external/internal

        var rb = document.getElementById("<%=ddlType.ClientID%>");
        if (rb.value == "I") {
            $('#divFileUpload').show();
            $('#divUrl').hide();
            //alert("l");
            var ra = document.getElementById('<%=RequiredFieldValidator6.ClientID %>');
            ra.ValidationGroup = "a";
            ValidatorEnable(ra, true);

            var ra1 = document.getElementById('<%=RequiredFieldValidator4.ClientID %>');
           ra1.ValidationGroup = "x";
           ValidatorEnable(ra1, false);

           var ra2 = document.getElementById('<%=regUrl.ClientID %>');
            ra2.ValidationGroup = "x";
            ValidatorEnable(ra2, false);
        }
        else if (rb.value == "E") {
            $('#divFileUpload').hide();
            $('#divUrl').show();

            var ra = document.getElementById('<%=RequiredFieldValidator6.ClientID %>');
            ra.ValidationGroup = "x";
            ValidatorEnable(ra, false);

            var ra1 = document.getElementById('<%=RequiredFieldValidator4.ClientID %>');
            ra1.ValidationGroup = "a";
            ValidatorEnable(ra1, true);

            var ra2 = document.getElementById('<%=regUrl.ClientID %>');
            ra2.ValidationGroup = "a";
            ValidatorEnable(ra2, true);
        }
};


//blink
function blinker() {
    $('.blinking').fadeOut(500);
    $('.blinking').fadeIn(500);
}
setInterval(blinker, 1000);
//end blink

//---------------------------------------------for preview on keyup function call---------------ekta 3 may 2017---------------//

function header() {

    var test = document.getElementById("CPH_MasterLogin_txtHeader").value;
    document.getElementById("CPH_MasterLogin_lblpreview").innerHTML = test;
    document.getElementById("<%=HiddenField2.ClientID%>").value = test;

}

        <%--function color() {
    var color = document.getElementById("CPH_MasterLogin_color").value;
    alert("hi :" + color);
    document.getElementById("CPH_MasterLogin_lblpreview").innerHTML = color;
    //document.getElementById("<%=HiddenField3.ClientID%>").value = color;
}--%>
    </script>
    <script>




        //-----------------------------------bold----------------------------------------

        $(document).ready(function () {
            $('#<%=RadioButtonListbold.ClientID%> input[type="radio"]').each(function () {


                $(this).click(function () {
                    var selectedd = (this).value;

                    $("#CPH_MasterLogin_RadioButtonListbold").val(selectedd);
                    if (selectedd == "Yes") {
                        var color = document.getElementById("CPH_MasterLogin_color").value;
                        var test = document.getElementById("CPH_MasterLogin_txtHeader").value;
                        document.getElementById("CPH_MasterLogin_lblpreview").innerHTML = "<div><font color=" + color + "><b>" + test + "</b></font></div>";
                        document.getElementById("<%=HiddenFieldbold.ClientID%>").value = "B";

                    }
                    else if (selectedd == "No") {

                        var color = document.getElementById("CPH_MasterLogin_color").value;
                        var test = document.getElementById("CPH_MasterLogin_txtHeader").value;
                        document.getElementById("CPH_MasterLogin_lblpreview").innerHTML = "<div><font color=" + color + ">" + test + "</font></div>";
                        //document.getElementById("<%=HiddenField2.ClientID%>").value = "<div><font color=" + color + ">" + test + "</font></div>";
                    }
                });
            });

        });
        //-------------------------------------------------------blink----------------------
        $(document).ready(function () {
            $('#<%=RadioButtonListblink.ClientID%> input[type="radio"]').each(function () {

                $(this).click(function () {
                    var blink = (this).value;

                    $("#CPH_MasterLogin_RadioButtonListblink").val(blink);
                    if (blink == "Yes") {


                        $("#circle").addClass("blinking");
                        document.getElementById("<%=HiddenFieldblink.ClientID%>").value = "BL";

                    }
                    else if (blink == "No") {
                        $("#circle").removeClass("blinking");
                    }
                });
            });

        });

        //when update selected hidden field
        $(document).ready(function () {
            var rb = document.getElementById("<%=ddlHyperlink.ClientID%>");
            if (rb.value == "Y") {
                $('#divType').show();
                var rbb = document.getElementById("<%=ddlType.ClientID%>");
                if (rbb.value == "I") {
                    $('#divFileUpload').show();
                    $('#divUrl').hide();
                }
                else if (rbb.value == "E") {
                    $('#divFileUpload').hide();
                    $('#divUrl').show();
                }
            }
            else {
                $('#divType').hide();
                $('#divFileUpload').hide();
                $('#divUrl').hide();

            }
        });
        $(document).ready(function () {
            var rb = document.getElementById("<%=ddlType.ClientID%>");
            if (rb.value == "I") {
                $('#divFileUpload').show();
                $('#divUrl').hide();
            }
            else if (rb.value == "E") {
                $('#divFileUpload').hide();
                $('#divUrl').show();
            }
        });

        $(document).ready(function () {
            var rb = document.getElementById("<%=ddlPermanent.ClientID%>");
        if (rb.value == "Y") {
            $('#divPublishDate').show();
            $('#divFrom').hide();
            $('#divTo').hide();
        }
        else if (rb.value == "N") {
            $('#divPublishDate').hide();
            $('#divFrom').show();
            $('#divTo').show();
        }
    });
    </script>



    <%-- --%>
    <div class="col-sm-12">
        <div class="panel panel-default">

            <div class="panel-heading text-center">
                <h3 class="panel-title"><%=Resources.Resource.NoticeUpdateForm %></h3>
            </div>
            <div class="text-danger text-right"><%=Resources.Resource.MandatoryField %></div>

            <div class="panel-heading">
                <h4 class="panel-title"><%=Resources.Resource.ContentOfNoticeBoard %> </h4>
            </div>
            <div class="panel-body">
                <div class="form-horizontal">
                    <asp:UpdatePanel ID="UPD_1" runat="server">
                        <ContentTemplate>
                            <div class="form-group">

                                <div class="col-sm-4">
                                    <label><%=Resources.Resource.Department %>  <span class="text-danger">*</span> </label>
                                    <asp:DropDownList ID="ddl_department" runat="server" class="input" Height="30px" Width="93%" OnSelectedIndexChanged="ddl_department_SelectedIndexChanged" AutoPostBack="true"
                                        CssClass="text_box">
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="rfv_department" runat="server" ControlToValidate="ddl_department" SetFocusOnError="true"
                                        ErrorMessage="required" ForeColor="#FF3300" Font-Size="Large" InitialValue="0" Font-Bold="true" ValidationGroup="a">
                                    </asp:RequiredFieldValidator>
                                </div>
                                <div class="col-sm-4">
                                    <label><%=Resources.Resource.District %> <span class="text-danger">*</span> </label>
                                    <asp:DropDownList ID="ddl_district" runat="server" class="input" Height="30px" Width="93%" OnSelectedIndexChanged="ddlDistrict_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="rfv_district" runat="server" ControlToValidate="ddl_district" SetFocusOnError="true"
                                        ErrorMessage="required" ForeColor="#FF3300" Font-Size="Large" InitialValue="0" Font-Bold="true" ValidationGroup="a">
                                    </asp:RequiredFieldValidator>
                                </div>

                                <div class="col-sm-4">
                                    <label><%=Resources.Resource.Office %> <span class="text-danger">*</span> </label>
                                    <asp:DropDownList ID="ddl_office" runat="server" class="input" Height="30px" Width="93%"></asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="rfv_office" runat="server" ControlToValidate="ddl_office" SetFocusOnError="true"
                                        ErrorMessage="required" ForeColor="#FF3300" Font-Size="Large" InitialValue="0" Font-Bold="true" ValidationGroup="a">
                                    </asp:RequiredFieldValidator>
                                </div>

                            </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                    <div class="form-group">
                        <%-- <div style="padding-left: 15px;">
                                    <h4>Content of Notice Board</h4>
                                </div>--%>

                        <div class="col-sm-4">
                            <label><%=Resources.Resource.Subject %> <span class="text-danger">*</span> </label>

                            <asp:TextBox ID="txtHeader" runat="server" class="input form-control" placeholder="<%$Resources:Resource, SubjectHere %>"
                                OnKeyUp="limiter(),header()" CssClass="form-control" TextMode="MultiLine"></asp:TextBox>
                            <asp:RequiredFieldValidator Display="Dynamic" ID="RequiredFieldValidator8" runat="server" ControlToValidate="txtHeader"
                                ErrorMessage="required" ForeColor="#FF3300" Font-Size="Large" Font-Bold="true" SetFocusOnError="true" ValidationGroup="a">
                            </asp:RequiredFieldValidator>
                            <asp:Label ID="lblChar" runat="server" ForeColor="Maroon">Characters left.</asp:Label>
                            <asp:Label ID="limit1" runat="server" ForeColor="Maroon" Text=""></asp:Label>

                        </div>
                        <div class="col-sm-4">
                            <label><%=Resources.Resource.Active %> <span class="text-danger">*</span> </label>
                            <asp:DropDownList ID="ddlActive" runat="server" AppendDataBoundItems="true" class="input " CssClass="text_box form-control">
                                <asp:ListItem Text="Active" Value="A" Selected="True"></asp:ListItem>
                                <asp:ListItem Text="Inactive" Value="D"></asp:ListItem>
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlActive" SetFocusOnError="true"
                                ErrorMessage="required" ForeColor="#FF3300" Font-Size="Large" InitialValue="0" Font-Bold="true" ValidationGroup="a">
                            </asp:RequiredFieldValidator>
                        </div>
                        <div class="col-sm-4">
                            <label><%=Resources.Resource.Hyperlink %> <span class="text-danger">*</span> </label>
                            <asp:DropDownList ID="ddlHyperlink" onchange="Gettype();" runat="server" class="input"
                                CssClass="text_box form-control">
                                <asp:ListItem Selected="True" Text="Select" Value="0"></asp:ListItem>
                                <asp:ListItem Text="Yes" Value="Y"></asp:ListItem>
                                <asp:ListItem Text="No" Value="N"></asp:ListItem>
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="ddlHyperlink" SetFocusOnError="true"
                                ErrorMessage="required" ForeColor="#FF3300" Font-Size="Large" InitialValue="0" Font-Bold="true" ValidationGroup="a">
                            </asp:RequiredFieldValidator>
                        </div>
                    </div>

                    <div class="form-group">
                        <div id="divType" class="col-sm-4" style="display: none">
                            <label><%=Resources.Resource.HyperlinkType %>  <span class="text-danger">*</span> </label>
                            <asp:DropDownList ID="ddlType" runat="server" onchange="Get2();"
                                CssClass="text_box form-control">
                                <asp:ListItem Selected="True" Text="Select" Value="0"></asp:ListItem>
                                <asp:ListItem Text="Internal" Value="I"></asp:ListItem>
                                <asp:ListItem Text="External" Value="E"></asp:ListItem>
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="ddlType" SetFocusOnError="true"
                                ErrorMessage="required" ForeColor="#FF3300" Font-Size="Large" InitialValue="0" Font-Bold="true" ValidationGroup="a">
                            </asp:RequiredFieldValidator>
                        </div>

                        <div id="divFileUpload" class="col-sm-6" style="display: none">
                            <div class="col-sm-8">
                                <label><%=Resources.Resource.FileUpload %>  <span class="text-danger">*</span> </label>
                                <asp:FileUpload ID="FileUpload1" runat="server" class="input form-control" />
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="FileUpload1" SetFocusOnError="true"
                                    ErrorMessage="required" ForeColor="#FF3300" Font-Size="Large" Font-Bold="true" ValidationGroup="a">
                                </asp:RequiredFieldValidator>


                                <asp:Label ID="Label1" runat="server" ForeColor="Maroon"><%=Resources.Resource.OnlyPDFandJPGfilesareAllowed %></asp:Label>

                                <asp:Label ID="Label2" runat="server" ForeColor="Maroon"><%=Resources.Resource.Maximumsizeallowed %></asp:Label>
                            </div>
                             <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                        <ContentTemplate>
                            <div class="col-sm-4">
                                <asp:LinkButton ID="LinkButton2" runat="server" OnClick="LinkButton2_Click" CausesValidation="false">View</asp:LinkButton>
                            </div>
                            </ContentTemplate>
                                 </asp:UpdatePanel>
                        </div>

                        <div id="divUrl" class="col-sm-6" style="display: none">
                            <label><%=Resources.Resource.EnterURL %> <span class="text-danger">*</span> </label>
                            <asp:TextBox ID="txtUrl" runat="server" class="input" CssClass="text_box form-control" placeholder="<%$Resources:Resource, Ex_URL %>"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtUrl"
                                ErrorMessage="required" ForeColor="#FF3300" Font-Bold="true" Font-Size="Large" ValidationGroup="a">
                            </asp:RequiredFieldValidator>
                            <asp:Label ID="lblUrl" runat="server" ForeColor="Maroon">Ex. http://google.com</asp:Label>
                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                    <asp:RegularExpressionValidator ID="regUrl" runat="server" ControlToValidate="txtUrl" ForeColor="#FF3300" SetFocusOnError="true"
                                        ValidationExpression="(http|ftp|https):\/\/[\w\-_]+(\.[\w\-_]+)+([\w\-\.,@?^=%&amp;:/~\+#]*[\w\-\@?^=%&amp;/~\+#])?" Text="Invalid" />
                        </div>
                    </div>
                </div>
            </div>


            <div class="panel-heading">
                <h4 class="panel-title"><%=Resources.Resource.LayoutofNoticeBoard %></h4>
            </div>
            <div class="panel-body">
                <div class="form-horizontal">
                    <div class="form-group">
                        <%-- <div style="padding-left: 15px;">
                                    <h4>Layout of Notice Board</h4>
                                </div>--%>


                        <div class="col-sm-4">
                            <label><%=Resources.Resource.Priority %>  </label>
                            <asp:DropDownList ID="ddlpriority" runat="server" class="input"
                                CssClass="text_box form-control">
                                <asp:ListItem Selected="True" Text="Select" Value="0"></asp:ListItem>
                                <asp:ListItem Text="High" Value="H"></asp:ListItem>
                                <asp:ListItem Text="Medium" Value="M"></asp:ListItem>
                                <asp:ListItem Text="Low" Value="L"></asp:ListItem>
                            </asp:DropDownList>
                        </div>
                        <div class="col-sm-4">
                            <label><%=Resources.Resource.Bold %>   </label>
                            <asp:RadioButtonList ID="RadioButtonListbold" runat="server" CssClass="RadioButtonWidth form-control" RepeatLayout="Flow" RepeatDirection="Horizontal">
                                <asp:ListItem Text="Yes" Value="Yes"></asp:ListItem>
                                <asp:ListItem Text="No" Value="No"></asp:ListItem>
                            </asp:RadioButtonList>

                        </div>
                        <div class="col-sm-4">
                            <label><%=Resources.Resource.ImageBlink %> </label>
                            <asp:RadioButtonList ID="RadioButtonListblink" runat="server" CssClass="RadioButtonWidth form-control" RepeatLayout="Flow" RepeatDirection="Horizontal">
                                <asp:ListItem Text="Yes" Value="Yes"></asp:ListItem>
                                <asp:ListItem Text="No" Value="No"></asp:ListItem>
                            </asp:RadioButtonList>
                        </div>

                    </div>
                    <div class="form-group">

                        <div class="col-sm-4">
                            <label><%=Resources.Resource.SelectImage %>  </label>
                            <asp:DropDownList ID="ddlimage" runat="server" CssClass="form-control" class="mydds form-control">
                                <asp:ListItem Value="0">Select</asp:ListItem>
                            </asp:DropDownList>
                            <%--<asp:Label ID="lbltext" runat="server"></asp:Label>--%>
                        </div>
                        <div class="col-sm-4">
                            <label><%=Resources.Resource.Color %>  </label>
                            <asp:TextBox ID="color" placeholder="<%$Resources:Resource, Ex_Color %>" runat="server" class="form-control"></asp:TextBox>
                            <div id="preview">
                                <br />
                                <asp:ColorPickerExtender ID="ColorPicker1" runat="server"
                                    TargetControlID="color"
                                    SampleControlID="preview"
                                    PopupButtonID="color"
                                    PopupPosition="Right"
                                    OnClientColorSelectionChanged="Color_Changed" />
                                <%--<label>Image  </label>    
                                    <asp:DropDownList ID="ddl_icon" runat="server" AppendDataBoundItems="true" class="input ddl_icon" Height="30px" Width="93%" CssClass="text_box form-control">
                                        <asp:ListItem Text="Active" Value="A" Selected="True"></asp:ListItem>
                                        <asp:ListItem Text="Inactive" Value="D"></asp:ListItem>
                                    </asp:DropDownList> --%>
                            </div>
                            <div class="col-sm-4">
                                <label></label>
                                <div class="col-sm-6">
                                </div>
                                <%--<asp:Button ID="Button1" runat="server" Text="Choose Color" />--%>
                            </div>
                        </div>
                    </div>
                </div>
            </div>


            <div class="panel-heading">
                <h3 class="panel-title"><%=Resources.Resource.DurationofNoticeBoard %></h3>
            </div>
            <div class="panel-body">
                <div class="form-horizontal">
                    <div class="form-group">
                        <%-- <div style="padding-left: 15px;">
                                    <h4>Duration of Notice Board</h4>
                                </div>--%>


                        <div class="col-sm-4">
                            <label><%=Resources.Resource.DisplayPermanently %><span class="text-danger">*</span> </label>
                            <asp:DropDownList ID="ddlPermanent" onchange="Get();" runat="server" class="input" Height="30px" Width="93%"
                                CssClass="text_box form-control">
                                <asp:ListItem Text="Yes" Value="Y"></asp:ListItem>
                                <asp:ListItem Text="No" Value="N" Selected="True"></asp:ListItem>
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="ddlPermanent" SetFocusOnError="true"
                                ErrorMessage="required" ForeColor="#FF3300" Font-Size="Large" InitialValue="0" Font-Bold="true" ValidationGroup="a">
                            </asp:RequiredFieldValidator>

                        </div>
                        <div id="divPublishDate" class="col-sm-3" style="">
                            <label><%=Resources.Resource.PublishDate %> <span class="text-danger">*</span> </label>
                            <div class="input-group">
                                <asp:TextBox ID="txtPublishDate" runat="server" placeholder="<%$Resources:Resource, Ex_PublishDate %>" CssClass="form-control"></asp:TextBox>
                                <span class="input-group-btn   pull-right" style="position: relative;">
                                    <asp:ImageButton ID="btn_calander2" runat="server" CausesValidation="False" Height="34" Width="34" ImageUrl="~/images/calendar-icon.png" />
                                </span>
                                <asp:CalendarExtender ID="CalendarExtender3" runat="server" Format="dd/MM/yyyy" PopupButtonID="btn_calander2" TargetControlID="txtPublishDate">
                                </asp:CalendarExtender>

                                <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="txtPublishDate" ErrorMessage="required" ForeColor="#FF3300"
                                    Font-Size="Large" Font-Bold="true" SetFocusOnError="true" ValidationGroup="a">
                                </asp:RequiredFieldValidator>
                                <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtPublishDate" SetFocusOnError="True"
                                    ErrorMessage="Invaild Date" ForeColor="#FF3300" ValidationExpression="^[0-9]{1,2}/[0-9]{1,2}/[0-9]{4}( [0-9]{2}:[0-9]{2}:[0-9]{2})?$" ValidationGroup="a">
                                </asp:RegularExpressionValidator>
                            </div>
                        </div>
                        <div id="divFrom" class="col-sm-3 ">
                            <label><%=Resources.Resource.DisplayFrom %> <span class="text-danger">*</span> </label>
                            <div class="input-group">
                                <asp:TextBox ID="txtDateFrom" runat="server" placeholder="<%$Resources:Resource, DisplayFrom %>" CssClass="form-control"></asp:TextBox>
                                <span class="input-group-btn   pull-right" style="position: relative; width: unset;">
                                    <asp:ImageButton ID="btn_calander" CssClass="" runat="server" CausesValidation="False" Height="34" Width="34" ImageUrl="~/images/calendar-icon.png" />
                                </span>
                                <asp:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd/MM/yyyy" PopupButtonID="btn_calander" TargetControlID="txtDateFrom">
                                </asp:CalendarExtender>

                                <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ControlToValidate="txtDateFrom" ErrorMessage="required" ForeColor="#FF3300"
                                    Font-Size="Large" Font-Bold="true" SetFocusOnError="true" ValidationGroup="a">
                                </asp:RequiredFieldValidator>
                                <asp:RegularExpressionValidator ID="RegularExpressionValidator10" runat="server" ControlToValidate="txtDateFrom" SetFocusOnError="True"
                                    ErrorMessage="Invaild Date" ForeColor="#FF3300" ValidationExpression="^[0-9]{1,2}/[0-9]{1,2}/[0-9]{4}( [0-9]{2}:[0-9]{2}:[0-9]{2})?$" ValidationGroup="a">
                                </asp:RegularExpressionValidator>
                            </div>
                        </div>

                        <div id="divTo" class="col-sm-3">
                            <label><%=Resources.Resource.DisplayTo %> <span class="text-danger">*</span> </label>
                            <div class="input-group">
                                <asp:TextBox ID="txtDateTo" runat="server" placeholder="<%$Resources:Resource, DisplayTo %>" CssClass="form-control"></asp:TextBox>
                                <span class="input-group-btn   pull-right" style="position: relative; width: unset;">
                                    <asp:ImageButton ID="btn_calander1" runat="server" CausesValidation="False" Height="34" Width="34" ImageUrl="~/images/calendar-icon.png" />
                                </span>
                                <asp:CalendarExtender ID="CalendarExtender2" runat="server" Format="dd/MM/yyyy" PopupButtonID="btn_calander1" TargetControlID="txtDateTo">
                                </asp:CalendarExtender>


                                <asp:RequiredFieldValidator Display="Dynamic" ID="RequiredFieldValidator10" runat="server" ControlToValidate="txtDateTo" ErrorMessage="required" ForeColor="#FF3300"
                                    Font-Size="Large" Font-Bold="true" SetFocusOnError="true" ValidationGroup="a">
                                </asp:RequiredFieldValidator>
                                <asp:RegularExpressionValidator ID="RegularExpressionValidator17" runat="server" ControlToValidate="txtDateTo" SetFocusOnError="True"
                                    ErrorMessage="Invaild Date" ForeColor="#FF3300" ValidationExpression="^[0-9]{1,2}/[0-9]{1,2}/[0-9]{4}( [0-9]{2}:[0-9]{2}:[0-9]{2})?$" ValidationGroup="a">
                                </asp:RegularExpressionValidator>
                                <asp:CompareValidator ControlToCompare="txtDateFrom" ControlToValidate="txtDateTo" CultureInvariantValues="false" Display="Dynamic" CssClass="text-danger" ErrorMessage="Invalid Date"
                                    ID="CompareValidator1" Operator="GreaterThanEqual" Type="Date" runat="server" />
                            </div>
                        </div>

                    </div>
                </div>
            </div>

            <div class="panel-body">
                <div class="form-horizontal">
                    <div class="form-group">
                        <div class="col-sm-12">

                            <div class="col-sm-6">
                                <div class="col-sm-2" id="circle">
                                    <div class="col-sm-12" runat="server" id="blinkimage">
                                        <asp:Image ID="Image1" runat="server" />
                                        <asp:HiddenField ID="HiddenField1" runat="Server" />
                                    </div>
                                </div>
                                <div class="col-sm-10">
                                    <asp:Label ID="lblpreview" runat="server"></asp:Label>
                                    <asp:HiddenField ID="HiddenField2" runat="Server" />
                                    <asp:HiddenField ID="HiddenField3" runat="Server" />
                                    <asp:HiddenField ID="HiddenFieldbold" runat="Server" />
                                    <asp:HiddenField ID="HiddenFieldblink" runat="Server" />


                                </div>
                            </div>

                            <asp:Literal ID="literal" runat="server"> </asp:Literal>
                        </div>
                    </div>
                </div>
            </div>


            <div class="form-group" style="padding-left: 650px;">
                <%-- <asp:Button ID="Btnpreview" runat="server" Text="Preview" OnClick="Btnpreview_Click" OnClientClick="preview();" CssClass="button button-large button-dark button-rounded" ValidationGroup="a" />--%>

                <asp:Button ID="btnUpdate" runat="server" Text="<%$Resources:Resource, Update %>" OnClick="btnUpdate_Click" CssClass="button button-large button-dark button-rounded"  ValidationGroup="a"/>
            </div>

        </div>
    </div>
    <%--</ContentTemplate>
        <Triggers>
            
            <asp:PostBackTrigger ControlID="btnSubmit" />
        </Triggers>
    </asp:UpdatePanel>--%>
</asp:Content>

