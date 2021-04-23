function OpenRegistration(val) {
    alert('succeded');
    alert(val);

    if (val == 1) {
        $.ajax({
            type: "POST",
            url: "GeneralScedulerHandler.ashx",
            data: { Method: "OpenRegestration", flag: true },
            success: function () {

            }
        });
    } else {

        $.ajax({
            type: "POST",
            url: "GeneralScedulerHandler.ashx",
            data: { Method: "OpenRegestration", flag: false },
            success: function () {

            }
        });

    }
}

function CloseSemester(val) {

    alert('succeded');
    alert(val);

    if (val == 1) {
        $.ajax({
            type: "POST",
            url: "GeneralScedulerHandler.ashx",
            data: { Method: "CloseSemester", flag: true },
            success: function () {

            }
        });
    } else {

        $.ajax({
            type: "POST",
            url: "GeneralScedulerHandler.ashx",
            data: { Method: "CloseSemester", flag: false },
            success: function () {

            }
        });

    }

}