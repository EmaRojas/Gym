

function Asignar(id) {
    window.location.href = "/AbonoPorCliente/Create?id=" + encodeURIComponent(id);

}

function Ficha(id) {
    window.location.href = "/Ficha/Create?id=" + encodeURIComponent(id);
}

function EditarCliente(id) {
    window.location.href = "/Cliente/Edit?id=" + encodeURIComponent(id);

}

function EliminarCliente(id) {
    window.location.href = "/Cliente/Delete?id=" + encodeURIComponent(id);

}




$(document).ready(function () {
    $('#datepicker').datepicker({
        format: 'dd/mm/yyyy',
        defaultDate: new Date(),
        monthNames: ['Enero', 'Febrero', 'Marzo', 'Abril', 'Mayo', 'Junio',
            'Julio', 'Agosto', 'Septiembre', 'Octubre', 'Noviembre', 'Diciembre'
        ],
    });

    $("#formValidate").validate({
        rules: {
            Nombre: {
                required: true,
            },
            Apellido: {
                required: true,
            },
            FechaNacimiento: {
                required: true,
            },
            Dni: {
                required: true,
                number: true,
                rangelength: [7, 8]
            },
            Telefono: {
                required: true,
                number: true
            }
        }
    });
});





