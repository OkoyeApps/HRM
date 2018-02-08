$(document).ready(function () {
    function getJobs() {
        $.ajax({
            type: 'GET',
            url: 'http://localhost:58124/api/settings/getjobs',
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

$('#jobtitleId').bind('change', function () {
    getPositionsById($(this).val());
})

function getPositionsById(id) {
    var currentId = id;
    $.ajax({
        type: 'GET',
        url: 'http://localhost:58124/api/Settings/GetPositionById/' + id,
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            console.log('in the Get position method');
            console.log(currentId);
            console.log(data);
            $('#positionId').empty();
            if (data != '') {
                $('#positionId').append('<option value="">' + '--Select Position--' + '</option>');
                $.each(data, function (index, val) {
                    $('#positionId').append('<option value="' + val.PositionId + '">' + val.PositionName + '</option>');
                })
            } else {
                $('#positionId').append('<option value="">' + 'No position For this job' + '</option>')
            }
        },
        failure: function () {
            var message = {
                message: "Failed to load data... Try again later"
            };
            $('#position').append('<option value="">' + message.message + '</option>')
        }
    })
}

$('#businessunitId').bind('change', function () {
    getDepartment($(this).val());
})

function getDepartment(id) {
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

window.onload = getidentityCode();

function getidentityCode() {
    $.ajax({
        type: 'GET',
        url: 'http://localhost:58124/api/Settings/GetEmployeeCode',
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            console.log('in the Get  method');
            console.log(data);
            $('#empStatusId').empty();
            if (data != '') {
                $.each(data, function (index, data) {
                    $('#identityCode').html(data.EmployeeId);
                    $('#identityCode').val(data.EmployeeCode);
                })
            } else {
                $('#identityCode').html("please configure identity Code")
            }
        },
        failure: function () {
            var message = {
                message: "Failed to load data... Try again later"
            };
            $('#identityCode').html('No identity code found');
        }
    })
};

function GetEmployeeByBUnit(id) {
    $.ajax({
        type: 'GET',
        url: 'http://localhost:58124/api/Settings/GetEmpByBusinessUnit/' + id,
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            console.log('in the GetEmployeeByBusinessUnit  method');
            console.log(data);
            $('#userId').empty();
            if (data != '') {
                $('#userId').append('<option value="">' + '--Select Employee--' + '</option>');
                $.each(data, function (index, data) {
                    $('#userId').append('<option value="' + data.userId + '">' + data.empEmail + '</option>');
                })
            } else {
               
                $('#userId').append('<option value="">' + 'No Employee For this Unit yet' + '</option>')
            }
        },
        failure: function () {
            var message = {
                message: "Failed to load data... Try again later"
            };
            $('#userId').append('<option value="">' + message.message + '</option>')
        }
    })
};



$('#BunitId').bind('change', function () {
    GetEmployeeByBUnit($(this).val());
});

function getHrByBusId(id){
    $.ajax({
        type: 'GET',
        url: 'http://localhost:58124/api/Settings/GetHrByBusinessUnit/' + id,
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            console.log('in the getHrByBusId  method');
            console.log(data);
            $('#HrId').empty();
            if (data != '') {
                $('#HrId').append('<option value="">' + '--Select HR--' + '</option>');
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
http://localhost:58124/api/Settings/GetHrByBusinessUnit/2

    $('#businessunitId').bind('change', function () {
        getHrByBusId($(this).val());
    });
//Write code that would validate the business units for empty and any drop down that is empty


