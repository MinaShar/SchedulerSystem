<%@ Page Title="" Language="C#" MasterPageFile="~/CStudent/CStudent.Master" AutoEventWireup="true" CodeBehind="AllStudentGrades.aspx.cs" Inherits="SSP.CStudent.AllStudentGrades" %>
<%@ Import Namespace=" System.Globalization" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../Content/TimeTableWithoutHover.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <asp:Repeater runat="server" ID="StudentSemesters">

        <ItemTemplate>
            <div>
                <h4 style="color:red;"><%# Eval("SemesterName") %>-<%# ((DateTime)Eval("StartD")).ToString("dd/M/yyyy", CultureInfo.InvariantCulture) %></h4>
            </div>
            <asp:Repeater runat="server" ID="EachSemesterGrade" DataSource='<%# GetStudentGradesInSemester( (int)Eval("Id") ) %>'>
                <ItemTemplate>
                    <div>
                        <label style="margin-left:100px;font-style:italic;font-weight:bolder;"><%# Eval("CourseName") %></label><label style="margin-left:300px;"><%# (int)Eval("Grade") == -1 ? "UnClosed Course" : Eval("Grade") %></label>
                    </div>
                </ItemTemplate>
            </asp:Repeater>

            <div style="margin-left:80px;">
                Total points : <%# GetStudentGradeINSemester( (int)Eval("Id") ) == -1 ? 0 : GetStudentGradeINSemester( (int)Eval("Id") ) %>
            </div>

        </ItemTemplate>
    </asp:Repeater>

</asp:Content>
