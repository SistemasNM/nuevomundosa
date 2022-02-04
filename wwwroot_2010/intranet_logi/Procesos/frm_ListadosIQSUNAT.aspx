<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="frm_ListadosIQSUNAT.aspx.vb" Inherits="intranet_logi.frm_ListadosIQSUNAT" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html>
 <head>
 <title>Listado de insumos quimicos - SUNAT</title>
 <meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR" />
 <meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE" />
 <meta content="JavaScript" name="vs_defaultClientScript" />
 <meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema" />
 <link href="../css/NM0001.css" type="text/css" rel="stylesheet"/>
 <script language="javascript" type="text/javascript" src="../js/jsCalendario_N3.js"></script>
 <script language="javascript" type="text/javascript" src="../../intranet/JS/functions.js"></script>
 
 <script language="javascript" type="text/javascript">
     function btnReporte_Onclick(strUrl) {
         var intWidth = screen.width;
         var intHeight = screen.height;
         window.open(strUrl, "", "top=0; left=0; width=" + intWidth + "; height=" + intHeight + "; resizable=1;");
     }

     function fnc_AdjuntarDocs() {
         if (document.forms[0].cmbAnno.value == "0000") {
             alert("Por favor seleccione el año...!");
             return false;
         }
         if (document.forms[0].cmbMes.value == "00") {
             alert("Por favor seleccione el mes...!");
             return false;
         }
         var pstrAnno = document.forms[0].cmbAnno.value;
         var pstrMes = document.forms[0].cmbMes.value;
         var retorno = window.showModalDialog("frm_RegistroAdjuntoIQ.aspx?pstrAnno=" + pstrAnno + "&pstrMes=" + pstrMes, "Adjuntar archivo", "dialogHeight:400px;dialogWidth:720px;center:yes;help:no;");
         return false;
     }

     // Mostrar reporte

     function fMostrarReporte(strURL) {
         var intWidth = screen.width;
         var intHeight = screen.height;
         window.open(strURL, "", "top=0; left=0; width=" + intWidth + "; height=" + intHeight + "; resizable=1;");
     }
 </script>
</head>
<body>
 <form id="frm_listadoIQSUNAT"  method="post" runat="server">
  <center>
   <table border="0" cellpadding="0" cellspacing="0" style="width:800px;height:30px">
    <tr>
     <td style="width:800px" align="center" valign="middle" class="Cabecera" >LISTADO DE INSUMOS QUIMICOS 
         FISCALIZDOS X SUNAT</td>
    </tr>
   </table>
   <table border="0" cellpadding="0" cellspacing="0" style="width:800px">
    <tr>
     <td class="Etiqueta" style="width:100px" align="left">Fec. Inicio:</td>
     <td style="width:150px" align="left">
      <asp:textbox id="TxtFechaPresentacionIni" runat="server" Width="120px" Font-Size="10px" MaxLength="10" CssClass="inputDisabled"></asp:textbox>
      <img onclick="popUpCalendar(this, frm_listadoIQSUNAT.TxtFechaPresentacionIni, 'dd/mm/yyyy')" border="0" alt="Seleccione fecha de presentacion de información...!" src="../../intranet/Imagenes/Calendario.gif" width="16" height="16"/>
     </td>
     <td style="width:100px" align="left" class="Etiqueta">Fec. Fin:</td>
     <td style="width:150px" align="left"><asp:textbox id="TxtFechaPresentacionFin" runat="server" Width="120px" Font-Size="10px" MaxLength="10" CssClass="inputDisabled"></asp:textbox>
      <img onclick="popUpCalendar(this, frm_listadoIQSUNAT.TxtFechaPresentacionFin, 'dd/mm/yyyy')" border="0" alt="Seleccione fecha de presentacion de información...!" src="../../intranet/Imagenes/Calendario.gif" width="16" height="16"/></td>
     <td style="width:300px" align="right">
         &nbsp;</td>
    </tr>
   </table>

   <table border="0" cellpadding="0" cellspacing="0" style="width:800px">
    <tr>
	 <td class="Etiqueta" style="width:100px" align="left">Movimientos:</td>
	 <td style="width:150px" align="left">
      <asp:DropDownList ID="cmbMovimiento" runat="server" Font-Size="10px" Width="150px">
       <asp:ListItem Value="00">Seleccione</asp:ListItem>
       <asp:ListItem Value="I">Ingresos</asp:ListItem>
       <asp:ListItem Value="S">Salidas</asp:ListItem>
      </asp:DropDownList>
     </td>	
     <td  style="width:100px" align="left">&nbsp;</td>
	 <td style="width:150px" align="left">
          </td>
     <td style="width:300px" align="right">
      <asp:button id="btnConsultar" runat="server" Text="Consultar" CssClass="Boton" Width="100px"></asp:button>
      <asp:button id="cmdGrabar" runat="server" Text="Generar (TXT)" CssClass="Boton" 
             Width="100px"></asp:button>
     </td>
    </tr>
   </table>

   
   <table border="0" cellpadding="0" cellspacing="0" style="width:800px">
    <tr>
	 <td align="left"><asp:label id="lblMsg" runat="server" CssClass="error"></asp:label></td>
    </tr>
    <tr>
     <td>
      <asp:datagrid id="dtgLista" runat="server" Width="800px" AutoGenerateColumns="False">
       <AlternatingItemStyle CssClass="GridAltItem"></AlternatingItemStyle>
	   <ItemStyle CssClass="GridItem"></ItemStyle>
	   <HeaderStyle CssClass="gridheader"></HeaderStyle>
	   <Columns>
        
        <%--<asp:TemplateColumn>
		 <ItemTemplate>
		  <asp:ImageButton id="btnReporte" runat="server" ImageUrl="../../intranet/Imagenes/buscar.gif" ToolTip="Ver seguimiento" CommandName="Seg" Width="16px" Height="16px"></asp:ImageButton>
         </ItemTemplate>
		 <ItemStyle HorizontalAlign="Center"></ItemStyle>
	    </asp:TemplateColumn>--%>
       
       <asp:TemplateColumn HeaderText="Codigo">
        <HeaderStyle HorizontalAlign="Center" Width="100px" Font-Bold="false"></HeaderStyle>
		<ItemStyle Font-Size="9px" HorizontalAlign="center"></ItemStyle>
		<ItemTemplate>
		 <asp:Label id="lblCodigoArticulo" runat="server" Font-Size="9px" Text='<%# DataBinder.Eval(Container, "DataItem.presentacion_producto") %>'></asp:Label>
        </ItemTemplate>
	   </asp:TemplateColumn>
  								
       <asp:BoundColumn DataField="de_item" HeaderText="Descripcion">
        <HeaderStyle HorizontalAlign="center"></HeaderStyle>
        <ItemStyle Font-Size="9px" HorizontalAlign="left" VerticalAlign="Middle" Width="300px"></ItemStyle>
       </asp:BoundColumn>
													
       <asp:BoundColumn DataField="fecha_transaccion" HeaderText="Fecha Mov.">
        <HeaderStyle HorizontalAlign="center"></HeaderStyle>
        <ItemStyle Font-Size="9px" HorizontalAlign="center" VerticalAlign="Middle" Width="100px"></ItemStyle>
       </asp:BoundColumn>
													
       <asp:BoundColumn DataField="cantidad" HeaderText="Cantidad" DataFormatString="{0:#,##0.00}">
        <HeaderStyle HorizontalAlign="center"></HeaderStyle>
        <ItemStyle Font-Size="9px" HorizontalAlign="Right" VerticalAlign="Middle" Width="100px "></ItemStyle>
       </asp:BoundColumn>
													
       <%--<asp:BoundColumn DataField="num_CantidadSalida" HeaderText="Salidas"  DataFormatString="{0:#,##0.00}">
        <HeaderStyle HorizontalAlign="center"></HeaderStyle>
        <ItemStyle Font-Size="9px" HorizontalAlign="Right" VerticalAlign="Middle" Width="100px "></ItemStyle>
       </asp:BoundColumn>--%>
                                                    
       <%--<asp:BoundColumn DataField="num_Saldo" HeaderText="Saldo"  DataFormatString="{0:#,##0.00}">
        <HeaderStyle HorizontalAlign="center"></HeaderStyle>
        <ItemStyle Font-Size="9px" HorizontalAlign="Right" VerticalAlign="Middle" Width="100px "></ItemStyle>
       </asp:BoundColumn>--%>

	  </Columns>
     </asp:datagrid>
    </td>
   </tr>
  </table>
 </center>
</form>
</body>
</html>
