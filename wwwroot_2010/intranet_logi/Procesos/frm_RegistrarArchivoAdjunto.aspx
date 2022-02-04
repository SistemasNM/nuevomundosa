<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="frm_RegistrarArchivoAdjunto.aspx.vb" Inherits="intranet_logi.frm_RegistrarArchivoAdjunto" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html>
 <head>
 
  <title>Requisiciones - Doc. Adjuntos</title>
  <base target="_self"/>
  <meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR"/>
  <meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE"/>
  <meta content="JavaScript" name="vs_defaultClientScript"/>
  <meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema"/>
  
  <script language="javascript" src="../../intranet/JS/jsCalendario_N3.js" type="text/javascript"></script>
  <link href="../css/NM0001.css" type="text/css" rel="stylesheet"/>

  <%--<script language="javascript" src="../Scripts/jsCalendario_N3.js" type="text/javascript"></script>
  <link href="../Styles/NM0001.css" type="text/css" rel="stylesheet"/>--%>
  <style type="text/css">
      .pnlModal
      {
          background-color :#FFFFFF;
          border:solid 1px #000000;
          color:Black;
          font-family:Arial;
          font-size:10pt;	
          padding:3px 3px 3px 3px;
      }
      .pnlTitulo{
          background-color: #BCCAE0;;
          color:#333333;
          font-size:10pt;
          font-family:Arial;
          font-weight:bold;
          cursor:move;
          padding:3px 3px 3px 3px;
      }
  </style>
  <script language="javascript" type="text/javascript">
      
      function fnc_Cerrar() {
          window.close();
      }
      function fnc_AbrirDocumento(pstrDocumento, pstrVentana) {
          window.open(pstrDocumento, "Adjunto");
      }

      function fnc_RegistraDocsAdjuntos() {
          var pstrTipoDoc = document.all('hdnTipoDoc').value
          var pstrNumeroDoc = document.all('hdnCodigoDoc').value
          var pstrSecuencia = document.all('hdnSecuencia').value
          var retorno = window.showModalDialog("frm_AdjuntarArchivo.aspx?pstrTipoDoc=" + pstrTipoDoc + "&pstrNumeroDoc=" + pstrNumeroDoc + "&pstrSecuencia=" + pstrSecuencia, "Adjuntos archivo", "dialogHeight:240px;dialogWidth:650px;center:yes;help:no;");
          return false;
      }

 </script>
</head>

<body>
  <form id="frm_RegistrarArchivoAdjunto" method="post" autocomplete="off" runat="server">

  <center>
   <%--Cabecera--%>
   <table cellspacing="0" cellpadding="0" border="0" class = "Cabecera" style = "width: 700px; height: 20px">
    <tr>
	 <td class="cabecera" style="HEIGHT: 20px" align="center">REGISTRAR DOCUMENTOS ADJUNTOS</td>
    </tr>
   </table>

   <%--Parte 1: Controles ocultos--%>
   <table style="WIDTH: 700px; height:20px" cellspacing="0" cellpadding="0" border="0">
    <tr>
	 <td style="WIDTH:700px; HEIGHT:20px" align="right">
      <input id="hdnTipoDoc" style="WIDTH: 16px; HEIGHT: 20px" type="hidden" size="1" name="hdnCodigoDoc" runat="server"/>
      <input id="hdnCodigoDoc" style="WIDTH: 16px; HEIGHT: 20px" type="hidden" size="1" name="hdnCodigoDoc" runat="server"/>
      <input id="hdnSecuencia" style="WIDTH: 16px; HEIGHT: 20px" type="hidden" size="1" name="hdnSecuencia" runat="server"/>
      <input id="hdnCodigoArchivo" style="WIDTH: 16px; HEIGHT: 20px" type="hidden" size="1" name="hdnCodigoArchivo" runat="server"/>
      <input id="hdnMantenimiento" style="WIDTH: 16px; HEIGHT: 22px" type="hidden" size="1" name="hdnMantenimiento" runat="server"/>
      <input id="hdnDestinoAbrir" style="WIDTH: 16px; HEIGHT: 20px" type="hidden" size="1" name="hdnDestinoAbrir" runat="server"/>
      <input id="hdnAgregarArchivo" style="WIDTH: 16px; HEIGHT: 20px" type="hidden" size="1" name="hdnAgregarArchivo" runat="server"/>
      <input id="hdnDestinoGuardar" style="WIDTH: 16px; HEIGHT: 20px" type="hidden" size="1" name="hdnDestinoGuardar" runat="server"/>
     </td>
    </tr>
    </table>
  
    <%-- Parte 2: Documento--%>
    <table style="WIDTH: 700px" cellspacing="0" cellpadding="0" border="0">
     <tr>
      <td style="WIDTH: 700px; height:10px" align="left" class="Etiqueta">
       &nbsp;<asp:Label id="lblTipoDoc" runat="server" Font-Bold="True"></asp:Label>
       &nbsp;<asp:Label id="lblNumDoc" runat="server" Font-Bold="True"></asp:Label>
       &nbsp;<asp:Label id="lblSecuencia" runat="server" Font-Bold="True"></asp:Label>
      </td>
     </tr>
     <tr>
      <td style="WIDTH: 700px; height:10px" align="left" class="Etiqueta">
       &nbsp;<asp:Label id="lblDescripcionDoc" runat="server" Width="700px" Font-Bold="True"></asp:Label>
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
		  <HeaderStyle HorizontalAlign="Center" Width="20px" VerticalAlign="Middle"></HeaderStyle>
		  <ItemStyle Font-Size="9px" HorizontalAlign="Center"></ItemStyle>
		  <ItemTemplate>
           <asp:Label id="lblNumeroDoc" runat="server" Visible="false" Text='<%# DataBinder.Eval(Container, "DataItem.NumeroDoc") %>'></asp:Label>
           <asp:Label id="lblSecuencia" runat="server" Visible="false" Text='<%# DataBinder.Eval(Container, "DataItem.Secuencia") %>'></asp:Label>
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

         <asp:TemplateColumn HeaderText="Inf. Carga Archivo">
		  <HeaderStyle HorizontalAlign="Center" Width="400px" VerticalAlign="Middle"></HeaderStyle>
		  <ItemStyle Font-Size="9px" HorizontalAlign="Left"></ItemStyle>
		  <ItemTemplate><asp:Label id="lblDatos" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Informacion") %>'></asp:Label></ItemTemplate>
		 </asp:TemplateColumn>

         <asp:TemplateColumn>
		  <HeaderStyle Width="25px"></HeaderStyle>
		  <ItemTemplate><asp:ImageButton id="btnEliminarItem" runat="server" ImageUrl="../../intranet/Imagenes/eliminar.gif" CommandName="Eliminar" CommandArgument='<%# DataBinder.Eval(Container.DataItem,"Secuencia")%>' ToolTip= "Eliminar archivo adjunto"></asp:ImageButton></ItemTemplate>
		 </asp:TemplateColumn>
        </Columns>
	   </asp:datagrid>
      </td>
	  </tr>
      
      <%--Cerrar--%>
      <tr><td></td></tr>
      <tr>
       <td class="pie" align="right">
       <asp:button id="btnAgregar" runat="server" Width="120px" Text="Adjuntar archivo" CssClass="Boton" Visible="true" ToolTip= "Adjuntar archivo"></asp:button>
       &nbsp;<asp:button id="btnCerrar" runat="server" Width="120px" Text="Cerrar" CssClass="Boton" Visible="true" ToolTip = "Cerrar ventana"></asp:button>
       </td>
      </tr>
      <tr>
       <td td class="pie" align="left">
        <asp:Label ID="lblMensaje" runat="server" Text = "" Width="700px" CssClass="error"></asp:Label>
       </td>
      </tr>
     </table>
 </center>
</form>
</body>
</html>