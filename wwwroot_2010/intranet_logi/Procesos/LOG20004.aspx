<%@ Register TagPrefix="cc1" Namespace="NM.Posicionador" Assembly="Posicionador" %>
<%@ Page Language="vb" AutoEventWireup="false" Codebehind="LOG20004.aspx.vb" Inherits="intranet_logi.LOG20004"%>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html>
	<head>
		<title>LOG20004</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR"/>
		<meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE"/>
		<meta content="JavaScript" name="vs_defaultClientScript"/>
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema"/>
        <script src="../js/jsCalendario_N3.js" type="text/javascript"></script>
        <link href="../css/NM0001.css" rel="stylesheet" type="text/css" />
		<script language="javascript" type="text/javascript">
		    function popUp(strUrl) 
		    {
			    var intWidth = screen.width;
			    var intHeight = screen.height;
			    //window.open(strUrl);
			    window.open(strUrl, "", "top=0; left=0; width=" + intWidth + "; height=" + intHeight + "; resizable=1;");
		    }
		    /*
            //20120906 EPM Se comenta porque no se usa.
		    function MostrarRequisicion (Requisicion,Estado)
		    {
		    var retorno = window.showModalDialog("LOG20003.aspx?strEstado="+Estado+"&strRequisicion="+Requisicion,"","dialogHeight:500px;dialogWidth:750px;center:yes;help:no;");
		    if (retorno=="1" )
		    {
		    document.getElementById("hdn1.value="1";
		    Form1.submit;
		    }
		    }
		    */
		    /*
            //20120906 EPM Se comenta porque no se usa.
		    function MostrarSeleccion (Requisicion,Orden,Estado)
		    {
		    var retorno = window.showModalDialog("LOG20008.aspx?strEstado="+Estado+"&strOrden="+Orden+"&strRequisicion="+Requisicion,"","dialogHeight:150px;dialogWidth:350px;center:yes;help:no;");
		    }
		    */
		    /*
            //20120906 EPM Se comenta porque no se usa.
		    function MostrarOrdenImpresion(strOrdenCompra)
		    {
		    var lstrURL = '';
		    lstrURL = '../../../CrystalReports/_Logistica.asp?strFormulario=LOG20006&strOC=' + strOrdenCompra;
		    popUp(lstrURL);
		    }
		    */
		    function MostrarDetalle(pstrrequisicion,pstrordencompra)
		    {
			    if(pstrrequisicion == null || pstrrequisicion==""){
				    pstrrequisicion = ""
			    }
			    if(pstrordencompra == null || pstrordencompra==""){
				    pstrordencompra = ""
			    }
			    var retorno = window.showModalDialog("LOG20009.aspx?pstrrequisicion="+pstrrequisicion+"&pstrordencompra="+pstrordencompra,"","dialogHeight:500px;dialogWidth:800px;center:yes;help:no;");
			} //end function

		    function BuscarUsuario()
			{
				var txtUsuario = document.all("txtSolicitador");
				var lblUsuario = document.all("lblSolicitada");
				var oXml = new ActiveXObject("Microsoft.XMLDOM");
				txtUsuario.value = txtUsuario.value.toUpperCase();
				oXml.async = false;
				oXml.validateOnParse = false;
					
				oXml.load("../NMGetXml.aspx?Opcion=Usuario&Codigo=" + txtUsuario.value);
				root = oXml.documentElement;
				if (root.childNodes.length>0)
				{
					var opc = root.getElementsByTagName("Datos")(0);
					if (opc.getAttribute("UsuarioCodigo").toUpperCase() == txtUsuario.value.toUpperCase() )
					{
						txtUsuario.value = opc.getAttribute("UsuarioCodigo");
						lblUsuario.innerHTML = opc.getAttribute("UsuarioNombreCompleto");
						document.getElementById("hdnSolicitador").value = opc.getAttribute("UsuarioNombreCompleto");
					}
					else
					{
						lblUsuario.innerHTML = "";
						document.getElementById("hdnSolicitador").value = "";
					}
					
				}
            }

		    function BuscarProveedor()
			{
				var txtProveedor = document.all("txtProveedor");
				var lblProveedor = document.all("lblProveedor");
				var oXml = new ActiveXObject("Microsoft.XMLDOM");
				oXml.async = false;
				oXml.validateOnParse = false;
					
				oXml.load("../NMGetXml.aspx?Opcion=Proveedor&Codigo=" + txtProveedor.value);
				root = oXml.documentElement;
				if (root.childNodes.length>0)
				{
					var opc = root.getElementsByTagName("Datos")(0);
					if (txtProveedor.value.toUpperCase() == opc.getAttribute("ProveedorCodigo").toUpperCase())
					{
						lblProveedor.innerHTML = opc.getAttribute("ProveedorNombre");
						document.getElementById("hdnProveedor").value = opc.getAttribute("ProveedorNombre");
					}
					else
					{
						lblProveedor.innerHTML = "";
						document.getElementById("hdnProveedor").value = "";
					}
				}
			}//end function
		    function ListarUsuario()
		    {
			    var retorno = window.showModalDialog("../../intranet/Buscadores/SEG_0004.aspx","","dialogHeight:450px;dialogWidth:450px;center:yes;help:no;");
			    if (retorno!=":") 
			    {
				    if (retorno!="")
				    {
					    var datos = retorno.split(":");
					    var Codigo=datos[0];
					    var Articulo=datos[1];
					    //alert(Articulo);
					    var txtSolicitador = document.all["txtSolicitador"];
					    var lblSolicitador = document.all["lblSolicitada"];
					    txtSolicitador.value=Codigo=datos[0];
					    lblSolicitador.innerHTML =datos[1];
					    document.getElementById("hdnSolicitador").value=datos[1];
				    }
			    }
            } //end function	

		    function ListarProveedor()
		    {
			    var retorno = window.showModalDialog("../../intranet/Buscadores/LOG_0003.aspx","","dialogHeight:450px;dialogWidth:450px;center:yes;help:no;");
			    if (retorno!=":") 
			    {
				    if (retorno!="")
				    {
					    var datos = retorno.split(":");
					    var Codigo=datos[0];
					    var Articulo=datos[1];
					    var txtProveedor = document.all["txtProveedor"];
					    var lblProveedor = document.all["lblProveedor"];
					    txtProveedor.value=Codigo=datos[0];
					    lblProveedor.innerHTML =datos[1];
					    document.getElementById("hdnProveedor").value = datos[1];
				    }
			    }
		    }//end function
			
		    function EnviarOC_onClick(pstrDocumento)
		    {
			    //verificar datos
			    hdnOrdenCompra=document.all["hdnOrdenCompra"];
			    hdnOrdenCompra.value='';
			    var lstrNumero=pstrDocumento;
			    if(lstrNumero=='')
			    {
				    alert('La orden de compra/servicio -- ' + lstrNumero + ' -- no existe.');
				    return false;
			    }//end if
			    //datos de OC
			    var response=LOG20004.fnc_VerificarOC_paraEnvio(lstrNumero);
			    if(response!=null && typeof(response)=="object")
			    {
				    var ldtbResultado=response.value;
				    if(ldtbResultado.Rows.length<=0)
				    {
					    alert('La orden de compra/servicio -- '+ lstrNumero +' -- no existe.');
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
					    hdnOrdenCompra.value=lstrNumero;
					    return confirm('La orden de compra/servicio -- '+ lstrNumero +' -- se enviará a la cuenta del proveedor\n-- '+ldtbResultado.Rows[0].prv_de_mail + ' --.\n\n¿ Esta seguro de continuar ?');
				    }
				    else//reenvio
				    {
					    hdnOrdenCompra.value=lstrNumero;
					    return confirm('La orden de compra/servicio -- '+ lstrNumero +' -- se enviará a la cuenta del proveedor -- '+ldtbResultado.Rows[0].prv_de_mail + ' --.\n\nEsta orden de servicio se envió el ' + ldtbResultado.Rows[0].fe_usua_envi2 + ' por el usuario '+ ldtbResultado.Rows[0].co_usua_envi +'\n\n¿ Esta seguro de continuar ?');
				    }//end if
				    //mensaje de aceptacion
			    }
			    else
			    {
				    alert('La orden de compra/servicio no existe.');
				    return false;
			    }//end if
		    }//end function			
		
		    function FormatearBusqDoc(pTexto)
		    {
			    if(pTexto==1)//serie
			    {
				    var lserie='000000'+document.all["txtdocserie"].value;
				    lserie=lserie.substring(lserie.length, lserie.length-4);
				    if (lserie=='0000')
				    {
					    document.all["txtdocserie"].value='';
				    }else{
					    document.all["txtdocserie"].value=lserie;
				    }//end if
				
			    }//end if
			
			    if(pTexto==2)//numero
			    {
				    var lnume='00000000000'+document.all["txtdocnumero"].value;
				    lnume=lnume.substring(lnume.length,lnume.length-10);
				    if (lnume=='0000000000')
				    {
					    document.all["txtdocnumero"].value='';
				    }else{
					    document.all["txtdocnumero"].value=lnume;
				    }//end if
				
			    }//end if
		    }//end function
		
		</script>
	    <style type="text/css">
            .style1
            {
                font-weight: bold;
                font-size: 9px;
                color: #333333;
                background-color: #BCCAE0;
                font-family: Verdana;
                text-decoration: none;
                width: 80px;
            }
        </style>
	</head>
	<body id="Cuerpo">
		<form id="Form1" method="post" runat="server">
			<table id="Table1" 
                style="Z-INDEX: 101; POSITION: absolute; WIDTH: 1079px; HEIGHT: 477px; TOP: 0px; LEFT: 0px" 
                cellspacing="2" cellpadding="2" border="0">
				<tr>
					<td class="Cabecera">&nbsp;SEGUIMIENTO DE DOCUMENTOS DE LOGISTICA (REQ, O/C, O/S)</td>
				</tr>
				<tr>
					<td class="Panel" valign="top">
						<table cellspacing="2" cellpadding="2" width="840" border="0">
							<tr>
								<td class="Etiqueta" width="80" style="WIDTH: 80px">Tipos</td>
								<td width="210"><asp:radiobutton id="rbtTodos" runat="server" Checked="True" CssClass="input" GroupName="Tipos" Text="Todos"></asp:radiobutton><asp:radiobutton id="rbtArticulos" runat="server" CssClass="input" GroupName="Tipos" Text="Artículos"></asp:radiobutton><asp:radiobutton id="rbtServicios" runat="server" CssClass="input" GroupName="Tipos" Text="Servicios"></asp:radiobutton></td>
								<td class="Etiqueta" width="80" style="WIDTH: 80px">Documento :</td>
								<td>
									<asp:radiobutton id="rbtdocreq" runat="server" Text="REQ" GroupName="TiposDoc" CssClass="input"></asp:radiobutton>
									<asp:radiobutton id="rbtdococm" runat="server" Text="OCM" GroupName="TiposDoc" CssClass="input" Checked="True"></asp:radiobutton>&nbsp;
									<asp:textbox id="txtdocserie" runat="server" CssClass="input" Width="48px" MaxLength="4"></asp:textbox>&nbsp;
									<asp:textbox id="txtdocnumero" runat="server" CssClass="input" Width="104px" MaxLength="10"></asp:textbox><FONT style="BACKGROUND-COLOR: #ffff66" face="Calibri" size="1"><EM>*Dejar en blanco para otras busquedas</EM></FONT></td>
							</tr>
						</table>
						<table cellspacing="2" cellpadding="2" width="600" border="0">
							<tr>
								<td width="91" class="Etiqueta" style="WIDTH: 91px">Fecha creación
								</td>
								<td width="67"><asp:checkbox id="cbxDesde" runat="server" Checked="True" CssClass="Input" Text="Desde" AutoPostBack="True"></asp:checkbox></td>
								<td width="104"><asp:textbox id="txtDesde" runat="server" CssClass="input" Width="104px"></asp:textbox></td>
								<td width="13"><img id="imgDesde" onclick="popUpCalendar(this, Form1.txtDesde, 'dd/mm/yyyy')" height="15"
										alt="Seleccionar fecha" src="../../intranet/Imagenes/Calendario.gif" width="13" border="0" name="imgDesde"
										runat="server"></td>
								<td width="65"><asp:checkbox id="cbxHasta" runat="server" Checked="True" CssClass="Input" Text="Hasta" AutoPostBack="True"></asp:checkbox></td>
								<td width="104"><span style="WIDTH: 103px"><asp:textbox id="txtHasta" runat="server" CssClass="input" Width="104px"></asp:textbox></span></td>
								<td width="157"><img id="imgHasta" onclick="popUpCalendar(this, Form1.txtHasta, 'dd/mm/yyyy')" height="15"
										alt="Seleccionar fecha" src="../../intranet/Imagenes/Calendario.gif" width="13" border="0" runat="server"></td>
							</tr>
						</table>
						<table style="WIDTH: 968px" cellspacing="2" cellpadding="2" border="0">
							<tr>
								<td class="style1" vAlign="top">Estado</td>
								<td width="720" style="WIDTH: 720px"><asp:checkboxlist id="cblRequisicionesEstados" 
                                        runat="server" CssClass="input" Width="864px" RepeatColumns="6"></asp:checkboxlist></td>
							</tr>
						</table>
						<table cellspacing="2" cellpadding="2" width="800" border="0">
							<tr>
								<td class="Etiqueta" width="81" style="WIDTH: 81px">Solicitada por
								</td>
								<td width="112"><asp:textbox id="txtSolicitador" runat="server" CssClass="input" Width="104px"></asp:textbox></td>
								<td width="21"><input class="Boton" id="btnSolicitador" style="WIDTH: 20px; HEIGHT: 20px" onclick="ListarUsuario();"
										type="button" value="..." name="btnSolicitador" runat="server"></td>
								<td width="377"><asp:label id="lblSolicitada" runat="server" CssClass="Input" Width="100%"></asp:label></td>
                                 <td class="Etiqueta" width="80" style="width:80px">Total:</td>
                                 <td width="112"><asp:Label id="lblTotal" runat="server" CssClass="input" Width="100%"></asp:Label></td>
							</tr>
							<tr>
								<td class="Etiqueta" width="81" style="WIDTH: 81px">Proveedor</td>
								<td width="112"><asp:textbox id="txtProveedor" runat="server" CssClass="input" Width="104px"></asp:textbox></td>
								<td width="21"><input class="Boton" id="btnProveedor" style="WIDTH: 20px; HEIGHT: 20px" onclick="ListarProveedor();"
										type="button" value="..." name="btnProveedor" runat="server"></td>
								<td width="377"><asp:label id="lblProveedor" runat="server" CssClass="Input" Width="100%"></asp:label></td>
                                <td class="Etiqueta" width="80" style="width:80px">Moneda:</td>
                                <td width="112"><asp:Label id="lblMoneda" runat="server" CssClass="input" Width="100%">Soles</asp:Label></td>
							</tr>
                               
						</table>
						<table cellspacing="2" cellpadding="2" width="600" border="0">
							<tr>
								<td class="Etiqueta" vAlign="top" width="79" style="WIDTH: 79px">Observaciones</td>
								<td width="510"><asp:textbox id="txtObservaciones" runat="server" CssClass="input" Width="100%"></asp:textbox></td>
							</tr>
						</table>
						<table class="botonera" id="Table3" cellspacing="2" cellpadding="2" width="100%" border="0">
							<tr>
								<td align="right"><input id="hdnOrdenCompra" type="hidden" name="hdnOrdenCompra" runat="server" class="input"><input class="Input" id="hdnProveedor" type="hidden" name="hdn1" runat="server"><input class="Input" id="hdnSolicitador" type="hidden" name="hdn1" runat="server"><input class="Input" id="hdn1" type="hidden" name="hdn1" runat="server">
									<asp:button id="btnConsultar" runat="server" CssClass="Boton" Text="Consultar"></asp:button></td>
							</tr>
						</table>
						<asp:datagrid id="dtgLista" runat="server" Width="1066px" 
                            AutoGenerateColumns="False" Height="232px">
							<AlternatingItemStyle CssClass="GridAlternateItem"></AlternatingItemStyle>
							<ItemStyle CssClass="GridItem"></ItemStyle>
							<HeaderStyle CssClass="gridheader"></HeaderStyle>
							<Columns>
								<asp:TemplateColumn>
									<HeaderStyle Width="60px"></HeaderStyle>
									<ItemTemplate>
										<asp:ImageButton id="ibtConsultar" runat="server" ImageUrl="~/images/buscar.gif" ToolTip = "Consultar"></asp:ImageButton>
										<asp:ImageButton id="ibtenviarcorreo" runat="server" ImageUrl="~/images/img_email.jpg" ToolTip = "Enviar Correo" Visible="False" CommandName="EnviarOC"></asp:ImageButton>
										<asp:ImageButton id="ibtBloquear" runat="server" ImageUrl="~/images/Unlock.bmp" ToolTip = "Bloquear" CommandName="Bloquear"></asp:ImageButton>
										<asp:ImageButton id="ibtDesbloquear" runat="server" ImageUrl="~/images/Lock.bmp" ToolTip = "Desbloquear" CommandName="Desbloquear"></asp:ImageButton>
										<asp:ImageButton id="IbtAnular" runat="server" ImageUrl="~/images/borrador.gif" ToolTip = "Anular" CommandName="Desbloquear"></asp:ImageButton>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:BoundColumn DataField="var_Tipo" HeaderText="Tipo"></asp:BoundColumn>
								<asp:BoundColumn DataField="var_Estado" HeaderText="Estado"></asp:BoundColumn>
								<asp:BoundColumn DataField="var_OrdenCompra_2" HeaderText="OC">
									<HeaderStyle HorizontalAlign="Center" Width="100px"></HeaderStyle>
									<ItemStyle HorizontalAlign="Center"></ItemStyle>
								</asp:BoundColumn>
								<asp:BoundColumn Visible="False" DataField="var_OrdenCompra" HeaderText="OC"></asp:BoundColumn>
								<asp:BoundColumn DataField="var_UsuarioCreador" HeaderText="Solicitada por"></asp:BoundColumn>								
                                <asp:BoundColumn DataField="FE_EMIS_REQI" HeaderText="Fecha Ini. Requi."></asp:BoundColumn>
                                <asp:BoundColumn DataField="FE_EXPI_REQI" HeaderText="Fecha Fin Requi."></asp:BoundColumn>
                                <asp:BoundColumn DataField="var_FechaCreacion" HeaderText="Fecha Aprob. Requi."></asp:BoundColumn>
                                <asp:BoundColumn DataField="var_FechaDocumento" HeaderText="Fecha O/C"></asp:BoundColumn>
								<asp:BoundColumn DataField="var_Documento_2" HeaderText="NRO REQ.">
									<HeaderStyle HorizontalAlign="Center" Width="100px"></HeaderStyle>
									<ItemStyle HorizontalAlign="Center"></ItemStyle>
								</asp:BoundColumn>
                                <asp:BoundColumn DataField="estado_postular" HeaderText="Postulacion"></asp:BoundColumn>
                                <asp:BoundColumn DataField="FE_CIE_REQI" HeaderText="Fecha Cierre Proceso"></asp:BoundColumn>
								<asp:BoundColumn Visible="false" DataField="var_Documento" HeaderText="Documento"></asp:BoundColumn>
								<asp:BoundColumn Visible="False" DataField="var_Unidad" HeaderText="Unid."></asp:BoundColumn>
								<asp:BoundColumn Visible="False" DataField="var_Monto" HeaderText="Monto">
									<HeaderStyle HorizontalAlign="Right"></HeaderStyle>
									<ItemStyle HorizontalAlign="Right"></ItemStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="var_Observaciones" HeaderText="Observaciones"></asp:BoundColumn>
								<asp:BoundColumn DataField="var_ProveedorNombre" HeaderText="Proveedor"></asp:BoundColumn>
								<asp:BoundColumn DataField="var_Moneda" HeaderText="Moneda"></asp:BoundColumn>
								<asp:BoundColumn DataField="int_ImporteOrigen" HeaderText="Imp. Origen" DataFormatString="{0:#,##0.00}"></asp:BoundColumn>
								<asp:BoundColumn DataField="int_ImporteSoles" HeaderText="Imp. Soles" DataFormatString="{0:#,##0.00}"></asp:BoundColumn>
                                <asp:BoundColumn DataField="NU_OT" HeaderText="#OT">
									<HeaderStyle HorizontalAlign="Center" Width="90px"></HeaderStyle>
								</asp:BoundColumn>
							</Columns>
						</asp:datagrid></td>
				</tr>
			</table>
			<cc1:posicionador id="Posicionador1" style="Z-INDEX: 102; POSITION: absolute; TOP: 552px; LEFT: 24px" runat="server" Controlcito="Cuerpo"></cc1:posicionador>
        </form>
	</body>
</html>
