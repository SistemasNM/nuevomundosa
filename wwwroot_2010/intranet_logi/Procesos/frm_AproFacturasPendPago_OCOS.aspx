<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="frm_AproFacturasPendPago_OCOS.aspx.vb" Inherits="intranet_logi.frm_AproFacturasPendPago_OCOS" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../css/NM0001.css" rel="stylesheet" type="text/css" />
    <link href="../css/sytle.css" rel="stylesheet" type="text/css" />
    <script language="javascript" type = "text/javascript">
function checkAll(objRef)
{
    var GridView = objRef.parentNode.parentNode.parentNode;
    var inputList = GridView.getElementsByTagName("input");
    for (var i=0;i<inputList.length;i++)
    {
        //Get the Cell To find out ColumnIndex
        var row = inputList[i].parentNode.parentNode;
        if(inputList[i].type == "checkbox"  && objRef != inputList[i])
        {
            if (objRef.checked)
            {
                //If the header checkbox is checked
                //check all checkboxes
                //and highlight all rows
                row.style.backgroundColor = "#3399FF";
                inputList[i].checked=true;
            }
            else
            {
                //If the header checkbox is checked
                //uncheck all checkboxes
                //and change rowcolor back to original
                if(row.rowIndex % 2 == 0)
                {
                   //Alternating Row Color
                    row.style.backgroundColor = "#FFF7E5";
                }
                else
                {
                   row.style.backgroundColor = "white";
                }
                inputList[i].checked=false;
            }
        }
    }
}

function Check_Click(objRef) {
    //Get the Row based on checkbox
    var row = objRef.parentNode.parentNode;
    if (objRef.checked) {
        //If checked change color to Aqua
        row.style.backgroundColor = "#3399FF";
    }
    else {
        //If not checked change back to original color
        if (row.rowIndex % 2 == 0) {
            //Alternating Row Color
            row.style.backgroundColor = "#FFF7E5";
        }
        else {
            row.style.backgroundColor = "white";
        }
    }

    //Get the reference of GridView
    var GridView = row.parentNode;

    //Get all input elements in Gridview
    var inputList = GridView.getElementsByTagName("input");

    for (var i = 0; i < inputList.length; i++) {
        //The First element is the Header Checkbox
        var headerCheckBox = inputList[0];

        //Based on all or none checkboxes
        //are checked check/uncheck Header Checkbox
        var checked = true;
        if (inputList[i].type == "checkbox" && inputList[i] != headerCheckBox) {
            if (!inputList[i].checked) {
                checked = false;
                break;
            }
        }
    }
    headerCheckBox.checked = checked;

}
</script> 

</head>
<body>
    <form id="form1" runat="server">
    <center>
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        <asp:Panel ID="pnlcabecera" runat="server" HorizontalAlign="Center">
            <%--Cabecera--%>
            <table style="width:900px;" border="0" cellpadding="0" cellspacing="0" >
                <tr>
                    <td class="Cabecera" style="width:800px; height:30px" align="center" valign="middle">
                        LISTADO DE FACTURAS PENDIENTES DE APROBACION - OC/OS</td>
                </tr>
            </table>
        </asp:Panel>

    <!-- **************  Inicio de Contenido ****************** -->            
        <asp:UpdatePanel ID="panContenido" runat="server">
            <ContentTemplate>
                <%--imagen de espera--%>
                <asp:UpdateProgress ID="up_Load" DisplayAfter="300" runat="server" >   
                    <ProgressTemplate>
                        <div id="divLoad" class="CssLoadNormal" 
                            
                            style="width:900px; height:40px; vertical-align: middle; text-align: center;"><br />
                            <img src="../images/Loading.gif" style="vertical-align:middle; height:20px; width:20px" alt="" />Procesando...
                        </div>                        
                    </ProgressTemplate> 
                </asp:UpdateProgress>
                <%--Mensaje--%>
                <asp:Panel ID="pnlMensaje" runat="server">
                    <table cellspacing="0" cellpadding="0" border="0" width="900px">
                        <tr>
                            <td align="left" colspan="2">
                                <asp:Label ID="lblMsg" runat="server" CssClass="error"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td align="left" style="width: 450px;">
                                <asp:Button id="btnAprobarMasivo" runat="server" width="124px" 
                                    Text="Aprobar Masivo" CssClass="Boton" BackColor="#006600"></asp:Button>
                            </td>                           
                            <td align="right">
                                <asp:Button ID="btnActualizar" runat="server" CssClass="btnAzul" Text="Actualizar" />
                            </td>
                        </tr>
                    </table>
                    <table style="width:900px" id="Table4" class="botonera" border="0" cellspacing="0" cellpadding="0">
	                    <tr>
		                    <td style="width:800px" class="Etiqueta" align="right"># de documentos por aprobar:</td>
		                    <td style="width:100px"  align="right">
                                <asp:label id="lblCantidad" runat="server" width="63px" CssClass="Etiqueta" Font-Size="10px"></asp:label>
                            </td>
	                    </tr>
                    </table>
                </asp:Panel>
                <asp:Panel ID="pnlListado" runat="server">
                    <asp:GridView ID="grvFacturasPendientes" runat="server" Width="900px" AutoGenerateColumns="False"                        
                        DataKeyNames="int_IDFacturaOCOS">
                        <Columns>
                            <asp:TemplateField>
                                <HeaderTemplate>
                                  <asp:CheckBox ID="checkAll" runat="server" onclick = "checkAll(this);" />
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:CheckBox ID="chkFactura" runat="server"  onclick = "Check_Click(this)" />
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" Width="30px" />
                            </asp:TemplateField>
                            <asp:BoundField DataField="vch_TipoFactura" HeaderText="Tipo">
                            <ItemStyle Width="40px" HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="vch_NumeroFactura" HeaderText="Num. Factura">
                            <ItemStyle Width="110px" HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="vch_NombreProveedor" HeaderText="Proveedor">
                            <ItemStyle Width="280px" HorizontalAlign="Left" />
                            </asp:BoundField>
                            <asp:BoundField DataField="vch_CondPago" HeaderText="Cond. Pago">
                            <ItemStyle Width="180px" HorizontalAlign="Left" />
                            </asp:BoundField>
<%--                            <asp:BoundField DataField="vch_CodigoProveedor" HeaderText="RUC">
                            <ItemStyle Width="80px" HorizontalAlign="Center" />
                            </asp:BoundField>--%>
                            <asp:BoundField DataField="vch_NumeroOrco" HeaderText="OC/OS">
                            <ItemStyle Width="110px" HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="vch_Moneda" HeaderText="Moneda">
                            <ItemStyle Width="40px" HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="num_MontoTotal" HeaderText="Total Fact." DataFormatString="{0:N2}">
                            <ItemStyle Width="90px" HorizontalAlign="Right" />
                            </asp:BoundField>
                        </Columns>
                            <EmptyDataTemplate>
                                <table width="100%">
                                    <tr class ="gridheader">
                                        <td>&nbsp;</td>
                                    </tr>
                                    <tr class="GridItem">
                                        <td style="text-align:center;font-weight:bold;font-size:12px">No se encontro ningúna factura pendiente de Aprobación.</td>
                                    </tr>
                                </table>
                            </EmptyDataTemplate>
                        <HeaderStyle CssClass="gridheader" />
                        <RowStyle CssClass="GridItem" />
                        <AlternatingRowStyle CssClass="GridAlternateItem" />
                    </asp:GridView> 
                </asp:Panel>
            </ContentTemplate>
        </asp:UpdatePanel>
    </center>
    </form>
</body>
</html>
