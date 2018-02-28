<%@ Page Title="" Language="C#" MasterPageFile="~/Master_dio.master" AutoEventWireup="true" CodeFile="office_request.aspx.cs" Inherits="unregistered_office_request" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Content_master_dio" runat="Server">
    <script type="text/javascript">
        var count2 = "100";var lent;
        function limiterAdd() {

            document.getElementById("Content_master_dio_limit").innerHTML = count2-lent;
            var text = document.getElementById("Content_master_dio_txt_address").value;
            lent = text.length;
            if (lent > count2) {
                text = text.substring(0, count2);
                document.getElementById("Content_master_dio_txt_address").value = text;
                return false;
            }
            document.getElementById("Content_master_dio_limit").innerHTML = count2 - lent;
        }
    </script>
    <div class="panel panel-info">
        <div class="panel-heading">
            <label class="text-primary center">
                New Office Request
            </label>
        </div>
        <div class="panel-body">
            <div class="form-horizontal">
                <asp:UpdatePanel ID="udp" runat="server">
                    <ContentTemplate>
                        <div class="form-group">
                            <div class="col-sm-3">
                                <label class="text-primary">
                                    Select Department: <span class="text-danger">*</span>
                                </label>
                            </div>
                            <div class="col-sm-3">
                                <asp:DropDownList runat="server" ID="ddl_department" AppendDataBoundItems="true" AutoPostBack="true" OnSelectedIndexChanged="ddl_department_SelectedIndexChanged" CssClass="form-control" ValidationGroup="vg">
                                    <asp:ListItem Value="0" Text="---Select Department---"></asp:ListItem>
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="rfv_ddl_dept" runat="server" ValidationGroup="vg" ControlToValidate="ddl_department" InitialValue="0"
                                    CssClass="alert-danger" ErrorMessage="Department is Required" Display="Dynamic" SetFocusOnError="true"></asp:RequiredFieldValidator>
                            </div>
                            <div class="col-sm-3">
                                <label class="text-primary">Selecr District:<span class="text-danger">*</span> </label>
                            </div>
                            <div class="col-sm-3">
                                <asp:DropDownList runat="server" ID="ddl_district" AppendDataBoundItems="true" AutoPostBack="true" OnSelectedIndexChanged="ddl_district_SelectedIndexChanged" CssClass="form-control" ValidationGroup="vg">
                                    <asp:ListItem Value="0" Text="---Select District---"></asp:ListItem>
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="rfv_district" runat="server" ValidationGroup="vg" ControlToValidate="ddl_district" InitialValue="0"
                                    CssClass="alert-danger" ErrorMessage="District is Required" Display="Dynamic" SetFocusOnError="true"></asp:RequiredFieldValidator>
                            </div>
                        </div>
                        <div class="form-group">
                            <div class="col-sm-3">
                                <label class="text-primary">Office Level:<span class="text-danger">*</span></label>
                            </div>
                            <div class="col-sm-3">
                                <asp:DropDownList ID="ddl_Ofc_Lvl" runat="server" ValidationGroup="vg" CssClass="form-control"
                                    OnSelectedIndexChanged="ddl_Ofc_Lvl_SelectedIndexChanged" AppendDataBoundItems="true" AutoPostBack="true">
                                    <asp:ListItem Value="0" Text="---Select Office Level---"></asp:ListItem>
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="rfv_ofclvl" runat="server" ValidationGroup="vg" ControlToValidate="ddl_Ofc_Lvl" InitialValue="0"
                                    CssClass="alert-danger" ErrorMessage="Office Level is Required" Display="Dynamic" SetFocusOnError="true"></asp:RequiredFieldValidator>
                            </div>
                            <div class="col-sm-3">
                                <label class="text-primary">Office Category:<span class="text-danger">*</span></label>
                            </div>
                            <div class="col-sm-3">
                                <asp:DropDownList ID="ddl_category" runat="server" ValidationGroup="vg" CssClass="form-control" AppendDataBoundItems="true" OnSelectedIndexChanged="ddl_category_SelectedIndexChanged" AutoPostBack="true">
                                    <asp:ListItem Value="0" Text="---Select Category"></asp:ListItem>
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="rfv_category" runat="server" ValidationGroup="vg" ControlToValidate="ddl_category" InitialValue="0"
                                    CssClass="alert-danger" ErrorMessage="Office Category is Required" Display="Dynamic" SetFocusOnError="true"></asp:RequiredFieldValidator>
                            </div>
                        </div>
                        <div class="form-group">
                            <div class="col-sm-3">
                                <label class="text-primary">
                                    Office Name:  <span class="text-danger">*</span>
                                </label>
                            </div>
                            <div class="col-sm-3">
                                <asp:TextBox ID="txt_office" runat="server" ValidationGroup="vg" CssClass="form-control" placeholder="100 characters" MaxLength="100"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="rfv_office" runat="server" ValidationGroup="vg" SetFocusOnError="true" CssClass="alert-danger" ControlToValidate="txt_office" ErrorMessage="Office Name Is Required"></asp:RequiredFieldValidator>
                            </div>
                            <div class="col-sm-3">
                                <label class="text-primary">
                                    Office Address:  <span class="text-danger">*</span>
                                </label>
                            </div>
                            <div class="col-sm-3">
                                <asp:TextBox ID="txt_address" runat="server" ValidationGroup="vg" CssClass="form-control lol" MaxLength="100" TextMode="MultiLine" onkeyup="limiterAdd()"></asp:TextBox>
                                <asp:Label ID="lblChar" runat="server" CssClass="text-danger" Text="Characters left."></asp:Label>
                                <asp:Label ID="limit" runat="server" CssClass="text-danger" Text="100"></asp:Label>
                                <asp:RequiredFieldValidator ID="rfv_ofc_add" runat="server" ValidationGroup="vg" SetFocusOnError="true" CssClass="alert-danger" ControlToValidate="txt_address" ErrorMessage="Office Address Is Required"></asp:RequiredFieldValidator>
                            </div>
                        </div>
                        <div class="form-group">
                            <div class="col-sm-3">
                                <label class="text-primary">
                                    Contact Number: <span class="text-danger">*</span>
                                </label>
                            </div>
                            <div class="col-sm-3">
                                <asp:TextBox ID="txt_contact" runat="server" ValidationGroup="vg" CssClass="form-control" MaxLength="10" placeholder="Only 10 Numbers "></asp:TextBox>
                                <asp:RegularExpressionValidator ID="rev_mobile" runat="server" ControlToValidate="txt_contact" CssClass="alert-danger" Display="Dynamic" ErrorMessage="Please Enter a valid Number" SetFocusOnError="True" ValidationExpression="^[1-9][0-9]{9}$" ValidationGroup="vg"></asp:RegularExpressionValidator>
                            </div>
                            <div class="col-sm-3">
                                <label class="text-primary">
                                    Fax Number: <span class="text-danger">*</span>
                                </label>
                            </div>
                            <div class="col-sm-3">
                                <asp:TextBox ID="txt_fax" runat="server" ValidationGroup="vg" MaxLength="11" placeholder="only numbers allow" CssClass="form-control"></asp:TextBox>

                                <asp:RegularExpressionValidator ID="rev_fax" runat="server" ControlToValidate="txt_fax" CssClass="alert-danger" Display="Dynamic" ErrorMessage="Please Enter a valid FAX Number" 
                                    SetFocusOnError="True" ValidationExpression="^[0-9]{11}$" ValidationGroup="vg"></asp:RegularExpressionValidator>
                            </div>
                        </div>
                        <div class="form-group">
                            <div class="col-sm-3">
                                <label class="text-primary">
                                    Office Email: <span class="text-danger">*</span>
                                </label>
                            </div>
                            <div class="col-sm-3">
                                <asp:TextBox ID="txt_email" runat="server" CssClass="form-control" ValidationGroup="vg" MaxLength="60"></asp:TextBox>
                                <asp:RegularExpressionValidator ID="rev_email" runat="server" ControlToValidate="txt_email" CssClass="alert-danger" Display="Dynamic" ErrorMessage="Please Enter Valid Email ID" 
                                    SetFocusOnError="True" ValidationGroup="vg" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"></asp:RegularExpressionValidator>
                            </div>
                            <div class="col-sm-3">
                                <label class="text-primary">
                                    Office Website: <span class="text-danger">*</span>
                                </label>
                            </div>
                            <div class="col-sm-3">
                                <asp:TextBox ID="txt_url" runat="server"  CssClass="form-control" MaxLength="50"></asp:TextBox>
                            </div>
                        </div>
                        <div class="form-group">
                            <div class="col-sm-6">
                                <div class="col-sm-6">
                                    <label class="text-primary">Insert Your Office Id:<span class="text-danger">*</span></label>
                                </div>
                                <div class="col-sm-6">
                                    <asp:FileUpload runat="server" ID="fu_identity" />
                                    <asp:RequiredFieldValidator ID="rfv_filleupload" runat="server" ControlToValidate="fu_identity" SetFocusOnError="true" ErrorMessage="File Is Required" ValidationGroup="vg" CssClass="alert-danger"></asp:RequiredFieldValidator>
                                </div>
                                <label class="text-danger" style="font-size: xx-small"><span class="text-primary">Note:</span> Image only acceptable in .jpg or .jpeg format it should not greater than 200 kb</label>
                            </div>
                        </div>
                        <div class="form-group center">
                            <asp:Button ID="btn_submit" runat="server" ValidationGroup="vg" OnClick="btn_submit_Click" CssClass="btn btn-info" Text="submit" />
                        </div>
                    </ContentTemplate>
                    <Triggers>
                        <asp:PostBackTrigger ControlID="btn_submit" />
                    </Triggers>
                </asp:UpdatePanel>
            </div>
        </div>
    </div>
</asp:Content>

