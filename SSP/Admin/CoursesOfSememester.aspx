<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Admin.Master" AutoEventWireup="true" CodeBehind="CoursesOfSememester.aspx.cs" Inherits="SSP.Admin.CoursesOfSememester" %>

<%@ Import Namespace="Models" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../Content/Tabs.less" rel="stylesheet" />
    <script src="../Scripts/HandleCoursesOfsemester.js"></script>
    <link href="../Content/CustomForCoursesOfSemester.css" rel="stylesheet" />
    <link href="../Content/AddCourseToSemesterPopUpForm.css" rel="stylesheet" />
    <script src="../Scripts/AddCourseToSemester.js"></script>
</asp:Content>


<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div id="PopUpForm" class="AddCoursePopUp">
        <form id="RegisterNewCourseForm">
            <label style="font: 300; font-size: large; margin: 0px; padding: 0px;" id="ContainerForCourseName"></label>
            <br />
            <label id="GroupNumberMandatory" style="color: red; text-align: center; display: none;">There is error in the from</label>
            <br />


            <label>Number OF Groups</label>
            <br />
            <input type="text" name="GroupsNumber" />
            <br />
            <label>Number OF Sections per Group</label>
            <br />
            <input type="text" name="SectionsPerGroupNumber" />
            <br />
            <input type="submit" style="float: right" value="OK" />
        </form>
    </div>


    <h2>Select Term to Scedule.</h2>

    <div class="tab" style="display: inline-block">
        <a href="javascript:void(0)" class="tablinks" onclick="openCity(event, '1st_term','1st_term2')">1st term</a>
        <a href="javascript:void(0)" class="tablinks" onclick="openCity(event, '2nd_term','2nd_term2')">2nd term</a>
        <a href="javascript:void(0)" class="tablinks" onclick="openCity(event, '3rd_term','3rd_term2')">3rd term</a>
        <a href="javascript:void(0)" class="tablinks" onclick="openCity(event, '4th_term','4th_term2')">4th term</a>
        <a href="javascript:void(0)" class="tablinks" onclick="openCity(event, '5th_term','5th_term2')">5th term</a>
        <a href="javascript:void(0)" class="tablinks" onclick="openCity(event, '6th_term','6th_term2')">6th term</a>
        <a href="javascript:void(0)" class="tablinks" onclick="openCity(event, '7th_term','7th_term2')">7th term</a>
        <a href="javascript:void(0)" class="tablinks" onclick="openCity(event, '8th_term','8th_term2')">8th term</a>
        <a href="javascript:void(0)" class="tablinks" onclick="openCity(event, '9th_term','9th_term2')">9th term</a>
        <a href="javascript:void(0)" class="tablinks" onclick="openCity(event, '10th_term','10th_term2')">10th term</a>
    </div>


    <h3>All Courses</h3>


    <div id="AllCousresOfTerm" class="AllCousresOfTerm">



        <div id="1st_term" class="tabcontent">

            <table class="CoursesOfTerm">
                <tr>
                    <th>Course Name</th>
                    <th>Credit Hours</th>
                    <th>Number Of Lectures</th>
                    <th>Number Of Sections</th>
                    <th>Number Of Labs</th>
                    <th></th>
                </tr>

                <asp:Repeater runat="server" ID="AllCoursesTerm1">
                    <ItemTemplate>

                        <tr>
                            <td><%# Eval("CourseName") %></td>
                            <td><%# Eval("CreditHours") %></td>
                            <td><%# Eval("NumberOfLectures") %></td>
                            <td><%# Eval("NumberOfSections") %></td>
                            <td><%# Eval("NumberOfLabs") %></td>
                            <td id="<%# string.Format("Container-CourseID-{0}",Eval("Id")) %>">
                                <%# CourseSemester.CheckCourseExistInLastSemester((int)Eval("Id"))? AddLabelCalledADDED():AddBuutonToAddCourseToSemester((string)Eval("CourseName"),(int)Eval("Id"),1) %>
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
                    <th>Number Of Lectures</th>
                    <th>Number Of Sections</th>
                    <th>Number Of Labs</th>
                </tr>

                <asp:Repeater runat="server" ID="AllCoursesTerm2">
                    <ItemTemplate>

                        <tr>
                            <td><%# Eval("CourseName") %></td>
                            <td><%# Eval("CreditHours") %></td>
                            <td><%# Eval("NumberOfLectures") %></td>
                            <td><%# Eval("NumberOfSections") %></td>
                            <td><%# Eval("NumberOfLabs") %></td>
                            <td id="<%# string.Format("Container-CourseID-{0}",Eval("Id")) %>">

                                <%# CourseSemester.CheckCourseExistInLastSemester((int)Eval("Id"))? AddLabelCalledADDED():AddBuutonToAddCourseToSemester((string)Eval("CourseName"),(int)Eval("Id"),2) %>

                                <%--<form>
                                    <input type="submit" data-coursename="<%# Eval("CourseName") %>" data-courseid="<%# Eval("Id") %>" value="ADD" class="AddCourseToSemester" />
                                </form>--%>
                          
                            </td>
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
                    <th>Number Of Lectures</th>
                    <th>Number Of Sections</th>
                    <th>Number Of Labs</th>
                </tr>

                <asp:Repeater runat="server" ID="AllCoursesTerm3">
                    <ItemTemplate>

                        <tr>
                            <td><%# Eval("CourseName") %></td>
                            <td><%# Eval("CreditHours") %></td>
                            <td><%# Eval("NumberOfLectures") %></td>
                            <td><%# Eval("NumberOfSections") %></td>
                            <td><%# Eval("NumberOfLabs") %></td>
                            <td id="<%# string.Format("Container-CourseID-{0}",Eval("Id")) %>">

                                <%--                                <form>
                                    <input type="submit" data-coursename="<%# Eval("CourseName") %>" data-courseid="<%# Eval("Id") %>" value="ADD" class="AddCourseToSemester" />
                                </form>--%>


                                <%# CourseSemester.CheckCourseExistInLastSemester((int)Eval("Id"))? AddLabelCalledADDED():AddBuutonToAddCourseToSemester((string)Eval("CourseName"),(int)Eval("Id"),3) %>
                            
                            </td>
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
                    <th>Number Of Lectures</th>
                    <th>Number Of Sections</th>
                    <th>Number Of Labs</th>
                </tr>

                <asp:Repeater runat="server" ID="AllCoursesTerm4">
                    <ItemTemplate>

                        <tr>
                            <td><%# Eval("CourseName") %></td>
                            <td><%# Eval("CreditHours") %></td>
                            <td><%# Eval("NumberOfLectures") %></td>
                            <td><%# Eval("NumberOfSections") %></td>
                            <td><%# Eval("NumberOfLabs") %></td>
                            <td id="<%# string.Format("Container-CourseID-{0}",Eval("Id")) %>">

                                <%--  <form>
                                    <input type="submit" data-coursename="<%# Eval("CourseName") %>" data-courseid="<%# Eval("Id") %>" value="ADD" class="AddCourseToSemester" />
                                </form>--%>

                                <%# CourseSemester.CheckCourseExistInLastSemester((int)Eval("Id"))? AddLabelCalledADDED():AddBuutonToAddCourseToSemester((string)Eval("CourseName"),(int)Eval("Id"),4) %>
                            
                          
                            </td>
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
                    <th>Number Of Lectures</th>
                    <th>Number Of Sections</th>
                    <th>Number Of Labs</th>
                </tr>

                <asp:Repeater runat="server" ID="AllCoursesTerm5">
                    <ItemTemplate>

                        <tr>
                            <td><%# Eval("CourseName") %></td>
                            <td><%# Eval("CreditHours") %></td>
                            <td><%# Eval("NumberOfLectures") %></td>
                            <td><%# Eval("NumberOfSections") %></td>
                            <td><%# Eval("NumberOfLabs") %></td>
                            <td id="<%# string.Format("Container-CourseID-{0}",Eval("Id")) %>">

                                <%--<form>
                                    <input type="submit" data-coursename="<%# Eval("CourseName") %>" data-courseid="<%# Eval("Id") %>" value="ADD" class="AddCourseToSemester" />
                                </form>--%>

                                <%# CourseSemester.CheckCourseExistInLastSemester((int)Eval("Id"))? AddLabelCalledADDED():AddBuutonToAddCourseToSemester((string)Eval("CourseName"),(int)Eval("Id"),5) %>
                            
                          
                            </td>
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
                    <th>Number Of Lectures</th>
                    <th>Number Of Sections</th>
                    <th>Number Of Labs</th>
                </tr>

                <asp:Repeater runat="server" ID="AllCoursesTerm6">
                    <ItemTemplate>

                        <tr>
                            <td><%# Eval("CourseName") %></td>
                            <td><%# Eval("CreditHours") %></td>
                            <td><%# Eval("NumberOfLectures") %></td>
                            <td><%# Eval("NumberOfSections") %></td>
                            <td><%# Eval("NumberOfLabs") %></td>
                            <td id="<%# string.Format("Container-CourseID-{0}",Eval("Id")) %>">

                                <%-- <form>
                                    <input type="submit" data-coursename="<%# Eval("CourseName") %>" data-courseid="<%# Eval("Id") %>" value="ADD" class="AddCourseToSemester" />
                                </form>--%>

                                <%# CourseSemester.CheckCourseExistInLastSemester((int)Eval("Id"))? AddLabelCalledADDED():AddBuutonToAddCourseToSemester((string)Eval("CourseName"),(int)Eval("Id"),6) %>
                            
                            </td>
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
                    <th>Number Of Lectures</th>
                    <th>Number Of Sections</th>
                    <th>Number Of Labs</th>
                </tr>

                <asp:Repeater runat="server" ID="AllCoursesTerm7">
                    <ItemTemplate>

                        <tr>
                            <td><%# Eval("CourseName") %></td>
                            <td><%# Eval("CreditHours") %></td>
                            <td><%# Eval("NumberOfLectures") %></td>
                            <td><%# Eval("NumberOfSections") %></td>
                            <td><%# Eval("NumberOfLabs") %></td>
                            <td id="<%# string.Format("Container-CourseID-{0}",Eval("Id")) %>">

                                <%-- <form>
                                    <input type="submit" data-coursename="<%# Eval("CourseName") %>" data-courseid="<%# Eval("Id") %>" value="ADD" class="AddCourseToSemester" />
                                </form>--%>

                                <%# CourseSemester.CheckCourseExistInLastSemester((int)Eval("Id"))? AddLabelCalledADDED():AddBuutonToAddCourseToSemester((string)Eval("CourseName"),(int)Eval("Id"),7) %>
                            
                          
                            </td>
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
                    <th>Number Of Lectures</th>
                    <th>Number Of Sections</th>
                    <th>Number Of Labs</th>
                </tr>

                <asp:Repeater runat="server" ID="AllCoursesTerm8">
                    <ItemTemplate>

                        <tr>
                            <td><%# Eval("CourseName") %></td>
                            <td><%# Eval("CreditHours") %></td>
                            <td><%# Eval("NumberOfLectures") %></td>
                            <td><%# Eval("NumberOfSections") %></td>
                            <td><%# Eval("NumberOfLabs") %></td>
                            <td id="<%# string.Format("Container-CourseID-{0}",Eval("Id")) %>">

                                <%-- <form>
                                    <input type="submit" data-coursename="<%# Eval("CourseName") %>" data-courseid="<%# Eval("Id") %>" value="ADD" class="AddCourseToSemester" />
                                </form>--%>

                                <%# CourseSemester.CheckCourseExistInLastSemester((int)Eval("Id"))? AddLabelCalledADDED():AddBuutonToAddCourseToSemester((string)Eval("CourseName"),(int)Eval("Id"),8) %>
                            
                          
                            </td>
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
                    <th>Number Of Lectures</th>
                    <th>Number Of Sections</th>
                    <th>Number Of Labs</th>
                </tr>

                <asp:Repeater runat="server" ID="AllCoursesTerm9">
                    <ItemTemplate>

                        <tr>
                            <td><%# Eval("CourseName") %></td>
                            <td><%# Eval("CreditHours") %></td>
                            <td><%# Eval("NumberOfLectures") %></td>
                            <td><%# Eval("NumberOfSections") %></td>
                            <td><%# Eval("NumberOfLabs") %></td>
                            <td id="<%# string.Format("Container-CourseID-{0}",Eval("Id")) %>">

                                <%-- <form>
                                    <input type="submit" data-coursename="<%# Eval("CourseName") %>" data-courseid="<%# Eval("Id") %>" value="ADD" class="AddCourseToSemester" />
                                </form>--%>


                                <%# CourseSemester.CheckCourseExistInLastSemester((int)Eval("Id"))? AddLabelCalledADDED():AddBuutonToAddCourseToSemester((string)Eval("CourseName"),(int)Eval("Id"),9) %>
                            
                          
                            </td>
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
                    <th>Number Of Lectures</th>
                    <th>Number Of Sections</th>
                    <th>Number Of Labs</th>
                </tr>

                <asp:Repeater runat="server" ID="AllCoursesTerm10">
                    <ItemTemplate>

                        <tr>
                            <td><%# Eval("CourseName") %></td>
                            <td><%# Eval("CreditHours") %></td>
                            <td><%# Eval("NumberOfLectures") %></td>
                            <td><%# Eval("NumberOfSections") %></td>
                            <td><%# Eval("NumberOfLabs") %></td>
                            <td id="<%# string.Format("Container-CourseID-{0}",Eval("Id")) %>">

                                <%--<form>
                                    <input type="submit" data-coursename="<%# Eval("CourseName") %>" data-courseid="<%# Eval("Id") %>" value="ADD" class="AddCourseToSemester" />
                                </form>--%>

                                <%# CourseSemester.CheckCourseExistInLastSemester((int)Eval("Id"))? AddLabelCalledADDED():AddBuutonToAddCourseToSemester((string)Eval("CourseName"),(int)Eval("Id"),10) %>
                            
                          
                            </td>
                        </tr>

                    </ItemTemplate>
                </asp:Repeater>



            </table>
        </div>
    </div>

    <!-- begin the second half of page -->


    <h3>Selected Courses</h3>


    <div id="AllCousresOfTerm2" class="AllCousresOfTerm">


        <div id="1st_term2" class="tabcontent">
            <table class="CoursesOfTerm" id="SelectedCoursesTerm1">
                <tr>
                    <th>Course Name</th>
                    <th>Number Of Groups</th>
                    <th>Number of sections per group</th>
                    <th></th>
                </tr>

                <asp:Repeater runat="server" ID="SelectedTerm1">
                    <ItemTemplate>

                        <tr>
                            <td><%# Eval("CourseName") %></td>
                            <td><%# Eval("NumberOfGroups") %></td>
                            <td><%# Eval("NumberOfSectionsPerGroup") %></td>
                            <td>
                                <form>
                                    <input type="submit" data-coursename="<%# Eval("CourseName") %>" data-coursesemesterid="<%# Eval("Id") %>" value="Remove" class="RemoveCourseFromSemester" />
                                </form>
                            </td>
                        </tr>

                    </ItemTemplate>
                </asp:Repeater>



            </table>
        </div>

        <div id="2nd_term2" class="tabcontent">
            <table class="CoursesOfTerm" id="SelectedCoursesTerm2">
                <tr>
                    <th>Course Name</th>
                    <th>Number Of Groups</th>
                    <th>Number of sections per group</th>
                    <th></th>
                </tr>

                <asp:Repeater runat="server" ID="SelectedTerm2">
                    <ItemTemplate>

                        <tr>
                            <td><%# Eval("CourseName") %></td>
                            <td><%# Eval("NumberOfGroups") %></td>
                            <td><%# Eval("NumberOfSectionsPerGroup") %></td>
                            <td>
                                <form>
                                    <input type="submit" data-coursename="<%# Eval("CourseName") %>" data-coursesemesterid="<%# Eval("Id") %>" value="Remove" class="RemoveCourseFromSemester" />
                                </form>
                            </td>
                        </tr>

                    </ItemTemplate>
                </asp:Repeater>



            </table>
        </div>

        <div id="3rd_term2" class="tabcontent">
            <table class="CoursesOfTerm" id="SelectedCoursesTerm3">
                <tr>
                    <th>Course Name</th>
                    <th>Number Of Groups</th>
                    <th>Number of sections per group</th>
                    <th></th>
                </tr>

                <asp:Repeater runat="server" ID="SelectedTerm3">
                    <ItemTemplate>

                        <tr>
                            <td><%# Eval("CourseName") %></td>
                            <td><%# Eval("NumberOfGroups") %></td>
                            <td><%# Eval("NumberOfSectionsPerGroup") %></td>
                            <td>
                                <form>
                                    <input type="submit" data-coursename="<%# Eval("CourseName") %>" data-coursesemesterid="<%# Eval("Id") %>" value="Remove" class="RemoveCourseFromSemester" />
                                </form>
                            </td>
                        </tr>

                    </ItemTemplate>
                </asp:Repeater>



            </table>
        </div>
        <div id="4th_term2" class="tabcontent">
            <table class="CoursesOfTerm" id="SelectedCoursesTerm4">
                <tr>
                    <th>Course Name</th>
                    <th>Number Of Groups</th>
                    <th>Number of sections per group</th>
                    <th></th>
                </tr>

                <asp:Repeater runat="server" ID="SelectedTerm4">
                    <ItemTemplate>

                        <tr>
                            <td><%# Eval("CourseName") %></td>
                            <td><%# Eval("NumberOfGroups") %></td>
                            <td><%# Eval("NumberOfSectionsPerGroup") %></td>
                            <td>
                                <form>
                                    <input type="submit" data-coursename="<%# Eval("CourseName") %>" data-coursesemesterid="<%# Eval("Id") %>" value="Remove" class="RemoveCourseFromSemester" />
                                </form>
                            </td>
                        </tr>

                    </ItemTemplate>
                </asp:Repeater>



            </table>
        </div>

        <div id="5th_term2" class="tabcontent">
            <table class="CoursesOfTerm" id="SelectedCoursesTerm5">
                <tr>
                    <th>Course Name</th>
                    <th>Number Of Groups</th>
                    <th>Number of sections per group</th>
                    <th></th>
                </tr>

                <asp:Repeater runat="server" ID="SelectedTerm5">
                    <ItemTemplate>

                        <tr>
                            <td><%# Eval("CourseName") %></td>
                            <td><%# Eval("NumberOfGroups") %></td>
                            <td><%# Eval("NumberOfSectionsPerGroup") %></td>
                            <td>
                                <form>
                                    <input type="submit" data-coursename="<%# Eval("CourseName") %>" data-coursesemesterid="<%# Eval("Id") %>" value="Remove" class="RemoveCourseFromSemester" />
                                </form>
                            </td>
                        </tr>

                    </ItemTemplate>
                </asp:Repeater>



            </table>
        </div>

        <div id="6th_term2" class="tabcontent">
            <table class="CoursesOfTerm" id="SelectedCoursesTerm6">
                <tr>
                    <th>Course Name</th>
                    <th>Number Of Groups</th>
                    <th>Number of sections per group</th>
                    <th></th>
                </tr>

                <asp:Repeater runat="server" ID="SelectedTerm6">
                    <ItemTemplate>

                        <tr>
                            <td><%# Eval("CourseName") %></td>
                            <td><%# Eval("NumberOfGroups") %></td>
                            <td><%# Eval("NumberOfSectionsPerGroup") %></td>
                            <td>
                                <form>
                                    <input type="submit" data-coursename="<%# Eval("CourseName") %>" data-coursesemesterid="<%# Eval("Id") %>" value="Remove" class="RemoveCourseFromSemester" />
                                </form>
                            </td>
                        </tr>

                    </ItemTemplate>
                </asp:Repeater>



            </table>
        </div>
        <div id="7th_term2" class="tabcontent">
            <table class="CoursesOfTerm" id="SelectedCoursesTerm7">
                <tr>
                    <th>Course Name</th>
                    <th>Number Of Groups</th>
                    <th>Number of sections per group</th>
                    <th></th>
                </tr>

                <asp:Repeater runat="server" ID="SelectedTerm7">
                    <ItemTemplate>

                        <tr>
                            <td><%# Eval("CourseName") %></td>
                            <td><%# Eval("NumberOfGroups") %></td>
                            <td><%# Eval("NumberOfSectionsPerGroup") %></td>
                            <td>
                                <form>
                                    <input type="submit" data-coursename="<%# Eval("CourseName") %>" data-coursesemesterid="<%# Eval("Id") %>" value="Remove" class="RemoveCourseFromSemester" />
                                </form>
                            </td>
                        </tr>

                    </ItemTemplate>
                </asp:Repeater>



            </table>
        </div>
        <div id="8th_term2" class="tabcontent">
            <table class="CoursesOfTerm" id="SelectedCoursesTerm8">
                <tr>
                    <th>Course Name</th>
                    <th>Number Of Groups</th>
                    <th>Number of sections per group</th>
                    <th></th>
                </tr>

                <asp:Repeater runat="server" ID="SelectedTerm8">
                    <ItemTemplate>

                        <tr>
                            <td><%# Eval("CourseName") %></td>
                            <td><%# Eval("NumberOfGroups") %></td>
                            <td><%# Eval("NumberOfSectionsPerGroup") %></td>
                            <td>
                                <form>
                                    <input type="submit" data-coursename="<%# Eval("CourseName") %>" data-coursesemesterid="<%# Eval("Id") %>" value="Remove" class="RemoveCourseFromSemester" />
                                </form>
                            </td>
                        </tr>

                    </ItemTemplate>
                </asp:Repeater>



            </table>
        </div>

        <div id="9th_term2" class="tabcontent">
            <table class="CoursesOfTerm" id="SelectedCoursesTerm9">
                <tr>
                    <th>Course Name</th>
                    <th>Number Of Groups</th>
                    <th>Number of sections per group</th>
                    <th></th>
                </tr>

                <asp:Repeater runat="server" ID="SelectedTerm9">
                    <ItemTemplate>

                        <tr>
                            <td><%# Eval("CourseName") %></td>
                            <td><%# Eval("NumberOfGroups") %></td>
                            <td><%# Eval("NumberOfSectionsPerGroup") %></td>
                            <td>
                                <form>
                                    <input type="submit" data-coursename="<%# Eval("CourseName") %>" data-coursesemesterid="<%# Eval("Id") %>" value="Remove" class="RemoveCourseFromSemester" />
                                </form>
                            </td>
                        </tr>

                    </ItemTemplate>
                </asp:Repeater>



            </table>
        </div>

        <div id="10th_term2" class="tabcontent">
            <table class="CoursesOfTerm" id="SelectedCoursesTerm10">
                <tr>
                    <th>Course Name</th>
                    <th>Number Of Groups</th>
                    <th>Number of sections per group</th>
                    <th></th>
                </tr>

                <asp:Repeater runat="server" ID="SelectedTerm10">
                    <ItemTemplate>

                        <tr>
                            <td><%# Eval("CourseName") %></td>
                            <td><%# Eval("NumberOfGroups") %></td>
                            <td><%# Eval("NumberOfSectionsPerGroup") %></td>
                            <td>
                                <form>
                                    <input type="submit" data-coursename="<%# Eval("CourseName") %>" data-coursesemesterid="<%# Eval("Id") %>" value="Remove" class="RemoveCourseFromSemester" />
                                </form>
                            </td>
                        </tr>

                    </ItemTemplate>
                </asp:Repeater>



            </table>
        </div>
    </div>



</asp:Content>
