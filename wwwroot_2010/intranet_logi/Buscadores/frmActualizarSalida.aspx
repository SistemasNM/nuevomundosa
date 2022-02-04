<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="frmActualizarSalida.aspx.vb" Inherits="intranet_logi.frmActualizarSalida" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <base target="_self" />
    <link href="../css/Styles_Paginas.css" type="text/css" rel="Stylesheet" />
    <link href="../css/Styles_Controles.css" type="text/css" rel="Stylesheet" />
    <link href="../css/NM0001.css" rel="Stylesheet" type="text/css"/>
    <script type="text/javascript" language="javascript">
                    function btnCerrar_Onclick() {
                        returnValue = "";
		                window.close();
		            }
		            function fnc_Validar() {
		                if (document.getElementById("dwlpiso").value == "") {
		                    alert("Debe elegir en que piso finalizo el despacho");
		                    return false;
		                } else {
		                    window.close();
                            return true;
		                }
		            }
      </script>

</head>
<body>
    <form id="form1" runat="server">
        <center>
            <asp:Panel ID="pnlcabecera" runat="server" HorizontalAlign="Center">
                <table style="width:800px;" border="0" cellpadding="0" cellspacing="0" >
                        <tr>
                            <td class="Cabecera" style="width:800px; height:30px" align="center" valign="middle">&nbsp;ACTUALIZAR ESTADO DE SALIDA</td>
                        </tr>
                        <tr>
                            <td>&nbsp;</td>
                        </tr>
                    </table>
            </asp:Panel>

            <table  style="width:400px" border="0" cellpadding="0" cellspacing="0">

                 <tr style="height: 25px">
                    <td style="width:150px" class="Etiqueta" align="left">&nbsp;#SALIDA:</td>
                    <td style="width:100px" class="Etiqueta" align="center">
                        <asp:TextBox runat="server" ID="txtSalida" CssClass="inputDisabled" Width="70px"></asp:TextBox>
                    </td>
                    <td class="Etiqueta"  align="right" colspan="4">&nbsp;</td>
                </tr>

                <tr><td><asp:Label CssClass="error" ID="msgerror" runat="server"></asp:Label></td></tr>

                <%--<tr><td class="Etiqueta"></td><td class="Etiqueta"></td><td class="Etiqueta"></td></tr>--%>

                <tr style="height: 25px">
                    <td style="width:150px;border-style:groove" class="Etiqueta" align="left">&nbsp;PROGRAMACIÓN:</td>
                    <td style="width:150px;border-style:groove" class="Etiqueta" align="center">
                        <asp:Panel ID="pnActPro" Width="150px" runat="server">
                           <asp:Button runat="server" ID="btnPrograma" CssClass="Botonera" Text="Actualizar"/>   
                         </asp:Panel>                    
                        <asp:Panel ID="pnFechaPro" Width="150px" runat="server">
                             <asp:Label ID="lblFechaPro" runat="server"></asp:Label>
                         </asp:Panel>
                         </td>
                    <td style="width:150px;border-style:groove" class="Etiqueta" align="center">
                       <asp:Image runat="server" ID="imgProgram" ImageUrl="~/Imagenes/aceptar.PNG" Height="20px"/>
                    </td>
                </tr>

                 <tr><td class="Etiqueta"></td><td class="Etiqueta"></td><td class="Etiqueta"></td></tr>

                <tr style="height: 25px">
                    <td style="width:150px;border-style:groove" class="Etiqueta" align="left">&nbsp;RUTA:</td>
                    <td style="width:150px;border-style:groove" class="Etiqueta" align="center">
                        <asp:Panel ID="pnActRuta" Width="150px" runat="server">
                            <asp:Button runat="server" ID="btnRuta" CssClass="Botonera" Text="Actualizar"/>
                        </asp:Panel>
                        <asp:Panel ID="pnFechaRut" Width="150px" runat="server">
                            <asp:Label ID="lblFechaRut" runat="server"></asp:Label>
                         </asp:Panel>
                    </td>
                    <td style="width:150px;border-style:groove" class="Etiqueta" align="center">
                       <asp:Image runat="server" ID="imgRuta" ImageUrl="~/Imagenes/aceptar.PNG" Height="20px"/>
                    </td>
                </tr>

                 <tr><td class="Etiqueta"></td><td class="Etiqueta"></td><td class="Etiqueta"></td></tr>

                <tr style="height: 25px">
                    <td style="width:150px;border-style:groove" class="Etiqueta" align="left">&nbsp;EN PUNTO:</td>
                    <td style="width:150px;border-style:groove" class="Etiqueta" align="center">
                         <asp:Panel ID="pnActPun" Width="150px" runat="server">
                            <asp:Button runat="server" ID="btnPunto" CssClass="Botonera" Text="Actualizar"/>
                        </asp:Panel>                    
                        <asp:Panel ID="pnFechaPun" Width="150px" runat="server">
                            <asp:Label ID="lblFechaPun" runat="server"></asp:Label>
                         </asp:Panel>
                    </td>
                    <td style="width:150px;border-style:groove" class="Etiqueta" align="center">
                       <asp:Image runat="server" ID="imgPunto" ImageUrl="~/Imagenes/aceptar.PNG" Height="20px"/>
                    </td>
                </tr>

                 <tr><td class="Etiqueta"></td><td class="Etiqueta"></td><td class="Etiqueta"></td></tr>

                <tr style="height: 25px">
                    <td style="width:150px;border-style:groove" class="Etiqueta" align="left">&nbsp;ATENDIENDO:</td>
                    <td style="width:150px;border-style:groove" class="Etiqueta" align="center">
                        <asp:Panel ID="pnActAten" Width="150px" runat="server">
                             <asp:Button runat="server" ID="btnAtender" CssClass="Botonera" Text="Actualizar"/>
                         </asp:Panel>                    
                        <asp:Panel ID="pnFechaAten" Width="150px" runat="server">
                            <asp:Label ID="lblFechaAten" runat="server"></asp:Label>
                         </asp:Panel>
                    </td>
                    <td style="width:150px;border-style:groove" class="Etiqueta" align="center">
                       <asp:Image runat="server" ID="imgAtendido" ImageUrl="~/Imagenes/aceptar.PNG" Height="20px"/>
                    </td>
                </tr>

                <tr><td class="Etiqueta"></td><td class="Etiqueta"></td><td class="Etiqueta"></td></tr>

                <tr style="height: 25px">
                    <td style="width:150px;border-style:groove" class="Etiqueta" align="left">&nbsp;FINALIZAR:</td>
                    <td style="width:150px;border-style:groove" class="Etiqueta" align="center">
                        <asp:Panel ID="pnActFin" Width="150px" runat="server">
                            <asp:Button runat="server" ID="btnFinal" CssClass="Botonera" Text="Actualizar"/>
                         </asp:Panel>                    
                        <asp:Panel ID="pnFechaFin" Width="150px" runat="server">
                            <asp:Label ID="lblFechaFin" runat="server"></asp:Label>
                         </asp:Panel>
                    </td>
                    <td style="width:150px;border-style:groove" class="Etiqueta" align="center">
                       <asp:Image runat="server" ID="imgFinal" ImageUrl="~/Imagenes/aceptar.PNG" Height="20px"/>
                    </td>
                </tr>

            </table>
           
            <asp:Panel runat="server" ID="Piso" Width="400px">
                <table>
                    <tr><td></td></tr>
                    <tr style="height: 25px">
                        <td style="width:150px" class="Etiqueta" align="left">&nbsp;PISO:</td>
                        <td style="width:150px" class="Etiqueta" align="center">
                            <asp:DropDownList ID="dwlpiso" runat="server">
                                 <asp:ListItem Value="">--Seleccionar--</asp:ListItem>
                                 <asp:ListItem Value="1">1er Piso</asp:ListItem>
                                 <asp:ListItem Value="2">2do Piso</asp:ListItem>
                                 <asp:ListItem Value="3">3er Piso</asp:ListItem>
                                 <asp:ListItem Value="4">4to Piso</asp:ListItem>
                                 <asp:ListItem Value="5">5to Piso</asp:ListItem>
                                 <asp:ListItem Value="6">6to Piso</asp:ListItem>
                                 <asp:ListItem Value="7">7mo Piso</asp:ListItem>
                                 <asp:ListItem Value="8">8vo Piso</asp:ListItem>
                                 <asp:ListItem Value="9">9no Piso</asp:ListItem>
                                 <asp:ListItem Value="10">10mo Piso</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                        <td style="width:150px" class="Etiqueta" align="center">
                           <asp:Button id="btnAceptar"  runat="server" Text="Aceptar"/>
                        </td>
                    </tr>
                </table>
            </asp:Panel>
        </center>
    </form>
</body>
</html>
