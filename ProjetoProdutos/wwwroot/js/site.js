
//modal
$(document).ready(function () {
    if (erro && erro.length > 0) {
        $('#modalAviso').modal('show');
        $('#mensagem').text(erro);
    }

    if (sucesso && sucesso.length > 0) { 
        $('#modalAviso').modal('show');
        $('#mensagem').text(sucesso);
    }

    if (exclusao && exclusao.length > 0) {
        $('#modalExclusao').modal('show');
        $('#mensagemExclusao').text(exclusao);
    }


    addDataTable('#tableProds');
    addDataTable('#tableUsuarios');
});

$('.btn-fechar-modal').click(function () {
    $('.modal-all').modal('hide');
})

$('.btn-confirma-exclusao-produto').click(function () {
    let idString = idProduto;
    let idProd = Number.parseInt(idString);

    $.ajax({
        url: '/Produto/Excluir',
        type: 'POST',
        data: { id: idProd },
        success: function (res) {
            $('#modalAviso').modal('show');
            $('#mensagem').text(res.msg);

            $('#modalAviso').on('hidden.bs.modal', function () {
                window.location.href = '/Produto/Index';
            });
        },
        error: function (res) {
            $('#modalAviso').modal('show');
            $('#mensagem').text(res.responseText);

            $('#modalAviso').on('hidden.bs.modal', function () {
                window.location.href = '/Produto/Index';
            });
        }
    })

    $('#modalExclusao').modal('hide');
})

$('.btn-confirma-exclusao-usuario').click(function () {

    let id = Number.parseInt(idUsuario);

    $.ajax({
        url: '/Usuario/Excluir',
        type: 'POST',
        data: { id: id },
        success: function (res) {
            $('#modalAviso').modal('show');
            $('#mensagem').text(res.msg);

            $('#modalAviso').on('hidden.bs.modal', function () {
                window.location.href = '/Usuario/Index';
            });
        },
        error: function (res) {
            $('#modalAviso').modal('show');
            $('#mensagem').text(res.responseText);

            $('#modalAviso').on('hidden.bs.modal', function () {
                window.location.href = '/Produto/Index';
            });
        }
    })

    $('#modalExclusao').modal('hide');
})

//datatable

const addDataTable = (id) =>{
    $(id).DataTable({
        responsive: true,
        "ordering": true,
        "paging": true,
        "searching": true,
        "oLanguage": {
            "sEmptyTable": "Nenhum registro encontrado na tabela",
            "sInfo": "Mostrar _START_ até _END_ de _TOTAL_ registros",
            "sInfoEmpty": "Mostrar 0 até 0 de 0 Registros",
            "sInfoFiltered": "(Filtrar de _MAX_ total registros)",
            "sInfoPostFix": "",
            "sInfoThousands": ".",
            "sLengthMenu": "Mostrar _MENU_ registros por pagina",
            "sLoadingRecords": "Carregando...",
            "sProcessing": "Processando...",
            "sZeroRecords": "Nenhum registro encontrado",
            "sSearch": "Pesquisar",
            "oPaginate": {
                "sNext": "Proximo",
                "sPrevious": "Anterior",
                "sFirst": "Primeiro",
                "sLast": "Ultimo"
            },
            "oAria": {
                "sSortAscending": ": Ordenar colunas de forma ascendente",
                "sSortDescending": ": Ordenar colunas de forma descendente"
            }
        }
    });
}

//animacao login

const animacao = lottie.loadAnimation({
    container: document.getElementById('animacao'),
    renderer: 'svg',
    loop: true,
    autoplay: true,
    path: '/Animacao/loginanimacao.json'
});
