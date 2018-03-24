(function () {
    function initializeTable(name){
        name.DataTable({
            colReorder: true,
            dom: 'Bfrtip',
            buttons: [
                'copy', 'csv', 'excel', 'pdf', 'print'
            ]
        });
}

    $(document).ready(function () {
        initializeTable($('#tblapproved'));
        initializeTable($('#tbldenied'));
        initializeTable($('#tblpending'));
        initializeTable($('#tblall'));
    
    })

    

})()