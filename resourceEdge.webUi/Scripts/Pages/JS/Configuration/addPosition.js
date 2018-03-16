
(function () {
    var count = 0;
    $('#add').on('click', function () {
        var current = count++;
        console.log(current);
        if (current < 2) {
            $('#positionForm').append(`
      <div class ="col-md-6">
            <label for="positionname" class ="control-label">Name</label>
            <input name="positionname[0]" id="" class ="form-control" required />
        </div>
        <div class ="col-md-6">
            <label for="description" class ="control-label">Description</label>
            <textarea cols="2" name="description[0]" id="" class ="form-control" required></textarea>
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