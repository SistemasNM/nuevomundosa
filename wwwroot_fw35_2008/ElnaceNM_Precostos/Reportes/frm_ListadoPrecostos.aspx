<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="frm_ListadoPrecostos.aspx.vb" Inherits="ElnaceNM_PreCostos.frm_ListadoPrecostos" %>

<%@ Register assembly="Infragistics35.Web.v10.1, Version=10.1.20101.1011, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" namespace="Infragistics.Web.UI.EditorControls" tagprefix="ig" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<script language="JavaScript" src="../js/jsGeneral.js" type="text/jscript" ></script>  
        <script language="JavaScript" src="../Js/jsDesTabla.js" type="text/jscript" ></script>     
        <script language="JavaScript" src="../js/tabpane.js" type="text/jscript" ></script>    
        <link href ="../style/tab.webfx.css" type="text/css" rel="Stylesheet"/>
        <link href ="../style/Styles_Paginas.css" type="text/css" rel="Stylesheet"/>
        <link href ="../style/Styles_Controles.css" type="text/css" rel="Stylesheet"/>
<head runat="server">
    <title>Untitled Page</title>
<script language="javascript" type="text/jscript">

       
       //========================================//

      function fMostrarReporte(strURL) {

           var intWidth  = screen.width;
           var intHeight = screen.height;

           window.open(strURL, "", "top=0; left=0; width=" + intWidth + "; height=" + intHeight + "; resizable=1;");
       }
       
       //===============================================================//
       
</script>
</head>
<body>
    <form id="Form1" method="post" runat="server">
    
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager> 
    
			<TABLE id="Table1" style="HEIGHT: 19px" cellSpacing="0" cellPadding="0" width="100%" border="0">
				<TR>
					<TD>
						<TABLE class="cabecera" id="Table12" cellSpacing="0" cellPadding="0" width="100%" border="1">
							<TR>
								<TD class="cabecera" align="left" colSpan="5" height="25"><IMG height="10" alt="" src="/webproduccion/images/spacer.gif" width="20">LISTADO 
                                    DE PRE-COSTOS</TD>
							</TR>
						</TABLE>
					</TD>
				</TR>
			</TABLE>
			<table>
				<tr>
					<td class="titulo" style="WIDTH: 112px">&nbsp;Estados&nbsp;:&nbsp;</td>
					<td>
                        <asp:RadioButton ID="optPendiente" runat="server" Checked="True" 
                            Text="Pendientes" GroupName="Estado" />
                    </td>
					<td>&nbsp;</td>
					<td>
                        <asp:RadioButton ID="optGenerado" runat="server" Text="Generado" 
                            GroupName="Estado" />
                    </td>
				    <td>
                        &nbsp;</td>
				</tr>
				<tr>
					<td class="titulo" style="WIDTH: 112px">&nbsp;Fecha Inicial:&nbsp;</td>
					<td colspan="3">
                        <ig:WebDatePicker ID="TxtFechaIni" runat="server" Width="110px">
                        </ig:WebDatePicker>
                    </td>
				</tr>
				<tr>
					<td class="titulo" style="WIDTH: 112px">&nbsp;Feha Final&nbsp;&nbsp; :&nbsp;</td>
					<td>
                        <ig:WebDatePicker ID="TxtFechaFin" runat="server" Width="110px">
                        </ig:WebDatePicker>
                    </td>
					<td>
                        &nbsp;</td>
					<td>&nbsp;<asp:Button ID="Button1" runat="server" Text=" Reporte " />
					</td>
				</tr>
			</table>
			
				<!--//////////////     Resumen    //////////////  -->
			
			</div>
		</form>
</body>
</html>
