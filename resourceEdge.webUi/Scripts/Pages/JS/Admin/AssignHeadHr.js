(function(){
    function GetAllHrsByGroups(id) {
        $.ajax({
            type: 'GET',
            url: 'http://localhost:58124/api/settings/GetAllHrsByGroup/' + id,
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (data) {
                console.log('in the GetAllHR method');
                console.log(data);
                $('#emp').empty();
                if (data != '') {
                    $('#emp').append('<option value="">' + 'Select Hr' + '</option>');
                    $.each(data, function (index, val) {
                        $('#emp').append('<option value="' + val.userId + '">' + val.FullName + '</option>');
                    })
                } else {
                    $('#emp').append('<option value="">' + 'No HR For this Group' + '</option>')
                }
            },
            failure: function () {
                var message = {
                    message: "Failed to load data... Try again later"
                };
                $('#emp').append('<option value="">' + message.message + '</option>')
            }
        })
    }

    $('#groupId').on('change', function () {
        GetAllHrsByGroups($(this).val());
    })

})();