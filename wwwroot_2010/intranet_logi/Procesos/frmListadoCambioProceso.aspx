<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="frmListadoCambioProceso.aspx.vb" Inherits="intranet_logi.frmListadoCambioProceso" %>

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
        .style6
        {
            font-family: Verdana, Geneva, sans-serif;
            font-weight: bold;
            font-size: 9px;
            color: #333333;
            background-color: #BCCAE0;
            border: 1px dotted #000099;
            text-align: left;
            margin-left: 0px;
            height: 20px;
        }
        .txtEnabled
        {
            background-color: #ffffff;
            color: #000000;
        }
        .style7
        {
            width: 57px;
        }
    </style>
    <script language="javascript" type="text/javascript">
        function fnc_BuscarMaquina() {
            var retorno = window.showModalDialog("../Buscadores/frmBusquedaMaquina.aspx", "", "dialogheight:450px;dialogwidth:450px;center:yes;help:no;");
            if (retorno != "" && retorno != ":") {
                var datos = retorno.split(":");
                var codigo = datos[0];
                var nombre = datos[1];
                document.all('txtMaquina').value = nombre;
                document.all('hdfCodMaquina').value = codigo;
            }
        }
        function fnc_BuscarResponsable() {
            var retorno = window.showModalDialog("../Buscadores/frmBusquedaResponsable.aspx", "", "dialogheight:450px;dialogwidth:450px;center:yes;help:no;");
            if (retorno != "" && retorno != ":") {
                var datos = retorno.split(":");
                var codigo = datos[0];
                var nombre = datos[1];
                document.all('txtResponsable').value = nombre;
                document.all('hdnResponsable').value = codigo;
            }
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <table style="width: 828px" cellspacing="0" cellpadding="0" border="0">
        <tr>
            <td colspan="8">
                <asp:ImageButton ID="imgbtnVolver" runat="server" ImageUrl="~/Imagenes/Cancelar.png"
                    Width="24px" Height="24px" ToolTip="Volver"></asp:ImageButton>Volver
            </td>
        </tr>
        <tr>
            <td></td>
        </tr>
            <tr>
                <td class="Cabecera" style="width: 800px; height: 30px" align="center">
                    Formato Cambio de Proceso
                </td>
            </tr>
        </table>
        <%--Inicio: Parametros Generales--%>
        <table style="width: 800px">
            <%--Tipo de vale--%>
            <tr>
                <td class="Descripcion" style="width: 130px" align="left">
                    Area:
                </td>
                <td align="left" class="style1" colspan="2">
                    &nbsp;<asp:DropDownList ID="ddlArea" runat="server" CssClass="cboFormulario" Font-Size="9px"
                        Width="153px" Height="17px">
                    </asp:DropDownList>
                    <asp:Button ID="btnpurb" runat="server" Text="." Visible="false"/>
                    <asp:HiddenField ID="hdfIdGenFormato" runat="server" />
                </td>
                <td class="Descripcion" style="width: 130px" align="left">
                    Maquina:
                </td>
                <td align="left" colspan="3">
                    <%--&nbsp;<asp:TextBox ID="txtMaquina" runat="server" 
                        Width="245px"></asp:TextBox>
                    <input id="btnSolicitante" runat="server" style="width:20px;" onclick="javascript:fnc_BuscarMaquina();" type="button" value="..." name="btnSolicitante" class="Boton"/>
                    <asp:HiddenField ID="hdfCodMaquina" runat="server" />--%>
                    <asp:TextBox ID="txtMaquina" runat="server" Width="245px" MaxLength="200"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="Descripcion" style="width: 130px" align="left" colspan="2">
                    Responsable de Proyecto:
                </td>
                <td align="left" class="style1" colspan="3">
                    <%--<asp:DropDownList ID="ddlResponsable" runat="server" 
                        CssClass="cboFormulario" Font-Size="9px"
                        Width="229px" Height="22px">
                    </asp:DropDownList>--%>
                    <asp:TextBox ID="txtResponsable" runat="server" 
                        Width="180px"></asp:TextBox>
                    <input id="btnResponsable" runat="server" style="width:20px;" onclick="javascript:fnc_BuscarResponsable();" type="button" value="..." name="btnSolicitante" class="Boton"/>
                    <asp:HiddenField ID="hdnResponsable" runat="server" />
                </td>
                <td class="style5" align="left">
                    Fecha Creación:
                </td>
                <td align="left" class="style1">
                    &nbsp;<asp:TextBox ID="txtFecInicio" runat="server" class="input" Width="96px" MaxLength="10" Enabled="false"></asp:TextBox>
                    <%--<img onclick="popUpCalendar(this, form1.txtFecInicio, 'dd/mm/yyyy')" height="16"
                        width="16" alt="Seleccionar fecha" src="../images/Calendario.gif" border="0" id="ibtnCalendar1" runat="server"/>--%>
                </td>
            </tr>
            <tr>
                <td class="Descripcion" style="width: 130px" align="left" colspan="7">
                    OBJETIVO DEL CAMBIO:
                </td>
            </tr>
            <tr>
                <td style="width: 680px" align="left" class="Descripcion" colspan="7">
                    <asp:TextBox ID="txtObjCambio" runat="server" CssClass="txtAreaHabilitado" Height="87px"
                        Font-Size="9px" Width="791px"  TextMode="MultiLine"></asp:TextBox>
                    <br />
                  
                </td>
            </tr>
            <tr>
                <td align="left" class="style1" colspan="7">
                </td>
            </tr>
            <tr>
                <td class="Descripcion" align="left" colspan="7">
                    INFORMACION /PARAMETROS RELEVANTES A EVALUAR (Indicar valores)
                </td>
            </tr>
            <tr>
                <td colspan="7">
                    <asp:GridView ID="gvBloque1" runat="server" AutoGenerateColumns="false" ShowFooter="true"
                        DataKeyNames="INT_COD_GENPAR" ShowHeader="false">
                        <Columns>
                            <%--<asp:TemplateField HeaderText="">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtCampo" runat="server" Width="450px" Text='<%# Eval("VCH_COD_CAMPO") %>'
                                        CssClass="txtEnabled" Enabled="false"></asp:TextBox>
                                </ItemTemplate>
                                <FooterTemplate>
                                    <asp:TextBox ID="txtCampoF" runat="server" Width="450px"></asp:TextBox>
                                </FooterTemplate>
                            </asp:TemplateField>--%>
                            <asp:TemplateField HeaderText="">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtValor" runat="server" Width="340px" Text='<%# Eval("VCH_VAL_CAMPO") %>'></asp:TextBox>
                                </ItemTemplate>
                                <FooterTemplate>
                                    <asp:TextBox ID="txtValorF" runat="server" Width="340px" MaxLength="200"></asp:TextBox>
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
                </td>
            </tr>
            <tr>
                <td class="style3" align="left" colspan="7">
                    DETALLES DE CONSIDERACIONES DE RIESGOS Y CONTINGENCIA POSIBLE:
                </td>
            </tr>
            <tr>
                <td style="width: 680px" align="left" class="Descripcion" colspan="7">
                    <asp:TextBox ID="txtConsRiesgo" runat="server" CssClass="txtAreaHabilitado" Height="87px"
                        Font-Size="9px" Width="791px"  TextMode="MultiLine"></asp:TextBox>
                    <br />
                  
                </td>
            </tr>
            <tr>
                <td align="left" class="style1" colspan="7">
                </td>
            </tr>
            <tr>
                <td colspan="7">
                    <asp:ScriptManager ID="script1" runat="server">
                    </asp:ScriptManager>
                    <%--<asp:UpdatePanel runat="server">
                        <ContentTemplate>--%>
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
                                        DataKeyNames="INT_COD_GENPAR" ShowHeader="true" >
                                        <Columns>
                                            <asp:TemplateField HeaderText="Parámetro">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtCampo1" runat="server" Width="450px" Text='<%# Eval("VCH_COD_CAMPO") %>'
                                                        CssClass="txtEnabled" Enabled="false"></asp:TextBox>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:TextBox ID="txtCampoF1" runat="server" Width="450px" MaxLength="200"></asp:TextBox>
                                                </FooterTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Valor">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtValor1" runat="server" Width="340px" Text='<%# Eval("VCH_VAL_CAMPO") %>' 
                                                        CssClass="txtEnabled" Enabled="false"></asp:TextBox>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:TextBox ID="txtValorF1" runat="server" Width="340px" MaxLength="200"></asp:TextBox>
                                                </FooterTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:ImageButton ID="btnEliminar" runat="server" ImageUrl="~/Imagenes/delete.gif"
                                                    CommandName="Eliminar" CommandArgument='<%# Eval("INT_COD_GENPAR") %>'></asp:ImageButton>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:ImageButton ID="btnAgregar" runat="server" ImageUrl="~/Imagenes/Adicionar.png"
                                                        CommandName="Agregar1" CommandArgument=''></asp:ImageButton>
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
                                                    <asp:TextBox ID="txtCampoF2" runat="server" Width="450px" MaxLength="200"></asp:TextBox>
                                                </FooterTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Valor">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtValor2" runat="server" Width="340px" Text='<%# Eval("VCH_VAL_CAMPO") %>'
                                                        CssClass="txtEnabled" Enabled="false"></asp:TextBox>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:TextBox ID="txtValorF2" runat="server" Width="340px" MaxLength="200"></asp:TextBox>
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
                                                    <asp:TextBox ID="txtCampoF3" runat="server" Width="450px" MaxLength="200"></asp:TextBox>
                                                </FooterTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Valor">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtValor3" runat="server" Width="340px" Text='<%# Eval("VCH_VAL_CAMPO") %>'
                                                        CssClass="txtEnabled" Enabled="false"></asp:TextBox>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:TextBox ID="txtValorF3" runat="server" Width="340px" MaxLength="200"></asp:TextBox>
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
                        <%--</ContentTemplate>
                    </asp:UpdatePanel>--%>
                </td>
            </tr>
            <tr>
               <td colspan="2" class="Descripcion" align="left">
                    <span id="Label4" class="Descripcion" align="left" style="display: inline-block;
                        width: 144px;">Informe</span>
                </td>
                <td colspan="2" style="text-align: center;">
                    <asp:FileUpload ID="fluAdjuntarFicha" runat="server" class="input" />
                    <asp:ImageButton ID="ibtDescargarfichas" runat="server" CommandArgument='<%# Eval("VCH_FILENAMEINFORME") %>'
                        CommandName="descargarArchivo" ImageUrl="~/Imagenes/download.png" Width="24px"
                        Height="24px" ToolTip="Descargar Archivo"></asp:ImageButton>
                    <asp:Label ID="lblficha" runat="server"></asp:Label>
                </td>
            </tr>
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
                    &nbsp;<asp:TextBox ID="txtFecConc" runat="server" class="input" Width="96px" MaxLength="10"></asp:TextBox>
                    <img onclick="popUpCalendar(this, form1.txtFecConc, 'dd/mm/yyyy')" height="16" width="16"
                        alt="Seleccionar fecha" src="../images/Calendario.gif" border="0" runat="server" id="ibtCalendar2" />
                </td>
            </tr>
            <tr>
                <td style="width: 680px" align="left" class="Descripcion" colspan="7">
                    <asp:TextBox ID="txtDescConcl" runat="server" CssClass="txtAreaHabilitado" Height="87px"
                        Font-Size="9px" Width="791px"  TextMode="MultiLine"></asp:TextBox>
                    <br />
                   
                </td>
            </tr>
            <tr>
                <td align="left" class="style1" colspan="7">
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
                                <asp:GridView ID="gvFirmas" runat="server" AutoGenerateColumns="false" ShowFooter="true"
                                    ShowHeader="false" >
                                    <Columns>
                                        <asp:TemplateField HeaderText="">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtCampoFirma" runat="server" Width="285px" Text='<%# Eval("VCH_USU_FIRMA") %>'
                                                    CssClass="txtEnabled" Enabled="false"></asp:TextBox>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:DropDownList id="ddlFirmas" runat="server" DataSource='<%# fn_CargarFirmas() %>'  
                                                                            DataTextField="NO_USUA" DataValueField="var_Dato"></asp:DropDownList>
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
                            </tr>
                        </table>
                    </div>
                </td>
            </tr>
            <tr>
                <td></td>
            </tr>
        </table>
        <table style="width: 820px">
            <tr>
                <td></td>
            </tr>
            <tr>
                <td colspan="7" align="right" class="style7">
                    <asp:Button ID="btnGuardar" runat="server" Text="Guardar" CssClass="Boton" 
                        Height="29px" Width="99px" OnClientClick="return confirm('Desea registrar la información?');"/>
                    <asp:Button ID="btnSolicitar" runat="server" Text="Solicitar" CssClass="Boton"  style="background-color:Yellow;color:Black;"
                        Height="29px" Width="99px" OnClientClick="return confirm('Desea solicitar aprobación?');"/>
                    <asp:Button ID="btnAprobar" runat="server" Text="Aprobar" CssClass="btnVerde" 
                        Height="29px" Width="99px" OnClientClick="return confirm('Desea aprobar el formato?');"/>
                    <asp:Button ID="btnAnular" runat="server" Text="Anular" CssClass="BotonRojo" 
                        Height="29px" Width="99px" OnClientClick="return confirm('Desea anular el formato?');"/>
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
