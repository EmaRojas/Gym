$(document).ready(function () {
    $('#obColumna').prop("disabled", true);
    $('#obCardiaca').prop("disabled", true);
    $('#obLesion').prop("disabled", true);

    $('#ckColumna').click(function () {
        if (!$(this).is(':checked')) {
            $('#obColumna').prop("disabled", true);

        }
        else {
            $('#obColumna').prop("disabled", false);

        }
    });
    $('#ckCardiaca').click(function () {
        if (!$(this).is(':checked')) {
            $('#obCardiaca').prop("disabled", true);

        }
        else {
            $('#obCardiaca').prop("disabled", false);

        }
    });
    $('#cklesion').click(function () {
        if (!$(this).is(':checked')) {
            $('#obLesion').prop("disabled", true);

        }
        else {
            $('#obLesion').prop("disabled", false);

        }
    });
    $('#ckColumna').click(function () {
        if (!$(this).is(':checked')) {
            $('#obColumna').prop("disabled", true);

        }
        else {
            $('#obColumna').prop("disabled", false);

        }
    });

    $('#formFicha').on('submit', function (e) {
        e.preventDefault();
        var empObj = {
            Id: $("#Id").val(),
            ClienteId: $("#ClienteId").val(),
            NombreCompleto: $("#cliente").val(),
            Telefono: $("#telefono").val(),
            Edad: $("#edad").val(),
            Altura: $("#altura").val(),
            Peso: $("#peso").val(),
            GrupoSanguineo: $("#sangre").val(),
            Medico: $("#medico").val(),
            PColumna: $("#ckColumna").is(':checked'),
            DetalleColumna: $("#obColumna").val(),

            ECardiaca: $("#ckCardiaca").is(':checked'),
            DetalleCardiaca: $("#obCardiaca").val(),

            LRecientes: $("#cklesion").is(':checked'),
            DetalleLesion: $("#obLesion").val(),

            ObjPersonal: $("#objPersonal").val(),
            Observacion: $("#observacion").val(),

        };

        $.ajax({
            processing: true, // for show progress bar
            serverSide: true, // for process server side
            filter: true, // this is for disable filter (search box)
            orderMulti: false, // for disable multiple column at once
            url: "/Ficha/Create",
            type: "POST",
            data: JSON.stringify(empObj),
            contentType: "application/json;charset=utf-8",
            dataType: "json",
            success: function (result) {
                if (result.Success) {
                    alertify.success(result.Message);

                    setTimeout(function () {
                        window.location = result.Url;
                    }, 2000);
                }
                else {
                    alertify.error(result.Message);
                }            }
        });

    });
});



