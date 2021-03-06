﻿<%@ Page Title="" Language="C#" MasterPageFile="~/UserMaster.master" AutoEventWireup="true" CodeFile="PayFees.aspx.cs" Inherits="PayFees" %>

<%--<%@ Register Src="~/Control/Society_PageStatus.ascx" TagPrefix="uc1" TagName="PageStatus" %>--%>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <style type="text/css">
        .form_error {
            font: arial black,arial,verdana,helvetica !important;
            color: red !important;
        }

        .button {
            font: bold 15px arial black,arial,verdana,helvetica !important;
            width: 80px;
            height: 35px;
        }

        .button1 {
            font: bold 15px arial black,arial,verdana,helvetica !important;
            width: 200px;
            height: 35px;
        }

        .lol {
            margin-left: 130px;
        }

        .RadioButtonWidth label {
            margin-right: 40px;
            margin-left: 10px;
        }

        .button1 {
            font: bold 15px arial black,arial,verdana,helvetica !important;
            width: 200px;
            height: 35px;
        }

        .bold {
            font-weight: bolder;
            font-size: medium;
        }

        .bold1 {
            font-weight: bolder;
            font-size: large;
        }

        .dd_chk_select {
            height: 30px !important;
            text-align: center;
            border-radius: 5px;
        }

        textarea {
            resize: none;
        }

        ul {
            list-style-type: circle;
        }

            ul.columns > li {
                display: inline-block;
                padding-right: 1cm;
                margin-left: 20px;
            }

                ul.columns > li:before {
                    content: "";
                    display: list-item;
                    position: absolute;
                }

        .lef {
            margin-left: -30px;
            padding-right: 20px;
        }
    </style>

    <asp:UpdatePanel ID="UpdatePanel3" runat="server">
        <ContentTemplate>
            <%--<div class="col-sm-12">--%>
            <div class="panel panel-default">
                <%--<uc1:pagestatus runat="server" id="PageStatus" />--%>
            </div>
            <div class="panel-body col-sm-12" style="margin-top: -20px;">
                <div class="panel panel-default">
                    <div class="panel-heading ">
                        <h5 class="panel-title"><b>
                            <asp:Label ID="Ekai_name" CssClass="bold1" Text="<%$Resources:Resource, eChallanDetails %> " runat="server"></asp:Label>
                        </b></h5>
                    </div>

                    <div class="panel-body" style="background-color: #f9f8f8;">
                        <div id="form2" class="form-horizontal ls_form ls_form_horizontal">
                            <ul class="columns">
                                <li><a class="openPage">
                                    <asp:Label ID="Label5" CssClass="col-sm-12 form_error" Text="<%$Resources:Resource, MandatoryField %> " runat="server"></asp:Label></a></li>
                            </ul>
                            <hr />
                            <div class="form-group">
                                <div class="col-sm-12">
                                    <asp:Label ID="feesname" CssClass="col-sm-3 bold1" runat="server"></asp:Label>
                                    <asp:Label ID="feeamount" CssClass="col-sm-3 bold1" runat="server"></asp:Label>

                                </div>
                            </div>
                            <hr />
                            <div class="form-group">
                                <div class="col-sm-6">
                                    <span class="form_error">*</span>

                                    <asp:Label ID="trf_no" CssClass="col-sm-4" Text="Tr Ref No." runat="server"></asp:Label>

                                    <div class="col-sm-7">

                                        <asp:TextBox ID="txt_Ref_No" Placeholder="Enter Tr. Ref. No." CssClass="form-control" runat="server"></asp:TextBox>
                                        <asp:RequiredFieldValidator ValidationGroup="a" ID="RequiredFieldValidator1" ControlToValidate="txt_Ref_No" CssClass="form_error" runat="server" ErrorMessage="Required"></asp:RequiredFieldValidator>

                                    </div>
                                </div>

                                <div class="col-sm-6">

                                    <div class="col-sm-3">
                                        <asp:Button ID="Button1" runat="server" ValidationGroup="a" CssClass="button1" OnClick="Button1_Click" Text="<%$Resources:Resource, GetData %> " />

                                    </div>
                                </div>
                            </div>

                            <div class="form-group">
                                <div class="col-sm-6">
                                    <span class="form_error">*</span>
                                    <asp:Label ID="head_office" CssClass="col-sm-4" Text="<%$Resources:Resource, Amount %> " runat="server"> </asp:Label>
                                    <div class="col-sm-7">
                                        <asp:TextBox ID="txtamount" runat="server" CssClass="form-control" placeholder="Enter Amount" MaxLength="20" ValidationGroup="g"></asp:TextBox>

                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtamount" Display="Dynamic"
                                            ErrorMessage="Required" ForeColor="#FF3300" Font-Size="Large" Font-Bold="true" SetFocusOnError="true" ValidationGroup="g"></asp:RequiredFieldValidator>
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtamount" ErrorMessage="Must be greate than 0" SetFocusOnError="true" ValidationGroup="g" Display="Dynamic" ValidationExpression="^[1-9][0-9]*$" ForeColor="#FF3300"></asp:RegularExpressionValidator>
                                    </div>
                                </div>

                            </div>

                            <div class="form-group" style="padding-top: 5px;">
                                <div class="col-sm-6">
                                    <asp:Label ID="Label1" CssClass="col-sm-4" Text="<%$Resources:Resource, Major_head %>  " runat="server"></asp:Label>
                                    <div class="col-sm-7">
                                        <asp:TextBox ID="txt_major_head" CssClass="form-control" runat="server" MaxLength="20"></asp:TextBox>
                                    </div>


                                </div>
                                <div class="col-sm-6">
                                    <asp:Label ID="Label3" CssClass="col-sm-4" Text="<%$Resources:Resource, Sub_Major %>" runat="server"></asp:Label>
                                    <div class="col-sm-7">
                                        <asp:TextBox ID="txt_sub_major_head" CssClass="form-control" runat="server" MaxLength="20"></asp:TextBox>
                                    </div>


                                </div>
                            </div>

                            <div class="form-group" style="padding-top: 5px;">
                                <div class="col-sm-6">
                                    <asp:Label ID="Label2" CssClass="col-sm-4" Text="<%$Resources:Resource, Minor_head %> " runat="server"></asp:Label>
                                    <div class="col-sm-7">
                                        <asp:TextBox ID="txt_minor_head" CssClass="form-control" runat="server" MaxLength="20"></asp:TextBox>
                                    </div>


                                </div>
                                <div class="col-sm-6">
                                    <asp:Label ID="Label4" CssClass="col-sm-4" Text="<%$Resources:Resource, Sub_head %>  " runat="server"></asp:Label>
                                    <div class="col-sm-7">
                                        <asp:TextBox ID="txt_sub_head" CssClass="form-control" runat="server" MaxLength="20"></asp:TextBox>
                                    </div>


                                </div>
                            </div>



                            <div class="form-group" style="padding-top: 5px;">
                                <div class="col-sm-6">
                                    <asp:Label ID="Label6" CssClass="col-sm-4" Text="<%$Resources:Resource, purpose %>  " runat="server"></asp:Label>
                                    <div class="col-sm-7">
                                        <asp:TextBox ID="txt_purpose" TextMode="MultiLine" CssClass="form-control" runat="server" MaxLength="20"></asp:TextBox>
                                    </div>


                                </div>
                                <div class="col-sm-6">
                                    <asp:Label ID="Label7" CssClass="col-sm-4" Text="<%$Resources:Resource, Tin_cin %>" runat="server"></asp:Label>
                                    <div class="col-sm-7">
                                        <asp:TextBox ID="txt_tin_cin" CssClass="form-control" runat="server" MaxLength="20"></asp:TextBox>
                                    </div>


                                </div>
                            </div>


                            <div class="form-group" style="padding-top: 5px;">
                                <div class="col-sm-6">
                                    <asp:Label ID="Label8" CssClass="col-sm-4" Text="<%$Resources:Resource, e_challan_date %>" runat="server"></asp:Label>
                                    <div class="col-sm-7">
                                        <asp:TextBox ID="txt_ent_date" CssClass="form-control" runat="server" MaxLength="20"></asp:TextBox>
                                    </div>


                                </div>
                                <div class="col-sm-6">
                                    <asp:Label ID="Label9" CssClass="col-sm-4" Text="<%$Resources:Resource, Account_date %>" runat="server"></asp:Label>
                                    <div class="col-sm-7">
                                        <asp:TextBox ID="txt_acc_date" CssClass="form-control" runat="server" MaxLength="20"></asp:TextBox>
                                    </div>


                                </div>

                            </div>

                            <%--<div class="form-group" style="padding-top: 5px;">--%>

                            <div class="form-group">
                                <div class="col-sm-6">
                                    <%--<asp:Label ID="Label11" CssClass="col-sm-4" Text="office" runat="server"></asp:Label>
                                    <div class="col-sm-7">
                                        <asp:TextBox ID="tb_ofcName" CssClass="form-control" runat="server" MaxLength="20"></asp:TextBox>
                                    </div>--%>
                                </div>
                                <div class="col-sm-3">
                                    <%--<asp:Button ID="Clear" runat="server" ValidationGroup="g" CssClass="button1" OnClientClick="this.form.reset();return false" Text="Clear" />--%>
                                </div>
                                <div class="col-sm-3">
                                    <asp:Button ID="Button2" runat="server" ValidationGroup="g" Visible="false" CssClass="button1" OnClick="Button2_Click" Text="<%$Resources:Resource, Save %>" />
                                </div>
                            </div>
                            <div class="clearfix"></div>

                            <div class="form-group">
                                <asp:GridView ID="GridView1" DataKeyNames="serial_no,rti_provisional_no" runat="server" AutoGenerateColumns="False" OnRowDeleting="GridView1_RowDeleting" CssClass=" col-sm-12 table Grid">
                                    <Columns>
                                        <asp:BoundField DataField="tr_ref_no" SortExpression="tr_ref_no" HeaderText="Ref Number" />
                                        <asp:BoundField DataField="amount" SortExpression="amount" HeaderText="Amount" />
                                        <asp:BoundField DataField="tin_cin" SortExpression="tin_cin" HeaderText="Tin Cin" />
                                        <asp:BoundField DataField="major_head" SortExpression="major_head" HeaderText=" Major Head" />
                                        <asp:BoundField DataField="sub_major_head" SortExpression="sub_major_head" HeaderText="Sub Major Head" />
                                        <asp:BoundField DataField="minor_head" SortExpression="minor_head" HeaderText="Minor Head" />
                                        <asp:BoundField DataField="sub_head" SortExpression="sub_head" HeaderText="Sub Head" />
                                        <asp:BoundField DataField="purpose" SortExpression="purpose" HeaderText="Purpose" />
                                        <asp:BoundField DataField="serial_no" SortExpression="serial_no" HeaderText="serial_no" Visible="false" />
                                        <asp:TemplateField ShowHeader="False">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="LinkButton1" runat="server" CausesValidation="False" CommandName="Delete"
                                                    Text="Delete" OnClientClick="return confirm('Are you sure you want to delete?'); "></asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>

                            </div>
                            <div class="form-group"> 
                                <div class="col-sm-6"> </div>
                                <div class="col-sm-3">
                                    <asp:Button ID="btn_close" runat="server" visible="false" CssClass="button1" OnClick="btn_close_Click" Text="<%$Resources:Resource, Proceed %>" />
                                </div>

                            </div>
                        </div>
                    </div>
                </div>
            </div>

            <%--  <div class="panel-body col-sm-4" style="margin-top: -20px;">
                    <div class="panel panel-primary">
                        <div class="panel-heading ">
                            <h5 class="panel-title"><b>
                                <asp:Label ID="Label37" Text="<%$Resources:Strings, Guidelines4  %>" runat="server"></asp:Label>

                            </b>
                            </h5>
                        </div>
                        <div class="panel-body" style="background-color: #f9f8f8;">
                            <div class="form-group">
                                <asp:Label runat="server" ID="loo" CssClass="bold" Text="<%$Resources:Strings, tr_ref_no %>"></asp:Label>
                                <asp:Label runat="server" ID="Label10" Text="<%$Resources:Strings, tr_ref_no_desc %>"></asp:Label>

                            </div>
                            <br />
                        </div>

                    </div>
                </div>--%>

            <%--</div>--%>
        </ContentTemplate>
    </asp:UpdatePanel>


</asp:Content>

