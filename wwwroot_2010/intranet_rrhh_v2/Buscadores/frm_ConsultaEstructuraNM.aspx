<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="frm_ConsultaEstructuraNM.aspx.vb" Inherits="intranet_rrhh.frm_ConsultaEstructuraNM" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
 <title>Asignar gerencias</title>
 <base target="_self">
 <link href="../../intranet/Estilos/NM0001.css" type="text/css" rel="stylesheet"/>
 <link href="../Styles/sytle.css" type="text/css" rel="stylesheet"/>
 <script language="javascript" type="text/javascript">
     returnValue = "";
     function btnEscoger_Onclick(pCodigo, pDescripcion, pCodEmp) {
       //alert(pCodigo + pDescripcion + pCodEmp)       
       returnValue = pCodigo + ":" + pDescripcion + ":" + pCodEmp;
       window.close();
     }
 </script>
</head>
<body>
 <form id="frmConsultaEstructura" runat="server">
  <%--titulo--%>
  <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager> 
   <asp:UpdatePanel ID="panContenido" runat="server" UpdateMode="Always">
    <ContentTemplate>
     <div style="width:500px">
      <table style="width:450px" cellspacing="0" cellpadding="0" border="0">
	   <tr>
	    <td class="titulo" style="width:450px; height:20px" align="center" colspan="3">
         <asp:Label ID="lblTtulo" runat="server" Text=""></asp:Label> 
        </td>
	   </tr>
       <tr>
        <td style="width:100px" align="left" class="Etiqueta">Codigo:</td>
        <td style="width:250px" align="left">
         <asp:TextBox ID="txtCodigo" runat="server" Text="" Width="80px" CssClass="input"></asp:TextBox>
        </td>
        <td style="width:100px" align="right">
         <asp:Button ID="btnConsultar" runat="server" Text="Consultar" CssClass="btnAzul" />
        </td>
	   </tr>
       <tr>
        <td style="width:100px" align="left" class="Etiqueta">Descripcion:</td>
        <td style="width:250px" align="left">
         <asp:TextBox ID="txtDescripcion" runat="server" Text="" Width="200px" CssClass="input"></asp:TextBox>
        </td>
        <td style="width:100px" align="right"></td>
       </tr>
       <tr>
        <td style="width:450px" colspan="3">
         <asp:Label ID="lblError" runat="server" Text="" CssClass="mensaje"></asp:Label>
        </td>
       </tr>
       
       <tr>
        <td style="width:450px" colspan="3">
         <asp:datagrid id="dgDatos" runat="server" Width="450px" AutoGenerateColumns="False" AllowSorting="True">
	       <AlternatingItemStyle CssClass="GridAltItem"></AlternatingItemStyle>
	       <ItemStyle CssClass="GridItem"></ItemStyle>
	       <HeaderStyle CssClass="GridHeader"></HeaderStyle>
	       <Columns>
            
            <asp:TemplateColumn>
		     <HeaderStyle Width="20px"></HeaderStyle>
		     <ItemStyle HorizontalAlign="Center"></ItemStyle>
		     <ItemTemplate>
              <input runat="server" name="btnEscoger" id="btnEscoger" type="button" value="..." class="Boton" style="width:20px; height:20px"/>
		     </ItemTemplate>
		    </asp:TemplateColumn>
            
            <asp:TemplateColumn HeaderText="Empleado" Visible="false">
             <HeaderStyle HorizontalAlign="Center" Width="10px"></HeaderStyle>
             <ItemTemplate>
              <asp:Label id="lblCodEmp" Width="10px" text='<%# Container.dataitem("vch_CodigoEmpleado") %>' runat="server" Font-Size="8px" ></asp:Label>
		        </ItemTemplate>
            </asp:TemplateColumn>

            <asp:TemplateColumn HeaderText="Codigo">
             <HeaderStyle HorizontalAlign="Center" Width="50px"></HeaderStyle>
             <ItemTemplate>
              <asp:Label id="lblCodigo" Width="50px" text='<%# Container.dataitem("vch_Codigo") %>' runat="server" Font-Size="8px" ></asp:Label>
		        </ItemTemplate>
            </asp:TemplateColumn>
       
            <asp:TemplateColumn HeaderText="Descripcion">
             <HeaderStyle HorizontalAlign="Center" Width="370px"></HeaderStyle>
             <ItemTemplate>
              <asp:Label id="lblDescripcion" Width="370px" text='<%# Container.dataitem("vch_Descripcion") %>' runat="server" Font-Size="8px" ></asp:Label>
		     </ItemTemplate>
            </asp:TemplateColumn>
           </Columns>
          </asp:datagrid>
         </tr>
        </table>
       </div>
      </ContentTemplate>
      
      <Triggers>
       <asp:AsyncPostBackTrigger ControlID ="btnConsultar" EventName="click" />
      </Triggers>
     </asp:UpdatePanel>
    </form>
   </body>
</html>