<%@ Page Title="" Language="C#" MasterPageFile="~/Instructor/Instructor.Master" AutoEventWireup="true" CodeBehind="PreviousCourses.aspx.cs" Inherits="SSP.Instructor.PreviousCourses" %>

<%@ Import Namespace=" System.Globalization" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../Content/TimeTableWithoutHover.css" rel="stylesheet" />
    <script src="../Scripts/CollabseInstructorMenue.js"></script>
    <script src="../Scripts/PreviousCourses.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div style="padding-left: 288px; width: 1380px;">
        <h3>Previous courses.</h3>

        <ul id="InstructorPreviousSemesters">

            <asp:Repeater runat="server" ID="PreviousSemesters">
                <ItemTemplate>

                    <li style="padding-top: 10px; padding-bottom: 10px;"><%# Eval("SemesterName") %>-<%# ((DateTime)Eval("StartD")).ToString("dd/M/yyyy", CultureInfo.InvariantCulture) %>
                        <ul>
                            <asp:Repeater runat="server" ID="CoursesOfSemester" DataSource='<%# GetCoursesOfDoctorAtGivenSemesetr( (int)Eval("Id") ) %>'>
                                <ItemTemplate>
                                    <li style="padding-top: 10px; padding-bottom: 10px;">
                                        <a href="javascript:GetStudents(<%# Eval("Id") %>)"><%# Eval("CourseName") %>-G.<%# Eval("GroupNumber") %></a>
                                    </li>

                                    <div style="display: none; width: 1200px; height: auto;" class="StudentsOfLectureOFGroup" id="Students-<%# Eval("Id") %>"></div>

                                </ItemTemplate>
                            </asp:Repeater>
                        </ul>
                    </li>
                </ItemTemplate>
            </asp:Repeater>

        </ul>

    </div>
</asp:Content>
