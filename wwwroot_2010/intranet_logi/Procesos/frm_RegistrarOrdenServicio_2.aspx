<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="frm_RegistrarOrdenServicio_2.aspx.vb" Inherits="intranet_logi.frm_RegistrarOrdenServicio_2" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
 <head>
 <title>Requisicion de Servicios</title>
<base target="_self"/>
  <!--<meta content="Microsoft Visual Studio.NET 7.0" name="GENERATOR"/>-->
  <!--<meta content="Visual Basic 7.0" name="CODE_LANGUAGE"/>-->
  <meta content="JavaScript" name="vs_defaultClientScript"/>
  <meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema"/>

  <%--<script language="javascript" src="../Scripts/jsCalendario_N3.js" type="text/javascript"></script>
  <link href="../Styles/NM0001.css" type="text/css" rel="stylesheet"/>
  --%>

  <%--<link href="../css/NM0001.css" type="text/css" rel="stylesheet"/>--%>
     <link href="../css/NM0001.css" rel="stylesheet" type="text/css" />
  <!--<script language="javascript" type="text/javascript" src="../../intranet/JS/jsCalendario_N4.js"></script>-->
     <!--<script src="../js/jsCalendario_N4.js" type="text/javascript"></script>-->
     <script src="../js/jsCalendario_N3.js" type="text/javascript"></script>
  <%--<script src="../js/jsCalendario_N3.js" type="text/javascript"></script>--%>
 <%-- <script language="javascript" type="text/javascript" src="../../intranet/JS/functions.js"></script>--%>
     <script src="../js/functions.js" type="text/javascript"></script>
  <script language="JavaScript" type="text/javascript">

      //txtObservaciones_onBlur
      function txtObservaciones_onBlur() {      
          var txtObservaciones = document.getElementById("txtObservacion");
          txtObservaciones.value = txtObservaciones.value.toUpperCase();
          var lstrObservaciones = txtObservaciones.value;
          if (lstrObservaciones.length > 200) {
              alert('Solo se permiten 200 caracteres para la observación.');
              txtObservaciones.value = txtObservaciones.value.substring(0, 199);
          }
      }

      //txtDesServicio_onBlur
      function txtDesServicio_onBlur(ControlID, Constante) {
          var txtDesServicio = document.getElementById("txtDesServicio" + Constante);
          txtDesServicio.value = txtDesServicio.value.toUpperCase();
          var lstrObservaciones = txtDesServicio.value;
          if (lstrObservaciones.length > 100) {
              alert('Solo se permiten 100 caracteres para la descripción del servicio.');
              txtDesServicio.value = txtDesServicio.value.substring(0, 100);
          }
      }

      //Valida CentroCosto
      function txtCodigoCentroCosto_onBlur(ControlID, Constante) {
          var Codigo = document.getElementById("txtCodigoCentroCosto_S_" + Constante);
          var Nombre = document.getElementById("HDN1");

          var oXml = new ActiveXObject("Microsoft.XMLDOM");
          oXml.async = false;
          oXml.validateOnParse = false;
          oXml.load("../NMGetXml.aspx?Opcion=CentroCosto&Codigo=" + Codigo.value);
          root = oXml.documentElement;
          if (root.childNodes.length > 0) {
              var opc = root.getElementsByTagName("Datos")(0);
              Codigo.value = opc.getAttribute("CentroCostoCodigo");
              document.getElementById("hdnCCDestino").value = opc.getAttribute("CentroCostoCodigo");
              document.getElementById("HDN1").value = opc.getAttribute("CentroCostoNombre");
          }
      }

      //      //BuscarCentroCosto
      //      function BuscarCentroCosto() {
      //          var txtCodigo = document.all("txtAreaSolicitanteCodigo");
      //          var lblNombre = document.all("txtAreaSolicitanteNombre");
      //          var oXml = new ActiveXObject("Microsoft.XMLDOM");
      //          oXml.async = false;
      //          oXml.validateOnParse = false;
      //          oXml.load("../NMGetXml.aspx?Opcion=CentroCosto&Codigo=" + txtCodigo.value);
      //          root = oXml.documentElement;
      //          if (root.childNodes.length > 0) {
      //              var opc = root.getElementsByTagName("Datos")(0);
      //              txtCodigo.value = opc.getAttribute("CentroCostoCodigo");
      //              lblNombre.value = opc.getAttribute("CentroCostoNombre");
      //              document.getElementById("hdnAreaSolicitante").value = opc.getAttribute("CentroCostoNombre");
      //          }
      //      }

      //BuscarJefaturaSolicitante
      function BuscarJefaturaSolicitante() {
          var txtCodigo = document.getElementById("txtAreaSolicitanteCodigo");
          var lblNombre = document.getElementById("txtAreaSolicitanteNombre");
          var oXml = new ActiveXObject("Microsoft.XMLDOM");
          oXml.async = false;
          oXml.validateOnParse = false;
          oXml.load("../NMGetXml.aspx?Opcion=Jefatura&Codigo=" + txtCodigo.value);
          root = oXml.documentElement;
          if (root.childNodes.length > 0) {
              var opc = root.getElementsByTagName("Datos")(0);
              txtCodigo.value = opc.getAttribute("JefaturaCodigo");
              lblNombre.value = opc.getAttribute("JefaturaNombre");
              document.getElementById("hdnAreaSolicitante").value = opc.getAttribute("JefaturaNombre");
          }
      }

      //      //SolicitarAprobacion
      //      function SolicitarAprobacion() {
      //         document.getElementById("hdnAprobacion").value = '105';
      //         return true;
      //         
      //      }


      //SolicitarAprobacion
      function SolicitarAprobacion() {
          //alert("sssss");
          var bSoliApro_OK = false

          //alert(document.getElementById("lblMensaje").innerHTML);

          if (document.getElementById("lblMensaje").innerHTML != "") {
              var bConfirma = confirm("¿Esta Requisición fue Rechazada. Está seguro que desea volver a Solicitar su Aprobación? ");
              if (bConfirma == true) {
                  bSoliApro_OK = true;
              } else {
                  return false;
              }
          } else {
              bSoliApro_OK = true;
          }

          if (bSoliApro_OK == true) {
              document.getElementById("hdnAprobacion").value = '';
              var retorno = window.showModalDialog("../../intranet/buscadores/frmTiposAprobacion.aspx?strCodigo=&strNombre=", "", "dialogHeight:450px;dialogWidth:450px;center:yes;help:no;");
              //var retorno = window.showModalDialog("http://servnmprb/intranet/buscadores/frmTiposAprobacion.aspx?strCodigo=&strNombre=", "", "dialogHeight:450px;dialogWidth:450px;center:yes;help:no;");

              if (retorno != "" && retorno != ":") {
                  var datos = retorno.split(":");
                  document.getElementById("hdnAprobacion").value = datos[0];
                  var bConfirmaApro = confirm("Ha Seleccionado " + datos[1] + ". ¿Está seguro que desea continuar con la solicitud de Aprobación?");
                  if (bConfirmaApro == true) {
                      return true;
                  } else {
                      return false;
                  }
              }
          } else {
              return false;
          }
      }


      //      //ListarCentroCosto
      //      function ListarCentroCosto(ControlID, Constante) {
      //         
      //          var strCtaGasto = ""
      //          var retorno = window.showModalDialog("../../intranet/Buscadores/LOG_0001.aspx?strCtaGasto=" + strCtaGasto, "", "dialogHeight:450px;dialogWidth:450px;center:yes;help:no;");
      //          if (retorno != "" && retorno != ":") {
      //              var datos = retorno.split(":");
      //              var Codigo = datos[0];
      //              var Nombre = datos[1];
      //              switch (Constante) {
      //                  case "F":
      //                      if (document.all('txtTipoReq').value == 'ART') {
      //                          var txtCodigoCentroCostoF = document.all[ControlID + "_txtCodigoCentroCostoF"];
      //                      }
      //                      else {
      //                          var txtCodigoCentroCostoF = document.all[ControlID + "_txtCodigoCentroCosto_S_F"];
      //                      }
      //                      txtCodigoCentroCostoF.value = Codigo;
      //                      break;
      //                  case "E":
      //                      if (document.all('txtTipoReq').value == 'ART') {
      //                          var txtCodigoCentroCostoE = document.all[ControlID + "_txtCodigoCentroCostoE"];
      //                      }
      //                      else {
      //                          var txtCodigoCentroCostoE = document.all[ControlID + "_txtCodigoCentroCosto_S_E"];
      //                      } 
      //                      txtCodigoCentroCostoE.value = Codigo;
      //                      break;
      //                  case "":
      //                      document.all('txtCentroCostoCodigo').value = Codigo;
      //                      document.all('lblCentroCostoNombre').value = Nombre;
      //                      break;
      //                  case "A":
      //                      //document.all('txtAreaSolicitanteCodigo').value = Codigo;
      //                     // document.all('txtAreaSolicitanteNombre').value = Nombre;
      //                      break;
      //              }
      //              document.getElementById("HDN1").value = Nombre;
      //              document.getElementById("hdnCCDestino").value = Codigo;
      //          }
      //      }

      //ListarJefaturaSolicitante
      function ListarJefaturaSolicitante() {

          var retorno = window.showModalDialog("../Buscadores/frmBuscarJefatura.aspx", "", "dialogHeight:450px;dialogWidth:450px;center:yes;help:no;");
          if (retorno != "" && retorno != ":") {
              var datos = retorno.split(":");
              var Codigo = datos[0];
              var Nombre = datos[1];

              document.getElementById('txtAreaSolicitanteCodigo').value = Codigo;
              document.getElementById('txtAreaSolicitanteNombre').value = Nombre;

              document.getElementById("HDN1").value = Nombre;
              //document.getElementById("hdnCCDestino").value = Codigo;
          }
      }

      // BuscarOrdenServicio(ControlID,Constante
      function BuscarOrdenServicio(ControlID, Constante) {
          var strCentroCostos = document.getElementById("hdnCCDestino").value;
          if (strCentroCostos != "") {
              var retorno = window.showModalDialog("../../intranet/Buscadores/frmBusquedaActivo.aspx?strCentroCostos=" + strCentroCostos, "", "dialogHeight:450px;dialogWidth:450px;center:yes;help:no;");
              if (retorno != "" && retorno != ":") {
                  var datos = retorno.split(":");
                  var Codigo = datos[0];
                  var Nombre = datos[1];
                  switch (Constante) {
                      case "F":
                          if (document.getElementById('txtTipoReq').value == 'ART') {
                              var txtCodOrdenServicioF = document.getElementById("txtCodOrdenServicioF");
                          }
                          else {
                              var txtCodOrdenServicioF = document.getElementById("txtCodOrdenServicio_S_F");
                          }
                          txtCodOrdenServicioF.value = Codigo;
                          break;
                      case "E":
                          if (document.getElementById('txtTipoReq').value == 'ART') {
                              var txtCodOrdenServicioE = document.getElementById("txtCodOrdenServicioE");
                          }
                          else {
                              var txtCodOrdenServicioE = document.getElementById("txtCodOrdenServicio_S_E");
                          }
                          txtCodOrdenServicioE.value = Codigo;
                          break;
                      case "":
                          document.getElementById('txtCodigoServicio').value = Codigo;
                          document.getElementById('txtNombreServicio').value = Nombre;
                          break;
                  }
                  document.getElementById("HDN2").value = Nombre;
              }
          } else {
              alert("Debe seleccionar primero el Centro de Costos");
          }
      }

      function fnc_BuscarDatos(ControlID, Constante, strTipoBusqueda, strParam) {//begin fnc_BuscarDatos()
          var strCentroCostos = document.getElementById("hdnCCDestino").value
          if ((strTipoBusqueda != "CuentaGastos" && strTipoBusqueda != "OrdenTrabajo") || (strTipoBusqueda == "CuentaGastos" && strCentroCostos != "") || (strTipoBusqueda == 'OrdenTrabajo' && strCentroCostos != "")) {
          //if (strTipoBusqueda != "CuentaGastos" || (strTipoBusqueda == "CuentaGastos" && strCentroCostos != "")) {

              var retorno = window.showModalDialog("../Buscadores/frmBusqueda.aspx?TipoBusqueda=" + strTipoBusqueda + "&Param1=" + strCentroCostos + "&Param2=" + strParam, "", "dialogHeight:420px;dialogWidth:450px;center:yes;help:no;");              
              if (retorno != "" && retorno != ":") {
                  var datos = retorno.split(":");
                  var codigo = datos[0];
                  var nombre = datos[1];

                  switch (strTipoBusqueda) {
                      case "CuentaGastos":
                          document.getElementById("txtCodigoCuentaGastos_S_" + Constante).value = codigo;
                          document.getElementById("HDNCtaGasto").value = codigo;
                          document.getElementById("HDNDesCtaGasto").value = nombre;
                          //CONNTROL DE GESTION - DG  -INI
                          var resul = frm_RegistrarOrdenServicio_2.ValidarCuentaGastoOT(codigo)
                          if (resul.value == "") {
                              document.getElementById("txtCodigoOrdenTrabajo_S_" + Constante).disabled = true;
                              document.getElementById("BtnOrdenTrabajoSF").disabled = true;
                              document.getElementById("txtCodigoOrdenTrabajo_S_" + Constante).value = "";
                          } else {
                              document.getElementById("txtCodigoOrdenTrabajo_S_" + Constante).disabled = false;
                              document.getElementById("BtnOrdenTrabajoSF").disabled = false;
                          }
                          //CONNTROL DE GESTION - DG  -INI
                          break;
                      case "CentrodeCostos":
                          if (document.getElementById('txtTipoReq').value == 'ART') {
                              document.getElementById("txtCodigoCentroCosto" + Constante).value = codigo;
                          }
                          else {
                              document.getElementById("txtCodigoCentroCosto_S_" + Constante).value = codigo;
                          }
//                          if (Constante != 'F' && Constante != 'E') {
//                              document.getElementById('txtCentroCostoCodigo').value = codigo;
//                              document.getElementById('lblCentroCostoNombre').value = nombre;
//                          }
                          document.getElementById("HDN1").value = nombre;
                          document.getElementById("hdnCCDestino").value = codigo;

                          document.getElementById("txtCodigoCuentaGastos_S_" + Constante).value = "";
                          document.getElementById("HDNCtaGasto").value = "";
                          document.getElementById("HDNDesCtaGasto").value = "";

//                          document.getElementById("txtCodigoOrdenTrabajo_S_" + Constante).value = "";
//                          document.getElementById("hdnOrdenTrabajo").value = "";
                          break;
                     case "OrdenTrabajo":
                          if (document.getElementById('txtTipoReq').value == 'ART') {
                              document.getElementById("txtCodigoOrdenTrabajo" + Constante).value = codigo;
                          }
                          else {
                              document.getElementById("txtCodigoOrdenTrabajo_S_" + Constante).value = codigo;                              
                          }
                          document.getElementById("hdnOrdenTrabajo").value = codigo;
                          break;
                      case "Responsable":
                          document.getElementById('txtCodResponsableOT').value = codigo;
                          document.getElementById('lblDesResponsableOT').value = nombre;
                          break;
                  }

              } //end if

          } else {
              alert("Debe seleccionar primero el Centro de Costos");
          }

      } //end fnc_BuscarDatos() 

      //      //BuscarCtaGasto	
      //      function BuscarCtaGasto(ControlID, Constante) {
      //          //var strCentroCosto = document.all('txtCtaGasto').value;
      //          var strCentroCostos = document.getElementById("hdnCCDestino").value
      //          if (strCentroCostos != "") {
      //              var retorno = window.showModalDialog("../../intranet/Buscadores/frmCtaGasto.aspx?strCentroCosto=" + strCentroCostos, "", "dialogHeight:450px;dialogWidth:450px;center:yes;help:no;");
      //              if (retorno != "" && retorno != ":") {
      //                  var datos = retorno.split(":");
      //                  var Codigo = datos[0];
      //                  var Nombre = datos[1];
      //                  switch (Constante) {
      //                      case "F":
      //                          var txtCtaGastoSF = document.all[ControlID + "_txtCodigoCuentaGastos_S_F"];
      //                          txtCtaGastoSF.value = Codigo;
      //                          break;
      //                      case "E":
      //                          var txtCtaGastoSE = document.all[ControlID + "txtCodigoCuentaGastos_S_E"];
      //                          txtCtaGastoSE.value = Codigo;
      //                          break;
      ////                      case "":
      ////                          document.all('txtCtaGasto').value = Codigo;
      ////                          document.all('txtDescCtaGasto').value = Nombre;
      ////                          break;
      //                  }
      //                  document.getElementById("HDNCtaGasto").value = Codigo;
      //                  document.getElementById("HDNDesCtaGasto").value = Nombre;

      //              }
      //          } else {
      //              alert("Debe seleccionar primero el Centro de Costos");
      //          }
      //      }

      //BuscarServicio
      function BuscarServicio(ControlID) {
          var retorno = window.showModalDialog("../../intranet/Buscadores/frmServicios.aspx", "", "dialogHeight:450px;dialogWidth:450px;center:yes;help:no;");
          if (retorno != "" && retorno != "::") {
              var datos = retorno.split(":");
              var Grupo = datos[0];
              var Codigo = datos[1];
              var Nombre = datos[2];
              var txtGrupoServF = document.getElementById("txtGrupoServF");
              var txtTipoServF = document.getElementById("txtTipoServF");
              txtGrupoServF.value = Grupo;
              txtTipoServF.value = Codigo;
              document.getElementById("HdnDescServicio").value = Nombre;
          }
      }

      //buscar orden de trabajo
      function fnc_BuscarOrdenTrabajo(Constante) {
          var txtCodResponsableOT = document.getElementById("txtCodResponsableOT");
          if (txtCodResponsableOT.value == "") {
              alert("Primero debe selecciona un responsable de O/T");
              txtCodResponsableOT.focus();
              return false;
          }

          var retorno = window.showModalDialog("../Buscadores/frmBusquedaOrdenTrabajo.aspx?Param1=" + txtCodResponsableOT.value, "", "dialogheight:450px;dialogwidth:450px;center:yes;help:no;");
          if (retorno != "" && retorno != ":") {
              var datos = retorno.split(":");
              var Codigo = datos[0];
              var Nombre = datos[1];
              document.getElementById("txtCodigoOrdenTrabajo_S_" + Constante).value = Codigo;
              //CONNTROL DE GESTION - DG  -INI
              var ldtbResultado = frm_RegistrarOrdenServicio_2.CargarMontoOt(Codigo);
              document.getElementById("txtMonto_" + Constante).value = ldtbResultado.value;              
              //CONNTROL DE GESTION - DG  -FIN
          }
      }

      //popUp
      function popUp(strUrl) {
          var intWidth = screen.width;
          var intHeight = screen.height;
          window.open(strUrl, "", "top=0; left=0; width=" + intWidth + "; height=" + intHeight + "; resizable=1;");
      }

      //txtCantidad_onBlur
      function txtCantidad_onBlur(ControlID, Constante) {
          var ltxtCantidad = document.getElementById('txtCantidad' + Constante);
          if (ltxtCantidad.value == '' || isVarType(ltxtCantidad.value, 'NUM') == false) {
              alert('La cantidad ingresada no es valida.');
              ltxtCantidad.focus();
              return false;
          }
          ltxtCantidad.value = parseFloat(ltxtCantidad.value).toFixed(2);
          return true;
      }

      //txtCantidadS_onBlur
      function txtCantidadS_onBlur(ControlID, Constante) {
          var ltxtCantidadS = document.getElementById('txtCantidadS' + Constante);
          if (ltxtCantidadS.value == '') {
              return false;
          }
          if (ltxtCantidadS.value == '' || isVarType(ltxtCantidadS.value, 'NUM') == false) {
              alert('La cantidad ingresada no es valida.');
              ltxtCantidadS.focus();
              return false;
          }
          ltxtCantidadS.value = parseFloat(ltxtCantidadS.value);
          return true;
      }

      //fnc_ValidarRequisicion
      function fnc_ValidarRequisicion() {
          var lstrCuentaGasto = '';
          var lstrCodigosErroneos = '';
          if (document.getElementById('txtFecha').value == '') {
              alert('Debe ingresar la fecha de creación.');
              document.getElementById('txtFecha').focus();
              return false;
          }
          if (document.getElementById('txtAreaSolicitanteCodigo').value == '' || document.getElementById('txtAreaSolicitanteNombre').value == '') {
              alert('El código de area solicitante no es valido.');
              document.getElementById('txtAreaSolicitanteCodigo').focus();
              return false;
          }

//          //LUIS_AJ (20190202)
//          if (document.getElementById('txtCodResponsableOT').value == '') {
//              alert("Debe ingresar un responsable de OT.");
//              document.getElementById('txtCodResponsableOT').focus();
//              return false;
//          }

          //LUIS_AJ (20180112)
          if (document.getElementById('ddlMotivoRequisicion').value == '0') {
              alert("Seleccione el motivo de la requisición.");
              document.getElementById('ddlMotivoRequisicion').focus();
              return false;
          }


          if (document.getElementById("rdPresupuestado_SI").checked == false && document.getElementById("rdPresupuestado_NO").checked == false) {
              alert("Debe responder la pregunta ¿Está presupuestado?");
              return false;
          }
          //LUIS_AJ (20180112)

          //CAMBIO DG
          if (document.getElementById("rdTipoInterna").checked == false && document.getElementById("rdTipoExterna").checked == false) {
              alert("Debe responder la pregunta ¿Recibe charla de seguridad?");
              return false;
          }
          //CAMBIO DG

          //          if (document.all('txtTipoReq').value == 'SERVICIO') {
          //              lstrCuentaGasto = document.all('txtCtaGasto').value;
          //          }

          //var ldtbResultado = frm_RegistrarOrdenServicio.fnc_ValidarCodigos('', lstrCuentaGasto, '', '', document.all('txtAreaSolicitanteCodigo').value, '', '');
          var ldtbResultado = frm_RegistrarOrdenServicio_2.fnc_ValidarCodigos(document.getElementById('txtAreaSolicitanteCodigo').value, lstrCuentaGasto, '', '', '', '', '');
          if (ldtbResultado != null && typeof (ldtbResultado) == "object") {
              var ldtbResultadoOk = ldtbResultado.value;
              //if (ldtbResultadoOk.Rows[0].centrocosto_des == '') {
              if (ldtbResultadoOk.Rows[0].areasolicitante_des == '') {
                  lstrCodigosErroneos = lstrCodigosErroneos + 'La Jefatura solicitante -- ' + ldtbResultadoOk.Rows[0].areasolicitante_cod + ' -- no existe.\n';
              }
              if (ldtbResultadoOk.Rows[0].cuentagasto_des == '' && lstrCuentaGasto.lenght > 0) {
                  lstrCodigosErroneos = lstrCodigosErroneos + 'La cuenta de gasto -- ' + ldtbResultadoOk.Rows[0].cuentagasto_cod + ' -- no existe.\n';
              }
              if (lstrCodigosErroneos != '') {
                  alert(lstrCodigosErroneos);
                  return false;
              }
          }
          else {
              alert('No se pudó validar los códigos ingresados.');
              return false;
          }
          return true;
      }


      //fnc_Datagrid2_Validar	
      function fnc_Datagrid2_Validar(ControlID, Constante) {
          var ltxtCantidad = document.getElementById('txtCantidadS' + Constante);
          var ltxtGrupo = document.getElementById('txtGrupoServ' + Constante);
          var ltxtTipo = document.getElementById('txtTipoServ' + Constante);
          var ltxtCentroCosto = document.getElementById('txtCodigoCentroCosto_S_' + Constante);
          var ltxtCuentaGastos = document.getElementById('txtCodigoCuentaGastos_S_' + Constante);
          var ltxtOrdenServicio = document.getElementById('txtCodOrdenServicio_S_' + Constante);
          var ltxtDesServicio = document.getElementById('txtDesServicio' + Constante);
          var ltxtOrdenTrabajo = document.getElementById('txtCodigoOrdenTrabajo_S_' + Constante);

          var lstrCodigosErroneos = '';
          if (ltxtGrupo.value == '') {
              alert('El código de grupo de servicio no es valido.');
              ltxtGrupo.focus();
              return false;
          }
          if (ltxtTipo.value == '') {
              alert('El código de tipo de servicio no es valido.');
              ltxtTipo.focus();
              return false;
          }
          if (ltxtDesServicio.value == '') {
              alert('La descripción del servicio no es valido.');
              ltxtDesServicio.focus();
              return false;
          }
          if (ltxtDesServicio.value.indexOf('&') >= 0) {
              alert('El caracter -- & -- en la descripción del servicio no es valido.');
              ltxtDesServicio.focus();
              return false;
          }
          if (ltxtCantidad.value == '' || isVarType(ltxtCantidad.value, 'NUM') == false) {
              alert('La cantidad ingresada no es valida.');
              ltxtCantidad.focus();
              return false;
          }
          if (parseInt(ltxtCantidad.value) <= 0) {
              alert('La cantidad ingresada debe ser mayor a cero.');
              ltxtCantidad.focus();
              return false;
          }
          if (ltxtOrdenServicio.value == '' && ltxtCuentaGastos.value == '') {
              alert('Debe ingresar una cuenta de gastos.');
              ltxtCuentaGastos.focus();
              return false;
          }

          if (ltxtOrdenServicio.value != '' && ltxtCuentaGastos.value == '') {
              var strOrdenServicio = ltxtOrdenServicio.value;
              if (strOrdenServicio.substring(0, 1) != '9') {
                  alert('Debe ingresar una cuenta de gastos.');
                  ltxtCuentaGastos.focus();
                  return false;
              }
          }



          if (ltxtOrdenServicio.value != '' && ltxtCuentaGastos.value != '') {
              if (ltxtOrdenServicio.value == '' && (ltxtCuentaGastos.value == '9301003' || ltxtCuentaGastos.value == '9301006' || ltxtCuentaGastos.value == '9301007' || ltxtCuentaGastos.value == '9301008' || ltxtCuentaGastos.value == '9301010' || ltxtCuentaGastos.value == '9301013' || ltxtCuentaGastos.value == '9301014')) {
                  alert('Debe orden de servicio.');
                  ltxtOrdenServicio.focus();
                  return false;
              }
          }
          //DG - INI
//          if (ltxtOrdenTrabajo.value == '') {              
//              var strOrdenServicio2 = ltxtOrdenServicio.value;
//              if (strOrdenServicio2.substring(0, 1) != '9') {
//                  alert('Debe ingresar una orden de trabajo.');
//                  ltxtOrdenTrabajo.focus();
//                  return false;
//              }
//          }
          //DG - FIN
          var ldtbResultado = frm_RegistrarOrdenServicio_2.fnc_ValidarCodigos('', ltxtCuentaGastos.value, ltxtGrupo.value, ltxtTipo.value, ltxtCentroCosto.value, ltxtOrdenServicio.value, '');
          if (ldtbResultado != null && typeof (ldtbResultado) == "object") {
              var ldtbResultadoOk = ldtbResultado.value;
              if (ldtbResultadoOk.Rows[0].gruposervicio_des == '') {
                  lstrCodigosErroneos = lstrCodigosErroneos + 'El grupo de servicio -- ' + ldtbResultadoOk.Rows[0].gruposervicio_cod + ' -- no existe.\n';
              }
              if (ldtbResultadoOk.Rows[0].centrocosto_des == '' && ltxtCentroCosto.value != '') {
                  lstrCodigosErroneos = lstrCodigosErroneos + 'El centro de costo -- ' + ldtbResultadoOk.Rows[0].centrocosto_cod + ' -- no existe.\n';
              }
              if (ldtbResultadoOk.Rows[0].ordenservicio_des == '' && ltxtOrdenServicio.value != '') {
                  lstrCodigosErroneos = lstrCodigosErroneos + 'La orden de servicio -- ' + ldtbResultadoOk.Rows[0].ordenservicio_cod + ' -- no existe.\n';
              }
              if (ldtbResultadoOk.Rows[0].cuentagasto_des == '' && ltxtCuentaGastos.value != '') {
                  lstrCodigosErroneos = lstrCodigosErroneos + 'La cuenta de gastos -- ' + ldtbResultadoOk.Rows[0].cuentagasto_cod + ' -- no existe.\n';
              }

              if (lstrCodigosErroneos != '') {
                  alert(lstrCodigosErroneos);
                  return false;
              }
          }
          else {
              alert('No se pudó validar los códigos ingresados.');
              return false;
          }


          //CONNTROL DE GESTION - DG  -INI
          if (ltxtCuentaGastos.value != "") {
              var ldtbResultado = frm_RegistrarOrdenServicio_2.ValidarCuentaGastoOT(ltxtCuentaGastos.value);
              var strOrdenServicio2 = ltxtOrdenServicio.value;
              if (strOrdenServicio2.substring(0, 1) != '9') {
                  if (ldtbResultado.value != "" && ltxtOrdenTrabajo.value == "") {
                      alert("Debe ingresar una Orden de Trabajo, para el uso de esta cuenta de gastos.");
                      return false;
                  }
              }
              
          }
          //CONNTROL DE GESTION - DG  -FIN
          return true;
      }

      //fnc_ConfirmarAnulacion
      function fnc_ConfirmarAnulacion() {
          var lstrMensaje = 'Se anulará la requisición -- ' + document.getElementById('txtSerie').value + '-' + document.getElementById('txtNumero').value + ' --.\n\n¿Esta seguro de continuar?';
          if (document.getElementById('txtSerie').value != '' && document.getElementById('txtNumero').value != '') {
              return confirm(lstrMensaje);
          }
          return false;
      }

      //Registra fnc_AdjuntarDocs
      function fnc_AdjuntarDocs(strSecuencia) {
          var pstrTipoDoc = "RQS"
          var pstrNumeroDoc = document.getElementById('txtSerie').value + "-" + document.getElementById('txtNumero').value
          var pstrSecuencia = strSecuencia
          if (document.getElementById('txtSituacion').value == "ACTIVO") {
              var pstrMantenimiento = "1";
          }
          else {
              var pstrMantenimiento = "0";
          }

          var retorno = window.showModalDialog("frm_RegistrarArchivoAdjunto.aspx?pstrTipoDoc=" + pstrTipoDoc + "&pstrNumeroDoc=" + pstrNumeroDoc + "&pstrSecuencia=" + pstrSecuencia + "&pstrMantenimiento=" + pstrMantenimiento, "Adjuntar archivo", "dialogHeight:400px;dialogWidth:720px;center:yes;help:no;");
          return false;
      }

      //Lista fnc_AdjuntarDocs
      function fnc_ListarDocsAdjuntos() {
          var pstrTipoDoc = "RQS";
          var pstrNumeroDoc = document.getElementById('txtSerie').value + "-" + document.getElementById('txtNumero').value;
          var pstrSecuencia = " ";
          var retorno = window.showModalDialog("frm_ListadoArchivoAdjunto.aspx?pstrTipoDoc=" + pstrTipoDoc + "&pstrNumeroDoc=" + pstrNumeroDoc + "&pstrSecuencia=" + pstrSecuencia, "Listar Adjuntos", "dialogHeight:400px;dialogWidth:720px;center:yes;help:no;");
          return false;
      }

      //REQSIS201900029 - DG - INI
      function btnEditarFecha_Onclick() {
          if (document.all('txtNumero').value == '') {
              return false;
          }
          var intCodigoPedido = document.all('txtSerie').value  + "-" + document.all('txtNumero').value;
          var retorno = window.showModalDialog("frm_ModificarFechaInstalacion.aspx?intCodigoPedido=" + intCodigoPedido + "&intTipo=" + 2, "", "dialogheight:500px;dialogwidth:800px;center:yes;help:no;status=no");
          frmAprobacionPedidos.hdnFlg.value = "1";
      }
      //REQSIS201900029 - DG - FIN
 </script>


</head>

<body>
 <center>
  <form id="frmRequisicion" name="frmRequisicion" method="post" runat="Server">
   <input id="hdnCodigo" style="WIDTH: 24px; HEIGHT: 22px" type="hidden" size="1" name="hdnCodigo" runat="server" />
   <input id="hdnAccion" style="WIDTH: 24px; HEIGHT: 22px" type="hidden" size="1" name="hdnAccion" />
   <input id="hdnCCDestino" style="WIDTH: 24px; HEIGHT: 22px" type="hidden" size="1" name="hdnCCDestino" runat="server"/>
   <input id="hdnOrdServicio" style="WIDTH: 24px; HEIGHT: 22px" type="hidden" size="1" name="hdnOrdServicio" runat="server"/>
   <input id="hdnOrdenTrabajo" style="WIDTH: 24px; HEIGHT: 22px" type="hidden" size="1" name="hdnOrdenTrabajo" runat="server"/>
   

   <%--cabecera--%>
         <table id="Table1" cellspacing="0" cellpadding="0" border="0" class = "Cabecera" style = "width: 900px; height: 20px">
            <tr>
	            <td class="cabecera" style="HEIGHT: 27px" align="center">REQUISICIÓN DE SERVICIOS</td>
            </tr>
         </table>
            
            <%-- parte 1--%>
            <table cellspacing="2" cellpadding="0" width="900px" border="0">
                <tr>
                    <td class="Etiqueta" style="WIDTH: 150px; height:15px" valign="middle" align = "left">Tipo de Requisición:</td>
                    <td class="Etiqueta" style="WIDTH: 150px; height:15px" valign="middle" align = "left">
                        <asp:textbox id="txtTipoReq" runat="server" Width="140px" CssClass="inputDisabled2" Text = "SERVICIO"></asp:textbox>
                    </td>
                    <td class="Etiqueta" style="WIDTH: 500px; height:15px" valign="middle" align = "left">Número de Requisición:&nbsp;
                        <asp:textbox id="txtSerie" runat="server" Width="50px" CssClass="input" MaxLength="4"></asp:textbox>
                        <asp:textbox id="txtNumero" runat="server" Width="100px" AutoPostBack="True" CssClass="input" MaxLength="10"></asp:textbox>
                    </td>
                </tr>
                <tr>
                    <td class="Etiqueta" style="WIDTH: 150px; height:20px" valign="middle" align = "left">Fecha creación:</td>
                    <td class="Etiqueta" style="WIDTH: 150px; height:20px" valign="middle" align = "left">
                        <asp:textbox id="txtFecha" runat="server" Width="115px" CssClass="input"></asp:textbox>
                        &nbsp;<img onclick="popUpCalendar(this, frmRequisicion.txtFecha, 'dd/mm/yyyy')" height="16" width="16" alt="Seleccionar fecha" src="../../intranet/Imagenes/Calendario.gif" border="0" />
                    </td>
                    <td class="Etiqueta" style="WIDTH: 500px; height:20px" valign="middle" align = "left">Situación:&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                        <asp:textbox id="txtSituacion" runat="server" Width="158px" CssClass="input" MaxLength="10"></asp:textbox>
                    </td>
                </tr>
                <tr>
                    <td class="Etiqueta" style="WIDTH: 150px; height:30px" valign="middle" align = "left">Area Beneficiada:<br />(Pre-Aprobación)</td>
                    <td class="Etiqueta" style="WIDTH: 150px; height:20px" valign="middle" align = "left">
                        <asp:textbox id="txtAreaSolicitanteCodigo" runat="server" Width="115px" CssClass="input" AutoPostBack="false"></asp:textbox>
                        &nbsp;<input class="boton" id="btnAreaSolicitante" style="WIDTH:20px; HEIGHT:20px" onclick="javascript:ListarJefaturaSolicitante();" type="button" value="..." name="Button1" />
                    </td>
                    <td class="Etiqueta" style="WIDTH: 500px; height:20px" valign="middle" align = "left">
                        <asp:textbox id="txtAreaSolicitanteNombre" runat="server" Width="450px" CssClass="inputDisabled2" MaxLength="10"></asp:textbox>
                    </td>
                </tr>
<%--
                <tr id="CuentaGasto" runat="server">
                    <td class="Etiqueta" style="WIDTH: 150px; height:20px" valign="middle" align = "left">Cuenta de gasto :</td>
                    <td class="Etiqueta" style="WIDTH: 150px; height:20px" valign="middle" align = "left">
                        <asp:textbox id="txtCtaGasto" runat="server" Width="115px" CssClass="input"></asp:textbox>
                        &nbsp;<input class="boton" id="cmdCtaGasto" style="WIDTH: 20px; HEIGHT: 20px" onclick="javascript:BuscarCtaGasto('','');" type="button" value="..." />
                    </td>
                    <td class="Etiqueta" style="WIDTH: 500px; height:20px" valign="middle" align = "left"><asp:textbox id="txtDescCtaGasto" runat="server" Width="450px" CssClass="inputDisabled2" MaxLength="10"></asp:textbox></td>
                </tr>
--%>
                <tr>
                    <td class="Etiqueta" style="WIDTH: 150px; height:20px" valign="middle" align = "left">Responsable O/T:</td>
                    <td class="Etiqueta" style="WIDTH: 150px; height:20px" valign="middle" align = "left">
                        <asp:textbox id="txtCodResponsableOT" runat="server" Width="50px" CssClass="input" AutoPostBack="false"></asp:textbox>
                        &nbsp;<input class="boton" id="btnResponsableOT" style="WIDTH:20px; HEIGHT:20px" onclick="javascript:fnc_BuscarDatos(this,'','Responsable','');" type="button" value="..." name="btnResponsableOT" runat="server" />
                    </td>
                    <td class="Etiqueta" style="WIDTH: 500px; height:20px" valign="middle" align = "left">
                        <asp:textbox id="lblDesResponsableOT" Runat="server" width="450px"  CssClass="inputDisabled2"></asp:textbox>
                    </td>
                </tr>
                 <tr>
                    <td class="Etiqueta" style="WIDTH: 150px; height:20px" valign="middle" align = "left">
                        Fecha Inicio de trabajo:</td>
                    <td class="Etiqueta" style="WIDTH: 150px; height:20px" valign="middle" align = "left">
                        <asp:textbox id="txtFecIns" runat="server" Width="100px" CssClass="input"></asp:textbox>
                        &nbsp;<img onclick="popUpCalendar(this, frmRequisicion.txtFecIns, 'dd/mm/yyyy')" height="16" width="16" alt="Seleccionar fecha de Inst." src="../../intranet/Imagenes/Calendario.gif" border="0" />
                        <asp:ImageButton id="btnEditarFecInstal" runat="server" ImageUrl="~/Imagenes/Editar.gif"  Width="16px" Height="16px"></asp:ImageButton>
                    </td>
                    <td class="Etiqueta" style="WIDTH: 500px; height:20px" valign="middle" align = "left"></td>
                </tr>
                <tr>
                    <td class="Etiqueta" style="WIDTH: 150px; height:20px" valign="middle" align = "left">
                        Fecha Fin de trabajo:</td>
                    <td class="Etiqueta" style="WIDTH: 150px; height:20px" valign="middle" align = "left">
                        <asp:textbox id="txtFecExpi" runat="server" Width="100px" CssClass="input"></asp:textbox>
                        &nbsp;<img onclick="popUpCalendar(this, frmRequisicion.txtFecExpi, 'dd/mm/yyyy')" height="16" width="16" alt="Seleccionar fecha de Inst." src="../../intranet/Imagenes/Calendario.gif" border="0" />
                    </td>
                    <td class="Etiqueta" style="WIDTH: 500px; height:20px" valign="middle" align = "left"></td>
                </tr>
                <tr>
                    <td style="height:5px">&nbsp;</td>
                    <td colspan="2" style="height:5px">&nbsp;</td>
                </tr>
                <tr>
                    <td class="Etiqueta" style="WIDTH: 150px; height:20px" valign="middle" align = "left">Motivo:</td>
                    <td align="left" class="Etiqueta" colspan="2">
                        <asp:DropDownList ID="ddlMotivoRequisicion" runat="server" Width="630px" CssClass="input"></asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td class="Etiqueta" style="WIDTH: 150px; height:20px" valign="middle" align = "left">¿Está presupuestado?:&nbsp;</td>
                    <td align="center" class="Etiqueta" style="width:100px">
                        <asp:RadioButton ID="rdPresupuestado_SI" runat="server" text="Si" GroupName="Presupuesto" Width="100px" />
                    </td>
                    <td align="left" class="Etiqueta">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                        <asp:RadioButton ID="rdPresupuestado_NO" runat="server" GroupName="Presupuesto" text="No" />
                    </td>
                </tr>
                <%--CAMBIO DG--%>

                <tr>
                    <td class="Etiqueta" style="WIDTH: 150px; height:20px" valign="middle" align = "left">¿Recibe charla de seguridad?:</td>
                    <td align="center" class="Etiqueta" style="width:100px">
                        <asp:RadioButton ID="rdTipoInterna" runat="server" text="Si" GroupName="Servicio" Width="100px" />
                    </td>
                    <td align="left" class="Etiqueta">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                        <asp:RadioButton ID="rdTipoExterna" runat="server" GroupName="Servicio" text="No"/>
                    </td>
                </tr>
                <%--CAMBIO DG--%>
            </table>

            <%-- parte 2--%>
            <table cellspacing="2" cellpadding="0" width="900px" border="0">
                <tr>
                    <td style="height:5px">&nbsp;</td>
                    <td style="height:5px">&nbsp;</td>
                </tr>
                <tr>
                    <td class="Etiqueta" style="WIDTH: 150px" valign="middle" align = "left">Almacén destino:</td>
                    <td class="Etiqueta" style="WIDTH: 650px" valign="middle" align = "left">
                    <asp:textbox id="txtAlmacen" runat="server" Width="620px" CssClass="inputDisabled2" MaxLength="10"></asp:textbox>
                    </td>
                </tr>
                <tr>
                    <td class="Etiqueta" style="WIDTH: 150px" valign="middle" align = "left">Observaciones :</td>
                    <td class="Etiqueta" style="WIDTH: 650px" valign="middle" align = "left">
                    <asp:textbox id="txtObservacion" runat="server" Width="622px" CssClass="input" MaxLength="200" height="56px" TextMode="MultiLine" Font-Size="10px"></asp:textbox></td>
                </tr>
                <tr>
                    <td class="Etiqueta" style="WIDTH: 150px" valign="middle" align = "left"></td>
                    <td class="Etiqueta" style="WIDTH: 650px" valign="middle" align = "left">
                        <asp:Label ID="lblMensaje" runat="server" Text="" CssClass="error"></asp:Label><br />
                        <asp:Label ID="lblMotivoRechazo" runat="server" Text="" CssClass="error"></asp:Label>
                    </td>
                </tr>
            </table>
            
            <%-- parte 3--%>
            <table cellspacing="2" cellpadding="0" style="width:900px;height:10px;" border="0">
                <tr valign ="top">
                    <td style="WIDTH: 150px" valign="top" align = "left">
                        <input class="Input" id="hdnAprobacion" style="WIDTH: 10px" type="hidden" size="1" runat="server" name="hdnAprobacion"/>
                        <input class="Input" id="hdnAreaSolicitante" style="WIDTH: 10px" type="hidden" size="1" name="Hidden1" runat="server" />
                    </td>
                    <td style="WIDTH: 150px" valign="top" align = "right"><asp:button id="btnGrabar" runat="server" CssClass="Boton" Text="Grabar" Width = "150px"></asp:button>&nbsp;</td>
                    <td style="WIDTH: 150px" valign="top" align = "right"><asp:button id="btnSolicitarPRE" runat="server" CssClass="Boton" Text="Solicitar Aprobacion" Width = "160px"></asp:button>
                                                                             <asp:button id="btnSolicitar" runat="server" CssClass="Boton" Text="Solicitar Aprobacion" Width = "150px"></asp:button></td>
                    <td style="WIDTH: 150px" valign="top" align = "right"><asp:button id="btnAnular" runat="server" CssClass="Boton" Text="Anular" CausesValidation="False" Width = "150px"></asp:button>&nbsp;</td>
                    <td style="WIDTH: 150px" valign="top" align = "right"><asp:button id="BtnNuevo" runat="server" CssClass="Boton" Text="Nuevo" Width = "150px"></asp:button>&nbsp;</td>
                    <td style="WIDTH: 150px" valign="top" align = "right"><asp:button id="btnListaAdjuntos" runat="server" CssClass="Boton" Text="Adjuntos" Width = "150px"></asp:button>&nbsp;</td>
                </tr>
            </table>

        <%-- parte: Grilla--%>
        <table style="WIDTH: 990px">
            <tr>
                <td class="Etiqueta" style="WIDTH: 990px" valign="middle" align = "left">
                    <asp:datagrid id="DataGrid2" runat="server" Width="990px" ShowFooter="True" AutoGenerateColumns="False" Font-Bold="False">
                        <AlternatingItemStyle CssClass="GridAlternateItem"></AlternatingItemStyle>
                        <ItemStyle CssClass="GridItem"></ItemStyle>
                        <HeaderStyle CssClass="GridHeader"></HeaderStyle>
                        <Columns>
                            <%--Secuencia--%>
                            <asp:TemplateColumn HeaderText="Sec.">
                                <HeaderStyle Width="30px"></HeaderStyle>
                                <ItemStyle Width="30px"></ItemStyle>
                                <ItemTemplate><asp:Label id="LblSecuenciaS" runat="server" CssClass="InputCentro" Width="30px" text='<%#Container.DataItem("NU_SECU")%>' Font-Size="10px"></asp:Label></ItemTemplate>
                                <EditItemTemplate><asp:Label id="LblSecuenciaSE" runat="server" CssClass="InputCentro" Width="30px" text='<%#Container.DataItem("NU_SECU")%>' Font-Size="10px"></asp:Label></EditItemTemplate>
                            </asp:TemplateColumn>
	    
                            <asp:TemplateColumn HeaderText="Grpo. Servicio">
                                <HeaderStyle Width="120px"></HeaderStyle>
                                <HeaderTemplate>
                                    <table id="Table8" style="WIDTH: 120px; HEIGHT: 28px" cellspacing="1" cellpadding="0" border="0">
                                    <tr>
		                                <td class="GridHeader" width="60px">Grupo</td>
                                        <td class="GridHeader" width="60px">Tipo Servicio</td>
                                    </tr>
                                    </table>
                                </HeaderTemplate>
                                <ItemStyle Width="120px"></ItemStyle>
                                <ItemTemplate>
		                            <table id="Table11" style="WIDTH: 120px; HEIGHT: 24px" cellspacing="1" cellpadding="0" border="0">
                                    <tr>
		                                <td width="60px">
                                            <asp:Label id="LblGrupoServicio" runat="server" CssClass="InputCentro" Width="60px" text='<%#Container.DataItem("CO_GRUP_SERV")%>' Font-Size="10px"></asp:Label>                                            
                                        </td>
		                                <td width="60px">		                                    
			                                <asp:Label id="LblTipoServicio" runat="server" CssClass="InputCentro" Width="60px" text='<%#Container.DataItem("CO_SERV")%>' Font-Size="10px"></asp:Label>
                                        </td>
		                            </tr>
                                    </table>  
                                </ItemTemplate>
                                <FooterTemplate>
		                            <table id="Table10" style="WIDTH: 120px; HEIGHT: 24px" cellspacing="1" cellpadding="0" border="0">
                                    <tr>
                                        <td width="50px"><asp:TextBox id="txtGrupoServF" ClientIDMode="Static" runat="server" CssClass="input" Width="35px" Font-Size="10px"></asp:TextBox></td>
                                        <td width="50px"><asp:TextBox id="txtTipoServF" ClientIDMode="Static" runat="server" CssClass="input" Width="35px" Font-Size="10px"></asp:TextBox></td>
                                        <td width="20px"><input class="Boton" id="BtnGrupoSerF" style="WIDTH: 20px; HEIGHT: 20px" type="button" value="..." runat="server" name="BtnGrupoSerF" /></td>
		                            </tr>
                                    </table>
		                        </FooterTemplate>
                                <EditItemTemplate>
		                            <table id="Table16" style="WIDTH: 120px; HEIGHT: 24px" cellspacing="1" cellpadding="0" border="0">
                                    <tr>
                                        <td width="50px"><asp:TextBox id="txtGrupoServE"  ClientIDMode="Static" runat="server" CssClass="input" Width="35px"  text='<%#Container.DataItem("CO_GRUP_SERV")%>' Font-Size="10px"></asp:TextBox></td>
                                        <td width="50px"><asp:TextBox id="txtTipoServE"  ClientIDMode="Static" runat="server" CssClass="input" Width="35px" text='<%#Container.DataItem("CO_SERV")%>' Font-Size="10px"></asp:TextBox></td>
                                        <td width="20px"><input class="Boton" id="BtnGrupoSerE" style="WIDTH: 20px; HEIGHT: 20px" type="button" value="..." runat="server" name="BtnGrupoSerE" /></td>
                                    </tr>
		                            </table>
		                        </EditItemTemplate>
                            </asp:TemplateColumn>
                            
                            <%--Descripcion--%>
                            <asp:TemplateColumn HeaderText="Descripcion">
                            <HeaderStyle Width="280px"></HeaderStyle>
                            <HeaderTemplate>
		                        <table id="Table15" style="WIDTH: 280px; HEIGHT: 24px" cellspacing="1" cellpadding="1" border="0">
                                <tr>
                                    <td class="GridHeader">Descripcion de Servicio</td>
                                </tr>
                                </table>
		                    </HeaderTemplate>
                            <ItemStyle Width="280px"></ItemStyle>
		                    <ItemTemplate>
                                <asp:Label id="LblDescServicio" runat="server" CssClass="input" Width="280px" text='<%#Container.DataItem("DE_ITEM")%>' Font-Size="10px"></asp:Label>
                            </ItemTemplate>
                            <FooterTemplate>
                                <asp:TextBox id="txtDesServicioF" ClientIDMode="Static" runat="server" CssClass="Input" Width="270px" MaxLength="200" TextMode="MultiLine" Font-Size="10px"></asp:TextBox>
                            </FooterTemplate>
                            <EditItemTemplate>
                               <asp:TextBox id="txtDesServicioE"  ClientIDMode="Static" runat="server" CssClass="Input" Width="270px" MaxLength="200" TextMode="MultiLine" text='<%#Container.DataItem("DE_ITEM")%>' Font-Size="10px"></asp:TextBox>
                            </EditItemTemplate>
                            </asp:TemplateColumn>

                            
                            <%--Centro Costos--%>
                            <asp:TemplateColumn HeaderText="Centro Costo">
                            <HeaderStyle Width="90px"></HeaderStyle>
                            <HeaderTemplate>
                                <table id="Table19" style="WIDTH: 90px; HEIGHT: 24px" cellspacing="1" cellpadding="0" border="0">
                                <tr>
                                    <td class="GridHeader" width="90px">
			                            <asp:Panel id="Panel11" runat="server" Width="90px">Centro costo</asp:Panel>
                                    </td>
                                </tr>
                                </table>
                            </HeaderTemplate>

                            <ItemTemplate>
                                <table id="Table28" style="WIDTH: 90px; HEIGHT: 24px" cellspacing="1" cellpadding="0" border="0">
                                <tr>
                                    <td width="90px">
                                        <asp:Panel id="Panel15" runat="server" Width="90px">
                                            <asp:Label id="LblCodigoCentroCosto" runat="server" CssClass="input" Width="90px" text='<%#Container.DataItem("CO_AUXI_EMPR")%>' Font-Size="10px"></asp:Label>
                                        </asp:Panel>
                                    </td>
                                </tr>
                                </table>
                            </ItemTemplate>
                            <FooterTemplate>
                                <table id="Table26" style="WIDTH: 90px; HEIGHT: 24px" cellspacing="1" cellpadding="0" border="0">
                                <tr>
                                    <td width="90px"><asp:TextBox id="txtCodigoCentroCosto_S_F" ClientIDMode="Static" runat="server" CssClass="input" Width="60px" Font-Size="10px"></asp:TextBox></td>
                                    <td><input class="Boton" id="BtnCentroCostoSF" style="WIDTH: 20px; HEIGHT: 20px" type="button" value="..." runat="server" name="BtnCentroCostoSF" /></td>
                                </tr>
                                </table>
                            </FooterTemplate>
		                    <EditItemTemplate>
                                <table id="Table29" style="WIDTH: 90px; HEIGHT: 24px" cellspacing="1" cellpadding="0" border="0">
                                <tr>
                                    <td><asp:TextBox id="txtCodigoCentroCosto_S_E"  ClientIDMode="Static" runat="server" CssClass="input" Width="60px" text='<%#Container.DataItem("CO_AUXI_EMPR")%>' Font-Size="10px"></asp:TextBox></td>
                                    <td><input class="Boton" id="BtnCentroCostoSE" style="WIDTH: 20px; HEIGHT: 20px" type="button" value="..." runat="server" name="BtnCentroCostoSE" /></td>
                                </tr>
                                </table>
                            </EditItemTemplate>
		                    </asp:TemplateColumn>
                            <%--Cuenta de Gastos--%>
                            <asp:TemplateColumn HeaderText="Cuenta Gastos">
                            <HeaderStyle Width="90px"></HeaderStyle>
                            <HeaderTemplate>
                                <table id="Table192" style="WIDTH: 90px; HEIGHT: 24px" cellspacing="1" cellpadding="1" border="0">
                                <tr>
                                    <td class="GridHeader" width="90px">
			                            <asp:Panel id="Panel11" runat="server" Width="90px">Cuenta Gastos</asp:Panel>
                                    </td>
                                </tr>
                                </table>
                            </HeaderTemplate>

                            <ItemTemplate>
                                <table id="Table282" style="WIDTH: 90px; HEIGHT: 24px" cellspacing="1" cellpadding="0" border="0">
                                <tr>
                                    <td width="90px">
                                            <asp:Label id="LblCodigoCuentaGastos" runat="server" CssClass="input" Width="90px" text='<%#Container.DataItem("DE_OBSE")%>' Font-Size="10px"></asp:Label>
                                    </td>
                                </tr>
                                </table>
                            </ItemTemplate>
                            <FooterTemplate>
                                <table id="Table262" style="WIDTH: 90px; HEIGHT: 24px" cellspacing="1" cellpadding="0" border="0">
                                <tr>
                                    <td width="90px"><asp:TextBox id="txtCodigoCuentaGastos_S_F" ClientIDMode="Static" runat="server" CssClass="input" Width="60px" Font-Size="10px"></asp:TextBox></td>
                                    <td><input class="Boton" id="BtnCuentaGastosSF" style="WIDTH: 20px; HEIGHT: 20px" type="button" value="..." runat="server" name="BtnCuentaGastosSF" /></td>
                                </tr>
                                </table>
                            </FooterTemplate>
		                    <EditItemTemplate>
                                <table id="Table292" style="WIDTH: 90px; HEIGHT: 24px" cellspacing="1" cellpadding="1" border="0">
                                <tr>
                                    <td><asp:TextBox id="txtCodigoCuentaGastos_S_E" ClientIDMode="Static" runat="server" CssClass="input" Width="60px" text='<%#Container.DataItem("DE_OBSE")%>' Font-Size="10px"></asp:TextBox></td>
                                    <td><input class="Boton" id="BtnCuentaGastosSE" style="WIDTH: 20px; HEIGHT: 20px" type="button" value="..." runat="server" name="BtnCuentaGastosSE" /></td>
                                </tr>
                                </table>
                            </EditItemTemplate>
		                    </asp:TemplateColumn>

                            <%--ORDEN TRABAJO--%>
                            <asp:TemplateColumn HeaderText="Orden Trabajo">
                            <HeaderStyle Width="90px"></HeaderStyle>
                            <HeaderTemplate>
                                <table id="Table193" style="WIDTH: 90px; HEIGHT: 24px" cellspacing="1" cellpadding="0" border="0">
                                <tr>
                                    <td class="GridHeader" width="90px">
			                            <asp:Panel id="Panel111" runat="server" Width="120px">Orden Trabajo:/Monto:</asp:Panel>
                                    </td>
                                </tr>
                                </table>
                            </HeaderTemplate>

                            <ItemTemplate>
                                <table id="Table281" style="WIDTH: 90px; HEIGHT: 24px" cellspacing="1" cellpadding="0" border="0">
                                <tr>
                                    <td width="90px">
                                        <asp:Panel id="Panel115" runat="server" Width="90px">
                                            <asp:Label id="LblCodigoOrdenTrabajo" runat="server" CssClass="input" Width="90px" text='<%#Container.DataItem("NU_ORTR")%>' Font-Size="10px"></asp:Label>
                                        </asp:Panel>
                                    </td>
                                </tr>
                                </table>
                            </ItemTemplate>
                            <FooterTemplate>
                                <table id="Table26" style="WIDTH: 140px; HEIGHT: 24px" cellspacing="1" cellpadding="0" border="0">
                                <tr>
                                    <td width="90px"><asp:TextBox id="txtCodigoOrdenTrabajo_S_F" ClientIDMode="Static" runat="server" CssClass="input" Width="75px" Font-Size="10px"></asp:TextBox></td>
                                    <td><input class="Boton" id="BtnOrdenTrabajoSF" clientidmode="Static"  style="WIDTH: 15px; HEIGHT: 20px" type="button" value="..." runat="server" name="BtnOrdenTrabajoSF" /></td>
                                    <td width="90px"><asp:TextBox ID="txtMonto_F" runat="server" ClientIDMode="Static" BorderStyle="None" Cssclass="Etiqueta" Font-Size="8px" width="50px"></asp:TextBox></td>
                                </tr>
                                </table>
                            </FooterTemplate>
		                    <EditItemTemplate>
                                <table id="Table29" style="WIDTH: 90px; HEIGHT: 24px" cellspacing="1" cellpadding="0" border="0">
                                <tr>
                                    <td><asp:TextBox id="txtCodigoOrdenTrabajo_S_E"  runat="server" CssClass="input" Width="60px" text='<%#Container.DataItem("NU_ORTR")%>' Font-Size="10px"></asp:TextBox></td>
                                    <td><input class="Boton" id="BtnOrdenTrabajoSE" style="WIDTH: 20px; HEIGHT: 20px" type="button" value="..." runat="server" name="BtnOrdenTrabajoSE" /></td>
                                </tr>
                                </table>
                            </EditItemTemplate>
		                    </asp:TemplateColumn>

                            <%--Orden dse Servicio--%>
                            <asp:TemplateColumn HeaderText="Orden Servicio">
                            <HeaderStyle Width="90px"></HeaderStyle>
                            <HeaderTemplate>
                                <table id="Table27" cellspacing="1" cellpadding="0" width="90px" border="0">
                                <tr>
                                    <td class="GridHeader"><asp:Panel id="Panel2" runat="server" Width="85px">Órden servicio</asp:Panel></td>
<%--                                    <td class="GridHeader" width="75px"></td>--%>
                                </tr>
                                </table>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <table id="Table21"  style="WIDTH: 90px; HEIGHT: 24px" cellspacing="1" cellpadding="0" border="0">
                                <tr>
                                    <td>
                                        <asp:Panel id="Panel9" runat="server" Width="85px">
                                            <asp:Label id="Label1" runat="server" CssClass="input" Width="85px" text='<%#Container.DataItem("CO_ORDE_SERV")%>' Font-Size="10px"></asp:Label>
                                        </asp:Panel>
                                    </td>
                                    <%--<td width="80px"></td>--%>
                                </tr>
                                </table>
                            </ItemTemplate> 
                            <FooterTemplate>
		                        <table id="Table12" style="WIDTH: 90px; HEIGHT: 24px" cellspacing="1" cellpadding="0" border="0">
                                <tr>
                                    <td><asp:TextBox id="txtCodOrdenServicio_S_F" ClientIDMode="Static" runat="server" CssClass="Input" Width="60px" Font-Size="10px"></asp:TextBox></td>
                                    <td><input class="Boton" id="BtnOrdenServF" style="WIDTH: 20px; HEIGHT: 20px" type="button"	value="..." name="Button1" runat="server" /></td>
                                </tr>
                                </table>
                            </FooterTemplate>
                        <EditItemTemplate>
                            <table id="Table25" style="WIDTH: 90px; HEIGHT: 24px" cellspacing="1" cellpadding="0" border="0">
                            <tr>
                                <td><asp:TextBox id="txtCodOrdenServicio_S_E"  ClientIDMode="Static" runat="server" CssClass="input" Width="60px" text='<%#Container.DataItem("CO_ORDE_SERV")%>' Font-Size="10px"></asp:TextBox></td>
                                <td><input class="Boton" id="BtnOrdenServE" style="WIDTH: 20px; HEIGHT: 20px" type="button"	value="..." name="Button2" runat="server" /></td>
                            </tr>
                            </table>
		                </EditItemTemplate>
                    </asp:TemplateColumn>
                    
                    <%--Cantidad--%>
                    <asp:TemplateColumn HeaderText="Cantidad">
		            <HeaderStyle HorizontalAlign="Center" Width="80px"></HeaderStyle>
		            <ItemTemplate>
                        <asp:Label id="LblCantidadS" runat="server" CssClass="inputDerecha" Width="60px" text='<%#Format(Container.DataItem("CA_SOLI"),"0.00")%>' Font-Size="10px"></asp:Label>
                    </ItemTemplate>
		            <FooterTemplate>
                        <asp:TextBox id="txtCantidadSF" ClientIDMode="Static" runat="server" CssClass="inputDerecha" Width="50px" Font-Size="10px"></asp:TextBox>
                    </FooterTemplate>
		            <EditItemTemplate>
                        <asp:TextBox id="txtCantidadSE" ClientIDMode="Static" runat="server" CssClass="inputDerecha" Width="50px" text='<%#Container.DataItem("CA_SOLI")%>' Font-Size="10px"></asp:TextBox>
                    </EditItemTemplate>
                    </asp:TemplateColumn>

                    <%--Adjuntos--%>
                    <asp:TemplateColumn HeaderText="Adj.">
                    <HeaderStyle HorizontalAlign="Center" Width="30px"></HeaderStyle>
                    <ItemTemplate>
                        <asp:Label id="lblAdjuntos" runat="server" CssClass="InputDerecha" Width="30px" text='<%#Format(Container.DataItem("Adjuntos"))%>' Font-Size="10px"></asp:Label>
                    </ItemTemplate>
                    </asp:TemplateColumn>

                    <%--Botones--%>
                    <asp:TemplateColumn>
                    <HeaderStyle Width="90px"></HeaderStyle>
		            <ItemTemplate>
<%--		                <asp:ImageButton id="BtnEditS" runat="server" CommandName="Editar" ImageUrl="../../intranet/Imagenes/Editar.gif" ToolTip="Editar"></asp:ImageButton>&nbsp;
		                <asp:ImageButton id="btnEliminarS" runat="server" CommandName="Eliminar" ImageUrl="../../intranet/Imagenes/borrador.gif" ToolTip="Eliminar"></asp:ImageButton>&nbsp;
                        <asp:ImageButton id="btnAjuntarS" runat="server" CommandName="Adjuntar" ImageUrl="../../intranet/Imagenes/adjuntar_16x16.bmp" ToolTip="Adjuntar"></asp:ImageButton>--%>
                        <asp:ImageButton id="BtnEditS" runat="server" CommandName="Editar" ImageUrl="../images/im_edit.gif" ToolTip="Editar"></asp:ImageButton>&nbsp;
		                <asp:ImageButton id="btnEliminarS" runat="server" CommandName="Eliminar" ImageUrl="../images/trash_16.gif" ToolTip="Eliminar"></asp:ImageButton>&nbsp;
                        <asp:ImageButton id="btnAjuntarS" runat="server" CommandName="Adjuntar" ImageUrl="../images/adjuntar.bmp" ToolTip="Adjuntar"></asp:ImageButton>
                    </ItemTemplate>
		            <FooterTemplate><asp:ImageButton id="btnAdicionaS" runat="server" CommandName="Add" ImageUrl="../images/save.gif" ToolTip="Grabar"></asp:ImageButton></FooterTemplate>
                    <EditItemTemplate>
		                <asp:ImageButton id="btnGuardarS" runat="server" CommandName="Grabar" ImageUrl="../images/save.gif" ToolTip="Grabar"></asp:ImageButton>&nbsp;
		                <asp:ImageButton id="btnCancelarS" runat="server" CommandName="Cancel" ImageUrl="../images/Cancelar.gif" ToolTip="Cancelar"></asp:ImageButton>
		                </EditItemTemplate>
                    </asp:TemplateColumn>
	        </Columns>
	 </asp:datagrid>
    </td>
   </tr>
  </table>
   <%--Secuencia--%>
  <input id="HDN2" style="WIDTH: 30px; HEIGHT: 20px" type="hidden" size="1" name="HDN2" runat="server" />
  <input id="HDN1" style="WIDTH: 30px; HEIGHT: 20px" type="hidden" size="1" name="HDN1" runat="server" />
  <input id="HDNArticulo" style="WIDTH: 30px; HEIGHT: 20px" type="hidden" size="1" name="HDNArticulo" runat="server" />
  <input id="HDNUnidadMedida" style="WIDTH: 30px; HEIGHT: 20px" type="hidden" size="1" name="HDNUnidadMedida" runat="server" />
  <input id="HDNDesCtaGasto" style="WIDTH: 30px; HEIGHT: 20px" type="hidden" size="1" name="HDNDesCtaGasto" runat="server" />
  <input id="HDNCtaGasto" style="WIDTH: 30px; HEIGHT: 20px" type="hidden" size="1" name="HDNCtaGasto" runat="server" />
  <input id="HdnDescServicio" style="WIDTH: 30px; HEIGHT: 20px" type="hidden" size="1" name="HdnDescServicio" runat="server" />
 
 </form>
</center>
</body>
</html>
