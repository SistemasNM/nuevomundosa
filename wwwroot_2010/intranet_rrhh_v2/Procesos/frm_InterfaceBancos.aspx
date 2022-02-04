<%@ Page Language="vb" AutoEventWireup="false" Codebehind="frm_InterfaceBancos.aspx.vb" Inherits="intranet_rrhh_v2.frm_InterfaceBancos"%>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html>
	<head>
		<title>Interfase a Bancos</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR" />
		<meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE" />
		<meta content="JavaScript" name="vs_defaultClientScript" />
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema" />
	  <link href="../../intranet/Estilos/NM0001.css" type="text/css" rel="stylesheet"/>
	  <link href="../Styles/sytle.css" rel="stylesheet"/>
    <script language="javascript" src="../../intranet/JS/jsCalendario_N3.js" type="text/javascript"></script>
    <script language="javascript" type="text/javascript">
      // Mostrar reporte
      function fMostrarReporte(strURL) {
        var intWidth = screen.width;
        var intHeight = screen.height;
        window.open(strURL, "", "top=0; left=0; width=" + intWidth + "; height=" + intHeight + "; resizable=1;");
      }
    </script>
  </head>
	<body >
	 <form id="frm_InterfaceBancos" method="post" autocomplete="off" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager> 
    <asp:UpdatePanel ID="panContenido" runat="server" UpdateMode="Always">
    <ContentTemplate>
    
		 <div style="width:800px">
		  <table cellspacing="1" cellpadding="1" style="width:800px" border="0">
       <tr>
			  <td style="width:800px; height:20px" class="Cabecera" colspan="2" align="center">INTERFASE A BANCOS</td>
			 </tr>
        <tr>
         <td align="right" class="etiqueta" colspan="2" style="width:800px">
          <asp:Button ID="btnReporteDetalle" runat="server" CssClass="btnAzul" Text="Exportar" Width="120px"/>
          &nbsp;<asp:Button ID="btnGeneraFile" runat="server" accessKey="G" CssClass="btnVerde" Text="Generar TXT" Width="120px"/>
         </td>
        </tr>
			 <tr>
        <td class="etiqueta" style="width:800px" colspan="2" align ="right">Empresa:
         <asp:TextBox ID="txtCodEmpresa" runat="server" CssClass="Descripcion" Width="50px"></asp:TextBox>
         &nbsp;<asp:TextBox ID="txtDesEmpresa" runat="server" CssClass="Descripcion" Width="320px"></asp:TextBox>
        </td>
        <tr>
         <td class="etiqueta" style="width:100px">Planilla:</td>
         <td style="width:700px">
          <asp:DropDownList ID="ddlPlanilla" runat="server" AutoPostBack="True" CssClass="input" Width="200px">
           <asp:ListItem Value="Seleccionar">Seleccionar</asp:ListItem>
           <asp:ListItem Value="EMP">Empleados</asp:ListItem>
           <asp:ListItem Value="OBM">Obreros</asp:ListItem>
          </asp:DropDownList>
         </td>
        </tr>
        <tr>
         <td class="etiqueta" style="width:100px">Banco:</td>
         <td style="width:700px">
          <asp:DropDownList ID="ddlBanco" runat="server" AutoPostBack="True" CssClass="input" Width="200px">
           <asp:ListItem Value="Seleccionar">Seleccionar</asp:ListItem>
           <asp:ListItem Value="01">Credito</asp:ListItem>
           <asp:ListItem Value="02">Continental</asp:ListItem>
           <asp:ListItem Value="03">Interbank</asp:ListItem>
           <asp:ListItem Value="15">Scotiabank</asp:ListItem>
              <asp:ListItem Value="32">BanBif</asp:ListItem>
          </asp:DropDownList>
         </td>
        </tr>
        <tr>
         <td class="etiqueta" style="width:100px">Moneda:</td>
         <td style="width:700px">
          <asp:DropDownList ID="ddlMoneda" runat="server" AutoPostBack="True" CssClass="input" Width="200px">
           <asp:ListItem Value="Seleccionar">Seleccionar</asp:ListItem>
           <asp:ListItem Value="DOL">Dolares</asp:ListItem>
           <asp:ListItem Value="SOL">Soles</asp:ListItem>
          </asp:DropDownList>
         </td>
        </tr>
        <tr>
         <td class="etiqueta" style="width:100px">Cuenta:</td>
         <td style="width:700px">
          <asp:DropDownList ID="ddlCuenta" runat="server" CssClass="Input" Width="200px"></asp:DropDownList>
         </td>
        </tr>
        <tr>
         <td class="etiqueta" style="width:100px">Periodo:</td>
         <td style="width:700px">
          <asp:TextBox ID="txtAnno" runat="server" CssClass="Input" MaxLength="4" Width="60px"></asp:TextBox>&nbsp;
          <asp:DropDownList ID="ddlMes" runat="server" AutoPostBack="True" CssClass="input" Width="134px">
           <asp:ListItem Value="Seleccionar">Seleccionar</asp:ListItem>
           <asp:ListItem Value="01">Enero</asp:ListItem>
           <asp:ListItem Value="02">Febrero</asp:ListItem>
           <asp:ListItem Value="03">Marzo</asp:ListItem>
           <asp:ListItem Value="04">Abril</asp:ListItem>
           <asp:ListItem Value="05">Mayo</asp:ListItem>
           <asp:ListItem Value="06">Junio</asp:ListItem>
           <asp:ListItem Value="07">Julio</asp:ListItem>
           <asp:ListItem Value="08">Agosto</asp:ListItem>
           <asp:ListItem Value="09">Septiembre</asp:ListItem>
           <asp:ListItem Value="10">Octubre</asp:ListItem>
           <asp:ListItem Value="11">Noviembre</asp:ListItem>
           <asp:ListItem Value="12">Diciembre</asp:ListItem>
          </asp:DropDownList>
         </td>
        </tr>
        <tr>
         <td class="etiqueta" style="width:100px">Trabajadores:</td>
         <td style="width:700px">
          <asp:TextBox ID="txtCTrabI" runat="server" CssClass="Input" MaxLength="5" Width="97px"></asp:TextBox>
          &nbsp;<asp:TextBox ID="txtCTrabF" runat="server" CssClass="Input" MaxLength="5" Width="97px"></asp:TextBox>
         </td>
        </tr>
        <tr>
         <td class="etiqueta" style="width:100px">Fecha Abono:</td>
         <td style="width:700px">
          <asp:TextBox ID="txtFechaAbono" runat="server" CssClass="inputDisabled2" Width="180px"></asp:TextBox>
          &nbsp;<img onclick="popUpCalendar(this, frm_InterfaceBancos.txtFechaAbono, 'dd/mm/yyyy')" height="15px" width="15px" alt="Seleccionar fecha" src="../../intranet/Imagenes/Calendario.gif" border="0" />
         </td>
        </tr>
        <tr>
         <td class="etiqueta" style="width:100px">Abono:</td>
         <td style="width:700px">
          <asp:RadioButtonList ID="rblAbonos" runat="server" AutoPostBack="true" Width="200px">
           <asp:ListItem Value="00">Quincena</asp:ListItem>
           <asp:ListItem Value="01">Fin de Mes</asp:ListItem>
           <asp:ListItem Value="02">Por Concepto</asp:ListItem>
           <asp:ListItem Value="03">Mov.Varios</asp:ListItem>
           <asp:ListItem Value="04">C.T.S.</asp:ListItem>
           <asp:ListItem Value="05">Cuenta Corriente:</asp:ListItem>
          </asp:RadioButtonList>
         </td>
        </tr>
        <tr>
         <td class="etiqueta" style="width:100px">Concepto:</td>
         <td style="width:700px">
          <asp:TextBox ID="txtConcepto" runat="server" CssClass="Input" width="200px"></asp:TextBox>
         </td>
        </tr>
        <tr>
         <td class="etiqueta" style="width:100px">Tipo Cambio:</td>
         <td style="width:700px">
          <asp:TextBox ID="txtTC" runat="server" CssClass="inputnumber" width="200px"></asp:TextBox>
         </td>
        </tr>
        <tr>
				 <td class="Etiqueta" style="width:100px">Nombre archivo:</td>
				 <td style="width:700px">
          <asp:textbox id="txtArchivo" runat="server" CssClass="Input" Width="200px"></asp:textbox>
         </td>
				</tr>
        <tr>
         <td colspan="2" style="width:800px" align ="left">
          <asp:Label ID="lblmensaje" runat="server" CssClass="mensaje" Font-Bold="true" Text=""></asp:Label>
         </td>
        </tr>
       </table>
      </div>
     </ContentTemplate>
     <Triggers>
      <asp:AsyncPostBackTrigger ControlID ="btnGeneraFile" EventName="click" />
      <asp:AsyncPostBackTrigger ControlID ="rblAbonos" EventName="SelectedIndexChanged" />
     </Triggers>
    </asp:UpdatePanel>
	 </form>
  </body>
</html>