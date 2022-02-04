<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="frmBusquedaOT.aspx.vb" Inherits="intranet_logi.frmBusquedaOT" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">


<html >
<head id="Head1" runat="server">
    <title>Busqueda</title>
    <base target="_self" />
	<meta name="GENERATOR"  content="Microsoft Visual Studio .NET 7.1" />
	<meta name="CODE_LANGUAGE"  content="Visual Basic .NET 7.1" />
	<meta name="vs_defaultClientScript"  content="JavaScript" />
	<meta name="vs_targetSchema"  content="http://schemas.microsoft.com/intellisense/ie5" />
    <link href="../css/NM0001.css" rel="stylesheet" type="text/css" />	
    <script type="text/javascript" language="javascript">
        returnValue = "";
        function btnEscoger_Onclick(Codigo, Nombre) {
            returnValue = Codigo + ":" + Nombre;
            window.close();
        }

        function btnEscogerDesperdicios_Onclick(Codigo, Nombre, Stock) {
            returnValue = Codigo + ":" + Nombre + ":" + Stock;
            window.close();
        }


        function Cerrar() {
            window.close();
        }
	</script>
</head>
<body>
    <form id="Form1" method="post" runat="server">
            <table width="440px" border="0" cellspacing="2" cellpadding="1">
				<tr>
					<td class="Cabecera" style = "text-align:center">
                        <asp:Label ID="lblTitulo" runat="server" Text="Busqueda de Ordenes de Trabajo"></asp:Label>
                    </td>
				</tr>
				<tr>
					<td>
                        <table width="100%" border="0" cellspacing="2" cellpadding="1">
                            <tr>
                                <td width="100px" class="Titulo"><asp:Label ID="lblResponsable" runat="server" Text="Responsable"></asp:Label>
                                </td>
                                <td>
									<asp:DropDownList ID="ddlResponsableF" runat="server" Width="190px" AutoPostBack = "true">
                                                    <asp:ListItem Value="" Text="--------SELECCIONE-------"></asp:ListItem>
                                                    <asp:ListItem Value="01" Text="01 - GERENCIA DE PRODUCCIÓN"></asp:ListItem>
                                                    <asp:ListItem Value="02" Text="02 - CALIDAD"></asp:ListItem>
                                                    <asp:ListItem Value="03" Text="03 - HILANDERÍA"></asp:ListItem>
                                                    <asp:ListItem Value="04" Text="04 - PRETEJIDO"></asp:ListItem>
                                                    <asp:ListItem Value="05" Text="05 - TEJEDURÍA"></asp:ListItem>
                                                    <asp:ListItem Value="07" Text="07 - TINTORERÍA"></asp:ListItem>
                                                    <asp:ListItem Value="08" Text="08 - RRHH Y SSOMA"></asp:ListItem>
                                                    <asp:ListItem Value="10" Text="10 - GERENCIA GENERAL"></asp:ListItem>
                                                    <asp:ListItem Value="11" Text="11 - GERENCIA INGENIERÍA"></asp:ListItem>
                                                    <asp:ListItem Value="12" Text="12 - GERENCIA FINANZAS"></asp:ListItem>
                                                    <asp:ListItem Value="14" Text="14 - LOGÍSTICA"></asp:ListItem>
                                                    <asp:ListItem Value="15" Text="15 - SISTEMAS"></asp:ListItem>
                                                    <asp:ListItem Value="17" Text="17 - COMERCIALIZACIÓN"></asp:ListItem>
                                                    <asp:ListItem Value="18" Text="18 - REVISIÓN FINAL"></asp:ListItem>
                                                    <asp:ListItem Value="20" Text="20 - MANTENIMIENTO MECÁNICO"></asp:ListItem>
                                                    <asp:ListItem Value="21" Text="21 - MANTENIMIENTO ELÉCTRICO"></asp:ListItem>
                                                    <asp:ListItem Value="22" Text="22 - SERVICIOS INDUSTRIALES E INFRAESTRUCTURA"></asp:ListItem>
                                                    <asp:ListItem Value="23" Text="23 - PTAR"></asp:ListItem>
                                        </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                 <td width="100px" class="Titulo"><asp:Label ID="Label4" runat="server" Text="Año"></asp:Label></td>
                                <td>
									<asp:DropDownList ID="ddlAno" runat="server" Width="70px"  AutoPostBack = "true">
                                         <%--<asp:ListItem Value="2020" Text="2020"></asp:ListItem>
                                         <asp:ListItem Value="2019" Text="2019"></asp:ListItem>--%>
                                    </asp:DropDownList>
                                </td>
                            </tr>
							<tr>
								<td width="100px" class="Titulo"><asp:Label ID="lblCodigo" runat="server" Text="Código"></asp:Label></td>
								<td>
									<asp:TextBox id="txtCodigo" runat="server" CssClass="input"></asp:TextBox>
                                </td>
							</tr>
							<tr>
								<td width="100px" class="Titulo"><asp:Label ID="lblDescripcion" runat="server" Text="Descripcion"></asp:Label></td>
								<td>
									<asp:TextBox id="txtDescripcion" runat="server" CssClass="input" Width="300px"></asp:TextBox>
                                </td>
							</tr>
							<tr>
								<td width="100px" class="Titulo"><asp:Label ID="Label3" runat="server" Text="Centro Costos"></asp:Label></td>
								<td>								
                                    <asp:DropDownList ID="ddlCentroCosto" runat="server" Width="300px" CssClass="input" AutoPostBack = "true"></asp:DropDownList>
                                </td>
							</tr>
							<tr>
								<td colspan="2" class="Footer" style="height: 30px">
									<asp:Button id="btnBuscar" runat="server" Text="Buscar" CssClass="boton"></asp:Button>&nbsp;&nbsp;&nbsp;
                                    <input  class="boton" id="btnCerrar" type="button" value="Cerrar" onclick="Cerrar()" name="btnCerrar" />&nbsp;&nbsp;&nbsp;
                                </td>
							</tr>
							<tr>
								<td colspan="2" class="Footer" style="text-align:left" >
									<asp:Label ID="lblMensajeError" runat="server" Text="" CssClass = "Error" ></asp:Label>
                                </td>
							</tr>
						</table>
					</td>
				</tr>
				<tr>
					<td height="300px" valign="top">
                        <%--GRILLA ESTANDAR--%>
                        <asp:Panel ID="pnl_GrillaEstandar" runat="server" Height="300px" ScrollBars="Auto"  Width="440px">                     
                            <asp:DataGrid id="dgGrillaEstandar" runat="server" AutoGenerateColumns="False" 
                                Width="420px"   AllowPaging="True" AllowSorting ="false" PageSize="15">
                                <PagerStyle Mode="NumericPages"  />
                                <FooterStyle CssClass="GridFooter"></FooterStyle>
                                <AlternatingItemStyle CssClass="GridAltItem"></AlternatingItemStyle>
                                <ItemStyle CssClass="GridItem" ></ItemStyle>
                                <HeaderStyle CssClass="GridHeader" HorizontalAlign="Center"></HeaderStyle>
                                <Columns>
                                    <asp:TemplateColumn>
                                        <ItemTemplate>
                                            <input  class="btnDetalle" runat="server" name="btnEscoger" id="btnEscoger" type="button" value="..." />
                                        </ItemTemplate>
                                        <ItemStyle  Width="30px" />
                                    </asp:TemplateColumn>                                    
                                    <asp:TemplateColumn HeaderText="Código">
                                        <ItemTemplate >
                                            <asp:Label ID="Label1" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.CODIGO") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle  Width="100px" HorizontalAlign="Center"/>                                                                                
                                    </asp:TemplateColumn>
                                    <asp:TemplateColumn HeaderText="Descripcion" >
                                        <ItemTemplate>
                                            <asp:Label ID="Label2" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.DESCRIPCION") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateColumn>
                                </Columns>
                            </asp:DataGrid>
                        </asp:Panel>
					</td>
				</tr>
			</table>
    </form>
</body>
</html>

