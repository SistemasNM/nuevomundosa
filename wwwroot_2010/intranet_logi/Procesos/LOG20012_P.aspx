<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="LOG20012_P.aspx.vb" Inherits="intranet_logi.LOG20012_P" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title></title>
    <base target="_self" />
    <link href="../css/NM0001.css" type="text/css" rel="stylesheet"/>
    <script type="text/javascript" language="javascript" src="../../intranet/JS/jsCalendario_N4.js"></script>
	<script type="text/javascript" language="javascript" src="../../intranet/JS/functions.js"></script>
    <style type="text/css">
        .style1
        {
            height: 8px;
            width: 96px;
        }
        .style2
        {
            height: 8px;
            width: 34px;
        }
        .style3
        {
            border-width: thin;
            font-weight: bold;
            font-size: 14px;
            width: 774px;
            color: #ffffff;
            font-family: Verdana;
            background-color: #2A3A6E;
            height: 22px;
        }
        .style4
        {
            width: 324px;
        }
    </style>
</head>
<body>
<form id="frmRequisicion" name="frmRequisicion" method="post" runat="Server">
			<table id="Table4" style="LEFT: 0px; WIDTH: 592px;TOP: 0px; HEIGHT: 29px" border="0">
					<tr>
						<td class="style3">&nbsp;IMPRESION DE CODIGO DE BARRAS DE ARTICULOS</td>
					</tr>
			</table>
			<table style="WIDTH: 583px">
				<tr>
					<td class="Etiqueta" style="WIDTH: 103px">&nbsp;Código :&nbsp;</td>
					<td colspan="2">&nbsp;<asp:TextBox id="txtcodarticulo" runat="server" Width="198px" 
                            CssClass="input" MaxLength="50"></asp:TextBox></td>
					<td class="style2">&nbsp;</td>
				</tr>
				<tr>
					<td class="Etiqueta" style="WIDTH: 103px">&nbsp;Artículo.&nbsp;:</td>
					<td colspan="2">&nbsp;<asp:TextBox id="txtdesarticulo" runat="server" Width="412px" 
                            CssClass="input" MaxLength="50"></asp:TextBox></td>
					<td class="style2">&nbsp;
						</td>
				</tr>
				<tr>
					<td class="Etiqueta" style="WIDTH: 103px">&nbsp;Codigo&nbsp;de&nbsp;barra&nbsp;:</td>
					<td colspan="2">&nbsp;<asp:DropDownList ID="ddlcodigobarra" runat="server" CssClass="input" 
                            Width="197px">
                        </asp:DropDownList>
                    </td>
					<td class="style2">&nbsp;</td>
				</tr>
				<tr>
					<td class="Etiqueta" style="WIDTH: 103px">&nbsp;Ubicación&nbsp;:</td>
					<td class="style4">
                        &nbsp;<asp:TextBox id="txtUbicacion" runat="server" Width="100px" CssClass="input" ></asp:TextBox>
                    </td>
					<td class="style1">
						&nbsp;</td>
					<td class="style2">
						&nbsp;</td>
				</tr>
				<tr>
					<td class="Etiqueta" style="WIDTH: 103px">&nbsp;Cantidad&nbsp;:</td>
					<td class="style4">&nbsp;<asp:TextBox id="txtcant" runat="server" 
                            Width="100px" CssClass="input" MaxLength="3"></asp:TextBox></td>
					<td class="style1">
						<asp:button id="btnimprimir" runat="server" CssClass="Boton" Text="Imprimir"></asp:button></td>
					<td class="style2">
						&nbsp;</td>
				</tr>
			</table>
</form>
<OBJECT id="BlitzPrinter" style="WIDTH: 0px; HEIGHT: 0px" codeBase="Blitz.CAB#version=1,0,0,37"
	classid="CLSID:31EE91E3-C9E8-405D-9F71-2B0DBACEAD76" VIEWASTEXT>
	<PARAM NAME="_ExtentX" VALUE="0">
	<PARAM NAME="_ExtentY" VALUE="0">
</OBJECT>
<script language="javascript" type="text/javascript">
    function imprimir_etiqueta() {
    
    }//end function
</script>    
</body>
</html>
