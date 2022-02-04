
//Funciones de validacion

var matrizNumber = "-.0123456789";
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

function Redondeo2Decimales(numero)
{
	var original=parseFloat(numero);
	var result=Math.round(original*100)/100 ;
	return result;
}

function replaceAll( text, str1, str2 )
{
var command = "text.replace(/"+str1+"/g,\'"+str2+"\')";
return eval(command);
}//end function	


function trim(stringToTrim) {
	return stringToTrim.replace(/^\s+|\s+$/g,"");
}//end function

function ltrim(stringToTrim) {
	return stringToTrim.replace(/^\s+/,"");
}//end function

function rtrim(stringToTrim) {
	return stringToTrim.replace(/\s+$/,"");
}//end function

function FormatoMillares(Valor, NumDec) { 
//Valor-->el numero a formatear, 
//NumDec-->redondear a x decimales

//SepMil-->separador de miles,
//SepDec-->separador de decimales,
//Comprobamos si viene con decimales y separamos el valor entero y el decimal
var SepMil=",", SepDec="."
var ValorEntero; var ValorEntero2 = ""; 
var ValorDecimal = ""; var arrayNum = Valor.toString().split("."); 
ValorEntero = arrayNum[0].toString();

if (arrayNum.length==2) { 
ValorDecimal = arrayNum[1].toString();
ValorDecimal = ValorDecimal.substring(0,NumDec) 
}
else
{
for (var con=1;con<=NumDec;con++) ValorDecimal += "0"; }
//Formateamos la parte entera con separador de millar pasado por par metros

for (con=ValorEntero.length-3;con>-1;con-=3) ValorEntero2 = ValorEntero.substring(con,con+3) + SepMil + ValorEntero2;
 
//Eliminamos el £ltimo caracter

ValorEntero2 = ValorEntero2.substring(0,ValorEntero2.length-1);
//A¤adimos el resto de la cifra si lo hubiera calculando el resto (m¢dulo) de la divisi¢n entre 3
//if (ValorEntero.length%3>0) ValorEntero2 = ValorEntero.substring(0,ValorEntero.length%3) + SepMil + ValorEntero2;
if(ValorEntero.length<3)
{
	ValorEntero2 = ValorEntero;
}
else 
{
	if(ValorEntero.length%3>0) ValorEntero2 = ValorEntero.substring(0,ValorEntero.length%3) + SepMil + ValorEntero2;
}//end if
 
if (NumDec>0) return ValorEntero2 + SepDec + ValorDecimal; else
return ValorEntero2; 
}//end function


//================================================================//

function fValidaNum(obj){

   if ( obj.value != '' ) {
	    if (isVarType(obj.value,'NUM')==false) {
	       obj.value = "0.5"
	       obj.focus();
	       alert("Numero no Valido");
	    }else{
	      obj.value = obj.value
	    }
	}else{
        obj.value = "0.5"
	}
}