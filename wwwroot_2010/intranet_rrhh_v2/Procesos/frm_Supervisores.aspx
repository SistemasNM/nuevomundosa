<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="frm_Supervisores.aspx.vb" Inherits="intranet_rrhh_v2.frm_Supervisores" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Supervisores</title>
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
          var retorno = window.showModalDialog("../Buscadores/frm_ConsultaEstructuraNM.aspx?pCodCenCosGer=" + strCodCenCosGer + "&pCodCenCosJef=" + strCodCenCosJer + "&pNivel=" + strNivel, "", "dialogHeight:400px;dialogWidth:500px;center:yes;help:no;");
          if (retorno != "" && retorno != ":") {
            var datos = retorno.split(":");
            var Codigo = datos[0];
            var Nombre = datos[1];
            document.all('txtCodJef').value = Codigo;
            document.all('lblDesJef').value = Nombre;
          }
        }
      }

      //Empleados
      function fnc_Buscartrabajadores() {
        var tipo = "EMP";
        var retorno = window.showModalDialog("../../intranet/Buscadores/PLA_0001.aspx?strTipo=" + tipo, "", "dialogheight:450px;dialogwidth:450px;center:yes;help:no;");
        if (retorno != "" && retorno != ":") {
          var datos = retorno.split(":");
          var codigo = datos[0];
          var nombre = datos[1];
          document.all('txtCodEmp').value = codigo;
          document.all('txtDesEmp').value = nombre;
        }
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
 <form id="frm_Supervisores" runat="server">
  <%--titulo--%>
  <div style="width:800px">
   <table style="width:800px" cellspacing="0" cellpadding="0" border="0">
	<tr>
	 <td class="titulo" style="width:800px; height:20px" align="center">ASIGNAR SUPERVISORES</td>
	</tr>
    <tr>
	 <td style="width:800px; height:10px"></td>
	</tr>
   </table>
  </div>
  <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager> 
   <asp:UpdatePanel ID="panContenido" runat="server" UpdateMode="Always">
    <ContentTemplate>
     <div style="width:800px">
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
         <input id="btnSupervisor" style="width: 20px" onclick="javascript:fnc_BuscarJefaturas();" type="button" value="..." name="btnSupervisor" class="Boton"/>
        </td>
        <td style="width:400px" align="left">
         <asp:TextBox ID="lblDesJef" runat="server" Text = "" Font-Size="9px" CssClass="Etiqueta" BorderStyle="None" Width="400px"></asp:TextBox>
        </td>
        <td style="width:200px" align="right">
         <asp:Button ID="btnNuevo" runat="server" Text="Nuevo" CssClass="btnAzul" />
        </td>
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
       
       <%--grilla--%>
       <tr>
        <td style="width:800px" colspan="4">
         <asp:datagrid id="dgSupervisores" runat="server" Width="800px" AutoGenerateColumns="False" AllowSorting="True">
	      <AlternatingItemStyle CssClass="GridAltItem"></AlternatingItemStyle>
	      <ItemStyle CssClass="GridItem"></ItemStyle>
	      <HeaderStyle CssClass="GridHeader"></HeaderStyle>
          <FooterStyle CssClass="GridFooter"></FooterStyle>
	      <Columns>
         
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

           <asp:TemplateColumn HeaderText="Cod. Sup.">
            <HeaderStyle HorizontalAlign="Center" Width="50px"></HeaderStyle>
            <ItemTemplate>
             <asp:Label id="lblCodSupervisor" Width="50px" text='<%# Container.dataitem("vch_CodigoSup") %>' runat="server" Font-Size="9px" ></asp:Label>
		    </ItemTemplate>
           </asp:TemplateColumn>

           <asp:TemplateColumn HeaderText="Centro Costo">
            <HeaderStyle HorizontalAlign="Center" Width="50px"></HeaderStyle>
            <ItemTemplate>
             <asp:Label id="lblCodCenCos" Width="50px" text='<%# Container.dataitem("vch_CenCosSup") %>' runat="server" Font-Size="9px" ></asp:Label>
		    </ItemTemplate>
           </asp:TemplateColumn>

           <asp:TemplateColumn HeaderText="Descripcion Centro Costo">
            <HeaderStyle HorizontalAlign="Center" Width="200px"></HeaderStyle>
            <ItemTemplate>
             <asp:Label id="lblDesCenCos" Width="250px" text='<%# Container.dataitem("vch_CenCosDes") %>' runat="server" Font-Size="9px" ></asp:Label>
		    </ItemTemplate>
           </asp:TemplateColumn>

           <asp:TemplateColumn HeaderText="Codigo Emp.">
            <HeaderStyle HorizontalAlign="Center" Width="80px"></HeaderStyle>
            <ItemTemplate>
             <asp:Label id="lblCodEmp" Width="80px" text='<%# Container.dataitem("vch_CodigoEmpleado") %>' runat="server" Font-Size="9px" ></asp:Label>
		    </ItemTemplate>
           </asp:TemplateColumn>

           <asp:TemplateColumn HeaderText="Nombres Empleado">
            <HeaderStyle HorizontalAlign="Center" Width="250px"></HeaderStyle>
            <ItemTemplate>
             <asp:Label id="lblDesEmp" Width="200px" text='<%# Container.dataitem("Nombres") %>' runat="server" Font-Size="9px" ></asp:Label>
		    </ItemTemplate>
           </asp:TemplateColumn>

           <%--edita--%>
           <asp:TemplateColumn>
		    <HeaderStyle Width="20px"></HeaderStyle>
		    <ItemStyle HorizontalAlign="Center"></ItemStyle>
		    <ItemTemplate>
		     <asp:ImageButton id="btnEditar" runat="server" ImageUrl="../../intranet/Imagenes/edit.gif" CommandName="Editar" width="16px" height="16px"></asp:ImageButton>
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

     <%--panel de edicio de supervisores--%>
     <asp:Panel ID="pnlEditar" runat="server" Width="700px" Height="100px" Visible="false" CssClass="pnlModal">
      <table style="width:700px">
       <tr>
        <td style="width:650px; height:15px" class="pnlTitulo" colspan="4" align="center">Editar supervisor
         <input style="width:1px; height:1px" id="hdnCodigoGerencia" type="hidden" name="hdnCodigoGerencia" runat="server" />
         <input style="width:1px; height:1px" id="hdnCodigoJefatura" type="hidden" name="hdnCodigoJefatura" runat="server" />
         <input style="width:1px; height:1px" id="hdnCodigoSupervisor" type="hidden" name="hdnCodigoSupervisor" runat="server" />
         <input style="width:1px; height:1px" id="hdnAccion" type="hidden" name="hndAccion" runat="server" />
        </td>
        <td style="width:20px; height:15px" align="center">
         <asp:ImageButton ID="btnCerrar" runat="server" ImageUrl="../../intranet/Imagenes/cerrar.bmp" Width="18px" Height= "18px" ToolTip="Cerrar"/>
        </td>
       </tr>
       <tr>
        <td style="width:100px; height:10px" class="ui-widget-header">Codigo CC</td>
        <td style="width:230px; height:10px" class="ui-widget-header">Descripcion Cen. Cos.</td>
        <td style="width:100px; height:10px" class="ui-widget-header">Codigo Emp.</td>
        <td style="width:230px; height:10px" class="ui-widget-header">Nombres</td>
        <td style="width:20px; height:10px" class="ui-widget-header"></td>
       </tr>
       <tr>
        <td style="width:100px; height:20px" class="GridAlternateItem">
         <asp:TextBox ID="txtCodCenCos" Text ="" runat="server" Width="50px" CssClass="inputDisabled2" Font-Size="9px"></asp:TextBox>
         <input id="btnCentroCosto" style="width: 20px" onclick="javascript:fnc_BuscarCentroCosto();" type="button" value="..." name="btnCentroCosto" class="Boton" runat="server"/></td>
        <td style="width:230px; height:20px" class="GridAlternateItem">
         <asp:TextBox ID="txtDesCenCos" Text ="" runat="server" Width="220px" CssClass="inputDisabled2" Font-Size="9px" BorderStyle="None" BorderWidth="0"></asp:TextBox>
         <%--<asp:Label ID="lblDesCenCos" Text ="" runat="server" Width="240px" Font-Size="9px"></asp:Label>--%>
        </td>
        <td style="width:100px; height:20px" class="GridAlternateItem">
         <asp:TextBox ID="txtCodEmp" Text ="" runat="server" Width="50px" CssClass="inputDisabled2" Font-Size="9px"></asp:TextBox>
         <input id="btnSolicitante" style="width:20px;" onclick="javascript:fnc_Buscartrabajadores();" type="button" value="..." name="btnSolicitante" class="Boton" runat="server"/>
        </td>
        <td style="width:230px; height:20px" class="GridAlternateItem">
         <asp:TextBox ID="txtDesEmp" Text ="" runat="server" Width="220px" Font-Size="9px" CssClass="inputDisabled2" BorderStyle="None" BorderWidth="0"></asp:TextBox>
         <%--<asp:Label ID="lblDesEmp" Text ="" runat="server" Width="240px" Font-Size="9px"></asp:Label>--%>
        </td>
        <td style="width:20px; height:20px" class="GridAlternateItem">
         <asp:ImageButton id="btnGrabar" runat="server" ImageUrl="../../intranet/Imagenes/Grabar.gif" CommandName="Grabar" Width="16px" Height="16px"></asp:ImageButton>
        </td>
       </tr>
      </table>
     </asp:Panel>
    </ContentTemplate>

    <Triggers>
     <asp:AsyncPostBackTrigger ControlID ="btnConsultar" EventName="click" />
     <asp:AsyncPostBackTrigger ControlID ="btnNuevo" EventName="click" />
    </Triggers>

   </asp:UpdatePanel>
    
 </form>

</body>
</html>
