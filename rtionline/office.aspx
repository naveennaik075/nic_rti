<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="office.aspx.cs" Inherits="report" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CPH_MasterLogin" runat="Server">
  
    <script type="text/javascript">
        var count2 = "100";var lent;
        function limiterAdd() {

            document.getElementById("CPH_MasterLogin_limit").innerHTML = count2 - lent;
            var text = document.getElementById("CPH_MasterLogin_txt_address").value;
            lent = text.length;
            if (lent > count2) {
                text = text.substring(0, count2);
                document.getElementById("CPH_MasterLogin_txt_address").value = text;
                return false;
            }
            document.getElementById("CPH_MasterLogin_limit").innerHTML = count2 - lent;
        }
    </script>
      <style>
        .cssPager td {
            padding-left: 4px;
            padding-right: 4px;
        }

        .txt-bold {
            font-size: medium;
            font-weight: bold;
        }
    </style>
    <asp:UpdatePanel ID="udp" runat="server">
        <ContentTemplate>

            <div class="panel panel-info" id="pnl_div" runat="server">
                <div class="panel-heading">
                    <div class="panel-title">
                        <asp:Label ID="lbl_section1" runat="server" CssClass="text-primary" Text=" OFFICE"></asp:Label>
                    </div>
                </div>
                <div class="panel-body">
                    <div class="form-horizontal">
                        <div class="form-group">
                            <div class="col-sm-3">
                                <label class="text-primary">Department :</label><span class="text-danger">*</span>
                            </div>
                            <div class="col-sm-3">
                                <asp:DropDownList ID="ddl_department" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddl_department_SelectedIndexChanged" ValidationGroup="vg" CssClass="form-control">
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="rfv_department" runat="server" ValidationGroup="vg" ControlToValidate="ddl_department" CssClass="alert-danger" Display="Dynamic" ErrorMessage=" Please Select Department" SetFocusOnError="true" InitialValue="0"></asp:RequiredFieldValidator>
                            </div>
                            <div class="col-sm-3">
                                <label class="text-primary">District :</label><span class="text-danger">*</span>
                            </div>
                            <div class="col-sm-3">
                                <asp:DropDownList ID="ddl_district" runat="server" AutoPostBack="true" ValidationGroup="vg" CssClass="form-control" OnSelectedIndexChanged="ddl_district_SelectedIndexChanged">
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="rfv_district" runat="server" ValidationGroup="vg" ControlToValidate="ddl_district" CssClass="alert-danger" Display="Dynamic" ErrorMessage=" Please Select District" SetFocusOnError="true" InitialValue="0"></asp:RequiredFieldValidator>
                            </div>

                        </div>
                        <div class="form-group" id="div_ofc" runat="server">
                            <div class="col-sm-3">
                                <label class="text-primary  ">Office:</label>
                            </div>
                            <div class="col-sm-3">
                                <asp:DropDownList ID="ddl_doffice" runat="server" AutoPostBack="true" ValidationGroup="vg" CssClass="form-control" OnSelectedIndexChanged="ddl_doffice_SelectedIndexChanged">
                                    <asp:ListItem Value="0" Text="-----------Select-----------------"></asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div id="div_ofc_req" runat="server" visible="false">
                            <div class="form-group">
                                <div class="col-sm-3">
                                    <label class="text-primary">Office Level:<span class="text-danger">*</span></label>
                                </div>
                                <div class="col-sm-3">
                                    <asp:DropDownList ID="ddl_Ofc_Lvl" runat="server" ValidationGroup="vg" CssClass="form-control"
                                        AppendDataBoundItems="true">
                                        <asp:ListItem Value="0" Text="---Select Office Level---"></asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="rfv_ofclvl" runat="server" ValidationGroup="vg" ControlToValidate="ddl_Ofc_Lvl" InitialValue="0"
                                        CssClass="alert-danger" ErrorMessage="Office Level is Required" Display="Dynamic" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                </div>
                                <div class="col-sm-3">
                                    <label class="text-primary">Office Category:<span class="text-danger">*</span></label>
                                </div>
                                <div class="col-sm-3">
                                    <asp:DropDownList ID="ddl_category" runat="server" ValidationGroup="vg" CssClass="form-control" AppendDataBoundItems="true">
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
                                    <asp:TextBox ID="txt_url" runat="server" CssClass="form-control" MaxLength="50"></asp:TextBox>
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
                                <asp:Button ID="btn_preview" runat="server" ValidationGroup="vg" Text="Preview" CssClass="btn btn-info" OnClick="btn_preview_Click" />
                            </div>
                        </div>
                        <asp:Panel runat="server" ID="pnl_grid" ScrollBars="Both" CssClass="panel panel-info">
                            <div class="row center">
                                <asp:Label ID="lbl_count" runat="server" CssClass="text-primary bold"></asp:Label>
                            </div>
                            <asp:GridView ID="GV_Office" runat="server" AllowPaging="true" PageSize="15" AutoGenerateColumns="false" ShowHeaderWhenEmpty="true"
                                CssClass="table table-striped table-bordered  table-hover table-grid " OnPageIndexChanging="GV_Office_PageIndexChanging">
                                <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                <HeaderStyle BackColor="#507CD1" HorizontalAlign="left" VerticalAlign="Middle" Font-Bold="True" ForeColor="White" />
                                <AlternatingRowStyle BackColor="White" />
                                <Columns>
                                    <asp:TemplateField HeaderText="S.No.">
                                        <ItemTemplate>
                                            <%# Container.DataItemIndex+1 %>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="left" />
                                        <ControlStyle Width="10px" />
                                        <HeaderStyle HorizontalAlign="left" />
                                    </asp:TemplateField>
                                    <asp:BoundField HeaderText="Office Name" DataField="office">
                                        <ItemStyle HorizontalAlign="left" />
                                        <HeaderStyle HorizontalAlign="left" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="basedept" HeaderText="Base Department">
                                        <ItemStyle HorizontalAlign="left" />
                                        <HeaderStyle HorizontalAlign="left" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="ofclvl" HeaderText="Office Level">
                                        <ItemStyle HorizontalAlign="left" />
                                        <HeaderStyle HorizontalAlign="left" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="district" HeaderText=" Office District">
                                        <ItemStyle HorizontalAlign="left" />
                                        <HeaderStyle HorizontalAlign="left" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="address" HeaderText="Address ">
                                        <ItemStyle HorizontalAlign="left" />
                                        <HeaderStyle HorizontalAlign="left" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="mobile" HeaderText="Contact  ">
                                        <ItemStyle HorizontalAlign="left" />
                                        <HeaderStyle HorizontalAlign="left" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="email" HeaderText="Email ID  ">
                                        <ItemStyle HorizontalAlign="left" />
                                        <HeaderStyle HorizontalAlign="left" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="ofc_url" HeaderText="Office url  ">
                                        <ItemStyle HorizontalAlign="left" />
                                        <HeaderStyle HorizontalAlign="left" />
                                    </asp:BoundField>
                                </Columns>
                                <EmptyDataTemplate>Action  Records Not Available</EmptyDataTemplate>
                                <PagerSettings Mode="NumericFirstLast" />
                                <PagerStyle BackColor="White" ForeColor="Black" HorizontalAlign="left" CssClass="cssPager" />
                            </asp:GridView>
                        </asp:Panel>
                    </div>
                </div>
            </div>

            <div class="panel panel-success" id="pnl_farm" runat="server" visible="false">
                <div class="panel-heading">
                    Section-2 (Preview)
                </div>
                <div class="panel-body">
                    <div class="form-horizontal">
                        <div class="form-group">
                            <div class="col-sm-3">
                                <label class="txt-bold">Department</label>
                            </div>
                            <div class="col-sm-3">
                                <asp:Label ID="lbl_department" runat="server">

                                </asp:Label>
                            </div>
                            <div class="col-sm-3">
                                <label class="txt-bold">District</label>
                            </div>
                            <div class="col-sm-3">
                                <asp:Label ID="lbl_district" runat="server">
                                </asp:Label>
                            </div>
                        </div>
                        <div class="form-group">
                            <div class="col-sm-3">
                                <label class="txt-bold">Office Level</label>
                            </div>
                            <div class="col-sm-3">
                                <asp:Label ID="Lbl_ofc_lvl" runat="server">
                                </asp:Label>
                            </div>
                            <div class="col-sm-3">
                                <label class="txt-bold">Office Category</label>
                            </div>
                            <div class="col-sm-3">
                                <asp:Label ID="lbl_ofc_cat" runat="server">
                                </asp:Label>
                            </div>
                        </div>
                        <div class="form-group">
                            <div class="col-sm-3">
                                <label class="txt-bold">Office Name</label>
                            </div>
                            <div class="col-sm-3">
                                <asp:Label ID="lbl_ofc_name" runat="server">
                                </asp:Label>
                            </div>
                            <div class="col-sm-3">
                                <label class="txt-bold">Office Address</label>
                            </div>
                            <div class="col-sm-3">

                                <asp:Label ID="lbl_ofc_address" runat="server">
                                </asp:Label>

                            </div>
                        </div>
                        <div class="form-group">
                            <div class="col-sm-3">
                                <label class="txt-bold">Contact Number</label>
                            </div>
                            <div class="col-sm-3">
                                <asp:Label ID="lbl_contact" runat="server">
                                </asp:Label>
                            </div>
                            <div class="col-sm-3">
                                <label class="txt-bold">Fax Number</label>
                            </div>
                            <div class="col-sm-3">
                                <asp:Label ID="lbl_fax_number" runat="server">
                                </asp:Label>
                            </div>
                        </div>
                        <div class="form-group">
                            <div class="col-sm-3">
                                <label class="txt-bold">Office Email Id</label>
                            </div>
                            <div class="col-sm-3">
                                <asp:Label ID="lbl_email_id" runat="server">
                                </asp:Label>
                            </div>
                            <div class="col-sm-3">
                                <label class="txt-bold">Office Website</label>
                            </div>
                            <div class="col-sm-3">
                                <asp:Label ID="lbl_website" runat="server">
                                </asp:Label>
                            </div>
                        </div>
                        <div class="form-group">
                            <asp:RadioButton runat="server" ID="rdb_Acknowledgement" Text="This Is to declare that, i have read the terms   and conditions given overleaf and information given section 1 is correct" />
                        </div>
                    </div>
                    </br>
                    </br>
                    </br>
                    </br>
                    </br>
                    </br>

                      <div class="form-group">
                          <asp:Literal ID="ltr_farm" runat="server"></asp:Literal>
                      </div>

                    </br>
                    </br>

                  
                    <div class="form-group">
                        <div class="col-sm-3"></div>
                        <div class="col-sm-3">
                            <asp:Button runat="server" ID="btn_submit" ValidationGroup="vg" Text="submit" CssClass="btn btn-primary" OnClick="btn_submit_Click" />
                        </div>
                        <div class="col-sm-3">
                            <asp:Button runat="server" ID="btn_change" ValidationGroup="vg" Text="Change" CssClass="btn btn-primary" OnClick="btn_change_Click" />
                        </div>
                        <div class="col-sm-3"></div>
                    </div>
                </div>
            </div>

        </ContentTemplate>
        <Triggers>
      <%-- <asp:AsyncPostBackTrigger ControlID="btn_preview" EventName="Click" />--%>
            <asp:PostBackTrigger  ControlID="btn_preview" />
        </Triggers>
    </asp:UpdatePanel>

</asp:Content>

