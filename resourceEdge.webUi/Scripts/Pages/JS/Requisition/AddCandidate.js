(function () {
    window.isResume = false;
    window.isDetails = false;

    function DisableHiddenField() {
        $('#resume').toggleClass('hidden');

    }
    $('#submit').on('click', function () {
        DisableHiddenField();
    });

    var questiondetailToAdd = `
           <div name="detail" style="clear:both;">

    <div class ="col-md-4">
        <div class ="form-group">
            <label class ="control-label " for="Qualification">Qualification</label>
            <input class ="form-control text-box single-line" id="Qualification" name="Qualification" type="text" value="" required="">
        </div>
    </div>
    <div class ="col-md-4">
        <div class ="form-group">
            <label class ="control-label " for="Experience">Experience</label>
            <input class ="form-control text-box single-line" id="Experience" name="Experience" type="number" value="" required="">
        </div>
    </div>
  
    <div class ="col-md-4">
        <div class ="form-group">
            <label class ="control-label " for="Country">Country</label>
            <input class ="form-control text-box single-line" id="Country" name="Country" type="text" value="" required="">
        </div>
    </div>

    <div class ="col-md-4">
        <div class ="form-group">
            <label class ="control-label " for="State">State</label>
            <input class ="form-control text-box single-line" id="State" name="State" type="text" value="" required="">

        </div>
    </div>

    <div class ="col-md-4">
        <div class ="form-group">
            <label class ="control-label " for="City">City</label>
            <input class ="form-control text-box single-line" id="City" name="City" type="text" value="" required="">
        </div>
    </div>
      <div class ="col-md-4">
        <div class ="form-group">
            <label class ="control-label " for="EducationSummary">Education Summary</label>
            <input class ="form-control text-box single-line" id="EducationSummary" name="EducationSummary" type="text" value="">
        </div>
    </div>
   
</div>
        `
    ;
    var company = `
         <span style="clear:both" class="col-md-4"><p><b>Previous Company Detail</b></p></span>
         <div style="clear:both">
    <div class ="col-md-4">
        <div class ="form-group">
            <label class ="control-label " for="CandidateWork_CompanyName"> Name</label>
            <input class ="form-control text-box single-line" id="CandidateWork_CompanyName" name="CandidateWork.CompanyName" type="text" value="">
        </div>
    </div>
    <div class ="col-md-4">
        <div class ="form-group">
            <label class ="control-label " for="CandidateWork_CompanyDesignation"> Designation</label>
            <input class ="form-control text-box single-line" id="CandidateWork_CompanyDesignation" name="CandidateWork.CompanyDesignation" type="text" value="">
        </div>
    </div>
    <div class ="col-md-4">
        <div class ="form-group">
            <label class ="control-label " for="CandidateWork_CompanyFrom">From</label>
            <input class ="form-control text-box single-line" data-val="true" data-val-date="The field CompanyFrom must be a date." id="CandidateWork_CompanyFrom"
                name="CandidateWork.CompanyFrom" type="date" value="">

        </div>
    </div>
    <div class ="col-md-4">
        <div class ="form-group">
            <label class ="control-label " for="CandidateWork_CompanyTo">To</label>
            <input class ="form-control text-box single-line" id="CandidateWork_CompanyTo" name="CandidateWork.CompanyTo" type="date"
                value="" />
        </div>
    </div>
    <div class ="col-md-4">
        <div class ="form-group">
            <label class ="control-label " for="CandidateWork_CompanyPhoneNumber"> PhoneNumber</label>
            <input class ="form-control text-box single-line" id="CandidateWork_CompanyPhoneNumber" name="CandidateWork.CompanyPhoneNumber"
                type="text" value="">
        </div>
    </div>
    <div class ="col-md-4">
        <div class ="form-group">
            <label class ="control-label " for="CandidateWork_CompanyWebsite">CompanyWebsite</label>
            <input class ="form-control text-box single-line" id="CandidateWork_CompanyWebsite" name="CandidateWork.CompanyWebsite" type="text" value="">
        </div>
    </div>
    <div class ="col-md-4">
        <div class ="form-group">
            <label class ="control-label " for="CandidateWork_CompanyAddress">CompanyAddress</label>
            <input class ="form-control text-box single-line" id="CandidateWork_CompanyAddress" name="CandidateWork.CompanyAddress" type="text" value="">
        </div>
    </div>
</div>
        `
    var resume = `
         <div style="clear:both">
                <div class ="col-md-10">
           <div class ="form-group">
                <label class ="control-label col-md-2 " for="CandidateWork_CompanyAddress">CompanyAddress</label>
                    <input class ="form-control text-box single-line col-md-4" id="Resume" name="File" type="file" value="" required = "" accept=".pdf,.doc,.docx">
                </div>
            </div>
        `

    $('#upload-resume').on('click', function () {
        $("#entryPoint").empty();
        $('#workdetail').empty();
            $('#detail').remove();
            $('#entryPoint').append(resume);
           
    })

    $('#candidate-form').on('click', function () {
        $("#entryPoint").empty();
        $('#workdetail').empty();
        $("#entryPoint").append(questiondetailToAdd);
        $('#workdetail').append(company);
        $("#resume").remove();
       
    })  

})();
