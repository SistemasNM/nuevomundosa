<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="frm_Listado_Pagos_OrdenServicio_V2.aspx.vb" Inherits="intranet_logi.frm_Listado_Pagos_OrdenServicio_V2" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
     <title>Listado de Pagos de Ordenes de Servicio</title>
    <link href="../css/Styles_Paginas.css" type="text/css" rel="Stylesheet" />
    <link href="../css/Styles_Controles.css" type="text/css" rel="Stylesheet" />
    <link href="../css/NM0001.css" rel="Stylesheet" type="text/css"/>
    <script language="javascript" src="../js/functions.js" type="text/javascript" ></script>
    <script language="javascript" type="text/javascript">
        var matrizNumber = ".0123456789";
        var matrizInteger = "0123456789";
        var matrizAlfa = "abcdefghijklmnopqrstuwxyzABCDEFGHIJKLMNOPQRSTUWXYZ_";
        var matrizAlfaNumerico = "abcdefghijklmnopqrstuwxyzABCDEFGHIJKLMNOPQRSTUWXYZ_0123456789./";
        function FormatearBusqDoc(pTexto) {
            if (pTexto == 2)//numero
            {
                var lnume = '00000000000' + document.getElementById('txtNumOrden').value;
                lnume = lnume.substring(lnume.length, lnume.length - 10);
                if (lnume == '0000000000') {
                    document.getElementById('txtNumOrden').value = '';
                } else {
                    document.getElementById('txtNumOrden').value = lnume;
                } //end if

            } //end if
            return false;
        } //end function

        function btnSeleccion_Edit(strOS, strItem, strMonto) {
            //window.location.href = "frmFichaProveedor.aspx?strNumeroOrdenServicio=" + strCodigo;
            var retorno = window.showModalDialog("frm_MontoOSPorItem.aspx?strNumeroOrdenServicio=" + strOS + "&strItem=" + strItem + "&strMonto=" + strMonto, "", "dialogHeight:600px;dialogWidth:900px;center:yes;help:no;");
            document.all('hdnPopUp').value = "1";
            if (retorno == "") {
                return false;
            }
            return false;
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <%--<asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>--%>
            <center>
                <asp:Panel ID="pnlcabecera" runat="server" HorizontalAlign="Center">
                    <%--Cabecera--%>
                    <table style="width:800px;" border="0" cellpadding="0" cellspacing="0" >
                        <tr>
                            <td class="Cabecera" style="width:800px; height:30px" align="left" valign="middle">&nbsp;LISTADO DE PAGOS DE ORDEN DE SERVICIO</td>
                        </tr>
                        <tr>
                            <td>&nbsp;</td>
                        </tr>
                    </table>
                </asp:Panel>
                <table style="width:500px" border="0" cellpadding="0" cellspacing="0" >
                    <tr style="height: 25px">
                        <td align="left" class="Etiqueta" style="width:150px">&nbsp;Num. O/S:</td>
                            <td align="left" class="Etiqueta" style="width:220px">
                                <asp:TextBox ID="txtSerie" runat="server" CssClass="inputDisabled" Enabled="false"
                                    Font-Size="9px" Width="30px"></asp:TextBox>&nbsp;-&nbsp;
                                <asp:TextBox ID="txtNumOrden" runat="server" CssClass="input" Font-Size="9px" 
                                    MaxLength="10" Width="80px"></asp:TextBox>
                            </td>
                    </tr>
                    <tr style="height:25px">
                        <td align="left" class="Etiqueta" style="width:150px">&nbsp;Monto Total:</td>
                        <td align="left" class="Etiqueta" style="width:220px">
                            <asp:TextBox ID="txtMontoT" Enabled="false" runat="server"  Font-Size="9px"  Width="100px" Height="18px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr style="height: 25px">
                            <td class="Etiqueta"></td>
                            <td align="right" class="Etiqueta" style="width:40px" rowspan="2">
                                <asp:Button ID="btnBuscar" runat="server" CssClass="Boton" Text="Buscar" />
                            </td>
                            <%--<td align="right" class="Etiqueta" style="width:40px;" rowspan="2">
                                <asp:Button ID="btnAceptar" runat="server" CssClass="Boton" Text="Guardar" />
                             </td>--%>
                     </tr>            
                </table>
                <table cellspacing="0" cellpadding="0" border="0" style="width:500px; height:20px" >
                    <tr>
                         <td style="width:500px" class="Etiqueta" align="center">
                                <asp:Label ID="lblMsg" runat="server" CssClass="error"></asp:Label>
                         </td>
                    </tr>
               </table>
               <table>
                <asp:DataGrid ID="OSGrid" runat="server" Width="500px" Height="80px" 
                   AutoGenerateColumns="False" ShowFooter="True" Font-Bold="False" 
                   Font-Italic="False" Font-Overline="False" Font-Strikeout="False" 
                   Font-Underline="False" HorizontalAlign="Center" >
                   <AlternatingItemStyle CssClass="GridAltItem" Font-Bold="False" 
                            Font-Italic="False" Font-Overline="False" Font-Strikeout="False" 
                            Font-Underline="False" HorizontalAlign="Center"/>
                   <ItemStyle CssClass="GridItem" Font-Bold="False" Font-Italic="False" 
                            Font-Overline="False" Font-Strikeout="False" Font-Underline="False" 
                            HorizontalAlign="Center" Font-Size="100px"/>
                    <Columns>
                            <asp:TemplateColumn HeaderText = "Item">
                               <ItemTemplate>
                                    <asp:Label ID="lblItem" runat="server" Text='<%# String.Format("{0:N}", Container.DataItem("Item"))%>'></asp:Label>
                                </ItemTemplate> 
                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Names="ddddddd" 
                                        Font-Overline="False" Font-Strikeout="False" Font-Underline="False" Width="60px" />
                            </asp:TemplateColumn>
                            
                            <asp:TemplateColumn HeaderText="Monto">
                                    <ItemTemplate>
                                        <asp:Label ID="lblMonto" runat="server" Text='<%# String.Format("{0:N}", Container.DataItem("Monto"))%>'></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:TextBox ID="txtMonto" runat="server" Width="60px" CssClass="input" >
                                        </asp:TextBox>
                                    </FooterTemplate>

                                    <%--<EditItemTemplate>
                                        <asp:TextBox ID="txtMontoedt" runat="server" Text='<%# String.Format("{0:N}",Container.DataItem("Monto"))%>' Width="60px" CssClass="input" ></asp:TextBox>
                                    </EditItemTemplate>--%>
                                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Names="ddddddd" 
                                        Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="Estado" Visible="false">
                                <ItemStyle Width="45px"/>
                                <ItemTemplate>
                                    <asp:Label ID="lblEstado" runat="server" Text='<%#  Container.DataItem("Estado")%>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="Editar">
                                <ItemStyle Width="45px"/>
                                <ItemTemplate>
                                    <asp:LinkButton ID="lnkEdit" runat="server" CausesValidation="false" Text="<img border=0 src=../images/im_edit.gif alt=editar>" CommandName="Edit"></asp:LinkButton>
                                </ItemTemplate>
                                <%--<EditItemTemplate>
                                    <asp:LinkButton ID="lnkUpdate" runat="server" Text="<img border=0 src=../images/save.gif alt=guardar/actualizar>" CommandName="Update">
                                        <img border="0" src="../images/save.gif"/ alt="guardar/actualizar" width="16" height="16">
                                    </asp:LinkButton>&nbsp;
                                    <asp:LinkButton ID="lnkCancel" runat="server" CausesValidation="false" Text="<img border=0 src=../images/delete.gif alt=cancelar>" CommandName="Cancel">
                                    </asp:LinkButton>
                                </EditItemTemplate>--%>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="Eliminar" ItemStyle-Width="50px">
                                <ItemTemplate>
                                    <asp:LinkButton ID="lnkDelete" runat="server" CausesValidation="false" Text="<img border=0 src=../images/delete.gif alt=eliminar>" CommandName="Delete">
                                        <img border="0" src="../images/delete.gif" alt="eliminar" width="16" height="16"/>
                                    </asp:LinkButton>    
                                </ItemTemplate>
                                <FooterTemplate>
                                    <asp:LinkButton ID="linkAgregar" runat="server" CommandName="AddNewRow">
                                        <img border="0" src="../images/save.gif" alt="agregar" width="16" height="16"/>
                                    </asp:LinkButton>
                                </FooterTemplate>
                            </asp:TemplateColumn>
                    </Columns>   
                 </asp:DataGrid>
               </table>
               <table style="width:500px" border="0" cellpadding="0" cellspacing="0">
                    <tr align="center">
                        <td align="center" class="Etiqueta" style="width:40px" rowspan="2">
                            Suma :
                        </td>
                        <td align="left" style="width:50px" rowspan="2">
                            <asp:TextBox ID="TextSum" CssClass="txtReadOnly" Enabled="false" runat="server"  Font-Size="9px"  Width="100px" Height="18px"></asp:TextBox>
                        </td>
                        <td>
                            <asp:HiddenField id="hdnPopUp" runat="server"/>
                        </td>            
                    </tr>
                </table>
            </center>
    </form>
</body>
</html>
