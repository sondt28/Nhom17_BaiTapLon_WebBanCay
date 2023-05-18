$(document).ready(function () {

    ShowProductData();
})

function ShowProductData() {
    var count = 1;
    $.ajax({
        url: "/Products/GetProducts",
        type: "GET",
        success: function (data) {
            var object = '';
            $.each(data, function (index, item) {
                object += `<input type="hidden" value="` + item.id + `" />`;
                object += `<tr>`;
                object += `<td>` + count++ + `</td>`;
                object += `<td>` + item.name + `</td>`;
                object += `<td><img src='./images/` + item.image + `' width="30" height="30 alt="Smiley face""/></td>`;
                object += `<td>` + item.availability + `</td>`;
                object += `<td>` + item.category.name + `</td>`;
                object += `<td>` + item.description + `</td>`;
                object += '<td><a href="#" onclick="DetailsProductPopup(' + item.id +')">Detail</a> | <a href="#" onclick="EditProductPopup(' + item.id + ')">Edit</a></td>'
                object += `</tr>`;
            });
            $('#productTable').html(object);
        },
        error: function (error) {
            console.log(error);
        }
    });
}

$("#btnCreateProduct").click(function () {
    $("#ProductModal").modal('show');
    
    $.ajax({
        url: "/Products/Create",
        type: "GET",
        success: function (data) {
            var object = '<option value="">--select--</option>';
            $.each(data, function (index, item) {
                object += `<option value="${item.id}">${item.name}</option>`;
            });
            $("#createCategoriesSelection").html(object);
        },
        error: function (error) {
            console.log(error);
        }
    });
})

function CreateProduct() {
    var name = $("#nameProduct").val();
    var description = $("#descriptionProduct").val();
    var availability = $("#availabilityProduct").is(":checked");
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
            window.location.href = ''
        },
        error: function (error) {
            console.log(error);
        }
    });
}

function EditProductPopup(id) {
    $.ajax({
        url: 'Products/Update?id=' + id,
        type: 'GET',
        contentType: 'application/json;character=utf-8',
        dataType: 'json',
        success: function (data) {
            console.log(data);
            $("#UpdateProductModal").modal('show');
            $("#id").val(data.product.id);
            $("#updateNameProduct").val(data.product.name);
            $("#updateDescriptionProduct").val(data.product.description);
            /*$("#updateImageProduct")[0].files[0] = data.product.image;*/
            $("#updateAvailabilityProduct").prop("checked", data.product.availability);
            var object = '<option value="">--select--</option>';
            $.each(data.categories, function (index, item) {
                object += `<option value="${item.id}">${item.name}</option>`;
            });
            $("#updateCategoriesSelection").html(object);
        },
        error: function () {
            alert("Data not found"); 
        }
    })
}

function UpdateProduct() {
    var id = $("#id").val()
    var name = $("#updateNameProduct").val()
    var description = $("#updateDescriptionProduct").val()
    var image = $("#updateImageProduct")[0].files[0]
    var availability = $("#updateAvailabilityProduct").is(":checked")
    var categoryId = $("#updateCategoriesSelection").val();

    var formData = new FormData();
    formData.append("Id", id);
    formData.append("Name", name);
    formData.append("Description", description);
    formData.append("Availability", availability);
    formData.append("ProfileImage", image);
    formData.append("CategoryId", categoryId);

    $.ajax({
        url: "/Products/Update",
        type: "POST",
        data: formData,
        contentType: false,
        processData: false,
        success: function (data) {
            console.log(data);
            $("#UpdateProductModal").modal('hide');
            ShowProductData()
        },
        error: function (error) {
            console.log(error);
        }
    });
}

function DetailsProductPopup(id) {
    $.ajax({
        url: "Products/Details?id=" + id,
        type: "GET",
        contentType: "application/x-www-form-urlencoded",
        dateType: "json",
        success: function (data) {
            console.log(data);
            $("#DetailsProductModal").modal("show");
            var product = `<span>+ Tên: ${data.name}</span>
                            <br><span>+ Khả dụng: ${data.availability}</span>
                            <br><span>+ Mô tả: ${data.description}</span>
                            <br><span>+ Ảnh: <img src='./images/${data.image}' width="30" height="30 alt="Smiley face""/></span>
                            <h5>- Danh mục</h5>
                            <span>+ Tên: ${data.category.name}</span>
                            <h5>- Biến thể sản phẩm</h5>
                            `
            $.each(data.productOptions, function (index, item) {
                product += `<span>+ ${item.id}: ${item.name}</span><br>`
            });
            $("#product").html(product);
        },
        error: function (error) {
            console.log(error)
        }
    })
}

function HideModal() {
    $('#ProductModal').modal('hide');
}
