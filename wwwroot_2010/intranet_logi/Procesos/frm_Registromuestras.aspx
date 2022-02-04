<%@ Page Language="vb" AutoEventWireup="false" Codebehind="frm_Registromuestras.aspx.vb" Inherits="intranet_logi.frm_Registromuestras"%>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html>
	<head>
		<title></title>
		<meta content="Microsoft Visual Studio.NET 7.0" name="GENERATOR"/>
		<meta content="Visual Basic 7.0" name="CODE_LANGUAGE"/>
		<meta content="JavaScript" name="vs_defaultClientScript"/>
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema"/>
		<link href="../css/NM0001.css" type="text/css" rel="stylesheet"/>
		<script language="javascript" src="../../intranet/JS/jsCalendario_N4.js" type="text/javascript"></script>
		<script language="javascript" src="../../intranet/JS/functions.js" type="text/javascript"></script>
		
	    <style type="text/css">
            .style1
            {
                width: 4%;
            }
        </style>
		
	</head>
	<body>
		<form id="frmSolicitudMuestras" name="frmSolicitudMuestras" method="post" runat="Server">
			<input id="hdnCodigo" style="WIDTH: 24px; HEIGHT: 22px" type="hidden" size="1" name="hdnCodigo"
				runat="server" /><input id="hdnAccion" style="WIDTH: 24px; HEIGHT: 22px" type="hidden" size="1" name="hdnAccion"
				runat="server" /> &nbsp;&nbsp;&nbsp;&nbsp;
			<table id="Table4" 
				cellspacing="2" cellpadding="2" width="949" border="0">
				<tr>
					<td class="cabecera" style="HEIGHT: 22px">&nbsp;FORMULARIO DE SOLICITUD DE MUESTRAS Y ETIQUETAS
						<asp:label id="lblReg" runat="server" style="DISPLAY:none"></asp:label></td>
				</tr>
                <br />
				<tr>
					<td class="panel" valign="top">
						<table style="WIDTH: 720px; HEIGHT: 16px">
								<tr>
									<td class="Etiqueta" style="WIDTH: 103px" Align="middle">&nbsp;Tipo Solicitud:</td>
									<td style="WIDTH: 155px"><asp:dropdownlist id="ddlTipo" runat="server" Width="128px" AutoPostBack="true" CssClass="input">
											<asp:ListItem Value="0">Seleccione</asp:ListItem>
											<asp:ListItem Value="1">TELAS</asp:ListItem>
											<asp:ListItem Value="2">MEDIAS PIERNAS</asp:ListItem>
											<asp:ListItem Value="3">PIERNAS LARGAS</asp:ListItem>
											<asp:ListItem Value="4">PRENDAS</asp:ListItem>
										    <%--<asp:ListItem Value="5">HANG TAGS</asp:ListItem>--%>
										</asp:dropdownlist></td>
									<td class="etiqueta" style="WIDTH: 109px">&nbsp;Número Solicitud:</td>
									<td><asp:textbox id="txtNumero" runat="server" Width="128px" AutoPostBack="True" CssClass="input" MaxLength="10"></asp:textbox></td>
								</tr>
                                </table>
                                </td>
                                

				</tr>
			</table>
			<table id="Table23" style="WIDTH: 720px; HEIGHT: 8px">
				<tr>
					<td class="Etiqueta" style="WIDTH: 103px">&nbsp;Fecha Solicitud:</td>
					<td style="WIDTH: 98px" width="98"><asp:textbox id="txtFecha" runat="server" Width="100px" CssClass="input"></asp:textbox></td>
					<td style="WIDTH: 50px">&nbsp; <img id="imgCalFeSol" onclick="popUpCalendar(this, frmSolicitudMuestras.txtFecha, 'dd/mm/yyyy')"
							height="15" alt="Seleccionar fecha" src="../../intranet/Imagenes/Calendario.gif" width="13" border="0">&nbsp;
					</td>
					<td class="etiqueta" style="WIDTH: 108px">&nbsp;Situación :</td>
					<td>&nbsp;<asp:textbox id="txtSituacion" runat="server" Width="136px" CssClass="input" MaxLength="10"></asp:textbox></td>
				</tr>
			</table>
			<table style="WIDTH: 720px; HEIGHT: 48px">
				<tr>
					<td class="Etiqueta" style="WIDTH: 103px">&nbsp;Cliente:</td>
					<td style="WIDTH: 94px" width="94"><asp:textbox id="TxtCodigoCliente" runat="server" Width="100px" CssClass="input"></asp:textbox></td>
					<td style="WIDTH: 49px; HEIGHT: 8px" width="49"><input class="boton" id="btnClienteSolicitante" style="WIDTH: 20px; HEIGHT: 20px" onclick="ListarClientes();"
							type="button" value="..." name="btnClienteSolicitante"></td>
					<td style="HEIGHT: 8px" colspan="2"><asp:textbox id="txtNombreCliente" runat="server" Width="360px" CssClass="inputDisabled2" MaxLength="10"></asp:textbox></td>
				</tr>
			</table>
			<table style="WIDTH: 720px; HEIGHT: 40px">
				<tr>
					<td class="Etiqueta" style="WIDTH: 103px; HEIGHT: 23px">&nbsp;Almacén Origen:</td>
					<td style="HEIGHT: 23px"><asp:dropdownlist id="cmbAlmacen" runat="server" Width="192px" CssClass="input">
							<asp:ListItem Value="013">013-ALMACEN DE MUESTRAS</asp:ListItem>
						    <asp:ListItem Value="001">001-ALMACEN GENERAL</asp:ListItem>
						</asp:dropdownlist></td>
                    <td class="Etiqueta" style="WIDTH: 120px; HEIGHT: 23px">&nbsp;Tipo de Muestra:</td>
                    <td style="HEIGHT: 23px">
                        <asp:dropdownlist id="cmbTipoPagoMuestra" runat="server" 
                            Width="170px" CssClass="input">
							<asp:ListItem Value="0">-----Seleccione-----</asp:ListItem>
                            <asp:ListItem Value="1">Muestras para Ventas (Factura)</asp:ListItem>
						    <asp:ListItem Value="2">Muestras para Clientes</asp:ListItem>
						</asp:dropdownlist></td>
				</tr>
				<tr>
					<td class="Etiqueta" style="WIDTH: 95px" vAlign="middle">&nbsp;Observaciones :</td>
					<td style="HEIGHT: 40px" colspan="4"><asp:textbox id="txtObservacion" runat="server" Width="520px" CssClass="input" MaxLength="100"
							Height="35px" TextMode="MultiLine"></asp:textbox></td>
				</tr>
				<tr>
					<td colspan="5"><asp:button id="btnGrabar" runat="server" CssClass="Boton" Text="Grabar"></asp:button>&nbsp;&nbsp;<asp:button id="btnSolicitar" runat="server" Width="120px" CssClass="Boton" Text="Solicitar Aprobacion"></asp:button>&nbsp;&nbsp;
						<asp:button id="btnAnular" runat="server" CssClass="Boton" Text="Anular" CausesValidation="False"></asp:button>&nbsp;
						<span id="spanSep" style="WIDTH:206px">&nbsp;</span>
						<asp:button id="BtnNuevo" runat="server" CssClass="Boton" Text=" Nueva Solicitud"
							Width="250"></asp:button><input class="Input" id="hdnAprobacion" style="WIDTH: 32px; HEIGHT: 13px" type="hidden"
							size="1" value="070" name="hdnAprobacion" runat="server"><input class="Input" id="hdnAreaSolicitante" style="WIDTH: 32px; HEIGHT: 14px" type="hidden"
							size="1" name="Hidden1" runat="server"></td>
				</tr>
			</table>
			<table>
				<tr>
					<td><asp:datagrid id="DataGrid1" runat="server" Width="928px" Height="120px" AutoGenerateColumns="False"
							ShowFooter="True">
							<FooterStyle CssClass="GridFooter"></FooterStyle>
							<AlternatingItemStyle CssClass="GridAlternateItem"></AlternatingItemStyle>
							<ItemStyle CssClass="GridItem"></ItemStyle>
							<HeaderStyle CssClass="GridHeader"></HeaderStyle>
							<Columns>
								<asp:TemplateColumn HeaderText="Articulo">
									<HeaderStyle HorizontalAlign="Left" Width="450px"></HeaderStyle>
									<HeaderTemplate>
										<table cellspacing="1" cellpadding="1" width="100%" border="0">
											<tr>
												<td class="GridHeader" width="10">
													<asp:Panel id="Panel27" runat="server" Width="50px">Secuencia </asp:Panel></td>
												<td class="GridHeader" width="100">
													<asp:Panel id="Panel34" runat="server" Width="120px">Código</asp:Panel></td>
												<td class="GridHeader" width="100%">
													<asp:Panel id="Panel38" runat="server" Width="120px">Descripción</asp:Panel></td>
											</tr>
										</table>
									</HeaderTemplate>
									<ItemTemplate>
										<table id="Table1" cellspacing="1" cellpadding="1" width="100%" border="0">
											<tr>
												<td width="20">
													<asp:Panel id="Panel33" runat="server" Width="10px">
														<asp:Label id="lblNumeroSecuencia" runat="server" Width="50px" CssClass="InputCentro" Height="5px" text='<%#Container.DataItem("Int_Secuencia")%>'>
														</asp:Label>
													</asp:Panel></td>
												<td width="100">
													<asp:Panel id="Panel1" runat="server" Width="50px">
														<asp:Label id="lblCodigoArticulo" runat="server" Width="81px" CssClass="InputCentro" Height="12px" text='<%#Container.DataItem("var_CodigoArticulo")%>'>
														</asp:Label>
													</asp:Panel></td>
												<td width="100%">
													<asp:Panel id="Panel36" runat="server" Width="100%">
														<asp:Label id="lblArticulo" runat="server" Width="100%" CssClass="input" text='<%#Container.DataItem("DE_ITEM")%>'>
														</asp:Label>
													</asp:Panel></td>
											</tr>
										</table>
									</ItemTemplate>
									<FooterTemplate>
										<table id="Table3" style="HEIGHT: 20px" cellspacing="1" cellpadding="1" width="100%" border="0">
											<tr>
												<td width="100">
													<asp:Panel id="Panel37" runat="server" Width="50px"></asp:Panel></td>
												<td width="120">
													<asp:TextBox id="txtCodArticuloF" runat="server" Width="201px" CssClass="input" 
														Height="22px"></asp:TextBox></td>
												<td class="style1">
													<input class="Boton" id="btnArticuloF" style="WIDTH: 20px; HEIGHT: 20px" type="button"
														value="..." name="btnArticuloF" runat="server" />
                                                </td>
											    <td width="100%">
                                                    <asp:Label ID="lblArticuloF" runat="server" Width="100%"></asp:Label>
                                                </td>
											</tr>
										</table>
									</FooterTemplate>
									<EditItemTemplate>
										<table id="Table2" cellspacing="1" cellpadding="1" width="100%" border="1">
											<tr>
												<td width="20">
													<asp:Label id="lblNumeroSecuenciaE" runat="server" Width="57px" CssClass="InputCentro" Height="12px" text='<%#Container.DataItem("Int_Secuencia")%>'>
													</asp:Label></td>
												<td width="80">
													<asp:TextBox id="txtCodArticuloE" runat="server" Width="177px" CssClass="input" Height="22px" text='<%#Container.DataItem("var_CodigoArticulo")%>'>
													</asp:TextBox></td>
												<td width="20"><input class="boton" id="btnArticuloE" style="WIDTH: 19px; HEIGHT: 19px" type="button"
														size="20" value="..." name="btnArticuloE" runat="server" /></td>
												<td width="300">
													<asp:Label id="lblArticuloE" runat="server" Width="100%" CssClass="input" text='<%#Container.DataItem("DE_ITEM")%>'>
													</asp:Label></td>
											</tr>
										</table>
									</EditItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="U.M.">
									<HeaderStyle HorizontalAlign="Center" Width="20px"></HeaderStyle>
									<ItemTemplate>
										<asp:Label id="lblUnidadM" runat="server" CssClass="InputCentro" Width="20px" text='<%#Container.DataItem("co_unme")%>'>
										</asp:Label>
									</ItemTemplate>
									<FooterTemplate>
										<asp:Label id="lblUnidadMF" runat="server" CssClass="InputCentro" Width="20px"></asp:Label>
									</FooterTemplate>
									<EditItemTemplate>
										<asp:Label id="lblUnidadME" runat="server" CssClass="InputCentro" Width="20px" text='<%#Container.DataItem("co_unme")%>'>
										</asp:Label>
									</EditItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="STOCK">
									<HeaderStyle HorizontalAlign="Center" Width="20px"></HeaderStyle>
									<ItemTemplate>
										<asp:Label id="lblStock" runat="server" Width="20px" CssClass="InputDerecha" text='<%#Format(Container.DataItem("ca_actu"),"0.00")%>'>
										</asp:Label>
									</ItemTemplate>
									<FooterTemplate>
										<asp:Label id="lblStockF" runat="server" Width="20px" CssClass="InputDerecha"></asp:Label>
									</FooterTemplate>
									<EditItemTemplate>
										<asp:Label id="lblStockE" runat="server" Width="20px" CssClass="InputDerecha" text='<%#Container.DataItem("ca_actu")%>'>
										</asp:Label>
									</EditItemTemplate>
								    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" 
                                        Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Right" />
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="Cantidad">
									<HeaderStyle HorizontalAlign="Center" Width="70px"></HeaderStyle>
									<ItemTemplate>
										<asp:Label id="LblCantidad" runat="server" CssClass="InputDerecha" Width="100%" text='<%#Format(Container.DataItem("num_CantidadSolicitud"),"0.00")%>'>
										</asp:Label>
									</ItemTemplate>
									<FooterTemplate>
										<asp:TextBox id="txtCantidadF" runat="server" CssClass="InputDerecha" Width="100%"></asp:TextBox>
									</FooterTemplate>
									<EditItemTemplate>
										<asp:TextBox id="txtCantidadE" runat="server" CssClass="InputDerecha" Width="100%" text='<%#Container.DataItem("num_CantidadSolicitud")%>'>
										</asp:TextBox>
									</EditItemTemplate>
								</asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="Precio" >
									<HeaderStyle HorizontalAlign="Center" Width="50px"></HeaderStyle>
									<ItemTemplate>
										<asp:Label id="lblPrecio" runat="server" CssClass="InputDerecha" Width="100%" text='<%#Format(Container.DataItem("num_Precio"),"0.00")%>'>
										</asp:Label>
									</ItemTemplate>
									<FooterTemplate>
										<asp:TextBox id="txtPrecioF" runat="server" CssClass="InputDerecha" Width="100%"></asp:TextBox>
									</FooterTemplate>
									<EditItemTemplate>
										<asp:TextBox id="txtPrecioE" runat="server" CssClass="InputDerecha" Width="100%" text='<%#Container.DataItem("num_Precio")%>'>
										</asp:TextBox>
									</EditItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn>
									<HeaderStyle Width="40px"></HeaderStyle>
									<ItemTemplate>
										<asp:ImageButton id="btnEdit" runat="server" ImageUrl="../../intranet/Imagenes/Editar.gif" CommandName="Edit"
											Visible="False"></asp:ImageButton>
										<asp:ImageButton id="btnDelete" runat="server" ImageUrl="../../intranet/Imagenes/borrador.gif" CommandName="Delete"
											title="Clic para Eliminar Articulo del Detalle"></asp:ImageButton>
									</ItemTemplate>
									<FooterTemplate>
										<asp:ImageButton id="btnAdd" runat="server" ImageUrl="../../intranet/Imagenes/Grabar.gif" CommandName="Add"></asp:ImageButton>
									</FooterTemplate>
									<EditItemTemplate>
										<asp:ImageButton id="btnUpdate" runat="server" ImageUrl="../../intranet/Imagenes/Grabar.gif" CommandName="Update"></asp:ImageButton>
										<asp:ImageButton id="btnCancel" runat="server" ImageUrl="../../intranet/Imagenes/Cancelar.gif" CommandName="Cancel"></asp:ImageButton>
									</EditItemTemplate>
								</asp:TemplateColumn>
							</Columns>
						</asp:datagrid></td>
				</tr>
			</table>
			<input id="hdnIdentificador" style="WIDTH: 24px; HEIGHT: 22px" type="hidden" size="1" name="hdnIdentificador"
				runat="server"/> <input id="HDN2" style="WIDTH: 32px; HEIGHT: 22px" type="hidden" size="1" name="HDN2" runat="server"/>
			<input id="HDN1" style="WIDTH: 32px; HEIGHT: 22px" type="hidden" size="1" name="HDN1" runat="server"/>
			<input id="HDNArticulo" style="WIDTH: 32px; HEIGHT: 22px" type="hidden" size="1" name="HDNArticulo"
				runat="server"/><input id="HDNUnidadMedida" style="WIDTH: 32px; HEIGHT: 22px" type="hidden" size="1" name="HDNUnidadMedida"
				runat="server"/><input id="HDNCtaGasto" style="WIDTH: 32px; HEIGHT: 22px" type="hidden" size="1" name="HDNCtaGasto"
				runat="server"/> <input id="HdnDescServicio" style="WIDTH: 32px; HEIGHT: 22px" type="hidden" size="1" name="HdnDescServicio"
				runat="server"/>
			<asp:label id="lblMsg" style="Z-INDEX: 102; POSITION: absolute; TOP: 60px; LEFT: 650px" runat="server" Width="292px" Height="50px" Font-Bold="True"></asp:label>
        </form>
		<script language="JavaScript" type="text/javascript">

	        function fnc_ValidarSolicitudMuestras()
	        {
		        if (document.all('ddlTipo').value=='0')
		        {
			        //alert("Seleccione el tipo de muestra por favor...!");
			        alert("Seleccione el Tipo de Solicitud por favor...!");
			        document.all('ddlTipo').focus();
			        return false;
		        }
		
		        if (document.all('txtNombreCliente').value=='')
		        {
			        alert("Seleccione el cliente por favor...!");
			        return false;
		        }
		
		        return true;
		
	        }
			
	        function ListarClientes() /*Muestra el listado de los Clientes de la empresa*/
	        {
		        var retorno = window.showModalDialog("../Buscadores/frm_QryClientes.aspx","","dialogHeight:600px;dialogWidth:550px;center:yes;help:no;");
		        if (retorno!="" && retorno!=":")
		        {
			        var datos = retorno.split(":");
			        var Codigo = datos[0];
			        var Nombre = datos[1];
			        document.all('TxtCodigoCliente').value = Codigo;
			        document.all('txtNombreCliente').value = Nombre;
			        //document.frmSolicitudMuestras.HDN1.value=Nombre;
			        //break;
		        }
	        }
			
	        function BuscarArticulo(ControlID,Constante)
	        {
		        var lstrAlmacen=document.all('cmbAlmacen').value;
		        var lstrTipo=document.all('ddlTipo').value;
		        if (lstrTipo == "5") lstrTipo = "6";
		        if (lstrTipo == "1") lstrTipo = "5";
		     
		
		        var retorno = window.showModalDialog("../Buscadores/frmBusquedaArticulosStock.aspx?pstrAlmacen="+lstrAlmacen +"&pstrTipo="+ lstrTipo,"","dialogHeight:450px;dialogWidth:830px;center:yes;help:no;");
	
		        //retorno debe traer el resultado de la busqueda
		        if (retorno!="::" && retorno!="")
		        {
			        var datos = retorno.split(":");
			        var Codigo=datos[0];
			        var Articulo=datos[1];
			        var Unidad=datos[2];
			        var stock=datos[3];
			
			        if (parseFloat("0" + stock)<=0)
			        {
				        alert('No tiene stock disponible el articulo elegido.');
				        return false;
			        }

			        if (parseFloat("0" + document.all["lblReg"].innerHTML)==6) //  
			        {
				        alert('Solo puede ingresar 6 Articulos por Solicitud de Muestra.');
				        return false;
			        }			
		
			        if(Codigo=='')
			        {
				        return false;
			        }//end if
		
			
			        switch (Constante)
			        {
			            case "F":
			                var txtArticuloCod = document.all[ControlID + "_txtCodArticuloF"];
			                var lblArticulo = document.all[ControlID + "_lblArticuloF"];
			                var lblUnidadM = document.all[ControlID + "_lblUnidadMF"];
			                var lblStock = document.all[ControlID + "_lblStockF"];
			                //DG CAMBIO - INI
			                if (document.all('cmbTipoPagoMuestra').value == '1') {
			                    var txtPrecioF = document.all[ControlID + "_txtPrecioF"];
			                }
			                //var txtPrecioF = document.all[ControlID + "_txtPrecioF"];
			                //DG CAMBIO - FIN
			                txtArticuloCod.value = datos[0];
			                lblArticulo.innerHTML = datos[1];
			                frmSolicitudMuestras.HDNArticulo.value = datos[1];
			                lblUnidadM.innerHTML = datos[2];

			                //lblStock.innerHTML =datos[3];
			                lblStock.innerHTML = parseFloat("0" + datos[3]).toFixed(2);

			                document.all[ControlID + "_txtCantidadF"].value = lblStock.innerHTML;

			                //DG CAMBIO - INI
			                if (document.all('cmbTipoPagoMuestra').value == '1') {
			                    document.all[ControlID + "_txtPrecioF"].value = 0;
			                }
			                //document.all[ControlID + "_txtPrecioF"].value = 0;
			                //DG CAMBIO - FIN
			                break;
			            case "E":
			                var txtArticuloCod = document.all[ControlID + "_txtCodArticuloE"];
			                var lblArticulo = document.all[ControlID + "_lblArticuloE"];
			                var lblUnidadM = document.all[ControlID + "_lblUnidadME"];
			                var lblStock = document.all[ControlID + "_lblStockE"];
			                //DG CAMBIO - INI
			                if (document.all('cmbTipoPagoMuestra').value == '1') {
			                    var txtPrecioE = document.all[ControlID + "_txtPrecioE"];
			                }
			                //var txtPrecioE = document.all[ControlID + "_txtPrecioE"];
			                //DG CAMBIO - FIN
			                txtArticuloCod.value = Codigo = datos[0];
			                lblArticulo.innerHTML = datos[1];
			                lblUnidadM.innerHTML = datos[2];

			                //lblStock.innerHTML =datos[3];
			                lblStock.innerHTML = parseFloat("0" + datos[3]).toFixed(2);
			                if (document.all('cmbTipoPagoMuestra').value == '1') {
			                    if (document.all('txtPrecioE').value == 0) {
			                        //txtPrecioE.innerHTML = 0;
			                        document.all[ControlID + "txtPrecioE"].value = 0;
			                    }
			                }
			                break;
			        }
			
			        frmSolicitudMuestras.HDNUnidadMedida.value=datos[2];
		        } // end if
	        }//end function
			
	        function txtCantidad_onBlur(ControlID,Constante)
	        {
		        var ltxtCantidad=document.all(ControlID + '_txtCantidad'+Constante);
		
		        //alert(ControlID);
		        //alert(Constante);
		
		        if (ltxtCantidad.value=='') return false;
		
		
//		        if(ltxtCantidad.value=='' || isVarType(ltxtCantidad.value,'NUM')==false)
//		        {
//			        alert('La cantidad ingresada no es valida.');
//			        ltxtCantidad.focus();
//			        return false;
//			    } //end if	

		        if (ltxtCantidad.value == '' ) {
		            alert('La cantidad ingresada no es valida.');
		            ltxtCantidad.focus();
		            return false;
		        } 
			
		

		        if (parseFloat(ltxtCantidad.value) <= 0)
		        {
		           alert("La cantidad solicitada no pude ser Cero.")
		           ltxtCantidad.value = "";
		           return false;
		        }


//                if (isVarType(document.all[ControlID + "_lblStock"+Constante].innerHTML,'NUM')==true)
//                {
//			        if (parseFloat(ltxtCantidad.value) > parseFloat("0"+document.all[ControlID + "_lblStock"+Constante].innerHTML))
//			        {
//				        alert("La cantidad solicitada supera lo Disponible en Stock.")
//				        return false;
//			        }
		       //		       		        }

		   
		           if (parseFloat(ltxtCantidad.value) > parseFloat("0" + document.all[ControlID + "_lblStock" + Constante].innerHTML)) {
		               alert("La cantidad solicitada supera lo Disponible en Stock.")
		               return false;
		           }
		    
		      
		
		        ltxtCantidad.value=parseFloat(ltxtCantidad.value).toFixed(2);
		
		        return true;
		
	        }//end function


	        function fvalidarnumero(objPor2da) {
	            //            if (objPor2da.value != "") {
	            //                if (!/^([0-9])*$/.test(objPor2da.value)) {
	            //                    alert("El valor Porcentaje de 2das OB/P.ch Esta." + objPor2da.value + " no es un número");
	            //                    objPor2da.value = 0; return
	            //                }
	            //            }

	            var RE = /^\d*\.?\d*$/;
	            if (RE.test(objPor2da.value)) {
	                return true;
	            } else {
	                alert("El valor " + objPor2da.value + " no es un decimal.");
	                objPor2da.value = 0;
	                return false;
	            }
	        }
		
	        function fnc_ConfirmarAnulacion()
	        {
		        if (document.all('txtNumero').value=='')
		        {
			        alert("No existe un numero de Solictud...!");
			        return false;
		        }
		
		        Resp = confirm("Esta ud. seguro de anular esta solicitud de muestra?");
		        return Resp;
	        }

	        if (document.getElementById("txtSituacion").value != "")
	        {
		        document.getElementById("ddlTipo").disabled=true;
            }	

		</script>
	</body>
</html>
