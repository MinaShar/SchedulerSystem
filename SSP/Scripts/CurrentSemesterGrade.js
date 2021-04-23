function GetGrades(CurrentStudentID) {

    $.ajax({
        type: "POST",
        async:false,
        url: "CStudentHandler.ashx",
        data: { Method: "GetCurrentSemesterGrade", CurrentStudentID: CurrentStudentID },
        success: function (data) {
            var results = $.parseJSON(data);
            $.each(results, function (index, element) {
                if (element.Grade != -1) {
                    $("#ResultsTable").append("<tr><td>" + element.CourseName + "</td><td>" + element.Grade + "</td></tr>");
                } else {
                    $("#ResultsTable").append("<tr><td>" + element.CourseName + "</td><td> Not Closed Course </td></tr>");
                }
            });
        }
    });

}