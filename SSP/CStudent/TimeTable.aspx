<%@ Page Title="" Language="C#" MasterPageFile="~/CStudent/CStudent.Master" AutoEventWireup="true" CodeBehind="TimeTable.aspx.cs" Inherits="SSP.CStudent.TimeTable" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../Content/TimeTableWithoutHover.css" rel="stylesheet" />
    <script src="../Scripts/StudentTimeTable.js"></script>
    <script type="text/javascript">
       
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <form runat="server"> 
        <asp:HiddenField ID="CurrentStudentID" ClientIDMode="Static" runat="server" />
    </form>

    <table width="80%" align="center" style="margin-left: -243px; height: 300px;">


        <tr>
            <th>Time</th>
            <th>Saturday</th>
            <th>sunday</th>
            <th>Monday</th>
            <th>Tuesday</th>
            <th>Wednesday</th>
            <th>Thrusday</th>
        </tr>

        <tr>
            <th>08:30 - 10:00</th>

            <td class="period" data-day="1" data-period="1"></td>
            <td class="period" data-day="2" data-period="1"></td>
            <td class="period" data-day="3" data-period="1"></td>
            <td class="period" data-day="4" data-period="1"></td>
            <td class="period" data-day="5" data-period="1"></td>
            <td class="period" data-day="6" data-period="1"></td>

        </tr>

        <tr>
            <th>10:10 - 11:40</th>

            <td class="period" data-day="1" data-period="2"></td>
            <td class="period" data-day="2" data-period="2"></td>
            <td class="period" data-day="3" data-period="2"></td>
            <td class="period" data-day="4" data-period="2"></td>
            <td class="period" data-day="5" data-period="2"></td>
            <td class="period" data-day="6" data-period="2"></td>
        </tr>

        <tr>
            <th>11:50 - 01:30</th>

            <td class="period" data-day="1" data-period="3"></td>
            <td class="period" data-day="2" data-period="3"></td>
            <td class="period" data-day="3" data-period="3"></td>
            <td class="period" data-day="4" data-period="3"></td>
            <td class="period" data-day="5" data-period="3"></td>
            <td class="period" data-day="6" data-period="3"></td>
        </tr>

        <tr>
            <th>01:40 - 03:10</th>

            <td class="period" data-day="1" data-period="4"></td>
            <td class="period" data-day="2" data-period="4"></td>
            <td class="period" data-day="3" data-period="4"></td>
            <td class="period" data-day="4" data-period="4"></td>
            <td class="period" data-day="5" data-period="4"></td>
            <td class="period" data-day="6" data-period="4"></td>
        </tr>

        <tr>
            <th>03:20 - 05:00</th>

            <td class="period" data-day="1" data-period="5"></td>
            <td class="period" data-day="2" data-period="5"></td>
            <td class="period" data-day="3" data-period="5"></td>
            <td class="period" data-day="4" data-period="5"></td>
            <td class="period" data-day="5" data-period="5"></td>
            <td class="period" data-day="6" data-period="5"></td>
        </tr>

    </table>

</asp:Content>
