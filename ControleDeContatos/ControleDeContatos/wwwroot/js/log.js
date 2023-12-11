$(document).ready(function () {
    // Adicione o código existente
    const sign_in_btn = document.querySelector("#sign-in-btn");
    const sign_up_btn = document.querySelector("#sign-up-btn");
    const container = document.querySelector(".container");

    sign_up_btn.addEventListener("click", () => {
        container.classList.add("sign-up-mode");
    });

    sign_in_btn.addEventListener("click", () => {
        container.classList.remove("sign-up-mode");
    });

    // Adicione o código para fechar o erro
    $('.error__close').on('click', function () {
        $('.error').hide("hide");
    });
});