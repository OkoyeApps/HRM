(function () {
    console.log("___________________________");
    var error = $("#erroralert");
    var success = $("#successalert");
    var warning = $("#warningalert");
    var info = $("#infoalert");
    if (error != undefined) {
        console.log(error[0].innerText);
        if (error[0].innerText.toLowerCase() == "x") {
            error.addClass("hidden");
            console.log(error[0].innerText);
        }
    }
    if (success != undefined) {
        console.log(success);
        if (success[0].innerText.length == 2) {
            success.addClass("hidden");
        }
       
    }
    if (warning != undefined) {
        if (warning[0].innerText.length == 2) {
            warning.addClass("hidden");
        }
       
    }
    if (info != undefined) {
        if (info[0].innerText.length == 2) {
            info.addClass("hidden");
        }   
    }
})();
 