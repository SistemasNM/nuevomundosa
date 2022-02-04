<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="PLA20012.aspx.vb" Inherits="intranet_rrhh_v2.PLA20012" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>LOG20005</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR" />
		<meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE" />
		<meta content="JavaScript" name="vs_defaultClientScript" />
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema" />
    <%--<link href="../../intranet/Estilos/NM0001.css" type="text/css" rel="stylesheet" />--%>
    <link href="../Styles/NM0001.css" rel="stylesheet" type="text/css"/>
		
   <%--  <script type="text/javascript" language="javascript" src="../../intranet/JS/functions.js"></script>--%>
    <script type="text/javascript" language="javascript" src="../Scripts/functions.js"></script>
    <script type="text/javascript" language="javascript">

        function VerDetalle(pstrCodigoSol) {
            var retorno = window.showModalDialog("PLA20010.aspx?&pstrCodigoSolicitud=" + pstrCodigoSol, "", "dialogHeight:420px;dialogWidth:920px;center:yes;help:no;");
            if (retorno == "1") {
                //Form1.hdn1.value="1";
                //Form1.submit;            
                return true;
            }
            else {
                return false;
            } //end if
        } //end function


        function fnc_aprobarmasivo(pstr_id, pstr_documento) {
            //alert(pstr_id +" - "+pstr_documento);
            var lstr_valores = document.all('hdnaprobarmasivo').value;
            //alert(document.all(pstr_id+'_chkaprobari').checked);
            if (document.all(pstr_id + '_chkaprobari').checked == true) {
                lstr_valores = lstr_valores + pstr_documento + ',';
            } else {
                pstr_documento = pstr_documento + ',';
                lstr_valores = replaceAll(lstr_valores, pstr_documento, '');
            } //end if
            document.all('hdnaprobarmasivo').value = lstr_valores;
            //alert(document.all('hdnaprobarmasivo').value);
        } //end function
		</script>
</head>
<body>
    <form id="Form1" method="post" runat="server">
        <table style="Z-INDEX: 101; LEFT: 0px; WIDTH: 656px; POSITION: absolute; TOP: 0px; HEIGHT: 429px" id="Table1" border="0" cellspacing="2" cellpadding="2" width="656">
            <tr>
                <td style="height: 28px" class="Cabecera"> APROBACION DE&nbsp;HORAS EXTRAS</td>
            </tr>
            <tr>
					<td class="Panel">
						<table id="tblBotonera1" class="botonera" border="0" cellspacing="0" cellpadding="0" width="100%"
							runat="server">
							<tr>
								<td align="left"><asp:button id="btnaprobarmasivo" runat="server" Width="124px" Text="Aprobar Masivo" CssClass="Boton"
										Enabled="False" Visible="False"></asp:button>&nbsp;<input style="WIDTH: 33px; HEIGHT: 13px" id="hdn1" size="1" type="hidden" name="hdn1" runat="server"><input style="WIDTH: 33px; HEIGHT: 13px" id="hdnaprobarmasivo" size="1" type="hidden" name="hdnaprobarmasivo"
										runat="server"></td>
								<td align="right"><asp:button id="btnConsultar" runat="server" Text="Consultar" CssClass="Boton"></asp:button></td>
							</tr>
						</table>
						<div style="WIDTH: 100%; HEIGHT: 300px"><asp:datagrid id="dtgLista" runat="server" Width="584px" AutoGenerateColumns="False">
								<AlternatingItemStyle CssClass="GridAlternateItem"></AlternatingItemStyle>
								<ItemStyle CssClass="GridItem"></ItemStyle>
								<HeaderStyle CssClass="Gridheader"></HeaderStyle>
								<Columns>
									<asp:TemplateColumn Visible="False" HeaderText="Apr.">
										<HeaderStyle Width="20px"></HeaderStyle>
										<ItemTemplate>
											<asp:CheckBox id="chkaprobari" runat="server" Visible="True"></asp:CheckBox>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn>
										<HeaderStyle Width="30px"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center"></ItemStyle>
										<ItemTemplate>
											<asp:ImageButton id="ibtBuscar" runat="server" ImageUrl="../images/buscar.gif"></asp:ImageButton>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:BoundColumn DataField="chr_codigo_sol" HeaderText="Solicitud">
										<HeaderStyle Width="100px"></HeaderStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="vch_codigoarea" HeaderText="C&#243;digo &#193;rea">
										<HeaderStyle Width="100px"></HeaderStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="vch_desarea" HeaderText="Desc. &#193;rea">
										<HeaderStyle Width="300px"></HeaderStyle>
									</asp:BoundColumn>
									<asp:BoundColumn DataField="tin_horext_sol" HeaderText="Total Horas">
										<HeaderStyle Width="80px"></HeaderStyle>
									</asp:BoundColumn>
								</Columns>
							</asp:datagrid></div>
						<table id="Table4" class="botonera" border="0" cellspacing="0" cellpadding="0" width="100%">
							<tr>
								<td style="FONT-SIZE: small; WIDTH: 625px" class="Etiqueta" align="right">Total de 
									documentos por aprobar :
								</td>
								<td align="right"><asp:label id="lblCantidad" runat="server" Width="63px" CssClass="Etiqueta" Font-Size="Small"></asp:label></td>
								<td align="right">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
								</td>
							</tr>
						</table>
					</td>
				</tr>
        </table>
    </form>
</body>
</html>
