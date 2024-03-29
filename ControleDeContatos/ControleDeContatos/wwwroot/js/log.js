﻿$(document).ready(function () {
   // Adicione o código existente
   const sign_in_btn = document.querySelector("#sign-in-btn");
   const forget_password_btn = document.querySelector("#forget-password-btn");
   const container = document.querySelector(".container");

    forget_password_btn.addEventListener("click", () => {
        container.classList.add("sign-up-mode");
    });

    sign_in_btn.addEventListener("click", () => {
        container.classList.remove("sign-up-mode");
    });


    $("form").on("submit", function () {
        // Exibe o loader
        $(".pageloader").show();
        // Esconde o conteúdo
        $(".container").hide();
        // Volta para o topo da página
        top.document.body.scrollTop = 0;
        // Você pode adicionar aqui outras lógicas, se necessário
    });

    // Adicione o código para fechar os alerts
    $('#btn-close').on('click', function () {
        $('.error').fadeOut();
        $('.sucess').fadeOut();
    });

    $(".navbar-brand").click(function () {
        console.log("teste")
        // Verifica se a barra lateral está visível
        if ($("#sidebar").is(":visible")) {
            // Se estiver visível, esconde
            $("#sidebar").toggle("slide", { direction: "left" }, 500);
        } else {
            // Se estiver oculta, mostra
            $("#sidebar").toggle("slide", { direction: "left" }, 500);
        }
    });
});