const re = /^(([^<>()[\]\\.,;:\s@\"]+(\.[^<>()[\]\\.,;:\s@\"]+)*)|(\".+\"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/;

$("#email-field").keyup(function () {
    debugger;
    var error = $(".field-validation-error").html();
    var error2 = $(".msg-error").html();
    if (error == "Esse Usuário ja existe") {
        $(".field-validation-error").html("");
    }
    HandleValidation()
});

$("#password-field").keyup(function () {
    HandleValidation()
});

$("#name-field").keyup(function () {
    HandleValidation()
});

$("#password2-field").keyup(function () {
    debugger;
    var bla = $(this).val();
    if ($(this).val() != ($("#password-field").val())) {

        $(".msg-error").css("display", "block").html("As senhas não estão iguais.");
    } else {
        $(".msg-error").css("display", "none").html("");
    }

    HandleValidation();
});

function HandleValidation() {
    if (validateEmail($("#email-field").val())
        && $("#password-field").val().length >= 8
        && ($("#password-field").val() == $("#password2-field").val())
        && $("#name-field").val().length > 5) {
        $(".card-create button")
            .css("background-color", "#00FF5C")
            .css("border-color", "#00FF5C")
            .css("cursor", "pointer")
            .css("pointer-events", "auto");
    } else {
        $(".card-create button")
            .css("background-color", "#BDBDBD")
            .css("border-color", "#BDBDBD")
            .css("cursor", "initial")
            .css("pointer-events", "none");
    }
}

function validateEmail(email) {
    return re.test(email);
}