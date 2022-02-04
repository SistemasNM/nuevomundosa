<%@ Page Language="vb" AutoEventWireup="false" Codebehind="LOG30002.aspx.vb" Inherits="intranet_logi.LOG30002"%>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html>
	<head>
		<title>LOG30002</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR"/>
		<meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE"/>
		<meta content="JavaScript" name="vs_defaultClientScript"/>
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema"/>
		<link href="../css/NM0001.css" type="text/css" rel="stylesheet"/>
		<script language="javascript" type="text/javascript" src="../../intranet/JS/jsCalendario_N3.js"></script>
		<script language="javascript" type="text/javascript">
			function BlanquearArea()
			{
				if (document.getElementById("txtAreaCodigo").value == '')
				{
					document.getElementById("HDN1").value='';
					document.getElementById("lblAreaNombre").innerHTML='';
				}
				//alert('asdads');
				Form1.submit();
				
			}
			function BlanquearCC()
			{
				if (document.getElementById("txtCentroCostoCodigo").value == '')
				{
					document.getElementById("HDN2").value='';
					document.getElementById("lblCentroCostoNombre").innerHTML = '';
				}
				//alert('asdads');
			}			
			function popUp(strUrl) 
			{
				var intWidth = screen.width;
				var intHeight = screen.height;
				//window.open(strUrl);
				window.open(strUrl, "", "top=0; left=0; width=" + intWidth + "; height=" + intHeight + "; resizable=1;");
			}	
		</script>
	</head>
	<body>
		<form id="Form1" method="post" runat="server">
			&nbsp;
			<table id="Table2" style="Z-INDEX: 101; LEFT: 8px; WIDTH: 100%; POSITION: absolute; TOP: 8px"
				cellspacing="2" cellpadding="2" border="0">
				<tr>
					<td class="Cabecera">Salidas de almacén</td>
				</tr>
				<tr>
					<td class="panel">
						<table id="Table1" cellspacing="0" cellpadding="0" width="600" border="0">
							<tr>
								<td class="Etiqueta" style="HEIGHT: 28px" width="105">Area</td>
								<td style="HEIGHT: 30px" colspan="3">
									<table id="Table3" cellspacing="0" cellpadding="0" width="100%" border="0">
										<tr>
											<td width="102" height="18"><asp:textbox id="txtAreaCodigo" runat="server" Font-Size="XX-Small" Font-Names="Verdana" Height="19px"
													Width="100%"></asp:textbox></td>
											<td style="WIDTH: 25px" width="25"><input class="boton" id="btnBuscarArea" style="WIDTH: 19px; HEIGHT: 19px" onclick="javascript:BuscarArea()"
													type="button" value="..." name="btnBuscar12" /></td>
											<td style="TEXT-INDENT: 2px" width="369"><asp:label id="lblAreaNombre" runat="server" Font-Size="XX-Small" Font-Names="Verdana" Width="359px"></asp:label></td>
										</tr>
									</table>
								</td>
							</tr>
							<tr>
								<td class="Etiqueta" height="24">Centro Costo
								</td>
								<td colspan="3">
									<table id="Table4" cellspacing="0" cellpadding="0" width="100%" border="0">
										<tr>
											<td width="102" height="18"><asp:textbox id="txtCentroCostoCodigo" runat="server" Font-Size="XX-Small" Font-Names="Verdana"
													Height="19px" Width="100%"></asp:textbox></td>
											<td width="24"><input class="boton" id="btnBuscarCC" style="WIDTH: 19px; HEIGHT: 19px" onclick="javascript:BuscarCC()"
													type="button" value="..." name="btnBuscar1" /></td>
											<td style="TEXT-INDENT: 2px" width="369"><asp:label id="lblCentroCostoNombre" runat="server" Font-Size="XX-Small" Font-Names="Verdana"
													Width="359px"></asp:label></td>
										</tr>
									</table>
								</td>
							</tr>
							<tr>
								<td class="Etiqueta">Desde</td>
								<td width="198"><asp:textbox id="txtFechaInicial" runat="server" 
										Height="19px" Width="104px"></asp:textbox>&nbsp;<img onclick="popUpCalendar(this, Form1.txtFechaInicial, 'dd/mm/yyyy')" height="15" alt="Seleccionar fecha"
										src="../../intranet/Imagenes/Calendario.gif" width="13" border="0">
								</td>
								<td class="Etiqueta" width="91">Hasta</td>
								<td width="206"><asp:textbox id="txtFechaFinal" runat="server" Height="19px" Width="104px"></asp:textbox>&nbsp;<img onclick="popUpCalendar(this, Form1.txtFechaFinal, 'dd/mm/yyyy')" height="15" alt="Seleccionar fecha"
										src="../../intranet/Imagenes/Calendario.gif" width="13" border="0">
								</td>
							</tr>
						</table>
						<table id="Table5" style="HEIGHT: 8px" cellspacing="2" cellpadding="2" width="100%" border="0">
							<tr>
								<td class="botonera" align="right" width="100%">
                                <input id="HDN1" type="hidden" runat="server" />
                                <input runat="server" id="HDN2" type="hidden" />
                                <asp:button id="btnResumido" runat="server" Height="24px" Width="72px" Visible="False" Text="Resumido" CssClass="Boton" Font-Bold="True" ></asp:button>
                                <asp:button id="btnExportar" runat="server" Height="24px" Width="80px" Visible="False" Text="Exportar" CssClass="Boton" Font-Bold="True"></asp:button>
                                <asp:button id="btnVisualizar" runat="server" Height="24px" Width="71px" Text="Ver reporte" CssClass="Boton" Font-Bold="True"></asp:button>
                                </td>
							</tr>
						</table>
					</td>
				</tr>
			</table>
			<script type="text/javascript" language="javascript">
			function BuscarArea()
					{
						var retorno = window.showModalDialog("../../intranet/Buscadores/LOG_0002.aspx","","dialogHeight:450px;dialogWidth:450px;center:yes;help:no;");
						if (retorno!="" && retorno!=":")
						{
							//alert(retorno);
							var datos = retorno.split(":");
							var Codigo = datos[0];
							var Nombre = datos[1];
							//var txtAreaCodigo.value = Codigo;
						//	var hdnCodigoCliente = document.all(OutPut2);
							document.all('txtAreaCodigo').value = Codigo;
							document.all('lblAreaNombre').innerHTML = Nombre;
							document.getElementById("HDN1").value=Nombre;
						//	document.frmMain.submit(); 
						}
					}	
			function BuscarCC()
					{
						var retorno = window.showModalDialog("../../intranet/Buscadores/LOG_0001.aspx","","dialogHeight:450px;dialogWidth:450px;center:yes;help:no;");
						if (retorno!="" && retorno!=":")
						{
							//alert(retorno);
							var datos = retorno.split(":");
							var Codigo = datos[0];
							var Nombre = datos[1];
							//var txtAreaCodigo.value = Codigo;
						//	var hdnCodigoCliente = document.all(OutPut2);
							document.all('txtCentroCostoCodigo').value = Codigo;
							document.all('lblCentroCostoNombre').innerHTML = Nombre;
							document.getElementById("HDN2").value=Nombre;
						//	document.frmMain.submit(); 
						}
					}	
			</script>
		</form>
	</body>
</html>
