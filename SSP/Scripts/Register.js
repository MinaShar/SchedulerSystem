var Connection;
var Hub;
$(document).ready(function () {

    //GetStudentTimeTable(CurrentStudentID);


    Connection = $.hubConnection();
    Hub = Connection.createHubProxy('MyHub1');
    Hub.on('alert', function (i) {
        alert('value we get = ' + i);
    });
    Connection.start(function () {
        //Hub.invoke('Alert', 1);
        Hub.invoke('StudentArrived', $("#CurrentStudentIDTextBox").val());
    });

    Hub.on('AlertNewChangeInPeriod', function (LectureOfgroupID) {
        GetTheRemainingCapacityForPeriod(LectureOfgroupID);
    });

    /*
    $('.RegisterInPeriod').on('submit', function (event) {
        event.preventDefault();

        var form = $(this)[0];
        var INPUT = form.LectureOfGroupID;
        var CurrentStudentID = form.CurrentStudentID.value;
        var LectureOfGroupID = INPUT.getAttribute('data-lectureofgroupid');
        alert('Lecture of group id = ' + LectureOfGroupID + ' and current student ID = ' + CurrentStudentID);

        $.ajax({
            type: "POST",
            url: "CStudentHandler.ashx",
            data: { Method: "RegisterPeriod",CurrentStudentID:CurrentStudentID,LectureOfGroupID:LectureOfGroupID },
            success: function (data) {

            }
        });


    });*/
});

function GetStudentTimeTable(StudentID) {

    $('.period').each(function (index, element) {
        element.innerHTML = "";
        element.style.backgroundColor = "white";
    });

    $.ajax({
        type: "POST",
        async:false,
        url: "CStudentHandler.ashx",
        data: { Method: "GetStudentTimeTable", CurrentStudentID: StudentID },
        success: function (data) {
            var result = $.parseJSON(data);
            var AllPeriods = $('.period');
            $.each(result, function (index, element) {
                AllPeriods.each(function (index1,element1) {
                    if (element.PeriodDay == element1.getAttribute('data-day') && element.PeriodNumber == element1.getAttribute('data-period')) {
                        if (element.PeriodType == 1) {
                            element1.innerHTML = "Lecture";
                            $(this).css('background-color', 'antiquewhite');
                        } else if (element.PeriodType == 2) {
                            element1.innerHTML = "Section";
                            $(this).css('background-color', 'chocolate');
                        } else {
                            element1.innerHTML = "Lab";
                            $(this).css('background-color', 'coral');
                        }
                        element1.innerHTML = element1.innerHTML + '<br />' + element.CourseName + '<br />';
                        var i;
                        for (i = 0; i < element.DoctorsOfThePeriod.length; i++) {
                            element1.innerHTML += 'Dr.' + element.DoctorsOfThePeriod[i].Name + '<br />';
                        }
                        element1.innerHTML += 'G.' + element.GroupNumber + '<br />';
                        if (element.PeriodType != 1) {
                            element1.innerHTML += 'Section ' + element.SectionNumber + '<br />';
                        }
                        element1.innerHTML += element.Place + '<br />';
                    }
                });
            });
        }
    });

}

function RegisterNewPeriod(form) {
    alert('register NewPeriodForm called');
    var CurrentStudentID = form.CurrentStudentID.value;
    var LectureOfGroupID = form.LectureOfGroupID.value;


    $.ajax({
        type: "POST",
        async:false,
        url: "CStudentHandler.ashx",
        data: { Method: "RegisterPeriod", CurrentStudentID: CurrentStudentID, LectureOfGroupID: LectureOfGroupID },
        success: function (data) {
            var result = $.parseJSON(data);
            if (result.Flag == true) {
                GetStudentTimeTable(CurrentStudentID);
                document.getElementById("Handler-" + LectureOfGroupID).innerHTML = '<form class="RemoveRegisteration">' +
                                                                                   '<input type="hidden" name="CurrentStudentID" value="' + CurrentStudentID + '" />' +
                                                                                   '<input type="hidden" name="LectureOfGroupStudentID" value="' + result.LectureOfGroupStudentID + '" />' +
                                                                                   '<input type="submit" onclick="RemoveRegisteration(this.form);return false;" value="Remove" />' +
                                                                                   '</form>';
                GetTheRemainingCapacityForPeriod(LectureOfGroupID);
                Hub.invoke('AlertNewChangeInPeriod', LectureOfGroupID);

            } else {
                alert(result.ReasonWhyNot);
            }
        }
    });

    return false;
}

function RemoveRegisteration(form) {
    alert('remove registration form called successfully');

    var LectureOfGroupStudentID = form.LectureOfGroupStudentID.value;
    var CurrentStudentID = form.CurrentStudentID.value;

    $.ajax({
        type: "POST",
        async: false,
        url: "CStudentHandler.ashx",
        data: { Method: "RemoveRegistration", LectureOfGroupStudentID: LectureOfGroupStudentID },
        success: function (data) {
            var LectureOfGroupID = $.parseJSON(data);
            document.getElementById("Handler-" + LectureOfGroupID).innerHTML = '<form class="RegisterInPeriod">' +
                                                                               '<input type="hidden" name="CurrentStudentID" value="' + CurrentStudentID + '" />' +
                                                                               '<input type="hidden" name="LectureOfGroupID" value="' + LectureOfGroupID + '" />' +
                                                                               '<input type="submit" onclick="RegisterNewPeriod(this.form);return false;" value="Add" />' +
                                                                               '</form>';
            GetStudentTimeTable(CurrentStudentID);
            GetTheRemainingCapacityForPeriod(LectureOfGroupID);
            Hub.invoke('AlertNewChangeInPeriod', LectureOfGroupID);
        }
    });

    return false;
}

function GetTheRemainingCapacityForPeriod(LectureOfGroupID) {
    $.ajax({
        type: "POST",
        url: "CStudentHandler.ashx",
        data: { Method: "GetTheRemainingCapacityForPeriod", LectureOfGroupID: LectureOfGroupID },
        success: function (data) {
            var RemainingPlaces = $.parseJSON(data);
            document.getElementById("Capacity-" + LectureOfGroupID).innerHTML = RemainingPlaces;
        }
    });
}


function SaveRegistration(StudentID) {

    alert('save registration method called with ' + StudentID);
    $.ajax({
        type: "POST",
        url: "CStudentHandler.ashx",
        data: { Method: "SaveRegistration", StudentID: StudentID },
        success: function (data) {
            var Results = $.parseJSON(data);
            if (Results.Flag == true) {
                alert('Registration Saved Successfully');
            } else {
                alert(Results.ReasonWhyNot);
            }
        }
    });

}