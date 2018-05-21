(function () {
    var count = 0;
    $('#add').on('click', function () {
        var current = count++;
        console.log(current);
        if (current < 1) {
            $('#jobForm').append(`
        <div class ="col-md-6">
            <label for="jobtitlename" class ="control-label">Name</label>
            <input name="jobtitlename[0]" id="Country" class ="form-control" required />
        </div>

        <div class ="col-md-6">
            <label for="minexperiencerequired" class ="control-label">Minimum Experienced</label>
            <input name="minexperiencerequired[0]" id="minexp" class ="form-control" required data-parsley-min="0" />
        </div>

        <div class ="col-md-6">
            <label for="jobtitlecode" class ="control-label">Job Code</label>
            <input name="jobtitlecode[0]" id="City" class ="form-control" required />
        </div>

        <div class ="col-md-6">
            <label for="jobpayfrequency" class ="control-label">Job Code</label>
            <input name="jobpayfrequency[0]" id="City" class ="form-control" required />
        </div>
        <div class ="col-md-6">
            <label for="jobpaygradecode" class ="control-label">Job Code</label>
            <input name="jobpaygradecode[0]" id="City" class ="form-control" required />
        </div>
        <div class ="col-md-6">
            <label for="jobdescription" class ="control-label">Address1</label>
            <textarea cols="2" name="jobdescription[0]" id="Address1" class ="form-control" required></textarea>
        </div>

        <div class ="col-md-6">
            <label for="Address2" class ="control-label">Address2</label>
            <textarea cols="2" name="Address2[0]" id="Address2" class ="form-control"></textarea>
        </div>

         <div class ="col-md-12">
            <hr />
        </div>
                `);
        }
        else {
            $('#overload').empty();
            $('#overload').append(`<div class="alert alert-danger alert-dismissable" id="overload">
    <button type="button" class="close" data-dismiss="alert" aria-hidden="true">&times;</button>
    <span>Sorry you can't add more than Two(2) Jobs at a time. kindly submit and then add again</span>
    </div>`)
            //.toggleClass('hidden').addClass("alert-dismiss");
        }
    })


    $('#minexp').on('keypress', function (event) {
        return $.ValidateNumber(event, this);
    });

})();