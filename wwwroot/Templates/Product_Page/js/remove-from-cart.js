$(document).on("click", ".removeFromCart", function () {
    var _productId = $(this).data("productid");
    var productIdRow = $("#cart-item-" + _productId);
    var productIdTotal = parseFloat($("#cart-item-" + _productId).find(".per-item-total").attr("data-subtotal"));

    let subTotal = 0;
    $(".per-item-total").each(function () {
        subTotal += parseFloat($(this).attr("data-subtotal")); // Important .each() 
    });

    $.ajax({
        url: "/Customer/CartRemove",
        method: "POST",
        data: { productId: _productId },
        success: function(response){
            if(response.success){
                console.log(response.message)
                // prodcutIdRow.remove();
                $("#subtotal-display").html(`<strong class="text-black">${(subTotal-productIdTotal).toFixed(2)}</strong>`);
                productIdRow.fadeOut(300, function () { // for fade out removal
                    $(this).remove();
                });

                // Delay the redirection to allow fade-out to complete
                if(response.message === "Success. Cart empty!"){
                setTimeout(function () {
                    window.location.href = "/Customer/Cart";
                }, 300); // 300ms delay
              }  
            }
            else{
                console.log(response.message)
            }
        },
        error: function(){
            console.log("Something went wrong.")
        }

    });
});