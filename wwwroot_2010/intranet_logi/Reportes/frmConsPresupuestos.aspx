<%@ Page Language="vb" AutoEventWireup="false" Codebehind="frmConsPresupuestos.aspx.vb" Inherits="intranet_logi.frmConsPresupuestos"%>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html>
  <head>
	<title>frmConsPresupuestos</title>
	<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR"/>
	<meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE"/>
	<meta content="JavaScript" name="vs_defaultClientScript"/>
	<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema"/>
	<link href="../css/NM0001.css" type="text/css" rel="stylesheet"/>
	<script language="javascript" type="text/javascript" src="../../intranet/JS/jsCalendario_N2.js"></script>
	<script language="javascript" type="text/javascript">		
		//=================================================//

	   function fValidarBus(){
	       
	       if (document.frmData.txtCentroCostoCodigo.value == "" ){
	          alert("Seleccionar el Centro de Costo")
	           return false;
	       }
	   
	       return true;
	   }
	
		//=================================================//
	
		function BuscarCC()
        {
			var retorno = window.showModalDialog("../../intranet/Buscadores/LOG_0001.aspx","","dialogHeight:450px;dialogWidth:450px;center:yes;help:no;");
			if (retorno!="" && retorno!=":")
			{
			var datos = retorno.split(":");
			var Codigo = datos[0];
			var Nombre = datos[1];
			document.all('txtCentroCostoCodigo').value = Codigo;
			document.all('txtCentroCostoNombre').value = Nombre;
			}
		}	
		
		
		//=================================================//
		
		function VerReporte(sCuenta,sCCosto,sNumAno, sNumMes )
        {

        var pagina="/Smar0005/FNFN0002/FNPR0001/Reportes/Rpts/ADORecordSet1.asp?wReporte=CuadroComparativoGastos&CtaGasto="+sCuenta+"&CentCosto="+sCCosto+"&anno="+sNumAno+"&mes="+sNumMes;
        window.open(pagina, "ConsultaCosto", "width=750, height=500, location=yes, menubar=yes, status=yes, toolbar=yes, scrollbars=yes, resizable=yes");
        
        }



	</script>
  </head>
  <body>
	<center>
	  <form id="frmData" method="post" runat="server">
		<table id="Table1" cellspacing="2" cellpadding="2" width="700" border="0">
		  <tr>
			<td class="Cabecera">Consulta de Presupuesto</td>
		  </tr>
		</table>
		<table id="Table5" style="HEIGHT: 28px" cellspacing="2" cellpadding="2" width="700" border="0">
		  <tr>
			<td class="etiqueta" style="WIDTH: 84px">Año&nbsp;</td>
			<td class="input">
			  <asp:dropdownlist id="cboAnno" runat="server" Font-Size="XX-Small" Font-Names="Verdana" Height="16px"
				Width="160px">
				<asp:ListItem Value="2005">A&#241;o 2005</asp:ListItem>
				<asp:ListItem Value="2006">A&#241;o 2006</asp:ListItem>
				<asp:ListItem Value="2007">A&#241;o 2007</asp:ListItem>
				<asp:ListItem Value="2008">A&#241;o 2008</asp:ListItem>
				<asp:ListItem Value="2009">A&#241;o 2009</asp:ListItem>
				<asp:ListItem Value="2010">A&#241;o 2010</asp:ListItem>
				<asp:ListItem Value="2011">A&#241;o 2011</asp:ListItem>
				<asp:ListItem Value="2012">A&#241;o 2012</asp:ListItem>
                <asp:ListItem Value="2013">A&#241;o 2013</asp:ListItem>
			      <asp:ListItem Value="2014">Año 2014</asp:ListItem>
                  <asp:ListItem Value="2015">Año 2015</asp:ListItem>
                  <asp:ListItem Value="2016">Año 2016</asp:ListItem>
                  <asp:ListItem Value="2017">Año 2017</asp:ListItem>
                  <asp:ListItem Value="2018">Año 2018</asp:ListItem>
                  <asp:ListItem Value="2019">Año 2019</asp:ListItem>
			  </asp:dropdownlist></td>
		  </tr>
		</table>
		<table id="Table6" style="HEIGHT: 28px" cellspacing="2" cellpadding="2" width="700" border="0">
		  <tr>
			<td class="etiqueta" style="WIDTH: 84px; HEIGHT: 12px">Mes</td>
			<td class="input" style="HEIGHT: 12px">
			  <asp:dropdownlist id="cboMes" runat="server" Font-Size="XX-Small" Font-Names="Verdana" Height="16px"
				Width="160px">
				<asp:ListItem Value="1">Enero</asp:ListItem>
				<asp:ListItem Value="2">Febrero</asp:ListItem>
				<asp:ListItem Value="3">Marzo</asp:ListItem>
				<asp:ListItem Value="4">Abril</asp:ListItem>
				<asp:ListItem Value="5">Mayo</asp:ListItem>
				<asp:ListItem Value="6">Junio</asp:ListItem>
				<asp:ListItem Value="7">Julio</asp:ListItem>
				<asp:ListItem Value="8">Agosto</asp:ListItem>
				<asp:ListItem Value="9">Septiembre</asp:ListItem>
				<asp:ListItem Value="10">Octubre</asp:ListItem>
				<asp:ListItem Value="11">Noviembre</asp:ListItem>
				<asp:ListItem Value="12">Diciembre</asp:ListItem>
			  </asp:dropdownlist></td>
		  </tr>
		  <tr>
			<td class="etiqueta" style="WIDTH: 84px">Auxiliar</td>
			<td class="input">
			  <asp:textbox id="txtCentroCostoCodigo" runat="server" Font-Size="XX-Small" Font-Names="Verdana"
				Height="19px" Width="128px"></asp:textbox>&nbsp;<input class="boton" id="btnBuscarCC" style="WIDTH: 24px; HEIGHT: 17px" onclick="javascript:BuscarCC()"
				type="button" value="..." name="btnBuscar1">&nbsp;
			  <asp:TextBox id="txtCentroCostoNombre" BorderStyle="None" runat="server" Width="328px" Font-Size="7pt"
				BackColor="InactiveCaptionText" Height="17px"></asp:TextBox></td>
		  </tr>
		</table>
		<table class="botonera" id="Table7" style="HEIGHT: 28px" cellspacing="2" cellpadding="2"
		  width="700" border="0">
		  <tr>
			<td class="input" align="right"><asp:button id="btnBuscar" runat="server" CssClass="boton" Text="Buscar"></asp:button></td>
		  </tr>
		</table>
		<table>
		  <tr>
			<td><asp:datagrid id="grdData" runat="server" Width="700px" AutoGenerateColumns="False">
				<AlternatingItemStyle CssClass="GridAlternateItem"></AlternatingItemStyle>
				<ItemStyle CssClass="GridItem"></ItemStyle>
				<HeaderStyle Height="25px" CssClass="GridHeader"></HeaderStyle>
				<Columns>
				  <asp:TemplateColumn HeaderText="">
					<HeaderStyle Width="30"></HeaderStyle>
					<ItemStyle HorizontalAlign="Center"></ItemStyle>
					<ItemTemplate>
					  <asp:ImageButton id="btnVer" runat="server" CommandName="Ver" ImageUrl="../../intranet/Imagenes/buscar.gif"></asp:ImageButton>
					</ItemTemplate>
				  </asp:TemplateColumn>
				  <asp:BoundColumn DataField="CO_CNTA" HeaderText="Cuenta Gasto">
					<HeaderStyle Width="120px"></HeaderStyle>
					<ItemStyle HorizontalAlign="Left" Height="20px"></ItemStyle>
				  </asp:BoundColumn>
				  <asp:BoundColumn DataField="DE_CNTA" HeaderText="Descripci&#243;n">
					<HeaderStyle Width="350px"></HeaderStyle>
					<ItemStyle HorizontalAlign="Left"></ItemStyle>
				  </asp:BoundColumn>
				  <asp:BoundColumn DataField="IM_PRES_MESE" HeaderText="Gasto Presupuestado" DataFormatString="{0:#,##0.00}">
					<HeaderStyle Width="110px"></HeaderStyle>
					<ItemStyle HorizontalAlign="Right"></ItemStyle>
				  </asp:BoundColumn>
				  <asp:BoundColumn DataField="IM_EJEC_MESE" HeaderText="Gasto Ejecutado" DataFormatString="{0:#,##0.00}">
					<HeaderStyle Width="110px"></HeaderStyle>
					<ItemStyle HorizontalAlign="Right"></ItemStyle>
				  </asp:BoundColumn>
				  <asp:BoundColumn DataField="IM_SALDO" HeaderText="Saldo" DataFormatString="{0:#,##0.00}">
					<HeaderStyle Width="110px"></HeaderStyle>
					<ItemStyle HorizontalAlign="Right"></ItemStyle>
				  </asp:BoundColumn>
				</Columns>
			  </asp:datagrid>
            </td>
		  </tr>
		</table>
	  </form>
	</center>
  </body>
</html>
