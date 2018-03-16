
(function () {
    var count = 0;
    $('#add').on('click', function () {
        var current = count++;
        console.log(current);
        if (current < 2) {
            $('#positionForm').append(`
       <div class ="col-md-6">
            @Html.Label("Groups", htmlAttributes: new { @class = "control-label " })
            <div>
                @Html.DropDownList("GroupId", (SelectList) ViewBag.Groups, "Select Group", htmlAttributes: new { data_live_search = "true", data_size = "4", @class = "form-control bs-select" })
                @Html.ValidationMessage("GroupId", "", new { @class = "text-danger" })
            </div>
        </div>
                `);
        }
        else {
            $('#overload').removeClass('hidden');
        }
    })


})();