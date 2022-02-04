<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="frm_ModificarFechaInstalacion.aspx.vb" Inherits="intranet_logi.frm_ModificarFechaInstalacion" %>

<%--<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">--%>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Modificación de fechas Instalación</title>
   <base target="_self"/>
   <meta name="vs_showGrid" content="True"/>
   <meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1"/>
   <meta name="CODE_LANGUAGE" content="Visual Basic .NET 7.1"/>
   <meta name="vs_defaultClientScript" content="JavaScript"/>
   <meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5"/>
   <link rel="Stylesheet" type="text/css" href="../../intranet/Estilos/Styles_Paginas.css"/>
   <link rel="Stylesheet" type="text/css" href="../../intranet/Estilos/Styles_Controles.css"/>
   <link rel="stylesheet" type="text/css" href="../css/NM0001.css"/>
  <script src="../js/jsCalendario_N3.js" type="text/javascript"></script>
   <script type="text/javascript"  language="javascript">
       function GuardarFecha() {
           var pedido = document.getElementById('lblPedido').value;
           var Fecha = document.getElementById('txtFecInstal').value;
           var response = frm_ModificarFechaInstalacion.Actualizar(pedido, Fecha);
           if (response = true) {
               
               window.close();
               return true;
           } else {
           return false
           }

   }
   function Cerrar() {
       window.close();
   }
   function GuardarFechaReq() {

       var Requi = document.getElementById('txtReq').value;
       var FechaIni = document.getElementById('txtFecIni').value;
       var FechaFin = document.getElementById('txtFecFin').value;
       var IniFec = new Date(FechaIni);
       var FinFec = new Date(FechaFin);
       if (IniFec > FinFec) {
           alert("La Fecha de Inicio no puede ser mayor a la Fecha Final");
           return false;
       }
        var response = frm_ModificarFechaInstalacion.ActualizarReq(Requi, FechaIni, FechaFin);
       if (response = true) {

           window.close();
           return true;
       } else {
           return false
       }

   }
   function CerrarReq() {
       window.close();
   }
   </script>
</head>
<body>
    <form id="form1"  method="post" autocomplete="off" runat="server">
        <center>
            <table style="width: 800px" border="0" cellspacing="0" cellpadding="0">
	           <tr>
		            <td style="width:800px;height:20px" class="Cabecera" valign="middle" align="center">MODIFICACIÓN DE FECHA INSTALACIÓN</td>
	           </tr>
	         </table> 

             <table>
                <tr>
                    <td></td>
                </tr>
             </table>

             <asp:panel id="pnlPedido" runat="server" width="900px">
	           <table style="width:800px" border="0" cellspacing="0" cellpadding="0">
               <tr>
                  <td  class="etiqueta" style="width:100px">
                    Pedido
                </td>
                <td style="width:100px" class="etiqueta">
                    <asp:TextBox id="lblPedido" class="etiqueta" BorderStyle="None" runat="server" Font-Size="9px" width="100px"></asp:TextBox>
                </td>
               </tr>
               <tr>
                    <td class="etiqueta" style="width:50px">
                        Situación
                    </td>
                    <td style="width:50px" class="etiqueta">
		                <asp:TextBox id="lblSituacion" class="etiqueta" BorderStyle="None"  runat="server" width="250px" Font-Size="9px"></asp:TextBox>
                    </td>
               </tr>
               <tr>
                     <td class="etiqueta" style="width:100px">
                        Solicitante
                     </td>
                     <td style="width:450px" class="etiqueta">
		               <asp:TextBox id="lblSolicitante" class="etiqueta" BorderStyle="None" runat="server"	Font-Size="9px" width="450px"></asp:TextBox>
                     </td>
               </tr>
               <tr>
                    <td class="etiqueta" style="width:100px">
                        Centro de costo
                    </td>
                    <td  style="width:150px" class="etiqueta">
		              <asp:TextBox id="lblCentroCosto" class="etiqueta" BorderStyle="None" runat="server" width="150px" Font-Size="9px" ></asp:TextBox>
                    </td>
               </tr>
               <tr>
                 <td class="etiqueta" style="width:100px">Almacen</td>
                 <td  style="width:200px" class="etiqueta">
		          <asp:TextBox id="lblAlmacen" class="etiqueta" BorderStyle="None" runat="server" width="200px" Font-Size="9px" ></asp:TextBox>
                 </td>
               </tr>
               <tr>
                 <td class="etiqueta" style="width:100px">Fecha Pedido</td>
                 <td  style="width:100px" class="etiqueta">
		            <asp:TextBox id="lblFecPedido" class="etiqueta" BorderStyle="None" runat="server" width="100px" Font-Size="9px" ></asp:TextBox>
                 </td>
               </tr>
               <tr>
                  <td class="etiqueta" style="width:100px">Fecha Instalación</td>
                  <td  style="width:120px" class="etiqueta">
		              <asp:textbox id="txtFecInstal" runat="server" Font-Bold="True" width="90px" Font-Size="10px" CssClass="inputDisabled"></asp:textbox>
                      <img id="imgHasta" onclick="popUpCalendar(this, form1.txtFecInstal, 'dd/mm/yyyy')" height="15"
										    alt="Seleccionar fecha" src="../../intranet/Imagenes/Calendario.gif" width="13" border="0" runat="server"/>
                  </td>
                  
               </tr>

	           </table>
               <table>
                <tr>
        
                <td style="width:100px">
                    <input class="Boton" id="btnGrabar" onclick="GuardarFecha()" type="button" value="Grabar"
										name="btnGrabar" runat="server" />
                  </td>
                  <td></td><td></td><td></td><td></td><td></td><td></td>
                  <td style="width:100px"> 
                     <input class="Boton" id="btnCerrar" onclick="Cerrar()" type="button" value="Cerrar"
										name="btnCerrar" runat="server" />
                  </td>
               </tr>
               </table>
	          </asp:panel>
              <asp:panel id="pnlReque" runat="server" width="900px">
	           <table style="width:800px" border="0" cellspacing="0" cellpadding="0">
               <tr>
                  <td  class="etiqueta" style="width:100px">
                    Tipo Requisición
                </td>
                <td style="width:100px" class="etiqueta">
                    <asp:TextBox id="txtTipReq" class="etiqueta" BorderStyle="None" runat="server" Font-Size="9px" width="100px"></asp:TextBox>
                </td>
               </tr>
               <tr>
                  <td  class="etiqueta" style="width:100px">
                    Requisición
                </td>
                <td style="width:100px" class="etiqueta">
                    <asp:TextBox id="txtReq" class="etiqueta" BorderStyle="None" runat="server" Font-Size="9px" width="100px"></asp:TextBox>
                </td>
               </tr>
               <tr>
                  <td  class="etiqueta" style="width:100px">
                    Situación
                </td>
                <td style="width:100px" class="etiqueta">
                    <asp:TextBox id="txtSitu" class="etiqueta" BorderStyle="None" runat="server" Font-Size="9px" width="100px"></asp:TextBox>
                </td>
               </tr>
               <tr>
                    <td class="etiqueta" style="width:50px">
                        Fec.creación
                    </td>
                    <td style="width:50px" class="etiqueta">
		                <asp:TextBox id="txtFec" class="etiqueta" BorderStyle="None"  runat="server" width="250px" Font-Size="9px"></asp:TextBox>
                    </td>
               </tr>
               <tr>
                     <td class="etiqueta" style="width:100px">
                        Area Beneficiaria
                     </td>
                     <td style="width:450px" class="etiqueta">
		               <asp:TextBox id="txtAreaBenef" class="etiqueta" BorderStyle="None" runat="server"	Font-Size="9px" width="450px"></asp:TextBox>
                     </td>
               </tr>
               <tr>
                    <td class="etiqueta" style="width:100px">
                        Responsable O/T
                    </td>
                    <td  style="width:150px" class="etiqueta">
		              <asp:TextBox id="txtRespo" class="etiqueta" BorderStyle="None" runat="server" width="150px" Font-Size="9px" ></asp:TextBox>
                    </td>
               </tr>
               <tr>
                 <td class="etiqueta" style="width:100px">Almacén</td>
                 <td  style="width:100px" class="etiqueta">
		            <asp:TextBox id="txtAlma" class="etiqueta" BorderStyle="None" runat="server" width="100px" Font-Size="9px" ></asp:TextBox>
                 </td>
               </tr>
               <tr>
                 <td class="etiqueta" style="width:100px">Observación</td>
                 <td  style="width:200px" class="etiqueta">
		            <asp:TextBox id="txtObs" class="etiqueta" BorderStyle="None" runat="server" width="500px" Font-Size="9px" ></asp:TextBox>
                 </td>
               </tr>
               <tr>
                  <td class="etiqueta" style="width:100px">Fecha Inicio</td>
                  <td  style="width:120px" class="etiqueta">
		              <asp:textbox id="txtFecIni" runat="server" Font-Bold="True" width="90px" Font-Size="10px" CssClass="inputDisabled"></asp:textbox>
                      <img id="img2" onclick="popUpCalendar(this, form1.txtFecIni, 'dd/mm/yyyy')" height="15"
										    alt="Seleccionar fecha" src="../../intranet/Imagenes/Calendario.gif" width="13" border="0" runat="server"/>
                  </td>
                  
               </tr>
               <tr>
                 <td class="etiqueta" style="width:100px">Fecha Fin</td>
                 <td  style="width:200px" class="etiqueta">
		            <asp:TextBox id="txtFecFin" runat="server" width="90px" Font-Size="10px" CssClass="inputDisabled"></asp:TextBox>
                       <img id="img1" onclick="popUpCalendar(this, form1.txtFecFin, 'dd/mm/yyyy')" height="15"
										    alt="Seleccionar fecha" src="../../intranet/Imagenes/Calendario.gif" width="13" border="0" runat="server"/>
                 </td>
               </tr>
	           </table>
               <table>
                <tr>
        
                <td style="width:100px">
                    <input class="Boton" id="btnGrabReq" onclick="GuardarFechaReq()" type="button" value="Grabar"
										name="btnGrabReq" runat="server" />
                  </td>
                  <td></td><td></td><td></td><td></td><td></td><td></td>
                  <td style="width:100px"> 
                     <input class="Boton" id="btnCerrarReq" onclick="CerrarReq()" type="button" value="Cerrar"
										name="btnCerrarReq" runat="server" />
                  </td>
               </tr>
               </table>
	          </asp:panel>
              
        </center>
    </form>
</body>
</html>
