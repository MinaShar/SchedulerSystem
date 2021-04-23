<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Admin.Master" AutoEventWireup="true" CodeBehind="ModifiedScheduleCourse.aspx.cs" Inherits="SSP.Admin.ModifiedScheduleCourse" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <link href="../Content/TimeTable.css" rel="stylesheet" />
    <script src="../Scripts/HandleSchadulerTimeTable.js"></script>
    <link href="../Content/CustomForCoursesOfSemester.css" rel="stylesheet" />
    <link href="../Content/Tabs.less" rel="stylesheet" />
    <script src="../Scripts/ModifiedSceduleCourse.js"></script>
    <link href="../Content/InputPeriodInfoPOPUP.css" rel="stylesheet" />
    <link href="../Content/RemovePeriodInfoPOPUP.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">


    <h2>Select Course to Scedule.</h2>

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
            <table class="CoursesOfTerm" id="SelectedCoursesTerm1">
                <tr>
                    <th>Course Name</th>
                    <th>Number Of Lectures</th>
                    <th>Number Of Sections</th>
                    <th>Number Of Labs</th>
                    <th>Number Of Groups</th>
                    <th>Sections per Group</th>
                    <th></th>
                </tr>

                <asp:Repeater runat="server" ID="SelectedTerm1">
                    <ItemTemplate>

                        <tr>
                            <td><%# Eval("CourseName") %></td>
                            <td><%# Eval("NumberOfLectures") %></td>
                            <td><%# Eval("NumberOfSections") %></td>
                            <td><%# Eval("NumberOfLabs") %></td>
                            <td><%# Eval("NumberOfGroups") %></td>
                            <td><%# Eval("NumberOfSectionsPerGroup") %></td>
                            <td>
                                <form>
                                    <input type="submit" data-coursename="<%# Eval("CourseName") %>" data-coursesemesterid="<%# Eval("Id") %>" value="Scedule" class="SceduleCourse" />
                                </form>
                            </td>
                        </tr>

                    </ItemTemplate>
                </asp:Repeater>



            </table>
        </div>

        <div id="2nd_term" class="tabcontent">
            <table class="CoursesOfTerm" id="SelectedCoursesTerm2">
                <tr>
                    <th>Course Name</th>
                    <th>Number Of Lectures</th>
                    <th>Number Of Sections</th>
                    <th>Number Of Labs</th>
                    <th>Number Of Groups</th>
                    <th>Sections per Group</th>
                    <th></th>
                </tr>

                <asp:Repeater runat="server" ID="SelectedTerm2">
                    <ItemTemplate>

                        <tr>
                            <td><%# Eval("CourseName") %></td>
                            <td><%# Eval("NumberOfLectures") %></td>
                            <td><%# Eval("NumberOfSections") %></td>
                            <td><%# Eval("NumberOfLabs") %></td>
                            <td><%# Eval("NumberOfGroups") %></td>
                            <td><%# Eval("NumberOfSectionsPerGroup") %></td>
                            <td>
                                <form>
                                    <input type="submit" data-coursename="<%# Eval("CourseName") %>" data-coursesemesterid="<%# Eval("Id") %>" value="Scedule" class="SceduleCourse" />
                                </form>
                            </td>
                        </tr>

                    </ItemTemplate>
                </asp:Repeater>



            </table>
        </div>

        <div id="3rd_term" class="tabcontent">
            <table class="CoursesOfTerm" id="SelectedCoursesTerm3">
                <tr>
                    <th>Course Name</th>
                    <th>Number Of Lectures</th>
                    <th>Number Of Sections</th>
                    <th>Number Of Labs</th>
                    <th>Number Of Groups</th>
                    <th>Sections per Group</th>
                    <th></th>
                </tr>

                <asp:Repeater runat="server" ID="SelectedTerm3">
                    <ItemTemplate>

                        <tr>
                            <td><%# Eval("CourseName") %></td>
                            <td><%# Eval("NumberOfLectures") %></td>
                            <td><%# Eval("NumberOfSections") %></td>
                            <td><%# Eval("NumberOfLabs") %></td>
                            <td><%# Eval("NumberOfGroups") %></td>
                            <td><%# Eval("NumberOfSectionsPerGroup") %></td>
                            <td>
                                <form>
                                    <input type="submit" data-coursename="<%# Eval("CourseName") %>" data-coursesemesterid="<%# Eval("Id") %>" value="Scedule" class="SceduleCourse" />
                                </form>
                            </td>
                        </tr>

                    </ItemTemplate>
                </asp:Repeater>



            </table>
        </div>
        <div id="4th_term" class="tabcontent">
            <table class="CoursesOfTerm" id="SelectedCoursesTerm4">
                <tr>
                    <th>Course Name</th>
                    <th>Number Of Lectures</th>
                    <th>Number Of Sections</th>
                    <th>Number Of Labs</th>
                    <th>Number Of Groups</th>
                    <th>Sections per Group</th>
                    <th></th>
                </tr>

                <asp:Repeater runat="server" ID="SelectedTerm4">
                    <ItemTemplate>

                        <tr>
                            <td><%# Eval("CourseName") %></td>
                            <td><%# Eval("NumberOfLectures") %></td>
                            <td><%# Eval("NumberOfSections") %></td>
                            <td><%# Eval("NumberOfLabs") %></td>
                            <td><%# Eval("NumberOfGroups") %></td>
                            <td><%# Eval("NumberOfSectionsPerGroup") %></td>
                            <td>
                                <form>
                                    <input type="submit" data-coursename="<%# Eval("CourseName") %>" data-coursesemesterid="<%# Eval("Id") %>" value="Scedule" class="SceduleCourse" />
                                </form>
                            </td>
                        </tr>

                    </ItemTemplate>
                </asp:Repeater>



            </table>
        </div>

        <div id="5th_term" class="tabcontent">
            <table class="CoursesOfTerm" id="SelectedCoursesTerm5">
                <tr>
                    <th>Course Name</th>
                    <th>Number Of Lectures</th>
                    <th>Number Of Sections</th>
                    <th>Number Of Labs</th>
                    <th>Number Of Groups</th>
                    <th>Sections per Group</th>
                    <th></th>
                </tr>

                <asp:Repeater runat="server" ID="SelectedTerm5">
                    <ItemTemplate>

                        <tr>
                            <td><%# Eval("CourseName") %></td>
                            <td><%# Eval("NumberOfLectures") %></td>
                            <td><%# Eval("NumberOfSections") %></td>
                            <td><%# Eval("NumberOfLabs") %></td>
                            <td><%# Eval("NumberOfGroups") %></td>
                            <td><%# Eval("NumberOfSectionsPerGroup") %></td>
                            <td>
                                <form>
                                    <input type="submit" data-coursename="<%# Eval("CourseName") %>" data-coursesemesterid="<%# Eval("Id") %>" value="Scedule" class="SceduleCourse" />
                                </form>
                            </td>
                        </tr>

                    </ItemTemplate>
                </asp:Repeater>



            </table>
        </div>

        <div id="6th_term" class="tabcontent">
            <table class="CoursesOfTerm" id="SelectedCoursesTerm6">
                <tr>
                    <th>Course Name</th>
                    <th>Number Of Lectures</th>
                    <th>Number Of Sections</th>
                    <th>Number Of Labs</th>
                    <th>Number Of Groups</th>
                    <th>Sections per Group</th>
                    <th></th>
                </tr>

                <asp:Repeater runat="server" ID="SelectedTerm6">
                    <ItemTemplate>

                        <tr>
                            <td><%# Eval("CourseName") %></td>
                            <td><%# Eval("NumberOfLectures") %></td>
                            <td><%# Eval("NumberOfSections") %></td>
                            <td><%# Eval("NumberOfLabs") %></td>
                            <td><%# Eval("NumberOfGroups") %></td>
                            <td><%# Eval("NumberOfSectionsPerGroup") %></td>
                            <td>
                                <form>
                                    <input type="submit" data-coursename="<%# Eval("CourseName") %>" data-coursesemesterid="<%# Eval("Id") %>" value="Scedule" class="SceduleCourse" />
                                </form>
                            </td>
                        </tr>

                    </ItemTemplate>
                </asp:Repeater>



            </table>
        </div>
        <div id="7th_term" class="tabcontent">
            <table class="CoursesOfTerm" id="SelectedCoursesTerm7">
                <tr>
                    <th>Course Name</th>
                    <th>Number Of Lectures</th>
                    <th>Number Of Sections</th>
                    <th>Number Of Labs</th>
                    <th>Number Of Groups</th>
                    <th>Sections per Group</th>
                    <th></th>
                </tr>

                <asp:Repeater runat="server" ID="SelectedTerm7">
                    <ItemTemplate>

                        <tr>
                            <td><%# Eval("CourseName") %></td>
                            <td><%# Eval("NumberOfLectures") %></td>
                            <td><%# Eval("NumberOfSections") %></td>
                            <td><%# Eval("NumberOfLabs") %></td>
                            <td><%# Eval("NumberOfGroups") %></td>
                            <td><%# Eval("NumberOfSectionsPerGroup") %></td>
                            <td>
                                <form>
                                    <input type="submit" data-coursename="<%# Eval("CourseName") %>" data-coursesemesterid="<%# Eval("Id") %>" value="Scedule" class="SceduleCourse" />
                                </form>
                            </td>
                        </tr>

                    </ItemTemplate>
                </asp:Repeater>



            </table>
        </div>
        <div id="8th_term" class="tabcontent">
            <table class="CoursesOfTerm" id="SelectedCoursesTerm8">
                <tr>
                    <th>Course Name</th>
                    <th>Number Of Lectures</th>
                    <th>Number Of Sections</th>
                    <th>Number Of Labs</th>
                    <th>Number Of Groups</th>
                    <th>Sections per Group</th>
                    <th></th>
                </tr>

                <asp:Repeater runat="server" ID="SelectedTerm8">
                    <ItemTemplate>

                        <tr>
                            <td><%# Eval("CourseName") %></td>
                            <td><%# Eval("NumberOfLectures") %></td>
                            <td><%# Eval("NumberOfSections") %></td>
                            <td><%# Eval("NumberOfLabs") %></td>
                            <td><%# Eval("NumberOfGroups") %></td>
                            <td><%# Eval("NumberOfSectionsPerGroup") %></td>
                            <td>
                                <form>
                                    <input type="submit" data-coursename="<%# Eval("CourseName") %>" data-coursesemesterid="<%# Eval("Id") %>" value="Scedule" class="SceduleCourse" />
                                </form>
                            </td>
                        </tr>

                    </ItemTemplate>
                </asp:Repeater>



            </table>
        </div>

        <div id="9th_term" class="tabcontent">
            <table class="CoursesOfTerm" id="SelectedCoursesTerm9">
                <tr>
                    <th>Course Name</th>
                    <th>Number Of Lectures</th>
                    <th>Number Of Sections</th>
                    <th>Number Of Labs</th>
                    <th>Number Of Groups</th>
                    <th>Sections per Group</th>
                    <th></th>
                </tr>

                <asp:Repeater runat="server" ID="SelectedTerm9">
                    <ItemTemplate>

                        <tr>
                            <td><%# Eval("CourseName") %></td>
                            <td><%# Eval("NumberOfLectures") %></td>
                            <td><%# Eval("NumberOfSections") %></td>
                            <td><%# Eval("NumberOfLabs") %></td>
                            <td><%# Eval("NumberOfGroups") %></td>
                            <td><%# Eval("NumberOfSectionsPerGroup") %></td>
                            <td>
                                <form>
                                    <input type="submit" data-coursename="<%# Eval("CourseName") %>" data-coursesemesterid="<%# Eval("Id") %>" value="Scedule" class="SceduleCourse" />
                                </form>
                            </td>
                        </tr>

                    </ItemTemplate>
                </asp:Repeater>



            </table>
        </div>

        <div id="10th_term" class="tabcontent">
            <table class="CoursesOfTerm" id="SelectedCoursesTerm10">
                <tr>
                    <th>Course Name</th>
                    <th>Number Of Lectures</th>
                    <th>Number Of Sections</th>
                    <th>Number Of Labs</th>
                    <th>Number Of Groups</th>
                    <th>Sections per Group</th>
                    <th></th>
                </tr>

                <asp:Repeater runat="server" ID="SelectedTerm10">
                    <ItemTemplate>

                        <tr>
                            <td><%# Eval("CourseName") %></td>
                            <td><%# Eval("NumberOfLectures") %></td>
                            <td><%# Eval("NumberOfSections") %></td>
                            <td><%# Eval("NumberOfLabs") %></td>
                            <td><%# Eval("NumberOfGroups") %></td>
                            <td><%# Eval("NumberOfSectionsPerGroup") %></td>
                            <td>
                                <form>
                                    <input type="submit" data-coursename="<%# Eval("CourseName") %>" data-coursesemesterid="<%# Eval("Id") %>" value="Scedule" class="SceduleCourse" />
                                </form>
                            </td>
                        </tr>

                    </ItemTemplate>
                </asp:Repeater>



            </table>
        </div>


    </div>

    <div id="ViewPeriodInfo" class="ViewPeriodInfo">
        <h3>Period Info.</h3>
        <div class="form-group">
            <label class="col-md-2 control-label">Type</label>
            <div class="col-md-10">
                <label class="form-control" id="TypeOfPeriodViewedToAdmin"></label>
            </div>
        </div>

        <div id="ContainerForManagersOfPeriod">
        </div>

        <div class="form-group">
            <label class="col-md-2 control-label">Place</label>
            <div class="col-md-10">
                <label class="form-control" id="PlaceOfPeriodViewedToAdmin"></label>
            </div>
        </div>

        <div class="form-group">
            <label class="col-md-2 control-label">Capacity</label>
            <div class="col-md-10">
                <label class="form-control" id="CapacityOfPeriodViewedToAdmin"></label>
            </div>
        </div>

        <div class="form-group" id="SectionNumberViewedToAdminContainer" style="display:none;">
            <label class="col-md-2 control-label">Section</label>
            <div class="col-md-10">
                <label class="form-control" id="SectionNumberOfPeriodViewedToAdmin"></label>
            </div>
        </div>

        <form id="RemovePeriod">
            <div class="form-group">
                <div class="col-md-10">
                    <input type="submit" id="RemovePeriodSubmitBtn" value="Remove" class="form-control" />
                </div>
            </div>
        </form>

    </div>






    <div id="PeriodInfoScript" class="InputPeriodPopUp">
        <form id="PeriodInfoForm">
            <h3>Period Info.</h3>
            <div class="form-group" id="TypeOfPeriodFormGroup">
                <label class="col-md-2 control-label">Type</label>
                <div class="col-md-10">
                    <select class="form-control" id="TypeOfPeriod" name="PeriodType">
                        <option value="-1" selected="selected"></option>
                        <option value="1">lecture</option>
                        <option value="2">section</option>
                        <option value="3">lab</option>
                    </select>
                </div>
            </div>

            <div id="ContainerForDesiredAddedComponenets">
            </div>

            <div class="form-group">
                <label class="col-md-2 control-label">Place</label>
                <div class="col-md-10">
                    <input type="text" id="PlaceOfPeriod" name="PeriodPlace" class="form-control" />
                </div>
            </div>

            <div class="form-group">
                <label class="col-md-2 control-label">Capacity</label>
                <div class="col-md-10">
                    <input type="text" id="PeriodCapacity" name="PeriodCapacity" class="form-control" />
                </div>
            </div>

            <div id="ContainerForSectionNumber" style="display:none">
                <div class="form-group">
                    <label class="col-md-2 control-label">Section Number</label>
                    <div class="col-md-10">
                        <input type="text" id="SectionNumber" name="SectionNumber" class="form-control" />
                    </div>
                </div>
            </div>

            <div class="form-group">
                <div class="col-md-10">
                    <input type="submit" value="Save" class="form-control" />
                </div>
            </div>

        </form>
    </div>


    <div>
        <div id="ContainerForTabsOfGroups" class="tab" style="display: inline-block">
            <%--<a href="javascript:void(0)" class="tablinksSceduler" onclick="OpenSceduler(event,1)">1st term</a>
            <a href="javascript:void(0)" class="tablinksSceduler" onclick="OpenSceduler(event,2)">2nd term</a>--%>
        </div>

        <div class="AllGroupsOfCourse">



            <div class="tabcontentSceduler" id="ContainerForTimeTable">

                <table style="width: 100%; text-align: center; height: 293px;">

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
                        <th>10:10 - 11:40</td>
        
                            <td class="period" data-day="1" data-period="2"></td>
                            <td class="period" data-day="2" data-period="2"></td>
                            <td class="period" data-day="3" data-period="2"></td>
                            <td class="period" data-day="4" data-period="2"></td>
                            <td class="period" data-day="5" data-period="2"></td>
                            <td class="period" data-day="6" data-period="2"></td>
                    </tr>

                    <tr>
                        <th>11:50 - 01:30</td>
        
                            <td class="period" data-day="1" data-period="3"></td>
                            <td class="period" data-day="2" data-period="3"></td>
                            <td class="period" data-day="3" data-period="3"></td>
                            <td class="period" data-day="4" data-period="3"></td>
                            <td class="period" data-day="5" data-period="3"></td>
                            <td class="period" data-day="6" data-period="3"></td>
                    </tr>

                    <tr>
                        <th>01:40 - 03:10</td>
        
                            <td class="period" data-day="1" data-period="4"></td>
                            <td class="period" data-day="2" data-period="4"></td>
                            <td class="period" data-day="3" data-period="4"></td>
                            <td class="period" data-day="4" data-period="4"></td>
                            <td class="period" data-day="5" data-period="4"></td>
                            <td class="period" data-day="6" data-period="4"></td>
                    </tr>

                    <tr>
                        <th>03:20 - 05:00</td>
        
                            <td class="period" data-day="1" data-period="5"></td>
                            <td class="period" data-day="2" data-period="5"></td>
                            <td class="period" data-day="3" data-period="5"></td>
                            <td class="period" data-day="4" data-period="5"></td>
                            <td class="period" data-day="5" data-period="5"></td>
                            <td class="period" data-day="6" data-period="5"></td>
                    </tr>
                </table>

            </div>

        </div>

    </div>




</asp:Content>
