﻿(function () {

    function getDepartmentByBusinessUnit(id) {
        $.ajax({
            type: 'GET',
            url: 'http://localhost:58124/api/Settings/GetDepartmentsById/' + id,
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (data) {

                console.log('in the Get department method');
                console.log(data)
                console.log($('#businessunitId'))
                $('#departmentId').empty();
                $('#departmentId').attr({ 'data-live-search': "true", 'data-size': "4" })
                $('#departmentId').append('<option value="">' + 'Select department' + '</option>');
                $.each(data, function (index, val) {
                    $('#departmentId').append('<option value="' + val.deptId + '">' + val.deptName + '</option>');
                })
            },
        })
    }



    function getEmployessByDept(id) {
        $.ajax({
            type: 'GET',
            url: 'http://localhost:58124/api/Settings/GetEmpByDept/' + id,
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (data) {

                console.log('in the Get department method');
                console.log(data)
                $('#Employees').empty();
                $('#Employees').attr({ 'data-live-search': "true", 'data-size': "4" })
                $('#Employees').append('<option value="">' + 'Select department' + '</option>');
                if (data.length > 0) {
                    $.each(data, function (index, val) {
                        $('#Employees').append('<option value="' + val.userId + '">' + val.FullName + '</option>');
                    })
                } else {
                    $('#Employees').append('<option value="">' + 'No employee for this department' + '</option>');
                }

            },
        })
    }

    $('#BunitId').bind('change', function () {
        getDepartmentByBusinessUnit($(this).val());
    });
    $('#departmentId').bind('change', function () {
        getEmployessByDept($(this).val());
    });



    function addElement() {
    }
    window.onload = addElement();
})();