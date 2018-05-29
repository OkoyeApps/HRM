
(function () {
    var count = 0;
    $('#add').on('click', function () {
        var current = count++;
        console.log(current);
        if (current < 2) {
            $('#prefixForm').append(`
             <div class ="col-md-6">
            <label for="prefixName" class ="control-label">Name</label>
            <input name="prefixName[${count}]" id="City" class ="form-control" required />
        </div>
        <div class ="col-md-6">
            <label for="description" class ="control-label">Description</label>
            <textarea cols="2" name="description[${count}]" id="Address1" class ="form-control"></textarea>
        </div>
           <div class="col-md-12">
            <hr />
        </div>
                `);
        }
        else {
            $('#overload').empty();
            $('#overload').append(`<div class="alert alert-danger alert-dismissable">
                <button type="button" class ="close" data-dismiss="alert" aria-hidden="true">&times; </button>
    <span>Sorry you can't add more than Three(3) Prefix(es) at a time. kindly submit and then add again</span>
</div>`);
            
        }
    })


})();