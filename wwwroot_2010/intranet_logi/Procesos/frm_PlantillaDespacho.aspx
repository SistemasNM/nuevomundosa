<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="frm_PlantillaDespacho.aspx.vb" Inherits="intranet_logi.frm_PlantillaDespacho" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html>
	<head>
		<title>Plantilla de Despachos</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR"/>
		<meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE"/>
		<meta content="JavaScript" name="vs_defaultClientScript"/>
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema"/>
        <link href="../css/NM0001.css" rel="stylesheet" type="text/css" />
		<script src="../js/functions.js" type="text/javascript"></script>
        <script src="../js/jquery.js" type="text/javascript"></script>
        <script src="../js/jquery.min.js" type="text/javascript"></script>
        <script src="../js/jquery.validate.js" type="text/javascript"></script>
	
		<script language="javascript" type="text/javascript">

		    function fnInputNumberInteger() {
		        if ((event.keyCode >= 48 && event.keyCode <= 57))
		            return true;
		        else
		            return false;
		    }

		    function ValidarImporte(id, saldo) {
		        var importe = GetFloatValue(id);
		        saldo = parseFloat(saldo);
		        var formato = "0,0.00";
		        if (importe > saldo) {
		            SetValue(id, saldo);
		            alert('Se ha superado el monto máximo: ' + GetValue('hdnMonedaSimbolo') + ' ' + saldo.numberFormat(formato));
		        }
		        CalcularImporteTotal();
		    }

		    function txtLote_Onblur(prCodItem, prCantSol, prIDObj) {
		        var vrIdFila = "";
		        var vrLoteValid = "";
		        var vrCodLote = "";

		        vrCodLote = document.getElementById(prIDObj).value;
		        vrIdFila = prIDObj.replace("txtLote", "");
		        vrLoteValid = prIDObj.replace("txtLote", "txtLoteValid"); //+vrIdFila //+ "txtLoteValid";

		        if (document.getElementById(prIDObj).value == "") {
		            document.getElementById(vrLoteValid).value = "";
		            return;
		        }

		        // ini : ajax valid
		        $.ajax({
		            type: "GET",
		            url: "frm_AjaxValidRegistrarDespachoMuestras.aspx",
		            data: "codart=" + prCodItem + "&codlote=" + vrCodLote + "&cantsol=" + prCantSol,
		            async: false,
		            beforeSend: function (objeto) {
		                document.getElementById(vrLoteValid).value = "Validando...";
		            },
		            error: function (objeto, quepaso, otroobj) {
		                document.getElementById(vrLoteValid).value = "Errores al Procesar.";
		            },
		            global: true,
		            ifModified: false,
		            processData: true,
		            success: function (datos) {
		                document.getElementById(vrLoteValid).value = datos;
		            },
		            timeout: 300099999
		        });
		        // fin : ajax valid			
		        //			
		        //			
		    }

		    function funValidarGrabar() {
		        var vrBool = true
		        var vrTipoSol = ""

		        vrTipoSol = document.getElementById("ddlTipoSol").value;

		        if (FunValidErrResProc() == true) {
		            if (vrTipoSol == "1")
		                alert('Ingrese correctamente la información para poder Grabar.');
		            else
		                alert('No se puede grabar por falta de Stock.');

		            vrBool = false;
		        }

		        return vrBool;

		        //return false; // no ejecuta Grabar
		        //return true; // Si ejecuta Grabar
		    }
		    // Funcion Busca Articulos
		    function fnc_BuscarArticulos() {
		        var x = "001";
		        if (x != "") {
		            var lstrAlmacen = x.substring(0, 3);
		            var lpstrTipo = 9;
		            var retorno = window.showModalDialog("../../intranet_logi/Buscadores/frmBusquedaArticulosStock.aspx?pstrAlmacen=" + lstrAlmacen + "&pstrTipo=" + lpstrTipo, "", "dialogheight:500px;dialogwidth:800px;center:yes;help:no;");
		            //window.showModalDialog("frmFichaProveedor.aspx?strNumeroOrdenServicio=" + strCodigo, "", "dialogHeight:600px;dialogWidth:900px;center:yes;help:no;");
		            if (retorno != "" && retorno != ":") {
		                var datos = retorno.split(":");
		                var Codigo = datos[0];
		                var Nombre = datos[1];
		                var Unidad = datos[2];
		                var Stock = datos[3];
		                document.all('txtCodArticulo').value = Codigo;
		                document.all('lblDesArticulo').value = "Desc: " + Nombre;
		                document.all('lblUniMedida').value = "U.M.: " + Unidad;
		                document.all('lblStockArticulo').value = "Stock: " + parseFloat(Stock).toFixed(3);
		            }
		        }
		        else {

		            alert("Debe elegir un almacen para oonsultar Articulos");
		            document.all('txtCodAlmacen').focus();
		        }
		    }

		    function FunValidErrResProc() {
		        var frm = document.forms[0];
		        var nroErrados = 0;

		        for (i = 0; i < frm.length; i++) {
		            e = frm.elements[i];

		            if (e.type == 'text' && e.id.indexOf("txtLoteValid") != -1) {
		                var vrResProc = document.getElementById(e.id).value;

		                if (vrResProc != 'Todo OK.')
		                    nroErrados = nroErrados + 1;
		            }
		        }

		        if (nroErrados > 0)
		            return true;
		        else
		            return false;

		    }
		


		</script>
	    <style type="text/css">
            .style1
            {
                width: 100%;
            }
            .style2
            {
                height: 16px;
            }
            .style5
            {
            }
            .style6
            {
                height: 16px;
                width: 125px;
            }
            .style7
            {
                font-weight: bold;
                font-size: 9px;
                color: #333333;
                background-color: #BCCAE0;
                font-family: Verdana;
                text-decoration: none;
                width: 125px;
            }
            .style8
            {
                width: 299px;
            }
            .style9
            {
                height: 16px;
                width: 299px;
            }
            .style10
            {
                text-align: justify;
            }
            .style11
            {
                text-align: justify;
                width: 125px;
            }
            .txtHabilitado
            {}
        </style>
	</head>
	<body>
		<form id="Form1" method="post" runat="server">
			<table class="style1">
                <tr>
                    <td>
                        &nbsp;</td>
                    <td>
                        &nbsp;</td>
                    <td>
                        &nbsp;</td>
                    <td class="cabecera" colspan="3">
                        PLANTILLA DE DESPACHOS</td>
                    <td>
                        &nbsp;</td>
                    <td>
                        &nbsp;</td>
                    <td>
                        &nbsp;</td>
                    <td>
                        &nbsp;</td>
                </tr>
                <tr>
                    <td>
                        &nbsp;</td>
                    <td>
                        &nbsp;</td>
                    <td>
                        &nbsp;</td>
                    <td class="style8">
                        &nbsp;</td>
                    <td class="style11">
                        &nbsp;</td>
                    <td>
                        &nbsp;</td>
                    <td>
                        &nbsp;</td>
                    <td>
                        &nbsp;</td>
                    <td>
                        &nbsp;</td>
                    <td>
                        &nbsp;</td>
                </tr>
                <tr>
                    <td>
                        &nbsp;</td>
                    <td>
                        &nbsp;</td>
                    <td>
                        &nbsp;</td>
                    <td class="style8">
                        &nbsp;</td>
                    <td class="style5" colspan="2">
                        <asp:DropDownList ID="DdlPlantilla0" runat="server" Height="16px" Width="341px">
                            <asp:ListItem Value="1">Entrega EPPS</asp:ListItem>
                            <asp:ListItem Value="2">Utiles de Aseo</asp:ListItem>
                            <asp:ListItem Value="3">Canasta Navideña</asp:ListItem>
                            <asp:ListItem Value="4">Uniformes</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <td>
                        &nbsp;</td>
                    <td>
                        &nbsp;</td>
                    <td>
                        &nbsp;</td>
                    <td>
                        &nbsp;</td>
                </tr>
                <tr>
                    <td class="style2">
                    </td>
                    <td class="style2">
                    </td>
                    <td class="style2">
                    </td>
                    <td class="style9">
                    </td>
                    <td class="style6">
                    </td>
                    <td class="style2">
                    </td>
                    <td class="style2">
                    </td>
                    <td class="style2">
                    </td>
                    <td class="style2">
                    </td>
                    <td class="style2">
                    </td>
                </tr>
                <tr>
                    <td>
                        &nbsp;</td>
                    <td>
                        &nbsp;</td>
                    <td>
                        &nbsp;</td>
                    <td class="style8">
                        &nbsp;</td>
                    <td class="style7">
                        Código de Articulo</td>
                    <td>
		         <asp:textbox id="txtCodArticulo" runat="server" Font-Size="9px" width="92px" 
                            MaxLength="20" CssClass="txtHabilitado" Height="16px"></asp:textbox>
                        <input id="Button6" style="width: 20px" onclick="javascript:fnc_BuscarArticulos();" type="button" value="..." name="btnArticulos" class="Boton"/></td>
                    <td>
                        &nbsp;</td>
                    <td>
                        &nbsp;</td>
                    <td>
                        &nbsp;</td>
                    <td>
                        &nbsp;</td>
                </tr>
                <tr>
                    <td class="style2">
                    </td>
                    <td class="style2">
                    </td>
                    <td class="style2">
                    </td>
                    <td class="style9">
                    </td>
                    <td class="style7">
                        Descripcion</td>
                    <td class="style2">
                        <asp:TextBox ID="TxtDescr" runat="server" CssClass="input" Height="16px" 
                            Width="424px"></asp:TextBox>
                    </td>
                    <td class="style2">
                    </td>
                    <td class="style2">
                    </td>
                    <td class="style2">
                    </td>
                    <td class="style2">
                    </td>
                </tr>
                <tr>
                    <td>
                        &nbsp;</td>
                    <td>
                        &nbsp;</td>
                    <td>
                        &nbsp;</td>
                    <td class="style8">
                        &nbsp;</td>
                    <td class="style7">
                        Cuenta Contable</td>
                    <td>
                        <asp:TextBox ID="TextBox3" runat="server" CssClass="input" Height="16px" 
                            Width="71px"></asp:TextBox>
                        <asp:Button ID="btnCuenta" runat="server" CssClass="boton" Text="..." />
                    </td>
                    <td>
                        &nbsp;</td>
                    <td>
                        &nbsp;</td>
                    <td>
                        &nbsp;</td>
                    <td>
                        &nbsp;</td>
                </tr>
                <tr>
                    <td>
                        &nbsp;</td>
                    <td>
                        &nbsp;</td>
                    <td>
                        &nbsp;</td>
                    <td class="style8">
                        &nbsp;</td>
                    <td class="style7">
                        Descripcion de la Cuenta:</td>
                    <td>
                        <asp:TextBox ID="TextBox5" runat="server" CssClass="input" Height="16px" 
                            Width="425px"></asp:TextBox>
                    </td>
                    <td>
                        &nbsp;</td>
                    <td>
                        &nbsp;</td>
                    <td>
                        &nbsp;</td>
                    <td>
                        &nbsp;</td>
                </tr>
                <tr>
                    <td>
                        &nbsp;</td>
                    <td>
                        &nbsp;</td>
                    <td>
                        &nbsp;</td>
                    <td class="style8">
                        &nbsp;</td>
                    <td class="style7">
                        Cantidad</td>
                    <td>
                        <asp:TextBox ID="TextBox4" runat="server" CssClass="input" Height="18px" 
                            Width="56px"></asp:TextBox>
                    </td>
                    <td>
                        &nbsp;</td>
                    <td>
                        &nbsp;</td>
                    <td>
                        &nbsp;</td>
                    <td>
                        &nbsp;</td>
                </tr>
                <tr>
                    <td>
                        &nbsp;</td>
                    <td>
                        &nbsp;</td>
                    <td>
                        &nbsp;</td>
                    <td class="style8">
                        &nbsp;</td>
                    <td class="style11">
                        &nbsp;</td>
                    <td>
                        &nbsp;</td>
                    <td>
                        &nbsp;</td>
                    <td>
                        &nbsp;</td>
                    <td>
                        &nbsp;</td>
                    <td>
                        &nbsp;</td>
                </tr>
                <tr>
                    <td>
                        &nbsp;</td>
                    <td>
                        &nbsp;</td>
                    <td>
                        &nbsp;</td>
                    <td class="style8">
                        &nbsp;</td>
                    <td class="style10" colspan="2">
                        <asp:GridView ID="GridView1" runat="server">
                        </asp:GridView>
                    </td>
                    <td>
                        &nbsp;</td>
                    <td>
                        &nbsp;</td>
                    <td>
                        &nbsp;</td>
                    <td>
                        &nbsp;</td>
                </tr>
                <tr>
                    <td>
                        &nbsp;</td>
                    <td>
                        &nbsp;</td>
                    <td>
                        &nbsp;</td>
                    <td class="style8">
                        &nbsp;</td>
                    <td class="style11">
                        &nbsp;</td>
                    <td>
                        &nbsp;</td>
                    <td>
                        &nbsp;</td>
                    <td>
                        &nbsp;</td>
                    <td>
                        &nbsp;</td>
                    <td>
                        &nbsp;</td>
                </tr>
                <tr>
                    <td>
                        &nbsp;</td>
                    <td>
                        &nbsp;</td>
                    <td>
                        <asp:Button ID="Button1" runat="server" Text="Button" />
                    </td>
                    <td class="style8">
                        &nbsp;
                        <asp:Button ID="Button2" runat="server" Text="Button" />
&nbsp;
                        <asp:Button ID="Button3" runat="server" Text="Button" />
&nbsp;
                        <asp:Button ID="Button4" runat="server" Text="Button" />
&nbsp;
                        <asp:Button ID="Button5" runat="server" Text="Button" />
                    </td>
                    <td class="style11">
                        &nbsp;</td>
                    <td>
                        &nbsp;</td>
                    <td>
                        &nbsp;</td>
                    <td>
                        &nbsp;</td>
                    <td>
                        &nbsp;</td>
                    <td>
                        &nbsp;</td>
                </tr>
                 <tr>
            <td class="Descripcion" style="width: 300px">
             &nbsp;
            </td>
            </tr>
            </table>
		</form>
	</body>
</html>
