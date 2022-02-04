<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="frm_AprobacionesPrecosto.aspx.vb" Inherits="ElnaceNM_PreCostos.frm_Test" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >


<head id="Head1" runat="server" >

<title>Nuevo Mundo</title>

<script language="JavaScript" src="../js/jsGeneral.js" type="text/jscript" ></script>    
<script language="JavaScript" src="../js/jsFunciones.js" type="text/jscript" ></script>    

<link href ="../style/Styles_Paginas.css" type="text/css" rel="Stylesheet"/>
<link href ="../style/Styles_Controles.css" type="text/css" rel="Stylesheet"/>

<script language="javascript" type="text/jscript"> 
</script>

</head>

<body>
<center>
<form id="frmData" runat="server" method="post" >

		<table  cellpadding ="1"  cellspacing ="1" width="500px" border="0">
				<tr>
					<td style="HEIGHT: 22px;width:500px;" >
                        <asp:TextBox ID="txtNumeroReq" runat="server" CssClass="txtHid" Width="0px"></asp:TextBox>
                        <asp:TextBox ID="txtNumeroDet" runat="server" CssClass="txtHid" Width="0px"></asp:TextBox>
                        &nbsp;REQUISICIONES&nbsp; (PRE-COSTOS) - POR APROBAR</td>
				</tr>
			</table>
			
			<table  cellpadding ="1"  cellspacing ="1" width="600px" border="0">
                  <tr class="RowHead">
                  <td style="width:100px" class="ColHead">Grupo</td>
                  <td style="width:100px" class="ColHead">Usuario</td>
                  <td style="width:250px" class="ColHead">Nombre del Usuario</td>
                  <td style="width:70px" class="ColHead">Estado</td>
                  <td style="width:80px" class="ColHead">Aprobar</td>
                  </tr>
            </table>
            
            
			<asp:datagrid id="grdRequisiciones" runat="server" Width="600px" AutoGenerateColumns="False" ShowHeader="False" >
			<AlternatingItemStyle CssClass="FilaAlt" />
			<Columns>
			
				<asp:BoundColumn DataField="grupo" ReadOnly="True" Visible ="false">
                </asp:BoundColumn>
                
				<asp:BoundColumn DataField="nu_paso" ReadOnly="True" Visible ="false">
                </asp:BoundColumn>

                <asp:BoundColumn DataField="DE_GRUP_APRO" ReadOnly="True" Visible ="true">
                    <ItemStyle Height="23px" HorizontalAlign="Left" Width="100px" />
                </asp:BoundColumn>
                
								
				<asp:BoundColumn DataField="co_usua" ReadOnly="True" Visible ="true">
                    <ItemStyle Height="23px" HorizontalAlign="Left" Width="100px" />
                </asp:BoundColumn>
                
                <asp:BoundColumn DataField="no_usua">
                    <ItemStyle Height="23px" HorizontalAlign="Left" Width="250px" />
                </asp:BoundColumn>

                
                <asp:BoundColumn DataField="st_apro" ReadOnly="True" Visible ="true">
                    <ItemStyle Height="23px" HorizontalAlign="Left" Width="70px" />
                </asp:BoundColumn>
				    
				
				<asp:TemplateColumn>
				    <ItemStyle Width="80px" /> 
					<ItemTemplate>
						<asp:ImageButton ID="btnAprobar" runat="server" Height="17px" Visible ="false" 
                            ImageUrl="~/images/buscar.gif" Width="16px" CommandName="APROBAR" />
					</ItemTemplate>
				</asp:TemplateColumn>
			
		    </Columns>
		</asp:datagrid>
		
</form>    
</center>
</body>
</html>
