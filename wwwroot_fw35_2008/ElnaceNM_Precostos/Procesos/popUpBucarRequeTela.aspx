<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="popUpBucarRequeTela.aspx.vb" Inherits="ElnaceNM_PreCostos.popUpBucarRequeTela" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html>
<head>
    <title>Requerimiento de Analisis de Tela</title>
    <meta content="False" name="vs_showGrid" />
    <meta content="Microsoft Visual Studio.NET 7.0" name="GENERATOR" />
    <meta content="Visual Basic 7.0" name="CODE_LANGUAGE" />
    <meta content="JavaScript" name="vs_defaultClientScript" />
    <meta http-equiv="PRAGMA" content="NO-CACHE" />
    <meta http-equiv="Expires" content="-1" />
    <link href="../style/Styles.css" type="text/css" rel="stylesheet" />
    <script type="text/javascript" language="JavaScript" src="../Js/jsDesTabla.js"></script>
    <style type="text/css">
        .style8
        {
            height: 91px;
        }
    </style>
    <script language="javascript" type="text/javascript">
        returnValue = "";
        function btnEscoger_Onclick(Codigo,Referencia) {
            returnValue = Codigo + ":" + Referencia;
            window.close();
        }
    </script>
</head>
<body id="bdAnalisiTela">
    <form id="frmReqAnalisisTela" method="post" runat="server">
    <table cellspacing="0" cellpadding="0" width="600" border="0">
        <tr>
            <td class="style8">
                <br />
                <asp:DataGrid ID="dgDatos" runat="server" AutoGenerateColumns="False" Width="500px"
                    Height="80px" Style="margin-left: 5%;">
                    <FooterStyle CssClass="GridFooter"></FooterStyle>
                    <AlternatingItemStyle CssClass="GridAltItem"></AlternatingItemStyle>
                    <ItemStyle CssClass="GridItem"></ItemStyle>
                    <HeaderStyle CssClass="GridHeader"></HeaderStyle>
                    <Columns>
                        <asp:TemplateColumn HeaderText="" HeaderStyle-Width="10" ItemStyle-HorizontalAlign="center">
                            <ItemTemplate>
                                <input class="input" runat="server" name="btnEscoger" id="btnEscoger" type="button" value="..." />
                            </ItemTemplate>
                            <HeaderStyle Width="10px"></HeaderStyle>
                        </asp:TemplateColumn>
                        <asp:BoundColumn DataField="VCH_ID_REQUER" HeaderText="N° de DES" ItemStyle-HorizontalAlign="Center"
                            HeaderStyle-HorizontalAlign="Center">
                            <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                            <ItemStyle HorizontalAlign="Center"></ItemStyle>
                        </asp:BoundColumn>
                        <asp:BoundColumn DataField="VCH_REFERENCIA" HeaderText="Referencia" ItemStyle-HorizontalAlign="Center"
                            HeaderStyle-HorizontalAlign="Center" ItemStyle-Width="300px">
                            <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                            <ItemStyle HorizontalAlign="Center"></ItemStyle>
                        </asp:BoundColumn>
                    </Columns>
                </asp:DataGrid>
                <br />
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
