<%@ Page Language="vb" AutoEventWireup="false" Codebehind="LOG20001.aspx.vb" Inherits="intranet_logi.LOG20001"%>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html>
	<head>
		<title></title>
		<meta content="Microsoft Visual Studio.NET 7.0" name="GENERATOR"/>
		<meta content="Visual Basic 7.0" name="CODE_LANGUAGE"/>
		<meta content="JavaScript" name="vs_defaultClientScript"/>
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema"/>
		<link href="../css/NM0001.css" type="text/css" rel="stylesheet"/>
		<script language="javascript" type="text/javascript" src="../../intranet/JS/jsCalendario_N4.js"></script>
		<script language="javascript" type="text/javascript" src="../../intranet/JS/functions.js"></script>
		<script language="JavaScript" type="text/javascript">
		    function txtObservaciones_onBlur()
		    {
			    var txtObservaciones = document.all("txtObservacion");
			    txtObservaciones.value = txtObservaciones.value.toUpperCase();
			    var lstrObservaciones=txtObservaciones.value;
			    if(lstrObservaciones.length>200)
			    {
				    alert('Solo se permiten 200 caracteres para la observación.');
				    txtObservaciones.value = txtObservaciones.value.substring(0,199);
			    }//end if
		    }//end function
		
		    function txtDesServicio_onBlur(ControlID,Constante)
		    {	
			    var txtDesServicio = document.all[ControlID + "_txtDesServicio"+Constante];
			    txtDesServicio.value = txtDesServicio.value.toUpperCase();
			    var lstrObservaciones=txtDesServicio.value;
			    if(lstrObservaciones.length>200)
			    {
				    alert('Solo se permiten 200 caracteres para la descripción del servicio.');
				    txtDesServicio.value = txtDesServicio.value.substring(0,199);
			    }//end if
		    }//end function
		
		    function BuscarCentroCosto()
		    {
			    var txtCodigo= document.all("txtAreaSolicitanteCodigo");
			    var lblNombre = document.all("txtAreaSolicitanteNombre");
			    var oXml = new ActiveXObject("Microsoft.XMLDOM");
			    oXml.async = false;
			    oXml.validateOnParse = false;
				
			    oXml.load("../NMGetXml.aspx?Opcion=CentroCosto&Codigo=" + txtCodigo.value);
			    root = oXml.documentElement;
			    if (root.childNodes.length>0)
			    {
				    var opc = root.getElementsByTagName("Datos")(0);
				    txtCodigo.value = opc.getAttribute("CentroCostoCodigo");
				    lblNombre.value = opc.getAttribute("CentroCostoNombre");
				    document.getElementById("hdnAreaSolicitante").value = opc.getAttribute("CentroCostoNombre");
			    }
		    }
		    function SolicitarAprobacion()
		    {
			    document.getElementById("hdnAprobacion").value='';
			    /*
			    var retorno = window.showModalDialog("LOG20001_1.aspx","","dialogHeight:200px;dialogWidth:500px;center:yes;help:no;");
			    */
			    //////////////////////////////////////////
			    var retorno = window.showModalDialog("../../intranet/buscadores/frmTiposAprobacion.aspx?strCodigo=&strNombre=","","dialogHeight:450px;dialogWidth:450px;center:yes;help:no;");
			    if (retorno!="" && retorno!=":")
			    {
				    var datos = retorno.split(":");
				    //txtCodigo.value = datos[0];
				    //lblNombre.innerHTML = datos[1];
				    //document.getElementById("hdnTipo.value = datos[1];
				    document.getElementById("hdnAprobacion").value=datos[0];
				    return true;
			    }//end if
			    return false;
		    }//end function
		
		    function BuscarArticulo(ControlID,Constante)
		    {
			    var lstrAlmacen=document.all('cmbAlmacen').value;
			    var retorno = window.showModalDialog("../../intranet/Buscadores/frmBusquedaArticulosStock.aspx?pstrAlmacen="+lstrAlmacen,"","dialogHeight:450px;dialogWidth:550px;center:yes;help:no;");
			    //retorno debe traer el resultado de la busqueda
			    if (retorno!="::")
			    {
				    var datos = retorno.split(":");
				    var Codigo=datos[0];
				    var Articulo=datos[1];
				    var Unidad=datos[2];
				    if(Codigo=='')
				    {
					    return false;
				    }//end if
				    switch (Constante)
							    {
							    case "F":
								    var txtArticuloCod = document.all[ControlID + "_txtCodArticuloF"];
								    var lblArticulo = document.all[ControlID + "_lblArticuloF"];
								    var lblUnidadM=document.all[ControlID + "_lblUnidadMF"];
								    txtArticuloCod.value=datos[0];
								    lblArticulo.innerHTML =datos[1];
								    document.getElementById("HDNArticulo").value=datos[1];
								    lblUnidadM.innerHTML =datos[2];
								    break;
							    case "E":
								    var txtArticuloCod = document.all[ControlID + "_txtCodArticuloE"];
								    var lblArticulo = document.all[ControlID + "_lblArticuloE"];
								    var lblUnidadM=document.all[ControlID + "_lblUnidadME"];
								    txtArticuloCod.value=Codigo=datos[0];
								    lblArticulo.innerHTML =datos[1];
								    lblUnidadM.innerHTML =datos[2];
								    break;
							    }
						    document.getElementById("HDNUnidadMedida").value=datos[2];
			    }
		    }//end function
		    function ListarCentroCosto(ControlID,Constante) /*Muestra el listado de los centro de Costos */
		    {
			    if(document.all('ddlTipo').value=='SER')
			    {
			    if (document.all['txtAreaSolicitanteCodigo'].value!="")
				    {
				    if (document.all['txtCtaGasto'].value!="")
					    {
					    var strCtaGasto=document.all['txtCtaGasto'].value;
					    }
				    else
					    {
					    alert("Por favor seleccionar una cuenta de gasto...!");
					    document.all('txtCtaGasto').focus();
					    return false;
					    }
				    }
				    else
				    {
				    var strCtaGasto = "";
				    }
			    }
			    else
			    {
				    var strCtaGasto = "";
			    }
			    //alert(strCtaGasto);
			    var retorno = window.showModalDialog("../../intranet/Buscadores/LOG_0001.aspx?strCtaGasto=" + strCtaGasto ,"","dialogHeight:450px;dialogWidth:450px;center:yes;help:no;");
			    if (retorno!="" && retorno!=":")
			    {
				    var datos = retorno.split(":");
				    var Codigo = datos[0];
				    var Nombre = datos[1];
				    switch (Constante)
					    {
					    case "F":
						    if(document.all('ddlTipo').value=='ART')
						    {
							    var txtCodigoCentroCostoF=document.all[ControlID + "_txtCodigoCentroCostoF"];
						    }
						    else
						    {
							    var txtCodigoCentroCostoF=document.all[ControlID + "_txtCodigoCentroCosto_S_F"];
						    }//end if
						    //var LblDescCentroCostoF=document.all[ControlID + "_LblDescCentroCostoF"];
						    txtCodigoCentroCostoF.value = Codigo;
						    //LblDescCentroCostoF.innerHTML=Nombre;
						    break;
					    case "E":
						    if(document.all('ddlTipo').value=='ART')
						    {
							    var txtCodigoCentroCostoE=document.all[ControlID + "_txtCodigoCentroCostoE"];
						    }
						    else
						    {
							    var txtCodigoCentroCostoE=document.all[ControlID + "_txtCodigoCentroCosto_S_E"];
						    }//end if
						    //var LblDescCentroCostoE=document.all[ControlID + "_LblDescCentroCostoE"];
						    txtCodigoCentroCostoE.value = Codigo;
						    //LblDescCentroCostoE.innerHTML=Nombre;
						    break;
					    case "":
						    document.all('txtCentroCostoCodigo').value = Codigo;
						    document.all('lblCentroCostoNombre').value = Nombre;
						    break;
					    case "A":
						    document.all('txtAreaSolicitanteCodigo').value = Codigo;
						    document.all('txtAreaSolicitanteNombre').value = Nombre;
						    break;
					    }
				    document.getElementById("HDN1").value=Nombre;
			    }
		    }
	
		    function BuscarOrdenServicio(ControlID,Constante) /*Muestra el listado de los Activos fijos y las ordenes de Trabajo*/
		    {
			    var retorno = window.showModalDialog("../../intranet/Buscadores/frmOrdenServicio.aspx","","dialogHeight:450px;dialogWidth:450px;center:yes;help:no;");
			    if (retorno!="" && retorno!=":")
			    {
				    var datos = retorno.split(":");
				    var Codigo = datos[0];
				    var Nombre = datos[1];
				    switch (Constante)
					    {
					    case "F":
						    if(document.all('ddlTipo').value=='ART')
						    {
							    var txtCodOrdenServicioF=document.all[ControlID + "_txtCodOrdenServicioF"];
						    }
						    else
						    {
							    var txtCodOrdenServicioF=document.all[ControlID + "_txtCodOrdenServicio_S_F"];
						    }//end if
						    //var LblDescOrdenServicioF=document.all[ControlID + "_LblDescOrdenServicioF"];
						    txtCodOrdenServicioF.value = Codigo;
						    //LblDescOrdenServicioF.innerHTML=Nombre;
						    break;
					    case "E":
						    if(document.all('ddlTipo').value=='ART')
						    {
							    var txtCodOrdenServicioE=document.all[ControlID + "_txtCodOrdenServicioE"];
						    }
						    else
						    {
							    var txtCodOrdenServicioE=document.all[ControlID + "_txtCodOrdenServicio_S_E"];
						    }//end if
						    txtCodOrdenServicioE.value = Codigo;
						    break;
					    case "":
						    document.all('txtCodigoServicio').value=Codigo;
						    document.all('txtNombreServicio').value=Nombre;
						    break;
					    }
				    document.getElementById("HDN2").value=Nombre;
			    }
		    }	
		    function BuscarCtaGasto(ControlID,Constante) /*Muestra el listado de las Cuentas de Gastos de Contabilidad*/
		    {
			    var strCentroCosto;
			    switch (Constante)
			    {
			    case "F":
						    var valor = document.all[ControlID + "_txtCodigoCentroCostoF"];
						    strCentroCosto = valor.value;
						    break;
			    case "E":
						    var valor = document.all[ControlID + "_txtCodigoCentroCostoE"];
						    strCentroCosto = valor.value;
						    break;
			    case "":
						    strCentroCosto = "";
						    break;
			    }
			    //alert(strCentroCosto );
			    var retorno = window.showModalDialog("../../intranet/Buscadores/frmCtaGasto.aspx?strCentroCosto=" + strCentroCosto ,"","dialogHeight:450px;dialogWidth:450px;center:yes;help:no;");
			    if (retorno!="" && retorno!=":")
			    {
				    var datos = retorno.split(":");
				    var Codigo = datos[0];
				    var Nombre = datos[1];
				    switch (Constante)
					    {
					    case "F":
						    var txtCtaGastoSF = document.all[ControlID + "_txtCtaGastoSF"];
						    //var LblDesCtaGastoSF = document.all[ControlID + "_LblDesCtaGastoSF"];
						    txtCtaGastoSF.value=Codigo;
						    //LblDesCtaGastoSF.innerHTML=Nombre;
						    break;
					    case "E":
						    var txtCtaGastoSE = document.all[ControlID + "_txtCtaGastoSE"];
						    //var LblDesCtaGastoSE = document.all[ControlID + "_LblDesCtaGastoSE"];
						    txtCtaGastoSE.value=Codigo;
						    //LblDesCtaGastoSE.innerHTML=Nombre;
						    break;
					    case "":
						    document.all('txtCtaGasto').value=Codigo;
						    document.all('txtDescCtaGasto').value=Nombre;
						    break;
					    }
				    document.getElementById("HDNCtaGasto").value=Nombre;
			    }
		    }	
		    function BuscarServicio(ControlID)
		    {
			    var retorno = window.showModalDialog("../../intranet/Buscadores/frmServicios.aspx","","dialogHeight:450px;dialogWidth:450px;center:yes;help:no;");
			    if (retorno!="" && retorno!="::")
			    {
				    var datos = retorno.split(":");
				    var Grupo = datos[0];
				    var Codigo = datos[1];
				    var Nombre = datos[2];
				    var txtGrupoServF = document.all[ControlID + "_txtGrupoServF"];
				    var txtTipoServF =document.all[ControlID + "_txtTipoServF"];
				    //var LblDescServF = document.all[ControlID + "_LblDescServF"];
				    txtGrupoServF.value=Grupo;
				    txtTipoServF.value=Codigo;
				    //LblDescServF.innerHTML=Nombre;
				    document.getElementById("HdnDescServicio").value = Nombre;
			    }
		    }	
		    function popUp(strUrl) 
		    {
			    var intWidth = screen.width;
			    var intHeight = screen.height;
			    window.open(strUrl, "", "top=0; left=0; width=" + intWidth + "; height=" + intHeight + "; resizable=1;");
			}
            /*	
            //20120905 EPM Se comenta debido a que no se usa y el formulario de adjunto no se migra 2010.
		    function fnc_AdjuntarDocs(pstrCodigoCtc,pstrCodigoCosto)
		    {
		        var retorno = window.showModalDialog("LOG20001_A.aspx?pstrcodigorequisicion="+pstrCodigoCtc+"&pstrcodigoarticulo="+pstrCodigoCosto+"&pstrMantenimiento=1","","dialogHeight:600px;dialogWidth:750px;center:yes;help:no;");    
			    //refrescar lista de ctc
			    if (retorno!="" && retorno!=":")
				    {
					    var datos = retorno.split(":");
					    var codigo = datos[0];
					    if(codigo=="1")
						    {
						    return true;
						    }//end if
					    return false;
				    }//end if			
				    return false;	
		    }//end function.
			*/

		    function txtCantidad_onBlur(ControlID,Constante)
		    {
			    var ltxtCantidad=document.all(ControlID + '_txtCantidad'+Constante);
			    if(ltxtCantidad.value=='' || isVarType(ltxtCantidad.value,'NUM')==false)
			    {
				    alert('La cantidad ingresada no es valida.');
				    ltxtCantidad.focus();
				    return false;
			    }//end if	
			    ltxtCantidad.value=parseFloat(ltxtCantidad.value).toFixed(2);
			    return true;
		    }//end function
		
		    function txtCantidadS_onBlur(ControlID,Constante)
		    {
			    var ltxtCantidadS=document.all(ControlID + '_txtCantidadS'+Constante);
			    if(ltxtCantidadS.value=='' || isVarType(ltxtCantidadS.value,'NUM')==false)
			    {
				    alert('La cantidad ingresada no es valida.');
				    ltxtCantidadS.focus();
				    return false;
			    }//end if	
			    ltxtCantidadS.value=parsefloat(ltxtCantidadS.value);
			    return true;
		    }//end function
		
		    function fnc_ValidarRequisicion()
		    {
			    var lstrCuentaGasto='';
			    var lstrCodigosErroneos='';
			    if(document.all('txtFecha').value=='')
			    {
				    alert('Debe ingresar la fecha de creación.');
				    document.all('txtFecha').focus();
				    return false;
			    }//end if
			    if(document.all('txtAreaSolicitanteCodigo').value=='' || document.all('txtAreaSolicitanteNombre').value=='')
			    {
				    alert('El código de area solicitante no es valido.');
				    document.all('txtAreaSolicitanteCodigo').focus();
				    return false;
			    }//end if
			    if(document.all('ddlTipo').value=='SER')
			    {
				    lstrCuentaGasto=document.all('txtCtaGasto').value;
				    /*if(document.all('txtCtaGasto').value!='' || document.all('txtDescCtaGasto').value=='')
				    {
					    alert('El código de cuenta de gasto no es valido.');
					    document.all('txtCtaGasto').focus();
					    return false;
				    }//end if*/
			    }//end if
			    var ldtbResultado =LOG20001.fnc_ValidarCodigos('',lstrCuentaGasto,'','', document.all('txtAreaSolicitanteCodigo').value, '', '');		
			    if(ldtbResultado!=null && typeof(ldtbResultado)=="object")
			    {
				    var ldtbResultadoOk=ldtbResultado.value;
				    if(ldtbResultadoOk.Rows[0].centrocosto_des=='')
				    {
					    lstrCodigosErroneos=lstrCodigosErroneos+'El área solicitante -- '+ldtbResultadoOk.Rows[0].centrocosto_cod+' -- no existe.\n';
				    }//end if
				    if(ldtbResultadoOk.Rows[0].cuentagasto_des=='' && lstrCuentaGasto.lenght>0)
				    {
					    lstrCodigosErroneos=lstrCodigosErroneos+'La cuenta de gasto -- '+ldtbResultadoOk.Rows[0].cuentagasto_cod+' -- no existe.\n';
				    }//end if
				    if(lstrCodigosErroneos!='')
				    {
					    alert(lstrCodigosErroneos);
					    return false;
				    }//end if
			    }
			    else
			    {
				    alert('No se pudó validar los códigos ingresados.');
				    return false;
			    }//end if
			    return true;
		    }//end function
		
		    function fnc_Datagrid1_Validar(ControlID,Constante)
		    {
			    //verificar cantidad >0 y los códigos de CC, CG, OS, CA
			    var ltxtCantidad=document.all(ControlID + '_txtCantidad'+Constante);
			    var ltxtArticulo=document.all(ControlID + '_txtCodArticulo'+Constante);
			    var ltxtCentroCosto=document.all(ControlID + '_txtCodigoCentroCosto'+Constante);
			    var ltxtCuentaGasto=document.all(ControlID + '_txtCtaGastoS'+Constante);
			    var ltxtOrdenServicio=document.all(ControlID + '_txtCodOrdenServicio'+Constante);
			    var lintstock=0;
			    var lstrCodigosErroneos='';
			    if(document.forms[0].CheckStock.checked==true && document.all('ddlTipo').value=='ART')
			    {
				    lintstock=1;
			    }//end if
			    /////////////////////
			    if(ltxtArticulo.value=='')
			    {
				    alert('El código de artículo no es valido.');
				    ltxtArticulo.focus()
				    return false;
			    }//end if
			    /////////////////////
			    if(ltxtCantidad.value=='' || isVarType(ltxtCantidad.value,'NUM')==false)
			    {
				    alert('La cantidad ingresada no es valida.');
				    ltxtCantidad.focus()
				    return false;
			    }//end if	
			    if(parseFloat(ltxtCantidad.value)<=0)
			    {
				    alert('La cantidad ingresada debe ser mayor a cero.');
				    ltxtCantidad.focus()
				    return false;
			    }//end if	
			    ///////////////////////////////
			    var ldtbResultado =LOG20001.fnc_ValidarCodigos('',ltxtCuentaGasto.value,'','', ltxtCentroCosto.value, ltxtOrdenServicio.value, ltxtArticulo.value);		
			    if(ldtbResultado!=null && typeof(ldtbResultado)=="object")
			    {
				    var ldtbResultadoOk=ldtbResultado.value;
				    if(ldtbResultadoOk.Rows[0].articulo_des=='')
				    {
					    lstrCodigosErroneos=lstrCodigosErroneos+'El artículo -- '+ldtbResultadoOk.Rows[0].articulo_cod+' -- no existe.\n';
				    }//end if
				    if(ltxtCentroCosto.value!='')
				    {
				        if(ldtbResultadoOk.Rows[0].centrocosto_des=='')
				        {
					        lstrCodigosErroneos=lstrCodigosErroneos+'El centro de costo -- '+ldtbResultadoOk.Rows[0].centrocosto_cod+' -- no existe.\n';
				        }//end if
				    }//end if
				    if(ltxtOrdenServicio.value!='')
				    {
				        if(ldtbResultadoOk.Rows[0].ordenservicio_des=='' && ltxtOrdenServicio.value!='')
				        {
					        lstrCodigosErroneos=lstrCodigosErroneos+'La orden de servicio -- '+ldtbResultadoOk.Rows[0].ordenservicio_cod+' -- no existe.\n';
				        }//end if
				    }//end if
				    if(ltxtCuentaGasto.value!='')
				    {
				        if(ldtbResultadoOk.Rows[0].cuentagasto_des=='')
				        {
					        lstrCodigosErroneos=lstrCodigosErroneos+'La cuenta de gasto -- '+ldtbResultadoOk.Rows[0].cuentagasto_cod+' -- no existe.\n';
				        }//end if
				    }//end if
				    if(lstrCodigosErroneos!='')
				    {
					    alert(lstrCodigosErroneos);
					    return false;
				    }//end if
			    }
			    else
			    {
				    alert('No se pudó validar los códigos ingresados.');
				    return false;
			    }//end if
			    ///////////////////////////////
			    return true;
			    ///////////////////////////////
		    }//end function
		
		    function fnc_Datagrid2_Validar(ControlID,Constante)
		    {
			    //verificar cantidad >0 y los códigos de CC, OS, Grupo,Tipo
			    var ltxtCantidad=document.all(ControlID + '_txtCantidadS'+Constante);
			    var ltxtGrupo=document.all(ControlID + '_txtGrupoServ'+Constante);
			    var ltxtTipo=document.all(ControlID + '_txtTipoServ'+Constante);
			    var ltxtCentroCosto=document.all(ControlID + '_txtCodigoCentroCosto_S_'+Constante);
			    var ltxtOrdenServicio=document.all(ControlID + '_txtCodOrdenServicio_S_'+Constante);
			    var ltxtDesServicio=document.all(ControlID + '_txtDesServicio'+Constante);
			    var lstrCodigosErroneos='';
			    /////////////////////
			    if(ltxtGrupo.value=='')
			    {
				    alert('El código de grupo de servicio no es valido.');
				    ltxtGrupo.focus()
				    return false;
			    }//end if
			    if(ltxtTipo.value=='')
			    {
				    alert('El código de tipo de servicio no es valido.');
				    ltxtTipo.focus()
				    return false;
			    }//end if
			    /////////////////////
			    if(ltxtDesServicio.value=='')
			    {
				    alert('La descripción del servicio no es valido.');
				    ltxtDesServicio.focus()
				    return false;
			    }//end if
			    if(ltxtDesServicio.value.indexOf('&')>=0)
			    {
				    alert('El caracter -- & -- en la descripción del servicio no es valido.');
				    ltxtDesServicio.focus()
				    return false;
			    }//end if
			    /////////////////////
			    if(ltxtCantidad.value=='' || isVarType(ltxtCantidad.value,'NUM')==false)
			    {
				    alert('La cantidad ingresada no es valida.');
				    ltxtCantidad.focus()
				    return false;
			    }//end if	
			    if(parseInt(ltxtCantidad.value)<=0)
			    {
				    alert('La cantidad ingresada debe ser mayor a cero.');
				    ltxtCantidad.focus()
				    return false;
			    }//end if	
			    ///////////////////////////////
			    var ldtbResultado =LOG20001.fnc_ValidarCodigos('','',ltxtGrupo.value,ltxtTipo.value, ltxtCentroCosto.value, ltxtOrdenServicio.value, '');		
			    if(ldtbResultado!=null && typeof(ldtbResultado)=="object")
			    {
				    var ldtbResultadoOk=ldtbResultado.value;
				    if(ldtbResultadoOk.Rows[0].gruposervicio_des=='')
				    {
					    lstrCodigosErroneos=lstrCodigosErroneos+'El grupo de servicio -- '+ldtbResultadoOk.Rows[0].gruposervicio_cod+' -- no existe.\n';
				    }//end if
				    if(ldtbResultadoOk.Rows[0].centrocosto_des=='' && ltxtCentroCosto.value!='')
				    {
					    lstrCodigosErroneos=lstrCodigosErroneos+'El centro de costo -- '+ldtbResultadoOk.Rows[0].centrocosto_cod+' -- no existe.\n';
				    }//end if
				    if(ldtbResultadoOk.Rows[0].ordenservicio_des=='' && ltxtOrdenServicio.value!='')
				    {
					    lstrCodigosErroneos=lstrCodigosErroneos+'La orden de servicio -- '+ldtbResultadoOk.Rows[0].ordenservicio_cod+' -- no existe.\n';
				    }//end if
				    if(lstrCodigosErroneos!='')
				    {
					    alert(lstrCodigosErroneos);
					    return false;
				    }//end if
			    }
			    else
			    {
				    alert('No se pudó validar los códigos ingresados.');
				    return false;
			    }//end if
			    ///////////////////////////////
			    return true;
			    ///////////////////////////////
		    }//end function
		
		    function fnc_ConfirmarAnulacion()
		    {
			    var lstrMensaje='Se anulará la requisición -- '+document.all('txtSerie').value+'-'+document.all('txtNumero').value+' --.\n\n¿Esta seguro de continuar?';
			    if(document.all('txtSerie').value != '' && document.all('txtNumero').value != '')
			    {
				    return confirm(lstrMensaje);
			    }
			    return false;
		    }//end function
//End-->
		</script>
	</head>
	<body>
		<form id="frmRequisicion" name="frmRequisicion" method="post" runat="Server">
			<input id="hdnCodigo" style="WIDTH: 24px; HEIGHT: 22px" type="hidden" size="1" name="hdnCodigo"
				runat="server" /><input id="hdnAccion" style="WIDTH: 24px; HEIGHT: 22px" type="hidden" size="1" name="hdnAccion"
				runat="server" />
			<table id="Table4" cellspacing="2" cellpadding="2" width="720px" border="0">
				<tr>
					<td class="cabecera" style="HEIGHT: 22px">&nbsp;FORMULARIO DE REQUISICIÓN</td>
				</tr>
			</table>
            <table style="WIDTH: 720px; HEIGHT: 8px">
				<tr>
					<td class="Etiqueta" style="WIDTH: 103px" valign="middle">&nbsp;Tipo requisición :</td>
					<td style="WIDTH: 155px"><asp:dropdownlist id="ddlTipo" runat="server" Width="128px" AutoPostBack="True" CssClass="input">
							<asp:ListItem Value="ART" Selected="True">ARTICULOS</asp:ListItem>
							<asp:ListItem Value="SER">SERVICIOS</asp:ListItem>
						</asp:dropdownlist></td>
					<td class="etiqueta" style="WIDTH: 109px">&nbsp;Número requisición :</td>
					<td>&nbsp;<asp:textbox id="txtSerie" runat="server" Width="40px" CssClass="input" MaxLength="4"></asp:textbox>-<asp:textbox id="txtNumero" runat="server" Width="88px" AutoPostBack="True" CssClass="input"
							MaxLength="10"></asp:textbox>
                    </td>
				</tr>
            </table>
			<table id="Table23" style="WIDTH: 720px; HEIGHT: 8px">
				<tr>
					<td class="Etiqueta" style="WIDTH: 103px">&nbsp;Fecha creación:</td>
					<td style="WIDTH: 98px" width="98"><asp:textbox id="txtFecha" runat="server" Width="100px" CssClass="input"></asp:textbox></td>
					<td style="WIDTH: 50px">&nbsp; <img onclick="popUpCalendar(this, frmRequisicion.txtFecha, 'dd/mm/yyyy')" height="15"
							alt="Seleccionar fecha" src="../../intranet/Imagenes/Calendario.gif" width="13" border="0" />&nbsp;
					</td>
					<td class="etiqueta" style="WIDTH: 108px">&nbsp;Situación :</td>
					<td>&nbsp;<asp:textbox id="txtSituacion" runat="server" Width="134px" CssClass="input" MaxLength="10"></asp:textbox></td>
				</tr>
			</table>
			<table style="WIDTH: 720px; HEIGHT: 48px">
				<tr>
					<td class="Etiqueta" style="WIDTH: 103px">&nbsp;Área solicitante :</td>
					<td style="WIDTH: 94px" width="94"><asp:textbox id="txtAreaSolicitanteCodigo" runat="server" Width="100px" CssClass="input"></asp:textbox></td>
					<td style="WIDTH: 49px; HEIGHT: 8px" width="49"><input class="boton" id="btnAreaSolicitante" style="WIDTH: 20px; HEIGHT: 20px" onclick="javascript:ListarCentroCosto('','A');"
							type="button" value="..." name="Button1" /></td>
					<td style="HEIGHT: 8px" colspan="2"><asp:textbox id="txtAreaSolicitanteNombre" runat="server" Width="360px" CssClass="inputDisabled2" MaxLength="10"></asp:textbox></td>
				</tr>
				<tr id="Stock" runat="server">
					<td class="Etiqueta" style="WIDTH: 97px; HEIGHT: 2px">&nbsp;Motivo de&nbsp;stock :</td>
					<td style="WIDTH: 94px; HEIGHT: 2px" width="94"><asp:checkbox id="CheckStock" runat="server" Width="96px" CssClass="Input"></asp:checkbox></td>
					<td style="WIDTH: 49px; HEIGHT: 2px" width="49">&nbsp;</td>
					<td style="HEIGHT: 2px" colspan="2"></td>
				</tr>
				<tr id="CuentaGasto" runat="server">
					<td class="Etiqueta" style="WIDTH: 97px">&nbsp;Cuenta de gasto :</td>
					<td style="WIDTH: 94px" width="94"><asp:textbox id="txtCtaGasto" runat="server" Width="100px" CssClass="input"></asp:textbox></td>
					<td style="WIDTH: 49px" width="49"><input class="boton" id="cmdCtaGasto" style="WIDTH: 20px; HEIGHT: 20px" onclick="javascript:BuscarCtaGasto('','');"
							type="button" value="..." /></td>
					<td colspan="2"><asp:textbox id="txtDescCtaGasto" runat="server" Width="360px" CssClass="inputDisabled2" MaxLength="10"></asp:textbox></td>
				</tr>
			</table>
			<table style="WIDTH: 720px; HEIGHT: 96px">
				<tr>
					<td class="Etiqueta" style="WIDTH: 103px; HEIGHT: 23px">&nbsp;Almacén destino :</td>
					<td style="HEIGHT: 23px"><asp:dropdownlist id="cmbAlmacen" runat="server" Width="272px" CssClass="input"></asp:dropdownlist></td>
				</tr>
				<tr>
					<td class="Etiqueta" style="WIDTH: 95px" valign="middle">&nbsp;Observaciones :</td>
					<td style="HEIGHT: 40px" colspan="4"><asp:textbox id="txtObservacion" runat="server" Width="520px" CssClass="input" MaxLength="200"
							Height="56px" TextMode="MultiLine"></asp:textbox></td>
				</tr>
				<tr>
					<td colspan="5"><asp:button id="btnGrabar" runat="server" CssClass="Boton" Text="Grabar"></asp:button>&nbsp;&nbsp;<asp:button id="btnSolicitar" runat="server" Width="120px" CssClass="Boton" Text="Solicitar Aprobacion"></asp:button>&nbsp;&nbsp;
						<asp:button id="btnAnular" runat="server" CssClass="Boton" Text="Anular" CausesValidation="False"></asp:button>&nbsp;
						<asp:button id="BtnNuevo" runat="server" CssClass="Boton" Text=" Nuevo "></asp:button><input class="Input" id="hdnAprobacion" style="WIDTH: 32px; HEIGHT: 13px" type="hidden"
							size="1" runat="server" name="hdnAprobacion"><input class="Input" id="hdnAreaSolicitante" style="WIDTH: 32px; HEIGHT: 14px" type="hidden"
							size="1" name="Hidden1" runat="server" /></td>
				</tr>
			</table>
			<table>
				<tr>
					<td><asp:datagrid id="DataGrid1" runat="server" Width="928px" ShowFooter="True" AutoGenerateColumns="False">
							<FooterStyle CssClass="GridFooter"></FooterStyle>
							<AlternatingItemStyle CssClass="GridAlternateItem"></AlternatingItemStyle>
							<ItemStyle CssClass="GridItem"></ItemStyle>
							<HeaderStyle CssClass="GridHeader"></HeaderStyle>
							<Columns>
								<asp:TemplateColumn HeaderText="Articulo">
									<HeaderStyle HorizontalAlign="Left" Width="450px"></HeaderStyle>
									<HeaderTemplate>
										<table cellspacing="1" cellpadding="1" width="100%" border="0">
											<tr>
												<td class="GridHeader" width="20">
													<asp:Panel id="Panel27" runat="server" Width="50px">Secuencia </asp:Panel></td>
												<td class="GridHeader" width="100">
													<asp:Panel id="Panel34" runat="server" Width="120px">Código</asp:Panel></td>
												<td class="GridHeader" width="100%">
													<asp:Panel id="Panel38" runat="server" Width="120px">Descripción</asp:Panel></td>
											</tr>
										</table>
									</HeaderTemplate>
									<ItemTemplate>
										<table id="Table1" cellspacing="1" cellpadding="1" width="100%" border="0">
											<tr>
												<td width="20">
													<asp:Panel id="Panel33" runat="server" Width="50px">
														<asp:Label id="lblNumeroSecuencia" runat="server" CssClass="InputCentro" text='<%#Container.DataItem("NU_SECU")%>'>
														</asp:Label>
													</asp:Panel></td>
												<td width="100">
													<asp:Panel id="Panel35" runat="server" Width="120px">
														<asp:LinkButton id="lbtAdjuntosArticulo" runat="server">
															<asp:Label id="lblCodArticulo" runat="server" CssClass="input" Width="100%" text='<%#Container.DataItem("CO_ITEM")%>'>
															</asp:Label>
														</asp:LinkButton>
													</asp:Panel></td>
												<td width="100%">
													<asp:Panel id="Panel36" runat="server" Width="100%">
														<asp:Label id="lblArticulo" runat="server" CssClass="input" Width="100%" text='<%#Container.DataItem("DE_ITEM")%>'>
														</asp:Label>
													</asp:Panel></td>
											</tr>
										</table>
									</ItemTemplate>
									<FooterTemplate>
										<table id="Table3" style="HEIGHT: 20px" cellspacing="1" cellpadding="1" width="100%" border="0">
											<tr>
												<td width="100">
													<asp:Panel id="Panel37" runat="server" Width="50px"></asp:Panel></td>
												<td width="120">
													<asp:TextBox id="txtCodArticuloF" runat="server" CssClass="input" Width="100px" ></asp:TextBox><input class="Boton" id="btnArticuloF" style="WIDTH: 20px; HEIGHT: 20px" type="button"
														value="..." name="btnArticuloF" runat="server" /></td>
												<td width="100%">
													<asp:Label id="lblArticuloF" runat="server" Width="100%"></asp:Label></td>
											</tr>
										</table>
									</FooterTemplate>
									<EditItemTemplate>
										<table id="Table2" cellspacing="1" cellpadding="1" width="100%" border="0">
											<tr>
												<td width="20">
													<asp:Label id="lblNumeroSecuenciaE" runat="server" CssClass="InputCentro" text='<%#Container.DataItem("NU_SECU")%>'>
													</asp:Label></td>
												<td width="80">
													<asp:TextBox id="txtCodArticuloE" runat="server" CssClass="input" Width="90px"  text='<%#Container.DataItem("CO_ITEM")%>'>
													</asp:TextBox></td>
												<td width="20"><input class="boton" id="btnArticuloE" style="WIDTH: 19px; HEIGHT: 19px" type="button"
														size="20" value="..." name="btnArticuloE" runat="server" /></td>
												<td width="300">
													<asp:Label id="lblArticuloE" runat="server" CssClass="input" Width="100%" text='<%#Container.DataItem("DE_ITEM")%>'>
													</asp:Label></td>
											</tr>
										</table>
									</EditItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="Centro Costo">
									<HeaderTemplate>
										<table id="Table30" style="WIDTH: 65px; HEIGHT: 28px" cellspacing="1" cellpadding="1" width="113"
											border="0">
											<tr>
												<td class="GridHeader" width="80">
													<asp:Panel id="Panel1" runat="server" Width="80px">Centro Costo</asp:Panel>
												</td>
											</tr>
										</table>
									</HeaderTemplate>
									<ItemTemplate>
										<table id="Table22" style="WIDTH: 65px; HEIGHT: 9px" cellspacing="1" cellpadding="1" width="114"
											border="0">
											<tr>
												<td width="80">
													<asp:Panel id="Panel31" runat="server" Width="80px">
														<asp:Label id="LblCodCentroCosto" runat="server" CssClass="input" text='<%#Container.DataItem("CO_AUXI_EMPR")%>'>
														</asp:Label>
													</asp:Panel></td>
											</tr>
										</table>
									</ItemTemplate>
									<FooterTemplate>
										<table id="Table20" style="WIDTH: 17px; HEIGHT: 24px" cellspacing="1" cellpadding="1" width="17"
											border="0">
											<tr>
												<td width="80">
													<asp:TextBox id="txtCodigoCentroCostoF" runat="server" CssClass="Input" Width="65px"></asp:TextBox></td>
												<td><input class="Boton" id="btnCentroCostoF" style="WIDTH: 20px; HEIGHT: 20px" type="button"
														value="..." name="btnCentroCostoF" runat="server" /></td>
											</tr>
										</table>
									</FooterTemplate>
									<EditItemTemplate>
										<table id="Table18" style="WIDTH: 1px; HEIGHT: 24px" cellspacing="1" cellpadding="1" width="1"
											border="0">
											<tr>
												<td>
													<asp:TextBox id="txtCodigoCentroCostoE" runat="server" CssClass="Input" Width="65px" Text='<%# Container.DataItem("co_auxi_empr") %>'>
													</asp:TextBox></td>
												<td><input class="Boton" id="btnCentroCostoE" style="WIDTH: 20px; HEIGHT: 20px" type="button"
														value="..." name="btnCentroCostoE" runat="server" /></td>
											</tr>
										</table>
									</EditItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="Orden Servicio">
									<HeaderTemplate>
										<table id="Table31" style="WIDTH: 105px; HEIGHT: 28px" cellspacing="1" cellpadding="1"
											width="105" border="0">
											<tr>
												<td class="GridHeader">
													<asp:Panel id="Panel24" runat="server" Width="80px">Órden servicio</asp:Panel>
												</td>
											</tr>
										</table>
									</HeaderTemplate>
									<ItemTemplate>
										<table id="Table13" cellspacing="1" cellpadding="1" border="0">
											<tr>
												<td>
													<asp:Panel id="Panel29" runat="server" Width="80px">
														<asp:Label id="lblOrdenServicio" runat="server" CssClass="input" text='<%#Container.DataItem("CO_ORDE_SERV")%>'>
														</asp:Label>
													</asp:Panel></td>
											</tr>
										</table>
									</ItemTemplate>
									<FooterTemplate>
										<table id="Table17" style="WIDTH: 9px; HEIGHT: 24px" cellspacing="1" cellpadding="1" width="9"
											border="0">
											<tr>
												<td>
													<asp:TextBox id="txtCodOrdenServicioF" runat="server" CssClass="Input" Width="75px"></asp:TextBox></td>
												<td><input class="Boton" id="BtnOrdenServicioF" style="WIDTH: 20px; HEIGHT: 20px" type="button"
														value="..." name="BtnOrdenServicioF" runat="server" /></td>
											</tr>
										</table>
									</FooterTemplate>
									<EditItemTemplate>
										<table id="Table14" style="WIDTH: 57px; HEIGHT: 24px" cellspacing="1" cellpadding="1" width="57"
											border="0">
											<tr>
												<td>
													<asp:TextBox id="txtCodOrdenServicioE" runat="server" CssClass="input" Width="75px" text='<%#Container.DataItem("CO_ORDE_SERV")%>'>
													</asp:TextBox></td>
												<td><input class="Boton" id="BtnOrdenServicioE" style="WIDTH: 20px; HEIGHT: 20px" type="button"
														value="..." name="BtnOrdenServicioE" runat="server" /></td>
											</tr>
										</table>
									</EditItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="Cta. Gasto">
									<HeaderStyle HorizontalAlign="Center" Width="80px"></HeaderStyle>
									<HeaderTemplate>
										<table id="Table6" style="HEIGHT: 28px" cellspacing="1" cellpadding="1" width="105" border="0">
											<tr>
												<td class="GridHeader">
													<asp:Panel id="Panel23" runat="server" Width="80px">Cuenta gasto</asp:Panel></td>
											</tr>
										</table>
									</HeaderTemplate>
									<ItemTemplate>
										<table id="Table7" style="WIDTH: 105px; HEIGHT: 8px" cellspacing="1" cellpadding="1" width="105"
											border="0">
											<tr>
												<td>
													<asp:Panel id="Panel25" runat="server" Width="80px">
														<asp:Label id="LblCodigoCtaGasto" runat="server" CssClass="input" Width="73px" text='<%#Container.DataItem("CO_DEST_FINA")%>'>
														</asp:Label>
													</asp:Panel></td>
											</tr>
										</table>
									</ItemTemplate>
									<FooterTemplate>
										<table id="Table5" style="WIDTH: 65px; HEIGHT: 24px" cellspacing="1" cellpadding="1" width="65"
											border="0">
											<tr>
												<td>
													<asp:TextBox id="txtCtaGastoSF" runat="server" CssClass="input" Width="75px" ></asp:TextBox></td>
												<td><input class="boton" id="btnCtaGastoF" style="WIDTH: 20px; HEIGHT: 20px" type="button"
														value="..." name="btnCtaGastoF" runat="server" /></td>
											</tr>
										</table>
									</FooterTemplate>
									<EditItemTemplate>
										<table id="Table9" style="WIDTH: 105px; HEIGHT: 24px" cellspacing="1" cellpadding="1" width="73"
											border="0">
											<tr>
												<td>
													<asp:TextBox id="txtCtaGastoSE" runat="server" CssClass="input" Width="75px"  text='<%#Container.DataItem("CO_DEST_FINA")%>'>
													</asp:TextBox></td>
												<td><input class="boton" id="btnCtaGastoE" style="WIDTH: 20px; HEIGHT: 20px" type="button"
														value="..." runat="server" name="btnCtaGastoE" /></td>
											</tr>
										</table>
									</EditItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="U.M.">
									<HeaderStyle HorizontalAlign="Center" Width="20px"></HeaderStyle>
									<ItemTemplate>
										<asp:Label id="lblUnidadM" runat="server" CssClass="InputCentro" Width="20px" text='<%#Container.DataItem("co_unme")%>'>
										</asp:Label>
									</ItemTemplate>
									<FooterTemplate>
										<asp:Label id="lblUnidadMF" runat="server" CssClass="InputCentro" Width="20px"></asp:Label>
									</FooterTemplate>
									<EditItemTemplate>
										<asp:Label id="lblUnidadME" runat="server" CssClass="InputCentro" Width="20px" text='<%#Container.DataItem("co_unme")%>'>
										</asp:Label>
									</EditItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="Cantidad">
									<HeaderStyle HorizontalAlign="Center" Width="70px"></HeaderStyle>
									<ItemTemplate>
										<asp:Label id="LblCantidad" runat="server" CssClass="InputDerecha" Width="100%" text='<%#Format(Container.DataItem("CA_SOLI"),"0.00")%>'>
										</asp:Label>
									</ItemTemplate>
									<FooterTemplate>
										<asp:TextBox id="txtCantidadF" runat="server" CssClass="InputDerecha" Width="100%"></asp:TextBox>
									</FooterTemplate>
									<EditItemTemplate>
										<asp:TextBox id="txtCantidadE" runat="server" CssClass="InputDerecha" Width="100%" text='<%#Container.DataItem("CA_SOLI")%>'>
										</asp:TextBox>
									</EditItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn>
									<HeaderStyle Width="40px"></HeaderStyle>
									<ItemTemplate>
										<asp:ImageButton id="btnEdit" runat="server" CommandName="Edit" ImageUrl="../../intranet/Imagenes/Editar.gif"></asp:ImageButton>
										<asp:ImageButton id="btnDelete" runat="server" CommandName="Delete" ImageUrl="../../intranet/Imagenes/borrador.gif"></asp:ImageButton>
									</ItemTemplate>
									<FooterTemplate>
										<asp:ImageButton id="btnAdd" runat="server" CommandName="Add" ImageUrl="../../intranet/Imagenes/Grabar.gif"></asp:ImageButton>
									</FooterTemplate>
									<EditItemTemplate>
										<asp:ImageButton id="btnUpdate" runat="server" CommandName="Update" ImageUrl="../../intranet/Imagenes/Grabar.gif"></asp:ImageButton>
										<asp:ImageButton id="btnCancel" runat="server" CommandName="Cancel" ImageUrl="../../intranet/Imagenes/Cancelar.gif"></asp:ImageButton>
									</EditItemTemplate>
								</asp:TemplateColumn>
							</Columns>
						</asp:datagrid><asp:datagrid id="DataGrid2" runat="server" Width="928px" ShowFooter="True" AutoGenerateColumns="False">
							<AlternatingItemStyle CssClass="GridAlternateItem"></AlternatingItemStyle>
							<ItemStyle CssClass="GridItem"></ItemStyle>
							<HeaderStyle CssClass="GridHeader"></HeaderStyle>
							<Columns>
								<asp:TemplateColumn HeaderText="Secuencia">
									<HeaderStyle Width="20px"></HeaderStyle>
									<ItemTemplate>
										<asp:Label id="LblSecuenciaS" runat="server" CssClass="input" Width="20px" text='<%#Container.DataItem("NU_SECU")%>'>
										</asp:Label>
									</ItemTemplate>
									<EditItemTemplate>
										<asp:Label id="LblSecuenciaSE" runat="server" CssClass="input" Width="20px" text='<%#Container.DataItem("NU_SECU")%>'>
										</asp:Label>
									</EditItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="Grpo. Servicio">
									<HeaderStyle Width="80px"></HeaderStyle>
									<HeaderTemplate>
										<table id="Table8" style="WIDTH: 145px; HEIGHT: 28px" cellspacing="1" cellpadding="1" width="145"
											border="0">
											<tr>
												<td class="GridHeader" width="40">
													<asp:Panel id="Panel17" runat="server" Width="40px">Grupo</asp:Panel></td>
												<td class="GridHeader" width="40">
													<asp:Panel id="Panel18" runat="server" Width="68px">Tipo Servicio</asp:Panel></td>
											</tr>
										</table>
									</HeaderTemplate>
									<ItemTemplate>
										<table id="Table11" cellspacing="1" cellpadding="1" width="100%" border="0">
											<tr>
												<td width="40">
													<asp:Panel id="Panel20" runat="server" Width="40px">
														<asp:Label id="LblGrupoServicio" runat="server" CssClass="input" Width="20px" text='<%#Container.DataItem("CO_GRUP_SERV")%>'>
														</asp:Label>
													</asp:Panel></td>
												<td width="40">
													<asp:Panel id="Panel21" runat="server" Width="60px">
														<asp:Label id="LblTipoServicio" runat="server" CssClass="input" Width="20px" text='<%#Container.DataItem("CO_SERV")%>'>
														</asp:Label>
													</asp:Panel></td>
											</tr>
										</table>
									</ItemTemplate>
									<FooterTemplate>
										<table id="Table10" style="WIDTH: 129px; HEIGHT: 24px" cellspacing="1" cellpadding="1"
											width="129" border="0">
											<tr>
												<td width="40">
													<asp:TextBox id="txtGrupoServF" runat="server" CssClass="input" Width="40px" ></asp:TextBox></td>
												<td width="40">
													<asp:TextBox id="txtTipoServF" runat="server" CssClass="input" Width="40px"></asp:TextBox></td>
												<td width="20"><input class="Boton" id="BtnGrupoSerF" style="WIDTH: 20px; HEIGHT: 20px" type="button"
														value="..." runat="server" name="BtnGrupoSerF" /></td>
											</tr>
										</table>
									</FooterTemplate>
									<EditItemTemplate>
										<table id="Table16" style="WIDTH: 129px; HEIGHT: 24px" cellspacing="1" cellpadding="1"
											width="129" border="0">
											<tr>
												<td width="30">
													<asp:TextBox id="txtGrupoServE" runat="server" CssClass="input" Width="40px"  text='<%#Container.DataItem("CO_GRUP_SERV")%>'>
													</asp:TextBox></td>
												<td width="30">
													<asp:TextBox id="txtTipoServE" runat="server" CssClass="input" Width="40px" text='<%#Container.DataItem("CO_SERV")%>'>
													</asp:TextBox></td>
												<td width="20"><input class="Boton" id="BtnGrupoSerE" style="WIDTH: 20px; HEIGHT: 20px" type="button"
														value="..." runat="server" name="BtnGrupoSerE" /></td>
											</tr>
										</table>
									</EditItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="Descripcion">
									<HeaderStyle Width="800px"></HeaderStyle>
									<HeaderTemplate>
										<table id="Table15" cellspacing="1" cellpadding="1" width="100%" border="0">
											<tr>
												<td class="GridHeader">Descripcion de Servicio</td>
											</tr>
										</table>
									</HeaderTemplate>
									<ItemTemplate>
										<asp:Label id="LblDescServicio" runat="server" CssClass="input" Width="100%" text='<%#Container.DataItem("DE_ITEM")%>'>
										</asp:Label>
									</ItemTemplate>
									<FooterTemplate>
										<asp:TextBox id="txtDesServicioF" runat="server" CssClass="Input" Width="100%" MaxLength="200"
											TextMode="MultiLine"></asp:TextBox>
									</FooterTemplate>
									<EditItemTemplate>
										<asp:TextBox id="txtDesServicioE" runat="server" CssClass="Input" Width="100%" MaxLength="200" TextMode="MultiLine" text='<%#Container.DataItem("DE_ITEM")%>'>
										</asp:TextBox>
									</EditItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="Centro Costo">
									<HeaderStyle Width="80px"></HeaderStyle>
									<HeaderTemplate>
										<table id="Table19" style="WIDTH: 73px; HEIGHT: 28px" cellspacing="1" cellpadding="1" width="73"
											border="0">
											<tr>
												<td class="GridHeader" width="80">
													<asp:Panel id="Panel11" runat="server" Width="80px">
														<P>Centro costo</P>
													</asp:Panel></td>
											</tr>
										</table>
									</HeaderTemplate>
									<ItemTemplate>
										<table id="Table28" style="WIDTH: 65px; HEIGHT: 9px" cellspacing="1" cellpadding="1" width="144"
											border="0">
											<tr>
												<td width="80">
													<asp:Panel id="Panel15" runat="server" Width="80px">
														<asp:Label id="LblCodigoCentroCosto" runat="server" CssClass="input" Width="80px" text='<%#Container.DataItem("CO_AUXI_EMPR")%>'>
														</asp:Label>
													</asp:Panel></td>
											</tr>
										</table>
									</ItemTemplate>
									<FooterTemplate>
										<table id="Table26" style="WIDTH: 65px; HEIGHT: 24px" cellspacing="1" cellpadding="1" width="65"
											border="0">
											<tr>
												<td width="80">
													<asp:TextBox id="txtCodigoCentroCosto_S_F" runat="server" CssClass="Input" Width="65px"></asp:TextBox></td>
												<td><input class="Boton" id="BtnCentroCostoSF" style="WIDTH: 20px; HEIGHT: 20px" type="button"
														value="..." runat="server" name="BtnCentroCostoSF" /></td>
											</tr>
										</table>
									</FooterTemplate>
									<EditItemTemplate>
										<table id="Table29" style="WIDTH: 57px; HEIGHT: 24px" cellspacing="1" cellpadding="1" width="57"
											border="0">
											<tr>
												<td>
													<asp:TextBox id="txtCodigoCentroCosto_S_E" runat="server" CssClass="Input" Width="65px" text='<%#Container.DataItem("CO_AUXI_EMPR")%>'>
													</asp:TextBox></td>
												<td><input class="Boton" id="BtnCentroCostoSE" style="WIDTH: 20px; HEIGHT: 20px" type="button"
														value="..." runat="server" name="BtnCentroCostoSE" /></td>
											</tr>
										</table>
									</EditItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="Orden Servicio">
									<HeaderStyle Width="80px"></HeaderStyle>
									<HeaderTemplate>
										<table id="Table27" cellspacing="1" cellpadding="1" width="100%" border="0">
											<tr>
												<td class="GridHeader">
													<asp:Panel id="Panel2" runat="server" Width="80px">Órden servicio</asp:Panel></td>
												<td class="GridHeader" width="100%"></td>
											</tr>
										</table>
									</HeaderTemplate>
									<ItemTemplate>
										<table id="Table21" cellspacing="1" cellpadding="1" width="100%" border="0">
											<tr>
												<td>
													<asp:Panel id="Panel9" runat="server" Width="80px">
														<asp:Label id="Label1" runat="server" CssClass="input" Width="100%" text='<%#Container.DataItem("CO_ORDE_SERV")%>'>
														</asp:Label>
													</asp:Panel></td>
												<td width="100%"></td>
											</tr>
										</table>
									</ItemTemplate>
									<FooterTemplate>
										<table id="Table12" style="WIDTH: 89px; HEIGHT: 24px" cellspacing="1" cellpadding="1" width="89"
											border="0">
											<tr>
												<td>
													<asp:TextBox id="txtCodOrdenServicio_S_F" runat="server" CssClass="Input" Width="75px"></asp:TextBox></td>
												<td><input class="Boton" id="BtnOrdenServF" style="WIDTH: 20px; HEIGHT: 20px" type="button"
														value="..." name="Button1" runat="server" /></td>
											</tr>
										</table>
									</FooterTemplate>
									<EditItemTemplate>
										<table id="Table25" style="WIDTH: 65px; HEIGHT: 24px" cellspacing="1" cellpadding="1" width="65"
											border="0">
											<tr>
												<td>
													<asp:TextBox id="txtCodOrdenServicio_S_E" runat="server" CssClass="input" Width="75px" text='<%#Container.DataItem("CO_ORDE_SERV")%>'>
													</asp:TextBox></td>
												<td><input class="Boton" id="BtnOrdenServE" style="WIDTH: 20px; HEIGHT: 20px" type="button"
														value="..." name="Button2" runat="server" /></td>
											</tr>
										</table>
									</EditItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="Cantidad">
									<HeaderStyle HorizontalAlign="Center" Width="80px"></HeaderStyle>
									<ItemTemplate>
										<asp:Label id="LblCantidadS" runat="server" CssClass="InputDerecha" Width="100%" text='<%#Format(Container.DataItem("CA_SOLI"),"0.00")%>'>
										</asp:Label>
									</ItemTemplate>
									<FooterTemplate>
										<asp:TextBox id="txtCantidadSF" runat="server" CssClass="InputDerecha" Width="100%"></asp:TextBox>
									</FooterTemplate>
									<EditItemTemplate>
										<asp:TextBox id="txtCantidadSE" runat="server" CssClass="InputDerecha" Width="100%" text='<%#Container.DataItem("CA_SOLI")%>'>
										</asp:TextBox>
									</EditItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn>
									<HeaderStyle Width="80px"></HeaderStyle>
									<ItemTemplate>
										<asp:ImageButton id="BtnEditS" runat="server" CommandName="Editar" ImageUrl="../../intranet/Imagenes/Editar.gif"></asp:ImageButton>
										<asp:ImageButton id="btnEliminarS" runat="server" CommandName="Eliminar" ImageUrl="../../intranet/Imagenes/borrador.gif"></asp:ImageButton>
									</ItemTemplate>
									<FooterTemplate>
										<asp:ImageButton id="btnAdicionaS" runat="server" CommandName="Add" ImageUrl="../../intranet/Imagenes/Grabar.gif"></asp:ImageButton>
									</FooterTemplate>
									<EditItemTemplate>
										<asp:ImageButton id="btnGuardarS" runat="server" CommandName="Grabar" ImageUrl="../../intranet/Imagenes/Grabar.gif"></asp:ImageButton>
										<asp:ImageButton id="btnCancelarS" runat="server" CommandName="Cancel" ImageUrl="../../intranet/Imagenes/Cancelar.gif"></asp:ImageButton>
									</EditItemTemplate>
								</asp:TemplateColumn>
							</Columns>
						</asp:datagrid></td>
				</tr>
			</table>
			<input id="HDN2" style="WIDTH: 32px; HEIGHT: 22px" type="hidden" size="1" name="HDN2" runat="server" />
			<input id="HDN1" style="WIDTH: 32px; HEIGHT: 22px" type="hidden" size="1" name="HDN1" runat="server" />
			<input id="HDNArticulo" style="WIDTH: 32px; HEIGHT: 22px" type="hidden" size="1" name="HDNArticulo" runat="server" />
            <input id="HDNUnidadMedida" style="WIDTH: 32px; HEIGHT: 22px" type="hidden" size="1" name="HDNUnidadMedida" runat="server" />
            <input id="HDNCtaGasto" style="WIDTH: 32px; HEIGHT: 22px" type="hidden" size="1" name="HDNCtaGasto" runat="server" />
            <input id="HdnDescServicio" style="WIDTH: 32px; HEIGHT: 22px" type="hidden" size="1" name="HdnDescServicio" runat="server" />
        </form>
	</body>
</html>
