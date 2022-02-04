var g_Nivel = 0
var a_Ruta = new Array("../","../", "../../", "../../../", "../../../../","../../../../../")
//===================================================//
//se agrega variable para decidir si es enlace directo para otras aplicaciones
//30/05/2011 -- epoma 
var g_RutaDirecto =0 //0-no,1-si
//===================================================//

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
      var sRuta = "";

      if (g_RutaDirecto == 1) {
          sRuta = "/intranet/";
      } else {
          sRuta = a_Ruta[g_Nivel]; 
      }//end function
        

        var sLink = sRuta + "frmDesTabla.aspx?sTabla=" + sTabla + "&sFiltro=" + sFiltro
	    var sFeatures="DialogWidth:580px;dialogHeight:505px;scroll:no;status:no;"
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

  function fdesUsuarios(objCodi, objDesc) {
      var sTabla = "LISTA_USUARIO"
      var sFiltro = ""

      if (fDesTabla(sTabla, sFiltro, objCodi, objDesc) == true) {
          return true;
      } else {
          return false;
      }
  }

  //===================================================//

  function fdesListaProveedor(objCodi, objDesc) {
      var sTabla = "LISTA_PROVEEDOR"
      var sFiltro = ""

      if (fDesTabla(sTabla, sFiltro, objCodi, objDesc) == true) {
          return true;
      } else {
          return false;
      }
  }
  
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

  //===================================================//
  
  
  function fdesPuestoRRHH(objCodi, objDesc) {
      var sTabla = "PUESTO_RRHH"
      var sFiltro = ""

      if (fDesTabla(sTabla, sFiltro, objCodi, objDesc) == true) {
          return true;
      } else {
          return false;
      }
  }

  //===================================================//

  function fdesAreaRRHH(objCodi, objDesc) {
      var sTabla = "AREA_RRHH"
      var sFiltro = ""

      if (fDesTabla(sTabla, sFiltro, objCodi, objDesc) == true) {
          return true;
      } else {
          return false;
      }
  }


  //=====================================================//

  function fdesAreaRRHH2(objCodi, objDesc) {
      var sTabla = "AREA_RRHH2"
      var sFiltro = ""

      if (fDesTabla(sTabla, sFiltro, objCodi, objDesc) == true) {
          return true;
      } else {
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

  function fdesPFCFamilia(objCodi, objDesc) {
      var sTabla = "PFCFAMILIA"
      var sFiltro = ""

      if (fDesTabla(sTabla, sFiltro, objCodi, objDesc) == true) {
          return true;
      } else {
          return false;
      }
  }

  //===================================================//

  function fdesCliente(objCodi, objDesc) {
      var sTabla = "tmclie"
      var sFiltro = ""

      if (fDesTabla(sTabla, sFiltro, objCodi, objDesc) == true) {
          return true;
      } else {
          return false;
      }
  }

  function fdesTelares(objCodi, objDesc) {
      var sTabla = "TELARES"
      var sFiltro = ""

      if (fDesTabla(sTabla, sFiltro, objCodi, objDesc) == true) {
          return true;
      } else {
          return false;
      }
  }

  function fdesTipoAprobacion(objCodi, objDesc) {
      var sTabla = "TIPO_APROBACIONES"
      var sFiltro = ""

      if (fDesTabla(sTabla, sFiltro, objCodi, objDesc) == true) {
          return true;
      } else {
          return false;
      }
  }
 //================= Definicion de Tablas ================//
