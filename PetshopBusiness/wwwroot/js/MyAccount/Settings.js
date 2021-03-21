//var cepRegex = /^[0-9]{8}$/;
var cepRegex = /^[0-9]+(-[0-9]+)+$/;

function ValidateCep(cep) {
    return cepRegex.test(cep);
}

function somenteNumeros(num) {
    //var er = /[^0-9]/;
    var er = /^[0-9]+(-[0-9]+)+$/;
    er.lastIndex = 0;
    var campo = num;
    if (er.test(campo.value)) {
        campo.value = "";
    }
}

$("#cep-field").on("keyup", function () {
    $(".error-cep").css("display", "none");
});

function GetAddressInfo() {
    var cep = $("#cep-field").val();
    var endPoint = 'https://viacep.com.br/ws/' + cep + '/json/';

    $.get(endPoint, function (data) {
        if (data.erro) {
            $(".error-cep").css("display", "block");
        } else {
            $("#street-field").val(data.logradouro);
            //$("#number-field").val()
            //$("#complement-field").val()
            $("#district-field").val(data.bairro);
            $("#city-field").val(data.localidade);
            //$("#state-field")
            $('#state-field').val(data.uf);

            $("#number-field").focus();
        }
    });
}

function HandleAddressCards() {
    $(".edit-address-div").slideDown(200);
    $(".address-description-card").slideUp(200);
}

function RenderEditAddress() {
    var Address = JSON.parse($("#Teste").val());

    $.post("/MyAccount/EditAddress", Address , function (data) {
        $(".edit-address-div").html(data);
        $(".edit-address-div").show(200);
        $(".address-description-card").hide(200);
    });
    debugger;
}