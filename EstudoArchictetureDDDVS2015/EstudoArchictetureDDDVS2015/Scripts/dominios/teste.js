/* -- Scripts para teste.cshtml ------------------------------------------------------------------------------------  */

function teste_Load() {
    $("#btnTeste").click(btnTeste_Click);
    $("#btnSalvarTeste").click(btnSalvarTeste_Click);
}

function btnTeste_Click() {
    $.ajax({
        url: Action("_ExibirTeste", "teste"),
        data: {},
        success: function (data) {
            $("#DivExibirTeste").html(data);
        }
    });
}

function btnSalvarTeste_Click() {
    $.ajax({
        url: Action("_SalvarTeste", "teste"),
        data: $("#DivExibirTeste").find("[name]").serialize(),
        success: function (data) {
            $("#DivExibirTeste").html(data);
        }
    });
}


function editar_Load() {
    $("#btnDeletarTeste").click(btnDeletarTeste_Click);
    $(".btnEditarTeste").click(btnEditarTeste_Click);
}


function btnEditarTeste_Click() {
    
    var idTeste = $(this).data("ideditarteste");
    alert(idTeste);

    $.ajax({
        url: Action("_EditarTeste", "teste"),
        //data: { idTeste: idTeste },
        data: { idTeste: idTeste },
        success: function (data) {
            $("#DivExibirTeste").html(data);
        }
    });
}

function btnDeletarTeste_Click() {
    var idDelTeste = $(this).data("idDelTeste");

    $.ajax({
        url: Action("_DeletarTeste", "teste"),
        data: { idDelTeste: idDelTeste },
        success: function (data) {
            $("#DivExibirTeste").html(data);
        }
    });
}