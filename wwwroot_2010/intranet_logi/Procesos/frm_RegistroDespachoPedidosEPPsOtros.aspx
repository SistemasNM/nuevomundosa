<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="frm_RegistroDespachoPedidosEPPsOtros.aspx.vb"
    Inherits="intranet_logi.frm_RegistroDespachoPedidosEPPsOtros" %>

<%--<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">--%>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>DESPACHO DE PEDIDOS EPPs y Otros</title>
    <base target="_self" />
    <meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1" />
    <meta name="CODE_LANGUAGE" content="Visual Basic .NET 7.1" />
    <meta name="vs_defaultClientScript" content="JavaScript" />
    <meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5" />
    <link href="../../intranet/Estilos/Styles_Paginas.css" type="text/css" rel="Stylesheet" />
    <link href="../css/NM0001.css" type="text/css" rel="stylesheet" />
    <link href="../../intranet/Estilos/Styles_Controles.css" type="text/css" rel="Stylesheet" />
    <link href="../../intranet/Estilos/EstilosWeb.css" type="text/css" rel="Stylesheet" />
    <link href="../css/sytle.css" type="text/css" rel="stylesheet" />
    <script language="javascript" type="text/javascript" src="../../intranet/JS/jsCalendario_N4.js"></script>
    <script language="javascript" type="text/javascript" src="../../intranet/JS/functions.js"></script>
    <script language="javascript" type="text/javascript">

        function VerValesPedido(strNumeroPedido) {
            var dt_time = new Date().getTime();
            var strURL = "frm_ListadoValesPedido.aspx?&strNumeroPedido=" + strNumeroPedido + "&dtTime=" + dt_time;
            var retorno = window.showModalDialog(strURL, "", "dialogheight:400px;dialogwidth:750px;center:yes;help:no;");
        }

        function fnc_Cerrar() {
            window.open('', '_parent', '');
            window.close();
        }

        function fnc_ConfirmarOperacion(strOperacion) {
            var strMensaje = "";

            if (strOperacion == "Despachar") {
                if (document.all('txtCodRecepciona').value == "") {
                    alert("Debe ingresar el codigo del usuario a entregar el articulo");
                    return false;
                }
            }

            switch (strOperacion) {
                case "Culminar":
                    strMensaje = "Está seguro que desea culminar el Vale?";
                    break;
                case "PreDespachar":
                    strMensaje = "Está seguro que desea realizar el Pre-Despacho del Vale?";
                    break;
                case "Despachar":
                    strMensaje = "Está seguro que desea realizar el Despacho del Vale?";
                    break;
                default: return false;
            }

            if (confirm(strMensaje) == true) {
                return true;
            } else { return false; }
        }

        function fnc_BuscarTrabajadores() {
            var tipo = "EMP";
            var dt_time = new Date().getTime();
            var strURL = "../../intranet/Buscadores/PLA_0001.aspx?strTipo=" + tipo + "&dtTime=" + dt_time;
            var retorno = window.showModalDialog(strURL, "", "dialogheight:450px;dialogwidth:450px;center:yes;help:no;");
            if (retorno != "" && retorno != ":") {
                var datos = retorno.split(":");
                var codigo = datos[0];
                var nombre = datos[1];
                document.all('txtCodRecepciona').value = codigo;
                document.all('lblDesRecepciona').value = nombre;
                //document.location.href = "frm_RegistroDespachoPedidosEPPsOtros.aspx?id=" + codigo + "&strNumeroPedido=" + document.all('txtNumeroPedido').value;
                document.all('txtCodRecepciona').focus();
                frm_RegistroDespachoPedidosEPPsOtros.submit();  
            }

//            document.all('txtCodRecepciona').value = "16658";
//            document.all('lblDesRecepciona').value = "david gamarra paredes";
//            //document.location.href = "frm_RegistroDespachoPedidosEPPsOtros.aspx?id=" + document.all('txtCodRecepciona').value;
//            frm_RegistroDespachoPedidosEPPsOtros.submit();  
        }
    </script>
</head>
<body>
    <form id="frm_RegistroDespachoPedidosEPPsOtros" method="post" autocomplete="off"
    runat="server">
    <center>
     <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager> 
        <%--titulo--%>
        <table id="tblCabeceraPagina" border="0" cellspacing="0" cellpadding="0" width="900px">
            <tr>
                <td style="width: 900px; height: 30px" class="Cabecera">
                    DESPACHO DE VALES DE ALMACEN EPPS Y OTROS
                </td>
            </tr>
        </table>
        <asp:UpdatePanel ID="panContenido" runat="server" UpdateMode="Always">
            <ContentTemplate>
                <table style="width: 900px" border="0" cellspacing="0" cellpadding="0">
                    <tr>
                        <td style="width: 120px" class="Descripcion">
                            &nbsp;Situacion:
                        </td>
                        <td style="width: 380px" valign="middle" align="left" colspan="2">
                            <asp:ImageButton ID="btnImprimirEtiqueta" runat="server" ImageUrl="../../intranet/Imagenes/im_printer.gif"
                                ToolTip="Imprimir Etiqueta Pre-DEspacho"></asp:ImageButton>&nbsp;
                            <asp:Label ID="txtEstado" runat="server" Font-Bold="True" Width="357px" Font-Size="8px"></asp:Label>
                        </td>
                        <td style="width: 300px" class="Descripcion" valign="middle" align="center" rowspan="9">
                            <table style="width: 300px">
                                <tr>
                                    <td class="Descripcion" style="width: 100px">
                                        &nbsp;Solicitante:
                                    </td>
                                    <td align="left" style="width: 200px" valign="middle">
                                        <asp:Label ID="txtCodSolicitante" runat="server" Font-Bold="True" Font-Size="8px"
                                            Width="30px"></asp:Label>
                                        <asp:Label ID="txtDesSolicitante" runat="server" Font-Size="8px" Width="170px"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="Descripcion" style="width: 100px">
                                        &nbsp;Area:
                                    </td>
                                    <td align="left" style="width: 200px" valign="middle">
                                        <asp:Label ID="txtDesArea" runat="server" Font-Size="8px" Width="200px"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="Descripcion" style="width: 100px">
                                        &nbsp;Seccion:
                                    </td>
                                    <td align="left" style="width: 200px">
                                        <asp:Label ID="txtDesSeccion" runat="server" Font-Size="8px" Width="200px"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="Descripcion" style="width: 100px">
                                        &nbsp;Cargo:
                                    </td>
                                    <td align="left" style="width: 200px">
                                        <asp:Label ID="txtDesCargo" runat="server" Font-Size="8px" Width="200px"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 300px" colspan="2" valign="top" align="center">
                                        <asp:Image ID="imgFoto" runat="server" Width="110px" Height="140px" CssClass="Descripcion" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 120px" class="Descripcion">
                            &nbsp;Num. Pedido:
                        </td>
                        <td style="width: 380px" valign="middle" align="left" colspan="2">
                            <%--&nbsp;<asp:label id="txtSeriePedido" runat="server" Font-Bold="True" width="30px" Font-Size="8px"></asp:label>--%>
                            &nbsp;<asp:Label ID="txtNumeroPedido" runat="server" Width="100px" Font-Size="8px"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 120px" class="Descripcion">
                            &nbsp;Centro Costos:
                        </td>
                        <td style="width: 380px" valign="middle" align="left" colspan="2">
                            <%--&nbsp;<asp:label id="txtCodCentroCostos" runat="server" Font-Bold="True" width="50px" Font-Size="8px"></asp:label>--%>
                            &nbsp;<asp:Label ID="txtDesCentroCostos" runat="server" Width="250px" Font-Size="8px"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 120px" class="Descripcion">
                            &nbsp;Almacen:
                        </td>
                        <td style="width: 380px" valign="middle" align="left" colspan="2">
                            <%--&nbsp;<asp:label id="txtCodAlmacen" runat="server" Font-Bold="True" width="30px" Font-Size="8px"></asp:label>--%>
                            &nbsp;<asp:Label ID="txtDesAlmacen" runat="server" Width="250px" Font-Size="8px"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 120px;" class="Descripcion">
                            &nbsp;Fec Pedido:
                        </td>
                        <td style="width: 380px;" valign="middle" align="left" colspan="2">
                            &nbsp;<asp:Label ID="txtFechaPedido" runat="server" Width="120px" Font-Size="8px"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 120px" class="Descripcion">
                            &nbsp;Fec. Aprobacion:
                        </td>
                        <td style="width: 380px" valign="middle" align="left" colspan="2">
                            &nbsp;<asp:Label ID="txtFechaAprobacion" runat="server" Width="120px" Font-Size="8px"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 120px" class="Descripcion">
                            &nbsp;Fec. Atencion:
                        </td>
                        <td style="width: 380px" colspan="2">
                            &nbsp;<asp:TextBox ID="txtFechaDespacho" runat="server" Width="80px" CssClass="txtDeshabilitado2"></asp:TextBox>
                            &nbsp;<img onclick="popUpCalendar(this, frmRegistroDespachoPedidos.txtFechaDespacho, 'dd/mm/yyyy')"
                                border="0" alt="Seleccionar fecha de Despacho" src="../../intranet/Imagenes/Calendario.gif"
                                width="16px" height="16px" />
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 120px" class="Descripcion">
                            &nbsp;Entregado a:
                        </td>
                        <td style="width: 380px" valign="middle" align="left" colspan="2">
                            &nbsp;<asp:TextBox ID="txtCodRecepciona" runat="server" CssClass="txtHabilitado"
                                Width="80px"></asp:TextBox>
                                <%--&nbsp;<input style="width: 20px; height: 20px" id="btnCargarUsuario" class="boton"
                                onclick="" value="..." type="button" name="CargarUsuario" />--%>
                                &nbsp;<asp:Button id="carga" runat="server" type="button" class="boton" Text="..." Width="20px"/>
                            <%--&nbsp;<input style="width: 20px; height: 20px" id="btnSolicitante" class="boton"
                                onclick="javascript:fnc_BuscarTrabajadores();" value="..." type="button" name="btnSolicitante" />--%>
                           <%-- &nbsp;<asp:ImageButton ID="imgBuscarCliente" runat="server" ImageUrl="../../intranet/Imagenes/Buscar.gif"
                                ToolTip="Consultar cliente"></asp:ImageButton>--%>
                            &nbsp;<asp:Label ID="lblDesRecepciona" runat="server" Font-Size="8px" Width="200px"></asp:Label>
                            <asp:CheckBox id="chkbox" runat="server" Text="Imprimir" Checked="true"/>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 120px" class="Descripcion">
                            &nbsp;Obervaciones:
                        </td>
                        <td style="width: 380px" colspan="2" valign="top">
                            &nbsp;<asp:TextBox ID="txtObservaciones" runat="server" Width="380px" CssClass="txtAreaDeshabilitado"
                                TextMode="MultiLine"></asp:TextBox>
                        </td>
                    </tr>
                </table>
                <table style="width: 900px">
                    <tr>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 900px">
                            <asp:Label ID="lblError" Text="" runat="server" CssClass="mensaje"></asp:Label>
                        </td>
                    </tr>
                </table>
                <table style="width: 900px">
                    <tr>
                        <td style="width: 900px" align="right">
                            <asp:Label ID="lblItems" Text="Numero de Items" runat="server" CssClass="contador">Numero de Items:</asp:Label>
                        </td>
                    </tr>
                </table>
                <%--grilla--%>
                <table style="width: 900px" border="0" cellspacing="0" cellpadding="0">
                    <tr>
                        <td style="width: 900px" class="Descripcion" align="left">
                            <asp:DataGrid ID="dgDetalle" runat="server" Width="900px" AutoGenerateColumns="False">
                                <AlternatingItemStyle CssClass="GridAltItem"></AlternatingItemStyle>
                                <ItemStyle CssClass="GridItem"></ItemStyle>
                                <HeaderStyle CssClass="GridHeader"></HeaderStyle>
                                <Columns>
                                    <asp:TemplateColumn HeaderText="Sec.">
                                        <HeaderStyle HorizontalAlign="Center" Width="20px" VerticalAlign="Middle"></HeaderStyle>
                                        <ItemStyle Font-Size="8px" HorizontalAlign="left" Width="20px"></ItemStyle>
                                        <ItemTemplate>
                                            <asp:Label ID="lblSecuencia" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.nu_secu") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateColumn>
                                    <asp:TemplateColumn HeaderText="Codigo">
                                        <HeaderStyle HorizontalAlign="Center" Width="100px"></HeaderStyle>
                                        <ItemStyle Font-Size="8px" HorizontalAlign="left" Width="100px"></ItemStyle>
                                        <ItemTemplate>
                                            <asp:Label ID="lblCodigo" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.CO_ITEM") %>'></asp:Label>
                                            <asp:Label ID="lblCentroCostos" runat="server" Visible="False" Text='<%# DataBinder.Eval(Container, "DataItem.CentroCostos") %>'></asp:Label>
                                            <asp:Label ID="lblCtagasto" runat="server" Visible="False" Text='<%# DataBinder.Eval(Container, "DataItem.CtaGasto") %>'></asp:Label>
                                            <asp:Label ID="lblActivoFijo" runat="server" Visible="false" Text='<%# DataBinder.Eval(Container, "DataItem.ActivoFijo") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateColumn>
                                    <asp:TemplateColumn HeaderText="Descripción">
                                        <HeaderStyle HorizontalAlign="Center" Width="250px" VerticalAlign="Middle"></HeaderStyle>
                                        <ItemStyle Font-Size="8px" HorizontalAlign="left" Width="250px"></ItemStyle>
                                        <ItemTemplate>
                                            <asp:Label ID="lblDescripcion" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.de_item") %>'>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                            </asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateColumn>
                                    <asp:TemplateColumn HeaderText="Ubi.">
                                        <HeaderStyle HorizontalAlign="Center" Width="50px" VerticalAlign="Middle"></HeaderStyle>
                                        <ItemStyle Font-Size="8px" HorizontalAlign="left" Width="50px"></ItemStyle>
                                        <ItemTemplate>
                                            <asp:Label ID="lblUbicacion" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Ubicacion") %>'>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                            </asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateColumn>
                                    <asp:TemplateColumn HeaderText="U.M.">
                                        <HeaderStyle HorizontalAlign="Center" Width="30px"></HeaderStyle>
                                        <ItemStyle Font-Size="8px" HorizontalAlign="center" Width="30px"></ItemStyle>
                                        <ItemTemplate>
                                            <asp:Label ID="lblUnidaMedida" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.CO_UNME") %>'>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                            </asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateColumn>
                                    <asp:TemplateColumn HeaderText="Can. Aprobada">
                                        <HeaderStyle HorizontalAlign="center" Width="50px"></HeaderStyle>
                                        <ItemStyle Font-Size="8px" HorizontalAlign="right" Width="50px"></ItemStyle>
                                        <ItemTemplate>
                                            <asp:Label ID="lblCantidad" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.CA_APRO") %>'>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                            </asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateColumn>
                                    <asp:TemplateColumn HeaderText="Can. Pendiente">
                                        <HeaderStyle HorizontalAlign="center" Width="50px"></HeaderStyle>
                                        <ItemStyle Font-Size="8px" HorizontalAlign="Right" Width="50px"></ItemStyle>
                                        <ItemTemplate>
                                            <asp:Label ID="lblCantidadPendiente" runat="server" Visible="true" Text='<%# DataBinder.Eval(Container, "DataItem.CA_PEND") %>'>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                            </asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateColumn>
                                    <asp:TemplateColumn HeaderText="Can. Atendida">
                                        <HeaderStyle HorizontalAlign="center" Width="50px"></HeaderStyle>
                                        <ItemStyle Font-Size="8px" HorizontalAlign="right" Width="50px"></ItemStyle>
                                        <ItemTemplate>
                                            <asp:Label ID="lblCantidadAtendida" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.CA_ATEN") %>'>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                            </asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateColumn>
                                    <asp:TemplateColumn HeaderText="Can. Reservada">
                                        <HeaderStyle HorizontalAlign="center" Width="50px"></HeaderStyle>
                                        <ItemStyle Font-Size="8px" HorizontalAlign="Right" Width="50px"></ItemStyle>
                                        <ItemTemplate>
                                            <asp:Label ID="lblCantidadReservada" runat="server" Visible="true" Text='<%# DataBinder.Eval(Container, "DataItem.ca_reservada") %>'>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                            </asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateColumn>
                                    <asp:TemplateColumn HeaderText="Stock">
                                        <HeaderStyle HorizontalAlign="Center" Width="50px"></HeaderStyle>
                                        <ItemStyle Font-Size="8px" HorizontalAlign="Right" Width="50px"></ItemStyle>
                                        <ItemTemplate>
                                            <asp:Label ID="lblStock" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Stock") %>'>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                            </asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateColumn>
                                    <asp:TemplateColumn HeaderText="Can. Disponible">
                                        <HeaderStyle HorizontalAlign="Center" Width="50px"></HeaderStyle>
                                        <ItemStyle Font-Size="8px" HorizontalAlign="Right" Width="50px"></ItemStyle>
                                        <ItemTemplate>
                                            <asp:Label ID="lblCantidadDisponible" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.ca_disponible") %>'>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                            </asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateColumn>
                                    <asp:TemplateColumn HeaderText="Situacion">
                                        <HeaderStyle HorizontalAlign="Center" Width="70px"></HeaderStyle>
                                        <ItemStyle Font-Size="8px" HorizontalAlign="Center" Width="70px"></ItemStyle>
                                        <ItemTemplate>
                                            <asp:Label ID="lblsituacion" runat="server" Font-Bold="True"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateColumn>
                                    <asp:TemplateColumn HeaderText="Can. a Despachar">
                                        <HeaderStyle Width="80px" HorizontalAlign="Center"></HeaderStyle>
                                        <ItemStyle HorizontalAlign="Right" Width="80px"></ItemStyle>
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtDespachable" runat="server" Width="80px" CssClass="inputDerecha"
                                                Text='<%# DataBinder.Eval(Container, "DataItem.CA_DESP") %>'></asp:TextBox>
                                            <asp:Label ID="lblTopeDespachable" runat="server" Visible="false" Text='<%# DataBinder.Eval(Container, "DataItem.CA_DESP") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateColumn>
                                    <asp:TemplateColumn HeaderText="Ultimo Despacho">
                                        <HeaderStyle Width="80px" HorizontalAlign="Center"></HeaderStyle>
                                        <ItemStyle HorizontalAlign="Right" Width="80px"></ItemStyle>
                                        <ItemTemplate>
                                            <asp:Label ID="lblFchUltimoDespacho" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.ultimo_despacho") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateColumn>
                                </Columns>
                            </asp:DataGrid>
                        </td>
                    </tr>
                    <tr>
                        <td style="height: 10PX">
                        </td>
                    </tr>
                    <tr align="center">
                        <td align="center" style="width: 900px">
                            <table align="center" cellpadding="0" cellspacing="0" style="width: 900px; text-align: center;">
                                <tr>
                                    <td style="text-align: left">
                                        <asp:Button ID="btnVales" runat="server" Width="120px" ToolTip="Ver vales generados"
                                            Text="Vales generados" CssClass="btnAzul"></asp:Button>
                                    </td>
                                    <td>
                                        <asp:Button ID="btnVerVale" runat="server" Width="120px" ToolTip="Vista previa de pedido"
                                            Text="Vista previa vale" CssClass="btnAzul"></asp:Button>
                                    </td>
                                    <td>
                                        <asp:Button ID="btnCulminar" runat="server" Width="120px" ToolTip="Culminar pedido"
                                            Text="Culminar vale" CssClass="btnRojo"></asp:Button>
                                    </td>
                                    <td>
                                        <asp:Button ID="btnDespachar" runat="server" Width="120px" ToolTip="Despachar Pedido"
                                            Text="Despachar vale" CssClass="btnVerde"></asp:Button>
                                    </td>
                                    <%--<td>
                                        <asp:Button ID="btnPreDespachar" runat="server" Width="120px" ToolTip="Pre-Despacho"
                                            Text="Pre-Despacho" CssClass="btnNaranja"></asp:Button>
                                    </td>--%>
                                    <td style="text-align: right">
                                        <input id="btnSalir" onclick="javascript:fnc_Cerrar();" value="Salir" type="button"
                                            name="btnSalir" runat="server" size="120px" class="btnAzul" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <input id="hdnCodEmpresa" style="width: 40px; height: 22px" type="hidden" size="1"
                                name="hdnCodEmpresa" runat="server" />
                            <input id="hdnCodUnidad" style="width: 40px; height: 22px" type="hidden" size="1"
                                name="hdnCodUnidad" runat="server" />
                            <input id="hdnCodAlmacen" style="width: 40px; height: 22px" type="hidden" size="1"
                                name="hdnCodAlmacen" runat="server" />
                            <input id="hdnCodReserva" style="width: 40px; height: 22px" type="hidden" size="1"
                                name="hdnCodReserva" runat="server" />
                            <input id="hdnTipoPed" style="width: 40px; height: 22px" type="hidden" size="1"
                                name="hdnTipoPed" runat="server" />
                        </td>
                    </tr>
                </table>
            </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="btnDespachar" EventName="click" />
            </Triggers>
        </asp:UpdatePanel>
    </center>
    </form>
</body>
</html>
