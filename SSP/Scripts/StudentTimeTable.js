$(document).ready(function () {
    GetStudentTimeTable($("#CurrentStudentID").val());
});

function GetStudentTimeTable(StudentID) {

    $('.period').each(function (index, element) {
        element.innerHTML = "";
    });

    $.ajax({
        type: "POST",
        async: false,
        url: "CStudentHandler.ashx",
        data: { Method: "GetStudentTimeTable", CurrentStudentID: StudentID },
        success: function (data) {
            var result = $.parseJSON(data);
            var AllPeriods = $('.period');
            $.each(result, function (index, element) {
                AllPeriods.each(function (index1, element1) {
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