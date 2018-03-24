(function () {


    function getDepartmentByBusinessUnit(id) {
        console.log(id);
        $.ajax({
            type: 'GET',
            url: '/api/Settings/GetDepartmentsById/' + id,
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (data) {

                console.log('in the Get department method');
                console.log(data);
                $('#departmentId').empty();
                $('#departmentId').append('<option value="">' + 'Select department' + '</option>');
                $.each(data, function (index, val) {
                    $('#departmentId').append('<option value="' + val.deptId + '">' + val.deptName + '</option>');
                })
            },
        })
    }
    $('#businessunitId').bind('change', function () {
        getDepartmentByBusinessUnit($(this).val());
    })

    function GetEmpByDept(id) {
        $.ajax({
            type: 'GET',
            url: '/api/Settings/GetEmpByDeptForLeave/' + id,
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (data) {
                if (data.length > 0) {
                    $('#tbluser').empty();
                    $('#searchBody').empty();
                    data.forEach(function (element, index) {
                        console.log(element);
                        console.log(index);
                        $('#getCount').removeClass("hidden");
                       
                        //$('#tbluser').append("<tr><td>" + element.FullName + "</td><br /></tr>");
                        //$('#tblTime').append("<tr><td><input name='amount' class='form-control value='' /> <br /></td><input type='hidden' Name='id' Value='" + element.userId + "'/></tr>'")
                        $('#searchBody').append(`
                              <tr>
                                    <td id="tbluser">${element.FullName}</td>
                                    <td>
                                        <table>
                                            <tr>
                                                <td id="tblTime" class="pull-right">
                                                    <input name="amount" class ="form-control" value="" />
                                                    <input type='hidden' Name='id' Value="${element.userId}"/>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            `)
                    });
                } else {
                    $('#noEmp').removeClass("hidden");
                }
            },
            failure: function () {
                var message = {
                    message: "Failed to load data... Try again later"
                };
                $('#userId').append('<option value="">' + message.message + '</option>')
            }
        })
    }
    $('#departmentId').bind('change', function () {
        searchValue = $(this).val();
        $('#noEmp').addClass("hidden");
    });

    var searchValue = null;

    $('#searchEmp').on("click", function () {
        $('#getCount').addClass("hidden");
        $('#noEmp').addClass("hidden");
        GetEmpByDept(searchValue);
    })

})();