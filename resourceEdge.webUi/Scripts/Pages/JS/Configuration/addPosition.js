
(function () {
    var count = 0;
    $('#add').on('click', function () {
        var current = count++;
        console.log(current);
        if (current < 2) {
            $('#positionForm').append(`
      <div class ="col-md-6">
            <label for="positionname" class ="control-label">Name</label>
            <input name="positionname[${count}]" id="" class ="form-control" required="" />
        </div>
        <div class ="col-md-6">
            <label for="description" class ="control-label">Description</label>
            <textarea cols="2" name="description[${count}]" id="" class ="form-control" ></textarea>
        </div>

        <div class ="col-md-12">
            <hr />
        </div>

                `);
        }
        else {
            $('#overload').empty();
            $('#overload').append(`
                <div class ="alert alert-danger alert-dismissable">
                <button type="button" class ="close" data-dismiss="alert" aria-hidden="true">&times; </button>
         <span>Sorry you can't add more than Three(3) Position(s) at a time. kindly submit and then add again</span>
</div>`);
            console.log($('#overload'));
        }
    })


})();