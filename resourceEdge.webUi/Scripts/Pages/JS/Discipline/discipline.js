(function () {

    function getDepartmentByBusinessUnit(id) {
        $.ajax({
            type: 'GET',
            url: '/api/Settings/GetDepartmentsById/' + id,
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (data) {

                console.log('in the Get department method');
                console.log(data)
                console.log($('#BusinessUnit'))
                $('#department').empty();
                if (data.length > 0) {
                    $('#department').append('<option>Select Department</option>');
                    $.each(data, function (index, val) {
                        $('#department').append('<option value="' + val.deptId + '">' + val.deptName + '</option>');
                    })
                } else {
                    $('#department').append('<option value="">' + 'No Department For this Unit' + '</option>')
                }
            },
        })
    }


    $('#BusinessUnit').bind('change', function () {
        getDepartmentByBusinessUnit($(this).val());
    })


    function getEmploymentByDepartment(id,locId,gropId) {
        $.ajax({
            type: 'GET',
            url: `/api/settings/GetEmployeeByDepartment/${id}/${locId}/${gropId}`,
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (data) {

                console.log('in the Get department method');
                console.log(data)
                console.log($('#employee'))
                $('#employee').empty();
                if (data.length > 0) {
                    $('#employee').append('<option>Select Employee</option>');
                    $.each(data, function (index, val) {
                        $('#employee').append('<option value="' + val.Id + '">' + val.name + '</option>');
                    })
                } else {
                    $('#employee').append('<option value="">' + 'No Employee For this Department' + '</option>')
                }
            },
        })
    }
    
    $('#department').bind('change', function () {
        var loc = $('#cusloc')[0].innerText;
        var grp = $('#grp')[0].innerText;
        console.log("________________");
        console.log($(this).val());
        getEmploymentByDepartment($(this).val(),loc,grp);
    });


    function GetRmByUserId(id) {
        console.log("GetRmByUserId Method entered");
        $.ajax({
            type: 'GET',
            url: '/api/Settings/GetRmByUserId/' + id,
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (data) {
                console.log('in the GetRmByUserId method');
                console.log(data);
                $('#rmanager').empty();
                if (data.length > 0) {
                    $('#rmanager').append('<option value="">' + 'Select Report Manager' + '</option>');
                    $.each(data, function (index, data) {
                        $('#rmanager').append('<option value="' + data.userId + '">' + data.FullName + '</option>');
                    })
                } else {

                    $('#rmanager').append('<option value="">' + 'No Reporting Manager For you yet' + '</option>')
                }
            },
            failure: function () {
                var message = {
                    message: "Failed to load data... Try again later"
                };
                $('#rmanager').append('<option value="">' + message.message + '</option>')
            }
        })
    };
    $('#employee').bind('change', function () {
        GetRmByUserId($(this).val());
    });



})();