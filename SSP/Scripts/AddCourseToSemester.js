jQuery(document).ready(function ($) {
    StartPage();
});
function StartPage(){
    var Buttons = $('.AddCourseToSemester');
    Buttons.on('click', function (event) {
        event.preventDefault();
        
        var Element = event.target;
        var FormRequired = document.getElementById('RegisterNewCourseForm');
        document.getElementById("ContainerForCourseName").innerHTML = Element.getAttribute('data-coursename');
        FormRequired.GroupsNumber.value = "";
        FormRequired.SectionsPerGroupNumber.value = "";
        /*
        $.ajax({
            type: "POST",
            url: "GeneralScedulerHandler.ashx",
            data: { Method: "GetCourseInfo", Id: Element.getAttribute('data-courseid') },
            success: function (data) {
                var result = $.parseJSON(data);
                $.each(result, function (index, object) {
                    FormRequired.LecturesNumber.value=object.NumberOfLectures;
                    FormRequired.SectionsNumber.value=object.NumberOfSections;
                    FormRequired.LabsNumber.value=object.NumberOfLabs;
                    FormRequired.GroupsNumber.value="";
                });
            }
        });
        */

        $('#PopUpForm').addClass('Show');
        

        
        $('#RegisterNewCourseForm').on('submit', function (event) {

            event.preventDefault();
            if (FormRequired.GroupsNumber.value == "" || FormRequired.SectionsPerGroupNumber.value == "" || FormRequired.SectionsPerGroupNumber.value < 1)
            {
                document.getElementById('GroupNumberMandatory').style.display = "block";
                return;
            }
            $('#PopUpForm').removeClass('Show');
            document.getElementById('GroupNumberMandatory').style.display = "none";
            var GroupsNumber = FormRequired.GroupsNumber.value;
            var SectionsPerGroupNumber = FormRequired.SectionsPerGroupNumber.value;

            $.ajax({
                type: "POST",
                url: "GeneralScedulerHandler.ashx",
                data: { Method: "AddCourseToSemester", Id: Element.getAttribute('data-courseid'), GroupsNumber: GroupsNumber, SectionsPerGroupNumber: SectionsPerGroupNumber },
                success: function (data) {
                    document.getElementById("courseID-" + Element.getAttribute('data-courseid')).innerHTML = "<label>ADDED</label>";
                    var table = document.getElementById("SelectedCoursesTerm" + Element.getAttribute('data-term'));
                    var row = table.insertRow(1);
                    var cell1 = row.insertCell(0);
                    var cell2 = row.insertCell(1);
                    var cell3 = row.insertCell(2);
                    var cell4 = row.insertCell(3);
                    cell1.innerHTML = Element.getAttribute('data-coursename');
                    cell2.innerHTML = GroupsNumber;
                    cell3.innerHTML = SectionsPerGroupNumber;
                    cell4.innerHTML = '<form>' +
                    '<input type="submit" data-coursename="' + Element.getAttribute('data-coursename') + '" data-coursesemesterid="' + data + '" value="Remove" class="RemoveCourseFromSemester" />' +
                    '</form>';


                    $('#RegisterNewCourseForm').off('submit');
                    $('.AddCourseToSemester').off('click');
                    $('.RemoveCourseFromSemester').off('click');
                    StartPage();
                }
            });
        });


    });

    $(document).keyup(function (event) {
        if (event.keyCode == 27) {
            $('#PopUpForm').removeClass('Show');
            $('#RegisterNewCourseForm').off('submit');
            document.getElementById('GroupNumberMandatory').style.display = "none";
        }
    });


    var RemoveButtons = $('.RemoveCourseFromSemester');
    RemoveButtons.one('click', function (event) {
        event.preventDefault();
        var RowToRemove = $(this).closest("tr");
        var CourseRemove = RowToRemove.find('.RemoveCourseFromSemester').attr('data-coursename');
        var CourseSemesterRemove = RowToRemove.find('.RemoveCourseFromSemester').attr('data-coursesemesterid');
        if (confirm("Are you sure you want to remove << " + CourseRemove + " >> from semester !") == true) {
            

            $.ajax({
                type: "POST",
                url: "GeneralScedulerHandler.ashx",
                data: { Method: "RemoveCourseFromSemester", CourseSemesterID: CourseSemesterRemove },
                success: function (Data) {
                    RowToRemove.remove();
                    var result = $.parseJSON(Data);
                    $.each(result, function (index, object) {
                        document.getElementById('Container-CourseID-' + object.Id).innerHTML = '<form id="courseID-' + object.Id + '">' +
                    '<input type="submit" data-coursename="' + object.CourseName + '" data-courseid="' + object.Id + '" data-term="' + object.Term + '" value="ADD" class="AddCourseToSemester" />' +
                    '</form >';
                    });


                    $('.AddCourseToSemester').off('click');
                    $('.RemoveCourseFromSemester').off('click');
                    StartPage();
                }
            });


        } else {
            return;
        }
    });
}