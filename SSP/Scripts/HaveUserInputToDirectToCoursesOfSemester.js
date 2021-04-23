function GetUserInput() {
    var PopUpMessage = $(".WhichCourse");
    PopUpMessage.addClass('Show');
    
    $(document).keyup(function (event) {
        if (event.keyCode == 27) {
            PopUpMessage.removeClass("Show");
            $('#ProgramsListContainer').text("");
            $("#Advance").off('click');
        }
    });


    $("#Advance").on("click", function (event) {
        var ProgramId = $('input[name=ProgramId]:checked').val();
        alert('program choosen=' + ProgramId);
    });

    $.ajax({
        type: "POST",
        url: "GeneralScedulerHandler.ashx",
        data: { Method: "GetAllPrograms" },
        success: function (data) {
            AllProgramsList = FormProgramsList(data);
            $('#ProgramsListContainer').append(AllProgramsList);

        }
    });


}



function GetUserInput2() {
    var PopUpMessage = $(".WhichCourse2");
    PopUpMessage.addClass('Show');

    $(document).keyup(function (event) {
        if (event.keyCode == 27) {
            PopUpMessage.removeClass("Show");
            $('#ProgramsListContainer2').text("");
            $("#Advance2").off("click");
        }
    });


    $("#Advance2").on("click", function (event) {
        var ProgramId = $('input[name=ProgramId]:checked').val();
        alert('program choosen=' + ProgramId);
    });

    $.ajax({
        type: "POST",
        url: "GeneralScedulerHandler.ashx",
        data: { Method: "GetAllPrograms" },
        success: function (data) {
            AllProgramsList = FormProgramsList(data);
            $('#ProgramsListContainer2').append(AllProgramsList);

        }
    });


}


function FormProgramsList(data)
{
    var Options="";
    var result = $.parseJSON(data);
    $.each(result, function (index, object) {
        Options += '<input type="radio" name="ProgramId" value="' + object.Id + '" />' + object.Name + '<br />';
    });
    return Options;
}