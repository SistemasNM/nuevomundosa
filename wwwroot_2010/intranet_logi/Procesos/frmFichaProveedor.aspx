<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="frmFichaProveedor.aspx.vb" Inherits="intranet_logi.frmFichaProveedor" %>
<%@ Register Assembly="Infragistics35.Web.v10.1, Version=10.1.20101.1011, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb"
    Namespace="Infragistics.Web.UI.GridControls" TagPrefix="ig" %>
<%@ Register Assembly="Infragistics35.Web.v10.1, Version=10.1.20101.1011, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb"
    Namespace="Infragistics.Web.UI.EditorControls" TagPrefix="ig" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" >
 <head id="Head1" runat="server">
 <title>EVALUACION DE ORDENES DE SERVICIO</title>
 <base target="_self"/>
 <link href="../css/NM0001.css" type="text/css" rel="stylesheet"/>
 <link href="../../intranet/Estilos/tab.webfx.css" type="text/css" rel="Stylesheet"/>
 <link href="../../intranet/Estilos/Styles_Paginas.css" type="text/css" rel="Stylesheet"/>
 <link href="../../intranet/Estilos/Styles_Controles.css" type="text/css" rel="Stylesheet"/>
 <script language="JavaScript" src="../../intranet/js/jsDesTabla.js" type="text/jscript" ></script>    
 <script language="JavaScript" src="../../intranet/js/jsGeneral.js" type="text/jscript" ></script>    
    
 <script language="JavaScript" type="text/javascript">
     g_RutaDirecto = 1;
     function fAnular(Obj) {
         ObjReg = document.getElementById("txtRegSel");
         if (ObjReg.value == "") {
             alert("Por favor Seleccionar un registro...!")
             return false;
         }
         var sw = confirm("¿Esta seguro de anular el registro seleccionado?");
         return sw
     }

     function fnc_Cerrar() {
         window.close();
     }
     function fnc_ListarDocsAdjuntos() {
         var pstrTipoDoc = "RQS"
         var pstrNumeroDoc = document.all('txtNroRequisicion').value
         var pstrSecuencia = " "
         var retorno = window.showModalDialog("frm_ListadoArchivoAdjunto.aspx?pstrTipoDoc=" + pstrTipoDoc + "&pstrNumeroDoc=" + pstrNumeroDoc + "&pstrSecuencia=" + pstrSecuencia, "Listar Adjuntos", "dialogheight:400px;dialogwidth:720px;center:yes;help:no;");
         return false;
     }

  </script>
 </head>
 <body>
  <form id="frmData" runat="server" method="post">
   <center>
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager> 
    <table border="0" cellpadding="0" cellspacing="0" style="width:800px; height:30px">
     <tr>
      <td class="Cabecera">Conformidad de Orden de Servicio</td>
     </tr>
     </table>
     
     <table border="0" cellpadding="0" cellspacing="0" style="width:800px">
      <tr>
       <td>
        <asp:Panel ID="panActualizaO" runat="server" Visible="true" Width="800px">
         <%--Cabecera--%>
         <table style="width:100%; height:110px" border="0" cellpadding="0" cellspacing="0" class="FrameSimple">
          <tr>
           <td align="left" style="width:150px">ORDEN SERVICIO:&nbsp;</td>
           <td align="left" style="width:400px"><asp:Label id="lblNroOrdeServicio" runat="server" CssClass="lbl"></asp:Label></td>
           <td align="left" style="width:100px">NRO. REQUISICION:&nbsp;</td>
           <td align="left" style="width:200px">
            <%--<asp:Label ID="lblNroRequisicion" runat="server" CssClass="lbl"></asp:Label>--%>
            <asp:TextBox ID="txtNroRequisicion" runat="server" CssClass="txtReadOnly" Width="100px" ></asp:TextBox>
           </td>
          </tr>
          <tr>
           <td align="left" style="width:150px">PROVEEDOR:&nbsp;</td>
           <td align="left" style="width:400px"><asp:Label ID="lblNombreProveedor" runat="server" CssClass="lbl"></asp:Label></td>
           <td align="left" style="width:100px">RUC:&nbsp;</td>
           <td align="left" style="width:200px"><asp:Label ID="lblRuc" runat="server" CssClass="lbl"></asp:Label></td>
          </tr>
          <tr>
           <td align="left" style="width:150px">NOMBRE DE CONTACTO:&nbsp;</td>
           <td align="left" style="width:400px"><asp:Label ID="lblNombreContacto" runat="server" CssClass="lbl"></asp:Label></td>
           <td align="left" style="width:100px">EMAIL:&nbsp;</td>
           <td align="left" style="width:200px"><asp:Label ID="lblEmail" runat="server" CssClass="lbl"></asp:Label></td>
          </tr>
          <tr>
           <td align="left" style="width:150px">TELEFONO CONTACTO:&nbsp;</td>
           <td align="left" style="width:400px"><asp:Label ID="lblTelefonoContacto" runat="server" CssClass="lbl"></asp:Label></td>
           <td align="left" style="width:100px">ESTADO DOC.:&nbsp;</td>
           <td align="left" style="width:200px"><asp:Label ID="lblEstado" runat="server" CssClass="lbl"></asp:Label></td>
          </tr>
          <tr>
           <td align="left" style="width:150px">FECHA O/S:&nbsp;</td>
           <td align="left" style="width:400px"><asp:Label ID="lblFecha" runat="server" CssClass="lbl"></asp:Label></td>
           <td align="left" style="width:100px">SOLICITADO POR:&nbsp;</td>
           <td align="left" style="width:200px"><asp:Label ID="lblUsuario" runat="server" CssClass="lbl"></asp:Label></td>
          </tr>
          <tr>
           <td align="left" style="width:150px">OBSERVACIONES O/S:&nbsp;</td>
           <td align="left" colspan="3" style="width:650px"><asp:Label ID="lblObservaciones" runat="server" CssClass="lbl"></asp:Label></td>
          </tr>
         </table>
         
         <%--Registro parte 1:--%>
         <table style="width:800px" border="0" cellpadding="0" cellspacing="0" class="FrameSimple">
          <tr>
           <td align="left" style="width:150px">Servicio:&nbsp;</td>
           <td align="left" style="width:100px"><asp:RadioButton ID="rdTiposervicio1" runat="server" text="A todo costo" Checked="true" GroupName="Servicio" Width="100px" /></td>
           <td align="left" style="width:100px"><asp:RadioButton ID="rdTiposervicio2" runat="server" GroupName="Servicio" text="Mano de Obra"  Width="100px"/></td>
           <td align="left" style="width:450px"></td>
          </tr>
          <tr>
           <td align="left" style="width:150px">Requerido Por:&nbsp;</td>
           <td align="left" style="width:100px"><asp:TextBox ID="TxtCodigoTrabajador" runat="server" CssClass="txt" Width="100px"></asp:TextBox></td>&nbsp;
           <td align="left" style="width:100px"><asp:TextBox ID="TxtNombreTrabajador" runat="server" CssClass="txt" Width="100px"></asp:TextBox></td>&nbsp; 
           <td align="left" style="width:450px"><input id="btnTrabajador" class="Boton" style="width:20px; height:20px" onclick="fdesUsuarios(TxtCodigoTrabajador,TxtNombreTrabajador)" type="button" value="..." /></td>
          </tr>
          <tr>
           <td align="left" style="width:150px">Fecha Inicio:&nbsp;</td>
           <td align="left" style="width:100px"><ig:WebDatePicker ID="wdpFechaInicio" runat="server" Width="100px"></ig:WebDatePicker></td>&nbsp;
           <td align="left" style="width:100px">Fecha Fin&nbsp;:</td>
           <td align="left" style="width:450px"><ig:WebDatePicker ID="wdpFechaFin" runat="server" Width="100px"></ig:WebDatePicker></td>
          </tr>
          <tr>
           <td align="left" style="width:150px">Tiempo Ofertado:&nbsp;</td>
           <td align="left" style="width:100px"><asp:TextBox ID="TxtTiempoOfertado" runat="server" CssClass="txt" Width="100px" ></asp:TextBox></td>
           <td align="left" style="width:100px">Tiempo Real:&nbsp;</td>
           <td align="left" style="width:450px"><asp:TextBox ID="TxtTiempoReal" runat="server" CssClass="txt" Width="100px"></asp:TextBox></td>
          </tr>
          <tr>
           <td align="left" style="width:150px">
           <td align="left" style="width:100px">
           <td align="left" style="width:100px">
           <td align="left" style="width:450px">
          </tr>
         </table>

         <%--Grilla:--%>
         <table style="width:800px;height:100px"  border="0" cellpadding="0" cellspacing="0" class="FrameSimple">
          <tr>
           <td align="left" style="width:800px"> 
            <ig:WebDataGrid ID="wdgDetalle" runat="server" AutoGenerateColumns="false" EnableDataViewState="True" Height="100px" Width="800px" DataKeyFields="var_ArticuloCodigo" Font-Size="10px">
             
             <editorproviders>
              <ig:DropDownProvider ID="cmbTrabajo">
               <EditorControl ID="EditorControl1" runat="server" DisplayMode="DropDownList" TextField="var_Descripcion" ValueField="chr_Codigo" Font-Size="9px">
                <Button AltText="" /> 
                <DropDownItemBinding TextField="var_Descripcion" ValueField="chr_Codigo"/>
               </EditorControl>
              </ig:DropDownProvider>
              <ig:DropDownProvider ID="cmbMateriales" VerticalAlign="Middle" >
               <EditorControl ID="EditorControl2" runat="server" DisplayMode="DropDownList" TextField="var_Descripcion" ValueField="chr_Codigo"  Font-Size="9px">
                <Button AltText="" />
                <DropDownItemBinding TextField="var_Descripcion" ValueField="chr_Codigo" />
               </EditorControl>
              </ig:DropDownProvider>
              <ig:DropDownProvider ID="cmbCalidad">
               <EditorControl ID="EditorControl3" runat="server" DisplayMode="DropDownList" TextField="var_Descripcion" ValueField="chr_Codigo" Width="100px"  Font-Size="9px">
                <Button AltText="" />
                <DropDownItemBinding TextField="var_Descripcion" ValueField="chr_Codigo" />
               </EditorControl>
              </ig:DropDownProvider>
              <ig:DropDownProvider ID="cmbNormasSeguridad">
               <EditorControl ID="EditorControl4" runat="server" DisplayMode="DropDownList" TextField="var_Descripcion" ValueField="chr_Codigo" Width="100px"  Font-Size="9px">
                <Button AltText="" />
                <DropDownItemBinding TextField="var_Descripcion" ValueField="chr_Codigo" />
                </EditorControl>
              </ig:DropDownProvider>
             </editorProviders>
             
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

         <%--Registro: parte 2--%>
         <table style="width:800px"  border="0" cellpadding="0" cellspacing="0" class="FrameSimple"> 
          <tr>
           <td align="left" style="width:150px"></td>
           <td align="left" style="width:650px" colspan="3"></td>
          </tr>
          <tr>
           <td align="left" style="width:150px">Observaciones:</td>
           <td align="left" style="width:650px" colspan="3"><asp:TextBox ID="TxtObservaciones" runat="server" Height="30px" Width="650px"></asp:TextBox></td>
          </tr>
          <tr>
           <td align="left" style="width:150px">Experiencia Personal:</td>
           <td align="left" style="width:300px">
            <asp:DropDownList ID="cmbExperiencia" runat="server" Font-Size = "10px" Width="150px">
             <asp:ListItem Value="00">Seleccione</asp:ListItem>
             <asp:ListItem Value="Bueno">Bueno</asp:ListItem>
             <asp:ListItem Value="Regular">Regular</asp:ListItem>
             <asp:ListItem Value="Malo">Malo</asp:ListItem>
            </asp:DropDownList>
           </td>
           <td align="left" style="width:150px">Conformidad a la O/S:</td>
           <td align="left" style="width:200px">
            <asp:DropDownList ID="cmbConformidad" runat="server" Font-Size="10px" Width="150px">
             <asp:ListItem Value="00">Seleccione</asp:ListItem>
             <asp:ListItem Value="OK">CONFORME</asp:ListItem>
             <asp:ListItem Value="NO">NO CONFORME</asp:ListItem>
            </asp:DropDownList>
           </td>
          </tr>
         </table>
       
         <%--botonera--%>
         <table id="tblGrabarO" style="width:800px" border="0" cellpadding="0" cellspacing="0">
          <tr>
           <td style="width:800px" align="center">
            <asp:ImageButton ID="imgGrabarFicha" runat="server" Height="25px" ImageUrl="../../intranet/Imagenes/grabar.gif" ToolTip="Grabar Conformidad" Width="25px" />
            &nbsp;<asp:ImageButton ID="imgCancelar" runat="server" ImageUrl="../../intranet/Imagenes/Cancelar.gif" ToolTip="Regresar / Cancelar" Height="25px" Width="25px"/>
            &nbsp;<asp:button id="btnSolicitaAprobacion" Runat="server" CssClass="Boton" Width="150px" Text="Solicitar Aprobacion" ></asp:button>
            &nbsp;<asp:button id="btnAdjuntos" runat="server" width="120px" Text="Ver adjuntos" CssClass="Boton"></asp:button>
            <asp:TextBox id="TxtAprobacion" runat="server" CssClass="txtHid" Width="5px"></asp:TextBox>
            <asp:TextBox id="TxtDescripcion" runat="server" CssClass="txtHid" Width="5px"></asp:TextBox>
           </td>
          </tr>
         </table>
         <%--Errores--%>
        <table id="tblMsgO" style="width:800px" border="0" cellpadding="0" cellspacing="0">
         <tr>
          <td align="center" style="width:800px"><asp:Label ID="lblMsg" runat="server" Font-Size="10px"></asp:Label></td>
         </tr>
        </table>              
        
        </asp:Panel>
       
       </td>
      </tr>
     </table>
    </center>
   </form>
  </body>
</html>
