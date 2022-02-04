<%@ Page Language="vb" AutoEventWireup="false" Codebehind="frm_AprobacionMuestras.aspx.vb" Inherits="intranet_logi.frm_AprobacionMuestras" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html>
	<head>
		<title>LOG20005</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR"/>
		<meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE"/>
		<meta content="JavaScript" name="vs_defaultClientScript"/>
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema"/>
		<link href="../css/NM0001.css" type="text/css" rel="stylesheet"/>
		<script language="javascript" src="../../intranet/JS/functions.js" type="text/javascript"></script>
		<script language="javascript" type="text/javascript">
		    function VerDetalle(Solicitud)
			{
				var retorno = window.showModalDialog("frm_DetalleAprobacionMuestras.aspx?&strNumeroSolicitud="+Solicitud,"","dialogHeight:520px;dialogWidth:790px;center:yes;help:no;");
				if (retorno == "1") {
				    //document.getElementById("hdn1").value="1";
				    return true;
				} else {
				    return false;
                }
            }

		    function VerHistorial (Solicitud)
			{
				var retorno = window.showModalDialog("LOG20007.aspx?&strRequisicion="+OCOS,"","dialogHeight:300px;dialogWidth:500px;center:yes;help:no;");
			}//end function
			
			function fnc_aprobarmasivo(pstr_id,pstr_documento)
			{
				//alert(pstr_id +" - "+pstr_documento);
				var lstr_valores=document.all('hdnaprobarmasivo').value;
				//alert(document.all(pstr_id+'_chkaprobari').checked);
				if(document.all(pstr_id+'_chkaprobari').checked==true)
				{
					lstr_valores=lstr_valores+pstr_documento+',';
				}else{
					pstr_documento=pstr_documento+',';
					lstr_valores=replaceAll( lstr_valores, pstr_documento, '' );
				}//end if
				document.all('hdnaprobarmasivo').value=lstr_valores;
				//alert(document.all('hdnaprobarmasivo').value);
			}//end function
		</script>
	</head>
	<body>
		<form id="Form1" method="post" runat="server">
			<table id="table1" style="Z-INDEX: 101; LEFT: 0px; WIDTH: 872px; POSITION: absolute; TOP: 0px; HEIGHT: 429px"
				cellspacing="2" cellpadding="2" width="872" border="0">
				<tr>
					<td class="Cabecera" style="HEIGHT: 28px">APROBACION DE SOLICITUD DE MUESTRAS</td>
				</tr>
				<tr>
					<td class="Panel">
						<table id="tblFil1" cellspacing="0" cellpadding="0" width="300" border="0" runat="server">
							<tr>
								<td class="Etiqueta" width="90">Tipos</td>
								<td width="210"></td>
							</tr>
						</table>
						<table id="tblFil2" cellspacing="0" cellpadding="0" width="600" border="0" runat="server">
							<tr>
								<td class="Etiqueta" valign="top" width="90">Observaciones</td>
								<td width="510"></td>
							</tr>
						</table>
						<table class="botonera" id="tblBotonera1" cellspacing="0" cellpadding="0" width="100%"
							border="0" runat="server">
							<tr>
								<td align="left"><asp:button id="btnaprobarmasivo" runat="server" CssClass="Boton" Text="Aprobar Masivo" Width="124px"></asp:button>&nbsp;<input id="hdn1" style="WIDTH: 33px; HEIGHT: 13px" type="hidden" size="1" name="hdn1" runat="server"><input id="hdnaprobarmasivo" style="WIDTH: 33px; HEIGHT: 13px" type="hidden" size="1" name="hdnaprobarmasivo"
										runat="server"></td>
								<td align="right"><asp:button id="btnConsultar" runat="server" CssClass="Boton" Text="Consultar"></asp:button></td>
							</tr>
						</table>
						<DIV style="WIDTH: 100%; HEIGHT: 300px"><asp:datagrid id="dtgLista" runat="server" AutoGenerateColumns="False" Width="100%">
								<AlternatingItemStyle CssClass="GridAlternateItem"></AlternatingItemStyle>
								<ItemStyle CssClass="GridItem"></ItemStyle>
								<HeaderStyle CssClass="gridheader"></HeaderStyle>
								<Columns>
									<asp:TemplateColumn HeaderText="Apr.">
										<ItemTemplate>
											<asp:CheckBox id="chkaprobari" runat="server"></asp:CheckBox>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn>
										<ItemTemplate>
											<asp:ImageButton id="ibtBuscar" runat="server" ImageUrl="../../intranet/Imagenes/buscar.gif"></asp:ImageButton>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:BoundColumn DataField="var_GrupoAprobacionNombre" HeaderText="Grupo de aprobaci&#243;n"></asp:BoundColumn>
									<asp:BoundColumn DataField="var_TipoAprobacionNombre" HeaderText="Tipo de aprobaci&#243;n"></asp:BoundColumn>
									<asp:BoundColumn DataField="var_NumeroSolicitud" HeaderText="Numero Solicitud"></asp:BoundColumn>
									<asp:BoundColumn DataField="num_TotalMetros" HeaderText="Metros" DataFormatString="{0:#,##0.00}">
										<HeaderStyle HorizontalAlign="Right"></HeaderStyle>
										<ItemStyle HorizontalAlign="Right"></ItemStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="var_FechaCreacion" HeaderText="Fecha Creaci&#243;n"></asp:BoundColumn>
									<asp:BoundColumn DataField="var_NombreCliente" HeaderText="Cliente"></asp:BoundColumn>
									<asp:TemplateColumn>
										<ItemTemplate>
											<asp:ImageButton id="ibtHistorial" runat="server" ImageUrl="../../intranet/Imagenes/Historial.JPG"></asp:ImageButton>
										</ItemTemplate>
									</asp:TemplateColumn>
								</Columns>
							</asp:datagrid></DIV>
						<table class="botonera" id="table4" cellspacing="0" cellpadding="0" width="100%" border="0">
							<tr>
								<td class="Etiqueta" style="FONT-SIZE: small; WIDTH: 625px" align="right">Total de 
									documentos por aprobar :
								</td>
								<td align="right"><asp:label id="lblCantidad" runat="server" CssClass="Etiqueta" Width="63px" Font-Size="Small"></asp:label></td>
								<td align="right">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
								</td>
							</tr>
						</table>
					</td>
				</tr>
			</table>
		</form>
	</body>
</html>
