<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="frm_SeguimientoDetalladoOS.aspx.vb" Inherits="intranet_logi.frm_SeguimientoDetalladoOS" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
 <title>Seguimiento de Documentos</title>
 <link href="../css/NM0001.css" rel="stylesheet" type="text/css" />
 <link href="../css/Styles_Controles.css" rel="stylesheet" type="text/css" />
 <script src="../js/jsCalendario_N3.js" type="text/javascript"></script>
 <script language="javascript" type="text/jscript">
     function fMostrarReporte(strURL) {
         var intWidth = screen.width;
         var intHeight = screen.height;
         window.open(strURL, "", "top=0; left=0; width=" + intWidth + "; height=" + intHeight + "; resizable=1;");
     }

     function BuscarProveedor() {
         var retorno = window.showModalDialog('http://servnmprb/intranet/Buscadores/frm_QryProveedores.aspx', '', 'dialogHeight:450px;dialogWidth:450px;center:yes;help:no;');
         //var retorno = window.showModalDialog('../../../intranet/Buscadores/frm_QryProveedores.aspx', '', 'dialogHeight:450px;dialogWidth:450px;center:yes;help:no;');
         if (retorno != "" && retorno != ":") {
             datos = retorno.split(":");
             var codigo = datos[0];
             var nombre = datos[1];
             frm_SeguimientoDetalladoOS.txtCodigoProveedor.value = codigo;
             frm_SeguimientoDetalladoOS.txtNombreProveedor.value = nombre;
         }
     }

     //BuscarServicio
     function BuscarServicio() {
         var retorno = window.showModalDialog("http://servnmprb/intranet/Buscadores/frmServicios.aspx", "", "dialogHeight:450px;dialogWidth:450px;center:yes;help:no;");
         if (retorno != "" && retorno != "::") {
             var datos = retorno.split(":");
             var Grupo = datos[0];
             var Codigo = datos[1];
             var Nombre = datos[2];
             var txtGrupoServF = document.getElementById("txtGrupoServF");
             var txtTipoServF = document.getElementById("txtTipoServF");
             var txtDescr_Servicios = document.getElementById("txtDescr_Servicios");             
             
             txtGrupoServF.value = Grupo;
             txtTipoServF.value = Codigo;
             txtDescr_Servicios.value = Nombre;
         }
     }
 </script>

    </head>
<body>
  <form id="frm_SeguimientoDetalladoOS" runat="server">
   <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager> 
   
   <asp:UpdatePanel ID="panContenido" runat="server">
    <ContentTemplate>
 
     <!-- Cabecera -->
     <table border="0" cellspacing="1" cellpadding="1" width="100%">
      <tr>
	   <td align="center" style="HEIGHT:20px;" class="Cabecera">
           REPORTE&nbsp;DE&nbsp;SEGUIMIENTO DE O/S</td>
      </tr>
     </table>
     <table style="WIDTH:700px; HEIGHT: 20px" border="0" cellpadding="0" cellspacing="0" align="center">
        <tr>
            <td style="width:600px;text-align:left">
                &nbsp;</td>
            <td style="width:100px;text-align:center">               
                &nbsp;</td>
        </tr>
         <tr>
             <td style="width:600px;text-align:left">
                 <table style="WIDTH:600px;" border="0" cellpadding="0" cellspacing="0" align="center">
                     <tr>
                         <td style="WIDTH: 150px;" align="left" class="Etiqueta" height="30px">&nbsp;Tipo de Reporte :</td>
                         <td>
                             <asp:RadioButton ID="rdbTipoReporteDeta" runat="server" Text="Detallado" 
                                 Checked="True"  GroupName="TipoReporte" AutoPostBack="True" />
                         </td>
                         <td>
                             <asp:RadioButton ID="rdbTipoReporteCont" runat="server" Text="Contabilizado" 
                                 GroupName="TipoReporte" AutoPostBack="True" />
                         </td>
                     </tr>
                     <tr>
                         <td>&nbsp;</td>
                         <td>&nbsp;</td>
                         <td>&nbsp;</td>
                     </tr>
                 </table>
             </td>
             <td style="width:100px;text-align:center">
                 &nbsp;</td>
         </tr>
         <tr>
             <td style="width:600px;text-align:left">
                <asp:Panel ID="pnlContabilizado" runat="server">
                    <table style="WIDTH:600px;" border="0" cellpadding="0" cellspacing="0" align="center">
                         <tr>
                            <td style="WIDTH: 100px;" align="left" class="Etiqueta" height="30px">&nbsp;Inicio Fecha 
                                O/S:</td>
                            <td style="WIDTH: 200px;" align="left" height="30px">
                                <asp:textbox id="txtFechaIniOS" runat="server" Width="100px" CssClass="input" Font-Size="10px" MaxLength="10"></asp:textbox>
                                &nbsp;<img onclick="popUpCalendar(this, frm_SeguimientoDetalladoOS.txtFechaIniOS, 'dd/mm/yyyy')" border="0"
		                                alt="Seleccionar fecha Inicial" src="../images/Calendario.gif" width="16px" height="16px"  /></td>
                            <td style="WIDTH: 100px;" align="left" class="Etiqueta" height="30px">&nbsp;Fin Fecha 
                                O/S.:</td>
                            <td style="WIDTH: 200px;" align="left" height="30px">
                                <asp:textbox id="txtFechaFinOS" runat="server" Width="100px" CssClass="input" Font-Size= "10px" MaxLength="10"></asp:textbox>
                                &nbsp;<img onclick="popUpCalendar(this, frm_SeguimientoDetalladoOS.txtFechaFinOS, 'dd/mm/yyyy')" border="0"
	                            alt="Seleccionar fecha Final" src="../images/Calendario.gif" width="16px" height="16px"/>
                            </td>
                        </tr>
                        <tr>
                            <td style="WIDTH: 100px;" align="left" class="Etiqueta" height="30px">&nbsp;Inicio Fecha 
                                Contab.</td>
                            <td style="WIDTH: 200px;" align="left" height="30px">
                                <asp:textbox id="txtFechaIniCont" runat="server" Width="100px" CssClass="input" Font-Size="10px" MaxLength="10"></asp:textbox>
                                &nbsp;<img onclick="popUpCalendar(this, frm_SeguimientoDetalladoOS.txtFechaIniCont, 'dd/mm/yyyy')" border="0"
		                                alt="Seleccionar fecha Inicial" src="../images/Calendario.gif" width="16px" height="16px"  /></td>
                            <td style="WIDTH: 100px;" align="left" class="Etiqueta" height="30px">&nbsp;Fin Fecha 
                                Contab.:</td>
                            <td style="WIDTH: 200px;" align="left" height="30px">
                                <asp:textbox id="txtFechaFinCont" runat="server" Width="100px" CssClass="input" Font-Size= "10px" MaxLength="10"></asp:textbox>
                                &nbsp;<img onclick="popUpCalendar(this, frm_SeguimientoDetalladoOS.txtFechaFinCont, 'dd/mm/yyyy')" border="0"
	                            alt="Seleccionar fecha Final" src="../images/Calendario.gif" width="16px" height="16px"/>
                            </td>
                        </tr>
                    </table>
                 </asp:Panel>     
                 <asp:Panel ID="pnlDetallado" runat="server">
                     <table style="WIDTH:600px; HEIGHT: 20px" border="0" cellpadding="0" cellspacing="0" align="center">
                         <tr>
	                       <td style="WIDTH: 100px;" align="left" class="Etiqueta" height="30px">&nbsp;Fecha Inicio:</td>
	                       <td style="WIDTH: 200px;" align="left" height="30px">
                                <asp:textbox id="txtFechaIni" runat="server" Width="100px" CssClass="input" Font-Size="10px" MaxLength="10"></asp:textbox>
                                &nbsp;<img onclick="popUpCalendar(this, frm_SeguimientoDetalladoOS.txtFechaIni, 'dd/mm/yyyy')" border="0"
		                                alt="Seleccionar fecha Inicial" src="../images/Calendario.gif" width="16px" height="16px"  /></td>
                           <td style="WIDTH: 100px;" align="left" class="Etiqueta" height="30px">&nbsp;Fecha Fin:</td>
                           <td style="WIDTH: 200px;" align="left" height="30px">
                                <asp:textbox id="txtFechaFin" runat="server" Width="100px" CssClass="input" Font-Size= "10px" MaxLength="10"></asp:textbox>
                             &nbsp;<img onclick="popUpCalendar(this, frm_SeguimientoDetalladoOS.txtFechaFin, 'dd/mm/yyyy')" border="0"
		                     alt="Seleccionar fecha Final" src="../images/Calendario.gif" width="16px" height="16px"/>
                           </td>
                          </tr>
                         <tr>
                             <td align="left" class="Etiqueta" height="30px" style="WIDTH: 100px;">&nbsp;Estados:</td>
                             <td align="left" colspan="3" height="30px">
                                <table cellpadding="0" cellspacing="0" width="100%">
                                  <tr>
                                      <td>
                                          <asp:CheckBox ID="chkOrdenSinRuta" runat="server" Text="ORDEN SIN RUTA" />
                                      </td>
                                      <td>
                                          <asp:CheckBox ID="chkOrdenxAprobar" runat="server" Text="ORDEN POR APROBAR" />
                                      </td>
                                      <td>
                                          <asp:CheckBox ID="chkOrdenArpobada" runat="server" Text="ORDEN APROBADA" />
                                      </td>
                                  </tr>
                              </table>
                             </td>
                         </tr>
                         <tr>
                             <td align="left" class="Etiqueta" height="30px" style="WIDTH: 100px;">&nbsp;Proveedor:</td>
                             <td align="left" height="30px" style="WIDTH: 200px;">
                                <asp:textbox id="txtCodigoProveedor" runat="server" Width="106px" CssClass="inputDisabled" MaxLength="13"></asp:textbox>&nbsp;&nbsp;
                                <input class="Boton" id="btnProveedor" style="width:20px; height:20px" onclick="BuscarProveedor()" type="button" value="..."  />
                             </td>
                             <td align="left" height="30px" colspan="2">
                                <asp:TextBox ID="txtNombreProveedor" runat="server" BorderStyle="None" 
                                            CssClass="Etiqueta" Width="100%"></asp:TextBox>
                             </td>
                         </tr>
                         <tr>
                             <td align="left" class="Etiqueta" height="30px" style="WIDTH: 100px;">&nbsp;Motivo:</td>
                             <td align="left" height="30px" colspan="3">
                                <asp:DropDownList ID="ddlMotivoRequisicion" runat="server" Width="495px" CssClass="input"></asp:DropDownList>
                             </td>
                         </tr>
                         <tr>
                             <td align="left" class="Etiqueta" height="30px" style="WIDTH: 100px;">&nbsp;Presupuestado</td>
                             <td align="left" height="30px" colspan="3">
                                <table cellpadding="0" cellspacing="0" width="300px">
                                  <tr>
                                      <td style="width: 100px">
                                          <asp:RadioButton ID="rdbTodosPre" runat="server" Text="Todos" Checked="True" 
                                              GroupName="presupuesto" />
                                      </td>
                      
                                      <td style="width: 100px">
                                          <asp:RadioButton ID="rdbSiPre" runat="server" Text="Si" 
                                              GroupName="presupuesto" />
                                      </td>
                                      <td style="width: 100px">
                                          <asp:RadioButton ID="rdbNoPre" runat="server" Text="No" 
                                              GroupName="presupuesto" />
                                      </td>
                                  </tr>
                                </table>
                             </td>
                         </tr>
                         <tr>
                             <td align="left" class="Etiqueta" height="30px" style="WIDTH: 100px;">&nbsp;Servicios</td>
                             <td align="left" height="30px" style="WIDTH: 200px;">
                                <asp:TextBox id="txtGrupoServF" runat="server" CssClass="input" Width="35px" Font-Size="10px"></asp:TextBox>&nbsp;&nbsp;
                                <asp:TextBox id="txtTipoServF" runat="server" CssClass="input" Width="35px" Font-Size="10px"></asp:TextBox>&nbsp;&nbsp;
                                <input class="Boton" id="BtnGrupoSerF" style="WIDTH: 20px; HEIGHT: 20px" type="button" value="..." 
                                      onclick="BuscarServicio()" />
                             </td>
                             <td align="left" colspan="2" height="30px">
                                 <asp:TextBox ID="txtDescr_Servicios" runat="server" BorderStyle="None" 
                                     CssClass="Etiqueta" Width="100%"></asp:TextBox>
                             </td>
                         </tr>
                     </table>
                </asp:Panel>
             </td>
             <td style="width:100px;text-align:center">
                 <asp:ImageButton ID="btnBuscar" runat="server" Height="30px" 
                     ImageUrl="../images/Buscar.png" style="cursor:hand;" ToolTip="Buscar" 
                     Width="30px" />
             </td>
         </tr>
        <tr>
            <td style="width:600px;text-align:left">
                <asp:Label ID="lblMensaje" runat="server" CssClass="error"></asp:Label>
            </td>
            <td style="width:100px;text-align:center">               
                &nbsp;</td>
        </tr>
     </table>


    </ContentTemplate>
       <Triggers>
           <asp:AsyncPostBackTrigger ControlID="rdbTipoReporteDeta" 
               EventName="CheckedChanged" />
           <asp:AsyncPostBackTrigger ControlID="rdbTipoReporteCont" 
               EventName="CheckedChanged" />
       </Triggers>
   </asp:UpdatePanel>
 </form>
</body>
</html>
