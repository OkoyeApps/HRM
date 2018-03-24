function getLocationByGroup(id) {
    $.ajax({
        type: 'GET',
        url: '/api/settings/GetLocationByGroup/' + id,
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            console.log(data);
            $('#locationId').empty();
            if (data.length != 0) {
                $('#locationId').append('<option class="centre" value="">' + 'Select Location' + '</option>');
                $.each(data, function (index, val) {
                    $('#locationId').append('<option value="' + val.Id + '">' + val.State + '</option>');
                })
            } else {
                $('#locationId').append('<option value="">' + 'No Location for this Group yet' + '</option>')
            }
        },
        failure: function () {
            console.log("Failed");
            var message = {
                message: "Failed to load data... Try again later"
            };
            $('#locationId').append('<option value="">' + message.message + '</option>')
        }
    })
}

$('#GroupId').on('change', function () {
    getLocationByGroup($(this).val());
})