$(document).ready(function () {
    $('#AbonoId').on('change',
        function () {
            if ($(this).val() == 0) {
                $('#Precio').val("0");
            }
            console.log($(this).val());
            $.ajax({
                url: "/AbonoPorCliente/InputPrecio",
                method: "GET",
                data: { id: $(this).val() },
            }).done((precio) => {
                $('label').addClass('active')

                $('#Precio').val(precio);
            });
        });

    if ($('#ckPagado').val() == "True") {
        $('#ckPagado').prop("checked", true);
    }
    else {
        $('#ckPagado').prop("checked", false);
    }

    $('#formValidate').on('submit', function (e) {
        e.preventDefault();
        var empObj = {
            Id: $("#Id").val(),
            ClienteId: $("#ClienteId").val(),
            AbonoId: $("#AbonoId").val(),
            Precio: $("#Precio").val(),
            FechaVencimiento: $("#FechaVencimiento").val(),
            Pagado: $("#ckPagado").is(':checked'),
            PendientePago: $("#PendientePago").val(),
        };

        var url = "";

        if (empObj.Id != 0) {
            url = "/AbonoPorCliente/Edit"
        }
        else {
            "/AbonoPorCliente/Create"
        }

        $.ajax({
            processing: true, // for show progress bar
            serverSide: true, // for process server side
            filter: true, // this is for disable filter (search box)
            orderMulti: false, // for disable multiple column at once
            url: url,
            type: "POST",
            data: JSON.stringify(empObj),
            contentType: "application/json;charset=utf-8",
            dataType: "json",
            success: function (result) {
                if (result.Success) {
                    NotificationSuccess(result.Message);

                    setTimeout(function () {
                        window.location = result.Url;
                    }, 2000);
                }
                else {
                    NotificationError(result.Message);
                }
            }
        });

    });


    $("#formValidate").validate({
        rules: {
            Precio: {
                required: true,
                number: true
            }
        },
    });

});
