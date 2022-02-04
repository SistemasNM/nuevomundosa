<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="frm_Solicitud_PreCostos.aspx.vb"
    Inherits="ElnaceNM_PreCostos.frm_Solicitud_PreCostos" %>

<%@ Register Assembly="Infragistics35.Web.v10.1, Version=10.1.20101.1011, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb"
    Namespace="Infragistics.Web.UI.EditorControls" TagPrefix="ig" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Requisicion de Pre-Costos</title>

    <script language="JavaScript" src="../js/jsGeneral.js" type="text/jscript"></script>

    <script language="JavaScript" src="../js/jsDesTabla.js" type="text/jscript"></script>

    <script language="JavaScript" src="../js/tabpane.js" type="text/jscript"></script>

    <link href="../style/tab.webfx.css" type="text/css" rel="Stylesheet" />
    <link href="../style/Styles_Paginas.css" type="text/css" rel="Stylesheet" />
    <link href="../style/Styles_Controles.css" type="text/css" rel="Stylesheet" />

    <script language="JavaScript" type="text/javascript">
        function fbtnBusquedaOP() {
            //se va a utilizar lo del tinto
            //lstrCodigoMaquina = Request("CodigoMaquina")
            //lstrNombreMaquina = Request("NombreMaquina")
            var lstrCodigoMaquina = document.getElementById("txtCodMaquinaO").value;
            var lstrNombreMaquina = document.getElementById("txtDescUnidadO").value;

            var sLink = "http://servnmprb/EnlaceNM_Tinto/busqueda/frm_QOperacionXMaquina.aspx?CodigoMaquina=" + lstrCodigoMaquina + "&NombreMaquina=" + lstrNombreMaquina.toUpperCase();
            var sFeatures = "DialogWidth:500px;dialogHeight:500px;scroll:yes;status:no;";
            var aTabla = window.showModalDialog(sLink, "Tablas", sFeatures);
            var arrDatos = new Array();

            //alert(aTabla.len)

            if (aTabla != null) {
                arrDatos = aTabla.split(':');
                if (arrDatos[0].toString() != '') {
                    document.getElementById("txtCodOperacionO").value = arrDatos[0].toString();
                    document.getElementById("txtDescOperacionO").value = arrDatos[1].toString();
                    return true;
                } //end if
            } //end if
            return false;

        } //end function

        //===============================================================================//

        function fAnular(Obj) {
            ObjReg = document.getElementById("txtRegSel");

            if (ObjReg.value == "") {
                alert("Por favor Seleccionar un registro...!")
                return false;
            }

            var sw = confirm("¿Esta seguro de anular el registro seleccionado?");

            return sw
        }

        function abrirPopUp() {
            var retorno;
            retorno = window.showModalDialog('../Procesos/popUpBucarRequeTela.aspx?Mca_estado=ANLS', 'Buscar Requerimiento', 'dialogheight:300px;dialogwidth:560px;center:yes;help:no;');
            if (retorno != "" && retorno != ":") {
                var datos = retorno.split(":");
                var codigo = datos[0];
                var referencia = datos[1];
                frmData.txtCodReq.value = codigo;
                //lblNombreMaquina.innerHTML = nombre;
                //Form1.hdnNombreMaquina.value = nombre;
                //frmProcesarAnalisisTela.submit();
            }
        }
        
    </script>

    <style type="text/css">
        .style1
        {
            font-weight: bold;
            font-size: 11px;
            font-family: Arial,Verdana;
            width: 122px;
        }
        .style2
        {
            width: 278px;
        }
    </style>
</head>
<body>
    <br />
    <center>
        <form id="frmData" runat="server" method="post">
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        <br />
        <!-- ***********************************************  Inicio de Cabecera ************************************************ -->
        <table class="CabMain" border="0" cellpadding="0" cellspacing="0" style="width: 820px;
            height: 30px">
            <tr>
                <td style="width: 100%" align="right" valign="bottom">
                    &nbsp;&nbsp;Módulo de Pre-Costos
                </td>
            </tr>
        </table>
        <!-- ***********************************************  Final de Cabecera ************************************************ -->
        <!-- ***********************************************  Inicio de Detalle ************************************************ -->
        <br />
        <!-- para redimencionar cambiar el ancho  -->        
        <table border="0" cellpadding="0" cellspacing="0" style="width: 800px">
            <tr>
                <td valign="top" style="width: 100%">
                    <!-- **************  Inicio de Contenido ****************** -->
                    <!-- para redimencionar cambiar el ancho  -->
                    <div class="tab-pane" id="tabpane1">
                        <div class="tab-page" id="tabpage10" runat="server">
                            <h2 class="tab">
                                &nbsp;&nbsp;Solicitud de Pre-Costos</h2>
                            <asp:UpdatePanel ID="panContenido" runat="server">
                                <ContentTemplate>
                                    <table style="width: 800px" border="0" cellpadding="0" cellspacing="0" id="tabContenido">
                                        <tr>
                                            <td valign="top" style="width: 800px">
                                                <!-- **************  Inicio de Listado ****************** -->
                                                <asp:Panel ID="panListado" runat="server">
                                                    <table id="tblFiltro" style="height: 45px; width: 800px" border="0" cellpadding="0"
                                                        cellspacing="0" class="FrameSimple">
                                                        <tr>
                                                            <td align="left" class="lbl">
                                                                Nro Requisición
                                                            </td>
                                                            <td align="left" class="lbl">
                                                                <asp:TextBox ID="TxtNum_Requisicion" runat="server" Width="111px" MaxLength="20"></asp:TextBox>
                                                            </td>
                                                            <td align="left" class="lbl">
                                                                &nbsp;
                                                            </td>
                                                            <td align="right" class="lbl">
                                                                <asp:ImageButton ID="btnBuscar" runat="server" Height="30px" ImageUrl="~/Images/Buscar.png"
                                                                    ToolTip="Buscar" Width="30px" />
                                                                <asp:ImageButton ID="btnNuevo" runat="server" Height="40px" ImageUrl="~/Images/Nuevo.png"
                                                                    ToolTip="Nuevo" Width="40px" />&nbsp;<asp:ImageButton ID="btnEditar" runat="server"
                                                                        Height="40px" ImageUrl="~/Images/Editar.png" ToolTip="Modificar" Width="40px" />
                                                                &nbsp;
                                                                <asp:ImageButton ID="btnAnular" runat="server" Height="40px" ImageUrl="~/Images/Delete.png"
                                                                    ToolTip="Anular" Width="40px" />
                                                                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td class="lbl" align="left">
                                                                Fecha Inicial
                                                            </td>
                                                            <td class="lbl" align="left">
                                                                &nbsp;<ig:WebDatePicker ID="TxtFechaIni" runat="server" Width="110px">
                                                                </ig:WebDatePicker>
                                                            </td>
                                                            <td class="lbl" align="left">
                                                                Fecha Final
                                                            </td>
                                                            <td align="left" class="lbl">
                                                                &nbsp;<ig:WebDatePicker ID="TxtFechaFin" runat="server" Width="110px">
                                                                </ig:WebDatePicker>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                    <table id="tblReg" border="0" cellpadding="0" cellspacing="0" style="width: 800px;
                                                        height: 25px">
                                                        <tr>
                                                            <td style="width: 100%" align="right" class="lbl">
                                                                <asp:TextBox ID="txtOpc" runat="server" CssClass="txtHid" Width="30px"></asp:TextBox>
                                                                <asp:TextBox ID="txtRegSel" runat="server" CssClass="txtHid" Width="30px"></asp:TextBox>
                                                                Nro. Registros&nbsp;
                                                                <asp:Label ID="lblReg" runat="server" Text="" CssClass="lbl"></asp:Label>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                    <!-- Configurando la grilla  -->
                                                    <table id="tblHead" border="0" cellpadding="0" cellspacing="0" style="width: 800px;
                                                        height: 25px" class="Grilla">
                                                        <tr>
                                                            <td style="width: 100%">
                                                                <table border="0" cellpadding="0" cellspacing="0" style="width: 800px; height: 25px"
                                                                    class="GridHeader">
                                                                    <tr>
                                                                        <td style="width: 080px">
                                                                            Nro Requi.
                                                                        </td>
                                                                        <td style="width: 080px">
                                                                            Fecha
                                                                        </td>
                                                                        <td style="width: 220px">
                                                                            Observaciones
                                                                        </td>
                                                                        <td style="width: 070px">
                                                                            Estado
                                                                        </td>
                                                                        <td style="width: 120px">
                                                                            Usuario Creacion
                                                                        </td>
                                                                        <td style="width: 100px">
                                                                            ver Detalle
                                                                        </td>
                                                                        <td>
                                                                            Sol Aprob.
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="left" style="width: 100%">
                                                                <asp:DataGrid ID="grdData" runat="server" AutoGenerateColumns="False" ShowHeader="False"
                                                                    Width="800px">
                                                                    <AlternatingItemStyle CssClass="FilaAlt" />
                                                                    <Columns>
                                                                        <asp:BoundColumn DataField="var_NumeroRequisicion" ReadOnly="True" Visible="true">
                                                                            <ItemStyle HorizontalAlign="Left" Width="62px" />
                                                                        </asp:BoundColumn>
                                                                        <asp:BoundColumn DataField="dtm_Fecha" ReadOnly="True">
                                                                            <ItemStyle HorizontalAlign="Left" Width="62px" />
                                                                        </asp:BoundColumn>
                                                                        <asp:BoundColumn DataField="var_Observaciones" ReadOnly="True">
                                                                            <ItemStyle HorizontalAlign="Left" Width="180px" />
                                                                        </asp:BoundColumn>
                                                                        <asp:BoundColumn DataField="chr_Estado" ReadOnly="True">
                                                                            <ItemStyle HorizontalAlign="Left" Width="67px" />
                                                                        </asp:BoundColumn>
                                                                        <asp:BoundColumn DataField="var_UsuarioCreacion" ReadOnly="True">
                                                                            <ItemStyle HorizontalAlign="Left" Width="117px" />
                                                                        </asp:BoundColumn>
                                                                        <asp:TemplateColumn HeaderText="Detalle">
                                                                            <ItemStyle Width="60px" />
                                                                            <ItemTemplate>
                                                                                <asp:Button ID="btnDetalle" runat="server" CommandName="DETALLE" Height="20px" Text="..."
                                                                                    Width="20px" />
                                                                            </ItemTemplate>
                                                                        </asp:TemplateColumn>
                                                                        <asp:TemplateColumn HeaderText="Aprobacion" HeaderStyle-CssClass="GridHeader">
                                                                            <ItemTemplate>
                                                                                <asp:Button ID="btnEditar" runat="server" Text="..." Height="20px" CommandName="ESTADO" />
                                                                            </ItemTemplate>
                                                                            <HeaderStyle CssClass="GridHeader" />
                                                                            <ItemStyle Width="60px" Font-Bold="False" Font-Italic="False" Font-Overline="False"
                                                                                Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" />
                                                                        </asp:TemplateColumn>
                                                                    </Columns>
                                                                </asp:DataGrid>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </asp:Panel>
                                                <!-- **************  Fin de Listado ****************** -->
                                                <!-- *********** Inicio de Actualización ************** -->
                                                <asp:Panel ID="panActualiza" runat="server" Visible="false">
                                                    <table id="tblDatos" border="0" cellpadding="0" cellspacing="0" style="width: 100%;
                                                        height: 90px">
                                                        <tr>
                                                            <td valign="middle">
                                                                <br />
                                                                <table id="Table2" style="height: 45px; width: 800px" border="0" cellpadding="0"
                                                                    cellspacing="0" class="FrameSimple">
                                                                    <tr>
                                                                        <td align="left" class="lbl">
                                                                            Nro Requisición
                                                                        </td>
                                                                        <td align="left" class="lbl">
                                                                            <asp:TextBox ID="TxtNumerorequisicion" runat="server" Width="111px" MaxLength="20"></asp:TextBox>
                                                                        </td>
                                                                        <td align="left" class="lbl">
                                                                            Req. Anls.
                                                                        </td>
                                                                        <td align="left" class="lbl">
                                                                            <asp:TextBox ID="txtCodReq" runat="server" Width="143px" MaxLength="16"></asp:TextBox>
                                                                            <asp:ImageButton ID="imgBuscar" runat="server" ImageUrl="~/images/Buscar.png" Height="20" Width="20" OnClientClick="abrirPopUp();"/>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td class="lbl" align="left">
                                                                            Fecha
                                                                        </td>
                                                                        <td class="lbl" align="left">
                                                                            <ig:WebDatePicker ID="TxtFecha" runat="server" Width="110px">
                                                                            </ig:WebDatePicker>
                                                                        </td>
                                                                        <td class="lbl" align="left">
                                                                            Estado
                                                                        </td>
                                                                        <td align="left" class="lbl">
                                                                            <asp:DropDownList ID="ddlEstado" runat="server" CssClass="lbl" Height="20px" Width="139px"
                                                                                AutoPostBack="True">
                                                                                <asp:ListItem Value="ACT">ACT - Activo</asp:ListItem>
                                                                                <asp:ListItem Value="ANU">ANU - Anulado</asp:ListItem>
                                                                                <asp:ListItem Value="SOL">SOL - Solicitado</asp:ListItem>
                                                                                <asp:ListItem Value="CON">CON - Datos Completos</asp:ListItem>
                                                                            </asp:DropDownList>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td class="lbl" align="left">
                                                                            Observaciones
                                                                        </td>
                                                                        <td align="left" class="lbl" colspan="3">
                                                                            <asp:TextBox ID="TxtObservaciones" runat="server" Height="66px" Width="481px"></asp:TextBox>
                                                                        </td>
                                                                        <td class="lbl" align="left">
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                                <table id="tblGrabar" style="width: 90%; height: 40px" border="0" cellpadding="0"
                                                                    cellspacing="0">
                                                                    <tr>
                                                                        <td style="width: 100%" align="center">
                                                                            <asp:ImageButton ID="btnGraba" runat="server" Height="25px" ImageUrl="~/Images/Grabar.png"
                                                                                ToolTip="Grabar" Width="25px" />
                                                                            &nbsp; &nbsp; &nbsp;&nbsp;
                                                                            <asp:ImageButton ID="btnActiva" runat="server" Height="25px" ImageUrl="~/Images/Activar.png"
                                                                                ToolTip="Activar" Width="25px" />
                                                                            &nbsp;
                                                                            <asp:ImageButton ID="btnCancelar" runat="server" ImageUrl="~/Images/Cancelar.png"
                                                                                ToolTip="Regresar / Cancelar" Height="25px" Width="25px" />
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </asp:Panel>
                                                <!-- *********** Fin de Actualización ************** -->
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <table id="tblMsg" style="width: 100%; height: 20px" border="0" cellpadding="0" cellspacing="0">
                                                    <tr>
                                                        <td align="center">
                                                            <asp:Label ID="lblMsg" runat="server"></asp:Label>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                    </table>
                                </ContentTemplate>
                                <Triggers>
                                    <asp:AsyncPostBackTrigger ControlID="btnNuevo" EventName="click" />
                                    <asp:AsyncPostBackTrigger ControlID="btnEditar" EventName="click" />
                                </Triggers>
                            </asp:UpdatePanel>
                        </div>
                        <div class="tab-page" id="tabpage21" runat="server">
                            <h2 class="tab">
                                &nbsp;&nbsp;Detalle de Pre-costos&nbsp;&nbsp;</h2>
                            <asp:UpdatePanel ID="UpdatePanelO" runat="server">
                                <ContentTemplate>
                                    <table style="width: 800px" border="0" cellpadding="0" cellspacing="0" id="tabContenidoO">
                                        <tr>
                                            <td valign="top" style="width: 800px">
                                                <!-- **************  Inicio de Listado ****************** -->
                                                <asp:Panel ID="panListadoO" runat="server">
                                                    <table id="tblFiltroO" style="height: 45px; width: 800px" border="0" cellpadding="0"
                                                        cellspacing="0" class="FrameSimple">
                                                        <tr>
                                                            <td align="left" class="lbl">
                                                                Nro Requisicion
                                                            </td>
                                                            <td align="left" class="lbl">
                                                                <asp:TextBox ID="TxtRequisicion_Detalle" runat="server" Width="111px" ReadOnly="True"
                                                                    MaxLength="10"></asp:TextBox>
                                                            </td>
                                                            <td align="left" class="lbl">
                                                                Fecha
                                                            </td>
                                                            <td align="Left" class="lbl">
                                                                <asp:TextBox ID="TxtFecha_Detalle" runat="server" Width="111px" ReadOnly="True" MaxLength="10"></asp:TextBox>
                                                                <asp:ImageButton ID="btnNuevoO" runat="server" Height="40px" ImageUrl="~/Images/Nuevo.png"
                                                                    ToolTip="Nuevo" Width="40px" />&nbsp;<asp:ImageButton ID="btnEditarO" runat="server"
                                                                        Height="40px" ImageUrl="~/Images/Editar.png" ToolTip="Modificar" Width="40px" />
                                                                &nbsp;
                                                                <asp:ImageButton ID="btnAnularO" runat="server" Height="40px" ImageUrl="~/Images/Delete.png"
                                                                    ToolTip="Anular" Width="40px" />
                                                                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td class="lbl" align="left">
                                                                Observaciones
                                                            </td>
                                                            <td align="left" class="lbl" colspan="3">
                                                                <asp:TextBox ID="TxtObservaciones_Detalle" runat="server" Height="56px" Width="528px"
                                                                    ReadOnly="True"></asp:TextBox>
                                                            </td>
                                                            <td class="lbl" align="left">
                                                            </td>
                                                        </tr>
                                                    </table>
                                                    <table id="tblRegO" border="0" cellpadding="0" cellspacing="0" style="width: 100%;
                                                        height: 25px">
                                                        <tr>
                                                            <td style="width: 100%" align="right" class="lbl">
                                                                <asp:TextBox ID="txtOpcO" runat="server" CssClass="txtHid" Width="30px"></asp:TextBox>
                                                                <asp:TextBox ID="txtRegSelO" runat="server" CssClass="txtHid" Width="30px"></asp:TextBox>
                                                                Nro. Registros&nbsp;<asp:Label ID="lblRegO" runat="server" CssClass="lbl" Text=""></asp:Label>
                                                                &nbsp;
                                                            </td>
                                                        </tr>
                                                    </table>
                                                    <!-- Configurando la grilla  -->
                                                    <table id="tblHeadO" border="0" cellpadding="0" cellspacing="0" style="width: 800px;
                                                        height: 25px" class="Grilla">
                                                        <tr>
                                                            <td style="width: 100%">
                                                                <table border="0" cellpadding="0" cellspacing="0" style="width: 800px; height: 25px"
                                                                    class="GridHeader">
                                                                    <tr class="RowHead">
                                                                        <td style="width: 90px" class="ColHead">
                                                                            Articulo Crudo
                                                                        </td>
                                                                        <td style="width: 90px" class="ColHead">
                                                                            Articulo Base
                                                                        </td>
                                                                        <td style="width: 130px" class="ColHead">
                                                                            Acabados
                                                                        </td>
                                                                        <td style="width: 90px" class="ColHead">
                                                                            Color
                                                                        </td>
                                                                        <td style="width: 40px" class="ColHead">
                                                                            Colorante
                                                                        </td>
                                                                        <td style="width: 390px" class="ColHead">
                                                                            Observaciones
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="left" style="width: 100%">
                                                                <asp:DataGrid ID="grdDataO" runat="server" AutoGenerateColumns="False" ShowHeader="False"
                                                                    Width="800px">
                                                                    <AlternatingItemStyle CssClass="FilaAlt" />
                                                                    <Columns>
                                                                        <asp:BoundColumn DataField="var_ArticuloCrudo" ReadOnly="True" Visible="true">
                                                                            <ItemStyle Height="23px" HorizontalAlign="Left" Width="90px" />
                                                                        </asp:BoundColumn>
                                                                        <asp:BoundColumn DataField="var_ArticuloBase" ReadOnly="True" Visible="true">
                                                                            <ItemStyle Height="23px" HorizontalAlign="Left" Width="90px" />
                                                                        </asp:BoundColumn>
                                                                        <asp:BoundColumn DataField="var_Acabado" ReadOnly="True" Visible="true">
                                                                            <ItemStyle Height="23px" HorizontalAlign="Left" Width="130px" />
                                                                        </asp:BoundColumn>
                                                                        <asp:BoundColumn DataField="var_Color" ReadOnly="True">
                                                                            <ItemStyle Height="23px" HorizontalAlign="Left" Width="90px" />
                                                                        </asp:BoundColumn>
                                                                        <asp:BoundColumn DataField="var_Colorante">
                                                                            <ItemStyle Height="23px" HorizontalAlign="Left" Width="40px" />
                                                                        </asp:BoundColumn>
                                                                        <asp:BoundColumn DataField="var_Observaciones" ReadOnly="True">
                                                                            <ItemStyle Height="23px" HorizontalAlign="Left" Width="390px" />
                                                                        </asp:BoundColumn>
                                                                    </Columns>
                                                                </asp:DataGrid>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </asp:Panel>
                                                <!-- **************  Fin de Listado ****************** -->
                                                <!-- *********** Inicio de Actualización ************** -->
                                                <asp:Panel ID="panActualizaO" runat="server" Visible="false">
                                                    <table id="tableDatosO" border="0" cellpadding="0" cellspacing="0" style="width: 100%;
                                                        height: 100px">
                                                        <tr>
                                                            <td valign="middle">
                                                                <br />
                                                                <table style="width: 95%; height: 110px" border="0" cellpadding="0" cellspacing="0"
                                                                    class="FrameSimple">
                                                                    <tr style="height: 30px">
                                                                        <td align="left" class="style1">
                                                                            &nbsp;Nro Requisicion&nbsp;:
                                                                        </td>
                                                                        <td align="left" class="style2">
                                                                            <asp:Label ID="lblNroRequisicion" runat="server" CssClass="lbl"></asp:Label>
                                                                        </td>
                                                                        <td align="left" class="style1">
                                                                            &nbsp;Fecha&nbsp;:&nbsp;&nbsp;&nbsp;&nbsp;
                                                                        </td>
                                                                        <td align="left" class="style1">
                                                                            <asp:Label ID="LblFecha" runat="server" CssClass="lbl"></asp:Label>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td align="left" class="style1">
                                                                            &nbsp;Articulo Crudo :
                                                                        </td>
                                                                        <td align="left" class="style2">
                                                                            <asp:TextBox ID="TxtArticuloCrudoCod" runat="server" Width="70px" CssClass="txt"></asp:TextBox>
                                                                            <asp:TextBox ID="TxtArticuloCrudoNom" runat="server" CssClass="txt" Width="150px"></asp:TextBox>
                                                                            <input id="btnArticulos" class="btnSel" type="button" value="..." onclick="fdesArticulos(TxtArticuloCrudoCod,TxtArticuloCrudoNom)" />
                                                                        </td>
                                                                        <td align="left" class="style1">
                                                                            &nbsp;Articulo Base &nbsp;:&nbsp;&nbsp;&nbsp;&nbsp;
                                                                        </td>
                                                                        <td align="left" class="style1">
                                                                            <asp:TextBox ID="TxtArticuloBase" runat="server" Width="70px" CssClass="txt"></asp:TextBox><input
                                                                                id="Button1" class="btnSel" type="button" value="..." onclick="fdesUnidTinto(txtCodUnidadO,txtDescUnidadO, txtCodMaquinaO)" />
                                                                        </td>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="left" class="style1">
                                                                &nbsp;Acabado :
                                                            </td>
                                                            <td align="left" class="style2">
                                                                <asp:TextBox ID="TxtCod_Acabado" runat="server" CssClass="txt" Width="70px"></asp:TextBox>
                                                                <asp:TextBox ID="TxtDesc_Acabado" runat="server" CssClass="txt" Width="70px"></asp:TextBox>
                                                                <input id="Button3" class="btnSel" onclick="fdesAcabado(TxtCod_Acabado,TxtDesc_Acabado)"
                                                                    type="button" value="..." />
                                                            </td>
                                                            <td align="left" class="style1">
                                                                Color :
                                                            </td>
                                                            <td align="left" class="style1">
                                                                <asp:TextBox ID="TxtColorCod" runat="server" CssClass="txt" Width="51px"></asp:TextBox>
                                                                <asp:TextBox ID="TxtColorDesc" runat="server" CssClass="txt" Height="16px" ReadOnly="True"
                                                                    Width="197px"></asp:TextBox>
                                                                <input id="bntColor" class="btnSel" onclick="fdesColor(TxtColorCod,TxtColorDesc)"
                                                                    type="button" value="..." />
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="left" class="style1">
                                                                &nbsp;Tipo Colorante :
                                                            </td>
                                                            <td align="left" class="style2">
                                                                <asp:TextBox ID="TxtColoranteCod" runat="server" CssClass="txt" Width="70px"></asp:TextBox>
                                                                <asp:TextBox ID="TxtColoranteDes" runat="server" CssClass="txt" Width="70px"></asp:TextBox>
                                                                <input id="Button2" class="btnSel" onclick="fdesColorante(TxtColoranteCod,TxtColoranteDes)"
                                                                    type="button" value="..." />
                                                            </td>
                                                            <td align="left" class="style1">
                                                            </td>
                                                            <td align="left" class="style1">
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="left" class="style1">
                                                                &nbsp;Observaciones&nbsp;:
                                                            </td>
                                                            <td align="left" class="style2" colspan="3">
                                                                <asp:TextBox ID="TxtObservacionDetalle_Articulo" runat="server" Width="690px" CssClass="txt"
                                                                    Height="62px"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                    <table id="tblGrabarO" style="width: 90%; height: 40px" border="0" cellpadding="0"
                                                        cellspacing="0">
                                                        <tr>
                                                            <td style="width: 100%" align="center">
                                                                <asp:ImageButton ID="btnGrabarO" runat="server" Height="25px" ImageUrl="~/Images/Grabar.png"
                                                                    ToolTip="Grabar" Width="25px" />
                                                                &nbsp; &nbsp; &nbsp; &nbsp;
                                                                <asp:ImageButton ID="btnCancelarO" runat="server" ImageUrl="~/Images/Cancelar.png"
                                                                    ToolTip="Regresar / Cancelar" Height="25px" Width="25px" />
                                                            </td>
                                                        </tr>
                                                    </table>
                                            </td>
                                        </tr>
                                    </table>
                                    </asp:Panel>
                                    <!-- *********** Fin de Actualización ************** -->
                                    </td> </tr>
                                    <tr>
                                        <td>
                                            <table id="tblMsgO" style="width: 100%; height: 20px" border="0" cellpadding="0"
                                                cellspacing="0">
                                                <tr>
                                                    <td align="center">
                                                        <asp:Label ID="lblMsgO" runat="server"></asp:Label>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    </table>
                                </ContentTemplate>
                                <Triggers>
                                    <asp:AsyncPostBackTrigger ControlID="btnNuevo" EventName="click" />
                                    <asp:AsyncPostBackTrigger ControlID="btnEditar" EventName="click" />
                                </Triggers>
                            </asp:UpdatePanel>
                        </div>
                    </div>
                    <!-- **************  Fin de tabContenido ****************** -->
                </td>
            </tr>
        </table>
        <!-- ****************** Fin de Detalle ******************* -->
        </form>
    </center>
</body>
</html>
