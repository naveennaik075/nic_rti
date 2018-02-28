<%@ Page Language="C#" AutoEventWireup="true" CodeFile="LoginOfficerDetail.aspx.cs" Inherits="LoginOfficerDetail" MasterPageFile="~/UserMaster.master" %>



<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <style type="text/css">
        .logintime {
            text-align: right;
        }

        
    </style>


    <p class="logintime">
        Last Successfull Login
        <asp:Label ID="lbl_LoginTime" runat="server" Text="Label"></asp:Label>
    </p>
    <div class="form-horizontal">
        <div class="form-group">
            <div class="col-sm-4">
                User Name:
                <asp:Label ID="lbl_UserName" runat="server" Text="Label"></asp:Label>
            </div>
            <div class="col-sm-4">
               
            </div>
            <div class="col-sm-4">
                 <asp:HyperLink ID="HL_ChangePassword" runat="server" NavigateUrl="~/ChangePassword.aspx">Change Password</asp:HyperLink>
                <%--User Type:
                <asp:Label ID="lbl_UserType" runat="server" Text="Label"></asp:Label>--%>
            </div>
        </div>
    </div>

    <br />
    <div class="col-sm-3"></div>
    <div class="panel panel-primary col-sm-6">
        <div class="panel-body">
            <div class="col-sm-2"></div>
            <div class="col-sm-8">
                Request/Appeal Status as on
                <asp:Label ID="lbl_RequestApealStatus" runat="server" Text="Date"></asp:Label>
            </div>
            <div class="col-sm-2"></div>
        </div>
    </div>
    <div class="col-sm-3"></div>
    <br />
    <br />
    <br />
    <br />
    <br />

    <div class="col-sm-2"></div>
    <div class="panel panel-primary col-sm-8">
                <div class="form-group">
                    <div class="col-sm-6">
                          <span >Requests </span>
                          <ul class="list-group">
                                <li class="list-group-item">
                                 <span class="badge"> <asp:Label ID="lbl_Request_Registered" runat="server" Text="0"></asp:Label></span>
                                Registered
                                </li> 
                               <li class="list-group-item">
                                 <span class="badge"> <asp:Label ID="lbl_Request_DisposedOf" runat="server" Text="0"></asp:Label></span>
                                Disposed of
                                </li>
                              <li class="list-group-item">
                                 <span class="badge"> <asp:Label ID="lbl_Request_Pending" runat="server" Text="0"></asp:Label></span>
                                Pending
                                </li>

                          </ul>
                    </div>
                  
                    <div class="col-sm-6">
                         <span >Appeals </span>
                           <ul class="list-group">
                                <li class="list-group-item">
                                 <span class="badge"> <asp:Label ID="lbl_Appeal_Registered" runat="server" Text="0"></asp:Label></span>
                                Registered
                                </li>
                               <li class="list-group-item">
                                 <span class="badge"> <asp:Label ID="lbl_Appeal_DisposedOf" runat="server" Text="0"></asp:Label></span>
                                Disposed of
                                </li>
                              <li class="list-group-item">
                                 <span class="badge"> <asp:Label ID="lbl_Appeal_Pending" runat="server" Text="0"></asp:Label></span>
                                Pending
                                </li>

                          </ul>
                    </div>

                </div>
                                      
    </div>
    <div class="col-sm-2"></div>
    <br />

</asp:Content>
