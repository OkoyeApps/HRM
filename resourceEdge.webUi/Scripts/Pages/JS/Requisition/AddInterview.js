(function () {

    function getInterviewer(id) {
        console.log(id);
        $.ajax({
            type: 'GET',
            url: '/api/settings/GetInterviewer/' + id,
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (data) {
                console.log('in the Get department method');
                console.log(data);
                $('#Interviewer').empty();
                $('#Interviewer').append('<option value="">' + 'Select Interviewer' + '</option>');
                $.each(data, function (index, val) {
                    $('#Interviewer').append('<option value="' + val.Id + '">' + val.name + '</option>');
                })
            },
        })
    }
    $('#RequisitionId').bind('change', function () {
        getInterviewer($(this).val());
    })

})();