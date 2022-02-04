<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="frm_RegistroAdjuntoIQ.aspx.vb" Inherits="intranet_logi.frm_RegistroAdjuntoIQ" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html>
	<head>
	<title>CERTIFICADO DE INSUMOS QUIMICOS</title>
		<base target="_self"/>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR"/>
		<meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE"/>
		<meta content="JavaScript" name="vs_defaultClientScript"/>
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema"/>
		<link href="../css/NM0001.css" type="text/css" rel="stylesheet"/>
		
        <script type="text/javascript" language="javascript">
		    function fnc_Cerrar() {
		        window.close();
		    }

		    function fnc_AdjuntarArchivo() {
		        var lstrDestino = document.all('hdnDestinoGuardar').value;
		        var retorno = window.showModalDialog("../../intranet/Generales/NMM00002.aspx?pstrDestino=" + lstrDestino + "&pstrSubfijo=ctc&pstrAceptaObs=1&pstrAceptaDes=1", "", "dialogHeight:250px;dialogWidth:550px;center:yes;help:no;");

		        if (retorno != "" && retorno != ":") {
		            var datos = retorno.split(":");
		            var lstrArchivo = datos[0];

		            if (lstrArchivo.length > 0) {
		                frm_RegistroAdjuntoIQ.hdnArchivo.value = lstrArchivo;
		                frm_RegistroAdjuntoIQ.hdnDescri.value = datos[1];
		                frm_RegistroAdjuntoIQ.hdnObserva.value = datos[2];
		                return true;
		            }
		        }
		        return false;
		    }

		    function fnc_AbrirDocumento(pstrDocumento, pstrVentana) {
		        window.open(pstrDocumento, "Adjunto");
		    }
		
		</script>

	</head>
	<body>
		<form id="frm_RegistroAdjuntoIQ" method="post" runat="server">
			<center>
				<table id="Table1" cellspacing="0" cellpadding="0" width="700" border="0">
					<tr>
						<td class="cabecera" style="HEIGHT: 27px">LISTA DE CERTIFICADO DE IQ:</td>
					</tr>
					<tr>
						<td align="center">
							<table id="Table2" cellspacing="0" cellpadding="0" border="0">
								<tr>
									<td>
										<table cellpadding="0" cellspacing="0" border="0" width="700" bgcolor="#ffff66">
											<tr>
												<td width="600" height="30"><asp:Label id="lblPeriodoIQ" runat="server" 
                                                        BackColor="#FFFF66"></asp:Label></td>
											</tr>
										</table>
									</td>
								</tr>
								<tr>
									<td vAlign="top" align="center">
										<br>
										<asp:DataGrid id="dgArchivo" runat="server" Width="700px" AutoGenerateColumns="False" BackColor="White">
											<AlternatingItemStyle CssClass="GridAlternateItem"></AlternatingItemStyle>
											<Columns>
												<asp:TemplateColumn HeaderText="">
													<HeaderStyle Width="10px" CssClass="GridHeader"></HeaderStyle>
													<ItemTemplate>
														<asp:ImageButton id="imgTipoArchivoI" runat="server" Width="16px" Height="16px"></asp:ImageButton>
													</ItemTemplate>
												</asp:TemplateColumn>

												<asp:TemplateColumn HeaderText="Archivo">
													<HeaderStyle Width="150px" CssClass="GridHeader"></HeaderStyle>
													<ItemTemplate>
                                                        <asp:Label id="lblTipoAdjunto" runat="server" Font-Size="9px" Text='<%# DataBinder.Eval(Container, "DataItem.chr_TipoArchivo") %>' visible ="false"></asp:Label>
                                                        <asp:Label id="lblCertificado" runat="server" Font-Size="9px" Text='<%# DataBinder.Eval(Container, "DataItem.var_Certificado") %>' visible ="true"></asp:Label>
													</ItemTemplate>
												</asp:TemplateColumn>

												<asp:BoundColumn DataField="var_Observaciones" HeaderText="Observaci&#243;n">
													<HeaderStyle CssClass="GridHeader"></HeaderStyle>
												</asp:BoundColumn>
                                                												
											</Columns>
										</asp:DataGrid></td>
								</tr>
							</table> <!--TABLA 2--></td>
					</tr>
					<tr>
						<td class="pie"><input id="hdnArchivo" style="WIDTH: 20px; HEIGHT: 22px" type="hidden" size="0" name="hdnArchivo"
								runat="server"><input id="hdnDescri" style="WIDTH: 20px; HEIGHT: 22px" type="hidden" size="0" name="hdnDescri"
								runat="server"><input id="hdnObserva" style="WIDTH: 20px; HEIGHT: 22px" type="hidden" size="0" name="hdnObserva"
								runat="server">
							<asp:button id="btnAgregar" runat="server" Width="96px" Text="Agregar Archivo" CssClass="boton"></asp:button>&nbsp;
							<input id="hdnPeriodoIQ" style="WIDTH: 20px; HEIGHT: 22px" type="hidden" size="0" name="hdnCodigoPeriodoIQ"
								runat="server"><input id="hdnDestinoAbrir" style="WIDTH: 20px; HEIGHT: 22px" type="hidden" size="0" name="hdnDestinoAbrir"
								runat="server"><input id="hdnDestinoGuardar" style="WIDTH: 20px; HEIGHT: 22px" type="hidden" size="0"
								name="hdnDestinoGuardar" runat="server">
							<asp:button id="btnCerrar" runat="server" Text="Cerrar" CssClass="boton"></asp:button>
						</td>
					</tr>
				</table>
			</center>
		</form>
	</body>
</html>
