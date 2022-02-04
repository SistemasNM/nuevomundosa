<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="frm_InventarioDiarioRegistrar_v2.aspx.vb" Inherits="intranet_logi.frm_InventarioDiarioRegistrar_v2" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
 <title>Inventario diario (ERI)</title>
 <link href="../css/NM0001.css" type="text/css" rel="stylesheet"/>
 <link href="../css/sytle.css" type="text/css" rel="stylesheet"/>

 <script type="text/javascript" language="javascript" src="../../intranet/JS/jsCalendario_N3.js"></script>
 <script type="text/javascript" language="javascript" src="../../intranet/JS/functions.js"></script>
 <script type="text/javascript" language="javascript" src="../../enlacenm_costosreales/js/jsDesTabla.js"></script>
 <script type="text/javascript" language="JavaScript">
     // Ventana
     function modalWin(purl) {
         if (window.showModalDialog) {
             window.showModalDialog(purl, "name", "dialogwidth:255px;dialogheight:250px");
         } else {
             window.open(purl, "name", "height=255,width=250,toolbar=no,directories=no,status=no, menubar=no,scrollbars=no,resizable=no ,modal=yes");
         }
     }
     // Ventana Reporte
     function popUp(strUrl) {
         var intwidth = screen.width;
         var intheight = screen.height;
         window.open(strUrl, "", "top=0; left=0; width=" + intwidth + "; height=" + intheight + "; resizable=1;");
     }

     // Eliminar registro
     function fnc_Eliminar(ControlID) {
         var lstrMensaje = 'Se eliminará el registro de -- ' + document.all(ControlID + '_lblTrabDesI').innerHTML + ' --.\n\n¿Esta seguro de continuar?';
         return confirm(lstrMensaje);
     }
     // Actualizar registro
     function fnc_Update(ControlID) {
         var lstrMensaje = 'Se actualizará el reintegro de -- ' + document.all(ControlID + '_lblTrabDesI').innerHTML + ' --.\n\n¿Esta seguro de continuar?';
         return confirm(lstrMensaje);
     }

     // Muestra reporte
     function fMostrarReporte(strUrl) {
         var intwidth = screen.width;
         var intheight = screen.height;
         window.open(strUrl, "", "top=0; left=0; width=" + intwidth + "; height=" + intheight + "; resizable=1;");
     }

     //Listar Inventario Diario
     function ListarERI() {

         var strUnidad = document.all('ddlUnidad').value;
         var retorno = window.showModalDialog("../Buscadores/frmBuscarERI.aspx?pCodU=" + strUnidad, "", "dialogHeight:450px;dialogWidth:450px;center:yes;help:no;");
         if (retorno != "" && retorno != ":") {
             var datos = retorno.split(":");
             var Codigo = datos[0];
             var Almacen = datos[1];

             if (Almacen == 'TODOS') { Almacen = '' }

             document.all('txtcodinv').value = Codigo;
             document.all('ddlAlmacenes').value = Almacen;

             return true;

         }
         else {
             return false;
         }
     }

     //Verifica generar ERI
     function ValidaGenerar() {
         var posicion = document.getElementById('ddlAlmacenes').options.selectedIndex; //posicion
         var strAlmacen = document.getElementById('ddlAlmacenes').options[posicion].text;

         if (posicion == 0) {
             alert("Debe seleccionar un Almacen");
             frm_InventarioDiario.ddlAlmacenes.focus();
             return false;
         }

         var bResult = confirm("Está seguro que desea generar un ERI para el Almacen : " + strAlmacen);
         if (bResult == true) {
             return true;
         }
         else {
             return false;
         }
     }


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
            width: 120px;
            height: 6px;
        }
    </style>


</head>
<body>
 <form id="frm_InventarioDiario" autocomplete="off" runat="server" method="post">
  <center>
      <%--ajax--%>   <%--<asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
   <script language="JavaScript" src="../../intranet/Js/jsUpdateProgress.js" type="text/jscript" ></script>    
   <asp:UpdateProgress ID="UpdateProg1" DisplayAfter="0" runat="server">
    <ProgressTemplate>     
     <center>
      <div id="divLoad" class="CssLoadNormal">
       <br/>
       <img src="../../intranet/imagenes/Loading.gif" style="vertical-align:middle" alt=""/>
       <br/>
       Procesando...                
      </div>   
     </center>
    </ProgressTemplate>
   </asp:UpdateProgress--%>
   <%--titulo--%>
   <table cellspacing="0" cellpadding="0" border="0" width="800px">
    <tr>
	 <td style=" width:800px; height:30px" class="Cabecera">&nbsp;INVENTARIO DIARIO - ERI</td>
    </tr>
   </table>

   <table cellpadding="0" cellspacing="0" border="0" width="800px" >
       <%--botonera--%>
    <tr><td style="width:800px; height:10px" colspan="7"></td></tr>
    <tr>
     <td style="width:120px; height:20px" align="left"><asp:Button ID="btnNuevo" runat="server" CssClass="btnAzul" Text="Nuevo"/></td>
     <td style="width:120px; height:20px" align="left"><asp:Button ID="btnGenerar" runat="server" CssClass="btnAzul" Text="Generar (ERI)"/></td>
     <td style="width:120px; height:20px" align="left"><asp:Button ID="btnGuardar" runat="server" CssClass="btnAzul" Text="Guardar" /></td>
     <td style="width:120px; height:20px" align="left"><asp:Button ID="btnEstado" runat="server" CssClass="btnAzul" Text="Cerrar" /></td>
     <td style="width:120px; height:20px" align="left"><asp:Button ID="btnExportar" runat="server" CssClass="btnAzul" Text="Exportar" /></td>
     <td style="width:120px; height:20px" align="left"></td>
 	 <td style="width:80px; height:20px" align="left"></td>
    </tr>
    <tr><td style="width:800px; height:10px" colspan="7"></td></tr>
    
    <%--Parametros--%>
    <tr>
	 <td style="height:20px" align="left" class="Etiqueta" colspan="6">
         <table cellpadding="0" cellspacing="0" style="width: 800px">
             <tr>
                 <td style="width: 100px">&nbsp;&nbsp;Unidad:</td>
                 <td style="width: 280px"><asp:DropDownList ID="ddlUnidad" runat="server" Height="20px" Width="123px" CssClass ="ddlListado">
                     </asp:DropDownList></td>
                 <td style="width: 30px">&nbsp;</td>
                 <td style="width: 120px">&nbsp;Situación:</td>
                 <td style="width: 200px"><asp:Label ID="lblEstado" runat="server" Text="" CssClass="mensaje" Width="50px"></asp:Label></td>
                 <td>&nbsp;</td>
             </tr>
             <tr>
                 <td style="width: 100px">&nbsp;</td>
                 <td style="width: 280px">&nbsp;</td>
                 <td style="width: 30px">&nbsp;</td>
                 <td style="width: 120px">&nbsp;</td>
                 <td style="width: 200px">&nbsp;</td>
                 <td>&nbsp;</td>
             </tr>
             <tr>
                 <td style="width: 100px">&nbsp;&nbsp;Almacen :</td>
                 <td style="width: 280px"><asp:DropDownList ID="ddlAlmacenes" runat="server" Height="20px" Width="272px" CssClass ="ddlListado" AutoPostBack="true">
                     </asp:DropDownList></td>
                 <td style="width: 30px">&nbsp;</td>
                 <td style="width: 120px">&nbsp;Código Inventario:</td>
                 <td style="width: 200px">&nbsp;<asp:TextBox ID="txtcodinv" AutoPostBack="false" runat="server" CssClass="input" MaxLength="10" Width="116px" Font-Bold="true" Font-Size="10px"></asp:TextBox>&nbsp;
                            <asp:Button ID="btnConsultar" name="btnConsultar" runat="server" CssClass="btnAzul" text="..." style="WIDTH:20px; HEIGHT:20px"/> 
                            </td>
                 <td>&nbsp;</td>
             </tr>
         </table>
        </td>
     <td style="width:80px; height:20px" align="left">&nbsp;</td>
 	</tr>
    <tr><td style="width:800px; height:10px" colspan="7"></td></tr>

    <tr>
     <td style="width:120px; height:20px" class="Etiqueta" align="center" colspan="2">
         <asp:RadioButtonList ID="rdbParamBusqueda" runat="server" 
             RepeatDirection="Horizontal" Width="254px" AutoPostBack="true" Visible="false">
             <asp:ListItem Value="ART">Articulo</asp:ListItem>
             <asp:ListItem Value="UBI">Ubicacion</asp:ListItem>
            
         </asp:RadioButtonList>
     </td>
     <td style="width:120px; height:20px" class="Etiqueta" align="center">
         <asp:Label ID="lblInfoDesde" runat="server" Text="Desde:" Visible="false"></asp:Label></td>
     <td style="width:120px; height:20px" class="Etiqueta" align="center">
         <asp:TextBox ID="txtUbicDesde" runat="server" Visible="false"></asp:TextBox></td>
     <td style="width:120px; height:20px" class="Etiqueta" align="center"><asp:Label ID="lblInfoHasta" runat="server" Text="Hasta:" Visible="false"></asp:Label></td>
     <td style="width:120px; height:20px" class="Etiqueta" align="center"><asp:TextBox ID="txtUbicHasta" runat="server" Visible="false"></asp:TextBox></td>
     <td style="width:80px; height:20px"></td>
    </tr>

    <tr><td style="width:800px; height:10px" colspan="7"></td></tr>

    <tr>
     <td class="style1" align="center" colspan="1">
        <asp:Label ID="lblInfoArt" runat="server" Text="Art." Visible="false"></asp:Label>
     </td>
     <td style="width: 275px" colspan="3">
        &nbsp;<asp:TextBox ID="txtArtBusqueda" AutoPostBack="false" runat="server" 
             CssClass="input" MaxLength="25" Width="203px" Font-Bold="true" 
             Font-Size="10px" Visible="false"></asp:TextBox>
        &nbsp;<asp:Button ID="btnConsultarArt" name="btnConsultarArt" runat="server" CssClass="btnAzul" text="..." style="WIDTH:20px; HEIGHT:20px" Visible="false"/> 
     </td>
    </tr>

    <tr><td style="width:800px; height:10px" colspan="7"></td></tr>

    <tr>
     <td style="width:120px; height:20px" class="Etiqueta" align="center"><asp:Label ID="lblInfoMasCos" runat="server" Text="Mas Costoso"></asp:Label></td>
     <td style="width:120px; height:20px" class="Etiqueta" align="center"><asp:Label ID="lblInfoMenosCos" runat="server" Text="Menos Costoso"></asp:Label></td>
     <td style="width:120px; height:20px" class="Etiqueta" align="center"><asp:Label ID="lblInfoMasRot" runat="server" Text="Mas Rotacion"></asp:Label></td>
     <td style="width:120px; height:20px" class="Etiqueta" align="center"><asp:Label ID="lblInfoMenosRot" runat="server" Text="Menos Rotacion"></asp:Label></td>
     <td style="width:120px; height:20px" class="Etiqueta" align="center"><asp:Label ID="lblInfoSinRot" runat="server" Text="Sin Rotacion"></asp:Label></td>
     <td style="width:120px; height:20px" class="Etiqueta" align="center"><asp:Label ID="lblInfoTotal" runat="server" Text="Total"></asp:Label></td>
     <td style="width:80px; height:20px"></td>
    </tr>

    <tr>
     <td style="width:120px; height:20px" class="Etiqueta"><asp:TextBox ID="txtMasCostoso" Text="" runat="server" width="120px" CssClass="inputDerecha" Font-Size="9px"></asp:TextBox></td>
     <td style="width:120px; height:20px" class="Etiqueta"><asp:TextBox ID="txtMenosCostoso" Text="" runat="server" width="120px" CssClass="inputDerecha" Font-Size="9px"></asp:TextBox></td>
     <td style="width:120px; height:20px" class="Etiqueta"><asp:TextBox ID="txtMasRotacion" Text="" runat="server" width="120px" CssClass="inputDerecha" Font-Size="9px"></asp:TextBox></td>
     <td style="width:120px; height:20px" class="Etiqueta"><asp:TextBox ID="txtMenosRotacion" Text="" runat="server" width="120px" CssClass="inputDerecha" Font-Size="9px"></asp:TextBox></td>
     <td style="width:120px; height:20px" class="Etiqueta"><asp:TextBox ID="txtSinRotacion" Text="" runat="server" width="120px" CssClass="inputDerecha" Font-Size="9px"></asp:TextBox></td>
     <td style="width:120px; height:20px" class="Etiqueta"><asp:TextBox ID="txtTotal" Text="" runat="server" width="120px" CssClass="inputDerecha" Font-Size="9px" ReadOnly= "true"></asp:TextBox></td>
     <td style="width:80px; height:20px" align="center"></td>     
    </tr>
   </table>

   <%--Mensaje/#Reg--%>
   <table style="width:800px" cellspacing="0" cellpadding="0">
	<tr>
	 <td style="width:800px" align="left"><asp:Label id="lblMensaje" runat="server" CssClass="mensaje"></asp:Label></td>
    </tr>
    <tr>
	 <td style="WIDTH: 800px" align="right">
      <asp:Label ID="lblNumReq" runat="server" Font-Size="9px" Font-Bold="true" Text=""></asp:Label>
     </td>
    </tr>
   </table>

  <%-- <asp:UpdatePanel ID="panContenido" runat="server">
    <ContentTemplate>--%>     <%--grilla--%>
     <table style="width:800px" cellspacing="0" cellpadding="0">
      <tr><td style="width:800px; height:10px" colspan="7"></td></tr>
	  <tr>
	   <td>
        <asp:datagrid id="dtgInventario" runat="server" Width="800px" AutoGenerateColumns="False">
         <FooterStyle CssClass="GridFooter"></FooterStyle>
	     <AlternatingItemStyle CssClass="GridAlternateItem"></AlternatingItemStyle>
	     <ItemStyle CssClass="GridItem"></ItemStyle>
	     <HeaderStyle CssClass="GridHeader"></HeaderStyle>
	     
         <Columns>
		
          <asp:TemplateColumn>
		   <HeaderStyle Width="20px"></HeaderStyle>
		   <%--<ItemTemplate>
            <asp:ImageButton id="ibtConsultar" runat="server" ImageUrl="../../intranet/Imagenes/buscar.gif" Width="16px" Height="16px"></asp:ImageButton>
		   </ItemTemplate>--%>
		  </asp:TemplateColumn>
        
		  <asp:BoundColumn DataField="Des_TipoItem" HeaderText="Tipo">
           <HeaderStyle HorizontalAlign="Center" Width="150px" VerticalAlign="Middle" Font-Bold="false"></HeaderStyle>
		   <ItemStyle Font-Size="10px" HorizontalAlign="left" VerticalAlign="Middle"></ItemStyle>
           <HeaderStyle Width="150px" />
          </asp:BoundColumn>
                                            
          <asp:BoundColumn DataField="vch_CodigoItem" HeaderText="Código Item">
           <HeaderStyle HorizontalAlign="Center" Width="100px" VerticalAlign="Middle" Font-Bold="false"></HeaderStyle>
		   <ItemStyle Font-Size="10px" HorizontalAlign="left" VerticalAlign="Middle"></ItemStyle>
           <HeaderStyle Width="100px" />
          </asp:BoundColumn>

         <asp:BoundColumn DataField="Cod_Almacen" HeaderText="Alm.">
          <HeaderStyle HorizontalAlign="Center" Width="50px" VerticalAlign="Middle" Font-Bold="false"></HeaderStyle>
		  <ItemStyle Font-Size="10px" HorizontalAlign="center" VerticalAlign="Middle"></ItemStyle>
          <HeaderStyle Width="50px" />
         </asp:BoundColumn>

         <asp:BoundColumn DataField="Des_Item" HeaderText="Descripción">
          <HeaderStyle HorizontalAlign="Center" Width="250px" VerticalAlign="Middle" Font-Bold="false"></HeaderStyle>
		  <ItemStyle Font-Size="10px" HorizontalAlign="left" VerticalAlign="Middle"></ItemStyle>
          <HeaderStyle Width="250px" />
         </asp:BoundColumn>
        
          <asp:BoundColumn DataField="Unidad_Medida" HeaderText="U.M.">
           <HeaderStyle HorizontalAlign="Center" Width="50px" VerticalAlign="Middle" Font-Bold="false"></HeaderStyle>
		   <ItemStyle Font-Size="10px" HorizontalAlign="left" VerticalAlign="Middle"></ItemStyle>
           <HeaderStyle Width="50px" />
          </asp:BoundColumn>
          
          <asp:BoundColumn DataField="Cod_Ubicacion" HeaderText="Ubicacion">
           <HeaderStyle HorizontalAlign="Center" Width="50px" VerticalAlign="Middle" Font-Bold="false"></HeaderStyle>
		   <ItemStyle Font-Size="10px" HorizontalAlign="left" VerticalAlign="Middle"></ItemStyle>
           <HeaderStyle Width="50px" />
          </asp:BoundColumn>
		
          <asp:BoundColumn DataField="num_Stock" HeaderText="Stock" DataFormatString="{0:N2}">
           <HeaderStyle HorizontalAlign="Center" Width="50px" VerticalAlign="Middle" Font-Bold="false"></HeaderStyle>
		   <ItemStyle Font-Size="10px" HorizontalAlign="right" VerticalAlign="Middle"></ItemStyle>
           <HeaderStyle Width="50px" />
          </asp:BoundColumn>

		  <asp:TemplateColumn HeaderText="Cant. Inv.">
           <ItemTemplate>
            <asp:TextBox ID="txtcantinv" runat="server" CssClass="inputDerecha" Width="80px" Font-Size="10px" Text='<%#Container.DataItem("num_CanInventario")%>'></asp:TextBox>
           </ItemTemplate>
		   <HeaderStyle HorizontalAlign="Center" Width="80px" Font-Bold="false" Wrap="False"></HeaderStyle>
		   <ItemStyle HorizontalAlign="Center" Font-Bold="False"></ItemStyle>
           <HeaderStyle Width="80px" />
          </asp:TemplateColumn>
	     </Columns>
	    </asp:datagrid>
       </td>
	  </tr>
      <tr>
        <td>&nbsp;</td>
      </tr>
      <tr>
        <td>
            &nbsp;
            &nbsp;
            <asp:GridView ID="grdInventarioTelas" runat="server" 
                AutoGenerateColumns="False" BackColor="White" BorderColor="#DEDFDE" 
                BorderStyle="None" BorderWidth="1px" CellPadding="4" CssClass="serh-grid" 
                ForeColor="Black" GridLines="Vertical" 
                OnRowCreated="grdInventarioTelas_RowCreated" 
                OnRowDataBound="grdInventarioTelas_RowDataBound" TabIndex="1" Width="75%">
                <Columns>
                    <asp:BoundField DataField="De_Almacen" HeaderText="Almacen" 
                        ItemStyle-Width="230px" SortExpression="De_Almacen"/>
                    <asp:BoundField DataField="vch_CodigoUbicacion" HeaderText="Ubicacion" 
                        ItemStyle-HorizontalAlign="Center" ItemStyle-Width="100px" 
                        SortExpression="vch_CodigoUbicacion" />
                    <asp:BoundField DataField="num_StockInventario" HeaderText="Stock Sistema" 
                        ItemStyle-HorizontalAlign="Right" ItemStyle-Width="80px" 
                        SortExpression="num_StockInventario" DataFormatString="{0:N2}"/>
                    <asp:BoundField DataField="num_CanInventario" HeaderText="Stock Inventario" 
                        ItemStyle-HorizontalAlign="Right" ItemStyle-Width="80px" 
                        SortExpression="num_CanInventario" DataFormatString="{0:N2}"/>
                    <%--<asp:BoundField DataField="Estado" HeaderText="Estado" 
                        ItemStyle-HorizontalAlign="Center" ItemStyle-Width="50px" 
                        SortExpression="Estado" />--%>
                    <asp:TemplateField HeaderText="Estado">
                        <ItemTemplate>
                            <asp:Label ID="lblEstadoUbic" runat="server" Text='<%#Container.DataItem("Estado")%>' Width="85px"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
                <FooterStyle BackColor="#CCCC99" />
                <SelectedRowStyle CssClass="grid-sltrow" />
                <HeaderStyle BackColor="#6B696B" BorderColor="Black" BorderStyle="Solid" 
                    BorderWidth="1px" Font-Bold="True" ForeColor="White" />
            </asp:GridView>
        </td>
      </tr>
      <tr>
        <td>
            &nbsp;
            &nbsp;
            <asp:GridView ID="grdInventarioUbic" runat="server" 
                AutoGenerateColumns="False" BackColor="White" BorderColor="#DEDFDE" 
                BorderStyle="None" BorderWidth="1px" CellPadding="4" CssClass="serh-grid" 
                ForeColor="Black" GridLines="Vertical" 
                OnRowCreated="grdInventarioUbic_RowCreated" 
                OnRowDataBound="grdInventarioUbic_RowDataBound" TabIndex="1" Width="100%">
                <Columns>
                    <asp:BoundField DataField="De_Almacen" HeaderText="Almacen" 
                        ItemStyle-Width="250px" SortExpression="De_Almacen"/>
                    <asp:BoundField DataField="NomArticulo" HeaderText="Articulo" 
                        ItemStyle-HorizontalAlign="Center" ItemStyle-Width="600px" 
                        SortExpression="NomArticulo" />
                    <asp:BoundField DataField="num_StockInventario" HeaderText="Stock Sistema" 
                        ItemStyle-HorizontalAlign="Right" ItemStyle-Width="80px" 
                        SortExpression="num_StockInventario" DataFormatString="{0:N2}"/>
                    <asp:BoundField DataField="num_CanInventario" HeaderText="Stock Inventario" 
                        ItemStyle-HorizontalAlign="Right" ItemStyle-Width="80px" 
                        SortExpression="num_CanInventario" DataFormatString="{0:N2}"/>
                    <%--<asp:BoundField DataField="Estado" HeaderText="Estado" 
                        ItemStyle-HorizontalAlign="Center" ItemStyle-Width="50px" 
                        SortExpression="Estado" />--%>
                    <asp:TemplateField HeaderText="Estado">
                        <ItemTemplate>
                            <asp:Label ID="lblEstadoUbic" runat="server" Text='<%#Container.DataItem("Estado")%>' Width="85px"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
                <FooterStyle BackColor="#CCCC99" />
                <SelectedRowStyle CssClass="grid-sltrow" />
                <HeaderStyle BackColor="#6B696B" BorderColor="Black" BorderStyle="Solid" 
                    BorderWidth="1px" Font-Bold="True" ForeColor="White" />
            </asp:GridView>
        </td>
      </tr>
     </table>

   <%--<ItemTemplate>
            <asp:ImageButton id="ibtConsultar" runat="server" ImageUrl="../../intranet/Imagenes/buscar.gif" Width="16px" Height="16px"></asp:ImageButton>
		   </ItemTemplate>--%>
 </center>
</form>
</body>
</html>
