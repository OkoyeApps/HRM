(function () {
    var count = 0;
    $('#add').on('click', function () {
        var current = count++;
        console.log(current);
        if (current < 2) {
            $('#jobForm').append(`
        <div class ="col-md-3">
            <label for="jobtitlename" class ="control-label">Name&nbsp; <span style="color:red">*</span></label>
            <input name="jobtitlename[${count}]" id="Country" class ="form-control" required />
        </div>

        <div class ="col-md-3">
            <label for="minexperiencerequired" class ="control-label">Minimum Experience&nbsp;<span style="color:red">*</span></label>
            <input name="minexperiencerequired[${count}]" id="minexp" class ="form-control" required data-parsley-min="0" />
        </div>

        <div class ="col-md-3">
            <label for="jobtitlecode" class ="control-label">Job Code&nbsp; <span style="color:red">*</span></label>
            <input name="jobtitlecode[${count}]" id="City" class ="form-control" required />
        </div>

        <div class ="col-md-3">
            <label for="jobpayfrequency" class ="control-label">Job Pay Frequency</label>
            <input name="jobpayfrequency[${count}]" id="City" class ="form-control"  />
        </div>
        <div class ="col-md-3" style="clear:both;">
            <label for="jobpaygradecode" class ="control-label">Pay Grade Code</label>
            <input name="jobpaygradecode[${count}]" id="City" class ="form-control"  />
        </div>
        <div class ="col-md-3">
            <label for="jobdescription" class ="control-label">Description</label>
            <textarea cols="2" name="jobdescription[${count}]" id="Address1" class ="form-control"></textarea>
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
    <span>Sorry you can't add more than Two(3) Jobs at a time. kindly submit and then add again</span>
    </div>`)
            //.toggleClass('hidden').addClass("alert-dismiss");
        }
    })


    $('#minexp').on('keypress', function (event) {
        return $.ValidateNumber(event, this);
    });

})();