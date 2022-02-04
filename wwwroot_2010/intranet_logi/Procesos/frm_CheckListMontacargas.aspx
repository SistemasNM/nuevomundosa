<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="frm_CheckListMontacargas.aspx.vb" Inherits="intranet_logi.frm_CheckListMontacargas" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html>
<head>
    <title>Consulta de pedido</title>
    <base target="_self" />
    <meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1" />
    <meta name="CODE_LANGUAGE" content="Visual Basic .NET 7.1" />
    <meta name="vs_defaultClientScript" content="JavaScript" />
    <meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5" />
    <link rel="Stylesheet" type="text/css" href="../../intranet/Estilos/Styles_Paginas.css" />
    <link rel="Stylesheet" type="text/css" href="../../intranet/Estilos/Styles_Controles.css" />
    <link rel="stylesheet" type="text/css" href="../css/NM0001.css" />
    <link rel="Stylesheet" type="text/css" href="../../intranet/Estilos/EstilosWeb.css" />
    <script language="javascript" src="../../intranet/JS/jsCalendario_N4.js" type="text/javascript"></script>
    <script language="javascript" src="../../intranet/JS/functions.js" type="text/javascript"></script>
    <script language="javascript" type="text/javascript">
        function fnc_Cerrar() {
            window.open('', '_parent', '');
            window.close();
        }
        // Funcion Busca Empleados
        function fnc_BuscarTrabajadores() {
            var tipo = "EMP";
            var retorno = window.showModalDialog("../Buscadores/frmBuscadorOperario.aspx?strTipo=" + tipo, "", "dialogHeight:450px;dialogwidth:450px;center:yes;help:no;");
            if (retorno != "" && retorno != ":") {
                var datos = retorno.split(":");
                var codigo = datos[0];
                var nombre = datos[1];
                document.all('txtSolicitante').value = codigo;
                document.getElementById('lblSolicitante').innerHTML = nombre;
            }
        }
        // Funcion: Carga la ventana de Registro de Pedidos
        function VerRegistroPedido(NumPedido) {
            //window.location.href = "frm_RegistroPedido.aspx?strNumeroPedido="+NumPedido;
            //window.open("frm_RegistroPedido.aspx?&strNumeroPedido="+NumPedido,"","");
            returnValue = NumPedido;
            window.open('', '_parent', '');
            window.close();
        }
        function FormatearBusqDoc(pTexto) {
            if (pTexto == 1)//serie
            {
                var lserie = '000000' + document.all["txtSerie"].value;
                lserie = lserie.substring(lserie.length, lserie.length - 4);
                if (lserie == '0000') {
                    document.all["txtSerie"].value = '';
                } else {
                    document.all["txtSerie"].value = lserie;
                } //end if

            } //end if

            if (pTexto == 2)//numero
            {
                var lnume = '00000000000' + document.all["txtNumeroPedido"].value;
                lnume = lnume.substring(lnume.length, lnume.length - 10);
                if (lnume == '0000000000') {
                    document.all["txtNumeroPedido"].value = '';
                } else {
                    document.all["txtNumeroPedido"].value = lnume;
                } //end if

            } //end if
        } //end function
    </script>
    <style type="text/css">
        .style6
        {
            font-weight: bold;
            font-size: 9px;
            color: #333333;
            background-color: #BCCAE0;
            font-family: Verdana;
            text-decoration: none;
            width: 56px;
        }
        .style7
        {
            font-weight: bold;
            font-size: 9px;
            color: #333333;
            background-color: #BCCAE0;
            font-family: Verdana;
            text-decoration: none;
            width: 3px;
        }
        .style9
        {
            width: 105px;
        }
        .style10
        {
            width: 113px;
        }
        #tblCabeceraPagina
        {
            width: 563px;
        }
    </style>
</head>
<body>
    <form id="frm_ConsultaPedidos" method="post" runat="server">
    
        <!-- Titulo -->
        <table id="tblCabeceraPagina" border="0" cellspacing="1" cellpadding="1">
            <tr>
                <td style="width: 800px; height: 30px" class="Cabecera">
                    Check List de Montacargas
                </td>
            </tr>
        </table>
        <!-- Cabecera de Pagina -->
        <table style="width: 562px" border="0">
            <tr>
                <td class="style6" colspan="1">
                    Operador:
                </td>
                <td class="style9" colspan="1">
                    <asp:TextBox ID="txtSolicitante" runat="server" Width="50px" CssClass="input"></asp:TextBox>
                    <input style="width: 20px; height: 20px" id="Button1" class="boton" onclick="javascript:fnc_BuscarTrabajadores();"
                        value="..." type="button" name="btnSolicitante" />&nbsp;
                </td>
                <td colspan="4">
                    <asp:Label ID="lblSolicitante" runat="server" Font-Bold="True" Width="316px" Font-Size="9px"></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="style6" colspan="1">
                    Turno:
                </td>
                <td class="style9" colspan="1">
                    <asp:DropDownList ID="ddlTurno" runat="server" Width="120px" CssClass="input" AutoPostBack="false">
                        <asp:ListItem Value="0">TODOS</asp:ListItem>
                        <asp:ListItem Value="1">1er</asp:ListItem>
                        <asp:ListItem Value="2">2do</asp:ListItem>
                        <asp:ListItem Value="3">3er</asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td class="style7" colspan="1">
                    Fecha
                </td>
                <td colspan="1" class="style10">
                    <asp:TextBox ID="txtFechaIni" runat="server" Width="90px" CssClass="input"></asp:TextBox>
                    <!--<img onclick="popUpCalendar(this, frm_ConsultaPedidos.txtFechaIni, 'dd/mm/yyyy')" border="0" alt="Seleccionar fecha Inicial" src="../../intranet/Imagenes/Calendario.gif" width="16px" height="16px"  />-->
                </td>
                <td colspan="1">
                </td>
                <td colspan="1">
                </td>
            </tr>
            <tr>
                <td class="style6" colspan="1">
                    Maquina N°
                </td>
                <td class="style9" colspan="1">
                    <asp:DropDownList ID="ddlNumMaquina" runat="server" Width="120px" CssClass="input" AutoPostBack="false">
                        <asp:ListItem Value="0">TODOS</asp:ListItem>
                        <asp:ListItem Value="L8">006478 - L8</asp:ListItem>
                        <asp:ListItem Value="L10">006760 - L10</asp:ListItem>
                        <asp:ListItem Value="L11">006968 - L11</asp:ListItem>
                        <asp:ListItem Value="L12">006990 - L12</asp:ListItem>
                        <asp:ListItem Value="L13">007322 - L13</asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td class="style7" colspan="1">
                    Horometro
                </td>
                <td colspan="3">
                    <span style="font-weight: bold; font-size: 10px;visibility:hidden;">E</span>
                    <asp:TextBox ID="txtHorometroE" runat="server" Width="63px" CssClass="input"></asp:TextBox>
                    <span style="font-weight: bold; font-size: 10px;visibility:hidden;">&nbsp;&nbsp;S</span>
                    <asp:TextBox ID="txtHorometroS" runat="server" Width="65px" CssClass="input" Text="" Visible="false"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td colspan="6">
                    <asp:Label ID="lblMsgError" runat="server" Text="" Style="color: Red; font-size: 10px;"></asp:Label>
                </td>
            </tr>
            <tr>
                <td colspan="6">
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td colspan="4" class="style6" style="border-style: solid;border-color: Black;border-width:2px;text-align:center;">
                    TAREAS
                </td>
                <td colspan="1" class="style6" style="border-style: solid;border-color: Black;border-width:2px;text-align:center;">
                    NORMAL
                </td>
                <td colspan="1" class="style6" style="border-style: solid;border-color: Black;border-width:2px;text-align:center;">
                    CANTIDAD AÑADIDA
                </td>
            </tr>
            <tr>
                <td colspan="4" style="border-style: solid;border-color: Black;border-width:2px;">
                    1 NIVEL DE ACEITE DEL MOTOR</td>
                <td colspan="1" style="border-style: solid;border-color: Black;border-width:2px;text-align:center;">
                    <asp:DropDownList ID="ddlItem1" runat="server" Width="70px" CssClass="input" AutoPostBack="false">
                        <asp:ListItem Value="S">OK</asp:ListItem>
                        <asp:ListItem Value="N">X</asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td colspan="1" style="border-style: solid;border-color: Black;border-width:2px;">
                   <asp:DropDownList ID="ddlCantAnad1" runat="server" Width="70px" CssClass="input" AutoPostBack="false">
                        <asp:ListItem Value="N">--------</asp:ListItem>
                        <asp:ListItem Value="S">SI</asp:ListItem>
                    </asp:DropDownList>                    
                </td>
            </tr>
            <tr>
                <td colspan="4" style="border-style: solid;border-color: Black;border-width:2px;">
                    2 NIVEL DE AGUA EN EL RADIADOR</td>
                <td colspan="1" style="border-style: solid;border-color: Black;border-width:2px;text-align:center;">
                    <asp:DropDownList ID="ddlItem2" runat="server" Width="70px" CssClass="input" AutoPostBack="false">
                        <asp:ListItem Value="S">OK</asp:ListItem>
                        <asp:ListItem Value="N">X</asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td colspan="1" style="border-style: solid;border-color: Black;border-width:2px;">
                    <asp:DropDownList ID="ddlCantAnad2" runat="server" Width="70px" CssClass="input" AutoPostBack="false">
                        <asp:ListItem Value="N">--------</asp:ListItem>
                        <asp:ListItem Value="S">SI</asp:ListItem>
                    </asp:DropDownList> 
                </td>
            </tr>
            <tr>
                <td colspan="4" style="border-style: solid;border-color: Black;border-width:2px;">
                    3 NIVEL DE ACEITE HIDRAULICO</td>
                <td colspan="1" style="border-style: solid;border-color: Black;border-width:2px;text-align:center;">
                    <asp:DropDownList ID="ddlItem3" runat="server" Width="70px" CssClass="input" AutoPostBack="false">
                        <asp:ListItem Value="S">OK</asp:ListItem>
                        <asp:ListItem Value="N">X</asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td colspan="1" style="border-style: solid;border-color: Black;border-width:2px;">
                    <asp:DropDownList ID="ddlCantAnad3" runat="server" Width="70px" CssClass="input" AutoPostBack="false">
                        <asp:ListItem Value="N">--------</asp:ListItem>
                        <asp:ListItem Value="S">SI</asp:ListItem>
                    </asp:DropDownList> 
                </td>
            </tr>
            <tr>
                <td colspan="4" style="border-style: solid;border-color: Black;border-width:2px;">
                    4 NIVEL DE LIQUIDO DE FRENOS</td>
                <td colspan="1" style="border-style: solid;border-color: Black;border-width:2px;text-align:center;">
                    <asp:DropDownList ID="ddlItem4" runat="server" Width="70px" CssClass="input" AutoPostBack="false">
                        <asp:ListItem Value="S">OK</asp:ListItem>
                        <asp:ListItem Value="N">X</asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td colspan="1" style="border-style: solid;border-color: Black;border-width:2px;">
                    <asp:DropDownList ID="ddlCanAnad4" runat="server" Width="70px" CssClass="input" AutoPostBack="false">
                        <asp:ListItem Value="N">--------</asp:ListItem>
                        <asp:ListItem Value="S">SI</asp:ListItem>
                    </asp:DropDownList> 
                </td>
            </tr>
            <tr>
                <td colspan="4" style="border-style: solid;border-color: Black;border-width:2px;">
                    5 NIVEL DEL COMBUSTIBLE</td>
                <td colspan="1" style="border-style: solid;border-color: Black;border-width:2px;text-align:center;">
                    <asp:DropDownList ID="ddlItem5" runat="server" Width="70px" CssClass="input" AutoPostBack="false">
                        <asp:ListItem Value="S">OK</asp:ListItem>
                        <asp:ListItem Value="N">X</asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td colspan="1" style="border-style: solid;border-color: Black;border-width:2px;">
                    <asp:DropDownList ID="ddlCanAnad5" runat="server" Width="70px" CssClass="input" AutoPostBack="false">
                        <asp:ListItem Value="N">--------</asp:ListItem>
                        <asp:ListItem Value="S">SI</asp:ListItem>
                    </asp:DropDownList> 
                </td>
            </tr>
            <tr>
                <td colspan="4" style="border-style: solid;border-color: Black;border-width:2px;">
                    6 OBSERVAR EL FUNCIONAMIENTO DE INSTRUMENTOS EN EL TABLERO</td>
                <td colspan="1" style="border-style: solid;border-color: Black;border-width:2px;text-align:center;">
                    <asp:DropDownList ID="ddlItem6" runat="server" Width="70px" CssClass="input" AutoPostBack="false">
                        <asp:ListItem Value="S">OK</asp:ListItem>
                        <asp:ListItem Value="N">X</asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td colspan="1">
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td colspan="4" style="border-style: solid;border-color: Black;border-width:2px;">
                    7 OBSERVAR EL ESTADO DE LLANTAS</td>
                <td colspan="1" style="border-style: solid;border-color: Black;border-width:2px;text-align:center;">
                    <asp:DropDownList ID="ddlItem7" runat="server" Width="70px" CssClass="input" AutoPostBack="false">
                        <asp:ListItem Value="S">OK</asp:ListItem>
                        <asp:ListItem Value="N">X</asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td colspan="1" >
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td colspan="4" style="border-style: solid;border-color: Black;border-width:2px;">
                    8 OBSERVAR SI HAY FUGA DE ACEITE</td>
                <td colspan="1" style="border-style: solid;border-color: Black;border-width:2px;text-align:center;">
                    <asp:DropDownList ID="ddlItem8" runat="server" Width="70px" CssClass="input" AutoPostBack="false">
                        <asp:ListItem Value="S">OK</asp:ListItem>
                        <asp:ListItem Value="N">X</asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td colspan="1">
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td colspan="4" style="border-style: solid;border-color: Black;border-width:2px;">
                    9 OBSERVAR SI HAY FUGA DE AGUA</td>
                <td colspan="1" style="border-style: solid;border-color: Black;border-width:2px;text-align:center;">
                    <asp:DropDownList ID="ddlItem9" runat="server" Width="70px" CssClass="input" AutoPostBack="false">
                        <asp:ListItem Value="S">OK</asp:ListItem>
                        <asp:ListItem Value="N">X</asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td colspan="1">
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td colspan="4" style="border-style: solid;border-color: Black;border-width:2px;">
                    10 OBSERVAR EL ESTADO DE LUCES</td>
                <td colspan="1" style="border-style: solid;border-color: Black;border-width:2px;text-align:center;">
                    <asp:DropDownList ID="ddlItem10" runat="server" Width="70px" CssClass="input" AutoPostBack="false">
                        <asp:ListItem Value="S">OK</asp:ListItem>
                        <asp:ListItem Value="N">X</asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td colspan="1">
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td colspan="4">
                    &nbsp;
                </td>
                <td colspan="1">
                    &nbsp;
                </td>
                <td colspan="1">
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td colspan="5">
                    ANOTAR LO QUE ESTA MALOGRADO O POR MALOGRARSE
                </td>
                <td colspan="1">
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td colspan="4" style="border-style: solid;border-color: Black;border-width:2px;height: 25px;">
                    &nbsp;<asp:TextBox ID="txtObservacion_1" runat="server" Width="373px"></asp:TextBox></td>
                <td colspan="2" style="border-style: solid;border-color: Black;border-width:2px;font-size:9px;" rowspan="3">
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 
                    <br />
                    <br />
                    <br />
                    <br />                    
                    <br />
                    <br />
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    Firma&nbsp;</td>
            </tr>
            <tr>
                <td colspan="4" style="border-style: solid;border-color: Black;border-width:2px;height: 25px;">
                    &nbsp;<asp:TextBox ID="txtObservacion_2" runat="server" 
                        Width="372px"></asp:TextBox></td>
            </tr>
             <tr>
                <td colspan="4" style="border-style: solid;border-color: Black;border-width:2px;height: 25px;">
                    &nbsp;<asp:TextBox ID="txtObservacion_3" runat="server" 
                        Width="372px"></asp:TextBox></td>
            </tr>

            <tr>
                <td colspan="6">
                    &nbsp;
                </td>
            </tr>

            <tr>
                <td colspan="4">
                    &nbsp;
                </td>
                <td colspan="1">
                    <asp:Button ID="btnGrabar" runat="server" Text="Grabar" CssClass="Boton" 
                        Width="87px"/>
                </td>
                <td colspan="1">
                    <asp:Button ID="btnVolver" runat="server" Text="Volver" CssClass="Boton" 
                        Width="74px"/>
                </td>
            </tr>

        </table>
    </form>
</body>
</html>

