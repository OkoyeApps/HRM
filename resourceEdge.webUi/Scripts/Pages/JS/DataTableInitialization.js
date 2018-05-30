(function(){
    function initializeTable(name) {
    name.DataTable({
        colReorder: {
            realtime: false
        },
        dom: 'Bfrtip',
        buttons: [
            'copy', 'csv', 'excel', 'pdf', 'print'
        ]
    });
}

    $(document).ready(function () {
        console.log("In the document mwthod");
    initializeTable($('#tblGeneral'));

})
})();
