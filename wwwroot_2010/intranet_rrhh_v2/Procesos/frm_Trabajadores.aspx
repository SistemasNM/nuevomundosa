<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="frm_Trabajadores.aspx.vb" Inherits="intranet_rrhh_v2.frm_Trabajadores" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
 <title>Trabajadores</title>
 <link href="../../intranet/Estilos/NM0001.css" type="text/css" rel="stylesheet"/>
 <link href="../Styles/sytle.css" type="text/css" rel="stylesheet"/>
 <script language="javascript" type="text/javascript">
   // consulta gerencias
   function fnc_BuscarGerencias() {
     strCodCenCosGer = document.all('txtCodGer').value;
     strCodCenCosJer = document.all('txtCodJef').value;
     strNivel = "1";
     var retorno = window.showModalDialog("../Buscadores/frm_ConsultaEstructuraNM.aspx?pCodCenCosGer=" + strCodCenCosGer + "&pCodCenCosJef=" + strCodCenCosGer + "&pNivel=" + strNivel, "", "dialogHeight:400px;dialogWidth:500px;center:yes;help:no;");
     if (retorno != "" && retorno != ":") {
       var datos = retorno.split(":");
       var Codigo = datos[0];
       var Nombre = datos[1];
       document.all('txtCodGer').value = Codigo;
       document.all('lblDesGer').value = Nombre;
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
         document.all('txtCodJef').value = Codigo;
         document.all('lblDesJef').value = Nombre;
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
         document.all('txtCodSup').value = Codigo;
         document.all('lblDesSup').value = Nombre;
       }
     }
   }

   // seleccion masiva
   function fnc_aprobarmasivo(pstr_id, strEmp) {
     var lstr_valores = document.all('hdnaprobarmasivo').value;
     if (pstr_id.checked == true) {
       lstr_valores = lstr_valores + strEmp + ',';
     }
     else {
       pstr_documento = strEmp + ',';
       lstr_valores = replaceAll(lstr_valores, strEmp, '');
     }
     document.all('hdnaprobarmasivo').value = lstr_valores;
   }

   //Centro de Costos
   function fnc_BuscarCentroCosto() {
     var retorno = window.showModalDialog("../../intranet/Buscadores/LOG_0001.aspx", "", "dialogheight:450px;dialogwidth:450px;center:yes;help:no;");
     if (retorno != "" && retorno != ":") {
       var datos = retorno.split(":");
       var Codigo = datos[0];
       var Nombre = datos[1];
       document.all('txtCodCenCos').value = Codigo;
       document.all('txtDesCenCos').value = Nombre;
     }
   }

    </script>
</head>
<body>
  <form id="frm_Trabajadores" runat="server">
   <%--titulo--%>
    <div>
     <table style="width:800px" cellspacing="0" cellpadding="0" border="0">
	  <tr>
	   <td class="titulo" style="width:800px; height:20px" align="center">ASIGNAR TRABAJADORES</td>
	  </tr>
      <tr>
	   <td style="width:800px; height:5px"></td>
	  </tr>
     </table>
    </div>

   <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager> 
   <asp:UpdatePanel ID="panContenido" runat="server" UpdateMode="Always">
   <ContentTemplate>
  
    <div>
     <table style="width:800px" cellspacing="0" cellpadding="0" border="0">
	  <tr>
	   <td style="width:100px" class="Etiqueta">Elegir gerencia:</td>
       <td style="width:100px">
        <asp:TextBox ID="txtCodGer" runat="server" Text="" Width="50px" CssClass="input" Font-Size="9px"></asp:TextBox>
        <input id="btnGerencia" style="width: 20px" onclick="javascript:fnc_BuscarGerencias();" type="button" value="..." name="btnGerencia" class="Boton"/>
       </td>
       <td style="width:400px" align="left">
        <asp:TextBox ID="lblDesGer" runat="server" Text="" Font-Size="9px" CssClass="Etiqueta" BorderStyle="None" Width="400px"></asp:TextBox>
       </td>
       <td style="width:200px" align="right">
        <asp:Button ID="btnConsultar" runat="server" Text="Consultar" CssClass="btnAzul" />
       </td>
	  </tr>
      <tr>
	   <td style="width:100px" class="Etiqueta">Elegir Jefatura:</td>
       <td style="width:100px">
        <asp:TextBox ID="txtCodJef" runat="server" Text="" Width="50px" CssClass="input" Font-Size="9px"></asp:TextBox>
        <input id="btnJefatura" style="width: 20px" onclick="javascript:fnc_BuscarJefaturas();" type="button" value="..." name="btnSupervisor" class="Boton"/>
       </td>
       <td style="width:400px" align="left">
        <asp:TextBox ID="lblDesJef" runat="server" Text = "" Font-Size = "9px" CssClass="Etiqueta" BorderStyle="None" Width="400px"></asp:TextBox>
       </td>
       <td style="width:200px" align="right">
        <asp:Button ID="btnAsignar" runat="server" Text="Asignar" CssClass="btnAzul" />
       </td>
	  </tr>
      <tr>
	   <td style="width:100px" class="Etiqueta">Elegir Supervisor:</td>
       <td style="width:100px">
        <asp:TextBox ID="txtCodSup" runat="server" Text="" Width="50px" CssClass="input" Font-Size="9px"></asp:TextBox>
        <input id="btnSupervisor" style="width: 20px" onclick="javascript:fnc_BuscarSupervisores();" type="button" value="..." name="btnSupervisor" class="Boton"/>
       </td>
       <td style="width:400px" align="left">
        <asp:TextBox ID="lblDesSup" runat="server" Text = "" Font-Size = "9px"  CssClass="Etiqueta" BorderStyle="None" Width="400px"></asp:TextBox>
       </td>
       <td style="width:200px" align="right">
        <input style="width:1px; height: 1px" id="hdnaprobarmasivo" type="hidden" name="hdnaprobarmasivo" runat="server"/>
       </td>
	  </tr>
      <tr>
	   <td style="width:100px" class="Etiqueta">Situacion:</td>
       <td style="width:100px">
        <asp:DropDownList ID="ddlEstado" runat="server" Width="100px" CssClass="input" Font-Size="9px">
         <asp:ListItem Value="0" Selected="True">SIN ASIGNAR</asp:ListItem>
         <asp:ListItem Value="1">ASIGNADOS</asp:ListItem>
        </asp:DropDownList>
       </td>
       <td style="width:400px" align="left"></td>
       <td style="width:200px" align="right"></td>
	  </tr>
      <tr>
       <td align="left" style="width:800px" colspan="4">
        <asp:Label ID="lblError" runat="server" Text="" CssClass="mensaje"></asp:Label>
       </td>
      </tr>
      <tr>
       <td align="right" style="width:800px" colspan="4">
        <asp:label id="lblItems" runat="server" text="" CssClass="contador"></asp:label>
       </td>  
      </tr>
     </table>
     </div>

    <div>
     <%--panel de edicio de supervisores--%>
     <asp:Panel ID="pnlEditar" runat="server" Width="600px" Height="100px" Visible="false" CssClass="pnlModal">
      <table style="width:600px">
       <tr>
        <td style="width:580px; height:15px" class="pnlTitulo" colspan="4" align="center">Asignar supervisor
         <input style="width:1px; height:1px" id="hdnCodigoGerencia" type="hidden" name="hdnCodigoGerencia" runat="server" />
         <input style="width:1px; height:1px" id="hdnCodigoJefatura" type="hidden" name="hdnCodigoJefatura" runat="server" />
         <input style="width:1px; height:1px" id="hdnCodigoSupervisor" type="hidden" name="hdnCodigoSupervisor" runat="server" />
         <input style="width:1px; height:1px" id="hdnCodigoTrabajador" type="hidden" name="hdnCodigoTrabajador" runat="server" />
         <input style="width:1px; height:1px" id="hdnCodigoEmpleado" type="hidden" name="hdnCodigoEmpleado" runat="server" />
         <input style="width:1px; height:1px" id="hdnAccion" type="hidden" name="hndAccion" runat="server" />
        </td>
        <td style="width:20px; height:15px" align="center">
         <asp:ImageButton ID="btnCerrar" runat="server" ImageUrl="../../intranet/Imagenes/cerrar.bmp" Width="18px" Height= "18px" ToolTip="Cerrar"/>
        </td>
       </tr>
       <tr>
        <td style="width:100px; height:10px" class="ui-widget-header">Codigo CC</td>
        <td style="width:240px; height:10px" class="ui-widget-header">Descripcion Cen. Cos.</td>
        <td style="width:240px; height:10px" class="ui-widget-header">Supervisores</td>
        <td style="width:20px; height:10px" class="ui-widget-header"></td>
       </tr>
       <tr>
        <td style="width:100px; height:20px" class="GridAlternateItem">
         <asp:TextBox ID="txtCodCenCos" Text ="" runat="server" Width="40px" CssClass="input" Font-Size="9px"></asp:TextBox>
         <input id="btnCentroCosto" style="width: 20px" onclick="javascript:fnc_BuscarCentroCosto();" type="button" value="..." name="btnCentroCosto" class="Boton" runat="server"/></td>
        <td style="width:240px; height:20px" class="GridAlternateItem">
         <asp:TextBox ID="txtDesCenCos" Text ="" runat="server" Width="230px" Font-Size="9px" CssClass="inputDisabled2" BorderStyle="None" BorderWidth="0"></asp:TextBox>
         <%--<asp:Label ID="lblDesCenCos" Text ="" runat="server" Width="230px" Font-Size="9px"></asp:Label>--%>
        </td>
        <td style="width:240px; height:20px" class="GridAlternateItem">
         <asp:DropDownList ID="ddlSupervisores" runat="server" CssClass="input" Font-Size="9px" Width="200px"></asp:DropDownList>
        </td>
        <td style="width:20px; height:20px" class="GridAlternateItem">
         <asp:ImageButton id="btnGrabar" runat="server" ImageUrl="../../intranet/Imagenes/Grabar.gif" CommandName="Grabar" Width="16px" Height="16px"></asp:ImageButton>
        </td>
       </tr>
      </table>
     </asp:Panel>
    </div>

    <%--grilla--%>
    <div>
     <table style="width:1000px" cellspacing="0" cellpadding="0" border="0">
      <tr>
       <td style="width:1000px">
        <asp:datagrid id="dgTrabajador" runat="server" Width="1000px" AutoGenerateColumns="False" AllowSorting="True">
	     <AlternatingItemStyle CssClass="GridAltItem"></AlternatingItemStyle>
	     <ItemStyle CssClass="GridItem"></ItemStyle>
	     <HeaderStyle CssClass="GridHeader"></HeaderStyle>
         <FooterStyle CssClass="GridFooter"></FooterStyle>
	     <Columns>
          
         <%--Check asignar masivo. --%>
         <asp:TemplateColumn>
          <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="20px" Font-Bold="true"></HeaderStyle>
		  <ItemTemplate><asp:CheckBox id="chkSeleccion" runat="server" ToolTip="Marque para asignar" Width="16px" Height="16px"></asp:CheckBox></ItemTemplate>
		 </asp:TemplateColumn>
           
         <asp:TemplateColumn HeaderText="Cod. Ger." Visible="false">
          <HeaderStyle HorizontalAlign="Center" Width="50px"></HeaderStyle>
          <ItemTemplate>
           <asp:Label id="lblCodGerencia" Width="50px" text='<%# Container.dataitem("vch_CodigoGer") %>' runat="server" Font-Size="9px" Visible="false"></asp:Label>
		  </ItemTemplate>
         </asp:TemplateColumn>

         <asp:TemplateColumn HeaderText="Cod. Jef." Visible="false">
          <HeaderStyle HorizontalAlign="Center" Width="50px"></HeaderStyle>
          <ItemTemplate>
           <asp:Label id="lblCodJefatura" Width="50px" text='<%# Container.dataitem("vch_CodigoJef") %>' runat="server" Font-Size="9px" Visible="false"></asp:Label>
		  </ItemTemplate>
         </asp:TemplateColumn>

         <asp:TemplateColumn HeaderText="Cod. Sup." Visible="false">
          <HeaderStyle HorizontalAlign="Center" Width="50px"></HeaderStyle>
          <ItemTemplate>
           <asp:Label id="lblCodSupervisor" Width="50px" text='<%# Container.dataitem("vch_CodigoSup") %>' runat="server" Font-Size="9px" Visible="false"></asp:Label>
		  </ItemTemplate>
         </asp:TemplateColumn>

         <asp:TemplateColumn HeaderText="Cod. Trab." Visible="false">
          <HeaderStyle HorizontalAlign="Center" Width="50px"></HeaderStyle>
          <ItemTemplate>
           <asp:Label id="lblCodTrabajador" Width="50px" text='<%# Container.dataitem("vch_CodigoTrab") %>' runat="server" Font-Size="9px" Visible="false"></asp:Label>
		  </ItemTemplate>
         </asp:TemplateColumn>

         <asp:TemplateColumn HeaderText="Responsable">
          <HeaderStyle HorizontalAlign="Center" Width="250px"></HeaderStyle>
          <ItemTemplate>
           <asp:Label id="lblSupRes" Width="250px" text='<%# Container.dataitem("vch_SuperResp") %>' runat="server" Font-Size="9px"></asp:Label>
		  </ItemTemplate>
         </asp:TemplateColumn>

         <asp:TemplateColumn HeaderText="Centro Costo">
          <HeaderStyle HorizontalAlign="Center" Width="80px"></HeaderStyle>
          <ItemTemplate>
           <asp:Label id="lblCodCenCos" Width="50px" text='<%# Container.dataitem("vch_CenCosTrab") %>' runat="server" Font-Size="9px" ></asp:Label>
		  </ItemTemplate>
         </asp:TemplateColumn>

         <asp:TemplateColumn HeaderText="Descripcion Centro Costo">
          <HeaderStyle HorizontalAlign="Center" Width="250px"></HeaderStyle>
          <ItemTemplate>
           <asp:Label id="lblDesCenCos" Width="250px" text='<%# Container.dataitem("vch_CenCosDes") %>' runat="server" Font-Size="9px" ></asp:Label>
		  </ItemTemplate>
         </asp:TemplateColumn>

         <asp:TemplateColumn HeaderText="Codigo Emp.">
          <HeaderStyle HorizontalAlign="Center" Width="75px"></HeaderStyle>
          <ItemTemplate>
           <asp:Label id="lblCodEmp" Width="75px" text='<%# Container.dataitem("vch_CodigoEmpleado") %>' runat="server" Font-Size="9px" ></asp:Label>
		  </ItemTemplate>
         </asp:TemplateColumn>

         <asp:TemplateColumn HeaderText="Nombres Empleado">
          <HeaderStyle HorizontalAlign="Center" Width="250px"></HeaderStyle>
          <ItemTemplate>
           <asp:Label id="lblDesEmp" Width="250px" text='<%# Container.dataitem("Nombres") %>' runat="server" Font-Size="9px" ></asp:Label>
		  </ItemTemplate>
         </asp:TemplateColumn>

         <%--elimina--%>
         <asp:TemplateColumn>
		  <HeaderStyle Width="20px"></HeaderStyle>
		  <ItemTemplate>
		   <asp:ImageButton id="btnEliminarItem" runat="server" ImageUrl="../../intranet/Imagenes/delete.gif" CommandName="Eliminar" CommandArgument='<%# DataBinder.Eval(Container.DataItem,"vch_CodigoGer")%>' Width="16px" Height="16px"></asp:ImageButton>
		  </ItemTemplate>
		 </asp:TemplateColumn>

        </Columns>
       </asp:datagrid>
      </td>
     </tr>
    </table>
   </div>
   </ContentTemplate>

   <Triggers>
    <asp:AsyncPostBackTrigger ControlID ="btnConsultar" EventName="click" />
    <asp:AsyncPostBackTrigger ControlID ="btnAsignar" EventName="click" />
   </Triggers>
   </asp:UpdatePanel>

  </form>
 </body>
</html>
