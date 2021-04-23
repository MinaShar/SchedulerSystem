<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Admin.Master" AutoEventWireup="true" CodeBehind="CurrentSemesterInfo.aspx.cs" Inherits="SSP.Admin.CurrentSemesterInfo" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../Content/CheckBox.css" rel="stylesheet" />
    <script src="../Scripts/CurrentSemesterInfoHandler.js"></script>
    <script type="text/javascript">
        jQuery(document).ready(function ($) {
            $('#<%= OpenReg0.ClientID%>').change(function () {
                OpenRegistration($(this).val());
            });
            $('#<%= OpenReg1.ClientID%>').change(function () {
                OpenRegistration($(this).val());
            });

            $('#<%= IsClosed0.ClientID%>').change(function () {
                CloseSemester($(this).val());
            });
            $('#<%= IsClosed1.ClientID%>').change(function () {
                CloseSemester($(this).val());
            });
        });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <h1>Semester Info</h1>
    <hr />

    <div class="form-group">
        <label class="col-md-2 control-label">Open Regestration</label>
        <div class="col-md-10">
            <div class="example">
                <div>
                    <input runat="server" id="OpenReg1" type="radio" name="OpenReg" value="1" /><label for="radio1"><span><span></span></span>Yes</label>
                </div>
                <div>
                    <input runat="server" id="OpenReg0" type="radio" name="OpenReg" value="0" /><label for="radio2"><span><span></span></span>No</label>
                </div>
            </div>
        </div>
    </div>

    <div class="form-group">
        <label class="col-md-2 control-label">Close semester</label>
        <div class="col-md-10">
            <div class="example">
                <div>
                    <input runat="server" id="IsClosed1" type="radio" name="IsClosed" value="1" /><label for="radio1"><span><span></span></span>Yes</label>
                </div>
                <div>
                    <input runat="server" id="IsClosed0" type="radio" name="IsClosed" value="0" /><label for="radio2"><span><span></span></span>No</label>
                </div>
            </div>
        </div>
    </div>

</asp:Content>
