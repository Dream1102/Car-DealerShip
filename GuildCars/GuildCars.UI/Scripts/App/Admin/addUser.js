$(document).ready(function () {
    console.log('ready');

});

function validateForm() {
    $('#formAddUser').submit(function (e) {

        var firstName = $('#firstName').val();
        var lastName = $('#lastName').val();
        var userEmail = $('#userEmail').val();
        var newPassword = $('#newPassword').val();
        var confirmPassword = $('#confirmPassword').val();

        $(".error").remove();
        if (firstName.length < 1) {
            $('#fName').after('<span class="error">  This field is required.</span>');
            e.preventDefault();
        }
        if (lastName.length < 1) {
            $('#lName').after('<span class="error">  This field is required.</span>');
            e.preventDefault();
        }
        if (userEmail.length < 1) {
            $('#email').after('<span class="error">  This field is required.</span>');
            e.preventDefault();
        }
        if (newPassword.length < 1) {
            $('#nPassword').after('<span class="error">  This field is required.</span>');
            e.preventDefault();
        }
        if (confirmPassword.length < 1) {
            $('#cPassword').after('<span class="error">  This field is required.</span>');
            e.preventDefault();
        }
        if (newPassword.length > 0 && confirmPassword.length > 0) {
            if (newPassword != confirmPassword) {
                $('#pageTitle').after('<span class="error">  Passwords do not match!</span>');
                e.preventDefault();
            }
        }
    })
};