$(document).ready(function () {
    getProducts();
})

function getProducts() {
    $.ajax({
        url: "Products/GetProducts",
        type: "GET",
        success: function (data) {
            console.log(data);
            //var object = '';
            //$.each(data, function (index, item) {
            //    object += `<input type="hidden" value="` + item.id + `" />`;
            //    object += `<tr>`;
            //    object += `<td>` + item.name + `</td>`;
            //    object += '<td><a href="#" onclick="DetailsCategoryPopup(' + item.id + ')">Details</a> | <a href="#" onclick="UpdateCategoryPopup(' + item.id + ')">Edit</a> | <a href="#" onclick="DeleteCategoryPopup(' + item.id + ')">Delete </a> | <a href="#" onclick="AddProductsPopup(' + item.id + ')">Add/Remove Products</a></td>'
            //    object += `</tr>`;
            //});
            //$('#categoryTable').html(object);
        },
        error: function (error) {
            console.log(error)
        }
    })
}

function createProduct() {
    var name = $("#nameProduct").val();
    var description = $("#descriptionProduct").val();
    var availability = $("#availabilityProduct").val();
    var image = $("#imageProduct")[0].files[0];

    var formData = new FormData();
    formData.append("Name", name);
    formData.append("Description", description);
    formData.append("Availability", availability);
    formData.append("ProfileImage", image);

    console.log(formData);

    $.ajax({
        url: "/Products/Create",
        type: "POST",
        data: formData,
        contentType: false,
        processData: false,
        success: function (data) {
            console.log(data);
        },
        error: function (error) {
            console.log(error);
        }
    });
}