<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Admin.Master" AutoEventWireup="true" CodeBehind="AddNewSemester.aspx.cs" Inherits="SSP.Admin.AddNewSemester" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../Content/jquery-ui.css" rel="stylesheet" />
    <link href="../Content/jquery-ui.theme.min.css" rel="stylesheet" />
    <script src="../Scripts/AddNewSemesterScript.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <asp:Label runat="server" Font-Bold="true" Font-Italic="true" ID="ConfirmInsertion"></asp:Label>

    <form id="AddNewSemesterForm" method="post" runat="server">
        <div class="row">
            <div class="col-md-8">
                <section id="loginForm">
                    <div class="form-horizontal">
                        <h2>Create new semester</h2>
                        <hr />

                        <div class="form-group">
                            <label class="col-md-2 control-label">Name</label>
                            <div class="col-md-10">
                                <div class="radio">
                                    <label>
                                        <input type="radio" value="Spring" name="NewSemesterName" checked="checked" />Spring</label>
                                </div>
                                <div class="radio">
                                    <label>
                                        <input type="radio" value="Automn" name="NewSemesterName" />Automn</label>
                                </div>
                                <div class="radio">
                                    <label>
                                        <input type="radio" value="Summer" name="NewSemesterName" />Summer</label>
                                </div>
                            </div>
                        </div>

                        <div class="form-group">
                            <label class="col-md-2 control-label">Start date</label>
                            <div class="col-md-10">
                                <input data-required="true" name="StartDate" type="text" class="form-control Date" />
                            </div>
                        </div>

                        <div class="form-group">
                            <label class="col-md-2 control-label">End date</label>
                            <div class="col-md-10">
                                <input data-required="true" name="EndDate" type="text" class="form-control Date" />
                            </div>
                        </div>

                        <div class="form-group">
                            <div class="col-md-2"></div>
                            <div class="col-md-10">
                                <label style="font: 900; font-style: italic">
                                    Note : by adding new semester the last semester will be automatically deactivated and all the actions taken after that will be on the new semester Only.
                                </label>
                            </div>
                        </div>

                        <div class="form-group">
                            <div class="col-md-offset-2 col-md-10">
                                <input type="submit" value="Add" />
                            </div>
                        </div>

                    </div>



                </section>
            </div>
        </div>
    </form>

</asp:Content>
