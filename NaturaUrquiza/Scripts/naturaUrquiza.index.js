$(document).ready(function () {
    $('#dialogo-comprar-body form').attr('data-ajax-success', 'respuestaComprar');
    $('#dialogo-comprar-comprar').click(function () {
        $('#dialogo-comprar form').submit();
        if ($('#dialogo-comprar form').valid()) {
            $("#dialogo-comprar-comprar").attr("disabled", true);
        }
        return false;
    });
});

function respuestaComprar(respuesta) {
    if (respuesta == "ajax-edit-success") {
            $('#dialogo-comprar').modal('hide');
            MostrarAlertaExitosa();
    } else {
        cargarDialogocomprar(respuesta);
    }
}

function cargarDialogocomprar(data) {
    $('#dialogo-comprar-body').html(data);
    $('#dialogo-comprar-body form').attr('data-ajax-success', 'respuestaComprar');
    $('#dialogo-comprar').modal().css({
        'top': '5%',
    });;
    $("#dialogo-comprar-comprar").attr("disabled", false);
}

