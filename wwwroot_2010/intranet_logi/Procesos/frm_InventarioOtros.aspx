<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="frm_InventarioOtros.aspx.vb" Inherits="intranet_logi.frm_InventarioOtros" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">

<html>
 <head>
 <title>Registro de Inventario</title>
        
		<meta content="Microsoft Visual Studio.NET 7.0" name="GENERATOR" />
		<meta content="Visual Basic 7.0" name="CODE_LANGUAGE" />
		<meta content="JavaScript" name="vs_defaultClientScript" />
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<link href="../css/NM0001.css" type="text/css" rel="stylesheet">
		<script type="text/javascript" language="javascript" src="../../intranet/JS/jsCalendario_N4.js"></script>
		<script type="text/javascript" language="javascript" src="../../intranet/JS/functions.js"></script>
		<script type="text/javascript" language="JavaScript">
     

    
      //popUp
      function popUp(strUrl) {
          var intWidth = screen.width;
          var intHeight = screen.height;
          window.open(strUrl, "", "top=0; left=0; width=" + intWidth + "; height=" + intHeight + "; resizable=1;");
      }

     //fnc_ConfirmarAnulacion
      function fnc_ConfirmarAnulacion() {
          var lstrMensaje = 'Se anulará la requisición -- ' + document.all('txtSerie').value + '-' + document.all('txtNumero').value + ' --.\n\n¿Esta seguro de continuar?';
          if (document.all('txtSerie').value != '' && document.all('txtNumero').value != '') {
              return confirm(lstrMensaje);
          }
          return false;
      }

      

 </script>

</head>

<body>
 <center>
  <form id="frmRequisicion" name="frmRequisicion" method="post" runat="Server">
   <input id="hdnCodigo" style="WIDTH: 24px; HEIGHT: 22px" type="hidden" size="1" name="hdnCodigo" runat="server" />
   <input id="hdnAccion" style="WIDTH: 24px; HEIGHT: 22px" type="hidden" size="1" name="hdnAccion" />
   <input id="hdnCCDestino" style="WIDTH: 24px; HEIGHT: 22px" type="hidden" size="1" name="hdnCCDestino" runat="server"/>
   <input id="hdnOrdServicio" style="WIDTH: 24px; HEIGHT: 22px" type="hidden" size="1" name="hdnOrdServicio" runat="server"/>

   <%--cabecera--%>
         <table id="Table1" cellspacing="0" cellpadding="0" border="0" class = "Cabecera" style = "width: 900px; height: 20px">
            <tr>
	            <td class="cabecera" style="HEIGHT: 27px" align="center">REGISTRO DE INVENTARIOS - 
                    REPUESTOS Y OTROS</td>
            </tr>
         </table>
            
            <%-- parte 1--%>
            <table cellspacing="2" cellpadding="0" width="900px" border="0">
                <tr>
                    <td class="Etiqueta" style="WIDTH: 150px; height:15px" valign="middle" align = "left">
                        Código Ubicacion:</td>
                    <td class="Etiqueta" style="WIDTH: 150px; height:15px" valign="middle" align = "left">
                        <asp:textbox id="txtCodigoUbicacion" runat="server" Width="100px" AutoPostBack="True" 
                            CssClass="input" MaxLength="10"></asp:textbox>
                    </td>
                    <td class="Etiqueta" style="WIDTH: 500px; height:15px" valign="middle" align = "left">
                        &nbsp;</td>
                </tr>
                <tr>
                    <td class="Etiqueta" style="WIDTH: 150px; height:20px" valign="middle" align = "left">
                        Inventariador:</td>
                    <td class="Etiqueta" style="WIDTH: 150px; height:20px" valign="middle" align = "left">
                        <asp:textbox id="txtInventariador" runat="server" Width="99px" CssClass="input" 
                            Height="17px"></asp:textbox>
                        &nbsp;
                    </td>
                    <td class="Etiqueta" style="WIDTH: 500px; height:20px" valign="middle" align = "left">
                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</td>
                </tr>
                <tr>
                    <td class="Etiqueta" style="WIDTH: 150px; height:20px" valign="middle" align = "left">
                        Fecha Inventario:</td>
                    <td class="Etiqueta" style="WIDTH: 150px; height:20px" valign="middle" align = "left">
                        <asp:textbox id="txtFecha" runat="server" Width="99px" CssClass="input" 
                            Height="20px"></asp:textbox>
                        &nbsp;
                    </td>
                    <td class="Etiqueta" style="WIDTH: 500px; height:20px" valign="middle" align = "left">
                        &nbsp;</td>
                </tr>
            </table>

            <%-- parte 2--%>
            <table cellspacing="2" cellpadding="0" width="900px" border="0">
                <tr>
                    <td class="Etiqueta" style="WIDTH: 150px" valign="middle" align = "left">Almacén :</td>
                    <td class="Etiqueta" style="WIDTH: 650px" valign="middle" align = "left">
                    <asp:textbox id="txtAlmacen" runat="server" Width="456px" CssClass="inputDisabled2" 
                            MaxLength="10" Height="20px"></asp:textbox>
                    </td>
                </tr>
                <tr>
                    <td class="Etiqueta" style="WIDTH: 150px" valign="middle" align = "left"></td>
                    <td class="Etiqueta" style="WIDTH: 650px" valign="middle" align = "left">
                        <asp:Label ID="lblMensaje" runat="server" Text="" CssClass="error"></asp:Label><br />
                    </td>
                </tr>
            </table>
            
            <%-- parte 3--%>
            <table cellspacing="2" cellpadding="0" style="width:900px;height:10px;" border="0">
                <tr valign ="top">
                    <td style="WIDTH: 150px" valign="top" align = "left">
                        <input class="Input" id="hdnAprobacion" style="WIDTH: 10px" type="hidden" size="1" runat="server" name="hdnAprobacion"/>
                        <input class="Input" id="hdnAreaSolicitante" style="WIDTH: 10px" type="hidden" size="1" name="Hidden1" runat="server" />
                    </td>
                    <td style="WIDTH: 150px" valign="top" align = "right">&nbsp;</td>
                    <td style="WIDTH: 150px" valign="top" align = "right">&nbsp;</td>
                    <td style="WIDTH: 150px" valign="top" align = "right">&nbsp;</td>
                    <td style="WIDTH: 150px" valign="top" align = "right">&nbsp;</td>
                    <td style="WIDTH: 150px" valign="top" align = "right">&nbsp;</td>
                </tr>
            </table>

        <%-- parte: Grilla--%>
        <table style="WIDTH: 900px">
            <tr>
                <td class="Etiqueta" style="WIDTH: 900px" valign="middle" align = "left">
                    <asp:datagrid id="dgDetalle" runat="server" Width="900px" ShowFooter="True" 
                        AutoGenerateColumns="False" Font-Bold="False" Height="140px">
                        <AlternatingItemStyle CssClass="GridAlternateItem"></AlternatingItemStyle>
                        <ItemStyle CssClass="GridItem"></ItemStyle>
                        <HeaderStyle CssClass="GridHeader"></HeaderStyle>
                        <Columns>
                            <%--Secuencia--%>
                            <asp:TemplateColumn HeaderText="Secuencia">
                                <HeaderStyle Width="10px"></HeaderStyle>
                                <ItemTemplate><asp:Label id="LblSecuenciaS" runat="server" CssClass="input" Width="20px" text='<%#Container.DataItem("NU_SECU")%>' Font-Size="10px"></asp:Label></ItemTemplate>
                            </asp:TemplateColumn>
	    
                            <asp:TemplateColumn HeaderText="Codigo_Item">
                                <HeaderStyle Width="100px" Font-Bold="False" Font-Italic="False" 
                                    Font-Overline="False" Font-Strikeout="False" Font-Underline="False" 
                                    HorizontalAlign="Right"></HeaderStyle>
                                <ItemTemplate><asp:Label id="LblCodigoItem" runat="server" CssClass="input" Width="100px" text='<%#Container.DataItem("CO_ITEM")%>' Font-Size="10px"></asp:Label></ItemTemplate>
                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" 
                                    Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" />
                            </asp:TemplateColumn>
                            
                            <%--Descripcion--%>
                            <asp:TemplateColumn HeaderText="Descripcion">
                            <HeaderStyle Width="280px" Font-Bold="False" Font-Italic="False" 
                                    Font-Overline="False" Font-Strikeout="False" Font-Underline="False" 
                                    HorizontalAlign="Center"></HeaderStyle>
                             <ItemTemplate><asp:Label id="LblDescripcionItem" runat="server" CssClass="input" Width="280px" text='<%#Container.DataItem("DE_ITEM")%>' Font-Size="10px"></asp:Label></ItemTemplate>
                            <HeaderTemplate>
		                        <table id="Table15" style="WIDTH: 280px; HEIGHT: 24px" cellspacing="1" cellpadding="1" border="0">
                                <tr>
                                    <td class="GridHeader">Descripcion</td>
                                </tr>
                                </table>
		                    </HeaderTemplate>
                            </asp:TemplateColumn>
                                                       
                            <asp:TemplateColumn HeaderText="Uni_Medida">
                            <HeaderStyle Width="80px"></HeaderStyle>
                            <ItemTemplate><asp:Label id="LblUnidadMedida" runat="server" CssClass="input" Width="80px" text='<%#Container.DataItem("CO_UNME")%>' Font-Size="10px"></asp:Label></ItemTemplate>
		                    </asp:TemplateColumn>
                          
                            <asp:TemplateColumn HeaderText="Conteo">
                            <HeaderStyle Width="80px" Font-Bold="False" Font-Italic="False" 
                                    Font-Overline="False" Font-Strikeout="False" Font-Underline="False" 
                                    HorizontalAlign="Center"></HeaderStyle>
                             <ItemTemplate><asp:Label id="LblConteo" runat="server" CssClass="input" Width="80px" text='<%#Container.DataItem("CA_CONT")%>' Font-Size="10px"></asp:Label></ItemTemplate>
		                        <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" 
                                    Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Right" />
		                    </asp:TemplateColumn>

                            <asp:TemplateColumn HeaderText="Reconteo">
                            <HeaderStyle Width="80px" Font-Bold="False" Font-Italic="False" 
                                    Font-Overline="False" Font-Strikeout="False" Font-Underline="False" 
                                    HorizontalAlign="Center"></HeaderStyle>
                            <ItemTemplate><asp:Label id="LblReconteo" runat="server" CssClass="input" Width="80px" text='<%#Container.DataItem("CA_RECO")%>' Font-Size="10px"></asp:Label></ItemTemplate>
                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" 
                                    Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Right" />
                            </asp:TemplateColumn>
                            
                            <asp:TemplateColumn HeaderText="Inventariador">
                            <HeaderStyle Width="80px"></HeaderStyle>
                            <ItemTemplate><asp:Label id="LblInventariador" runat="server" CssClass="input" Width="80px" text='<%#Container.DataItem("NO_USUA_INVE")%>' Font-Size="10px"></asp:Label></ItemTemplate>
                            </asp:TemplateColumn>
                            
                            <asp:TemplateColumn HeaderText="Fecha_Inventario">
                            <HeaderStyle Width="120px" Font-Bold="False" Font-Italic="False" 
                                    Font-Overline="False" Font-Strikeout="False" Font-Underline="False" 
                                    HorizontalAlign="Center"></HeaderStyle>
                            <ItemTemplate><asp:Label id="LblFechaInventario" runat="server" CssClass="input" Width="120px" text='<%#Container.DataItem("FE_USUA_CREA")%>' Font-Size="10px"></asp:Label></ItemTemplate>
                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" 
                                    Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" />
                            </asp:TemplateColumn>
                    <asp:TemplateColumn>
                    <HeaderStyle Width="200px"></HeaderStyle>
		            <ItemTemplate>
                    
		                <asp:ImageButton id="btnEliminarS" runat="server" CommandName="Eliminar" ImageUrl="../Images/borrador.gif" ToolTip="Eliminar"></asp:ImageButton>
                    </ItemTemplate>
                    </asp:TemplateColumn>
	        </Columns>
	 </asp:datagrid>
    </td>
   </tr>
  </table>
   <%--Secuencia--%>
  <input id="HDN2" style="WIDTH: 30px; HEIGHT: 20px" type="hidden" size="1" name="HDN2" runat="server" />
  <input id="HDN1" style="WIDTH: 30px; HEIGHT: 20px" type="hidden" size="1" name="HDN1" runat="server" />
  <input id="HDNArticulo" style="WIDTH: 30px; HEIGHT: 20px" type="hidden" size="1" name="HDNArticulo" runat="server" />
  <input id="HDNUnidadMedida" style="WIDTH: 30px; HEIGHT: 20px" type="hidden" size="1" name="HDNUnidadMedida" runat="server" />
  <input id="HDNDesCtaGasto" style="WIDTH: 30px; HEIGHT: 20px" type="hidden" size="1" name="HDNDesCtaGasto" runat="server" />
  <input id="HDNCtaGasto" style="WIDTH: 30px; HEIGHT: 20px" type="hidden" size="1" name="HDNCtaGasto" runat="server" />
  <input id="HdnDescServicio" style="WIDTH: 30px; HEIGHT: 20px" type="hidden" size="1" name="HdnDescServicio" runat="server" />
 
 </form>
</center>
</body>
</html>
