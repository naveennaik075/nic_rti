<%@ Page Title="" Language="C#" MasterPageFile="~/admin_master.master" AutoEventWireup="true" CodeFile="DepartmentUserManual.aspx.cs" Inherits="admin_DepartmentUserManual" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CPH_MasterLogin" runat="Server">
    <script type="text/javascript">
        function compareYear() {
            var txtYearOfIssue = document.getElementById("<%=txtYearOfIssue.ClientID%>");
              var hiddennn = document.getElementById("<%=hiddenn.ClientID%>");
              if (txtYearOfIssue.value < hiddennn.value) {
                  alert("Year should be greater then or equal to current year");
              }
          }
    </script>


    <div class="panel panel-success">
        <div class="panel-heading">
            <h5 class="panel-title">
                <b>
                    <asp:Label ID="Label3" runat="server" Text="<%$Resources:Resource, Upload_Dept_User_Manual %>"></asp:Label>

                </b>
            </h5>
        </div>
        <div class="panel-body">
            <div class="form-horizontal">
                <asp:UpdatePanel runat="server" ID="update1">
                    <ContentTemplate>
                        <div class="form-group">
                            <div class="col-sm-3">
                                <span class="lol1">*</span>
                                <asp:Label ID="Label21" runat="server" Text="<%$Resources:Resource, District %>"></asp:Label>
                            </div>
                            <div class="col-sm-3">
                                <asp:DropDownList ID="RTI_DDL_District" runat="server" AutoPostBack="True" OnSelectedIndexChanged="DDL_District_SelectedIndexChanged" CssClass=" form-control">
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="RTI_RFV_District" runat="server" ControlToValidate="RTI_DDL_District" Display="Dynamic" ErrorMessage="Select District" CssClass="text-danger" ValidationGroup="vg" InitialValue="0"></asp:RequiredFieldValidator>
                            </div>
                            <div class="col-sm-3">
                                <span class="lol1">*</span>
                                <asp:Label ID="Label22" runat="server" Text="<%$Resources:Resource, BaseDepartmentName %> "> </asp:Label>
                            </div>
                            <div class="col-sm-3">
                                <asp:DropDownList ID="DDL_BaseDepartment" runat="server" AutoPostBack="True" OnSelectedIndexChanged="DDL_BaseDepartment_SelectedIndexChanged" CssClass=" form-control">
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="RFV_BaseDepartment" runat="server" ControlToValidate="DDL_BaseDepartment" Display="Dynamic" ErrorMessage="Base Department Required" CssClass="text-danger" ValidationGroup="vg" InitialValue="0"></asp:RequiredFieldValidator>
                            </div>
                        </div>
                        <div class="form-group">
                            <div class="col-sm-3">
                                <span class="lol1">*</span>
                                <asp:Label ID="Label23" runat="server" Text="<%$Resources:Resource, OfficeCategory %> "></asp:Label>
                            </div>
                            <div class="col-sm-3">
                                <asp:DropDownList ID="DDL_OfficeCategory" runat="server" AutoPostBack="True" OnSelectedIndexChanged="DDL_Category_SelectedIndexChanged" CssClass="form-control">
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="RFV_OfficeCategory" runat="server" ControlToValidate="DDL_OfficeCategory" Display="Dynamic" ErrorMessage="Select Office Category" CssClass="text-danger" ValidationGroup="vg" InitialValue="0"></asp:RequiredFieldValidator>
                            </div>
                            <div class="col-sm-3">
                                <span class="lol1">*</span>
                                <asp:Label ID="Label24" runat="server" Text="<%$Resources:Resource, OfficeLevel %>"> </asp:Label>
                            </div>
                            <div class="col-sm-3">
                                <asp:DropDownList ID="DDL_OfficeLevel" runat="server" AutoPostBack="True" OnSelectedIndexChanged="DDL_OfficeLevel_SelectedIndexChanged" CssClass=" form-control">
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="RFV_OfficeLevel" runat="server" ControlToValidate="DDL_OfficeLevel" Display="Dynamic" ErrorMessage="Office Level Required" CssClass="text-danger" ValidationGroup="vg" InitialValue="0"></asp:RequiredFieldValidator>
                            </div>
                        </div>
                        <div class="form-group">
                            <div class="col-sm-3">
                                <span class="lol1">*</span>
                                <asp:Label ID="Label25" runat="server" Text="<%$Resources:Resource, Office %>"></asp:Label>
                            </div>
                            <div class="col-sm-3">
                                <asp:DropDownList ID="DDL_Office" runat="server" CssClass=" form-control">
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="RFV_Office" runat="server" ControlToValidate="DDL_Office" Display="Dynamic" ErrorMessage="Office  Required" CssClass="text-danger" ValidationGroup="vg" InitialValue="0"></asp:RequiredFieldValidator>
                            </div>
                            <div class="col-sm-3">
                                <span class="lol1">*</span>
                                <asp:Label ID="Label14" runat="server" Text="<%$Resources:Resource, YearofIssue %>"></asp:Label>


                            </div>
                            <div class="col-sm-3">
                                <asp:TextBox ID="txtYearOfIssue" onblur="compareYear();" runat="server" MaxLength="4" CssClass=" form-control" placeholder="<%$Resources:Resource, IssueYear %>"></asp:TextBox>
                                <%--<asp:TextBox ID="HiddenTodayDate" Visible="false" runat="server" />--%>
                                <asp:HiddenField ID="hiddenn" runat="server" />
                                <%--<asp:CompareValidator ID="CV_Year_Of_Issue" runat="server" ControlToValidate="txtYearOfIssue" ControlToCompare="HiddenTodayDate" Display="Dynamic" CssClass="text-danger" ErrorMessage="Year Should be less then or equal to current year" Operator="LessThanEqual" Type="String" ValidationGroup="vg"></asp:CompareValidator>--%>
                                <asp:RegularExpressionValidator ID="REV_Year_Of_Issue" runat="server" ControlToValidate="txtYearOfIssue" Display="Dynamic" ErrorMessage="Enter Valid Year" CssClass="text-danger" ValidationExpression="\d+" ValidationGroup="vg"></asp:RegularExpressionValidator>
                                <asp:RequiredFieldValidator ID="RFV_Year_Of_Issue" runat="server" ControlToValidate="txtYearOfIssue" Display="Dynamic" ErrorMessage="Year Required" CssClass="text-danger" ValidationGroup="vg"></asp:RequiredFieldValidator>
                            </div>
                        </div>
                    </ContentTemplate>

                </asp:UpdatePanel>

                <div class="form-group" id="div_file_upload" runat="server">
                    <div class="col-sm-3">
                        <label class="text-primary"><%=Resources.Resource.InsertFile %>:</label>
                        <label class="text-danger" style="font-size: small"><%=Resources.Resource.OnlyPdfIsAcceptable %> </label>
                    </div>
                    <div class="col-sm-3">
                        <asp:FileUpload ID="fu_UserManual" runat="server" />
                        <asp:RequiredFieldValidator ID="rfv_fu" runat="server" ValidationGroup="vg" Display="Dynamic" ControlToValidate="fu_UserManual" ErrorMessage="File is Required" CssClass="alert-danger"></asp:RequiredFieldValidator>
                    </div>
                    <div class="col-sm-3">
                        <label class="text-primary"><%=Resources.Resource.FileDescription %> :</label>
                    </div>
                    <div class="col-sm-3">
                        <asp:TextBox ID="txt_file_desc" runat="server" ValidationGroup="vg" CssClass="form-control"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="rfv_file_desc" runat="server" ValidationGroup="vg" ControlToValidate="txt_file_desc" ErrorMessage="File Discription is required" CssClass="alert-danger" Display="Dynamic"></asp:RequiredFieldValidator>
                    </div>
                </div>
            </div>
        </div>
        <div class="form-group center">
            <asp:Button ID="btnSubmit" runat="server" Text="<%$Resources:Resource, Submit %>" ValidationGroup="vg" CssClass="btn button-blue" ForeColor="White" OnClick="btnSubmit_Click" />
        </div>
    </div>

</asp:Content>

