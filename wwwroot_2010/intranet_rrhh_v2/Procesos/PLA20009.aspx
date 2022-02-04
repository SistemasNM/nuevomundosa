<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="PLA20009.aspx.vb" Inherits="intranet_rrhh_v2.PLA20009" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <meta content="Microsoft Visual Studio.NET 7.0" name="GENERATOR" />
	<meta content="Visual Basic 7.0" name="CODE_LANGUAGE" />
    <meta http-equiv="X-UA-Compatible" content="IE=8"> 
	<meta content="JavaScript" name="vs_defaultClientScript" />
	<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema" />
    <%--<link href="../../intranet/Estilos/NM0001.css" type="text/css" rel="stylesheet" />--%>
    <link href="../Styles/NM0001.css" rel="stylesheet" type="text/css"/>
	<!--<script language="javascript" src="../../intranet/JS/jsCalendario_N4.js"></script>-->
	<%--<script type="text/javascript" language="javascript" src="../../intranet/JS/jsCalendario_N3.js"></script>--%>
    <script type="text/javascript" language="javascript" src="../Scripts/jsCalendario_N3.js"></script>
    <script type="text/javascript" language="javascript" src="../Scripts/functions.js"></script>
	<%--<script type="text/javascript" language="javascript" src="../../intranet/JS/functions.js"></script>--%>
	<%--<script type="text/javascript" language="javascript" src="../../intranet/JS/jsDesTabla.js"></script>--%>
    <script type="text/javascript" language="javascript" src="../Scripts/jsDesTabla.js"></script>
	<script type="text/javascript" language="JavaScript">
<!--Begin

	    function fnc_mostrardetalle(pstrcodigo) {
	        var retorno = window.showModalDialog("PLA20011.aspx?pstrCodigo=" + pstrcodigo, "", "dialogHeight:400px;dialogWidth:840px;center:yes;help:no;");
	        //modalWin
	        retorno.close();
	        retorno = null;
	        return false;
	    } //end function


	    function modalWin(purl) {
	        if (window.showModalDialog) {
	            window.showModalDialog(purl, "name", "dialogWidth:255px;dialogHeight:250px");
	        } else {
	            window.open(purl, "name", "height=255,width=250,toolbar=no,directories=no,status=no, menubar=no,scrollbars=no,resizable=no ,modal=yes");
	        } //end if
	    } //end function

	    function fnc_areasolicitante(pobjcod, pobjdes) {
	        g_RutaDirecto = 1; //opcion para que ejecute de otra aplicacion
	        fdesAreaRRHH2(pobjcod, pobjdes);
	    } //end function

	    function SolicitarAprobacion() {
	        //document.all[document.all["HDN4"].value+"_chkSelI"].value
	        var lint_inex = 0;
	        var ltxt_sele = document.getElementById("HDN5");
	        ltxt_sele.value = "";
	        for (var lint_fila = 0; lint_fila < 1000; lint_fila++) {
	            //validar inexistentes
	            if (lint_inex > 3) {
	                break;
	            } //end if
	            var lchksel = document.getElementById(document.all["HDN4"].value + "chkSelI_" + lint_fila);
	            var llblest = document.getElementById(document.all["HDN4"].value + "lblSituI_" + lint_fila);
	            var lhdncod = document.getElementById(document.all["HDN4"].value + "hdnCodigoI_" + lint_fila);
	            if (lchksel != null) {
	                //alert(lhdncod.value);
	                if (lchksel.checked == true && lhdncod.value != "") {
	                    if (llblest.innerHTML == "ACT") {
	                        ltxt_sele.value = ltxt_sele.value + lhdncod.value + "|";
	                    } else {
	                        alert('Ha seleccionado registros con estados no permitidos.');
	                        return false;
	                    } //end if
	                } //end if
	                lint_inex = 0;
	            } else {
	                lint_inex = lint_inex + 1;
	            } //end if
	        } //end for

	        //verificar si hay seleccionados
	        if (ltxt_sele.value != "") {
	            return confirm('¿ Desea enviar a aprobar las horas extras seleccionadas ?');
	        } else {
	            alert('No ha seleccionado ningun registro.');
	            return false;
	        } //end if
	        /*
	        document.frmRequisicion.hdnAprobacion.value='';
	        var retorno = window.showModalDialog("LOG20001_1.aspx","","dialogHeight:200px;dialogWidth:500px;center:yes;help:no;");
			
	        //////////////////////////////////////////
	        var retorno = window.showModalDialog("../../../buscadores/frmTiposAprobacion.aspx?strCodigo=&strNombre=","","dialogHeight:450px;dialogWidth:450px;center:yes;help:no;");
	        if (retorno!="" && retorno!=":")
	        {
	        var datos = retorno.split(":");
	        //txtCodigo.value = datos[0];
	        //lblNombre.innerHTML = datos[1];
	        //document.Form1.hdnTipo.value = datos[1];
	        document.frmRequisicion.hdnAprobacion.value=datos[0];
	        return true;
	        }//end if
	        return false;
	        */
	    } //end function

	    function txtTrabajador_onBlur(ControlID, Constante) {
	        var ltxttrab = document.all[ControlID + "_txtTrabajador" + Constante];
	        //<> de blanco y <> del último codigo buscado
	        if (trim(ltxttrab.value) != '') {
	            if (trim(ltxttrab.value).length == 5 && trim(ltxttrab.value) != document.all["HDN3"].value) {//buscar por AJAX al trabajador
	                var ldtbResultado = PLA20009.fnc_DatosTrabajador(trim(ltxttrab.value));
	                if (ldtbResultado != null && typeof (ldtbResultado) == "object") {
	                    var ldtbResultadoOk = ldtbResultado.value;
	                    if (ldtbResultadoOk.Rows.length > 0) {
	                        document.all[ControlID + "_lblTrabDes" + Constante].innerHTML = ldtbResultadoOk.Rows[0].var_NombreTrabajador;
	                        document.all["HDN3"].value = trim(ltxttrab.value);
	                        return true;
	                    } //end if					
	                } //end if
	                document.all["HDN3"].value = '';
	                document.all[ControlID + "_lblTrabDes" + Constante].innerHTML = '';
	            } //end if
	        } else {
	            ltxttrab.value = '';
	            document.all["HDN3"].value = '';
	            document.all[ControlID + "_lblTrabDes" + Constante].innerHTML = '';
	        } //end if
	    } //end function

	    function BuscarTrabajador(ControlID, Constante) {
	        var retorno = window.showModalDialog("../../intranet/Buscadores/PLA_0001.aspx?strTipo=EMP", "", "dialogHeight:370px;dialogWidth:490px;center:yes;help:no;");
	        //retorno debe traer el resultado de la busqueda
	        if (retorno != "::") {
	            var datos = retorno.split(":");
	            var Codigo = datos[0];
	            var Descripcion = datos[1];
	            if (Codigo == '') {
	                return false;
	            } //end if
	            switch (Constante) {
	                case "F":
	                    var txtTrabajador = document.all[ControlID + "_txtTrabajadorF"];
	                    var lblTrabajador = document.all[ControlID + "_lblTrabDesF"];
	                    txtTrabajador.value = datos[0];
	                    lblTrabajador.innerHTML = datos[1];
	                    break;
	                case "E":
	                    var txtTrabajador = document.all[ControlID + "_txtTrabajadorE"];
	                    var lblTrabajador = document.all[ControlID + "_lblTrabDesE"];
	                    txtTrabajador.value = datos[0];
	                    lblTrabajador.innerHTML = datos[1];
	                    break;
	            } //end switch
	            document.all["HDN3"].value = trim(txtTrabajador.value);
	        } //end if
	    } //end function


	    function ddlTarea_onFocus(ControlID, Constante) {
	        var lhdn2 = document.all["HDN2"]

	        //verificar el HDN2 si hay datos debe traer colocarlos y el focus enviar al cod_trabajador
	        if (lhdn2.value != '' && document.all[ControlID + "_txtTrabajadorF"].value == '') {
	            var larrdatos = lhdn2.value.split('|');

	            document.all[ControlID + "_ddlTareaF"].value = larrdatos[0];
	            document.all[ControlID + "_txtCtcF"].value = larrdatos[1];
	            document.all[ControlID + "_txtFechaF"].value = larrdatos[2];
	            document.all[ControlID + "_txtHESolF"].value = larrdatos[4];
	            document.all[ControlID + "_txtObservacionF"].value = larrdatos[5];

	            document.all[ControlID + "_txtTrabajadorF"].focus();
	        } //end if
	    } //end function

	    function popUp(strUrl) {
	        var intWidth = screen.width;
	        var intHeight = screen.height;
	        window.open(strUrl, "", "top=0; left=0; width=" + intWidth + "; height=" + intHeight + "; resizable=1;");
	    } //end funcion


	    function txtHESol_onBlur(ControlID, Constante) {
	        //validar que el numero sea un entero
	        var ltxtCantidad = document.all(ControlID + '_txtHESol' + Constante);
	        if (ltxtCantidad.value == '') {
	            ltxtCantidad.value = 0;
	            return;
	        } //end if
	        if (isVarType(ltxtCantidad.value, 'INT') == false) {
	            alert('La cantidad ingresada no es valida.');
	            ltxtCantidad.focus();
	            return;
	        } //end if	
	        //evaluar la cantidad de horas extras no debe ser mayor de 8 horas(ejemplo)
	        if (parseInt(ltxtCantidad.value) < 0 || parseInt(ltxtCantidad.value) > 16) {
	            alert('La cantidad ingresada no debe ser mayor de 8.');
	            ltxtCantidad.focus();
	        } //end if
	    } //end function			

	    function txtObservacion_onBlur(ControlID, Constante) {
	        //validar que el numero sea un entero
	        var ltxtObservacion = document.all(ControlID + '_txtObservacion' + Constante);
	        ltxtObservacion.value = ltxtObservacion.value.toUpperCase();
	    } //end function			

	    function fnc_Datagrid1_Validar(ControlID, Constante) {
	        //verificar horassol >0 y <9, si tarea=B => ctc<>''
	        var lddlTarea = document.all(ControlID + '_ddlTarea' + Constante);
	        var ltxtCtc = document.all(ControlID + '_txtCtc' + Constante);
	        var ltxtFecha = document.all(ControlID + '_txtFecha' + Constante);
	        var ltxtTrabajador = document.all(ControlID + '_txtTrabajador' + Constante);
	        var ltxtHESol = document.all(ControlID + '_txtHESol' + Constante);
	        var ltxtHDN6 = document.all('HDN6');

	        //horas solicitadas
	        if (trim(ltxtHESol.value) == '' || isVarType(ltxtHESol.value, 'INT') == false) {
	            alert('Las horas solicitadas no es valido.');
	            ltxtHESol.focus()
	            return false;
	        } //end if	
	        if (parseInt(ltxtHESol.value) <= 0 || parseInt(ltxtHESol.value) > 16) {
	            alert('Las horas solicitadas debe ser mayor a cero y menor a 8.');
	            ltxtHESol.focus()
	            return false;
	        } //end if	

	        //validar tarea
	        if (trim(lddlTarea.value) == '') {
	            alert('Debe seleccionar una tarea.');
	            lddlTarea.focus()
	            return false;
	        } //end if		
	        if (trim(lddlTarea.value) == 'B' && trim(ltxtCtc.value) == '') {
	            alert('Debe ingresar una CTC para esta tarea.');
	            ltxtCtc.focus()
	            return false;
	        } //end if

	        if (trim(ltxtTrabajador.value) == '') {
	            alert('Debe ingresar un trabajador.');
	            ltxtTrabajador.focus()
	            return false;
	        } //end if

	        //validar hora de inicio y ctc de retorno
	        var retorno = window.showModalDialog("PLA20009_1.aspx?pstrctc=" + ltxtCtc.value + "&pstrinicio=" + ltxtHDN6.value, "", "dialogHeight:170px;dialogWidth:480px;center:yes;help:no;");
	        //estado|inicio|ctc
	        //alert(retorno);
	        //if(retorno==null)
	        //{
	        //	alert('NULL--');
	        //}
	        if (retorno != null) {
	            var datos = retorno.split("|");
	            var estado = datos[0];
	            var inicio = datos[1];
	            var ctc = datos[2];
	            if (estado == '1') {
	                if (inicio != '') {
	                    ltxtHDN6.value = inicio;
	                    ltxtCtc.value = ctc;
	                } else {
	                    alert('Debe ingresar un hora de inicio aprox.');
	                    return false;
	                    //ltxtTrabajador.focus()
	                } //end if
	            } else {
	                return false;
	            } //end if
	        } else {
	            alert('Debe ingresar un hora de inicio aprox.');
	            return false;
	        } //end if

	        ///////////////////////////////
	        return true;
	        ///////////////////////////////
	    } //end function

	    function fnc_Eliminar(ControlID) {
	        var lstrMensaje = 'Se eliminará el registro de -- ' + document.all(ControlID + '_lblTrabDesI').innerHTML + ' --.\n\n¿Esta seguro de continuar?';
	        return confirm(lstrMensaje);
	        //return false;
	    } //end function
				
//End-->
</script>
</head>
<body>
    <form id="frmHESolicitud" name="frmHESolicitud" method="post" runat="Server">
        <input id="hdnCodigo" style="WIDTH: 24px; HEIGHT: 22px" type="hidden" size="1" name="hdnCodigo"
				runat="server" /><input id="hdnAccion" style="WIDTH: 24px; HEIGHT: 22px" type="hidden" size="1" name="hdnAccion"
				runat="server" /> &nbsp;&nbsp;&nbsp;&nbsp;
        <table id="Table4" style="Z-INDEX: 101; LEFT: 0px; POSITION: absolute; TOP: 0px" cellspacing="2" cellpadding="2" width="1008" border="0">
            <tr>
                <td class="Cabecera" style="HEIGHT: 22px">&nbsp;SOLICITUD DE HORAS EXTRAS</td>
            </tr>
            <tr>
					<td class="panel">
						<table id="Table23" style="WIDTH: 100%; HEIGHT: 8px" cellspacing="0" cellpadding="0">
							<tr>
								<td class="Etiqueta" style="HEIGHT: 16px" colspan="3">SOLICITANTE</td>
							</tr>
							<tr>
								<td class="Etiqueta" style="WIDTH: 114px">&nbsp;Área :</td>
								<td style="WIDTH: 245px"><nobr>&nbsp;&nbsp;<asp:textbox id="txtArea" runat="server" Width="208px" CssClass="input"></asp:textbox>&nbsp;<input class="Boton" id="btnAreaSolicitante" style="WIDTH: 20px; HEIGHT: 20px" onclick="javascript:fnc_areasolicitante(HDN1,txtArea);"
											type="button" value="..." name="btnAreaSolicitante" /></nobr></td>
								<td>&nbsp;<asp:label id="lblaprobadores" runat="server"></asp:label></td>
							</tr>
						</table>
						<table style="WIDTH: 1016px; HEIGHT: 83px" cellspacing="0" cellpadding="0">
							<tr>
								<td class="Etiqueta" style="WIDTH: 502px; HEIGHT: 20px" colspan="3">BUSQUEDA</td>
							</tr>
							<tr>
								<td class="Etiqueta" style="WIDTH: 98px; HEIGHT: 13px">&nbsp;Fecha&nbsp;Solicitada&nbsp;:</td>
								<td style="WIDTH: 65px; HEIGHT: 13px"><nobr><asp:checkbox id="chkdesde" runat="server" Text="Desde"></asp:checkbox>&nbsp;<asp:textbox id="txtfechaini" runat="server" Width="88px" CssClass="INPUT" Height="18px"
											Font-Names="Verdana" Font-Size="XX-Small"></asp:textbox>&nbsp;<img onclick="popUpCalendar(this, txtfechaini, 'dd/mm/yyyy')" height="15" alt="Seleccionar fecha"
											src="../images/Calendario.gif" width="13" border="0" /></nobr></td>
								<td style="HEIGHT: 13px">&nbsp;
									<asp:checkbox id="chkhasta" runat="server" Text="Hasta"></asp:checkbox>&nbsp;<asp:textbox id="txtfechafin" runat="server" Width="88px" CssClass="INPUT" Height="18px"
										Font-Names="Verdana" Font-Size="XX-Small"></asp:textbox>&nbsp;<img onclick="popUpCalendar(this, txtfechafin, 'dd/mm/yyyy')" height="15" alt="Seleccionar fecha"
										src="../images/Calendario.gif" width="13" border="0" /></td>
							</tr>
							<tr id="CuentaGasto" runat="server">
								<td class="Etiqueta" style="WIDTH: 98px">&nbsp;Situación :</td>
								<td style="WIDTH: 30px" colspan="2"><nobr><asp:checkbox id="chkSitACT" runat="server" Width="96px" Text="Activo" Checked="True"></asp:checkbox><asp:checkbox id="chkSitENV" runat="server" Width="96px" Text="Enviado" Checked="True"></asp:checkbox><asp:checkbox id="chkSitAPR" runat="server" Width="96px" Text="Aprobado" Checked="True"></asp:checkbox><asp:checkbox id="chkSitREC" runat="server" Width="96px" Text="Reconocido"></asp:checkbox><asp:checkbox id="chkSitANU" runat="server" Width="96px" Text="Anulado"></asp:checkbox></nobr></td>
							</tr>
							<tr>
								<td colspan="3">&nbsp;<asp:button id="btnConsultar" runat="server" CssClass="Boton" Text="Consultar"></asp:button>&nbsp;
									<asp:button id="btnSolicitar" runat="server" Width="117px" CssClass="Boton" Text="Solicitar Aprob."></asp:button>&nbsp;
									<asp:Label id="lblMensaje" runat="server" CssClass="input" ForeColor="Red"></asp:Label></td>
							</tr>
						</table>
						<table>
							<tr>
								<td><asp:datagrid id="DataGrid1" runat="server" Width="1008px" ShowFooter="True" AutoGenerateColumns="False">
										<FooterStyle CssClass="GridFooter"></FooterStyle>
										<AlternatingItemStyle CssClass="GridAlternateItem"></AlternatingItemStyle>
										<ItemStyle CssClass="GridItem"></ItemStyle>
										<HeaderStyle CssClass="GridHeader"></HeaderStyle>
										<Columns>
											<asp:TemplateColumn>
												<HeaderStyle Width="10px"></HeaderStyle>
												<ItemTemplate>
													<NOBR>
														<asp:Label id="lblCodigoI" runat="server" Width="0px" Height="0px" text='<%# Container.DataItem("int_codigo") %>' Visible="False">
														</asp:Label><input id="hdnCodigoI" style="WIDTH: 32px; HEIGHT: 22px" type="hidden" size="1" runat="server">
														<asp:ImageButton id="ibtConsultar" runat="server" ImageUrl="../images/buscar.gif"></asp:ImageButton>&nbsp;
														<asp:CheckBox id="chkSelI" runat="server"></asp:CheckBox></NOBR>
												</ItemTemplate>
												<EditItemTemplate>
													<asp:Label id=lblCodigoE runat="server" Width="0px" Height="0px" text='<%# Container.DataItem("int_codigo") %>' Visible="False">
													</asp:Label><input id="hdnCodigoE" style="WIDTH: 32px; HEIGHT: 22px" type="hidden" size="1" runat="server" />
												</EditItemTemplate>
											</asp:TemplateColumn>
											<asp:TemplateColumn HeaderText="Tarea">
												<HeaderStyle HorizontalAlign="Center" Width="40px"></HeaderStyle>
												<ItemStyle HorizontalAlign="Center"></ItemStyle>
												<ItemTemplate>
													<asp:Label id="lblTareaI" runat="server" Width="100%" CssClass="Input" text='<%# Container.DataItem("chr_tarea") %>'>
													</asp:Label>
												</ItemTemplate>
												<FooterTemplate>
													<asp:DropDownList id="ddlTareaF" runat="server" Width="100%" CssClass="input">
														<asp:ListItem Value="A">A</asp:ListItem>
														<asp:ListItem Value="B">B</asp:ListItem>
														<asp:ListItem Value="C">C</asp:ListItem>
														<asp:ListItem Value="D">D</asp:ListItem>
													</asp:DropDownList>
												</FooterTemplate>
												<EditItemTemplate>
													<asp:DropDownList id="ddlTareaE" runat="server" Width="100%" CssClass="input" SelectedValue='<%# Container.Dataitem("chr_tarea") %>'>
														<asp:ListItem Value="A" Selected="True">A</asp:ListItem>
														<asp:ListItem Value="B">B</asp:ListItem>
														<asp:ListItem Value="C">C</asp:ListItem>
														<asp:ListItem Value="D">D</asp:ListItem>
													</asp:DropDownList>
												</EditItemTemplate>
											</asp:TemplateColumn>
											<asp:TemplateColumn HeaderText="CTC">
												<HeaderStyle HorizontalAlign="Center" Width="80px"></HeaderStyle>
												<ItemTemplate>
													<asp:Label id="lblCtcI" runat="server" CssClass="Input" Width="100%" text='<%# Container.DataItem("vch_codctc") %>'>
													</asp:Label>
												</ItemTemplate>
												<FooterTemplate>
													<asp:TextBox id="txtCtcF" runat="server" CssClass="Input" Width="100%" Height="20px"></asp:TextBox>
												</FooterTemplate>
												<EditItemTemplate>
													<asp:TextBox id="txtCtcE" runat="server" CssClass="Input" Width="100%" Height="20px" text='<%# Container.DataItem("vch_codctc") %>'>
													</asp:TextBox>
												</EditItemTemplate>
											</asp:TemplateColumn>
											<asp:TemplateColumn HeaderText="Fecha">
												<HeaderStyle HorizontalAlign="Center" Width="115px"></HeaderStyle>
												<ItemTemplate>
													<asp:Label id="lblFechaI" runat="server" CssClass="Input" Width="80px" text='<%# Container.DataItem("chr_fecha_sol") %>'>
													</asp:Label>
												</ItemTemplate>
												<FooterTemplate>
													<asp:TextBox id="txtFechaF" runat="server" CssClass="Input" Width="80px" Height="20px"></asp:TextBox>&nbsp;<img onclick="popUpCalendar(this, <%# Container.ClientID %>_txtFechaF, 'dd/mm/yyyy')" height=15 alt="Seleccionar fecha" src="../images/Calendario.gif" width=13 border=0>
												</FooterTemplate>
												<EditItemTemplate>
													<asp:TextBox id="txtFechaE" runat="server" CssClass="Input" Width="80px" Height="20px" text='<%# Container.DataItem("chr_fecha_sol") %>'>
													</asp:TextBox>&nbsp;<img onclick="popUpCalendar(this, <%# Container.ClientID %>_txtFechaE, 'dd/mm/yyyy')" height=15 alt="Seleccionar fecha" src="../images/Calendario.gif" width=13 border=0>
												</EditItemTemplate>
											</asp:TemplateColumn>
											<asp:TemplateColumn HeaderText="C&#243;digo">
												<HeaderStyle HorizontalAlign="Center" Width="75px"></HeaderStyle>
												<HeaderTemplate>
													<table id="Table6" style="HEIGHT: 28px" cellspacing="1" cellpadding="1" width="65" border="0">
														<tr>
															<td class="GridHeader">
																<center>Código</center>
															</td>
														</tr>
													</table>
												</HeaderTemplate>
												<ItemTemplate>
													<table id="Table7" style="WIDTH: 50px; HEIGHT: 8px" cellspacing="1" cellpadding="1" width="105"
														border="0">
														<tr>
															<td>
																<asp:Panel id="Panel25" runat="server" Width="80px">
																	<asp:Label id="lblTrabajadorI" runat="server" CssClass="input" Width="50px" text='<%# Container.DataItem("vch_codtrabajador") %>'>
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
																<asp:TextBox id="txtTrabajadorF" runat="server" CssClass="input" Width="50px" MaxLength="5"></asp:TextBox></td>
															<td><input class="Boton" id="btnBuscarTrabF" style="WIDTH: 20px; HEIGHT: 20px" type="button"
																	value="..." runat="server" /></td>
														</tr>
													</table>
												</FooterTemplate>
												<EditItemTemplate>
													<table id="Table9" style="WIDTH: 65px; HEIGHT: 24px" cellspacing="1" cellpadding="1" width="73"
														border="0">
														<tr>
															<td>
																<asp:TextBox id="txtTrabajadorE" runat="server" CssClass="input" Width="50px" text='<%# Container.DataItem("vch_codtrabajador") %>'>
																</asp:TextBox></td>
															<td><input class="Boton" id="btnBuscarTrabE" style="WIDTH: 20px; HEIGHT: 20px" type="button"
																	value="..." runat="server" /></td>
														</tr>
													</table>
												</EditItemTemplate>
											</asp:TemplateColumn>
											<asp:TemplateColumn HeaderText="Trabajador">
												<HeaderStyle HorizontalAlign="Center" Width="200px"></HeaderStyle>
												<ItemTemplate>
													<asp:Label id="lblTrabDesI" runat="server" CssClass="Input" Width="100%" text='<%# Container.DataItem("vch_destrabajador") %>'>
													</asp:Label>
												</ItemTemplate>
												<FooterTemplate>
													<asp:Label id="lblTrabDesF" runat="server" CssClass="Input" Width="100%"></asp:Label>
												</FooterTemplate>
												<EditItemTemplate>
													<asp:Label id="lblTrabDesE" runat="server" CssClass="Input" Width="100%" text='<%# Container.DataItem("vch_destrabajador") %>'>
													</asp:Label>
												</EditItemTemplate>
											</asp:TemplateColumn>
											<asp:TemplateColumn HeaderText="Horas Solici.">
												<HeaderStyle HorizontalAlign="Center" Width="50px"></HeaderStyle>
												<ItemStyle HorizontalAlign="Center"></ItemStyle>
												<ItemTemplate>
													<asp:Label id="lblHESolI" runat="server" CssClass="InputDerecha" Width="100%" text='<%# Container.DataItem("tin_horext_sol") %>'>
													</asp:Label>
												</ItemTemplate>
												<FooterTemplate>
													<asp:TextBox id="txtHESolF" runat="server" CssClass="InputDerecha" Width="100%" Height="20px"></asp:TextBox>
												</FooterTemplate>
												<EditItemTemplate>
													<asp:TextBox id="txtHESolE" runat="server" CssClass="InputDerecha" Width="100%" Height="20px" text='<%# Container.DataItem("tin_horext_sol") %>'>
													</asp:TextBox>
												</EditItemTemplate>
											</asp:TemplateColumn>
											<asp:TemplateColumn HeaderText="Motivo">
												<HeaderStyle HorizontalAlign="Center" Width="450px"></HeaderStyle>
												<ItemTemplate>
													<asp:Label id="lblObservacionI" runat="server" CssClass="Input" Width="100%" text='<%# Container.DataItem("vch_observacion") %>'>
													</asp:Label>
												</ItemTemplate>
												<FooterTemplate>
													<asp:TextBox id="txtObservacionF" runat="server" CssClass="Input" Width="100%" Height="20px"></asp:TextBox>
												</FooterTemplate>
												<EditItemTemplate>
													<asp:TextBox id="txtObservacionE" runat="server" CssClass="Input" Width="100%" Height="20px" text='<%# Container.DataItem("vch_observacion") %>'>
													</asp:TextBox>
												</EditItemTemplate>
											</asp:TemplateColumn>
											<asp:TemplateColumn HeaderText="Situ.">
												<HeaderStyle Width="30px"></HeaderStyle>
												<ItemTemplate>
													<asp:Label id="lblSituI" runat="server" Width="100%" CssClass="Input" text='<%# Container.DataItem("chr_estado") %>'>
													</asp:Label>
												</ItemTemplate>
												<EditItemTemplate>
													<asp:Label id="lblSituE" runat="server" Width="100%" CssClass="Input" text='<%# Container.DataItem("chr_estado") %>'>
													</asp:Label>
												</EditItemTemplate>
											</asp:TemplateColumn>
											<asp:TemplateColumn>
												<HeaderStyle Width="60px"></HeaderStyle>
												<ItemTemplate>
													<asp:ImageButton id="btnEdit" runat="server" CommandName="Edit" ImageUrl="../images/Editar.gif"></asp:ImageButton>
													<asp:ImageButton id="btnDelete" runat="server" CommandName="Delete" ImageUrl="../images/borrador.gif"></asp:ImageButton>
												</ItemTemplate>
												<FooterTemplate>
													<asp:ImageButton id="btnAdd" runat="server" CommandName="Add" ImageUrl="../images/Grabar.gif"></asp:ImageButton>
												</FooterTemplate>
												<EditItemTemplate>
													<asp:ImageButton id="btnUpdate" runat="server" CommandName="Update" ImageUrl="../images/Grabar.gif"></asp:ImageButton>
													<asp:ImageButton id="btnCancel" runat="server" CommandName="Cancel" ImageUrl="../images/Cancelar.gif"></asp:ImageButton>
												</EditItemTemplate>
											</asp:TemplateColumn>
										</Columns>
									</asp:datagrid></td>
							</tr>
						</table>
						<input id="HDN2" style="WIDTH: 32px; HEIGHT: 22px" type="hidden" size="1" name="HDN2" runat="server" />
						<input id="HDN1" style="WIDTH: 32px; HEIGHT: 22px" type="hidden" size="1" name="HDN1" runat="server" />&nbsp;
						<input id="HDN4" style="WIDTH: 32px; HEIGHT: 21px" type="hidden" size="1" name="hdnAprobacion" runat="server" />
                        <input id="HDN3" style="WIDTH: 32px; HEIGHT: 22px" type="hidden" size="1" name="HDN3" runat="server" />
                        <input id="HDN5" style="WIDTH: 32px; HEIGHT: 22px" type="hidden" size="1" name="HDN5" runat="server" />
                        <input id="HDN6" style="WIDTH: 32px; HEIGHT: 22px" type="hidden" size="1" name="HDN6" runat="server" /></td>
				</tr>
        </table>
    </form>
</body>
</html>
