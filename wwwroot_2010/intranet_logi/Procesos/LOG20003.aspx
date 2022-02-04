<%@ Register TagPrefix="cc2" Namespace="NM.Posicionador" Assembly="Posicionador" %>
<%@ Page Language="vb" AutoEventWireup="false" Codebehind="LOG20003.aspx.vb" Inherits="intranet_logi.LOG20003"%>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html>
	<head>
		<title>LOG20003</title>
		<base target="_self"/>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR"/>
		<meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE"/>
		<meta content="JavaScript" name="vs_defaultClientScript"/>
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema"/>
		<link href="../css/NM0001.css" type="text/css" rel="stylesheet"/>
		<script language="javascript" type="text/javascript">
			returnValue = "";
			function Resultado(Valor, Mensaje)
			{
			    if (Mensaje != "") {
			        alert(Mensaje);
			    }
                returnValue = Valor;
				window.close();
            }

//            function Desaprobar() {
//                var cMotivo = prompt("Ingrese el motivo de la Desaprobación. (Max. 200 Caracteres) ", "");
//                if (cMotivo != null) {
//                    document.getElementById("hdMotivo").value = cMotivo;
//                    return true;
//                } else {
//                    return false;
//                }
//            }

            function Rechazar(pMotivo) {
                var cMotivo = prompt("Ingrese el motivo del Rechazo. (Max. 200 Caracteres)  ", pMotivo);
                if (cMotivo != null) {
                    if (cMotivo.length > 200) {
                        alert('Solo se permiten 200 caracteres para el Motivo del Rechazo.');
                        return Rechazar(cMotivo.substring(0, 200));

                    } else {
                        if (cMotivo.length > 0) {
                            document.getElementById("hdMotivo").value = cMotivo;
                            var cAnular = confirm("¿Desea tambien Anular la Requisicion? ");
                            document.getElementById("hdAnular").value = cAnular;
                            return true;
                        }
                        else {
                            alert("Debe ingresar un Motivo para el Rechazo.");
                            return false;
                        }
                    }
                } else {
                    return false;
                }
            }
			
			function Cerrar()
			{
				window.close();				
			}
			function ConfirmarEli()
			{
				if (confirm("Está seguro de eliminar el registro."))
				{
					return true;	
				}else{
					return false;
				}
			
			}
			function popUp(strUrl) 
			{
				var intWidth = screen.width;
				var intHeight = screen.height;
				//window.open(strUrl);
				window.open(strUrl, "", "top=0; left=0; width=" + intWidth + "; height=" + intHeight + "; resizable=1;");
            }

            //fnc_AdjuntarDocs
            function fnc_AdjuntarDocs(strSecuencia) {
            var pstrTipoDoc = "RQS"
            var pstrNumeroDoc = document.all('txtDocumento').value
            var pstrSecuencia = " "
            var retorno = window.showModalDialog("frm_ListadoArchivoAdjunto.aspx?pstrTipoDoc=" + pstrTipoDoc + "&pstrNumeroDoc=" + pstrNumeroDoc + "&pstrSecuencia=" + pstrSecuencia, "Lista adjuntos", "dialogHeight:400px;dialogWidth:700px;center:yes;help:no;");
            return false;
        }

        //REQSIS201900029 - DG - INI
        function btnEditarFecha_Onclick() {
            if (document.all('txtDocumento').value == '') {
                return false;
            }
            var intCodigoPedido = document.all('txtDocumento').value;
            var retorno = window.showModalDialog("frm_ModificarFechaInstalacion.aspx?intCodigoPedido=" + intCodigoPedido + "&intTipo=" + 2, "", "dialogheight:500px;dialogwidth:800px;center:yes;help:no;status=no");
            Form1.hdnFlg.value = "1";
        }
        function fnc_ConfirmarOperacion() {
            if (confirm("Esta seguro de reiniciar el proceso. \n Volvera a instancia del creador de la requisición.") == true) {
                return true;
            } else {
                return false;
            }
        }
        //REQSIS201900029 - DG - FIN
		</script>
	</head>
	<body>
		<form id="Form1" method="post" runat="server">
			<table id="Table2" cellspacing="2" cellpadding="2" width="724" align="center" border="0">
				<tr>
					<td class="Cabecera">&nbsp;REQUISICIÓN</td>
				</tr>
				<tr>
					<td class="panel">
						<table id="Table1" width="100%" border="0">
							<tr>
								<td class="Etiqueta" align="left">
                                    <asp:HiddenField ID="hdMotivo" runat="server" Value="" />
                                    <asp:HiddenField ID="hdAnular" runat="server" Value="" />
									<asp:Label id="lblEstado" runat="server" CssClass="etiqueta" Width="640px" Font-Size="X-Small" Font-Bold="True"></asp:Label>
                                </td>
							</tr>
						</table>
						<table width="100%" border="0">
							<tr>
								<td class="Etiqueta" width="85">
									<asp:Panel id="Panel1" runat="server" Width="90px">Número</asp:Panel></td>
								<td width="120"><span style="WIDTH: 115px"><asp:textbox id="txtDocumento" runat="server" CssClass="input"></asp:textbox></span></td>
								<td width="100%"><asp:label id="lblTipo" runat="server" Width="424px" Font-Size="X-Small" Font-Bold="True" CssClass="Input"></asp:label></td>
							</tr>
						</table>
						<table width="100%" border="0">
							<tr>
								<td class="Etiqueta" width="85">
									<asp:Panel id="Panel2" runat="server" Width="90px">Fecha de emisión</asp:Panel></td>
								<td width="120"><asp:textbox id="txtEmision" runat="server" CssClass="input"></asp:textbox></td>
								<td class="Etiqueta" width="85">
									<asp:Panel id="Panel3" runat="server" Width="90px">Fecha tope</asp:Panel>
								</td>
								<td width="120">
                                    <asp:textbox id="txtFechaTope" runat="server" CssClass="input" Width="100px"></asp:textbox>
                                    <asp:ImageButton id="btnEditarFecInstal" runat="server" ImageUrl="../../intranet/Imagenes/editar.gif"  Width="16px" Height="16px"></asp:ImageButton>
                                </td>
								<td class="Etiqueta" width="85">
									<asp:Panel id="Panel4" runat="server" Width="90px">Comprador</asp:Panel></td>
								<td width="139"><asp:textbox id="txtComprador" runat="server" CssClass="input" Width="132px"></asp:textbox></td>
							</tr>
						</table>
						<table id="tblCentroCosto" width="100%" border="0" runat="server">
							<tr>
								<td class="Etiqueta" width="85">
									<asp:Panel id="Panel5" runat="server" Width="90px">Centro de costo</asp:Panel>
									&nbsp;
								</td>
								<td width="100%"><asp:textbox id="txtCentroCosto" runat="server" CssClass="input" Width="100%"></asp:textbox></td>
							</tr>
						</table>
						<table width="100%" border="0">
							<tr>
								<td class="Etiqueta" width="85">
									<asp:Panel id="Panel6" runat="server" Width="90px">Unidad</asp:Panel></td>
								<td width="120"><asp:textbox id="txtUnidad" runat="server" CssClass="input"></asp:textbox></td>
								<td class="Etiqueta" width="85">
									<asp:Panel id="Panel7" runat="server" Width="90px">Almacén</asp:Panel></td>
								<td width="100%"><asp:textbox id="txtAlmacen" runat="server" CssClass="input" Width="100%"></asp:textbox></td>
							</tr>
						</table>
						<table width="100%" border="0">
							<tr>
								<td class="Etiqueta" valign="top" width="85">
									<asp:Panel id="Panel8" runat="server" Width="90px">Observaciones</asp:Panel></td>
								<td width="100%"><asp:textbox id="txtObservaciones" runat="server" CssClass="input" Width="100%"
										Height="56px" TextMode="MultiLine"></asp:textbox>
                                
                                        </td>
							</tr>
						</table>
						<div style="WIDTH:734px; HEIGHT:170px; OVERFLOW:auto">
                        <asp:datagrid id="dtgDetalle" runat="server" Width="710px" AutoGenerateColumns="False">
                        		<AlternatingItemStyle CssClass="gridalternateitem"></AlternatingItemStyle>
								<ItemStyle CssClass="griditem"></ItemStyle>
								<HeaderStyle CssClass="gridheader"></HeaderStyle>
								<Columns>
                                <asp:TemplateColumn>
                                <HeaderStyle Width="50px"></HeaderStyle>
                                <ItemTemplate>
                                    <asp:Label id="lblSecuencia" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.var_Secuencia") %>'></asp:Label>
								</ItemTemplate>
                                </asp:TemplateColumn>
									<%--<asp:BoundColumn DataField="var_Secuencia" ReadOnly="True"></asp:BoundColumn>--%>
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
                                    <asp:BoundColumn DataField="vch_CodOrdenTrabajo" ReadOnly="True" HeaderText="OT"></asp:BoundColumn>
									<asp:BoundColumn DataField="var_CodigoAuxiliar" ReadOnly="True" HeaderText="Centro Costo"></asp:BoundColumn>
									<asp:BoundColumn DataField="var_CuentaGasto" ReadOnly="True" HeaderText="Cuenta Gasto"></asp:BoundColumn>
									<asp:TemplateColumn>
										<HeaderStyle Width="50px"></HeaderStyle>
										<ItemTemplate>
											<asp:ImageButton id="ibtEditar" runat="server" ImageUrl="../../intranet/Imagenes/Editar.gif" CommandName="Editar" ToolTip="Editar"></asp:ImageButton>
											<asp:ImageButton id="ibtEliminar" runat="server" ImageUrl="../../intranet/Imagenes/Eliminar.gif" CommandName="Eliminar" ToolTip="Eliminar"></asp:ImageButton>
											<asp:ImageButton id="ibtReactivar" runat="server" Width="13px" Height="14px" ImageUrl="../../intranet/Imagenes/Reactivar.gif" CommandName="Reactivar"  ToolTip="Reactivar"></asp:ImageButton>
                                            <asp:ImageButton id="btnAjuntarS" runat="server" CommandName="Adjuntar" ImageUrl="../../intranet/Imagenes/adjuntar_16x16.bmp" ToolTip="Ver archivos adjuntos"></asp:ImageButton>
										</ItemTemplate>
										<EditItemTemplate>
											<asp:ImageButton id="ibtCancelar" runat="server" ImageUrl="../../intranet/Imagenes/Cancelar.gif" CommandName="Cancelar" ToolTip="Cancelar"></asp:ImageButton>
											<asp:ImageButton id="ibtGrabar" runat="server" ImageUrl="../../intranet/Imagenes/Grabar.gif" CommandName="Grabar" ToolTip="Grabar"></asp:ImageButton>
										</EditItemTemplate>
									</asp:TemplateColumn>
								</Columns>
							</asp:datagrid></div>
						<table class="botonera" id="Table9" cellspacing="2" cellpadding="2" width="100%" border="0">
							<tr>
								<td align="right">
                                    <asp:button id="btnImprimir" runat="server" CssClass="boton" Text="Imprimir"></asp:button>
                                    <asp:button id="btnAnular" runat="server" CssClass="boton" Text="Anular"></asp:button>
                                    <asp:button id="btnAprobar" runat="server" CssClass="boton" Text="Aprobar" BackColor="#006600"></asp:button>
                                    <asp:button id="btnDesaprobar" runat="server" CssClass="boton" Text="Desaprobar" Visible="False" BackColor="Red"></asp:button>
                                    <asp:button id="btnRechazar" runat="server" CssClass="boton" Text="Rechazar" Visible="False"></asp:button>
                                    <%--REQSIS201900029 - DG - INI--%>
                                    <asp:button id="btnReiniciar" Runat="server" CssClass="boton" Text="Reiniciar" width="100px" BackColor="Orange"></asp:button>
                                    <%--REQSIS201900029 - DG - FIN--%>
                                </td>
							</tr>
						</table>
                        <table>
                            <tr>
		                        <td>
                                    <input id="hdnFlg" style="WIDTH: 40px; HEIGHT: 22px" type="hidden" size="1" name="hdnFlg" runat="server" /> 
                                </td>
                            </tr>
                        </table>
					</td>
				</tr>
			</table>
		</form>
	</body>
</html>
