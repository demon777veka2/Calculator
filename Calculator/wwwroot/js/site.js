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

function inputExpression() {
    expession = prompt("Введите выражение:");
    document.getElementById("expression").value = expession;
}