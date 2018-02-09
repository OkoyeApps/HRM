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
    getDepartmentByBusinessUnit($(this).val());
})

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
            $('#identityCode').empty();
            $('#RequisitionCode').empty();
            if (data != '') {
                $.each(data, function (index, data) {
                    $('#identityCode').html(data.EmployeeId);
                    $('#identityCode').val(data.EmployeeCode);
                    $('#RequisitionCode').val(data.RequisitionCode);
                    $('#RequisitionCode').html(data.RequisitionCode);
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


function getHrByBusId(id) {
    $.ajax({
        type: 'GET',
        url: 'http://localhost:58124/api/Settings/GetRMByBusinessUnit/' + id,
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
$('#businessunitId').bind('change', function () {
    getHrByBusId($(this).val());
});
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
                    $('#tblTime').append("<tr><td><input name='Emp' class='form-control value='' /></td> <br><input type='hidden' Name='id' Value='"+element.userId+"'/></tr>'")
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






















//$('.date-own').datepicker({
//    minViewMode: 2,
//    format: 'yyyy'
//});

//$('.date-own').datepicker( {
//    format: " yyyy", // Notice the Extra space at the beginning
//    viewMode: "years", 
//    minViewMode: "years"
//});
//http://localhost:58124/api/Settings/GetEmpByDept/3

//Write code that would validate the business units for empty and any drop down that is empty


