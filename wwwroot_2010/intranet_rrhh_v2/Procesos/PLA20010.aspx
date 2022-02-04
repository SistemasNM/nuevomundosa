<%@ Page Language="vb" AutoEventWireup="false" Codebehind="PLA20010.aspx.vb" Inherits="intranet_rrhh_v2.PLA20010"%>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html>
	<head>
		<title></title>
		<base target="_self" />
		<meta content="Microsoft Visual Studio.NET 7.0" name="GENERATOR" />
		<meta content="Visual Basic 7.0" name="CODE_LANGUAGE" />
		<meta content="JavaScript" name="vs_defaultClientScript" />
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema" />
		<%--<link href="../../intranet/Estilos/NM0001.css" type="text/css" rel="stylesheet" />--%>
        <link href="../Styles/NM0001.css" rel="stylesheet" type="text/css" />
		<style type="text/css">  
			#toolTipBox { BORDER-BOTTOM: black 1px solid; POSITION: absolute; BORDER-LEFT: black 1px solid; PADDING-BOTTOM: 5px; BACKGROUND-COLOR: #ffffcc; PADDING-LEFT: 5px; PADDING-RIGHT: 5px; DISPLAY: none; FONT-FAMILY: verdana; COLOR: #000000; FONT-SIZE: 9px; BORDER-TOP: black 1px solid; BORDER-RIGHT: black 1px solid; PADDING-TOP: 5px }  
		</style>
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
		
		
		function txtHESol_onBlur(ControlID,Constante,CantVal,Index) {

		    alert(ControlID);
		    alert(Constante);
			//validar que el numero sea un entero
		    var ltxtCantidad = document.all(ControlID + '_txtHESol' + Constante +'_'+ Index);
			if(ltxtCantidad.value=='')
			{
				ltxtCantidad.value=0;
				return;
			}//end if
			if(isVarType(ltxtCantidad.value,'INT')==false)
			{
				alert('La cantidad ingresada no es valida.');
				ltxtCantidad.value=CantVal;
				ltxtCantidad.focus();
				return;
			}//end if	
			//evaluar la cantidad de horas extras no debe ser mayor a horas solicitadas
			if(parseInt(ltxtCantidad.value)<0 || parseInt(ltxtCantidad.value)>CantVal)
			{
				alert('La cantidad ingresada no debe ser mayor que lo solicitado.');
				ltxtCantidad.value=CantVal;
				ltxtCantidad.focus();
			}//end if
		}//end function

		function txtHEApr_onBlur(ControlID, Constante, CantVal, Index)
		{
			//validar que el numero sea un entero
		    var ltxtCantidad = document.all(ControlID + '_txtHEApr' + Constante + '_' + Index);
			if(ltxtCantidad.value=='')
			{
				ltxtCantidad.value=0;
				return;
			}//end if
			if(isVarType(ltxtCantidad.value,'INT')==false)
			{
				alert('La cantidad ingresada no es valida.');
				ltxtCantidad.value=CantVal;
				ltxtCantidad.focus();
				return;
			}//end if	
			//evaluar la cantidad de horas extras no debe ser mayor a horas solicitadas
			if(parseInt(ltxtCantidad.value)<0 || parseInt(ltxtCantidad.value)>CantVal)
			{
				alert('La cantidad ingresada no debe ser mayor que lo solicitado.');
				ltxtCantidad.value=CantVal;
				ltxtCantidad.focus();
			}//end if
		}//end function
		
		function fnc_datosgrilla()
		{
			var lint_sigue=0;
			var lstr_textbox='';
			
			var lhdn2=document.all('HDN2');//SI, NO
			var lhdn3=document.all('HDN3');//codigos
			var lhdn4=document.all('HDN4');//nombre de datagrid
			var lhdn5=document.all('HDN5');//cantidades
			
			lhdn3.value='';
			lhdn5.value='';
			//es aprobación?
			if(lhdn2.value=='SI')
			{
				lstr_textbox='txtHEAprI_';
			}else{
				lstr_textbox='txtHESolI_';
			}//end if
			
			
			/*
			if(document.all(lhdn4.value+'0_hdnCodigoI')==null)
			{		alert('0');}
			if(document.all(lhdn4.value+'1_hdnCodigoI')==null)
			{		alert('1');}
			if(document.all(lhdn4.value+'2_hdnCodigoI')==null)
			{		alert('2');}
			if(document.all(lhdn4.value+'3_hdnCodigoI')==null)
			{		alert('3');}
			if(document.all(lhdn4.value+'4_hdnCodigoI')==null)
			{		alert('4');}
			if(document.all(lhdn4.value+'5_hdnCodigoI')==null)
			{		alert('5');}
			*/
			
			for(var i=0; lint_sigue<5; i++)
			{

			    var ltxtcodigo = document.all(lhdn4.value + 'hdnCodigoI_' + i);
							
				if(ltxtcodigo==null)
				{
					lint_sigue=lint_sigue+1;
				}else{
	    var ltxtcantidad = document.all(lhdn4.value + lstr_textbox + i);
					
					lhdn3.value=lhdn3.value+ltxtcodigo.value+'|';
					lhdn5.value=lhdn5.value+ltxtcantidad.value+'|';
				}//end if
			}//end for
			
			if (lhdn3.value=='')
			{
				alert('No hay registros para aprobar.');
				return false;
			}//end if
			
			//return confirm('¿ Desea aprobar las horas extras ?');
			return true;
			
		}//end function
		
		function fnc_Datagrid1_Validar(ControlID,Constante)
		{
		    //verificar horassol >0 y <9, si tarea=B => ctc<>''
		    alert(ControlID);

			var lddlTarea=document.all(ControlID + '_ddlTarea_'+Constante);
			var ltxtCtc=document.all(ControlID + '_txtCtc_'+Constante);
			var ltxtFecha=document.all(ControlID + '_txtFecha'+Constante);
			var ltxtTrabajador=document.all(ControlID + '_txtTrabajador'+Constante);
			var ltxtHESol=document.all(ControlID + '_txtHESol'+Constante);
			
			//horas solicitadas
			if(trim(ltxtHESol.value)=='' || isVarType(ltxtHESol.value,'INT')==false)
			{
				alert('Las horas solicitadas no es valido.');
				ltxtHESol.focus()
				return false;
			}//end if	
			if(parseInt(ltxtHESol.value)<0 || parseInt(ltxtHESol.value)>16)
			{
				alert('Las horas solicitadas debe ser mayor o igual a 0 y menor a 8.');
				ltxtHESol.focus()
				return false;
			}//end if	
			
			//validar tarea
			if(trim(lddlTarea.value)=='')
			{
				alert('Debe seleccionar una tarea.');
				lddlTarea.focus()
				return false;
			}//end if		
			if(trim(lddlTarea.value)=='B' && trim(ltxtCtc.value)=='')
			{
				alert('Debe ingresar una CTC para esta tarea.');
				ltxtCtc.focus()
				return false;
			}//end if
			
			if(trim(ltxtTrabajador.value)=='')
			{
				alert('Debe ingresar un trabajador.');
				ltxtTrabajador.focus()
				return false;
			}//end if
			
			///////////////////////////////
			return true;
			///////////////////////////////
		}//end function
							
		</script>
		<script type="text/javascript">  
		var theObj="";
		function toolTip(text,me) {  
			theObj=me;  
			theObj.onmousemove=updatePos;  
			document.getElementById('toolTipBox').innerHTML=text;  
			document.getElementById('toolTipBox').style.display="block";  
			window.onscroll=updatePos;  
		}  
		function updatePos() {  
			var ev=arguments[0]?arguments[0]:event;  
			var x=ev.clientX;  
			var y=ev.clientY;  
			diffX=24;  
			diffY=0;  
			document.getElementById('toolTipBox').style.top  = y-2+diffY+document.body.scrollTop+ "px";  
			document.getElementById('toolTipBox').style.left = x-2+diffX+document.body.scrollLeft+"px";  
			theObj.onmouseout=hideMe;  
		}  
		function hideMe() {  
			document.getElementById('toolTipBox').style.display="none";  
		}  
		</script>
		<link href="../../intranet/Estilos/NM0001.css" type="text/css" rel="stylesheet" />
	</head>
	<body >
		<form id="frmHESolicitud" name="frmHESolicitud" method="post" runat="Server">
			<input id="hdnCodigo" style="WIDTH: 24px; HEIGHT: 22px" type="hidden" size="1" name="hdnCodigo"
				runat="server" /><input id="hdnAccion" style="WIDTH: 24px; HEIGHT: 22px" type="hidden" size="1" name="hdnAccion"
				runat="server" /> &nbsp;&nbsp;&nbsp;&nbsp;
			<table id="Table4" style="Z-INDEX: 101; POSITION: absolute; WIDTH: 816px; HEIGHT: 300px; TOP: 0px; LEFT: 0px"
				cellspacing="2" cellpadding="2" width="816" border="0">
				<tr>
					<td class="cabecera" style="HEIGHT: 22px">&nbsp;APROBACION DE HORAS EXTRAS</td>
				</tr>
				<tr valign="top">
					<td class="panel">
						<table id="Table23" style="WIDTH: 100%; HEIGHT: 120px" cellspacing="0" cellpadding="0">
							<tr>
								<td class="Etiqueta" style="WIDTH: 446px; HEIGHT: 16px"><center>&nbsp;A) SEGUIMIENTO</center>
								</td>
								<td class="Etiqueta" style="WIDTH: 26px">&nbsp;&nbsp;&nbsp;&nbsp;</td>
								<td class="Etiqueta" style="HEIGHT: 16px">
									<center>B) ÁREA</center>
								</td>
							</tr>
							<tr>
								<td style="WIDTH: 446px"><nobr><asp:datagrid id="dgSeguimiento" runat="server" Width="500px" AutoGenerateColumns="False">
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
										</asp:datagrid></nobr></td>
								<td style="WIDTH: 26px">&nbsp;&nbsp;</td>
								<td>
									<table cellspacing="0" cellpadding="0" width="100%">
										<tr>
											<td>
												<center><asp:label id="lblDesArea" runat="server" CssClass="input" Font-Size="11px" Font-Bold="True"
														BackColor="#FFFF80"></asp:label></center>
											</td>
										</tr>
										<tr>
											<td>&nbsp;</td>
										</tr>
										<tr>
											<td>
												<center><asp:button id="btnAprobar" runat="server" CssClass="Boton" Text="Aprobar" Width="118px" BackColor="#006600"></asp:button>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
													<asp:button id="btnRechazar" runat="server" CssClass="Boton" Text="Rechazar" Width="117px" BackColor="Red"></asp:button></center>
											</td>
										</tr>
										<tr>
											<td><asp:TextBox ID="txtobservacion" runat="server" CssClass="input" Width="99%" Height="37px" MaxLength="100"
													TextMode="MultiLine"></asp:TextBox>
											</td>
										</tr>
									</table>
									<asp:label id="lblMensaje" runat="server" CssClass="input"></asp:label>
								</td>
							</tr>
							<tr>
								<td class="Etiqueta" colspan="3" height="19"><center>&nbsp;C) REGISTROS DE SOLICITUD</center>
								</td>
							</tr>
						</table>
						<table>
							<tr valign="top">
								<td><asp:datagrid id="DataGrid1" runat="server" Width="880px" AutoGenerateColumns="False">
										<FooterStyle CssClass="GridFooter"></FooterStyle>
										<AlternatingItemStyle CssClass="GridAlternateItem"></AlternatingItemStyle>
										<ItemStyle CssClass="GridItem"></ItemStyle>
										<HeaderStyle CssClass="GridHeader"></HeaderStyle>
										<Columns>
											<asp:TemplateColumn HeaderText="Tarea">
												<HeaderStyle HorizontalAlign="Center" Width="120px"></HeaderStyle>
												<ItemStyle HorizontalAlign="Center"></ItemStyle>
												<ItemTemplate>
													<input id="hdnCodigoI" style="WIDTH: 32px; HEIGHT: 22px" type="hidden" size="1" runat="server" />
													<asp:label id="lblTareaI" runat="server" CssClass="Input" Width="100%" text='<%# Container.DataItem("chr_destarea") %>'>
													</asp:label>
												</ItemTemplate>
											</asp:TemplateColumn>
											<asp:TemplateColumn HeaderText="CTC">
												<HeaderStyle HorizontalAlign="Center" Width="70px"></HeaderStyle>
												<ItemStyle HorizontalAlign="Center"></ItemStyle>
												<ItemTemplate>
													<asp:label id="lblCtcI" runat="server" Width="100%" CssClass="Input" text='<%# Container.DataItem("vch_codctc") %>'>
													</asp:label>
												</ItemTemplate>
											</asp:TemplateColumn>
											<asp:TemplateColumn HeaderText="Fecha">
												<HeaderStyle HorizontalAlign="Center" Width="80px"></HeaderStyle>
												<ItemStyle HorizontalAlign="Center"></ItemStyle>
												<ItemTemplate>
													<asp:label id="lblFechaI" runat="server" Width="80px" CssClass="Input" text='<%# Container.DataItem("chr_fecha_sol") %>'>
													</asp:label>
												</ItemTemplate>
												<FooterTemplate>
													&nbsp;
												</FooterTemplate>
												<EditItemTemplate>
													&nbsp;
												</EditItemTemplate>
											</asp:TemplateColumn>
											<asp:TemplateColumn HeaderText="Inicio (aprox)">
												<HeaderStyle HorizontalAlign="Center" Width="50px"></HeaderStyle>
												<ItemStyle HorizontalAlign="Center"></ItemStyle>
												<ItemTemplate>
													<asp:label id="lblInicioAprox" runat="server" Width="100%" CssClass="Input" text='<%# Container.DataItem("vch_inicioaprox") %>'>
													</asp:label>
												</ItemTemplate>
											</asp:TemplateColumn>
											<asp:TemplateColumn HeaderText="Trabajador">
												<HeaderStyle HorizontalAlign="Center" Width="200px"></HeaderStyle>
												<ItemTemplate>
													<asp:label id="lblTrabDesI" runat="server" CssClass="Input" Width="100%" text='<%# Container.DataItem("vch_destrabajador") %>'>
													</asp:label>
												</ItemTemplate>
											</asp:TemplateColumn>
											<asp:TemplateColumn HeaderText="Horas Solici.">
												<HeaderStyle HorizontalAlign="Center" Width="50px"></HeaderStyle>
												<ItemStyle HorizontalAlign="Center"></ItemStyle>
												<ItemTemplate>
													<center>
														<asp:label id="lblHESolI" runat="server" Width="100%" CssClass="Input" Visible="False" text='<%# Container.DataItem("tin_horext_sol") %>'>
														</asp:label>
														<asp:TextBox id="txtHESolI" runat="server" Width="100%" CssClass="InputDerecha" Visible="False" text='<%# Container.DataItem("tin_horext_sol") %>' Height="20px">
														</asp:TextBox>
													</center>
												</ItemTemplate>
											</asp:TemplateColumn>
											<asp:TemplateColumn HeaderText="Horas Apro.">
												<HeaderStyle HorizontalAlign="Center" Width="50px"></HeaderStyle>
												<ItemStyle HorizontalAlign="Center"></ItemStyle>
												<ItemTemplate>
													<center>
														<asp:label id="lblHEAprI" runat="server" Width="100%" CssClass="Input" Visible="False" text='<%# Container.DataItem("tin_horext_apr") %>'>
														</asp:label>
														<asp:TextBox id="txtHEAprI" runat="server" Width="100%" CssClass="InputDerecha" Visible="False" text='<%# iif(Container.DataItem("tin_horext_apr")=0,Container.DataItem("tin_horext_sol"),Container.DataItem("tin_horext_apr")) %>' Height="20px">
														</asp:TextBox></center>
												</ItemTemplate>
											</asp:TemplateColumn>
											<asp:TemplateColumn HeaderText="Motivo">
												<HeaderStyle HorizontalAlign="Center" Width="600px"></HeaderStyle>
												<ItemTemplate>
													<asp:label id="lblObservacionI" runat="server" CssClass="Input" Width="100%" text='<%# Container.DataItem("vch_observacion") %>'>
													</asp:label>
												</ItemTemplate>
											</asp:TemplateColumn>
										</Columns>
									</asp:datagrid></td>
							</tr>
						</table>
						<input id="HDN2" style="WIDTH: 32px; HEIGHT: 22px" type="hidden" size="1" name="HDN2" runat="server" />
						<input id="HDN1" style="WIDTH: 32px; HEIGHT: 22px" type="hidden" size="1" name="HDN1" runat="server" />&nbsp;
						<input id="HDN4" style="WIDTH: 32px; HEIGHT: 21px" type="hidden" size="1" name="HDN4" runat="server" />
						<input id="HDN3" style="WIDTH: 32px; HEIGHT: 22px" type="hidden" size="1" name="HDN3" runat="server" />
						<input id="HDN5" style="WIDTH: 32px; HEIGHT: 22px" type="hidden" size="1" name="HDN5" runat="server" />
						<span id="toolTipBox" width="200px"></span>
					</td>
				</tr>
			</table>
		</form>
	</body>
</html>
