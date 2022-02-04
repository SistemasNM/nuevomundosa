<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="frmHojaRuta.aspx.vb" Inherits="intranet_logi.frmHojaRuta" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="X-UA-Compatible" content="IE=8" />
    <title>Hoja de Ruta</title>
    <%--<link href="../../intranet/Estilos/Styles_Paginas.css" type="text/css" rel="Stylesheet"/>--%>
    <link href="../css/Styles_Paginas.css" rel="stylesheet" type="text/css" />
    <%--<link href="../css/NM0001.css" type="text/css" rel="stylesheet"/>--%>
    <link href="../css/NM0001.css" rel="stylesheet" type="text/css" />
    <script language="javascript" type="text/javascript" src="../../intranet/JS/jsCalendario_N4.js"></script>
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
        .txtEnabled
        {
            background-color: #ffffff;
            color: #000000;
        }
        .textoV
        {
            text-overflow: clip;
            writing-mode: lr-tb;
        }
        .style16
        {
            height: 28px;
        }
        .style17
        {
            font-family: Verdana, Geneva, sans-serif;
            font-weight: bold;
            font-size: 9px;
            color: #333333;
            background-color: #BCCAE0;
            border: 1px dotted #000099;
            text-align: left;
            margin-left: 0px;
            width: 115px;
        }
        .style18
        {
            height: 17px;
        }
        .style19
        {
            font-size: small;
        }
        .ttable
        {
            border: #336699 5px solid;
        }
        .style20
        {
            font-weight: bold;
            font-size: 9px;
            color: #333333;
            background-color: #BCCAE0;
            font-family: Verdana;
            text-decoration: none;
            height: 20px;
            width: 210px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <asp:ScriptManager ID="script1" runat="server">
    </asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel4" runat="server" UpdateMode="Always">
        <ContentTemplate>
            <div>
                <table id="tabla1" runat="server" style="width: 924px" cellspacing="0" cellpadding="0"
                    border="0">
                    <tr>
                        <td colspan="8" class="style16">
                            <asp:ImageButton ID="imgbtnVolver" runat="server" ImageUrl="~/Imagenes/Cancelar.png"
                                Width="24px" Height="24px" ToolTip="Volver"></asp:ImageButton>Volver
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 150px; border-style: groove" class="Etiqueta" align="center">
                            <asp:Image runat="server" ID="imgProgram" ImageUrl="~/Imagenes/LOGO_NuevoMundo.png"
                                Height="60px" />
                        </td>
                        <td class="Cabecera" style="width: 800px; height: 30px" align="center">
                            <h3>
                                INFORME DE PRUEBAS DE NUEVOS MATERIALES
                            </h3>
                        </td>
                    </tr>
                    <tr>
                        <td class="Descripcion" align="left" colspan="2" style="text-align: center;">
                            PRUEBA NRO.
                            <asp:TextBox ID="txtPruebaNro" runat="server" class="input" MaxLength="200"></asp:TextBox>
                        </td>
                    </tr>
                </table>
            </div>
            <table id="tabla2" runat="server" class="ttable" style="width: 924px" cellspacing="0"
                cellpadding="0" border="0">
                <tr>
                    <td class="style17" align="center">
                        DESCRIPCION DEL PRODUCTO:
                    </td>
                    <td align="left" class="style1" colspan="1">
                        <asp:TextBox ID="txtDescProdcuto" runat="server" class="input" Height="50px" Width="300px"
                            TextMode="MultiLine"></asp:TextBox>
                    </td>
                    <td rowspan="2">
                        <table cellspacing="0" cellpadding="0" border="0">
                            <tr>
                                <td class="Cabecera" align="center" colspan="3">
                                    Resumen de etapa de ejecución de la prueba
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <%--  <asp:UpdatePanel ID="UpdatePanel3" runat="server" UpdateMode="Always">
                                        <ContentTemplate>--%>
                                    <table cellspacing="0" cellpadding="0" border="0" align="center">
                                        <tr>
                                            <td rowspan="7" align="center" style="width: 20px; background-color: Yellow; color: Black;">
                                                <h1>
                                                    <asp:Label ID="lbltipoprueba" name="lblTipoPrueba" Style="color: #336699; font-size: 20px;"
                                                        runat="server" Text=""></asp:Label>
                                                </h2>
                                            </td>
                                            <td style="background-color: LightBlue; color: Black;" align="center">
                                                <asp:Button ID="btnHilanderiaR" name="btnHilanderiaR" CssClass="Boton" runat="server"
                                                    Text="HILANDERIA"></asp:Button>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="background-color: LightBlue; color: Black;" align="center">
                                                <asp:Button ID="btnLaboratorioHIlanderiaR" name="btnLaboratorioHIlanderiaR" CssClass="Boton"
                                                    runat="server" Text="LABORATORIO HILANDERIA"></asp:Button>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="background-color: LightBlue; color: Black;" align="center">
                                                <asp:Button ID="btnPretejeduriaR" name="btnPretejeduriaR" CssClass="Boton" runat="server"
                                                    Text="PRETEJEDURIA"></asp:Button>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="background-color: LightBlue; color: Black;" align="center">
                                                <asp:Button ID="btnLaboratorioFisicoR" name="btnLaboratorioFisicoR" CssClass="Boton"
                                                    runat="server" Text="LABORATORIO FÍSICO"></asp:Button>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="background-color: LightBlue; color: Black;" align="center">
                                                <asp:Button ID="btnTejeduriaR" name="btnTejeduriaR" CssClass="Boton" runat="server"
                                                    Text="TEJEDURIA"></asp:Button>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="background-color: LightBlue; color: Black;" align="center">
                                                <asp:Button ID="btnTintoreriaR" name="btnTintoreriaR" CssClass="Boton" runat="server"
                                                    Text="TINTORERIA"></asp:Button>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="background-color: LightBlue; color: Black;" align="center">
                                                <asp:Button ID="btnRevisionFinalR" name="btnRevisionFinalR" CssClass="Boton" runat="server"
                                                    Text="REVISION FINAL"></asp:Button>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                                <%--   </ContentTemplate>
                                    </asp:UpdatePanel>--%>
                                <td>
                                    <table cellspacing="0" cellpadding="0" border="0">
                                        <tr>
                                            <td>
                                                <asp:TextBox ID="txtPorEjecutar" runat="server" class="input" MaxLength="200" Style="background-color: Orange;
                                                    width: 40px;"></asp:TextBox>
                                            </td>
                                            <td align="center">
                                                POR EJECUTAR
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:TextBox ID="txtNoAplica" runat="server" class="input" MaxLength="200" Style="background-color: #336699;
                                                    width: 40px;"></asp:TextBox>
                                            </td>
                                            <td align="center">
                                                NO APLICA
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td class="style17" align="center">
                        OBJETIVO DE CAMBIO AL NUEVO PRODUCTO:
                    </td>
                    <td align="left" class="style1" colspan="1">
                        <asp:TextBox ID="txtObjCambio" runat="server" class="input" Width="300px" TextMode="MultiLine"
                            Height="50px"></asp:TextBox>
                    </td>
                </tr>
            </table>
            <table id="tabla3" runat="server" class="ttable" style="width: 924px" cellspacing="0"
                cellpadding="0" border="0">
                <tr>
                    <td class="titulo" colspan="3">
                        INFORMACIÓN PRELIMINAR DEL MATERIAL - PRUEBA N°
                        <asp:TextBox ID="txtNroPreliminar2" runat="server" class="input" MaxLength="200"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                        <table cellspacing="0" cellpadding="0" border="1" align="center">
                            <tr>
                                <td style="background-color: LightBlue; color: Black;" align="center">
                                    ITEM
                                </td>
                                <td style="background-color: LightBlue; color: Black;" align="center">
                                    <asp:TextBox ID="txtItem" runat="server" TextMode="MultiLine" Height="20px" Width="300px"
                                        Style="background-color: Orange" class="input" MaxLength="200"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td style="background-color: LightBlue; color: Black;" align="center">
                                    PROVEEDOR
                                </td>
                                <td style="background-color: LightBlue; color: Black;" align="center">
                                    <asp:TextBox ID="txtProveedor" runat="server" class="input" Width="300px" MaxLength="200"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td style="background-color: LightBlue; color: Black;" align="center">
                                    PLANTA
                                </td>
                                <td style="background-color: LightBlue; color: Black;" align="center">
                                    <asp:TextBox ID="txtPlanta" runat="server" class="input" Width="300px" MaxLength="200"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td style="background-color: LightBlue; color: Black;" align="center">
                                    FECHA DE INGRESO
                                </td>
                                <td class="Etiqueta" style="width: 150px; height: 20px" valign="middle" align="left">
                                    <asp:TextBox ID="txtFechaIngreso" runat="server" Width="100px" Style="background-color: Orange"
                                        CssClass="input"></asp:TextBox><!--date-->
                                    &nbsp;<img onclick="popUpCalendar(this, form1.txtFechaIngreso, 'dd/mm/yyyy')" height="16"
                                        width="16" alt="Seleccionar fecha de Inst." src="../images/Calendario.gif" border="0" />
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td>
                        <table cellspacing="0" cellpadding="0" border="1" align="center">
                            <tr>
                                <td style="background-color: LightBlue; color: Black;" align="center">
                                    LOTE
                                </td>
                                <td style="background-color: LightBlue; color: Black;" align="center">
                                    <asp:TextBox ID="txtLote" runat="server" class="input" MaxLength="200"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td style="background-color: LightBlue; color: Black;" align="center">
                                    PAÍS DE PROCEDENCIA
                                </td>
                                <td style="background-color: LightBlue; color: Black;" align="center">
                                    <asp:TextBox ID="txtPaisProcedencia" runat="server" class="input" MaxLength="200"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td style="background-color: LightBlue; color: Black;" align="center">
                                    CODIGO PROVEEDOR
                                </td>
                                <td style="background-color: LightBlue; color: Black;" align="center">
                                    <asp:TextBox ID="txtCodProveedor" runat="server" class="input" MaxLength="200"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td style="background-color: LightBlue; color: Black;" align="center">
                                    CANTIDAD INGRESADA
                                </td>
                                <td style="background-color: LightBlue; color: Black;" align="center">
                                    <asp:TextBox ID="txtCantidadIngresada" runat="server" class="input" MaxLength="200"></asp:TextBox>
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td>
                        <table cellspacing="0" cellpadding="0" border="1" align="center">
                            <tr>
                                <td style="background-color: LightBlue; color: Black;" align="center">
                                    FICHA / ESPECIFICACIÓN TÉCNICA
                                </td>
                                <td style="background-color: LightBlue; color: Black;" align="center">
                                    <asp:ImageButton ID="ibtDescargarfichas" runat="server" CommandArgument='<%# Eval("VCHFILENAMEFICHA") %>'
                                        CommandName="descargarArchivo" ImageUrl="~/Imagenes/download.png" Width="24px"
                                        Height="24px" ToolTip="Descargar Archivo"></asp:ImageButton>
                                    <asp:TextBox ID="txtFichaEspecificacionTecnica" runat="server" class="input" MaxLength="200"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td style="background-color: LightBlue; color: Black;" align="center">
                                    CERTIFICADO DE CALIDAD
                                </td>
                                <td style="background-color: LightBlue; color: Black;" align="center">
                                    <asp:ImageButton ID="ibtDescargarcertificados" runat="server" CommandArgument='<%# Eval("VCH_FILENAMECERTIFICADO") %>'
                                        CommandName="descargarArchivo" ImageUrl="~/Imagenes/download.png" Width="24px"
                                        Height="24px" ToolTip="Descargar Archivo"></asp:ImageButton>
                                    <asp:TextBox ID="txtFichaCertificadoCalidad" runat="server" class="input" MaxLength="200"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td style="background-color: LightBlue; color: Black;" align="center">
                                    CARTA DE COMPROMISO
                                </td>
                                <td style="background-color: LightBlue; color: Black;" align="center">
                                    <asp:ImageButton ID="ibtDescargarcarta" runat="server" CommandArgument='<%# Eval("VCHFILENAMECARTA") %>'
                                        CommandName="descargarArchivo" ImageUrl="~/Imagenes/download.png" Width="24px"
                                        Height="24px" ToolTip="Descargar Archivo"></asp:ImageButton>
                                    <asp:TextBox ID="txtFichaCartaCompromiso" runat="server" class="input" MaxLength="200"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td style="background-color: LightBlue; color: Black;" align="center">
                                    DOCUMENTO ADICIONAL
                                </td>
                                <td style="background-color: LightBlue; color: Black;" align="center">
                                    <asp:ImageButton ID="ibtDescargardocumento" runat="server" CommandArgument='<%# Eval("VCH_FILENAMEDOCUMENTO") %>'
                                        CommandName="descargarArchivo" ImageUrl="~/Imagenes/download.png" Width="24px"
                                        Height="24px" ToolTip="Descargar Archivo"></asp:ImageButton>
                                    <asp:TextBox ID="txtFichadocumento" runat="server" class="input" MaxLength="200"></asp:TextBox>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
            <table id="TablaPrincipal" runat="server" cellspacing="0" cellpadding="0">
                <tr>
                    <td style="text-align: center;">
                        <asp:Button ID="btnGuardarProcesos" runat="server" Style="text-align: center; font-size: 20px;
                            background-color: #336699; color: White; font-weight: bold;" Text="Guardar Procesos" />
                    </td>
                </tr>
                <tr>
                    <td class="titulo" colspan="2">
                        DESEMPEÑO EN PROCESO
                    </td>
                </tr>
                <tr>
                    <td>
                        <table id="tablaHilanderia" runat="server" border="0" cellpadding="0" cellspacing="0"
                            class="ttable" style="width: 924px">
                            <tr>
                                <td class="RowHead" rowspan="12">
                                    HILANDERIA
                                </td>
                                <td colspan="2">
                                    <table align="center" border="1" cellpadding="0" cellspacing="0">
                                        <tr>
                                            <td align="center" style="background-color: LightBlue; color: Black;">
                                                FECHA DE PRODUCCIÓN
                                            </td>
                                            <td align="left" class="Etiqueta" style="width: 150px; height: 20px" valign="middle">
                                                <asp:TextBox ID="txtFechaProduccionHila" runat="server" CssClass="input" Width="80px"></asp:TextBox><!--date-->
                                                &nbsp;<img onclick="popUpCalendar(this, form1.txtFechaProduccionHila, 'dd/mm/yyyy')"
                                                    height="16" width="16" alt="Seleccionar fecha de Inst." src="../images/Calendario.gif"
                                                    border="0" />
                                            </td>
                                            <td align="center" style="background-color: LightBlue; color: Black;">
                                                MÁQUINA
                                            </td>
                                            <td align="center" style="background-color: LightBlue; color: Black;">
                                                <asp:TextBox ID="txtMaquinaHila" runat="server" class="input" name="txtMaquinaHila"
                                                    type="text"></asp:TextBox>
                                            </td>
                                            <td align="center" style="background-color: LightBlue; color: Black;">
                                                SUPERVISOR
                                            </td>
                                            <td align="center" style="background-color: LightBlue; color: Black;">
                                                <asp:TextBox ID="txtSupervisorHila" runat="server" class="input" MaxLength="200"></asp:TextBox>
                                            </td>
                                            <td align="center" style="background-color: LightBlue; color: Black;">
                                                OPERARIO
                                            </td>
                                            <td align="center" style="background-color: LightBlue; color: Black;">
                                                <asp:TextBox ID="txtOperarioHila" runat="server" class="input" MaxLength="200"></asp:TextBox>
                                            </td>
                                            <td align="center" style="background-color: LightBlue; color: Black;">
                                                TURNO
                                            </td>
                                            <td align="center" style="background-color: LightBlue; color: Black;">
                                                <asp:TextBox ID="txtTurnoHila" runat="server" class="input" MaxLength="200"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="center" style="background-color: LightBlue; color: Black;">
                                                LÍNEA
                                            </td>
                                            <td align="center" style="background-color: LightBlue; color: Black;">
                                                <asp:TextBox ID="txtLineaHila" runat="server" class="input" MaxLength="200"></asp:TextBox>
                                            </td>
                                            <td align="center" style="background-color: LightBlue; color: Black;">
                                                PROCESO
                                            </td>
                                            <td align="center" style="background-color: LightBlue; color: Black;">
                                                <asp:TextBox ID="txtProcesoHila" runat="server" class="input" MaxLength="200"></asp:TextBox>
                                            </td>
                                            <td align="center" style="background-color: LightBlue; color: Black;">
                                                MATERIAL
                                            </td>
                                            <td align="center" style="background-color: LightBlue; color: Black;">
                                                <asp:TextBox ID="txtMaterialHila" runat="server" class="input" MaxLength="200"></asp:TextBox>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <table align="center" border="0" cellpadding="0" cellspacing="0" style="margin: 5px;">
                                        <tr align="center" class="RowHead">
                                            <td>
                                                PROCESO DE HILATURA
                                            </td>
                                            <td>
                                                VALORES
                                            </td>
                                            <td>
                                                OBSERVACIONES
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="center" style="background-color: LightBlue; color: Black;">
                                                CONTINUA
                                            </td>
                                            <td align="center" style="background-color: LightBlue; color: Black;">
                                                <asp:TextBox ID="txtContinuaHila" runat="server" class="input" MaxLength="200"></asp:TextBox>
                                            </td>
                                            <td align="center" style="background-color: LightBlue; color: Black;">
                                                <asp:TextBox ID="txtObContinuaHila" runat="server" class="input" MaxLength="200"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="center" style="background-color: LightBlue; color: Black;">
                                                TORSIÓN (vueltas/m)
                                            </td>
                                            <td align="center" style="background-color: LightBlue; color: Black;">
                                                <asp:TextBox ID="txtTorsionHilaHila" runat="server" class="input" MaxLength="200"></asp:TextBox>
                                            </td>
                                            <td align="center" style="background-color: LightBlue; color: Black;">
                                                <asp:TextBox ID="txtObsTorsionHila" runat="server" class="input" MaxLength="200"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="center" style="background-color: LightBlue; color: Black;">
                                                ESTIRAJE
                                            </td>
                                            <td align="center" style="background-color: LightBlue; color: Black;">
                                                <asp:TextBox ID="txtEstirajeHila" runat="server" class="input" MaxLength="200"></asp:TextBox>
                                            </td>
                                            <td align="center" style="background-color: LightBlue; color: Black;">
                                                <asp:TextBox ID="txtObsEstirajeHila" runat="server" class="input" MaxLength="200"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="center" style="background-color: LightBlue; color: Black;">
                                                VELOCIDAD (RPM)
                                            </td>
                                            <td align="center" style="background-color: LightBlue; color: Black;">
                                                <asp:TextBox ID="txtVelocidadHila" runat="server" class="input" MaxLength="200"></asp:TextBox>
                                            </td>
                                            <td align="center" style="background-color: LightBlue; color: Black;">
                                                <asp:TextBox ID="txtObsVelocidadHila" runat="server" class="input" MaxLength="200"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="center" style="background-color: LightBlue; color: Black;">
                                                CURSORES
                                            </td>
                                            <td align="center" style="background-color: LightBlue; color: Black;">
                                                <asp:TextBox ID="txtCursoresHila" runat="server" class="input" MaxLength="200"></asp:TextBox>
                                            </td>
                                            <td align="center" style="background-color: LightBlue; color: Black;">
                                                <asp:TextBox ID="txtObsCursoresHila" runat="server" class="input" MaxLength="200"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="center" style="background-color: LightBlue; color: Black;">
                                                CLIPS
                                            </td>
                                            <td align="center" style="background-color: LightBlue; color: Black;">
                                                <asp:TextBox ID="txtClipHila" runat="server" class="input" MaxLength="200"></asp:TextBox>
                                            </td>
                                            <td align="center" style="background-color: LightBlue; color: Black;">
                                                <asp:TextBox ID="txtObsClipHila" runat="server" class="input" MaxLength="200"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="center" style="background-color: LightBlue; color: Black;">
                                                HUMEDAD RELATIVA DE SALA (%)
                                            </td>
                                            <td align="center" style="background-color: LightBlue; color: Black;">
                                                <asp:TextBox ID="txtHumedadHila" runat="server" class="input" MaxLength="200"></asp:TextBox>
                                            </td>
                                            <td align="center" style="background-color: LightBlue; color: Black;">
                                                <asp:TextBox ID="txtObsHumedadHila" runat="server" class="input" MaxLength="200"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="center" style="background-color: LightBlue; color: Black;">
                                                TEMPERATURA DE SALA (°C)
                                            </td>
                                            <td align="center" style="background-color: LightBlue; color: Black;">
                                                <asp:TextBox ID="txtTemperaturaHila" runat="server" class="input" MaxLength="200"></asp:TextBox>
                                            </td>
                                            <td align="center" style="background-color: LightBlue; color: Black;">
                                                <asp:TextBox ID="txtObsTemperaturaHila" runat="server" class="input" MaxLength="200"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="center" style="background-color: LightBlue; color: Black;">
                                                OTROS
                                            </td>
                                            <td align="center" style="background-color: LightBlue; color: Black;">
                                                <asp:TextBox ID="txtOtrosHila" runat="server" class="input" MaxLength="200"></asp:TextBox>
                                            </td>
                                            <td align="center" style="background-color: LightBlue; color: Black;">
                                                <asp:TextBox ID="txtObsOtrosHila" runat="server" class="input" MaxLength="200"></asp:TextBox>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                                <td>
                                    <table align="center" border="0" cellpadding="0" cellspacing="0" style="margin: 5px;">
                                        <tr align="center" class="RowHead">
                                            <td>
                                                PROCESO DE HILATURA
                                            </td>
                                            <td>
                                                VALORES
                                            </td>
                                            <td>
                                                OBSERVACIONES
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="center" style="background-color: LightBlue; color: Black;">
                                                OPEN RIETER
                                            </td>
                                            <td align="center" style="background-color: LightBlue; color: Black;">
                                                <asp:TextBox ID="txtOpenRieterHila2" runat="server" class="input" MaxLength="200"></asp:TextBox>
                                            </td>
                                            <td align="center" style="background-color: LightBlue; color: Black;">
                                                <asp:TextBox ID="txtObsOpenRieterHila2" runat="server" class="input" MaxLength="200"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="center" style="background-color: LightBlue; color: Black;">
                                                TORSIÓN (vueltas/m)
                                            </td>
                                            <td align="center" style="background-color: LightBlue; color: Black;">
                                                <asp:TextBox ID="txtTorsionHila2" runat="server" class="input" MaxLength="200"></asp:TextBox>
                                            </td>
                                            <td align="center" style="background-color: LightBlue; color: Black;">
                                                <asp:TextBox ID="txtObsTorsionHila2" runat="server" class="input" MaxLength="200"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="center" style="background-color: LightBlue; color: Black;">
                                                TENSIÓN (cN)
                                            </td>
                                            <td align="center" style="background-color: LightBlue; color: Black;">
                                                <asp:TextBox ID="txtTensionHila2" runat="server" class="input" MaxLength="200"></asp:TextBox>
                                            </td>
                                            <td align="center" style="background-color: LightBlue; color: Black;">
                                                <asp:TextBox ID="txtObsTensionHila2" runat="server" class="input" MaxLength="200"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="center" style="background-color: LightBlue; color: Black;">
                                                ESTIRAJE
                                            </td>
                                            <td align="center" style="background-color: LightBlue; color: Black;">
                                                <asp:TextBox ID="txtEstirajeHila2" runat="server" class="input" MaxLength="200"></asp:TextBox>
                                            </td>
                                            <td align="center" style="background-color: LightBlue; color: Black;">
                                                <asp:TextBox ID="tstObsEstirajeHila2" runat="server" class="input" MaxLength="200"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="center" style="background-color: LightBlue; color: Black;">
                                                VELOCIDAD DEL ROTOR (RPM)
                                            </td>
                                            <td align="center" style="background-color: LightBlue; color: Black;">
                                                <asp:TextBox ID="txtVelocidadRotorHila2" runat="server" class="input" MaxLength="200"></asp:TextBox>
                                            </td>
                                            <td align="center" style="background-color: LightBlue; color: Black;">
                                                <asp:TextBox ID="txtObsVelocidadRotorHila2" runat="server" class="input" MaxLength="200"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="center" style="background-color: LightBlue; color: Black;">
                                                VELOCIDAD DEL DISGREGADOR (RPM)
                                            </td>
                                            <td align="center" style="background-color: LightBlue; color: Black;">
                                                <asp:TextBox ID="txtVelocidadDisgregadorHila2" runat="server" class="input" MaxLength="200"></asp:TextBox>
                                            </td>
                                            <td align="center" style="background-color: LightBlue; color: Black;">
                                                <asp:TextBox ID="txtObsVelocidadDisgregadorHila2" runat="server" class="input" MaxLength="200"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="center" style="background-color: LightBlue; color: Black;">
                                                HUMEDAD RELATIVA DE SALA (%)
                                            </td>
                                            <td align="center" style="background-color: LightBlue; color: Black;">
                                                <asp:TextBox ID="txtHumedadRelativaSalaHila2" runat="server" class="input" MaxLength="200"></asp:TextBox>
                                            </td>
                                            <td align="center" style="background-color: LightBlue; color: Black;">
                                                <asp:TextBox ID="txtObsHumedadRelativaSalaHila2" runat="server" class="input" MaxLength="200"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="center" style="background-color: LightBlue; color: Black;">
                                                TEMPERATURA DE SALA (°C)
                                            </td>
                                            <td align="center" style="background-color: LightBlue; color: Black;">
                                                <asp:TextBox ID="txtTemperaturaSalaHila2" runat="server" class="input" MaxLength="200"></asp:TextBox>
                                            </td>
                                            <td align="center" style="background-color: LightBlue; color: Black;">
                                                <asp:TextBox ID="txtObsTemperaturaSalaHila2" runat="server" class="input" MaxLength="200"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="center" style="background-color: LightBlue; color: Black;">
                                                OTROS
                                            </td>
                                            <td align="center" style="background-color: LightBlue; color: Black;">
                                                <asp:TextBox ID="txtOtrosHila2" runat="server" class="input" MaxLength="200"></asp:TextBox>
                                            </td>
                                            <td align="center" style="background-color: LightBlue; color: Black;">
                                                <asp:TextBox ID="txtObsOtrosHila2" runat="server" class="input" MaxLength="200"></asp:TextBox>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2">
                                    <table align="center" border="1" cellpadding="0" cellspacing="0">
                                        <tr>
                                            <td align="center" style="background-color: LightBlue; color: Black;">
                                                FECHA DE PRODUCCIÓN
                                            </td>
                                            <td align="left" class="Etiqueta" style="width: 150px; height: 20px" valign="middle">
                                                <asp:TextBox ID="txtFechaProduccionBobi" runat="server" CssClass="input" Width="80px"></asp:TextBox>
                                                &nbsp;<img onclick="popUpCalendar(this, form1.txtFechaProduccionBobi, 'dd/mm/yyyy')"
                                                    height="16" width="16" alt="Seleccionar fecha de Inst." src="../images/Calendario.gif"
                                                    border="0" />
                                            </td>
                                            <td align="center" style="background-color: LightBlue; color: Black;">
                                                MÁQUINA
                                            </td>
                                            <td align="center" style="background-color: LightBlue; color: Black;">
                                                <asp:TextBox ID="txtMaquinaBobi" runat="server" class="input" MaxLength="200"></asp:TextBox>
                                            </td>
                                            <td align="center" style="background-color: LightBlue; color: Black;">
                                                SUPERVISOR
                                            </td>
                                            <td align="center" style="background-color: LightBlue; color: Black;">
                                                <asp:TextBox ID="txtSupervisorBobi" runat="server" class="input" MaxLength="200"></asp:TextBox>
                                            </td>
                                            <td align="center" style="background-color: LightBlue; color: Black;">
                                                OPERARIO
                                            </td>
                                            <td align="center" style="background-color: LightBlue; color: Black;">
                                                <asp:TextBox ID="txtOperarioBobi" runat="server" class="input" MaxLength="200"></asp:TextBox>
                                            </td>
                                            <td align="center" style="background-color: LightBlue; color: Black;">
                                                TURNO
                                            </td>
                                            <td align="center" style="background-color: LightBlue; color: Black;">
                                                <asp:TextBox ID="txtTurnoBobi" runat="server" class="input" MaxLength="200"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="center" style="background-color: LightBlue; color: Black;">
                                                LÍNEA
                                            </td>
                                            <td align="center" style="background-color: LightBlue; color: Black;">
                                                <asp:TextBox ID="txtLineaBobi" runat="server" class="input" MaxLength="200"></asp:TextBox>
                                            </td>
                                            <td align="center" style="background-color: LightBlue; color: Black;">
                                                PROCESO
                                            </td>
                                            <td align="center" style="background-color: LightBlue; color: Black;">
                                                <asp:TextBox ID="txtProcesoBobi" runat="server" class="input" MaxLength="200"></asp:TextBox>
                                            </td>
                                            <td align="center" style="background-color: LightBlue; color: Black;">
                                                MATERIAL
                                            </td>
                                            <td align="center" style="background-color: LightBlue; color: Black;">
                                                <asp:TextBox ID="txtMaterialBobi" runat="server" class="input" MaxLength="200"></asp:TextBox>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <table align="center" border="0" cellpadding="0" cellspacing="0" style="margin: 5px;">
                                        <tr align="center" class="RowHead">
                                            <td>
                                                PROCESO DE BOBINADO
                                            </td>
                                            <td>
                                                VALORES
                                            </td>
                                            <td>
                                                OBSERVACIONES
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="center" style="background-color: LightBlue; color: Black;">
                                                CONERA
                                            </td>
                                            <td align="center" style="background-color: LightBlue; color: Black;">
                                                <asp:TextBox ID="txtCONERABobinado" runat="server" class="input" MaxLength="200"></asp:TextBox>
                                            </td>
                                            <td align="center" style="background-color: LightBlue; color: Black;">
                                                <asp:TextBox ID="txtObsCONERABobinado" runat="server" class="input" MaxLength="200"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="center" style="background-color: LightBlue; color: Black;">
                                                TENSIÓN (cN)
                                            </td>
                                            <td align="center" style="background-color: LightBlue; color: Black;">
                                                <asp:TextBox ID="txtTENSIONBobinado" runat="server" class="input" MaxLength="200"></asp:TextBox>
                                            </td>
                                            <td align="center" style="background-color: LightBlue; color: Black;">
                                                <asp:TextBox ID="txtObsTENSIONBobinado" runat="server" class="input" MaxLength="200"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="center" style="background-color: LightBlue; color: Black;">
                                                LONGITUD (m)
                                            </td>
                                            <td align="center" style="background-color: LightBlue; color: Black;">
                                                <asp:TextBox ID="txtLONGITUDBobinado" runat="server" class="input" MaxLength="200"></asp:TextBox>
                                            </td>
                                            <td align="center" style="background-color: LightBlue; color: Black;">
                                                <asp:TextBox ID="txtObsLONGITUDBobinado" runat="server" class="input" MaxLength="200"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="center" style="background-color: LightBlue; color: Black;">
                                                VELOCIDAD (m/min)
                                            </td>
                                            <td align="center" style="background-color: LightBlue; color: Black;">
                                                <asp:TextBox ID="txtVELOCIDADBobinado" runat="server" class="input" MaxLength="200"></asp:TextBox>
                                            </td>
                                            <td align="center" style="background-color: LightBlue; color: Black;">
                                                <asp:TextBox ID="txtObsVELOCIDADBobinado" runat="server" class="input" MaxLength="200"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="center" style="background-color: LightBlue; color: Black;">
                                                PRESIÓN DEL MARCO PORTABOBINA (cN)
                                            </td>
                                            <td align="center" style="background-color: LightBlue; color: Black;">
                                                <asp:TextBox ID="txtPRESIÓNMARCOBobinado" runat="server" class="input" MaxLength="200"></asp:TextBox>
                                            </td>
                                            <td align="center" style="background-color: LightBlue; color: Black;">
                                                <asp:TextBox ID="txtObsPRESIÓNMARCOBobinado" runat="server" class="input" MaxLength="200"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="center" style="background-color: LightBlue; color: Black;">
                                                HUMEDAD RELATIVA DE SALA (%)
                                            </td>
                                            <td align="center" style="background-color: LightBlue; color: Black;">
                                                <asp:TextBox ID="TextBtxtHUMEDADRELATIVABobinado" runat="server" class="input" MaxLength="200"></asp:TextBox>
                                            </td>
                                            <td align="center" style="background-color: LightBlue; color: Black;">
                                                <asp:TextBox ID="txtObsBtxtHUMEDADRELATIVABobinado" runat="server" class="input"
                                                    MaxLength="200"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="center" style="background-color: LightBlue; color: Black;">
                                                TEMPERATURA DE SALA (°C)
                                            </td>
                                            <td align="center" style="background-color: LightBlue; color: Black;">
                                                <asp:TextBox ID="txtTemperaturaSalaBObinado" runat="server" class="input" MaxLength="200"></asp:TextBox>
                                            </td>
                                            <td align="center" style="background-color: LightBlue; color: Black;">
                                                <asp:TextBox ID="txtObsTemperaturaSalaBObinado" runat="server" class="input" MaxLength="200"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="center" style="background-color: LightBlue; color: Black;">
                                                OTROS
                                            </td>
                                            <td align="center" style="background-color: LightBlue; color: Black;">
                                                <asp:TextBox ID="txtOtrosBobinado" runat="server" class="input" MaxLength="200"></asp:TextBox>
                                            </td>
                                            <td align="center" style="background-color: LightBlue; color: Black;">
                                                <asp:TextBox ID="txtObsOtrosBobinado" runat="server" class="input" MaxLength="200"></asp:TextBox>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                                <td>
                                    <table align="center" border="0" cellpadding="0" cellspacing="0" style="margin: 5px;">
                                        <%-- <tr align="center" class="RowHead">
                        <td>
                            PROCESO DE BOBINADO
                        </td>
                        <td>
                            VALORES
                        </td>
                        <td>
                            OBSERVACIONES
                        </td>
                    </tr>--%><%--  <tr>
                        <td style="background-color: LightBlue; color: Black;" align="center">
                            CONERA
                        </td>
                        <td style="background-color: LightBlue; color: Black;" align="center">
                            <asp:TextBox ID="txtCONERABobinado2" runat="server" class="input" MaxLength="200"></asp:TextBox>
                        </td>
                        <td style="background-color: LightBlue; color: Black;" align="center">
                            <asp:TextBox ID="txtObsCONERABobinado2" runat="server" class="input" MaxLength="200"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td style="background-color: LightBlue; color: Black;" align="center">
                            TENSIÓN
                        </td>
                        <td style="background-color: LightBlue; color: Black;" align="center">
                            <asp:TextBox ID="txtTENSIONBobinado2" runat="server" class="input" MaxLength="200"></asp:TextBox>
                        </td>
                        <td style="background-color: LightBlue; color: Black;" align="center">
                            <asp:TextBox ID="txtObsTENSIONBobinado2" runat="server" class="input" MaxLength="200"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td style="background-color: LightBlue; color: Black;" align="center">
                            LONGITUD
                        </td>
                        <td style="background-color: LightBlue; color: Black;" align="center">
                            <asp:TextBox ID="txtLONGITUDBobinado2" runat="server" class="input" MaxLength="200"></asp:TextBox>
                        </td>
                        <td style="background-color: LightBlue; color: Black;" align="center">
                            <asp:TextBox ID="txtObsLONGITUDBobinado2" runat="server" class="input" MaxLength="200"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td style="background-color: LightBlue; color: Black;" align="center">
                            VELOCIDAD
                        </td>
                        <td style="background-color: LightBlue; color: Black;" align="center">
                            <asp:TextBox ID="txtVELOCIDADBobinado2" runat="server" class="input" MaxLength="200"></asp:TextBox>
                        </td>
                        <td style="background-color: LightBlue; color: Black;" align="center">
                            <asp:TextBox ID="txtObsVELOCIDADBobinado2" runat="server" class="input" MaxLength="200"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td style="background-color: LightBlue; color: Black;" align="center">
                            PRESIÓN DEL MARCO PORTABOBINA (cN)
                        </td>
                        <td style="background-color: LightBlue; color: Black;" align="center">
                            <asp:TextBox ID="txtPRESIÓNMARCOBobinado2" runat="server" class="input" MaxLength="200"></asp:TextBox>
                        </td>
                        <td style="background-color: LightBlue; color: Black;" align="center">
                            <asp:TextBox ID="txtObsPRESIÓNMARCOBobinado2" runat="server" class="input" MaxLength="200"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td style="background-color: LightBlue; color: Black;" align="center">
                            % HUMEDAD RELATIVA DE SALA
                        </td>
                        <td style="background-color: LightBlue; color: Black;" align="center">
                            <asp:TextBox ID="TextBtxtHUMEDADRELATIVABobinado2" runat="server" class="input" MaxLength="200"></asp:TextBox>
                        </td>
                        <td style="background-color: LightBlue; color: Black;" align="center">
                            <asp:TextBox ID="txtObsBtxtHUMEDADRELATIVABobinado2" runat="server" class="input"
                                MaxLength="200"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td style="background-color: LightBlue; color: Black;" align="center">
                            TEMPERATURA DE SALA
                        </td>
                        <td style="background-color: LightBlue; color: Black;" align="center">
                            <asp:TextBox ID="txtTemperaturaSalaBObinado2" runat="server" class="input" MaxLength="200"></asp:TextBox>
                        </td>
                        <td style="background-color: LightBlue; color: Black;" align="center">
                            <asp:TextBox ID="txtObsTemperaturaSalaBObinado2" runat="server" class="input" MaxLength="200"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td style="background-color: LightBlue; color: Black;" align="center">
                            OTROS
                        </td>
                        <td style="background-color: LightBlue; color: Black;" align="center">
                            <asp:TextBox ID="txtOtrosBobinado2" runat="server" class="input" MaxLength="200"></asp:TextBox>
                        </td>
                        <td style="background-color: LightBlue; color: Black;" align="center">
                            <asp:TextBox ID="txtObsOtrosBobinado2" runat="server" class="input" MaxLength="200"></asp:TextBox>
                        </td>
                    </tr>--%>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2">
                                    <table align="center" border="1" cellpadding="0" cellspacing="0">
                                        <tr>
                                            <td align="center" style="background-color: LightBlue; color: Black;">
                                                FECHA DE PRODUCCIÓN
                                            </td>
                                            <td align="left" class="Etiqueta" style="width: 150px; height: 20px" valign="middle">
                                                <asp:TextBox ID="txtFechaProduccionRecu" runat="server" CssClass="input" Width="80px"></asp:TextBox>
                                                &nbsp;<img onclick="popUpCalendar(this, form1.txtFechaProduccionRecu, 'dd/mm/yyyy')"
                                                    height="16" width="16" alt="Seleccionar fecha de Inst." src="../images/Calendario.gif"
                                                    border="0" />
                                            </td>
                                            <td align="center" style="background-color: LightBlue; color: Black;">
                                                MÁQUINA
                                            </td>
                                            <td align="center" style="background-color: LightBlue; color: Black;">
                                                <asp:TextBox ID="txtMaquinaRecu" runat="server" class="input" MaxLength="200"></asp:TextBox>
                                            </td>
                                            <td align="center" style="background-color: LightBlue; color: Black;">
                                                SUPERVISOR
                                            </td>
                                            <td align="center" style="background-color: LightBlue; color: Black;">
                                                <asp:TextBox ID="txtSupervisorRecu" runat="server" class="input" MaxLength="200"></asp:TextBox>
                                            </td>
                                            <td align="center" style="background-color: LightBlue; color: Black;">
                                                OPERARIO
                                            </td>
                                            <td align="center" style="background-color: LightBlue; color: Black;">
                                                <asp:TextBox ID="txtOperarioRecu" runat="server" class="input" MaxLength="200"></asp:TextBox>
                                            </td>
                                            <td align="center" style="background-color: LightBlue; color: Black;">
                                                TURNO
                                            </td>
                                            <td align="center" style="background-color: LightBlue; color: Black;">
                                                <asp:TextBox ID="txtTurnoRecu" runat="server" class="input" MaxLength="200"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="center" style="background-color: LightBlue; color: Black;">
                                                LÍNEA
                                            </td>
                                            <td align="center" style="background-color: LightBlue; color: Black;">
                                                <asp:TextBox ID="txtLineaRecu" runat="server" class="input" MaxLength="200"></asp:TextBox>
                                            </td>
                                            <td align="center" style="background-color: LightBlue; color: Black;">
                                                PROCESO
                                            </td>
                                            <td align="center" style="background-color: LightBlue; color: Black;">
                                                <asp:TextBox ID="txtProcesoRecu" runat="server" class="input" MaxLength="200"></asp:TextBox>
                                            </td>
                                            <td align="center" style="background-color: LightBlue; color: Black;">
                                                MATERIAL
                                            </td>
                                            <td align="center" style="background-color: LightBlue; color: Black;">
                                                <asp:TextBox ID="txtMaterialRecu" runat="server" class="input" MaxLength="200"></asp:TextBox>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <table align="center" border="0" cellpadding="0" cellspacing="0" style="margin: 5px;">
                                        <tr align="center" class="RowHead">
                                            <td>
                                                PROCESO DE RECUBIERTO
                                            </td>
                                            <td>
                                                VALORES
                                            </td>
                                            <td>
                                                OBSERVACIONES
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="center" style="background-color: LightBlue; color: Black;">
                                                RECUBRIDORA
                                            </td>
                                            <td align="center" style="background-color: LightBlue; color: Black;">
                                                <asp:TextBox ID="txtRecubridoraRecubierto" runat="server" class="input" MaxLength="200"></asp:TextBox>
                                            </td>
                                            <td align="center" style="background-color: LightBlue; color: Black;">
                                                <asp:TextBox ID="txtObsRecubridoraRecubierto" runat="server" class="input" MaxLength="200"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="center" style="background-color: LightBlue; color: Black;">
                                                VELOCIDAD DE LA RECUBRIDORA (m/min)
                                            </td>
                                            <td align="center" style="background-color: LightBlue; color: Black;">
                                                <asp:TextBox ID="txtVelocidadRecubridoraRecubierto" runat="server" class="input"
                                                    MaxLength="200"></asp:TextBox>
                                            </td>
                                            <td align="center" style="background-color: LightBlue; color: Black;">
                                                <asp:TextBox ID="txtObsVelocidadRecubridoraRecubierto" runat="server" class="input"
                                                    MaxLength="200"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="center" style="background-color: LightBlue; color: Black;">
                                                RECETA
                                            </td>
                                            <td align="center" style="background-color: LightBlue; color: Black;">
                                                <asp:TextBox ID="txtRecetaRecubierto" runat="server" class="input" MaxLength="200"></asp:TextBox>
                                            </td>
                                            <td align="center" style="background-color: LightBlue; color: Black;">
                                                <asp:TextBox ID="txtObsRecetaRecubierto" runat="server" class="input" MaxLength="200"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="center" style="background-color: LightBlue; color: Black;">
                                                OTROS
                                            </td>
                                            <td align="center" style="background-color: LightBlue; color: Black;">
                                                <asp:TextBox ID="txtOtrosRecubierto" runat="server" class="input" MaxLength="200"></asp:TextBox>
                                            </td>
                                            <td align="center" style="background-color: LightBlue; color: Black;">
                                                <asp:TextBox ID="txtObsOtrosRecubierto" runat="server" class="input" MaxLength="200"></asp:TextBox>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                                <td>
                                    <table align="center" border="0" cellpadding="0" cellspacing="0" style="margin: 5px;">
                                        <tr align="center" class="RowHead">
                                            <td>
                                                PROCESO DE RECUBIERTO
                                            </td>
                                            <td>
                                                VALORES
                                            </td>
                                            <td>
                                                OBSERVACIONES
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="center" style="background-color: LightBlue; color: Black;">
                                                RECUBRIDORA
                                            </td>
                                            <td align="center" style="background-color: LightBlue; color: Black;">
                                                <asp:TextBox ID="txtRecubridoraRecubierto2" runat="server" class="input" MaxLength="200"></asp:TextBox>
                                            </td>
                                            <td align="center" style="background-color: LightBlue; color: Black;">
                                                <asp:TextBox ID="txtObsRecubridoraRecubierto2" runat="server" class="input" MaxLength="200"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="center" style="background-color: LightBlue; color: Black;">
                                                VELOCIDAD DE LA RECUBRIDORA (RPM)
                                            </td>
                                            <td align="center" style="background-color: LightBlue; color: Black;">
                                                <asp:TextBox ID="txtVelocidadRecubridoraRecubierto2" runat="server" class="input"
                                                    MaxLength="200"></asp:TextBox>
                                            </td>
                                            <td align="center" style="background-color: LightBlue; color: Black;">
                                                <asp:TextBox ID="txtObsVelocidadRecubridoraRecubierto2" runat="server" class="input"
                                                    MaxLength="200"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="center" style="background-color: LightBlue; color: Black;">
                                                RECETA
                                            </td>
                                            <td align="center" style="background-color: LightBlue; color: Black;">
                                                <asp:TextBox ID="txtRecetaRecubierto2" runat="server" class="input" MaxLength="200"></asp:TextBox>
                                            </td>
                                            <td align="center" style="background-color: LightBlue; color: Black;">
                                                <asp:TextBox ID="txtObsRecetaRecubierto2" runat="server" class="input" MaxLength="200"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="center" style="background-color: LightBlue; color: Black;">
                                                OTROS
                                            </td>
                                            <td align="center" style="background-color: LightBlue; color: Black;">
                                                <asp:TextBox ID="txtOtrosRecubierto2" runat="server" class="input" MaxLength="200"></asp:TextBox>
                                            </td>
                                            <td align="center" style="background-color: LightBlue; color: Black;">
                                                <asp:TextBox ID="txtObsOtrosRecubierto2" runat="server" class="input" MaxLength="200"></asp:TextBox>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Button ID="btnHilanderia" runat="server" CssClass="BotonVerde" Text="Guardar" />
                                </td>
                                <td>
                                    <asp:Button ID="btnTerminoHilanderia" runat="server" CssClass="btnAzul" Text="Concluir Proceso" />
                                </td>
                            </tr>
                        </table>
                        <table id="TablaLaboratorioHIlanderia" runat="server" border="0" cellpadding="0"
                            cellspacing="0" class="ttable" style="width: 924px">
                            <tr>
                                <td class="RowHead" rowspan="12">
                                    LABORATORIO DE HILANDERÍA
                                </td>
                                <td colspan="2">
                                    <table align="center" border="1" cellpadding="0" cellspacing="0">
                                        <tr>
                                            <td align="center" style="background-color: LightBlue; color: Black;">
                                                FECHA DE EVALUACION
                                            </td>
                                            <td align="left" class="Etiqueta" style="width: 150px; height: 20px" valign="middle">
                                                <asp:TextBox ID="txtFechaEvaluacionHilande" runat="server" CssClass="input" Width="80px"></asp:TextBox><!--date-->
                                                &nbsp;<img onclick="popUpCalendar(this, form1.txtFechaEvaluacionHilande, 'dd/mm/yyyy')"
                                                    height="16" width="16" alt="Seleccionar fecha de Inst." src="../images/Calendario.gif"
                                                    border="0" />
                                            </td>
                                            <td align="center" style="background-color: LightBlue; color: Black;">
                                                LABORATORISTA
                                            </td>
                                            <td align="center" style="background-color: LightBlue; color: Black;">
                                                <asp:TextBox ID="txtLaboratoristaHilande" runat="server" class="input" MaxLength="200"></asp:TextBox>
                                            </td>
                                            <td align="center" style="background-color: LightBlue; color: Black;">
                                                MAQUINA
                                            </td>
                                            <td align="center" style="background-color: LightBlue; color: Black;">
                                                <asp:TextBox ID="txtMaquinaHilande" runat="server" class="input" MaxLength="200"></asp:TextBox>
                                            </td>
                                            <td align="center" style="background-color: LightBlue; color: Black;">
                                                TURNO
                                            </td>
                                            <td align="center" style="background-color: LightBlue; color: Black;">
                                                <asp:TextBox ID="txtTurnoHilande" runat="server" class="input" MaxLength="200"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="center" style="background-color: LightBlue; color: Black;">
                                                LÍNEA
                                            </td>
                                            <td align="center" style="background-color: LightBlue; color: Black;">
                                                <asp:TextBox ID="txtLineaHilande" runat="server" class="input" MaxLength="200"></asp:TextBox>
                                            </td>
                                            <td align="center" style="background-color: LightBlue; color: Black;">
                                                PROCESO
                                            </td>
                                            <td align="center" style="background-color: LightBlue; color: Black;">
                                                <asp:TextBox ID="txtProcesoHilande" runat="server" class="input" MaxLength="200"></asp:TextBox>
                                            </td>
                                            <td align="center" style="background-color: LightBlue; color: Black;">
                                                MATERIAL
                                            </td>
                                            <td align="center" style="background-color: LightBlue; color: Black;">
                                                <asp:TextBox ID="txtMaterialHilande" runat="server" class="input" MaxLength="200"></asp:TextBox>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <table align="center" border="0" cellpadding="0" cellspacing="0" style="margin: 5px;">
                                        <tr align="center" class="RowHead">
                                            <td>
                                                EVALUACIÓN DE LA HILATURA
                                            </td>
                                            <td>
                                                VALORES
                                            </td>
                                            <td>
                                                OBSERVACIONES
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="center" style="background-color: LightBlue; color: Black;">
                                                FINURA DE FIBRA (Mic)
                                            </td>
                                            <td align="center" style="background-color: LightBlue; color: Black;">
                                                <asp:TextBox ID="txtFINURAFIBRAHilande" runat="server" class="input" MaxLength="200"></asp:TextBox>
                                            </td>
                                            <td align="center" style="background-color: LightBlue; color: Black;">
                                                <asp:TextBox ID="txtObsFINURAFIBRAHilande" runat="server" class="input" MaxLength="200"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="center" style="background-color: LightBlue; color: Black;">
                                                RESISTENCIA DE FIBRA (g/tex)
                                            </td>
                                            <td align="center" style="background-color: LightBlue; color: Black;">
                                                <asp:TextBox ID="txRESISTENCIAFIBRAHilande" runat="server" class="input" MaxLength="200"></asp:TextBox>
                                            </td>
                                            <td align="center" style="background-color: LightBlue; color: Black;">
                                                <asp:TextBox ID="txObsRESISTENCIAFIBRAHilande" runat="server" class="input" MaxLength="200"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="center" style="background-color: LightBlue; color: Black;">
                                                LONGITUD DE FIBRA (mm)
                                            </td>
                                            <td align="center" style="background-color: LightBlue; color: Black;">
                                                <asp:TextBox ID="txtLONGITUDFIBRAHilande" runat="server" class="input" MaxLength="200"></asp:TextBox>
                                            </td>
                                            <td align="center" style="background-color: LightBlue; color: Black;">
                                                <asp:TextBox ID="txtObsLONGITUDFIBRAHilande" runat="server" class="input" MaxLength="200"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="center" style="background-color: LightBlue; color: Black;">
                                                UNIFORMIDAD DE FIBRA (%)
                                            </td>
                                            <td align="center" style="background-color: LightBlue; color: Black;">
                                                <asp:TextBox ID="txtUNIFORMIDADFIBRAHilande" runat="server" class="input" MaxLength="200"></asp:TextBox>
                                            </td>
                                            <td align="center" style="background-color: LightBlue; color: Black;">
                                                <asp:TextBox ID="txtObsUNIFORMIDADFIBRAHilande" runat="server" class="input" MaxLength="200"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="center" style="background-color: LightBlue; color: Black;">
                                                TÍTULO DE CINTA / HILO
                                            </td>
                                            <td align="center" style="background-color: LightBlue; color: Black;">
                                                <asp:TextBox ID="txtTÍTULOCINTAHILOHilande" runat="server" class="input" MaxLength="200"></asp:TextBox>
                                            </td>
                                            <td align="center" style="background-color: LightBlue; color: Black;">
                                                <asp:TextBox ID="txtObsTÍTULOCINTAHILOHilande" runat="server" class="input" MaxLength="200"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="center" style="background-color: LightBlue; color: Black;">
                                                Cv DE TÍTULO (Nec)
                                            </td>
                                            <td align="center" style="background-color: LightBlue; color: Black;">
                                                <asp:TextBox ID="txtCvTÍTULOHilande" runat="server" class="input" MaxLength="200"></asp:TextBox>
                                            </td>
                                            <td align="center" style="background-color: LightBlue; color: Black;">
                                                <asp:TextBox ID="txtObsCvTÍTULOHilande" runat="server" class="input" MaxLength="200"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="center" style="background-color: LightBlue; color: Black;">
                                                PRUEBA DE UNIFORMIDAD DE LA CINTA / HILO (CVm)
                                            </td>
                                            <td align="center" style="background-color: LightBlue; color: Black;">
                                                <asp:TextBox ID="txtPRUEBAUNIFORMIDADCINTAHILOHilande" runat="server" class="input"
                                                    MaxLength="200"></asp:TextBox>
                                            </td>
                                            <td align="center" style="background-color: LightBlue; color: Black;">
                                                <asp:TextBox ID="txtObsPRUEBAUNIFORMIDADCINTAHILOHilande" runat="server" class="input"
                                                    MaxLength="200"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="center" style="background-color: LightBlue; color: Black;">
                                                PRUEBA DE UNIFORMIDAD DE LA CINTA / HILO (P.D.)
                                            </td>
                                            <td align="center" style="background-color: LightBlue; color: Black;">
                                                <asp:TextBox ID="txtPRUEBAUNIFORMIDADCINTAPDHilande" runat="server" class="input"
                                                    MaxLength="200"></asp:TextBox>
                                            </td>
                                            <td align="center" style="background-color: LightBlue; color: Black;">
                                                <asp:TextBox ID="txtObsPRUEBAUNIFORMIDADCINTAPDHilande" runat="server" class="input"
                                                    MaxLength="200"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="center" style="background-color: LightBlue; color: Black;">
                                                PRUEBA DE UNIFORMIDAD DE LA CINTA / HILO (P.G.)
                                            </td>
                                            <td align="center" style="background-color: LightBlue; color: Black;">
                                                <asp:TextBox ID="txtPRUEBAUNIFORMIDADCINTAPGHilande" runat="server" class="input"
                                                    MaxLength="200"></asp:TextBox>
                                            </td>
                                            <td align="center" style="background-color: LightBlue; color: Black;">
                                                <asp:TextBox ID="txtObsPRUEBAUNIFORMIDADCINTAPGHilande" runat="server" class="input"
                                                    MaxLength="200"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="center" style="background-color: LightBlue; color: Black;">
                                                PRUEBA DE UNIFORMIDAD DE LA CINTA / HILO (NEPS)
                                            </td>
                                            <td align="center" style="background-color: LightBlue; color: Black;">
                                                <asp:TextBox ID="txtPRUEBAUNIFORMIDADCINTANEPSHilande" runat="server" class="input"
                                                    MaxLength="200"></asp:TextBox>
                                            </td>
                                            <td align="center" style="background-color: LightBlue; color: Black;">
                                                <asp:TextBox ID="txtObsPRUEBAUNIFORMIDADCINTANEPSHilande" runat="server" class="input"
                                                    MaxLength="200"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="center" style="background-color: LightBlue; color: Black;">
                                                PRUEBA DE UNIFORMIDAD DE LA CINTA / HILO (H)
                                            </td>
                                            <td align="center" style="background-color: LightBlue; color: Black;">
                                                <asp:TextBox ID="txtPRUEBAUNIFORMIDADCINTAHHilande" runat="server" class="input"
                                                    MaxLength="200"></asp:TextBox>
                                            </td>
                                            <td align="center" style="background-color: LightBlue; color: Black;">
                                                <asp:TextBox ID="txtObsPRUEBAUNIFORMIDADCINTAHHilande" runat="server" class="input"
                                                    MaxLength="200"></asp:TextBox>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                                <td>
                                    <table align="center" border="0" cellpadding="0" cellspacing="0" style="margin: 5px;">
                                        <tr align="center" class="RowHead">
                                            <td>
                                                EVALUACIÓN DE HILATURA
                                            </td>
                                            <td>
                                                VALORES
                                            </td>
                                            <td>
                                                OBSERVACIONES
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="center" style="background-color: LightBlue; color: Black;">
                                                RESISTENCIA DEL HILO (RKM)
                                            </td>
                                            <td align="center" style="background-color: LightBlue; color: Black;">
                                                <asp:TextBox ID="txtRESISTENCIAHILOHilande" runat="server" class="input" MaxLength="200"></asp:TextBox>
                                            </td>
                                            <td align="center" style="background-color: LightBlue; color: Black;">
                                                <asp:TextBox ID="txtObsRESISTENCIAHILOHilande" runat="server" class="input" MaxLength="200"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="center" style="background-color: LightBlue; color: Black;">
                                                RESISTENCIA MÍNIMA DEL HILO (RKM)
                                            </td>
                                            <td align="center" style="background-color: LightBlue; color: Black;">
                                                <asp:TextBox ID="txtRESISTENCIAMÍNIMAHILOHilande" runat="server" class="input" MaxLength="200"></asp:TextBox>
                                            </td>
                                            <td align="center" style="background-color: LightBlue; color: Black;">
                                                <asp:TextBox ID="txtObsRESISTENCIAMÍNIMAHILOHilande" runat="server" class="input"
                                                    MaxLength="200"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="center" style="background-color: LightBlue; color: Black;">
                                                Cv RESISTENCIA DEL HILO
                                            </td>
                                            <td align="center" style="background-color: LightBlue; color: Black;">
                                                <asp:TextBox ID="txtRESISTENCIALHILOHilande" runat="server" class="input" MaxLength="200"></asp:TextBox>
                                            </td>
                                            <td align="center" style="background-color: LightBlue; color: Black;">
                                                <asp:TextBox ID="txtObsRESISTENCIALHILOHilande" runat="server" class="input" MaxLength="200"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="center" style="background-color: LightBlue; color: Black;">
                                                ELONGACIÓN DEL HILO (%)
                                            </td>
                                            <td align="center" style="background-color: LightBlue; color: Black;">
                                                <asp:TextBox ID="txtELONGACIÓNLHILOHilande" runat="server" class="input" MaxLength="200"></asp:TextBox>
                                            </td>
                                            <td align="center" style="background-color: LightBlue; color: Black;">
                                                <asp:TextBox ID="txtObsELONGACIÓNLHILOHilande" runat="server" class="input" MaxLength="200"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="center" style="background-color: LightBlue; color: Black;">
                                                TORSIÓN DEL HILO (TPM)
                                            </td>
                                            <td align="center" style="background-color: LightBlue; color: Black;">
                                                <asp:TextBox ID="txtTORSIÓNHILOHilande" runat="server" class="input" MaxLength="200"></asp:TextBox>
                                            </td>
                                            <td align="center" style="background-color: LightBlue; color: Black;">
                                                <asp:TextBox ID="txtObsTORSIÓNHILOHilande" runat="server" class="input" MaxLength="200"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="center" style="background-color: LightBlue; color: Black;">
                                                ROTURAS 1,000 HUSOS / HORA
                                            </td>
                                            <td align="center" style="background-color: LightBlue; color: Black;">
                                                <asp:TextBox ID="txtROTURASHUSOsHORAHilande" runat="server" class="input" MaxLength="200"></asp:TextBox>
                                            </td>
                                            <td align="center" style="background-color: LightBlue; color: Black;">
                                                <asp:TextBox ID="txtObsROTURASHUSOsHORAHilande" runat="server" class="input" MaxLength="200"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="center" style="background-color: LightBlue; color: Black;">
                                                RESISTENCIA DE EMPALME (%)
                                            </td>
                                            <td align="center" style="background-color: LightBlue; color: Black;">
                                                <asp:TextBox ID="txtRESISTENCIAEMPALMEHilande" runat="server" class="input" MaxLength="200"></asp:TextBox>
                                            </td>
                                            <td align="center" style="background-color: LightBlue; color: Black;">
                                                <asp:TextBox ID="txtObsRESISTENCIAEMPALMEHilande" runat="server" class="input" MaxLength="200"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="center" class="style18" style="background-color: LightBlue; color: Black;">
                                                PESO DE CONO (gr)
                                            </td>
                                            <td align="center" class="style18" style="background-color: LightBlue; color: Black;">
                                                <asp:TextBox ID="txtPesoConoHilande" runat="server" class="input" MaxLength="200"></asp:TextBox>
                                            </td>
                                            <td align="center" class="style18" style="background-color: LightBlue; color: Black;">
                                                <asp:TextBox ID="txtObsPesoConoHilande" runat="server" class="input" MaxLength="200"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="center" class="style18" style="background-color: LightBlue; color: Black;">
                                                DUREZA DE CONO (N)
                                            </td>
                                            <td align="center" class="style18" style="background-color: LightBlue; color: Black;">
                                                <asp:TextBox ID="txtDUREZACONOHiland" runat="server" class="input" MaxLength="200"></asp:TextBox>
                                            </td>
                                            <td align="center" class="style18" style="background-color: LightBlue; color: Black;">
                                                <asp:TextBox ID="txtObsDUREZACONOHiland" runat="server" class="input" MaxLength="200"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="center" style="background-color: LightBlue; color: Black;">
                                                OTROS
                                            </td>
                                            <td align="center" style="background-color: LightBlue; color: Black;">
                                                <asp:TextBox ID="txtOtrosHilande" runat="server" class="input" MaxLength="200"></asp:TextBox>
                                            </td>
                                            <td align="center" style="background-color: LightBlue; color: Black;">
                                                <asp:TextBox ID="txtObsOtrosHilande" runat="server" class="input" MaxLength="200"></asp:TextBox>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Button ID="btnLaboratorioHilanderia" runat="server" CssClass="BotonVerde" Text="Guardar" />
                                </td>
                                <td>
                                    <asp:Button ID="btnTerminoLaboratorioHilanderia" runat="server" CssClass="btnAzul"
                                        Text="Concluir Proceso" />
                                </td>
                            </tr>
                        </table>
                        <table id="TablaPretejido" runat="server" border="0" cellpadding="0" cellspacing="0"
                            class="ttable" style="width: 924px">
                            <tr>
                                <td class="RowHead" rowspan="12">
                                    PRETEJIDO
                                </td>
                                <td colspan="2">
                                    <table align="center" border="1" cellpadding="0" cellspacing="0">
                                        <tr>
                                            <td align="center" style="background-color: LightBlue; color: Black;">
                                                FECHA DE PRODUCCIÓN
                                            </td>
                                            <td align="left" class="Etiqueta" style="width: 150px; height: 20px" valign="middle">
                                                <asp:TextBox ID="txtFechaProduccionPrete" runat="server" CssClass="input" Width="80px"></asp:TextBox><!--date-->
                                                &nbsp;<img onclick="popUpCalendar(this, form1.txtFechaProduccionPrete, 'dd/mm/yyyy')"
                                                    height="16" width="16" alt="Seleccionar fecha de Inst." src="../images/Calendario.gif"
                                                    border="0" />
                                            </td>
                                            <td align="center" style="background-color: LightBlue; color: Black;">
                                                MÁQUINA
                                            </td>
                                            <td align="center" style="background-color: LightBlue; color: Black;">
                                                <asp:TextBox ID="txtMaquinaPrete" runat="server" class="input" MaxLength="200"></asp:TextBox>
                                            </td>
                                            <td align="center" style="background-color: LightBlue; color: Black;">
                                                SUPERVISOR
                                            </td>
                                            <td align="center" style="background-color: LightBlue; color: Black;">
                                                <asp:TextBox ID="txtSupervisorPrete" runat="server" class="input" MaxLength="200"></asp:TextBox>
                                            </td>
                                            <td align="center" style="background-color: LightBlue; color: Black;">
                                                MAQUINISTA
                                            </td>
                                            <td align="center" style="background-color: LightBlue; color: Black;">
                                                <asp:TextBox ID="txtMaquinistaPrete" runat="server" class="input" MaxLength="200"></asp:TextBox>
                                            </td>
                                            <td align="center" style="background-color: LightBlue; color: Black;">
                                                TURNO
                                            </td>
                                            <td align="center" style="background-color: LightBlue; color: Black;">
                                                <asp:TextBox ID="txtTurnoPrete" runat="server" class="input" MaxLength="200"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="center" style="background-color: LightBlue; color: Black;">
                                                PARTIDA
                                            </td>
                                            <td align="center" style="background-color: LightBlue; color: Black;">
                                                <asp:TextBox ID="txtPartidaPrete" runat="server" class="input" MaxLength="200"></asp:TextBox>
                                            </td>
                                            <td align="center" style="background-color: LightBlue; color: Black;">
                                                URDIMBRE
                                            </td>
                                            <td align="center" style="background-color: LightBlue; color: Black;">
                                                <asp:TextBox ID="txtUrdimbrePrete" runat="server" class="input" Height="18px" MaxLength="200"></asp:TextBox>
                                            </td>
                                            <td align="center" style="background-color: LightBlue; color: Black;">
                                                ANALISTA
                                            </td>
                                            <td align="center" style="background-color: LightBlue; color: Black;">
                                                <asp:TextBox ID="txtAnalistaPrete" runat="server" class="input" MaxLength="200"></asp:TextBox>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <table align="center" border="0" cellpadding="0" cellspacing="0" style="margin: 5px;">
                                        <tr align="center" class="RowHead">
                                            <td>
                                                PROCESO DE URDIDO
                                            </td>
                                            <td>
                                                VALORES
                                            </td>
                                            <td>
                                                OBSERVACIONES
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="center" style="background-color: LightBlue; color: Black;">
                                                VELOCIDAD DE MÁQUINA (m/min)
                                            </td>
                                            <td align="center" style="background-color: LightBlue; color: Black;">
                                                <asp:TextBox ID="txtVelocidadMaquinaPrete" runat="server" class="input" MaxLength="200"></asp:TextBox>
                                            </td>
                                            <td align="center" style="background-color: LightBlue; color: Black;">
                                                <asp:TextBox ID="txtObsVelocidadMaquinaPrete" runat="server" class="input" MaxLength="200"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="center" style="background-color: LightBlue; color: Black;">
                                                TENSION DE MAQUINA (cN)
                                            </td>
                                            <td align="center" style="background-color: LightBlue; color: Black;">
                                                <asp:TextBox ID="txtTensionMaquinaPrete" runat="server" class="input" MaxLength="200"></asp:TextBox>
                                            </td>
                                            <td align="center" style="background-color: LightBlue; color: Black;">
                                                <asp:TextBox ID="txtObsTensionMaquinaPrete" runat="server" class="input" MaxLength="200"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="center" style="background-color: LightBlue; color: Black;">
                                                OTROS
                                            </td>
                                            <td align="center" style="background-color: LightBlue; color: Black;">
                                                <asp:TextBox ID="txtOtrosPrete" runat="server" class="input" MaxLength="200"></asp:TextBox>
                                            </td>
                                            <td align="center" style="background-color: LightBlue; color: Black;">
                                                <asp:TextBox ID="txtObsOtrosPrete" runat="server" class="input" MaxLength="200"></asp:TextBox>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                                <td>
                                    <table align="center" border="0" cellpadding="0" cellspacing="0" style="margin: 5px;">
                                        <tr align="center" class="RowHead">
                                            <td>
                                                EVALUACIÓN DE URDIDO
                                            </td>
                                            <td>
                                                VALORES
                                            </td>
                                            <td>
                                                OBSERVACIONES
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="center" style="background-color: LightBlue; color: Black;">
                                                ROTURAS x MILLÓN
                                            </td>
                                            <td align="center" style="background-color: LightBlue; color: Black;">
                                                <asp:TextBox ID="txtRotulasMIllonPrete" runat="server" class="input" MaxLength="200"></asp:TextBox>
                                            </td>
                                            <td align="center" style="background-color: LightBlue; color: Black;">
                                                <asp:TextBox ID="txtObsRotulasMIllonPrete" runat="server" class="input" MaxLength="200"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="center" style="background-color: LightBlue; color: Black;">
                                                TENSIÓN DEL HILO (cN)
                                            </td>
                                            <td align="center" style="background-color: LightBlue; color: Black;">
                                                <asp:TextBox ID="txtTensionHiloPrete" runat="server" class="input" MaxLength="200"></asp:TextBox>
                                            </td>
                                            <td align="center" style="background-color: LightBlue; color: Black;">
                                                <asp:TextBox ID="txtObsTensionHiloPrete" runat="server" class="input" MaxLength="200"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="center" style="background-color: LightBlue; color: Black;">
                                                OTROS
                                            </td>
                                            <td align="center" style="background-color: LightBlue; color: Black;">
                                                <asp:TextBox ID="txtOtrosPreteTenido" runat="server" class="input" MaxLength="200"></asp:TextBox>
                                            </td>
                                            <td align="center" style="background-color: LightBlue; color: Black;">
                                                <asp:TextBox ID="txtObsOtrosPreteTenido" runat="server" class="input" MaxLength="200"></asp:TextBox>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2">
                                    <table align="center" border="1" cellpadding="0" cellspacing="0">
                                        <tr>
                                            <td align="center" style="background-color: LightBlue; color: Black;">
                                                FECHA DE PRODUCCIÓN
                                            </td>
                                            <td align="left" class="Etiqueta" style="width: 150px; height: 20px" valign="middle">
                                                <asp:TextBox ID="txtFechaProduccionPrete2" runat="server" CssClass="input" Width="80px"></asp:TextBox><!--date-->
                                                &nbsp;<img onclick="popUpCalendar(this, form1.txtFechaProduccionPrete2, 'dd/mm/yyyy')"
                                                    height="16" width="16" alt="Seleccionar fecha de Inst." src="../images/Calendario.gif"
                                                    border="0" />
                                            </td>
                                            <td align="center" style="background-color: LightBlue; color: Black;">
                                                MÁQUINA
                                            </td>
                                            <td align="center" style="background-color: LightBlue; color: Black;">
                                                <asp:TextBox ID="txtMaquinaPrete2" runat="server" class="input" MaxLength="200"></asp:TextBox>
                                            </td>
                                            <td align="center" style="background-color: LightBlue; color: Black;">
                                                SUPERVISOR
                                            </td>
                                            <td align="center" style="background-color: LightBlue; color: Black;">
                                                <asp:TextBox ID="txtSupervisorPrete2" runat="server" class="input" MaxLength="200"></asp:TextBox>
                                            </td>
                                            <td align="center" style="background-color: LightBlue; color: Black;">
                                                MAQUINISTA
                                            </td>
                                            <td align="center" style="background-color: LightBlue; color: Black;">
                                                <asp:TextBox ID="txtMaquinistaPrete2" runat="server" class="input" MaxLength="200"></asp:TextBox>
                                            </td>
                                            <td align="center" style="background-color: LightBlue; color: Black;">
                                                ANALISTA
                                            </td>
                                            <td align="center" style="background-color: LightBlue; color: Black;">
                                                <asp:TextBox ID="txtTurnoPrete2" runat="server" class="input" MaxLength="200"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="center" style="background-color: LightBlue; color: Black;">
                                                PARTIDA
                                            </td>
                                            <td align="center" style="background-color: LightBlue; color: Black;">
                                                <asp:TextBox ID="txtPartidaPrete2" runat="server" class="input" MaxLength="200"></asp:TextBox>
                                            </td>
                                            <td align="center" style="background-color: LightBlue; color: Black;">
                                                ARTICULO
                                            </td>
                                            <td align="center" style="background-color: LightBlue; color: Black;">
                                                <asp:TextBox ID="txtArticuloPrete2" runat="server" class="input" MaxLength="200"></asp:TextBox>
                                            </td>
                                            <td align="center" style="background-color: LightBlue; color: Black;">
                                                ANALISTA
                                            </td>
                                            <td align="center" style="background-color: LightBlue; color: Black;">
                                                <asp:TextBox ID="txtAnalistaPrete2" runat="server" class="input" MaxLength="200"></asp:TextBox>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <table align="center" border="0" cellpadding="0" cellspacing="0" style="margin: 5px;">
                                        <tr align="center" class="RowHead">
                                            <td>
                                                PROCESO DE TEÑIDO Y/O ENGOMADO
                                            </td>
                                            <td>
                                                VALORES
                                            </td>
                                            <td>
                                                OBSERVACIONES
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="center" style="background-color: LightBlue; color: Black;">
                                                VELOCIDAD DE MÁQUINA (m/min)
                                            </td>
                                            <td align="center" style="background-color: LightBlue; color: Black;">
                                                <asp:TextBox ID="txtVELOCIDADMÁQUINAPret" runat="server" class="input" MaxLength="200"></asp:TextBox>
                                            </td>
                                            <td align="center" style="background-color: LightBlue; color: Black;">
                                                <asp:TextBox ID="txtObsVELOCIDADMÁQUINAPret" runat="server" class="input" MaxLength="200"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="center" style="background-color: LightBlue; color: Black;">
                                                TENSIÓN DEL CABEZAL (cN)
                                            </td>
                                            <td align="center" style="background-color: LightBlue; color: Black;">
                                                <asp:TextBox ID="txtTENSIÓNCABEZALPrete" runat="server" class="input" MaxLength="200"></asp:TextBox>
                                            </td>
                                            <td align="center" style="background-color: LightBlue; color: Black;">
                                                <asp:TextBox ID="txtObsTENSIÓNCABEZALPrete" runat="server" class="input" MaxLength="200"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="center" style="background-color: LightBlue; color: Black;">
                                                TENSIÓN DE LA FILETA (cN)
                                            </td>
                                            <td align="center" style="background-color: LightBlue; color: Black;">
                                                <asp:TextBox ID="txtTENSIÓNFILETAPrete" runat="server" class="input" MaxLength="200"></asp:TextBox>
                                            </td>
                                            <td align="center" style="background-color: LightBlue; color: Black;">
                                                <asp:TextBox ID="txtObsTENSIÓNFILETAPrete" runat="server" class="input" MaxLength="200"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="center" style="background-color: LightBlue; color: Black;">
                                                PRESIONES DEL RODILLO (bar)
                                            </td>
                                            <td align="center" style="background-color: LightBlue; color: Black;">
                                                <asp:TextBox ID="txtPRESIONESRODILLOPrete" runat="server" class="input" MaxLength="200"></asp:TextBox>
                                            </td>
                                            <td align="center" style="background-color: LightBlue; color: Black;">
                                                <asp:TextBox ID="txtObsPRESIONESRODILLOPrete" runat="server" class="input" MaxLength="200"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="center" style="background-color: LightBlue; color: Black;">
                                                DENSIDAD DE LA SODA (° Be)
                                            </td>
                                            <td align="center" style="background-color: LightBlue; color: Black;">
                                                <asp:TextBox ID="txtDENSIDADSODAPrete" runat="server" class="input" MaxLength="200"></asp:TextBox>
                                            </td>
                                            <td align="center" style="background-color: LightBlue; color: Black;">
                                                <asp:TextBox ID="txtObsDENSIDADSODAPrete" runat="server" class="input" MaxLength="200"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="center" style="background-color: LightBlue; color: Black;">
                                                TEMPERATURA TINAS (TEÑIDO) (°C)
                                            </td>
                                            <td align="center" style="background-color: LightBlue; color: Black;">
                                                <asp:TextBox ID="txtTEMPERATURATINASPrete" runat="server" class="input" MaxLength="200"></asp:TextBox>
                                            </td>
                                            <td align="center" style="background-color: LightBlue; color: Black;">
                                                <asp:TextBox ID="txtObsTEMPERATURATINASPrete" runat="server" class="input" MaxLength="200"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="center" style="background-color: LightBlue; color: Black;">
                                                OTROS
                                            </td>
                                            <td align="center" style="background-color: LightBlue; color: Black;">
                                                <asp:TextBox ID="txtOtrosPreteUrdido" runat="server" class="input" MaxLength="200"></asp:TextBox>
                                            </td>
                                            <td align="center" style="background-color: LightBlue; color: Black;">
                                                <asp:TextBox ID="txtObsOtrosPreteUrdido" runat="server" class="input" MaxLength="200"></asp:TextBox>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                                <td>
                                    <table align="center" border="0" cellpadding="0" cellspacing="0" style="margin: 5px;">
                                        <tr align="center" class="RowHead">
                                            <td>
                                                EVALUACIÓN DE TEÑIDO Y/O ENGOMADO
                                            </td>
                                            <td>
                                                VALORES
                                            </td>
                                            <td>
                                                OBSERVACIONES
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="center" style="background-color: LightBlue; color: Black;">
                                                VISCOSIDAD DE LA GOMA
                                            </td>
                                            <td align="center" style="background-color: LightBlue; color: Black;">
                                                <asp:TextBox ID="txtVISCOSIDADGOMAPrete" runat="server" class="input" MaxLength="200"></asp:TextBox>
                                            </td>
                                            <td align="center" style="background-color: LightBlue; color: Black;">
                                                <asp:TextBox ID="txtObsVISCOSIDADGOMAPrete" runat="server" class="input" MaxLength="200"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="center" style="background-color: LightBlue; color: Black;">
                                                CONCENTRACIÓN DE INDIGO (g/L)
                                            </td>
                                            <td align="center" style="background-color: LightBlue; color: Black;">
                                                <asp:TextBox ID="txtCONCENTRACIONINDIGHIDROSULFITOPrete" runat="server" class="input"
                                                    MaxLength="200"></asp:TextBox>
                                            </td>
                                            <td align="center" style="background-color: LightBlue; color: Black;">
                                                <asp:TextBox ID="txtObsCONCENTRACIONINDIGHIDROSULFITOPrete" runat="server" class="input"
                                                    MaxLength="200"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="center" style="background-color: LightBlue; color: Black;">
                                                CONCENTRACIÓN DE HIDROSULFITO (g/L)
                                            </td>
                                            <td align="center" style="background-color: LightBlue; color: Black;">
                                                <asp:TextBox ID="txtCONCENTRACIONINDHOROSULFITOPrete" runat="server" class="input"
                                                    MaxLength="200"></asp:TextBox>
                                            </td>
                                            <td align="center" style="background-color: LightBlue; color: Black;">
                                                <asp:TextBox ID="txtObsCONCENTRACIONINDHOROSULFITOPrete" runat="server" class="input"
                                                    MaxLength="200"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="center" style="background-color: LightBlue; color: Black;">
                                                ROTURAS / KM
                                            </td>
                                            <td align="center" style="background-color: LightBlue; color: Black;">
                                                <asp:TextBox ID="txtROTURASKMPrete" runat="server" class="input" MaxLength="200"></asp:TextBox>
                                            </td>
                                            <td align="center" style="background-color: LightBlue; color: Black;">
                                                <asp:TextBox ID="txtObsROTURASKMPrete" runat="server" class="input" MaxLength="200"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="center" style="background-color: LightBlue; color: Black;">
                                                OTROS
                                            </td>
                                            <td align="center" style="background-color: LightBlue; color: Black;">
                                                <asp:TextBox ID="txtOTROSPreteTenido2" runat="server" class="input" MaxLength="200"></asp:TextBox>
                                            </td>
                                            <td align="center" style="background-color: LightBlue; color: Black;">
                                                <asp:TextBox ID="txtObsOTROSPreteTenido2" runat="server" class="input" MaxLength="200"></asp:TextBox>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Button ID="btn_Pretejido" runat="server" CssClass="BotonVerde" Text="Guardar" />
                                </td>
                                <td>
                                    <asp:Button ID="btnTerminoPretejido" runat="server" CssClass="btnAzul" Text="Concluir Proceso" />
                                </td>
                            </tr>
                        </table>
                        <table id="TablaTejeduria" runat="server" border="0" cellpadding="0" cellspacing="0"
                            class="ttable" style="width: 924px">
                            <tr>
                                <td class="RowHead" rowspan="12">
                                    TEJEDURÍA
                                </td>
                                <td colspan="2">
                                    <table align="center" border="1" cellpadding="0" cellspacing="0">
                                        <tr>
                                            <td align="center" style="background-color: LightBlue; color: Black;">
                                                FECHA DE PRODUCCIÓN
                                            </td>
                                            <td align="left" class="Etiqueta" style="width: 150px; height: 20px" valign="middle">
                                                <asp:TextBox ID="txtFechaProduccionTeje" runat="server" CssClass="input" Width="80px"></asp:TextBox>
                                                &nbsp;<img onclick="popUpCalendar(this, form1.txtFechaProduccionTeje, 'dd/mm/yyyy')"
                                                    height="16" width="16" alt="Seleccionar fecha de Inst." src="../images/Calendario.gif"
                                                    border="0" />
                                            </td>
                                            <td align="center" style="background-color: LightBlue; color: Black;">
                                                TEJEDOR
                                            </td>
                                            <td align="center" style="background-color: LightBlue; color: Black;">
                                                <asp:TextBox ID="txtTejedorTeje" runat="server" class="input" MaxLength="200"></asp:TextBox>
                                            </td>
                                            <td align="center" style="background-color: LightBlue; color: Black;">
                                                MECÁNICO
                                            </td>
                                            <td align="center" style="background-color: LightBlue; color: Black;">
                                                <asp:TextBox ID="txtMecanicoTeje" runat="server" class="input" MaxLength="200"></asp:TextBox>
                                            </td>
                                            <td align="center" style="background-color: LightBlue; color: Black;">
                                                FECHA DE DESMONTE
                                            </td>
                                            <td align="left" class="Etiqueta" style="width: 150px; height: 20px" valign="middle">
                                                <asp:TextBox ID="txtFechaDesmonteTeje" runat="server" CssClass="input" Width="80px"></asp:TextBox>
                                                &nbsp;<img onclick="popUpCalendar(this, form1.txtFechaDesmonteTeje, 'dd/mm/yyyy')"
                                                    height="16" width="16" alt="Seleccionar fecha de Inst." src="../images/Calendario.gif"
                                                    border="0" />
                                            </td>
                                            <td align="center" style="background-color: LightBlue; color: Black;">
                                                ANALISTA
                                            </td>
                                            <td align="center" style="background-color: LightBlue; color: Black;">
                                                <asp:TextBox ID="txtAnalistaTeje" runat="server" class="input" MaxLength="200"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="center" style="background-color: LightBlue; color: Black;">
                                                PARTIDA
                                            </td>
                                            <td align="center" style="background-color: LightBlue; color: Black;">
                                                <asp:TextBox ID="txtPartidaTeje" runat="server" class="input" MaxLength="200"></asp:TextBox>
                                            </td>
                                            <td align="center" style="background-color: LightBlue; color: Black;">
                                                PLEGADOR
                                            </td>
                                            <td align="center" style="background-color: LightBlue; color: Black;">
                                                <asp:TextBox ID="txtPlegadorTeje" runat="server" class="input" MaxLength="200"></asp:TextBox>
                                            </td>
                                            <td align="center" style="background-color: LightBlue; color: Black;">
                                                PIEZA
                                            </td>
                                            <td align="center" style="background-color: LightBlue; color: Black;">
                                                <asp:TextBox ID="txtPiezaTeje" runat="server" class="input" MaxLength="200"></asp:TextBox>
                                            </td>
                                            <td align="center" style="background-color: LightBlue; color: Black;">
                                                TELAR
                                            </td>
                                            <td align="center" style="background-color: LightBlue; color: Black;">
                                                <asp:TextBox ID="txtTelarTeje" runat="server" class="input" MaxLength="200"></asp:TextBox>
                                            </td>
                                            <td align="center" style="background-color: LightBlue; color: Black;">
                                                PLANTA
                                            </td>
                                            <td align="center" style="background-color: LightBlue; color: Black;">
                                                <asp:TextBox ID="txtPlantaTeje" runat="server" class="input" MaxLength="200"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="center" style="background-color: LightBlue; color: Black;">
                                                TÍTULO
                                            </td>
                                            <td align="center" style="background-color: LightBlue; color: Black;">
                                                <asp:TextBox ID="txtTituloTeje" runat="server" class="input" MaxLength="200"></asp:TextBox>
                                            </td>
                                            <td align="center" style="background-color: LightBlue; color: Black;">
                                                METROS
                                            </td>
                                            <td align="center" style="background-color: LightBlue; color: Black;">
                                                <asp:TextBox ID="txtMetroTeje" runat="server" class="input" MaxLength="200"></asp:TextBox>
                                            </td>
                                            <td align="center" style="background-color: LightBlue; color: Black;">
                                                PROCEDENCIA
                                            </td>
                                            <td align="center" style="background-color: LightBlue; color: Black;">
                                                <asp:TextBox ID="txtProcedenciaTeje" runat="server" class="input" MaxLength="200"></asp:TextBox>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <table align="center" border="0" cellpadding="0" cellspacing="0" style="margin: 5px;">
                                        <tr align="center" class="RowHead">
                                            <td>
                                                PROCESO DE TEJIDO
                                            </td>
                                            <td>
                                                VALORES
                                            </td>
                                            <td>
                                                OBSERVACIONES
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="center" style="background-color: LightBlue; color: Black;">
                                                VELOCIDAD DEL TELAR (RPM)
                                            </td>
                                            <td align="center" style="background-color: LightBlue; color: Black;">
                                                <asp:TextBox ID="txtVELOCIDADTELARRPMTeje" runat="server" class="input" MaxLength="200"></asp:TextBox>
                                            </td>
                                            <td align="center" style="background-color: LightBlue; color: Black;">
                                                <asp:TextBox ID="txtObsVELOCIDADTELARRPMTeje" runat="server" class="input" MaxLength="200"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="center" style="background-color: LightBlue; color: Black;">
                                                ANCHO DE PEINE (cm)
                                            </td>
                                            <td align="center" style="background-color: LightBlue; color: Black;">
                                                <asp:TextBox ID="txtANCHOPEINERPMTEje" runat="server" class="input" MaxLength="200"></asp:TextBox>
                                            </td>
                                            <td align="center" style="background-color: LightBlue; color: Black;">
                                                <asp:TextBox ID="txtObsANCHOPEINERPMTEje" runat="server" class="input" MaxLength="200"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="center" style="background-color: LightBlue; color: Black;">
                                                OTROS
                                            </td>
                                            <td align="center" style="background-color: LightBlue; color: Black;">
                                                <asp:TextBox ID="txtOtrosTeje" runat="server" class="input" MaxLength="200"></asp:TextBox>
                                            </td>
                                            <td align="center" style="background-color: LightBlue; color: Black;">
                                                <asp:TextBox ID="txtObsOtrosTeje" runat="server" class="input" MaxLength="200"></asp:TextBox>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                                <td>
                                    <table align="center" border="0" cellpadding="0" cellspacing="0" style="margin: 5px;">
                                        <tr align="center" class="RowHead">
                                            <td>
                                                EVALUACIÓN DE TEJIDO
                                            </td>
                                            <td>
                                                VALORES
                                            </td>
                                            <td>
                                                OBSERVACIONES
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="center" style="background-color: LightBlue; color: Black;">
                                                CANTIDAD DE ROTURAS EN TRAMA / TURNO
                                            </td>
                                            <td align="center" style="background-color: LightBlue; color: Black;">
                                                <asp:TextBox ID="txtCANTIDADROTURASTRAMATURNOTeje" runat="server" class="input" MaxLength="200"></asp:TextBox>
                                            </td>
                                            <td align="center" style="background-color: LightBlue; color: Black;">
                                                <asp:TextBox ID="txtObsCANTIDADROTURASTRAMATURNOTeje" runat="server" class="input"
                                                    MaxLength="200"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="center" style="background-color: LightBlue; color: Black;">
                                                CANTIDAD DE ROTURAS EN URDIMBRE / TURNO
                                            </td>
                                            <td align="center" style="background-color: LightBlue; color: Black;">
                                                <asp:TextBox ID="txtCANTIDADROTURASURDIMBRETEje" runat="server" class="input" MaxLength="200"></asp:TextBox>
                                            </td>
                                            <td align="center" style="background-color: LightBlue; color: Black;">
                                                <asp:TextBox ID="txtObsCANTIDADROTURASURDIMBRETEje" runat="server" class="input"
                                                    MaxLength="200"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="center" style="background-color: LightBlue; color: Black;">
                                                PRUEBA DE BOIL OFF (%)
                                            </td>
                                            <td align="center" style="background-color: LightBlue; color: Black;">
                                                <asp:TextBox ID="txtPRUEBABOILOFFTEje" runat="server" class="input" MaxLength="200"></asp:TextBox>
                                            </td>
                                            <td align="center" style="background-color: LightBlue; color: Black;">
                                                <asp:TextBox ID="txtObsPRUEBABOILOFFTEje" runat="server" class="input" MaxLength="200"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="center" style="background-color: LightBlue; color: Black;">
                                                ANCHO DE ROLLO (cm)
                                            </td>
                                            <td align="center" style="background-color: LightBlue; color: Black;">
                                                <asp:TextBox ID="txtANCHOROLLOTEje" runat="server" class="input" MaxLength="200"></asp:TextBox>
                                            </td>
                                            <td align="center" style="background-color: LightBlue; color: Black;">
                                                <asp:TextBox ID="txObstANCHOROLLOTEje" runat="server" class="input" MaxLength="200"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="center" style="background-color: LightBlue; color: Black;">
                                                EFICIENCIA (%)
                                            </td>
                                            <td align="center" style="background-color: LightBlue; color: Black;">
                                                <asp:TextBox ID="txtEFICIENCIATeje" runat="server" class="input" MaxLength="200"></asp:TextBox>
                                            </td>
                                            <td align="center" style="background-color: LightBlue; color: Black;">
                                                <asp:TextBox ID="txtObsEFICIENCIATeje" runat="server" class="input" MaxLength="200"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="center" style="background-color: LightBlue; color: Black;">
                                                OTROS
                                            </td>
                                            <td align="center" style="background-color: LightBlue; color: Black;">
                                                <asp:TextBox ID="txtOtrosTeje2" runat="server" class="input" MaxLength="200"></asp:TextBox>
                                            </td>
                                            <td align="center" style="background-color: LightBlue; color: Black;">
                                                <asp:TextBox ID="txtObsOtrosTeje2" runat="server" class="input" MaxLength="200"></asp:TextBox>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Button ID="btnTejeduria" runat="server" CssClass="BotonVerde" Text="Guardar" />
                                </td>
                                <td>
                                    <asp:Button ID="btnTerminoTejeduria" runat="server" CssClass="btnAzul" Text="Concluir Proceso" />
                                </td>
                            </tr>
                        </table>
                        <table id="TablaTintoreria" runat="server" border="0" cellpadding="0" cellspacing="0"
                            class="ttable" style="width: 824px">
                            <tr>
                                <td class="RowHead" rowspan="12">
                                    TINTORERÍA
                                </td>
                                <td colspan="2">
                                    <table align="center" border="1" cellpadding="0" cellspacing="0">
                                        <tr>
                                            <td align="center" style="background-color: LightBlue; color: Black;">
                                                FECHA DE PRODUCCIÓN
                                            </td>
                                            <td align="left" class="Etiqueta" style="width: 150px; height: 20px" valign="middle">
                                                <asp:TextBox ID="txtFechProduccionTinto" runat="server" CssClass="input" Width="80px"></asp:TextBox>
                                                &nbsp;<img onclick="popUpCalendar(this, form1.txtFechProduccionTinto, 'dd/mm/yyyy')"
                                                    height="16" width="16" alt="Seleccionar fecha de Inst." src="../images/Calendario.gif"
                                                    border="0" />
                                            </td>
                                            <td align="center" style="background-color: LightBlue; color: Black;">
                                                MÁQUINA
                                            </td>
                                            <td align="center" style="background-color: LightBlue; color: Black;">
                                                <asp:TextBox ID="txtMaquinaTinto" runat="server" class="input" MaxLength="200"></asp:TextBox>
                                            </td>
                                            <td align="center" style="background-color: LightBlue; color: Black;">
                                                SUPERVISOR
                                            </td>
                                            <td align="center" style="background-color: LightBlue; color: Black;">
                                                <asp:TextBox ID="txtSupervisorTinto" runat="server" class="input" MaxLength="200"></asp:TextBox>
                                            </td>
                                            <td align="center" style="background-color: LightBlue; color: Black;">
                                                MAQUINISTA
                                            </td>
                                            <td align="center" style="background-color: LightBlue; color: Black;">
                                                <asp:TextBox ID="txtMaquinistaTinto" runat="server" class="input" MaxLength="200"></asp:TextBox>
                                            </td>
                                            <td align="center" style="background-color: LightBlue; color: Black;">
                                                TURNO
                                            </td>
                                            <td align="center" style="background-color: LightBlue; color: Black;">
                                                <asp:TextBox ID="txtTurnoTinto" runat="server" class="input" MaxLength="200"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="center" style="background-color: LightBlue; color: Black;">
                                                ARTÍCULO
                                            </td>
                                            <td align="center" style="background-color: LightBlue; color: Black;">
                                                <asp:TextBox ID="txtArticuloTinto" runat="server" class="input" MaxLength="200"></asp:TextBox>
                                            </td>
                                            <td align="center" style="background-color: LightBlue; color: Black;">
                                                FICHA
                                            </td>
                                            <td align="center" style="background-color: LightBlue; color: Black;">
                                                <asp:TextBox ID="txtFichaTinto" runat="server" class="input" MaxLength="200"></asp:TextBox>
                                            </td>
                                            <td align="center" style="background-color: LightBlue; color: Black;">
                                                PROCESO
                                            </td>
                                            <td align="center" style="background-color: LightBlue; color: Black;">
                                                <asp:TextBox ID="txtProcesoTinto" runat="server" class="input" MaxLength="200"></asp:TextBox>
                                            </td>
                                            <td align="center" style="background-color: LightBlue; color: Black;">
                                                METROS
                                            </td>
                                            <td align="center" style="background-color: LightBlue; color: Black;">
                                                <asp:TextBox ID="txtMetrosTinto" runat="server" class="input" MaxLength="200"></asp:TextBox>
                                            </td>
                                            <td align="center" style="background-color: LightBlue; color: Black;">
                                                ANALISTA
                                            </td>
                                            <td align="center" style="background-color: LightBlue; color: Black;">
                                                <asp:TextBox ID="txtAnalistaTinto" runat="server" class="input" MaxLength="200"></asp:TextBox>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <table align="center" border="0" cellpadding="0" cellspacing="0" style="margin: 5px;">
                                        <tr align="center" class="RowHead">
                                            <td>
                                                PROCESO DE CHAMUSCADO / ENROLLADO
                                            </td>
                                            <td>
                                                VALORES
                                            </td>
                                            <td>
                                                OBSERVACIONES
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="center" style="background-color: LightBlue; color: Black;">
                                                VELOCIDAD DE MÁQUINA (m/min)
                                            </td>
                                            <td align="center" style="background-color: LightBlue; color: Black;">
                                                <asp:TextBox ID="txtVELOCIDADMÁQUINAminTinto1" runat="server" class="input" MaxLength="200"></asp:TextBox>
                                            </td>
                                            <td align="center" style="background-color: LightBlue; color: Black;">
                                                <asp:TextBox ID="txtObsVELOCIDADMÁQUINAminTinto1" runat="server" class="input" MaxLength="200"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="center" style="background-color: LightBlue; color: Black;">
                                                TEMPERATURA (°C)
                                            </td>
                                            <td align="center" style="background-color: LightBlue; color: Black;">
                                                <asp:TextBox ID="txtTEMPERATURACTinto1" runat="server" class="input" MaxLength="200"></asp:TextBox>
                                            </td>
                                            <td align="center" style="background-color: LightBlue; color: Black;">
                                                <asp:TextBox ID="txtObsTEMPERATURACTinto1" runat="server" class="input" MaxLength="200"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="center" style="background-color: LightBlue; color: Black;">
                                                INTENSIDAD DE LLAMA (mbar)
                                            </td>
                                            <td align="center" style="background-color: LightBlue; color: Black;">
                                                <asp:TextBox ID="txtINTENSIDADLLAMAmbarTinto1" runat="server" class="input" MaxLength="200"></asp:TextBox>
                                            </td>
                                            <td align="center" style="background-color: LightBlue; color: Black;">
                                                <asp:TextBox ID="txtObsINTENSIDADLLAMAmbarTinto1" runat="server" class="input" MaxLength="200"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="center" style="background-color: LightBlue; color: Black;">
                                                OTROS
                                            </td>
                                            <td align="center" style="background-color: LightBlue; color: Black;">
                                                <asp:TextBox ID="txtOtrosTinto1" runat="server" class="input" MaxLength="200"></asp:TextBox>
                                            </td>
                                            <td align="center" style="background-color: LightBlue; color: Black;">
                                                <asp:TextBox ID="txtObsOtrosTinto1" runat="server" class="input" MaxLength="200"></asp:TextBox>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                                <td>
                                    <table align="center" border="0" cellpadding="0" cellspacing="0" style="margin: 5px;">
                                        <tr align="center" class="RowHead">
                                            <td>
                                                EVALUACIÓN DE PROCESO
                                            </td>
                                            <td>
                                                VALORES
                                            </td>
                                            <td>
                                                OBSERVACIONES
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="center" style="background-color: LightBlue; color: Black;">
                                                ANCHO DE INGRESO (cm)
                                            </td>
                                            <td align="center" style="background-color: LightBlue; color: Black;">
                                                <asp:TextBox ID="txtANCHOINGRESOcmTinto1" runat="server" class="input" MaxLength="200"></asp:TextBox>
                                            </td>
                                            <td align="center" style="background-color: LightBlue; color: Black;">
                                                <asp:TextBox ID="txtObsANCHOINGRESOcmTinto1" runat="server" class="input" MaxLength="200"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="center" style="background-color: LightBlue; color: Black;">
                                                ANCHO DE SALIDA (cm)
                                            </td>
                                            <td align="center" style="background-color: LightBlue; color: Black;">
                                                <asp:TextBox ID="txtANCHOSALIDAcmTinto" runat="server" class="input" MaxLength="200"></asp:TextBox>
                                            </td>
                                            <td align="center" style="background-color: LightBlue; color: Black;">
                                                <asp:TextBox ID="txtObsANCHOSALIDAcmTinto" runat="server" class="input" MaxLength="200"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="center" style="background-color: LightBlue; color: Black;">
                                                OTROS
                                            </td>
                                            <td align="center" style="background-color: LightBlue; color: Black;">
                                                <asp:TextBox ID="txtOtrosTinto11" runat="server" class="input" MaxLength="200"></asp:TextBox>
                                            </td>
                                            <td align="center" style="background-color: LightBlue; color: Black;">
                                                <asp:TextBox ID="txtObsOtrosTinto11" runat="server" class="input" MaxLength="200"></asp:TextBox>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2">
                                    <table align="center" border="1" cellpadding="0" cellspacing="0">
                                        <tr>
                                            <td align="center" style="background-color: LightBlue; color: Black;">
                                                FECHA DE PRODUCCIÓN
                                            </td>
                                            <td align="left" class="Etiqueta" style="width: 150px; height: 20px" valign="middle">
                                                <asp:TextBox ID="txtFechaProduccionTinto2" runat="server" CssClass="input" Width="80px"></asp:TextBox>
                                                &nbsp;<img onclick="popUpCalendar(this, form1.txtFechaProduccionTinto2, 'dd/mm/yyyy')"
                                                    height="16" width="16" alt="Seleccionar fecha de Inst." src="../images/Calendario.gif"
                                                    border="0" />
                                            </td>
                                            <td align="center" style="background-color: LightBlue; color: Black;">
                                                MÁQUINA
                                            </td>
                                            <td align="center" style="background-color: LightBlue; color: Black;">
                                                <asp:TextBox ID="txtMaquinaTinto2" runat="server" class="input" MaxLength="200"></asp:TextBox>
                                            </td>
                                            <td align="center" style="background-color: LightBlue; color: Black;">
                                                SUPERVISOR
                                            </td>
                                            <td align="center" style="background-color: LightBlue; color: Black;">
                                                <asp:TextBox ID="txtSupervisorTinto2" runat="server" class="input" MaxLength="200"></asp:TextBox>
                                            </td>
                                            <td align="center" style="background-color: LightBlue; color: Black;">
                                                MAQUINISTA
                                            </td>
                                            <td align="center" style="background-color: LightBlue; color: Black;">
                                                <asp:TextBox ID="txtMaquinistaTinto2" runat="server" class="input" MaxLength="200"></asp:TextBox>
                                            </td>
                                            <td align="center" style="background-color: LightBlue; color: Black;">
                                                TURNO
                                            </td>
                                            <td align="center" style="background-color: LightBlue; color: Black;">
                                                <asp:TextBox ID="txtTurnoTinto2" runat="server" class="input" MaxLength="200"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="center" style="background-color: LightBlue; color: Black;">
                                                ARTÍCULO
                                            </td>
                                            <td align="center" style="background-color: LightBlue; color: Black;">
                                                <asp:TextBox ID="txtArticuloTinto2" runat="server" class="input" MaxLength="200"></asp:TextBox>
                                            </td>
                                            <td align="center" style="background-color: LightBlue; color: Black;">
                                                FICHA
                                            </td>
                                            <td align="center" style="background-color: LightBlue; color: Black;">
                                                <asp:TextBox ID="txtFichaTinto2" runat="server" class="input" MaxLength="200"></asp:TextBox>
                                            </td>
                                            <td align="center" style="background-color: LightBlue; color: Black;">
                                                PROCESO
                                            </td>
                                            <td align="center" style="background-color: LightBlue; color: Black;">
                                                <asp:TextBox ID="txtProcesoTinto2" runat="server" class="input" MaxLength="200"></asp:TextBox>
                                            </td>
                                            <td align="center" style="background-color: LightBlue; color: Black;">
                                                METROS
                                            </td>
                                            <td align="center" style="background-color: LightBlue; color: Black;">
                                                <asp:TextBox ID="txtMetrosTinto2" runat="server" class="input" MaxLength="200"></asp:TextBox>
                                            </td>
                                            <td align="center" style="background-color: LightBlue; color: Black;">
                                                ANALISTA
                                            </td>
                                            <td align="center" style="background-color: LightBlue; color: Black;">
                                                <asp:TextBox ID="txtAnalistaTinto2" runat="server" class="input" MaxLength="200"></asp:TextBox>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <table align="center" border="0" cellpadding="0" cellspacing="0" style="margin: 5px;">
                                        <tr align="center" class="RowHead">
                                            <td>
                                                PROCESO DE CHAMUSCADO / ENROLLADO
                                            </td>
                                            <td>
                                                VALORES
                                            </td>
                                            <td>
                                                OBSERVACIONES
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="center" style="background-color: LightBlue; color: Black;">
                                                VELOCIDAD DE MÁQUINA (m/min)
                                            </td>
                                            <td align="center" style="background-color: LightBlue; color: Black;">
                                                <asp:TextBox ID="txtVelocidadMaquinaTinto2" runat="server" class="input" MaxLength="200"></asp:TextBox>
                                            </td>
                                            <td align="center" style="background-color: LightBlue; color: Black;">
                                                <asp:TextBox ID="txtObsVelocidadMaquinaTinto2" runat="server" class="input" MaxLength="200"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="center" style="background-color: LightBlue; color: Black;">
                                                TEMPERATURA DE CALDERO
                                            </td>
                                            <td align="center" style="background-color: LightBlue; color: Black;">
                                                <asp:TextBox ID="txtTemperaturasCalderaTinto2" runat="server" class="input" MaxLength="200"></asp:TextBox>
                                            </td>
                                            <td align="center" style="background-color: LightBlue; color: Black;">
                                                <asp:TextBox ID="txtObsTemperaturasCalderaTinto2" runat="server" class="input" MaxLength="200"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="center" style="background-color: LightBlue; color: Black;">
                                                OTROS
                                            </td>
                                            <td align="center" style="background-color: LightBlue; color: Black;">
                                                <asp:TextBox ID="txtOtrosTinto2" runat="server" class="input" MaxLength="200"></asp:TextBox>
                                            </td>
                                            <td align="center" style="background-color: LightBlue; color: Black;">
                                                <asp:TextBox ID="txtObsOtrosTinto2" runat="server" class="input" MaxLength="200"></asp:TextBox>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                                <td>
                                    <table align="center" border="0" cellpadding="0" cellspacing="0" style="margin: 5px;">
                                        <tr align="center" class="RowHead">
                                            <td>
                                                EVALUACIÓN DE PROCESO
                                            </td>
                                            <td>
                                                VALORES
                                            </td>
                                            <td>
                                                OBSERVACIONES
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="center" style="background-color: LightBlue; color: Black;">
                                                ANCHO DE INGRESO (cm)
                                            </td>
                                            <td align="center" style="background-color: LightBlue; color: Black;">
                                                <asp:TextBox ID="txtAnchoIngresoTinto2" runat="server" class="input" MaxLength="200"></asp:TextBox>
                                            </td>
                                            <td align="center" style="background-color: LightBlue; color: Black;">
                                                <asp:TextBox ID="txtObsAnchoIngresoTinto2" runat="server" class="input" MaxLength="200"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="center" style="background-color: LightBlue; color: Black;">
                                                ANCHO DE SALIDA (cm)
                                            </td>
                                            <td align="center" style="background-color: LightBlue; color: Black;">
                                                <asp:TextBox ID="txtAnchoSalidaTinto2" runat="server" class="input" MaxLength="200"></asp:TextBox>
                                            </td>
                                            <td align="center" style="background-color: LightBlue; color: Black;">
                                                <asp:TextBox ID="txtObsAnchoSalidaTinto2" runat="server" class="input" MaxLength="200"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="center" style="background-color: LightBlue; color: Black;">
                                                OTROS
                                            </td>
                                            <td align="center" style="background-color: LightBlue; color: Black;">
                                                <asp:TextBox ID="txtOtrosTinto22" runat="server" class="input" MaxLength="200"></asp:TextBox>
                                            </td>
                                            <td align="center" style="background-color: LightBlue; color: Black;">
                                                <asp:TextBox ID="txtObsOtrosTinto22" runat="server" class="input" MaxLength="200"></asp:TextBox>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2">
                                    <table align="center" border="1" cellpadding="0" cellspacing="0">
                                        <tr>
                                            <td align="center" style="background-color: LightBlue; color: Black;">
                                                FECHA DE PRODUCCIÓN
                                            </td>
                                            <td align="left" class="Etiqueta" style="width: 150px; height: 20px" valign="middle">
                                                <asp:TextBox ID="txtFechaProduccionTinto3" runat="server" CssClass="input" Width="80px"></asp:TextBox>
                                                &nbsp;<img onclick="popUpCalendar(this, form1.txtFechaProduccionTinto3, 'dd/mm/yyyy')"
                                                    height="16" width="16" alt="Seleccionar fecha de Inst." src="../images/Calendario.gif"
                                                    border="0" />
                                            </td>
                                            <td align="center" style="background-color: LightBlue; color: Black;">
                                                MÁQUINA
                                            </td>
                                            <td align="center" style="background-color: LightBlue; color: Black;">
                                                <asp:TextBox ID="txtMaquinaTinto3" runat="server" class="input" MaxLength="200"></asp:TextBox>
                                            </td>
                                            <td align="center" style="background-color: LightBlue; color: Black;">
                                                SUPERVISOR
                                            </td>
                                            <td align="center" style="background-color: LightBlue; color: Black;">
                                                <asp:TextBox ID="txtSupervisorTinto3" runat="server" class="input" MaxLength="200"></asp:TextBox>
                                            </td>
                                            <td align="center" style="background-color: LightBlue; color: Black;">
                                                MAQUINISTA
                                            </td>
                                            <td align="center" style="background-color: LightBlue; color: Black;">
                                                <asp:TextBox ID="txtMaquinistaTinto3" runat="server" class="input" MaxLength="200"></asp:TextBox>
                                            </td>
                                            <td align="center" style="background-color: LightBlue; color: Black;">
                                                TURNO
                                            </td>
                                            <td align="center" style="background-color: LightBlue; color: Black;">
                                                <asp:TextBox ID="txtTurnoTinto3" runat="server" class="input" MaxLength="200"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="center" style="background-color: LightBlue; color: Black;">
                                                ARTÍCULO
                                            </td>
                                            <td align="center" style="background-color: LightBlue; color: Black;">
                                                <asp:TextBox ID="txtArticuloTinto3" runat="server" class="input" MaxLength="200"></asp:TextBox>
                                            </td>
                                            <td align="center" style="background-color: LightBlue; color: Black;">
                                                FICHA
                                            </td>
                                            <td align="center" style="background-color: LightBlue; color: Black;">
                                                <asp:TextBox ID="txtFichaTinto3" runat="server" class="input" MaxLength="200"></asp:TextBox>
                                            </td>
                                            <td align="center" style="background-color: LightBlue; color: Black;">
                                                PROCESO
                                            </td>
                                            <td align="center" style="background-color: LightBlue; color: Black;">
                                                <asp:TextBox ID="txtProcesoTinto3" runat="server" class="input" MaxLength="200"></asp:TextBox>
                                            </td>
                                            <td align="center" style="background-color: LightBlue; color: Black;">
                                                METROS
                                            </td>
                                            <td align="center" style="background-color: LightBlue; color: Black;">
                                                <asp:TextBox ID="txtMetrosTinto3" runat="server" class="input" MaxLength="200"></asp:TextBox>
                                            </td>
                                            <td align="center" style="background-color: LightBlue; color: Black;">
                                                ANALISTA
                                            </td>
                                            <td align="center" style="background-color: LightBlue; color: Black;">
                                                <asp:TextBox ID="txtAnalistaTinto3" runat="server" class="input" MaxLength="200"></asp:TextBox>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <table align="center" border="0" cellpadding="0" cellspacing="0" style="margin: 5px;">
                                        <tr align="center" class="RowHead">
                                            <td>
                                                PROCESO DE CHAMUSCADO / ENROLLADO
                                            </td>
                                            <td>
                                                VALORES
                                            </td>
                                            <td>
                                                OBSERVACIONES
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="center" style="background-color: LightBlue; color: Black;">
                                                VELOCIDAD DE MÁQUINA (m/min)
                                            </td>
                                            <td align="center" style="background-color: LightBlue; color: Black;">
                                                <asp:TextBox ID="txtVelicidadMaquinaTinto3" runat="server" class="input" MaxLength="200"></asp:TextBox>
                                            </td>
                                            <td align="center" style="background-color: LightBlue; color: Black;">
                                                <asp:TextBox ID="txtoBSVelicidadMaquinaTinto3" runat="server" class="input" MaxLength="200"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="center" style="background-color: LightBlue; color: Black;">
                                                TEMPERATURA DE MÁQUINA (°C)
                                            </td>
                                            <td align="center" style="background-color: LightBlue; color: Black;">
                                                <asp:TextBox ID="txtTEMPERATURAmaQUINATinto3" runat="server" class="input" MaxLength="200"></asp:TextBox>
                                            </td>
                                            <td align="center" style="background-color: LightBlue; color: Black;">
                                                <asp:TextBox ID="txtObsTEMPERATURAmaQUINATinto3" runat="server" class="input" MaxLength="200"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="center" style="background-color: LightBlue; color: Black;">
                                                TEMPERATURA DE CALDERO (°C)
                                            </td>
                                            <td align="center" style="background-color: LightBlue; color: Black;">
                                                <asp:TextBox ID="txtTemperaturaCaldero3" runat="server" class="input" MaxLength="200"></asp:TextBox>
                                            </td>
                                            <td align="center" style="background-color: LightBlue; color: Black;">
                                                <asp:TextBox ID="txtObsTemperaturaCaldero3" runat="server" class="input" MaxLength="200"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="center" style="background-color: LightBlue; color: Black;">
                                                VENTILADORES (RPM)
                                            </td>
                                            <td align="center" style="background-color: LightBlue; color: Black;">
                                                <asp:TextBox ID="txtVentiladoresTinto3" runat="server" class="input" MaxLength="200"></asp:TextBox>
                                            </td>
                                            <td align="center" style="background-color: LightBlue; color: Black;">
                                                <asp:TextBox ID="txtObsVentiladoresTinto3" runat="server" class="input" MaxLength="200"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="center" style="background-color: LightBlue; color: Black;">
                                                ANCHO DE SALIDA DE MÁQUINA (cm)
                                            </td>
                                            <td align="center" style="background-color: LightBlue; color: Black;">
                                                <asp:TextBox ID="txtAnchooSalidaTinto3" runat="server" class="input" MaxLength="200"></asp:TextBox>
                                            </td>
                                            <td align="center" style="background-color: LightBlue; color: Black;">
                                                <asp:TextBox ID="txtObsAnchooSalidaTinto3" runat="server" class="input" MaxLength="200"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="center" style="background-color: LightBlue; color: Black;">
                                                EXTRACTORES (%)
                                            </td>
                                            <td align="center" style="background-color: LightBlue; color: Black;">
                                                <asp:TextBox ID="txtExtractoresTinto3" runat="server" class="input" MaxLength="200"></asp:TextBox>
                                            </td>
                                            <td align="center" style="background-color: LightBlue; color: Black;">
                                                <asp:TextBox ID="txtObsExtractoresTinto3" runat="server" class="input" MaxLength="200"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="center" style="background-color: LightBlue; color: Black;">
                                                OTROS
                                            </td>
                                            <td align="center" style="background-color: LightBlue; color: Black;">
                                                <asp:TextBox ID="txtOtrosTinto3" runat="server" class="input" MaxLength="200"></asp:TextBox>
                                            </td>
                                            <td align="center" style="background-color: LightBlue; color: Black;">
                                                <asp:TextBox ID="txtObsOtrosTinto3" runat="server" class="input" MaxLength="200"></asp:TextBox>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                                <td>
                                    <table align="center" border="0" cellpadding="0" cellspacing="0" style="margin: 5px;">
                                        <tr align="center" class="RowHead">
                                            <td>
                                                EVALUACIÓN DE PROCESO
                                            </td>
                                            <td>
                                                VALORES
                                            </td>
                                            <td>
                                                OBSERVACIONES
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="center" style="background-color: LightBlue; color: Black;">
                                                ANCHO DE INGRESO (cm)
                                            </td>
                                            <td align="center" style="background-color: LightBlue; color: Black;">
                                                <asp:TextBox ID="txtAnchoIngresoTinto3" runat="server" class="input" MaxLength="200"></asp:TextBox>
                                            </td>
                                            <td align="center" style="background-color: LightBlue; color: Black;">
                                                <asp:TextBox ID="txtObsAnchoIngresoTinto3" runat="server" class="input" MaxLength="200"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="center" style="background-color: LightBlue; color: Black;">
                                                ANCHO DE SALIDA (cm)
                                            </td>
                                            <td align="center" style="background-color: LightBlue; color: Black;">
                                                <asp:TextBox ID="txtAnchoSalidaTinto3" runat="server" class="input" MaxLength="200"></asp:TextBox>
                                            </td>
                                            <td align="center" style="background-color: LightBlue; color: Black;">
                                                <asp:TextBox ID="txtObsAnchoSalidaTinto3" runat="server" class="input" MaxLength="200"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="center" style="background-color: LightBlue; color: Black;">
                                                OTROS
                                            </td>
                                            <td align="center" style="background-color: LightBlue; color: Black;">
                                                <asp:TextBox ID="txtOtrosTinto33" runat="server" class="input" MaxLength="200"></asp:TextBox>
                                            </td>
                                            <td align="center" style="background-color: LightBlue; color: Black;">
                                                <asp:TextBox ID="txtObsOtrosTinto33" runat="server" class="input" MaxLength="200"></asp:TextBox>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2">
                                    <table align="center" border="1" cellpadding="0" cellspacing="0">
                                        <tr>
                                            <td align="center" style="background-color: LightBlue; color: Black;">
                                                FECHA DE PRODUCCIÓN
                                            </td>
                                            <td align="left" class="Etiqueta" style="width: 150px; height: 20px" valign="middle">
                                                <asp:TextBox ID="txtFechaProduccionTinto4" runat="server" CssClass="input" Width="80px"></asp:TextBox>
                                                &nbsp;<img onclick="popUpCalendar(this, form1.txtFechaProduccionTinto4, 'dd/mm/yyyy')"
                                                    height="16" width="16" alt="Seleccionar fecha de Inst." src="../images/Calendario.gif"
                                                    border="0" />
                                            </td>
                                            <td align="center" style="background-color: LightBlue; color: Black;">
                                                MÁQUINA
                                            </td>
                                            <td align="center" style="background-color: LightBlue; color: Black;">
                                                <asp:TextBox ID="txtMaquinaTinto4" runat="server" class="input" MaxLength="200"></asp:TextBox>
                                            </td>
                                            <td align="center" style="background-color: LightBlue; color: Black;">
                                                SUPERVISOR
                                            </td>
                                            <td align="center" style="background-color: LightBlue; color: Black;">
                                                <asp:TextBox ID="txtSupervisorTinto4" runat="server" class="input" MaxLength="200"></asp:TextBox>
                                            </td>
                                            <td align="center" style="background-color: LightBlue; color: Black;">
                                                MAQUINISTA
                                            </td>
                                            <td align="center" style="background-color: LightBlue; color: Black;">
                                                <asp:TextBox ID="txtMAquinistaTinto4" runat="server" class="input" MaxLength="200"></asp:TextBox>
                                            </td>
                                            <td align="center" style="background-color: LightBlue; color: Black;">
                                                TURNO
                                            </td>
                                            <td align="center" style="background-color: LightBlue; color: Black;">
                                                <asp:TextBox ID="txtTurnoTinto4" runat="server" class="input" MaxLength="200"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="center" style="background-color: LightBlue; color: Black;">
                                                ARTÍCULO
                                            </td>
                                            <td align="center" style="background-color: LightBlue; color: Black;">
                                                <asp:TextBox ID="txtArticuloTinto4" runat="server" class="input" MaxLength="200"></asp:TextBox>
                                            </td>
                                            <td align="center" style="background-color: LightBlue; color: Black;">
                                                FICHA
                                            </td>
                                            <td align="center" style="background-color: LightBlue; color: Black;">
                                                <asp:TextBox ID="txtFichaTinto4" runat="server" class="input" MaxLength="200"></asp:TextBox>
                                            </td>
                                            <td align="center" style="background-color: LightBlue; color: Black;">
                                                PROCESO
                                            </td>
                                            <td align="center" style="background-color: LightBlue; color: Black;">
                                                <asp:TextBox ID="txtProcesoTinto4" runat="server" class="input" MaxLength="200"></asp:TextBox>
                                            </td>
                                            <td align="center" style="background-color: LightBlue; color: Black;">
                                                METROS
                                            </td>
                                            <td align="center" style="background-color: LightBlue; color: Black;">
                                                <asp:TextBox ID="txtMetrosTinto4" runat="server" class="input" MaxLength="200"></asp:TextBox>
                                            </td>
                                            <td align="center" style="background-color: LightBlue; color: Black;">
                                                ANALISTA
                                            </td>
                                            <td align="center" style="background-color: LightBlue; color: Black;">
                                                <asp:TextBox ID="txtAnalistaTinto4" runat="server" class="input" MaxLength="200"></asp:TextBox>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <table align="center" border="0" cellpadding="0" cellspacing="0" style="margin: 5px;">
                                        <tr align="center" class="RowHead">
                                            <td>
                                                PROCESO DE CHAMUSCADO / ENROLLADO
                                            </td>
                                            <td>
                                                VALORES
                                            </td>
                                            <td>
                                                OBSERVACIONES
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="center" style="background-color: LightBlue; color: Black;">
                                                VELOCIDAD DE MÁQUINA (m/min)
                                            </td>
                                            <td align="center" style="background-color: LightBlue; color: Black;">
                                                <asp:TextBox ID="txtVelocidadMaquinaTinto4" runat="server" class="input" MaxLength="200"></asp:TextBox>
                                            </td>
                                            <td align="center" style="background-color: LightBlue; color: Black;">
                                                <asp:TextBox ID="txtObsVelocidadMaquinaTinto4" runat="server" class="input" MaxLength="200"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="center" style="background-color: LightBlue; color: Black;">
                                                TEMPERATURA BANDA (°C)
                                            </td>
                                            <td align="center" style="background-color: LightBlue; color: Black;">
                                                <asp:TextBox ID="txtTemperaturaBandaTinto4" runat="server" class="input" MaxLength="200"></asp:TextBox>
                                            </td>
                                            <td align="center" style="background-color: LightBlue; color: Black;">
                                                <asp:TextBox ID="txtObsTemperaturaBandaTinto4" runat="server" class="input" MaxLength="200"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="center" style="background-color: LightBlue; color: Black;">
                                                TEMPERATURA PALMER (°C)
                                            </td>
                                            <td align="center" style="background-color: LightBlue; color: Black;">
                                                <asp:TextBox ID="txtTemperaturaPalmerTinto4" runat="server" class="input" MaxLength="200"></asp:TextBox>
                                            </td>
                                            <td align="center" style="background-color: LightBlue; color: Black;">
                                                <asp:TextBox ID="txtObsTemperaturaPalmerTinto4" runat="server" class="input" MaxLength="200"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="center" style="background-color: LightBlue; color: Black;">
                                                OTROS
                                            </td>
                                            <td align="center" style="background-color: LightBlue; color: Black;">
                                                <asp:TextBox ID="txtOtrosTinto4" runat="server" class="input" MaxLength="200"></asp:TextBox>
                                            </td>
                                            <td align="center" style="background-color: LightBlue; color: Black;">
                                                <asp:TextBox ID="txtObsOtrosTinto4" runat="server" class="input" MaxLength="200"></asp:TextBox>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                                <td>
                                    <table align="center" border="0" cellpadding="0" cellspacing="0" style="margin: 5px;">
                                        <tr align="center" class="RowHead">
                                            <td>
                                                EVALUACIÓN DE PROCESO
                                            </td>
                                            <td>
                                                VALORES
                                            </td>
                                            <td>
                                                OBSERVACIONES
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="center" style="background-color: LightBlue; color: Black;">
                                                ANCHO DE INGRESO (cm)
                                            </td>
                                            <td align="center" style="background-color: LightBlue; color: Black;">
                                                <asp:TextBox ID="txtAnchoIngresoTinto4" runat="server" class="input" MaxLength="200"></asp:TextBox>
                                            </td>
                                            <td align="center" style="background-color: LightBlue; color: Black;">
                                                <asp:TextBox ID="txtObsAnchoIngresoTinto4" runat="server" class="input" MaxLength="200"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="center" style="background-color: LightBlue; color: Black;">
                                                ANCHO DE SALIDA (cm)
                                            </td>
                                            <td align="center" style="background-color: LightBlue; color: Black;">
                                                <asp:TextBox ID="txtAnchoSalidaTinto4" runat="server" class="input" MaxLength="200"></asp:TextBox>
                                            </td>
                                            <td align="center" style="background-color: LightBlue; color: Black;">
                                                <asp:TextBox ID="txtObsAnchoSalidaTinto4" runat="server" class="input" MaxLength="200"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="center" style="background-color: LightBlue; color: Black;">
                                                PRUEBA DE TENDIDO (ANCHO ACABADO &gt; 170 cm)
                                            </td>
                                            <td align="center" style="background-color: LightBlue; color: Black;">
                                                <asp:TextBox ID="txtPruebaTendidoAnchoAcbTinto4" runat="server" class="input" MaxLength="200"></asp:TextBox>
                                            </td>
                                            <td align="center" style="background-color: LightBlue; color: Black;">
                                                <asp:TextBox ID="txtObsPruebaTendidoAnchoAcbTinto4" runat="server" class="input"
                                                    MaxLength="200"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="center" style="background-color: LightBlue; color: Black;">
                                                OTROS
                                            </td>
                                            <td align="center" style="background-color: LightBlue; color: Black;">
                                                <asp:TextBox ID="txtOtrosTinto44" runat="server" class="input" MaxLength="200"></asp:TextBox>
                                            </td>
                                            <td align="center" style="background-color: LightBlue; color: Black;">
                                                <asp:TextBox ID="txtObsOtrosTinto44" runat="server" class="input" MaxLength="200"></asp:TextBox>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Button ID="btnTintoreria" runat="server" CssClass="BotonVerde" Text="Guardar" />
                                </td>
                                <td>
                                    <asp:Button ID="btnTerminarTintoreria" runat="server" CssClass="btnAzul" Text="Concluir Proceso" />
                                </td>
                            </tr>
                        </table>
                        <table id="TablaLaboratorioFisico" runat="server" border="0" cellpadding="0" cellspacing="0"
                            class="ttable" style="width: 824px">
                            <tr>
                                <td class="RowHead" rowspan="12">
                                    LABORATORIO DE FÍSICO
                                </td>
                                <td colspan="2">
                                    <table align="center" border="1" cellpadding="0" cellspacing="0">
                                        <tr>
                                            <td align="center" style="background-color: LightBlue; color: Black;">
                                                FECHA DE EVALUACIÓN
                                            </td>
                                            <td align="left" class="Etiqueta" style="width: 150px; height: 20px" valign="middle">
                                                <asp:TextBox ID="txtFechaEvaluacionLaboFisico" runat="server" CssClass="input" Width="80px"></asp:TextBox>
                                                &nbsp;<img onclick="popUpCalendar(this, form1.txtFechaEvaluacionLaboFisico, 'dd/mm/yyyy')"
                                                    height="16" width="16" alt="Seleccionar fecha de Inst." src="../images/Calendario.gif"
                                                    border="0" />
                                            </td>
                                            <td align="center" style="background-color: LightBlue; color: Black;">
                                                LABORATORISTA
                                            </td>
                                            <td align="center" style="background-color: LightBlue; color: Black;">
                                                <asp:TextBox ID="txtLaboratoristaLaboFisi" runat="server" class="input" MaxLength="200"></asp:TextBox>
                                            </td>
                                            <td align="center" style="background-color: LightBlue; color: Black;">
                                                TURNO
                                            </td>
                                            <td align="center" style="background-color: LightBlue; color: Black;">
                                                <asp:TextBox ID="txtTurnoLaboFisi" runat="server" class="input" MaxLength="200"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="center" style="background-color: LightBlue; color: Black;">
                                                ARTÍCULO
                                            </td>
                                            <td align="center" style="background-color: LightBlue; color: Black;">
                                                <asp:TextBox ID="txtArticuloLaboFisi" runat="server" class="input" MaxLength="200"></asp:TextBox>
                                            </td>
                                            <td align="center" style="background-color: LightBlue; color: Black;">
                                                FICHA
                                            </td>
                                            <td align="center" style="background-color: LightBlue; color: Black;">
                                                <asp:TextBox ID="txtFichaLaboFisi" runat="server" class="input" MaxLength="200"></asp:TextBox>
                                            </td>
                                            <td align="center" style="background-color: LightBlue; color: Black;">
                                                PROCESO
                                            </td>
                                            <td align="center" style="background-color: LightBlue; color: Black;">
                                                <asp:TextBox ID="txtProcesoLaboFisi" runat="server" class="input" MaxLength="200"></asp:TextBox>
                                            </td>
                                            <td align="center" style="background-color: LightBlue; color: Black;">
                                                TIPO DE ACABADO
                                            </td>
                                            <td align="center" style="background-color: LightBlue; color: Black;">
                                                <asp:TextBox ID="txtTipoAcabadoLaboFisi" runat="server" class="input" MaxLength="200"></asp:TextBox>
                                            </td>
                                            <td align="center" style="background-color: LightBlue; color: Black;">
                                                METROS
                                            </td>
                                            <td align="center" style="background-color: LightBlue; color: Black;">
                                                <asp:TextBox ID="txtMetrosLaboFisi" runat="server" class="input" MaxLength="200"></asp:TextBox>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <table align="center" border="0" cellpadding="0" cellspacing="0" style="margin: 5px;">
                                        <tr align="center" class="RowHead">
                                            <td>
                                                PROCESO DE TEJIDO
                                            </td>
                                            <td>
                                                VALORES
                                            </td>
                                            <td>
                                                OBSERVACIONES
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="center" style="background-color: LightBlue; color: Black;">
                                                ANCHO ACABADO (cm)
                                            </td>
                                            <td align="center" style="background-color: LightBlue; color: Black;">
                                                <asp:TextBox ID="txtANCHOACABADOLaboFisi" runat="server" class="input" MaxLength="200"></asp:TextBox>
                                            </td>
                                            <td align="center" style="background-color: LightBlue; color: Black;">
                                                <asp:TextBox ID="txtObsANCHOACABADOLaboFisi" runat="server" class="input" MaxLength="200"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="center" style="background-color: LightBlue; color: Black;">
                                                ENCOG. DE URDIMBRE (%)
                                            </td>
                                            <td align="center" style="background-color: LightBlue; color: Black;">
                                                <asp:TextBox ID="txtENCOGURDIMBRELaboFisi" runat="server" class="input" MaxLength="200"></asp:TextBox>
                                            </td>
                                            <td align="center" style="background-color: LightBlue; color: Black;">
                                                <asp:TextBox ID="txtObsENCOGURDIMBRELaboFisi" runat="server" class="input" MaxLength="200"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="center" style="background-color: LightBlue; color: Black;">
                                                ENCOG. DE TRAMA (%)
                                            </td>
                                            <td align="center" style="background-color: LightBlue; color: Black;">
                                                <asp:TextBox ID="txtENCOGTRAMALaboFisi" runat="server" class="input" MaxLength="200"></asp:TextBox>
                                            </td>
                                            <td align="center" style="background-color: LightBlue; color: Black;">
                                                <asp:TextBox ID="txtObsENCOGTRAMALaboFisi" runat="server" class="input" MaxLength="200"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="center" style="background-color: LightBlue; color: Black;">
                                                ELONGACIÓN (%)
                                            </td>
                                            <td align="center" style="background-color: LightBlue; color: Black;">
                                                <asp:TextBox ID="txtELONGACIÓNLaboFisi" runat="server" class="input" MaxLength="200"></asp:TextBox>
                                            </td>
                                            <td align="center" style="background-color: LightBlue; color: Black;">
                                                <asp:TextBox ID="txtObsELONGACIÓNLaboFisi" runat="server" class="input" MaxLength="200"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="center" style="background-color: LightBlue; color: Black;">
                                                REVIRADO (DERECHO)
                                            </td>
                                            <td align="center" style="background-color: LightBlue; color: Black;">
                                                <asp:TextBox ID="txtREVIRADODERECHOLaboFisi" runat="server" class="input" MaxLength="200"></asp:TextBox>
                                            </td>
                                            <td align="center" style="background-color: LightBlue; color: Black;">
                                                <asp:TextBox ID="txtObsREVIRADODERECHOLaboFisi" runat="server" class="input" MaxLength="200"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="center" style="background-color: LightBlue; color: Black;">
                                                REVIRADO (CENTRO)
                                            </td>
                                            <td align="center" style="background-color: LightBlue; color: Black;">
                                                <asp:TextBox ID="txtReviradoCentroLaboFisi" runat="server" class="input" MaxLength="200"></asp:TextBox>
                                            </td>
                                            <td align="center" style="background-color: LightBlue; color: Black;">
                                                <asp:TextBox ID="txtObsReviradoCentroLaboFisi" runat="server" class="input" MaxLength="200"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="center" style="background-color: LightBlue; color: Black;">
                                                REVIRADO (IZQUIERDO)
                                            </td>
                                            <td align="center" style="background-color: LightBlue; color: Black;">
                                                <asp:TextBox ID="txtReviradoIzquierdoLaboFisi" runat="server" class="input" MaxLength="200"></asp:TextBox>
                                            </td>
                                            <td align="center" style="background-color: LightBlue; color: Black;">
                                                <asp:TextBox ID="txtObsReviradoIzquierdoLaboFisi" runat="server" class="input" MaxLength="200"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="center" style="background-color: LightBlue; color: Black;">
                                                RESISTENCIA DE URDIMBRE (KG-F)
                                            </td>
                                            <td align="center" style="background-color: LightBlue; color: Black;">
                                                <asp:TextBox ID="txtResistenciaUrdimbreLaboFisi" runat="server" class="input" MaxLength="200"></asp:TextBox>
                                            </td>
                                            <td align="center" style="background-color: LightBlue; color: Black;">
                                                <asp:TextBox ID="txtObsResistenciaUrdimbreLaboFisi" runat="server" class="input"
                                                    MaxLength="200"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="center" style="background-color: LightBlue; color: Black;">
                                                RESISTENCIA DE TRAMA (KG-F)
                                            </td>
                                            <td align="center" style="background-color: LightBlue; color: Black;">
                                                <asp:TextBox ID="txtResistenciaTramaLaboFisi" runat="server" class="input" MaxLength="200"></asp:TextBox>
                                            </td>
                                            <td align="center" style="background-color: LightBlue; color: Black;">
                                                <asp:TextBox ID="txtObsResistenciaTramaLaboFisi" runat="server" class="input" MaxLength="200"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="center" style="background-color: LightBlue; color: Black;">
                                                PESO (oz/yd2)
                                            </td>
                                            <td align="center" style="background-color: LightBlue; color: Black;">
                                                <asp:TextBox ID="txtPesoLaboFisi" runat="server" class="input" MaxLength="200"></asp:TextBox>
                                            </td>
                                            <td align="center" style="background-color: LightBlue; color: Black;">
                                                <asp:TextBox ID="txtObsPesoLaboFisi" runat="server" class="input" MaxLength="200"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="center" style="background-color: LightBlue; color: Black;">
                                                OTROS
                                            </td>
                                            <td align="center" style="background-color: LightBlue; color: Black;">
                                                <asp:TextBox ID="txtOtrosLaboFisi" runat="server" class="input" MaxLength="200"></asp:TextBox>
                                            </td>
                                            <td align="center" style="background-color: LightBlue; color: Black;">
                                                <asp:TextBox ID="txObstOtrosLaboFisi" runat="server" class="input" MaxLength="200"></asp:TextBox>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                                <td>
                                    <table align="center" border="0" cellpadding="0" cellspacing="0" style="margin: 5px;">
                                        <tr align="center" class="RowHead">
                                            <td>
                                                EVALUACIÓN DE TEJIDO
                                            </td>
                                            <td>
                                                VALORES
                                            </td>
                                            <td>
                                                OBSERVACIONES
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="center" style="background-color: LightBlue; color: Black;">
                                                RETIRO DE LYCRA (mm)
                                            </td>
                                            <td align="center" style="background-color: LightBlue; color: Black;">
                                                <asp:TextBox ID="txtRetroLycraLaboFisi" runat="server" class="input" MaxLength="200"></asp:TextBox>
                                            </td>
                                            <td align="center" style="background-color: LightBlue; color: Black;">
                                                <asp:TextBox ID="txtObsRetroLycraLaboFisi" runat="server" class="input" MaxLength="200"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="center" style="background-color: LightBlue; color: Black;">
                                                PRUEBA DE RETORNO - RECOVERY (%)
                                            </td>
                                            <td align="center" style="background-color: LightBlue; color: Black;">
                                                <asp:TextBox ID="txtPRUEBARETORNORECOVERYLaboFisi" runat="server" class="input" MaxLength="200"></asp:TextBox>
                                            </td>
                                            <td align="center" style="background-color: LightBlue; color: Black;">
                                                <asp:TextBox ID="txtObsPRUEBARETORNORECOVERYLaboFisi" runat="server" class="input"
                                                    MaxLength="200"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="center" style="background-color: LightBlue; color: Black;">
                                                SOLIDEZ AL FROTE HÚMEDO
                                            </td>
                                            <td align="center" style="background-color: LightBlue; color: Black;">
                                                <asp:TextBox ID="txtSolidezHumedoLaboFisi" runat="server" class="input" MaxLength="200"></asp:TextBox>
                                            </td>
                                            <td align="center" style="background-color: LightBlue; color: Black;">
                                                <asp:TextBox ID="txtSObsolidezHumedoLaboFisi" runat="server" class="input" MaxLength="200"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="center" style="background-color: LightBlue; color: Black;">
                                                SOLIDEZ AL FROTE SECO
                                            </td>
                                            <td align="center" style="background-color: LightBlue; color: Black;">
                                                <asp:TextBox ID="txtSolidezSecoLaboFisi" runat="server" class="input" MaxLength="200"></asp:TextBox>
                                            </td>
                                            <td align="center" style="background-color: LightBlue; color: Black;">
                                                <asp:TextBox ID="txtObsSolidezSecoLaboFisi" runat="server" class="input" MaxLength="200"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="center" style="background-color: LightBlue; color: Black;">
                                                SOLIDEZ AL LAVADO (TRANSFERENCIA DE COLOR)
                                            </td>
                                            <td align="center" style="background-color: LightBlue; color: Black;">
                                                <asp:TextBox ID="txtSolidezLavadoTransferenciaLaboFisi" runat="server" class="input"
                                                    MaxLength="200"></asp:TextBox>
                                            </td>
                                            <td align="center" style="background-color: LightBlue; color: Black;">
                                                <asp:TextBox ID="txtObsSolidezLavadoTransferenciaLaboFisi" runat="server" class="input"
                                                    MaxLength="200"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="center" style="background-color: LightBlue; color: Black;">
                                                SOLIDEZ AL LAVADO (CAMBIO DE COLOR)
                                            </td>
                                            <td align="center" style="background-color: LightBlue; color: Black;">
                                                <asp:TextBox ID="txtSolidezLavadoLaboFisi" runat="server" class="input" MaxLength="200"></asp:TextBox>
                                            </td>
                                            <td align="center" style="background-color: LightBlue; color: Black;">
                                                <asp:TextBox ID="txtObsObsSolidezLavadoLaboFisi" runat="server" class="input" MaxLength="200"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="center" style="background-color: LightBlue; color: Black;">
                                                DENSIDAD DE TEJIDO (pasadas/pulgadas)
                                            </td>
                                            <td align="center" style="background-color: LightBlue; color: Black;">
                                                <asp:TextBox ID="txtDensidadTejdoLaboFisi" runat="server" class="input" MaxLength="200"></asp:TextBox>
                                            </td>
                                            <td align="center" style="background-color: LightBlue; color: Black;">
                                                <asp:TextBox ID="txtObsDensidadTejdoLaboFisi" runat="server" class="input" MaxLength="200"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="center" style="background-color: LightBlue; color: Black;">
                                                DENSIDAD DE TEJIDO (hilos/pulgadas)
                                            </td>
                                            <td align="center" style="background-color: LightBlue; color: Black;">
                                                <asp:TextBox ID="txtDensidadHilosTejdoLaboFisi" runat="server" class="input" MaxLength="200"></asp:TextBox>
                                            </td>
                                            <td align="center" style="background-color: LightBlue; color: Black;">
                                                <asp:TextBox ID="txtObsDensidadHilosTejdoLaboFisi" runat="server" class="input" MaxLength="200"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="center" style="background-color: LightBlue; color: Black;">
                                                OTROS
                                            </td>
                                            <td align="center" style="background-color: LightBlue; color: Black;">
                                                <asp:TextBox ID="txtOtrosLaboFisi2" runat="server" class="input" MaxLength="200"></asp:TextBox>
                                            </td>
                                            <td align="center" style="background-color: LightBlue; color: Black;">
                                                <asp:TextBox ID="txtObsOtrosLaboFisi2" runat="server" class="input" MaxLength="200"></asp:TextBox>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Button ID="btnLaboratorioFisico" runat="server" CssClass="BotonVerde" Height="22px"
                                        Text="Guardar" />
                                </td>
                                <td>
                                    <asp:Button ID="btnTerminanoLaboratorioFisico" runat="server" CssClass="btnAzul"
                                        Text="Concluir Proceso" />
                                </td>
                            </tr>
                        </table>
                        <table id="TablaRevisionFinal" runat="server" border="0" cellpadding="0" cellspacing="0"
                            class="ttable" style="width: 924px">
                            <tr>
                                <td class="RowHead" rowspan="12">
                                    REVISIÓN FINAL
                                </td>
                                <td colspan="2">
                                    <table align="center" border="1" cellpadding="0" cellspacing="0">
                                        <tr>
                                            <td align="center" style="background-color: LightBlue; color: Black;">
                                                FECHA DE MAPEADO
                                            </td>
                                            <td align="left" class="Etiqueta" style="width: 150px; height: 20px" valign="middle">
                                                <asp:TextBox ID="txtFechaMapeadoRFinal" runat="server" CssClass="input" Width="70px"></asp:TextBox>
                                                &nbsp;<img onclick="popUpCalendar(this, form1.txtFechaMapeadoRFinal, 'dd/mm/yyyy')"
                                                    height="16" width="16" alt="Seleccionar fecha de Inst." src="../images/Calendario.gif"
                                                    border="0" />
                                            </td>
                                            <td align="center" style="background-color: LightBlue; color: Black;">
                                                REVISADOR
                                            </td>
                                            <td align="center" style="background-color: LightBlue; color: Black;">
                                                <asp:TextBox ID="txtRevisadorRfinal" runat="server" class="input" MaxLength="200"></asp:TextBox>
                                            </td>
                                            <td align="center" style="background-color: LightBlue; color: Black;">
                                                TURNO
                                            </td>
                                            <td align="center" style="background-color: LightBlue; color: Black;">
                                                <asp:TextBox ID="txtTurno" runat="server" class="input" MaxLength="200"></asp:TextBox>
                                            </td>
                                            <td align="center" style="background-color: LightBlue; color: Black;">
                                                Artículo
                                            </td>
                                            <td align="center" style="background-color: LightBlue; color: Black;">
                                                <asp:TextBox ID="txtArticuloRfinal" runat="server" class="input" MaxLength="200"></asp:TextBox>
                                            </td>
                                            <td align="center" style="background-color: LightBlue; color: Black;">
                                                FICHA
                                            </td>
                                            <td align="center" style="background-color: LightBlue; color: Black;">
                                                <asp:TextBox ID="txtFichaRFinal" runat="server" class="input" MaxLength="200"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="center" style="background-color: LightBlue; color: Black;">
                                                FECHA DE CORTE
                                            </td>
                                            <td align="left" class="Etiqueta" style="width: 150px; height: 20px" valign="middle">
                                                <asp:TextBox ID="txtFechaCorteRFinal" runat="server" CssClass="input" Width="70px"></asp:TextBox>
                                                &nbsp;<img onclick="popUpCalendar(this, form1.txtFechaCorteRFinal, 'dd/mm/yyyy')"
                                                    height="16" width="16" alt="Seleccionar fecha de Inst." src="../images/Calendario.gif"
                                                    border="0" />
                                            </td>
                                            <td align="center" style="background-color: LightBlue; color: Black;">
                                                CORTADOR
                                            </td>
                                            <td align="center" style="background-color: LightBlue; color: Black;">
                                                <asp:TextBox ID="txtCortadorRFinal" runat="server" class="input" MaxLength="200"></asp:TextBox>
                                            </td>
                                            <td align="center" style="background-color: LightBlue; color: Black;">
                                                ANALISTA
                                            </td>
                                            <td align="center" style="background-color: LightBlue; color: Black;">
                                                <asp:TextBox ID="txtAnalistaRfinal" runat="server" class="input" MaxLength="200"></asp:TextBox>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <table align="center" border="0" cellpadding="0" cellspacing="0" style="margin: 5px;">
                                        <tr align="center" class="RowHead">
                                            <td>
                                                EVALUACIÓN DE CALIDAD
                                            </td>
                                            <td>
                                                VALORES
                                            </td>
                                            <td>
                                                OBSERVACIONES
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="center" style="background-color: LightBlue; color: Black;">
                                                METROS CORTADOS (m)
                                            </td>
                                            <td align="center" style="background-color: LightBlue; color: Black;">
                                                <asp:TextBox ID="txtMetrosCortadosRFinal" runat="server" class="input" MaxLength="200"></asp:TextBox>
                                            </td>
                                            <td align="center" style="background-color: LightBlue; color: Black;">
                                                <asp:TextBox ID="txtObsMetrosCortadosRFinal" runat="server" class="input" MaxLength="200"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="center" style="background-color: LightBlue; color: Black;">
                                                METROS DE SEGUNDAS + OBSERVADAS (m)
                                            </td>
                                            <td align="center" style="background-color: LightBlue; color: Black;">
                                                <asp:TextBox ID="txtMetrosSegundasRFinal" runat="server" class="input" MaxLength="200"></asp:TextBox>
                                            </td>
                                            <td align="center" style="background-color: LightBlue; color: Black;">
                                                <asp:TextBox ID="txtObsMetrosSegundasRFinal" runat="server" class="input" MaxLength="200"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="center" style="background-color: LightBlue; color: Black;">
                                                PRINCIPAL DEFECTO 1 (%)
                                            </td>
                                            <td align="center" style="background-color: LightBlue; color: Black;">
                                                <asp:TextBox ID="txtPrincipalDefectoRFinal" runat="server" class="input" MaxLength="200"></asp:TextBox>
                                            </td>
                                            <td align="center" style="background-color: LightBlue; color: Black;">
                                                <asp:TextBox ID="txtObsPrincipalDefectoRFinal" runat="server" class="input" MaxLength="200"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="center" style="background-color: LightBlue; color: Black;">
                                                OTROS
                                            </td>
                                            <td align="center" style="background-color: LightBlue; color: Black;">
                                                <asp:TextBox ID="txtOtrosRfinal" runat="server" class="input" MaxLength="200"></asp:TextBox>
                                            </td>
                                            <td align="center" style="background-color: LightBlue; color: Black;">
                                                <asp:TextBox ID="txtObsOtrosRfinal" runat="server" class="input" MaxLength="200"></asp:TextBox>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                                <td>
                                    <table align="center" border="0" cellpadding="0" cellspacing="0" style="margin: 5px;">
                                        <tr align="center" class="RowHead">
                                            <td>
                                                EVALUACIÓN DE CALIDAD
                                            </td>
                                            <td>
                                                VALORES
                                            </td>
                                            <td>
                                                OBSERVACIONES
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="center" style="background-color: LightBlue; color: Black;">
                                                PRINCIPAL DEFECTO 2 (%)
                                            </td>
                                            <td align="center" style="background-color: LightBlue; color: Black;">
                                                <asp:TextBox ID="txtPrincipalDefecto2RFinal" runat="server" class="input" MaxLength="200"></asp:TextBox>
                                            </td>
                                            <td align="center" style="background-color: LightBlue; color: Black;">
                                                <asp:TextBox ID="txtObsPrincipalDefecto2RFinal" runat="server" class="input" MaxLength="200"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="center" style="background-color: LightBlue; color: Black;">
                                                PRINCIPAL DEFECTO 3 (%)
                                            </td>
                                            <td align="center" style="background-color: LightBlue; color: Black;">
                                                <asp:TextBox ID="txtPrinciaplDefecto3RFinal" runat="server" class="input" MaxLength="200"></asp:TextBox>
                                            </td>
                                            <td align="center" style="background-color: LightBlue; color: Black;">
                                                <asp:TextBox ID="txtObsPrinciaplDefecto3RFinal" runat="server" class="input" MaxLength="200"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="center" style="background-color: LightBlue; color: Black;">
                                                OTROS
                                            </td>
                                            <td align="center" style="background-color: LightBlue; color: Black;">
                                                <asp:TextBox ID="txtOtrosRFinal2" runat="server" class="input" MaxLength="200"></asp:TextBox>
                                            </td>
                                            <td align="center" style="background-color: LightBlue; color: Black;">
                                                <asp:TextBox ID="txtObsOtrosRFinal2" runat="server" class="input" MaxLength="200"></asp:TextBox>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Button ID="btnRevisionFinal" runat="server" CssClass="BotonVerde" Text="Guardar" />
                                </td>
                                <td>
                                    <asp:Button ID="btnTerminoRevisionFinal" runat="server" CssClass="btnAzul" Text="Concluir Proceso" />
                                </td>
                            </tr>
                        </table>
                        <br />
                        <table id="TablaConclusiones" runat="server" class="ttable" width="924px">
                            <caption>
                                <br />
                                <tr>
                                    <td align="center">
                                        <h1>
                                            CONCLUSION FINAL</h1>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="left" class="Descripcion" colspan="7" style="width: 900px">
                                        <asp:TextBox ID="txtConclusionFinal" runat="server" CssClass="txtAreaHabilitado"
                                            Font-Size="9px" Height="68px" TextMode="MultiLine" Width="900px"></asp:TextBox>
                                        <br />
                                        &nbsp;
                                    </td>
                                </tr>
                            </caption>
                        </table>
                        <table style="text-align: center;" width="944px">
                            <tr>
                                <td class="style19" >
                                    <strong>JEFE DE ASEG DE CALIDAD<br />
                                        (firma y fecha) </strong>
                                    <table>
                                        <tr>
                                            <td>
                                                <%-- <asp:ScriptManager ID="ScriptManager2" runat="server">
                                    </asp:ScriptManager>--%>
                                                <asp:UpdatePanel ID="UpdatePanel5" runat="server" UpdateMode="Always">
                                                    <ContentTemplate>
                                                      <asp:TextBox ID="txtFechaCalidad" runat="server" CssClass="input" Width="80px"></asp:TextBox>
                                                &nbsp;<img onclick="popUpCalendar(this, form1.txtFechaCalidad, 'dd/mm/yyyy')" runat=server id="im1"
                                                    height="16" width="16" alt="Seleccionar fecha de Inst." src="../images/Calendario.gif"
                                                    border="0" />
                                                        <asp:GridView ID="gvFirmasCalidad" runat="server" AutoGenerateColumns="false" ShowFooter="true"
                                                            ShowHeader="true">
                                                            <Columns>
                                                                <asp:TemplateField HeaderText="">
                                                                    <ItemTemplate>
                                                                        <asp:TextBox ID="txtCampoFirmaCal" runat="server" Width="100px" Text='<%# Eval("VCH_USU_FIRMA") %>'
                                                                            CssClass="txtEnabled" Enabled="false"></asp:TextBox>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField>
                                                                    <ItemTemplate>
                                                                        <asp:TextBox ID="txtCampoFFirmaCal" runat="server" Width="250px" Text='<%# Eval("NO_USUA") %>'
                                                                            CssClass="txtEnabled" Enabled="false"></asp:TextBox>
                                                                    </ItemTemplate>
                                                                    <FooterTemplate>
                                                                        <asp:DropDownList ID="ddlFirmasCal" runat="server" DataSource='<%# fn_CargarFirmas() %>'
                                                                            DataTextField="NO_USUA" DataValueField="var_Dato">
                                                                        </asp:DropDownList>
                                                                    </FooterTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField Visible="false" ItemStyle-HorizontalAlign="Center">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblEstadoFirmaCal" runat="server" Width="100px" Font-Bold="true" Text='<%# Eval("VCH_EST_FIRMA") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField>
                                                                    <ItemTemplate>
                                                                        <asp:ImageButton ID="btnEliminarCal" runat="server" ImageUrl="~/Imagenes/delete.gif"
                                                                            CommandName="Eliminar" CommandArgument='<%# Eval("VCH_USU_FIRMA") %>'></asp:ImageButton>
                                                                    </ItemTemplate>
                                                                    <FooterTemplate>
                                                                        <asp:ImageButton ID="btnAgregarCal" runat="server" ImageUrl="~/Imagenes/Adicionar.png"
                                                                            CommandName="Agregar" CommandArgument=''></asp:ImageButton>
                                                                    </FooterTemplate>
                                                                </asp:TemplateField>
                                                            </Columns>
                                                        </asp:GridView>
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>
                                            </td>
                                           
                                        </tr>
                                        <tr>
                                            <td colspan="2">
                                           
                                                <asp:DropDownList ID="ddlFirmasCal" runat="server" DataTextField="NO_USUA" DataValueField="var_Dato">
                                                </asp:DropDownList>
                                            </td>
                                            <td>
                                                <asp:ImageButton ID="btnAgregarCal" runat="server" ImageUrl="~/Imagenes/Adicionar.png"
                                                    CommandName="Agregar" CommandArgument=''></asp:ImageButton>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                                <td class="style19">
                                    <strong>DIRECCIÓN TECNICAS<br />
                                        (firma y fecha) </strong>
                                    <table>
                                        <tr>
                                            <td>
                                                <%-- <asp:ScriptManager ID="ScriptManager2" runat="server">
                                    </asp:ScriptManager>--%>
                                                <asp:UpdatePanel ID="UpdatePanel6" runat="server" UpdateMode="Always">
                                                    <ContentTemplate>
                                                      <asp:TextBox ID="txtFechaDireccion" runat="server" CssClass="input" Width="80px"></asp:TextBox>
                                                &nbsp;<img onclick="popUpCalendar(this, form1.txtFechaDireccion, 'dd/mm/yyyy')" id="im2" runat=server
                                                    height="16" width="16" alt="Seleccionar fecha de Inst." src="../images/Calendario.gif"
                                                    border="0" />
                                                        <asp:GridView ID="gvFirmasDireccion" runat="server" AutoGenerateColumns="false" ShowFooter="true"
                                                            ShowHeader="true">
                                                            <Columns>
                                                                <asp:TemplateField HeaderText="">
                                                                    <ItemTemplate>
                                                                        <asp:TextBox ID="txtCampoFirmaDir" runat="server" Width="100px" Text='<%# Eval("VCH_USU_FIRMA") %>'
                                                                            CssClass="txtEnabled" Enabled="false"></asp:TextBox>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField>
                                                                    <ItemTemplate>
                                                                        <asp:TextBox ID="txtCampoFFirmaDir" runat="server" Width="250px" Text='<%# Eval("NO_USUA") %>'
                                                                            CssClass="txtEnabled" Enabled="false"></asp:TextBox>
                                                                    </ItemTemplate>
                                                                    <FooterTemplate>
                                                                        <asp:DropDownList ID="ddlFirmasDir" runat="server" DataSource='<%# fn_CargarFirmas() %>'
                                                                            DataTextField="NO_USUA" DataValueField="var_Dato">
                                                                        </asp:DropDownList>
                                                                    </FooterTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField Visible="false" ItemStyle-HorizontalAlign="Center">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblEstadoFirmaDir" runat="server" Width="100px" Font-Bold="true" Text='<%# Eval("VCH_EST_FIRMA") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField>
                                                                    <ItemTemplate>
                                                                        <asp:ImageButton ID="btnEliminarDir" runat="server" ImageUrl="~/Imagenes/delete.gif"
                                                                            CommandName="Eliminar" CommandArgument='<%# Eval("VCH_USU_FIRMA") %>'></asp:ImageButton>
                                                                    </ItemTemplate>
                                                                    <FooterTemplate>
                                                                        <asp:ImageButton ID="btnAgregarDir" runat="server" ImageUrl="~/Imagenes/Adicionar.png"
                                                                            CommandName="Agregar" CommandArgument=''></asp:ImageButton>
                                                                    </FooterTemplate>
                                                                </asp:TemplateField>
                                                            </Columns>
                                                        </asp:GridView>
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>
                                            </td>
                                          
                                        </tr>
                                        <tr>
                                            <td colspan ="2">
                                            
                                                <asp:DropDownList ID="ddlFirmasDir" runat="server" DataTextField="NO_USUA" DataValueField="var_Dato">
                                                </asp:DropDownList>

                                            </td>
                                            <td>
                                                <asp:ImageButton ID="btnAgregarDir" runat="server" ImageUrl="~/Imagenes/Adicionar.png"
                                                    CommandName="Agregar" CommandArgument=''></asp:ImageButton>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="8">
                                    <asp:Button ID="btnGuardadoFinal" runat="server" CssClass="btnAzul" Text="Guardar" />
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>
    </form>
</body>
</html>
