(function () {
       function GetEmployeeAllLeave(userId) {
           var title = $('#tblTitle');
           console.log(title);
           $.ajax({
               type: "GET",
               url: "/api/settings/GetEmployeeAllLeave/" + userId,
               contentType: "application/json; charset=utf-8",
               dataType: "json",
               success: function (data) {
                   $('#tBody').empty();
                   $('#AllRequest').removeClass("hidden");
                   $('#tblTitle')["0"].innerText = "All Leave Request";
                   $('#alert').addClass('alert-success');
                   $('#alert').removeClass('alert-danger');
                   $('#alert').removeClass('alert-warning');
                   console.log("in the GetEmployeeAllLeave");
                   console.log(data)
                   $.each(data, function (index, val) {
                       var fromDate = new Date(val.FromDate);
                       var ToDate = new Date(val.ToDate);
                       $('#tBody').append('<tr><td>' + val.LeaveName + '</td><td>' + val.requestDaysNo +
                            '</td><td>' + fromDate.getDate() + '</td><td>' + ToDate.getDate() + 'th' + '</td><td>' +
                          val.Reason + '</td></tr>')

                   })

               }
           })
       }
    $('#AllLeave').click(function () {
        GetEmployeeAllLeave("@ViewBag.UserId");
    });

    function GetEmployeeApprovedLeave(userId) {
        console.log(userId);

        $.ajax({
            type: "GET",
            url: "/api/settings/GetEmployeeApprovedLeave/" + userId,
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (data) {
                $('#tBody').empty();
                $('#AllRequest').removeClass("hidden");
                $('#tblTitle')["0"].innerText = "Approved Request";
                $('#alert').addClass('alert-success');
                $('#alert').removeClass('alert-danger');
                $('#alert').removeClass('alert-warning');
                console.log("in the GetEmployeeApprovedLeave");
                console.log(data)
                $.each(data, function (index, val) {
                    var fromDate = new Date(val.FromDate);
                    var ToDate = new Date(val.ToDate);
                    $('#tBody').append(
                        '<tr><td>' + val.LeaveName + '</td><td>' + val.requestDaysNo +
                        '</td><td>' + fromDate.getDate() + '</td><td>' + ToDate.getDate() +
                        'th' + '</td><td>' + val.Reason + '</td></tr>')
                })

            }
        })
    }
    $('#approved').click(function () {
        GetEmployeeApprovedLeave("@ViewBag.UserId");
    });

    function GetEmployeePendingLeave(userId) {
        $.ajax({
            type: "GET",
            url: "/api/settings/GetEmployeePendingLeave/" + userId,
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (data) {
                $('#tBody').empty();
                $('#AllRequest').removeClass("hidden");
                $('#tblTitle')["0"].innerText = "Pending Request";
                $('#alert').removeClass("alert-success");
                $('#alert').removeClass('alert-danger');
                $('#alert').addClass('alert-warning');
                console.log("in the GetEmployeePendingLeave");
                console.log(data)
                $.each(data, function (index, val) {
                    var fromDate = new Date(val.FromDate);
                    var ToDate = new Date(val.ToDate);
                    $('#tBody').append(
                        '<tr><td>' + val.LeaveName + '</td><td>' + val.requestDaysNo +
                        '</td><td>' + fromDate.getDate() + '</td><td>' + ToDate.getDate() +
                        'th' + '</td><td>' + val.Reason + '</td></tr>')
                });
            }
        });
    }
    $('#pending').click(function () {
        GetEmployeePendingLeave("@ViewBag.UserId");
    });

    function GetEmployeeDeniedLeave(userId) {
        console.log(userId);
        $.ajax({
            type: "GET",
            url: "/api/settings/GetEmployeeDeniedLeave/" + userId,
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (data) {
                $('#tBody').empty();
                $('#AllRequest').removeClass("hidden");
                $('#tblTitle')["0"].innerText = "Denied Request";
                $('#alert').removeClass("alert-success");
                $('#alert').removeClass("alert-warning");
                $('#alert').addClass('alert-danger');
                console.log("in the GetEmployeeDeniedLeave");
                console.log(data)
                $.each(data, function (index, val) {
                    var fromDate = new Date(val.FromDate);
                    var ToDate = new Date(val.ToDate);
                    $('#tBody').append(
                        '<tr><td>' + val.LeaveName + '</td><td>' + val.requestDaysNo +
                        '</td><td>' + fromDate.getDate() + '</td><td>' + ToDate.getDate() +
                        'th' + '</td><td>' + val.Reason + '</td></tr>')
                })
            }
        })
    }
    $('#deny').click(function () {
        GetEmployeeDeniedLeave("@ViewBag.UserId");
    });

    function initializeTable(name){
        name.DataTable({
            colReorder: true,
            dom: 'Bfrtip',
            buttons: [
                'copy', 'csv', 'excel', 'pdf', 'print'
            ]
        });
}

    $(document).ready(function () {
        initializeTable($('#tblapproved'));
        initializeTable($('#tbldenied'));
        initializeTable($('#tblpending'));
        initializeTable($('#tblall'));
    
    })

    

})()