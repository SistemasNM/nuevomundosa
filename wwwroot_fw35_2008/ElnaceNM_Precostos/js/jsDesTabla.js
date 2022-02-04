var g_Nivel = 0
var a_Ruta = new Array("../","../", "../../", "../../../", "../../../../","../../../../../")


  //===================================================//
  
  function fDesLista(sTabla, sFiltro, ObjCod){

        var sLink ="../frmDesLista.aspx?sTabla=" + sTabla  + "&sFiltro=" + sFiltro
	    var sFeatures="DialogWidth:428px;dialogHeight:440px;scroll:no;status:no;"
        var aTabla =window.showModalDialog(sLink,"Parametrización",sFeatures);
        
        if (aTabla[0]!="") {
           ObjCod.value = aTabla[0];
	       return true;
	    }else{
	       return false;
        }
  }  
    
  //===================================================//
  
  function fDesTabla(sTabla, sFiltro, ObjCod, ObjDes){

      var sRuta = a_Ruta[g_Nivel];

        var sLink = sRuta + "frmDesTabla.aspx?sTabla=" + sTabla + "&sFiltro=" + sFiltro
	    var sFeatures="DialogWidth:580px;dialogHeight:480px;scroll:no;status:no;"
	    var aTabla = window.showModalDialog(sLink, "Tablas", sFeatures);
        
        if(aTabla!=null)
        {
            if (aTabla[0]!="") {
               ObjCod.value = aTabla[0];
	           ObjDes.value = aTabla[1];
	           return true;
            }//end if
        }//end if
        return false;
  }//end function

 
 //===================================================//
 
 function fdesCCosto(objCodi,objDesc){
      var sTabla  = "CENTROCOSTO"
      var sFiltro = ""
      
      if (fDesTabla(sTabla, sFiltro, objCodi , objDesc) == true){
         return true;
      }else{
         return false;
      }
  }
  

//=====================================================//

 function fdesAFijo(objCodi,objDesc){
      var sTabla  = "ACTIVOFIJO"
      var sFiltro = ""
      
      if (fDesTabla(sTabla, sFiltro, objCodi , objDesc) == true){
         return true;
      }else{
         return false;
      }
  }



//=====================================================//

 function fdesMaqProd(objCodi,objDesc){
      var sTabla  = "MAQUINAPROD"
      var sFiltro = ""
      
      if (fDesTabla(sTabla, sFiltro, objCodi , objDesc) == true){
         return true;
      }else{
         return false;
      }
  }



//=====================================================//

 function fdesUniConsumo(objCodi,objDesc){
      var sTabla  = "UNIDADCONSUMO"
      var sFiltro = ""
      
      if (fDesTabla(sTabla, sFiltro, objCodi , objDesc) == true){
         return true;
      }else{
         return false;
      }
  }



//=====================================================//

 function fdesUnidTinto(objCodi,objDesc,objMaqu){
      var sTabla  = "UNIDADTINTO"
      var sFiltro = ""
      var sCodigo = ""
      var sCodUni = ""
      var sCodMaq = ""
      var iIndPos = 0
      
      if (fDesTabla(sTabla, sFiltro, objCodi , objDesc) == true){
         sCodigo = objCodi.value;
         iIndPos = sCodigo.indexOf("(");
         sCodUni = sCodigo.substring(0,iIndPos-1);
         sCodMaq = sCodigo.substring(iIndPos+1, sCodigo.length -1 );
         
         objCodi.value =  sCodUni;
         objMaqu.value =  sCodMaq;
      
         return true;
      }else{
         return false;
      }
  }

  //=====================================================//

  function fdesHilos(objCodi, objDesc) {
      var sTabla = "NM_HILOS"
      var sFiltro = ""

      if (fDesTabla(sTabla, sFiltro, objCodi, objDesc) == true) {
          return true;
      } else {
          return false;
      }
  }


  //=====================================================//

  function fdesInsumo(objCodi, objDesc) {
      var sTabla = "NM_INSUMO"
      var sFiltro = ""

      if (fDesTabla(sTabla, sFiltro, objCodi, objDesc) == true) {
          return true;
      } else {
          return false;
      }
  }
  //=====================================================//
  
  
function fdesArticulos(objCodi,objDesc)
    {
      var sTabla  = "vw_articulo"
      var sFiltro = ""
      
      if (fDesTabla(sTabla, sFiltro, objCodi , objDesc) == true){
         return true;
      }else{
         return false;
      }
      
  }
  
   //=====================================================//
   
   
 function fdesColor(objCodi,objDesc)
    {
      var sTabla  = "COLOR"
      var sFiltro = ""
      if (fDesTabla(sTabla, sFiltro, objCodi , objDesc) == true)
           {
            return true;
            }
      else
            {
            return false;
            }
  }
 //================= Definicion de Tablas ================//
 function fdesAcabado(objCodi,objDesc)
    {
      var sTabla  = "ACABADO"
      var sFiltro = ""
      if (fDesTabla(sTabla, sFiltro, objCodi , objDesc) == true)
           {
            return true;
            }
      else
            {
            return false;
            }
  }
  function fdesColorante(objCodi,objDesc)
    {
      var sTabla  = "COLORANTE"
      var sFiltro = ""
      if (fDesTabla(sTabla, sFiltro, objCodi , objDesc) == true)
           {
            return true;
            }
      else
            {
            return false;
            }
  }