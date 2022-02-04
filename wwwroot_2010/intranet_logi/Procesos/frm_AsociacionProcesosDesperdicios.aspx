<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="frm_AsociacionProcesosDesperdicios.aspx.vb" Inherits="intranet_logi.frm_AsociacionProcesosDesperdicios" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <link href="../css/NM0001.css" rel="stylesheet" type="text/css" />
    <link href="../css/sytle.css" rel="stylesheet" type="text/css" />
        <script type="text/javascript" language="javascript">
            function fnc_BuscarDatos(strTipoBusqueda) {//begin fnc_BuscarDatos()

                var retorno = window.showModalDialog("../Buscadores/frmBusqueda.aspx?TipoBusqueda=" + strTipoBusqueda, "", "dialogHeight:420px;dialogWidth:450px;center:yes;help:no;");
                if (retorno != "" && retorno != ":") {
                    var datos = retorno.split(":");
                    var codigo = datos[0];
                    var nombre = datos[1];

                    switch (strTipoBusqueda) {
                        case "ArticuloDesperdicios":
                            document.getElementById('txtCodigoArticulo').value = codigo;
                            document.getElementById('txtDescripcionArticulo').value = nombre;
                            break;
                        case "CentroCostosTejeduria":
                            document.getElementById('txtCentroCostoTejeduria').value = codigo;
                            document.getElementById('txtDescripcionCentroCosto').value = nombre;
                            break;
                        case "ProcesoOrigen":
                            document.getElementById('txtProcesoOrigen').value = codigo;
                            document.getElementById('txtDescripcionProceso').value = nombre;
                            break;
                    }
                } //end if
            } //end fnc_BuscarDatos()      


            function fMostrarReporte(strURL) {
                var intWidth = screen.width;
                var intHeight = screen.height;
                window.open(strURL, "", "top=0; left=0; width=" + intWidth + "; height=" + intHeight + "; resizable=1;");
            }  // end fMostrarReporte(strURL)


            function fnc_ValidaFormulario() {

                var txtCodigoArticulo = document.getElementById('txtCodigoArticulo');
                if (txtCodigoArticulo.value == "") {
                    alert("Primero debe seleccionar un Articulo");
                    txtCodigoArticulo.focus();
                    return false;
                }

                var txtCentroCostoTejeduria = document.getElementById('txtCentroCostoTejeduria');
                if (txtCentroCostoTejeduria.value == "") {
                    alert("Primero debe seleccionar un Centro de Costo");
                    txtCentroCostoTejeduria.focus();
                    return false;
                }

                var txtProcesoOrigen = document.getElementById('txtProcesoOrigen');
                if (txtProcesoOrigen.value == "") {
                    alert("Primero debe seleccionar un Proceso Origen");
                    txtProcesoOrigen.focus();
                    return false;
                }

                return confirm("¿Esta seguro que desea grabar la nueva asociación?.");

            }

            function ConfirmaEliminacion() {
                return confirm("¿Esta seguro que desea eliminar esta asociación?.");
            }

        </script>
</head>
<body>
    <form id="form1" runat="server">
    <center>
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        <!-- **************  Inicio de Contenido ****************** -->            
        <asp:UpdatePanel ID="panContenido" runat="server">
            <ContentTemplate>
        <asp:Panel ID="pnlListadoProcesos" runat="server" >
            <%--Cabecera--%>
            <table style="width:100%;" border="0" cellpadding="0" cellspacing="0" >
                <tr>
                    <td class="Cabecera" style="width:100%; height:30px; vertical-align:middle; text-align:center" >
                        &nbsp;Maestro de Procesos que generan de Desperdicios</td>
                </tr>
            </table>

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
                    <table cellspacing="0" cellpadding="0" border="0" width="900px">
                        <tr>
                            <td align="left" style="width: 450px; height: 30px;">
                                &nbsp;</td>                           
                            <td align="center">&nbsp;</td>
                        </tr>
                        <tr>
                            <td align="left" style="width: 450px; height: 30px;">
                                <asp:Button ID="btnNuevoProcesoD" runat="server" CssClass="btnAzul" 
                                    Text="Nuevo Proceso" width="140px" />
                            </td>
                            <td align="center">
                                &nbsp;</td>
                        </tr>
                        <tr>
                            <td align="left" colspan="2">
                                <asp:Label ID="lblMsg" runat="server" CssClass="error"></asp:Label>
                            </td>
                        </tr>
                    </table>                                
                    <asp:GridView ID="grvAsociacionProcesos" runat="server" Width="900px" AutoGenerateColumns="False"                        
                        DataKeyNames="CODIGO_ARTICULO, CODIGO_CENTROCOSTO, CODIGO_PROCESOORIGEN" ShowHeaderWhenEmpty="True">
                        <Columns>
                            <asp:TemplateField HeaderText="Cod. Art.">
                                <ItemTemplate>
                                    <asp:Label ID="lblCodigoArt" runat="server" Text='<%# Bind("CODIGO_ARTICULO") %>'></asp:Label>
                                </ItemTemplate>                                
                                <ItemStyle HorizontalAlign="Center" Width="80px" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Artículo">
                                <ItemTemplate>
                                    <asp:Label ID="lblDescripcionArt" runat="server" Text='<%# Bind("DESCRI_ARTICULO") %>'></asp:Label>
                                </ItemTemplate>  
                                <ItemStyle HorizontalAlign="Left" />                           
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Cod. CC">
                                <ItemTemplate>
                                    <asp:Label ID="lblCodigoCC" runat="server" Text='<%# Bind("CODIGO_CENTROCOSTO") %>'></asp:Label>
                                </ItemTemplate>                                
                                <ItemStyle HorizontalAlign="Center" Width="70px" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Centro de Costo">
                                <ItemTemplate>
                                    <asp:Label ID="lblDescripcionCC" runat="server" Text='<%# Bind("DESCRI_CENTROCOSTO") %>'></asp:Label>
                                </ItemTemplate>                                
                                <ItemStyle HorizontalAlign="Center" Width="200px" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Cod. Proc.">
                                <ItemTemplate>
                                    <asp:Label ID="lblCodigoProc" runat="server" Text='<%# Bind("CODIGO_PROCESOORIGEN") %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" Width="60px" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Proceso Origen">
                                <ItemTemplate>
                                    <asp:Label ID="lblDescripcionProc" runat="server" Text='<%# Bind("DESCRI_PROCESOORIGEN") %>'></asp:Label>
                                </ItemTemplate> 
                                <ItemStyle HorizontalAlign="Left" Width="200px" />                          
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="">
                                <ItemTemplate>
                                    <asp:ImageButton ID="btnModificar" runat="server" CommandName="SELECCIONAR" Height="16px" Width="16px"
                                                     ImageUrl="~/images/evaluar.png" ToolTip="Modificar Asociación" />
                                </ItemTemplate> 
                                <ItemStyle HorizontalAlign="Center" Width="25px" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="">
                                <ItemTemplate>
                                    <asp:ImageButton ID="btnEliminar" runat="server" CommandName="ELIMINAR" Height="16px" Width="16px"
                                         ImageUrl="~/images/trash_16.gif" ToolTip="Eliminar Asociación" OnClientClick='if (! ConfirmaEliminacion()) return false;' />
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" Width="25px" />
                            </asp:TemplateField>
                        </Columns>
                            <EmptyDataTemplate>
                                <table width="100%">
                                    <tr class ="gridheader">
                                        <td>&nbsp;</td>
                                    </tr>
                                    <tr class="GridItem">
                                        <td style="text-align:center;font-weight:bold;font-size:12px">No se encontro ningún registro en la consulta.</td>
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
        <asp:UpdatePanel ID="panRegistro" runat="server" UpdateMode="Always">
            <ContentTemplate>
                <asp:Panel ID="pnlRegistroProceso" runat="server" >
                <%--Cabecera--%>
                <table style="width:900px;" border="0" cellpadding="0" cellspacing="0" >
                    <tr>
                        <td class="Cabecera" style="width:900px; height:30px; vertical-align:middle; text-align:center" >
                            <asp:Label ID="lblTituloDetalle" runat="server" Text="" CssClass="Cabecera"></asp:Label>
                            &nbsp;de Procesos de Desperdicios</td>
                    </tr>
                </table>
                    <%--imagen de espera--%>
                    <asp:UpdateProgress ID="UpdateProgress1" DisplayAfter="200" runat="server" >   
                        <ProgressTemplate>
                                <div id="divLoad" class="CssLoadNormal" 
                                    style="width:900px; height:40px; vertical-align: middle; text-align: center;"><br />
                                    <img src="../images/Loading.gif" style="vertical-align:middle; height:20px; width:20px" alt="" />Procesando...
                                </div>                      
                        </ProgressTemplate> 
                    </asp:UpdateProgress>
                    <table  border="0" cellpadding="0" cellspacing="2" style="width:900px;">
                        <tr>
                            <td style="width:30px;height:15px;"></td>
                            <td style="width:120px;height:15px;"></td>
                            <td style="width:170px;height:15px;"></td>
                            <td style="width:560px;height:15px;"></td>
                            <td style="width:20px;height:15px;"></td>
                        </tr>
                        <tr>
                            <td style="width:30px;height:40px;"></td>
                            <td style="width:120px;height:40px;" class="Etiqueta">&nbsp;&nbsp;Código Articulo:</td>
                            <td style="width:170px;height:40px;">
                                <asp:TextBox ID="txtCodigoArticulo" runat="server" Width="100px" 
                                    cssclass="input" AutoPostBack="false" Height="20px"></asp:TextBox>&nbsp;&nbsp;
                                <input class="btnAzul" id="btnBuscarCodigoArticulo" style="width:25px; height:25px" onclick="javascript:fnc_BuscarDatos('ArticuloDesperdicios');" type="button" value="..." />
                            </td>
                            <td style="width:560px;height:40px;" align="left">
                                <asp:TextBox ID="txtDescripcionArticulo" runat="server" 
                                    CssClass="inputDisabled2" Height="20px" MaxLength="10" Width="500px"></asp:TextBox>
                            </td>
                            <td style="width:20px;height:40px;"></td>
                        </tr>
                        <tr>
                            <td style="width:30px;height:40px;"></td>
                            <td style="width:120px;height:40px;" class="Etiqueta">&nbsp;&nbsp;Centro de Costo:</td>
                            <td style="width:170px;height:40px;">
                                <asp:TextBox ID="txtCentroCostoTejeduria" runat="server" Width="100px" 
                                    cssclass="input" AutoPostBack="false" Height="20px"></asp:TextBox>&nbsp;&nbsp;
                                <input class="btnAzul" id="btnBuscarCentroCosto" style="width:25px; height:25px" onclick="javascript:fnc_BuscarDatos('CentroCostosTejeduria');" type="button" value="..." />
                          
                            </td>
                            <td style="width:560px;height:40px;" align="left">
                                <asp:TextBox ID="txtDescripcionCentroCosto" runat="server" 
                                    CssClass="inputDisabled2" Height="20px" MaxLength="10" Width="500px"></asp:TextBox>
                            </td>
                            <td style="width:20px;height:40px;"></td>
                        </tr>
                        <tr>
                            <td style="width:30px;height:40px;"></td>
                            <td style="width:120px;height:40px;" class="Etiqueta">&nbsp;&nbsp;Proceso Origen:</td>
                            <td style="width:170px;height:40px;">
                                <asp:TextBox ID="txtProcesoOrigen" runat="server" Width="100px" 
                                cssclass="input" AutoPostBack="false" Height="20px"></asp:TextBox>&nbsp;&nbsp;
                                <input class="btnAzul" id="btnBuscarCodigoProceso" style="WIDTH:25px; HEIGHT:25px" onclick="javascript:fnc_BuscarDatos('ProcesoOrigen');" type="button" value="..." name="Button1" />                            
                            </td>
                            <td style="width:560px;height:40px;" align="left">
                                <asp:TextBox ID="txtDescripcionProceso" runat="server" 
                                    CssClass="inputDisabled2" Height="20px" MaxLength="10" Width="500px"></asp:TextBox>
                            </td>
                            <td style="width:20px;height:40px;"></td>
                        </tr>
                        <tr>
                            <td style="width:30px;height:15px;"></td>
                            <td style="width:120px;height:15px;"></td>
                            <td style="width:170px;height:15px;"></td>
                            <td style="width:560px;height:15px;"></td>
                            <td style="width:20px;height:15px;"></td>
                        </tr>
                        <tr>
                            <td style="width:30px;height:15px;"></td>
                            <td style="width:120px;height:15px;"></td>
                            <td style="height:15px;" colspan="2">
                                <table cellpadding="0" cellspacing="0" width="100%">
                                    <tr>
                                        <td align="left">
                                            <asp:Button ID="btnRegresar" runat="server" CssClass="btnAzul" Text="Regresar" />
                                        </td>
                                        <td align="left">
                                            <asp:Button ID="btnGrabar" runat="server" Text="Grabar"  CssClass="btnAzul" />    
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td style="width:20px;height:15px;"></td>
                        </tr>
                        <tr>
                            <td style="width:30px;height:15px;">&nbsp;</td>
                            <td style="width:120px;height:15px;">&nbsp;</td>
                            <td colspan="2" style="height:15px;">&nbsp;</td>
                            <td style="width:20px;height:15px;">&nbsp;</td>
                        </tr>
                        <tr>
                            <td style="width:30px;height:15px;">
                                &nbsp;</td>
                            <td align="left" colspan="3" style="height:15px;">
                                <asp:Label ID="lblMensajeIngreso" runat="server" CssClass="error"></asp:Label>
                            </td>
                            <td style="width:20px;height:15px;">
                                &nbsp;</td>
                        </tr>
                        
                    </table>
                    <asp:HiddenField ID="hdnCodigoArticuloORI" runat="server" />
                    <asp:HiddenField ID="hdnCentroCostoTejeduriaORI" runat="server" />
                    <asp:HiddenField ID="hdnProcesoOrigenORI" runat="server" />
                    <asp:HiddenField ID="hdnOpcion" runat="server" />
                </asp:Panel> 
            </ContentTemplate>
         </asp:UpdatePanel>
    </center>
    </form>
</body>
</html>
