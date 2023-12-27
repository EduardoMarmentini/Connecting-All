$(document).ready(function () {
    // Cria função que vincula a tabela que ira receber os metodos da datatable de paginação e busca nas tabelas das view
    function getDataTable(id_table) {
        $(id_table).DataTable({
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

    // Seta as tabelas que iram receber os metodos de paginação
    getDataTable($("#table-usuarios"))
    getDataTable($("#table-contatos"))


    $('#calendario').fullCalendar({
        aspectRatio: 1.5, // Ajuste a proporção conforme necessário para a largura desejada
        height: 800, // Ajuste a altura conforme necessário
        events: [
            {
                title: "teste",
                start: "2023-12-23",
                end: "2023-12-23",
                color: "ligth blue",
                textColor: "black"
            },
            {
                title: "aniversario da empresa",
                start: "2023-12-27",
                end: "2023-12-23",
                color: "yellow",
                textColor: "black"
            }
        ],
        selectable: true,
        selectHelper: true,
        select: function ()
        {
            $("#CreateEvent").modal("toggle");
        },
        dayRender: function (date, cell)
        {
            let today = $.fullCalendar.moment();
            if (date.get("date") == today.get("date")) {
                cell.css("background", "ligth yellow");
            }
            else {
                cell.css("background", "white");
            }
        },
        header: {
            left   : "month, agendaWeek, agendaDay",
            center : "title",
            right  : "prev, today, next"
        },
        buttonText: {
            today : "Hoje",
            month : "Mês",
            week  : "Semana",
            day   : "Dia"
        },
        ignoreTimezone: false,
        monthNames: ['Janeiro', 'Fevereiro', 'Março', 'Abril', 'Maio', 'Junho', 'Julho', 'Agosto', 'Setembro', 'Outubro', 'Novembro', 'Dezembro'],
        monthNamesShort: ['Jan', 'Fev', 'Mar', 'Abr', 'Mai', 'Jun', 'Jul', 'Ago', 'Set', 'Out', 'Nov', 'Dez'],
        dayNames: ['Domingo', 'Segunda', 'Terça', 'Quarta', 'Quinta', 'Sexta', 'Sabado'],
        dayNamesShort: ['Dom', 'Seg', 'Ter', 'Qua', 'Qui', 'Sex', 'Sab'],
        axisFormat: 'H:mm',
    });

    $('#datepicker-start').datepicker({
        uiLibrary: 'bootstrap5'
    });
    $('#datepicker-end').datepicker({
        uiLibrary: 'bootstrap5'
    });
    
    const inputFile = $("#picture_upload");
    const pictureImage = $(".exibe_foto");
    inputFile.change(function (e) {
        const file = e.target.files[0];

        if (file) {
            // Obtém a extensão do arquivo
            const fileExtension = file.name.split('.').pop().toLowerCase();

            // Verifica se a extensão do arquivo é jpeg
            if (fileExtension === "jpeg") {
                const reader = new FileReader();

                reader.onload = function (e) {
                    var img = $("<img/>", {
                        "src": e.target.result,
                        "class": "exibe_foto"
                    });

                    pictureImage.empty();
                    pictureImage.append(img);
                };

                reader.readAsDataURL(file);
            } else {
                alert("Por favor, selecione uma imagem jpeg.");
            }
        }
    });

    //$("#remove_foto").click(() => {
    //    pictureImage.empty();
    //    pictureImage.text("Foto de perfil:")
    //})

    // Exibe e esconde o a senha no input
    $(".show-password").click(() => {
        if ($('.txtPassword').attr('type') == "password") {
            $(".show-password").text("visibility_off")
            $(".txtPassword").prop("type", "text")
            
        }
        else {
            $(".txtPassword").prop("type", "password")
            $(".show-password").text("visibility")
        }
    })
    // Desabilita o botão "Apagar" por padrão
    $('.btn.btn-danger').prop('disabled', true);

    $('input[type="radio"]').change(function () {
        // Verifica qual botão de opção está selecionado
        if ($('#rdNao').is(':checked')) {
            // Se "Não" estiver selecionado, desabilita o botão "Apagar"
            $('.btn.btn-danger').prop('disabled', true);
        } else if ($('#rdSim').is(':checked')) {
            // Se "Sim" estiver selecionado, habilita o botão "Apagar"
            $('.btn.btn-danger').prop('disabled', false);
        }
    });




    $('#burger').on('change', function () {
        if ($(this).prop('checked')) {
            $(".sidebar").animate({ left: "0px" }, 300);
        } else {
            $(".sidebar").animate({ left: "-250px" }, 300);
        }
    });

});
