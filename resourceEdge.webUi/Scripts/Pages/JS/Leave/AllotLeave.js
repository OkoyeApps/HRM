(function(){

function getRMByBusId(id) {
            $.ajax({
                type: 'GET',
                url: 'http://localhost:58124/api/Settings/GetRMByBusinessUnit/' + id,
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
                url: 'http://localhost:58124/api/Settings/GetDepartmentsById/' + id,
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

        function GetEmpByDept(id) {
            $.ajax({
                type: 'GET',
                url: 'http://localhost:58124/api/Settings/GetEmpByDept/' + id,
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (data) {
                    console.log('in the GetEmpByDept  method');
                    console.log(data);
                    $('#HrId').empty();
                    if (data != '') {
                        $('#HrId').append('<option value="">' + '--Select HR--' + '</option>');

                        data.forEach(function (element, index) {
                            console.log(element);
                            console.log(index);
                            $('#getCount').removeClass("hidden");
                            $('#tbluser').append("<tr><td>" + element.FullName + "</td></tr><br/>");
                            $('#tblTime').append("<tr><td><input name='Emp' class='form-control value='' /></td> <br><input type='hidden' Name='id' Value='" + element.userId + "'/></tr>'")
                        });

                        $.each(data, function (index, data) {
                            $('#HrId').append('<option value="' + data.userId + '">' + data.empEmail + '</option>');
                        })
                    } else {
                        $('#HrId').append('<option value="">' + 'No  For this Unit yet' + '</option>')
                    }
                },
                failure: function () {
                    var message = {
                        message: "Failed to load data... Try again later"
                    };
                    $('#userId').append('<option value="">' + message.message + '</option>')
                }
            })
        }
        $('#departmentId').bind('change', function () {
            GetEmpByDept($(this).val());
        });


})();