(function () {
    app.controller('MainCtrl', function (Status) {
        console.log("MainCtrl Availble")
        var main = this;
        Status.getEmploymentStatus().then(function (response) {
            console.log(response);
            console.log("In The getEmploymentStatus function")
            main.mainStat = response.data;
        
        });
        main.check = function(empId){
            Status.getEmployeeCode(empId).then(function (response) {
                console.log("-------------------------------");
                console.log(response);
                main.code = response.data[0].EmployeeCode;
            });
        }

        main.postion = function (empId) {
            console.log("Inside position function")
            Status.getpositionByJob(empId).then(function (response) {
                console.log("Position Response Gotten");
                console.log(response);
                main.position = response.data;
                
            });
        }

        main.allJob = function () {
            console.log("Inside the job function");
            Status.getJobs().then(function (response) {
                console.log("Jobs response gotten");
                console.log(response);
                console.log(response.data[0].JobName)
                main.jobs = response.data;
                console.log(main.job);
            });
        }






        });
  })();