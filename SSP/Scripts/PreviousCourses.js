function GetStudents(LectureOfGroupID) {

    $(".StudentsOfLectureOFGroup").css('display', 'none');
    $("#Students-" + LectureOfGroupID).html("");

    $.ajax({
        type: "POST",
        async: false,
        url: "InstructorHandler.ashx",
        data: { Method: "GetStudentsOfPreviousCourse", LectureOfGroupID: LectureOfGroupID },
        success: function (data) {
            var Results = $.parseJSON(data);
            $("#Students-" + LectureOfGroupID).append("<table style='position:relative;align-items:stretch;width:100%;'><tr><th>Student ID</th><th>Student Name</th><th>Grade</th></tr></table>");
            $.each(Results, function (index, element) {
                if (element.Grade != -1) {
                    $("#Students-" + LectureOfGroupID + " > table").append("<tr><td>" + element.StudentId + "</td><td>" + element.StudentName + "</td><td>" + element.Grade + "</td></tr>");
                }
                else {
                    $("#Students-" + LectureOfGroupID + " > table").append("<tr><td>" + element.StudentId + "</td><td>" + element.StudentName + "</td><td>Not closed course</td></tr>");
                }
            });

            $("#Students-" + LectureOfGroupID).css('display', 'flex');
        }
    });

}