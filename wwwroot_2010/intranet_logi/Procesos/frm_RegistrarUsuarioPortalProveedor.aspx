<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="frm_RegistrarUsuarioPortalProveedor.aspx.vb"
    Inherits="intranet_logi.frm_RegistrarUsuarioPortalProveedor" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html>
<head>
    <title>Registrar Usuario Portal Proveedor</title>
    <link href="../css/NM0001.css" type="text/css" rel="stylesheet" />
    <script language="javascript" type="text/javascript">
        function fnc_txtCodigoProveedor_onkeyPress() {
            if (event.keyCode == 13) {
                var txtCodigoProveedor = document.getElementById('txtCodigoProveedor').value
                if (txtCodigoProveedor.length > 0) {
                    document.getElementById("btnBuscarProveedor").click();
                }
                return false;
            } else {
                return true;
            }
        }
    </script>
    <style>
        .btnHid
        {
	        border-width:0px;
	        background-color:#ffffff;
	        color:#ffffff;
	        font-size:8px;
	        width: 1px;
        }        
    </style>
</head>
<body>
    <center>
        <form id="frmRegistrarUsuarioPortalProveedor" name="frmRegistrarUsuarioPortalProveedor"
        method="post" runat="server">
        <table cellspacing="0" cellpadding="0" border="0" style="width: 500px;">
            <tr>
                <td>
                    <table cellspacing="0" cellpadding="0" border="0" class="Cabecera" style="width: 100%;
                        height: 20px">
                        <tr>
                            <td class="cabecera" style="height: 27px" align="center">
                                REGISTRAR USUARIO PROVEEDOR
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td>
                    <table cellspacing="2" cellpadding="0" width="100%" border="0">
                        <tr>
                            <td class="Etiqueta" style="width: 30px; height: 15px">
                                &nbsp;
                            </td>
                            <td class="Etiqueta" style="width: 100px; height: 15px">
                                C&oacute;digo Proveedor:
                            </td>
                            <td class="Etiqueta" style="width: 250px; height: 15px">
                                <asp:TextBox ID="txtCodigoProveedor" runat="server" Width="158px" CssClass="input"></asp:TextBox>
                                <asp:Button ID="btnBuscarProveedor" runat="server" Text="Button" CssClass="btnHid"
                                    Height="1px" Width="1px" />
                            </td>
                        </tr>
                        <tr>
                            <td class="Etiqueta" style="width: 30px; height: 15px">
                                &nbsp;
                            </td>
                            <td class="Etiqueta" style="width: 100px; height: 15px">
                                Nombre Proveedor:
                            </td>
                            <td class="Etiqueta" style="width: 300px; height: 15px">
                                <asp:TextBox ID="txtNombreProveedor" runat="server" Width="300px" CssClass="inputDisabled2"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="Etiqueta" style="width: 30px; height: 15px">
                                &nbsp;
                            </td>
                            <td class="Etiqueta" style="width: 100px; height: 15px">
                                Email Proveedor:
                            </td>
                            <td class="Etiqueta" style="width: 300px; height: 15px">
                                <asp:TextBox ID="txtCorreoElectronicoProveedor" runat="server" Width="300px" CssClass="inputDisabled2"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="Etiqueta" style="width: 30px; height: 15px">
                                &nbsp;
                            </td>
                            <td class="Etiqueta" style="width: 100px; height: 15px">
                                Usuario:
                            </td>
                            <td class="Etiqueta" style="width: 150px; height: 15px">
                                <asp:TextBox ID="txtUsuario" runat="server" Width="150px" CssClass="inputDisabled2"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="Etiqueta" style="width: 30px; height: 15px">
                                &nbsp;
                            </td>
                            <td class="Etiqueta" style="width: 100px; height: 15px">
                                Password:
                            </td>
                            <td class="Etiqueta" style="width: 300px; height: 15px">
                                <asp:TextBox ID="txtPassword" runat="server" Width="150px" 
                                    CssClass="inputDisabled2"></asp:TextBox>
                                &nbsp;&nbsp;&nbsp;
                                <asp:Button ID="btnRegistrarUsuario" runat="server" CssClass="Boton" Text="Generar" />
                            </td>
                        </tr>
                        <tr>
                            <td class="Etiqueta" style="width: 30px; height: 15px">
                                &nbsp;
                            </td>
                            <td class="Etiqueta" style="width: 100px; height: 15px">
                                &nbsp;
                            </td>
                            <td class="Etiqueta" style="width: 300px; height: 15px">
                                <asp:Label ID="lblMensaje" runat="server"></asp:Label>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
        <asp:HiddenField ID="hdnCodigoEmpresa" runat="server" />
        <asp:HiddenField ID="hdnSituacionProveedor" runat="server" />
        <asp:HiddenField ID="hdnEstadoUsuario" runat="server" />
        <asp:HiddenField ID="hdnExiste" runat="server" />
        </form>
    </center>
</body>
</html>
