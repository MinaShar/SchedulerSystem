var NumberOfRequiredAddedColumns;
var PublicCourseID;
var PublicStudentID;

function FormGradeHeader(CourseID) {

    PublicCourseID = CourseID;
    NumberOfRequiredAddedColumns = 0;
    $.ajax({
        type: "POST",
        async: false,
        url: "InstructorHandler.ashx",
        data: { Method: "GetCourseInfo", CourseID: CourseID },
        success: function (data) {
            var Course = $.parseJSON(data);
            if (Course.NumberOfLectures > 0) {
                $("#GradeTableHeader").append("<th>Lecture Grade</th>");
                NumberOfRequiredAddedColumns++;
            }
            if (Course.NumberOfSections > 0) {
                $("#GradeTableHeader").append("<th>Tutorial Grade</th>");
                NumberOfRequiredAddedColumns++;
            }
            if (Course.NumberOfLabs > 0) {
                $("#GradeTableHeader").append("<th>Lab Grade</th>");
                NumberOfRequiredAddedColumns++;
            }

            $("#GradeTableHeader").append("<th>Final Grade</th>");
        }
    });
}

function GetStudents(CourseID, DoctorID, GroupNumber) {

    var TableRow;
    if (NumberOfRequiredAddedColumns == 1) {
        TableRow = "<td><input type='text' class='MarkContainer' /></td>";
    } else if (NumberOfRequiredAddedColumns == 2) {
        TableRow = "<td><input type='text' class='MarkContainer' /></td><td><input type='text' class='MarkContainer' /></td>";
    } else {
        TableRow = "<td><input type='text' class='MarkContainer' /></td><td><input type='text' class='MarkContainer' /></td><td><input type='text' class='MarkContainer' /></td>";
    }

    $.ajax({
        type: "POST",
        async: false,
        url: "InstructorHandler.ashx",
        data: { Method: "GetStudents", CourseID: CourseID, DoctorID: DoctorID, GroupNumber: GroupNumber },
        success: function (data) {
            var AllStudents = $.parseJSON(data);
            $.each(AllStudents, function (index, element) {
                if (element.Grade > 0) {
                    $("#GradeTable").append("<tr class='RowContainer'><td class='StudentID'>" + element.Id + "</td><td>" + element.Name + "</td>" + TableRow + "<td class='FinalGrade'>" + element.Grade + "</td></tr>");
                } else {
                    $("#GradeTable").append("<tr class='RowContainer'><td class='StudentID'>" + element.Id + "</td><td>" + element.Name + "</td>" + TableRow + "<td class='FinalGrade'></td></tr>");
                }
            });
        }
    });

}

function StartListening() {

    $('.MarkContainer').on('keyup', function (event) {
        var row = $(this).closest('tr[class="RowContainer"]');
        var CellsInThisRow = row.find(".MarkContainer");
        var flag = 1;
        $.each(CellsInThisRow, function (index, element) {
            if (element.value == "") {
                flag = 0;
            }
        });
        if (flag == 1) {
            var Sum = 0;
            $.each(CellsInThisRow, function (index, element) {
                Sum += parseInt(element.value, 10);
            });
            row.find(".FinalGrade").text(Sum);
            var StudentID = parseInt(row.find(".StudentID").html());

            $.ajax({
                type: "POST",
                async: false,
                url: "InstructorHandler.ashx",
                data: { Method: "InsertGrade", StudentID: StudentID, CourseID: PublicCourseID, Grade: Sum },
                success: function () {

                }
            });

        } else {
            row.find(".FinalGrade").text("");
        }
    });

}