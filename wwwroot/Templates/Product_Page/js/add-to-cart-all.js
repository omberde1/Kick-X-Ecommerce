$(".js-btn-minus, .js-btn-plus").on("click", function () {
    let input = $(this).closest(".input-group").find(".product-quantity"); // Get quantity input
    let row = input.closest("tr");  // Get the row
    let totalCell = row.find(".per-item-total"); // Get total cell
    let pricePerItem = parseFloat(totalCell.data("price")); // Get price from data-attribute
    let productId = input.data("productid");


    let newQuantity = parseInt(input.val());
    if ($(this).hasClass("js-btn-plus") && newQuantity < 10) {
        newQuantity += 1;  // Increase quantity
    } else if ($(this).hasClass("js-btn-minus") && newQuantity > 1) {
        newQuantity -= 1;  // Decrease quantity
    }

    input.val(newQuantity);  // Update input field
    let newTotal = pricePerItem * newQuantity;
    totalCell.html(`<span>&#8377;</span>${newTotal.toFixed(2)}`); // Update total price
    totalCell.attr("data-subtotal", newTotal); // Updated data-subtotal

    let subTotal = 0;
    $(".per-item-total").each(function () {
        subTotal += parseFloat($(this).attr("data-subtotal")); // Important .each() 
    });
    $("#subtotal-display").html(`<strong class="text-black">${subTotal.toFixed(2)}</strong>`);

    updateCart(productId, newQuantity);
});



function updateCart(_productId, _productQuantity) {
    // Send AJAX request
    $.ajax({
        url: "/Customer/CartAll",
        method: "POST",
        data: {
            productId: _productId,
            productQuantity: _productQuantity
        },
        success: function (response) {
            if (response.success) {
                console.log("Cart updated successfully!");
            } else {
                console.log("Error updating cart: " + response.message);
            }
        },
        error: function () {
            console.log("Something went wrong.");
        }
    });
}