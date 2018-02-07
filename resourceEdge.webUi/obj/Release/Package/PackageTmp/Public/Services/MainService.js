(function () {
    app.factory('Status', function ($http) {
        var mainServiceFactory = {};
        mainServiceFactory.getEmploymentStatus = function () {
            return $http({
                url: 'http://localhost:58124/api/Settings/Getempstatus',
                method: "GET",
            });
        };
        mainServiceFactory.getEmployeeCode = function () {
            return $http({
                url: 'http://localhost:58124/api/settings/getemployeecode/',
                method: "GET",
            });
        }
        mainServiceFactory.getpositionByJob = function (empId) {
            return $http.get('http://localhost:58124/api/Settings/GetPositionById/' + empId);
        }

        mainServiceFactory.getJobs = function () {
            return $http.get('http://localhost:58124/api/settings/getjobs');
        }
        













        return mainServiceFactory;
    });

})();
