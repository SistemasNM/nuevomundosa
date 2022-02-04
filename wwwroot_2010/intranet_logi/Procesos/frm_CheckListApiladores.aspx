<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="frm_CheckListApiladores.aspx.vb" Inherits="intranet_logi.frm_CheckListApiladores" %>

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
                document.getElementById('txtSolicitante').value = codigo;
                //document.all('txtSolicitante').value = codigo;
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
                //var lserie = '000000' + document.all["txtSerie"].value;
                var lserie = '000000' + document.getElementById('txtSerie').value;
                lserie = lserie.substring(lserie.length, lserie.length - 4);

                if (lserie == '0000') {
                    //document.all["txtSerie"].value = '';                    
                    document.getElementById('txtSerie').value = '';
                } else {
                    //document.all["txtSerie"].value = lserie;
                    document.getElementById('txtSerie').value = lserie;
                } //end if

            } //end if

            if (pTexto == 2)//numero
            {
                //var lnume = '00000000000' + document.all["txtNumeroPedido"].value;
                var lnume = '00000000000' + document.getElementById('txtNumeroPedido').value;
                lnume = lnume.substring(lnume.length, lnume.length - 10);

                if (lnume == '0000000000') {
                    //document.all["txtNumeroPedido"].value = '';
                    document.getElementById('txtNumeroPedido').value = '';
                } else {
                    //document.all["txtNumeroPedido"].value = lnume;
                    document.getElementById('txtNumeroPedido').value = lnume;
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

        }
        .style7
        {
            font-weight: bold;
            font-size: 9px;
            color: #333333;
            background-color: #BCCAE0;
            font-family: Verdana;
            text-decoration: none;

        }


    </style>
</head>
<body>
    <form id="frm_ConsultaPedidos" method="post" runat="server">
    
        <!-- Titulo -->
        <table id="tblCabeceraPagina" border="0" cellspacing="1" cellpadding="1" style="width: 450px;">
            <tr>
                <td style="height: 30px; text-align: center;" class="Cabecera">Check List de Apiladores</td>
            </tr>
        </table>
        <!-- Cabecera de Pagina -->
        <table style="width: 450px" border="0">
            <tr>
                <td  style="width: 60px" class="style6" >Operador:</td>
                <td style="width: 120px" >
                    <asp:TextBox ID="txtSolicitante" runat="server" Width="50px" CssClass="input"></asp:TextBox>
                    <input style="width: 20px; height: 20px" id="Button1" class="boton" onclick="javascript:fnc_BuscarTrabajadores();"
                        value="..." type="button" name="btnSolicitante" />&nbsp;
                </td>
                <td style="width: 180px" colspan="2">
                    <asp:Label ID="lblSolicitante" runat="server" Font-Bold="True" Font-Size="9px"></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="style6" style="width: 60px">Turno:</td>
                <td style="width: 120px"> 
                    <asp:DropDownList ID="ddlTurno" runat="server" Width="120px" CssClass="input" AutoPostBack="false">
                        <asp:ListItem Value="0">TODOS</asp:ListItem>
                        <asp:ListItem Value="1">1er</asp:ListItem>
                        <asp:ListItem Value="2">2do</asp:ListItem>
                        <asp:ListItem Value="3">3er</asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td class="style6" style="width: 60px">Fecha:</td>
               <td style="width: 180px">&nbsp;&nbsp; 
                    <asp:TextBox ID="txtFechaIni" runat="server" Width="90px" CssClass="input"></asp:TextBox>
                    <!--<img onclick="popUpCalendar(this, frm_ConsultaPedidos.txtFechaIni, 'dd/mm/yyyy')" border="0" alt="Seleccionar fecha Inicial" src="../../intranet/Imagenes/Calendario.gif" width="16px" height="16px"  />-->
                </td>
            </tr>
            <tr>
                <td class="style6" style="width: 60px">Apilador:</td>
               <td style="width: 120px">
                    <asp:DropDownList ID="ddlNumMaquina" runat="server" Width="120px" CssClass="input" AutoPostBack="false">
                        <asp:ListItem Value="0">TODOS</asp:ListItem>
                        <asp:ListItem Value="A1">A1</asp:ListItem>
                        <asp:ListItem Value="A2">A2</asp:ListItem>
                        <asp:ListItem Value="A3">A3</asp:ListItem>                        
                        <asp:ListItem Value="Transpaleta">Transpaleta</asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td class="style6" style="width: 60px">Horometro:</td>
                <td style="width: 180px"> 
                    <span style="font-weight: bold; font-size: 10px;visibility:hidden;">E</span>
                    <asp:TextBox ID="txtHorometroE" runat="server" Width="63px" CssClass="input"></asp:TextBox>
                    <span style="font-weight: bold; font-size: 10px;visibility:hidden;">&nbsp;&nbsp;S</span>
                    <asp:TextBox ID="txtHorometroS" runat="server" Width="65px" CssClass="input" Text="" Visible="false"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td colspan="4">
                    <asp:Label ID="lblMsgError" runat="server" Text="" Style="color: Red; font-size: 10px;"></asp:Label>
                </td>
            </tr>
        </table>
        <table style="width: 450px" border="0">
            <tr>
                <td colspan="4" class="style6" style="border-style: solid;border-color: Black;border-width:2px;text-align:center;">TAREAS</td>
                <td colspan="1" class="style6" style="border-style: solid;border-color: Black;border-width:2px;text-align:center;">NORMAL</td>
            </tr>
            <tr>
                <td style="border-style: solid;border-color: Black;border-width:2px; text-align: center; width: 15px;">1</td>
                <td colspan="3" style="border-style: solid;border-color: Black;border-width:2px;">CHASIS-ESTA GOLPEADO</td>
                <td colspan="1" style="border-style: solid;border-color: Black;border-width:2px;text-align:center;">
                    <asp:DropDownList ID="ddlItem1" runat="server" Width="70px" CssClass="input" AutoPostBack="false">
                        <asp:ListItem Value="S">OK</asp:ListItem>
                        <asp:ListItem Value="N">X</asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td style="border-style: solid;border-color: Black;border-width:2px; text-align: center;">2</td>
                <td colspan="3" style="border-style: solid;border-color: Black;border-width:2px;">CARRO PORTA HORQUILLAS - ESTA DAÑADO</td>
                <td colspan="1" style="border-style: solid;border-color: Black;border-width:2px;text-align:center;">
                    <asp:DropDownList ID="ddlItem2" runat="server" Width="70px" CssClass="input" AutoPostBack="false">
                        <asp:ListItem Value="S">OK</asp:ListItem>
                        <asp:ListItem Value="N">X</asp:ListItem>
                    </asp:DropDownList>
                </td>

            </tr>
            <tr>
                <td style="border-style: solid;border-color: Black;border-width:2px; text-align: center;">3</td>
                <td colspan="3" style="border-style: solid;border-color: Black;border-width:2px;">MASTIL - ESTA DAÑADO</td>
                <td colspan="1" style="border-style: solid;border-color: Black;border-width:2px;text-align:center;">
                    <asp:DropDownList ID="ddlItem3" runat="server" Width="70px" CssClass="input" AutoPostBack="false">
                        <asp:ListItem Value="S">OK</asp:ListItem>
                        <asp:ListItem Value="N">X</asp:ListItem>
                    </asp:DropDownList>
                </td>

            </tr>
            <tr>
                <td style="border-style: solid;border-color: Black;border-width:2px; text-align: center;">4</td>
                <td colspan="3" style="border-style: solid;border-color: Black;border-width:2px;">RUEDAS - ESTADO</td>
                <td colspan="1" style="border-style: solid;border-color: Black;border-width:2px;text-align:center;">
                    <asp:DropDownList ID="ddlItem4" runat="server" Width="70px" CssClass="input" AutoPostBack="false">
                        <asp:ListItem Value="S">OK</asp:ListItem>
                        <asp:ListItem Value="N">X</asp:ListItem>
                    </asp:DropDownList>
                </td>

            </tr>
            <tr>
                <td style="border-style: solid;border-color: Black;border-width:2px; text-align: center;">5</td>
                <td colspan="3" style="border-style: solid;border-color: Black;border-width:2px;">COMPRUEBE DERRAMES DE ACEITE EN PISO</td>
                <td colspan="1" style="border-style: solid;border-color: Black;border-width:2px;text-align:center;">
                    <asp:DropDownList ID="ddlItem5" runat="server" Width="70px" CssClass="input" AutoPostBack="false">
                        <asp:ListItem Value="S">OK</asp:ListItem>
                        <asp:ListItem Value="N">X</asp:ListItem>
                    </asp:DropDownList>
                </td>

            </tr>
            <tr>
               <td style="border-style: solid;border-color: Black;border-width:2px; text-align: center;">6</td>
                <td colspan="3" style="border-style: solid;border-color: Black;border-width:2px;">INTERRUPTO PARADA DE EMERGENCIA</td>
                <td colspan="1" style="border-style: solid;border-color: Black;border-width:2px;text-align:center;">
                    <asp:DropDownList ID="ddlItem6" runat="server" Width="70px" CssClass="input" AutoPostBack="false">
                        <asp:ListItem Value="S">OK</asp:ListItem>
                        <asp:ListItem Value="N">X</asp:ListItem>
                    </asp:DropDownList>
                </td>

            </tr>
            <tr>
                <td style="border-style: solid;border-color: Black;border-width:2px; text-align: center;">7</td>
                <td colspan="3" style="border-style: solid;border-color: Black;border-width:2px;">BOCINA</td>
                <td colspan="1" style="border-style: solid;border-color: Black;border-width:2px;text-align:center;">
                    <asp:DropDownList ID="ddlItem7" runat="server" Width="70px" CssClass="input" AutoPostBack="false">
                        <asp:ListItem Value="S">OK</asp:ListItem>
                        <asp:ListItem Value="N">X</asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td style="border-style: solid;border-color: Black;border-width:2px; text-align: center;">8</td>
                <td colspan="3" style="border-style: solid;border-color: Black;border-width:2px;">MANDO DE FUNCIONAMIENTO - ATRÁS / ADELANTE</td>
                <td colspan="1" style="border-style: solid;border-color: Black;border-width:2px;text-align:center;">
                    <asp:DropDownList ID="ddlItem8" runat="server" Width="70px" CssClass="input" AutoPostBack="false">
                        <asp:ListItem Value="S">OK</asp:ListItem>
                        <asp:ListItem Value="N">X</asp:ListItem>
                    </asp:DropDownList>
                </td>

            </tr>
            <tr>
                <td style="border-style: solid;border-color: Black;border-width:2px; text-align: center;">9</td>
                <td colspan="3" style="border-style: solid;border-color: Black;border-width:2px;">FRENO DE DESPLAZAMIENTO - TIMON HACIA ABAJO</td>
                <td colspan="1" style="border-style: solid;border-color: Black;border-width:2px;text-align:center;">
                    <asp:DropDownList ID="ddlItem9" runat="server" Width="70px" CssClass="input" AutoPostBack="false">
                        <asp:ListItem Value="S">OK</asp:ListItem>
                        <asp:ListItem Value="N">X</asp:ListItem>
                    </asp:DropDownList>
                </td>

            </tr>
            <tr>
                <td style="border-style: solid;border-color: Black;border-width:2px; text-align: center;">10</td>
                <td colspan="3" style="border-style: solid;border-color: Black;border-width:2px;">FRENO DE ESTACIONAMIENTO - TIMON HACIA ARRIBA LENTAMENTE (EN MOV.)</td>
                <td colspan="1" style="border-style: solid;border-color: Black;border-width:2px;text-align:center;">
                    <asp:DropDownList ID="ddlItem10" runat="server" Width="70px" CssClass="input" AutoPostBack="false">
                        <asp:ListItem Value="S">OK</asp:ListItem>
                        <asp:ListItem Value="N">X</asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td style="border-style: solid;border-color: Black;border-width:2px; text-align: center;">11</td>
                <td colspan="3" style="border-style: solid;border-color: Black;border-width:2px;">MANDO DE ELEVACION Y DESCENSO</td>
                <td colspan="1" style="border-style: solid;border-color: Black;border-width:2px;text-align:center;">
                    <asp:DropDownList ID="ddlItem11" runat="server" Width="70px" CssClass="input" AutoPostBack="false">
                        <asp:ListItem Value="S">OK</asp:ListItem>
                        <asp:ListItem Value="N">X</asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td style="border-style: solid;border-color: Black;border-width:2px; text-align: center;">12</td>
                <td colspan="3" style="border-style: solid;border-color: Black;border-width:2px;">DIRECCION - FUNCIONAMIENTO</td>
                <td colspan="1" style="border-style: solid;border-color: Black;border-width:2px;text-align:center;">
                    <asp:DropDownList ID="ddlItem12" runat="server" Width="70px" CssClass="input" AutoPostBack="false">
                        <asp:ListItem Value="S">OK</asp:ListItem>
                        <asp:ListItem Value="N">X</asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td style="border-style: solid;border-color: Black;border-width:2px; text-align: center;">13</td>
                <td colspan="3" style="border-style: solid;border-color: Black;border-width:2px;">INTERRUPTOR INVERSION DE SEG. - CAMBIA DIRECCION AL PRESIONARLO</td>
                <td colspan="1" style="border-style: solid;border-color: Black;border-width:2px;text-align:center;">
                    <asp:DropDownList ID="ddlItem13" runat="server" Width="70px" CssClass="input" AutoPostBack="false">
                        <asp:ListItem Value="S">OK</asp:ListItem>
                        <asp:ListItem Value="N">X</asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>

            <tr>
                <td colspan="4">&nbsp;</td>
                <td colspan="1">&nbsp;</td>
            </tr>
            <tr>
                <td colspan="6">
                    ANOTAR LO QUE ESTA MALOGRADO O POR MALOGRARSE
                </td>

            </tr>
        </table>
        <table style="width: 450px" border="0">
            <tr>
                <td colspan="4" style="border-style: solid;border-color: Black;border-width:2px;height: 25px;">
                    &nbsp;<asp:TextBox ID="txtObservacion_1" runat="server" Width="325px"></asp:TextBox></td>
                <td colspan="2" style="border-style: solid;border-color: Black;border-width:2px;font-size:9px; width: 100px; vertical-align: bottom; text-align: center;" 
                    rowspan="3">Firma</td>
            </tr>
            <tr>
                <td colspan="4" style="border-style: solid;border-color: Black;border-width:2px;height: 25px;">
                    &nbsp;<asp:TextBox ID="txtObservacion_2" runat="server" Width="325px"></asp:TextBox></td>
            </tr>
             <tr>
                <td colspan="4" style="border-style: solid;border-color: Black;border-width:2px;height: 25px;">
                    &nbsp;<asp:TextBox ID="txtObservacion_3" runat="server" Width="325px"></asp:TextBox></td>
            </tr>
        </table>
        <table style="width: 450px" border="0">
            <tr>
                <td colspan="3">
                    &nbsp;
                </td>
            </tr>

            <tr>
                <td style="width: 50px">&nbsp;</td>
                <td>
                    <asp:Button ID="btnGrabar" runat="server" Text="Grabar" CssClass="Boton" Width="87px"/>
                </td>
                <td>
                    <asp:Button ID="btnVolver" runat="server" Text="Volver" CssClass="Boton" Width="74px"/>
                </td>
            </tr>

        </table>
    </form>
</body>
</html>