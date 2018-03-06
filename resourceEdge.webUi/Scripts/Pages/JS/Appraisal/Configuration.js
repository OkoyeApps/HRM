(function () {
 
    window.unitObject = {
        unitId: 0,
        groupId :0
    }
    $('#BusinessUnit').on('change', function () {
        unitObject.unitId = $('#BusinessUnit').find(":selected").val();
        console.log(unitObject);
    })

    $(document).ajaxStart($.blockUI).ajaxStop($.unblockUI);

    function getDepartmentByBusinessUnit(id) {
        $.ajax({
            type: 'GET',
            url: '/api/Settings/GetDepartmentsById/' + id,
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (data) {
                console.log('in the Get department method');
                console.log(data)
                console.log($('#businessunitId'))
                $('#departmentId').empty();
                $('#departmentId').append('<option value="">' + 'Select department' + '</option>');
                if (data.length > 0) {
                    $.each(data, function (index, val) {
                        $('#departmentId').append('<option value="' + val.deptId + '">' + val.deptName + '</option>');
                    })
                }
                else {
                    $('#departmentId').html('<option value="">' + 'No department Yet' + '</option>');
                }
               
            },
        })
    }
    $('#BusinessUnit').on('change', function () {
        getDepartmentByBusinessUnit($(this).val());
    });

    function getLocationManagers(id){
        $.ajax({
            type: 'GET',
            url: '/api/settings/GetLocationDetails/' + id,
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (data) {
                $('#sLine1').html("");
                $('#sLine1').val("");
                $('#LineManager1').empty();
                $('#LineManager1').append('<option value="">' + 'Select Line manager 2' + '</option>');
                if (data != null) {
                    if (data.Manager1) {
                        $('#LineManager1').append('<option id="opt" value="' + data.Manager1 + '">' + data.FullName1 + '</option>');
                    } if (data.Manager2) {
                        $('#LineManager1').append('<option id="opt" value="' + data.Manager2 + '">' + data.FullName2 + '</option>');
                    }
                    if (data.Manager3) {
                        $('#LineManager1').append('<option id="opt" value="' + data.Manager3 + '">' + data.FullName3 + '</option>');
                    } 
                    
                }
                else {
                    $('#LineManager1').html('<option value="">' + 'No department Yet' + '</option>');
                }

            },
        })
    }
    
        window.onload = function () {
        getLocationManagers($('#group').text());
        unitObject.groupId = $('#group').text();
        getUnitHeads($)
    }

        $('#LineManager1').on('change', function () {
            $('#sLine1').html("");
            $('#sLine1').val("");
            $('#sLine1').val($('#LineManager1').find(":selected").text());
            $('#sLine1').html($('#LineManager1').find(":selected").text());
        })


    function getUnitHeads(unitId) {
        var idToSend = null;
        if (unitId == "A"){
            idToSend = unitObject.unitId;
        }
        if (unitId == "B") {
            idToSend = "All"
        }
        $.ajax({
            type: 'GET',
            url: '/api/Settings/GetUnitHeadForAppraisal/' + idToSend,
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (data) {
                $('#sLine2').html("");
                $('#sLine2').val("")
                $('#LineManager2').empty();
                $('#LineManager2').append('<option id="opt1"  value="">' + 'Select Line Manager 2' + '</option>');
                if (data.length > 0) {
                    $.each(data, function (index, val) {
                        $('#LineManager2').append('<option id="opt1" value="' + val.UserId + '">' + val.FullName + '</option>');
                    })
                }
                else {
                    $('#LineManager2').html('<option id="opt1"  value="">' + 'No Head Yet' + '</option>');
                }

            },
        })
    }

    $("#ALManager2").on('change', function () {
        getUnitHeads($(this).val());
    })
    $('#LineManager2').on('change', function () {
        $('#sLine2').html("");
        $('#sLine2').val("");
        $('#sLine2').val($('#LineManager2').find(":selected").text());
        $('#sLine2').html($('#LineManager2').find(":selected").text());
    })

    function getLinemanager3(id) {
        var idToSend = null;
        var option = null;
        if (id == 1) {
            idToSend = unitObject.unitId
            option = 'D'
        }
        if (id == 2) {
            idToSend = unitObject.unitId,
            option = 'U'
        }
        if (id == 3) {
            idToSend = unitObject.unitId,
            option = 'G'
        }
        $.ajax({
            type: 'GET',
            url: '/api/Settings/GetLineManagers/' + idToSend + '/' + option,
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (data) {
                $('#sLine3').html("");
                $('#sLine3').val("");
                $('#LineManager3').empty();
                $('#LineManager3').append('<option id="opt2"  value="">' + 'Select Line manager 3' + '</option>');
                if (data.length > 0) {
                    $.each(data, function (index, val) {
                        $('#LineManager3').append('<option id="opt2" value="' + val.userId + '">' + val.FullName + '</option>');
                    })
                }
                else {
                    $('#LineManager3').html('<option id="opt2"  value="">' + 'No User Yet' + '</option>');
                }

            },
        })
    }

    
    $("#ALManager3").on('change', function () {
        getLinemanager3($(this).val());
    })
    $('#LineManager3').on('change', function () {
        $('#sLine3').val($('#LineManager3').find(":selected").text());
        $('#sLine3').html($('#LineManager3').find(":selected").text());
    })


    var conceptName = $('#LineManager1').find(":selected").text();


       





    //$.blockUI({ message: '<h1><img src="busy.gif" /> Just a moment...</h1>' });
})()