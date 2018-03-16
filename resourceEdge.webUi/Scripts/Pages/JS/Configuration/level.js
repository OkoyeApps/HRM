
(function () {
    var count = 0;
    $('#add').on('click', function () {
        var current = count++;
        console.log(current);
        if (current < 2) {
            $('#levelForm').append(`
        <div class ="col-md-6">
            <label for="LevelName" class ="control-label">Name</label>
            <input name="LevelName[0]" id="City" class ="form-control" placeholder="Name for level" required />
        </div>

        <div class ="col-md-6">
            <label for="EligibleYears" class ="control-label">Eligible Year(s) </label>
            <input name="EligibleYears[0]" id="City" class ="form-control" placeholder="Eligible years for level" type="number" required />
        </div>

        <div class ="col-md-6">
            <label for="levelNo" class ="control-label">Level Number</label>
            <input name="levelNo[0]" id="City" class ="form-control" type="number" placeholder="level number in ranking" required />
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