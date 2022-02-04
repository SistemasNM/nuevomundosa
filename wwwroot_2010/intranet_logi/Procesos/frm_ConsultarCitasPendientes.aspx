<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="frm_ConsultarCitasPendientes.aspx.vb" Inherits="intranet_logi.frm_ConsultarCitasPendientes" %>
<%@ Register Assembly="Infragistics35.Web.v10.1, Version=10.1.20101.1011, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" Namespace="Infragistics.Web.UI.EditorControls" TagPrefix="ig" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../css/NM0001.css" rel="stylesheet" type="text/css" />
    <link href="../css/Styles_Controles.css" rel="stylesheet" type="text/css" />
    <link href="../css/Styles_Paginas.css" rel="stylesheet" type="text/css" />
    <script language = "javascript" type ="text/javascript">
        function wdpFecIni_onchange() {
            var btnBuscar = document.getElementById("btnBuscar");
            btnBuscar.click();

        }

        function ConfirmarAtencionCita(ctrl, tipo) {
            if (tipo == 1) {
                if (ctrl.CodigoCita == undefined) {
                    return confirm("¿Esta seguro que desea ATENDER la cita.?.");
                }
                else {
                    return confirm("¿Esta seguro que desea ATENDER la cita Nro.: " + ctrl.CodigoCita + "?.");
                }
            }

            if (tipo == 2) {
                var lblNumCita = document.getElementById("lblNumCita");
                return confirm("¿Esta seguro que desea ATENDER la cita Nro.: " + lblNumCita.innerHTML + "?.");
            }

            
        }

        function ConfirmarAnularCita(ctrl, tipo) {

            if (tipo == 2) {
                var lblNumCita = document.getElementById("lblNumCita");
                return confirm("¿Esta seguro que desea ANULAR la cita Nro.: " + lblNumCita.innerHTML + "?.");
            }

        }
    </script>
    </head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        <center>
            <!-- **************  Inicio de Contenido ****************** -->            
            <asp:UpdatePanel ID="panContenido" runat="server">
                <ContentTemplate> 
            <asp:Panel ID="pnlListadoCitasPend" runat="server" HorizontalAlign="Center">
                <%--Cabecera--%>
                <table style="width:100%;" border="0" cellpadding="0" cellspacing="0" >
                    <tr>
                        <td class="Cabecera" style="width:100%; height:30px; vertical-align:middle; text-align:center" >
                            &nbsp;LISTADO DE CITAS - PROVEEDOR</td>
                    </tr>
                </table>
                <%--imagen de espera--%>
                <asp:UpdateProgress ID="up_Load" DisplayAfter="300" runat="server" >   
                    <ProgressTemplate>
                        <div id="divLoad" class="CssLoadNormal" 
                            style="width:900px; height:40px; vertical-align: middle;"><br />
                            <img src="../images/Loading.gif" style="vertical-align:middle; height:20px; width:20px" alt="" />Procesando...
                        </div>                        
                    </ProgressTemplate> 
                </asp:UpdateProgress>
                <table style="width:900px" border="0" cellpadding="0" cellspacing="0">
                    <tr style="height: 10px">
                        <td style="width:30px" align="left">&nbsp;</td>
                        <td style="width:100px" align="left">&nbsp;</td>
                        <td style="width:150px" align="left">&nbsp;</td>
                        <td style="width:650px" align="center">&nbsp;</td>
                    </tr>
                        <tr style="height: 20px">
                            <td style="width:30px;height:30px;">&nbsp;</td>
                            <td style="width:100px;height:30px; text-align: left;" class="Etiqueta">&nbsp;&nbsp;Fecha Cita:</td>
                            <td align="center" style="width:150px">
                                <ig:WebDatePicker ID="wdpFecIni" runat="server" >
                                </ig:WebDatePicker>
                            </td>
                            <td style="width:650px;height:30px; text-align: left;" colspan="3">&nbsp;&nbsp;&nbsp;&nbsp;
                                <asp:Button ID="btnBuscar" runat="server" CssClass="Boton" Text="Buscar" />
                            </td>
                        </tr>
                    <tr style="height: 15px">
                        <td align="left" colspan="4">
                            <asp:Label ID="lblMsg" runat="server" CssClass="error"></asp:Label>
                        </td>
                    </tr>
                    <tr style="height: 15px">
                        <td align="right" colspan="4">Total: 
                            <asp:Label ID="lblContador" runat="server" ></asp:Label>
                        </td>
                    </tr>
                </table>
                    <asp:GridView ID="grvCitasLogistica" runat="server" 
                        AutoGenerateColumns="False" DataKeyNames="int_IDCita" Width="900px">
                        <Columns>
                            <asp:TemplateField ShowHeader="False">
                                <ItemTemplate>
                                    <asp:Button ID="btnAtender" runat="server" CausesValidation="false" CodigoCita='<%# Eval("vch_CodigoCita") %>'
                                        CommandName="Atender" Text="Atender" OnClientClick = "if (! ConfirmarAtencionCita(this,1)) return false;" CssClass="Boton" 
                                        CommandArgument='<%# Eval("vch_CodigoCita") %>'/>
                                    <asp:Label ID="lblEstado" runat="server" Text='<%# Eval("vch_Estado") %>' Visible = "false" CssClass = "error"></asp:Label>
                                </ItemTemplate>
                                <ControlStyle  Width="70px" />
                                <ItemStyle Width="80px" />
                            </asp:TemplateField>
                            <asp:BoundField DataField="vch_HoraCita" HeaderText="Hora">
                            <ItemStyle Width="85px" Height= "30px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="vch_CodigoCita" HeaderText="Cod. Cita">
                            <ItemStyle Width="65px" Height= "30px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="vch_CodProveedor" HeaderText="Cod. Prov.">
                            <ItemStyle Width="100px" Height= "30px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="vch_NombreProveedor" HeaderText="Proveedor">
                            <ItemStyle Width="400px" Height= "30px"  HorizontalAlign = "Left" />
                            </asp:BoundField>
                            <asp:BoundField DataField="int_TotalItem" HeaderText="Tot. Item">
                            <ItemStyle Width="60px" Height= "30px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="int_CantidadEntrega" HeaderText="Tot. Cant" DataFormatString="{0:N2}">
                            <ItemStyle Width="70px" Height= "30px" />
                            </asp:BoundField>
                            <asp:TemplateField HeaderText=".">
                                <ItemTemplate>
                                    <asp:ImageButton ID="btnSelect" runat="server" CommandName="Select" 
                                        Height="20px" ImageUrl="~/images/buscar.gif" ToolTip="Seleccionar Orden" 
                                        Width="20px" />
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" Width="40px" />
                            </asp:TemplateField>
                        </Columns>
                        <EmptyDataTemplate>
                            <table width="900px">
                                <tr class="GridHeader3">
                                    <td>&nbsp;</td>
                                </tr>
                                <tr class="Grilla">
                                    <td style="text-align:center;font-weight:bold;font-size:12px">No se encontro ningúna Cita Pendiente.</td>
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
        <asp:UpdatePanel ID="panBusqueda" runat="server" UpdateMode="Always">
            <ContentTemplate>
                <asp:Panel ID="pnlDetalleCita" runat="server" >
                <%--Cabecera--%>
                <table style="width:900px;" border="0" cellpadding="0" cellspacing="0" >
                    <tr>
                        <td class="Cabecera" style="width:900px; height:30px; vertical-align:middle; text-align:center" >
                            &nbsp;DETALLE DE CITA DE ENTREGA</td>
                    </tr>
                </table>
                    <%--imagen de espera--%>
                    <asp:UpdateProgress ID="UpdateProgress1" DisplayAfter="200" runat="server" >   
                        <ProgressTemplate>
                            <center>
                                <div id="divLoad" class="CssLoadNormal" style="width:900px; height:30px">
                                    <img src="../images/Loading.gif" style="vertical-align:middle; height:20px; width:20px" alt="" />Procesando...
                                </div>  
                            </center>                      
                        </ProgressTemplate> 
                    </asp:UpdateProgress>
                    <table  border="0" cellpadding="0" cellspacing="0" style="width:900px;">
                        <tr>
                            <td style="width:30px;height:15px;"></td>
                            <td style="width:150px;height:15px;"></td>
                            <td style="width:700px;height:15px;" colspan="3"></td>
                            <td style="width:20px;height:15px;"></td>
                        </tr>
                        <tr>
                            <td style="width:30px;height:30px;">&nbsp;</td>
                            <td style="width:150px;height:30px; text-align: left;" class="Etiqueta">&nbsp;&nbsp;# Cita</td>
                            <td style="width:200px;height:30px; text-align: left;">&nbsp;
                                <asp:Label ID="lblNumCita" runat="server" CssClass="txt2" Width="100px"></asp:Label>
                            </td>
                            <td style="width:150px;height:30px; text-align: left;" class="Etiqueta">&nbsp;&nbsp;Estado 
                                Cita:</td>
                            <td style="width:350px;height:30px; text-align: left;">&nbsp;
                                <asp:Label ID="lblEstadoCita" runat="server" CssClass="txt2" Width="120px"></asp:Label>
                            </td>
                            <td style="width:20px;height:30px;">&nbsp;</td>
                        </tr>
                        <tr>
                            <td style="width:30px;height:30px;">&nbsp;</td>
                            <td style="width:150px;height:30px; text-align: left;" class="Etiqueta">&nbsp;&nbsp;Fecha Entrega:</td>
                            <td style="width:200px;height:30px; text-align: left;">&nbsp;
                                <asp:Label ID="lblFechaEnt" runat="server" CssClass="txt2" Width="100px"></asp:Label>
                            </td>
                            <td style="width:150px;height:30px; text-align: left;" class="Etiqueta">&nbsp;&nbsp;Hora Entrega:</td>
                            <td style="width:350px;height:30px; text-align: left;">&nbsp;
                                <asp:Label ID="lblHoraEnt" runat="server" CssClass="txt2" Width="50px"></asp:Label>
                            </td>
                            <td style="width:20px;height:30px;">&nbsp;</td>
                        </tr>
                        <tr>
                            <td style="width:30px;height:25px;">&nbsp;</td>
                            <td style="width:150px;height:25px; text-align: left;" class="Etiqueta">&nbsp;&nbsp;Proveedor:</td>
                            <td style="width:700px;height:25px; text-align: left;" colspan="3">&nbsp;
                                <asp:Label ID="lblProveedor" runat="server" CssClass="txt2" Width="600px"></asp:Label>
                            </td>
                            <td style="width:20px;height:25px;">&nbsp;</td>
                        </tr>
                        <tr>
                            <td style="width:30px;height:15px;"></td>
                            <td style="width:150px;height:15px;"></td>
                            <td style="width:700px;height:15px;" colspan="3"></td>
                            <td style="width:20px;height:15px;"></td>
                        </tr>
                        <tr>
                            <td style="width:30px;height:25px;">
                                &nbsp;</td>
                            <td style="width:150px;height:25px; text-align: left;">
                                
                            </td>
                            <td style="width:700px;height:25px;" colspan="3">
                                
                            </td>
                            <td style="width:20px;height:25px;">
                                &nbsp;</td>
                        </tr>
                        <tr>
                            <td style="width:30px;height:30px;">&nbsp;</td>
                            <td style="width:150px;height:30px; text-align: left;">
                                <asp:Button ID="btnRegresar" runat="server" Text="Regresar"  CssClass="Boton" />
                            </td>
                            <td style="width:200px;height:30px; text-align: right;">
                                <asp:Button ID="btnAnular" runat="server" Text="Anular"  CssClass="BotonRojo"  
                                    OnClientClick = "if (! ConfirmarAnularCita(this,2)) return false;"/>
                            </td>
                            <td style="width:150px;height:30px; text-align: left;">&nbsp;</td>
                            <td style="width:350px;height:30px; text-align: left;">
                                <asp:Button ID="btnAtender2" runat="server" Text="Atender"  CssClass="Boton"  OnClientClick = "if (! ConfirmarAtencionCita(this,2)) return false;"/>
                            </td>
                            <td style="width:20px;height:30px;">&nbsp;</td>
                        </tr>
                        <tr>
                            <td style="width:30px;height:15px;"></td>
                            <td style="width:150px;height:15px;"></td>
                            <td style="width:700px;height:15px;" colspan="3"></td>
                            <td style="width:20px;height:15px;"></td>
                        </tr>
                        <tr>
                            <td style="width:900px;" colspan="6">
                                <asp:GridView ID="grvDetalleCita" runat="server" 
                                    AutoGenerateColumns="False" DataKeyNames="CODIGO_ITEM" 
                                    ShowHeaderWhenEmpty="True" Width="900px">
                                    <Columns>
                                        <asp:BoundField DataField="TIPO" HeaderText="Tipo">
                                        <ItemStyle Height="30px" Width="30px" />
                                        <HeaderStyle Height="30px" Width="30px" HorizontalAlign="Center"/>
                                        </asp:BoundField>
                                        <asp:BoundField DataField="ORDEN_COMPRA" HeaderText="Número">
                                        <ItemStyle Height="30px" Width="110px" HorizontalAlign="Center"/>
                                        <HeaderStyle Height="30px" Width="110px" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="CODIGO_ITEM" HeaderText="Codigo">
                                        <ItemStyle Height="30px" Width="100px" />
                                        <HeaderStyle Height="30px" Width="100px" HorizontalAlign="Center" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="DESCRIPCION" HeaderText="Descripción" 
                                            ItemStyle-HorizontalAlign="Left">
                                        <ItemStyle Height="30px" Width="300px" />
                                        <HeaderStyle Height="30px" Width="300px" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="PRECIO_UNITARIO" DataFormatString="{0:N2}" 
                                            HeaderText="Costo Unitario" ItemStyle-HorizontalAlign="Right">
                                        <ItemStyle Height="30px" Width="60px" />
                                        <HeaderStyle Height="30px" Width="60px" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="CANT_SOLICITADA" DataFormatString="{0:N2}" 
                                            HeaderText="Cantidad Solicitada" ItemStyle-HorizontalAlign="Right">
                                        <ItemStyle Height="30px" Width="60px" />
                                        <HeaderStyle Height="30px" Width="60px" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="CANT_PENDIENTE" DataFormatString="{0:N2}" 
                                            HeaderText="Cantidad Pendiente" ItemStyle-HorizontalAlign="Right">
                                        <ItemStyle Height="30px" Width="60px" />
                                        <HeaderStyle Height="30px" Width="60px" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="CONSUMO6M" DataFormatString="{0:N0}" 
                                            HeaderText="Consumo Promedio 6Meses" ItemStyle-HorizontalAlign="Right">
                                        <ItemStyle Height="30px" Width="60px" />
                                        <HeaderStyle Height="30px" Width="60px" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="STOCK" DataFormatString="{0:N2}" 
                                            HeaderText="Stock NM" ItemStyle-HorizontalAlign="Right">
                                        <ItemStyle Height="30px" Width="60px" />
                                        <HeaderStyle Height="30px" Width="60px" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="CANT_ENTREGAR" DataFormatString="{0:N2}" 
                                            HeaderText="Cant. por Entregar" ItemStyle-HorizontalAlign="Right">
                                        <ItemStyle Height="30px" Width="60px" />
                                        <HeaderStyle Height="30px" Width="60px" />
                                        </asp:BoundField>
                                    </Columns>
                                    <EmptyDataTemplate>
                                        <table width="100%">
                                            <tr class="GridHeader3">
                                                <td>
                                                    &nbsp;</td>
                                            </tr>
                                            <tr class="Grilla">
                                                <td style="text-align:center;font-weight:bold;font-size:12px">
                                                    No se encontro ningun Item para la Orden de Compra.</td>
                                            </tr>
                                        </table>
                                    </EmptyDataTemplate>
                                    <HeaderStyle CssClass="gridheader" />
                                    <RowStyle CssClass="GridItem" />
                                    <AlternatingRowStyle CssClass="GridAlternateItem" />
                                </asp:GridView>
                            </td>
                        </tr>
                    </table>
                </asp:Panel> 
            </ContentTemplate>
         </asp:UpdatePanel>
        </center>
    </form>
</body>
</html>
