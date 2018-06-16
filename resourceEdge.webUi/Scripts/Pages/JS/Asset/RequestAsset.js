(function () {
    function GetAssetByCategory(id) {
        $.ajax({
            type: 'GET',
            url: '/api/settings/GetAssetByCategory/' + id,
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (data) {
                console.log('in the GetRmByUserId method');
                console.log(data);
                    $('#asset').empty();
                if (data.length > 0) {
                    $('#asset').append('<option value="">' + 'Select Asset' + '</option>');
                    $.each(data, function (index, data) {
                        $('#asset').append('<option value="' + data.Id + '">' + data.name + '</option>');
                    })
                } else {

                    $('#asset').append('<option value="">' + 'No Asset in this category' + '</option>')
                }
            },
            failure: function () {
                var message = {
                    message: "Failed to load data... Try again later"
                };
                $('#asset').append('<option value="">' + message.message + '</option>')
            }
        })
    }
    $('#category').on('change', function () {
        console.log("######################")
        GetAssetByCategory($(this).val());
    });
})();