var dataTable;
$(document).ready(function () {
    loadData();
});

function loadData() {
    dataTable = $("#p").DataTable({
        "ajax": {
            "url": "/Admin/Orders/GetOrdersJson",
            "type": "GET",
        },
        "columns": [
            { "data": "id" },
            { "data": "name" },
            { "data": "applicationUser.email" },
            { "data": "orderStatus" },
            { "data": "totalPrice" },
            {
                "data": "id",
                "render": function (data) {
                    return `
                                <a href="/Admin/Orders/Details?orderId=${data}" class="btn custom-btn">Details</a>
                               
                            `
                }
            }
        ]
    });

}