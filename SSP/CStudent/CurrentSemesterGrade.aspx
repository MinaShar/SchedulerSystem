<%@ Page Title="" Language="C#" MasterPageFile="~/CStudent/CStudent.Master" AutoEventWireup="true" CodeBehind="CurrentSemesterGrade.aspx.cs" Inherits="SSP.CStudent.CurrentSemesterGrade" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../Content/TimeTableWithoutHover.css" rel="stylesheet" />
    <script src="../Scripts/CurrentSemesterGrade.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            GetGrades($("#StudentIDHidden").val());
        });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <%--<h1><%=StudentID %></h1>--%>
    <form runat="server">
        <asp:HiddenField runat="server" ID="StudentIDHidden" ClientIDMode="Static" />
    </form>

    <table id="ResultsTable" width="80%" align="center" style="margin-left: -243px; height: 300px;">

            <tr>
                <th>Course</th>
                <th>Points</th>
            </tr>

        </table>

</asp:Content>
