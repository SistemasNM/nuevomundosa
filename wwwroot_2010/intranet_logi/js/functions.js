
//Funciones de validacion

var matrizNumber = ".0123456789";
var matrizInteger = "0123456789";
var matrizAlfa = "abcdefghijklmnopqrstuwxyzABCDEFGHIJKLMNOPQRSTUWXYZ_";
var matrizAlfaNumerico = "abcdefghijklmnopqrstuwxyzABCDEFGHIJKLMNOPQRSTUWXYZ_0123456789./";
function isVarType(strValue, Tipo)
{
	var matriz = "";
	switch (Tipo)
	{
		case "INT":
			matriz = matrizInteger;
		break;
		case "ALF":
			matriz = matrizAlfa;
		break;
		case "ALN":
			matriz = matrizAlfaNumerico;
		break;
		case "NUM":
			matriz = matrizNumber;
		break;
	}
	for(x=0;x<strValue.length;x++)
	{
		if(ExistIntoMatriz(strValue.charAt(x), matriz)==false)
		{
			return false;
		}
	}
	return true;
}

function ExistIntoMatriz(Valor, Matriz)
{
	for(y=0;y<Matriz.length;y++)
	{
		if(Matriz.charAt(y)==Valor)
		{
			return true;
		}
	}
	return false;
}



//==========================================================================//


function fValidaNum(obj){

    if ( obj.value != '' ) {

	    if (isVarType(obj.value,'NUM')==false) {
	       obj.value = 0
	       obj.focus();
	       alert("Numero no Valido");
	    }
	}else{
        obj.value = 0
	}

}

//==========================================================================//