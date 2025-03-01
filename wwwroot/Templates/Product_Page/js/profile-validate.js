$(document).ready(function () {
    $('#profileForm').submit(function (e) {
        let isValid = true;

        // Clear previous errors
        $('.text-danger').text('');

        // Validate Name
        if ($('#customer_name').val().trim() === '') {
            $('#nameError').text('Name is required');
            isValid = false;
        }

        // Validate Address
        if ($('#customer_address').val().trim() === '') {
            $('#addressError').text('Address is required');
            isValid = false;
        }

        // Validate City
        if ($('#customer_city').val().trim() === '') {
            $('#cityError').text('City is required');
            isValid = false;
        }

        // Validate Postal Code
        if ($('#customer_pincode').val().trim() === '') {
            $('#pincodeError').text('Postal code is required');
            isValid = false;
        }

        // Validate Phone
        if ($('#customer_phone').val().trim() === '') {
            $('#phoneError').text('Phone is required');
            isValid = false;
        }

        // Validate Country
        if ($('#customer_country').val().trim() === '') {
            $('#countryError').text('Country is required');
            isValid = false;
        }

        // Prevent form submission if validation fails
        if (!isValid) {
            e.preventDefault();
        }
        
    });
});