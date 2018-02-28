<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Notice_Pending_For_Approval.aspx.cs" Inherits="admin_Pending_For_Approval" MasterPageFile="~/admin_master.master" EnableEventValidation = "false" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CPH_MasterLogin" runat="Server">
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
    </style>
    <script type="text/javascript">
        function TestCheckBox() {
            //alert("Hi1");
            var TargetChildControl = "chk_approve";
            var Inputs;
            Inputs = document.getElementById('<%= this.GridView1.ClientID %>').getElementsByTagName("input");
            for (var n = 0; n < Inputs.length; ++n)
                if (Inputs[n].type == 'checkbox' &&
                   Inputs[n].id.indexOf(TargetChildControl, 0) >= 0 &&
                   Inputs[n].checked)
                    return true;

            alert('Select at least one checkbox!');
            return false;
        }
</script>

     <div class="col-sm-12">
        <div class="panel panel-default">
            <div class="panel-heading">
                <h3 class="panel-title"><%=Resources.Resource.PendingList %></h3>
            </div>
            <div class="panel-body">                
                <div class="form-group">
                        <%--<div class="text-right">
                            <asp:ImageButton ID="exp_excel" runat="server" AlternateText="Export to Excel" ImageUrl="../images/Features-export-300x240.gif" OnClick="exp_excel_Click" CausesValidation="false" />
                        </div>--%>

                        <div class="text-left">
                            <asp:Label ID="lblTotalBoilers" runat="server" Font-Bold="true" Style="color: blue; text-align: left;"></asp:Label>
                        </div>
                        
                        <div class="table-responsive">
                            <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" CssClass="table table-condensed table-hover" AlternatingRowStyle-CssClass="alt" EmptyDataText="No Record Found."  DataKeyNames="Notice_Id,file_id,file_type"
                            AllowPaging="True" PageSize="10" GridLines="Horizontal" OnPageIndexChanging="GridView1_PageIndexChanging" OnRowDataBound="GridView1_RowDataBound" >
                            <Columns>
                            <asp:TemplateField HeaderText="<%$Resources:Resource, SNo %> ">
                                <ItemTemplate>
                                    <%#Container.DataItemIndex+1%>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="left" Width="3%" />
                                <HeaderStyle HorizontalAlign="Left" Width="3%" />
                            </asp:TemplateField>
                            <asp:HyperLinkField DataNavigateUrlFields="file_id" DataTextField="Header" HeaderText="<%$Resources:Resource, Subject %> ">
                                <HeaderStyle HorizontalAlign="Left" Width="30%" />
                                <ItemStyle HorizontalAlign="Left" Width="30%" />
                            </asp:HyperLinkField>


                            <asp:BoundField DataField="Permanent" HeaderText="<%$Resources:Resource, Permanent %> " ReadOnly="True" SortExpression="Permanent">
                                <HeaderStyle HorizontalAlign="Left" />
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:BoundField>

                            <asp:BoundField DataField="Publish_Date" HeaderText="<%$Resources:Resource, PublishDate %>" ReadOnly="True" SortExpression="Publish_Date">
                                <HeaderStyle HorizontalAlign="Left" />
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:BoundField>

                            <asp:BoundField DataField="Validity_From" HeaderText="<%$Resources:Resource, From %>" ReadOnly="True" SortExpression="Validity_From">
                                <HeaderStyle HorizontalAlign="Left" />
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:BoundField>

                            <asp:BoundField DataField="Validity_To" HeaderText="<%$Resources:Resource, To %>" ReadOnly="True" SortExpression="Validity_To">
                                <HeaderStyle HorizontalAlign="Left" />
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:BoundField>

                            <asp:BoundField DataField="Active" HeaderText="<%$Resources:Resource, Active %>" ReadOnly="True" SortExpression="Active">
                                <HeaderStyle HorizontalAlign="Left" />
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:BoundField>

                            <asp:BoundField DataField="Hyperlink" HeaderText="<%$Resources:Resource, Hyperlink %>" ReadOnly="True" SortExpression="Hyperlink">
                                <HeaderStyle HorizontalAlign="Left" />
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:BoundField>

                            <asp:BoundField DataField="File_Type" HeaderText="<%$Resources:Resource, HyperlinkType %>" ReadOnly="True" SortExpression="File_Type">
                                <HeaderStyle HorizontalAlign="Left" />
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:BoundField>

                            <asp:BoundField DataField="Url" HeaderText="<%$Resources:Resource, URL %>" ReadOnly="True" SortExpression="Url">

                                <HeaderStyle HorizontalAlign="Left" />
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:BoundField>

                            <asp:TemplateField HeaderText="<%$Resources:Resource, Delete %>">
                                <ItemTemplate>
                                    <asp:LinkButton ID="lnkDelete" OnClick="lnkDelete_Click" CssClass="HyperLink" OnClientClick="return confirm('Are you sure you want delete');" runat="server"><%=Resources.Resource.Delete %></asp:LinkButton>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>


                            <asp:TemplateField HeaderText="<%$Resources:Resource, All %>">
                                <HeaderTemplate>
                                    <asp:CheckBox ID="chkAllSelect" Text="All" runat="server" AutoPostBack="true" OnCheckedChanged="chkAllSelect_CheckedChanged" />

                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:CheckBox ID="chk_approve" runat="server" Text="Approve" />
                                </ItemTemplate>
                            </asp:TemplateField>

                            </Columns>
                            </asp:GridView>
                        </div>
                        
                        <div class="form-group">
                            <div class="text-center">
                                <asp:Button ID="btnapprove"  CssClass="button button-large button-dark button-rounded" runat="server" Text="<%$Resources:Resource, Approve %>" OnClick="btnapprove_Click" OnClientClick="return TestCheckBox();" />
                            </div>
                        </div>                        
                    </div>
                <%-- surjeet --%>
            </div>
        </div>
     </div>






  


</asp:Content>

