<%@ Page Title="" Language="C#" MasterPageFile="~/Instructor/Instructor.Master" AutoEventWireup="true" CodeBehind="TimeTable.aspx.cs" Inherits="SSP.Instructor.TimeTable" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <link href="../Content/TimeTable.css" rel="stylesheet" />

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">


    <div style="padding-left: 288px; padding-top: 67px; width: 1380px;">
        <h2>Time table</h2>
        <hr />
    </div>
    <table style="height: 438.96px; padding-left: 288px; padding-top: 15px; text-align: center; width: 1666.62px;">

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

            <td runat="server" id="P1_1" class="period" data-day="1" data-period="1"></td>
            <td runat="server" id="P2_1" class="period" data-day="2" data-period="1"></td>
            <td runat="server" id="P3_1" class="period" data-day="3" data-period="1"></td>
            <td runat="server" id="P4_1" class="period" data-day="4" data-period="1"></td>
            <td runat="server" id="P5_1" class="period" data-day="5" data-period="1"></td>
            <td runat="server" id="P6_1" class="period" data-day="6" data-period="1"></td>

        </tr>

        <tr>
            <th>10:10 - 11:40</td>
        
                <td runat="server" id="P1_2" class="period" data-day="1" data-period="2"></td>
                <td runat="server" id="P2_2" class="period" data-day="2" data-period="2"></td>
                <td runat="server" id="P3_2" class="period" data-day="3" data-period="2"></td>
                <td runat="server" id="P4_2" class="period" data-day="4" data-period="2"></td>
                <td runat="server" id="P5_2" class="period" data-day="5" data-period="2"></td>
                <td runat="server" id="P6_2" class="period" data-day="6" data-period="2"></td>
        </tr>

        <tr>
            <th>11:50 - 01:30</td>
        
                <td runat="server" id="P1_3" class="period" data-day="1" data-period="3"></td>
                <td runat="server" id="P2_3" class="period" data-day="2" data-period="3"></td>
                <td runat="server" id="P3_3" class="period" data-day="3" data-period="3"></td>
                <td runat="server" id="P4_3" class="period" data-day="4" data-period="3"></td>
                <td runat="server" id="P5_3" class="period" data-day="5" data-period="3"></td>
                <td runat="server" id="P6_3" class="period" data-day="6" data-period="3"></td>
        </tr>

        <tr>
            <th>01:40 - 03:10</td>
        
                <td runat="server" id="P1_4" class="period" data-day="1" data-period="4"></td>
                <td runat="server" id="P2_4" class="period" data-day="2" data-period="4"></td>
                <td runat="server" id="P3_4" class="period" data-day="3" data-period="4"></td>
                <td runat="server" id="P4_4" class="period" data-day="4" data-period="4"></td>
                <td runat="server" id="P5_4" class="period" data-day="5" data-period="4"></td>
                <td runat="server" id="P6_4" class="period" data-day="6" data-period="4"></td>
        </tr>

        <tr>
            <th>03:20 - 05:00</td>
        
                <td runat="server" id="P1_5" class="period" data-day="1" data-period="5"></td>
                <td runat="server" id="P2_5" class="period" data-day="2" data-period="5"></td>
                <td runat="server" id="P3_5" class="period" data-day="3" data-period="5"></td>
                <td runat="server" id="P4_5" class="period" data-day="4" data-period="5"></td>
                <td runat="server" id="P5_5" class="period" data-day="5" data-period="5"></td>
                <td runat="server" id="P6_5" class="period" data-day="6" data-period="5"></td>
        </tr>
    </table>

</asp:Content>
