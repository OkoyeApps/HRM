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
                        try{

                       
                        $('#searchBody').append(`
                              <tr>
                                    <td id="tbluser">${element.FullName}</td>
                                    <td>
                                        <table>
                                            <tr>
                                                <td id="tblTime" class="pull-right">
                                                    <input name="amount" id="amountLeave" class ="form-control" value="" placeholder="Enter numbers only" data-parsley-type="number" data-parsley-range="[0, 1865]" data-parsley-range-message="This value should be between 0 and 1865, which is a provision for a 5 years leave "/>
                                                    <input type='hidden' Name='id' Value="${element.userId}"/>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            `)
                        }catch(ex){console.log(ex);};
                    });
                } else {
                    $('#noEmp').empty();
                    $('#noEmp').append(
                        `
                        <div class ="alert alert-danger alert-dismissable" style="text-align:center;"  >
                            <button type="button" class ="close" data-dismiss="alert" aria-hidden="true">&times; </button>
                            <span>No Employee in this department yet or all Employees in this department has been assigned.<br />To assign more leave please use the Employee Configuration Menu</span>
                        </div>
                        `);
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
    var searchValue = null;
    $('#departmentId').bind('change', function () {
        searchValue = $(this).val();
    });


    $('#searchEmp').on("click", function () {
        $('#getCount').addClass("hidden");
        if (searchValue != null) {
             GetEmpByDept(searchValue);
        }
    })

    

    $('#year').on('keypress', function (event) {
        return $.ValidateNumber(event, this);
    });
    
})();