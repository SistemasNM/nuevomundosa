<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="LOG20014.aspx.vb" Inherits="intranet_logi.LOG20014" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html>
	<head>
		<title></title>
		<meta content="Microsoft Visual Studio.NET 7.0" name="GENERATOR"/>
		<meta content="Visual Basic 7.0" name="CODE_LANGUAGE"/>
		<meta content="JavaScript" name="vs_defaultClientScript"/>
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema"/>
		<link href="../css/NM0001.css" type="text/css" rel="stylesheet"/>
		<script type="text/javascript" language="javascript" src="../../intranet/JS/jsCalendario_N3.js"></script>
		<script type="text/javascript" language="javascript" src="../../intranet/JS/functions.js"></script>
		<script type="text/javascript" language="javascript" src="../../enlacenm_costosreales/js/jsDesTabla.js"></script>
		<script type="text/javascript" language="JavaScript">

		    /*
            //20120904 EPM No se usa
		    function fnc_mostrardetalle(pstrcodigo)
		    {
		    var retorno = window.showModalDialog("PLA20011.aspx?pstrCodigo="+pstrcodigo,"","dialogHeight:400px;dialogWidth:840px;center:yes;help:no;");
		    //modalWin
		    retorno.close();
		    retorno=null;
		    return false;
		    }//end function
		    */
		
		    function modalWin(purl) 
		    {
			    if (window.showModalDialog) {
				    window.showModalDialog(purl,"name","dialogWidth:255px;dialogHeight:250px");
			    } else {
				    window.open(purl,"name","height=255,width=250,toolbar=no,directories=no,status=no, menubar=no,scrollbars=no,resizable=no ,modal=yes");
			    }//end if
		    }//end function

		    function popUp(strUrl) 
            {
			    var intWidth = screen.width;
			    var intHeight = screen.height;
			    window.open(strUrl, "", "top=0; left=0; width=" + intWidth + "; height=" + intHeight + "; resizable=1;");
		    }//end funcion			
			
		    function fnc_Eliminar(ControlID)
		    {
			    var lstrMensaje='Se eliminará el registro de -- '+document.all(ControlID+'_lblTrabDesI').innerHTML+' --.\n\n¿Esta seguro de continuar?';
			    return confirm(lstrMensaje);
			    //return false;
		    }//end function
		
		    function fnc_Update(ControlID)
		    {
			    var lstrMensaje='Se actualizará el reintegro de -- '+document.all(ControlID+'_lblTrabDesI').innerHTML+' --.\n\n¿Esta seguro de continuar?';
			    return confirm(lstrMensaje);
			    //return false;
		    }//end function				
		</script>
	    </head>
	<body>
		<form id="frmHESolicitud" name="frmHESolicitud" method="post" runat="Server">
		    <asp:ScriptManager ID="ScriptManager1" runat="server">
            </asp:ScriptManager>
		    <script language="JavaScript" src="../../intranet/Js/jsUpdateProgress.js" type="text/jscript" ></script>    
		    
            <asp:UpdateProgress ID="UpdateProg1" DisplayAfter="0" runat="server">
            <ProgressTemplate>     
                <center>
                <div id="divLoad" class="CssLoadNormal">                
                <br/>
                <img src="../../intranet/imagenes/Loading.gif" style="vertical-align:middle" /><br/>
                Procesando...                
                </div>   
                </center>
            </ProgressTemplate>
            </asp:UpdateProgress>
            
			<input id="hdnCodigo" style="WIDTH: 24px; HEIGHT: 22px" type="hidden" size="1" name="hdnCodigo"
				runat="server"><input id="hdnAccion" style="WIDTH: 24px; HEIGHT: 22px" type="hidden" size="1" name="hdnAccion"
				runat="server"> &nbsp;&nbsp;&nbsp;&nbsp;
			<table id="Table4" style="Z-INDEX: 101; LEFT: 0px; POSITION: absolute; TOP: 0px" cellspacing="2"
				cellpadding="2" width="1008" border="0">
				<tr>
					<td class="cabecera" style="HEIGHT: 22px">&nbsp;INVENTARIO DIARIO</td>
				</tr>
				<tr>
					<td class="panel">
					    <asp:UpdatePanel ID="panContenido" runat="server">
                        <ContentTemplate>
						<asp:Panel ID="panActualiza" runat="server" Visible="true">		
						<table style="WIDTH: 1016px; HEIGHT: 18px" cellspacing="0" cellpadding="0">
						    <tr>
						        <td class="Etiqueta" style="width:140px;">&nbsp; Código Inventario :</td>
						        <td style="width:150px;">
                                    <asp:TextBox ID="txtcodinv" runat="server" CssClass="input" MaxLength="10"></asp:TextBox>
                                </td>
                                <td class="Etiqueta">&nbsp; Estado:</td>
                                <td>
                                    <asp:Label ID="lblEstado" runat="server" Text="" CssClass="input"></asp:Label>
                                </td>
                                <td><asp:Button ID="btnConsultar" runat="server" CssClass="Boton" 
                                        Text="Consultar" /></td>
                                <td align="left"><asp:Button ID="btnNuevo" runat="server" CssClass="Boton" Text="Nuevo" /></td>
                                <td style="width:350px"></td>
						    </tr>
						    <tr>
						        <td class="Etiqueta" style="width:140px;" colspan="7">.</td>
						    </tr>
						    <tr>
						        <td style="width:140px;" colspan="7">
						        <nobr>
						        
						        &nbsp;&nbsp;&nbsp;<asp:Button ID="btnGuardar" runat="server" CssClass="Boton" Text="Guardar" />
						        &nbsp;&nbsp;&nbsp;<asp:Button ID="btnCerrar" runat="server" CssClass="Boton" Text="Cerrar" />
						            &nbsp;&nbsp;&nbsp;&nbsp;
                                    <asp:Button ID="btnImprimir" runat="server" CssClass="Boton" Text="Exportar" />
						        </nobr>
                                </td>
						    </tr>
							<tr>
								<td colspan="3">&nbsp;&nbsp;&nbsp;&nbsp;
									<asp:Label id="lblMensaje" runat="server" CssClass="input" ForeColor="Red"></asp:Label></td>
							</tr>
						</table>
						<table>
							<tr>
								<td><asp:datagrid id="DataGrid1" runat="server" Width="893px" 
                                        AutoGenerateColumns="False">
										<FooterStyle CssClass="GridFooter"></FooterStyle>
										<AlternatingItemStyle CssClass="GridAlternateItem"></AlternatingItemStyle>
										<ItemStyle CssClass="GridItem"></ItemStyle>
										<HeaderStyle CssClass="GridHeader"></HeaderStyle>
										<Columns>
											<asp:TemplateColumn>
												<HeaderStyle Width="10px"></HeaderStyle>
												<ItemTemplate>
													<NOBR>
														<asp:ImageButton id="ibtConsultar" runat="server" ImageUrl="../../intranet/Imagenes/buscar.gif"></asp:ImageButton>&nbsp;
													</NOBR>
												</ItemTemplate>
											</asp:TemplateColumn>
											<asp:BoundColumn DataField="de_tipo_item" HeaderText="Tipo">
                                                <HeaderStyle Width="100px" />
                                            </asp:BoundColumn>
                                            <asp:BoundColumn DataField="vch_item" HeaderText="Código Item">
                                                <HeaderStyle Width="150px" />
                                            </asp:BoundColumn>
                                            <asp:BoundColumn DataField="de_item" HeaderText="Descripción">
                                                <HeaderStyle Width="300px" />
                                            </asp:BoundColumn>
                                            <asp:BoundColumn DataField="co_unme" HeaderText="U.M.">
                                                <HeaderStyle Font-Bold="True" HorizontalAlign="Center" Width="30px" />
                                                <ItemStyle HorizontalAlign="center" />
                                            </asp:BoundColumn>
											<asp:BoundColumn DataField="num_stock" HeaderText="Stock" DataFormatString="{0:N2}">
                                                <HeaderStyle Font-Bold="True" HorizontalAlign="Center" Width="60px" />
                                                <ItemStyle HorizontalAlign="Right" />
                                            </asp:BoundColumn>
											<asp:TemplateColumn HeaderText="Cant. Inv.">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtcantinv" runat="server" CssClass="inputDerecha" 
                                                        Width="65px" Text='<%#Container.DataItem("num_cantinv")%>'></asp:TextBox>
                                                </ItemTemplate>
												<HeaderStyle HorizontalAlign="Center" Width="60px" Font-Bold="True" 
                                                    Font-Italic="False" Font-Overline="False" Font-Strikeout="False" 
                                                    Font-Underline="False" Wrap="False"></HeaderStyle>
												<ItemStyle HorizontalAlign="Center" Font-Bold="False" Font-Italic="False" 
                                                    Font-Overline="False" Font-Strikeout="False" Font-Underline="False"></ItemStyle>
											</asp:TemplateColumn>
										</Columns>
									</asp:datagrid></td>
							</tr>
						</table>
						</asp:Panel>
						</ContentTemplate>
		                 <triggers>
		                   <asp:AsyncPostBackTrigger ControlID="btnConsultar" EventName="click" />
                         </Triggers>
                        </asp:UpdatePanel>
						<input id="HDN2" style="WIDTH: 32px; HEIGHT: 22px" type="hidden" size="1" name="HDN2" runat="server">
						<input id="HDN1" style="WIDTH: 32px; HEIGHT: 22px" type="hidden" size="1" name="HDN1" runat="server">&nbsp;
						<input class="Input" id="HDN4" style="WIDTH: 32px; HEIGHT: 21px" type="hidden" size="1"
							name="hdnAprobacion" runat="server"><input id="HDN3" style="WIDTH: 32px; HEIGHT: 22px" type="hidden" size="1" name="HDN3" runat="server"><input id="HDN5" style="WIDTH: 32px; HEIGHT: 22px" type="hidden" size="1" name="HDN5" runat="server"></td>
				</tr>
			</table>
		</form>
	</body>
</html>
