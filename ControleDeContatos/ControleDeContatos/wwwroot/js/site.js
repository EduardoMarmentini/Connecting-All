﻿$(document).ready(function () {
    // Cria função que vincula a tabela que ira receber os metodos da datatable de paginação e busca nas tabelas das view
    function getDataTable(id_table) {
        $(id_table).DataTable({
            responsive: true,
            "ordering": true,
            "paging": true,
            "searching": true,
            resize: true,
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


    $("#btn-cria-req").click(() => {
        $("#modalCriarReq").modal("toggle")
    })

    // METODOS AJAX PARA BUSCA DE INFORMAÇÕES NO BACK-END DE FORMA ESPECIFICA

    // Bucar sugetoes de responsavel baseado no input
    $("#txtResponsavel").on("input", function () {
        var textoDigitado = $(this).val();

        $.ajax({
            type: "GET",
            url: "/Requisicao/BuscarSugestao",
            dataType: "json",
            data: {
                txtSugestao: textoDigitado,
                tipo_sugestao : "responsavel"
            },
            sucess: function (result) {
                console.log(result)
            }
        })

    });
    // Buscar requisições do usuario logado
    $("#minhasReq-btn").click(function () {
        $.ajax({
            type: "GET",
            url: "/Requisicao/BuscarRequisicoesUsuarioLogado",
            dataType: "json",
            success: function (requisicoes) {
                // Limpe o conteúdo da div antes de adicionar a nova tabela
                $(".table-container").empty();

                $(".table-container").addClass("mb-3 ps-3 pe-3 pt-3 ")
                if (requisicoes && requisicoes.length > 0) {
                    // Crie a tabela dinâmica
                    let table = $("<table>").addClass("table table-striped table-hover table-requisicoes");
                    table.attr("id", "table-requisicoes")
                    let thead = $("<thead>").appendTo(table);
                    let tbody = $("<tbody>").appendTo(table);

                    // Adicione cabeçalho à tabela
                    let headerRow = $("<tr>").appendTo(thead);
                    headerRow.append("<th class='text-center'>#</th>");
                    headerRow.append("<th class='text-center'>Nº</th>");
                    headerRow.append("<th class='text-center'>Requisição</th>");
                    headerRow.append("<th class='text-center'>Status</th>");
                    headerRow.append("<th class='text-center'>Cliente</th>");
                    headerRow.append("<th class='text-center'>Data de Cadastro</th>");
                    headerRow.append("<th class='text-center'>Data de Entrega</th>");
                    headerRow.append("<th class='text-center'>Prioridade</th>");
                    headerRow.append("<th class='text-center'>Responsável</th>");
                    headerRow.append("<th class='text-center'>Horas Trabalhadas</th>");

                    // Adicione linhas à tabela
                    requisicoes.forEach(function (result) {
                        let newRow = $("<tr>").appendTo(tbody);
                        newRow.append("<td class='text-center'> <button class='btn btn-primary btn-sm'> <span class='material-symbols-outlined'>prompt_suggestion</span> Encaminhar </button></td>");
                        newRow.append("<td class='text-center'>" + result.id_requisicao + "</td>");
                        newRow.append("<td class='text-center'>" + result.titulo_requisicao + "</td>");
                        newRow.append("<td style='background-color:" + result.color_status + "; color:black;' class='text-center'>" + result.descricao + "</td>");
                        newRow.append("<td class='text-center'>" + result.cliente + "</td>");
                        newRow.append("<td class='text-center'>" + result.data_cadastro + "</td>");
                        newRow.append("<td class='text-center'>" + result.data_entrega + "</td>");
                        newRow.append("<td style='background-color:" + result.color_prioridade + "; color:black;' class='text-center'>" + result.prioridade + "</td>");
                        newRow.append("<td class='text-center'>" + result.responsavel + "</td>");
                        newRow.append("<td class='text-center'>" + result.horas_trabalhadas + "</td>");
                    });

                    // Adicione a tabela à div
                    table.appendTo(".table-container");

                    getDataTable("#table-requisicoes");
                } else {
                    // Exiba uma mensagem indicando que não há requisições
                    $(".table-container").html("<p>Nenhuma requisição encontrada.</p>");
                }
            },
     
        });
    });

});
