
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
            $('#overload').empty();
            $('#overload').append(`<div class="alert alert-danger alert-dismissable">
                <button type="button" class ="close" data-dismiss="alert" aria-hidden="true">&times; </button>
    <span>Sorry you can't add more than Three(3) Employment Status at a time. kindly submit and then add again</span>
</div>`);
        }
    })


})();