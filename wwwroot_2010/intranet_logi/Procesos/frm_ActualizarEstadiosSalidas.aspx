<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="frm_ActualizarEstadiosSalidas.aspx.vb" Inherits="intranet_logi.frm_ActualizarEstadiosSalidas" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../css/Styles_Paginas.css" type="text/css" rel="Stylesheet" />
    <link href="../css/Styles_Controles.css" type="text/css" rel="Stylesheet" />
    <link href="../css/NM0001.css" rel="Stylesheet" type="text/css"/>
     <script src="../js/jsCalendario_N3.js" type="text/javascript"></script>
     <script type="text/javascript"  language="javascript">

         function fnc_Estado(id,lnk) {

             //var obj = eval("form1." + id + "_" + txtlId);
             //var strSalida = obj.value;
             var strSalida = document.getElementById('txtSalida').value;

             var row = lnk.parentNode.parentNode;
             var rowIndex = row.rowIndex - 1;

             var strSalida =  document.getElementById(id + '_txtSalida_' + rowIndex).value;
             //document.getElementById(id + '_lblDescCeco_' + rowIndex).value = nombre;

             var retorno = window.showModalDialog("../Buscadores/frmActualizarSalida.aspx?Salida=" + strSalida, "", "dialogHeight:300px;dialogWidth:300px;center:yes;help:no;");
//             if (retorno != "" && retorno != ":") {
//                 var datos = retorno.split(":");
//                 var codigo = datos[0];
//                 var nombre = datos[1];

//                 document.getElementById("txtCodOT").value = codigo;
//                 document.getElementById("txtDesOT").value = nombre;
//               }

             form1.hdnActualizar.value = "SI";
             form1.submit();
         }

         function btnSeleccion_Onclick(strSalida,strUsuario) {
             //window.location.href = "frmFichaProveedor.aspx?strNumeroOrdenServicio=" + strCodigo;
             
                 var WinSettings = "status:no;center:yes;resizable:no;dialogHeight:600px;dialogWidth:900px;scroll:no";
                 var returnValue = window.showModalDialog("../Buscadores/frmActualizarSalida.aspx?Salida=" + strSalida + "&Usuario=" + strUsuario, null, WinSettings);

                 form1.hdnActualizar.value = "SI";
                 form1.submit();
         }

     </script>
</head>
<body>
    <form id="form1" runat="server">
        <center>
            <asp:Panel ID="pnlcabecera" runat="server" HorizontalAlign="Center">
                <table style="width:800px;" border="0" cellpadding="0" cellspacing="0" >
                        <tr>
                            <td class="Cabecera" style="width:800px; height:30px" align="center" valign="middle">&nbsp;LISTADO DE SALIDAS - ACTUALIZACIÓN DE ESTADOS</td>
                        </tr>
                        <tr>
                            <td>&nbsp;</td>
                        </tr>
                    </table>
            </asp:Panel>
                <table  style="width:500px" border="0" cellpadding="0" cellspacing="0">
                     <tr style="height: 25px">
                        <td style="width:150px" class="Etiqueta" align="left">&nbsp;Fecha Inicio:</td>
                        <td style="width:150px" class="Etiqueta" align="left"><asp:TextBox Width="80px" runat="server" ID="txtFecIni"></asp:TextBox>
                            &nbsp;<img onclick="popUpCalendar(this, form1.txtFecIni, 'dd/mm/yyyy')" border="0"
		                                    alt="Seleccionar fecha Inicial" src="../images/Calendario.gif" width="16px" height="16px"  />
                        </td>
                        <td class="Etiqueta"  align="right" colspan="4">&nbsp;</td>
                     </tr>
                      <tr style="height: 25px">
                        <td style="width:150px" class="Etiqueta" align="left">&nbsp;Fecha Fin:</td>
                        <td style="width:150px" class="Etiqueta" align="left"><asp:TextBox Width="80px" runat="server" ID="txtFecFin"></asp:TextBox>
                            &nbsp;<img onclick="popUpCalendar(this, form1.txtFecFin, 'dd/mm/yyyy')" border="0"
		                                    alt="Seleccionar fecha Inicial" src="../images/Calendario.gif" width="16px" height="16px"  />
                        </td>
                        <td class="Etiqueta"  align="right" colspan="4">&nbsp;</td>
                     </tr>
                      <tr style="height: 25px">
                           <td style="width:150px" class="Etiqueta" align="left">&nbsp;#Salida:</td>
                           <td style="width:150px" class="Etiqueta" align="left"><asp:TextBox Width="80px" runat="server" ID="txtSalida"></asp:TextBox></td>
                           <%--<td class="Etiqueta"  align="center" colspan="4"><asp:ImageButton runat="server" ID="Buscar" OnClientClick="fnc_Estado(1)" ImageUrl="~/Imagenes/Buscar.png"/></td>--%>
                           <td class="Etiqueta"  align="center" colspan="4"><asp:ImageButton runat="server" ID="ImageButton1" ImageUrl="~/Imagenes/Buscar.png"/></td>
                      </tr>
                     
                       <tr>
                         <td>&nbsp;</td>
                         <td>&nbsp;</td>
                         <td>&nbsp;</td>
                     </tr>
                </table>
                
                <div style="width:610px;height:200px;overflow:scroll">
                <table style="width:600px">
                    <asp:DataGrid runat="server" ID="grdSalida" Width="600px" AutoGenerateColumns="false">
                     <AlternatingItemStyle CssClass="GridAltItem"></AlternatingItemStyle>
		            <ItemStyle CssClass="GridItem"></ItemStyle>
		            <HeaderStyle CssClass="GridHeader"></HeaderStyle>
                        <Columns>
                               
                                <asp:BoundColumn DataField="SALIDA" HeaderText="#SALIDA">
                                  <HeaderStyle HorizontalAlign="Center" Width="50px" VerticalAlign="Middle" Font-Bold="true"></HeaderStyle>
                                  <ItemStyle Font-Size="9px" HorizontalAlign="left" VerticalAlign="Middle"></ItemStyle>
                                 </asp:BoundColumn>

                                

                                <asp:BoundColumn DataField="CLIENTE" HeaderText="CLIENTE">
                                  <HeaderStyle HorizontalAlign="Center" Width="100px" VerticalAlign="Middle" Font-Bold="true"></HeaderStyle>
                                  <ItemStyle Font-Size="9px" HorizontalAlign="left" VerticalAlign="Middle"></ItemStyle>
                                 </asp:BoundColumn>

                               

                                <asp:BoundColumn DataField="FECHA" HeaderText="FECHA">
                                  <HeaderStyle HorizontalAlign="Center" Width="100px" VerticalAlign="Middle" Font-Bold="true"></HeaderStyle>
                                  <ItemStyle Font-Size="9px" HorizontalAlign="left" VerticalAlign="Middle"></ItemStyle>
                                 </asp:BoundColumn>

                                

                                <asp:BoundColumn DataField="TURNO" HeaderText="TURNO">
                                  <HeaderStyle HorizontalAlign="Center" Width="50px" VerticalAlign="Middle" Font-Bold="true"></HeaderStyle>
                                  <ItemStyle Font-Size="9px" HorizontalAlign="left" VerticalAlign="Middle"></ItemStyle>
                                 </asp:BoundColumn>

                                <asp:TemplateColumn HeaderText="ESTADO">
                                    <ItemStyle Width="10px" />
                                    
                                    <ItemTemplate>
                                        <asp:label ID="txtEstado" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Estado") %>'></asp:label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                 <asp:TemplateColumn >
                                     <ItemStyle Width="10px" />
                                     <ItemTemplate>
                                        <input id="btnBuscarEstadosalida" runat="server"  style="width:18px;height:18px" type="button"/>
                                     </ItemTemplate>
                                 </asp:TemplateColumn>
                        </Columns>
                    </asp:DataGrid>
                </table>
                </div>
                
                
        </center>
        <tr>
            <td>
                <input id="hdnActualizar" style="WIDTH: 40px; HEIGHT: 22px" type="hidden" size="1" name="hdnActualizar" runat="server" />
            </td>
        </tr>
    </form>
</body>
</html>
