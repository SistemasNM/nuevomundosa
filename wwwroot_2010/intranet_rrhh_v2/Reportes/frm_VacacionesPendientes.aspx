<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="frm_VacacionesPendientes.aspx.vb" Inherits="intranet_rrhh.frm_VacacionesPendientes" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
 <title>Vacaciones Pendientes</title>
 <link href="../../intranet/Estilos/NM0001.css" type="text/css" rel="stylesheet"/>
 <link href="../Styles/sytle.css" type="text/css" rel="stylesheet"/>
 <script language="javascript" type="text/javascript">
     // Mostrar reporte
     function fMostrarReporte(strURL) {
         var intWidth = screen.width;
         var intHeight = screen.height;
         window.open(strURL, "", "top=0; left=0; width=" + intWidth + "; height=" + intHeight + "; resizable=1;");
     }

     function fnc_BuscarGerencias() {
         strCodCenCosGer = document.all('txtCodGer').value;
         strCodCenCosJer = document.all('txtCodJef').value;
         strNivel = "1";
         var retorno = window.showModalDialog("../Buscadores/frm_ConsultaEstructuraNM.aspx?pCodCenCosGer=" + strCodCenCosGer + "&pCodCenCosJef=" + strCodCenCosGer + "&pNivel=" + strNivel, "", "dialogHeight:400px;dialogWidth:500px;center:yes;help:no;");
         if (retorno != "" && retorno != ":") {
             var datos = retorno.split(":");
             var Codigo = datos[0];
             var Nombre = datos[1];
             var CodGer = datos[2];
             document.all('txtCodGer').value = Codigo;
             document.all('lblDesGer').value = Nombre;
             document.all('hdnCodEmpGer').value = CodGer;
         }
     }

     // consulta jefaturas
     function fnc_BuscarJefaturas() {
         strCodCenCosGer = document.all('txtCodGer').value;
         strCodCenCosJer = document.all('txtCodJef').value;
         strNivel = "2";
         if (strCodCenCosGer == "") {
             window.alert("Debe elegir una gerencia.");
             return false;
         }
         else {
             var retorno = window.showModalDialog("../Buscadores/frm_ConsultaEstructuraNM.aspx?pCodCenCosGer=" + strCodCenCosGer + "&pCodCenCosJef=" + strCodCenCosGer + "&pNivel=" + strNivel, "", "dialogHeight:400px;dialogWidth:500px;center:yes;help:no;");
             if (retorno != "" && retorno != ":") {
                 var datos = retorno.split(":");
                 var Codigo = datos[0];
                 var Nombre = datos[1];
                 var CodJef = datos[2];
                 document.all('txtCodJef').value = Codigo;
                 document.all('lblDesJef').value = Nombre;
                 document.all('hdnCodEmpJef').value = CodJef;
             }
         }
     }

     // consulta supervisores
     function fnc_BuscarSupervisores() {
         strCodCenCosGer = document.all('txtCodGer').value;
         strCodCenCosJer = document.all('txtCodJef').value;
         strNivel = "3";
         if (strCodCenCosGer == "" || strCodCenCosJer == "") {
             window.alert("Debe elegir una gerencia y jefatura previamente.");
             return false;
         }
         else {
             var retorno = window.showModalDialog("../Buscadores/frm_ConsultaEstructuraNM.aspx?pCodCenCosGer=" + strCodCenCosGer + "&pCodCenCosJef=" + strCodCenCosJer + "&pNivel=" + strNivel, "", "dialogHeight:400px;dialogWidth:500px;center:yes;help:no;");
             if (retorno != "" && retorno != ":") {
                 var datos = retorno.split(":");
                 var Codigo = datos[0];
                 var Nombre = datos[1];
                 var CodSup = datos[2];
                 document.all('txtCodSup').value = Codigo;
                 document.all('lblDesSup').value = Nombre;
                 document.all('hdnCodEmpSup').value = CodSup;
             }
         }
     }
 </script>
</head>
<body>
 <form id="frm_VacacionesPendientes" runat="server">
  <%--titulo--%>
  <div>
   <table style="width:800px" cellspacing="0" cellpadding="0" border="0">
	<tr>
	 <td class="titulo" style="width:800px; height:20px" align="center">LISTADO DE VACACIONES PENDIENTES</td>
	</tr>
    <tr>
	 <td style="width:800px; height:5px"></td>
    </tr>
    <tr>
     <td align="right" style="width:800px" colspan="4">
      <asp:label id="lblUsuario" runat="server" CssClass="contador"></asp:label>
      <input style="WIDTH: 10px; HEIGHT: 10px" id="hdnasignamasivo" size="1" type="hidden" name="hdnaprobarmasivo" runat="server"/>
     </td>  
    </tr>
   </table>
  </div>
   
  <div>
   <table style="width:800px" cellspacing="0" cellpadding="0" border="0">
	<tr>
	 <td style="width:100px" class="Etiqueta">Elegir gerencia:</td>
     <td style="width:100px">
      <asp:TextBox ID="txtCodGer" runat="server" Text="" Width="70px" CssClass="input" Font-Size="9px"></asp:TextBox>
      <input id="btnGerencia" style="width: 20px" onclick="javascript:fnc_BuscarGerencias();" type="button" value="..." name="btnGerencia" class="Boton"/>
     </td>
     <td style="width:400px" align="left">
      <input style="width:1px; height:1px" id="hdnCodEmpGer" type="hidden" name="hdnCodEmpGer" runat="server" />
      <asp:TextBox ID="lblDesGer" runat="server" Text="" Font-Size="9px" class="Etiqueta" BorderStyle="None" Width="400px"></asp:TextBox>
     </td>
     <td style="width:200px" align="right">
      <asp:Button ID="btnConsultar" runat="server" Text="Consultar" CssClass="btnAzul" />
     </td>
	</tr>

    <tr>
	 <td style="width:100px" class="Etiqueta">Elegir Jefatura:</td>
     <td style="width:100px">
      <asp:TextBox ID="txtCodJef" runat="server" Text="" Width="70px" CssClass="input" Font-Size="9px"></asp:TextBox>
      <input id="btnJefatura" style="width: 20px" onclick="javascript:fnc_BuscarJefaturas();" type="button" value="..." name="btnSupervisor" class="Boton"/>
    </td>
    <td style="width:400px" align="left">
     <input style="width:1px; height:1px" id="hdnCodEmpJef" type="hidden" name="hdnCodEmpJef" runat="server" />
     <asp:TextBox ID="lblDesJef" runat="server" Text = "" Font-Size = "9px" class="Etiqueta" BorderStyle="None" Width="400px"></asp:TextBox>
    </td>
    <td style="width:200px" align="right"></td>
   </tr>
   <tr>
    <td style="width:100px" class="Etiqueta">Elegir Supervisor:</td>
    <td style="width:100px">
     <asp:TextBox ID="txtCodSup" runat="server" Text="" Width="70px" CssClass="input" Font-Size="9px"></asp:TextBox>
     <input id="btnSupervisor" style="width: 20px" onclick="javascript:fnc_BuscarSupervisores();" type="button" value="..." name="btnSupervisor" class="Boton"/>
    </td>
    <td style="width:400px" align="left">
     <input style="width:1px; height:1px" id="hdnCodEmpSup" type="hidden" name="hdnCodEmpSup" runat="server" />
     <asp:TextBox ID="lblDesSup" runat="server" Text = "" Font-Size = "9px" class="Etiqueta" BorderStyle="None" Width="400px"></asp:TextBox>
    </td>
    <td style="width:200px" align="right"></td>
   </tr>
   <tr>
    <td align="left" style="width:800px" colspan="4">
     <asp:Label ID="lblError" runat="server" Text="" CssClass="mensaje"></asp:Label>
    </td>
   </tr>
  </table>
 </div>
</form>
</body>
</html>
