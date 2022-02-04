<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="frmDesTabla.aspx.vb" Inherits="intranet_logi.frmDesTabla" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Buscar Datos</title>
    <base target="_self">
    <link href="../css/Styles_Paginas.css" type="text/css" rel="Stylesheet"/>
    <link href="../css/Styles_Controles.css" type="text/css" rel="Stylesheet" />
    <script language="javascript" src="../js/jsGeneral.js" type="text/jscript"></script>
    <script language="jscript" type="text/jscript">
        var aValor = new Array(1)

        aValor[0] = ""
        aValor[1] = ""


        function fdes_Aceptar() {

            if (frmData.txtRegSel.value == "") {
                alert("Seleccionar un registro de la lista");
                return false;
            }

            var iFila = frmData.txtRegSel.value;



            var table = document.getElementById("grdData");
            var rowAct = table.rows[iFila];


            aValor[0] = rowAct.cells[0].innerHTML;
            aValor[1] = rowAct.cells[1].innerHTML;

            window.returnValue = aValor
            window.close();

        }


        //=========================================================//

        function fdes_Salir() {

            window.returnValue = aValor;
            window.close();
        }

    </script>
</head>
<body>
    <form id="frmData" runat="server">
        <center>
            <table cellspacing="0" cellpadding="0" border="0">
                <tr style="height: 5px">
                    <td></td>
                </tr>
            </table>
            <table class="CabMain" style="width:550px; height:20px" cellspacing="0" cellpadding="0" border="0">
                <tr>
                    <td valign="bottom" align="right">
                        <asp:Label ID="lblTitulo" runat="server" Text="Label"></asp:Label>&nbsp;
                    </td>
                </tr>
            </table>
            <table class="FrameSimple" style="width:550px; height:60px" cellspacing="0" cellpadding="0" border="0">
                <tr>
                    <td align="left" width="120">&nbsp;
                        <asp:Label ID="lblTitCodigo1" runat="server"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="txtCodigo" runat="server" Width="100px" CssClass="txt"></asp:TextBox>
                    </td>
                    <td rowspan="2">
                        <asp:ImageButton ID="btnBuscar" runat="server" ImageUrl="~/images/Lupa.png"/>
                    </td>
                </tr>
                <tr>
                    <td align="left" width="120">&nbsp;
                        <asp:Label ID="lblTit2Descri" runat="server"></asp:Label>
                    </td>
                    <td align="left">
                        <asp:TextBox ID="txtDescri" runat="server" Width="350px" CssClass="txt"></asp:TextBox>
                    </td>
                </tr>
            </table>
            <table id="tblReg" style="width: 550px; height :25px" cellspacing="0" cellpadding="0" border="0">
                <tr>
                    <td align="right">
                        <asp:TextBox ID="txtOpc" runat="server" Width="30px" CssClass="txtHid"></asp:TextBox>
                        <asp:TextBox ID="txtRegSel" runat="server" Width="30px" CssClass="txtHid"></asp:TextBox>&nbsp;
                        Nro.Registros
                        <asp:TextBox id="txtReg" runat="server" Width="30px" CssClass="txtReg">0</asp:TextBox>
                    </td>
                </tr>
            </table>
            <table class="GridHeader" style="Width: 550px; height:20px" cellspacing="0" cellpadding="0" border="0">
                <tr>
                    <td width="100">
                        <asp:Label ID="lblTitCodigo2" runat="server"></asp:Label>
                    </td>
                    <td width="425">
                        <asp:Label ID="lblTitDescri2" runat="server"></asp:Label>
                    </td>
                    <td width="25"></td>
                </tr>
            </table>
            <div class="Grilla" id="divGrd" style="overflow: auto; width:548px; height:300px; text-align:left" >
                <asp:DataGrid ID="grdData" runat="server" Width="525px" ShowHeader="false" AutoGenerateColumns="false">
                    <AlternatingItemStyle CssClass="FilaAlt"/>
                    <Columns>
                        <asp:BoundColumn DataField="Codigo" ReadOnly="true">
                            <ItemStyle HorizontalAlign="left" Height="23px" Width="100px"/>
                        </asp:BoundColumn>
                        <asp:BoundColumn DataField="Descri" ReadOnly="true">
                            <ItemStyle HorizontalAlign="left" Height="23px" Width="425px"/>
                        </asp:BoundColumn>
                    </Columns>
                </asp:DataGrid>
            </div>
            <table cellspacing="0" cellpadding="0" width="400" border="0">
                <tr>
                    <td align="center">
                        <img style="width:33px; height:34px" onclick="fdes_Aceptar()" alt="Aceptar" src="../images/Yes.gif" border="0"/>
                        &nbsp;&nbsp;&nbsp;&nbsp;
                        <img style="width:34px; height:32px" onclick="fdes_Salir()" alt="Cancelar" src="../images/No.gif" border="0"/>
                    </td>
                </tr>
            </table>
            <asp:TextBox ID="txtSql" runat="server" Width="1px" CssClass="txtHid"></asp:TextBox>
            <asp:TextBox ID="txtFiltro" runat="server" Width="1px" CssClass="txtHid"></asp:TextBox>
        </center>
    </form>
</body>
</html>
