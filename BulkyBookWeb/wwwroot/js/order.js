var dataTable;
$(document).ready(function () {
    loadDataTable();
})

function loadDataTable()
{
    dataTable = $('#tblData').DataTable(
        {
            "ajax": { url: '/Admin/Order/GetAll' },
            "columns": [
                { data: 'id',"width": "15%" },
                { data: 'name', "width": "25%" },
                { data: 'applicationUser.phoneNumber', "width": "10%" },
                { data: 'applicationUser.email', "width": "15%" },
                { data: 'orderStatus', "width": "10%" },
                { data: 'orderTotal', "width": "10%" },
                {
                    data: 'id',
                    "render": function (data) {
                        return `<a href="/Admin/Order/Details?id=${data}" class="btn btn-primary">
                        <i class="bi bi-pencil-square"></i> Edit
                    </a>
                    `
                    },
                    "width" : "15%"
                }
            ]


        });
}

function Delete(url)
{
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

