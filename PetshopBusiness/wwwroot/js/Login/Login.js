const re = /^(([^<>()[\]\\.,;:\s@\"]+(\.[^<>()[\]\\.,;:\s@\"]+)*)|(\".+\"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/;

$("#email-field").keyup(function () {
    if (validateEmail($("#email-field").val()) && $("#password-field").val().length >= 8) {
        $(".card button")
            .css("background-color", "#00FF5C")
            .css("border-color", "#00FF5C")
            .css("cursor", "pointer")
            .css("pointer-events", "auto");
    } else {
        $(".card button")
            .css("background-color", "#BDBDBD")
            .css("border-color", "#BDBDBD")
            .css("cursor", "initial")
            .css("pointer-events", "none");
    }
});

$("#password-field").keyup(function () {
    if (validateEmail($("#email-field").val()) && $("#password-field").val().length >= 8) {
        $(".card button")
            .css("background-color", "#00FF5C")
            .css("border-color", "#00FF5C")
            .css("cursor", "pointer")
            .css("pointer-events", "auto");
    } else {
        $(".card button")
            .css("background-color", "#BDBDBD")
            .css("border-color", "#BDBDBD")
            .css("cursor", "initial")
            .css("pointer-events", "none");
    }
});

function validateEmail(email) {
    return re.test(email);
}


//$(window).on("hashchange", function () {
//    if (location.hash.slice(1) == "register") {
//        $(".card").addClass("extend");
//        $("#login").removeClass("selected");
//        $("#register").addClass("selected");
//    } else {
//        $(".card").removeClass("extend");
//        $("#login").addClass("selected");
//        $("#register").removeClass("selected");
//    }
//});

//$(window).trigger("hashchange");