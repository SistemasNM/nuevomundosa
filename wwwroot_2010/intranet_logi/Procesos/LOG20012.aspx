<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="LOG20012.aspx.vb" Inherits="intranet_logi.LOG20012" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title></title>
		<meta content="Microsoft Visual Studio.NET 7.0" name="GENERATOR" />
		<meta content="Visual Basic 7.0" name="CODE_LANGUAGE" />
		<meta content="JavaScript" name="vs_defaultClientScript" />
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<link href="../css/NM0001.css" type="text/css" rel="stylesheet">
		<script type="text/javascript" language="javascript" src="../../intranet/JS/jsCalendario_N4.js"></script>
		<script type="text/javascript" language="javascript" src="../../intranet/JS/functions.js"></script>
		<script type="text/javascript" language="JavaScript">
<!--		    Begin

		    function BuscarArticulo(ControlID, Constante) {
		        var retorno = window.showModalDialog("../../intranet/Buscadores/frmBusquedaArticulos.aspx", "", "dialogHeight:420px;dialogWidth:480px;center:yes;help:no;");
		        //retorno debe traer el resultado de la busqueda
		        if (retorno != "::") {
		            var datos = retorno.split(":");
		            var Codigo = datos[0];
		            var Articulo = datos[1];
		            if (Codigo == '') {
		                return false;
		            } //end if
		            switch (Constante) {
		                case "f":
		                    var txtArticuloCod = document.all[ControlID + "_txtcodarticulof"];
		                    var lblArticulo = document.all[ControlID + "_lbldesarticulof"];
		                    txtArticuloCod.value = datos[0];
		                    lblArticulo.innerHTML = datos[1];
		                    break;
		                case "e":
		                    var txtArticuloCod = document.all[ControlID + "_txtcodarticuloe"];
		                    var lblArticulo = document.all[ControlID + "_lbldesarticuloe"];
		                    txtArticuloCod.value = Codigo = datos[0];
		                    lblArticulo.innerHTML = datos[1];
		                    break;
		            } //ens switch
		        } //end if
		    } //end function

		    function txtcodalm_onBlur(ControlID, Constante) {
		        //buscar el almacen por ajax
		        var lstrcodalm = document.all[ControlID + "_txtcodalmtransf" + Constante].value;

		        if (lstrcodalm == '') {
		            return false;
		        } //end if

		        var ldtbResultado = LOG20012.fnc_BuscarAlmacen(lstrcodalm);
		        if (ldtbResultado != null && typeof (ldtbResultado) == "object") {
		            var ldtbResultadoOk = ldtbResultado.value;
		            if (ldtbResultadoOk.Rows.length > 0) {
		                var lbldesalmacen = document.all[ControlID + "_lbldesalmtransf" + Constante];
		                lbldesalmacen.innerHTML = ldtbResultadoOk.Rows[0].de_alma;

		                return 1;
		            } //end if				
		        } //end if

		        document.all[ControlID + "_txtcodalmtransf" + Constante].value = '';
		        document.all[ControlID + "_lbldesalmtransf" + Constante].innerHTML = '';
		    } //end function

		    function ImprimirCodBar(pcod, pdes, pcb1, pcb2, pcb3, pcb4, pubi) {

		        //alert("LOG20012_P.aspx?pcod=" + pcod + "&pdes=" + pdes + "&pcb1=" + pcb1 + "&pcb2=" + pcb2 + "&pcb3=" + pcb3 + "&pcb4=" + pcb4);
		        var retorno = window.showModalDialog("LOG20012_P.aspx?pcod=" + pcod + "&pdes=" + encodeURIComponent(pdes) + "&pcb1=" + pcb1 + "&pcb2=" + pcb2 + "&pcb3=" + pcb3 + "&pcb4=" + pcb4 + "&pubi=" + pubi, "", "dialogHeight:220px;dialogWidth:620px;center:yes;help:no;");
		        //alert('ingreso');
		        retorno = null;
		    } //end function

		      //Almacenes
		      function BuscarAlmacenes() {
		        var retorno = window.showModalDialog("../../intranet/Buscadores/frmAlmacenes.aspx", "", "dialogheight:450px;dialogwidth:450px;center:yes;help:no;");
		        if (retorno != "" && retorno != ":") {
		          var datos = retorno.split(":");
		          var Codigo = datos[0];
		          var Nombre = datos[1];
		          document.all('txtCodAlmacen').value = Codigo;
		          document.all('lblDesAlmacen').value = Nombre;
		        }
		      }
		
//End-->
		</script>

	    <style type="text/css">
            .style1
            {
                width: 80px;
            }
        </style>
	</HEAD>
	<BODY>
		<form id="frmRequisicion" name="frmRequisicion" method="post" runat="Server">
			&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
			<TABLE id="Table4" style="Z-INDEX: 101; LEFT: 0px; WIDTH: 1192px; POSITION: absolute; TOP: 0px; HEIGHT: 289px"
				cellSpacing="2" cellPadding="2" width="1192" height="289" border="0">
				<TBODY>
					<TR>
						<TD class="cabecera" style="HEIGHT: 22px">&nbsp;CONFIGURACION DE ARTICULOS</TD>
					</TR>
					<TR>
						<TD class="panel">
							<TABLE style="WIDTH: 720px; HEIGHT: 16px">
					</TR>
			</TABLE>
			<TABLE style="WIDTH: 784px">
				<TR>
					<TD class="Etiqueta" style="WIDTH: 103px">&nbsp;Almacén Transfe.&nbsp;:</TD>
					<TD style="WIDTH: 333px" width="333" colspan="2">&nbsp;
						<asp:DropDownList id="ddlalmtransf" runat="server" CssClass="input" Width="280px"></asp:DropDownList></TD>
					<!--<TD style="WIDTH: 116px; HEIGHT: 8px" width="116">&nbsp;</TD>-->
					<TD style="HEIGHT: 8px">&nbsp;
						<asp:button id="btnbuscar" runat="server" CssClass="Boton" Text="Buscar"></asp:button></TD>
				</TR>
				<TR>
					<TD class="Etiqueta" style="WIDTH: 103px">&nbsp;Artículo&nbsp;:</TD>
					<TD style="WIDTH: 129px" width="129">&nbsp;<asp:TextBox id="txtcodarticulo" runat="server" Width="112px" CssClass="input" MaxLength="30"></asp:TextBox></TD>
					<TD style="WIDTH: 210px; HEIGHT: 8px" width="210">
						<asp:TextBox id="txtdesarticulo" runat="server" Width="204px" CssClass="input" MaxLength="50"></asp:TextBox></TD>

					<TD style="HEIGHT: 8px">&nbsp;</TD>
				</TR>
			</TABLE>
			<TABLE width="100%">
				<TR>
					<TD><asp:datagrid id="DataGrid1" runat="server" Width="1112px" ShowFooter="True" AutoGenerateColumns="False">
							<FooterStyle CssClass="GridFooter"></FooterStyle>
							<AlternatingItemStyle CssClass="GridAlternateItem"></AlternatingItemStyle>
							<ItemStyle CssClass="GridItem"></ItemStyle>
							<HeaderStyle CssClass="GridHeader"></HeaderStyle>
							<Columns>
								<asp:TemplateColumn HeaderText="Articulo">
									<HeaderStyle HorizontalAlign="Left" Width="350px"></HeaderStyle>
									<HeaderTemplate>
										<TABLE cellSpacing="1" cellPadding="1" width="100%" border="0">
											<TR>
												<TD class="GridHeader" width="100">
													<asp:Panel id="Panel34" runat="server" Width="120px" Height="12">Código</asp:Panel></TD>
												<TD class="GridHeader" width="100%">
													<asp:Panel id="Panel38" runat="server" Width="120px">Descripción</asp:Panel></TD>
											</TR>
										</TABLE>
									</HeaderTemplate>
									<ItemTemplate>
										<TABLE id="Table1" style="WIDTH: 100%; HEIGHT: 17px" height="17" cellSpacing="1" cellPadding="1"
											width="434" border="0">
											<TR>
												<TD width="100">
													<asp:Label id=lblcodarticuloi runat="server" Width="120px" CssClass="input" Height="12px" text='<%#Container.DataItem("vch_codarticulo")%>'>
													</asp:Label></TD>
												<TD width="100%">
													<asp:Label id=lbldesarticuloi runat="server" Width="297px" CssClass="input" Height="12px" text='<%#Container.DataItem("de_item")%>'>
													</asp:Label></TD>
											</TR>
										</TABLE>
									</ItemTemplate>
									<FooterTemplate>
										<TABLE id="Table3" style="HEIGHT: 20px" cellSpacing="1" cellPadding="1" width="100%" border="0">
											<TR>
												<TD width="120"><NOBR>
														<asp:TextBox id="txtcodarticulof" runat="server" Width="100px" CssClass="input"></asp:TextBox><INPUT class="Boton" id="btnarticulof" style="WIDTH: 20px; HEIGHT: 20px" type="button"
															value="..." name="btnarticulof" runat="server"></NOBR>
												</TD>
												<TD width="100%">
													<asp:Label id="lbldesarticulof" runat="server" Width="100%"></asp:Label></TD>
											</TR>
										</TABLE>
									</FooterTemplate>
									<EditItemTemplate>
										<TABLE id="Table2" cellSpacing="1" cellPadding="1" width="100%" border="0">
											<TR>
												<TD class="style1">
													<asp:TextBox id=txtcodarticuloe runat="server" Width="100px" CssClass="input" text='<%#Container.DataItem("vch_codarticulo")%>' ReadOnly="True">
													</asp:TextBox></TD>
												<TD width="20"><INPUT class="boton" id="btnarticuloe" style="WIDTH: 19px; HEIGHT: 19px" type="button"
														size="20" value="..." name="btnarticuloe" runat="server"></TD>
												<TD width="300">
													<asp:Label id=lbldesarticuloe runat="server" Width="100%" CssClass="input" text='<%#Container.DataItem("de_item")%>'>
													</asp:Label></TD>
											</TR>
										</TABLE>
									</EditItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="Centro Costo">
									<HeaderStyle Width="350px"></HeaderStyle>
									<HeaderTemplate>
										<TABLE id="Table30" style="WIDTH: 313px; HEIGHT: 28px" cellSpacing="1" cellPadding="1"
											width="313" border="0">
											<TR>
												<TD class="GridHeader" width="80">
													<asp:Panel id="Panel1" runat="server" Width="80px">Cod.Alm.</asp:Panel>
												</TD>
												<TD class="GridHeader" width="100%">
													<asp:Panel id="Panel2" runat="server" Width="120px">Alm. de transferencia</asp:Panel></TD>
											</TR>
										</TABLE>
									</HeaderTemplate>
									<ItemTemplate>
										<TABLE id="Table22" style="WIDTH: 305px; HEIGHT: 20px" cellSpacing="1" cellPadding="1"
											width="305" border="0">
											<TR>
												<TD width="80">
													<asp:Label id=lblcodalmtransfi runat="server" CssClass="input" text='<%#Container.DataItem("vch_almatransfauto")%>'>
													</asp:Label>
												</TD>
												<TD width="100%">
													<asp:Label id=lbldesalmtransfi runat="server" CssClass="input" text='<%#Container.DataItem("de_alma")%>'>
													</asp:Label>
												</TD>
											</TR>
										</TABLE>
									</ItemTemplate>
									<FooterTemplate>
										<TABLE id="Table20" style="WIDTH: 305px; HEIGHT: 28px" cellSpacing="1" cellPadding="1"
											width="305" border="0">
											<TR>
												<TD width="120"><NOBR>
														<asp:TextBox id="txtcodalmtransff" runat="server" Width="50px" CssClass="Input" MaxLength="3"></asp:TextBox>
                              &nbsp;
                              <input id="btnalmtransff" style="width: 20px; height:20px" onclick="javascript:BuscarAlmacenes();" type="button" value="..." name="btnalmtransff" class="Boton"/>
															 </NOBR>
												</TD>
												<TD width="100%">
													<asp:Label id="lbldesalmtransff" runat="server" CssClass="input" text=""></asp:Label>
												</TD>
											</TR>
										</TABLE>
									</FooterTemplate>
									<EditItemTemplate>
										<TABLE id="Table18" style="WIDTH: 305px; HEIGHT: 26px" cellSpacing="1" cellPadding="1"
											width="305" border="0">
											<TR>
												<TD>
													<asp:TextBox id=txtcodalmtransfe runat="server" Width="50px" CssClass="Input" Text='<%# Container.DataItem("vch_almatransfauto") %>' MaxLength="3">
													</asp:TextBox>&nbsp;<INPUT class="Boton" id="btnCentroCostoE" style="WIDTH: 20px; HEIGHT: 20px" type="button"
														value="..." name="btnCentroCostoE" runat="server"></TD>
												<TD>
													<asp:Label id=lbldesalmtransfe runat="server" CssClass="input" text='<%#Container.DataItem("de_alma")%>'>
													</asp:Label></TD>
											</TR>
										</TABLE>
									</EditItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="Cod. Barra - 1">
									<ItemTemplate>
										<asp:Label id="lblcodbarra1i" runat="server" text='<%#Container.DataItem("vch_codigobarra1")%>'>
										</asp:Label>
									</ItemTemplate>
									<FooterTemplate>
										<asp:TextBox id="txtcodbarra1f" runat="server" Width="120px" CssClass="input" text=''></asp:TextBox>
									</FooterTemplate>
									<EditItemTemplate>
										<asp:TextBox id=txtcodbarra1e runat="server" Width="120px" CssClass="input" text='<%#Container.DataItem("vch_codigobarra1")%>'>
										</asp:TextBox>
									</EditItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="Cod. Barra - 2">
									<ItemTemplate>
										<asp:Label id="lblcodbarra2i" runat="server" text='<%#Container.DataItem("vch_codigobarra2")%>'>
										</asp:Label>
									</ItemTemplate>
									<FooterTemplate>
										<asp:TextBox id="txtcodbarra2f" runat="server" Width="120px" CssClass="input" text=''></asp:TextBox>
									</FooterTemplate>
									<EditItemTemplate>
										<asp:TextBox id=txtcodbarra2e runat="server" Width="120px" CssClass="input" text='<%#Container.DataItem("vch_codigobarra2")%>'>
										</asp:TextBox>
									</EditItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="Cod. Barra - 3">
									<ItemTemplate>
										<asp:Label id="lblcodbarra3i" runat="server" text='<%#Container.DataItem("vch_codigobarra3")%>'>
										</asp:Label>
									</ItemTemplate>
									<FooterTemplate>
										<asp:TextBox id="txtcodbarra3f" runat="server" Width="120px" CssClass="input" text=''></asp:TextBox>
									</FooterTemplate>
									<EditItemTemplate>
										<asp:TextBox id=txtcodbarra3e runat="server" Width="120px" CssClass="input" text='<%#Container.DataItem("vch_codigobarra3")%>'>
										</asp:TextBox>
									</EditItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="Cod. Barra - 4">
									<ItemTemplate>
										<asp:Label id="lblcodbarra4i" runat="server" text='<%#Container.DataItem("vch_codigobarra4")%>'>
										</asp:Label>
									</ItemTemplate>
									<FooterTemplate>
										<asp:TextBox id="txtcodbarra4f" runat="server" Width="120px" CssClass="input" text=''></asp:TextBox>
									</FooterTemplate>
									<EditItemTemplate>
										<asp:TextBox id=txtcodbarra4e runat="server" Width="120px" CssClass="input" text='<%#Container.DataItem("vch_codigobarra4")%>'>
										</asp:TextBox>
									</EditItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText=".">
									<HeaderStyle Width="40px"></HeaderStyle>
									<ItemTemplate>
										<nobr>
											<asp:ImageButton id="btnEdit" runat="server" ImageUrl="../../intranet/Imagenes/Editar.gif" CommandName="Edit"></asp:ImageButton>&nbsp;
											<asp:ImageButton id="btnDelete" runat="server" ImageUrl="../../intranet/Imagenes/borrador.gif" CommandName="Delete"></asp:ImageButton>&nbsp;
                                            <asp:ImageButton ID="btnPrint" runat="server" ImageUrl="../../intranet/Imagenes/im_printer.gif" CommandName="Print"></asp:ImageButton>
                                        </nobr>
									</ItemTemplate>
									<FooterTemplate>
										<asp:ImageButton id="btnAdd" runat="server" ImageUrl="../../intranet/Imagenes/Grabar.gif" CommandName="Add"></asp:ImageButton>
									</FooterTemplate>
									<EditItemTemplate>
										<nobr>
											<asp:ImageButton id="btnUpdate" runat="server" ImageUrl="../../intranet/Imagenes/Grabar.gif" CommandName="Update"></asp:ImageButton>&nbsp;
											<asp:ImageButton id="btnCancel" runat="server" ImageUrl="../../intranet/Imagenes/Cancelar.gif" CommandName="Cancel"></asp:ImageButton></nobr>
									</EditItemTemplate>
								</asp:TemplateColumn>
							</Columns>
						</asp:datagrid></TD>
				</TR>
			</TABLE>
			&nbsp;&nbsp; </TD></TR></TBODY></TABLE></form>
	</BODY>
</HTML>
