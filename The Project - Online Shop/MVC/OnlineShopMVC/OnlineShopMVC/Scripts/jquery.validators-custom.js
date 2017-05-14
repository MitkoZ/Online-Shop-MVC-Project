
jQuery.validator.addMethod("validatepassword", function (value, element, param) {
    var hasLetters = value.match(/[a-zA-Z]+/);
    var hasDigits = value.match(/[0-9]+/);

    return hasLetters && hasDigits;
});

jQuery.validator.unobtrusive.adapters.addBool("validatepassword");