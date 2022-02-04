<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="PLA30019.aspx.vb" Inherits="intranet_rrhh.PLA30019" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html>
	<head>
		<title>PLA30019</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR" />
		<meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE" />
		<meta content="JavaScript" name="vs_defaultClientScript" />
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema" />
        <link href="../Styles/tab.webfx.css" type="text/css" rel="stylesheet" />
		<link href="../Styles/NM0001.css" type="text/css" rel="stylesheet" />
		<script type="text/javascript" language="javascript" src="../Scripts/jsCalendario_N3.js"></script>
		<script type="text/javascript" language="javascript" src="../Scripts/tabpane.js"></script>
		<script type="text/javascript" language="javascript">
		    function popUp(strUrl) {
		        var intWidth = screen.width;
		        var intHeight = screen.height;
		        //window.open(strUrl);
		        window.open(strUrl, "", "top=0; left=0; width=" + intWidth + "; height=" + intHeight + "; resizable=1;");
		    }	
		</script>
        <script type="text/javascript" language="javascript">
            var tabPane;
            function showArticleTab(sName) {
                if (typeof tabPane != "undefined") {
                    switch (sName) {
                        case "PROCESAR":
                            tabPane.setSelectedIndex(0);
                            break;
                        case "CONSULTAR":
                            tabPane.setSelectedIndex(1);
                            break;
                        case "VER REPORTE":
                            tabPane.setSelectedIndex(1);
                            break;
                    }
                }
            }	
		</script>
        <script language="javascript" type="text/javascript">

            function popUpPdf(strRuta) {
                if (strRuta != "")
                    window.open(strRuta, 'pdfBoleta', 'top=0,left=0,width=800,height=600,status=no,resizable=no,scrollbars=yes');
            }

            function fMostrarReporte(strURL) {
                var intWidth = screen.width;
                var intHeight = screen.height;
                window.open(strURL, "", "top=0; left=0; width=" + intWidth + "; height=" + intHeight + "; resizable=1;");
            }

            function popUpMensaje(origen, strFlag, strMensaje) {
                if (strMensaje != "") {
                    var strDestino = "";
                    var strTipo = document.getElementById("ddlTipoBoleta");

                    if (strFlag = '1') {

                        if (origen == "B")
                            strDestino = "PLA30019_B.aspx?idMensaje=" + strMensaje + "&Tipo=" + strTipo.value;

                        if (origen == "V")
                            strDestino = "PLA30019_V.aspx?idMensaje=" + strMensaje + "&Tipo=" + strTipo.value;

                        if (origen == "R")
                            strDestino = "PLA30019_R.aspx?idMensaje=" + strMensaje + "&Tipo=" + strTipo.value;


                        window.open(strDestino, 'winMensaje', 'top=0,left=0,width=800,height=600,status=no,resizable=no,scrollbars=yes');
                    }
                }

            }
            function fConfirmar(iTipo, iCantidad) {
                var sMensaje = '';
                if (iTipo == 1) {
                    if (iCantidad > 1) {
                        sMensaje = "Existen " + iCantidad.toString() + " trabajadores con documentos ya generados.\n\n ¿Esta seguro de volver a generar los documentos? ";
                    } else {
                        sMensaje = "Existe " + iCantidad.toString() + " trabajador con su documento ya generado.\n\n ¿Esta seguro de volver a generar los documentos? ";
                    }
                    if (confirm(sMensaje) == true) {
                        //postback
                        __doPostBack('generarBoletas', 'generarBoletas')
                    }
                }
                if (iTipo == 2) {
                    if (iCantidad > 1) {
                        sMensaje = "Existen " + iCantidad.toString() + " trabajadores con documentos ya enviados.\n\n ¿Esta seguro de volver a enviar los documentos? ";
                    } else {
                        sMensaje = "Existe " + iCantidad.toString() + " trabajador con su documento ya enviado.\n\n ¿Esta seguro de volver a enviar los documentos? ";
                    }
                    if (confirm(sMensaje) == true) {
                        //postback
                        __doPostBack('enviarBoletas', '')
                    }
                }
            }
            function __doPostBack(eventTarget, eventArgument) {
                var theForm = document.forms['Form1'];
                theForm.__EVENTTARGET.value = eventTarget;
                theForm.__EVENTARGUMENT.value = eventArgument;
                theForm.submit();
            } 

</script> 
	</head>
	<body >
		<form id="Form1" method="post" runat="server">
			<table id="Table1" style="Z-INDEX: 101; LEFT: 8px; WIDTH: 1000px; POSITION: absolute; TOP: 8px;"
				cellspacing="2" cellpadding="2" width="1000px" border="0">
				<tr>
					<td class="cabecera" style="BACKGROUND-IMAGE: url(http://localhost/intranet/Imagenes/Cabecera.bmp)">Emisi&oacute;n 
						de Documentos Digitales
					</td>
				</tr>
                <tr>
                    <td>
                    <table id="Table8" style="WIDTH: 976px; HEIGHT: 11px" cellspacing="1" cellpadding="1" width="976px" border="0">
                    <tr>
                        <td class="Etiqueta" style="WIDTH: 150px; HEIGHT: 11px">&nbsp;Tipo de Documento :</td>
                        <td><asp:dropdownlist id="ddlTipoBoleta" runat="server" Height="16px" Width="200px" Font-Names="Verdana" Font-Size="XX-Small">
							    <asp:ListItem Value="MEN" Selected="True">Boleta Mensual</asp:ListItem>
                                <asp:ListItem Value="QUI">Boleta Quincenal</asp:ListItem>
                                <asp:ListItem Value="CTS">Liquidación CTS</asp:ListItem>
							</asp:dropdownlist>
                        </td>
                    </tr>
                    </table>
                    </td>
                </tr>
                <tr>
                    <td>
                        <div class="tab-pane" id="tab-pane-1">
							<script type="text/javascript">
							    tabPane = new WebFXTabPane(document.getElementById("tab-pane-1"), true);
							</script>
							<!-- GENERAR -->
                            <div class="tab-page" id="divProcesar" style="HEIGHT: 250px">
								<h2 class="tab">Procesar Documentos</h2>
								<table id="Table4" style="WIDTH: 976px; HEIGHT: 63px" cellspacing="1" cellpadding="1" width="976px" border="0">
							    <tr>
								    <td class="Etiqueta" style="WIDTH: 41px; HEIGHT: 11px">&nbsp;Planilla :</td>
								    <td style="WIDTH: 90px; HEIGHT: 11px"><asp:dropdownlist id="ddlPlanilla" runat="server" Height="16px" Width="88px" Font-Names="Verdana" Font-Size="XX-Small">
										<asp:ListItem Value="EMP">Empleados</asp:ListItem>
										<asp:ListItem Value="OBM">Obreros</asp:ListItem>
									    </asp:dropdownlist>
                                    </td>
								<td class="Etiqueta" style="WIDTH: 5px; HEIGHT: 11px">&nbsp;Año :</td>
								<td style="WIDTH: 88px; HEIGHT: 11px"><asp:textbox id="txtAnio" runat="server" Height="20px" Width="53px" Font-Names="Verdana" Font-Size="XX-Small"
										MaxLength="4"></asp:textbox></td>
								<td class="Etiqueta" style="WIDTH: 12px; HEIGHT: 11px">&nbsp;Mes :</td>
								<td style="WIDTH: 140px; HEIGHT: 11px"><asp:dropdownlist id="ddlMes" runat="server" Height="16px" Width="106px" Font-Names="Verdana" Font-Size="XX-Small">
										<asp:ListItem Value="1">Enero</asp:ListItem>
										<asp:ListItem Value="2">Febrero</asp:ListItem>
										<asp:ListItem Value="3">Marzo</asp:ListItem>
										<asp:ListItem Value="4">Abril</asp:ListItem>
										<asp:ListItem Value="5">Mayo</asp:ListItem>
										<asp:ListItem Value="6">Junio</asp:ListItem>
										<asp:ListItem Value="7">Julio</asp:ListItem>
										<asp:ListItem Value="8">Agosto</asp:ListItem>
										<asp:ListItem Value="9">Septiembre</asp:ListItem>
										<asp:ListItem Value="10">Octubre</asp:ListItem>
										<asp:ListItem Value="11">Noviembre</asp:ListItem>
										<asp:ListItem Value="12">Diciembre</asp:ListItem>
									</asp:dropdownlist>&nbsp;&nbsp;&nbsp;</td>
								<td class="ETiqueta" style="HEIGHT: 11px">&nbsp;Correlativo&nbsp;&nbsp;&nbsp;&nbsp; 
									:</td>
								<td style="HEIGHT: 11px"><asp:textbox id="txtCorrelativo" runat="server" Height="20px" Width="53px" Font-Names="Verdana"
										Font-Size="XX-Small" MaxLength="1"></asp:textbox></td>
							    </tr>
							    <tr>
								    <td class="Etiqueta" style="WIDTH: 41px; HEIGHT: 11px">Código 
									    Inicio&nbsp;&nbsp;&nbsp;&nbsp; :</td>
								    <td style="WIDTH: 90px; HEIGHT: 11px"><asp:textbox id="txtCodInicio" runat="server" Height="20px" Width="53px" Font-Names="Verdana"
										    Font-Size="XX-Small" MaxLength="5"></asp:textbox></td>
								    <td class="Etiqueta" style="WIDTH: 5px; HEIGHT: 11px">Código Final :</td>
								    <td style="WIDTH: 88px; HEIGHT: 11px"><asp:textbox id="txtCodFin" runat="server" Height="20px" Width="53px" Font-Names="Verdana" Font-Size="XX-Small"
										    MaxLength="5"></asp:textbox></td>
								    <td class="Etiqueta" style="WIDTH: 12px; HEIGHT: 11px">Centro&nbsp; Costo :</td>
								    <td style="WIDTH: 140px; HEIGHT: 11px"><asp:dropdownlist id="ddlCCosto" runat="server" Height="16px" Width="106px" Font-Names="Verdana" Font-Size="XX-Small">
										    <asp:ListItem Value="EMP" Selected="True">Empleados</asp:ListItem>
										    <asp:ListItem Value="ONM">Pta.Principal</asp:ListItem>
										    <asp:ListItem Value="OPN">Pta.Hilanderia</asp:ListItem>
									    </asp:dropdownlist></td>
								    <td class="Etiqueta" style="HEIGHT: 11px">Observaciones:</td>
								    <td style="HEIGHT: 11px"><asp:textbox id="txtObservacion" runat="server" Height="20px" Width="427px" Font-Names="Verdana" Font-Size="XX-Small" MaxLength="60"></asp:textbox></td>
							    </tr>
						        </table>
						        <table class="botonera" id="Table3" style="WIDTH: 958px; HEIGHT: 32px" cellspacing="2" cellpadding="2" width="958" border="0">
							    <tr>
								    <td style="WIDTH: 958px" align="right">                                 
                                    <asp:button id="btnGenerar" runat="server" CssClass="boton" Text=" Generar "></asp:button>
                                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                    <asp:button id="btnEnviar" runat="server" CssClass="boton" Text="  Enviar  "></asp:button>
                                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                    <asp:button id="btnRecepcionar" runat="server" CssClass="boton" Text="  Recepcionar  "></asp:button>
                                    </td>
							    </tr>
						        </table>
						    </div>	
                            <!-- CONSULTAR -->
                            <div class="tab-page" id="divConsultar" style="HEIGHT: 250px">
								<h2 class="tab">Consultar Documentos</h2>
								<table id="Table2" style="WIDTH: 976px; HEIGHT: 63px" cellspacing="1" cellpadding="1" width="976px" border="0">
							    <tr>
								    <td class="Etiqueta" style="WIDTH: 41px; HEIGHT: 11px">&nbsp;Planilla :</td>
								    <td style="WIDTH: 90px; HEIGHT: 11px"><asp:dropdownlist id="ddlCPlanilla" runat="server" Height="16px" Width="88px" Font-Names="Verdana" Font-Size="XX-Small">
                                        <asp:ListItem Value="" Selected="True">Todos</asp:ListItem>
										<asp:ListItem Value="EMP">Empleados</asp:ListItem>
										<asp:ListItem Value="OBM">Obreros</asp:ListItem>
									    </asp:dropdownlist>
                                    </td>
								<td class="Etiqueta" style="WIDTH: 5px; HEIGHT: 11px">&nbsp;Año :</td>
								<td style="WIDTH: 88px; HEIGHT: 11px"><asp:textbox id="txtCAno" runat="server" Height="20px" Width="53px" Font-Names="Verdana" Font-Size="XX-Small"
										MaxLength="4"></asp:textbox></td>
								<td class="Etiqueta" style="WIDTH: 12px; HEIGHT: 11px">&nbsp;Mes :</td>
								<td style="WIDTH: 140px; HEIGHT: 11px"><asp:dropdownlist id="ddlCMes" runat="server" Height="16px" Width="106px" Font-Names="Verdana" Font-Size="XX-Small">
										<asp:ListItem Value="1">Enero</asp:ListItem>
										<asp:ListItem Value="2">Febrero</asp:ListItem>
										<asp:ListItem Value="3">Marzo</asp:ListItem>
										<asp:ListItem Value="4">Abril</asp:ListItem>
										<asp:ListItem Value="5">Mayo</asp:ListItem>
										<asp:ListItem Value="6">Junio</asp:ListItem>
										<asp:ListItem Value="7">Julio</asp:ListItem>
										<asp:ListItem Value="8">Agosto</asp:ListItem>
										<asp:ListItem Value="9">Septiembre</asp:ListItem>
										<asp:ListItem Value="10">Octubre</asp:ListItem>
										<asp:ListItem Value="11">Noviembre</asp:ListItem>
										<asp:ListItem Value="12">Diciembre</asp:ListItem>
									</asp:dropdownlist>&nbsp;&nbsp;&nbsp;</td>
								<td class="ETiqueta" style="HEIGHT: 11px">Situaci&oacute;n:</td>
								<td style="HEIGHT: 11px">
                                    <asp:dropdownlist id="ddlCSituacion" runat="server" Height="16px" Width="106px" Font-Names="Verdana" Font-Size="XX-Small">
										<asp:ListItem Value="" Selected="True">Todos</asp:ListItem>
                                        <asp:ListItem Value="NG" >No Generados</asp:ListItem>
										<asp:ListItem Value="NE">No Enviados</asp:ListItem>
										<asp:ListItem Value="NR">No Recepcionados</asp:ListItem>
									</asp:dropdownlist>
                                </td>
							    </tr>
							    <tr>
								    <td class="Etiqueta" style="WIDTH: 41px; HEIGHT: 11px">Código 
									    Inicio&nbsp;&nbsp;&nbsp;&nbsp; :</td>
								    <td style="WIDTH: 90px; HEIGHT: 11px"><asp:textbox id="txtCCodigoInicio" runat="server" Height="20px" Width="53px" Font-Names="Verdana"
										    Font-Size="XX-Small" MaxLength="5"></asp:textbox></td>
								    <td class="Etiqueta" style="WIDTH: 5px; HEIGHT: 11px">Código Final :</td>
								    <td style="WIDTH: 88px; HEIGHT: 11px"><asp:textbox id="txtCCodigoFinal" runat="server" Height="20px" Width="53px" Font-Names="Verdana" Font-Size="XX-Small"
										    MaxLength="5"></asp:textbox></td>
								    <td class="Etiqueta" style="WIDTH: 12px; HEIGHT: 11px">Centro&nbsp; Costo :</td>
								    <td style="WIDTH: 140px; HEIGHT: 11px"><asp:dropdownlist id="ddlCCentroCosto" runat="server" Height="16px" Width="106px" Font-Names="Verdana" Font-Size="XX-Small">
										    <asp:ListItem Value="" Selected="True">Todos</asp:ListItem>
                                            <asp:ListItem Value="EMP" >Empleados</asp:ListItem>
										    <asp:ListItem Value="ONM">Pta.Principal</asp:ListItem>
										    <asp:ListItem Value="OPN">Pta.Hilanderia</asp:ListItem>
									    </asp:dropdownlist></td>
								    <td class="Etiqueta" style="HEIGHT: 11px"></td>
								    <td style="HEIGHT: 11px"><asp:button id="btnConsultar" runat="server" CssClass="boton" Text=" Consultar "></asp:button></td>
							    </tr>
						        </table>
						        <table class="botonera" id="Table5" style="WIDTH: 958px; HEIGHT: 32px" cellspacing="2" cellpadding="2" width="958" border="0">
							    <tr>
								    <td align="right"></td>
                                    <td style="WIDTH: 758px" align="right">                                   
                                    </td>
								    <td style="WIDTH: 200px" align="right">                                        
                                    </td>
							    </tr>
                                <tr>
                                    <td class="panel" colspan="4">
                                        <asp:datagrid id="DataGrid1" runat="server" Width="1100px" AutoGenerateColumns="False">
							                    <FooterStyle CssClass="GridFooter"></FooterStyle>
							                    <AlternatingItemStyle CssClass="GridAlternateItem"></AlternatingItemStyle>
							                    <ItemStyle CssClass="GridItem"></ItemStyle>
							                    <HeaderStyle CssClass="GridHeader"></HeaderStyle>
							                    <Columns>
								                    <asp:TemplateColumn HeaderText="Código">
									                    <HeaderStyle Width="100px"></HeaderStyle>
                                                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
									                    <ItemTemplate>
										                    <asp:label  id="lblCoTrab" runat="server" CssClass="Input" Width="100%" text='<%# Container.DataItem("co_trab") %>'></asp:label>
									                    </ItemTemplate>
								                    </asp:TemplateColumn>
								                    <asp:TemplateColumn HeaderText="Nombres">
									                    <HeaderStyle HorizontalAlign="Center" Width="350px"></HeaderStyle>
									                    <ItemStyle HorizontalAlign="Left"></ItemStyle>
									                    <ItemTemplate>
										                    <asp:label id="lblNoTrabCompleto" runat="server" CssClass="Input" Width="100%" text='<%# Container.DataItem("no_trab_completo") %>'></asp:label>
									                    </ItemTemplate>
								                    </asp:TemplateColumn>
								                    <asp:TemplateColumn HeaderText="Generado">
									                    <HeaderStyle HorizontalAlign="Center" Width="150px"></HeaderStyle>
                                                        <HeaderTemplate>
										                    <table style="HEIGHT: 28px" cellspacing="1" cellpadding="1" width="150px" border="0">
											                    <tr>
												                    <td class="GridHeader" colspan="2"><center>Generación</center></td>
											                    </tr>
                                                                <tr>
                                                                    <td class="GridHeader" style="width:100px"><center>Fecha</center></td>
                                                                    <td class="GridHeader" style="width:50px"><center>...</center></td>												
											                    </tr>
										                    </table>
									                    </HeaderTemplate>
									                    <ItemTemplate>
                                                            <table style="HEIGHT: 28px" cellspacing="1" cellpadding="1" width="150px" border="0">
                                                                <tr>
												                    <td style="width:100px"><center><asp:label id="lblGenFecha" runat="server" CssClass="Input" Width="100%" text='<%# Container.DataItem("vch_boleta_dmy") %>'></asp:label></center></td>
                                                                    <td style="width:50px"><center>
                                                                    <a href="#"  style="text-decoration:none; outline-style:none;" onclick="popUpMensaje('B','<%# Container.DataItem("bit_boleta") %>','<%# Container.DataItem("int_id") %>');return false;"><img src="../../intranet/Imagenes/buscar.gif" alt=""/></a>
                                                                    </center></td>
											                    </tr>
										                    </table>
									                    </ItemTemplate>
								                    </asp:TemplateColumn>
								                    <asp:TemplateColumn HeaderText="Env&iacute;o">
									                    <HeaderStyle HorizontalAlign="Center" Width="320px"></HeaderStyle>
                                                        <HeaderTemplate>
										                    <table style="HEIGHT: 28px" cellspacing="1" cellpadding="1" width="320px" border="0">
											                    <tr>
												                    <td class="GridHeader" colspan="3"><center>Env&iacute;o</center></td>
											                    </tr>
                                                                <tr>
												                    <td style="width:100px" class="GridHeader"><center>Fecha</center></td>
                                                                    <td style="width:200px" class="GridHeader"><center>Correo</center></td>
                                                                    <td style="width:20px" class="GridHeader"><center>...</center></td>
											                    </tr>
										                    </table>
									                    </HeaderTemplate>
									                    <ItemTemplate>
										                    <table style="HEIGHT: 28px" cellspacing="1" cellpadding="1" width="320px" border="0">
                                                                <tr>
												                    <td style="width:100px"><center><asp:label id="lblEnvFecha" runat="server" CssClass="Input" Width="100%" text='<%# Container.DataItem("vch_envio_dmy") %>'></asp:label></center></td>
                                                                    <td style="width:200px"><asp:label id="lblEnvMail" runat="server" CssClass="Input" Width="100%" text='<%# Container.DataItem("vch_enviocorreo") %>'></asp:label></td>
                                                                    <td style="width:20px"><center><a href="#" style="text-decoration:none" onclick="popUpMensaje('V','<%# Container.DataItem("bit_envio") %>','<%# Container.DataItem("int_id") %>');return false;"><img src="../../intranet/Imagenes/buscar.gif" alt=""/></a></center></td>
											                    </tr>
										                    </table>
									                    </ItemTemplate>
								                    </asp:TemplateColumn>
								                    <asp:TemplateColumn HeaderText="Recepci&oacute;n">
									                    <HeaderStyle HorizontalAlign="Center" Width="150px"></HeaderStyle>
									                    <HeaderTemplate>
										                    <table id="Table6" style="HEIGHT: 28px" cellspacing="1" cellpadding="1" width="150px" border="0">
											                    <tr>
												                    <td class="GridHeader" colspan="2"><center>Recepci&oacute;n</center></td>
											                    </tr>
                                                                <tr>
												                    <td style="width:100px" class="GridHeader"><center>Fecha</center></td>
                                                                    <td style="width:50px" class="GridHeader"><center>...</center></td>
											                    </tr>
										                    </table>
									                    </HeaderTemplate>
									                    <ItemTemplate>
										                    <table style="HEIGHT: 28px" cellspacing="1" cellpadding="1" width="150px" border="0">
                                                                <tr>
												                    <td style="width:100px"><center><asp:label id="lblRecFecha" runat="server" CssClass="Input" Width="100%" text='<%# Container.DataItem("vch_recepcion_dmy") %>'></asp:label></center></td>
                                                                    <td style="width:50px"><center><a href="#" style="text-decoration:none" onclick="popUpMensaje('R','<%# Container.DataItem("bit_recepcion") %>','<%# Container.DataItem("int_id") %>');return false;"><img src="../../intranet/Imagenes/buscar.gif" alt=""/></a></center></td>
											                    </tr>
										                    </table>
									                    </ItemTemplate>
								                    </asp:TemplateColumn>								
							                    </Columns>
						                    </asp:datagrid>
                                    </td>
                                </tr>
						        </table>
							</div>
                            <!-- VER REPORTE -->
                            <div class="tab-page" id="div1" style="HEIGHT: 250px">
								<h2 class="tab">Reporte</h2>
								<table id="Table7" style="WIDTH: 500px; HEIGHT: 63px" cellspacing="1" cellpadding="1"  border="0">
							    <tr>
								<td class="Etiqueta" style="WIDTH: 40px; HEIGHT: 11px">&nbsp;Año :</td>
								<td style="WIDTH: 88px; HEIGHT: 11px"><asp:textbox id="txtRAno" runat="server" Height="20px" Width="53px" Font-Names="Verdana" Font-Size="XX-Small"
										MaxLength="4"></asp:textbox></td>
								<td class="Etiqueta" style="WIDTH: 40px; HEIGHT: 11px">&nbsp;Mes :</td>
								<td style="WIDTH: 140px; HEIGHT: 11px"><asp:dropdownlist id="ddlRMes" runat="server" Height="16px" Width="106px" Font-Names="Verdana" Font-Size="XX-Small">
										<asp:ListItem Value="1">Enero</asp:ListItem>
										<asp:ListItem Value="2">Febrero</asp:ListItem>
										<asp:ListItem Value="3">Marzo</asp:ListItem>
										<asp:ListItem Value="4">Abril</asp:ListItem>
										<asp:ListItem Value="5">Mayo</asp:ListItem>
										<asp:ListItem Value="6">Junio</asp:ListItem>
										<asp:ListItem Value="7">Julio</asp:ListItem>
										<asp:ListItem Value="8">Agosto</asp:ListItem>
										<asp:ListItem Value="9">Septiembre</asp:ListItem>
										<asp:ListItem Value="10">Octubre</asp:ListItem>
										<asp:ListItem Value="11">Noviembre</asp:ListItem>
										<asp:ListItem Value="12">Diciembre</asp:ListItem>
									</asp:dropdownlist>&nbsp;&nbsp;&nbsp;</td>
                                    <td style="WIDTH: 100px" align="right">                                 
                                    <asp:button id="btnVerReporte" runat="server" CssClass="boton" Text=" Ver Reporte "></asp:button>
                                    </td>
							    </tr>							    
						        </table>
						    </div>	
                        </div>
                    </td>
                </tr>
				<tr>
					<td class="panel"></td>
				</tr>
                
			</table>
            <input type="hidden" name="__EVENTTARGET"  id="__EVENTTARGET" value="" /> 
            <input type="hidden" name="__EVENTARGUMENT" id="__EVENTARGUMENT" value="" /> 
		</form>
	</body>
</html>