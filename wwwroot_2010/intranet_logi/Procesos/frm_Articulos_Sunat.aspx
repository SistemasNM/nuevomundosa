<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="frm_Articulos_Sunat.aspx.vb" Inherits="intranet_logi.frm_Articulos_Sunat" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html>
	<head>
		<title>LISTADO DE ARTICULOS DE SUNAT</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR" />
		<meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE" />
		<meta content="JavaScript" name="vs_defaultClientScript" />
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema" />
		
		<link href="../css/NM0001.css" type="text/css" rel="stylesheet"/>
		<link href="../../intranet/Estilos/tab.webfx.css" type="text/css" rel="Stylesheet"/>
        <link href="../../intranet/Estilos/Styles_Paginas.css" type="text/css" rel="Stylesheet"/>
        <link href="../../intranet/Estilos/Styles_Controles.css" type="text/css" rel="Stylesheet"/>
        <script language="JavaScript" src="../../intranet/js/jsDesTabla.js" type="text/jscript" ></script>    
        <script language="JavaScript" src="../../intranet/js/jsGeneral.js" type="text/jscript" ></script>       
  
    
		<script language="javascript" type="text/javascript">

		    g_RutaDirecto = 1;

		    function btnSeleccion_Onclick(strCodigo) {
		        window.location.href = "frmFichaProveedor.aspx?strNumeroOrdenServicio=" + strCodigo;
		        return false;
		    }

		    //  ****************  //

		    function btnReporte_Onclick(strUrl) {
		        var intWidth = screen.width;
		        var intHeight = screen.height;
		        window.open(strUrl, "", "top=0; left=0; width=" + intWidth + "; height=" + intHeight + "; resizable=1;");
		    }
        			
	    </script>

</head>
	<body>
	
		<form id="frmListadoArticulosSunat"  method="post" runat="server">
		<asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager> 
			<table id="Table40" style="Z-INDEX: 101; LEFT: 0px; WIDTH: 949px; POSITION: absolute; TOP: 0px; HEIGHT: 416px" cellspacing="2" cellpadding="2" width="949" border="0">
			<tr>
            <td>
					<table class="CabMain" border="0" cellpadding="0" cellspacing="0" style="width:820px;height:30px">
                        <tr>
                            <td style="width:100%" align="right" valign="bottom" >LISTADO DE ARTICULOS X PERIODO</td>
                        </tr>
                    </table>
			</td>
            </tr>

					<tr>
						<td class="panel">
							<table style="WIDTH: 720px; HEIGHT: 16px" border="0">
							<tr>
									<td class="Etiqueta" style="WIDTH: 103px">&nbsp;Año:</td>
									<td style="WIDTH: 94px" width="94">
                                        <asp:DropDownList ID="cmbAnno" runat="server">
                                            <asp:ListItem Value="00">Seleccione</asp:ListItem>
                                            <asp:ListItem Value="2013">2013</asp:ListItem>
                                            <asp:ListItem Value="2014">2014</asp:ListItem>
                                        </asp:DropDownList>
									</td>
									<td style="WIDTH: 49px; HEIGHT: 8px" width="49">&nbsp;Mes:</td>
									<td style="HEIGHT: 8px" colspan="2"><asp:DropDownList ID="cmbMes" runat="server">
                                            <asp:ListItem Value="00">Seleccione</asp:ListItem>
                                            <asp:ListItem Value="01">Enero</asp:ListItem>
                                            <asp:ListItem Value="02">Febrero</asp:ListItem>
                                            <asp:ListItem Value="03">Marzo</asp:ListItem>
                                            <asp:ListItem Value="04">Abril</asp:ListItem>
                                            <asp:ListItem Value="05">Mayo</asp:ListItem>
                                            <asp:ListItem Value="06">Junio</asp:ListItem>
                                            <asp:ListItem Value="07">Julio</asp:ListItem>
                                            <asp:ListItem Value="08">Agosto</asp:ListItem>
                                            <asp:ListItem Value="09">Septiembre</asp:ListItem>
                                            <asp:ListItem Value="10">Octubre</asp:ListItem>
                                            <asp:ListItem Value="11">Noviembre</asp:ListItem>
                                            <asp:ListItem Value="12">Diciembre</asp:ListItem>
                                        </asp:DropDownList></td>
                                									<td>&nbsp;<asp:button id="btnBuscar" runat="server" Text="Buscar"></asp:button></td>
								</tr>
								

							</table>
							<table id="Table1" cellspacing="1" cellpadding="1" width="98%" border="0">
								<tr>
									<td>
										<!--GridAlternateItem-->
										<div style="WIDTH: 100%; HEIGHT: 300px;overflow: auto;">
										    <asp:datagrid id="dtgLista" runat="server" Width="100%" AutoGenerateColumns="False">
												<AlternatingItemStyle CssClass="GridAltItem"></AlternatingItemStyle>
												<ItemStyle CssClass="GridItem"></ItemStyle>
												<HeaderStyle CssClass="gridheader"></HeaderStyle>
												<Columns>
													<asp:TemplateColumn>
														<ItemTemplate>
															<input  id="btnSeleccion" name="btnSeleccion" runat="server" type="button"  
																value="..." style="cursor:hand;" title="Presione para ver detalle de OS.">&nbsp
															<asp:ImageButton id="btnReporte" runat="server" src="../../intranet/Imagenes/buscar.gif"/>
														</ItemTemplate>
														<ItemStyle HorizontalAlign="Center"></ItemStyle>
													</asp:TemplateColumn>
													<asp:BoundColumn DataField="Co_item" HeaderText="CODIGO"></asp:BoundColumn>
													<asp:BoundColumn DataField="de_item" HeaderText="DESCRIPCION"></asp:BoundColumn>
													<asp:BoundColumn DataField="co_unme" HeaderText="UNI. MEDIDA"></asp:BoundColumn>
													<asp:BoundColumn DataField="ca_Ingresos" HeaderText="INGRESOS"></asp:BoundColumn>
													<asp:BoundColumn DataField="ca_Salidas" HeaderText="SALIDAS"></asp:BoundColumn>
													<asp:BoundColumn DataField="Stock" HeaderText="STOCK"></asp:BoundColumn>
													<asp:BoundColumn DataField="Adjunto" HeaderText="ADJUNTO"></asp:BoundColumn>
												</Columns>
											</asp:datagrid></DIV>
									</td>
								</tr>
								<tr>
									<td align="center">
										<table class="botonera7" id="Table4" cellspacing="0" cellpadding="0" width="100%" align="center"
											border="0">
											<tr>
												<td align="left"><asp:label id="lblMsg" runat="server" ForeColor="Red" Font-Bold="True"></asp:label></td>
											</tr>
										</table>
									</td>
								</tr>
								<tr>
									<td class="FOOTER"></td>
								</tr>
							</table>
		
		                </td>
                    </tr>
           
           </table>
        </form>
	</body>
</html>
