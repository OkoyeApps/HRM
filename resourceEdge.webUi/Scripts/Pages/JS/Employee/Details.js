(function () {
    (function (i, s, o, g, r, a, m) {
        i['GoogleAnalyticsObject'] = r; i[r] = i[r] || function () {
            (i[r].q = i[r].q || []).push(arguments)
        }, i[r].l = 1 * new Date(); a = s.createElement(o),
            m = s.getElementsByTagName(o)[0]; a.async = 1; a.src = g; m.parentNode.insertBefore(a, m)
    })(window, document, 'script', '../www.google-analytics.com/analytics.js', 'ga');

    ga('create', 'UA-42863888-9', 'auto');
    ga('send', 'pageview');

    function getEmpAvater(userid, passData) {
        return new Promise(function (resolve, reject) {
            $.ajax({
                type: 'GET',
                url: '/Employee/GetEmpAvater/' + userid,
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: resolve(function (data) {
                    passData(data);
                }),
                failure: reject(function () {
                    var message = {
                        message: "No image for this employee"
                    }
                })
            })
        });
    }

    function getTeamMember(searchString) {
        $.ajax({
            type: 'GET',
            url: '/employee/GetTeamMember?userid=' + '@Model.userId' + '&searchstring=' + searchString,
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (data) {
                console.log(data);
                $('#Teammember').empty();
                $.each(data, function (index, val) {
                    console.log(val)
                    getEmpAvater(val.userId, function (response) {
                        console.log("########")
                        console.log(response)
                        var path = response.replace("~", '/');
                        $('#Teammember').append(
                        `
                                                <div class ="user-w with-status status-green">
                                                    <div class ="user-avatar-w">
                                                        <div class ="user-avatar">

                                                            <img alt="" src="${path}" />
                                                        </div>
                                                    </div>
                                                    <div class ="user-name">
                                                        <h6 class ="user-title">${val.FullName}</h6>
                                                        <div class ="user-role">${val.Departments.deptname}</div>
                                                    </div>
                                                    <div class ="user-action">
                                                        <div class ="os-icon os-icon-email-forward">
                                                        </div>
                                                    </div>
                                                </div>

                            `
                        );
                    });
                });
            }
        })
    }

    $('#seachbox').on('change', function () {
        var searchstring = $('#seachbox').val();
        getTeamMember(searchstring);
    })
})()