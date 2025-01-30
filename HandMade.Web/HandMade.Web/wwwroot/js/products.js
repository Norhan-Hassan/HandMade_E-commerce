var dataTable;
$(document).ready(function () {
    loadData();
});

function loadData() {
    dataTable = $("#p").DataTable({
        "ajax": {
            "url": "/Admin/Product/GetProductsJson",
            "type": "GET",
        },
        "columns": [
            { "data": "name" },
            { "data": "description" },
            { "data": "price" },
            { "data": "quantity" },
            { "data": "category.name" },
            {
                "data": "id",
                "render": function (data) {
                    return `
                                <a href="/Admin/Product/Edit/${data}" class="btn btn-sm btn-outline-secondary me-2">Edit</a>

                                <a href="/Admin/Product/Delete/${data}"class="btn btn-sm btn-outline-secondary me-2>Delete</a>
                            `
                }
            }
        ]
    });

}