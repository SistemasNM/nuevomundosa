<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="frmListadoNuevoInsumo.aspx.vb"
    Inherits="intranet_logi.frmListadoNuevoInsumo" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html>
<head>
    <meta http-equiv="X-UA-Compatible" content="IE=8" />
    <title>Formato Cambio de Proceso</title>
    <%--<link href="../../intranet/Estilos/Styles_Paginas.css" type="text/css" rel="Stylesheet"/>--%>
    <link href="../css/Styles_Paginas.css" rel="stylesheet" type="text/css" />
    <%--<link href="../css/NM0001.css" type="text/css" rel="stylesheet"/>--%>
    <link href="../css/NM0001.css" rel="stylesheet" type="text/css" />
    <%--<link href="../../intranet/Estilos/Styles_Controles.css" type="text/css" rel="Stylesheet"/>--%>
    <link href="../css/Styles_Controles.css" rel="stylesheet" type="text/css" />
    <%--<link href="../../intranet/Estilos/EstilosWeb.css" type="text/css" rel="Stylesheet"/>--%>
    <link href="../css/EstilosWeb.css" rel="stylesheet" type="text/css" />
    <link href="../css/sytle.css" type="text/css" rel="stylesheet" />
    <%--<script language="javascript" src="../../intranet/JS/jsCalendario_N4.js" type="text/javascript"></script>--%>
    <%--  <script src="../js/jsCalendario_N4.js" type="text/javascript"></script>--%>
    <script src="../js/jquery-1.12.1.js.js" type="text/javascript"></script>
    <script src="../js/jquery-ui.js" type="text/javascript"></script>
    <link href="../css/jquery-ui.css" rel="stylesheet" type="text/css" />
    <script src="../js/jsCalendario_N4.js" type="text/javascript"></script>
    <style type="text/css">
        .style1
        {
            width: 200px;
        }
        .style3
        {
            font-family: Verdana, Geneva, sans-serif;
            font-weight: bold;
            font-size: 9px;
            color: #333333;
            background-color: #BCCAE0;
            border: 1px dotted #000099;
            text-align: left;
            margin-left: 0px;
            height: 23px;
        }
        .style5
        {
            font-family: Verdana, Geneva, sans-serif;
            font-weight: bold;
            font-size: 9px;
            color: #333333;
            background-color: #BCCAE0;
            border: 1px dotted #000099;
            text-align: left;
            margin-left: 0px;
            width: 153px;
        }
        .style14
        {
            font-family: Verdana, Geneva, sans-serif;
            font-weight: bold;
            font-size: 9px;
            color: #333333;
            background-color: #BCCAE0;
            border: 1px dotted #000099;
            text-align: left;
            margin-left: 0px;
            height: 56px;
        }
        .style15
        {
            height: 56px;
        }
        .txtEnabled
        {
            background-color: #ffffff;
            color: #000000;
        }
        </style>
</head>
<script language="javascript" type="text/javascript">
    function valideKey(evt) {

        // code is the decimal ASCII representation of the pressed key.
        var code = (evt.which) ? evt.which : evt.keyCode;

        if (code == 8) { // backspace.
            return true;
        } else if (code >= 48 && code <= 57) { // is a number.
            return true;
        } else { // other keys.
            return false;
        }
    }

    function fnc_BuscarProveedor() {
        var retorno = window.showModalDialog("../Buscadores/frmBusquedaProveedor.aspx", "", "dialogheight:450px;dialogwidth:450px;center:yes;help:no;");
        if (retorno != "" && retorno != ":") {
            var datos = retorno.split(":");
            var codigo = datos[0];
            var nombre = datos[1];
            document.all('hdfCodProv').value = codigo;
            document.all('txtNomProv').value = nombre;
        }
    }
    function fnc_BuscarResponsable() {
        var retorno = window.showModalDialog("../Buscadores/frmBusquedaResponsable.aspx", "", "dialogheight:450px;dialogwidth:450px;center:yes;help:no;");
        if (retorno != "" && retorno != ":") {
            var datos = retorno.split(":");
            var codigo = datos[0];
            var nombre = datos[1];
            document.all('txtResponsable').value = nombre;
            //document.all('hdnResponsable').value = codigo;
        }
    }
    function fncAprobar() {

        $('#txtAprobado').val('Aprobado');
        $('#txtAprobado').css("background-color", "yellow");
        $('#txtDesaprobado').val('');
        $('#txtDesaprobado').css("background-color", "white");

    }
    function fncDesaprobar() {

        $('#txtDesaprobado').val("Desaprobado");
        $('#txtAprobado').val("");
        $('#txtAprobado').css("background-color", "white");
        $('#txtDesaprobado').css("background-color", "red");

    }

    function fnc_Mostrar() {

        alert("Se realizara el guardado de los datos, antes de continuar con la Hoja de Ruta.")

    }

</script>
<body>
    <form id="form1" runat="server">
    <asp:ScriptManager ID="script1" runat="server">
    </asp:ScriptManager>
    <div>
        <table style="width: 824px" cellspacing="0" cellpadding="0" border="0">
            <tr>
                <td colspan="8">
                    <asp:ImageButton ID="imgbtnVolver" runat="server" ImageUrl="~/Imagenes/Cancelar.png"
                        Width="24px" Height="24px" ToolTip="Volver"></asp:ImageButton>Volver
                </td>
            </tr>
            <tr>
                <td class="Cabecera" style="width: 800px; height: 30px" align="center" colspan="3">
                    <asp:Label ID="lblPrueba" runat="server" CssClass="Cabecera" Text="PRUEBA N°"></asp:Label>
                    <asp:TextBox ID="txtNroPreliminar" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="Cabecera" style="width: 800px; height: 30px" align="center">
                    Nuevo Insumo/Materia Prima
                </td>
            </tr>
        </table>
        <%--Inicio: Parametros Generales--%>
        <table style="width: 800px">
            <%--Tipo de vale--%>
            <tr>
                <td class="Descripcion" style="width: 130px" align="left" colspan="2">
                    Responsable de Proyecto:
                </td>
                <td align="left" class="style1" colspan="3">
                    <%--<asp:DropDownList ID="ddlResponsable" runat="server" CssClass="cboFormulario"
                        Font-Size="9px" Width="199px" Height="21px">
                    </asp:DropDownList>--%>
                    <asp:TextBox ID="txtResponsable" runat="server" Width="180px"></asp:TextBox>
                    <input id="btnResponsable" runat="server" style="width: 20px;" onclick="javascript:fnc_BuscarResponsable();"
                        type="button" value="..." name="btnSolicitante" class="Boton" />
                    <asp:HiddenField ID="hdfIdFormato" runat="server" />
                </td>
                <td class="style5" align="left">
                    Fecha Creación:
                </td>
                <td align="left" class="style1">
                    <asp:TextBox ID="txtFecInicio" runat="server" class="input" Width="96px" MaxLength="10"
                        Enabled="false"></asp:TextBox>
                    <%--<img onclick="popUpCalendar(this, form1.txtFecInicio, 'dd/mm/yyyy')" height="16"
                        width="16" alt="Seleccionar fecha" src="../images/Calendario.gif" border="0" runat="server" id="ibtCalendar1"/>--%>
                </td>
            </tr>
            <tr>
                <td align="left" class="style14" colspan="2">
                    Nombre del Proveedor:
                </td>
                <td align="left" colspan="5" class="style15">
                    <asp:TextBox ID="txtNomProv" runat="server" class="input" Width="494px" MaxLength="20"></asp:TextBox>
                    <input id="btnSolicitante" style="width: 20px;" onclick="javascript:fnc_BuscarProveedor();"
                        type="button" value="..." name="btnSolicitante" class="Boton" runat="server" />
                    <asp:HiddenField ID="hdfCodProv" runat="server" Value="" />
                </td>
            </tr>
            <tr>
                <td class="Descripcion" style="width: 130px" align="left" colspan="2">
                    Descripción del Producto:
                </td>
                <td align="left" class="style1" colspan="4">
                    <asp:TextBox ID="txtDetProc" runat="server" class="input" Width="372px" MaxLength="200"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="Descripcion" style="width: 130px" align="left" colspan="2">
                    Tipo de Prueba:
                </td>
                <td align="left" colspan="5">
                    <asp:DropDownList ID="ddlTipoPrueba" runat="server" class="input" AppendDataBoundItems="true"
                        Height="16px" Width="145px" CssClass="input">
                        <asp:ListItem Value="">SELECCIONE T.Prueba</asp:ListItem>
                        <asp:ListItem Value="PRE">Preliminar</asp:ListItem>
                        <asp:ListItem Value="IND">Industrial</asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td class="Descripcion" style="width: 130px" align="left" colspan="2">
                    Fabricante:
                </td>
                <td align="left" colspan="5">
                    <asp:DropDownList ID="ddlFabricante" runat="server" class="input" AppendDataBoundItems="true"
                        Height="16px" Width="145px" CssClass="input">
                        <asp:ListItem Value="">SELECCIONE FABRIC.</asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td class="Descripcion" style="width: 130px" align="left" colspan="2">
                    Planta:
                </td>
                <td align="left" class="style1" colspan="4">
                    <asp:TextBox ID="txtPlanta" runat="server" class="input" Width="372px" MaxLength="200"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="Descripcion" align="left" colspan="2">
                    País de Procedencia:
                </td>
                <td>
                    <asp:DropDownList ID="ddlProcedenciaPais" runat="server" class="input" AppendDataBoundItems="true"
                        Height="16px" Width="145px" CssClass="input">
                        <asp:ListItem Value="">SELECCIONE PAÍS</asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td class="Descripcion" style="width: 130px" align="left" colspan="2">
                    Cantidad:
                </td>
                <td align="left" class="style1" colspan="1">
                    <asp:TextBox ID="txtCantidad" runat="server" onkeypress="return valideKey(event);" class="input" Width="145px" MaxLength="200"></asp:TextBox>
                </td>
                <td class="Descripcion" style="width: 50px" align="left" colspan="1">
                    UND
                </td>
                <td align="left" class="style1" colspan="1">
                    <asp:TextBox ID="txtUNidadMedida" runat="server" class="input" Width="145px" MaxLength="200"></asp:TextBox>
                </td>
                <td class="Descripcion" style="width: 50px" align="left" colspan="1">
                    Lote
                </td>
                <td align="left" class="style1" colspan="1">
                    <asp:TextBox ID="txtLote" runat="server" class="input" Width="145px" MaxLength="200"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="Descripcion" style="width: 130px" align="left" colspan="2">
                    Fecha de Producción:
                </td>
                <td align="left" class="style1" colspan="2">
                    <asp:TextBox ID="txtFechaProduccion" runat="server" class="input" Width="145px" MaxLength="200"></asp:TextBox>
                    &quot;&quot;</td>
                <td class="Descripcion" style="width: 200px" align="left" colspan="2">
                    Ubicación del Nuevo Material:
                </td>
                <td align="left" class="style1" colspan="2">
                    <asp:TextBox ID="txtUbicacionNUevoMaterial" runat="server" class="input" Width="145px"
                        MaxLength="200"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="Descripcion" style="width: 130px" align="left" colspan="2">
                    Carácteristicas del Embalaje:
                </td>
                <td align="left" class="style1" colspan="4">
                    <asp:TextBox ID="txtCaracteristicasEmbalaje" runat="server" class="input" Width="372px"
                        MaxLength="200"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="Descripcion" style="width: 130px" align="left" colspan="2">
                    Código del Material:
                </td>
                <td align="left" class="style1" colspan="4">
                    <asp:TextBox ID="txtCodigoMaterial" runat="server" class="input" Width="372px" MaxLength="200"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="Descripcion" style="width: 130px" align="left" colspan="2">
                    Características Relevantes del Material:
                </td>
                <td align="left" class="style1" colspan="4">
                    <asp:TextBox ID="txtCaracteristicasRelevantesMaterial" runat="server" class="input"
                        Width="372px" MaxLength="200"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td colspan="2" class="Descripcion" align="left">
                    <span id="Label4" class="Descripcion" align="left" style="display: inline-block;
                        width: 144px;">Adjuntar Ficha / Especificación Técnica</span>
                </td>
                <td colspan="2" style="text-align: center;">
                    <asp:FileUpload ID="fluAdjuntarFicha" runat="server" class="input" />
                    <asp:ImageButton ID="ibtDescargarfichas" runat="server" CommandArgument='<%# Eval("VCHFILENAMEFICHA") %>'
                        CommandName="descargarArchivo" ImageUrl="~/Imagenes/download.png" Width="24px"
                        Height="24px" ToolTip="Descargar Archivo"></asp:ImageButton>
                    <asp:Label ID="lblficha" runat="server"></asp:Label>
                </td>
                <td colspan="2" class="Descripcion" align="left">
                    <span id="Label6" class="Descripcion" align="left">Adjuntar Certificado de Calidad</span>
                </td>
                <td colspan="2" style="text-align: center;">
                    <asp:FileUpload ID="fluAdjuntarCertificadoCalidad" runat="server" class="input" />
                    <asp:ImageButton ID="ibtDescargarcertificados" runat="server" CommandArgument='<%# Eval("VCH_FILENAMECERTIFICADO") %>'
                        CommandName="descargarArchivo" ImageUrl="~/Imagenes/download.png" Width="24px"
                        Height="24px" ToolTip="Descargar Archivo"></asp:ImageButton>
                    <asp:Label ID="lblCertificado" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <td colspan="2" class="Descripcion" align="left">
                    <span id="Label5" class="Descripcion" align="left" style="display: inline-block;
                        width: 144px;">Adjuntar Carta de Compromiso</span>
                </td>
                <td colspan="2" style="text-align: center;" >
                    <asp:FileUpload ID="fluAdjuntarCartaCompremiso" runat="server" class="input" />
                    <asp:ImageButton ID="ibtDescargarcarta" runat="server" CommandArgument='<%# Eval("VCHFILENAMECARTA") %>'
                        CommandName="descargarArchivo" ImageUrl="~/Imagenes/download.png" Width="24px"
                        Height="24px" ToolTip="Descargar Archivo"></asp:ImageButton>
                    <asp:Label ID="lblCarta" runat="server"></asp:Label>
                </td>
                 <td colspan="2" class="Descripcion" align="left">
                    <span id="Span1" class="Descripcion" align="left" style="display: inline-block;
                        width: 144px;">informe técnico / documento adicional</span>
                </td>
                <td colspan="2" style="text-align: center;" >
                    <asp:FileUpload ID="fluAdjuntarDocAdicional" runat="server" class="input" />
                    <asp:ImageButton ID="ibtDescargarDocumento" runat="server" CommandArgument='<%# Eval("VCH_FILENAMEDOCUMENTO") %>'
                        CommandName="descargarArchivo" ImageUrl="~/Imagenes/download.png" Width="24px"
                        Height="24px" ToolTip="Descargar Archivo"></asp:ImageButton>
                    <asp:Label ID="lbldocumentoadicional" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="Descripcion" align="left" colspan="2">
                    Proceso en el cual se usuará el producto:
                </td>
                <td align="left" class="style1" colspan="4">
                    <asp:TextBox ID="txtProcProd" runat="server" class="input" Width="374px" MaxLength="200"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="Descripcion" align="left" colspan="7">
                    OBJETIVO DEL CAMBIO AL NUEVO PRODUCTO:
                </td>
            </tr>
            <tr>
                <td style="width: 680px" align="left" class="Descripcion" colspan="7">
                    <asp:TextBox ID="txtObjCambio" runat="server" CssClass="txtAreaHabilitado" Height="68px"
                        Font-Size="9px" Width="791px" TextMode="MultiLine"></asp:TextBox>
                    <br />
                </td>
            </tr>
            <tr>
                <td align="left" class="style1" colspan="7">
                </td>
            </tr>
            <tr>















































                <td class="style3" align="left" colspan="7">
                    CONSIDERACIONES DE RIESGOS DEL NUEVO PRODUCTO:
                </td>
            </tr>
            <tr>
                <%--<td style="width: 680px" align="left" class="Descripcion" colspan="7">
                    <asp:GridView ID="gvBloque2" runat="server" AutoGenerateColumns="false" ShowFooter="true"
                        DataKeyNames="INT_COD_GENPAR" ShowHeader="false">
                        <Columns>
                            <asp:TemplateField HeaderText="">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtCampo" runat="server" Width="450px" Text='<%# Eval("VCH_COD_CAMPO") %>'
                                        CssClass="txtEnabled" Enabled="false"></asp:TextBox>
                                </ItemTemplate>
                                <FooterTemplate>
                                    <asp:TextBox ID="txtCampoF" runat="server" Width="450px"></asp:TextBox>
                                </FooterTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtValor2" runat="server" Width="340px" Text='<%# Eval("VCH_VAL_CAMPO") %>'></asp:TextBox>
                                </ItemTemplate>
                                <FooterTemplate>
                                    <asp:TextBox ID="txtValorF" runat="server" Width="340px"></asp:TextBox>
                                </FooterTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:ImageButton ID="btnEliminar" runat="server" ImageUrl="~/Imagenes/delete.gif"
                                                    CommandName="Eliminar" CommandArgument='<%# Eval("INT_COD_GENPAR") %>'></asp:ImageButton>
                                </ItemTemplate>
                                <FooterTemplate>
                                    <asp:ImageButton ID="btnAgregar" runat="server" ImageUrl="~/Imagenes/Adicionar.png"
                                        CommandName="Agregar" CommandArgument=''></asp:ImageButton>
                                </FooterTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </td>--%>
                <td style="width: 680px" align="left" class="Descripcion" colspan="7">
                    <asp:TextBox ID="txtConsRiesgo" runat="server" CssClass="txtAreaHabilitado" Height="68px"
                        Font-Size="9px" Width="791px" TextMode="MultiLine"></asp:TextBox>
                    <br />
                </td>
            </tr>
            <tr>
                <td align="left" class="style1" colspan="7">
                </td>
            </tr>
            <%--      <tr>
                <td colspan="7">
                    <%--  <asp:ScriptManager ID="script1" runat="server">
                    </asp:ScriptManager>
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Always">
                        <ContentTemplate>
                            <div id="pnlProduccion">
                                <div id="pnlProduccion1">
                                    <table>
                                        <tr>
                                            <td class="style6" align="left" style="width: 625px;">
                                                MEDICIONES O RESULTADOS CUANTIFICADOS DE NUEVO PROCESO:
                                            </td>
                                            <td class="style6" colspan="1">
                                                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                &nbsp;&nbsp;&nbsp;
                                                <asp:ImageButton ID="ibtAgregarProd" runat="server" ImageUrl="~/Imagenes/Abajo.png"
                                                    Style="text-align: center" />
                                            </td>
                                        </tr>
                                    </table>
                                    <h4>
                                        PRODUCCIÓN 1</h4>
                                    <asp:HiddenField ID="hdfPanel2" runat="server" />
                                    <asp:HiddenField ID="hdfPanel3" runat="server" />
                                    <asp:GridView ID="gvProduc1" runat="server" AutoGenerateColumns="false" ShowFooter="true"
                                        DataKeyNames="INT_COD_GENPAR" ShowHeader="true">
                                        <Columns>
                                            <asp:TemplateField HeaderText="Parámetro">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtCampo1" runat="server" Width="450px" Text='<%# Eval("VCH_COD_CAMPO") %>'
                                                        CssClass="txtEnabled" Enabled="false"></asp:TextBox>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:TextBox ID="txtCampoF1" runat="server" Width="450px"></asp:TextBox>
                                                </FooterTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Valor">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtValor1" runat="server" Width="340px" Text='<%# Eval("VCH_VAL_CAMPO") %>'
                                                        CssClass="txtEnabled" Enabled="false"></asp:TextBox>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:TextBox ID="txtValorF1" runat="server" Width="340px"></asp:TextBox>
                                                </FooterTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:ImageButton ID="btnEliminar" runat="server" ImageUrl="~/Imagenes/delete.gif"
                                                        CommandName="Eliminar" CommandArgument='<%# Eval("INT_COD_GENPAR") %>'></asp:ImageButton>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:ImageButton ID="btnAgregar" runat="server" ImageUrl="~/Imagenes/Adicionar.png"
                                                        CommandName="Agregar" CommandArgument=''></asp:ImageButton>
                                                </FooterTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                </div>
                                <div id="pnlProduccion2" style="display: block;" runat="server">
                                    <table>
                                        <tr>
                                            <td>
                                                <h4>
                                                    PRODUCCIÓN 2
                                                </h4>
                                            </td>
                                            <td>
                                                <asp:ImageButton ID="ibtMenos2" runat="server" ImageUrl="~/Imagenes/menos.png" Width="40px"
                                                    Height="20px" />
                                            </td>
                                        </tr>
                                    </table>
                                    <asp:GridView ID="gvProduc2" runat="server" AutoGenerateColumns="false" ShowFooter="true"
                                        DataKeyNames="INT_COD_GENPAR" ShowHeader="true">
                                        <Columns>
                                            <asp:TemplateField HeaderText="Parámetro">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtCampo2" runat="server" Width="450px" Text='<%# Eval("VCH_COD_CAMPO") %>'
                                                        CssClass="txtEnabled" Enabled="false"></asp:TextBox>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:TextBox ID="txtCampoF2" runat="server" Width="450px"></asp:TextBox>
                                                </FooterTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Valor">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtValor2" runat="server" Width="340px" Text='<%# Eval("VCH_VAL_CAMPO") %>'
                                                        CssClass="txtEnabled" Enabled="false"></asp:TextBox>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:TextBox ID="txtValorF2" runat="server" Width="340px"></asp:TextBox>
                                                </FooterTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:ImageButton ID="btnEliminar" runat="server" ImageUrl="~/Imagenes/delete.gif"
                                                        CommandName="Eliminar" CommandArgument='<%# Eval("INT_COD_GENPAR") %>'></asp:ImageButton>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:ImageButton ID="btnAgregar" runat="server" ImageUrl="~/Imagenes/Adicionar.png"
                                                        CommandName="Agregar" CommandArgument=''></asp:ImageButton>
                                                </FooterTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                </div>
                                <div id="pnlProduccion3" style="display: block;" runat="server">
                                    <table>
                                        <tr>
                                            <td>
                                                <h4>
                                                    PRODUCCIÓN 3
                                                </h4>
                                            </td>
                                            <td>
                                                <asp:ImageButton ID="ibtMenos3" runat="server" ImageUrl="~/Imagenes/menos.png" Width="40px"
                                                    Height="20px" />
                                            </td>
                                        </tr>
                                    </table>
                                    <asp:GridView ID="gvProduc3" runat="server" AutoGenerateColumns="false" ShowFooter="true"
                                        ShowHeader="true" Style="margin-bottom: 0px" DataKeyNames="INT_COD_GENPAR">
                                        <Columns>
                                            <asp:TemplateField HeaderText="Parámetro">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtCampo3" runat="server" Width="450px" Text='<%# Eval("VCH_COD_CAMPO") %>'
                                                        CssClass="txtEnabled" Enabled="false"></asp:TextBox>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:TextBox ID="txtCampoF3" runat="server" Width="450px"></asp:TextBox>
                                                </FooterTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Valor">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtValor3" runat="server" Width="340px" Text='<%# Eval("VCH_VAL_CAMPO") %>'
                                                        CssClass="txtEnabled" Enabled="false"></asp:TextBox>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:TextBox ID="txtValorF3" runat="server" Width="340px"></asp:TextBox>
                                                </FooterTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:ImageButton ID="btnEliminar" runat="server" ImageUrl="~/Imagenes/delete.gif"
                                                        CommandName="Eliminar" CommandArgument='<%# Eval("INT_COD_GENPAR") %>'></asp:ImageButton>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:ImageButton ID="btnAgregar" runat="server" ImageUrl="~/Imagenes/Adicionar.png"
                                                        CommandName="Agregar" CommandArgument=''></asp:ImageButton>
                                                </FooterTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                </div>
                            </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </td>
            </tr>--%>
            <tr>
                <td align="left" class="style1" colspan="7">
                </td>
            </tr>
            <tr>
                <td class="style3" align="left" colspan="5">
                    CONCLUSIONES:
                </td>
                <td class="style5" align="left">
                    Fecha Conclusión Estimada:
                </td>
                <td align="left" class="style1">
                    <asp:UpdatePanel ID="UpdatePanel4" runat="server" UpdateMode="Always">
                        <ContentTemplate>
                            &nbsp;<asp:TextBox ID="txtFecConc" runat="server" class="input" Width="96px" MaxLength="10"></asp:TextBox>
                            <img onclick="popUpCalendar(this, form1.txtFecConc, 'dd/mm/yyyy')" height="16" width="16"
                                alt="Seleccionar fecha" src="../images/Calendario.gif" border="0" runat="server"
                                id="ibtCalendar2" />
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </td>
            </tr>
            <tr>
                <td style="width: 680px" align="left" class="Descripcion" colspan="7">
                    <asp:TextBox ID="txtDescConcl" runat="server" CssClass="txtAreaHabilitado" Height="87px"
                        Font-Size="9px" Width="791px" TextMode="MultiLine"></asp:TextBox>
                    <br />
                </td>
            </tr>
            <tr>
                <td align="left" class="style1" colspan="7">
                </td>
            </tr>
            <tr>
                <td colspan="7" style="text-align: center;">
                    <input name="btnAprobado" type="button" id="btnAprobado" style="width: 200px;" onclick="javascript:fncAprobar();"
                        value="Aprobado" class="Boton" />
                    <asp:TextBox type="text" ID="txtAprobado" name="txtAprobado" runat="server" class="input"
                        Style="width: 200px;"></asp:TextBox>
                    <input name="btnDesaprobado" type="button" id="btnDesaprobado" style="width: 200px;"
                        onclick="javascript:fncDesaprobar();" value="Desaprobado" class="Boton" />
                    <asp:TextBox type="text" ID="txtDesaprobado" name="txtDesaprobado" runat="server"
                        class="input" Style="width: 200px;"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td colspan="7" class="style3">
                    OBSERVACIONES
                </td>
            </tr>
            <tr>
                <td style="width: 680px" align="left" class="Descripcion" colspan="7">
                    <asp:TextBox ID="txtObservacionesInsumos" runat="server" class="input" CssClass="txtAreaHabilitado"
                        Height="87px" Font-Size="9px" Width="791px" TextMode="MultiLine"></asp:TextBox>
                    <br />
                </td>
            </tr>
            <tr>
                <td colspan="7">
                    <div id="pnlFirmas">
                        <table>
                            <tr>
                                <td>
                                    <h4>
                                        FIRMAS:
                                    </h4>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <%-- <asp:ScriptManager ID="ScriptManager2" runat="server">
                                    </asp:ScriptManager>--%>
                                    <asp:UpdatePanel ID="UpdatePanel3" runat="server" UpdateMode="Always">
                                        <ContentTemplate>
                                            <asp:GridView ID="gvFirmas" runat="server" AutoGenerateColumns="false" ShowFooter="true"
                                                ShowHeader="false">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="txtCampoFirma" runat="server" Width="285px" Text='<%# Eval("VCH_USU_FIRMA") %>'
                                                                CssClass="txtEnabled" Enabled="false"></asp:TextBox>
                                                        </ItemTemplate>
                                                        <FooterTemplate>
                                                            <asp:DropDownList ID="ddlFirmas" runat="server" DataSource='<%# fn_CargarFirmas() %>'
                                                                DataTextField="NO_USUA" DataValueField="var_Dato">
                                                            </asp:DropDownList>
                                                        </FooterTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField>
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="txtCampoFFirma" runat="server" Width="380px" Text='<%# Eval("NO_USUA") %>'
                                                                CssClass="txtEnabled" Enabled="false"></asp:TextBox>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField Visible="false" ItemStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblEstadoFirma" runat="server" Width="100px" Font-Bold="true" Text='<%# Eval("VCH_EST_FIRMA") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField>
                                                        <ItemTemplate>
                                                            <asp:ImageButton ID="btnEliminar" runat="server" ImageUrl="~/Imagenes/delete.gif"
                                                                CommandName="Eliminar" CommandArgument='<%# Eval("VCH_USU_FIRMA") %>'></asp:ImageButton>
                                                        </ItemTemplate>
                                                        <FooterTemplate>
                                                            <asp:ImageButton ID="btnAgregar" runat="server" ImageUrl="~/Imagenes/Adicionar.png"
                                                                CommandName="Agregar" CommandArgument=''></asp:ImageButton>
                                                        </FooterTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                            </asp:GridView>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </td>
                                <td>
                                    <br />
                                    <br />
                                </td>
                            </tr>
                        </table>
                    </div>
                </td>
            </tr>
        </table>
        <table style="width: 825px">
            <tr>
                <td>
                </td>
            </tr>
            <tr>
                <td colspan="7" style="text-align: center;">
                    <asp:Button ID="btnHojaRuta" runat="server" Style="width: 400px;" OnClientClick="fnc_Mostrar()"
                        Text="Hoja de Ruta" CssClass="Boton" />
                </td>
                <td colspan="7" style="text-align: center;">
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td colspan="7" align="right" class="style7">
                    <asp:Button ID="btnGuardar" runat="server" Text="Guardar" CssClass="Boton" Height="29px"
                        Width="99px" />
                    <asp:Button ID="btnSolicitar" runat="server" Text="Solicitar" CssClass="Boton" Style="background-color: Yellow;
                        color: Black;" Height="29px" Width="99px" OnClientClick="return confirm('Desea solicitar aprobación?');" />
                    <asp:Button ID="btnAprobar" runat="server" Text="Aprobar" CssClass="btnVerde" Height="29px"
                        Width="99px" OnClientClick="return confirm('Desea aprobar el formato?');" />
                    <asp:Button ID="btnAnular" runat="server" Text="Anular" CssClass="BotonRojo" Height="29px"
                        Width="99px" OnClientClick="return confirm('Desea anular el formato?');" />
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>

