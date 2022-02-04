<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="frmArticulosDistribucion.aspx.vb" Inherits="intranet_logi.frmArticulosDistribucion" %>
<%@ Register Assembly="Infragistics35.Web.v10.1, Version=10.1.20101.1011, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" Namespace="Infragistics.Web.UI.ListControls" TagPrefix="ig" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<title>Artículos de Distribución</title>
 <link rel="stylesheet" type="text/css" href="../css/NM0001.css"/>
 <link href ="../../intranet/Estilos/Styles_Paginas.css" type="text/css" rel="Stylesheet"/>
 <link href ="../../intranet/Estilos/Styles_Controles.css" type="text/css" rel="Stylesheet"/>
 <script src="../../intranet/js/LibGrales.js" type="text/javascript"></script>
    <style type="text/css">
        .style4
        {
            width: 68px;
        }
        .style6
        {
            width: 676px;
        }
        .style7
        {
            width: 532px;
        }
        .style8
        {
            width: 697px;
        }
        .style9
        {
            width: 206px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" EnablePageMethods="true" runat="server">
  </asp:ScriptManager>
    <center>
        <div>
        <table class="CabMain" border="0" cellpadding="0" cellspacing="0" 
            style="width:1456px; height:30px">
            <tr>
                <td style="width:100%;Font-Size:14px;" valign="bottom" align="center" >
                    Asociar Artículos de Distribución</td>
            </tr>
        </table>
        <br />
        <table border="0" cellpadding="0" cellspacing="5" 
                style="width: 1455px; margin-left: 0px">
            <tr>
                <td class="style7">
                    <asp:TextBox ID="txtArtSAsoc" runat="server" Width="542px"></asp:TextBox>
                </td>
                <td style="text-align:left;" class="style9">
                    <asp:Button ID="btnBuscar" runat="server" Text="Buscar" CssClass="Boton"  Height="25px" Width="85px"/>
                </td>
                <td class="style6" style="text-align:left;">
                    <asp:Label ID="lblTitulo" Text="Copiar Artículo: " runat="server" Width="111px" 
                        style="text-align:left;font-size:14px"></asp:Label>
                    <asp:Label ID="lblCodigoArticulo" runat="server" Width="562px" Text="Copiar artículo"
                        style="text-align:left;font-size:14px;font-style: italic;" ForeColor="Red"></asp:Label>
                </td>
            </tr>
        </table>
        <table>
            <tr>
                <td style="text-align:left;font-size:14px" class="style8">
                    Artículos de Regulares
                </td>
                <td></td>
                <td style="text-align:left;font-size:14px" class="style6">
                    Artículos de Distribución
                </td>
            </tr>
            <tr>
                <td class="style8">
                    <asp:ListBox ID="lstLeft" runat="server" SelectionMode="Multiple" Width="697px" Height="400px">
                    </asp:ListBox>
                </td>
                <td class="style4">
                    <center>
                        <asp:Button ID="btnRight" Text=">" runat="server" OnClick="RightClick" 
                            Height="34px" Width="44px" /><br /><br />  
                        <asp:Button ID="btnLeft" Text="<" runat="server" OnClick="LeftClick" Height="34px" Width="44px"/>                                               
                    </center>
                </td>
                <td class="style6">
                    <asp:ListBox ID="lstRight" runat="server" SelectionMode="Multiple" Width="687px" Height="400px" style="margin-left: 0px" AutoPostBack="true">
                    </asp:ListBox>
                </td>
            </tr>
        </table>
    </div>
    </center>
    </form>
</body>
</html>
