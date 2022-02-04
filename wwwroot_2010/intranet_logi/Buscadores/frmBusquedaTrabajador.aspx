﻿<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="frmBusquedaTrabajador.aspx.vb" Inherits="intranet_logi.frmBusquedaTrabajador" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html>
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
	    <style type="text/css">
            .style1
            {
                width: 100%;
            }
        </style>
	</head>
	<body >
		<form id="Form1" method="post" runat="server">
			<table border="0" cellspacing="2" cellpadding="1" style="width: 716px">
				<tr>
					<td class="Cabecera" style = "text-align:center">
                        <asp:Label ID="lblTitulo" runat="server" Text="Búsqueda de Puestos de Trabajo"></asp:Label>
                    </td>
				</tr>
				<tr>
					<td class="style1">
                        <table width="100%" border="0" cellspacing="2" cellpadding="1">
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
								<td width="100px" class="Titulo">Área</td>
								<td>								
                                    <asp:DropDownList ID="ddlArea" runat="server" Width="300px" CssClass="input" ></asp:DropDownList>
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
					<td height="300px" valign="top" class="style1">
                        <%--GRILLA ESTANDAR--%>
                        <asp:Panel ID="pnl_GrillaEstandar" runat="server" Height="300px" 
                            ScrollBars="Auto"  Width="710px">                     
                            <asp:DataGrid id="dgGrillaEstandar" runat="server" AutoGenerateColumns="False" 
                                Width="706px"  AllowSorting ="false" >
                                <%--<PagerStyle Mode="NumericPages"  />--%>
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
                                        <ItemStyle  Width="200px" HorizontalAlign="Center"/>
                                    </asp:TemplateColumn>
                                    <asp:TemplateColumn HeaderText="Area" >
                                        <ItemTemplate>
                                            <asp:Label ID="Label3" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.DE_AREA") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle  Width="100px" HorizontalAlign="Center"/>
                                    </asp:TemplateColumn>
                                    <asp:TemplateColumn HeaderText="Puesto" >
                                        <ItemTemplate>
                                            <asp:Label ID="Label4" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.DE_PUES_TRAB") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle  Width="100px" HorizontalAlign="Center"/>
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

