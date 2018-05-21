(function () {
    $(document).ajaxStart($.blockUI).ajaxStop($.unblockUI);
    window.myUrl = "http:\/\/\/";

    function validateDateOfJoining() {
        var dateOfJoining = $('#dateOfJoining').val();
        if (dateOfJoining == '') {
            console.log(dateOfJoining);
            var error_message_dateJoin = false;
            //$('#error_message_dateJoin').html("The Days exceeds the alloted date for the specified leave");
            $('#error_message_dateJoin').addClass("show");
            $('#btnSubmit').prop('disabled', true);
            error_message_dateJoin = true;
        } else {
            $('#Date_error_message').removeClass("show")
            $('#btnSubmit').prop('disabled', false);
        }
    }
 
    //this does the Front-End Validation
    function ValidateDateofJoingAndLeaving(date1, date2) {
        var error = $('#error_message_dateJoin');

        var newDate1 = new Date(date1);
        var newDate2 = new Date(date2);
        if (newDate1 > newDate2) {
            error.addClass('hide');
            $('#btnSubmit').prop('disabled', true);
            $('#join_date_error_message').html("Entry date must be less than exit date")
            $('#join_date_error_message').toggleClass('hidden');
        }
        else if (newDate1 < newDate2) {
            error.removeClass('show');
            error.addClass('hide');
            $('#btnSubmit').prop('disabled', false);
        }

    }
    $('#dateleave').on('change', function () {
        var date1 = $('#dateJoin').val();
        var date2 = $('#dateleave').val()
        if (date2 != undefined) {
            ValidateDatesFromBackend(date1, date2);
        }
        //var newdate1 = date2.split('/');
        //var currentDay1 = newdate1[1] + '/' + newdate1[0] + '/' + newdate1[2];
        //$('#dateleave').val(currentDay2);
    })

       $('#dateJoin').on('change', function () {
           var date1 = $('#dateJoin').val();
           var date2 = $('#dateleave').val()
           console.log(date2);
           if (date1 != undefined) {
               ValidateDatesFromBackend(date1, date2);
           }
        //var newdate1 = date1.split('/');
        //console.log(newdate1);
        //var currentDay1 = newdate1[1] + '/' + newdate1[0] + '/' + newdate1[2];
        //console.log(currentDay1);
        //$('#dateJoin').val(currentDay1);
    })

    

    //this method does the backend validation
    function ValidateDatesFromBackend(date1, date2) {
        console.log("#########################")
        console.log(date1);
        console.log(date2);
        var newdate1 = date1.split('-');
        console.log(newdate1);
        var currentDay1 = newdate1[1] + '/' + newdate1[0] + '/' + newdate1[2];
        var newdate2 = date2.split('-');
        var currentDay2 = newdate2[1] + '/' + newdate2[0] + '/' + newdate2[2];
        console.log(currentDay1);
        $.ajax({
            type: 'GET',
            url: '/validation/validatedate?date1=' + currentDay1 + '&date2=' + currentDay2,
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (data) {
                console.log(data);
                if (data.message == false) {
                    $('#dateSpan').append(
                    `   <span class="red">Entry date must be less than Exist date</span>
                        `)
                }
                else {
                    //$('#dateSpan').removeClass('fa fa-calendar');
                    //$('#dateSpan').addClass("fa fa-check tooltips"); $('#dateSpan').attr("data-original-title", "success input!");
                    $('#dateSpan').append(
                    `<div class="alert alert-success>Valid date/div>`
                    )
                }
            },
            failure: function () {
                console.log("Failed");
                var message = {
                    message: "Failed to load data... Try again later"
                };
                //$('#position').append('<option value="">' + message.message + '</option>')
            }
        })
    }


    function getUnitByLocation(id) {
        var currentId = id;
        $.ajax({
            type: 'GET',
            url: '/api/settings/GetBusinessunitByLocation/' + id,
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (data) {
                console.log('in the Get unit by Location method');
                console.log(currentId);
                console.log(data);
                $('#businessunitId').empty();
                if (data != '') {
                    $('#businessunitId').append('<option class="centre" value="">' + 'Select Business Unit' + '</option>');
                    $.each(data, function (index, val) {
                        $('#businessunitId').append('<option value="' + val.Id + '">' + val.unitname + '</option>');
                    })
                } else {
                    $('#businessunitId').append('<option value="">' + 'No Unit for this Location yet' + '</option>')
                }
            },
            failure: function () {
                var message = {
                    message: "Failed to load data... Try again later"
                };
                $('#businessunitId').append('<option value="">' + message.message + '</option>')
            }
        })
    }

    $('#Location').on('change', function () {
        getUnitByLocation($(this).val());
    })
 
    function getidentityCode(id) {
    $.ajax({
        type: 'GET',
        url:'/api/Settings/GetEmployeeCodeByGroup/' + id,
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            console.log('in the Get  method');
            console.log(data);
            $('#identityCode').empty();
            if (data.EmployeeCode != null ) {
                    $('#identityCode').html(data.Employeeid);
                    $('#identityCode').val(data.EmployeeCode);
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
   

    function getLocationByGroup(id) {
        $.ajax({
            type: 'GET',
            url: '/api/settings/GetLocationByGroup/' + id,
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (data) {
                console.log(data);
                $('#Location').empty();
                if (data.length != 0) {
                    $('#Location').append('<option class="centre" value="">' + 'Select Location' + '</option>');
                    $.each(data, function (index, val) {
                        $('#Location').append('<option value="' + val.Id + '">' + val.State + '</option>');
                    })
                } else {
                    $('#Location').append('<option value="">' + 'No Location for this Group yet' + '</option>')
                }
            },
            failure: function () {
                console.log("Failed");
                var message = {
                    message: "Failed to load data... Try again later"
                };
                $('#Location').append('<option value="">' + message.message + '</option>')
            }
        })
    }


    function getJobs() {
            $.ajax({
                type: 'GET',
                url:'/api/settings/getjobs',
                success: function (data) {
                    console.log('data returned from getJobs')
                    console.log(data)
                    $('#SelectItem').empty();
                    $('#SelectItem').append('<option value="">' + 'Select job' + '</option>');
                    $.each(data, function (index, val) {

                        $('#SelectItem').append('<option value="' + val.JobId + '">' + val.JobName + '</option>');
                    })
                }
            });
        };

    $('#jobtitleId').bind('change', function () {
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
                $('#positionId').empty();
                if (data != '') {
                    $('#positionId').append('<option value="">' + 'Select Position' + '</option>');
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
                if (data.length > 0) {
                $.each(data, function (index, val) {
                    $('#departmentId').append('<option value="' + val.deptId + '">' + val.deptName + '</option>');
                })
                } else {
                    $('#departmentId').append('<option value="">' + 'No Department For this Unit' + '</option>')
                }
            },
        })
    }

 $('#GroupId').on('change', function () {
        getidentityCode($(this).val());
        getLocationByGroup($(this).val());
 })
 $('#businessunitId').bind('change', function () {
     getDepartmentByBusinessUnit($(this).val());
 })

 //function isNumberKey(evt, obj) {

 //    var charCode = (evt.which) ? evt.which : event.keyCode

 //    var value = obj.value;
 //    var dotcontains = value.indexOf(".") != -1;
 //    if (dotcontains)
 //        if (charCode == 46) return false;
 //    if (charCode == 46) return true;
 //    if (charCode > 31 && (charCode < 48 || charCode > 57))
 //        return false;
 //    return true;
    //}



 $('#officeNumber').on('keypress', function (event) {
     return $.ValidateNumber(event, this);
 });

 $('#empId').on('keypress', function (event) {
     return $.ValidateNumber(event, this);
 })

 $('#levelNo').on('keypress', function (event) {
     return $.ValidateNumber(event, this);
 })
 $('#EligibleYears').on('keypress', function (event) {
     return $.ValidateNumber(event, this);
 })

 $('#yearsExp').on('keypress', function (event) {
     return $.ValidateNumber(event, this);
 })
 function addAttributeToModal() {
   console.log($('#jModal'))
   $('#statusModal').attr("data-backdrop", "static");
  
 };

 $('#smodal').on('click', function () {
     $('#statusModal').attr("data-backdrop", "static");
     $('#statusModal').attr("data-keyboard", "false");
 })

 function addElement() {

     //$('#Location').append('<option class="centre" value="">' + 'No Location For now' + '</option>')
     $('#businessunitId').append('<option class="centre" value="">' + 'Please select a Location' + '</option>');
     $('#departmentId').append('<option class="centre" value="">' + 'Please select a Business Unit' + '</option>');
     addAttributeToModal();
 }
 window.onload = addElement();

})()