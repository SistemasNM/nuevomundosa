<%@ Page Language="vb" AutoEventWireup="false" Codebehind="frm_RegistroDespachoPedidos.aspx.vb" Inherits="intranet_logi.frm_RegistraDespachoPedidos"%>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html>
	<head>
		<title>DESPACHO DE PEDIDOS</title>
		<base target="_self"/>
		<meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1"/>
		<meta name="CODE_LANGUAGE" content="Visual Basic .NET 7.1"/>
		<meta name="vs_defaultClientScript" content="JavaScript"/>
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5"/>
		<link href="../../intranet/Estilos/Styles_Paginas.css" type="text/css" rel="Stylesheet"/>
        <link href="../css/NM0001.css" type="text/css" rel="stylesheet"/>
        <link href="../../intranet/Estilos/Styles_Controles.css" type="text/css" rel="Stylesheet"/>
        <link href="../../intranet/Estilos/EstilosWeb.css" type="text/css" rel="Stylesheet"/>
<%--        <link href="C:\inetpub\wwwroot_fw4\intranet\Estilos\Styles_Paginas.css" type="text/css" rel="Stylesheet"/>
        <link href="C:\inetpub\wwwroot_fw4\intranet\Estilos\NM0001.css" type="text/css" rel="stylesheet"/>
        <link href="C:\inetpub\wwwroot_fw4\intranet\Estilos\Styles_Controles.css" type="text/css" rel="Stylesheet"/>
        <link href="C:\inetpub\wwwroot_fw4\intranet\Estilos\EstilosWeb.css" type="text/css" rel="Stylesheet"/>--%>
        <link href="../css/sytle.css" type="text/css" rel="stylesheet"/>
		<script language="javascript" type="text/javascript" src="../../intranet/JS/jsCalendario_N4.js"></script>
		<script language="javascript" type="text/javascript" src="../../intranet/JS/functions.js"></script>
		<script language="javascript" type="text/javascript">
		    // funciones
		    function VerValesPedido(strNumeroPedido) {
		        var dt_time = new Date().getTime();
		        var strURL = "frm_ListadoValesPedido.aspx?&strNumeroPedido=" + strNumeroPedido + "&dtTime=" + dt_time;
		        var retorno = window.showModalDialog(strURL, "", "dialogheight:400px;dialogwidth:750px;center:yes;help:no;");
		    }
		    function fMostrarReporte(strUrl) {
			    var intwidth = screen.width;
			    var intheight = screen.height;
			    window.open(strUrl, "", "top=0; left=0; width=" + intwidth + "; height=" + intheight + "; resizable=1;");
			}
			
		    function fnc_Cerrar(){
			    window.open('','_parent','');
			    window.close();
		    }
		    function VerSolicitante(strSolicitante) {
		        var dt_time = new Date().getTime();
		        var strURL = "../../intranet/Maestros/frm_TrabajadorConsulta.aspx?&strEmpleado=" + strSolicitante + "&dtTime=" + dt_time;
		        window.showModalDialog(strURL, "", "dialogheight:250px;dialogwidth:500px;center:yes;help:no;");
		    }
		    function fnc_BuscarTrabajadores(){
		        var tipo = "EMP";
		        var dt_time = new Date().getTime();
		        var strURL = "../../intranet/Buscadores/PLA_0001.aspx?strTipo="+tipo + "&dtTime=" + dt_time;
		        var retorno = window.showModalDialog(strURL, "", "dialogheight:450px;dialogwidth:450px;center:yes;help:no;");
			    if (retorno!="" && retorno!=":"){
				    var datos = retorno.split(":");
				    var codigo = datos[0];
				    var nombre = datos[1];
				    document.all('txtCodRecepciona').value = codigo;
				    document.all('lblDesRecepciona').value = nombre;
				    document.all('txtCodRecepciona').focus();
			    }
            }

//            function fnc_CulminarVale() {
//                if (confirm("Está seguro que desea culminar el Vale?") == true) {
//                    return true;
//                } else { return false; }
//            }


            function fnc_ConfirmarOperacion(strOperacion) {
                var strMensaje = "";
                switch (strOperacion) {
                    case "Culminar":
                        strMensaje = "Está seguro que desea culminar el Vale?";
                        break;
                    case "PreDespachar":
                        strMensaje = "Está seguro que desea realizar el Pre-Despacho del Vale?";
                        break;
                    case "Despachar":
                        strMensaje = "Está seguro que desea realizar el Despacho del Vale?";
                        break;
                    default: return false;
                }

                if (confirm(strMensaje) == true) {
                    return true;
                } else { return false; }
            }


            //Ejemplo fSoloNumeros :  txtCodigoProv.Attributes.Add("onkeydown", "return fSoloNumeros(event);")
            function fSoloNumeros(e) {
                var charCode = (e.which) ? e.which : event.keyCode
                var ctrlDown = e.ctrlKey || e.metaKey // Mac support
                //alert(charCode);
                return (charCode == 110 || charCode == 190 || charCode == 8 || charCode == 13 || charCode == 46 || (charCode >= 48 && charCode <= 57) || (charCode >= 96 && charCode <= 105) || (ctrlDown == true && (charCode == 86 || charCode == 67)));
            }


            function VerificaNumero(objTope, control) {
                var numero = 0;
                var tope = 0;
                numero = parseFloat(control.value);
                tope = parseFloat(objTope);

                if (isNaN(numero)) {
                    alert("ERROR: El valor no es un número valido");
                    control.value = 0;
                    control.focus();
                } else {
                    if (numero <= parseFloat(tope)) {
                        control.value = numero.toFixed(2);
                    }
                    else {
                        alert("ERROR: No se puede despachar mas de " + tope.toFixed(2));
                        control.value = 0;
                        control.focus();
                    }
                }
                //
                
            }
		</script>

	</head>
	<body>
	  <form id="frmRegistroDespachoPedidos" method="post" autocomplete="off" runat="server">
     <center>
      <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager> 
    	
      <%--titulo--%>
			<table id="tblCabeceraPagina" border="0" cellspacing="0" cellpadding="0" width="900px">
				<tr>
					<td style="width:900px; height:30px" class="Cabecera">DESPACHO DE VALES DE ALMACEN</td>
				</tr>
			</table>
      
      <asp:UpdatePanel ID="panContenido" runat="server" UpdateMode="Always">
      <ContentTemplate>
      <%--datos--%>
			<table style="width:900px" border="0" cellspacing="0" cellpadding="0">
				<tr>
					<td style="width:120px" class="Descripcion">&nbsp;Situacion:</td>
					<td style="width:380px" valign="middle" align="left" colspan="2">
                        <asp:ImageButton ID="btnImprimirEtiqueta" runat="server" ImageUrl="../../intranet/Imagenes/im_printer.gif" ToolTip = "Imprimir Etiqueta Pre-DEspacho"></asp:ImageButton>&nbsp;
                        <asp:label id="txtEstado" runat="server" Font-Bold="True" width="357px" 
                            Font-Size="8px"></asp:label></td>
                    <td style="width:300px" class="Descripcion" valign="middle" align="center" rowspan="9">
						
					    <table style="width:300px">
                            <tr>
                                <td class="Descripcion" style="width:100px">
                                    &nbsp;Solicitante:</td>
                                <td align="left" style="width:200px" valign="middle">
                                    <asp:Label ID="txtCodSolicitante" runat="server" Font-Bold="True" 
                                        Font-Size="8px" width="30px"></asp:Label>
                                    <asp:Label ID="txtDesSolicitante" runat="server" Font-Size="8px"
                                        width="170px"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td class="Descripcion" style="width:100px">
                                    &nbsp;Area:</td>
                                <td align="left" style="width:200px" valign="middle">
                                    <asp:Label ID="txtDesArea" runat="server" Font-Size="8px" width="200px"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td class="Descripcion" style="width:100px">
                                    &nbsp;Seccion:</td>
                                <td align="left" style="width:200px">
                                    <asp:Label ID="txtDesSeccion" runat="server" Font-Size="8px" width="200px"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td class="Descripcion" style="width:100px">
                                    &nbsp;Cargo:</td>
                                <td align="left" style="width:200px">
                                    <asp:Label ID="txtDesCargo" runat="server" Font-Size="8px" width="200px"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td style="width:300px" colspan="2" valign="top" align="center">
                                    <asp:Image ID="imgFoto" runat="server" width="110px" Height="140px" CssClass="Descripcion"/>
                                </td>
                            </tr>
                        </table>
						
					</td>
				</tr>
				<tr>
					<td style="width:120px" class="Descripcion">&nbsp;Num. Pedido:</td>
					<td style="width:380px" valign="middle" align="left" colspan="2">
           <%--&nbsp;<asp:label id="txtSeriePedido" runat="server" Font-Bold="True" width="30px" Font-Size="8px"></asp:label>--%>
           &nbsp;<asp:label id="txtNumeroPedido" runat="server" width="100px" Font-Size="8px"></asp:label>
          </td>
					
				</tr>
				<tr>
					<td style="width:120px" class="Descripcion">&nbsp;Centro Costos:</td>
					<td style="width:380px" valign="middle" align="left" colspan="2">
           <%--&nbsp;<asp:label id="txtCodCentroCostos" runat="server" Font-Bold="True" width="50px" Font-Size="8px"></asp:label>--%>
           &nbsp;<asp:label id="txtDesCentroCostos" runat="server" width="250px" Font-Size="8px"></asp:label>
          </td>
				</tr>
				<tr>
					<td style="width:120px" class="Descripcion">&nbsp;Almacen:</td>
					<td style="width:380px" valign="middle" align="left" colspan="2">
           <%--&nbsp;<asp:label id="txtCodAlmacen" runat="server" Font-Bold="True" width="30px" Font-Size="8px"></asp:label>--%>
           &nbsp;<asp:label id="txtDesAlmacen" runat="server" width="250px" Font-Size="8px"></asp:label></td>
				</tr>
				<tr>
					<td style="width:120px;" class="Descripcion">&nbsp;Fec Pedido:</td>
					<td style="width:380px;" valign="middle" align="left" colspan="2">
           &nbsp;<asp:label id="txtFechaPedido" runat="server" width="120px" Font-Size="8px"></asp:label></td>
				</tr>
				<tr>
					<td style="width:120px" class="Descripcion">&nbsp;Fec. Aprobacion:</td>
					<td style="width:380px" valign="middle" align="left" colspan="2">
           &nbsp;<asp:label id="txtFechaAprobacion" runat="server" width="120px" Font-Size="8px"></asp:label>
          </td>
				</tr>
        <tr>
					<td style="width:120px" class="Descripcion">&nbsp;Fec. Atencion:</td>
					<td style="width:380px" colspan="2">
						&nbsp;<asp:textbox id="txtFechaDespacho" runat="server" width="80px" CssClass="txtDeshabilitado2"></asp:textbox>
						&nbsp;<img onclick="popUpCalendar(this, frmRegistroDespachoPedidos.txtFechaDespacho, 'dd/mm/yyyy')"
							border="0" alt="Seleccionar fecha de Despacho" src="../../intranet/Imagenes/Calendario.gif"
							width="16px" height="16px" /></td>
				</tr>
				<tr>
					<td style="width:120px" class="Descripcion">&nbsp;Entregado a:</td>
					<td style="width:380px" valign="middle" align="left" colspan="2">
           <%--&nbsp;<asp:textbox id="txtCodRecepciona" runat="server" CssClass="txtDeshabilitado2" width="80px"></asp:textbox>--%>
           &nbsp;<asp:textbox id="txtCodRecepciona" runat="server" CssClass="txtHabilitado" width="80px"></asp:textbox>
           <%--MOFICICACION HUELLERO - INI--%>
            &nbsp;<asp:Button id="btncarga" runat="server" CssClass="Boton" Text="..." Width="20px" />
           <%--&nbsp;<asp:Button id="carga" runat="server" type="button" class="boton" Text="..." Width="20px"/>--%>
           <%--&nbsp;<input style="width:20px; height:20px" id="btnSolicitante" class="boton" onclick="javascript:fnc_BuscarTrabajadores();"	value="..." type="button" name="btnSolicitante" />--%>
           <%--&nbsp;<asp:imagebutton id="imgBuscarCliente" runat="server" ImageUrl="../../intranet/Imagenes/Buscar.gif" ToolTip="Consultar cliente"></asp:imagebutton>--%>
           <%--MOFICICACION HUELLERO - FIN--%>
           &nbsp;<asp:label id="lblDesRecepciona" runat="server" Font-Size="8px" Width="200px"></asp:label>
           <%--MOFICICACION HUELLERO - INI--%>
           <asp:CheckBox id="chkbox" runat="server" Text="Imprimir" Checked="false"/>
           <%--MOFICICACION HUELLERO - FIN--%>
          </td>
				</tr>
				<tr>
					<td style="width:120px" class="Descripcion">&nbsp;Obervaciones:</td>
					<td style="width:380px" colspan="2" valign="top">
           &nbsp;<asp:textbox id="txtObservaciones" runat="server" width="380px" CssClass="txtAreaDeshabilitado"	TextMode="MultiLine"></asp:textbox></td>
				</tr>
		</table>
        
      <table style="width:900px">
       <tr><td></td></tr>
			 <tr>
			  <td style="width:900px">
          <asp:label id="lblError" text="" runat="server" CssClass="mensaje"></asp:label>
        </td>
			 </tr>
      </table>

      <table style="width:900px">
       <tr>
				<td style="width:900px" align="right">
         <asp:label id="lblItems" text="Numero de Items" runat="server" CssClass="contador">Numero de Items:</asp:label>
        </td>
			 </tr>
			</table>
      <%--grilla--%>
			<table style="width:900px" border="0" cellspacing="0" cellpadding="0">
				<tr>
					<td style="width:900px" class="Descripcion" align="left">
           <asp:datagrid id="dgDetalle" runat="server" width="900px" AutoGenerateColumns="False" >
							<AlternatingItemStyle CssClass="GridAltItem"></AlternatingItemStyle>
							<ItemStyle CssClass="GridItem"></ItemStyle>
							<HeaderStyle CssClass="GridHeader"></HeaderStyle>
							<Columns>
								<asp:TemplateColumn HeaderText="Sec.">
									<HeaderStyle HorizontalAlign="Center" Width="20px" VerticalAlign="Middle"></HeaderStyle>
                                    
									<ItemStyle Font-Size="8px" HorizontalAlign="left" Width="20px"></ItemStyle>
									<ItemTemplate>
										<asp:Label id="lblSecuencia" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.nu_secu") %>'></asp:Label>
									</ItemTemplate>
                                    
                                    <EditItemTemplate>
                                        <asp:Label id="lblSecuencia_Edit" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.nu_secu") %>'></asp:Label>
                                    </EditItemTemplate>
								</asp:TemplateColumn>

								<asp:TemplateColumn HeaderText="Codigo">
									<HeaderStyle HorizontalAlign="Center" Width="100px"></HeaderStyle>
									<ItemStyle Font-Size="8px" HorizontalAlign="left" Width="100px"></ItemStyle>
									<ItemTemplate>
										<asp:Label id="lblCodigo" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.CO_ITEM") %>'></asp:Label>
                                        <asp:Label id="lblCentroCostos" runat="server"  Visible ="False" Text='<%# DataBinder.Eval(Container, "DataItem.CentroCostos") %>'></asp:Label>
										<asp:Label id="lblCtagasto" runat="server" Visible ="False" Text='<%# DataBinder.Eval(Container, "DataItem.CtaGasto") %>'></asp:Label>
										<asp:Label id="lblActivoFijo" runat="server" Visible = "false" Text='<%# DataBinder.Eval(Container, "DataItem.ActivoFijo") %>'></asp:Label>                                        
									</ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:Label id="lblCodigo_Edit" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.CO_ITEM") %>'></asp:Label>
                                        <asp:Label id="lblCentroCostos_Edit" runat="server"  Visible ="False" Text='<%# DataBinder.Eval(Container, "DataItem.CentroCostos") %>'></asp:Label>
										<asp:Label id="lblCtagasto_Edit" runat="server" Visible ="False" Text='<%# DataBinder.Eval(Container, "DataItem.CtaGasto") %>'></asp:Label>
										<asp:Label id="lblActivoFijo_Edit" runat="server" Visible = "false" Text='<%# DataBinder.Eval(Container, "DataItem.ActivoFijo") %>'></asp:Label>  
                                    </EditItemTemplate>
								</asp:TemplateColumn>

								<asp:TemplateColumn HeaderText="Descripción">
									<HeaderStyle HorizontalAlign="Center" width="250px" VerticalAlign="Middle"></HeaderStyle>
									<ItemStyle Font-Size="8px" HorizontalAlign="left" Width="250px"></ItemStyle>
									<ItemTemplate>
										<asp:Label id="lblDescripcion" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.de_item") %>'>
										</asp:Label>
									</ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:Label id="lblDescripcion_Edit" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.de_item") %>'>
										</asp:Label>
                                    </EditItemTemplate>
								</asp:TemplateColumn>

                                <asp:TemplateColumn HeaderText="Ubi.">
									<HeaderStyle HorizontalAlign="Center" width="50px" VerticalAlign="Middle"></HeaderStyle>
									<ItemStyle Font-Size="8px" HorizontalAlign="left" Width="50px"></ItemStyle>
									<ItemTemplate>
										<asp:Label id="lblUbicacion" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Ubicacion") %>'>
										</asp:Label>
									</ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:Label id="lblUbicacion_Edit" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Ubicacion") %>'>
										</asp:Label>
                                    </EditItemTemplate>
								</asp:TemplateColumn>

								<asp:TemplateColumn HeaderText="U.M.">
									<HeaderStyle HorizontalAlign="Center" Width="30px"></HeaderStyle>
									<ItemStyle Font-Size="8px" HorizontalAlign="center" width="30px"></ItemStyle>
									<ItemTemplate>
										<asp:Label id="lblUnidaMedida" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.CO_UNME") %>'>
										</asp:Label>
									</ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:Label id="lblUnidaMedida_Edit" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.CO_UNME") %>'>
										</asp:Label>
                                    </EditItemTemplate>
								</asp:TemplateColumn>

								<asp:TemplateColumn HeaderText="Can. Aprobada">
									<HeaderStyle HorizontalAlign="center" width="50px"></HeaderStyle>
									<ItemStyle Font-Size="8px" HorizontalAlign="right" width="50px"></ItemStyle>
									<ItemTemplate>
										<asp:Label id="lblCantidad" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.CA_APRO") %>'>
										</asp:Label>
									</ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:TextBox id="txtCantidad_Edi" runat="server" Width="50px" Text='<%# DataBinder.Eval(Container, "DataItem.CA_APRO") %>'>
										</asp:TextBox>
                                        <asp:HiddenField  id="txtCantidad_Edi_hiden" runat="server" value='<%# DataBinder.Eval(Container, "DataItem.CA_APRO") %>'> </asp:HiddenField>
                                    </EditItemTemplate>
								</asp:TemplateColumn>

								<asp:TemplateColumn HeaderText="Can. Pendiente">
									<HeaderStyle HorizontalAlign="center" width="50px"></HeaderStyle>
									<ItemStyle Font-Size="8px" HorizontalAlign="Right" width="50px"></ItemStyle>
									<ItemTemplate>
										<asp:Label id="lblCantidadPendiente" runat="server" Visible = "true" Text='<%# DataBinder.Eval(Container, "DataItem.CA_PEND") %>'>
										</asp:Label>
									</ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:Label id="lblCantidadPendiente_Edit" runat="server" Visible = "true" Text='<%# DataBinder.Eval(Container, "DataItem.CA_PEND") %>'>
										</asp:Label>
                                    </EditItemTemplate>
								</asp:TemplateColumn>

								<asp:TemplateColumn HeaderText="Can. Atendida">
									<HeaderStyle HorizontalAlign="center" width="50px"></HeaderStyle>
									<ItemStyle Font-Size="8px" HorizontalAlign="right" width="50px"></ItemStyle>
									<ItemTemplate>
										<asp:Label id="lblCantidadAtendida" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.CA_ATEN") %>'>
										</asp:Label>
									</ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:Label id="lblCantidadAtendida_Edit" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.CA_ATEN") %>'>
										</asp:Label>
                                    </EditItemTemplate>
								</asp:TemplateColumn>

								<asp:TemplateColumn HeaderText="Can. Reservada">
									<HeaderStyle HorizontalAlign="center" width="50px"></HeaderStyle>
									<ItemStyle Font-Size="8px" HorizontalAlign="Right" width="50px"></ItemStyle>
									<ItemTemplate>
										<asp:Label id="lblCantidadReservada" runat="server" Visible = "true" Text='<%# DataBinder.Eval(Container, "DataItem.ca_reservada") %>'>
										</asp:Label>
									</ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:Label id="lblCantidadReservada_Edit" runat="server" Visible = "true" Text='<%# DataBinder.Eval(Container, "DataItem.ca_reservada") %>'>
										</asp:Label>
                                    </EditItemTemplate>
								</asp:TemplateColumn>

								<asp:TemplateColumn HeaderText="Stock">
									<HeaderStyle HorizontalAlign="Center" width="50px"></HeaderStyle>
									<ItemStyle Font-Size="8px" HorizontalAlign="Right" width="50px"></ItemStyle>
									<ItemTemplate>
										<asp:Label id="lblStock" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Stock") %>'>
										</asp:Label>
									</ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:Label id="lblStock_Edit" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Stock") %>'>
										</asp:Label>
                                    </EditItemTemplate>
								</asp:TemplateColumn>

								<asp:TemplateColumn HeaderText="Can. Disponible">
									<HeaderStyle HorizontalAlign="Center" width="50px"></HeaderStyle>
									<ItemStyle Font-Size="8px" HorizontalAlign="Right" width="50px"></ItemStyle>
									<ItemTemplate>
										<asp:Label id="lblCantidadDisponible" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.ca_disponible") %>'>
										</asp:Label>
									</ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:Label id="lblCantidadDisponible_Edit" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.ca_disponible") %>'>
										</asp:Label>
                                    </EditItemTemplate>
								</asp:TemplateColumn>
                                
								<asp:TemplateColumn HeaderText="Situacion">
									<HeaderStyle HorizontalAlign="Center" width="70px"></HeaderStyle>
									<ItemStyle Font-Size="8px" HorizontalAlign="Center" width="70px"></ItemStyle>
									<ItemTemplate>
										<asp:Label id="lblsituacion" runat="server" Font-Bold="True"></asp:Label>
									</ItemTemplate>
								</asp:TemplateColumn>

								<asp:TemplateColumn HeaderText="Can. a Despachar">
									<HeaderStyle width="80px" HorizontalAlign="Center"></HeaderStyle>
									<ItemStyle HorizontalAlign="Right" width="80px"></ItemStyle>
									<ItemTemplate>
										<asp:TextBox ID="txtDespachable" runat="server" width="80px" CssClass="inputDerecha" Text='<%# DataBinder.Eval(Container, "DataItem.CA_DESP") %>'></asp:TextBox>
                                        <asp:Label id="lblTopeDespachable" runat="server" Visible = "false" Text='<%# DataBinder.Eval(Container, "DataItem.CA_DESP") %>'></asp:Label>
									</ItemTemplate>
								</asp:TemplateColumn>

                                 <asp:TemplateColumn HeaderText="Editar">
                                    <ItemStyle Width="80px"/>
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lnkEdit" runat="server" CausesValidation="false" Text="<img border=0 src=../images/im_edit.gif alt=editar>" CommandName="Edit"></asp:LinkButton>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:LinkButton ID="lnkUpdate" runat="server" Text="<img border=0 src=../images/save.gif alt=guardar/actualizar>" CommandName="Update">
                                            <img border="0" src="../images/save.gif"/ alt="guardar/actualizar" width="16" height="16">
                                        </asp:LinkButton>&nbsp;
                                        <asp:LinkButton ID="lnkCancel" runat="server" CausesValidation="false" Text="<img border=0 src=../images/delete.gif alt=cancelar>" CommandName="Cancel">
                                        </asp:LinkButton>
                                    </EditItemTemplate>
                                </asp:TemplateColumn>
							</Columns>
						</asp:datagrid>
                    </td>
				</tr>
                <tr><td style="height:10PX"></td></tr>
			    <tr align="center">
                    <td align="center" style="width:900px">
                        <table align="center" cellpadding="0" cellspacing="0" 
                            style="width: 900px; text-align: center;">
                            <tr>
                                <td style="text-align: left"><asp:button id="btnVales" runat="server" width="120px" toolTip="Ver vales generados" Text="Vales generados" CssClass="btnAzul"></asp:button></td>
                                <td><asp:button id="btnVerVale" runat="server" width="120px" toolTip="Vista previa de pedido" Text="Vista previa vale" CssClass="btnAzul"></asp:button></td>
                                <td><asp:button id="btnCulminar" runat="server" width="120px" tooltip="Culminar pedido" Text="Culminar vale" CssClass="btnRojo"></asp:button></td>
                                <td><asp:button id="btnDespachar" runat="server" width="120px" toolTip="Despachar Pedido" Text="Despachar vale" CssClass="btnVerde"></asp:button></td>
                                <td><asp:button id="btnPreDespachar" runat="server" width="120px" toolTip="Pre-Despacho" Text="Pre-Despacho" CssClass="btnNaranja"></asp:button></td>
                                <td style="text-align: right"><input id="btnSalir" onclick="javascript:fnc_Cerrar();" value="Salir" type="button"	name="btnSalir" runat="server" size="120px" class="btnAzul"/></td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td>
                        <input id="hdnCodEmpresa" style="WIDTH: 40px; HEIGHT: 22px" type="hidden" size="1" name="hdnCodEmpresa" runat="server" />
                        <input id="hdnCodUnidad" style="WIDTH: 40px; HEIGHT: 22px" type="hidden" size="1" name="hdnCodUnidad" runat="server" />
                        <input id="hdnCodAlmacen" style="WIDTH: 40px; HEIGHT: 22px" type="hidden" size="1" name="hdnCodAlmacen" runat="server" />
                        <input id="hdnCodReserva" style="WIDTH: 40px; HEIGHT: 22px" type="hidden" size="1" name="hdnCodReserva" runat="server" />
                        <input id="hdnTipoPed" style="width: 40px; height: 22px" type="hidden" size="1" name="hdnTipoPed" runat="server" />
                    </td>
                </tr>
			</table>
      
      </ContentTemplate>
      <Triggers>
       <asp:AsyncPostBackTrigger ControlID ="btnDespachar" EventName="click" />
      </Triggers>
      </asp:UpdatePanel>
     </center>
		</form>
	</body>
</html>
