<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="ConformidadServicio.aspx.vb" Inherits="intranet_logi.ConformidadServicio" %>
<%@ Register Assembly="Infragistics35.Web.v10.1, Version=10.1.20101.1011, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb"
    Namespace="Infragistics.Web.UI.GridControls" TagPrefix="ig" %>
<%@ Register Assembly="Infragistics35.Web.v10.1, Version=10.1.20101.1011, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb"
    Namespace="Infragistics.Web.UI.EditorControls" TagPrefix="ig" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<meta http-equiv="X-UA-Compatible" content="IE=EmulateIE8" />
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>EVALUACION DE ORDENES DE SERVICIO</title>
     <base target="_self"/>
    <link href="../css/Styles_Controles.css" type="text/css" rel="Stylesheet"/>
    <link href="../css/Styles_Paginas.css" type="text/css" rel="Stylesheet"/>
    <link href="../css/NM0001.css" type="text/css" rel="Stylesheet"/>
    <link href="../css/tab.webfx.css" type="text/css" rel="Stylesheet"/>
    
    <script language="javascript" src="../../intranet/js/jsDesTabla.js" type="text/jscript"></script>
   <%--<script language="javascript" src="../js/jsDesTabla.js" type="text/jscript"></script>--%>
    <script language="JavaScript" type="text/javascript">
        g_RutaDirecto = 1;
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
        function Confirm_LevObs() {
            var confirm_value = document.createElement("INPUT");
            confirm_value.type = "hidden";
            confirm_value.name = "confirm_value";
            if (confirm("Desea levantar las observaciones?")) {
                confirm_value.value = "Si";
            } else {
                confirm_value.value = "No";
            }
            document.forms[0].appendChild(confirm_value);
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
                        <table style="width:100%; height:110px" border="0" cellpadding="0" cellspacing="0" class="FrameSimple">
                            <tr>
                               <td align="left" style="width:150px">ORDEN SERVICIO:&nbsp;</td>
                               <td align="left" style="width:400px"><asp:Label id="lblNroOrdeServicio" runat="server" CssClass="lbl"></asp:Label></td>
                               <td align="left" style="width:100px">NRO. REQUISICION:&nbsp;</td>
                               <td align="left" style="width:200px">
        
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
                                <td align="left" style="width:150px">RECIBE CHARLA DE SEGURIDAD:</td>
                                <td align="left" style="width:200px"><asp:Label ID="lblTipoServicio" runat="server" CssClass="lbl"></asp:Label></td>
                                 <td align="left" style="width:100px">MONTO:&nbsp;</td>
                               <td align="left" style="width:200px"><asp:Label ID="lblMonto" runat="server" CssClass="lbl"></asp:Label></td>
                             </tr>
                             <tr>
                                <td align="left" style="width:150px">OBSERVACIONES O/S:&nbsp;</td>
                                <td align="left" colspan="3" style="width:650px"><asp:Label ID="lblObservaciones" runat="server" CssClass="lbl"></asp:Label></td>
                             </tr>
                        </table>
                       <%--<table style="width:800px;height:80px"  border="0" cellpadding="0" cellspacing="0" class="FrameSimple">--%>
                            <%--<tr>
                                <td>--%>
                                <div ID="divServicio" runat="server" style="border: 1px solid #e2e2e2; overflow:auto; height:110px; width:800px;">
                                    <asp:GridView ID="grvItem" runat="server" AutoGenerateColumns="False" 
                                                        Width="790px" Height="50px"  DataKeyNames ="Numero,Item">
                                        <Columns>
                                               <asp:TemplateField HeaderText="Secuencia">
                                                    <ItemTemplate>
                                                            <asp:Label ID="lblNumero" runat="server" Text='<%# Bind("Numero") %>'></asp:Label>
                                                     </ItemTemplate>
                                                     <ItemStyle  HorizontalAlign="Center" Width="80px"/>
                                               </asp:TemplateField>
                                               <asp:TemplateField HeaderText="Servicio">
                                                    <ItemTemplate>
                                                            <asp:Label ID="lblItem" runat="server" Text='<%# Bind("Item") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle  HorizontalAlign="Left" Width="720px"/>
                                               </asp:TemplateField>
                                               <asp:TemplateField HeaderText="Monto">
                                                    <ItemTemplate>
                                                            <asp:Label ID="lblMonto" runat="server" Text='<%# Bind("Monto") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle  HorizontalAlign="Center" Width="80px"/>
                                               </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                </div>
                               <%-- </td>
                            </tr>--%>
                        <%--</table>--%>
                        <table style="width:800px" border="0" cellpadding="0" cellspacing="0" class="FrameSimple">
                            <tr>
                                <td align="left" style="width:150px">Servicio:&nbsp;</td>
                                <td align="left" style="width:100px"><asp:RadioButton ID="rdTiposervicio1" runat="server" text="A todo costo" Checked="true" GroupName="Servicio" Width="100px" /></td>
                                <td align="left" style="width:100px"><asp:RadioButton ID="rdTiposervicio2" runat="server" GroupName="Servicio" text="Mano de Obra"  Width="100px"/></td>
                                <td align="left" style="width:450px"></td>
                            </tr>
                            <tr>
                               <td align="left" style="width:150px">Requerido Por:&nbsp;</td>
                               <td align="left" style="width:100px"><asp:TextBox ID="TxtCodigoTrabajador" runat="server" CssClass="txt" Width="100px"></asp:TextBox></td>        
                                      
                                       
                                              &nbsp;
                                              <td align="left" style="width:100px">
                                                  <asp:TextBox ID="TxtNombreTrabajador" runat="server" CssClass="txt" 
                                                      Width="180px"></asp:TextBox>
                                              </td>
                                             
                                                  &nbsp;
                                                  <td align="left" style="width:450px">
                                                      <input id="btnTrabajador" class="Boton" style="width:20px; height:20px" 
                                        onclick="fdesUsuarios(TxtCodigoTrabajador,TxtNombreTrabajador)" type="button" 
                                        value="..." />
                                                  </td>
                                
                            </tr>
                            <tr>
                               <td align="left" style="width:150px">Fecha Inicio:&nbsp;</td>
                               <td align="left" style="width:100px"><ig:WebDatePicker ID="wdpFechaInicio" runat="server" Width="100px"></ig:WebDatePicker></td>
                                <td align="right" style="width:100px">Fecha Fin&nbsp;:</td>
                                <td align="left" style="width:450px"><ig:WebDatePicker ID="wdpFechaFin" runat="server" Width="100px"></ig:WebDatePicker></td>
                           </tr>
                        </table>
                        <table style="width:800px;height:100px"  border="0" cellpadding="0" cellspacing="0" class="FrameSimple">
      
							<tr>
								<td style="height: 5px" valign="middle"></td>
							</tr>
							    <tr>
								    <td valign="top">
                                           
                                        <asp:GridView ID="grvEval" runat="server" AutoGenerateColumns="False" 
                                                Width="720px"   DataKeyNames ="Pregunta,Porcentaje,IdPregunta">
                                                <Columns>  
                                                    <%--<asp:BoundField HeaderText="Pregunta" DataField="Pregunta" >
                                                    <HeaderStyle HorizontalAlign="Left" />
                                                    <ItemStyle HorizontalAlign="Left" Width="100px" BackColor="White" />
                                                    </asp:BoundField>--%>
                                                    <asp:TemplateField HeaderText="IdPregunta" Visible="false">
                                                        <ItemTemplate>
                                                            <asp:Label id="lblIdPregunta" runat="server" Text='<%# Bind("IdPregunta") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center"/>
                                                        <ItemStyle BackColor="White" HorizontalAlign="Center" Width="170px"/>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Pregunta">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblPregunta" runat="server" Text='<%# Bind("Pregunta") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" />
                                                        <ItemStyle BackColor="White" HorizontalAlign="Center" Width="170px" />
                                                    </asp:TemplateField>
                                                    
                                                    <asp:TemplateField HeaderText="Porcentaje" Visible="false">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblPorcentaje" runat="server" Text='<%# Bind("Porcentaje") %>'></asp:Label>
                                                        </ItemTemplate>                                                        
                                                        <HeaderStyle HorizontalAlign="Center" />
                                                        <ItemStyle BackColor="White" HorizontalAlign="Center" Width="170px" />
                                                    </asp:TemplateField>
                                                    <%--<asp:BoundField DataField="Porcentaje" Visible="false"/>--%>
                                                    <asp:TemplateField HeaderText="Evaluacion Servicio">
                                                        <ItemTemplate>
                                                            <asp:DropDownList ID="dwnRespuesta" runat="server" CssClass="cbo" Width="170PX"></asp:DropDownList>
                                                        </ItemTemplate>
                                                        <ItemStyle Width="80px" HorizontalAlign="Center" BackColor="White"/>
                                                    </asp:TemplateField>
                                                </Columns>
                                        </asp:GridView>
                                      </td>
							    </tr>	
                        </table>
                        <table style="width:300px" border="0" cellpadding="0" cellspacing="0" class="FrameSimple">
                            <tr>
                               <td align="left" style="width:150px">Resultado:&nbsp;</td>
                               <td align="left" style="width:100px" ><asp:TextBox ID="txtResultado" runat="server" CssClass="txtReadOnly" Width="130px" Enabled="false"></asp:TextBox></td>
                            </tr>
                            <tr>
                               <td align="left" style="width:150px">Observaciones:</td>
                               <td align="left" style="width:650px"><asp:TextBox ID="TxtObservaciones" runat="server" Height="30px" Width="650px"></asp:TextBox></td>
                            </tr>
                        </table>
                        <table id="tblGrabarO" style="width:800px" border="0" cellpadding="0" cellspacing="0">
                            <tr>
                               <td style="width:800px" align="center">
                                    
                                    <asp:ImageButton ID="imgGrabarFicha" runat="server" Height="25px" ImageUrl="~/images/Grabar.png" ToolTip="Grabar Conformidad" Width="25px"/>
                                    &nbsp;<asp:ImageButton ID="imgCancelar" runat="server" ImageUrl="~/images/Salir.png" ToolTip="Regresar / Cancelar" Height="25px" Width="25px"/>
                                    &nbsp;<asp:button id="btnSolicitaAprobacion" Runat="server" CssClass="Boton" Width="150px" Text="Solicitar Aprobacion" ></asp:button>
                                    &nbsp;<asp:button id="btnAdjuntos" runat="server" width="120px" Text="Ver adjuntos" CssClass="Boton"></asp:button>
                                    &nbsp;<asp:button id="btnObervar" Runat="server" CssClass="Boton" Width="150px" Text="Levantar Observaciones" OnClientClick="Confirm_LevObs()" ></asp:button>
                                    <asp:TextBox id="TxtAprobacion" runat="server" CssClass="txtHid" Width="5px"></asp:TextBox>
                                    <asp:TextBox id="TxtDescripcion" runat="server" CssClass="txtHid" Width="5px"></asp:TextBox>
                               </td>
                            </tr>
                        </table>
                        <table id="tblMsgO" style="width:800px" border="0" cellpadding="0" cellspacing="0">
                             <tr>
                              <td align="center" style="width:800px"><asp:Label ID="lblMsg" runat="server" Font-Size="10px" CssClass="error"></asp:Label></td>
                             </tr>
                             <tr>
                                <td align="center" style="width:800px"><asp:Label ID="lblError" runat="server" Font-Size="10px" CssClass="error "></asp:Label></td>
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
