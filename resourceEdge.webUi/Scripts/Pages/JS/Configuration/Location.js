(function(){
    var count = 0;
    $('#add').on('click', function () {
        var current = count++;
        console.log(current);
        if (current < 2) {
            $('#locationForm').append(`
        <div class ="col-md-6">
            <label for="Country" class ="control-label">Country</label>
            <input name="Country[${count}]" id="Country" class ="form-control" required />
        </div>
         
        <div class ="col-md-6">
            <label for="State" class ="control-label">State</label>
            <input name="State[${count}]" id="State" class ="form-control" required />
        </div>


        <div class ="col-md-6">
            <label for="City" class ="control-label">City</label>
            <input name="City[${count}]" id="City" class ="form-control" required />
        </div>

        <div class ="col-md-6">
            <label for="Address1" class ="control-label">Address1</label>
            <textarea cols="2" name="Address1[${count}]" id="Address1" class ="form-control" required></textarea>
        </div>

        <div class ="col-md-6">
            <label for="Address2" class ="control-label">Address2</label>
            <textarea cols="2" name="Address2[${count}]" id="Address2" class ="form-control"></textarea>
        </div>
             <div class ="col-md-12">
                <hr />
            </div>
                `);
        }
        else {
            $('#overload').empty();
            $('#overload').append(`<div class="alert alert-danger">
            <button type="button" class="close" data-dismiss="alert" aria-hidden="true">&times;</button>
    <span>Sorry you can't add more than Three(3) Location(s) at a time. kindly submit and then add again</span>
</div>`);
        }
        })


})();