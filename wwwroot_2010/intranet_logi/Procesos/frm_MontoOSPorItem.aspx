<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="frm_MontoOSPorItem.aspx.vb" Inherits="intranet_logi.frm_MontoOSPorItem" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Pago de OS por Item</title>
    <base target="_self"/>
    <link href="../css/Styles_Controles.css" type="text/css" rel="Stylesheet"/>
    <link href="../css/Styles_Paginas.css" type="text/css" rel="Stylesheet"/>
    <link href="../css/NM0001.css" type="text/css" rel="Stylesheet"/>
    <link href="../css/tab.webfx.css" type="text/css" rel="Stylesheet"/>
    <script language="javascript" src="../../intranet/js/jsDesTabla.js" type="text/jscript"></script>
</head>
<body>
    <form id="form1" runat="server">
        <center>
            <table border="0" cellpadding="0" cellspacing="0" style="width:800px; height:30px">
                <tr>
                  <td class="Cabecera">Detalle de Actividades por Item</td>
                 </tr>
            </table>
            <table border="0" cellpadding="0" cellspacing="0" style="width:800px">
                <tr>
                    <td>
                        <table style="width:100%; height:110px" border="0" cellpadding="0" cellspacing="0" class="FrameSimple">
                            <tr>
                               <td align="left" style="width:150px">ORDEN SERVICIO:&nbsp;</td>
                               <td align="left" style="width:400px"><asp:Label id="lblNroOrdeServicio" runat="server" CssClass="lbl"></asp:Label></td>
                               <td align="left" style="width:100px">NRO. REQUISICION:&nbsp;</td>
                               <td align="left" style="width:200px">
        
                                <asp:TextBox ID="txtNroRequisicion" runat="server" CssClass="txtReadOnly" Width="100px" ></asp:TextBox>
                               </td>
                             </tr>
                            <tr>
                                 <td align="left" style="width:150px">PROVEEDOR:&nbsp;</td>
                                 <td align="left" style="width:400px"><asp:Label ID="lblNombreProveedor" runat="server" CssClass="lbl"></asp:Label></td>
                                 <td align="left" style="width:100px">RUC:&nbsp;</td>
                                 <td align="left" style="width:200px"><asp:Label ID="lblRuc" runat="server" CssClass="lbl"></asp:Label></td>
                            </tr>
                            <tr>
                                <td align="left" style="width:150px">NOMBRE DE CONTACTO:&nbsp;</td>
                                <td align="left" style="width:400px"><asp:Label ID="lblNombreContacto" runat="server" CssClass="lbl"></asp:Label></td>
                                <td align="left" style="width:100px">EMAIL:&nbsp;</td>
                                <td align="left" style="width:200px"><asp:Label ID="lblEmail" runat="server" CssClass="lbl"></asp:Label></td>
                            </tr>
                            <tr>
                               <td align="left" style="width:150px">TELEFONO CONTACTO:&nbsp;</td>
                               <td align="left" style="width:400px"><asp:Label ID="lblTelefonoContacto" runat="server" CssClass="lbl"></asp:Label></td>
                               <td align="left" style="width:100px">ESTADO DOC.:&nbsp;</td>
                               <td align="left" style="width:200px"><asp:Label ID="lblEstado" runat="server" CssClass="lbl"></asp:Label></td>
                             </tr>
                             <tr>
                               <td align="left" style="width:150px">FECHA O/S:&nbsp;</td>
                               <td align="left" style="width:400px"><asp:Label ID="lblFecha" runat="server" CssClass="lbl"></asp:Label></td>
                               <td align="left" style="width:100px">SOLICITADO POR:&nbsp;</td>
                               <td align="left" style="width:200px"><asp:Label ID="lblUsuario" runat="server" CssClass="lbl"></asp:Label></td>
                             </tr>
                             <tr>
                                <td align="left" style="width:150px">RECIBE CHARLA DE SEGURIDAD:</td>
                                <td align="left" style="width:200px"><asp:Label ID="lblTipoServicio" runat="server" CssClass="lbl"></asp:Label></td>
                             </tr>
                             <tr>
                              <td align="left" style="width:100px">ITEM:&nbsp;</td>
                                <td align="left" style="width:200px"><asp:Label ID="lblItem" runat="server" CssClass="lbl"></asp:Label></td>
                                 <td align="left" style="width:150px">MONTO TOTAL POR ITEM:&nbsp;</td>
                               <td align="left" style="width:100px"><asp:Label ID="lblMonto" runat="server" CssClass="lbl"></asp:Label></td>
                             </tr>
                             <tr>
                                <td align="left" style="width:150px">OBSERVACIONES O/S:&nbsp;</td>
                                <td align="left" colspan="3" style="width:400px"><asp:Label ID="lblObservaciones" runat="server" CssClass="lbl"></asp:Label></td>
                              
                             </tr>
                        </table>
                    </td>
                </tr>
            </table>
             <table id="tblMsgO" style="width:800px" border="0" cellpadding="0" cellspacing="0">
                    <tr>
                    <td align="center" style="width:800px"><asp:Label ID="lblError" runat="server" Font-Size="10px" CssClass="error "></asp:Label></td>
                    </tr>
            </table>
            <table>
                <tr>
                    <td>
                         <asp:DataGrid  ID="grvItem" runat="server" AutoGenerateColumns="False" 
                            Width="790px" Height="50px"  DataKeyNames ="Numero,Item">
                            <AlternatingItemStyle CssClass="GridAlternateItem"></AlternatingItemStyle>
	                        <ItemStyle CssClass="GridItem"></ItemStyle>
	                        <HeaderStyle CssClass="gridheader"></HeaderStyle>        
                            <Columns>
                                    <asp:TemplateColumn HeaderText="Secuencia">
                                        <ItemTemplate>
                                                <asp:Label ID="lblSecu" runat="server" Text='<%# Bind("NU_SECU") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle  HorizontalAlign="Center" Width="80px"/>
                                    </asp:TemplateColumn>
                                    <asp:TemplateColumn HeaderText="Servicio">
                                        <ItemTemplate>
                                                <asp:Label ID="lblItem" runat="server" Text='<%# Bind("DESC_ITEM") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle  HorizontalAlign="Center" Width="720px"/>
                                    </asp:TemplateColumn>
                                    <asp:TemplateColumn HeaderText="Monto">
                                        <ItemTemplate>
                                                <asp:Label ID="lblMonto" runat="server" Text='<%# Bind("MONTO") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle  HorizontalAlign="Left" Width="40px"/>
                                         <EditItemTemplate>
                                            <asp:TextBox ID="txtMonto" runat="server" Text='<%# Bind("MONTO")%>' Width="70px" CssClass="input" ></asp:TextBox>
                                        </EditItemTemplate>
                                    </asp:TemplateColumn>
                                     <asp:TemplateColumn HeaderText="Editar">
                                        <ItemStyle Width="60px"/>
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lnkEdit" runat="server" CausesValidation="false" Text="<img border=0 src=../images/im_edit.gif alt=editar>" CommandName="Edit"></asp:LinkButton>
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <asp:LinkButton ID="lnkUpdate" runat="server" Text="<img border=0 src=../images/save.gif alt=guardar/actualizar>" CommandName="Update">
                                                <img border="0" src="../images/save.gif"/ alt="guardar/actualizar" width="16" height="16">
                                            </asp:LinkButton>&nbsp;
                                            <asp:LinkButton ID="lnkCancel" runat="server" CausesValidation="false" Text="<img border=0 src=../images/delete.gif alt=cancelar>" CommandName="Cancel">
                                            </asp:LinkButton>
                                        </EditItemTemplate>
                                    </asp:TemplateColumn>
                            </Columns>
                        </asp:DataGrid>
                    </td>
                </tr>
            </table>
            <table style="width:500px" border="0" cellpadding="0" cellspacing="0">
                    <tr align="center">
                        <td align="center" class="Etiqueta" style="width:40px" rowspan="2">
                            Suma :
                        </td>
                        <td align="left" style="width:50px" rowspan="2">
                            <asp:TextBox ID="TextSum" CssClass="txtReadOnly" Enabled="false" runat="server"  Font-Size="9px"  Width="100px" Height="18px"></asp:TextBox>
                        </td>       
                    </tr>
                </table>
        </center>
    </form>
</body>
</html>
