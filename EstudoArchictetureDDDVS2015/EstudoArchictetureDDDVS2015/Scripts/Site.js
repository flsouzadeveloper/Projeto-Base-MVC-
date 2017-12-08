
/* -- ========== Funcoes de Mensagens ============================================ -- */

function fAlerta(mensagem, callback) {
    if (typeof bootbox === "undefined") {
        alert("O script de bootbox não está incluso neste contexto.\r\nDefina ViewBag.UseBootbox = true para incluir.");
        return;
    }

    bootbox.alert("teste falerta bootbox!");

    //bootbox.dialog({
    //    message: "<div class='logo2'></div><br/><h3>" + mensagem + "</h3>",
    //    buttons: {
    //        primary: {
    //            label: "OK",
    //            className: "btn-primary",
    //            callback: callback
    //        }
    //    }
    //});
}

/* -- ========== Funcoes auxiliares ============================================== -- */

function Action(actionName, controllerName, parameters) {
    if (!isNaN(parseInt(parameters))) {
        parameters = "/" + parameters;
    } else if (typeof parameters === "object") {
        try {
            var p = "";
            for (var key in parameters) {
                p += "&" + key + "=" + parameters[key];
            }
            parameters = "?" + p.substring(1, p.length);
        } catch (e) {
            parameters = "";
        }
    } else if (parameters != undefined && parameters != null && parameters != "") {
        parameters = "?" + parameters;
    } else {
        parameters = "";
    }

    return rootUrl + controllerName + "/" + actionName + parameters;
}