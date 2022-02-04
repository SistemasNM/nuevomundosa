<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="frm_Listar_OS_Conformidades.aspx.vb" Inherits="intranet_logi.frm_Listar_OS_Conformidades" %>
<%@ Register Assembly="Infragistics35.Web.v10.1, Version=10.1.20101.1011, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" Namespace="Infragistics.Web.UI.EditorControls" TagPrefix="ig" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../css/NM0001.css" rel="stylesheet" type="text/css" />
    <link href="../css/Styles_Controles.css" rel="stylesheet" type="text/css" />
    <link href="../css/Styles_Paginas.css" rel="stylesheet" type="text/css" />
    <script language="JavaScript" src="../../intranet/js/jsDesTabla.js" type="text/jscript" ></script>    
    <%--<script language="JavaScript" src="../../intranet/js/jsGeneral.js" type="text/jscript" ></script>  --%>
  
    <%--<script language="javascript" src="../js/jsDesTabla.js" type="text/jscript"></script>--%>
    <script language="javascript" src="../js/jsGeneral.js" type="text/jscript"></script>
    <script language="javascript" type="text/javascript">
        g_RutaDirecto = 1;

        function btnSeleccion_Onclick_Antiguo(strCodigo) {
            //window.location.href = "frmFichaProveedor.aspx?strNumeroOrdenServicio=" + strCodigo;
            var retorno = window.showModalDialog("frmFichaProveedor.aspx?strNumeroOrdenServicio=" + strCodigo, "", "dialogHeight:600px;dialogWidth:900px;center:yes;help:no;");
            if (retorno == "") {
                return false;
            }
            return false;
        }

        function btnSeleccion_Onclick(strCodigo,strItem,strFlg) {
            //window.location.href = "frmFichaProveedor.aspx?strNumeroOrdenServicio=" + strCodigo;
            if (strFlg == "1") {
                var WinSettings = "status:no;center:yes;resizable:no;dialogHeight:600px;dialogWidth:900px;scroll:no";
                var returnValue = window.showModalDialog("ConformidadServicio.aspx?strNumeroOrdenServicio=" + strCodigo + "&strItem=" + strItem, null, WinSettings);
            } else {
                alert("Tiene conformidades pendientes por realizar para la \n OS " + strCodigo);
            }
        }

        function btnReporte_Onclick(strUrl) {
            var intwidth = screen.width;
            var intheight = screen.height;
            window.open(strUrl, "", "top=0; left=0; width=" + intwidth + "; height=" + intheight + "; resizable=1;");
        }

        function FormatearBusqDoc(pTexto) {
            if (pTexto == 2)//numero
            {
                var lnume = '00000000000' + document.getElementById('txtNumOrden').value;
                lnume = lnume.substring(lnume.length, lnume.length - 10);
                if (lnume == '0000000000') {
                    document.getElementById('txtNumOrden').value = '';
                } else {
                    document.getElementById('txtNumOrden').value = lnume;
                } //end if

            } //end if
            return false;
        } //end function

        function AnularEnter(e) {
            tecla = (document.all) ? e.keyCode : e.which;
            return (tecla != 13);
        }

        function popUp(strUrl, strBol, strCodigo) {

            if (strBol == "False") {
                alert("La OS " + strCodigo + " no tiene una conformidad");
            } else {
                var intwidth = screen.width;
                var intheight = screen.height;
                window.open(strUrl, "", "top=0; left=0; width=" + intwidth + "; height=" + intheight + "; resizable=1;");
            }
            
        }
    </script>
</head>
<body>
    <form id="frmListadoOS"  method="post" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        <center>
        <asp:Panel ID="pnlcabecera" runat="server" HorizontalAlign="Center">
            <%--Cabecera--%>
            <table style="width:800px;" border="0" cellpadding="0" cellspacing="0" >
                <tr>
                    <td class="Cabecera" style="width:800px; height:30px" align="left" valign="middle">&nbsp;LISTADO DE ORDENES DE SERVICIO - CONFORMIDAD</td>
                </tr>
                <tr>
                    <td>&nbsp;</td>
                </tr>
            </table>
        </asp:Panel>

    <!-- **************  Inicio de Contenido ****************** -->            
        <asp:UpdatePanel ID="panContenido" runat="server">
            <ContentTemplate> 
                <%--Filtros--%>
                <asp:Panel ID="pnlFiltros" runat="server" DefaultButton="btnBuscar">                    
                    <table style="width:800px" border="0" cellpadding="0" cellspacing="0">
                        <tr style="height: 25px">
                            <td style="width:150px" class="Etiqueta" align="left">&nbsp;Estado O/S:</td>
                            <td style="width:150px" class="Etiqueta" align="left">
                                <asp:DropDownList ID="cmbOpcion" runat="server" width="120px" CssClass="cbo" Font-Size="10px">
                                <asp:ListItem Value="00">Seleccionar estado</asp:ListItem>
                                <asp:ListItem Value="PEN">O/S por concluir</asp:ListItem>
                                <asp:ListItem Value="CON">O/S concluidas</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                            <td class="Etiqueta"  align="right" colspan="4">&nbsp;</td>
                        </tr>
                        <tr style="height: 25px">
                            <td class="Etiqueta" style="width:150px" align="left">&nbsp;Proveedor:</td>
                            <td style="width:150px" class="Etiqueta" align="left">
                                <asp:textbox id="txtCodigoProveedor" runat="server" width="90px" CssClass="input"></asp:textbox>&nbsp;
                                <input id="btnProveedores" style="width:20px; height:20px" onclick="fdesListaProveedor(txtCodigoProveedor,txtNombreProveedor);" 
                                    type="button" size="20" value="..." name="btnProveedores" class="Boton"/>
                            </td>
                            <td class="Etiqueta"  align="left" colspan="4">
                                <asp:textbox id="txtNombreProveedor" runat="server" width="540px" Height="20px" CssClass="txtReadOnly"></asp:textbox>
                            </td>
                        </tr>
                        <tr style="height: 25px">
                            <td class="Etiqueta" style="width:150px" align="left">&nbsp;Fec. Inicio:</td>
                            <td style="width:150px" class="Etiqueta" align="left">
                                <ig:WebDatePicker ID="wdpFecIni" runat="server" width="100px" Font-Size="10px"></ig:WebDatePicker>
                            </td>
                            <td class="Etiqueta" style="width:70px" align="left">&nbsp;</td>
                            <td class="Etiqueta" style="width:70px" align="left">Fec. Fin:</td>
                            <td align="left" class="Etiqueta" style="width:150px">
                                <ig:WebDatePicker ID="wdpFecFin" runat="server" width="100px" Font-Size="10px"></ig:WebDatePicker>
                            </td>
                            <td align="center" class="Etiqueta" style="width:480px" rowspan="2">
                                <asp:Button ID="btnBuscar" runat="server" CssClass="Boton" Text="Buscar" />
                            </td>
                        </tr>
                        <tr style="height: 25px">
                            <td align="left" class="Etiqueta" style="width:150px">&nbsp;Num. O/S:</td>
                            <td align="left" class="Etiqueta" colspan="2" style="width:220px">
                                <asp:TextBox ID="txtSerie" runat="server" CssClass="inputDisabled" 
                                    Font-Size="9px" Width="30px"></asp:TextBox>&nbsp;-&nbsp;
                                <asp:TextBox ID="txtNumOrden" runat="server" CssClass="input" Font-Size="9px" 
                                    MaxLength="10" Width="80px"></asp:TextBox>
                            </td>
                            <td class="Etiqueta" style="width:70px" align="left"></td>
                            <td align="left" class="Etiqueta" style="width:150px">&nbsp;</td>
                        </tr>
                    </table>
                </asp:Panel>
                <%--imagen de espera--%>
                <asp:UpdateProgress ID="up_Load" DisplayAfter="300" runat="server" >   
                    <ProgressTemplate>
                        <div id="divLoad" class="CssLoadNormal" 
                            style="width:800px; height:40px; vertical-align: middle;"><br />
                            <img src="../images/Loading.gif" style="vertical-align:middle; height:20px; width:20px" alt="" />Procesando...
                        </div>                        
                    </ProgressTemplate> 
                </asp:UpdateProgress>
                <%--Mensaje--%>
                <asp:Panel ID="pnlMensaje" runat="server">
                    <table cellspacing="0" cellpadding="0" border="0" width="800px">
                        <tr>
                            <td align="left">
                                <asp:Label ID="lblMsg" runat="server" CssClass="error"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td align="right">
                                <asp:Label ID="lblContador" runat="server" CssClass="contador"></asp:Label>
                            </td>                           
                        </tr>
                    </table>
                </asp:Panel>
                <%--Listado--%>
                <asp:Panel ID="pnlListado" runat="server">
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
                       <asp:ImageButton id="btnReporte" runat="server" Width="20px" Height="20px" ImageUrl="~/images/buscar.gif" ToolTip="Ver Orden de Servicio" />
                      </ItemTemplate>
                     </asp:TemplateColumn>
         
                     <asp:BoundColumn DataField="vch_NumOrdenServicio" HeaderText="Orden Servicio">
                      <HeaderStyle HorizontalAlign="Center" Width="120px" VerticalAlign="Middle" Font-Bold="true"></HeaderStyle>
                      <ItemStyle Font-Size="9px" HorizontalAlign="left" VerticalAlign="Middle"></ItemStyle>
                     </asp:BoundColumn>

		             <asp:BoundColumn DataField="vch_FechaEmision" HeaderText="Fec. Solicitud">
                      <HeaderStyle HorizontalAlign="Center" Width="70px" VerticalAlign="Middle" Font-Bold="true"></HeaderStyle>
                      <ItemStyle Font-Size="9px" HorizontalAlign="left" VerticalAlign="Middle"></ItemStyle>
                     </asp:BoundColumn>

                     <asp:BoundColumn DataField="vch_CodigoProveedor" HeaderText="R.U.C">
                      <HeaderStyle HorizontalAlign="Center" Width="80px" VerticalAlign="Middle" Font-Bold="true"></HeaderStyle>
                      <ItemStyle Font-Size="9px" HorizontalAlign="left" VerticalAlign="Middle"></ItemStyle>
                     </asp:BoundColumn>
         
                     <asp:BoundColumn DataField="vch_NombreProveedor" HeaderText="Proveedor">
                      <HeaderStyle HorizontalAlign="Center" Width="250px" VerticalAlign="Middle" Font-Bold="true"></HeaderStyle>
                      <ItemStyle Font-Size="9px" HorizontalAlign="left" VerticalAlign="Middle"></ItemStyle>
                     </asp:BoundColumn>
         
                     <%--<asp:BoundColumn DataField="vch_EstadoOrdenServ" HeaderText="Estado O/S">
                      <HeaderStyle HorizontalAlign="Center" Width="60px" VerticalAlign="Middle" Font-Bold="true"></HeaderStyle>
                      <ItemStyle Font-Size="9px" HorizontalAlign="center" VerticalAlign="Middle"></ItemStyle>
                     </asp:BoundColumn>--%>

                     <asp:BoundColumn DataField="vch_UsuarioSoli" HeaderText="Solicitante">
                      <HeaderStyle HorizontalAlign="Center" Width="60px" VerticalAlign="Middle" Font-Bold="true" ></HeaderStyle>
                      <ItemStyle Font-Size="9px" HorizontalAlign="center" VerticalAlign="Middle"></ItemStyle>
                     </asp:BoundColumn>

                     <asp:BoundColumn DataField="vch_IdConformidad" HeaderText="Conformidad">
                      <HeaderStyle HorizontalAlign="Center" Width="70px" VerticalAlign="Middle" Font-Bold="true" ></HeaderStyle>
                      <ItemStyle Font-Size="9px" HorizontalAlign="center" VerticalAlign="Middle"></ItemStyle>
                     </asp:BoundColumn>
		 
                     <asp:BoundColumn DataField="vch_EstadoConform" HeaderText="Estado">
                      <HeaderStyle HorizontalAlign="Center" Width="40px" VerticalAlign="Middle" Font-Bold="true" ></HeaderStyle>
                      <ItemStyle Font-Size="9px" HorizontalAlign="center" VerticalAlign="Middle"></ItemStyle>
                     </asp:BoundColumn>
		               
                    <asp:BoundColumn DataField="vch_Item" Visible="true" HeaderText="Item" >
                         <HeaderStyle HorizontalAlign="Center" Width="30px" VerticalAlign="Middle" Font-Bold="true" ></HeaderStyle>
                        <ItemStyle Font-Size="9px" HorizontalAlign="center" VerticalAlign="Middle" Width="20px"></ItemStyle>
                    </asp:BoundColumn>
                    
                    <asp:BoundColumn DataField="flg" Visible="false" HeaderText="Flg">
                    </asp:BoundColumn>

                    </Columns>
	               </asp:datagrid>
                </asp:Panel>
            </ContentTemplate> 
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="btnBuscar" EventName="Click" />
            </Triggers>
        </asp:UpdatePanel>                       
        </center>
    </form>
</body>
</html>
