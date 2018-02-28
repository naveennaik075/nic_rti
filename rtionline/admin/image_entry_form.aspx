<%@ Page Language="C#" AutoEventWireup="true" CodeFile="image_entry_form.aspx.cs" Inherits="user_image_entry_form" MasterPageFile="~/admin_master.master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CPH_MasterLogin" runat="Server">

    <div class="col-sm-12">
        <div class="panel panel-default">

            <div class="panel-heading">
                <h3 class="panel-title"><%=Resources.Resource.NoticeBoardImageEntryForm %> </h3>
            </div>

            <div class="panel-body">
                <div class="form-horizontal">
                    <div class="form-group" >
                        <div class="col-sm-4">
                            <label><%=Resources.Resource.UploadImage %><span class="text-danger"></span> </label>
                        </div>
                        <div class="col-sm-4">
                            <asp:FileUpload ID="FileUpload7" runat="server" CssClass="form-control" />
                            <span class="text-danger">(only .jpg,.png,.gif,.jpeg format allowed Maximum Size  is 5 KB)</span>
                            <asp:Image ID="Image1" runat="server" Height="100" Width="100" />
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ValidationGroup="a"
                                ControlToValidate="FileUpload7" CssClass="form_error"
                                ErrorMessage="कृप्या फाइल अपलोड करे" ForeColor="#FF3300" Display="Dynamic">
                            </asp:RequiredFieldValidator>
                        </div>
                    </div>
                    <div class="form-group">
                        <div class="col-sm-12 text-center">
                            <asp:Button ID="Btnsubmit" runat="server" CssClass="button button-large button-dark button-rounded" ValidationGroup="a" Text="<%$Resources:Resource, Submit %>" OnClick="Btnsubmit_Click" />
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>

</asp:Content>



