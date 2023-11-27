$(document).ready(function () {

    const inputFile = $("#picture_upload");
    const pictureImage = $(".exibe_foto");

    inputFile.change(function (e) {
        const file = e.target.files[0];

        if (file) {
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
        }
    });

});