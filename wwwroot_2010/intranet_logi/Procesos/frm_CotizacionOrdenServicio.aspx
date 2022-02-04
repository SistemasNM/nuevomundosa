<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="frm_CotizacionOrdenServicio.aspx.vb" Inherits="intranet_logi.frm_CotizacionOrdenServicio" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
 <title>Cotizaciones</title>
 <base target="_self"/>
 <link href="../css/NM0001.css" type="text/css" rel="stylesheet"/>
 <script language="javascript" type="text/javascript" src="../../intranet/JS/jsCalendario_N3.js"></script>
 <script language="javascript" type="text/javascript" src="../../intranet/JS/functions.js"></script>
 
 <style type="text/css">
     a:link 
    {
        color: #42413C;
        text-decoration: none;
         height: 12px;
     }
    a:visited 
    {
        color: #6E6C64;
        text-decoration: none;
    }
    a:hover, a:active, a:focus 
    {
        text-decoration: underline;
	}
 </style>

 <script language="javascript" type="text/javascript">
     // Funcion Busca Empleados
     function fnc_Buscartrabajadores() {
         var tipo = "EMP";
         var retorno = window.showModalDialog("../../intranet/Buscadores/PLA_0001.aspx?strTipo=" + tipo, "", "dialogHeight:450px;dialogWidth:450px;center:yes;help:no;");
         if (retorno != "" && retorno != ":") {
             var datos = retorno.split(":");
             var codigo = datos[0];
             var nombre = datos[1];
             document.all('txtSolicitante').value = codigo;
             document.all('txtDesSolicitante').value = nombre;
             document.all('hndDesSolicitante').value = nombre;
         }
     }

     // Funcion Busca Centro de Costos
     function fnc_BuscarCentroCosto() {
         var retorno = window.showModalDialog("../../intranet/Buscadores/LOG_0001.aspx", "", "dialogHeight:450px;dialogWidth:450px;center:yes;help:no;");
         if (retorno != "" && retorno != ":") {
             var datos = retorno.split(":");
             var Codigo = datos[0];
             var Nombre = datos[1];
             document.all('txtCentroCosto').value = Codigo;
             document.all('txtDesCentroCosto').value = Nombre;
             document.all('hndDesCentroCosto').value = Nombre;
         }
     }

     //Adjunta files
     function fnc_AdjuntarDocs(strNumeroDoc, strSecuencia) {
         var pstrTipoDoc = "RQS"
         var pstrNumeroDoc = strNumeroDoc
         var pstrSecuencia = strSecuencia
         var pstrMantenimiento = "1"
         var retorno = window.showModalDialog("frm_RegistrarArchivoAdjunto.aspx?pstrTipoDoc=" + pstrTipoDoc + "&pstrNumeroDoc=" + pstrNumeroDoc + "&pstrSecuencia=" + pstrSecuencia + "&pstrMantenimiento=" + pstrMantenimiento, "Adjuntar archivo", "dialogHeight:400px;dialogWidth:720px;center:yes;help:no;");
         return false;
     }

     //Lista fnc_AdjuntarDocs
     function fnc_ListarDocsAdjuntos(strNumeroDoc) {
         var pstrTipoDoc = "RQS"
         var pstrNumeroDoc = strNumeroDoc
         var pstrSecuencia = " "
         var retorno = window.showModalDialog("frm_ListadoArchivoAdjunto.aspx?pstrTipoDoc=" + pstrTipoDoc + "&pstrNumeroDoc=" + pstrNumeroDoc + "&pstrSecuencia=" + pstrSecuencia, "Listar Adjuntos", "dialogHeight:400px;dialogWidth:720px;center:yes;help:no;");
         return false;
     }

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

         // Lista de doc adjuntos
         function fnc_aprobarmasivo(pstr_id, pstr_documento) {
             var lstr_valores = document.all('hdnaprobarmasivo').value;
             if (pstr_id.checked == true) {
                 lstr_valores = lstr_valores + pstr_documento + ',';
             } else {
                 pstr_documento = pstr_documento + ',';
                 lstr_valores = replaceAll(lstr_valores, pstr_documento, '');
             }
             document.all('hdnaprobarmasivo').value = lstr_valores;
         }
         
         // Mostrar reporte
         function fMostrarReporte(strURL) {
             var intWidth = screen.width;
             var intHeight = screen.height;
             window.open(strURL, "", "top=0; left=0; width=" + intWidth + "; height=" + intHeight + "; resizable=1;");
         }
         
 </script>
</head>
<body>
 <form id="frm_Cotizacion"  runat="server" method="post" autocomplete="off" >
  <center>
   <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager> 
    
    <!-- Cabecera -->
    <table border="0" cellspacing="1" cellpadding="1" width="800px">
     <tr>
	  <td style="width:800px;HEIGHT:20px" class="Cabecera">COTIZACION&nbsp;DE&nbsp;DOCUMENTOS</td>
     </tr>
    </table>

    <asp:UpdatePanel ID="panContenido" runat="server" UpdateMode="Always">
    <ContentTemplate>
    
    <%--Pantalla 1: Consulta --%>
    <asp:Panel ID="pnlListado" runat="server">
     <!-- Botonera -->
     <table border="0" cellspacing="1" cellpadding="1" width="800px">
      <tr>
       <td style="width:100px;HEIGHT:10px"></td>
       <td style="width:100px;HEIGHT:10px"></td>
       <td style="width:600px;HEIGHT:10px"></td>
      </tr>
      <tr>
	   <td style="width:100px;HEIGHT:10px" align="left" class="Etiqueta">
        &nbsp;<asp:ImageButton ID="ibtConsultar" runat="server" Height="16px" Width="16px" ImageUrl="../../intranet/Imagenes/buscar.png" ToolTip="Consultar" ImageAlign="Middle"/>  
        &nbsp;<asp:LinkButton ID="lnkConsultar" runat="server" BorderStyle="None" Text = "Consultar" ToolTip="Consultar" CssClass = "Informacion"></asp:LinkButton>
       </td>
       <td style="width:100px;HEIGHT:10px" align="left" class="Etiqueta">
        &nbsp;<asp:ImageButton ID="ibtCotizar" runat="server" Height="16px" Width="16px" ImageUrl="~/images/cotizar.bmp" ToolTip="Cotizar" ImageAlign="Middle" />  
        &nbsp;<asp:LinkButton ID="lnkCotizar" runat="server" BorderStyle="None" Text = "Cotizar" ToolTip="Cotizar" CssClass = "Informacion"></asp:LinkButton>
       </td>
       <td style="width:600px;HEIGHT:10px" align="left" class="Etiqueta">
        &nbsp;<asp:ImageButton ID="ibtRoporte" runat="server" Height="16px" Width="16px" ImageUrl="../../intranet/Imagenes/prntpv_16.png" ToolTip="Reporte" ImageAlign="Middle" Visible ="false"/>  
        &nbsp;<asp:LinkButton ID="lnkReporte" runat="server" BorderStyle="None" Text = "Reporte" ToolTip="Reporte" CssClass = "Informacion" Visible = "false"></asp:LinkButton>
       </td>
      </tr>
      <tr>
       <td style="width:100px;HEIGHT:10px"></td>
       <td style="width:100px;HEIGHT:10px"></td>
       <td style="width:600px;HEIGHT:10px"></td>
      </tr>
     </table>
  
     <!-- Filtros -->
     <!-- Fechas, Estado -->
     <table style="WIDTH: 800px; HEIGHT: 20px" border="0" cellpadding="0" cellspacing="0">
      <tr>
	   <td style="WIDTH: 100px; HEIGHT: 20px" align="left" class="Etiqueta">&nbsp;Fecha Inicio:</td>
	   <td style="WIDTH: 150px; HEIGHT: 20px" align="left">
       <asp:textbox id="txtFechaIni" runat="server" Width="100px" CssClass="input" Font-Size= "10px" MaxLength="10"></asp:textbox>
       &nbsp;<img onclick="popUpCalendar(this, frm_Cotizacion.txtFechaIni, 'dd/mm/yyyy')" border="0"
 		alt="Seleccionar fecha Inicial" src="../../intranet/Imagenes/Calendario.gif" width="16" height="16"  /></td>
       <td style="WIDTH: 100px; HEIGHT: 20px" align="left" class="Etiqueta">&nbsp;Fecha Fin :</td>
       <td style="WIDTH: 150px; HEIGHT: 20px" align="left">
        <asp:textbox id="txtFechaFin" runat="server" Width="100px" CssClass="input" Font-Size= "10px" MaxLength="10"></asp:textbox>
         &nbsp;<img onclick="popUpCalendar(this, frm_Cotizacion.txtFechaFin, 'dd/mm/yyyy')" border="0"
		 alt="Seleccionar fecha Final" src="../../intranet/Imagenes/Calendario.gif" width="16" height="16"/>
       </td>
	   <td style="WIDTH:50px; HEIGHT:20px" align="left"  class="Etiqueta">&nbsp;Estado:</td>
       <td style="WIDTH:250px; HEIGHT:20px" align="left">
        <asp:DropDownList ID="ddlEstados" runat="server" CssClass="cboFormulario" Font-Size="10px" Width = "100px" AutoPostBack="false">
         <asp:ListItem Value="APR">APROBADO</asp:ListItem>
	     <asp:ListItem Value="COT">COTIZADO</asp:ListItem>
       </asp:DropDownList></td>
      </tr>
     </table>

     <!-- Centro de costo -->
     <table style="WIDTH:800px; HEIGHT:20px" border="0" cellpadding="0" cellspacing="0">
      <tr>
	   <td style="WIDTH: 100px; HEIGHT: 20px" align="left" class="Etiqueta">&nbsp;Area:</td>
	   <td style="WIDTH: 150px; HEIGHT: 20px" align="left">
        <asp:textbox id="txtCentroCosto" runat="server" Width="100px" CssClass="input" Font-Size= "10px" MaxLength = "10"></asp:textbox>
        &nbsp;<input style="WIDTH:20px; HEIGHT:20px" id="btnCentroCosto" onclick="javascript:fnc_BuscarCentroCosto();" value="..." type="button" name="btnCentroCosto" class="Boton"/>
       </td>
       <td style="WIDTH: 550px; HEIGHT: 20px" align="left">
        &nbsp;<asp:TextBox ID="txtDesCentroCosto" runat="server" Text="" Width="540px" 
            CssClass="Etiqueta" BorderStyle="None" Font-Italic="False"></asp:TextBox>
       </td>
      </tr>
     </table>

     <!-- Solicitado -->
     <table style="WIDTH:800px; HEIGHT:20px" border="0" cellpadding="0" cellspacing="0">
      <tr>
	   <td style="WIDTH: 100px; HEIGHT: 20px" align="left" class="Etiqueta">&nbsp;Solicitado Por:</td>
	   <td style="WIDTH: 150px; HEIGHT: 20px" align="left">
        <asp:textbox id="txtSolicitante" runat="server" Width="100px" CssClass="input" Font-Size= "10px" MaxLength = "5"></asp:textbox>
        &nbsp;<input style="WIDTH:20px; HEIGHT:20px" id="btnSolicitante" onclick="javascript:fnc_Buscartrabajadores();" value="..." type="button" name="btnSolicitante" class="Boton"/>
       </td>
       <td style="WIDTH: 550px; HEIGHT: 20px" align="left">
        &nbsp;<asp:textbox id="txtDesSolicitante" text="" runat="server" Font-Bold="True" Width="540px" Font-Size="10px" CssClass="Etiqueta" BorderStyle="None"></asp:textbox>
       </td>
      </tr>
     </table>

     <!-- Nuemero de documento -->
     <table style="WIDTH:800px;HEIGHT:20px" border="0" cellpadding="0" cellspacing="0">
      <tr>
	   <td style="WIDTH: 100px; HEIGHT:20px" align="left" class="Etiqueta">&nbsp;Num. de Doc:</td>
	   <td style="WIDTH: 150px; HEIGHT:20px" align="left">
        <asp:textbox id="txtSerie" runat="server" width="30px" Font-Size= "10px" MaxLength = "5"  CssClass="input"></asp:textbox>
        &nbsp;<asp:TextBox ID="txtNumeroDocumento" runat="server" width="80px" Font-Size= "10px" MaxLength = "10" CssClass="input"></asp:TextBox>
       </td>
       <td style="WIDTH:550px; HEIGHT:20px" align="left"></td>
      </tr>
     </table>

     <!--- Imagen de espera -->
     <%--<script language="JavaScript" src="../Js/jsUpdateProgress.js" type="text/jscript" ></script>    --%>
      <asp:UpdateProgress ID="UpdateProg1" DisplayAfter="0" runat="server">
       <ProgressTemplate>     
        <div id="divLoad" class="CssLoadNormal">
         <br />
         <img src="../images/Loading.gif" style="vertical-align:middle; height: 20px; width:20px" alt="" />
         <br/>Procesando...
        </div>
       </ProgressTemplate> 
      </asp:UpdateProgress> 
  
      <!-- mensaje, controles ocultos, num registros -->
      <table style="WIDTH:800px;HEIGHT:20px" border="0" cellpadding="0" cellspacing="0">
       <tr>
        <td style="WIDTH: 800px" align="left">
         <asp:Label ID="lblMensaje" runat="server" CssClass="error"></asp:Label>
         <input style="WIDTH: 10px; HEIGHT: 10px" id="hdnaprobarmasivo" size="1" type="hidden" name="hdnaprobarmasivo" runat="server"/>
         <input style="WIDTH: 10px; HEIGHT: 10px" id="hndDesCentroCosto" size="1" type="hidden" name="hndDesCentroCosto" runat="server"/>
         <input style="WIDTH: 10px; HEIGHT: 10px" id="hndDesSolicitante" size="1" type="hidden" name="hndDesSolicitante" runat="server"/>
        </td>
       </tr>
       <tr>
	    <td style="WIDTH: 800px" align="right">
         <asp:Label ID="lblNumReq" runat="server" Font-Size="9px" Font-Bold="true" Text=""></asp:Label>
        </td>
       </tr>
      </table>
      <!-- Grilla -->
      <table style="WIDTH:800px;HEIGHT:20px" border="0" cellpadding="0" cellspacing="0">
       <tr>
	    <td style="WIDTH: 800px" align="left">
         <asp:datagrid id="drgListaCotizacion" runat="server" Width="800px" AutoGenerateColumns="False">
          <AlternatingItemStyle CssClass="GridAlternateItem"></AlternatingItemStyle>
	      <ItemStyle CssClass="GridItem"></ItemStyle>
	      <HeaderStyle CssClass="gridheader"></HeaderStyle>
          <Columns>
		
           <%--Ver detalle --%>
           <asp:TemplateColumn HeaderText="Det.">
            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="20px" Font-Bold="true"></HeaderStyle>
	        <ItemTemplate>
             <asp:ImageButton id="ibtVerDet" runat="server" ImageUrl="../../intranet/Imagenes/buscar.gif" ToolTip="Ver detalle del documento" CommandName="VerDet" Width="16px" Height="16px"></asp:ImageButton>
            </ItemTemplate>
	       </asp:TemplateColumn>

           <%--Num. adjuntos del doc. --%>
           <asp:TemplateColumn HeaderText="Adj">
            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="20px" Font-Bold="true"></HeaderStyle>
		    <ItemTemplate>
             <asp:ImageButton id="ibtVerAdj" runat="server" ImageUrl="~/images/adjuntos.ico" ToolTip="Ver documentos adjuntos" Width="16px" Height="16px"></asp:ImageButton>
             <asp:Label id="lblNumAdj" runat="server" Font-Size="8px" Text='<%# DataBinder.Eval(Container, "DataItem.Adjuntos") %>' visible ="false"></asp:Label>
            </ItemTemplate>
	       </asp:TemplateColumn>

           <%--Check Corizar masivo. --%>
           <asp:TemplateColumn HeaderText="Cot.">
            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="20px" Font-Bold="true"></HeaderStyle>
		    <ItemTemplate><asp:CheckBox id="chkCotMasivo" runat="server" ToolTip="Marque para cotizar" Width="16px" Height="16px"></asp:CheckBox></ItemTemplate>
		   </asp:TemplateColumn>
        
           <%--Numero de doc. --%>
           <asp:TemplateColumn HeaderText="Num. Doc.">
		    <HeaderStyle HorizontalAlign="Center" Width="80px" Font-Bold="true"></HeaderStyle>
		    <ItemStyle Font-Size="8px" HorizontalAlign="Center"></ItemStyle>
		    <ItemTemplate>
		     <asp:Label id="lblNumDoc" runat="server" Font-Size="8px" Text='<%# DataBinder.Eval(Container, "DataItem.vch_NumeroDocumento") %>'></asp:Label>
		    </ItemTemplate>
		   </asp:TemplateColumn>

           <%--Fecha de doc. --%>
           <asp:BoundColumn DataField="dtm_FechaDocumento" HeaderText="Fecha Doc." >
            <HeaderStyle HorizontalAlign="Center" Width="60px" VerticalAlign="Middle" Font-Bold="true"></HeaderStyle>
            <ItemStyle Font-Size="8px" HorizontalAlign="center" VerticalAlign="Middle"></ItemStyle>
           </asp:BoundColumn>

		   <%--Centro costo --%>
           <asp:BoundColumn DataField="vch_AreaSolicitante" HeaderText="Area Solic.">
            <HeaderStyle HorizontalAlign="Center" Width="200px" VerticalAlign="Middle" Font-Bold="true"></HeaderStyle>
		    <ItemStyle Font-Size="8px" HorizontalAlign="left" VerticalAlign="Middle"></ItemStyle>
           </asp:BoundColumn>

           <%--Solicitante --%>
           <asp:BoundColumn DataField="Solicitante" HeaderText="Solicitante" >
            <HeaderStyle HorizontalAlign="Center" Width="150px" VerticalAlign="Middle" Font-Bold="true"></HeaderStyle>
		    <ItemStyle Font-Size="8px" HorizontalAlign="left" VerticalAlign="Middle"></ItemStyle>
           </asp:BoundColumn>

           <%--Observaciones. --%>
           <asp:BoundColumn DataField="vch_Observaciones" HeaderText="Observaciones" >
            <HeaderStyle HorizontalAlign="Center" Width="250px" VerticalAlign="Middle" Font-Bold="true"></HeaderStyle>
		    <ItemStyle Font-Size="8px" HorizontalAlign="left" VerticalAlign="Middle"></ItemStyle>
           </asp:BoundColumn>
		
	    </Columns>
       </asp:datagrid>
      </td>
     </tr>
    </table>
   </asp:Panel>
   
   <%--Pantalla 2: Detalle --%>
   <asp:Panel ID="pnlDetalle" runat="server" Width="800px">
    <table style="WIDTH:500px" border="0" cellpadding="0" cellspacing="0">
     <tr> 
      <td style="width:500px;HEIGHT:10px"></td>
     </tr>
     <tr> 
      <td style="width:500px;HEIGHT:20px" class="Cabecera">DETALLE&nbsp;DE&nbsp;REQUISICION</td>
     </tr>
     <tr>
      <td style="WIDTH:500px;height:10px" align="left">
       <asp:TextBox ID="txtNumero" Text="" runat="server" Width="500px" CssClass="Etiqueta" BorderStyle="None"></asp:TextBox>
      </td>
      </tr>
      <tr>
       <td>
        <asp:datagrid id="drgDetalle" runat="server" Width="500px" AutoGenerateColumns="False">
         <AlternatingItemStyle CssClass="GridAlternateItem"></AlternatingItemStyle>
	      <ItemStyle CssClass="GridItem"></ItemStyle>
	      <HeaderStyle CssClass="gridheader"></HeaderStyle>
          <Columns>
           
           <%--Secuencia --%>
           <asp:TemplateColumn HeaderText="Sec">
            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="30px" Font-Bold="true"></HeaderStyle>
		    <ItemTemplate>
             <asp:Label id="lblSec" runat="server" Font-Size="8px" Text='<%# DataBinder.Eval(Container, "DataItem.Secuencia") %>' visible ="true"></asp:Label>
              <asp:Label id="lblCot" runat="server" Font-Size="8px" Text='<%# DataBinder.Eval(Container, "DataItem.Cotizado") %>' visible ="false"></asp:Label>
            </ItemTemplate>
	       </asp:TemplateColumn>
           
           <%--Descripcion --%>
           <asp:BoundColumn DataField="Descripcion" HeaderText="Descripcion" >
            <HeaderStyle HorizontalAlign="Center" Width="380px" VerticalAlign="Middle" Font-Bold="true"></HeaderStyle>
		    <ItemStyle Font-Size="8px" HorizontalAlign="left" VerticalAlign="Middle"></ItemStyle>
           </asp:BoundColumn>

           <%--Cantidad --%>
           <asp:BoundColumn DataField="Cantidad" HeaderText="Cant." DataFormatString="{0:#,##0.00}">
            <HeaderStyle HorizontalAlign="Center" Width="50px" VerticalAlign="Middle" Font-Bold="true"></HeaderStyle>
		    <ItemStyle Font-Size="8px" HorizontalAlign="Right" VerticalAlign="Middle"></ItemStyle>
           </asp:BoundColumn>

           <%--boton cotizar item --%>
           <asp:TemplateColumn HeaderText="Cot.">
            <HeaderStyle HorizontalAlign="Center" Width="20px" Font-Bold="true"></HeaderStyle>
	        <ItemTemplate>
             <asp:ImageButton id="ibtCotDet" runat="server" ImageUrl="~/images/cotizar.bmp" ToolTip="Cotizar item" CommandName="Cotizar" Width="16px" Height="16px"></asp:ImageButton>
            </ItemTemplate>
	       </asp:TemplateColumn>

           <%--boton adjuntar --%>
           <asp:TemplateColumn HeaderText="Adj.">
            <HeaderStyle HorizontalAlign="Center" Width="20px" Font-Bold="true"></HeaderStyle>
	        <ItemTemplate>
             <asp:ImageButton id="ibtAdjDet" runat="server" ImageUrl="~/images/adjuntar.bmp" ToolTip="Adjuntar archivo" CommandName="Adjuntar" Width="16px" Height="16px"></asp:ImageButton>
            </ItemTemplate>
	       </asp:TemplateColumn>

	      </Columns>
         </asp:datagrid>
        </td>
       </tr>
       
       <tr>
        <td style="width:500px;HEIGHT:10px" align="left">
        </td>
       </tr>
       <tr>
        <td align="right" style="width:500px;HEIGHT:20px">
         <asp:Button ID="btnActualizar" runat="server" CssClass="Boton" Text="Actualizar" Width="100px" />
         &nbsp;<asp:Button ID="btnSalir" runat="server" CssClass="Boton" Text="Volver" Width="100px" />
        </td>
       </tr>
      </table>
     </asp:Panel>
   
    </ContentTemplate>
    
    <Triggers>
     <asp:AsyncPostBackTrigger ControlID ="ibtConsultar" EventName="click" />
     <asp:AsyncPostBackTrigger ControlID ="lnkConsultar" EventName="click" />
    </Triggers>
   </asp:UpdatePanel>

   </center>
  </form>
 </body>
</html>