<%@ Page Language="vb" AutoEventWireup="false" Codebehind="frm_RegistrarDespachoMuestras.aspx.vb" Inherits="intranet_logi.frm_RegistrarDespachoMuestras" uiCulture="es-PE" culture="es-PE" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html>
	<head>
		<title>Despacho de Muestras y Etiquetas - Pendientes</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR"/>
		<meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE"/>
		<meta content="JavaScript" name="vs_defaultClientScript"/>
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema"/>
        <link href="../css/NM0001.css" rel="stylesheet" type="text/css" />
		<script src="../js/functions.js" type="text/javascript"></script>
        <script src="../js/jquery.js" type="text/javascript"></script>
        <script src="../js/jquery.min.js" type="text/javascript"></script>
        <script src="../js/jquery.validate.js" type="text/javascript"></script>
	
		<script language="javascript" type="text/javascript">
	
		    function fnInputNumberInteger()
		    {
			    if ((event.keyCode >= 48 && event.keyCode <= 57))           
				    return true;
			    else
				    return false;
		    }    

		    function ValidarImporte(id, saldo)
		    {
			    var importe = GetFloatValue(id);
			    saldo = parseFloat(saldo);
			    var formato = "0,0.00";
			    if(importe > saldo)     
			    {
				    SetValue(id, saldo);  
				    alert('Se ha superado el monto máximo: '+GetValue('hdnMonedaSimbolo')+' '+saldo.numberFormat(formato) );
			    }   
			    CalcularImporteTotal();        
		    }

		    function txtLote_Onblur(prCodItem, prCantSol, prIDObj)

		    {
			    var vrIdFila="";
			    var vrLoteValid = "";
			    var vrCodLote = "";
			
			    vrCodLote = document.getElementById(prIDObj).value;
			    vrIdFila = prIDObj.replace("txtLote","");
			    vrLoteValid = prIDObj.replace("txtLote", "txtLoteValid"); //+vrIdFila //+ "txtLoteValid";
			
			    if (document.getElementById(prIDObj).value=="") 
			    {
				    document.getElementById(vrLoteValid).value ="";
				    return;
			    }			
			
			    // ini : ajax valid
			    $.ajax({
					    type: "GET",
					    url: "frm_AjaxValidRegistrarDespachoMuestras.aspx",
					    data: "codart="+ prCodItem + "&codlote="+ vrCodLote + "&cantsol="+ prCantSol,
					    async:false,
					    beforeSend: function(objeto){
						    document.getElementById(vrLoteValid).value="Validando...";
					    },					
					    error: function(objeto, quepaso, otroobj){
						    document.getElementById(vrLoteValid).value="Errores al Procesar.";
					    },
					    global: true,
					    ifModified: false,
					    processData:true,
					    success: function(datos){
						    document.getElementById(vrLoteValid).value=datos;
					    },
					    timeout: 300099999
			    });			
			    // fin : ajax valid			
//			
//			
		    }

		    function funValidarGrabar()
		    {
		       var vrBool = true
		       var vrTipoSol =""
		   		
		       vrTipoSol = document.getElementById("ddlTipoSol").value;

               if (FunValidErrResProc()== true)
               {
                  if (vrTipoSol=="1")
                     alert('Ingrese correctamente la información para poder Grabar.');
                  else
                     alert('No se puede grabar por falta de Stock.');
              
                  vrBool = false;
              }

//              var vrDocumento = ""
//              vrDocumento = document.getElementById("ddlDocumento").value;
//              if (vrDocumento == "") {
//                  alert('Debe Seleccionar un tipo de documento que se va a generar.');
//                  vrBool = false;
//              }
		   
			    return vrBool;

			    //return false; // no ejecuta Grabar
			    //return true; // Si ejecuta Grabar
		    }
		
		
		    function FunValidErrResProc()
		    {
			    var frm = document.forms[0];
			    var nroErrados=0;
			
			    for(i=0;i< frm.length;i++)
			    {
				    e = frm.elements[i];
				
				    if ( e.type=='text' && e.id.indexOf("txtLoteValid") != -1 )
				    {
					    var vrResProc=document.getElementById(e.id).value;

					    if (vrResProc != 'Todo OK.')
   					       nroErrados = nroErrados +1;
   				    }
			    }
			
			    if (nroErrados>0)
			       return true;
			    else
			       return false;

			}
		


		</script>
	</head>
	<body>
		<form id="Form1" method="post" runat="server">
			<table id="Table40" style="Z-INDEX: 101; LEFT: 0px; WIDTH: 949px; POSITION: absolute; TOP: 0px; HEIGHT: 416px"
				cellspacing="2" cellpadding="2" width="949" border="0">
					<tr>
						<td class="cabecera" style="HEIGHT: 22px">&nbsp;FORMULARIO DE REGISTRO SALIDA ALMACEN DE MUESTRA</td>
					</tr>
					<tr>
						<td class="panel">
							<table style="WIDTH: 720px; HEIGHT: 16px">
								<tr>
									<td class="Etiqueta" style="WIDTH: 103px" valign="middle">&nbsp;Tipo Solicitud:</td>
									<td style="WIDTH: 155px">
										<asp:dropdownlist id="ddlTipoSol" runat="server" CssClass="input" Width="128px">
											<asp:ListItem Value="0" Selected="True">Seleccione</asp:ListItem>
											<asp:ListItem Value="1">TELAS</asp:ListItem>
											<asp:ListItem Value="2">MEDIAS PIERNAS</asp:ListItem>
											<asp:ListItem Value="3">PIERNAS LARGAS</asp:ListItem>
											<asp:ListItem Value="4">PRENDAS</asp:ListItem>
										    <asp:ListItem Value="5">HANG TAGS</asp:ListItem>
										</asp:dropdownlist>									
									</td>
									<td class="etiqueta" style="WIDTH: 109px">&nbsp;Número Solicitud:</td>
									<td><asp:label id="lblNroSol" runat="server"></asp:label></td>
								</tr>
								<tr>
									<td class="Etiqueta" style="WIDTH: 103px">&nbsp;Fecha Solicitud:</td>
									<td style="WIDTH: 155px"><asp:label id="lblFecSol" runat="server"></asp:label></td>
									<td class="etiqueta" style="WIDTH: 109px">&nbsp;Situación :</td>
									<td>&nbsp;<asp:label id="lblSituSol" runat="server"></asp:label></td>
								</tr>
								<tr>
									<td class="Etiqueta" style="WIDTH: 103px">&nbsp;Cliente:</td>
									<td style="WIDTH: 617px" colspan="3"><asp:label id="lblCodCli" runat="server"></asp:label>&nbsp;&nbsp;&nbsp;<asp:label id="lblCliente" runat="server"></asp:label></td>
                                    
									
								
								</tr>
								<tr>
									<td class="Etiqueta" style="WIDTH: 103px; HEIGHT: 23px">&nbsp;Almacén Origen:</td>
									<td style="HEIGHT: 23px"><asp:dropdownlist id="cmbAlmacen" runat="server" CssClass="input" Width="192px">
											<asp:ListItem Value="013">013-ALMACEN DE MUESTRAS</asp:ListItem>
										    <asp:ListItem Value="001">001-ALMACEN GENERAL</asp:ListItem>
										</asp:dropdownlist></td>
                                        <%--<td class="Etiqueta" style="WIDTH: 103px; HEIGHT: 23px">&nbsp;Tipo Documento:</td>
                                    <td style="HEIGHT: 23px"><asp:dropdownlist id="ddlDocumento" runat="server" CssClass="input" Width="220px">
											<asp:ListItem Value="" Selected="true">------SELECCIONE------</asp:ListItem>
                                            <asp:ListItem Value="GRR">GRR - GUIA REMISIÓN REMITENTE</asp:ListItem>
										    <asp:ListItem Value="GUR">GUR - GUIA DE REMISIÓN</asp:ListItem>
										</asp:dropdownlist></td>--%>
                                        <td class="Etiqueta" style="WIDTH: 120px; HEIGHT: 23px">&nbsp;Tipo de Pago Muestra:</td>
                                    <td style="HEIGHT: 23px"><asp:dropdownlist id="cmbTipoPagoMuestra" runat="server" Width="150px" CssClass="input" >							            
                                            <asp:ListItem Value="0">-----Seleccione-----</asp:ListItem>
                                            <asp:ListItem Value="1">Muestras para Ventas</asp:ListItem>
						                    <asp:ListItem Value="2">Muestras para Clientes</asp:ListItem>
						                </asp:dropdownlist></td>
								</tr>
								<tr>
									<td class="Etiqueta" style="WIDTH: 95px" valign="middle">&nbsp;Observaciones :</td>
									<td style="HEIGHT: 40px" colspan="4"><asp:textbox id="txtObservacion" runat="server" CssClass="input" Width="520px" MaxLength="100"
											TextMode="MultiLine" Height="35px"></asp:textbox></td>
								</tr>
							</table>
							<table id="Table1" cellspacing="1" cellpadding="1" width="98%" border="0">
								<tr>
									<td>
										<DIV style="WIDTH: 100%; HEIGHT: 300px"><asp:datagrid id="dtgLista" runat="server" Width="100%" AutoGenerateColumns="False">
												<AlternatingItemStyle CssClass="GridAltItem"></AlternatingItemStyle>
												<ItemStyle CssClass="GridItem"></ItemStyle>
												<HeaderStyle CssClass="gridheader"></HeaderStyle>
												<Columns>
													<asp:BoundColumn DataField="codart" HeaderText="codart" Visible="true"></asp:BoundColumn>
													<asp:BoundColumn DataField="articulo" HeaderText="Artículo"></asp:BoundColumn>
													<asp:BoundColumn DataField="cant_sol" HeaderText="Metros" DataFormatString="{0:#,##0.00}">
														<HeaderStyle HorizontalAlign="Right"></HeaderStyle>
														<ItemStyle HorizontalAlign="Right"></ItemStyle>
													</asp:BoundColumn>
													<asp:BoundColumn DataField="cant_dispon" HeaderText="Stock Disponible" DataFormatString="{0:#,##0.00}">
														<HeaderStyle HorizontalAlign="Right"></HeaderStyle>
														<ItemStyle HorizontalAlign="Right"></ItemStyle>
													</asp:BoundColumn>
													<asp:TemplateColumn HeaderText="Lote">
														<HeaderStyle HorizontalAlign="Center"></HeaderStyle>
														<ItemStyle HorizontalAlign="Center" Width="150px"></ItemStyle>
														<ItemTemplate>
															<input id="txtLote" name="txtLote" runat="server" type="text" style="width:100px;" onkeypress="javascript:return fnInputNumberInteger();"
																maxlength="20" />
														</ItemTemplate>
														<ItemStyle HorizontalAlign="Center"></ItemStyle>
													</asp:TemplateColumn>
                                                    <asp:BoundColumn DataField="num_precio" HeaderText="Precio" DataFormatString="{0:#,##0.00}">
														<HeaderStyle HorizontalAlign="Right"></HeaderStyle>
														<ItemStyle HorizontalAlign="Right"></ItemStyle>
													</asp:BoundColumn>
													<asp:TemplateColumn>
														<HeaderStyle HorizontalAlign="Center"></HeaderStyle>
														<ItemStyle HorizontalAlign="Center" Width="150px"></ItemStyle>
														<ItemTemplate>
															<input id="txtLoteValid" name="txtLoteValid" runat="server" type="text" style="WIDTH: 210px; BORDER-TOP-STYLE: none; BORDER-RIGHT-STYLE: none; BORDER-LEFT-STYLE: none; BACKGROUND-COLOR: transparent; BORDER-BOTTOM-STYLE: none" />
														</ItemTemplate>
														<ItemStyle HorizontalAlign="Center"></ItemStyle>
													</asp:TemplateColumn>
												</Columns>
											</asp:datagrid></DIV>
									</td>
								</tr>
								<tr>
									<td align="center">
										<table class="botonera" id="Table4" cellspacing="0" cellpadding="0" width="100%" align="center"
											border="0">
											<tr>
												<td align="center">
													<table>
														<tr>
															<td><asp:button id="btnGrabar" runat="server" Width="150" Text="Grabar"></asp:button></td>
															<td>&nbsp;</td>
															<td><asp:button id="btnRegresar" runat="server" Width="150" Text="Regresar"></asp:button></td>
														</tr>
													</table>
												</td>
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
