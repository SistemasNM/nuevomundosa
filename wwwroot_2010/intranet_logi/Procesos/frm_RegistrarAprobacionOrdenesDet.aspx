<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="frm_RegistrarAprobacionOrdenesDet.aspx.vb" Inherits="intranet_logi.frm_RegistrarAprobacionOrdenesDet" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
 <title>Aprobacion de ordenes - detalle</title>
 <base target="_self"/>
 <link href="../css/sytle.css" rel="stylesheet"/>
 <link rel="stylesheet" type="text/css" href="../css/NM0001.css"/>

 <script language="javascript" type="text/javascript">

     function popUp(strUrl) {
         var intwidth = screen.width;
         var intheight = screen.height;
         window.open(strUrl, "", "top=0; left=0; width=" + intwidth + "; height=" + intheight + "; resizable=1;");
     }
     
     function popUp(strUrl) {
         var intwidth = screen.width;
         var intheight = screen.height;
         window.open(strUrl, "", "top=0; left=0; width=" + intwidth + "; height=" + intheight + "; resizable=1;");
     }

     function fnc_estadisticosxarticulo(pstrarticulo) {
         document.all('hdnarticuloestadistica').value = pstrarticulo;
         document.all('hdnposicionpanel').value = event.clientY + 10;
         document.all['pnldatosestadisticos'].style.display = 'inline';
         return true;
     }

     function fnc_ocultarestadisticos() {
         document.all('hdnarticuloestadistica').value = '';
         document.all['pnldatosestadisticos'].style.display = 'none';
     }

     function fnc_Desaprueba() {
         alert('Por favor ingrese una observacion (en el campo otras observaciones) para la Desaprobacion.');
         document.all('txtObservaciones2').focus();
     }

     //Lista fnc_AdjuntarDocs
     function fnc_ListarDocsAdjuntos() {
         var pstrTipoDoc = "RQS"
         var pstrNumeroDoc = document.all('txtNumReq').value
         var pstrSecuencia = " "
         var retorno = window.showModalDialog("frm_ListadoArchivoAdjunto.aspx?pstrTipoDoc=" + pstrTipoDoc + "&pstrNumeroDoc=" + pstrNumeroDoc + "&pstrSecuencia=" + pstrSecuencia, "Listar Adjuntos", "dialogheight:400px;dialogwidth:720px;center:yes;help:no;");
         return false;
     }

     // Funcion Busca Grupo
     function fnc_BuscarGrupo(strNumOC) {
         var pstrTipo = strNumOC.substring(0, 4)
         if (pstrTipo == "0001") {
             var pstrGrupo = "005";
         }
         else {
             var pstrGrupo = "009";
         }
         var retorno = window.showModalDialog("frm_SecuenciaAprobacionOrdenes.aspx?pstrTipo=" + pstrGrupo + "&pstrNumeroDoc=" + strNumOC, "", "dialogheight:370px;dialogwidth:520px;center:yes;help:no;status:no;toolbar=no;resizable:no;toolbar=no;resizable:no;");
         if (retorno != "" && retorno != ":") {
             var datos = retorno.split(":");
             var codigo = datos[0];
             var nombre = datos[1];
             window.close();
         }
     }
 </script>
</head>
<body>
 <form id="Form1" method="post" runat="server">
  <center>
      <br />
   <!-- Inicio: Cabecera --> 
   <div class="titulo">
    <table cellpadding="0" cellspacing="0" border="0" width="800px">
     <tr>
      <td width="800px" height="30px"> APROBACION DE ORDENES DE COMPRA/SERVICIOS - DETALLE</td>
     </tr>
    </table>
   </div>
   <!-- Fin: Cabecera --> 

   <!-- Datos --> 
   <div>
    <table border="0" cellspacing="0" cellpadding="0" width="800px">
     <tr>
	  <td style="width: 400px" align="left">
	   <table style="width: 400px" border="0" cellpadding="0" cellspacing="0">
        <tr>
         <td style="width:100px" align="left" class="Etiqueta">Número Req.:</td>
         <td><asp:textbox id="txtNumReq" runat="server" width="150px" CssClass="txtDeshabilitado" Visible="true"></asp:textbox></td>
        </tr>
        <tr>
	     <td style="width:100px" align="left" class="Etiqueta">Número OC/OS:</td>
	     <td style="width:300px" align="left"><asp:textbox id="txtDocumento" runat="server" width="150px" CssClass="txtDeshabilitado"></asp:textbox></td>
	    </tr>
        <tr>
		 <td style="width:100px" align="left" class="Etiqueta">Almacén:</td>
		 <td style="width:300px" align="left"><asp:textbox id="txtAlmacen" runat="server" width="300px" CssClass="txtDeshabilitado"></asp:textbox></td>
	    </tr>
        <tr>
	     <td style="width: 100px" align="left" class="Etiqueta">Moneda:</td>
	     <td style="width: 300px" align="left"><asp:textbox id="txtMoneda" runat="server" width="300px" CssClass="txtDeshabilitado"></asp:textbox></td>
	    </tr>
	    <tr>
	     <td style="width:100px" align="left" class="Etiqueta">Proveedor:</td>
	     <td style="width:300px" align="left"><asp:textbox id="txtProveedor" runat="server" width="300px" CssClass="txtDeshabilitado"></asp:textbox></td>
	    </tr>
        <tr>
	     <td style="width:100px" align="left" class="Etiqueta">Condición:</td>
	     <td style="width:300px" align="left"><asp:textbox id="txtCondicion" runat="server" width="300px" CssClass="txtDeshabilitado"></asp:textbox></td>
        </tr>
        <tr>
	     <td style="width:100px" align="left" class="Etiqueta">Fecha Creacion:</td>
	     <td style="width:300px" align="left"><asp:textbox id="txtFecCrea" runat="server" width="150px" CssClass="txtDeshabilitado"></asp:textbox></td>
	    </tr>
        <tr>
	     <td style="width:100px" align="left" class="Etiqueta">Fecha Ini. Trabajo:</td>
	     <td style="width:300px" align="left"><asp:textbox id="txtFecIni" runat="server" width="150px" CssClass="txtDeshabilitado"></asp:textbox></td>
	    </tr>
        <tr>
	     <td style="width:100px" align="left" class="Etiqueta">Fecha Fin Trabajo:</td>
	     <td style="width:300px" align="left"><asp:textbox id="txtFecFin" runat="server" width="150px" CssClass="txtDeshabilitado"></asp:textbox></td>
	    </tr>
	   </table>
      </td>
      <td style="width:400px" align="left" valign="top">
       <asp:label id="lblTitulo" runat="server" width="400px" Text = "Seguimiento" CssClass="Etiqueta"></asp:label>
       
       <asp:datagrid id="dtgSeguimiento" runat="server" width="400px" AutoGenerateColumns="False">
       <AlternatingItemStyle CssClass="gridalternateitem"></AlternatingItemStyle>
	   <ItemStyle CssClass="griditem"></ItemStyle>
	   <HeaderStyle CssClass="gridheader"></HeaderStyle>
	   <Columns>
          
        <asp:BoundColumn DataField="var_Tipodoc" HeaderText="Tipo Doc." HeaderStyle-Font-Bold="true">
         <HeaderStyle HorizontalAlign="Center" width="40px" Font-Bold="true" ></HeaderStyle>
		 <ItemStyle Font-Size="9px" HorizontalAlign="left" VerticalAlign="Middle" Width="40px"></ItemStyle>
        </asp:BoundColumn>
		
        <asp:BoundColumn DataField="var_Estado" HeaderText="Estado" HeaderStyle-Font-Bold="true">
          <HeaderStyle HorizontalAlign="Center" width="70px" Font-Bold="true" ></HeaderStyle>
		 <ItemStyle Font-Size="9px" HorizontalAlign="left" VerticalAlign="Middle" Width="70px"></ItemStyle>
        </asp:BoundColumn>

		<asp:BoundColumn DataField="var_Usuario" HeaderText="Usuario" HeaderStyle-Font-Bold="true">
         <HeaderStyle HorizontalAlign="Center" width="200px" Font-Bold="true" ></HeaderStyle>
		 <ItemStyle Font-Size="9px" HorizontalAlign="left" VerticalAlign="Middle" Width="200px"></ItemStyle>
        </asp:BoundColumn>

        <asp:BoundColumn DataField="var_Fecha" HeaderText="Fecha" HeaderStyle-Font-Bold="true">
         <HeaderStyle HorizontalAlign="Center" width="90px" Font-Bold="true" ></HeaderStyle>
		 <ItemStyle Font-Size="9px" HorizontalAlign="left" VerticalAlign="Middle" Width="90px"></ItemStyle>
        </asp:BoundColumn>
	   </Columns>
	   </asp:datagrid>
      </td>
     </tr>
    </table>
    
    <table border="0" cellspacing="0" cellpadding="0" width="800px">
     <tr>
      <td style="width:100px" align="left" class="Etiqueta">Observacion:</td>
      <td style="width:700px" align="left">
       <asp:textbox id="txtObservaciones" runat="server" width="700px" CssClass="txtAreaDeshabilitado" height="40px" TextMode="MultiLine"></asp:textbox>
      </td>
     </tr>
	 <tr>
	  <td style="width:100px" align="left" class="Etiqueta" valign="top">Otras Observaciones:</td>
      <td style="width:700px" align="left">
       <asp:textbox id="txtObservaciones2" runat="server" width="700px" CssClass="txtAreaHabilitado" height="40px" TextMode="MultiLine" Font-Size = "10px"></asp:textbox>
      </td>
     </tr>
     <tr>
      <td style="width:800px" align="left" colspan="2">
       <asp:label id="lblMsgDesaprobacion" runat="server" CssClass="mensaje"></asp:label>
      </td>
     </tr>
     <tr>
      <td style="width:800px" align="left" colspan="2">
       <asp:label id="lblError" runat="server" CssClass="error"></asp:label>
      </td>
     </tr>
	</table>
   </div>					
   <!-- Fin: Datos --> 

   <!-- Inicio: Grilla --> 
   <div>
    <table style="width:800px" border="0" cellpadding="0" cellspacing="0">
     <tr><td></td></tr>
     <tr>
      <td style="width:800px">
       <asp:datagrid id="dtgDetalle" runat="server" width="800px" AutoGenerateColumns="False">
        <AlternatingItemStyle CssClass="gridalternateitem"></AlternatingItemStyle>
	    <ItemStyle CssClass="griditem"></ItemStyle>
	    <HeaderStyle CssClass="gridheader"></HeaderStyle>
	    <Columns>
	     
         <asp:TemplateColumn>
          <HeaderStyle width="16px" Height="16px"></HeaderStyle>
          <ItemTemplate>
		   <asp:ImageButton id="ibtiestadisticas" runat="server" CommandName="cmd_estadisticas" ImageUrl="~/images/Lupa.png"></asp:ImageButton>
		  </ItemTemplate>
		 </asp:TemplateColumn>
		 
         <asp:BoundColumn DataField="var_ArticuloCodigo" HeaderText="Cod." HeaderStyle-Font-Bold = "true">
          <HeaderStyle HorizontalAlign="Center" width="80px" Font-Bold="true" ></HeaderStyle>
		  <ItemStyle Font-Size="9px" HorizontalAlign="left" VerticalAlign="Middle" Width="100px"></ItemStyle>
         </asp:BoundColumn>

		 <asp:BoundColumn DataField="var_ArticuloNombre" HeaderText="Descripcion." HeaderStyle-Font-Bold = "true">
		  <HeaderStyle HorizontalAlign="Center" width="370px" Font-Bold="true" ></HeaderStyle>
		  <ItemStyle Font-Size="9px" HorizontalAlign="left" VerticalAlign="Middle" Width="300px"></ItemStyle>
         </asp:BoundColumn>

		 <asp:BoundColumn DataField="var_UnidadMedidaCodigo" HeaderText="U.M." HeaderStyle-Font-Bold = "true">
          <HeaderStyle HorizontalAlign="Center" width="30px" Font-Bold="true" ></HeaderStyle>
		  <ItemStyle Font-Size="9px" HorizontalAlign="center" VerticalAlign="Middle" Width="30px"></ItemStyle>
		 </asp:BoundColumn>

		 <asp:BoundColumn DataField="num_CantidadSolicitada" HeaderText="Cantidad" DataFormatString="{0:#,##0.00}" HeaderStyle-Font-Bold = "true">
		  <HeaderStyle HorizontalAlign="Center" width="75px" Font-Bold="true" ></HeaderStyle>
		  <ItemStyle Font-Size="9px" HorizontalAlign="Right" VerticalAlign="Middle" Width="75px"></ItemStyle>
		 </asp:BoundColumn>
		
         <asp:BoundColumn DataField="num_Precio" HeaderText="Precio" DataFormatString="{0:#,##0.0000}" HeaderStyle-Font-Bold = "true">
		  <HeaderStyle HorizontalAlign="Center" width="75px" Font-Bold="true" ></HeaderStyle>
		  <ItemStyle Font-Size="9px" HorizontalAlign="Right" VerticalAlign="Middle" Width="75px"></ItemStyle>
		 </asp:BoundColumn>
		
         <asp:BoundColumn DataField="num_Descuento" HeaderText="Descuento" DataFormatString="{0:#,##0.00}" HeaderStyle-Font-Bold = "true">
		  <HeaderStyle HorizontalAlign="Center" width="75px" Font-Bold="true" ></HeaderStyle>
		  <ItemStyle Font-Size="9px" HorizontalAlign="Right" VerticalAlign="Middle" Width="75px"></ItemStyle>
		 </asp:BoundColumn>
		
         <asp:BoundColumn DataField="num_Total" HeaderText="SubTotal" DataFormatString="{0:#,##0.00}" HeaderStyle-Font-Bold = "true">
		  <HeaderStyle HorizontalAlign="Center" width="75px" Font-Bold="true" ></HeaderStyle>
		  <ItemStyle Font-Size="9px" HorizontalAlign="Right" VerticalAlign="Middle" Width="75px"></ItemStyle>
		 </asp:BoundColumn>
         
	    </Columns>
	   </asp:datagrid>
      </td>
	 </tr>
     <tr><td></td></tr>
    </table>
   </div>
   <!-- Fin: Grilla --> 

   <!-- Inicio: Resumen --> 
   <div>
    <table style="width:800px" border="0" cellpadding="0" cellspacing="0">
    <tr>
	 <td style="width:650px; height:20px" class="Etiqueta" align="right">Sub Total:</td>
     <td style="width:150px; height:20px" align="left"><asp:textbox id="txtSubTotal" runat="server" width="150px" CssClass="NumDeshabilitado"></asp:textbox></td>
    </tr>
    <tr>
	 <td style="width:650px; height:20px" class="Etiqueta" align="right">Descuento:</td>
	 <td style="width:150px; height:20px" align="left"><asp:textbox id="txtDescuento" runat="server" width="150px" CssClass="NumDeshabilitado"></asp:textbox></td>
    </tr>
    <tr>
	 <td style="width:650px; height:20px" class="Etiqueta" align="right">I.G.V.:</td>
	 <td style="width:150px; height:20px" align="left"><asp:textbox id="txtIGV" runat="server" width="150px" CssClass="NumDeshabilitado"></asp:textbox></td>
    </tr>
    <tr>
	 <td style="width:650px; height:20px" class="Etiqueta" align="right">Total:</td>
	 <td style="width:150px; height:20px" align="left"><asp:textbox id="txtTotal" runat="server" width="150px" CssClass="NumDeshabilitado"></asp:textbox></td>
    </tr>
   </table>
   </div>
   <!-- Fin: Resumen --> 
  
  <!-- Inicio: Botonera --> 
  <div>
   <table style="width: 800px" border="0" cellpadding="0" cellspacing="0">
    <tr>
    <td></td>
    </tr>
    <tr>
	 <td style="width:800px" align="right">
      <asp:button id="btnAnalisis" runat="server" width="120px" CssClass="btnAzul" Text="Analisis Detalle"></asp:button>
      &nbsp;<asp:button id="btnAdjuntos" runat="server" width="120px" CssClass="btnAzul" Text="Ver adjuntos"></asp:button>
      &nbsp;<asp:button id="btnImprimir" runat="server" width="120px" CssClass="btnAzul" Text="Imprimir"></asp:button>
      &nbsp;<asp:button id="btnAprobar" runat="server" width="120px" CssClass="btnVerde" Text="Aprobar"></asp:button>
      &nbsp;<asp:button id="btnDesaprobar" runat="server" width="120px" CssClass="btnRojo" Text="Desaprobar"></asp:button>
     </td>
    </tr>
   </table>
  </div>
  <!-- Inicio: Botonera --> 

  <!-- Inicio: Resumen 6 ultimos meses --> 
  <div>
   <asp:panel style="position:absolute; top:300px; left:100px" id="pnldatosestadisticos" BorderStyle="Solid" Borderwidth="8px" runat="server" width="600px" CssClass="GridItem" BorderColor="#336699">
    <table style="width:600px" class="GridItem" border="0" cellpadding="0" cellspacing="0">
     <%--parte 1--%>
     <tr>
      <td style="width:450px; height:20px" class="cabecera">&nbsp;ULTIMAS 6 COMPRAS</td>
      <td style="width:150px; height:20px" align="right">
       <input style="width:1px; height:1px" id="hdnarticuloestadistica" size="1" type="hidden" name="hdnarticuloestadistica" runat="server" />
       <input style="width:1px; height:1px" id="hdnposicionpanel" size="1" type="hidden" name="hdnposicionpanel" runat="server" />
       <input id="btnestcerrar" onclick="fnc_ocultarestadisticos();" value="Cerrar" type="button" name="btnestcerrar" class="btnAzul"/>
      </td>
     </tr>
     
     <%--parte 2--%>
     <tr>
      <td style="width:600px" class="GridItem" valign="top" colspan="2">
       <asp:datagrid id="dgestcompras" runat="server" width="580px" AutoGenerateColumns="False">
		<AlternatingItemStyle CssClass="gridalternateitem"></AlternatingItemStyle>
		<ItemStyle CssClass="griditem"></ItemStyle>
		<HeaderStyle CssClass="gridheader"></HeaderStyle>
		<Columns>
		 
         <asp:BoundColumn DataField="no_cort_prov" HeaderText="Proveedor">
          <HeaderStyle HorizontalAlign="Center" width="300px" Font-Bold="true"></HeaderStyle>
          <ItemStyle Font-Size="9px" HorizontalAlign="Left" VerticalAlign="Middle" Width="300px"></ItemStyle>
		 </asp:BoundColumn>
         
         <asp:BoundColumn DataField="fe_emis" HeaderText="Fecha" DataFormatString="{0:d}">
		  <HeaderStyle HorizontalAlign="Center" width="80px" Font-Bold="true"></HeaderStyle>
          <ItemStyle Font-Size="9px" HorizontalAlign="center" VerticalAlign="Middle" Width="80px"></ItemStyle>
		 </asp:BoundColumn>
		  
         <asp:BoundColumn DataField="ca_orde" HeaderText="Cant." DataFormatString="{0:N0}">
		  <HeaderStyle HorizontalAlign="Center" width="50px" Font-Bold="true" ></HeaderStyle>
		  <ItemStyle Font-Size="9px" HorizontalAlign="Right" VerticalAlign="Middle" Width="50px"></ItemStyle>
		 </asp:BoundColumn>
		
         <asp:BoundColumn DataField="co_mone" HeaderText="Mon.">
		  <HeaderStyle HorizontalAlign="Center" width="30px" Font-Bold="true" ></HeaderStyle>
		  <ItemStyle Font-Size="9px" HorizontalAlign="center" VerticalAlign="Middle" Width="30px"></ItemStyle>
		 </asp:BoundColumn>
		  
         <asp:BoundColumn DataField="im_unit" HeaderText="P.Compra" DataFormatString="{0:F2}">
		  <HeaderStyle HorizontalAlign="Center" width="75px" Font-Bold="true"></HeaderStyle>
		  <ItemStyle Font-Size="9px" HorizontalAlign="Right" VerticalAlign="Middle" Width="70px"></ItemStyle>
		 </asp:BoundColumn>
			
         <asp:BoundColumn DataField="im_cost_movi" HeaderText="P.Costo(US$)" DataFormatString="{0:F2}">
		  <HeaderStyle HorizontalAlign="Center" width="75px" Font-Bold="true"></HeaderStyle>
		  <ItemStyle Font-Size="9px" HorizontalAlign="Right" VerticalAlign="Middle" Width="70px"></ItemStyle>
		 </asp:BoundColumn>

		</Columns>
	   </asp:datagrid>
      </td>
     </tr>

     <%--parte 3--%>
     <tr>
      <td style="width:600px" class="cabecera" height="20px" colspan="2">&nbsp;CONSUMO DE LOS ULTIMOS&nbsp;6 MESES :</td>
	 </tr>
     <tr>
	  <td style="width: 300px" class="GridItem" valign="top">
	   <asp:datagrid id="dgestconsumo" runat="server" Width="300px" AutoGenerateColumns="False">
		<AlternatingItemStyle CssClass="gridalternateitem"></AlternatingItemStyle>
		<ItemStyle CssClass="griditem"></ItemStyle>
		<HeaderStyle CssClass="gridheader"></HeaderStyle>
		<Columns>
		 
         <asp:BoundColumn DataField="ano_mes" HeaderText="A&#241;o - Mes">
		  <HeaderStyle HorizontalAlign="Center" width="100px" Font-Bold="true"></HeaderStyle>
		  <ItemStyle Font-Size="9px" HorizontalAlign="left" VerticalAlign="Middle"></ItemStyle>
		 </asp:BoundColumn>
		 
         <asp:BoundColumn DataField="total" HeaderText="Cantidad" DataFormatString="{0:F2}">
		  <HeaderStyle HorizontalAlign="Center" width="200px" Font-Bold="true"></HeaderStyle>
		  <ItemStyle Font-Size="9px" HorizontalAlign="Right" VerticalAlign="Middle"></ItemStyle>
		 </asp:BoundColumn>
		</Columns>
	   </asp:datagrid>
      </td>
	  
      <%--parte 4--%>
      <td style="WIDTH: 300px" class="GridItem" valign="top">
	   <table style="WIDTH: 300px; HEIGHT: 50px" border="0" cellpadding="0" cellspacing="0">
		<tr>
		 <td style="WIDTH: 150px" class="Etiqueta" align="right">&nbsp;Stock actual:&nbsp;</td>
		 <td style="WIDTH: 150px" align="left"><asp:TextBox id="txteststockactual" runat="server" Width="80px" CssClass="NumDeshabilitado" Font-Size="9px"></asp:TextBox></td>
		</tr>
		<tr>
		 <td style="WIDTH: 150px" class="Etiqueta" align="right">&nbsp;Consumo promedio:&nbsp;</td>
         <td style="WIDTH: 150px" align="left"><asp:TextBox id="txtestconsumopromedio" runat="server" Width="80px" CssClass="NumDeshabilitado" Font-Size="9px"></asp:TextBox></td>
		</tr>
	   </table>
	  </td>
	 </tr>
    </table>
   </asp:panel>
  </div>
  <!-- Fin: Resumen 6 ultimos meses --> 
 </center>
</form>
</body>
</html>
