// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
function sumText(text) {
    document.getElementById("expression").value += text;
}
function clearText() {
    document.getElementById("expression").value = "";
}
function getRandomNumber(min, max) {
    return Math.round(Math.random() * (max - min) + min)
}

function randonExpression() {
    isConfirm = confirm("Вы хотите сгенерировать случайное выражение?");

    if (isConfirm == true) {
        var expession = getRandomNumber(1, 4);
        var randomCountOperator = getRandomNumber(1, 4);

        for (var i = 0; i < randomCountOperator; i++) {
            var randomOperator = getRandomNumber(1, 4);
            var randomNumber = String(getRandomNumber(1, 100));

            switch (randomOperator) {
                case 1:
                    expession += "+" + randomNumber;
                    break;
                case 2:
                    expession += "-" + randomNumber;
                    break;
                case 3:
                    expession += "*" + randomNumber;
                    break;
                case 4:
                    expession += "/" + randomNumber;
                    break;

            }
        }
        document.getElementById("expression").value = expession;
    }
}