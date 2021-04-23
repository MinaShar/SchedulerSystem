
jQuery(document).ready(function ($) {
    $("#SaveSchaduler").on("click", function (event) {
        event.preventDefault();
        var valid=true;
        alert('inside handler');
        var AllGroups = $('.GroupContext');
        AllGroups.each(function (index, element) {
            var CurrentGroup = $(this);
            if(CurrentGroup.find('.DoctorOfGroup').val()=="")
            {
                var Directories = CurrentGroup.find('.Directory');
                Directories.each(function (index, element) {
                    var CurrentDirectory=$(this);
                    var CurrentDirectoryDay=CurrentDirectory.find('.DirectoryDay');
                    var CurrentDirectoryPeriod=CurrentDirectory.find('.DirectoryPeriod');
                    if(CurrentDirectoryDay.val()!="" || CurrentDirectoryPeriod.val()!="")
                    {
                        valid = false;
                        alert('No doctor Choosen for Group!');
                        return;
                    }
                });
            } else {
                var Directories = CurrentGroup.find('.Directory');
                Directories.each(function (index, element) {
                    var currentDirectory=$(this);
                    if(index==0)
                    {///lecture
                        if(currentDirectory.find('.DirectoryDay').val()=="" || currentDirectory.find('.DirectoryPeriod').val()=="")
                        {
                            alert('No Lecture specified for group '+ index+1 );
                            valid = false;
                            return;
                        }
                    } else {
                        if (currentDirectory.find('.DirectoryDay').val() != "" && currentDirectory.find('.DirectoryPeriod').val() == "" || currentDirectory.find('.DirectoryDay').val() == "" && currentDirectory.find('.DirectoryPeriod').val() != "") {
                            alert('No Complete info about section or lab of group '+ index+1 );
                            valid = false;
                            return;
                        }
                    }
                });
            }
        });


    });
});



function FormDoctorsList(Data)
{
    var Options;
    var result = $.parseJSON(Data);
    Options += '<option value=""></option>';
    $.each(result, function (index, object) {
        Options += '<option value="' + object.Id + '">' + object.Name + '</option>';
    });
    var HeaderTag = '<div class="form-group">' +
                                '<label class="col-md-2 control-label">Doctor</label>' +
                                '<div class="col-md-10">' +
                                    '<select class="form-control DoctorOfGroup">' +
                                    Options+
                                    '</select>' +
                                '</div>' +
                            '</div>';
    return HeaderTag;
}

function FormDirectoryList(Header) {

    var DirectoryInfo = '<div class="form-group Directory">' +
                                '<h4 style="margin-left:132px;">' + Header + '</h4>' +
                                '<label class="col-md-2 control-label">Day</label>' +
                                '<div class="col-md-10">' +
                                    '<select class="form-control DirectoryDay">' +
                                    '<option value=""></option>' +
                                    '<option value="saturday">saturday</option>' +
                                    '<option value="sunday">sunday</option>' +
                                    '<option value="monday">monday</option>' +
                                    '<option value="tuesday">tuesday</option>' +
                                    '<option value="wednesday">wednesday</option>' +
                                    '<option value="thursday">thursday</option>' +
                                    '</select>' +
                                '</div>' +
                                '<label class="col-md-2 control-label">Period</label>' +
                                '<div class="col-md-10">' +
                                    '<select class="form-control DirectoryPeriod">' +
                                    '<option value=""></option>' +
                                    '<option value="1">1</option>' +
                                    '<option value="2">2</option>' +
                                    '<option value="3">3</option>' +
                                    '<option value="4">4</option>' +
                                    '<option value="5">5</option>' +
                                    '</select>' +
                                '</div>' +
                            '</div>';
    return DirectoryInfo;

}


function openCity(evt, cityName) {
    var i, tabcontent, tablinks;
    tabcontent = document.getElementsByClassName("tabcontent");
    for (i = 0; i < tabcontent.length; i++) {
        tabcontent[i].style.display = "none";
    }
    tablinks = document.getElementsByClassName("tablinks");
    for (i = 0; i < tablinks.length; i++) {
        tablinks[i].className = tablinks[i].className.replace(" active", "");
    }
    document.getElementById(cityName).style.display = "block";
    evt.currentTarget.className += " active";

    var AllDoctorsList;

    $.ajax({
        type:"POST",
        url:"GeneralScedulerHandler.ashx",
        data:{ Method:"GetAllDoctors" },
        success : function(data){
            AllDoctorsList = FormDoctorsList(data);
        }
    });


    $("#NumberOfGroups").keyup(function (event) {
        document.getElementById("ContainerForGroups").innerHTML = "";
        var NumberOfGroups = event.currentTarget.value;
        for(var i=0;i<NumberOfGroups;i++)
        {
            var GroupToPrint=i+1;
            $("#ContainerForGroups").append(
                '<div class="GroupContext">'+
                '<h3>Group-' + GroupToPrint + '</h3>' +
                AllDoctorsList +
                FormDirectoryList('Lecture :')+
                FormDirectoryList('Section :')+
                FormDirectoryList('Lab :')+
                '</div>'
                );
        }


        var AllGroups = $(".GroupContext");
        AllGroups.each(function (index, element) {
            var DoctorOfGroup = $(this).find(".DoctorOfGroup");
            DoctorOfGroup.change(function (event) {
                ValidateToAddItemInTable();
            });
            var Directories = $(this).find(".Directory");
            Directories.each(function (index, element) {
                var DirectoryDay = $(this).find(".DirectoryDay");
                var DirectoryPeriod = $(this).find(".DirectoryPeriod");
                DirectoryDay.change(function (event) {
                    ValidateToAddItemInTable();
                });
                DirectoryPeriod.change(function (event) {
                    ValidateToAddItemInTable();
                });
            });
        });


    });

}


function ValidateToAddItemInTable() {

    var AllPeriods = $(".period");
    AllPeriods.each(function (index, element) {
        $(this).text("");
    });


    var AllGroups = $(".GroupContext");
    AllGroups.each(function (index, element) {
        var DoctorOfGroup = $(this).find(".DoctorOfGroup");
        if(DoctorOfGroup.val()=="")
        {

        } else {
            var AllDirectories = $(this).find(".Directory");
            AllDirectories.each(function (index, elemet) {
                var DiretoryDay = $(this).find(".DirectoryDay");
                var DirectoryPeriod = $(this).find(".DirectoryPeriod");
                if(DiretoryDay.val()!="" && DirectoryPeriod.val()!="")
                {
                    alert('now writing ' + DiretoryDay.val() + ' with ' + DirectoryPeriod.val());
                    var Periods = $(".period");
                    Periods.each(function (index, element) {
                        if(DiretoryDay.val()==$(this).attr('data-day') && DirectoryPeriod.val()==$(this).attr('data-period') )
                        {
                            alert('now writing in td=' + $(this).attr('data-day') + ' with ' + $(this).attr('data-period'));
                            $(this).text("writing here og log lo tet here gi");
                        }
                    });
                }
            });
        }
    });
}