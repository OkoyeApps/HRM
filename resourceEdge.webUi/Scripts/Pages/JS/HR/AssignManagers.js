(function(){

    //function getDepartmentByBusinessUnit(id) {
    //    $.ajax({
    //        type: 'GET',
    //        url: '/api/Settings/GetDepartmentsById/' + id,
    //        contentType: "application/json; charset=utf-8",
    //        dataType: "json",
    //        success: function (data) {

    //            console.log('in the Get department method');
    //            console.log(data)
    //            console.log($('#businessunitId'))
    //            $('#departmentId').empty();
    //            $('#departmentId').attr({ 'data-live-search': "true", 'data-size': "4" })
    //            $('#departmentId').append('<option value="">' + 'Select department' + '</option>');
    //            $.each(data, function (index, val) {
    //                $('#departmentId').append('<option value="' + val.deptId + '">' + val.deptName + '</option>');
    //            })
    //        },
    //    })
    //}
    


    function getUnitHeadById(id) {
        console.log(id);
        $.ajax({
            type: 'GET',
            url: '/api/Settings/GetEligibleReportManager/' + id,
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (data) {

                console.log('in the Get Unit Head method');
                console.log(data)
                console.log($('#businessunitId'))
                $('#ManagerId').empty();
                $('#ManagerId').attr({ 'data-live-search': "true", 'data-size': "4" })
                $('#ManagerId').append('<option value="">' + 'Select department' + '</option>');
                if (data.length >0) {
                    $.each(data, function (index, val) {
                        $('#ManagerId').append('<option value="' + val.userId + '">' + val.FullName + '</option>');
                    })
                } else {
                    $('#ManagerId').append('<option value="">' + 'No employee for this department' + '</option>');
                }
               
            },
        })
    }

    $('#BunitId').bind('change', function () {
        getUnitHeadById($(this).val());
    });
    $('#departmentId').bind('change', function () {
        getEmployessByDept($(this).val());
    });



    function addElement() {
        $('#Location').append('<option class="centre" value="">' + 'No Location For now' + '</option>')
        $('#businessunitId').append('<option class="centre" value="">' + 'Please select a Location' + '</option>');
        $('#departmentId').append('<option class="centre" value="">' + 'Please select a Business Unit' + '</option>');
    }
    window.onload = addElement();
})();