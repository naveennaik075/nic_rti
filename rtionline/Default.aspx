<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" %>

<%@ Register Assembly="MSCaptcha" Namespace="MSCaptcha" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">
    <style type="text/css">
        td {
            padding: 0 15px 0 15px;
        }

        .spa {
            padding-right: 10px;
        }

        .noticeboard {
            padding-top: 5px;
            padding-bottom: 5px;
            /*background-color: rgba(4, 4, 4, 0.70);*/
            /*background-color:darkblue;*/
            margin-bottom: 5px;
        }

        .pos {
            /*position:fixed;
            bottom:0;
            right:0;*/
        }
        /* Just for testing start */
        .col {
            color: #ebe346;
        }
        /* Just for testing end */
        @media screen and (max-width: 1023px) and (min-width: 768px) {
            .scr_height_top {
                height: 200px;
            }

            .scr_height_bottom {
                height: 250px;
            }

            .scr_height_side {
                height: 450px;
            }

            body {
                font-size: 12px;
            }
        }

        @media screen and (max-width: 1900px) and (min-width: 1024px) {
            .scr_height_top {
                height: 200px;
            }

            .scr_height_bottom {
                height: 230px;
            }

            .scr_height_side {
                height: 450px;
            }

            body {
                font-size: 14px;
            }
        }

        @media screen and (min-width: 1901px) {
            .scr_height_top {
                height: 275px;
            }

            .scr_height_bottom {
                height: 350px;
            }

            .scr_height_side {
                height: 625px;
            }

            body {
                font-size: 18px;
            }
        }
    </style>

    <script type="text/javascript">

        function setTarget() {
            document.forms[0].target = "_blank";
        }

        function blinker() {
            $('.blink_me').fadeOut(500);
            $('.blink_me').fadeIn(500);
        }
        setInterval(blinker, 500);
    </script>

    <section id="slider" class="slider-parallax full-screen dark slider-parallax-visible" style="background: transparent url(&quot;images/b01.jpg&quot;) no-repeat scroll center center /cover;">
        <%--<section id="slider" class="slider-parallax full-screen dark slider-parallax-visible" style="background: transparent url(&quot;images/images_test1.jpg&quot;) no-repeat scroll center center /cover;">--%>

        <div class="slider-parallax-inner" style="height: 376px; transform: translateY(0px);">

            <div class="container vertical-middle clearfix" style="position: absolute; top: 50%; width: 100%; padding-top: 0px; padding-bottom: 0px; margin-top: -90.5px; opacity: 1.23388;">
                <div class=" bottommargin">

                    <div class="container-fluid">

                        <%--<div class="col-sm-12 col-md-12 col-lg-8 nobottommargin">
                        </div>--%>
                        <div class="col-sm-8 col-md-8 col-lg-8 " style="margin-top: 100px;">
                            <div class="col-sm-12  ">
                                <%--<div class="well well-lg " style="height: 200px; overflow-y: auto;">--%>
                                <div class="well well-lg scr_height_top">
                                    <div id="description">
                                        <p>
                                            <%=Resources.Resource.RTIDescription %>
                                            <br />
                                            <br />
                                            <u><b><%=Resources.Resource.RTIDescription_P %>  </b></u>
                                        </p>

                                    </div>

                                </div>
                            </div>
                            <div class="col-sm-12">
                                <div class="well well-lg scr_height_bottom">
                                    <div id="notice-bord">
                                        <div class='col-sm-12 noticeboard pull-left'>
                                            <div class='col-sm-12'>
                                                <h3 class='text-center nobottommargin notopmargin'><%=Resources.Resource.NoticeBoard %>  </h3>
                                                <asp:Literal ID="Literal1" runat="server"></asp:Literal>
                                            </div>

                                            <asp:LinkButton ID="lnk_ShowAllNotice" runat="server" CssClass="pull-right" OnClick="lnk_Click_showAllNotice" Text="<%$Resources:Resource, ShowAllNotices %>" OnClientClick="setTarget();"></asp:LinkButton>

                                        </div>

                                    </div>

                                </div>
                            </div>


                        </div>


                        <div class="col-sm-4 col-md-4 col-lg-4" style="margin-top: 100px;">
                            <div class="well well-lg scr_height_side">
                                <div id="login-form">

                                    <h3><%=Resources.Resource.LoginToAccount %></h3>

                                    <div class="col_full">
                                        <div class="col-sm-6">
                                            <label for="login-form-username"><%=Resources.Resource.UserName %>:</label>
                                            <asp:TextBox ID="txt_usr_id" CssClass="form-control" CausesValidation="true" MaxLength="20" ValidationGroup="vg" runat="server"></asp:TextBox>
                                        </div>
                                        <div class="col-sm-6">
                                            <asp:RequiredFieldValidator ID="RFV_UserID" runat="server" ControlToValidate="txt_usr_id" CssClass="alert-danger" Display="Dynamic" ErrorMessage="<%$Resources:Resource, FieldRequired %>" ValidationGroup="vg"></asp:RequiredFieldValidator>
                                        </div>
                                    </div>

                                    <div class="col_full">
                                        <div class="col-sm-6">
                                            <label for="login-form-password"><%=Resources.Resource.Password %>:</label>
                                            <asp:TextBox ID="txt_Password" TextMode="Password" CssClass="form-control" runat="server"></asp:TextBox>
                                        </div>
                                        <div class="col-sm-6">
                                            <asp:RequiredFieldValidator ID="rfv_pass" runat="server" ControlToValidate="txt_Password" CssClass="alert-danger" Display="Dynamic" ErrorMessage="<%$Resources:Resource, FieldRequired %>" ValidationGroup="vg"></asp:RequiredFieldValidator>
                                        </div>
                                    </div>

                                    <div class="col_full">
                                        <div class="col-sm-12 ">
                                            <label for="login-form-Captch"><%=Resources.Resource.SecurityCode %> :</label>
                                        </div>
                                        <div class="col-sm-6">
                                            <asp:TextBox ID="txt_captcha" CssClass="form-control" CausesValidation="true" MaxLength="10" runat="server"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="rfv_captcha" runat="server" ValidationGroup="vg" ErrorMessage="<%$Resources:Resource, SecurityCodeRequired %>" SetFocusOnError="true" CssClass="alert-danger" Display="Dynamic" ControlToValidate="txt_captcha"></asp:RequiredFieldValidator>
                                        </div>
                                        <div class="col_half col_last">
                                            <div class="col-sm-9">
                                                <cc1:CaptchaControl ID="Captcha1" runat="server" CaptchaBackgroundNoise="Low" CaptchaLength="5"
                                                    CaptchaHeight="35" CaptchaWidth="120" CaptchaMinTimeout="5" CaptchaMaxTimeout="240"
                                                    FontColor="#D20B0C" NoiseColor="#B1B1B1" ValidationGroup="vg" />
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col_full">
                                        <div class="col-sm-6">
                                            <asp:Button CssClass="button button-3d btn-yellow nomargin" ID="login_form_submit" runat="server" ValidationGroup="vg" OnClick="login_form_submit_Click" Text="<%$Resources:Resource, Login %>"></asp:Button>

                                        </div>
                                        <div class="col-sm-6">

                                            <a href="registration.aspx" class="fright"><%=Resources.Resource.Registration %></a>
                                            <br />
                                            <a href="Forget_password.aspx" class="fright"><%=Resources.Resource.ForgotPassword %>? </a>
                                        </div>


                                    </div>
                                </div>


                            </div>
                        </div>



                    </div>

                </div>
            </div>
        </div>
    </section>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <%--<div class="container-fluid" >
        <h2 style="text-align:center">What our customers say</h2>
        <div id="myCarousel" class="carousel slide text-center" data-ride="carousel">
            <!-- Indicators -->
            <ol class="carousel-indicators">
                <li data-target="#myCarousel" data-slide-to="0" class="active"></li>
                <li data-target="#myCarousel" data-slide-to="1"></li>
                <li data-target="#myCarousel" data-slide-to="2"></li>
            </ol>

            <!-- Wrapper for slides -->
            <div class="carousel-inner" role="listbox">
                <div class="item active">
                    <h4>"This company is the best. I am so happy with the result!"<br>
                        <span style="font-style: normal;">Michael Roe, Vice President, Comment Box</span></h4>
                </div>
                <div class="item">
                    <h4>"One word... WOW!!"<br>
                        <span style="font-style: normal;">John Doe, Salesman, Rep Inc</span></h4>
                </div>
                <div class="item">
                    <h4>"Could I... BE any more happy with this company?"<br>
                        <span style="font-style: normal;">Chandler Bing, Actor, FriendsAlot</span></h4>
                </div>
            </div>

            <!-- Left and right controls -->
            <a class="left carousel-control" href="#myCarousel" role="button" data-slide="prev">
                <span class="glyphicon glyphicon-chevron-left" aria-hidden="true"></span>
                <span class="sr-only">Previous</span>
            </a>
            <a class="right carousel-control" href="#myCarousel" role="button" data-slide="next">
                <span class="glyphicon glyphicon-chevron-right" aria-hidden="true"></span>
                <span class="sr-only">Next</span>
            </a>
        </div>
    </div>--%>

    <div class="row">
        <div class="container-fullwidth">

            <asp:Literal ID="Literal_Count" runat="server"></asp:Literal>
            <%-- <div class="col_one_fourth nobottommargin center box-green">
                <div class="counter counter-large"> <span data-from="100" data-to="3000" data-refresh-interval="50" data-speed="2000">3000</span> </div>
                <h5>Total RTI Submited</h5>
            </div>
            <div class="col_one_fourth nobottommargin center box-red">
                <div class="counter counter-large"><span data-from="100" data-to="49" data-refresh-interval="50" data-speed="2000">49</span></div>
                <h5>Total Department</h5>
            </div>
            <div class="col_one_fourth nobottommargin center box-blue">
                <div class="counter counter-large"><span data-from="100" data-to="2000" data-refresh-interval="50" data-speed="2000">2000</span></div>
                <h5>Total offices</h5>
            </div>
            <div class="col_one_fourth col_last nobottommargin center box-purple">
                <div class="counter counter-large"><span data-from="100" data-to="5000" data-refresh-interval="50" data-speed="2000">5000</span></div>
                <h5>Total Employee</h5>
            </div> --%>
        </div>

    </div>
    <div>
        No. of Viewer:
        <asp:Label ID="lblCount" runat="server" ForeColor="Red" />
    </div>
</asp:Content>
