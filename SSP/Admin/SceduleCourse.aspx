<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Admin.Master" AutoEventWireup="true" CodeBehind="SceduleCourse.aspx.cs" Inherits="SSP.Admin.SceduleCourse" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../Content/TimeTable.css" rel="stylesheet" />
    <script src="../Scripts/HandleSchadulerTimeTable.js"></script>
    <link href="../Content/Tabs.less" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">


    <h2>Select Course to Scedule.</h2>

    <div class="tab" style="display:inline-block">
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

    <div id="1st_term" class="tabcontent">
        <h3>London</h3>
        <p>London is the capital city of England.</p>
    </div>

    <div id="2nd_term" class="tabcontent">
        <h3>Paris</h3>
        <p>Paris is the capital of France.</p>
    </div>

    <div id="3rd_term" class="tabcontent">
        <h3>Tokyo</h3>
        <p>Tokyo is the capital of Japan.</p>
    </div>
    <div id="4th_term" class="tabcontent">
        <h3>London</h3>
        <p>London is the capital city of England.</p>
    </div>

    <div id="5th_term" class="tabcontent">
        <h3>Paris</h3>
        <p>Paris is the capital of France.</p>
    </div>

    <div id="6th_term" class="tabcontent">
        <h3>Tokyo</h3>
        <p>Tokyo is the capital of Japan.</p>
    </div>
    <div id="7th_term" class="tabcontent">
        <h3>Tokyo</h3>
        <p>Tokyo is the capital of Japan.</p>
    </div>
    <div id="8th_term" class="tabcontent">
        <h3>London</h3>
        <p>London is the capital city of England.</p>
    </div>

    <div id="9th_term" class="tabcontent">
        <h3>Paris</h3>
        <p>Paris is the capital of France.</p>
    </div>

    <div id="10th_term" class="tabcontent">
        <h3>Tokyo</h3>
        <p>Tokyo is the capital of Japan.</p>
    </div>




    <form>
        <div class="row">
            <div class="col-md-8">
                <section id="loginForm">
                    <div class="form-horizontal">
                        <h2>Choose Course First.</h2>
                        <hr />

                        <div class="form-group">
                            <label class="col-md-2 control-label">Course Name</label>
                            <div class="col-md-10">
                                <label id="CourseName" class="form-control"></label>
                            </div>
                        </div>

                        <div class="form-group">
                            <label class="col-md-2 control-label">Number Of Groups.</label>
                            <div class="col-md-10">
                                <input type="text" id="NumberOfGroups" class="form-control" />
                            </div>
                        </div>


                        <div class="container" id="ContainerForGroups">

                            <div class="form-group">
                                <label class="col-md-2 control-label">Password</label>
                                <div class="col-md-10">
                                    <input type="password" class="form-control" />
                                </div>
                            </div>

                            
                            <div class="form-group">
                                <h4 style="margin-left:132px;">Lecture</h4>
                                <label class="col-md-2 control-label">Day</label>
                                <div class="col-md-10">
                                    <select class="form-control">
                                        <option>jhghhj</option>
                                        <option>fsdfsdf</option>
                                        <option>gdfgdfg</option>
                                    </select>
                                </div>
                                <label class="col-md-2 control-label">Period</label>
                                <div class="col-md-10">
                                    <select class="form-control">
                                        <option>jhghhj</option>
                                        <option>fsdfsdf</option>
                                        <option>gdfgdfg</option>
                                    </select>
                                </div>
                            </div>

                            <div class="form-group">
                                <label class="col-md-2 control-label">Department</label>
                                <div class="col-md-10">
                                    <select class="form-control">
                                        <asp:Repeater runat="server" ID="Departments">
                                            <ItemTemplate>
                                                <option value="<%# Eval("Id") %>"><%# Eval("Name") %></option>
                                            </ItemTemplate>
                                        </asp:Repeater>
                                    </select>
                                </div>
                            </div>

                        </div>


                        <div class="form-group">
                            <div class="col-md-offset-2 col-md-10">
                                <input id="SaveSchaduler" type="submit" value="Add" />
                            </div>
                        </div>
                    </div>
                    <p>
                        <asp:HyperLink runat="server" ID="RegisterHyperLink" ViewStateMode="Disabled">Register as a new user</asp:HyperLink>
                    </p>
                    <p>
                        <%-- Enable this once you have account confirmation enabled for password reset functionality
                    <asp:HyperLink runat="server" ID="ForgotPasswordHyperLink" ViewStateMode="Disabled">Forgot your password?</asp:HyperLink>
                        --%>
                    </p>
                </section>
            </div>

            <div class="col-md-4">
                <section id="socialLoginForm">
                </section>
            </div>
        </div>
    </form>


    <table style="width: 80%; text-align: center;">

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

            <td class="period" data-day="saturday" data-period="1"></td>
            <td class="period" data-day="sunday" data-period="1"></td>
            <td class="period" data-day="monday" data-period="1"></td>
            <td class="period" data-day="tuesday" data-period="1"></td>
            <td class="period" data-day="wednesday" data-period="1"></td>
            <td class="period" data-day="thursday" data-period="1"></td>

        </tr>

        <tr>
            <th>10:10 - 11:40</td>
        
            <td class="period" data-day="saturday" data-period="2"></td>
                <td class="period" data-day="sunday" data-period="2"></td>
                <td class="period" data-day="monday" data-period="2"></td>
                <td class="period" data-day="tuesday" data-period="2"></td>
                <td class="period" data-day="wednesday" data-period="2"></td>
                <td class="period" data-day="thursday" data-period="2"></td>
        </tr>

        <tr>
            <th>11:50 - 01:30</td>
        
            <td class="period" data-day="saturday" data-period="3"></td>
                <td class="period" data-day="sunday" data-period="3"></td>
                <td class="period" data-day="monday" data-period="3"></td>
                <td class="period" data-day="tuesday" data-period="3"></td>
                <td class="period" data-day="wednesday" data-period="3"></td>
                <td class="period" data-day="thursday" data-period="3"></td>
        </tr>

        <tr>
            <th>01:40 - 03:10</td>
        
            <td class="period" data-day="saturday" data-period="4"></td>
                <td class="period" data-day="sunday" data-period="4"></td>
                <td class="period" data-day="monday" data-period="4"></td>
                <td class="period" data-day="tuesday" data-period="4"></td>
                <td class="period" data-day="wednesday" data-period="4"></td>
                <td class="period" data-day="thursday" data-period="4"></td>
        </tr>

        <tr>
            <th>03:20 - 05:00</td>
        
            <td class="period" data-day="saturday" data-period="5"></td>
                <td class="period" data-day="sunday" data-period="5"></td>
                <td class="period" data-day="monday" data-period="5"></td>
                <td class="period" data-day="tuesday" data-period="5"></td>
                <td class="period" data-day="wednesday" data-period="5"></td>
                <td class="period" data-day="thursday" data-period="5"></td>
        </tr>
    </table>

</asp:Content>
