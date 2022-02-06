

function ListaCliente() {
    $.ajax({
        processing: true, // for show progress bar
        serverSide: true, // for process server side
        filter: true, // this is for disable filter (search box)
        orderMulti: false, // for disable multiple column at once
        url: "/Cliente/ListaClientes",
        type: "GET",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        success: function (result) {
            var html = '';
            $.each(result, function (key, item) {
                html += '<tr>';
                html += '<td>' + item.Nombre + '</td>';
                html += '<td>' + item.Apellido + '</td>';
                html += '<td>' + item.FechaNacimiento + '</td>';
                html += '<td>' + item.Dni + '</td>';
                html += '<td>' + item.Telefono + '</td>';
                html += '<td class="center">';
                html += '<a style="margin-right: 10px;" class="btn-floating waves-effect waves-light yellow" title="Ficha" onclick = "ValidaFicha(' + item.Id + ')" id="' + item.Id + '"><i class="mdi-action-assignment"></i></a>';
                html += '<a style="margin-right: 10px;" class="btn-floating waves-effect waves-light teal" title="Editar" onclick = "EditarCliente(' + item.Id + ')" id="' + item.Id + '"><i class="mdi-editor-border-color"></i></a>';
                html += '<a class="btn-floating waves-effect waves-light red" title="Eliminar" onclick = "EliminarCliente(' + item.Id + ')" id="' + item.Id + '"><i class="mdi-action-delete"></i></a>';
                html += '</td>';
                html += '</tr>';
            });
            $('#tblCliente').html(html);

        }
    });

}

function EditarCliente(id) {
    $.ajax({
        url: '/CLiente/Edit',
        success: function (partialView) {
            $(".modal-content").html(partialView);
            $('label').addClass('active')
            //$("#nacimiento").val("")
            //$("#apellido").val("")
            //$("#nacimiento").val("")
            //$("#dni").val("")
            //$("#tel").val("")
            //$("#direccion").val("")

        }
    });
    var empObj = {
        Id: id
    };
    $.ajax({
        processing: true, // for show progress bar
        serverSide: true, // for process server side
        filter: true, // this is for disable filter (search box)
        orderMulti: false, // for disable multiple column at once
        url: "/Cliente/ObtenerCliente",
        type: "POST",
        data: JSON.stringify(empObj),
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        success: function (result) {
            $('#modalID').openModal();
            $('label').addClass('active')
            $("#nombre").addClass("active").val(result[0].Nombre)
            $("#apellido").val(result[0].Apellido)
            $("#nacimiento").val(result[0].FechaNacimiento)
            $("#dni").val(result[0].Dni)
            $("#tel").val(result[0].Telefono)
            $("#direccion").val(result[0].Direccion)
        }
    });

}

function NuevoCliente() {
    $.ajax({
        url: '/CLiente/Create',
        success: function (result) {
            $(".modal-content").html(result);
            $('label').addClass('active')
            $('#modalID').openModal();

        }
    });


}

function ValidaFicha(id) {
    var empObj = {
        IdCliente: id
    };

    $.ajax({
        processing: true, // for show progress bar
        serverSide: true, // for process server side
        filter: true, // this is for disable filter (search box)
        orderMulti: false, // for disable multiple column at once
        url: '/FichaCliente/ValidarFicha',
        type: "POST",
        data: JSON.stringify(empObj),
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        success: function (result) {
            if (!result.exist) {
                $.ajax({
                    url: '/FichaCliente/Create',
                    success: function (partial) {
                        $(".modal-content").html(partial);
                        $('label').addClass('active')
                        $('#modalID').openModal();
                    }
                });
            }
        }
    });

}

function EliminarCliente(id) {
    var empObj = {
        Id: id
    };
    $.ajax({
        processing: true, // for show progress bar
        serverSide: true, // for process server side
        filter: true, // this is for disable filter (search box)
        orderMulti: false, // for disable multiple column at once
        url: "/Cliente/EliminarCliente",
        type: "POST",
        data: JSON.stringify(empObj),
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        success: function (result) {
            if (result.estado) {
                alertify.success(result.message);
                ListaCliente();
            }
            else {
                alertify.error(result.message);
            }

        }
    });
}

$('#nacimiento').formatter({
    'pattern': '{{99}}/{{99}}/{{9999}}',
});

$("#formValidate").validate({
    rules: {
        nombre: {
            required: true,
        },
        apellido: {
            required: true,
        },
        nacimiento: {
            required: true,
        },
        dni: {
            required: true,
            number: true
        },
        tel: {
            required: true,
            number: true
        }
    },
    messages: {
        nombre: {
            required: "Requerido",
        },
        apellido: {
            required: "Requerido",
        },
        nacimiento: {
            required: "Requerido",
        },
        dni: {
            required: "Requerido",
            number: "Solo numeros"
        },
        tel: {
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
            Nombre: $("#nombre").val(),
            Apellido: $("#apellido").val(),
            FechaNacimiento: $("#nacimiento").val(),
            Dni: $("#dni").val(),
            Telefono: $("#tel").val(),
            Direccion: $("#direccion").val(),
        };

        $.ajax({
            processing: true, // for show progress bar
            serverSide: true, // for process server side
            filter: true, // this is for disable filter (search box)
            orderMulti: false, // for disable multiple column at once
            url: "/Cliente/Create",
            type: "POST",
            data: JSON.stringify(empObj),
            contentType: "application/json;charset=utf-8",
            dataType: "json",
            success: function (result) {
                if (result.estado) {
                    ListaCliente();
                    $('#modalID').closeModal();
                    alertify.success(result.message);
                }
                else {
                    alertify.error(result.message);
                }

            }
        });
    }
});


