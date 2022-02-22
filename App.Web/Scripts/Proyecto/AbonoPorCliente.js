function Lista() {
    $.ajax({
        processing: true, // for show progress bar
        serverSide: true, // for process server side
        filter: true, // this is for disable filter (search box)
        orderMulti: false, // for disable multiple column at once
        url: "/AbonoPorCliente/ListaAbonosAsignados",
        type: "GET",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        success: function (result) {
            var html = '';
            $.each(result, function (key, item) {
                html += '<tr>';
                html += '<td>' + item.Cliente + '</td>';
                //html += '<td>' + item.Dni + '</td>';
                html += '<td>' + item.Abono + '</td>';
                html += '<td>' + item.Precio + '</td>';
                html += '<td>' + item.FechaIngreso + '</td>';
                html += '<td>' + item.FechaVencimiento + '</td>';
                html += '<td class="center">' + item.Pagado + '</td>';
                html += '<td class="">' + item.Estado + '</td>';
                html += '<td class="center">';
                html += '<a style="margin-right: 5px;" class="btn-floating waves-effect waves-light yellow" title="Renovar" onclick = "Renovar(' + item.Id + ')" id="' + item.Id + '"><i class="mdi-action-assignment"></i></a>';
                html += '<a style="margin-right: 5px;" class="btn-floating waves-effect waves-light teal" title="Editar" onclick = "Editar(' + item.Id + ')" id="' + item.Id + '"><i class="mdi-editor-border-color"></i></a>';
                html += '<a class="btn-floating waves-effect waves-light red" title="Eliminar" onclick="Eliminar(' + item.Id + ')" id="' + item.Id + '"><i class="mdi-action-delete"></i></a>';
                html += '</td>';
                html += '</tr>';
            });
            $('#tblAbonoCliente').html(html);

        }
    });

}

function Renovar(id) {
    window.location.href = "/AbonoPorCliente/Renovar?id=" + encodeURIComponent(id);
}

function Editar(id) {
    window.location.href = "/AbonoPorCliente/Edit?id=" + encodeURIComponent(id);

}

function Eliminar(id) {
    window.location.href = "/Cliente/Delete?id=" + encodeURIComponent(id);

}

$.validator.addMethod("valueNotEquals", function (value, element, arg) {
    return arg !== value;
}, "Value must not equal arg.");

$("#formValidate").validate({
    rules: {
        clienteId: {
            valueNotEquals: "default",
        },
        itemAbono: {
            valueNotEquals: "default",
        },
        fechaVencimiento: {
            required: true,
        },
        precio: {
            required: true,
            number: true
        }
    },
    messages: {
        clienteId: {
            valueNotEquals: "Please select an item!",
        },
        itemAbono: {
            valueNotEquals: "Please select an item!",
        },
        fechaVencimiento: {
            required: "Requerido",
        },
        precio: {
            required: "Requerido",
            number: "Solo numeros"
        },
    },
    errorElement: 'div',
    errorPlacement: function (error, element) {
        var placement = $(element).data('error');
        if (placement) {
            $(placement).append(error)
        } else {
            error.insertAfter(element);
        }
    },
    submitHandler: function () {
        var empObj = {
            Id: $("#Id").val(),
            ClienteId: $("#ClienteId").val(),
            AbonoId: $("#AbonoId").val(),
            Precio: $("#Precio").val(),
            FechaVencimiento: $("#fechaVencimiento").val(),
            Pagado: $("#ckPagado").is(':checked'),

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
                    alertify.success(result.Message);

                    setTimeout(function () {
                        window.location = result.Url;
                    }, 2000);
                }
                else {
                    alertify.error(result.Message);
                }
            }
        });
    }
});

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

    $('#fechaVencimiento').formatter({
        'pattern': '{{99}}/{{99}}/{{9999}}',
    });

});
