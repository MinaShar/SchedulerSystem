jQuery(document).ready(function ($) {
    $("#DoctorType").change(function (event) {
        if ($(this).is(':checked')) {
            $('#RankBlock').css('display', 'block');
            $('#DepartmentBlock').css('display', 'none');
        }
    });

    $("#StudentType").change(function (event) {
        if ($(this).is(':checked')) {
            $('#DepartmentBlock').css('display', 'block');
            $('#RankBlock').css('display', 'none');
        }
    });

    $("#AdminType").change(function (event) {
        if ($(this).is(':checked')) {
            $('#DepartmentBlock').css('display', 'none');
            $('#RankBlock').css('display', 'none');
        }
    });

    $('#AddNewUserForm').on('submit', function (event) {
        var flag = 1;
        var form = $(this)[0];
        for (var i = 0; i < form.elements.length; i++) {
             var e = form.elements[i];
             if (e.getAttribute("data-required") == "true" && e.value == "") {
                 event.preventDefault();
                 alert('fill in the data required');
                 return false;
             } 
        }
        if ($("#DoctorType").is(':checked')) {
            var choices = $('input[data-userrank]');
            var doctor_flag = 0;
            for (var q = 0; q < choices.length; q++) {
                if (choices[q].checked == true) {
                    doctor_flag = 1;
                }
            }
            if (doctor_flag == 0) {
                event.preventDefault();
                alert('you must choose rank');
                return false;
            }
        }
    });

});