<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Admin.Master" AutoEventWireup="true" CodeBehind="AddNewUser.aspx.cs" Inherits="SSP.Admin.AddNewUser" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="../Scripts/AddNewUserScripts.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <asp:Label runat="server" ID="ConfirmInsertion" Font-Bold="true" Font-Italic="true" ></asp:Label>

    <form id="AddNewUserForm" method="post" runat="server">
        <div class="row">
            <div class="col-md-8">
                <section id="loginForm">
                    <div class="form-horizontal">
                        <h2>Add New User.</h2>
                        <hr />
                        <div class="form-group">
                            <label class="col-md-2 control-label">Name</label>
                            <div class="col-md-10">
                                <input data-required="true" name="NewUserName" type="text" class="form-control" />
                            </div>
                        </div>
                        <div class="form-group">
                            <label class="col-md-2 control-label">Password</label>
                            <div class="col-md-10">
                                <input data-required="true" name="NewUserPassword" type="password" class="form-control" />
                            </div>
                        </div>


                        <div class="form-group">
                            <label class="col-md-2 control-label">Type</label>
                            <div class="col-md-10">
                                <div class="radio">
                                    <label>
                                        <input type="radio" value="1" id="AdminType" name="NewUserType" />Admin</label>
                                </div>
                                <div class="radio">
                                    <label>
                                        <input type="radio" value="2" id="DoctorType" name="NewUserType" />Doctor</label>
                                </div>
                                <div class="radio">
                                    <label>
                                        <input type="radio" value="3" id="StudentType" name="NewUserType" />Student</label>
                                </div>
                            </div>
                        </div>



                        <div id="RankBlock" style="display:none;" class="form-group">
                            <label class="col-md-2 control-label">Rank</label>
                            <div class="col-md-10">
                                <div class="radio">
                                    <label>
                                        <input type="radio" value="1" data-userrank="true" name="Rank" />Doctor</label>
                                </div>
                                <div class="radio">
                                    <label>
                                        <input type="radio" value="2" data-userrank="true" name="Rank" />TA</label>
                                </div>
                                <div class="radio">
                                    <label>
                                        <input type="radio" value="3" data-userrank="true" name="Rank" />Fresh Graduate</label>
                                </div>
                            </div>
                        </div>

                        <div id="DepartmentBlock" style="display:none;" class="form-group">
                            <label class="col-md-2 control-label">Department</label>
                            <div class="col-md-10">
                                <select class="form-control" name="NewUserDepartmentID">
                                    <asp:Repeater runat="server" ID="Departments">
                                        <ItemTemplate>
                                            <option value="<%# Eval("Id") %>"><%# Eval("Name") %></option>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                </select>
                            </div>
                        </div>


                        <div class="form-group">
                            <div class="col-md-offset-2 col-md-10">
                                <input type="submit" value="Add" />
                            </div>
                        </div>
                    </div>
                   <%-- <p>
                        <asp:HyperLink runat="server" ID="RegisterHyperLink" ViewStateMode="Disabled">Register as a new user</asp:HyperLink>
                    </p>--%>
                    <p>
                        <%-- Enable this once you have account confirmation enabled for password reset functionality
                    <asp:HyperLink runat="server" ID="ForgotPasswordHyperLink" ViewStateMode="Disabled">Forgot your password?</asp:HyperLink>
                        --%>
                    </p>
                </section>
            </div>

            <div class="col-md-4">
                <section id="socialLoginForm">
                </section>
            </div>
        </div>
    </form>

</asp:Content>
