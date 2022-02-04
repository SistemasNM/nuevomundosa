<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="frmListaOS.aspx.vb" Inherits="intranet_logi.frmListaOS" %>
<%@ Register Assembly="Infragistics35.Web.v10.1, Version=10.1.20101.1011, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" Namespace="Infragistics.Web.UI.EditorControls" TagPrefix="ig" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html>
 <head>
 <title>ORDENES DE SERVICIO- Pendientes/Atendidos</title>
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
       //window.location.href = "frmFichaProveedor.aspx?strNumeroOrdenServicio=" + strCodigo;
       var retorno = window.showModalDialog("frmFichaProveedor.aspx?strNumeroOrdenServicio=" + strCodigo, "", "dialogHeight:600px;dialogWidth:900px;center:yes;help:no;");
       if (retorno == "") {
           return false;
       }
       return false;
   }

   function btnReporte_Onclick(strUrl) {
       var intwidth = screen.width;
       var intheight = screen.height;
       window.open(strUrl, "", "top=0; left=0; width=" + intwidth + "; height=" + intheight + "; resizable=1;");
   }

   function FormatearBusqDoc(pTexto) {
       if (pTexto == 2)//numero
       {
           var lnume = '00000000000' + document.all["txtNumOrden"].value;
           lnume = lnume.substring(lnume.length, lnume.length - 10);
           if (lnume == '0000000000') {
               document.all["txtNumOrden"].value = '';
           } else {
               document.all["txtNumOrden"].value = lnume;
           } //end if

       } //end if
   } //end function
   			
 </script>
</head>

<body>
 <form id="frmListadoOS"  method="post" runat="server">
 <center>
  <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager> 
  
  <%--Cabecera--%>
  <table style="width:800px;" border="0" cellpadding="0" cellspacing="0">
   <tr>
    <td class="Cabecera" style="width:800px; height:30px" align="left" valign="middle">&nbsp;LISTADO DE ORDENES DE SERVICIO - CONFORMIDAD</td>
   </tr>
   <tr><td></td></tr>
  </table>

  <%--Filtros--%>
	<table style="width:800px" border="0" cellpadding="0" cellspacing="0">
     <tr>
      <td style="width:120px" class="Etiqueta" align="left">&nbsp;Estado O/S:</td>
	  <td style="width:130px" align="left">
       <asp:DropDownList ID="cmbOpcion" runat="server" width="120px" CssClass="cbo" Font-Size="10px">
        <asp:ListItem Value="00">Seleccionar estado</asp:ListItem>
        <asp:ListItem Value="PEN">O/S por concluir</asp:ListItem>
        <asp:ListItem Value="CON">O/S concluidas</asp:ListItem>
       </asp:DropDownList>
      </td>
      <td class="Etiqueta" style="width: 550px" align="right" colspan="2">
       <asp:button id="btnBuscar" runat="server" Text="Buscar" CssClass="Boton"></asp:button>
      </td>
	 </tr>
     <tr>
      <td class="Etiqueta" style="width:120px" align="left">&nbsp;Proveedor:</td>
	  <td style="width:130px" align="left">
       <asp:textbox id="TxtCodigoProveedor" runat="server" width="90px" CssClass="input"></asp:textbox>
       &nbsp;<input id="btnProveedores" style="width:20px; height:20px" onclick="fdesListaProveedor(TxtCodigoProveedor,txtNombreProveedor);" type="button" size="20" value="..." name="btnProveedores" class="Boton"/>
      </td>
	  <td class="Etiqueta" style="width:550px" align="left" colspan="2">
       <asp:textbox id="txtNombreProveedor" runat="server" width="540px" Height="20px" CssClass="txtReadOnly"></asp:textbox>
      </td>
     </tr>
     <tr>
      <td class="Etiqueta" style="width:120px" align="left">&nbsp;Fec. Inicio:</td>
      <td style="width:130px" align="left">
       <ig:WebDatePicker ID="wdpFecIni" runat="server" width="120px" Font-Size="10px"></ig:WebDatePicker>
      </td>
      <td class="Etiqueta" style="width:70px" align="left">Fec. Fin:</td>
      <td class="Etiqueta" style="width:480px" align="left">
       <ig:WebDatePicker ID="wdpFecFin" runat="server" width="120px" Font-Size="10px"></ig:WebDatePicker>
      </td>
     </tr>
     <tr> 
      <td class="Etiqueta" style="width:120px" align="left">&nbsp;Num. O/S:</td>
      <td style="width:130px" align="left">
       <asp:TextBox ID="txtSerie" runat="server" Font-Size="9px" Width="30px" CssClass="inputDisabled"></asp:TextBox>
       <asp:TextBox ID="txtNumOrden" runat="server" Font-Size="9px" Width="80px" CssClass="input" MaxLength="10"></asp:TextBox>
      </td>
      <td class="Etiqueta" style="width:70px" align="left"></td>
      <td class="Etiqueta" style="width:480px" align="left"></td>
      </tr>
    </table>
    <%--Grilla--%>
    <table cellspacing="0" cellpadding="0"  border="0" width="800px">
     <tr>
	  <td align="left"><asp:label id="lblMsg" runat="server" Width="800px" CssClass="error"></asp:label></td>
	 </tr>
     <tr>
      <td align="right"><asp:Label ID="lblContador" runat="server" Text=" " CssClass="contador"></asp:Label></td>
     </tr>
     <tr>
	  <td>
       <asp:datagrid id="dtgLista" runat="server" width="800px" AutoGenerateColumns="False" Font-Bold="false">
        <AlternatingItemStyle CssClass="GridAlternateItem"></AlternatingItemStyle>
	    <ItemStyle CssClass="GridItem"></ItemStyle>
	    <HeaderStyle CssClass="gridheader"></HeaderStyle>        
		<Columns>

		 <asp:TemplateColumn>
          <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="20px"></HeaderStyle>
		  <ItemTemplate>
		   <input id="btnSeleccion" name="btnSeleccion" runat="server" type="button" value="..." style="cursor:hand; width:20px; height:20px" class="Boton" size="20" title="Presione para ver detalle de OS."/>
		  </ItemTemplate>
		 </asp:TemplateColumn>

         <asp:TemplateColumn>
          <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="20px"></HeaderStyle>
          <ItemTemplate>
           <asp:ImageButton id="btnReporte" runat="server" src="../../intranet/Imagenes/buscar.gif" Width="20px" Height="20px"/>
          </ItemTemplate>
         </asp:TemplateColumn>
         
         <asp:BoundColumn DataField="var_Numero" HeaderText="Orden Servicio">
          <HeaderStyle HorizontalAlign="Center" Width="100px" VerticalAlign="Middle" Font-Bold="true"></HeaderStyle>
          <ItemStyle Font-Size="9px" HorizontalAlign="left" VerticalAlign="Middle"></ItemStyle>
         </asp:BoundColumn>

		 <asp:BoundColumn DataField="fe_emis" HeaderText="Fec. Solicitud">
          <HeaderStyle HorizontalAlign="Center" Width="70px" VerticalAlign="Middle" Font-Bold="true"></HeaderStyle>
          <ItemStyle Font-Size="9px" HorizontalAlign="left" VerticalAlign="Middle"></ItemStyle>
         </asp:BoundColumn>

         <asp:BoundColumn DataField="CO_PROV" HeaderText="R.U.C">
          <HeaderStyle HorizontalAlign="Center" Width="80px" VerticalAlign="Middle" Font-Bold="true"></HeaderStyle>
          <ItemStyle Font-Size="9px" HorizontalAlign="left" VerticalAlign="Middle"></ItemStyle>
         </asp:BoundColumn>
         
         <asp:BoundColumn DataField="NO_CORT_PROV" HeaderText="Proveedor">
          <HeaderStyle HorizontalAlign="Center" Width="250px" VerticalAlign="Middle" Font-Bold="true"></HeaderStyle>
          <ItemStyle Font-Size="9px" HorizontalAlign="left" VerticalAlign="Middle"></ItemStyle>
         </asp:BoundColumn>
         
         <asp:BoundColumn DataField="chr_EstadoServicio" HeaderText="Calif.">
          <HeaderStyle HorizontalAlign="Center" Width="50px" VerticalAlign="Middle" Font-Bold="true"></HeaderStyle>
          <ItemStyle Font-Size="9px" HorizontalAlign="center" VerticalAlign="Middle"></ItemStyle>
         </asp:BoundColumn>
		 
         <asp:BoundColumn DataField="chr_EstadoServicio" HeaderText="Aprob.">
          <HeaderStyle HorizontalAlign="Center" Width="50px" VerticalAlign="Middle" Font-Bold="true"></HeaderStyle>
          <ItemStyle Font-Size="9px" HorizontalAlign="center" VerticalAlign="Middle"></ItemStyle>
         </asp:BoundColumn>
		 
         <asp:BoundColumn DataField="co_usua_crea" HeaderText="Usuario">
          <HeaderStyle HorizontalAlign="Center" Width="100px" VerticalAlign="Middle" Font-Bold="true" ></HeaderStyle>
          <ItemStyle Font-Size="9px" HorizontalAlign="left" VerticalAlign="Middle"></ItemStyle>
         </asp:BoundColumn>
        </Columns>
	   </asp:datagrid>
	  </td>
	 </tr>
	</table>
   </center>
  </form>
</body>
</html>