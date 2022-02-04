<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="frm_RegistrarAprobacionOrdenes.aspx.vb" Inherits="intranet_logi.frm_RegistrarAprobacionOrdenes" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Aprobacion de Ordenes</title>
    <link href="../css/sytle.css" rel="stylesheet"/>
    <link rel="stylesheet" type="text/css" href="../css/NM0001.css"/>
    <script language="javascript" type="text/javascript">
        //Lista fnc_AdjuntarDocs
        function fnc_ListarDocsAdjuntos(strNumeroDoc) {
            var pstrTipoDoc = "RQS"
            var pstrNumeroDoc = strNumeroDoc
            var pstrSecuencia = " "
            var retorno = window.showModalDialog("frm_ListadoArchivoAdjunto.aspx?pstrTipoDoc=" + pstrTipoDoc + "&pstrNumeroDoc=" + pstrNumeroDoc + "&pstrSecuencia=" + pstrSecuencia, "Listar Adjuntos", "dialogHeight:400px;dialogWidth:720px;center:yes;help:no;status:no;");
            return false;
        }

        
        //Lista fnc_AdjuntarDocsOC
        function fnc_ListarDocsAdjuntosOC(strNumeroDoc) {
            var pstrTipoDoc = "OCM"
            var pstrNumeroDoc = strNumeroDoc
            var pstrSecuencia = " "
            var retorno = window.showModalDialog("frm_ListadoArchivoAdjunto.aspx?pstrTipoDoc=" + pstrTipoDoc + "&pstrNumeroDoc=" + pstrNumeroDoc + "&pstrSecuencia=0" , "Listar Adjuntos", "dialogHeight:400px;dialogWidth:720px;center:yes;help:no;status:no;");
            return false;
        }


        //Envio correo electronico
        function fnc_EnviaCorreo(strNumeroDoc) {
            var pstrNumeroDoc = strNumeroDoc
            var retorno = window.showModalDialog("frm_EnvioCorreoOrdenes.aspx?pstrNumeroDoc=" + pstrNumeroDoc, "Enviar correo electronico", "dialogHeight:200px;dialogWidth:550px;center:yes;help:no;status:no;toolbar=no;resizable:no;toolbar=no;resizable:no;");
            return false;
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

        // Funcion Busca Grupo
        function fnc_BuscarGrupo(ControlID, strTipo, strNumOC) {
            if (strTipo == "OC") {
                var pstrGrupo = "005";
            }
            else {
                var pstrGrupo = "009";
            }
            var retorno = window.showModalDialog("frm_SecuenciaAprobacionOrdenes.aspx?pstrTipo=" + pstrGrupo + "&pstrNumeroDoc=" + strNumOC, "", "dialogHeight:370px;dialogWidth:520px;center:yes;help:no;status:no;toolbar=no;resizable:no;toolbar=no;resizable:no;");
            if (retorno != "" && retorno != ":") {
                var ltxtGrupo = document.all["dtgLista_txtGrupo_" + ControlID];
                var datos = retorno.split(":");
                var codigo = datos[0];
                var nombre = datos[1];
                ltxtGrupo.value = codigo;
            }
        }

        function VerDetalle(OCOS) {
            var NumeroDoc = OCOS
            var retorno = window.showModalDialog("frm_RegistrarAprobacionOrdenesDet.aspx?&NumeroDoc=" + NumeroDoc, "", "dialogHeight:500px;dialogWidth:820px;center:yes;help:no;");
            if (retorno == "1") {
                return true;
            } else {
                return false;
            }
        }
    </script>
</head>
<body>
 <form runat="server" >
  <center>
   <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager> 
  <!-- Inicio: Contenedor--> 
  <div class="contenedor">
   
   <!-- Inicio: Cabecera --> 
   <div class="titulo">
    <table cellpadding="0" cellspacing="0" border="0" width="800px">
     <tr>
      <td width="800px" height="30px"> APROBACION DE ORDENES DE COMPRA/SERVICIOS</td>
     </tr>

    </table>
   </div>
   <!-- Fin: Cabecera --> 
   
   <asp:UpdatePanel ID="panContenido" runat="server" UpdateMode="Always">
    <ContentTemplate>

   <!-- Inicio: Botonera -->
   <div id="Botonera" class="botonera">
    <table cellpadding="0" cellspacing="0" border="0" width="800px" >
    <tr>
     <td width="120px" align="left">
      <input style="WIDTH: 10px; HEIGHT: 10px" id="hdnaprobarmasivo" size="1" type="hidden" name="hdnaprobarmasivo" runat="server"/>
      <asp:Button id="btnConsultar" text="Consultar" class="btnAzul" runat="server" />
     </td>
     <td width="680px" align="left">
      <asp:Button id="btnAprobar" text="Aprobar" class="btnAzul" runat="server" Visible="false"/>
     </td>
    </tr>
    </table>
   </div>
   <!--  Fin: Botonera -->
   
   <!-- Inicio: Mensaje -->
   <div>
    <table cellpadding="0" cellspacing="0" border="0" width="800px">
     <tr>
      <td align="left"><asp:Label ID="lblMensaje" runat="server" Text="" CssClass="mensaje"></asp:Label>
      </td>
     </tr>
     <tr>
      <td align="right"><asp:Label ID="lblContador" runat="server" Text=" " CssClass="contador"></asp:Label></td>
     </tr>
    </table>
   </div>
   <!-- Fin: Mensaje -->
   
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

   <!-- Grilla -->
   <div>
    <table border="0" cellspacing="1" cellpadding="1" width="800px">
     <tr>
      <td>
       <asp:datagrid id="dtgLista" runat="server" Width="800px" AutoGenerateColumns="False" ShowHeader="true">
        <AlternatingItemStyle CssClass="GridAlternateItem"></AlternatingItemStyle>
        
	    <ItemStyle CssClass="GridItem"></ItemStyle>
	    <HeaderStyle CssClass="gridheader"></HeaderStyle>
	    <Columns>
	     <%--Aprobacion--%>
         <asp:TemplateColumn>
          <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="20px"></HeaderStyle>
           <ItemTemplate><asp:CheckBox id="chkAprobar" runat="server" Width="16px" Visible="false"></asp:CheckBox></ItemTemplate>
	     </asp:TemplateColumn>

         <%--Detalle--%>
         <asp:TemplateColumn>
          <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="20px"></HeaderStyle>
           <ItemTemplate>
            <asp:ImageButton id="ibtDetalle" runat="server" Width="16px" Height="16px" ImageUrl="~/images/Lupa.png" CommandName="Detalle" ></asp:ImageButton>
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

         <%--Email--%>
          <asp:TemplateColumn>
          <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="20px" Font-Bold="true"></HeaderStyle>
		  <ItemTemplate>
           <asp:ImageButton id="lblEmail" runat="server" Width="16px" Height="16px" ImageUrl="~/images/email.jpg"></asp:ImageButton>
          </ItemTemplate>
	     </asp:TemplateColumn>
                  
         <%--Campos--%>

         <%--Tipo--%>
         <asp:TemplateColumn HeaderText="Tipo">
          <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="50px" Font-Bold="true"></HeaderStyle>
		  <ItemTemplate>
           <asp:Label id="lblTipo" runat="server" Font-Size="8px" Text='<%# DataBinder.Eval(Container, "DataItem.chr_TipoOrden") %>' visible ="true"></asp:Label>
          </ItemTemplate>
	     </asp:TemplateColumn>

         <%--# Orden de compra--%>
         <asp:TemplateColumn HeaderText="#OC/OS">
          <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="100px" Font-Bold="true"></HeaderStyle>
		  <ItemTemplate>
           <asp:Label id="lblNumDoc" runat="server" Font-Size="8px" Text='<%# DataBinder.Eval(Container, "DataItem.vch_NumeroOrden") %>' visible ="true"></asp:Label>
           <asp:Label id="lblNumRq" runat="server" Font-Size="8px" Text='<%# DataBinder.Eval(Container, "DataItem.vch_NumeroRequi") %>' visible ="false"></asp:Label>
          </ItemTemplate>
	     </asp:TemplateColumn>

         <%--fecha--%>
         <asp:BoundColumn DataField="vch_FechaOrden" HeaderText="Fecha Doc." HeaderStyle-Font-Bold="true">
          <ItemStyle Font-Size="8px" HorizontalAlign="center" VerticalAlign="Middle" Width="80px"></ItemStyle>
         </asp:BoundColumn>

         <%--Proveedor--%>
	     <asp:BoundColumn DataField="vch_DesProveedor" HeaderText="Proveedor" HeaderStyle-Font-Bold="true">
          <ItemStyle Font-Size="8px" HorizontalAlign="left" VerticalAlign="Middle" Width="200px"></ItemStyle>
         </asp:BoundColumn>
         
          <%--Moneda--%>
         <asp:BoundColumn DataField="chr_CodMoneda" HeaderText="Moneda" HeaderStyle-Font-Bold="true">
          <ItemStyle Font-Size="8px" HorizontalAlign="center" VerticalAlign="Middle" Width="50px"></ItemStyle>
         </asp:BoundColumn>

         <%--Monto--%>
         <asp:BoundColumn DataField="num_Monto" DataFormatString="{0:#,##0.00}" HeaderText="Monto" HeaderStyle-Font-Bold="true">
	      <ItemStyle Font-Size="8px" HorizontalAlign="Right" VerticalAlign="Middle" Width="50px"></ItemStyle>
	     </asp:BoundColumn>
           
         <%--CTC/Activo--%>
         <asp:BoundColumn DataField="vch_DesActivo" HeaderText="CTC/Activo" HeaderStyle-Font-Bold="true">
	      <ItemStyle Font-Size="8px" HorizontalAlign="left" VerticalAlign="Middle" Width="200px"></ItemStyle>
	     </asp:BoundColumn>

	     <%--Secuencia de aprobacion--%>
         <asp:TemplateColumn HeaderText="Sec.">
          <HeaderStyle HorizontalAlign="left" VerticalAlign="Middle" Width="60px" Font-Bold="true"></HeaderStyle>
           <ItemTemplate>
            <asp:TextBox ID="txtGrupo" Text="" runat="server" Width="20px" Height="16px" Font-Size="9px" CssClass="inputDisabled"></asp:TextBox>
            <asp:Button id="btnGrupo" runat="server" style="WIDTH: 20px; HEIGHT: 20px" text="..." CssClass="Boton" CommandName="Aprobar" />
           </ItemTemplate>
          </asp:TemplateColumn>
        </Columns>

       </asp:datagrid>
      </td>
     </tr>
    </table>
   </div>   
   <!-- Contenedor--> 
  </div>
  </ContentTemplate>
  <Triggers>
   <asp:AsyncPostBackTrigger ControlID ="btnConsultar" EventName="click" />
  </Triggers>
  </asp:UpdatePanel>
 
  </center>
 </form>
</body>
</html>
