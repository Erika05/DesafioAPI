var testeNode = getDataHora();
console.log(testeNode);

function getDataHora() {

    var data = new Date();

    var hora = data.getHours();
    hora = (hora < 10 ? "0" : "") + hora;

    var min = data.getMinutes();
    min = (min < 10 ? "0" : "") + min;

    var sec = data.getSeconds();
    sec = (sec < 10 ? "0" : "") + sec;

    var ano = data.getFullYear();

    var mes = data.getMonth() + 1;
    mes = (mes < 10 ? "0" : "") + mes;

    var dia = data.getDate();
    dia = (dia < 10 ? "0" : "") + dia;

    return "_" + dia + "-" + mes + "-" + ano + "_" + hora + "-" + min + "-" + sec;
}