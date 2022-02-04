<%@ Page Language="vb" AutoEventWireup="false" Codebehind="LOG20005.aspx.vb" Inherits="intranet_logi.LOG20005"%>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html>
	<head>
		<title>LOG20005</title>
		<meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1"/>
		<meta name="CODE_LANGUAGE" content="Visual Basic .NET 7.1"/>
		<meta name="vs_defaultClientScript" content="JavaScript"/>
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5"/>		
        <link href="../css/NM0001.css" rel="stylesheet" type="text/css" />
        <script src="../js/functions.js" type="text/javascript"></script>	
		<script language="javascript" type="text/javascript">

		function VerDetalle (OCOS)
			{
				var retorno = window.showModalDialog("LOG20006.aspx?&strRequisicion="+OCOS,"","dialogheight:600px;dialogwidth:790px;center:yes;help:no;");
				if (retorno == "1") {
				    return true;
				} else {
                   return false;
                }
           }


		function VerHistorial (OCOS)
			{
				var retorno = window.showModalDialog("LOG20007.aspx?&strRequisicion="+OCOS,"","dialogheight:300px;dialogwidth:500px;center:yes;help:no;");
			}//end function
			
			function fnc_aprobarmasivo(pstr_id,pstr_documento)
			{

			    //alert(pstr_id.id);
			    //alert(pstr_documento);

			    var lstr_valores = document.all('hdnaprobarmasivo').value;

			    if (pstr_id.checked == true) {
			        lstr_valores = lstr_valores + pstr_documento + ',';
			    } else {
			        pstr_documento = pstr_documento + ',';
			        lstr_valores = replaceAll(lstr_valores, pstr_documento, '');
			    } //end if

			    //alert(lstr_valores);

				document.all('hdnaprobarmasivo').value=lstr_valores;


} //end function

//Lista fnc_AdjuntarDocsOC
function fnc_ListarDocsAdjuntosOC(strNumeroDoc) {
    var pstrTipoDoc = "OCM"
    var pstrNumeroDoc = strNumeroDoc
    var pstrSecuencia = " "
    var retorno = window.showModalDialog("frm_ListadoArchivoAdjunto.aspx?pstrTipoDoc=" + pstrTipoDoc + "&pstrNumeroDoc=" + pstrNumeroDoc + "&pstrSecuencia=0", "Listar Adjuntos", "dialogHeight:400px;dialogWidth:720px;center:yes;help:no;status:no;");
    return false;
}
		</script>
	</head>
	<body>
		<form id="Form1" method="post" runat="server">
			<table style="LEFT: 0px; width: 900px; POSITION: absolute; TOP: 0px" border="0" cellspacing="0" cellpadding="0">
				<tr>
					<td style="width:800px; height:25px" class="Cabecera">APROBACION DE ORDENES DE COMPRA/SERVICIO</td>
				</tr>
				<tr>
					<td class="Panel">
						<table id="tblFil1" border="0" cellspacing="0" cellpadding="0" width="900px" runat="server">
							<tr>
								<td class="Etiqueta" width="900px">Tipos</td>
							</tr>
						</table>
						<table id="tblFil2" border="0" cellspacing="0" cellpadding="0" width="900px" runat="server">
							<tr>
								<td class="Etiqueta" valign="top" width="900px">Observaciones</td>
							</tr>
						</table>
						<table id="tblBotonera1" class="botonera" border="0" cellspacing="0" cellpadding="0" width="900px" runat="server">
							<tr>
								<td style="width:700px" align="left">
                                 <asp:button id="btnaprobarmasivo" runat="server" width="124px" Text="Aprobar Masivo" CssClass="Boton" BackColor="#006600"></asp:button>&nbsp;
                                 <input style="width: 33px; height: 13px" id="hdn1" size="1" type="hidden" name="hdn1" runat="server" />
                                 <input style="width: 33px; height: 13px" id="hdnaprobarmasivo" size="1" type="hidden" name="hdnaprobarmasivo" runat="server" />
                                </td>
								<td style="width:200px" align="right"><asp:button id="btnConsultar" runat="server" Text="Consultar" CssClass="Boton"></asp:button></td>
							</tr>
						</table>
						<div style="width:900px">
                        
                        <asp:datagrid id="dtgLista" runat="server" width="900px" AutoGenerateColumns="False">
                  		 <AlternatingItemStyle CssClass="GridAlternateItem"></AlternatingItemStyle>
						 <ItemStyle CssClass="GridItem"></ItemStyle>
						 <HeaderStyle CssClass="gridheader"></HeaderStyle>
						 <Columns>

						  <asp:TemplateColumn HeaderText="Apr.">
                           <ItemTemplate>
							<asp:CheckBox id="chkaprobari" runat="server"></asp:CheckBox>
						   </ItemTemplate>
						  </asp:TemplateColumn>
									
                          <asp:TemplateColumn>
						   <ItemTemplate>
							<asp:ImageButton id="ibtBuscar" runat="server" ImageUrl="~/images/buscar.gif"></asp:ImageButton>
						   </ItemTemplate>
						  </asp:TemplateColumn>

                          <%--Adjuntos--%>
                          <asp:TemplateColumn>
                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="20px" Font-Bold="true"></HeaderStyle>
		                    <ItemTemplate>
                            <asp:ImageButton id="ibtVerAdj" runat="server" ImageUrl="~/images/adjuntos.ico" ToolTip="Ver documentos adjuntos" Width="16px" Height="16px"></asp:ImageButton>
                            <asp:Label id="lblNumAdj" runat="server" Font-Size="8px" Text='<%# DataBinder.Eval(Container, "DataItem.int_Adjunto") %>' visible ="false"></asp:Label>
                            </ItemTemplate>
	                      </asp:TemplateColumn>

						  <asp:BoundColumn DataField="var_GrupoAprobacionNombre" HeaderText="Grupo de aprobaci&#243;n">
                           <HeaderStyle HorizontalAlign="center"></HeaderStyle>
                           <ItemStyle Font-Size="9px" HorizontalAlign="left" VerticalAlign="Middle" width="150px"></ItemStyle>
                          </asp:BoundColumn>

						  <asp:BoundColumn DataField="var_TipoAprobacionNombre" HeaderText="Tipo de aprobaci&#243;n">
                           <HeaderStyle HorizontalAlign="center"></HeaderStyle>
                           <ItemStyle Font-Size="9px" HorizontalAlign="left" VerticalAlign="Middle" width="150px"></ItemStyle>
                          </asp:BoundColumn>

						  <asp:BoundColumn DataField="var_OrdenCompra" HeaderText="N&#186; Documento">
                           <HeaderStyle HorizontalAlign="center"></HeaderStyle>
                           <ItemStyle Font-Size="9px" HorizontalAlign="left" VerticalAlign="Middle" width="100px"></ItemStyle>
                          </asp:BoundColumn>

						  <asp:BoundColumn DataField="var_Moneda" HeaderText="Moneda">
                           <HeaderStyle HorizontalAlign="center"></HeaderStyle>
                           <ItemStyle Font-Size="9px" HorizontalAlign="left" VerticalAlign="Middle" width="50px"></ItemStyle>
                          </asp:BoundColumn>
						  
                          <asp:BoundColumn DataField="num_Importe" HeaderText="Importe" DataFormatString="{0:#,##0.00}">
						   <HeaderStyle HorizontalAlign="center"></HeaderStyle>
						   <ItemStyle Font-Size="9px" HorizontalAlign="right" VerticalAlign="Middle" width="100px"></ItemStyle>
						  </asp:BoundColumn>
						  
                          <asp:BoundColumn DataField="var_FechaCreacion" HeaderText="Fecha Creaci&#243;n">
                          <HeaderStyle HorizontalAlign="center"></HeaderStyle>
						   <ItemStyle Font-Size="9px" HorizontalAlign="right" VerticalAlign="Middle" width="100px"></ItemStyle>
                          </asp:BoundColumn>

						  <asp:BoundColumn DataField="var_ProveedorNombre" HeaderText="Entidad">
                           <HeaderStyle HorizontalAlign="center"></HeaderStyle>
						   <ItemStyle Font-Size="9px" HorizontalAlign="left" VerticalAlign="Middle" width="200px"></ItemStyle>
                          </asp:BoundColumn>
						  
                          <asp:BoundColumn DataField="var_CentroCostoNombre" HeaderText="Centro Costo">
                           <HeaderStyle HorizontalAlign="center"></HeaderStyle>
						   <ItemStyle Font-Size="9px" HorizontalAlign="right" VerticalAlign="Middle" width="100px"></ItemStyle>
                          </asp:BoundColumn>
                          
                          <%--CTC/Activo--%>
                          <asp:BoundColumn DataField="var_DescripcionActivo" HeaderText="CTC/Activo" HeaderStyle-Font-Bold="true">
                           <HeaderStyle HorizontalAlign="center"></HeaderStyle>
	                       <ItemStyle Font-Size="9px" HorizontalAlign="left" VerticalAlign="Middle" width="200px"></ItemStyle>
	                      </asp:BoundColumn>
									
                          <asp:TemplateColumn>
						   <ItemTemplate>
							<asp:ImageButton id="ibtHistorial" runat="server" ImageUrl="~/images/Historial.JPG"></asp:ImageButton>
						   </ItemTemplate>
						  </asp:TemplateColumn>
                                    
						 </Columns>
						</asp:datagrid>
                        </div>
						<table style="width:900px" id="Table4" class="botonera" border="0" cellspacing="0" cellpadding="0">
							<tr>
								<td style="width:700px" class="Etiqueta" align="right"># de documentos por aprobar:</td>
								<td style="width:200px"  align="right"><asp:label id="lblCantidad" runat="server" width="63px" CssClass="Etiqueta" Font-Size="10px"></asp:label></td>
							</tr>
						</table>
					</td>
				</tr>
			</table>
		</form>
	</body>
</html>
