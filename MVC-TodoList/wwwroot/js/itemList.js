var dataTable;

$(document).ready(function () {
    loadDataTable();
});

function loadDataTable() {
    dataTable = $('#DT_load').DataTable({
        "ajax": {
            "url": "/items/getall/",
            "type": "GET",
            "datatype": "json"
        },
        "columns": [
            { "data": "title", "width": "38%" },       
            {
                "data": "dueDate",                 //first character needs to be lowercase
                "render": function (data) {
                    return `<div class="text-center">
                            ${data.substring(0, 10)}
                        </div>`;
                }, "width": "13%"
            } ,
            { "data": "priority", "width": "12%" },
            { "data": "status", "width": "12%" },
            {
                "data": "id",
                "render": function (data) {
                    return `<div class="text-center">
                        <a href="/Items/Create?id=${data}" class='btn btn-success text-white mt-1' style='cursor:pointer; width:70px;'>
                            Edit
                        </a>
                        &nbsp;
                        <a class='btn btn-danger text-white mt-1' style='cursor:pointer; width:70px;'
                            onclick=Delete('/items/Delete?id='+${data})>
                            Delete
                        </a>
                        </div>`;
                }, "width": "25%"
            }
        ],
        "language": {
            "emptyTable": "no data found"
        },
        "width": "100%"
    });
}

function Delete(url) {
    swal({
        title: "Are you sure?",
        text: "Once deleted, you will not be able to recover",
        icon: "warning",
        buttons: true,
        dangerMode: true
    }).then((willDelete) => {
        if (willDelete) {
            $.ajax({
                type: "DELETE",
                url: url,
                success: function (data) {
                    if (data.success) {
                        toastr.success(data.message);
                        dataTable.ajax.reload();
                    }
                    else {
                        toastr.error(data.message);
                    }
                }
            });
        }
    });
}