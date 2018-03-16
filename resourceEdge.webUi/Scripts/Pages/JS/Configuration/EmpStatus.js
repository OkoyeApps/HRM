
(function () {
    var count = 0;
    $('#add').on('click', function () {
        var current = count++;
        console.log(current);
        if (current < 2) {
            $('#empForm').append(`
        <div class ="col-md-6">
            <label for="employemntStatus" class ="control-label">Position Name</label>
            <input name="employemntStatus[0]" id="employemntStatus" class ="form-control" required />
        </div>

        <div class ="col-md-12">
            <hr />
        </div>
                `);
        }
        else {
            $('#overload').removeClass('hidden');
        }
    })


})();