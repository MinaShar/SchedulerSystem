<%@ Page Title="" Language="C#" MasterPageFile="~/CStudent/CStudent.Master" AutoEventWireup="true" CodeBehind="Register.aspx.cs" Inherits="SSP.CStudent.Register" %>

<%@ Import Namespace="Models" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="../Scripts/OpenDifferentTermsForStudentRegister.js"></script>
    <link href="../Content/Tabs.less" rel="stylesheet" />
    <link href="../Content/TableToPrintTermPeriodsForStudentregistration.css" rel="stylesheet" />
    <script src="../Scripts/Register.js"></script>
    <link href="../Content/TimeTableWithoutHover.css" rel="stylesheet" />
    <script src="../Scripts/jquery.signalR-2.2.2-preview1.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            var CurrentStudentID = document.getElementById('CurrentStudentIDTextBox').value;
            GetStudentTimeTable(CurrentStudentID);
        });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <form runat="server">
        <asp:HiddenField runat="server" ClientIDMode="Static" ID="CurrentStudentIDTextBox" />
        <%--<asp:TextBox runat="server" ClientIDMode="Static" ID="CurrentStudentIDTextBox"></asp:TextBox>--%>
    </form>


    <h2>Register.</h2>

    <div class="tab" style="display: inline-block">
        <a href="javascript:void(0)" class="tablinks" onclick="openCity(event, '1st_term')">1st term</a>
        <a href="javascript:void(0)" class="tablinks" onclick="openCity(event, '2nd_term')">2nd term</a>
        <a href="javascript:void(0)" class="tablinks" onclick="openCity(event, '3rd_term')">3rd term</a>
        <a href="javascript:void(0)" class="tablinks" onclick="openCity(event, '4th_term')">4th term</a>
        <a href="javascript:void(0)" class="tablinks" onclick="openCity(event, '5th_term')">5th term</a>
        <a href="javascript:void(0)" class="tablinks" onclick="openCity(event, '6th_term')">6th term</a>
        <a href="javascript:void(0)" class="tablinks" onclick="openCity(event, '7th_term')">7th term</a>
        <a href="javascript:void(0)" class="tablinks" onclick="openCity(event, '8th_term')">8th term</a>
        <a href="javascript:void(0)" class="tablinks" onclick="openCity(event, '9th_term')">9th term</a>
        <a href="javascript:void(0)" class="tablinks" onclick="openCity(event, '10th_term')">10th term</a>
    </div>


    <div id="AllCousresOfTerm" class="AllCousresOfTerm">



        <div id="1st_term" class="tabcontent">

            <table class="CoursesOfTerm">
                <tr>
                    <th>Course Name</th>
                    <th>Credit Hours</th>
                    <th>Periods</th>
                </tr>

                <asp:Repeater runat="server" ID="AllCoursesTerm1" ClientIDMode="Static">
                    <ItemTemplate>

                        <tr>
                            <td><%# Eval("CourseName") %></td>
                            <td><%# Eval("CreditHours") %></td>
                            <td>
                                <table class="CoursesOfTerm">
                                    <asp:Repeater runat="server" DataSource='<%# GetAllPeriodsForThisCourseSemester( (int)Eval("Id") ) %>'>
                                        <ItemTemplate>
                                            <tr>
                                                <td>G. <%# (int)Eval("GroupNumber") %></td>
                                                <td><%# GetSectionNumber((int)Eval("SectionNumber")) %></td>
                                                <td><%# GetPeriodType((int)Eval("PeriodType")) %></td>
                                                <td>PeriodDay: <%# (int)Eval("PeriodDay") %></td>
                                                <td>PeriodNumber: <%# (int)Eval("PeriodNumber") %></td>
                                                <td>Place: <%# (string)Eval("Place") %></td>
                                                <td>
                                                    <table class="CoursesOfTerm">
                                                        <asp:Repeater runat="server" DataSource='<%# GetDoctorsOfPeriod((int)Eval("Id")) %>'>
                                                            <ItemTemplate>
                                                                <tr>
                                                                    <td>DR. <%# (string)Eval("Name") %></td>
                                                                </tr>
                                                            </ItemTemplate>
                                                        </asp:Repeater>
                                                    </table>
                                                </td>
                                                <td>Capacity:<label id="Capacity-<%# (int)Eval("Id") %>"><%# GetTheRemainingCapacityForPeriod((int)Eval("Id")) %></label></td>
                                                <td id="Handler-<%# (int)Eval("Id") %>">
                                                    <%# GetTheProberForm((int)Eval("Id")) %>
                                                </td>
                                            </tr>
                                        </ItemTemplate>

                                    </asp:Repeater>

                                </table>
                            </td>
                        </tr>

                    </ItemTemplate>
                </asp:Repeater>



            </table>

        </div>

        <div id="2nd_term" class="tabcontent">

            <table class="CoursesOfTerm">
                <tr>
                    <th>Course Name</th>
                    <th>Credit Hours</th>
                    <th>Periods</th>
                </tr>

                <asp:Repeater runat="server" ID="AllCoursesTerm2" ClientIDMode="Static">
                    <ItemTemplate>

                        <tr>
                            <td><%# Eval("CourseName") %></td>
                            <td><%# Eval("CreditHours") %></td>
                            <td>
                                <table class="CoursesOfTerm">

                                    <asp:Repeater runat="server" DataSource='<%# GetAllPeriodsForThisCourseSemester( (int)Eval("Id") ) %>'>
                                        <ItemTemplate>
                                            <tr>
                                                <td><%# (int)Eval("GroupNumber") %></td>
                                                <td><%# GetPeriodType((int)Eval("PeriodType")) %></td>
                                                <td>PeriodDay: <%# (int)Eval("PeriodDay") %></td>
                                                <td>PeriodNumber: <%# (int)Eval("PeriodNumber") %></td>
                                                <td>
                                                    <form class="RegisterInPeriod">
                                                        <input type="hidden" name="CurrentStudentID" value="<%# CurrentStudentID %>" />
                                                        <input type="submit" name="LectureOfGroupID" value="Add" data-lectureofgroupid="<%# (int)Eval("Id") %>" />
                                                    </form>
                                                </td>
                                            </tr>
                                        </ItemTemplate>

                                    </asp:Repeater>

                                </table>
                            </td>
                            <%--<td><%# Eval("NumberOfSections") %></td>
                            <td><%# Eval("NumberOfLabs") %></td>
                            <td id="<%# string.Format("Container-CourseID-{0}",Eval("Id")) %>">
                                <%# CourseSemester.CheckCourseExistInLastSemester((int)Eval("Id"))? AddLabelCalledADDED():AddBuutonToAddCourseToSemester((string)Eval("CourseName"),(int)Eval("Id"),1) %>
                            </td>--%>
                        </tr>

                    </ItemTemplate>
                </asp:Repeater>



            </table>

        </div>

        <div id="3rd_term" class="tabcontent">

            <table class="CoursesOfTerm">
                <tr>
                    <th>Course Name</th>
                    <th>Credit Hours</th>
                    <th>Periods</th>

                </tr>

                <asp:Repeater runat="server" ID="AllCoursesTerm3" ClientIDMode="Static">
                    <ItemTemplate>

                        <tr>
                            <td><%# Eval("CourseName") %></td>
                            <td><%# Eval("CreditHours") %></td>
                            <td>
                                <table class="CoursesOfTerm">

                                    <asp:Repeater runat="server" DataSource='<%# GetAllPeriodsForThisCourseSemester( (int)Eval("Id") ) %>'>
                                        <ItemTemplate>
                                            <tr>
                                                <td><%# (int)Eval("GroupNumber") %></td>
                                                <td><%# GetPeriodType((int)Eval("PeriodType")) %></td>
                                                <td>PeriodDay: <%# (int)Eval("PeriodDay") %></td>
                                                <td>PeriodNumber: <%# (int)Eval("PeriodNumber") %></td>
                                                <td>
                                                    <form class="RegisterInPeriod">
                                                        <input type="hidden" name="CurrentStudentID" value="<%# CurrentStudentID %>" />
                                                        <input type="submit" name="LectureOfGroupID" value="Add" data-lectureofgroupid="<%# (int)Eval("Id") %>" />
                                                    </form>
                                                </td>
                                            </tr>
                                        </ItemTemplate>

                                    </asp:Repeater>

                                </table>
                            </td>
                            <%--<td><%# Eval("NumberOfSections") %></td>
                            <td><%# Eval("NumberOfLabs") %></td>
                            <td id="<%# string.Format("Container-CourseID-{0}",Eval("Id")) %>">
                                <%# CourseSemester.CheckCourseExistInLastSemester((int)Eval("Id"))? AddLabelCalledADDED():AddBuutonToAddCourseToSemester((string)Eval("CourseName"),(int)Eval("Id"),1) %>
                            </td>--%>
                        </tr>

                    </ItemTemplate>
                </asp:Repeater>



            </table>

        </div>
        <div id="4th_term" class="tabcontent">

            <table class="CoursesOfTerm">
                <tr>
                    <th>Course Name</th>
                    <th>Credit Hours</th>
                    <th>Periods</th>

                </tr>

                <asp:Repeater runat="server" ID="AllCoursesTerm4" ClientIDMode="Static">
                    <ItemTemplate>

                        <tr>
                            <td><%# Eval("CourseName") %></td>
                            <td><%# Eval("CreditHours") %></td>
                            <td>
                                <table class="CoursesOfTerm">

                                    <asp:Repeater runat="server" DataSource='<%# GetAllPeriodsForThisCourseSemester( (int)Eval("Id") ) %>'>
                                        <ItemTemplate>
                                            <tr>
                                                <td><%# (int)Eval("GroupNumber") %></td>
                                                <td><%# GetPeriodType((int)Eval("PeriodType")) %></td>
                                                <td>PeriodDay: <%# (int)Eval("PeriodDay") %></td>
                                                <td>PeriodNumber: <%# (int)Eval("PeriodNumber") %></td>
                                                <td>
                                                    <form class="RegisterInPeriod">
                                                        <input type="hidden" name="CurrentStudentID" value="<%# CurrentStudentID %>" />
                                                        <input type="submit" name="LectureOfGroupID" value="Add" data-lectureofgroupid="<%# (int)Eval("Id") %>" />
                                                    </form>
                                                </td>
                                            </tr>
                                        </ItemTemplate>

                                    </asp:Repeater>

                                </table>
                            </td>
                            <%--<td><%# Eval("NumberOfSections") %></td>
                            <td><%# Eval("NumberOfLabs") %></td>
                            <td id="<%# string.Format("Container-CourseID-{0}",Eval("Id")) %>">
                                <%# CourseSemester.CheckCourseExistInLastSemester((int)Eval("Id"))? AddLabelCalledADDED():AddBuutonToAddCourseToSemester((string)Eval("CourseName"),(int)Eval("Id"),1) %>
                            </td>--%>
                        </tr>

                    </ItemTemplate>
                </asp:Repeater>



            </table>

        </div>

        <div id="5th_term" class="tabcontent">

            <table class="CoursesOfTerm">
                <tr>
                    <th>Course Name</th>
                    <th>Credit Hours</th>
                    <th>Periods</th>

                </tr>

                <asp:Repeater runat="server" ID="AllCoursesTerm5" ClientIDMode="Static">
                    <ItemTemplate>

                        <tr>
                            <td><%# Eval("CourseName") %></td>
                            <td><%# Eval("CreditHours") %></td>
                            <td>
                                <table class="CoursesOfTerm">

                                    <asp:Repeater runat="server" DataSource='<%# GetAllPeriodsForThisCourseSemester( (int)Eval("Id") ) %>'>
                                        <ItemTemplate>
                                            <tr>
                                                <td><%# (int)Eval("GroupNumber") %></td>
                                                <td><%# GetPeriodType((int)Eval("PeriodType")) %></td>
                                                <td>PeriodDay: <%# (int)Eval("PeriodDay") %></td>
                                                <td>PeriodNumber: <%# (int)Eval("PeriodNumber") %></td>
                                                <td>
                                                    <form class="RegisterInPeriod">
                                                        <input type="hidden" name="CurrentStudentID" value="<%# CurrentStudentID %>" />
                                                        <input type="submit" name="LectureOfGroupID" value="Add" data-lectureofgroupid="<%# (int)Eval("Id") %>" />
                                                    </form>
                                                </td>
                                            </tr>
                                        </ItemTemplate>

                                    </asp:Repeater>

                                </table>
                            </td>
                            <%--<td><%# Eval("NumberOfSections") %></td>
                            <td><%# Eval("NumberOfLabs") %></td>
                            <td id="<%# string.Format("Container-CourseID-{0}",Eval("Id")) %>">
                                <%# CourseSemester.CheckCourseExistInLastSemester((int)Eval("Id"))? AddLabelCalledADDED():AddBuutonToAddCourseToSemester((string)Eval("CourseName"),(int)Eval("Id"),1) %>
                            </td>--%>
                        </tr>

                    </ItemTemplate>
                </asp:Repeater>



            </table>

        </div>

        <div id="6th_term" class="tabcontent">

            <table class="CoursesOfTerm">
                <tr>
                    <th>Course Name</th>
                    <th>Credit Hours</th>
                    <th>Periods</th>

                </tr>

                <asp:Repeater runat="server" ID="AllCoursesTerm6" ClientIDMode="Static">
                    <ItemTemplate>

                        <tr>
                            <td><%# Eval("CourseName") %></td>
                            <td><%# Eval("CreditHours") %></td>
                            <td>
                                <table class="CoursesOfTerm">

                                    <asp:Repeater runat="server" DataSource='<%# GetAllPeriodsForThisCourseSemester( (int)Eval("Id") ) %>'>
                                        <ItemTemplate>
                                            <tr>
                                                <td><%# (int)Eval("GroupNumber") %></td>
                                                <td><%# GetPeriodType((int)Eval("PeriodType")) %></td>
                                                <td>PeriodDay: <%# (int)Eval("PeriodDay") %></td>
                                                <td>PeriodNumber: <%# (int)Eval("PeriodNumber") %></td>
                                                <td>
                                                    <form class="RegisterInPeriod">
                                                        <input type="hidden" name="CurrentStudentID" value="<%# CurrentStudentID %>" />
                                                        <input type="submit" name="LectureOfGroupID" value="Add" data-lectureofgroupid="<%# (int)Eval("Id") %>" />
                                                    </form>
                                                </td>
                                            </tr>
                                        </ItemTemplate>

                                    </asp:Repeater>

                                </table>
                            </td>
                            <%--<td><%# Eval("NumberOfSections") %></td>
                            <td><%# Eval("NumberOfLabs") %></td>
                            <td id="<%# string.Format("Container-CourseID-{0}",Eval("Id")) %>">
                                <%# CourseSemester.CheckCourseExistInLastSemester((int)Eval("Id"))? AddLabelCalledADDED():AddBuutonToAddCourseToSemester((string)Eval("CourseName"),(int)Eval("Id"),1) %>
                            </td>--%>
                        </tr>

                    </ItemTemplate>
                </asp:Repeater>



            </table>

        </div>
        <div id="7th_term" class="tabcontent">

            <table class="CoursesOfTerm">
                <tr>
                    <th>Course Name</th>
                    <th>Credit Hours</th>
                    <th>Periods</th>

                </tr>

                <asp:Repeater runat="server" ID="AllCoursesTerm7" ClientIDMode="Static">
                    <ItemTemplate>

                        <tr>
                            <td><%# Eval("CourseName") %></td>
                            <td><%# Eval("CreditHours") %></td>
                            <td>
                                <table class="CoursesOfTerm">

                                    <asp:Repeater runat="server" DataSource='<%# GetAllPeriodsForThisCourseSemester( (int)Eval("Id") ) %>'>
                                        <ItemTemplate>
                                            <tr>
                                                <td><%# (int)Eval("GroupNumber") %></td>
                                                <td><%# GetPeriodType((int)Eval("PeriodType")) %></td>
                                                <td>PeriodDay: <%# (int)Eval("PeriodDay") %></td>
                                                <td>PeriodNumber: <%# (int)Eval("PeriodNumber") %></td>
                                                <td>
                                                    <form class="RegisterInPeriod">
                                                        <input type="hidden" name="CurrentStudentID" value="<%# CurrentStudentID %>" />
                                                        <input type="submit" name="LectureOfGroupID" value="Add" data-lectureofgroupid="<%# (int)Eval("Id") %>" />
                                                    </form>
                                                </td>
                                            </tr>
                                        </ItemTemplate>

                                    </asp:Repeater>

                                </table>
                            </td>
                            <%--<td><%# Eval("NumberOfSections") %></td>
                            <td><%# Eval("NumberOfLabs") %></td>
                            <td id="<%# string.Format("Container-CourseID-{0}",Eval("Id")) %>">
                                <%# CourseSemester.CheckCourseExistInLastSemester((int)Eval("Id"))? AddLabelCalledADDED():AddBuutonToAddCourseToSemester((string)Eval("CourseName"),(int)Eval("Id"),1) %>
                            </td>--%>
                        </tr>

                    </ItemTemplate>
                </asp:Repeater>



            </table>

        </div>
        <div id="8th_term" class="tabcontent">

            <table class="CoursesOfTerm">
                <tr>
                    <th>Course Name</th>
                    <th>Credit Hours</th>
                    <th>Periods</th>

                </tr>

                <asp:Repeater runat="server" ID="AllCoursesTerm8" ClientIDMode="Static">
                    <ItemTemplate>

                        <tr>
                            <td><%# Eval("CourseName") %></td>
                            <td><%# Eval("CreditHours") %></td>
                            <td>
                                <table class="CoursesOfTerm">

                                    <asp:Repeater runat="server" DataSource='<%# GetAllPeriodsForThisCourseSemester( (int)Eval("Id") ) %>'>
                                        <ItemTemplate>
                                            <tr>
                                                <td><%# (int)Eval("GroupNumber") %></td>
                                                <td><%# GetPeriodType((int)Eval("PeriodType")) %></td>
                                                <td>PeriodDay: <%# (int)Eval("PeriodDay") %></td>
                                                <td>PeriodNumber: <%# (int)Eval("PeriodNumber") %></td>
                                                <td>
                                                    <form class="RegisterInPeriod">
                                                        <input type="hidden" name="CurrentStudentID" value="<%# CurrentStudentID %>" />
                                                        <input type="submit" name="LectureOfGroupID" value="Add" data-lectureofgroupid="<%# (int)Eval("Id") %>" />
                                                    </form>
                                                </td>
                                            </tr>
                                        </ItemTemplate>

                                    </asp:Repeater>

                                </table>
                            </td>
                            <%--<td><%# Eval("NumberOfSections") %></td>
                            <td><%# Eval("NumberOfLabs") %></td>
                            <td id="<%# string.Format("Container-CourseID-{0}",Eval("Id")) %>">
                                <%# CourseSemester.CheckCourseExistInLastSemester((int)Eval("Id"))? AddLabelCalledADDED():AddBuutonToAddCourseToSemester((string)Eval("CourseName"),(int)Eval("Id"),1) %>
                            </td>--%>
                        </tr>

                    </ItemTemplate>
                </asp:Repeater>



            </table>

        </div>

        <div id="9th_term" class="tabcontent">

            <table class="CoursesOfTerm">
                <tr>
                    <th>Course Name</th>
                    <th>Credit Hours</th>
                    <th>Periods</th>

                </tr>

                <asp:Repeater runat="server" ID="AllCoursesTerm9" ClientIDMode="Static">
                    <ItemTemplate>

                        <tr>
                            <td><%# Eval("CourseName") %></td>
                            <td><%# Eval("CreditHours") %></td>
                            <td>
                                <table class="CoursesOfTerm">

                                    <asp:Repeater runat="server" DataSource='<%# GetAllPeriodsForThisCourseSemester( (int)Eval("Id") ) %>'>
                                        <ItemTemplate>
                                            <tr>
                                                <td><%# (int)Eval("GroupNumber") %></td>
                                                <td><%# GetPeriodType((int)Eval("PeriodType")) %></td>
                                                <td>PeriodDay: <%# (int)Eval("PeriodDay") %></td>
                                                <td>PeriodNumber: <%# (int)Eval("PeriodNumber") %></td>
                                                <td>
                                                    <form class="RegisterInPeriod">
                                                        <input type="hidden" name="CurrentStudentID" value="<%# CurrentStudentID %>" />
                                                        <input type="submit" name="LectureOfGroupID" value="Add" data-lectureofgroupid="<%# (int)Eval("Id") %>" />
                                                    </form>
                                                </td>
                                            </tr>
                                        </ItemTemplate>

                                    </asp:Repeater>

                                </table>
                            </td>
                            <%--<td><%# Eval("NumberOfSections") %></td>
                            <td><%# Eval("NumberOfLabs") %></td>
                            <td id="<%# string.Format("Container-CourseID-{0}",Eval("Id")) %>">
                                <%# CourseSemester.CheckCourseExistInLastSemester((int)Eval("Id"))? AddLabelCalledADDED():AddBuutonToAddCourseToSemester((string)Eval("CourseName"),(int)Eval("Id"),1) %>
                            </td>--%>
                        </tr>

                    </ItemTemplate>
                </asp:Repeater>



            </table>

        </div>

        <div id="10th_term" class="tabcontent">

            <table class="CoursesOfTerm">
                <tr>
                    <th>Course Name</th>
                    <th>Credit Hours</th>
                    <th>Periods</th>

                </tr>

                <asp:Repeater runat="server" ID="AllCoursesTerm10" ClientIDMode="Static">
                    <ItemTemplate>

                        <tr>
                            <td><%# Eval("CourseName") %></td>
                            <td><%# Eval("CreditHours") %></td>
                            <td>
                                <table class="CoursesOfTerm">

                                    <asp:Repeater runat="server" DataSource='<%# GetAllPeriodsForThisCourseSemester( (int)Eval("Id") ) %>'>
                                        <ItemTemplate>
                                            <tr>
                                                <td><%# (int)Eval("GroupNumber") %></td>
                                                <td><%# GetPeriodType((int)Eval("PeriodType")) %></td>
                                                <td>PeriodDay: <%# (int)Eval("PeriodDay") %></td>
                                                <td>PeriodNumber: <%# (int)Eval("PeriodNumber") %></td>
                                                <td>
                                                    <form class="RegisterInPeriod">
                                                        <input type="hidden" name="CurrentStudentID" value="<%# CurrentStudentID %>" />
                                                        <input type="submit" name="LectureOfGroupID" value="Add" data-lectureofgroupid="<%# (int)Eval("Id") %>" />
                                                    </form>
                                                </td>
                                            </tr>
                                        </ItemTemplate>

                                    </asp:Repeater>

                                </table>
                            </td>
                            <%--<td><%# Eval("NumberOfSections") %></td>
                            <td><%# Eval("NumberOfLabs") %></td>
                            <td id="<%# string.Format("Container-CourseID-{0}",Eval("Id")) %>">
                                <%# CourseSemester.CheckCourseExistInLastSemester((int)Eval("Id"))? AddLabelCalledADDED():AddBuutonToAddCourseToSemester((string)Eval("CourseName"),(int)Eval("Id"),1) %>
                            </td>--%>
                        </tr>

                    </ItemTemplate>
                </asp:Repeater>



            </table>

        </div>
    </div>

    <!--HERE Time table-->
    <br />
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

    <form>
        <input style="position:relative;right:-600px;top:20px;margin-bottom:40px;" type="submit" value="Save" onclick="SaveRegistration($('#CurrentStudentIDTextBox').val()); return false;" />
    </form>


</asp:Content>
