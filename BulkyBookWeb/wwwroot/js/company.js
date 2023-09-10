﻿var dataTable;
$(document).ready(function () {
    loadDataTable();
})

function loadDataTable() {
    dataTable = $('#tblData').DataTable(
        {
            "ajax": { url: '/Admin/Company/GetAll' },
            "columns": [
                { data: 'name', "width": "25%" },
                { data: 'streetAddress', "width": "15%" },
                { data: 'city', "width": "10%" },
                { data: 'state', "width": "15%" },
                { data: 'phoneNumber', "width": "10%" },
                {
                    data: 'companyId',
                    "render": function (data) {
                        return `<a href="/Admin/Company/Upsert?id=${data}" class="btn btn-primary">
                        <i class="bi bi-pencil-square"></i> Edit
                    </a>
                    <a OnClick= Delete("/Admin/Company/Remove/${data}") class="btn btn-danger">
                        <i class="bi bi-trash"></i> Delete
                    </a>`
                    },
                    "width": "25%"
                }
            ]


        });
}

function Delete(url) {
    Swal.fire({
        title: 'Are you sure?',
        text: "You won't be able to revert this!",
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Yes, delete it!'
    }).then((result) => {
        if (result.isConfirmed) {
            $.ajax({
                url: url,
                type: 'DELETE',
                success: function (data) {
                    dataTable.ajax.reload();
                    toastr.success(data.message);
                }
            });
        }
    })
}

