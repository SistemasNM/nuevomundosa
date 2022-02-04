<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="frmDesTabla.aspx.vb" Inherits="ElnaceNM_PreCostos.frmDesTabla" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >

<head runat="server">
    <title>Buscar Datos</title>
    <base target="_self" />
    
    <link href ="Style/Styles_Paginas.css" type="text/css" rel="Stylesheet"/>
    <link href ="Style/Styles_Controles.css" type="text/css" rel="Stylesheet"/>
    <script language="JavaScript" src="Js/jsDesTabla.js" type="text/jscript" ></script>    
    <script language="JavaScript" src="Js/jsGeneral.js" type="text/jscript" ></script>    
    
    <script language ="jscript" type="text/jscript">
    
    var aValor = new Array(1)
    
    aValor[0] = ""
    aValor[1] = ""


    function fdes_Aceptar(){
    
       if ( frmData.txtRegSel.value == "" ) {
           alert("Seleccionar un registro de la lista");
           return false;
        }
        
        var iFila  =  frmData.txtRegSel.value;
        var table  = document.getElementById("grdData");
        var rowAct = table.rows[iFila];
      
   
       aValor[0] = rowAct.cells[0].innerHTML;
       aValor[1] = rowAct.cells[1].innerHTML;
       
       window.returnValue = aValor
	   window.close();
     
    }

   //=========================================================//
 
    function fdes_Salir(){
       
       window.returnValue = aValor;
	   window.close();
    }
    
    //=========================================================//
    
    </script>



</head>
<body>
    <form id="frmData" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager> 

    <asp:UpdatePanel ID="panContenido" runat="server" >
    <ContentTemplate>

    
    <center>
    <table border="0" cellpadding="0" cellspacing ="0" ><tr style="height:5px" ><td></td></tr></table>
    
    <!-- ***********************************************  Inicio de Cabecera ************************************************ -->

    <table class="CabMain" border="0" cellpadding="0" cellspacing="0" style="width:550px;height:20px">
        <tr>
            <td style="width:100%" align="right" valign="bottom" >
                <asp:Label ID="lblTitulo" runat="server" Text="Label"></asp:Label>
                &nbsp; </td>
        </tr>
    </table>
    
    <!-- ***********************************************  Final de Cabecera ************************************************ -->

    
    <table border="0" cellpadding ="0" cellspacing = "0" class="FrameSimple" style="width:550px;height:60px"  >
      <tr >
         <td style="width:120px" align="left" >&nbsp;<asp:Label ID="lblTitCodigo1" 
                 runat="server"></asp:Label></td>
         <td align="left"><asp:TextBox ID="txtCodigo" runat="server" CssClass="txt" 
                 Width="100px" ></asp:TextBox></td>
         <td rowspan="2">
             <asp:ImageButton ID="btnBuscar" ImageUrl="Images/Buscar.png"  runat="server" />
                </td>
     </tr> 
     <tr >
         <td style="width:120px" align="left">&nbsp;<asp:Label ID="lblTit2Descri" 
                 runat="server"></asp:Label></td>
         <td align="left"><asp:TextBox ID="txtDescri" runat="server" CssClass="txt" 
                 Width="350px" ></asp:TextBox></td>
     </tr>    

    </table>

     <table id="tblReg" border="0" cellpadding="0" cellspacing="0" style="width:550px;height:25px" >
        <tr>
        <td style="width:100%" align="right" >
            <asp:TextBox ID="txtOpc" runat="server" CssClass="txtHid" Width="30px"></asp:TextBox>
            <asp:TextBox ID="txtRegSel" runat="server" CssClass="txtHid" Width="30px"></asp:TextBox>
            &nbsp;
            Nro. Registros
            <asp:TextBox ID="txtReg" runat="server" CssClass="txtReg" Width="30px">0</asp:TextBox>
        </td>
        </tr>
    </table>

    <table border="0" cellpadding ="0" cellspacing = "0" class="GridHeader" style="width:550px;height:20px"  >
      <tr><td style="width:100px">
          <asp:Label ID="lblTitCodigo2" runat="server"></asp:Label>
          </td>
          <td style="width:425px">
              <asp:Label ID="lblTitDescri2" runat="server"></asp:Label>
          </td>
          <td style="width:025px"></td>
      </tr>
    </table> 
    
    
                    
    <div id="divGrd"  style="overflow: auto; width:548px; height: 300px; text-align:left" class="Grilla">
         <asp:DataGrid ID="grdData" runat="server" AutoGenerateColumns="False" ShowHeader="False" Width="525px">
         <AlternatingItemStyle CssClass="FilaAlt" /> 
        
         <Columns>
                                
                <asp:BoundColumn DataField="Codigo" ReadOnly="True">
                <ItemStyle HorizontalAlign="Left" Height="23px" Width="100px" />
                </asp:BoundColumn>                                

                <asp:BoundColumn DataField="Descri" ReadOnly="True">
                <ItemStyle HorizontalAlign="Left" Height="23px" Width="425px" />
                </asp:BoundColumn>
         </Columns>
        
        </asp:DataGrid>
    
    </div>

    <table border="0" cellpadding="0" cellspacing="0"  width="400" >
        <tr>
            <td align="center" >
                <img src="Images/Grabar.png"    border="0" onclick="fdes_Aceptar()" 
                    alt="Aceptar" style="height: 34px; width: 33px"/>
                &nbsp; &nbsp; &nbsp;&nbsp;
                <img src="Images/Cancelar.png" border="0" onclick="fdes_Salir()" alt="Cancelar" 
                    style="height: 32px; width: 34px" /></td>
        </tr>
    </table>

    
    <asp:TextBox ID="txtSQL" runat="server" CssClass="txtHid" Width="1px" ></asp:TextBox>
    
        <asp:TextBox ID="txtFiltro" runat="server" CssClass="txtHid" Width="1px"></asp:TextBox>
    
    </ContentTemplate>
        <Triggers>
        </Triggers>
    </asp:UpdatePanel>


    </center>
    
    </form>
</body>
</html>
