<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="frm_GeneraInventarioTelas.aspx.vb" Inherits="intranet_logi.frm_GeneraInventarioTelas" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
		<title>Genera Inventario de Telas</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR"/>
		<meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE"/>
		<meta content="JavaScript" name="vs_defaultClientScript"/>
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema"/>
		<link href="../css/NM0001.css" type="text/css" rel="stylesheet"/>
		<script language="javascript" src="../../intranet/JS/functions.js" type="text/javascript"></script>
		<script language="javascript" type="text/javascript">
        src="../../intranet/JS/functions.js"></script>
	    <style type="text/css">
            .style1
            {
                font-weight: bold;
                font-size: 9px;
                color: #333333;
                background-color: #BCCAE0;
                font-family: Verdana;
                text-decoration: none;
                width: 103px;
            }
        </style>
	</head>
	<body>
		<form id="frmRequisicion" name="frmRequisicion" method="post" runat="Server">
			&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
			<table id="Table4" style="Z-INDEX: 101; LEFT: 0px; POSITION: absolute; TOP: 0px" cellspacing="2"
				cellpadding="2" width="720" height="16" border="0">
				<TBODY>
					<tr>
						<td class="Cabecera" style="HEIGHT: 28px">GENERAR INVENTARIO DE TELAS</td>
					</tr>
					<tr>
						<td class="panel">
							<table style="WIDTH: 720px; HEIGHT: 16px">
					</tr>
			</table>
			<table style="WIDTH: 247px; HEIGHT: 40px">
				<tr>
					<td style="WIDTH: 2px; HEIGHT: 8px" width="2">&nbsp;</td>
					<td style="HEIGHT: 8px">&nbsp;
						&nbsp;
						<asp:button id="btnEjecutar" runat="server" CssClass="Boton" Text="Ejecutar"></asp:button></td>
				</tr>
			</table>
			<table>
				<tr>
					<td>&nbsp;</td>
				</tr>
			</table>
	    </form>
	</body>
</html>
