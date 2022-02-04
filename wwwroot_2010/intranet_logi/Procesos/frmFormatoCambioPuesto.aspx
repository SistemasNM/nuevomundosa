<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="frmFormatoCambioPuesto.aspx.vb"
    Inherits="intranet_logi.frmFormatoCambioPuesto" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="X-UA-Compatible" content="IE=8" />
    <title>Formato Cambio de Puesto</title>
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
        .txtEnabled
        {
            background-color: #ffffff;
            color: #000000;
        }
        .style1
        {
            font-family: Verdana, Geneva, sans-serif;
            font-weight: bold;
            font-size: 9px;
            color: #333333;
            background-color: #BCCAE0;
            border: 1px dotted #000099;
            text-align: left;
            margin-left: 0px;
            width: 458px;
        }
        .style2
        {
            border-width: thin;
            font-weight: bold;
            font-size: 14px;
            width: 100%;
            color: #ffffff;
            font-family: Verdana;
            background-color: #2A3A6E;
            height: 40px;
        }
        .style3
        {
            width: 70px;
        }
    </style>
    <script language="JavaScript" type="text/javascript">
        function soloNumeros(e) {
            var key = window.Event ? e.which : e.keyCode
            return (key >= 46 && key <= 57 && key != 47)
        }

        function fnc_BuscarPuestos() {
            var retorno = window.showModalDialog("../Buscadores/frmBusquedaPuesto.aspx", "", "dialogheight:450px;dialogwidth:450px;center:yes;help:no;");
            if (retorno != "" && retorno != ":") {
                var datos = retorno.split(":");
                var codigo = datos[0];
                var nombre = datos[1];
                document.all('hdfCodPuesto').value = codigo;
                document.all('txtPuestoProm').value = nombre;
                document.all('hdnCargarNuevoPuesto').value = "S";
                form1.submit();
            }
        }

        function fnc_BuscarTrabajador() {
            var retorno = window.showModalDialog("../Buscadores/frmBusquedaTrabajador.aspx", "", "dialogheight:500px;dialogwidth:670px;center:yes;help:no;");
            if (retorno != "" && retorno != ":") {
                var datos = retorno.split(":");
                var codigo = datos[0];
                var nombre = datos[1];
                document.all('hdfCodJefe').value = codigo;
                document.all('txtSuper').value = nombre;
            }
        }

        function fnc_BuscarTrabajador_2() {
            var retorno = window.showModalDialog("../Buscadores/frmBusquedaTrabajador.aspx", "", "dialogheight:500px;dialogwidth:670px;center:yes;help:no;");
            if (retorno != "" && retorno != ":") {
                var datos = retorno.split(":");
                var codigo = datos[0];
                var nombre = datos[1];
                document.all('hdfCodTrab').value = codigo;
                document.all('txtCompa').value = nombre;
            }
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <table style="width: 771px" cellspacing="0" cellpadding="0" border="0">
            <tr>
                <td colspan="8">
                    <asp:ImageButton ID="imgbtnVolver" runat="server" ImageUrl="~/Imagenes/Cancelar.png"
                        Width="24px" Height="24px" ToolTip="Volver"></asp:ImageButton>Volver
                </td>
            </tr>
            <tr>
                <td class="Cabecera" style="width: 800px; height: 30px" align="center">
                    EVALUACIÓN DE PERÍODO DE PRUEBA CAMBIO DE PUESTO
                </td>
            </tr>
        </table>
        <table style="width: 772px">
            <tr>
                <td align="left" class="style1" colspan="2">
                    Fecha Creación:
                </td>
                <td align="left" colspan="5">
                    &nbsp;<asp:TextBox ID="txtFecInicio" runat="server" class="input" Width="110px" Enabled="false"></asp:TextBox>
                    <%--<img onclick="popUpCalendar(this, form1.txtFecInicio, 'dd/mm/yyyy')" height="16"
                        width="16" alt="Seleccionar fecha" src="../images/Calendario.gif" border="0" runat="server" id="ibtCalendar1"/>--%>
                    <asp:HiddenField ID="hdfIdFormato" runat="server" />
                </td>
            </tr>
            <tr>
                <td align="center" colspan="7" class="Cabecera">
                    <h4>
                        DATOS DE TRABAJADOR</h4>
                </td>
            </tr>
            <tr>
                <td align="left" class="Descripcion" colspan="3">
                    Apellidos y Nombres:
                </td>
                <td align="left" colspan="5">
                    <asp:TextBox ID="txtNombres" runat="server" class="input" Width="387px" Enabled="false"
                        CssClass="txtEnabled"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td align="left" class="Descripcion" colspan="3">
                    Código:
                </td>
                <td align="left" colspan="5">
                    <asp:TextBox ID="txtCodigo" runat="server" class="input" Width="387px" AutoPostBack="true"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td align="left" class="Descripcion" colspan="3">
                    Puesto Actual:
                </td>
                <td align="left" colspan="5">
                    <asp:TextBox ID="txtPuestoAct" runat="server" class="input" Width="387px" Enabled="false"
                        CssClass="txtEnabled"></asp:TextBox>
                    <asp:HiddenField ID="hdfPuestoA" runat="server" />
                </td>
            </tr>
            <tr>
                <td align="left" class="Descripcion" colspan="3">
                    Puesto al que se promociona:
                </td>
                <td align="left" colspan="5">
                    <asp:TextBox ID="txtPuestoProm" runat="server" class="input" Width="361px"></asp:TextBox>
                    <input id="btnSolicitante" style="width: 20px;" onclick="javascript:fnc_BuscarPuestos();"
                        type="button" value="..." name="btnSolicitante" class="Boton" runat="server"/>
                    <asp:HiddenField ID="hdfCodPuesto" runat="server" />
                    <asp:HiddenField ID="hdnCargarNuevoPuesto" runat="server" />
                </td>
            </tr>
            <tr>
                <td align="left" class="Descripcion" colspan="3">
                    Área:
                </td>
                <td align="left" colspan="5">
                    <asp:TextBox ID="txtArea" runat="server" class="input" Width="387px" Enabled="false"
                        CssClass="txtEnabled"></asp:TextBox>
                    <asp:HiddenField ID="hdfArea" runat="server" />
                </td>
            </tr>
            <tr>
                <td align="left" class="Descripcion" colspan="3">
                    Fecha de inicio periódo de prueba:
                </td>
                <td align="left" colspan="5">
                    <asp:TextBox ID="txtFecIni" runat="server" class="input" Width="110px"></asp:TextBox>
                    <img onclick="popUpCalendar(this, form1.txtFecIni, 'dd/mm/yyyy')" height="16"
                        width="16" alt="Seleccionar fecha" src="../images/Calendario.gif" border="0" runat="server" id="ibtCalendar2" />
                </td>
            </tr>
            <tr>
                <td align="left" class="Descripcion" colspan="3">
                    Supervisor/Jefe directo:
                </td>
                <td align="left" colspan="5">
                    <asp:TextBox ID="txtSuper" runat="server" class="input" Width="351px"></asp:TextBox>
                    <input id="btnJefe" style="width: 20px;" onclick="javascript:fnc_BuscarTrabajador();"
                        type="button" value="..." name="btnSolicitante" class="Boton" runat="server"/>
                    <asp:HiddenField ID="hdfCodJefe" runat="server" />
                </td>
            </tr>
            <tr>
                <td align="left" class="style1" colspan="2">
                    Nombre de compañero asignado para su entrenamiento en nuevo puesto:
                </td>
                <td align="left" colspan="5">
                    <asp:TextBox ID="txtCompa" runat="server" class="input" Width="355px"></asp:TextBox>
                    <input id="btnCompa" style="width: 20px;" onclick="javascript:fnc_BuscarTrabajador_2();"
                        type="button" value="..." name="btnSolicitante" class="Boton" runat="server"/>
                    <asp:HiddenField ID="hdfCodTrab" runat="server" />
                </td>
            </tr>
            <tr>
                <td align="left" class="style1" colspan="2">
                    Tiempo de servicio en el puesto del compañero asignado(años):
                </td>
                <td align="left" colspan="5">
                    <asp:TextBox ID="txtTiempo" runat="server" class="input" Width="387px" MaxLength="2"></asp:TextBox>
                </td>
            </tr>
        </table>
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <table style="width: 772px">
                    <tr>
                        <td align="center" colspan="3" class="style2">
                            <h4>
                                EVALUACIÓN DE DOMINIO PRINCIPALES TAREAS NUEVO PUESTO (6 TAREAS PRINCIPALES)</h4>
                        </td>
                    </tr>
                    <tr>
                        <td align="left" colspan="3" class="Descripcion">
                            VALORACIÓN : 0&nbsp;&nbsp;&nbsp;&nbsp;1&nbsp;&nbsp;&nbsp;&nbsp;2&nbsp;&nbsp;&nbsp;&nbsp;3
                            &nbsp;&nbsp;&nbsp;&nbsp;4&nbsp;&nbsp;&nbsp;&nbsp;5&nbsp;&nbsp;&nbsp;&nbsp;6&nbsp;&nbsp;&nbsp;&nbsp;7
                            &nbsp;&nbsp;&nbsp;&nbsp;8&nbsp;&nbsp;&nbsp;&nbsp;9&nbsp;&nbsp;&nbsp;&nbsp;10 &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;0=SIN
                            DESTREZA &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;10=
                            DESTREZA ÓPTIMA
                        </td>
                    </tr>
                    <tr>
                        <td colspan="3">
                        <%--DataKeyNames="INT_COD_GENPAR"--%>
                            <asp:GridView ID="gvEval1" runat="server" AutoGenerateColumns="false" 
                                Width="765px">
                                <Columns>
                                <asp:TemplateField HeaderText="" Visible="false">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtIntOrden" runat="server" Width="660px" Height="32" Text='<%# Eval("INT_ORDEN") %>'
                                                CssClass="txtEnabled" Enabled="false"></asp:TextBox>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                                        <ItemStyle Width="590px"></ItemStyle>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="(6 TAREAS PRINCIPALES)" ItemStyle-Width="590px" HeaderStyle-HorizontalAlign="Left">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtCampo" runat="server" Width="660px" Height="32" Text='<%# Eval("VCH_COD_CAMPO") %>'
                                                CssClass="txtEnabled" Enabled="false"></asp:TextBox>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                                        <ItemStyle Width="590px"></ItemStyle>
                                    </asp:TemplateField>
                                    <%--<asp:TemplateField HeaderText="PUNTUACIÓN" ItemStyle-Width="80px" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtValor" runat="server" Width="80px" Height="32" Text='<%# Eval("VCH_VAL_CAMPO") %>'
                                                Style="text-align: center; font-size: 26px; font-weight: bold;" MaxLength="2"
                                                AutoPostBack="true" OnTextChanged="txtValor_TextChanged"></asp:TextBox>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" Width="80px"></ItemStyle>
                                    </asp:TemplateField>--%>
                                    <asp:TemplateField HeaderText="PUNTUACIÓN" ItemStyle-Width="80px" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:DropDownList ID="dwlPuntaje" runat="server" Width="40px">
                                                <asp:ListItem Value="1">1</asp:ListItem>
                                                <asp:ListItem Value="2">2</asp:ListItem>
                                                <asp:ListItem Value="3">3</asp:ListItem>
                                                <asp:ListItem Value="4">4</asp:ListItem>
                                                <asp:ListItem Value="5">5</asp:ListItem>
                                                <asp:ListItem Value="6">6</asp:ListItem>
                                                <asp:ListItem Value="7">7</asp:ListItem>
                                                <asp:ListItem Value="8">8</asp:ListItem>
                                                <asp:ListItem Value="9">9</asp:ListItem>
                                                <asp:ListItem Value="10">10</asp:ListItem>

                                            </asp:DropDownList>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" Width="80px"></ItemStyle>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 300px">
                            <h4 id="txtValorFooter1" runat="server">
                                PUNTAJE MÍNIMO REQUERIDO 45</h4>
                        </td>
                        <td style="width: 150px">
                            <h4>
                                TOTAL DE PUNTOS</h4>
                        </td>
                        <td align="center" class="style3">
                            <h1 id="txtValorTot1" runat="server">
                                0</h1>
                        </td>
                    </tr>
                </table>
                <table style="width: 771px">
                    <tr>
                        <td align="center" colspan="3" class="style2">
                            <h4>
                                APTITUDES GENERALES A EVALUAR</h4>
                        </td>
                    </tr>
                    <tr>
                        <td align="left" colspan="3" class="Descripcion">
                            VALORACIÓN : 0&nbsp;&nbsp;&nbsp;&nbsp;1&nbsp;&nbsp;&nbsp;&nbsp;2&nbsp;&nbsp;&nbsp;&nbsp;3
                            &nbsp;&nbsp;&nbsp;&nbsp;4&nbsp;&nbsp;&nbsp;&nbsp;5&nbsp;&nbsp;&nbsp;&nbsp;6&nbsp;&nbsp;&nbsp;&nbsp;7
                            &nbsp;&nbsp;&nbsp;&nbsp;8&nbsp;&nbsp;&nbsp;&nbsp;9&nbsp;&nbsp;&nbsp;&nbsp;10 &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;0=SIN
                            DESTREZA &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;10=
                            DESTREZA ÓPTIMA
                        </td>
                    </tr>
                    <tr>
                        <td colspan="3" style="width: 600px;">
                        <%--DataKeyNames="INT_COD_GENPAR"--%>
                            <asp:GridView ID="gvEval2" runat="server" AutoGenerateColumns="false" 
                                Width="764px">
                                <Columns>
                                
                                <asp:TemplateField HeaderText="" Visible="false">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtIntOrden" runat="server" Width="660px" Height="32" Text='<%# Eval("INT_ORDEN") %>'
                                                CssClass="txtEnabled" Enabled="false"></asp:TextBox>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                                        <ItemStyle Width="590px"></ItemStyle>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="" ItemStyle-Width="590px" HeaderStyle-HorizontalAlign="Left">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtCampo" runat="server" Width="660px" Height="32" Text='<%# Eval("VCH_COD_CAMPO") %>'
                                                CssClass="txtEnabled" Enabled="false"></asp:TextBox>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                                        <ItemStyle Width="590px"></ItemStyle>
                                    </asp:TemplateField>
                                    <%--<asp:TemplateField HeaderText="PUNTUACIÓN" ItemStyle-Width="80px" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtValor2" runat="server" Width="80px" Height="32" Text='<%# Eval("VCH_VAL_CAMPO") %>'
                                                Style="text-align: center; font-size: 26px; font-weight: bold;" MaxLength="2"
                                                AutoPostBack="true" OnTextChanged="txtValor2_TextChanged"></asp:TextBox>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" Width="80px"></ItemStyle>
                                    </asp:TemplateField>--%>
                                    <asp:TemplateField HeaderText="PUNTUACIÓN" ItemStyle-Width="80px" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:DropDownList ID="dwlPuntaje" runat="server" Width="40px">
                                                <asp:ListItem Value="1">1</asp:ListItem>
                                                <asp:ListItem Value="2">2</asp:ListItem>
                                                <asp:ListItem Value="3">3</asp:ListItem>
                                                <asp:ListItem Value="4">4</asp:ListItem>
                                                <asp:ListItem Value="5">5</asp:ListItem>
                                                <asp:ListItem Value="6">6</asp:ListItem>
                                                <asp:ListItem Value="7">7</asp:ListItem>
                                                <asp:ListItem Value="8">8</asp:ListItem>
                                                <asp:ListItem Value="9">9</asp:ListItem>
                                                <asp:ListItem Value="10">10</asp:ListItem>

                                            </asp:DropDownList>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" Width="80px"></ItemStyle>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 300px">
                            <h4 id="txtValorFooter2" runat="server">
                                PUNTAJE MÍNIMO REQUERIDO 45</h4>
                        </td>
                        <td style="width: 150px">
                            <h4>
                                TOTAL DE PUNTOS</h4>
                        </td>
                        <td style="width: 70px" align="center">
                            <h1 id="txtValorTot2" runat="server">
                                0</h1>
                        </td>
                    </tr>
                    <tr>
                        <td align="center" colspan="3" class="style2">
                            <h4>
                                OPINIÓN DEL RESPONSABLE DE LA EVALUACIÓN</h4>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="3">
                            <asp:TextBox ID="txtOpinion" runat="server" CssClass="txtAreaHabilitado" Height="87px"
                                Font-Size="13px" Width="761px" MaxLength="200" TextMode="MultiLine"></asp:TextBox>
                        </td>
                    </tr>
                </table>
            </ContentTemplate>
        </asp:UpdatePanel>
        <br />
        <%--<table>
            <tr>
                <td style="width: 345px" colspan="4">
                    <asp:TextBox ID="TextBox1" runat="server" Height="87px" Font-Size="9px" Width="376px"
                        MaxLength="200" TextMode="MultiLine" Enabled="false" CssClass="txtEnabled"></asp:TextBox>
                </td>
                <td style="width: 345px" colspan="3">
                    <asp:TextBox ID="TextBox2" runat="server" Height="87px" Font-Size="9px" Width="376px"
                        MaxLength="200" TextMode="MultiLine" Enabled="false" CssClass="txtEnabled"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td style="width: 345px; font-weight: bold; font-size: 13px;" colspan="4" align="center">
                    Firma del supervisor/Jefe directo
                </td>
                <td style="width: 345px; font-weight: bold; font-size: 13px;" colspan="3" align="center">
                    Firma del Jefe de Área
                </td>
            </tr>
        </table>--%>
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
        <table style="width: 774px">
            <tr>
                <td></td>
            </tr>
            <tr>
                <td colspan="7" align="right" class="style7">
                    <asp:Button ID="btnGuardar" runat="server" Text="Guardar" CssClass="Boton" 
                        Height="29px" Width="99px" />
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
