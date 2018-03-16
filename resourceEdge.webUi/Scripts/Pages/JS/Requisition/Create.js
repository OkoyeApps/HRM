(function () {

        $(document).ready(function () {
            function getJobs() {
                $.ajax({
                    type: 'GET',
                    url: '/api/settings/getjobs',
                    success: function (data) {
                        console.log('data returned from getJobs')
                        console.log(data)
                        $('#SelectItem').empty();
                        $('#SelectItem').append('<option value="">' + '--Select job--' + '</option>');
                        $.each(data, function (index, val) {

                            $('#SelectItem').append('<option value="' + val.JobId + '">' + val.JobName + '</option>');
                        })
                    }
                });
            };
        });

    $('#JobTitle').bind('change', function () {
        getPositionsById($(this).val());
    })

    function getPositionsById(id) {
        var currentId = id;
        $.ajax({
            type: 'GET',
            url: '/api/Settings/GetPositionById/' + id,
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (data) {
                console.log('in the Get position method');
                console.log(currentId);
                console.log(data);
                $('#Position').empty();
                if (data != '') {
                    $('#Position').append('<option value="">' + '--Select Position--' + '</option>');
                    $.each(data, function (index, val) {
                        $('#Position').append('<option value="' + val.PositionId + '">' + val.PositionName + '</option>');
                    })
                } else {
                    $('#Position').append('<option value="">' + 'No position For this job' + '</option>')
                }
            },
            failure: function () {
                var message = {
                    message: "Failed to load data... Try again later"
                };
                $('#Position').append('<option value="">' + message.message + '</option>')
            }
        })
    }

    $('#BusinessunitId').bind('change', function () {
        getDepartment($(this).val());
        getReportManagerByBunitId($(this).val());
    })

    function getDepartment(id) {
        console.log(id);
        $.ajax({
            type: 'GET',
            url: '/api/Settings/GetDepartmentsById/' + id,
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (data) {

                console.log('in the Get department method');
                console.log(data);
                $('#department').empty();
                $('#department').append('<option value="">' + '--Select department--' + '</option>');
                $.each(data, function (index, val) {
                    $('#department').append('<option value="' + val.deptId + '">' + val.deptName + '</option>');
                })
            },
            failure: function () {
                var message = {
                    message: "Failed to load data... Try again later"
                };
                $('#department').append('<option value="">' + message.message + '</option>')
            }
        })
    }

    function getReportManagerByBunitId(id) {
        $.ajax({
            type: 'GET',
            url: '/api/Settings/GetHrByBusinessUnit/' + id,
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (data) {
                console.log('in the getReportManagerByBunitId  method');
                console.log(data);
                $('#ReportingId').empty();
                if (data != '') {
                    $('#ReportingId').append('<option value="">' + '--Select Report Manager--' + '</option>');
                    $.each(data, function (index, data) {
                        $('#ReportingId').append('<option value="' + data.userId + '">' + data.FullName + '</option>');
                    })
                } else {

                    $('#ReportingId').append('<option value="">' + 'No Report manager For this Unit yet' + '</option>')
                }
            },
            failure: function () {
                var message = {
                    message: "Failed to load data... Try again later"
                };
                $('#ReportingId').append('<option value="">' + message.message + '</option>')
            }
        })
    }

})()