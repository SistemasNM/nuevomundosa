<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="frmListFormatoNuevoInsumo.aspx.vb"
    Inherits="intranet_logi.frmListFormatoNuevoInsumo" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html>
<head>
    <title>Nuevo Mundo - Seguimiento de Pedidos</title>
    <meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1" />
    <meta name="CODE_LANGUAGE" content="Visual Basic .NET 7.1" />
    <meta name="vs_defaultClientScript" content="JavaScript" />
    <meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5" />
    <link rel="Stylesheet" type="text/css" href="../../intranet/Estilos/Styles_Paginas.css" />
    <link rel="Stylesheet" type="text/css" href="../../intranet/Estilos/Styles_Controles.css" />
    <link rel="stylesheet" type="text/css" href="../css/NM0001.css" />
    <script language="javascript" type="text/javascript" src="../js/jsCalendario_N4.js"></script>
    <script language="javascript" type="text/javascript" src="../../intranet/JS/functions.js"></script>
    <script language="javascript" type="text/javascript">

        // Mostrar reporte
        function fMostrarReporte(strURL) {
            var intWidth = screen.width;
            var intHeight = screen.height;
            window.open(strURL, "", "top=0; left=0; width=" + intWidth + "; height=" + intHeight + "; resizable=1;");
        }

        function fnc_BuscarProveedor() {
            var retorno = window.showModalDialog("../Buscadores/frmBusquedaProveedor.aspx", "", "dialogheight:450px;dialogwidth:450px;center:yes;help:no;");
            if (retorno != "" && retorno != ":") {
                var datos = retorno.split(":");
                var codigo = datos[0];
                var nombre = datos[1];
                document.all('txtCodProveedor').value = codigo;
                document.all('lblNomProveedor').innerHTML = nombre;
            }
        }

        function fnc_BuscarArea() {
            var retorno = window.showModalDialog("../Buscadores/frmBusquedaArea.aspx", "", "dialogheight:450px;dialogwidth:450px;center:yes;help:no;");
            if (retorno != "" && retorno != ":") {
                var datos = retorno.split(":");
                var codigo = datos[0];
                var nombre = datos[1];
                document.all('txtArea').value = codigo;
                document.all('lblArea').innerHTML = nombre;
            }
        }

        function fnc_BuscarMaquina() {
            var retorno = window.showModalDialog("../Buscadores/frmBusquedaMaquina.aspx", "", "dialogheight:450px;dialogwidth:450px;center:yes;help:no;");
            if (retorno != "" && retorno != ":") {
                var datos = retorno.split(":");
                var codigo = datos[0];
                var nombre = datos[1];
                document.all('txtMaquina').value = codigo;
                document.all('lblMaquina').innerHTML = nombre;
            }
        }

        function fnc_BuscarTrabajador() {
            var retorno = window.showModalDialog("../Buscadores/frmBusquedaTrabajador.aspx", "", "dialogheight:465px;dialogwidth:600px;center:yes;help:no;");
            if (retorno != "" && retorno != ":") {
                var datos = retorno.split(":");
                var codigo = datos[0];
                var nombre = datos[1];
                document.all('txtResponsable').value = codigo;
                document.all('lblTrabajador').innerHTML = nombre;
            }
        }

        function fnc_BuscarResponsable() {
            var retorno = window.showModalDialog("../Buscadores/frmBusquedaResponsable.aspx", "", "dialogheight:450px;dialogwidth:450px;center:yes;help:no;");
            if (retorno != "" && retorno != ":") {
                var datos = retorno.split(":");
                var codigo = datos[0];
                var nombre = datos[1];
                document.all('txtResponsable').value = codigo;
                document.all('lblResponsable').innerHTML = nombre;
            }
        }

        function fnc_BuscarSolicitante() {
            var retorno = window.showModalDialog("../Buscadores/frmBusquedaUsuario.aspx", "", "dialogheight:450px;dialogwidth:450px;center:yes;help:no;");
            if (retorno != "" && retorno != ":") {
                var datos = retorno.split(":");
                var codigo = datos[0];
                var nombre = datos[1];
                document.all('txtSolicitante').value = codigo;
                document.all('lblSolicitante').innerHTML = nombre;
            }
        }

        function fnc_Cerrar() {
            window.open('', '_parent', '');
            window.close();
        }

        function FormatearBusqDoc(pTexto) {
            if (pTexto == 2) {
                var lnume = '00000000000' + document.all["txtNumeroPedido"].value;
                lnume = lnume.substring(lnume.length, lnume.length - 10);
                if (lnume == '0000000000') {
                    document.all["txtNumeroPedido"].value = '';
                } else {
                    document.all["txtNumeroPedido"].value = lnume;
                }
            }
        }

    </script>
</head>
<body>
    <form id="frmListaFormatoCP" method="post" runat="server" autocomplete="off">
    <table border="0" cellspacing="0" cellpadding="0" width="800px">
        <tr>
            <td style="width: 800px; height: 20px" class="Cabecera">
                Formato Nuevo Insumo
            </td>
        </tr>
    </table>
    <table style="width: 800px" border="0" cellspacing="0" cellpadding="0">
        <tr>
            <td style="width: 120px" class="Etiqueta">
                Responsable:
            </td>
            <td style="width: 150px">
                <asp:TextBox ID="txtResponsable" runat="server" Font-Size="10px" Width="100px" MaxLength="10"></asp:TextBox>
                &nbsp;<input style="width: 20px; height: 20px" id="btnArea" class="boton" onclick="javascript:fnc_BuscarArea();"
                    value="..." type="button" name="btnArea" />
            </td>
            <td style="width: 530px" valign="middle" align="left" colspan="3">
                <asp:Label ID="lblResponsable" runat="server" Font-Size="9px" Width="493px"></asp:Label>
            </td>
        </tr>
        <tr>
            <td style="width: 120px" class="Etiqueta">
                Proveedor:
            </td>
            <td style="width: 150px">
                <asp:TextBox ID="txtCodProveedor" runat="server" Font-Size="10px" Width="100px" MaxLength="10"></asp:TextBox>
                &nbsp;<input style="width: 20px; height: 20px" id="btnResponsable" class="boton"
                    onclick="javascript:fnc_BuscarProveedor();" value="..." type="button" name="btnResponsable" />
            </td>
            <td style="width: 530px" valign="middle" align="left" colspan="3">
                <asp:Label ID="lblNomProveedor" runat="server" Font-Size="9px" Width="421px"></asp:Label>
            </td>
        </tr>
        <tr>
            <td style="width: 120px" class="Etiqueta">
                Estado:
            </td>
            <td style="width: 150px" colspan="2">
                <asp:DropDownList ID="ddlEstado" runat="server" Height="16px" Width="145px" CssClass="cbo">
                    <asp:ListItem Value="">Seleccione estado</asp:ListItem>
                    <asp:ListItem Value="ACT">Creado</asp:ListItem>
                    <asp:ListItem Value="SOL">Solicitado</asp:ListItem>
                    <asp:ListItem Value="APR">Concluido</asp:ListItem>
                    <asp:ListItem Value="ANU">Anulado</asp:ListItem>
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td style="width: 120px" class="Etiqueta">
                Fecha Inicio:
            </td>
            <td style="width: 150px">
                <asp:TextBox ID="txtFechaIni" runat="server" CssClass="input" Width="100px" MaxLength="10"></asp:TextBox>
                <img onclick="popUpCalendar(this, frmListaFormatoCP.txtFechaIni, 'dd/mm/yyyy')" border="0"
                    alt="Seleccionar fecha Inicial" src="../Imagenes/Calendario.gif" width="16px"
                    height="16px" />
            </td>
            <td style="width: 120px" class="Etiqueta">
                Fecha Fin:
            </td>
            <td style="width: 150px">
                <asp:TextBox ID="txtFechaFin" runat="server" CssClass="input" Width="100px" MaxLength="10"></asp:TextBox>
                <img onclick="popUpCalendar(this, frmListaFormatoCP.txtFechaFin, 'dd/mm/yyyy')" border="0"
                    alt="Seleccionar fecha final" src="../Imagenes/Calendario.gif" width="16px" height="16px" />
            </td>
            <td style="width: 260px" valign="middle" align="left">
                <asp:Button ID="btnBuscar" runat="server" Font-Bold="True" CssClass="boton" Width="118px"
                    Text="Buscar"></asp:Button>
                <asp:Button ID="btnNuevo" runat="server" Font-Bold="True" CssClass="boton" Width="118px"
                    Text="Nuevo"></asp:Button>
            </td>
        </tr>
        <tr>
            <td style="width: 800px" colspan="5">
                <asp:Label ID="lblMsg" runat="server" Width="800px" CssClass="error"></asp:Label>
            </td>
        </tr>
    </table>
    <table style="width: 1000px" border="0" cellspacing="0" cellpadding="0">
        <tr>
            <td style="width: 1000px" align="right">
                <%--<asp:label id="lblItems" runat="server" Width="800px"></asp:label>--%>
            </td>
        </tr>
        <tr>
            <td style="width: 800px">
                <asp:DataGrid ID="gvFormatoCP" runat="server" Width="1000px" AutoGenerateColumns="False">
                    <AlternatingItemStyle CssClass="GridAltItem"></AlternatingItemStyle>
                    <ItemStyle CssClass="GridItem"></ItemStyle>
                    <HeaderStyle CssClass="GridHeader"></HeaderStyle>
                    <Columns>
                        <asp:TemplateColumn>
                            <HeaderStyle HorizontalAlign="center" Width="100px"></HeaderStyle>
                            <ItemTemplate>
                                <asp:ImageButton ID="ibtVer" runat="server" ImageUrl="~/Imagenes/buscar.gif" CommandName="Visualizar"
                                    CommandArgument='<%# DataBinder.Eval(Container.DataItem,"INT_COD_GENFOR")%>'>
                                </asp:ImageButton>
                                <asp:ImageButton ID="ibtPDF" runat="server" ImageUrl="~/Imagenes/pdficono.bmp" CommandName="VerPDF" ToolTip="Ver Formato de Insumos"
                                    CommandArgument='<%# DataBinder.Eval(Container.DataItem,"INT_COD_GENFOR") %>'>
                                </asp:ImageButton>
                                <asp:ImageButton ID="ibtPDFHojaRuta" runat="server" ImageUrl="~/Imagenes/pdficono.bmp" ToolTip="Ver Hoja de Ruta"
                                    CommandName="VerPDFHojaRuta" CommandArgument='<%# DataBinder.Eval(Container.DataItem,"INT_COD_GENFOR") %>'>
                                </asp:ImageButton>
                            </ItemTemplate>
                        </asp:TemplateColumn>
                        <asp:BoundColumn DataField="VCH_NROPRELIMINAR" HeaderText="Nro. Prueba">
                            <HeaderStyle Font-Bold="True" HorizontalAlign="Center" Width="80px"></HeaderStyle>
                            <ItemStyle Font-Size="8px" HorizontalAlign="Left"></ItemStyle>
                        </asp:BoundColumn>
                        <asp:BoundColumn DataField="INT_COD_GENFOR" HeaderText="Nro. Doc.">
                            <HeaderStyle Font-Bold="True" HorizontalAlign="Center" Width="80px"></HeaderStyle>
                            <ItemStyle Font-Size="8px" HorizontalAlign="Left"></ItemStyle>
                        </asp:BoundColumn>
                        <asp:BoundColumn DataField="NO_USUA" HeaderText="Responsable">
                            <HeaderStyle Font-Bold="True" HorizontalAlign="Center" Width="120px"></HeaderStyle>
                            <ItemStyle Font-Size="8px" HorizontalAlign="Left"></ItemStyle>
                        </asp:BoundColumn>
                        <asp:BoundColumn DataField="NO_PROV" HeaderText="Proveedor">
                            <HeaderStyle Font-Bold="True" HorizontalAlign="Center" Width="250px"></HeaderStyle>
                            <ItemStyle Font-Size="8px" HorizontalAlign="Left"></ItemStyle>
                        </asp:BoundColumn>
                        <asp:BoundColumn DataField="VCH_OBJ_CAMBIO" HeaderText="Objetivo Cambio">
                            <HeaderStyle Font-Bold="True" HorizontalAlign="Center" Width="250px"></HeaderStyle>
                            <ItemStyle Font-Size="8px" HorizontalAlign="Left"></ItemStyle>
                        </asp:BoundColumn>
                        <asp:BoundColumn DataField="FEC_INICIO" HeaderText="Fecha Creación">
                            <HeaderStyle Font-Bold="True" HorizontalAlign="Center" Width="80px"></HeaderStyle>
                            <ItemStyle Font-Size="8px" HorizontalAlign="Left"></ItemStyle>
                        </asp:BoundColumn>
                        <asp:BoundColumn DataField="VCH_EST_FORMATO" HeaderText="Estado">
                            <HeaderStyle Font-Bold="True" HorizontalAlign="Center" Width="80px"></HeaderStyle>
                            <ItemStyle Font-Size="8px" HorizontalAlign="Left"></ItemStyle>
                        </asp:BoundColumn>
                    </Columns>
                </asp:DataGrid>
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
