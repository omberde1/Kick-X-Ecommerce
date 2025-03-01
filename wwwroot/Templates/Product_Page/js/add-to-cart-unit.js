$(".js-btn-minus, .js-btn-plus").on("click", function (e) {
    e.preventDefault();
    let input = $(this).closest('.input-group').find('.form-control');
    let value = parseInt(input.val());

    if ($(this).hasClass("js-btn-plus") && value < 10) {
        input.val(value + 1);  // Increase quantity
    } else if ($(this).hasClass("js-btn-minus") && value > 1) {
        input.val(value - 1);  // Decrease quantity
    }
});

function addToCartUnit() {
    var _productId = $("#product-id").val();
    var _productQuantity = $("#product-quantity").val();

    $.ajax({
        url: "/Customer/CartUnit",
        method: "POST",
        data: { productId: _productId, productQuantity: _productQuantity },
        success: function(response){
            if(response.success){
                toastr.success(response.message)
            }
            else{
                toastr.error(response.message)
            }
        },
        error: function(xhr){
            if(xhr.status === 401) { 
                // If user is not logged in, redirect to login page
                window.location.href = "/Customer/Login";  // Change to your actual login URL
            } else {
                toastr.error("Something went wrong.");
            }
        }

    });
}

    //     var sitePlusMinus = function () {
    //     $('.js-btn-minus').on('click', function (e) {
    //         e.preventDefault();
    //         let input = $(this).closest('.input-group').find('.product-quantity');
    //         let value = parseInt(input.val());
    
    //         if (value > 1) {  // Prevents going below 1
    //             input.val(value - 1);
    //         }
    //     });
    
    //     $('.js-btn-plus').on('click', function (e) {
    //         e.preventDefault();
    //         let input = $(this).closest('.input-group').find('.product-quantity');
    //         let value = parseInt(input.val());
            
    //         if(value < 11) { // Prevents going above 10
    //             input.val(value + 1);
    //         }
    //     });
    // };
    // sitePlusMinus();