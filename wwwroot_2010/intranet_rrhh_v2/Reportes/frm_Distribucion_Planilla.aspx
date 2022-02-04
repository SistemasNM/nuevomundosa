<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="frm_Distribucion_Planilla.aspx.vb" Inherits="intranet_rrhh.frm_Distribucion_Planilla" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
	<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR" />
		<meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE" />
		<meta content="JavaScript" name="vs_defaultClientScript" />
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema" />
		<link href="../../intranet/Estilos/NM0001.css" type="text/css" rel="stylesheet" />
         <link href="../Styles/sytle.css" type="text/css" rel="stylesheet"/>
        <script language="javascript" type="text/javascript" src="../../intranet/JS/jsCalendario_N3.js"></script>
		<script type="text/javascript" language="javascript">


		    function fMostrarReporte(strURL) {
		        var intWidth = screen.width;
		        var intHeight = screen.height;
		        window.open(strURL, "", "top=0; left=0; width=" + intWidth + "; height=" + intHeight + "; resizable=1;");
		    }
		</script>
	</head>
	<body >
		<form id="Form1" method="post" runat="server">
			<table id="Table1" style="Z-INDEX: 101; LEFT: 8px; POSITION: absolute; TOP: 8px; HEIGHT: 32px"
				cellspacing="2" cellpadding="2" width="100%" border="0">
				<tr>
					<td class="titulo" style="width:800px; height:20px" align="center">DISTRIBUCION DE PLANILLAS</td>
				</tr>
				<tr>
					<td class="panel" rowspan="2">
						<table id="Table4" style="WIDTH: 472px; HEIGHT: 35px" cellspacing="1" cellpadding="1" width="472"
							border="0">
							<tr>
								<td class="Etiqueta" style="WIDTH: 41px; HEIGHT: 11px">Año</td>
								<td style="WIDTH: 145px; HEIGHT: 11px"><asp:textbox id="txtAnio" runat="server" 
                                        Font-Size="XX-Small" Font-Names="Verdana" Width="53px"
										Height="16px" MaxLength="4"></asp:textbox></td>
								<td class="Etiqueta" style="WIDTH: 31px; HEIGHT: 11px">Mes</td>
								<td style="HEIGHT: 11px"><asp:dropdownlist id="ddlMes" runat="server" Font-Size="XX-Small" Font-Names="Verdana" Width="106px"
										Height="16px">
										<asp:ListItem Value="01">ENERO</asp:ListItem>
										<asp:ListItem Value="02">FEBRERO</asp:ListItem>
										<asp:ListItem Value="03">MARZO</asp:ListItem>
										<asp:ListItem Value="04">ABRIL</asp:ListItem>
										<asp:ListItem Value="05">MAYO</asp:ListItem>
										<asp:ListItem Value="06">JUNIO</asp:ListItem>
										<asp:ListItem Value="07">JULIO</asp:ListItem>
										<asp:ListItem Value="08">AGOSTO</asp:ListItem>
										<asp:ListItem Value="09">SEPTIEMBRE</asp:ListItem>
										<asp:ListItem Value="10">OCTUBRE</asp:ListItem>
										<asp:ListItem Value="11">NOVIEMBRE</asp:ListItem>
										<asp:ListItem Value="12">DICIEMBRE</asp:ListItem>
									</asp:dropdownlist></td>
							</tr>
							<tr>
								<td class="Etiqueta" style="WIDTH: 41px">Planilla</td>
								<td style="WIDTH: 145px"><asp:dropdownlist id="ddlPlanilla" runat="server" Font-Size="XX-Small" Font-Names="Verdana" Width="106px"
										Height="16px">
										<asp:ListItem Value="EMP">EMPLEADOS</asp:ListItem>
										<asp:ListItem Value="PRA">PRACTICANTES</asp:ListItem>
										<asp:ListItem Value="OBM">OBREROS MES</asp:ListItem>
									</asp:dropdownlist></td>
								<td style="WIDTH: 31px"></td>
								<td></td>
							</tr>
						</table>
						<table class="botonera" id="Table3" style="HEIGHT: 16px" cellspacing="2" cellpadding="2"
							width="100%" border="0">
							<tr>
								<td align="right"><asp:button id="btnVer" runat="server" Text="Consulta" 
                                        CssClass="boton"></asp:button>&nbsp;</td>
							</tr>
						</table>
					</td>
				</tr>
			</table>
			&nbsp;
		</form>
	</body>
</html>
