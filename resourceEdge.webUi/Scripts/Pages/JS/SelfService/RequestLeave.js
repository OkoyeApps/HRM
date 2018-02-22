
(function () {

        window.onload = function () {
            //  var id = $('#userIdDiv')[0].innerHTML;
            GetRmByUserId("@ViewBag.userId");
            GetempLeaveAmount("@ViewBag.userId");
        };
    document.onload = function () {

    }
    function GetempLeaveAmount(userId) {
        console.log("Method entered");
        $.ajax({
            type: 'GET',
            url: 'http://localhost:58124/api/settings/GetempLeaveAmount/' + userId,
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (data) {
                console.log('in the GetempLeaveAmount method');
                console.log(data);
                if (data.length != 0) {
                    $('#AvailableLeave').html(data.EmpLeaveLimit);
                    $('#AvailableLeave').val(data.EmpLeaveLimit);
                    return data;
                        
                } else {
                    $('#AvailableLeave').html("please configure Employee leave")
                    $('#AvailableLeave').val("Please configure Employee leave");
                    //write the jquery to check agaist JQHxr
                }
            },
            failure: function () {
                var message = {
                    message: "Failed to load data... Try again later"
                };
                $('#AvailableLeave').val('No Leave found');
            }
        })
    };

    function GetempLeaveType(id) {
        console.log("Method entered");
        $.ajax({
            type: 'GET',
            url: 'http://localhost:58124/api/Settings/GetempLeaveTypeAmount/' + id,
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (data) {
                console.log('in the GetempLeaveAmount method');
                console.log(data);
                if (data != '') {

                    $('#LeaveNoOfDays').html(data.numberofdays);
                    $('#LeaveNoOfDays').val(data.numberofdays);
                    return data;

                } else {
                    $('#LeaveNoOfDays').html("please configure Leave Types")
                }
            },
            failure: function () {
                var message = {
                    message: "Failed to load data... Try again later"
                };
                $('#LeaveNoOfDays').html('No Leave found');
            }
        })
    };
    $('#LeavetypeId').bind('change', function () {
        GetempLeaveType($(this).val());
    });


    function dateDifference(start, end) {

        // Copy date objects so don't modify originals
        var Date1 = new Date(start);
        var Date2 = new Date(end);


        // Set time to midday to avoid dalight saving and browser quirks
        Date1.setHours(12, 0, 0, 0);
        Date2.setHours(12, 0, 0, 0);

        // Get the difference in whole days
        var totalDays = Math.round((Date2 - Date1) / 8.64e7);

        // Get the difference in whole weeks
        var wholeWeeks = totalDays / 7 | 0;

        // Estimate business days as number of whole weeks * 5
        var days = wholeWeeks * 5;

        // If not even number of weeks, calc remaining weekend days
        if (totalDays % 7) {
            Date1.setDate(Date1.getDate() + wholeWeeks * 7);

            while (Date1 < Date2) {
                Date1.setDate(Date1.getDate() + 1);

                // If day isn't a Sunday or Saturday, add to business days
                if (Date1.getDay() != 0 && Date1.getDay() != 6) {

                    ++days;
                }
            }
        }
        return days;
    }

    $("#ToDate").change(function () {
        var result = dateDifference($('#FromDate').val(), $('#ToDate').val());
        $()
        console.log("Result is: " + result)
        checkTimeAgainstDate(result);
        ValidateTotalRequestDay($('#requestDays').val());
    });
    $('#FromDate').change(function () {
        var result = dateDifference($('#FromDate').val(), $('#ToDate').val());
        checkTimeAgainstDate(result);
        ValidateTotalRequestDay($('#requestDays').val());
    })




    //function CalculateDateDifference() {
    //    var FromDate = $('#FromDate').val();
    //    var ToDate = $('#ToDate').val();
    //    var date1 = new Date(FromDate);
    //    var Date2 = new Date(ToDate);
    //    var diff = 0;
    //    var days = 8.64e7;
    //    diff = Date2 - date1;

    //    console.log("Diff Date  " + new Date(diff));
    //    //var dd = new Date(mdy[0], mdy[1]-1, mdy[2])
    //    //var dd = new Date(mdy[0], mdy[1]-1, mdy[2])
    //    var finalResult = null;
    //    var result = Math.floor(diff / days)
    //    switch (true) {
    //        case (result > 7 && result < 14):
    //            finalResult = result - 2
    //            console.log("only one week end");
    //            break;
    //        case (result >= 14 && result < 21):
    //            finalResult = result - 4
    //            console.log("only Two week end");
    //            break;
    //        case (result >= 21 && result < 28):
    //            console.log("only Three week end");
    //            finalResult = result - 6
    //            break;
    //        case (result >= 28 && result <= 31):
    //            finalResult = result - 8
    //            console.log("only Four week end");
    //            break;
    //        default:
    //            finalResult = result - 0;
    //            console.log("hitting default");
    //    }
    //    checkTimeAgainstDate(finalResult + "is finalResult");
    //    console.log("the differnce in days is " + finalResult);
    //}
    //$("#ToDate").change(function (event) {
    //    CalculateDateDifference(event, $("#ToDate"));
    //});
    $('#requestDays').on('change', function () {
        console.log($(this).val());
        ValidateTotalRequestDay(this)
    })

    function ValidateTotalRequestDay(days) {
        console.log("in the validateTotalRequest");
        if (days < 0) {

        }
        if (days == 0) {
            $('#btnSubmit').prop('disabled', true);
        }
        if (days >0) {
            $('#btnSubmit').prop('disabled', false);
        }
    }

    function checkTimeAgainstDate(daysPicked) {
        var Date_error_message = false;
        var realNoofDays = $('#LeaveNoOfDays').val();
        console.log(daysPicked + "is days picked");
        if (daysPicked <= Number(realNoofDays)) {
            Date_error_message = false;
            console.log("in the else method");
            $('#Date_error_message').removeClass("show")
            $('#btnSubmit').prop('disabled', false);
            $('#requestDays').val(daysPicked);

        } else {

            $('#Date_error_message').html("The Days exceeds the alloted date for the specified leave");
            $('#Date_error_message').addClass("show");
            $('#btnSubmit').prop('disabled', true);
            Date_error_message = true;
            console.log('got here');
        }

    }



    function GetRmByUserId(id) {
        console.log("GetRmByUserId Method entered");
        $.ajax({
            type: 'GET',
            url: 'http://localhost:58124/api/Settings/GetRmByUserId/' + id,
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (data) {
                console.log('in the GetRmByUserId method');
                console.log(data);
                if (data != '') {
                    $('#RepmangId').append('<option value="">' + '--Select Employee--' + '</option>');
                    $.each(data, function (index, data) {
                        $('#RepmangId').append('<option value="' + data.userId + '">' + data.FullName + '</option>');
                    })
                } else {

                    $('#RepmangId').append('<option value="">' + 'No Reporting Manager For you yet' + '</option>')
                }
            },
            failure: function () {
                var message = {
                    message: "Failed to load data... Try again later"
                };
                $('#RepmangId').append('<option value="">' + message.message + '</option>')
            }
        })
    };

})()