<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/UserMaster.master" CodeFile="RTI_Request_Info.aspx.cs" Inherits="RTI_Request_Info" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
 <style type="text/css">
/*ol li {
  list-style-type: decimal;
  display: list-item;
  }*/
</style>

    <div class="panel panel-default">
        <div class="panel-heading">
            <h5 class="panel-title">
                <b>
                    <asp:Label ID="Label3" runat="server" Text="<%$Resources:Resource, RTIRequestInformation %>"></asp:Label>
                </b>
            </h5>

        </div>

        <div class="panel-body">
            <div class="form-horizontal">
                <div class="form-group">

                    <ol class="list-group">
                        <li class="list-group-item"><%=Resources.Resource.RTI_Request_Info_1 %>
                        </li>
                        <li class="list-group-item"> <%=Resources.Resource.RTI_Request_Info_2 %>
                        </li>
                        <li class="list-group-item"> <%=Resources.Resource.RTI_Request_Info_3 %>
                            
                        </li>
                        <li class="list-group-item"> <%=Resources.Resource.RTI_Request_Info_4 %>
                            
                        </li>
                        <li class="list-group-item"> <%=Resources.Resource.RTI_Request_Info_5 %>
                            
                        </li>
                        <li class="list-group-item"> <%=Resources.Resource.RTI_Request_Info_6 %>
                          
                        </li>
                        <li class="list-group-item"> <%=Resources.Resource.RTI_Request_Info_7 %>
                            
                        </li>
                        <li class="list-group-item"> <%=Resources.Resource.RTI_Request_Info_8 %>
                            
                        </li>
                        <li class="list-group-item"> <%=Resources.Resource.RTI_Request_Info_9 %>
                            
                        </li>
                        <li class="list-group-item"> <%=Resources.Resource.RTI_Request_Info_10 %>
                            
                        </li>
                        <li class="list-group-item"> <%=Resources.Resource.RTI_Request_Info_11 %>
                            
                        </li>
                       
                    </ol>
                    <div class="col-sm-4">
                        <asp:Button ID="btn_Submit" runat="server" Text="<%$Resources:Resource, ContinueApplyforRTI %>" align="center" ValidationGroup="vg" OnClick="btn_Submit_Click1" />
                    </div>

                </div>
            </div>
        </div>


    </div>
</asp:Content>
