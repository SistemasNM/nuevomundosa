<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="frm_Generar_OS_Conformidad.aspx.vb" Inherits="intranet_logi.frm_Generar_OS_Conformidad" %>
<%@ Register Assembly="Infragistics35.Web.v10.1, Version=10.1.20101.1011, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" Namespace="Infragistics.Web.UI.EditorControls" TagPrefix="ig" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../css/NM0001.css" rel="stylesheet" type="text/css" />
    <link href="../css/Styles_Controles.css" rel="stylesheet" type="text/css" />
    <link href="../css/Styles_Paginas.css" rel="stylesheet" type="text/css" />
    <script language="JavaScript" src="../../intranet/js/jsDesTabla.js" type="text/jscript" ></script>    
    <script language="JavaScript" src="../../intranet/js/jsGeneral.js" type="text/jscript" ></script> 
</head>
<body>
    <form id="frmConformidad" runat="server">
        <center>
        <asp:Panel ID="pnlcabecera" runat="server" HorizontalAlign="Center">
            <%--Cabecera--%>
            <table style="width:800px;" border="0" cellpadding="0" cellspacing="0" >
                <tr>
                    <td class="Cabecera" style="width:800px; height:30px" align="center" valign="middle">&nbsp;CONFORMIDAD DE ORDEN DE SERVICIO</td>
                </tr>
                <tr>
                    <td>&nbsp;</td>
                </tr>
            </table>
        </asp:Panel>
    <!-- **************  Inicio de Contenido ****************** -->            
        <asp:UpdatePanel ID="panContenido" runat="server">
            <ContentTemplate>
                <%--imagen de espera--%>
                <asp:UpdateProgress ID="up_Load" DisplayAfter="1" runat="server" >   
                    <ProgressTemplate>
                        <div id="divLoad" class="CssLoadNormal" 
                            style="width:800px; height:40px; vertical-align: middle;"><br />
                            <img src="../images/Loading.gif" style="vertical-align:middle; height:20px; width:20px" alt="" />Procesando...
                        </div>                        
                    </ProgressTemplate> 
                </asp:UpdateProgress>
                <asp:Panel ID="pnlDatosOS" runat="server" Visible="true" Width="800px">
                <%--Cabecera--%>
                    <table style="width:100%; height:110px" border="0" cellpadding="0" cellspacing="0" class="FrameSimple">
                        <tr>
                            <td align="left" style="width:150px">ORDEN SERVICIO:&nbsp;</td>
                            <td align="left" style="width:400px">
                                <asp:Label id="lblNroOrdeServicio" runat="server" CssClass="lbl"></asp:Label>
                            </td>
                            <td align="left" style="width:100px">NRO. REQUISICION:&nbsp;</td>
                            <td align="left" style="width:200px">
                                <asp:TextBox ID="txtNroRequisicion" runat="server" CssClass="txtReadOnly" Width="100px" ></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td align="left" style="width:150px">PROVEEDOR:&nbsp;</td>
                            <td align="left" style="width:400px">
                                <asp:Label ID="lblNombreProveedor" runat="server" CssClass="lbl"></asp:Label>
                            </td>
                            <td align="left" style="width:100px">RUC:&nbsp;</td>
                            <td align="left" style="width:200px">
                                <asp:Label ID="lblRuc" runat="server" CssClass="lbl"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td align="left" style="width:150px">NOMBRE DE CONTACTO:&nbsp;</td>
                            <td align="left" style="width:400px">
                                <asp:Label ID="lblNombreContacto" runat="server" CssClass="lbl"></asp:Label>
                            </td>
                            <td align="left" style="width:100px">EMAIL:&nbsp;</td>
                            <td align="left" style="width:200px">
                                <asp:Label ID="lblEmail" runat="server" CssClass="lbl"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td align="left" style="width:150px">TELEFONO CONTACTO:&nbsp;</td>
                            <td align="left" style="width:400px">
                                <asp:Label ID="lblTelefonoContacto" runat="server" CssClass="lbl"></asp:Label>
                            </td>
                            <td align="left" style="width:100px">ESTADO DOC.:&nbsp;</td>
                            <td align="left" style="width:200px">
                                <asp:Label ID="lblEstado" runat="server" CssClass="lbl"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td align="left" style="width:150px">FECHA O/S:&nbsp;</td>
                            <td align="left" style="width:400px">
                                <asp:Label ID="lblFecha" runat="server" CssClass="lbl"></asp:Label>
                            </td>
                            <td align="left" style="width:100px">SOLICITADO POR:&nbsp;</td>
                            <td align="left" style="width:200px">
                                <asp:Label ID="lblUsuario" runat="server" CssClass="lbl"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td align="left" style="width:150px">OBSERVACIONES O/S:&nbsp;</td>
                            <td align="left" colspan="3" style="width:650px">
                                <asp:Label ID="lblObservaciones" runat="server" CssClass="lbl"></asp:Label>
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
                <asp:Panel ID="pnlConformidad" runat="server">
                    <%--Registro parte 1:--%>
                    <table style="width:800px" border="0" cellpadding="0" cellspacing="0" class="FrameSimple">
                        <tr style="height: 25px">
                            <td align="left" style="width:150px">Servicio:&nbsp;</td>
                            <td align="left" style="width:150px">
                                <asp:RadioButton ID="rdTiposervicio1" runat="server" text="A todo costo" Checked="true" GroupName="Servicio" Width="100px" />
                            </td>
                            <td align="left" style="width:100px">
                                <asp:RadioButton ID="rdTiposervicio2" runat="server" GroupName="Servicio" text="Mano de Obra"  Width="100px"/>
                            </td>
                            <td align="left" style="width:400px"></td>
                        </tr>

                        <tr style="height: 25px">
                            <td align="left" style="width:150px">Fecha Inicio:&nbsp;</td>
                            <td align="left" style="width:150px">
                                <ig:webdatepicker id="wdpFechaInicio" runat="server" Width="100px"></ig:webdatepicker>
                            </td>
                            <td align="left" style="width:100px">Fecha Fin&nbsp;:</td>
                            <td align="left" style="width:400px">
                                <ig:webdatepicker id="wdpFechaFin" runat="server" Width="100px"></ig:webdatepicker>
                            </td>
                        </tr>
                        <tr style="height: 25px">
                            <td align="left" style="width:150px">Tiempo Ofertado:&nbsp;</td>
                            <td align="left" style="width:150px">
                                <asp:TextBox ID="TxtTiempoOfertado" runat="server" CssClass="txtNumero" 
                                    Width="100px" ></asp:TextBox>
                            </td>
                            <td align="left" style="width:100px">Tiempo Real:&nbsp;</td>
                            <td align="left" style="width:400px">
                                <asp:TextBox ID="TxtTiempoReal" runat="server" CssClass="txtNumero" 
                                    Width="100px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr style="height: 25px">
                            <td align="left" style="width:150px">Requerido Por:&nbsp;</td>
                            <td align="left" style="width:150px">
                                <asp:TextBox ID="txtCodigoTrabajador" runat="server" CssClass="txt" Width="100px"></asp:TextBox>&nbsp;
                                <input id="btnTrabajador" class="Boton" style="width:20px; height:20px" onclick="fdesUsuarios(TxtCodigoTrabajador,TxtNombreTrabajador)" type="button" value="..." />
                            </td>
                            <td align="left" colspan="2">
                                <asp:Label ID="lblNombreTrabajador" runat="server" CssClass="lbl"></asp:Label>
                            </td>
                        </tr>
                        <tr style="height: 25px">
                            <td align="left" style="width:150px">Area Solicitante:&nbsp;</td>
                            <td align="left" style="width:150px">
                                <asp:TextBox ID="txtAreaSolicitante" runat="server" CssClass="txt" Width="100px" />&nbsp;
                                <input id="Button1" class="Boton" style="width:20px; height:20px" onclick="fdesUsuarios(TxtCodigoTrabajador,TxtNombreTrabajador)" type="button" value="..." />
                            </td>
                            <td align="left" colspan="2">
                                 <asp:Label ID="lblAreaSolicitante" runat="server" CssClass="lbl"></asp:Label>
                            </td>
                        </tr>
                        <tr style="height: 25px">
                            <td align="left" style="width:150px">&nbsp;</td>
                            <td align="left" style="width:150px">&nbsp;</td>
                            <td align="left" style="width:100px">&nbsp;</td>
                            <td align="left" style="width:400px">&nbsp;</td>
                        </tr>
                    </table>
                </asp:Panel>
            </ContentTemplate>
         </asp:UpdatePanel>
        </center>
    </form>
</body>
</html>
