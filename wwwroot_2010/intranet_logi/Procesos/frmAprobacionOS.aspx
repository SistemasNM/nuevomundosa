<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="frmAprobacionOS.aspx.vb" Inherits="intranet_logi.frmAprobacionOS" %>
<%@ Register Assembly="Infragistics35.Web.v10.1, Version=10.1.20101.1011, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb"
    Namespace="Infragistics.Web.UI.GridControls" TagPrefix="ig" %>
<%@ Register Assembly="Infragistics35.Web.v10.1, Version=10.1.20101.1011, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb"
    Namespace="Infragistics.Web.UI.EditorControls" TagPrefix="ig" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
 <title>APROBACION DE ORDENES DE SERVICIO</title>
 <base target="_self"/>
 <link href ="../../intranet/Estilos/tab.webfx.css" type="text/css" rel="Stylesheet"/>
 <link href ="../../intranet/Estilos/Styles_Paginas.css" type="text/css" rel="Stylesheet"/>
 <link href ="../../intranet/Estilos/Styles_Controles.css" type="text/css" rel="Stylesheet"/>
 <link rel="stylesheet" type="text/css" href="../css/NM0001.css"/>
 <script language="JavaScript" src="../../intranet/JS/jsDesTabla.js" type="text/jscript"></script>    
 <script language="JavaScript" src="../../intranet/JS/jsGeneral.js" type="text/jscript"></script>    
 
 <script language="JavaScript" type="text/javascript">
    g_RutaDirecto = 1;
    returnValue = "";
     function Aprobar(Valor) {
         returnValue = Valor;
         window.close();
     }

     function fnc_Cerrar() {
         window.open('', '_parent', '');
         window.close();
     }

     //Lista fnc_AdjuntarDocs
     function fnc_ListarDocsAdjuntos() {
         var pstrTipoDoc = "RQS"
         var pstrNumeroDoc = document.all('txtNroRequisicion').value
         var pstrSecuencia = " "
         var retorno = window.showModalDialog("frm_ListadoArchivoAdjunto.aspx?pstrTipoDoc=" + pstrTipoDoc + "&pstrNumeroDoc=" + pstrNumeroDoc + "&pstrSecuencia=" + pstrSecuencia, "Listar Adjuntos", "dialogHeight:400px;dialogWidth:720px;center:yes;help:no;");
         return false;
     }

 </script>
</head>

<body>
 <form id="frmData" runat="server" method="post" >
  <center>
  <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager> 
  
  <table border="0" cellpadding="0" cellspacing="0" style="width:800px;height:30px">
   <tr>
    <td style="width:800px" align="center" valign="middle" class="Cabecera">&nbsp;Aprobacion de Orden de Servicio</td>
   </tr>
  </table>
  
  <table border="0" cellpadding="0" cellspacing="0" style="width:800px">
 <%-- cabecera--%>
   <tr>
    <td>
     <table style="width:800px" border="0" cellpadding="0" cellspacing="0" class="FrameSimple">
      <tr>
       <td align="left" style="width:150px">ORDEN SERVICIO:&nbsp;</td>
       <td align="left" style="width:250px"><asp:Label ID="lblNroOrdenServicio" runat="server" CssClass="lbl"></asp:Label></td>
       <td align="left" style="width:150px">NRO. REQUISICION :</td>
       <td align="left" style="width:250px">
        <%--<asp:Label ID="lblNroRequisicion" runat="server" CssClass="lbl"></asp:Label>--%>
        <asp:TextBox ID="txtNroRequisicion" runat="server" width="110px" CssClass="txtReadOnly"></asp:TextBox>
       </td>
      </tr>
      <tr>
       <td align="left" style="width:150px">PROVEEDOR:&nbsp;</td>
       <td align="left" style="width:250px"><asp:Label ID="lblNombreProveedor" runat="server" CssClass="lbl"></asp:Label></td>
       <td align="left" style="width:150px">RUC:&nbsp;</td>
       <td align="left" style="width:250px"><asp:Label ID="lblRuc" runat="server" CssClass="lbl"></asp:Label></td>
      </tr>
      <tr>
       <td align="left" style="width:150px">NOMBRE CONTACTO:&nbsp;</td>
       <td align="left" style="width:250px"><asp:Label ID="lblNombreContacto" runat="server" CssClass="lbl"></asp:Label></td>
       <td align="left" style="width:150px">EMAIL:;&nbsp;</td>
       <td align="left" style="width:250px"><asp:Label ID="lblEmail" runat="server" CssClass="lbl"></asp:Label></td>
       </tr>
       <tr>
        <td align="left" style="width:150px">TELEFONO CONTACTO:&nbsp;</td>
        <td align="left" style="width:250px"><asp:Label ID="lblTelefonoContacto" runat="server" CssClass="lbl"></asp:Label></td>
        <td align="left" style="width:150px">ESTADO DOC.:&nbsp;</td>
        <td align="left" style="width:250px"><asp:Label ID="lblEstado" runat="server" CssClass="lbl"></asp:Label></td>
       </tr>
       <tr>
        <td align="left" style="width:150px">FECHA O/S:&nbsp;</td>
        <td align="left" style="width:250px"><asp:Label ID="lblFecha" runat="server" CssClass="lbl"></asp:Label></td>
        <td align="left" style="width:150px">SOLICITADO POR:&nbsp;</td>
        <td align="left" style="width:250px"><asp:Label ID="lblUsuario" runat="server" CssClass="lbl"></asp:Label></td>
       </tr>
       <tr>
        <td align="left" style="width:150px">OBSERVACIONES O/S:&nbsp;</td>
        <td align="left" style="width:650px" colspan="3"><asp:Label ID="lblObservaciones" runat="server" CssClass="lbl"></asp:Label></td>
       </tr>
      </table>
     </td>
    </tr>

    <%-- Cabecera parte 2--%>
    <tr>
    <td>
    <table style="width:800px" border="0" cellpadding="0" cellspacing="0" class="FrameSimple">
        <tr>
            <td align="left" style="width:150px">SERVICIO:&nbsp;</td>
            <td align="left" style="width:120px"><asp:RadioButton ID="rdTiposervicio1" runat="server" Text="A todo costo" Checked="true" GroupName="Servicio"  /></td>
            <td align="left" style="width:120px"><asp:RadioButton ID="rdTiposervicio2" runat="server" Text="Mano de Obra" GroupName="Servicio"/></td>
            <td align="left" style="width:110px"></td>
            <td align="left" style="width:150px"></td>
            <td align="left" style="width:150px"></td>
        </tr>
        <tr>
            <td align="left" style="width:150px">Requerido Por:&nbsp;</td>
            <td align="left" style="width:120px"><asp:TextBox ID="TxtCodigoTrabajador" runat="server" CssClass="txt" Width="120px"></asp:TextBox></td>&nbsp;
            <td align="left" style="width:230px" colspan="2"><asp:TextBox ID="TxtNombreTrabajador" runat="server" CssClass="txt" Width="220px"></asp:TextBox></td>&nbsp;
            <td align="left" style="width:110px"><input id="btnTrabajador" class="btnSel" onclick="fdesUsuarios(TxtCodigoTrabajador,TxtNombreTrabajador)" type="button" value="..." style="width:20px" /></td>
            <td align="left" style="width:150px"></td>
            </tr>
        <tr>
            <td align="left" style="width:150px">Fecha Inicio:&nbsp;</td>
            <td align="left" style="width:120px"><ig:WebDatePicker ID="WebDatePicker1" runat="server" Width="110px"></ig:WebDatePicker></td>
            <td align="left" style="width:120px">Fecha Fin</td>
            <td align="left" style="width:110px"><ig:WebDatePicker ID="WebDatePicker2" runat="server" Width="110px"></ig:WebDatePicker></td>
            <td align="left" style="width:150px"></td>
            <td align="left" style="width:150px"></td>
            </tr>
        <tr>
            <td align="left" style="width:150px">Tiempo Ofertado:&nbsp;</td>
            <td align="left" style="width:120px"><asp:TextBox ID="TxtTiempoOfertado" runat="server" CssClass="txt" Width="110px"></asp:TextBox></td>
            <td align="left" style="width:120px">Tiempo Real:&nbsp;</td>
            <td align="left" style="width:110px"><asp:TextBox ID="TxtTiempoReal" runat="server" CssClass="txt" Width="110px"></asp:TextBox></td>
            <td align="left" style="width:150px"></td>
            <td align="left" style="width:150px"></td>
        </tr>
    </table>
    </td>
    </tr>
    <%-- Detalle--%>
    <tr>
    <td>
    <table style="width:800px" border="0" cellpadding="0" cellspacing="0" class="FrameSimple">
     <tr>
      <td align="left" style="width:100%"> 
       <ig:WebDataGrid ID="WebDataGrid1" runat="server" AutoGenerateColumns="false" EnableDataViewState="True" Height="150px" Width="800px" DataKeyFields="var_ArticuloCodigo" Font-Size = "9px">

        <editorproviders>
         <ig:DropDownProvider ID="cmbTrabajo">
          <EditorControl ID="EditorControl1" runat="server" DisplayMode="DropDownList" TextField="var_Descripcion" ValueField="chr_Codigo">
           <Button AltText="" />
           <DropDownItemBinding TextField="var_Descripcion" ValueField="chr_Codigo"  />
          </EditorControl>
         </ig:DropDownProvider>
         <ig:DropDownProvider ID="cmbMateriales">
          <EditorControl ID="EditorControl2" runat="server" DisplayMode="DropDownList" TextField="var_Descripcion" ValueField="chr_Codigo" >
           <Button AltText="" />
           <DropDownItemBinding TextField="var_Descripcion" ValueField="chr_Codigo" />
          </EditorControl>
         </ig:DropDownProvider>
         <ig:DropDownProvider ID="cmbCalidad">
          <EditorControl ID="EditorControl3" runat="server" DisplayMode="DropDownList" textField="var_Descripcion" ValueField="chr_Codigo"  Width="100px" >
           <Button AltText="" />
           <DropDownItemBinding TextField="var_Descripcion" ValueField="chr_Codigo" />
          </EditorControl>
         </ig:DropDownProvider>
         <ig:DropDownProvider ID="cmbNormasSeguridad">
          <EditorControl ID="EditorControl4" runat="server" DisplayMode="DropDownList" TextField="var_Descripcion" ValueField="chr_Codigo"  Width="100px" >
           <Button AltText="" />
           <DropDownItemBinding TextField="var_Descripcion" ValueField="chr_Codigo" />
          </EditorControl>
         </ig:DropDownProvider>
        </editorproviders>
                            
        <Columns>
         <ig:BoundDataField DataFieldName="var_ArticuloCodigo" Key="var_ArticuloCodigo" Width="40px">
          <header text="Secuencia" />
         </ig:BoundDataField>
         <ig:BoundDataField DataFieldName="var_ArticuloNombre" Key="var_ArticuloNombre" Width="120px" >
          <header text="Servicio" />
         </ig:BoundDataField>
         <ig:BoundDataField DataFieldName="num_CantidadSolicitada" Key="num_CantidadSolicitada" Width="70px" >
          <header text="Cantidad" />
         </ig:BoundDataField>
         <ig:BoundDataField DataFieldName="vch_Trabajo" Key="vch_Trabajo" Width="90px" >
          <header text="Trabajo"/>
         </ig:BoundDataField>
         <ig:BoundDataField DataFieldName="vch_Materiales" Key="vch_Materiales" Width="90px" >
          <header text="Materiales"/>
         </ig:BoundDataField>
         <ig:BoundDataField DataFieldName="vch_Calidad" Key="vch_Calidad" Width="90px" >
          <header text="Calidad"/>
         </ig:BoundDataField>
         <ig:BoundDataField DataFieldName="vch_Seguridad" Key="vch_Seguridad" Width="150px" >
          <header text="Normas de Seguridad"/>
         </ig:BoundDataField>
        </Columns>
        
        <Behaviors>
         <ig:EditingCore>
          <Behaviors>
           <ig:CellEditing Enabled="true">
            <ColumnSettings>
             <ig:EditingColumnSetting ColumnKey="vch_Trabajo" EditorID="cmbTrabajo" />
             <ig:EditingColumnSetting ColumnKey="vch_Materiales" EditorID="cmbMateriales" />
             <ig:EditingColumnSetting ColumnKey="vch_Calidad" EditorID="cmbCalidad" />
             <ig:EditingColumnSetting ColumnKey="vch_Seguridad" EditorID="cmbNormasSeguridad" />
            </ColumnSettings>
           </ig:CellEditing>
          </Behaviors>
         </ig:EditingCore>
         <ig:Paging PageSize="100" PagerAppearance="Bottom" />
        </Behaviors>
       
       </ig:WebDataGrid>
      </td>
     </tr>
    </table>
    </td>
    </tr>
     <%-- Pie 1--%>
     <tr>
     <td>
    <table style="width:800px" border="0" cellpadding="0" cellspacing="0" class="FrameSimple">
        <tr>
            <td align="left" style="width:150px">Observaciones:</td>
            <td align="left" style="width:650px"><asp:TextBox id="TxtObservaciones" runat="server" Height="30px" Width="650px" Font-Size = "9px"></asp:TextBox></td>
        </tr>
     </table>
    </td>
    </tr>
     <%-- Pie 2--%>
    <tr>
    <td>
     <table style="width:800px" border="0" cellpadding="0" cellspacing="0" class="FrameSimple">
        <tr>
            <td align="left" style="width:150px">Experiencia Personal:</td>
            <td align="left" style="width:150px">
            <asp:DropDownList ID="cmbExperiencia" runat="server" Width="150px" Font-Size = "9px">
                <asp:ListItem Value="00">Seleccione</asp:ListItem>
                <asp:ListItem Value="Bueno">Bueno</asp:ListItem>
                <asp:ListItem Value="Regular">Regular</asp:ListItem>
                <asp:ListItem Value="Malo">Malo</asp:ListItem>
            </asp:DropDownList>
            </td>
            <td align="left" style="width:250px">Conformidad a la Orden de Servicio:</td>
            <td align="left" style="width:250px">
            <asp:DropDownList ID="cmbConformidad" runat="server" Width="150px" Font-Size = "9px">
                <asp:ListItem Value="00">Seleccione</asp:ListItem>
                <asp:ListItem Value="OK">CONFORME</asp:ListItem>
                <asp:ListItem Value="NO">NO CONFORME</asp:ListItem>
            </asp:DropDownList>
            </td>
        </tr>
      </table>
      </td>
      </tr>                    
      <%--botonera--%>
      <tr>
      <td>
      <table style="width:800px" border="0" cellpadding="0" cellspacing="0" class="FrameSimple">
       <tr>
        <td style="width:800px" align="center" >
         <asp:Button ID="btnAprobar" Runat="server" CssClass="Boton" Text="Aprobar" Width="100px" />&nbsp;
         <asp:Button ID="btnAnular" Runat="server" CssClass="Boton" Text="Anular" Width="100px"/>&nbsp;
         <input ID="btnSalir" runat="server" class="Boton" name="btnSalir" onclick="javascript:fnc_Cerrar();" type="button" value="Salir" style="width:100px"/>&nbsp;
         <asp:button id="btnListaAdjuntos" runat="server" CssClass="Boton" Text="Adjuntos" Width = "100px"></asp:button>

        </td>
       </tr>
     </table>
    </td>
    </tr>
    <tr><td>
     <table id="tblMsgO"  style="width:800px" border="0" cellpadding="0" cellspacing="0">
      <tr>
       <td align="center"><asp:Label ID="lblMsg" runat="server" CssClass="error"></asp:Label></td>
      </tr>
     </table>
     </td>
     </tr>
    </table>
   </center>
  </form>
 </body>
</html>
            