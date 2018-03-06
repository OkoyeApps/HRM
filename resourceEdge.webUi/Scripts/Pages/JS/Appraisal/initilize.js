(function(){
    var periodId = 0;
    var dataOut = {};
    function getPeriod(from, to, mode){
       var from =  $('#FromYear').val();
       var To =  $('#ToYear').val();
       mode = $('#AppraisalMode').val();
       $.ajax({
           type: 'GET',
           url: '/api/settings/GetPeriodForAppraisal/' + mode,
           contentType: "application/json; charset=utf-8",
           dataType: "json",
           success: function (data) {
               console.log('in the Get unit by Location method');
               console.log(data);
               dataOut = data;
               if (data != null) {
                   $('#Period').html(data.Name);
                 $('#Period').val(data.Name);
                 
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

    $('#AppraisalMode').on('change', function () {
        var from = $('#FromYear').val();
        var To = $('#ToYear').val();
        console.log(To + "############")
        if (from != "" && To != "" ) {
            getPeriod(from,To, $(this).val());
        }
    })

    $('#initsubmit').on('click', function () {
        //console.log(dataOut);
        //$('#period').val(dataOut.Id);
    })
})();