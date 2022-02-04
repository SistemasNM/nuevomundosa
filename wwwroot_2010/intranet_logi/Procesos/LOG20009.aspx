<%@ Page Language="vb" AutoEventWireup="false" Codebehind="LOG20009.aspx.vb" Inherits="intranet_logi.SIS200031"%>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html>
  <head>
		<title>LOG20009</title>
		<base target="_self"/>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR"/>
		<meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE"/>
		<meta content="JavaScript" name="vs_defaultClientScript"/>
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema"/>        
        <link href="../css/NM0001.css" rel="stylesheet" type="text/css" />
        <link href="../css/tab.webfx.css" rel="stylesheet" type="text/css" />				
        <script src="../js/tabpane.js" type="text/javascript"></script>

		<script language="javascript" type="text/javascript">
		    // funcion Ver Reporte de Analisis
		    function popUp1(strUrl) 
		    {
			    var intWidth = screen.width;
			    var intHeight = screen.height;
			    window.open(strUrl, "", "top=0; left=0; width=" + intWidth + "; height=" + intHeight + "; resizable=1;");
		    }
		
		    function popUp(strUrl) 
		    {
			    var intWidth = screen.width;
			    var intHeight = screen.height;
			    //window.open(strUrl);
			    window.open(strUrl, "", "top=0; left=0; width=" + intWidth + "; height=" + intHeight + "; resizable=1;");
			} //end function	

		    function ListarProveedores(ClientID , ID)
		    {
			    var retorno = window.showModalDialog("../../intranet/Buscadores/LOG_0003.aspx?strTipo=1","","dialogHeight:450px;dialogWidth:450px;center:yes;help:no;");
			    if (retorno!="" && retorno!=":")
			    {
				    var datos = retorno.split(":");
				    var codigo = datos[0];
				    var nombre = datos[1];
				    var txtCodigo = document.all(ClientID + "_txtProveedorCodigo" + ID + "E");
				    var lblNombre = document.all(ClientID + "_lblProveedorNombre" + ID + "E");
				    var hdnProveedor = document.all("hdnProveedor" + ID);
				    //alert(hdnProveedor.value);
				    txtCodigo.value = codigo;
				    lblNombre.innerHTML = nombre;
				    hdnProveedor.value = nombre;
				    //alert(hdnProveedor.value);
			    }//end if
            } //end function

		    function btnEnviarOC_onClick()
		    {
			    //verificar datos
			    var lstrNumero=document.all('txtDocumento2').value;
			    if(lstrNumero=='')
			    {
				    alert('La orden de servicio -- ' + lstrNumero + ' -- no existe.');
				    return false;
			    }//end if
			    //datos de OC
			    var response=SIS200031.fnc_VerificarOC_paraEnvio(lstrNumero);
			    if(response!=null && typeof(response)=="object")
			    {
				    var ldtbResultado=response.value;
				    if(ldtbResultado.Rows.length<=0)
				    {
					    alert('La orden de servicio -- '+ lstrNumero +' -- no existe.');
				    }//end if
				    if(ldtbResultado.Rows[0].ti_situ!='APR')
				    {
					    alert('La orden de servicio -- '+lstrNumero+' -- no ha sido aprobada.');
					    return false;
				    }//end if
				    if(ldtbResultado.Rows[0].prv_de_mail=='')
				    {
					    alert('La cuenta de correo del proveedor no ha sido configurado en el maestro de proveedores.');
					    return false;
				    }//end if
				    if(ldtbResultado.Rows[0].de_mail_envi=='')//primer envio
				    {
					    return confirm('La orden de servicio -- '+ lstrNumero +' -- se enviará a la cuenta del proveedor\n-- '+ldtbResultado.Rows[0].prv_de_mail + ' --.\n\n¿ Esta seguro de continuar ?');
				    }
				    else//reenvio
				    {
					    return confirm('La orden de servicio -- '+ lstrNumero +' -- se enviará a la cuenta del proveedor -- '+ldtbResultado.Rows[0].prv_de_mail + ' --.\n\nEsta orden de servicio se envió el ' + ldtbResultado.Rows[0].fe_usua_envi2 + ' por el usuario '+ ldtbResultado.Rows[0].co_usua_envi +'\n\n¿ Esta seguro de continuar ?');
				    }//end if
				    //mensaje de aceptacion
			    }
			    else
			    {
				    alert('La orden de servicio no existe.');
				    return false;
			    }//end if
		    }//end function
		
		    function fnc_estadisticosxarticulo(pstrarticulo)
		    {

			    document.all('hdnarticuloestadistica').value=pstrarticulo;
			    document.all('hdnposicionpanel').value=event.clientY-30;
			    //document.all['pnldatosestadisticos'].style.display='inline';
			    document.getElementById('pnldatosestadisticos').style.display='inline';
			    return true;
		    }//end function
	
		    function fnc_ocultarestadisticos()
		    {
			    document.all('hdnarticuloestadistica').value='';
			    document.all['pnldatosestadisticos'].style.display='none';
			} //end function

			function fnc_RegistraDocsAdjuntos() {
			    var pstrTipoDoc = "OCM";
			    var pstrNumeroDoc = document.getElementById('hdnCodigoDoc').value;
			    var retorno = window.showModalDialog("frm_AdjuntarArchivo_OC.aspx?pstrTipoDoc=OCM&pstrNumeroDoc=" + pstrNumeroDoc + "&pstrSecuencia=0", "Adjuntos archivo", "dialogHeight:240px;dialogWidth:650px;center:yes;help:no;");
			    //alert("PRueba");

			    return true;
			}

			function fnc_AbrirDocumento(pstrDocumento, pstrVentana) {
			    window.open(pstrDocumento, "Adjunto");
			}
		</script>
</head>
	<body>
		<form id="Form1" method="post" runat="server">
			<table id="Table1" style="Z-INDEX: 101; POSITION: absolute; TOP: 0px; LEFT: 0px" cellspacing="2" cellpadding="2" width="100%" border="0">
				<tr>
					<td class="Cabecera"></td>
				</tr>
				<tr>
					<td class="Input" style="BORDER-BOTTOM: lightgrey 2px solid; BORDER-LEFT: lightgrey 2px solid; BACKGROUND-COLOR: lightyellow; COLOR: dimgray; BORDER-TOP: lightgrey 2px solid; BORDER-RIGHT: lightgrey 2px solid"><%=Observaciones%></td>
				</tr>
				<tr>
					<td>
						<div class="tab-pane" id="tab-pane-1">
							<div class="tab-page" id="tabpage10" runat="server">
								<h2 class="tab">Seguimiento</h2>
								<table style="WIDTH: 712px; HEIGHT: 115px">
									<tr>
										<td><asp:datagrid id="dtgSeguimiento" runat="server" AutoGenerateColumns="False" Width="648px">
												<AlternatingItemStyle CssClass="gridalternateitem"></AlternatingItemStyle>
												<ItemStyle CssClass="griditem"></ItemStyle>
												<HeaderStyle CssClass="gridheader"></HeaderStyle>
												<Columns>
													<asp:BoundColumn DataField="var_Tipodoc" HeaderText="T.D.">
														<HeaderStyle Width="20px"></HeaderStyle>
													</asp:BoundColumn>
													<asp:BoundColumn DataField="var_Estado" HeaderText="Estado">
														<HeaderStyle Width="150px"></HeaderStyle>
													</asp:BoundColumn>
													<asp:BoundColumn DataField="var_Usuario" HeaderText="Usuario">
														<HeaderStyle Width="300px"></HeaderStyle>
													</asp:BoundColumn>
													<asp:BoundColumn DataField="var_Fecha" HeaderText="Fecha">
														<HeaderStyle Width="200px"></HeaderStyle>
													</asp:BoundColumn>
												</Columns>
											</asp:datagrid>&nbsp;</td>
									</tr>
								</table>
							</div>
							<div class="tab-page" id="Div1" runat="server">
								<h2 class="tab">Requisición de <%=TipoRequisicion%>
								</h2>
								<table style="HEIGHT: 24px" width="100%">
									<tr>
										<td>
											<table id="Table3" width="660" border="0">
												<tr>
													<td class="Etiqueta" width="85">Número</td>
													<td width="120"><asp:textbox id="txtDocumento" BorderWidth="0px" runat="server" BackColor="#CDE0EF" CssClass="input"></asp:textbox></td>
													<td width="440"></td>
												</tr>
											</table>
											<table id="Table4" width="660" border="0">
												<tr>
													<td class="Etiqueta" width="85">Fecha Emisión</td>
													<td width="120"><asp:textbox id="txtEmision" BorderWidth="0px" runat="server" BackColor="#CDE0EF" CssClass="input"></asp:textbox></td>
													<td class="Etiqueta" width="85">Fecha Ini. Trabajo
													</td>
													<td width="120"><asp:textbox id="txtFechaTope" BorderWidth="0px" runat="server" BackColor="#CDE0EF" CssClass="input"></asp:textbox></td>
                                                    <td class="Etiqueta" width="85">Fecha Fin Trabajo
													</td>
													<td width="120"><asp:textbox id="txtFechaExpi" BorderWidth="0px" runat="server" BackColor="#CDE0EF" CssClass="input"></asp:textbox></td>													
												</tr>
                                                <tr>
                                                    <td class="Etiqueta" width="85">Comprador</td>
													<td width="139"><asp:textbox id="txtComprador" BorderWidth="0px" runat="server" Width="132px" BackColor="#CDE0EF"
															CssClass="input"></asp:textbox></td>
                                                </tr>
											</table>
											<table id="tblCentroCosto" width="660" border="0" runat="server">
												<tr>
													<td class="Etiqueta" width="85">Centro de costo
													</td>
													<td width="565"><asp:textbox id="txtCentroCosto" BorderWidth="0px" runat="server" Width="559px" BackColor="#CDE0EF"
															CssClass="input"></asp:textbox></td>
												</tr>
											</table>
											<table id="Table5" width="660" border="0">
												<tr>
													<td class="Etiqueta" width="85">Unidad</td>
													<td width="120"><asp:textbox id="txtUnidad" BorderWidth="0px" runat="server" BackColor="#CDE0EF" CssClass="input"></asp:textbox></td>
													<td class="Etiqueta" width="85">Almacén</td>
													<td width="352"><asp:textbox id="txtAlmacen" BorderWidth="0px" runat="server" Width="344px" BackColor="#CDE0EF"
															 CssClass="input"></asp:textbox></td>
												</tr>
											</table>
											<table id="Table6" width="660" border="0">
												<tr>
													<td class="Etiqueta" vAlign="top" width="85">Observaciones</td>
													<td width="565"><asp:textbox id="txtObservaciones" BorderWidth="0px" runat="server" Width="559px" BackColor="#CDE0EF"
															CssClass="input" TextMode="MultiLine" Height="38px"></asp:textbox></td>
												</tr>
											</table>
											<asp:datagrid id="dtgDetalleRequisicion" runat="server" AutoGenerateColumns="False" Width="800px">
												<AlternatingItemStyle CssClass="gridalternateitem"></AlternatingItemStyle>
												<ItemStyle CssClass="griditem"></ItemStyle>
												<HeaderStyle CssClass="gridheader"></HeaderStyle>
												<Columns>
													<asp:BoundColumn DataField="var_Secuencia" ReadOnly="True"></asp:BoundColumn>
													<asp:BoundColumn DataField="var_ArticuloCodigo" ReadOnly="True" HeaderText="Art&#237;culo"></asp:BoundColumn>
													<asp:BoundColumn DataField="var_Descripcion" ReadOnly="True"></asp:BoundColumn>
													<asp:BoundColumn DataField="var_Solicitado" ReadOnly="True" HeaderText="Solicitado" DataFormatString="{0:F2}">
														<HeaderStyle HorizontalAlign="Right"></HeaderStyle>
														<ItemStyle HorizontalAlign="Right"></ItemStyle>
													</asp:BoundColumn>
													<asp:TemplateColumn HeaderText="Aprobado">
														<HeaderStyle HorizontalAlign="Right"></HeaderStyle>
														<ItemStyle HorizontalAlign="Right"></ItemStyle>
														<ItemTemplate>
															<asp:Label id="lblAprobadoI" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.var_Aprobado") %>'>
															</asp:Label>
														</ItemTemplate>
														<EditItemTemplate>
															<asp:TextBox id="txtAprobadoE" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.var_Aprobado") %>'>
															</asp:TextBox>
														</EditItemTemplate>
													</asp:TemplateColumn>
													<asp:BoundColumn Visible="False" DataField="var_Porcentaje" ReadOnly="True" HeaderText="%">
														<HeaderStyle HorizontalAlign="Right"></HeaderStyle>
														<ItemStyle HorizontalAlign="Right"></ItemStyle>
													</asp:BoundColumn>
													<asp:BoundColumn DataField="var_OrdenServicio" ReadOnly="True" HeaderText="Activo"></asp:BoundColumn>
													<asp:BoundColumn DataField="var_CodigoAuxiliar" ReadOnly="True" HeaderText="Centro Costo"></asp:BoundColumn>
													<asp:BoundColumn DataField="var_CuentaGasto" ReadOnly="True" HeaderText="Cuenta Gasto"></asp:BoundColumn>
                                                    <asp:BoundColumn DataField="COD_OT" ItemStyle-Width="70px" ReadOnly="True" HeaderText="# OT"></asp:BoundColumn>
                                                    <asp:BoundColumn DataField="DE_ORTR" ItemStyle-Width="80px" ReadOnly="True" HeaderText="DESC. OT"></asp:BoundColumn>
                                                    <asp:BoundColumn DataField="CO_RESP" ReadOnly="True" HeaderText="COD. RESPONSABLE"></asp:BoundColumn>
                                                    <asp:BoundColumn DataField="DE_RESP" ReadOnly="True" HeaderText="DESC. RESPONSABLE"></asp:BoundColumn>
												</Columns>
											</asp:datagrid></td>
									</tr>
									<tr>
										<td align="right"><asp:button id="btnImprimir" runat="server" CssClass="boton" Text="Imprimir"></asp:button></td>
									</tr>
								</table>
							</div>
							<div class="tab-page" id="tabpage21" runat="server">
								<h2 class="tab">Orden de
									<%=TipoOrden%>
								</h2>
								<table width="100%">
									<tr>
										<td>
											<table id="Table2" cellspacing="0" cellpadding="0" width="600" border="0">
												<tr>
													<td style="WIDTH: 300px">
														<table id="Table7" style="WIDTH: 336px; HEIGHT: 22px" border="0">
															<tr>
																<td class="Etiqueta" style="WIDTH: 82px">Número</td>
																<td><asp:textbox id="txtDocumento2" BorderWidth="0px" runat="server" Width="214px" BackColor="#CDE0EF"
																		CssClass="input"></asp:textbox></td>
															</tr>
														</table>
														<table id="Table8" style="WIDTH: 336px; HEIGHT: 22px" border="0" runat="server">
															<tr>
																<td class="Etiqueta" style="WIDTH: 82px">Almacén</td>
																<td><asp:textbox id="txtAlmacen2" BorderWidth="0px" runat="server" Width="214px" BackColor="#CDE0EF"
																		CssClass="input"></asp:textbox></td>
															</tr>
														</table>
														<table id="Table9" style="WIDTH: 336px; HEIGHT: 42px" border="0">
															<tr>
																<td class="Etiqueta" style="WIDTH: 81px" vAlign="top">Observaciones</td>
																<td><asp:textbox id="txtObservaciones2" BorderWidth="0px" runat="server" Width="235px" BackColor="#CDE0EF"
																		CssClass="input" TextMode="MultiLine" Height="34px"></asp:textbox></td>
															</tr>
														</table>
													</td>
													<td style="WIDTH: 300px" vAlign="top" colspan="2">
														<table id="Table17" style="WIDTH: 392px; HEIGHT: 22px" border="0">
															<tr>
																<td class="Etiqueta" style="WIDTH: 90px">Proveedor</td>
																<td><asp:textbox id="txtocmproveedor" BorderWidth="0px" runat="server" Width="280px" BackColor="#CDE0EF"
																		CssClass="input"></asp:textbox></td>
															</tr>
														</table>
														<table id="Table18" style="WIDTH: 392px; HEIGHT: 22px" border="0">
															<tr>
																<td class="Etiqueta" style="WIDTH: 90px">Moneda</td>
																<td><asp:textbox id="txtocmmoneda" BorderWidth="0px" runat="server" Width="264px" BackColor="#CDE0EF"
																		CssClass="input"></asp:textbox></td>                                                                
															</tr>
                                                            <tr>
                                                                <td class="Etiqueta" style="WIDTH: 90px">Fecha Cre.</td>
																<td><asp:textbox id="txtFecCrea" BorderWidth="0px" runat="server" Width="264px" BackColor="#CDE0EF"
																		CssClass="input" Enabled="false"></asp:textbox></td>
                                                            </tr>
                                                            <tr>
                                                                <td class="Etiqueta" style="WIDTH: 90px">Fecha Ini. Trabajo</td>
																<td><asp:textbox id="txtFecIni" BorderWidth="0px" runat="server" Width="264px" BackColor="#CDE0EF"
																		CssClass="input" Enabled="false"></asp:textbox></td>
                                                            </tr>
                                                            <tr>
                                                                <td class="Etiqueta" style="WIDTH: 90px">Fecha Fin Trabajo</td>
																<td><asp:textbox id="txtFecFin" BorderWidth="0px" runat="server" Width="264px" BackColor="#CDE0EF"
																		CssClass="input" Enabled="false"></asp:textbox></td>
                                                            </tr>
														</table>

													</td>
													<!--<td style="WIDTH: 300px"></td>--></tr>
											</table>
											<div style="WIDTH: 734px; HEIGHT: 140px; OVERFLOW: auto"><asp:datagrid id="dtgDetalleOrden" runat="server" AutoGenerateColumns="False" Width="710px">
													<AlternatingItemStyle CssClass="gridalternateitem"></AlternatingItemStyle>
													<ItemStyle CssClass="griditem"></ItemStyle>
													<HeaderStyle CssClass="gridheader"></HeaderStyle>
													<Columns>
														<asp:BoundColumn DataField="var_ArticuloCodigo" HeaderText="Item"></asp:BoundColumn>
														<asp:BoundColumn DataField="var_ArticuloNombre"></asp:BoundColumn>
														<asp:BoundColumn DataField="var_UnidadMedidaCodigo" HeaderText="Moneda">
															<HeaderStyle HorizontalAlign="Center"></HeaderStyle>
															<ItemStyle HorizontalAlign="Center"></ItemStyle>
														</asp:BoundColumn>
														<asp:BoundColumn DataField="num_CantidadSolicitada" HeaderText="Cantidad" DataFormatString="{0:#,##0.00}">
															<HeaderStyle HorizontalAlign="Right"></HeaderStyle>
															<ItemStyle HorizontalAlign="Right"></ItemStyle>
														</asp:BoundColumn>
														<asp:BoundColumn DataField="num_Precio" HeaderText="Precio" DataFormatString="{0:#,##0.0000}">
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
																<asp:ImageButton id="ibtiestadisticas" runat="server" ImageUrl="../../intranet/Imagenes/buscar16x16.gif"  CommandName="cmd_estadisticas"></asp:ImageButton>
															</ItemTemplate>
														</asp:TemplateColumn>
													</Columns>
												</asp:datagrid>
                                            </div>
											<asp:panel class="GridItem" id="pnldatosestadisticos" BorderStyle="Solid" BorderWidth="8px"
												runat="server" Width="624px" BorderColor="#336699" style="Z-INDEX: 102; POSITION: absolute; TOP: 928px; LEFT: 20px; display: none">
                                              <table style="WIDTH: 600px" id="Table19" class="GridItem" width="600">
                                                 <tr>
                                                    <td style="WIDTH: 42.79%; HEIGHT: 19px" class="cabecera" height="19" colspan="2">&nbsp;ULTIMAS 6 COMPRAS</td>
                                                    <td style="HEIGHT: 19px" align="right">
                                                       <input style="WIDTH: 73px; HEIGHT: 19px" id="btnestcerrar" class="boton" onclick="fnc_ocultarestadisticos();" value="Cerrar" type="button" name="btnestcerrar" />
                                                    </td>
                                                 </tr>
                                                 <tr>
                                                    <td class="GridItem" valign="top" colspan=3>
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
															</asp:datagrid>
                                                    </td><!--DataFormatString="{0:F2}-->
                                                 </tr>
                                                 <tr>
                                                    <td class="cabecera" height="19" colspan="3">&nbsp;CONSUMO DE LOS ULTIMOS&nbsp;6 MESES :
                                                    </td>
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
															</asp:datagrid>
                                                    </td>
                                                   <td style="WIDTH: 269px" class="GridItem" valign="top">
                                                       <table style="WIDTH: 264px; HEIGHT: 50px" id="Table20" >
                                                          <tr>
                                                             <td style="WIDTH: 121px" class="Etiqueta">&nbsp;Stock actual&nbsp;:&nbsp;</td>
                                                             <td><asp:TextBox id="txteststockactual" runat="server" Width="88px" CssClass="input" ></asp:TextBox></td>
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
                                    
                                    <input style="WIDTH: 32px; HEIGHT: 8px" id="hdnarticuloestadistica" size="1" type="hidden" name="hdnarticuloestadistica" runat="server" />
                                    <input style="WIDTH: 32px; HEIGHT: 8px" id="hdnposicionpanel" size="1" type="hidden" name="hdnposicionpanel" runat="server" />
                                   </asp:panel>
											<table id="Table10" style="WIDTH: 736px; HEIGHT: 64px" border="0">
                                            <tr>
													<td class="Input" style="WIDTH: 586px; HEIGHT: 10px; FONT-WEIGHT: bold" valign="top"
														align="right">IMPORTE BASE:</td>
													<td style="HEIGHT: 10px"><asp:textbox id="TxtBase" BorderWidth="0px" runat="server" BackColor="#CDE0EF" CssClass="inputDerecha"></asp:textbox></td>
												</tr>
                                                <tr>
													<td class="Input" style="WIDTH: 586px; HEIGHT: 10px; FONT-WEIGHT: bold" valign="top"
														align="right">FOB :</td>
													<td style="HEIGHT: 10px"><asp:textbox id="TxtFOB" BorderWidth="0px" runat="server" BackColor="#CDE0EF" CssClass="inputDerecha"></asp:textbox></td>
												</tr>
                                                <tr>
													<td class="Input" style="WIDTH: 586px; HEIGHT: 10px; FONT-WEIGHT: bold" valign="top"
														align="right">FLETE :</td>
													<td style="HEIGHT: 10px"><asp:textbox id="TxtFlete" BorderWidth="0px" runat="server" BackColor="#CDE0EF" CssClass="inputDerecha"></asp:textbox></td>
												</tr>
                                                <tr>
													<td class="Input" style="WIDTH: 586px; HEIGHT: 10px; FONT-WEIGHT: bold" valign="top"
														align="right">SEGURO :</td>
													<td style="HEIGHT: 10px"><asp:textbox id="TxtSeguro" BorderWidth="0px" runat="server" BackColor="#CDE0EF" CssClass="inputDerecha"></asp:textbox></td>
												</tr>
                                                <tr>
													<td class="Input" style="WIDTH: 586px; HEIGHT: 10px; FONT-WEIGHT: bold" valign="top"
														align="right">(-)DSCTO :</td>
													<td style="HEIGHT: 10px"><asp:textbox id="TxtDscto" BorderWidth="0px" runat="server" BackColor="#CDE0EF" CssClass="inputDerecha"></asp:textbox></td>
												</tr>
												<tr>
													<td class="Input" style="WIDTH: 586px; HEIGHT: 10px; FONT-WEIGHT: bold" valign="top"
														align="right">SUB TOTAL</td>
													<td style="HEIGHT: 10px"><asp:textbox id="txtSubTotal" BorderWidth="0px" runat="server" BackColor="#CDE0EF" CssClass="inputDerecha"></asp:textbox></td>
												</tr>
												<tr>
													<td class="Input" style="WIDTH: 586px; HEIGHT: 10px; FONT-WEIGHT: bold" valign="top" align="right">I.G.V.</td>
													<td style="HEIGHT: 10px"><asp:textbox id="txtIGV" BorderWidth="0px" runat="server" BackColor="#CDE0EF" CssClass="inputDerecha"></asp:textbox></td>
												</tr>
												<tr>
													<td class="Input" style="WIDTH: 586px; HEIGHT: 10px; FONT-WEIGHT: bold" valign="top" align="right">TOTAL</td>
													<td style="HEIGHT: 10px"><asp:textbox id="txtTotal" BorderWidth="0px" runat="server" BackColor="#CDE0EF" CssClass="inputDerecha"></asp:textbox></td>
												</tr>
											</table>
										</td>
									</tr>
									<tr>
										<td align="right">
											<input class="input" id="hdnocmproveedorcorreo" style="Z-INDEX: 0; WIDTH: 34px; HEIGHT: 18px"	type="hidden" size="1" name="hdnocmproveedorcorreo" runat="server" />
											<asp:Button ID="btnAnalisis" Runat="server" Text="Ver Analisis Mensual" tooltip="Clic para Ver detalle" CssClass="boton" style="Z-INDEX: 0" Width="123px"></asp:Button>
											<asp:Button ID="btnAnalisisSemanal" Runat="server" Text="Ver Analisis Semanal" tooltip="Clic para Ver detalle"	CssClass="boton" style="Z-INDEX: 0" Width="123px"></asp:Button>
											<asp:button id="btnEnviarOC" runat="server" Width="116px" CssClass="boton" Text="Enviar a proveedor" ToolTip="Envia la Orden de Servicio por e-mail al proveedor, tomando la cuenta de e-mail del maestro de Proveedores."></asp:button>&nbsp;
                                            <asp:button id="btnImprimir2" runat="server" CssClass="boton" Text="Imprimir"></asp:button></td>
									</tr>
								</table>
							</div>
							<div class="tab-page" id="tabpage31" runat="server">
								<h2 class="tab">Cotización</h2>
								<table width="100%">
									<tr>
										<td>
                                            <div style="WIDTH: 734px; HEIGHT: 200px; OVERFLOW: auto">
                                                   <asp:datagrid id="dtgAdjuntos" runat="server" Width="700px" AutoGenerateColumns="False">
		                                            <AlternatingItemStyle CssClass="GridAltItem"></AlternatingItemStyle>
		                                            <ItemStyle CssClass="GridItem"></ItemStyle>
		                                            <HeaderStyle CssClass="GridHeader"></HeaderStyle>
		                                            <Columns>
			
                                                     <asp:TemplateColumn>
		                                              <HeaderStyle Width="25px"></HeaderStyle>
		                                               <ItemStyle HorizontalAlign="Center"></ItemStyle>
		                                               <ItemTemplate>
			                                            <asp:HyperLink id="hlnAbrirAdjunto" runat="server" CommandName="Ver" ToolTip= "Ver archivo adjunto"></asp:HyperLink>
                                                        <asp:Label id="lblTipoAdjunto" runat="server" Visible="false" Text='<%# DataBinder.Eval(Container, "DataItem.TipoAdjunto") %>'></asp:Label>
                                                        <asp:Label id="lblNombreAdjunto" runat="server" Visible="false" Text='<%# DataBinder.Eval(Container, "DataItem.ArchivoAdjunto") %>'></asp:Label>
		                                              </ItemTemplate>
		                                             </asp:TemplateColumn>

                                                     <asp:TemplateColumn HeaderText="Sec.">
		                                              <HeaderStyle HorizontalAlign="Center" Width="20px" VerticalAlign="Middle"></HeaderStyle>
		                                              <ItemStyle Font-Size="9px" HorizontalAlign="Center"></ItemStyle>
		                                              <ItemTemplate>
                                                       <asp:Label id="lblNumeroDoc" runat="server" Visible="false" Text='<%# DataBinder.Eval(Container, "DataItem.NumeroDoc") %>'></asp:Label>
                                                       <asp:Label id="lblSecuencia" runat="server" Visible="false" Text='<%# DataBinder.Eval(Container, "DataItem.Secuencia") %>'></asp:Label>
                                                       <asp:Label id="lblCodigoAdjunto" runat="server" Visible="true" Text='<%# DataBinder.Eval(Container, "DataItem.CodigoArchivo") %>'></asp:Label>
		                                              </ItemTemplate>
		                                             </asp:TemplateColumn>

                                                     <asp:TemplateColumn HeaderText="Descripcion">
		                                              <HeaderStyle HorizontalAlign="Center" Width="150px" VerticalAlign="Middle"></HeaderStyle>
		                                              <ItemStyle Font-Size="9px" HorizontalAlign="Left"></ItemStyle>
		                                              <ItemTemplate><asp:Label id="lblDescripcion" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Descripcion") %>'></asp:Label></ItemTemplate>
		                                             </asp:TemplateColumn>
			
                                                     <asp:TemplateColumn HeaderText="Contenido">
		                                              <HeaderStyle HorizontalAlign="Center" Width="100px"  VerticalAlign="Middle"></HeaderStyle>
		                                              <ItemStyle Font-Size="9px" HorizontalAlign="Left"></ItemStyle>
		                                              <ItemTemplate><asp:Label id="lblObservacion" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.vch_DescripcionContenido") %>'></asp:Label></ItemTemplate>
		                                             </asp:TemplateColumn>

                                                     <asp:TemplateColumn HeaderText="Inf. Carga Archivo">
		                                              <HeaderStyle HorizontalAlign="Center" Width="400px" VerticalAlign="Middle"></HeaderStyle>
		                                              <ItemStyle Font-Size="9px" HorizontalAlign="Left"></ItemStyle>
		                                              <ItemTemplate><asp:Label id="lblDatos" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Informacion") %>'></asp:Label></ItemTemplate>
		                                             </asp:TemplateColumn>

                                                     <asp:TemplateColumn>
		                                              <HeaderStyle Width="25px"></HeaderStyle>
		                                              <ItemTemplate><asp:ImageButton id="btnEliminarItem" runat="server" ImageUrl="~/images/delete.gif" CommandName="Eliminar" CommandArgument='<%# DataBinder.Eval(Container.DataItem,"Secuencia")%>' ToolTip= "Eliminar archivo adjunto"></asp:ImageButton></ItemTemplate>
		                                             </asp:TemplateColumn>
                                                    </Columns>
	                                               </asp:datagrid>
                                            </div>
                                        </td>
									</tr>									
									<tr>
										<td align="right">                                            
                                            <asp:button id="btnActualizar" runat="server" Width="120px" Text="Actualizar" CssClass="Boton" Visible="false" ToolTip= "Actualizar"></asp:button>&nbsp;&nbsp;
                                            <asp:button id="btnAdjuntarArchivo" runat="server" Width="120px" Text="Adjuntar archivo" CssClass="Boton" Visible="true" ToolTip= "Adjuntar archivo"></asp:button></td>
									</tr>
								</table>
							</div>
<%--<div class="tab-page" id="tabpage32" runat="server">
								<h2 class="tab">Adjunto</h2>
								<table width="100%">
									<tr>
										<td><asp:datagrid id="Datagrid1" runat="server" AutoGenerateColumns="False" Width="912px">
												<AlternatingItemStyle CssClass="gridalternateitem"></AlternatingItemStyle>
												<ItemStyle CssClass="griditem"></ItemStyle>
												<HeaderStyle CssClass="gridheader"></HeaderStyle>
												<Columns>
													<asp:TemplateColumn HeaderText="Item">
														<HeaderStyle Width="80px"></HeaderStyle>
														<ItemTemplate>
															<asp:Label id="lblItemCodigoI" runat="server" Width="100%" CssClass="Input" Text='<%# DataBinder.Eval(Container, "DataItem.var_ArticuloCodigo") %>'>
															</asp:Label>
														</ItemTemplate>
														<EditItemTemplate>
															<asp:Label id="lblItemCodigoE" runat="server" Width="100%" CssClass="Input" Text='<%# DataBinder.Eval(Container, "DataItem.var_ArticuloCodigo") %>'>
															</asp:Label>
														</EditItemTemplate>
													</asp:TemplateColumn>
													<asp:TemplateColumn>
														<ItemTemplate>
															<asp:Label id="lblItemDescI" runat="server" Width="100%" CssClass="Input" Text='<%# DataBinder.Eval(Container, "DataItem.var_ArticuloNombre") %>'>
															</asp:Label>
														</ItemTemplate>
														<EditItemTemplate>
															<asp:Label id="lblItemDescE" runat="server" Width="100%" CssClass="Input" Text='<%# DataBinder.Eval(Container, "DataItem.var_ArticuloNombre") %>'>
															</asp:Label>
														</EditItemTemplate>
													</asp:TemplateColumn>
													<asp:TemplateColumn HeaderText="Ultima compra">
														<HeaderTemplate>
															<table id="Table11" cellspacing="0" cellpadding="0" width="100%" border="0">
																<tr>
																	<td class="GridHeader" style="BORDER-BOTTOM: #ffffff 1px solid" align="center" width="100%">Última 
																		compra</td>
																</tr>
															</table>
															<table id="Table13" cellspacing="0" cellpadding="0" width="100%" border="0">
																<tr>
																	<td class="GridHeader" width="20%">Precio</td>
																	<td class="GridHeader" width="5%">&nbsp;
																	</td>
																	<td class="GridHeader" style="BORDER-LEFT: #ffffff 1px solid" width="75%">&nbsp;Proveedor</td>
																</tr>
															</table>
														</HeaderTemplate>
														<ItemTemplate>
															<table id="Table15" style="HEIGHT: 15px" cellspacing="0" cellpadding="0" width="100%" border="0">
																<tr>
																	<td align="right" width="20%">
																		<asp:Label id="lblPrecioI" runat="server" Width="100%" CssClass="InputDerecha" Text='<%# Format(DataBinder.Eval(Container, "DataItem.num_UltimaCompraPrecio"),"#,##0.00") %>'>
																		</asp:Label></td>
																	<td width="5%">&nbsp;</td>
																	<td width="75%">
																		<asp:Label id="lblProveedorI" runat="server" Width="100%" CssClass="Input" Text='<%# DataBinder.Eval(Container, "DataItem.var_UltimaCompraProveedor") %>'>
																		</asp:Label></td>
																</tr>
															</table>
														</ItemTemplate>
														<EditItemTemplate>
															<table id="Table16" style="HEIGHT: 15px" cellspacing="0" cellpadding="0" width="100%" border="0">
																<tr>
																	<td align="right" width="20%">
																		<asp:Label id="lblPrecioE" runat="server" Width="100%" CssClass="InputDerecha" Text='<%# Format(DataBinder.Eval(Container, "DataItem.num_UltimaCompraPrecio"),"#,##0.00") %>'>
																		</asp:Label></td>
																	<td width="5%">&nbsp;</td>
																	<td width="75%">
																		<asp:Label id="lblProveedorE" runat="server" Width="100%" CssClass="Input" Text='<%# DataBinder.Eval(Container, "DataItem.var_UltimaCompraProveedor") %>'>
																		</asp:Label></td>
																</tr>
															</table>
														</EditItemTemplate>
													</asp:TemplateColumn>
													<asp:TemplateColumn HeaderText="Alternativas">
														<HeaderStyle Width="400px"></HeaderStyle>
														<ItemTemplate>
															<table id="Table12" cellspacing="0" cellpadding="0" width="100%" border="0">
																<tr>
																	<td style="FONT-WEIGHT: bold; FONT-SIZE: xx-small; FONT-FAMILY: Verdana; HEIGHT: 17px">1 
																		:</td>
																	<td style="HEIGHT: 17px" width="100%">
																		<asp:Label id="lblAlternativa1I" runat="server" Width="100%" CssClass="Input" Text='<%# DataBinder.Eval(Container, "DataItem.var_Alternativa1") %>'>
																		</asp:Label></td>
																</tr>
																<tr>
																	<td style="FONT-WEIGHT: bold; FONT-SIZE: xx-small; FONT-FAMILY: Verdana; HEIGHT: 17px">2 
																		:</td>
																	<td>
																		<asp:Label id="lblAlternativa2I" runat="server" Width="100%" CssClass="Input" Text='<%# DataBinder.Eval(Container, "DataItem.var_Alternativa2") %>'>
																		</asp:Label></td>
																</tr>
																<tr>
																	<td style="FONT-WEIGHT: bold; FONT-SIZE: xx-small; FONT-FAMILY: Verdana; HEIGHT: 17px">3 
																		:</td>
																	<td>
																		<asp:Label id="lblAlternativa3I" runat="server" Width="100%" CssClass="Input" Text='<%# DataBinder.Eval(Container, "DataItem.var_Alternativa3") %>'>
																		</asp:Label></td>
																</tr>
															</table>
														</ItemTemplate>
														<EditItemTemplate>
															<table id="Table14" cellspacing="0" cellpadding="0" width="100%" border="0">
																<tr>
																	<td style="FONT-WEIGHT: bold; FONT-SIZE: xx-small; FONT-FAMILY: Verdana; HEIGHT: 17px">1 
																		:</td>
																	<td style="HEIGHT: 17px">
																		<asp:TextBox id="txtProveedorCodigo1E" runat="server" Width="95px" CssClass="Input" Text='<%# DataBinder.Eval(Container, "DataItem.var_ProveedorCodigo1") %>'>
																		</asp:TextBox><input class="Boton" style="WIDTH: 20px; HEIGHT: 19px" onclick="ListarProveedores('<%# Container.ClientID%>','1')" type=button value=...>
																		<asp:Label id="lblProveedorNombre1E" runat="server" Width="250px" CssClass="Input" Text='<%# DataBinder.Eval(Container, "DataItem.var_ProveedorNombre1") %>'>
																		</asp:Label></td>
																</tr>
																<tr>
																	<td style="FONT-WEIGHT: bold; FONT-SIZE: xx-small; FONT-FAMILY: Verdana; HEIGHT: 17px">2 
																		:</td>
																	<td>
																		<asp:TextBox id="txtProveedorCodigo2E" runat="server" Width="95px" CssClass="Input" Text='<%# DataBinder.Eval(Container, "DataItem.var_ProveedorCodigo2") %>'>
																		</asp:TextBox><input class="Boton" style="WIDTH: 20px; HEIGHT: 19px" onclick="ListarProveedores('<%# Container.ClientID%>','2')" type=button value=...>
																		<asp:Label id="lblProveedorNombre2E" runat="server" Width="250px" CssClass="Input" Text='<%# DataBinder.Eval(Container, "DataItem.var_ProveedorNombre2") %>'>
																		</asp:Label></td>
																</tr>
																<tr>
																	<td style="FONT-WEIGHT: bold; FONT-SIZE: xx-small; FONT-FAMILY: Verdana; HEIGHT: 17px">3 
																		:</td>
																	<td>
																		<asp:TextBox id="txtProveedorCodigo3E" runat="server" Width="95px" CssClass="Input" Text='<%# DataBinder.Eval(Container, "DataItem.var_ProveedorCodigo3") %>'>
																		</asp:TextBox><input class="Boton" style="WIDTH: 20px; HEIGHT: 19px" onclick="ListarProveedores('<%# Container.ClientID%>','3')" type=button value=...>
																		<asp:Label id="lblProveedorNombre3E" runat="server" Width="250px" CssClass="Input" Text='<%# DataBinder.Eval(Container, "DataItem.var_ProveedorNombre3") %>'>
																		</asp:Label></td>
																</tr>
															</table>
														</EditItemTemplate>
													</asp:TemplateColumn>
													<asp:TemplateColumn>
														<HeaderStyle Width="40px"></HeaderStyle>
														<ItemTemplate>
															<asp:ImageButton id="ibtEditar" runat="server" ImageUrl="../../intranet/Imagenes/Editar.gif" CommandName="Editar"></asp:ImageButton>
														</ItemTemplate>
														<EditItemTemplate>
															<asp:ImageButton id="ibtCancelar" runat="server" ImageUrl="../../intranet/Imagenes/Cancelar.gif" CommandName="Cancelar"></asp:ImageButton>
															<asp:ImageButton id="ibtGrabar" runat="server" ImageUrl="../../intranet/Imagenes/Grabar.gif" CommandName="Actualizar"></asp:ImageButton>
														</EditItemTemplate>
													</asp:TemplateColumn>
												</Columns>
											</asp:datagrid></td>
									</tr>
									<tr>
										<td align="right"><asp:button id="Button1" runat="server" CssClass="Boton" Text="Grabar"></asp:button></td>
									</tr>
								</table>
							</div>--%>

							<input id="hdnProveedor1" style="WIDTH: 40px; HEIGHT: 22px" type="hidden" size="1" runat="server" />
                            <input id="hdnProveedor2" style="WIDTH: 32px; HEIGHT: 22px" type="hidden" size="1" runat="server" />
                            <input id="hdnProveedor3" style="WIDTH: 32px; HEIGHT: 22px" type="hidden" size="1" runat="server" />
                            <input id="hdnTipo" style="WIDTH: 32px; HEIGHT: 22px" type="hidden" size="1" name="Hidden1"	runat="server" />
                            <input id="hdnCodigoDoc" style="WIDTH: 32px; HEIGHT: 22px" type="hidden" size="1" name="hdnCodigoDoc"	runat="server" />                            
                            <input id="hdnObservaciones" style="WIDTH: 32px; HEIGHT: 22px" type="hidden" size="1" name="Hidden2" runat="server" />
                            <input id="hdnDestinoAbrir" style="WIDTH: 16px; HEIGHT: 20px" type="hidden" size="1" name="hdnDestinoAbrir" runat="server"/>                            
                            <input id="hdnDestinoGuardar" style="WIDTH: 16px; HEIGHT: 20px" type="hidden" size="1" name="hdnDestinoGuardar" runat="server"/>
                            </div>
					</td>
				</tr>
				<tr>
					<td>
					</td>
				</tr>
			</table>
		</form>
		<script type="text/javascript">
			//document.getElementById("tab-page-31.visible=false;
			setupAllTabs();
		</script>
	</body>
</html>
