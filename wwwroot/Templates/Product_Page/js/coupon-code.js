$('.apply-coupon').on('click', function (event) {
	event.preventDefault();
	console.log(typeof toastr);
	toastr.error("Invalid/Expired Coupon Code");
});