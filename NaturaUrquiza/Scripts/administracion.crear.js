$(document).ready(function () {
    if ($("#FotoPath").val() == null) {
        $("#dialogo-editar-guardar").attr("disabled", true);
    } else {
        $("#dialogo-editar-guardar").attr("disabled", false);
    }
    $('#fileupload').fileupload({
        dataType: "json",
        url: "/api/upload",
        limitConcurrentUploads: 1,
        sequentialUploads: true,
        maxNumberOfFiles: 1,
        progressInterval: 100,
        maxChunkSize: 10000,
        add: function (e, data) {
            $('#filelistholder').removeClass('hide');
            $('#fileimput').addClass('hide');
            data.context = $('<div />').text(data.files[0].name).appendTo('#filelistholder');
            $('</div><div class="progress"><div class="bar" style="width:0%"></div></div>').appendTo(data.context);
            data.submit();
        },
        done: function (e, data) {
            data.context.text(data.files[0].name + '... Completed');
            $("#dialogo-editar-guardar").attr("disabled", false);
            $("#FotoPath").val(data.files[0].name.replace(/\ /g, '-'));
        },
        progress: function (e, data) {
            var progress = parseInt(data.loaded / data.total * 100, 10);
            data.context.find('.bar').css('width', progress + '%');
        }
    });
});