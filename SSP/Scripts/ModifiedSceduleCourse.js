
var CourseSemesterID;

jQuery(document).ready(function ($) {
    $('.SceduleCourse').on('click', function (event) {
        event.preventDefault();
        CourseSemesterID = $(this).attr('data-coursesemesterid');
        $.ajax({
            type: "POST",
            url: "GeneralScedulerHandler.ashx",
            data: { Method: "GetCourseSemesterInfo", CourseSemesterId: CourseSemesterID },
            success: function (data) {
                var result = $.parseJSON(data);
                var NumberOfGroups = result.NumberOfGroups;
                document.getElementById('ContainerForTabsOfGroups').innerHTML = FormTabs(NumberOfGroups);
                document.getElementById("ContainerForTimeTable").style.display = "none";
            }
        });



    });
});


function FormDoctorsList(Data) {
    var Options;
    var result = $.parseJSON(Data);
    Options += '<option value=""></option>';
    $.each(result, function (index, object) {
        Options += '<option value="' + object.Id + '">' + object.Name + '</option>';
    });
    var HeaderTag = '<div class="form-group">' +
                                '<label class="col-md-2 control-label">Doctor</label>' +
                                '<div class="col-md-10">' +
                                    '<select onchange="AddNewDoctorList();" class="form-control DoctorOfGroup">' +
                                    Options +
                                    '</select>' +
                                '</div>' +
                            '</div>';
    return HeaderTag;
}


function OpenSceduler(evt, GroupNumber) {

    $('.period').off('click');

    var i, tabcontent, tablinks;
    tabcontent = document.getElementsByClassName("tabcontentSceduler");
    for (i = 0; i < tabcontent.length; i++) {
        tabcontent[i].style.display = "none";
    }
    tablinks = document.getElementsByClassName("tablinksSceduler");
    for (i = 0; i < tablinks.length; i++) {
        tablinks[i].className = tablinks[i].className.replace(" active", "");
    }
    document.getElementById("ContainerForTimeTable").style.display = "block";
    evt.currentTarget.className += " active";
    var AllDoctorsList = "";

    $('.period').each(function () {
        $(this).text("");
    });

    GetAllInfoAboutGroupOfCourseSemester(CourseSemesterID, GroupNumber);


    $('.period').on('click', function (event) {
        event.preventDefault();
        var day = $(this).attr('data-day');
        var period = $(this).attr('data-period');

        if ($(this).html() != "") {
            GetPeriodInfo(CourseSemesterID, GroupNumber, day, period);
            return;
        }


        $('.InputPeriodPopUp').addClass('Show');



        $(document).keyup(function (event) {
            if (event.keyCode == 27) {
                $('#TypeOfPeriod').val('-1');
                $('#ContainerForDesiredAddedComponenets').empty();
                $('#PlaceOfPeriod').val("");
                $('.InputPeriodPopUp').removeClass("Show");
                $("#PeriodInfoForm").off('submit');
                $('#TypeOfPeriod').off('change');
                return;
            }
        });




        $('#PeriodInfoForm').on('submit', function (event) {
            event.preventDefault();

            ///////////////////////check that the number of section enterd is in range////////////////////////////
            if ($('#TypeOfPeriod').val() == 2 || $('#TypeOfPeriod').val() == 3) {
                var flagToContinue = 1;
                var SectionNumberEntered = $('#SectionNumber').val();
                $.ajax({
                    type: "POST",
                    async: false,
                    url: "GeneralScedulerHandler.ashx",
                    data: { Method: "CheckSectionNumberInRange", CourseSemesterID: CourseSemesterID, SectionNumberEntered: SectionNumberEntered },
                    success: function (data) {
                        var result = $.parseJSON(data);
                        if (result == -1) {
                            alert('Section number is not in range');
                            flagToContinue = 0;
                        }
                    }
                });
                if (flagToContinue == 0) {
                    return;
                }

            }

            //////////////////////////////////////////////////////////////////////////////////////////////////////

            ////////////////////check if doctor have overlaps///////////////////////////////////
            ////////////////////////////////////////////////////////////////////////////////////

            if (CheckIfDoctorHaveOverLaps(day, period) == false) {

                var NewLectureInsertedID;
                var PeriodType = document.getElementById('PeriodInfoForm').PeriodType.value;
                var PeriodCapacity = document.getElementById('PeriodInfoForm').PeriodCapacity.value;
                var SectionNumber;
                if (PeriodType == 2 || PeriodType == 3) {
                    SectionNumber = $('#SectionNumber').val();
                }
                else {
                    SectionNumber = 0;
                }



                var PeriodPlace = document.getElementById('PeriodInfoForm').PeriodPlace.value;
                $.ajax({
                    type: "POST",
                    async: false,
                    url: "GeneralScedulerHandler.ashx",
                    data: { Method: "AddLectureToGroup", CourseSemesterId: CourseSemesterID, GroupNumber: GroupNumber, PeriodDay: day, PeriodNumber: period, PeriodPlace: PeriodPlace, PeriodType: PeriodType, PeriodCapacity: PeriodCapacity, SectionNumber: SectionNumber },
                    success: function (Data) {
                        var result = $.parseJSON(Data);
                        if (result == -1) {
                            alert('Sorry! No room for new period of this type');
                            ///////////////close pop up message////////////////////
                            $('#TypeOfPeriod').val('-1');
                            $('#ContainerForDesiredAddedComponenets').empty();
                            $('#PlaceOfPeriod').val("");
                            $('.InputPeriodPopUp').removeClass("Show");
                            $("#PeriodInfoForm").off('submit');
                            $('#TypeOfPeriod').off('change');
                            ///////////////////////////////////////////////////////
                            return;
                        }
                        NewLectureInsertedID = result;

                        if ($('.DoctorOfGroup').length > 1) {
                            $('.DoctorOfGroup').each(function (index, element) {
                                if (element.value != "") {

                                    $.ajax({
                                        type: "POST",
                                        url: "GeneralScedulerHandler.ashx",
                                        data: { Method: "AssignDoctorToLecture", LectureOfGroupsID: NewLectureInsertedID, DoctorID: element.value },
                                        success: function () {
                                            alert('Doctor Assigned to lecture successfully');

                                            GetAllInfoAboutGroupOfCourseSemester(CourseSemesterID, GroupNumber);




                                            ///////////////close pop up message////////////////////
                                            $('#TypeOfPeriod').val('-1');
                                            $('#ContainerForDesiredAddedComponenets').empty();
                                            $('#PlaceOfPeriod').val("");
                                            $('.InputPeriodPopUp').removeClass("Show");
                                            $("#PeriodInfoForm").off('submit');
                                            $('#TypeOfPeriod').off('change');
                                            ///////////////////////////////////////////////////////
                                            return;
                                        }
                                    });

                                }
                            });
                        } else {

                            GetAllInfoAboutGroupOfCourseSemester(CourseSemesterID, GroupNumber);

                            ///////////////close pop up message////////////////////
                            $('#TypeOfPeriod').val('-1');
                            $('#ContainerForDesiredAddedComponenets').empty();
                            $('#PlaceOfPeriod').val("");
                            $('.InputPeriodPopUp').removeClass("Show");
                            $("#PeriodInfoForm").off('submit');
                            $('#TypeOfPeriod').off('change');
                            ///////////////////////////////////////////////////////
                            return;
                        }


                    }
                });
                GetAllInfoAboutGroupOfCourseSemester(CourseSemesterID, GroupNumber);
                ///////////////close pop up message////////////////////
                $('#TypeOfPeriod').val('-1');
                $('#ContainerForDesiredAddedComponenets').empty();
                $('#PlaceOfPeriod').val("");
                $('.InputPeriodPopUp').removeClass("Show");
                $("#PeriodInfoForm").off('submit');
                $('#TypeOfPeriod').off('change');
                ///////////////////////////////////////////////////////
                return;
            }
        });





        $('#TypeOfPeriod').on('change', function (e) {
            //////////////////// if section or lab => get section number////////////////////////
            var ValueOfThePeriodSelected = this.value;
            if (ValueOfThePeriodSelected == 2 || ValueOfThePeriodSelected == 3) {
                document.getElementById("ContainerForSectionNumber").style.display = "block";
            } else {
                document.getElementById("ContainerForSectionNumber").style.display = "none";
            }
            ////////////////////////////////////////////////////////////////////////////////////
            var flag = true;
            var DoctorsOfGroup = $('.DoctorOfGroup');
            DoctorsOfGroup.each(function (index, element) {
                if (element.value == "") {
                    flag = false;
                }
            });
            if (flag == false) {
                return;
            }
            var optionSelected = $("option:selected", this);
            var valueSelected = this.value;
            if (valueSelected != -1) {
                $.ajax({
                    type: "POST",
                    url: "GeneralScedulerHandler.ashx",
                    data: { Method: "GetAllDoctors" },
                    success: function (data) {
                        AllDoctorsList = FormDoctorsList(data);
                        $('#ContainerForDesiredAddedComponenets').append(AllDoctorsList);
                    }
                });
            }
        });
    });

}

function FormTabs(NumberOfGroups) {
    var Result = "";
    for (var i = 0; i < NumberOfGroups; i++) {
        var GroupNumberToWrite = i + 1;
        Result += '<a href="javascript:void(0)" class="tablinksSceduler" onclick="OpenSceduler(event,' + GroupNumberToWrite + ')">Group-' + GroupNumberToWrite + '</a>';
    }
    return Result;
}

function CheckIfDoctorHaveOverLaps(PeriodDay, PeriodNumber) {

    ///////////////////// check if doctor have overlap///////////////////////////
    var flag = 0;
    if ($('.DoctorOfGroup').length > 1) {
        $('.DoctorOfGroup').each(function (index, element) {
            if (element.value != "") {

                $.ajax({
                    type: "POST",
                    async: false,
                    url: "GeneralScedulerHandler.ashx",
                    data: { Method: "CheckDoctorOverLap", DoctorID: element.value, PeriodDay: PeriodDay, PeriodNumber: PeriodNumber },
                    success: function (data) {
                        var result = $.parseJSON(data);
                        if (result == -1) {
                            var x = element[element.selectedIndex];
                            alert('Dr. ' + element[element.selectedIndex].text + ' have overlaps');
                            flag = 1;
                        }
                    }
                });

            }
        });
        if (flag == 1) {
            return true;
        } else {
            return false;
        }
    }
    return false;
    /////////////////////////////////////////////////////////////////////////////

}


function AddNewDoctorList() {
    var flag = true;
    $('.DoctorOfGroup').each(function (index, element) {
        if (element.value == "") {
            flag = false;
        }
    });
    if (flag == true) {

        $.ajax({
            type: "POST",
            url: "GeneralScedulerHandler.ashx",
            data: { Method: "GetAllDoctors" },
            success: function (data) {
                AllDoctorsList = FormDoctorsList(data);
                $('#ContainerForDesiredAddedComponenets').append(AllDoctorsList);
            }
        });

    }
}

function GetAllInfoAboutGroupOfCourseSemester(CourseSemesterID, GroupNumber) {

    $('.period').each(function () {
        $(this).text("");
    });


    $.ajax({
        type: "POST",
        url: "GeneralScedulerHandler.ashx",
        data: { Method: "GetAllLecturesOfGroupOfCourseSemester", CourseSemesterID: CourseSemesterID, GroupNumber: GroupNumber },
        success: function (data) {

            var result = $.parseJSON(data);
            $.each(result, function (index, Element) {
                var AllPeriods = $('.period');
                AllPeriods.each(function () {
                    var PeriodObject = $(this);
                    if (Element.PeriodDay == PeriodObject.attr('data-day') && Element.PeriodNumber == PeriodObject.attr('data-period')) {
                        if (Element.PeriodType == 1) {
                            PeriodObject.text('Lecture');
                        }
                        else if (Element.PeriodType == 2) {
                            PeriodObject.text('Section');
                        }
                        else if (Element.PeriodType == 3) {
                            PeriodObject.text('Lab');
                        }
                    }
                });
            });

        }
    });
}



function GetPeriodInfo(CourseSemesterID, GroupNumber, Day, Period) {

    alert('now trying to fetch the data!');
    $.ajax({
        type: "POST",
        url: "GeneralScedulerHandler.ashx",
        data: { Method: "GetPeriodInfo", CourseSemesterID: CourseSemesterID, GroupNumber: GroupNumber, Day: Day, Period: Period },
        success: function (data) {

            var result = $.parseJSON(data);
            $('#TypeOfPeriodViewedToAdmin').text(result.Type);
            $.each(result.Doctors, function (index, value) {
                $('#ContainerForManagersOfPeriod').append(
                    '<div class="form-group">' +
                    '<label class="col-md-2 control-label">Doctor</label>' +
                    '<div class="col-md-10">' +
                    '<label class="form-control">' + value.Name + '</label>' +
                    '</div>' +
                    '</div>'
                    );
            });
            $('#PlaceOfPeriodViewedToAdmin').text(result.Place);
            $('#CapacityOfPeriodViewedToAdmin').text(result.Capacity);

            if (result.Type != "Lecture") {
                document.getElementById("SectionNumberViewedToAdminContainer").style.display = "block";
                $("#SectionNumberOfPeriodViewedToAdmin").text(result.SectionNumber);
            }

            $('#RemovePeriodSubmitBtn').on('click', function (event) {

                event.preventDefault();


                $.ajax({
                    type: "POST",
                    url: "GeneralScedulerHandler.ashx",
                    data: { Method: "RemoveLectureByID", LectureID: result.LectureID },
                    success: function () {

                        GetAllInfoAboutGroupOfCourseSemester(CourseSemesterID, GroupNumber);
                        /////////////////close form///////////////////////
                        $('#ContainerForManagersOfPeriod').empty();
                        $('#TypeOfPeriodViewedToAdmin').val("");
                        $("#SectionNumberOfPeriodViewedToAdmin").val("");
                        document.getElementById("SectionNumberViewedToAdminContainer").style.display = "none";
                        $('#PlaceOfPeriodViewedToAdmin').val("");
                        $('.ViewPeriodInfo').removeClass("Show");
                        $('#RemovePeriodSubmitBtn').off('click');
                        //////////////////////////////////////////////////
                    }
                });

            });

            $('.ViewPeriodInfo').addClass('Show');

            $(document).keyup(function (event) {
                if (event.keyCode == 27) {
                    $('#ContainerForManagersOfPeriod').empty();
                    $('#TypeOfPeriodViewedToAdmin').val("");
                    $("#SectionNumberOfPeriodViewedToAdmin").val("");
                    document.getElementById("SectionNumberViewedToAdminContainer").style.display = "none";
                    $('#PlaceOfPeriodViewedToAdmin').val("");
                    $('.ViewPeriodInfo').removeClass("Show");
                    $('#RemovePeriodSubmitBtn').off('click');
                }
            });







        }
    });

}