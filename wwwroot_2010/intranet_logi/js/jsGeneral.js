var g_objFila
var g_swFila = false
var g_CssAnt = ""

var g_Nivel1 = 0
var a_Ruta2 = new Array("../", "../", "../../", "../../../", "../../../../", "../../../../../")


//===================================================//

function fSelFila(objRow, Num) {
    var iFila = Num - 1;

    if (g_swFila == true) {
        g_objFila.className = g_CssAnt
    }

    g_CssAnt = objRow.className
    frmData.txtRegSel.value = iFila;
    objRow.className = "FilaSel"
    g_objFila = objRow;
    g_swFila = true;

}

//===================================================//

function fSelFilaGrd(objRow, Num) {
    var iFila = Num - 1;

    if (g_swFila == true) {
        g_objFila.className = "FilaAnt";
    }

    lblSel = document.getElementById("lblRegSel")
    lblSel.innerHTML = iFila;
    objRow.className = "FilaSel"
    g_objFila = objRow;
    g_swFila = true;

}


//===================================================//  

function fSelFocus(objRow, sEvent) {

    if (objRow.className != "FilaSel") {
        if (sEvent == "over") {
            objRow.className = "FilaOver";
        } else {
            objRow.className = "FilaOut";
        }
    }

}



//===================================================//

function fDesOpc(Obj) {

    var strImg = Obj.src;

    if (strImg.indexOf("Off") > 0) {
        return false;
    }

    if (strImg.indexOf("Editar") > 0) {

        ObjReg = document.getElementById("txtRegSel");

        if (ObjReg.value == "") {
            alert("Seleccionar un registro")
            return false;
        }

    }

    var sRuta = a_Ruta[g_Nivel];

    ObjNue = document.getElementById("btnNuevo");
    if (ObjNue != null) {
        ObjNue.src = sRuta + "Images/Nuevo_Off.png";
    }
    ObjEdi = document.getElementById("btnEditar");
    if (ObjEdi != null) {
        ObjEdi.src = sRuta + "Images/Editar_Off.png";
    }

    ObjAnu = document.getElementById("btnAnular");
    if (ObjAnu != null) {
        ObjAnu.src = sRuta + "Images/Delete_Off.png";
    }

    return true;


}


//===================================================//

function fConfirma(strMsg) {
    var sw = confirm(strMsg);
    return sw;
}


//===================================================//

function fActOpc() {

    var sRuta = a_Ruta[g_Nivel];

    ObjNue = document.getElementById("btnNuevo");
    if (ObjNue != null) {
        ObjNue.src = sRuta + "Images/Nuevo.png";
    }

    ObjEdi = document.getElementById("btnEditar");
    if (ObjEdi != null) {
        ObjEdi.src = sRuta + "Images/Editar.png";
    }

    ObjAnu = document.getElementById("btnAnular");
    if (ObjAnu != null) {
        ObjAnu.src = sRuta + "Images/Delete.png";
    }

    return true;

}

