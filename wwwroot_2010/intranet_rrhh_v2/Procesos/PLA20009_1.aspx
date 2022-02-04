<%@ Page Language="vb" AutoEventWireup="false" Codebehind="PLA20009_1.aspx.vb" Inherits="intranet_rrhh_v2.PLA20009_1"%>
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
		function Aprobar(Valor1,Valor2,Valor3)
		{
			//Valor1 0-NO GRABA,1-SI GRABA
			returnValue = Valor1+"|"+Valor2+"|"+Valor3;
			window.close();
		}//end function
		
		function txtinicioaprox_onBlur()
		{
			var ltxtrecinicio=document.all('txtinicioaprox');
			if(trim(ltxtrecinicio.value)=='')
			{
				ltxtrecinicio.value='00:00';
			}//end if
		}//end function
		
		</script>
	</head>
	<body>
		<form id="frmHESolicitud" name="frmHESolicitud" method="post" runat="Server">
			<table>
				<tr>
					<td class="Etiqueta" colspan="2">&nbsp;DATOS ADICIONALES&nbsp;</td>
				</tr>
				<tr>
					<td class="Etiqueta" width="80">&nbsp;Hora Inicio (aprox):&nbsp;</td>
					<td><asp:textbox id="txtinicioaprox" runat="server" CssClass="input" MaxLength="5" Width="96px">00:00</asp:textbox></td>
				</tr>
				<tr>
					<td class="Etiqueta">&nbsp;Nº CTC:&nbsp;</td>
					<td><asp:textbox id="txtcodctc" runat="server" CssClass="input" MaxLength="15" Width="96px"></asp:textbox>&nbsp;
						<asp:button id="btnbuscarctc" runat="server" CssClass="Boton" Width="21px" Height="19px" Text="..."></asp:button></td>
				</tr>
				<tr>
					<td class="Etiqueta"></td>
					<td><asp:textbox id="txtdesctc" runat="server" CssClass="input" Width="368px" Height="56px" 
							TextMode="MultiLine"></asp:textbox></td>
				</tr>
				<tr>
					<td align="right" colspan="2"><asp:button id="btnguardar" runat="server" CssClass="Boton" Width="98px" Text="Guardar"></asp:button></td>
				</tr>
			</table>
		</form>
	</body>
</html>
