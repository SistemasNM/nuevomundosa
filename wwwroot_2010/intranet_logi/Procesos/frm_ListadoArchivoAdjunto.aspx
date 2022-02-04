<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="frm_ListadoArchivoAdjunto.aspx.vb" Inherits="intranet_logi.WebForm1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
 <title>Listado de documentos adjuntos</title>
 <base target="_self"/>
 <script language="javascript" src="../../intranet/JS/jsCalendario_N3.js" type="text/javascript"></script>
 <link href="../css/NM0001.css" type="text/css" rel="stylesheet"/>

 <script language="javascript" type="text/javascript">
  function fnc_Cerrar() {
   window.close();
  }
  function fnc_AbrirDocumento(pstrDocumento, pstrVentana) {
      window.open(pstrDocumento, "Adjunto");
  }
 </script>
</head>

<body>
 <form id="form1" runat="server" method="post">
  <center>
   <%--Cabecera--%>
   <table cellspacing="0" cellpadding="0" border="0" style = "width: 700px; height: 20px">
    <tr>
	 <td style="HEIGHT: 20px" align="center" class = "Cabecera">LISTADO DE DOCUMENTOS ADJUNTOS</td>
    </tr>
   </table>

   <%-- Parte 1: Documento--%>
    <table style="WIDTH: 700px" cellspacing="0" cellpadding="0" border="0">
     <tr>
      <td style="WIDTH: 700px; height:10px" align="left" class="Etiqueta">
       <asp:Label id="lblTipoDoc" runat="server" Font-Bold="True"></asp:Label>
       &nbsp;<asp:Label id="lblNumDoc" runat="server" Font-Bold="True"></asp:Label>
      </td>
     </tr>
     <tr>
      <td style="WIDTH: 700px; height:10px" align="left" class="Etiqueta">
       <asp:Label id="lblDescripcionDoc" runat="server" Width="700px" Font-Bold="True"></asp:Label>
      </td>
     </tr>
    </table>

     <%--Parte 2: Controles ocultos--%>
   <table style="WIDTH: 700px; height:1px" cellspacing="0" cellpadding="0" border="0">
    <tr>
	 <td style="WIDTH:700px; HEIGHT:10px" align="right">
      <input id="hdnTipoDoc" style="WIDTH: 16px; HEIGHT: 20px" type="hidden" size="1" name="hdnCodigoDoc" runat="server"/>
      <input id="hdnCodigoDoc" style="WIDTH: 16px; HEIGHT: 20px" type="hidden" size="1" name="hdnCodigoDoc" runat="server"/>
      <input id="hdnDestinoAbrir" style="WIDTH: 16px; HEIGHT: 20px" type="hidden" size="1" name="hdnDestinoAbrir" runat="server"/>
      <input id="hdnDestinoGuardar" style="WIDTH: 16px; HEIGHT: 20px" type="hidden" size="1" name="hdnDestinoGuardar" runat="server"/>
     </td>
    </tr>
    </table>
    <%--Parte 3: Grilla--%>
    <table style="WIDTH: 700px" cellspacing="0" cellpadding="0" border="0">
     <tr>
	  <td style="WIDTH: 700px" align="center" valign="top">
       <asp:datagrid id="dgAdjuntos" runat="server" Width="700px" AutoGenerateColumns="False">
		<AlternatingItemStyle CssClass="GridAltItem"></AlternatingItemStyle>
		<ItemStyle CssClass="GridItem"></ItemStyle>
		<HeaderStyle CssClass="GridHeader"></HeaderStyle>
		<Columns>
			
         <asp:TemplateColumn>
		  <HeaderStyle Width="25px"></HeaderStyle>
		   <ItemStyle HorizontalAlign="Center"></ItemStyle>
		   <ItemTemplate>
			<asp:HyperLink id="hlnAbrirAdjunto" runat="server" CommandName="Ver" ToolTip= "Ver archivo adjunto"></asp:HyperLink>
            <asp:Label id="lblTipoAdjunto" runat="server" Visible="false" Text='<%# DataBinder.Eval(Container, "DataItem.TipoAdjunto") %>'></asp:Label>
            <asp:Label id="lblNombreAdjunto" runat="server" Visible="false" Text='<%# DataBinder.Eval(Container, "DataItem.ArchivoAdjunto") %>'></asp:Label>
		   </ItemTemplate>
		  </asp:TemplateColumn>

          <asp:TemplateColumn HeaderText="Sec.">
		   <HeaderStyle HorizontalAlign="Center" Width="25px" VerticalAlign="Middle"></HeaderStyle>
		   <ItemStyle Font-Size="9px" HorizontalAlign="Center"></ItemStyle>
		   <ItemTemplate>
            <asp:Label id="lblCodigoAdjunto" runat="server" Visible="true" Text='<%# DataBinder.Eval(Container, "DataItem.CodigoArchivo") %>'></asp:Label>
		   </ItemTemplate>
		  </asp:TemplateColumn>

          <asp:TemplateColumn HeaderText="Descripcion">
		   <HeaderStyle HorizontalAlign="Center" Width="150px" VerticalAlign="Middle"></HeaderStyle>
		   <ItemStyle Font-Size="9px" HorizontalAlign="Left"></ItemStyle>
		   <ItemTemplate><asp:Label id="lblDescripcion" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Descripcion") %>'></asp:Label></ItemTemplate>
		  </asp:TemplateColumn>
			
          <asp:TemplateColumn HeaderText="Contenido">
		   <HeaderStyle HorizontalAlign="Center" Width="100px"  VerticalAlign="Middle"></HeaderStyle>
		   <ItemStyle Font-Size="9px" HorizontalAlign="Left"></ItemStyle>
		   <ItemTemplate><asp:Label id="lblObservacion" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.vch_DescripcionContenido") %>'></asp:Label></ItemTemplate>
		  </asp:TemplateColumn>

          <asp:TemplateColumn HeaderText="Observaciones">
		   <HeaderStyle HorizontalAlign="Center" Width="150px" VerticalAlign="Middle"></HeaderStyle>
		   <ItemStyle Font-Size="9px" HorizontalAlign="Left"></ItemStyle>
		   <ItemTemplate><asp:Label id="lblObservaciones" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Observacion") %>'></asp:Label></ItemTemplate>
		  </asp:TemplateColumn>

          <asp:TemplateColumn HeaderText="Inf. Adjuntos">
		   <HeaderStyle HorizontalAlign="Center" Width="250px" VerticalAlign="Middle"></HeaderStyle>
		   <ItemStyle Font-Size="9px" HorizontalAlign="Left"></ItemStyle>
		   <ItemTemplate><asp:Label id="lblDatos" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Informacion") %>'></asp:Label></ItemTemplate>
		  </asp:TemplateColumn>
         </Columns>
		</asp:datagrid>
       </td>
	  </tr>
     </table>

      <%--Cerrar--%>
      <table style="WIDTH: 700px" cellspacing="0" cellpadding="0" border="0">
      <tr>
       <td class="pie" align="right">
        <asp:button id="btnCerrar" runat="server" Width="120px" Text="Cerrar" 
               CssClass="Boton" Visible="true" ToolTip = "Cerrar ventana"></asp:button>
       </td>
      </tr>
     </table>
  </center>
 </form>
</body>
</html>
