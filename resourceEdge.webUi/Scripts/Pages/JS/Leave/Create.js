(function(){

 function getRMByBusId(id) {
     $.ajax({
         type: 'GET',
         url: '/api/Settings/GetRMByBusinessUnit/' + id,
         contentType: "application/json; charset=utf-8",
         dataType: "json",
         success: function (data) {
             console.log('in the getRMByBusId  method');
             console.log(data);
             $('#reportManager').empty();
             if (data != '') {
                 $('#reportManager').append('<option value="">' + '--Select Report Manager--' + '</option>');
                 $.each(data, function (index, data) {
                     $('#reportManager').append('<option value="' + data.userId + '">' + data.FullName + '</option>');
                 })
             } else {

                 $('#reportManager').append('<option value="">' + 'No Report Manager For this Unit yet' + '</option>')
             }
         },
         failure: function () {
             var message = {
                 message: "Failed to load data... Try again later"
             };
             $('#reportManager').append('<option value="">' + message.message + '</option>')
         }
     })
 };
    $('#RmBusiness').bind('change', function () {
        getRMByBusId($(this).val());
    });


    function getDepartmentByBusinessUnit(id) {
        console.log(id);
        $.ajax({
            type: 'GET',
            url: '/api/Settings/GetDepartmentsById/' + id,
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (data) {

                console.log('in the Get department method');
                console.log(data);
                $('#departmentId').empty();
                $('#departmentId').append('<option value="">' + '--Select department--' + '</option>');
                $.each(data, function (index, val) {
                    $('#departmentId').append('<option value="' + val.deptId + '">' + val.deptName + '</option>');
                })
            },
        })
    }
    $('#RmBusiness').bind('change', function () {
        getDepartmentByBusinessUnit($(this).val());
    })


})();