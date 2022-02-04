<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="frm_AnulaciondeDocumentos_2.aspx.vb" Inherits="intranet_logi.frm_AnulaciondeDocumentos_2" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html>
 <head>
 <title>Registro de Inventario</title>
        
		<meta content="Microsoft Visual Studio.NET 7.0" name="GENERATOR" />
		<meta content="Visual Basic 7.0" name="CODE_LANGUAGE" />
		<meta content="JavaScript" name="vs_defaultClientScript" />
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		
     <link href="../css/NM0001.css" rel="stylesheet" type="text/css" />
		<script type="text/javascript" language="javascript" src="../js/jsCalendario_N3.js"></script>
		<script type="text/javascript" language="javascript" src="../js/jsFunciones.js"></script>
		<script type="text/javascript" language="JavaScript">

		    //popUp
		    function popUp(strUrl) {
		        var intWidth = screen.width;
		        var intHeight = screen.height;
		        window.open(strUrl, "", "top=0; left=0; width=" + intWidth + "; height=" + intHeight + "; resizable=1;");
		    }


		    function FormatearNumero(pTexto) {

		        if (pTexto == 1)//numero
		        {
		            var lserie = '0000' + document.all["txtSerie"].value;
		            lserie = lserie.substring(lserie.length, lserie.length - 4);
		            if (lserie == '0000') {
		                document.all["txtSerie"].value = '';
		            } else {
		                document.all["txtSerie"].value = lserie;
		            }
		        }


		        //function FormatearNumero(pTexto) {
		        if (pTexto == 2)//numero
		        {
		            var lnume = '0000000000' + document.all["txtNumero"].value;
		            lnume = lnume.substring(lnume.length, lnume.length - 10);
		            if (lnume == '0000000000') {
		                document.all["txtNumero"].value = '';
		            } else {
		                document.all["txtNumero"].value = lnume;
		            }
		        }
		    }

		    //fnc_ConfirmarAnulacion
		    function fnc_ConfirmarAnulacion() {
		        var lstrMensaje = 'Se anulará la d¿ -- ' + document.all('txtSerie').value + '-' + document.all('txtNumero').value + ' --.\n\n¿Esta seguro de continuar?';
		        if (document.all('txtSerie').value != '' && document.all('txtNumero').value != '') {
		            return confirm(lstrMensaje);
		        }
		        return false;
		    }

      

 </script>

     <style type="text/css">
         .style3
         {
             font-weight: bold;
             font-size: 9px;
             color: #333333;
             background-color: #BCCAE0;
             font-family: Verdana;
             text-decoration: none;
             width: 790px;
         }
         .style4
         {
             font-weight: bold;
             font-size: 9px;
             color: #333333;
             background-color: #BCCAE0;
             font-family: Verdana;
             text-decoration: none;
             width: 382px;
         }
         .style6
         {
             font-weight: bold;
             font-size: 9px;
             color: #333333;
             background-color: #BCCAE0;
             font-family: Verdana;
             text-decoration: none;
             height: 15px;
             width: 209px;
         }
         .style7
         {
             font-weight: bold;
             font-size: 9px;
             color: #333333;
             background-color: #BCCAE0;
             font-family: Verdana;
             text-decoration: none;
             height: 20px;
             width: 209px;
         }
         .style8
         {
             font-weight: bold;
             font-size: 9px;
             color: #333333;
             background-color: #BCCAE0;
             font-family: Verdana;
             text-decoration: none;
             width: 209px;
         }
         .style9
         {
             font-weight: bold;
             font-size: 9px;
             color: #333333;
             background-color: #BCCAE0;
             font-family: Verdana;
             text-decoration: none;
             height: 15px;
             width: 290px;
         }
         .style10
         {
             font-weight: bold;
             font-size: 9px;
             color: #333333;
             background-color: #BCCAE0;
             font-family: Verdana;
             text-decoration: none;
             height: 20px;
             width: 290px;
         }
         .style11
         {
             font-weight: bold;
             font-size: 9px;
             color: #333333;
             background-color: #BCCAE0;
             font-family: Verdana;
             text-decoration: none;
             width: 290px;
         }
     </style>

</head>

<body>
 <center>
  <form id="frmRequisicion" name="frmRequisicion" method="post" runat="Server">
   <input id="hdnCodigo" style="WIDTH: 24px; HEIGHT: 22px" type="hidden" size="1" name="hdnCodigo" runat="server" />
   <input id="hdnAccion" style="WIDTH: 24px; HEIGHT: 22px" type="hidden" size="1" name="hdnAccion" />
   <input id="hdnCCDestino" style="WIDTH: 24px; HEIGHT: 22px" type="hidden" size="1" name="hdnCCDestino" runat="server"/>
   <input id="hdnOrdServicio" style="WIDTH: 24px; HEIGHT: 22px" type="hidden" size="1" name="hdnOrdServicio" runat="server"/>

   <%--cabecera--%>
         <table id="Table1" cellspacing="0" cellpadding="0" border="0" 
       class = "Cabecera" style = "width: 708px; height: 20px">
            <tr>
	            <td class="cabecera" style="HEIGHT: 27px" align="center">ANULACION DE DOCUMENTOS</td>
            </tr>
         </table>
            
            <%-- parte 1--%>
            <table cellspacing="2" cellpadding="0" border="0" style="width: 712px">
                <tr>
                    <td class="style9" valign="middle" align = "left">
                        Tipo de Documento :</td>
                    <td class="style6" valign="middle" align = "left">
                        <asp:DropDownList ID="ddlTipoDoc" runat="server" Height="18px" Width="64px" 
                            CssClass="TablaOpciones">
                            <asp:ListItem Selected="True">OCM</asp:ListItem>
                            <asp:ListItem>REQ</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <td class="Etiqueta" style="WIDTH: 500px; height:15px" valign="middle" align = "left">
                        &nbsp;</td>
                    <td class="Etiqueta" style="WIDTH: 500px; height:15px" valign="middle" align = "left">
                        &nbsp;</td>
                </tr>
                <tr>
                    <td class="style10" valign="middle" align = "left">
                        Serie del Documento :</td>
                    <td class="style7" valign="middle" align = "left">
                        <asp:textbox id="txtSerie" runat="server" Width="36px" CssClass="input" 
                            Height="18px"></asp:textbox>
                        &nbsp;
                    </td>
                    <td class="Etiqueta" style="WIDTH: 500px; height:20px" valign="middle" align = "left">
                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</td>
                    <td class="Etiqueta" style="WIDTH: 500px; height:20px" valign="middle" align = "left">
                        &nbsp;</td>
                </tr>
                <tr>
                    <td class="style11" valign="middle" align = "left">
                        Numero de Documento :</td>
                    <td class="style8" valign="middle" align = "left">
                        <asp:textbox id="txtNumero" runat="server" Width="85px" CssClass="input" 
                            Height="18px"></asp:textbox>
                        &nbsp;
                    </td>
                    <td class="Etiqueta" valign="middle" align = "left">
                        <asp:Button ID="btnConsultar" runat="server" Height="23px" Text="Consultar" 
                            Width="82px" CssClass="Boton" />
                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                        <asp:Button ID="btnGrabar" runat="server" Height="23px" Text="Grabar" 
                            Width="82px" CssClass="Boton" Visible="false"/>
                    </td>
                    <td class="Etiqueta" valign="middle" align = "left">
                        <asp:Button ID="btnAnular" runat="server" Height="23px" Text="Anular" 
                            Width="82px" CssClass="Boton" />
                    </td>
                </tr>
                <tr>
                    <td class="style10" valign="middle" align = "left">
                        Fecha de Documento :</td>
                    <td class="style7" valign="middle" align = "left">
                        <asp:textbox id="txtFecha" runat="server" Width="85px" AutoPostBack="True" 
                            CssClass="inputDisabled2" Height="18px" ReadOnly="True"></asp:textbox>
                    </td>
                    <td class="Etiqueta" style="WIDTH: 500px; height:20px" valign="middle" align = "left">
                        &nbsp;</td>
                    <td class="Etiqueta" style="WIDTH: 500px; height:20px" valign="middle" align = "left">
                        &nbsp;</td>
                </tr>
                <tr>
                    <td class="style10" valign="middle" align = "left">
                        Estado de Documento :</td>
                    <td class="style7" valign="middle" align = "left">
                        <asp:textbox id="txtEstado" runat="server" Width="36px" AutoPostBack="True" 
                            CssClass="inputDisabled2" Height="16px" ReadOnly="True"></asp:textbox>
                    </td>
                    <td class="Etiqueta" style="WIDTH: 500px; height:20px" valign="middle" align = "left">
                        &nbsp;</td>
                    <td class="Etiqueta" style="WIDTH: 500px; height:20px" valign="middle" align = "left">
                        &nbsp;</td>
                </tr>
                <tr>
                    <td class="style10" valign="middle" align = "left">
                        Fecha Fin de trabajo :</td>
                    <td class="style7" valign="middle" align = "left">
                        <asp:textbox id="txtFecExpi" runat="server" Width="84px" 
                            CssClass="inputDisabled2" Height="16px"></asp:textbox>
                        <img onclick="popUpCalendar(this, frmRequisicion.txtFecExpi, 'dd/mm/yyyy')" height="16" width="16" alt="Seleccionar fecha" src="../../intranet/Imagenes/Calendario.gif" border="0" />
                    </td>
                </tr>
            </table>

            <%-- parte 2--%>
            <table cellspacing="2" cellpadding="0" border="0" style="width: 704px">
                <tr>
                    <td class="Etiqueta" style="WIDTH: 150px" valign="middle" align = "left">Aréa / 
                        Proveedor :</td>
                    <td class="style4" valign="middle" align = "left">
                    <asp:textbox id="txtAuxiliar" runat="server" Width="562px" CssClass="inputDisabled2" 
                            MaxLength="10" Height="22px" ReadOnly="True"></asp:textbox>
                    </td>
                </tr>
                <tr>
                    <td class="Etiqueta" style="WIDTH: 150px" valign="middle" align = "left"></td>
                    <td class="style4" valign="middle" align = "left">
                        <asp:Label ID="lblMensaje" runat="server" Text="" CssClass="error"></asp:Label><br />
                    </td>
                </tr>
            </table>
            
            <%-- parte 3--%>
            <table cellspacing="2" cellpadding="0" 
       style="width:697px; height:10px;" border="0">
                <tr valign ="top">
                    <td style="WIDTH: 150px" valign="top" align = "left">
                        <input class="Input" id="hdnAprobacion" style="WIDTH: 10px" type="hidden" size="1" runat="server" name="hdnAprobacion"/>
                        <input class="Input" id="hdnAreaSolicitante" style="WIDTH: 10px" type="hidden" size="1" name="Hidden1" runat="server" />
                    </td>
                    <td style="WIDTH: 150px" valign="top" align = "right">&nbsp;</td>
                    <td style="WIDTH: 150px" valign="top" align = "right">&nbsp;</td>
                    <td style="WIDTH: 150px" valign="top" align = "right"></td>
                    <td style="WIDTH: 150px" valign="top" align = "right">&nbsp;</td>
                    <td style="WIDTH: 150px" valign="top" align = "right">&nbsp;</td>
                </tr>
            </table>

        <%-- parte: Grilla--%>
        <table style="WIDTH: 706px">
            <tr>
                <td class="style3" valign="middle" align = "left">
                    <asp:datagrid id="dgDetalle" runat="server" Width="702px" 
                        AutoGenerateColumns="False" Font-Bold="False" Height="75px">
                        <AlternatingItemStyle CssClass="GridAlternateItem"></AlternatingItemStyle>
                        <ItemStyle CssClass="GridItem" Font-Bold="False" Font-Italic="False" 
                            Font-Overline="False" Font-Strikeout="False" Font-Underline="False" 
                            HorizontalAlign="Left"></ItemStyle>
                        <HeaderStyle CssClass="GridHeader" Font-Bold="False" Font-Italic="False" 
                            Font-Overline="False" Font-Strikeout="False" Font-Underline="False" 
                            HorizontalAlign="Center"></HeaderStyle>
                        <Columns>
                            <%--Secuencia--%>
                            
                            <%--Descripcion--%>
                            <asp:BoundColumn DataField="NU_SECU" HeaderText="Item">
                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" 
                                    Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Right" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="CO_ITEM" HeaderText="Código Item">
                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" 
                                    Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="DE_ITEM" HeaderText="Descripción"></asp:BoundColumn>
                            <asp:BoundColumn DataField="CA_SOLI" DataFormatString="{0:#,##0.00}" 
                                HeaderText="Cantidad">
                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" 
                                    Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Right" />
                            </asp:BoundColumn>
                          
	        </Columns>
	 </asp:datagrid>
    </td>
   </tr>
  </table>
   <%--Secuencia--%>
  
 </form>
</center>
</body>
</html>
