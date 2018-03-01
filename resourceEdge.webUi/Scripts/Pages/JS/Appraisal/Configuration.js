(function () {
 
    window.submitObject = {
        status: false,
        result : null
    }

    $(document).ajaxStart($.blockUI).ajaxStop($.unblockUI);

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
        console.log('##########');
        getDepartmentByBusinessUnit($(this).val());
        //$.blockUI({ message: '<h1><img src="busy.gif" /> Just a moment...</h1>' });
    });


})()