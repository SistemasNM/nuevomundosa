<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="frm_SeguimientoDocLog.aspx.vb" Inherits="intranet_logi.frm_SeguimientoDocLog" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
 <title>Seguimiento de Documentos</title>
 <link href="../css/NM0001.css" type="text/css" rel="stylesheet"/>
 <script language="javascript" type="text/javascript" src="../../intranet/JS/jsCalendario_N3.js"></script>
 <script language="javascript" type="text/javascript" src="../../intranet/JS/functions.js"></script>
 <script language="javascript" type="text/javascript">
     //FormatearBusqDoc
     function FormatearBusqDoc(pTexto) {
         //serie
         if (pTexto == 1) {
             var lserie = '000000' + document.all["txtSerie"].value;
             lserie = lserie.substring(lserie.length, lserie.length - 4);
             if (lserie == '0000') {
                 document.all["txtSerie"].value = '';
             }
             else {
                 document.all["txtSerie"].value = lserie;
             }
         }
         //numero
         if (pTexto == 2) {
             var lnume = '00000000000' + document.all["txtNumeroDocumento"].value;
             lnume = lnume.substring(lnume.length, lnume.length - 10);
             if (lnume == '0000000000') {
                 document.all["txtNumeroDocumento"].value = '';
             }
             else {
                 document.all["txtNumeroDocumento"].value = lnume;
             }
         }
     }

     // Funcion Busca Centro de Costos
     function fnc_BuscarCentroCosto() {
         var retorno = window.showModalDialog("../../intranet/Buscadores/LOG_0001.aspx", "", "dialogHeight:450px;dialogWidth:450px;center:yes;help:no;");
         if (retorno != "" && retorno != ":") {
             var datos = retorno.split(":");
             var Codigo = datos[0];
             var Nombre = datos[1];
             document.all('txtCodCentroCosto').value = Codigo;
             document.all('txtDesCentroCosto').value = Nombre;
         }
     }

     // Funcion Busca Empleados
     function fnc_Buscartrabajadores() {
         var tipo = "EMP";
         var retorno = window.showModalDialog("../../intranet/Buscadores/PLA_0001.aspx?strTipo=" + tipo, "", "dialogHeight:450px;dialogWidth:450px;center:yes;help:no;");
         if (retorno != "" && retorno != ":") {
             var datos = retorno.split(":");
             var codigo = datos[0];
             var nombre = datos[1];
             document.all('txtCodSolicitante').value = codigo;
             document.all('txtDesSolicitante').value = nombre;
         }
     }
     function fnc_BuscarProveedor() {
         var retorno = window.showModalDialog("../../intranet/Buscadores/frm_QryProveedores.aspx", "dialogHeight:500px;dialogWidth:800px;center:yes;help:no;")
         var datos = retorno.split(":");
         var Codigo = datos[0];
         var Nombre = datos[1];
         document.all('txtCodProveedor').value = Codigo;
         document.all('txtDesProveedor').value = Nombre;
     }
     // Mostrar reporte
     function fMostrarReporte(strURL) {
         var intWidth = screen.width;
         var intHeight = screen.height;
         window.open(strURL, "", "top=0; left=0; width=" + intWidth + "; height=" + intHeight + "; resizable=1;");
     }

     //Muestra el listado de los Activos fijos y las ordenes de trabajo
     function BuscarOrdenServicio() {
         var strCentroCostos = ""
         var retorno = window.showModalDialog("../../intranet/Buscadores/frmBusquedaActivo.aspx?strCentroCostos=" + strCentroCostos, "", "dialogHeight:450px;dialogWidth:450px;center:yes;help:no;");
         if (retorno != "" && retorno != ":") {
             var datos = retorno.split(":");
             var Codigo = datos[0];
             var Nombre = datos[1];
             document.all('txtCodActivo').value = Codigo;
             document.all('txtDesActivo').value = Nombre;
         }
     }

 </script>
</head>
<body>
 <form id="frm_Seguimiento" runat="server" autocomplete="off">
  <center>
   
   <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager> 
   
   <asp:UpdatePanel ID="panContenido" runat="server">
    <ContentTemplate>
 
     <!-- Cabecera -->
     <table border="0" cellspacing="1" cellpadding="1" width="800px">
      <tr>
	   <td style="width:800px;HEIGHT:20px" class="Cabecera">SEGUIMIENTO&nbsp;DE&nbsp;DOCUMENTOS</td>
      </tr>
     </table>

      <!-- Botonera -->
      <table border="0" cellspacing="1" cellpadding="1" width="800px">
       <tr>
        <td style="width:100px;HEIGHT:10px"></td>
        <td style="width:700px;HEIGHT:10px"></td>
       </tr>
       <tr>
	    <td style="width:100px;HEIGHT:10px" class="Etiqueta">
         <asp:ImageButton ID="ibtConsultar" runat="server" Height="16px" Width="16px" ImageUrl="../../intranet/Imagenes/buscar.png" ToolTip="Consultar" ImageAlign="Middle" />
         &nbsp;<asp:LinkButton ID="lnkConsultar" runat="server" BorderStyle="None" Text = "Consultar" ToolTip="Consultar" CssClass = "Informacion"></asp:LinkButton>
        </td>
        <td style="width:100px;HEIGHT:10px" class="Etiqueta">
         <asp:ImageButton ID="ibtExportar" runat="server" Height="16px" Width="16px" ImageUrl="../../intranet/Imagenes/PaginasAI.bmp" ToolTip="Exportar" ImageAlign="Middle" />
          &nbsp;<asp:LinkButton ID="lnkExportar" runat="server" BorderStyle="None" Text = "Exportar" ToolTip="Exportar" CssClass = "Informacion"></asp:LinkButton>
        </td>
        <td style="width:600px;HEIGHT:10px" class="Etiqueta"></td>
       </tr>
      <tr>
       <td style="width:100px;HEIGHT:10px"></td>
       <td style="width:100px;HEIGHT:10px"></td>
       <td style="width:600px;HEIGHT:10px"></td>
      </tr>
     </table>
     
     <!-- Tipo documento -->
     <table style="WIDTH:800px;HEIGHT:20px" border="0" cellpadding="0" cellspacing="0">
      <tr>
       <td style="WIDTH: 100px; HEIGHT:20px" align="left" class="Etiqueta"></td>
       <td style="WIDTH: 700px; HEIGHT:20px" align="left" class="Etiqueta">
        <asp:RadioButtonList runat="server" id="rdlDocumentos" CssClass="cboFormulario" 
               Font-Size="9px" Width="120px" Height="20px" RepeatDirection="Horizontal" AutoPostBack="true">
         <asp:ListItem Selected="False" Text="RQS"></asp:ListItem>
         <asp:ListItem Selected="False" Text="OC/OS"></asp:ListItem>
        </asp:RadioButtonList>
       </td>
      </tr>
     </table>

     <!-- Filtros -->
     <!-- fechas -->
     <table style="WIDTH: 800px; HEIGHT: 20px" border="0" cellpadding="0" cellspacing="0">
      <tr>
	   <td style="WIDTH: 100px; HEIGHT: 20px" align="left" class="Etiqueta">&nbsp;Fecha Inicio:</td>
	   <td style="WIDTH: 150px; HEIGHT: 20px" align="left">
        <asp:textbox id="txtFechaIni" runat="server" Width="100px" CssClass="input" Font-Size="10px" MaxLength="10"></asp:textbox>
         &nbsp;<img onclick="popUpCalendar(this, frm_Seguimiento.txtFechaIni, 'dd/mm/yyyy')" border="0"
		 alt="Seleccionar fecha Inicial" src="../../intranet/Imagenes/Calendario.gif" width="16px" height="16px"  /></td>
       <td style="WIDTH: 100px; HEIGHT: 20px" align="left" class="Etiqueta">&nbsp;Fecha Fin:</td>
       <td style="WIDTH: 150px; HEIGHT: 20px" align="left">
        <asp:textbox id="txtFechaFin" runat="server" Width="100px" CssClass="input" Font-Size= "10px" MaxLength="10"></asp:textbox>
         &nbsp;<img onclick="popUpCalendar(this, frm_Seguimiento.txtFechaFin, 'dd/mm/yyyy')" border="0"
		 alt="Seleccionar fecha Final" src="../../intranet/Imagenes/Calendario.gif" width="16px" height="16px"/>
       </td>
       <td style="WIDTH: 300px; HEIGHT: 20px" align="left"></td>
      </tr>
     </table>
     
     <table style="WIDTH:800px; HEIGHT:20px" border="0" cellpadding="0" cellspacing="0">
      <!-- Centro Costo -->
      <tr>
	   <td style="WIDTH: 100px; HEIGHT: 20px" align="left" class="Etiqueta">&nbsp;Area:</td>
	   <td style="WIDTH: 150px; HEIGHT: 20px" align="left">
        <asp:textbox id="txtCodCentroCosto" runat="server" Width="100px" CssClass="input" Font-Size= "10px" MaxLength = "7"></asp:textbox>
        &nbsp;<input style="WIDTH:20px; HEIGHT:20px" id="btnCentroCosto" class="boton" onclick="javascript:fnc_BuscarCentroCosto();" value="..." type="button" name="btnCentroCosto" />
       </td>
       <td style="WIDTH: 550px; HEIGHT: 20px" align="left">
        <asp:TextBox Id="txtDesCentroCosto" Text="" runat="server" CssClass="Etiqueta" Width="540px" Font-Size="9px" BorderStyle="None"></asp:TextBox>
       </td>
      </tr>
      <!-- Solicitante -->
      <tr>
       <td style="WIDTH: 100px; HEIGHT: 20px" align="left" class="Etiqueta">&nbsp;Solicitante:</td>
       <td style="WIDTH: 150px; HEIGHT: 20px" align="left">
        <asp:textbox id="txtCodSolicitante" runat="server" Width="100px" CssClass="input" Font-Size= "10px" MaxLength = "5"></asp:textbox>
         &nbsp;<input style="WIDTH:20px; HEIGHT:20px" id="btnSolicitante" class="boton" onclick="javascript:fnc_Buscartrabajadores();" value="..." type="button" name="btnSolicitante" />
       </td>
       <td style="WIDTH: 550px; HEIGHT: 20px" align="left">
        <asp:TextBox Id="txtDesSolicitante" Text="" runat="server" CssClass="Etiqueta" Width="540px" Font-Size="9px" BorderStyle="None"></asp:TextBox>
       </td>
      </tr>
      <!-- Proveedor -->
      <tr>
       <td style="WIDTH: 100px; HEIGHT: 20px" align="left" class="Etiqueta">&nbsp;Proveedor:</td>
       <td style="WIDTH: 150px; HEIGHT: 20px" align="left">
        <asp:textbox id="txtCodProveedor" runat="server" Width="100px" CssClass="input" Font-Size= "10px" MaxLength = "20"></asp:textbox>
         &nbsp;<input style="WIDTH:20px; HEIGHT:20px" id="btnProveedor" class="boton" onclick="javascript:fnc_BuscarProveedor();" value="..." type="button" name="btnProveedor">
       </td>
       <td style="WIDTH: 550px; HEIGHT: 20px" align="left">
        <asp:TextBox Id="txtDesProveedor" Text="" runat="server" CssClass="Etiqueta" Width="540px" Font-Size="9px" BorderStyle="None"></asp:TextBox>
       </td>
      </tr>
      <!-- Activo/CTC -->
      <tr>
       <td align="left" class="Etiqueta" style="WIDTH: 100px; HEIGHT: 20px">&nbsp;Activo/CTC:</td>
       <td align="left" style="WIDTH: 150px; HEIGHT: 20px">
        <asp:TextBox ID="txtCodActivo" runat="server" Width="100px" CssClass="input" Font-Size="10px" MaxLength="6"></asp:TextBox>
        &nbsp;<input id="btnServicio" class="boton" name="btnServicio" onclick="javascript:BuscarOrdenServicio();" style="WIDTH: 20px; HEIGHT: 20px" type="button" value="..."></input></td>
       <td align="left" style="WIDTH:540px; HEIGHT:20px">
        <asp:textbox id="txtDesActivo" Text="" runat="server" CssClass="Etiqueta" Width="540px" Font-Size="9px" BorderStyle="None"></asp:textbox>
       </td>
      </tr>
     </table>
     <!-- Numero de documento -->
     <table style="WIDTH:800px;HEIGHT:20px" border="0" cellpadding="0" cellspacing="0">
      <tr>
	   <td style="WIDTH: 100px; HEIGHT:20px" align="left" class="Etiqueta">&nbsp;Num. Doc.:
       </td>
	   <td style="WIDTH: 700px; HEIGHT:20px" align="left">
        <asp:textbox id="txtSerie" runat="server" width="30px" Font-Size= "10px" MaxLength = "5"  CssClass="input"></asp:textbox>
        &nbsp;<asp:TextBox ID="txtNumeroDocumento" runat="server" width="80px" Font-Size= "10px" MaxLength = "10"></asp:TextBox>
       </td>
      </tr>
     </table>
     <!-- Tipo de Orden -->
     <table style="WIDTH:800px;HEIGHT:20px" border="0" cellpadding="0" cellspacing="0">
      <tr>
       <td style="WIDTH: 100px; HEIGHT:20px" align="left" class="Etiqueta">&nbsp;Tipo de Orden:</td>
       <td style="WIDTH: 700px; HEIGHT:20px" align="left">
        <asp:DropDownList ID="ddlTipo" runat="server" CssClass="cboFormulario" Font-Size="10px" Width = "130px" AutoPostBack="false">
         <asp:ListItem Value="ALL">TODOS</asp:ListItem>
	     <asp:ListItem Value="ART">ARTICULO</asp:ListItem>
         <asp:ListItem Value="SER">SERVICIO</asp:ListItem>
        </asp:DropDownList>
       </td>
      </tr>
     </table>
     <!-- Importacion -->
     <table style="WIDTH:800px;HEIGHT:20px" border="0" cellpadding="0" cellspacing="0">
      <tr>
       <td style="WIDTH: 100px; HEIGHT:20px" align="left" class="Etiqueta"></td>
       <td style="WIDTH: 700px; HEIGHT:20px" align="left">
        <asp:CheckBox ID="chkImportacion" runat="server" CssClass="input" Text = "Importacion" Checked="false" />
       </td>
      </tr>
     </table>
     
     <!--- Imagen de Espera --->
     <table style="WIDTH:800px;HEIGHT:20px" border="0" cellpadding="0" cellspacing="0">
      <tr>
       <td>
        <script language="JavaScript" src="../Js/jsUpdateProgress.js" type="text/jscript" ></script>    
        <asp:UpdateProgress ID="UpdateProg1" DisplayAfter="0" runat="server"  >   
         <ProgressTemplate>     
         <div id="divLoad" class="CssLoadNormal">
          <br />
          <img src="../images/Loading.gif" style="vertical-align:middle; height: 20px; width:20px" alt="" />
          <br/>
          Procesando...
          </div>   
         </ProgressTemplate> 
        </asp:UpdateProgress>   
       </td>
      </tr>
     </table>

     <!-- Mensaje -->
     <table style="WIDTH:800px;HEIGHT:20px" border="0" cellpadding="0" cellspacing="0">
      <tr>
       <td style="WIDTH: 500px; HEIGHT:20px" align="left">
        <asp:Label ID="lblMensaje" runat="server" Text = "" CssClass="error"></asp:Label>
       </td>
       <td style="WIDTH: 300px; HEIGHT:20px" align="right">
        <asp:Label ID="lblNumDoc" runat="server" Text = "" CssClass="informacion" Font-Bold="true"></asp:Label>
       </td>
      </tr>
      <tr><td style="WIDTH: 800px; HEIGHT:20px" align="left"></td>
      </tr>
     </table>

     <table style="WIDTH:800px;HEIGHT:20px" border="0" cellpadding="0" cellspacing="0">
      
      <%-- Grilla--%>
      <tr>
       <td>
        <asp:datagrid id="drgListado" runat="server" Width="800px" AutoGenerateColumns="False">
        <AlternatingItemStyle CssClass="GridAlternateItem"></AlternatingItemStyle>
	     <ItemStyle CssClass="GridItem"></ItemStyle>
	     <HeaderStyle CssClass="gridheader"></HeaderStyle>
         <Columns>
		
         <%-- Ver seguimiento Doc.--%>
         <asp:TemplateColumn HeaderText="">
          <HeaderStyle HorizontalAlign="Center" Width="2px" Font-Bold="true"></HeaderStyle>
	      <ItemTemplate>
           <asp:ImageButton id="ibtSeg" runat="server" ImageUrl="../../intranet/Imagenes/buscar.gif" ToolTip="Ver seguimiento" CommandName="Seg" Width="16px" Height="16px"></asp:ImageButton>
          </ItemTemplate>
	     </asp:TemplateColumn>

         <%-- Ver detalle Req.--%>
         <asp:TemplateColumn HeaderText="">
          <HeaderStyle HorizontalAlign="Center" Width="20px" Font-Bold="true"></HeaderStyle>
	      <ItemTemplate>
           <asp:ImageButton id="ibtDetReq" runat="server" ImageUrl="~/images/Lupa.png" Width = "16px" Height="16px" ToolTip="Ver detalle de la requisicion" CommandName="Req"></asp:ImageButton>
          </ItemTemplate>
	     </asp:TemplateColumn>

         <%-- Ver formato OC/OS.--%>
         <asp:TemplateColumn HeaderText="OC">
          <HeaderStyle HorizontalAlign="Center" Width="20px" Font-Bold="true"></HeaderStyle>
	      <ItemTemplate>
           <asp:ImageButton id="ibtVerOC" runat="server" ImageUrl="~/images/ocos.ico" Width = "16px" Height="16px" ToolTip="Ver formato de OC/OS" CommandName="VerOC"></asp:ImageButton>
          </ItemTemplate>
	     </asp:TemplateColumn>
         
         <%-- Ver formato req.--%>
         <asp:TemplateColumn HeaderText="Req.">
          <HeaderStyle HorizontalAlign="Center" Width="20px" Font-Bold="true"></HeaderStyle>
	      <ItemTemplate>
           <asp:ImageButton id="ibtVerRq" runat="server" ImageUrl="~/images/req.ico" Width = "16px" Height="16px" ToolTip="Ver formato de requisicion" CommandName="VerRq"></asp:ImageButton>
          </ItemTemplate>
	     </asp:TemplateColumn>

         <%-- Numero de OC/OS--%>
         <asp:TemplateColumn HeaderText="# OC/OS">
		  <HeaderStyle HorizontalAlign="Center" Width="100px" Font-Bold="true"></HeaderStyle>
		  <ItemStyle Font-Size="8px" HorizontalAlign="center"></ItemStyle>
		  <ItemTemplate>
		   <asp:Label id="lblNumDoc" runat="server" Font-Size="8px" Text='<%# DataBinder.Eval(Container, "DataItem.vch_NumOrdenCompra") %>'></asp:Label>
		  </ItemTemplate>
	     </asp:TemplateColumn>
 
         <%-- Numero de req--%>
         <asp:TemplateColumn HeaderText="Requisicion">
		  <HeaderStyle HorizontalAlign="Center" Width="100px" Font-Bold="true"></HeaderStyle>
		  <ItemStyle Font-Size="8px" HorizontalAlign="center" VerticalAlign="Middle"></ItemStyle>
		  <ItemTemplate>
		   <asp:Label id="lblReq" runat="server" Font-Size="8px" Text='<%# DataBinder.Eval(Container, "DataItem.vch_NumRequisicion") %>'></asp:Label>
		  </ItemTemplate>
	     </asp:TemplateColumn>

         <%-- Cotizaacion--%>
         <asp:TemplateColumn HeaderText="Cotizacion">
		  <HeaderStyle HorizontalAlign="Center" Width="80px" Font-Bold="true"></HeaderStyle>
		  <ItemStyle Font-Size="8px" HorizontalAlign="center"></ItemStyle>
		  <ItemTemplate>
		   <asp:Label id="lblCotizacion" runat="server" Font-Size="8px" Text='<%# DataBinder.Eval(Container, "DataItem.vch_NumCotizacion") %>' Visible="false"></asp:Label>
           <asp:ImageButton id="btnCotizacion" runat="server" ImageUrl="" Visible="false" Width="16px" Height="16px" ></asp:ImageButton>
		  </ItemTemplate>
	     </asp:TemplateColumn>

         <%-- Aprobacion 1--%>
         <asp:TemplateColumn HeaderText="Aprobador [1]">
		  <HeaderStyle HorizontalAlign="Center" Width="180px" Font-Bold="true"></HeaderStyle>
		  <ItemStyle Font-Size="8px" HorizontalAlign="left"></ItemStyle>
		  <ItemTemplate>
           <asp:ImageButton id="btnSituacionA" runat="server" ImageUrl= "" Visible="false" Width="16px" Height="16px" ></asp:ImageButton>
		   <asp:Label id="lblAprobadorA" runat="server" Font-Size="8px" Text='<%# DataBinder.Eval(Container, "DataItem.vch_AprobadoA") %>'></asp:Label>
           <asp:Label id="lblSituacionA" runat="server" Font-Size="8px" Visible="false" Text='<%# DataBinder.Eval(Container, "DataItem.vch_SituacionA") %>'></asp:Label>
		  </ItemTemplate>
	     </asp:TemplateColumn>

         <%-- Aprobacion 2--%>
         <asp:TemplateColumn HeaderText="Aprobador [2]">
		  <HeaderStyle HorizontalAlign="Center" Width="180px" Font-Bold="true"></HeaderStyle>
		  <ItemStyle Font-Size="8px" HorizontalAlign="left"></ItemStyle>
		  <ItemTemplate>
           <asp:ImageButton id="btnSituacionB" runat="server" ImageUrl= "" Visible="false" Width="16px" Height="16px" ></asp:ImageButton>
		   <asp:Label id="lblAprobadorB" runat="server" Font-Size="8px" Text='<%# DataBinder.Eval(Container, "DataItem.vch_AprobadoB") %>'></asp:Label>
           <asp:Label id="lblSituacionB" runat="server" Font-Size="8px" Visible="false" Text='<%# DataBinder.Eval(Container, "DataItem.vch_SituacionB") %>'></asp:Label>
		  </ItemTemplate>
	     </asp:TemplateColumn>

         <%-- Aprobacion 3--%>
         <asp:TemplateColumn HeaderText="Aprobador [3]">
		  <HeaderStyle HorizontalAlign="Center" Width="180px" Font-Bold="true"></HeaderStyle>
		  <ItemStyle Font-Size="8px" HorizontalAlign="left"></ItemStyle>
		  <ItemTemplate>
           <asp:ImageButton id="btnSituacionC" runat="server" ImageUrl= "" Visible="false" Width="16px" Height="16px" ></asp:ImageButton>
		   <asp:Label id="lblAprobadorC" runat="server" Font-Size="8px" Text='<%# DataBinder.Eval(Container, "DataItem.vch_AprobadoC") %>'></asp:Label>
           <asp:Label id="lblSituacionC" runat="server" Font-Size="8px" Visible="false" Text='<%# DataBinder.Eval(Container, "DataItem.vch_SituacionC") %>'></asp:Label>
		  </ItemTemplate>
	     </asp:TemplateColumn>

         <%-- Ingreso NM--%>
         <asp:TemplateColumn HeaderText="Ingreso NM">
		  <HeaderStyle HorizontalAlign="Center" Width="80px" Font-Bold="true"></HeaderStyle>
		  <ItemStyle Font-Size="8px" HorizontalAlign="center"></ItemStyle>
		  <ItemTemplate>
		   <asp:Label id="lblIngreso" runat="server" Font-Size="8px" Text='<%# DataBinder.Eval(Container, "DataItem.int_NotasIngreso") %>' Visible="false"></asp:Label>
           <asp:ImageButton id="btnIngreso" runat="server" ImageUrl= "" Visible="false" Width="16px" Height="16px" ></asp:ImageButton>
		  </ItemTemplate>
	     </asp:TemplateColumn>

         <%-- Conformidad--%>
         <asp:TemplateColumn HeaderText="Conformidad">
		  <HeaderStyle HorizontalAlign="Center" Width="80px" Font-Bold="true"></HeaderStyle>
		  <ItemStyle Font-Size="8px" HorizontalAlign="center"></ItemStyle>
		  <ItemTemplate>
		   <asp:Label id="lblConformidad" runat="server" Font-Size="8px" Text='<%# DataBinder.Eval(Container, "DataItem.vch_Conformidad") %>' Visible="false"></asp:Label>
           <asp:ImageButton id="btnConformidad" runat="server" ImageUrl= "" Visible="false" Width="16px" Height="16px" ></asp:ImageButton>
		  </ItemTemplate>
	     </asp:TemplateColumn>

	   </Columns>
      </asp:datagrid>
     </td>
    </tr>
   </table>

   </ContentTemplate>
    <Triggers> 
     <asp:AsyncPostBackTrigger ControlID ="ibtConsultar" EventName="click" />
     <asp:AsyncPostBackTrigger ControlID ="lnkConsultar" EventName="click" />
     <asp:AsyncPostBackTrigger ControlID ="ibtExportar" EventName="click" />
     <asp:AsyncPostBackTrigger ControlID ="lnkExportar" EventName="click" />
    </Triggers>
   </asp:UpdatePanel>
  </center>
 </form>
</body>
</html>
