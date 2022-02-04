<%@ Page Language="vb" AutoEventWireup="false" Codebehind="PLA20011.aspx.vb" Inherits="intranet_rrhh_v2.PLA20011"%>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html>
	<head>
		<title></title>
		<base target="_self" />
		<meta content="Microsoft Visual Studio.NET 7.0" name="GENERATOR" />
		<meta content="Visual Basic 7.0" name="CODE_LANGUAGE" />
		<meta content="JavaScript" name="vs_defaultClientScript" />
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema" />
		<link href="../../intranet/Estilos/NM0001.css" type="text/css" rel="stylesheet" />
		<script type="text/javascript" language="javascript" src="../../intranet/JS/jsCalendario_N3.js"></script>
		<script type="text/javascript" language="javascript" src="../../intranet/JS/functions.js"></script>
		<script type="text/javascript" language="javascript" src="../../intranet/JS/jsDesTabla.js"></script>
		<script type="text/javascript" language="JavaScript">

		returnValue = "";
		function Aprobar(Valor)
		{
			returnValue = Valor;
			window.close();
		}//end function
				
		function popUp(strUrl) 
		{
			var intWidth = screen.width;
			var intHeight = screen.height;
			window.open(strUrl, "", "top=0; left=0; width=" + intWidth + "; height=" + intHeight + "; resizable=1;");
		}//end funcion
		
		function fnc_validarreconocer()
		{
			var ltxtcantrec=document.all('txtreduracion');
			var ltxtcantapr=document.all('txthoraapr');
			var ltxtinicio=document.all('txtrecinicio');
			
			//debe validar que cantidadhoras>0
			if(isVarType(ltxtcantrec.value,'INT')==true)
			{
				if(parseInt(ltxtcantrec.value)<=0 || parseInt(ltxtcantrec.value)>parseInt(ltxtcantapr.value))
				{
					alert('Las horas reconocidas deben ser mayor a cero y menor a las aprobadas.');
					ltxtcantrec.focus();
					return false;
				}//end if
			}else{
				alert('Las horas reconocidas no son validas.');
				ltxtcantrec.focus();
				return false;
			}//end if	
			
			if(ltxtinicio.value.length!=5)
			{
				alert('Formato no es valido --HH:MM--.');
				ltxtinicio.focus();
				return false;
			}//end if
			
			if(ltxtinicio.value.substring(2,3)!=':')
			{
				alert('Formato no es valido --HH:MM--.');
				ltxtinicio.focus();
				return false;
			}//end if
				
			return confirm('¿ Desea reconocer --'+ ltxtcantrec.value +'-- hora(s) ?');
		}//end function
		
		function txtrecinicio_onBlur()
		{
			var ltxtrecinicio=document.all('txtrecinicio');
			if(trim(ltxtrecinicio.value)=='')
			{
				ltxtrecinicio.value='00:00';
			}//end if
		}//end function
		
		function fnc_validarreintegro()
		{
			var ltxtcantrec=document.all('txthorarec');
			var ltxtcantapr=document.all('txthoraapr');
			var ltxtusuariorec=document.all('txtusuariorec');
			var ltxtusuariorei=document.all('txtestadorei');
			
			//debe validar que cantidadhoras>0
			if(ltxtusuariorec.value!='')
			{
				if(trim(ltxtusuariorei.value)!='')
				{
					alert('Las horas ya fueron enviadas a reintegro.');
					ltxtcantrec.focus();
					return false;
				}//end if
			}else{
				alert('Debe de aprobar las horas reconocidas.');
				ltxtcantrec.focus();
				return false;
			}//end if	
			
			return confirm('¿ Desea enviar a la lista de reintegro ?');
		}//end function		
		
		function fnc_buscarmarca()
		{
			//validar las 2 fechas
			var ltxtfechaini=document.all('txtfechaini');
			var ltxtfechafin=document.all('txtfechafin');
			if(ltxtfechaini.value!='' && ltxtfechaini.value!='')
			{
				return true;
			}else{
				alert('Ingrese las dos fechas de inicio y fin.');
				return false;
			}//end if						
		}//end function
				
		function txtHESol_onBlur(ControlID,Constante)
		{
			//validar que el numero sea un entero
			var ltxtCantidad=document.all(ControlID + '_txtHESol'+Constante);
			if(ltxtCantidad.value=='')
			{
				ltxtCantidad.value=0;
				return;
			}//end if
			if(isVarType(ltxtCantidad.value,'INT')==false)
			{
				alert('La cantidad ingresada no es valida.');
				ltxtCantidad.focus();
				return;
			}//end if	
			//evaluar la cantidad de horas extras no debe ser mayor de 8 horas(ejemplo)
			if(parseInt(ltxtCantidad.value)<0 || parseInt(ltxtCantidad.value)>8)
			{
				alert('La cantidad ingresada no debe ser mayor de 8.');
				ltxtCantidad.focus();
			}//end if
		}//end function
		
		function fnc_mostrarreco()
		{	
			//document.all('hdnposicionpanel').value=event.clientY+10;
			//document.all['pnldatosestadisticos'].style.top=20;
			document.all['pnldatosestadisticos'].style.display='inline';
			//document.all['pnldatosestadisticos'].style.top=event.clientY+20;
			
			return true;
		}//end function
		
		function fnc_ocultarreco()
		{
			document.all['pnldatosestadisticos'].style.display='none';
		}//end function
									
		</script>
		<link href="../../intranet/Estilos/NM0001.css" type="text/css" rel="stylesheet" />
	</head>
	<body>
		<form id="frmHESolicitud" name="frmHESolicitud" method="post" runat="Server">
			<asp:panel id="pnldatosestadisticos" style="Z-INDEX: 103; LEFT: 160px; POSITION: absolute; TOP: 232px"
				BorderStyle="Solid" BorderWidth="8px" runat="server" Width="649px" CssClass="GridItem"
				Height="280px" BorderColor="#336699">
				<table class="GridItem" id="Table1" style="WIDTH: 632px; HEIGHT: 256px" width="632">
					<tr>
						<td class="cabecera" colspan="2" height="19">&nbsp;RECONOCIMIENTO DE HORAS EXTRAS</td>
					</tr>
					<tr>
						<td class="GridItem" style="WIDTH: 346px" valign="top">
							<table style="WIDTH: 392px; HEIGHT: 100%" cellspacing="0" cellpadding="0">
								<tr height="18">
									<td class="Etiqueta" rowspan="2">
										<asp:image id="Image2" runat="server" ImageUrl="../../intranet/Imagenes/lblMarcaciones.JPG"></asp:image></td>
									<td class="Etiqueta" height="18">&nbsp;Inicio:</td>
									<td height="18">&nbsp;
										<asp:textbox id="txtfechaini" runat="server" Height="18px" CssClass="INPUT" Width="72px" Font-Size="XX-Small"
											Font-Names="Verdana" ></asp:textbox>&nbsp;<img onclick="popUpCalendar(this, txtfechaini, 'dd/mm/yyyy')" height="15" alt="Seleccionar fecha"
											src="../../intranet/Imagenes/Calendario.gif" width="13" border="0" /></td>
									<td class="Etiqueta" style="WIDTH: 1px" height="18">&nbsp;Fin:&nbsp;&nbsp;&nbsp;</td>
									<td>&nbsp;
										<asp:textbox id="txtfechafin" runat="server" Height="18px" CssClass="INPUT" Width="72px" Font-Size="XX-Small"
											Font-Names="Verdana" ></asp:textbox>&nbsp;<img onclick="popUpCalendar(this, txtfechafin, 'dd/mm/yyyy')" height="15" alt="Seleccionar fecha"
											src="../../intranet/Imagenes/Calendario.gif" width="13" border="0" /></td>
									<td style="WIDTH: 33px">
										<asp:button id="btnbuscarmarca" runat="server" CssClass="Boton" Width="52px" Text="Buscar"></asp:button></td>
								</tr>
								<tr>
									<td style="WIDTH: 329px" valign="top" colspan="5" height="100%">
										<asp:datagrid id="dgmarcaciones" runat="server" Width="360px" AutoGenerateColumns="False">
											<AlternatingItemStyle CssClass="gridalternateitem"></AlternatingItemStyle>
											<ItemStyle CssClass="griditem"></ItemStyle>
											<HeaderStyle CssClass="gridheader"></HeaderStyle>
											<Columns>
												<asp:TemplateColumn HeaderText="Fecha">
													<ItemTemplate>
														<asp:label id="lblfechaI" runat="server" CssClass="input"></asp:label>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn HeaderText="Hora">
													<ItemTemplate>
														<asp:label id="lblhoraI" runat="server" CssClass="input" Text='<%# Container.dataitem("chr_hora") %>'>
														</asp:label>
													</ItemTemplate>
												</asp:TemplateColumn>
											</Columns>
										</asp:datagrid></td>
								</tr>
							</table>
						</td>
						<td class="GridItem" valign="top">
							<table id="Table2" style="WIDTH: 217px; HEIGHT: 72px">
								<tr>
									<td class="Etiqueta" style="WIDTH: 56px">&nbsp;Tipo&nbsp;:&nbsp;</td>
									<td>
										<asp:DropDownList id="ddlrecTipo" runat="server" CssClass="input" Width="150px"></asp:DropDownList></td>
								</tr>
								<tr>
									<td class="Etiqueta" style="WIDTH: 56px">&nbsp;Inicio&nbsp;:&nbsp;</td>
									<td>
										<asp:TextBox id="txtrecinicio" runat="server" CssClass="input" Width="80px" MaxLength="5"></asp:TextBox></td>
								</tr>
								<tr>
									<td class="Etiqueta" style="WIDTH: 56px">&nbsp;Duración:&nbsp;</td>
									<td>
										<asp:TextBox id="txtreduracion" runat="server" CssClass="input" Width="80px" MaxLength="2"></asp:TextBox></td>
								</tr>
							</table>
							<asp:button id="btnreconocer" runat="server" CssClass="Boton" Width="128px" Text="Guardar datos"
								Enabled="False"></asp:button>&nbsp;&nbsp;&nbsp;&nbsp;
							<asp:button id="btnocultarrec" runat="server" CssClass="Boton" Width="71px" Text="Cerrar"></asp:button></td>
					</tr>
				</table>
				<input id="hdnarticuloestadistica" style="WIDTH: 32px; HEIGHT: 8px" type="hidden" size="1"
					name="hdnarticuloestadistica" runat="server" /><input id="hdnposicionpanel" style="WIDTH: 32px; HEIGHT: 8px" type="hidden" size="1" name="hdnposicionpanel"
					runat="server" /></asp:panel>
			<input id="hdnCodigo" style="WIDTH: 24px; HEIGHT: 22px" type="hidden" size="1" name="hdnCodigo"
				runat="server" /><input id="hdnAccion" style="WIDTH: 24px; HEIGHT: 22px" type="hidden" size="1" name="hdnAccion"
				runat="server" /> &nbsp;&nbsp;&nbsp;&nbsp;
			<table id="Table4" style="Z-INDEX: 102; LEFT: 0px; WIDTH: 816px; POSITION: absolute; TOP: 0px; HEIGHT: 300px"
				cellspacing="2" cellpadding="2" width="816" border="0">
				<tr>
					<td class="cabecera" style="HEIGHT: 22px">&nbsp;DETALLE DE HORAS EXTRAS</td>
				</tr>
				<tr valign="top">
					<td class="panel">
						<table id="Table23" style="WIDTH: 100%; HEIGHT: 120px" cellspacing="0" cellpadding="0">
							<tr>
								<td class="Etiqueta" style="WIDTH: 446px; HEIGHT: 16px">&nbsp;1.-SOLICITUD DE HORAS 
									EXTRAS</td>
								<td class="Etiqueta">&nbsp;&nbsp;&nbsp;&nbsp;</td>
								<td class="Etiqueta" style="HEIGHT: 16px">
									<center>2.-APROBACION DE SOLICITUD</center>
								</td>
							</tr>
							<tr>
								<td valign="top">
									<table cellspacing="0" cellpadding="0" width="100%">
										<tr>
											<td class="Etiqueta">&nbsp;Área:</td>
											<td colspan="3">&nbsp;
												<asp:textbox id="txtdesarea" runat="server" Width="240px" CssClass="INPUT" Height="18px" Font-Size="XX-Small"
													Font-Names="Verdana"></asp:textbox>&nbsp;</td>
										</tr>
										<tr>
											<td class="Etiqueta">&nbsp;Trabajador:</td>
											<td colspan="3">&nbsp;
												<asp:textbox id="txtdestrabajador" runat="server" Width="240px" CssClass="INPUT" Height="18px"
													Font-Size="XX-Small" Font-Names="Verdana"></asp:textbox></td>
										</tr>
										<tr>
											<td class="Etiqueta">&nbsp;Fecha:</td>
											<td>&nbsp;
												<asp:textbox id="txtfechasol" runat="server" Width="88px" CssClass="INPUT" Height="18px" Font-Size="XX-Small"
													Font-Names="Verdana" ></asp:textbox></td>
											<td class="Etiqueta">&nbsp;Estado:</td>
											<td><nobr>&nbsp;
													<asp:textbox id="txtestado" runat="server" Width="73px" CssClass="INPUT" Height="18px" Font-Size="XX-Small"
														Font-Names="Verdana"  Wrap="False"></asp:textbox></nobr>
											</td>
										</tr>
										<tr>
											<td class="Etiqueta">&nbsp;Tipo:</td>
											<td colspan="3">&nbsp;
												<asp:textbox id="txtdestipo" runat="server" Width="240px" CssClass="INPUT" Height="18px" Font-Size="XX-Small"
													Font-Names="Verdana" ></asp:textbox></td>
										</tr>
										<tr>
											<td class="Etiqueta">&nbsp;Horas:</td>
											<td>&nbsp;
												<asp:textbox id="txthorassol" runat="server" Width="88px" CssClass="INPUT" Height="18px" Font-Size="XX-Small"
													Font-Names="Verdana" ></asp:textbox></td>
											<td class="Etiqueta">&nbsp;Inicio(aprox):</td>
											<td><nobr>&nbsp;
													<asp:textbox id="txtinicioaprox" runat="server" Width="73px" CssClass="INPUT" Height="18px" Font-Size="XX-Small"
														Font-Names="Verdana" Wrap="False"></asp:textbox></nobr>
											</td>
										</tr>
										<tr>
											<td class="Etiqueta">&nbsp;Obs.:</td>
											<td colspan="3">&nbsp;
												<asp:textbox id="txtobservacion" runat="server" Width="240px" CssClass="INPUT" Height="32px"
													Font-Size="XX-Small" Font-Names="Verdana" TextMode="MultiLine"></asp:textbox></td>
										</tr>
									</table>
								</td>
								<td>&nbsp;&nbsp;</td>
								<td style="WIDTH: 446px" valign="top">
									<table style="WIDTH: 472px" cellspacing="0" cellpadding="0" width="472">
										<tr>
											<td colspan="3"><asp:datagrid id="dgSeguimiento" runat="server" Width="474px" AutoGenerateColumns="False">
													<AlternatingItemStyle CssClass="GridAlternateItem"></AlternatingItemStyle>
													<ItemStyle CssClass="GridItem"></ItemStyle>
													<HeaderStyle CssClass="GridHeader"></HeaderStyle>
													<Columns>
														<asp:BoundColumn DataField="nu_paso" HeaderText="Sec.">
															<HeaderStyle Width="50px"></HeaderStyle>
														</asp:BoundColumn>
														<asp:BoundColumn DataField="st_apro" HeaderText="Estado">
															<HeaderStyle Width="50px"></HeaderStyle>
														</asp:BoundColumn>
														<asp:BoundColumn DataField="no_usua" HeaderText="Usuario">
															<HeaderStyle Width="250px"></HeaderStyle>
														</asp:BoundColumn>
														<asp:BoundColumn DataField="fe_apro" HeaderText="Fecha" DataFormatString="{0:dd/MM/yyyy hh:mm}">
															<HeaderStyle Width="150px"></HeaderStyle>
														</asp:BoundColumn>
													</Columns>
												</asp:datagrid></td>
										</tr>
										<tr height="18">
											<td class="Etiqueta" style="WIDTH: 2px">&nbsp;CTC:</td>
											<td><asp:textbox id="txtcodctc" runat="server" Width="88px" CssClass="INPUT" Height="18px" Font-Size="XX-Small"
													Font-Names="Verdana" ></asp:textbox>
											</td>
											<td align="right">
												<asp:button id="btnrefrescar" runat="server" CssClass="Boton" Width="97px" Text="Refrescar"></asp:button>
											</td>
										</tr>
										<tr>
											<td colspan="3"><asp:textbox id="txtdesctc" runat="server" Width="470px" CssClass="INPUT" Height="32px" Font-Size="XX-Small"
													Font-Names="Verdana" TextMode="MultiLine"></asp:textbox></td>
										</tr>
									</table>
								</td>
							</tr>
							<tr>
								<td class="Etiqueta" height="19">&nbsp;3.-RECONOCIMIENTO DE HORAS EXTRAS</td>
								<td class="Etiqueta">&nbsp;&nbsp;&nbsp;&nbsp;</td>
								<td class="Etiqueta" style="HEIGHT: 16px">4.-REINTEGRO</td>
							</tr>
							<tr>
								<td valign="top">
									<table cellspacing="0" cellpadding="0" width="100%">
										<tr>
											<td class="Etiqueta">&nbsp;Aprobado:</td>
											<td><asp:textbox id="txthoraapr" runat="server" Width="83px" CssClass="INPUT" Height="18px" Font-Size="XX-Small"
													Font-Names="Verdana"></asp:textbox></td>
											<td class="Etiqueta">&nbsp;Reconocido:</td>
											<td><asp:textbox id="txthorarec" runat="server" Width="72px" CssClass="INPUT" Height="18px" Font-Size="XX-Small"
													Font-Names="Verdana" ></asp:textbox></td>
										</tr>
										<tr>
											<td class="Etiqueta">&nbsp;Fecha:</td>
											<td><asp:textbox id="txtfecharec" runat="server" Width="83px" CssClass="INPUT" Height="18px" Font-Size="XX-Small"
													Font-Names="Verdana" ></asp:textbox></td>
											<td class="Etiqueta">&nbsp;Usuario:</td>
											<td><asp:textbox id="txtusuariorec" runat="server" Width="72px" CssClass="INPUT" Height="18px" Font-Size="XX-Small"
													Font-Names="Verdana"></asp:textbox></td>
										</tr>
										<tr>
											<td class="Etiqueta">&nbsp;Marcación:</td>
											<td colspan="3"><asp:textbox id="txtmarcacionrec" runat="server" Width="168px" CssClass="INPUT" Height="18px"
													Font-Size="XX-Small" Font-Names="Verdana"></asp:textbox></td>
										</tr>
										<tr>
											<td colspan="3"><FONT style="FONT-STYLE: italic; BACKGROUND-COLOR: #ffff66; TEXT-ALIGN: center">* 
													Registra al Módulo de Supervisores para el pago por planillas, en caso de 
													planilla cerrada debe generar&nbsp;reintegro.</FONT></td>
											<td align="right"><asp:button id="btningresorec" runat="server" Width="72px" CssClass="Boton" Text="Reconocer"
													Enabled="False"></asp:button></td>
										</tr>
										<tr>
											<td colspan="4"><asp:label id="lblMensaje" runat="server" CssClass="input"></asp:label></td>
										</tr>
									</table>
								</td>
								<td class="Etiqueta"></td>
								<td valign="top">
									<table cellspacing="0" cellpadding="0" width="432" style="WIDTH: 100%; HEIGHT: 45px">
										<tr>
											<td class="Etiqueta" style="WIDTH: 59px">&nbsp;Fecha:</td>
											<td><asp:textbox id="txtfecharei" runat="server" Width="83px" CssClass="INPUT" Height="18px" Font-Size="XX-Small"
													Font-Names="Verdana" ></asp:textbox></td>
											<td class="Etiqueta" style="WIDTH: 62px">&nbsp;Usuario:</td>
											<td style="WIDTH: 97px"><asp:textbox id="txtusuariorei" runat="server" Width="72px" CssClass="INPUT" Height="18px" Font-Size="XX-Small"
													Font-Names="Verdana"></asp:textbox>
											</td>
											<td><asp:button id="btnreintegro" runat="server" CssClass="Boton" Width="115px" Text="Enviar a reintegro"
													Enabled="False"></asp:button>
											</td>
										</tr>
										<tr>
											<td class="Etiqueta" style="WIDTH: 59px">&nbsp;Estado:</td>
											<td><asp:textbox id="txtestadorei" runat="server" Width="83px" CssClass="INPUT" Height="18px" Font-Size="XX-Small"
													Font-Names="Verdana" ></asp:textbox></td>
											<td style="WIDTH: 62px"></td>
											<td style="WIDTH: 97px; HEIGHT: 23px" align="right"></td>
										</tr>
									</table>
								</td>
							</tr>
						</table>
						<input id="HDN2" style="WIDTH: 32px; HEIGHT: 22px" type="hidden" size="1" name="HDN2" runat="server">
						<input id="HDN1" style="WIDTH: 32px; HEIGHT: 22px" type="hidden" size="1" name="HDN1" runat="server">&nbsp;
						<input class="Input" id="HDN4" style="WIDTH: 32px; HEIGHT: 21px" type="hidden" size="1"
							name="hdnAprobacion" runat="server"><input id="HDN3" style="WIDTH: 32px; HEIGHT: 22px" type="hidden" size="1" name="HDN3" runat="server"><input id="HDN5" style="WIDTH: 32px; HEIGHT: 22px" type="hidden" size="1" name="HDN5" runat="server"></td>
				</tr>
			</table>
		</form>
	</body>
</html>
