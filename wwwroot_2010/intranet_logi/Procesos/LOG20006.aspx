<%@ Page Language="vb" AutoEventWireup="false" Codebehind="LOG20006.aspx.vb" Inherits="intranet_logi.LOG20006"%>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html>
	<head>
		<title>LOG20006</title>
		<base target="_self"/>
		<meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1"/>
		<meta name="CODE_LANGUAGE" content="Visual Basic .NET 7.1"/>
		<meta name="vs_defaultClientScript" content="JavaScript"/>
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5"/>
		<LINK rel="stylesheet" type="text/css" href="../css/NM0001.css"/>
		<script language="javascript" type="text/javascript">
		    // funcion Ver Reporte de Analisis
		    function popUp(strUrl) 
		    {
			    var intWidth = screen.width;
			    var intHeight = screen.height;
			    window.open(strUrl, "", "top=0; left=0; width=" + intWidth + "; height=" + intHeight + "; resizable=1;");
		    }
		    function VerAdjunto (Orden , Articulo)
		    {
			    var retorno = window.showModalDialog("LOG20010.aspx?Orden="+Orden+"&Articulo="+Articulo,"","dialogHeight:300px;dialogWidth:550px;center:yes;help:no;");
		    }//end function
		
		    function Historial (OCOS)
		    {
			    var retorno = window.showModalDialog("LOG20007.aspx?&strRequisicion="+OCOS,"","dialogHeight:300px;dialogWidth:500px;center:yes;help:no;");
		    }
		    returnValue = "";
		    function Aprobar(Valor)
		    {
			    returnValue = Valor;
			    window.close();
		    }//end function
			
		    function ConfirmarEli()
		    {
			    if (confirm("Está seguro de aprobar la requisicion."))
			    {
				    return true;	
			    }else{
				    return false;
			    }
		    }//end function
			
		    function popUp(strUrl) 
		    {
			    var intWidth = screen.width;
			    var intHeight = screen.height;
			    //window.open(strUrl);
			    window.open(strUrl, "", "top=0; left=0; width=" + intWidth + "; height=" + intHeight + "; resizable=1;");
		    }//end function
			
		    function fnc_estadisticosxarticulo(pstrarticulo)
		    {
			    document.all('hdnarticuloestadistica').value=pstrarticulo;
			    document.all('hdnposicionpanel').value=event.clientY+10;
			    document.all['pnldatosestadisticos'].style.display='inline';
			    //document.all['pnldatosestadisticos'].style.top=event.clientY+20;
			    return true;
		    }//end function
	
	
		    function fnc_ocultarestadisticos()
		    {
			    document.all('hdnarticuloestadistica').value='';
			    document.all['pnldatosestadisticos'].style.display='none';
		    }//end function
		    function fnc_Desaprueba()
		    {
			    alert('Por favor ingrese una observacion (en el campo otras observaciones) para la Desaprobacion.');
			    document.all('txtObservaciones2').focus();
			}

			//Lista fnc_AdjuntarDocs
			function fnc_ListarDocsAdjuntos() {
			    var pstrTipoDoc = "RQS"
			    var pstrNumeroDoc = document.all('hdnNumReq').value
			    var pstrSecuencia = " "
			    var retorno = window.showModalDialog("frm_ListadoArchivoAdjunto.aspx?pstrTipoDoc=" + pstrTipoDoc + "&pstrNumeroDoc=" + pstrNumeroDoc + "&pstrSecuencia=" + pstrSecuencia, "Listar Adjuntos", "dialogHeight:400px;dialogWidth:720px;center:yes;help:no;");
			    return false;
			}

		</script>
	</head>
	<body>
		<form id="Form1" method="post" runat="server">
			<table style="Z-INDEX: 101; POSITION: absolute; TOP: 8px; LEFT: 8px" id="Table2" border="0"
				cellspacing="2" cellpadding="2" width="724" align="center">
				<tr>
					<td class="Cabecera">ORDEN DE COMPRA</td>
				</tr>
				<tr>
					<td class="panel">
						<table id="Table4" border="0" cellspacing="0" cellpadding="0" width="600">
							<tr>
								<td style="WIDTH: 300px">
									<table style="WIDTH: 300px" id="Table3" border="0">
										<tr>
											<td style="WIDTH: 90px" class="Etiqueta">Número</td>
											<td>
                                             <asp:textbox id="txtDocumento" runat="server" Width="214px" CssClass="input"></asp:textbox>
                                             <input style="WIDTH: 10px; HEIGHT: 10px" id="hdnNumReq" size="1" type="hidden" name="hdnNumReq" runat="server"/>
                                            </td>
										</tr>
									</table>
									<table style="WIDTH: 300px" id="tblCentroCosto" border="0" runat="server">
										<tr>
											<td style="WIDTH: 90px" class="Etiqueta">Almacén</td>
											<td><asp:textbox id="txtAlmacen" runat="server" Width="214px" CssClass="input"></asp:textbox></td>
										</tr>
										<tr>
											<td style="WIDTH: 90px" class="Etiqueta">Moneda</td>
											<td><asp:textbox id="txtMoneda" runat="server" Width="214px" CssClass="input"></asp:textbox></td>
										</tr>
										<tr>
											<td style="WIDTH: 90px" class="Etiqueta">Proveedor</td>
											<td><asp:textbox id="txtProveedor" runat="server" Width="214px" CssClass="input"></asp:textbox></td>
										</tr>
										<tr>
											<td style="WIDTH: 90px" class="Etiqueta">Condición</td>
											<td><asp:textbox id="txtCondicion" runat="server" Width="214px" CssClass="input"></asp:textbox></td>
										</tr>
									</table>
									<table style="WIDTH: 300px" id="Table6" border="0">
										<tr>
											<td style="WIDTH: 79px" class="Etiqueta" valign="top">Observaciones</td>
											<td><asp:textbox id="txtObservaciones" runat="server" Width="214px" CssClass="input" Height="40px" TextMode="MultiLine"></asp:textbox></td>
										</tr>
										<tr>
											<td style="WIDTH: 79px" class="Etiqueta" valign="top">Otras Observaciones:</td>
											<td><asp:textbox style="Z-INDEX: 0" id="txtObservaciones2" runat="server" Width="214px" CssClass="input"
													Height="40px" TextMode="MultiLine"></asp:textbox></td>
										</tr>
									</table>
								</td>
								<td style="WIDTH: 300px">&nbsp;&nbsp;</td>
								<td style="WIDTH: 300px" valign="top" align="left"><asp:label id="lblCreacion" runat="server" Width="100%">Label</asp:label><asp:label id="lblTitulo" runat="server" Width="368px" CssClass="Etiqueta" Font-Size="Small">Seguimiento</asp:label>
									<DIV style="WIDTH: 432px; HEIGHT: 109px"><asp:datagrid id="dtgSeguimiento" runat="server" Width="416px" AutoGenerateColumns="False">
											<AlternatingItemStyle CssClass="gridalternateitem"></AlternatingItemStyle>
											<ItemStyle CssClass="griditem"></ItemStyle>
											<HeaderStyle CssClass="gridheader"></HeaderStyle>
											<Columns>
												<asp:BoundColumn DataField="var_Estado" HeaderText="Estado"></asp:BoundColumn>
												<asp:BoundColumn DataField="var_Usuario" HeaderText="Usuario"></asp:BoundColumn>
												<asp:BoundColumn DataField="var_Fecha" HeaderText="Fecha"></asp:BoundColumn>
											</Columns>
										</asp:datagrid></DIV>
									<table style="Z-INDEX: 0; WIDTH: 432px; HEIGHT: 28px">
										<tr>
											<td></td>
											<td align="right"><asp:button style="Z-INDEX: 0" id="btnAnalisis" runat="server" Width="112px" CssClass="boton"
													Text="Analisis Detalle"></asp:button></td>
										</tr>
									</table>
									<P><asp:label id="lblMsgDesaprobacion" runat="server" ForeColor="Red" Font-Bold="True"></asp:label></P>
									<P><asp:label style="Z-INDEX: 0" id="lblError" runat="server" ForeColor="Black" Font-Bold="True"></asp:label></P>
                                    
								</td>
							</tr>
						</table>
						<DIV style="WIDTH: 734px; HEIGHT: 140px; OVERFLOW: auto"><asp:datagrid id="dtgDetalle" runat="server" Width="710px" AutoGenerateColumns="False">
								<AlternatingItemStyle CssClass="gridalternateitem"></AlternatingItemStyle>
								<ItemStyle CssClass="griditem"></ItemStyle>
								<HeaderStyle CssClass="gridheader"></HeaderStyle>
								<Columns>
									<asp:TemplateColumn>
										<HeaderStyle Width="3px"></HeaderStyle>
										<ItemTemplate>
											<asp:ImageButton id="ibtiestadisticas" runat="server" CommandName="cmd_estadisticas" ImageUrl="../../intranet/Imagenes/buscar16x16.gif"></asp:ImageButton>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:BoundColumn DataField="var_ArticuloCodigo" HeaderText="Item"></asp:BoundColumn>
									<asp:BoundColumn DataField="var_ArticuloNombre"></asp:BoundColumn>
									<asp:BoundColumn DataField="var_UnidadMedidaCodigo" HeaderText="U.M.">
										<HeaderStyle HorizontalAlign="Center"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center"></ItemStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="num_CantidadSolicitada" HeaderText="Cantidad" DataFormatString="{0:#,##0.00}">
										<HeaderStyle HorizontalAlign="Right"></HeaderStyle>
										<ItemStyle HorizontalAlign="Right"></ItemStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="num_Precio" HeaderText="Precio" DataFormatString="{0:#,##0.00}">
										<HeaderStyle HorizontalAlign="Right"></HeaderStyle>
										<ItemStyle HorizontalAlign="Right"></ItemStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="PC_DCT1" HeaderText="Dscto.1(%)" DataFormatString="{0:#,##0.00}">
										<HeaderStyle HorizontalAlign="center"></HeaderStyle>
										<ItemStyle HorizontalAlign="center"></ItemStyle>
									</asp:BoundColumn>
                                    <asp:BoundColumn DataField="PC_DCT2" HeaderText="Dscto.2(%)" DataFormatString="{0:#,##0.00}">
										<HeaderStyle HorizontalAlign="center"></HeaderStyle>
										<ItemStyle HorizontalAlign="center"></ItemStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="num_Total" HeaderText="SubTotal" DataFormatString="{0:#,##0.00}">
										<HeaderStyle HorizontalAlign="Right"></HeaderStyle>
										<ItemStyle HorizontalAlign="Right"></ItemStyle>
									</asp:BoundColumn>
									<asp:TemplateColumn>
										<ItemTemplate>
											<asp:ImageButton id="ibtAdjuntos" runat="server" CommandName="ADJUNTOS" ImageUrl="../../intranet/Imagenes/PaginasI.bmp"></asp:ImageButton>
										</ItemTemplate>
									</asp:TemplateColumn>
								</Columns>
							</asp:datagrid></DIV>
						<table id="Table1" border="0">
                             <tr>
								<td style="WIDTH: 586px; HEIGHT: 10px" class="Etiqueta" valign="top" align="right">IMPORTE BASE :</td>
								<td style="HEIGHT: 10px"><asp:textbox id="TxtBase" runat="server" CssClass="inputDerecha"></asp:textbox></td>
							</tr>
                            <tr>
								<td style="WIDTH: 586px; HEIGHT: 10px" class="Etiqueta" valign="top" align="right">FOB :</td>
								<td style="HEIGHT: 10px"><asp:textbox id="TxtFOB" runat="server" CssClass="inputDerecha"></asp:textbox></td>
							</tr>
                            <tr>
								<td style="WIDTH: 586px; HEIGHT: 10px" class="Etiqueta" valign="top" align="right">FLETE :</td>
								<td style="HEIGHT: 10px"><asp:textbox id="TxtFlete" runat="server" CssClass="inputDerecha"></asp:textbox></td>
							</tr>
                            <tr>
								<td style="WIDTH: 586px; HEIGHT: 10px" class="Etiqueta" valign="top" align="right">SEGURO :</td>
								<td style="HEIGHT: 10px"><asp:textbox id="TxtSeguro" runat="server" CssClass="inputDerecha"></asp:textbox></td>
							</tr>
							<tr>
								<td style="WIDTH: 586px; HEIGHT: 10px" class="Etiqueta" valign="top" align="right">
                                    SUB TOTAL&nbsp;:</td>
								<td style="HEIGHT: 10px"><asp:textbox id="txtSubTotal" runat="server" CssClass="inputDerecha"></asp:textbox></td>
							</tr>
							<tr>
								<td style="WIDTH: 586px; HEIGHT: 10px" class="Etiqueta" valign="top" align="right">
                                    (-)DSCTO. 
                                    :</td>
								<td style="HEIGHT: 10px"><asp:textbox id="txtDescuento" runat="server" CssClass="inputDerecha"></asp:textbox></td>
							</tr>
							<tr>
								<td style="WIDTH: 586px; HEIGHT: 10px" class="Etiqueta" valign="top" align="right">I.G.V.</td>
								<td style="HEIGHT: 10px"><asp:textbox id="txtIGV" runat="server" CssClass="inputDerecha"></asp:textbox></td>
							</tr>
							<tr>
								<td style="WIDTH: 586px; HEIGHT: 10px" class="Etiqueta" valign="top" align="right">TOTAL :</td>
								<td style="HEIGHT: 10px"><asp:textbox id="txtTotal" runat="server" CssClass="inputDerecha"></asp:textbox></td>
							</tr>
						</table>
						<table id="Table9" class="botonera" border="0" cellspacing="2" cellpadding="2" width="100%">
							<tr>
								<td align="right"><asp:button id="btnListaAdjuntos" runat="server" CssClass="boton" Text="Ver Adjuntos"></asp:button><asp:button id="btnHistorial" runat="server" CssClass="boton" Text="Historial"></asp:button><asp:button id="btnImprimir" runat="server" CssClass="boton" Text="Imprimir"></asp:button><asp:button id="btnAnular" runat="server" CssClass="boton" Text="Anular" Visible="False"></asp:button><asp:button style="Z-INDEX: 0" id="btnAprobar" runat="server" CssClass="boton" BackColor="#006600" Text="Aprobar"></asp:button><asp:button id="btnDesaprobar" runat="server" CssClass="boton" Text="Desaprobar" BackColor="Red"></asp:button><asp:button id="btnRechazar" runat="server" CssClass="boton" Text="Rechazar" Visible="False"></asp:button></td>
							</tr>
						</table>
					</td>
				</tr>
			</table>
			<asp:panel style="Z-INDEX: 102; POSITION: absolute; TOP: 608px; LEFT: 136px" id="pnldatosestadisticos"
				BorderStyle="Solid" BorderWidth="8px" runat="server" Width="624px" CssClass="GridItem"
				BorderColor="#336699">
				<table style="WIDTH: 600px" class="GridItem" width="600">
					<tr>
						<td style="WIDTH: 42.79%; HEIGHT: 19px" class="cabecera" height="19" colspan="2">&nbsp;ULTIMAS 
							6 COMPRAS</td>
						<td style="HEIGHT: 19px" align="right"><input style="WIDTH: 73px; HEIGHT: 19px" id="btnestcerrar" class="boton" onclick="fnc_ocultarestadisticos();"
								value="Cerrar" type="button" name="btnestcerrar" /></td>
					</tr>
					<tr>
						<td class="GridItem" valign="top" colspan="3">
							<asp:datagrid id="dgestcompras" runat="server" Width="592px" AutoGenerateColumns="False">
								<AlternatingItemStyle CssClass="gridalternateitem"></AlternatingItemStyle>
								<ItemStyle CssClass="griditem"></ItemStyle>
								<HeaderStyle CssClass="gridheader"></HeaderStyle>
								<Columns>
									<asp:BoundColumn DataField="no_cort_prov" HeaderText="Proveedor">
										<HeaderStyle Width="350px"></HeaderStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="fe_emis" HeaderText="Fecha" DataFormatString="{0:d}">
										<HeaderStyle Width="90px"></HeaderStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="ca_orde" HeaderText="Cant." DataFormatString="{0:N0}">
										<HeaderStyle Width="50px"></HeaderStyle>
										<ItemStyle HorizontalAlign="Right"></ItemStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="co_mone" HeaderText="T.M.">
										<HeaderStyle Width="20px"></HeaderStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="im_unit" HeaderText="P.Compra" DataFormatString="{0:F2}">
										<HeaderStyle Width="100px"></HeaderStyle>
										<ItemStyle HorizontalAlign="Right"></ItemStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="im_cost_movi" HeaderText="P.Costo(US$)" DataFormatString="{0:F2}">
										<HeaderStyle Width="100px"></HeaderStyle>
										<ItemStyle HorizontalAlign="Right"></ItemStyle>
									</asp:BoundColumn>
								</Columns>
							</asp:datagrid></td> <!--DataFormatString="{0:F2}--></tr>
					<tr>
						<td class="cabecera" height="19" colspan="3">&nbsp;CONSUMO DE LOS ULTIMOS&nbsp;6 
							MESES :</td>
					</tr>
					<tr>
						<td style="WIDTH: 187px" class="GridItem" valign="top">
							<asp:datagrid id="dgestconsumo" runat="server" Width="248px" AutoGenerateColumns="False">
								<AlternatingItemStyle CssClass="gridalternateitem"></AlternatingItemStyle>
								<ItemStyle CssClass="griditem"></ItemStyle>
								<HeaderStyle CssClass="gridheader"></HeaderStyle>
								<Columns>
									<asp:BoundColumn DataField="ano_mes" HeaderText="A&#241;o - Mes">
										<HeaderStyle Width="190px"></HeaderStyle>
										<ItemStyle HorizontalAlign="Left"></ItemStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="total" HeaderText="Cantidad" DataFormatString="{0:F2}">
										<HeaderStyle Width="100px"></HeaderStyle>
										<ItemStyle HorizontalAlign="Right"></ItemStyle>
									</asp:BoundColumn>
								</Columns>
							</asp:datagrid></td>
						<td style="WIDTH: 269px" class="GridItem" valign="top">
							<table style="WIDTH: 264px; HEIGHT: 50px">
								<tr>
									<td style="WIDTH: 121px" class="Etiqueta">&nbsp;Stock actual&nbsp;:&nbsp;</td>
									<td>
										<asp:TextBox id="txteststockactual" runat="server" Width="88px" CssClass="input"></asp:TextBox></td>
								</tr>
								<tr>
									<td style="WIDTH: 121px" class="Etiqueta">&nbsp;Consumo promedio&nbsp;:&nbsp;</td>
									<td>
										<asp:TextBox id="txtestconsumopromedio" runat="server" Width="88px" CssClass="input"></asp:TextBox></td>
								</tr>
							</table>
						</td>
					</tr>
				</table>
				<input style="WIDTH: 32px; HEIGHT: 8px" id="hdnarticuloestadistica" size="1" type="hidden"
					name="hdnarticuloestadistica" runat="server" /><input style="WIDTH: 32px; HEIGHT: 8px" id="hdnposicionpanel" size="1" type="hidden" name="hdnposicionpanel"
					runat="server" />
            </asp:panel>
        </form>
	</body>
</html>
