<%@ Page Title="" Language="C#" MasterPageFile="~/Instructor/Instructor.Master" AutoEventWireup="true" CodeBehind="CousreAndContent.aspx.cs" Inherits="SSP.Instructor.CousreAndContent" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../Content/GradesTable.css" rel="stylesheet" />
    <script src="../Scripts/CourseAndContent.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            FormGradeHeader($("#CourseID").val());
            GetStudents($("#CourseID").val(), $("#DoctorID").val(), $("#GroupNumber").val());
            StartListening();
        });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <form runat="server">
        <asp:HiddenField ID="CourseID" ClientIDMode="Static" runat="server" />
        <asp:HiddenField ID="GroupNumber" ClientIDMode="Static" runat="server" />
        <asp:HiddenField ID="DoctorID" ClientIDMode="Static" runat="server" />
    </form>

    <div style="padding-left: 300px;">
        <table id="GradeTable" width="80%" align="center">
            <div id="head_nav">
                <tr id="GradeTableHeader">
                    <th>ID</th>
                    <th>Name</th>
                </tr>
            </div>

        </table>
    </div>

</asp:Content>
