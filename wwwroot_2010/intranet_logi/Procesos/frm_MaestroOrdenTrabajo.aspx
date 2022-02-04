<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="frm_MaestroOrdenTrabajo.aspx.vb" Inherits="intranet_logi.frm_MaestroOrdenTrabajo" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">


<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>MESTRO DE ORDES DE TRABAJO</title>
    <meta http-equiv="X-UA-Compatible" content="IE=9"/> 
  <link href="../css/Styles_Paginas.css" rel="stylesheet" type="text/css" />
  <link href="../css/NM0001.css" rel="stylesheet" type="text/css" />
  <link href="../css/Styles_Controles.css" rel="stylesheet" type="text/css" />
  <link href="../css/EstilosWeb.css" rel="stylesheet" type="text/css" />
  <link href="../css/sytle.css" type="text/css" rel="stylesheet"/>
     <script src="../js/jquery-1.12.1.js.js" type="text/javascript"></script>
     <script src="../js/jquery-ui.js" type="text/javascript"></script>
     <link href="../css/jquery-ui.css" rel="stylesheet" type="text/css" />
     <meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema" />
     <%-- <link href="../css/NM0001.css" rel="stylesheet" type="text/css" />
 <link href="../css/Styles_Controles.css" rel="stylesheet" type="text/css" />
 <script src="../js/jsCalendario_N3.js" type="text/javascript"></script>--%>

    <script type="text/javascript"  language="javascript">

        function fValidaDescOT(OT, ClientId) {
            if (OT.value.length != 12) {
                alert("EL #OT DEBE TENER 12 CARACTERES");
                document.getElementById(ClientId + 'txtOTFoot').value = "";  
                return;
            }
            var response = frm_MaestroOrdenTrabajo.ValidarOT(OT.value);
            document.getElementById(ClientId + 'txtDescOTFoot').value = response.value;
            if (response.value == "") {
                document.getElementById(ClientId + 'txtDescOTFoot').readOnly = false;
                document.getElementById(ClientId + 'txtDescOTFoot').focus(); 
            } else {
                document.getElementById(ClientId + 'txtDescOTFoot').readOnly = true;
                document.getElementById(ClientId + 'txtCeCoFoot').focus(); 
            }
           

            var retorno = OT.value;
            var datos = retorno.split("-");
            var cod_res = datos[1];
            document.getElementById(ClientId + 'ddlResponsableFooter').value = cod_res;
            document.getElementById(ClientId + 'ddlResponsableFooter').readOnly = true;
            
        }


        function fValidaRespo(OT,ClientId) {
            var retorno = OT.value;
            var datos = retorno.split("-");
            var cod_res = datos[1];
            //document.getElementById("ddlResponsableFooter").value = codigo;

//            switch (cod_res) {
//                case "01":
//                    document.getElementById(ClientId + 'lblCodRespFoot').value = "01 - GERENCIA DE PRODUCCIÓN";
//                    break;
//                case "02":
//                    document.getElementById(ClientId + 'lblCodRespFoot').value = "02 - CALIDAD";
//                    break;
//                case "03":
//                    document.getElementById(ClientId + 'lblCodRespFoot').value = "03 - HILANDERÍA";
//                    break;
//                case "04":
//                    document.getElementById(ClientId + 'lblCodRespFoot').value = "04 - PRETEJIDO";
//                    break;
//                case "05":
//                    document.getElementById(ClientId + 'lblCodRespFoot').value = "05 - TEJEDURÍA";
//                    break;
//                case "07":
//                    document.getElementById(ClientId + 'lblCodRespFoot').value = "07 - TINTORERÍA";
//                    break;
//                case "08":
//                    document.getElementById(ClientId + 'lblCodRespFoot').value = "08 - RRHH Y SSOMA";
//                    break;
//                case "10":
//                    document.getElementById(ClientId + 'lblCodRespFoot').value = "10 - GERENCIA GENERAL";
//                    break;
//                case "11":
//                    document.getElementById(ClientId + 'lblCodRespFoot').value = "11 - GERENCIA INGENIERÍA";
//                    break;
//                case "12":
//                    document.getElementById(ClientId + 'lblCodRespFoot').value = "12 - GERENCIA FINANZAS";
//                    break;
//                case "14":
//                    document.getElementById(ClientId + 'lblCodRespFoot').value = "14 - LOGÍSTICA";
//                    break;
//                case "15":
//                    document.getElementById(ClientId + 'lblCodRespFoot').value = "15 - SISTEMAS";
//                    break;
//                case "17":
//                    document.getElementById(ClientId + 'lblCodRespFoot').value = "17 - COMERCIALIZACIÓN";
//                    break;
//                case "18":
//                    document.getElementById(ClientId + 'lblCodRespFoot').value = "18 - REVISIÓN FINAL";
//                    break;
//                case "20":
//                    document.getElementById(ClientId + 'lblCodRespFoot').value = "20 - MANTENIMIENTO MECÁNICO";
//                    break;
//                case "21":
//                    document.getElementById(ClientId + 'lblCodRespFoot').value = "21 - MANTENIMIENTO ELÉCTRICO";
//                    break;
//                case "22":
//                    document.getElementById(ClientId + 'lblCodRespFoot').value ="22 - SERVICIOS INDUSTRIALES E INFRAESTRUCTURA";
//                    break;
//                case "23":
//                    document.getElementById(ClientId + 'lblCodRespFoot').value = "23 - PTAR";
//                    break;
//                default:
//                    document.getElementById(ClientId + 'lblCodRespFoot').value = "";
//            }
            
            document.getElementById(ClientId + 'ddlResponsableFooter').value = cod_res;

        }
        
        function BuscarOT() {

            var retorno = window.showModalDialog("../Buscadores/frmBusquedaOT.aspx?", "", "dialogHeight:450px;dialogWidth:450px;center:yes;help:no;");
            if (retorno != "" && retorno != ":") {
                var datos = retorno.split(":");
                var codigo = datos[0];
                var nombre = datos[1];

                document.getElementById("txtCodOT").value = codigo;
                document.getElementById("txtDesOT").value = nombre;
            }

        }

        function fnc_BuscarDatos(id,strTipoBusqueda,dato, strParam,TipoDato,lnk) {//begin fnc_BuscarDatos()


            //if ((strTipoBusqueda == "CuentaGastos" && strCentroCostos != "") || (strTipoBusqueda != "CuentaGastos" && strCentroCostos == "")) {

           
            
            if (strTipoBusqueda == "CuentaGastos") {
                if (TipoDato == "E") {
                    var strCentroCostos = document.getElementById('txtCeCo').value;
                    if (strCentroCostos == ""){
                        alert("Debe seleccionar primero el Centro de Costos");
                        return;
                    }
                } else {
                    var obj = eval("Form1." + id + "_" + dato);
                    var strCentroCostos = obj.value;
                    if (strCentroCostos == "") {
                        alert("Debe seleccionar primero el Centro de Costos");
                        return;
                    }
                }
                
                var retorno = window.showModalDialog("../Buscadores/frmBusqueda.aspx?TipoBusqueda=" + strTipoBusqueda + "&Param1=" + strCentroCostos + "&Param2=" + strParam, "", "dialogHeight:450px;dialogWidth:450px;center:yes;help:no;");
            } else {

                var retorno = window.showModalDialog("../Buscadores/frmBusqueda.aspx?TipoBusqueda=" + strTipoBusqueda + "&Param1=" + "" + "&Param2=" + strParam, "", "dialogHeight:450px;dialogWidth:450px;center:yes;help:no;");

            }
                
                if (retorno != "" && retorno != ":") {
                    var datos = retorno.split(":");
                    var codigo = datos[0];
                    var nombre = datos[1];

                    switch (strTipoBusqueda) {

                        case "CentrodeCostos":
                            if (TipoDato == "E") {

                                //var row = lnk.parentNode.parentNode;
                                //var rowIndex = row.rowIndex - 1;

                                //document.getElementById(id + '_txtCeCo_' + rowIndex).value = codigo;
                                //document.getElementById(id + '_lblDescCeco_' + rowIndex).value = nombre;

                                document.getElementById('txtCeCo').value = codigo;
                                document.getElementById('lblDescCeco').value = nombre;
                            } else {
       

                                document.getElementById(id + '_txtCeCoFoot').value = codigo;
                                document.getElementById(id + '_lblDescCecoFoot').value = nombre;
                            }

                            break;
                        case "CuentaGastos":
                            if (TipoDato == "E") {

                                //var row = lnk.parentNode.parentNode;
                                //var rowIndex = row.rowIndex - 1;

                                   //document.getElementById(id + '_txtCuGa_' + rowIndex).value = codigo;
                                //document.getElementById(id + '_lblDescCuGa_' + rowIndex).value = nombre;      
                                document.getElementById('txtCuGa').value = codigo;
                                document.getElementById('lblDescCuGa').value = nombre;   
                                                            
                            } else {
                                document.getElementById(id + '_txtCuGaFo').value = codigo;
                                document.getElementById(id + '_lblDescCuGaFo').value = nombre;
                            }
                           
                            break;
                    }

                }
            //} else {
            //    alert("Debe seleccionar primero el Centro de Costos");
            //}
        }
    </script>
</head>
<body>
    <form id="Form1" runat="server">
        <%--<asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager> --%>
         <center>
           <%-- <asp:UpdatePanel ID="panContenido" runat="server">
                <ContentTemplate>--%>
                <table style="width:800px" cellspacing="0" cellpadding="0" border="0">
	                <tr>
	                    <td class="Cabecera" style="width: 800px; height: 30px" align="center">MAESTRO DE ORDENES DE TRABAJO</td>
	                </tr>
	            </table>
                <%--<table>
                    <tr>
                        <td>
                            <asp:RadioButton id="rdbModificar" runat="server" Text="Modificar OT's"  Checked="True" AutoPostBack="True"/>
                     
                        </td>
                        <td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td>
                        <td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td>
                        <td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td>
                         <td>
                           <asp:RadioButton id="rbnAgregar" runat="server" Text="Agregar" AutoPostBack="True"/>
                        </td>
                    </tr>
                </table>--%>
                <table style="width: 800px" align="center">   
                    <tr>
                            <td align="left" class="Etiqueta" height="30px" style="WIDTH: 100px;">&nbsp;Orden de Trabajo:</td>
                            <td align="left" height="30px" style="WIDTH: 200px;">
                            <asp:textbox id="txtCodOT" runat="server" Width="90px" CssClass="inputDisabled" MaxLength="13"></asp:textbox>&nbsp;&nbsp;
                            <input class="Boton" id="btnOT" style="width:20px; height:20px" onclick="BuscarOT()" type="button" value="..."  />
                            </td>
                            <td align="left" height="30px" style="WIDTH: 300px;">
                            <asp:TextBox ID="txtDesOT" runat="server" BorderStyle="None" 
                                        CssClass="Etiqueta" Width="100%"></asp:TextBox>
                            </td>
                            <td align="center" height="30px">
                            <asp:Button id="btnBuscar" runat="server" Text="Buscar"/>
                            </td>
                        </tr>
                    </table>
                    <table style="width: 800px" align="center">
                        <tr>
                             <td align="left" class="Etiqueta" height="30px" style="WIDTH: 100px;">&nbsp;Año:</td>
                             <td>
                                <asp:DropDownList ID="ddlAno" runat="server" Width="70px" AutoPostBack = "true">
                                        <%--<asp:ListItem Value="2020" Text="2020" ></asp:ListItem>
                                        <asp:ListItem Value="2019" Text="2019" ></asp:ListItem>--%>
                                </asp:DropDownList>
                             </td>
                        </tr>
                    </table>
                        <table style="width: 800px" align="center">
                            <tr>
                            <td align="left" class="Etiqueta" height="30px" style="WIDTH: 100px;">&nbsp;Responsable:
                            </td>
                            <td>
								<asp:DropDownList ID="ddlResponsableF" runat="server" Width="280px" AutoPostBack = "true">
                                                <asp:ListItem Value="" Text="---------SELECCIONAR---------"></asp:ListItem>
                                                <asp:ListItem Value="01" Text="01 - GERENCIA DE PRODUCCIÓN"></asp:ListItem>
                                                <asp:ListItem Value="02" Text="02 - CALIDAD"></asp:ListItem>
                                                <asp:ListItem Value="03" Text="03 - HILANDERÍA"></asp:ListItem>
                                                <asp:ListItem Value="04" Text="04 - PRETEJIDO"></asp:ListItem>
                                                <asp:ListItem Value="05" Text="05 - TEJEDURÍA"></asp:ListItem>
                                                <asp:ListItem Value="07" Text="07 - TINTORERÍA"></asp:ListItem>
                                                <asp:ListItem Value="08" Text="08 - RRHH Y SSOMA"></asp:ListItem>
                                                <asp:ListItem Value="10" Text="10 - GERENCIA GENERAL"></asp:ListItem>
                                                <asp:ListItem Value="11" Text="11 - GERENCIA INGENIERÍA"></asp:ListItem>
                                                <asp:ListItem Value="12" Text="12 - GERENCIA FINANZAS"></asp:ListItem>
                                                <asp:ListItem Value="14" Text="14 - LOGÍSTICA"></asp:ListItem>
                                                <asp:ListItem Value="15" Text="15 - SISTEMAS"></asp:ListItem>
                                                <asp:ListItem Value="17" Text="17 - COMERCIALIZACIÓN"></asp:ListItem>
                                                <asp:ListItem Value="18" Text="18 - REVISIÓN FINAL"></asp:ListItem>
                                                <asp:ListItem Value="20" Text="20 - MANTENIMIENTO MECÁNICO"></asp:ListItem>
                                                <asp:ListItem Value="21" Text="21 - MANTENIMIENTO ELÉCTRICO"></asp:ListItem>
                                                <asp:ListItem Value="22" Text="22 - SERVICIOS INDUSTRIALES E INFRAESTRUCTURA"></asp:ListItem>
                                                <asp:ListItem Value="23" Text="23 - PTAR"></asp:ListItem>
                                    </asp:DropDownList>
                            </td>
                        </tr>
                    </table>

                 <table style="width: 1500px" align="center">
                    <tr>
                        <td style="width:600px;text-align:left">
                            <asp:Panel ID="pnlModificar" runat="server">
                                 
                                 
                                 <table style="width: 2300px" align="center">
                                     <tr>
                                        <td>
                                            <table style="width: 2200px">
                                                <asp:DataGrid id="dgOT" runat="server" Width="2200px" AutoGenerateColumns="False" ShowFooter="True">
                                                    <AlternatingItemStyle CssClass="GridAltItem"></AlternatingItemStyle>
		                                            <ItemStyle CssClass="GridItem"></ItemStyle>
		                                            <HeaderStyle CssClass="GridHeader"></HeaderStyle>
                                                    <Columns>
                                                          <asp:TemplateColumn>
                                                            <ItemStyle Width="10px" />
                                                            <ItemTemplate>
                                                                <asp:label ID="txtlId" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.NU_ITEM") %>'></asp:label>
                                                            </ItemTemplate>
                                                            <EditItemTemplate>
                                                             <asp:TextBox id="txtlIdEdi" runat="server" Width="80px" Text='<%# DataBinder.Eval(Container, "DataItem.NU_ITEM") %>'></asp:TextBox>
                                                            </EditItemTemplate>
                                                            <FooterTemplate>
                                                                <asp:TextBox id="txtlIdFot" runat="server" Width="80px" Text='<%# DataBinder.Eval(Container, "DataItem.NU_ITEM") %>'></asp:TextBox>
                                                            </FooterTemplate>
                                                        </asp:TemplateColumn>
                                                        
                                                        <asp:TemplateColumn HeaderText="Cod. Responsable">
		                                                    <HeaderStyle HorizontalAlign="Center" Width="100px" VerticalAlign="Middle"></HeaderStyle>
		                                                    <ItemStyle Font-Size="9px" HorizontalAlign="Center"></ItemStyle>
		                                                    <ItemTemplate>
                                                                <table  id="tbResponsable"  border="0">
                                                                    <tr>
                                                                        <td>
                                                                            <asp:Label id="lblCodResp" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.CO_RESP") %>'></asp:Label>
                                                                             
                                                                        </td>
                                                                        <td>-</td>
                                                                        <td>
                                                                            <asp:Label id="Label1" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.DE_RESP") %>'></asp:Label>
                                                                        </td>
                                                                    </tr>
                                                                </table>
		                                                         
		                                                    </ItemTemplate>
                                                             <EditItemTemplate>
                                                               <table  id="tbResponsableEdit"  border="0">
                                                                    <tr>
                                                                        <td>
                                                                            <asp:Label id="lblCodRespEdit" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.CO_RESP") %>'></asp:Label>
                                                                        </td>
                                                                        <td>-</td>
                                                                        <td>
                                                                            <asp:Label id="lblDesRespEdit" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.DE_RESP") %>'></asp:Label>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </EditItemTemplate>
                                                            <FooterTemplate>
                                                                <%--<asp:TextBox id="lblCodRespFoot" runat="server" Cssclass="Descripcion"></asp:TextBox> AutoPostBack = "true"--%>
                                                                 <asp:DropDownList ID="ddlResponsableFooter" runat="server" Width="280px"  CssClass="Descripcion" >
                                                                        <asp:ListItem Value="01" Text="01 - GERENCIA DE PRODUCCIÓN"></asp:ListItem>
                                                                        <asp:ListItem Value="02" Text="02 - CALIDAD"></asp:ListItem>
                                                                        <asp:ListItem Value="03" Text="03 - HILANDERÍA"></asp:ListItem>
                                                                        <asp:ListItem Value="04" Text="04 - PRETEJIDO"></asp:ListItem>
                                                                        <asp:ListItem Value="05" Text="05 - TEJEDURÍA"></asp:ListItem>
                                                                        <asp:ListItem Value="07" Text="07 - TINTORERÍA"></asp:ListItem>
                                                                        <asp:ListItem Value="08" Text="08 - RRHH Y SSOMA"></asp:ListItem>
                                                                        <asp:ListItem Value="10" Text="10 - GERENCIA GENERAL"></asp:ListItem>
                                                                        <asp:ListItem Value="11" Text="11 - GERENCIA INGENIERÍA"></asp:ListItem>
                                                                        <asp:ListItem Value="12" Text="12 - GERENCIA FINANZAS"></asp:ListItem>
                                                                        <asp:ListItem Value="14" Text="14 - LOGÍSTICA"></asp:ListItem>
                                                                        <asp:ListItem Value="15" Text="15 - SISTEMAS"></asp:ListItem>
                                                                        <asp:ListItem Value="17" Text="17 - COMERCIALIZACIÓN"></asp:ListItem>
                                                                        <asp:ListItem Value="18" Text="18 - REVISIÓN FINAL"></asp:ListItem>
                                                                        <asp:ListItem Value="20" Text="20 - MANTENIMIENTO MECÁNICO"></asp:ListItem>
                                                                        <asp:ListItem Value="21" Text="21 - MANTENIMIENTO ELÉCTRICO"></asp:ListItem>
                                                                        <asp:ListItem Value="22" Text="22 - SERVICIOS INDUSTRIALES E INFRAESTRUCTURA"></asp:ListItem>
                                                                        <asp:ListItem Value="23" Text="23 - PTAR"></asp:ListItem>
                                                            </asp:DropDownList>
                                                            </FooterTemplate>
		                                                  </asp:TemplateColumn>

                                                         <asp:TemplateColumn HeaderText="# OT" HeaderStyle-HorizontalAlign="Center">
		                                                    <HeaderStyle HorizontalAlign="Center" Width="80px" VerticalAlign="Middle"></HeaderStyle>
		                                                    <ItemStyle Font-Size="9px" HorizontalAlign="Center" Width="80px"></ItemStyle>
                                                            <FooterStyle Font-Size="9px" HorizontalAlign="Center" Width="80px"/>
		                                                    <ItemTemplate>
		                                                         <asp:Label id="lblOT" runat="server" Width="80px" Text='<%# DataBinder.Eval(Container, "DataItem.NU_ORTR") %>'></asp:Label>
		                                                    </ItemTemplate>
                                                            <EditItemTemplate>
                                                                 <asp:Label id="lblOTEdit" runat="server" Width="80px" Text='<%# DataBinder.Eval(Container, "DataItem.NU_ORTR") %>'></asp:Label>
                                                            </EditItemTemplate>
                                                            <FooterTemplate>
                                                                <asp:TextBox id="txtOTFoot" runat="server" Width="80px" ></asp:TextBox>
                                                            </FooterTemplate>
		                                                  </asp:TemplateColumn>

                                                          <asp:TemplateColumn HeaderText="Descripcion OT" HeaderStyle-HorizontalAlign="Center">
		                                                    <HeaderStyle HorizontalAlign="Center" Width="200px" VerticalAlign="Middle"></HeaderStyle>
		                                                    <ItemStyle Font-Size="9px" HorizontalAlign="Center" Width="200px"></ItemStyle>
                                                            <FooterStyle Font-Size="9px" HorizontalAlign="Center" Width="200px"/>
		                                                    <ItemTemplate>
		                                                         <asp:Label id="lblDescOT" runat="server" Width="200px" Text='<%# DataBinder.Eval(Container, "DataItem.DE_ORTR") %>'></asp:Label>
		                                                    </ItemTemplate>
                                                            <EditItemTemplate>
                                                                 <asp:TextBox id="lblDescOTEdit" runat="server" Width="200px" Text='<%# DataBinder.Eval(Container, "DataItem.DE_ORTR") %>'></asp:TextBox>
                                                            </EditItemTemplate>
                                                            <FooterTemplate>
                                                                <asp:TextBox id="txtDescOTFoot" runat="server" Width="200px" ></asp:TextBox>
                                                            </FooterTemplate>
		                                                  </asp:TemplateColumn>

                                                          

                                                          <asp:TemplateColumn HeaderText="Centro Costos">
		                                                    <HeaderStyle HorizontalAlign="Center" Width="160px" VerticalAlign="Middle"></HeaderStyle>
		                                                    <ItemStyle Font-Size="9px" HorizontalAlign="Center" Width="140px"></ItemStyle>
                                                            <FooterStyle Font-Size="9px" HorizontalAlign="Center" Width="140px"/>
		                                                    <ItemTemplate>
                                                                 <table  id="tbCentroCosto"  border="0">
                                                                    <tr>
                                                                        <td>
                                                                            <asp:Label id="lblCeCo" runat="server"  Text='<%# DataBinder.Eval(Container, "DataItem.CO_AUXI") %>'></asp:Label>
                                                                        </td>
                                                                        <td>-</td>
                                                                        <td>
                                                                             <asp:Label id="lblDesCeCo" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.NO_AUXI") %>'></asp:Label>
                                                                        </td>
                                                                    </tr>
                                                                </table>
		                                                    </ItemTemplate>
                                                            <EditItemTemplate>
                                                                <table id="tbCentroCostoEdit"  border="0">
                                                                    <tr>
                                                                        <td>
                                                                            <asp:TextBox id="txtCeCo" runat="server"  Width="60px" Text='<%# DataBinder.Eval(Container, "DataItem.CO_AUXI") %>' ClientIDMode="Static"></asp:TextBox>
                                                                        </td>
                                                                        <td>
                                                                            <input id="btnBuscarCeCo" onclick="fnc_BuscarDatos('<%# Container.ClientID %>','CentrodeCostos','txtCeCo','','E',this)"  style="width:20px;height:20px"  type="button" >
                                                                            <%--<asp:Button  id="btnBuscarCeCo" OnClientClick="fnc_BuscarDatos('<%# Container.ClientID %>','CentrodeCostos','txtCeCo','','E',this)" runat="server">
                                                                            </asp:Button>--%>
                                                                        </td>
                                                                         <td>
                                                                              <asp:TextBox id="lblDescCeco" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.NO_AUXI") %>' Cssclass="Descripcion" ClientIDMode="Static"></asp:TextBox>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </EditItemTemplate>
                                                            <FooterTemplate>
                                                                <table id="tbCentroCostoFoot"  border="0">
                                                                    <tr>
                                                                        <td>
                                                                            <asp:TextBox id="txtCeCoFoot" Width="70px" runat="server" ></asp:TextBox>
                                                                        </td>
                                                                        <td>
                                                                            <input id="btnBuscarCecoFoot" onclick="fnc_BuscarDatos('<%# Container.ClientID %>','CentrodeCostos','txtCeCoFoot','','F',this)"  style="width:20px;height:20px"  type="button" >
                                                                        </td>
                                                                        <td>
                                                                             <asp:TextBox id="lblDescCecoFoot" runat="server" Width="140px"  Cssclass="Descripcion"></asp:TextBox>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </FooterTemplate>
		                                                  </asp:TemplateColumn>

                                                          <asp:TemplateColumn HeaderText="Cuenta gastos">
		                                                    <HeaderStyle HorizontalAlign="Center" Width="160px" VerticalAlign="Middle"></HeaderStyle>
		                                                    <ItemStyle Font-Size="9px" HorizontalAlign="Center" Width="130px"></ItemStyle>
                                                            <FooterStyle Font-Size="9px" HorizontalAlign="Center" Width="130px"/>
		                                                    <ItemTemplate>
                                                                 <table  id="tbCuentaGasto"  border="0">
                                                                    <tr>
                                                                        <td>
                                                                            <asp:Label id="lblCuGa" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.CO_CNTA") %>'></asp:Label>
                                                                        </td>
                                                                        <td>-</td>
                                                                         <td>
                                                                              <asp:Label id="lblDesCuGa" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.DE_CNTA_EMPR") %>'></asp:Label>
                                                                        </td>
                                                                    </tr>
                                                                </table>
		                                                    </ItemTemplate>
                                                            <EditItemTemplate>
                                                                <table id="tbCuentaGastoEdit"  border="0">
                                                                    <tr>
                                                                        <td>
                                                                            <asp:TextBox id="txtCuGa" runat="server"  Width="55px" Text='<%# DataBinder.Eval(Container, "DataItem.CO_CNTA") %>' ClientIDMode="Static"></asp:TextBox>
                                                                        </td>
                                                                        <td>
                                                                            <input id="btnBuscarCuGa" onclick="fnc_BuscarDatos('<%# Container.ClientID %>','CuentaGastos','txtCeCo','','E',this)"  style="width:20px;height:20px"  type="button" >
                                                                        </td>
                                                                         <td>
                                                                             <asp:TextBox id="lblDescCuGa" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.DE_CNTA_EMPR") %>' Cssclass="Descripcion" ClientIDMode="Static"></asp:TextBox>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </EditItemTemplate>
                                                            <FooterTemplate>
                                                                <table id="tbCuentaGastoFoot"  border="0">
                                                                    <tr>
                                                                        <td>
                                                                            <asp:TextBox id="txtCuGaFo" runat="server" Width="55px"></asp:TextBox>
                                                                        </td>
                                                                        <td>
                                                                            <input id="btnBuscarCuGaFo" onclick="fnc_BuscarDatos('<%# Container.ClientID %>','CuentaGastos','txtCeCoFoot','','F',this)"  style="width:20px;height:20px"  type="button" >
                                                                        </td>
                                                                        <td>
                                                                             <asp:TextBox id="lblDescCuGaFo" runat="server" Width="140px"  Cssclass="Descripcion"></asp:TextBox>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </FooterTemplate>
		                                                  </asp:TemplateColumn>

                                                          <asp:TemplateColumn HeaderText="Observacion">
		                                                    <HeaderStyle HorizontalAlign="Center" Width="200px" VerticalAlign="Middle"></HeaderStyle>
		                                                    <ItemStyle Font-Size="9px" HorizontalAlign="Center" Width="200px"></ItemStyle>
                                                            <FooterStyle Font-Size="9px" HorizontalAlign="Center" Width="200px"/>
		                                                    <ItemTemplate>
		                                                         <asp:Label id="lblObs" runat="server"  Width="200px" Text='<%# DataBinder.Eval(Container, "DataItem.DE_OBSE") %>'></asp:Label>
		                                                    </ItemTemplate>
                                                            <EditItemTemplate>
                                                                <asp:TextBox id="txtObs" runat="server" Width="200px" Text='<%# DataBinder.Eval(Container, "DataItem.DE_OBSE") %>'></asp:TextBox>
                                                            </EditItemTemplate>
                                                            <FooterTemplate>
                                                                <asp:TextBox id="txtObsFoot" Width="200px" runat="server" ></asp:TextBox>
                                                            </FooterTemplate>
		                                                  </asp:TemplateColumn>

                                                          <asp:TemplateColumn HeaderText="Enero">
                                                           <HeaderStyle HorizontalAlign="Center" Width="50px" VerticalAlign="Middle"></HeaderStyle>
                                                            <ItemStyle Font-Size="9px"  HorizontalAlign="Center" Width="50px"></ItemStyle>
                                                            <FooterStyle Font-Size="9px" HorizontalAlign="Center" Width="50px"/>
                                                            <ItemTemplate>
		                                                         <asp:Label id="lblImp1" runat="server" Width="60px" Text='<%# String.Format("{0:N}", Container.DataItem("IM_PR01"))%>'></asp:Label>
		                                                    </ItemTemplate>
                                                            <EditItemTemplate>
                                                                <asp:TextBox id="txtImp1" runat="server"  Width="60px" Text='<%# DataBinder.Eval(Container, "DataItem.IM_PR01") %>'></asp:TextBox>
                                                            </EditItemTemplate>
                                                            <FooterTemplate>
                                                                 <asp:TextBox id="txtImp1Foot"  Width="60px" runat="server" Text="0.00"></asp:TextBox>
                                                            </FooterTemplate>
                                                           </asp:TemplateColumn>

                                                           <asp:TemplateColumn HeaderText="Febrero">
                                                           <HeaderStyle HorizontalAlign="Center" Width="60px" VerticalAlign="Middle"></HeaderStyle>
                                                            <ItemStyle Font-Size="9px"  HorizontalAlign="Center" Width="50px"></ItemStyle>
                                                            <FooterStyle Font-Size="9px" HorizontalAlign="Center" Width="50px"/>
                                                            <ItemTemplate>
		                                                         <asp:Label id="lblImp2" runat="server" Width="60px" Text='<%# String.Format("{0:N}", Container.DataItem("IM_PR02"))%>' ></asp:Label>
		                                                    </ItemTemplate>
                                                            <EditItemTemplate>
                                                                <asp:TextBox id="txtImp2" runat="server" Width="60px" Text='<%# DataBinder.Eval(Container, "DataItem.IM_PR02") %>'></asp:TextBox>
                                                            </EditItemTemplate>
                                                            <FooterTemplate>
                                                                 <asp:TextBox id="txtImp2Foot" runat="server" Width="60px" Text="0.00"></asp:TextBox>
                                                            </FooterTemplate>
                                                           </asp:TemplateColumn>

                                                           <asp:TemplateColumn HeaderText="Marzo">
                                                           <HeaderStyle HorizontalAlign="Center" Width="50px" VerticalAlign="Middle"></HeaderStyle>
                                                            <ItemStyle Font-Size="9px"  HorizontalAlign="Center" Width="50px"></ItemStyle>
                                                            <FooterStyle Font-Size="9px" HorizontalAlign="Center" Width="50px" />
                                                            <ItemTemplate>
		                                                         <asp:Label id="lblImp3" runat="server" Width="60px" Text='<%# String.Format("{0:N}", Container.DataItem("IM_PR03"))%>' ></asp:Label>
		                                                    </ItemTemplate>
                                                            <EditItemTemplate>
                                                                <asp:TextBox id="txtImp3" runat="server" Width="60px" Text='<%# DataBinder.Eval(Container, "DataItem.IM_PR03") %>'></asp:TextBox>
                                                            </EditItemTemplate>
                                                            <FooterTemplate>
                                                                 <asp:TextBox id="txtImp3Foot" runat="server" Width="60px" Text="0.00"></asp:TextBox>
                                                            </FooterTemplate>
                                                           </asp:TemplateColumn>

                                                           <asp:TemplateColumn HeaderText="Abril">
                                                           <HeaderStyle HorizontalAlign="Center" Width="50px" VerticalAlign="Middle"></HeaderStyle>
                                                            <ItemStyle Font-Size="9px"  HorizontalAlign="Center" Width="50px"></ItemStyle>
                                                            <FooterStyle Font-Size="9px" HorizontalAlign="Center" Width="50px"/>
                                                            <ItemTemplate>
		                                                         <asp:Label id="lblImp4" runat="server" Width="60px" Text='<%# String.Format("{0:N}", Container.DataItem("IM_PR04"))%>'></asp:Label>
		                                                    </ItemTemplate>
                                                            <EditItemTemplate>
                                                                <asp:TextBox id="txtImp4" runat="server" Width="60px" Text='<%# DataBinder.Eval(Container, "DataItem.IM_PR04") %>'></asp:TextBox>
                                                            </EditItemTemplate>
                                                            <FooterTemplate>
                                                                 <asp:TextBox id="txtImp4Foot" runat="server" Width="60px" Text="0.00"></asp:TextBox>
                                                            </FooterTemplate>
                                                           </asp:TemplateColumn>

                                                           <asp:TemplateColumn HeaderText="Mayo">
                                                           <HeaderStyle HorizontalAlign="Center" Width="50px" VerticalAlign="Middle"></HeaderStyle>
                                                            <ItemStyle Font-Size="9px"  HorizontalAlign="Center" Width="50px"></ItemStyle>
                                                            <FooterStyle Font-Size="9px" HorizontalAlign="Center" Width="50px" />
                                                            <ItemTemplate>
		                                                         <asp:Label id="lblImp5" runat="server" Width="60px" Text='<%# String.Format("{0:N}", Container.DataItem("IM_PR05"))%>'></asp:Label>
		                                                    </ItemTemplate>
                                                            <EditItemTemplate>
                                                                <asp:TextBox id="txtImp5" runat="server" Width="60px" Text='<%# DataBinder.Eval(Container, "DataItem.IM_PR05") %>'></asp:TextBox>
                                                            </EditItemTemplate>
                                                            <FooterTemplate>
                                                                 <asp:TextBox id="txtImp5Foot" runat="server" Width="60px" Text="0.00"></asp:TextBox>
                                                            </FooterTemplate>
                                                           </asp:TemplateColumn>

                                                           <asp:TemplateColumn HeaderText="Junio">
                                                           <HeaderStyle HorizontalAlign="Center" Width="50px" VerticalAlign="Middle"></HeaderStyle>
                                                            <ItemStyle Font-Size="9px"  HorizontalAlign="Center" Width="50px"></ItemStyle>
                                                            <FooterStyle Font-Size="9px" HorizontalAlign="Center" Width="50px"/>
                                                            <ItemTemplate>
		                                                         <asp:Label id="lblImp6" runat="server" Width="60px" Text='<%# String.Format("{0:N}", Container.DataItem("IM_PR06"))%>'></asp:Label>
		                                                    </ItemTemplate>
                                                            <EditItemTemplate>
                                                                <asp:TextBox id="txtImp6" runat="server" Width="60px" Text='<%# DataBinder.Eval(Container, "DataItem.IM_PR06") %>'></asp:TextBox>
                                                            </EditItemTemplate>
                                                            <FooterTemplate>
                                                                 <asp:TextBox id="txtImp6Foot" runat="server" Width="60px" Text="0.00"></asp:TextBox>
                                                            </FooterTemplate>
                                                           </asp:TemplateColumn>

                                                           <asp:TemplateColumn HeaderText="Julio">
                                                           <HeaderStyle HorizontalAlign="Center" Width="50px" VerticalAlign="Middle"></HeaderStyle>
                                                            <ItemStyle Font-Size="9px"  HorizontalAlign="Center" Width="50px"></ItemStyle>
                                                            <FooterStyle Font-Size="9px" HorizontalAlign="Center" Width="50px"/>
                                                            <ItemTemplate>
		                                                         <asp:Label id="lblImp7" runat="server" Width="60px" Text='<%# String.Format("{0:N}", Container.DataItem("IM_PR07"))%>'></asp:Label>
		                                                    </ItemTemplate>
                                                            <EditItemTemplate>
                                                                <asp:TextBox id="txtImp7" runat="server" Width="60px" Text='<%# DataBinder.Eval(Container, "DataItem.IM_PR07") %>'></asp:TextBox>
                                                            </EditItemTemplate>
                                                            <FooterTemplate>
                                                                 <asp:TextBox id="txtImp7Foot" runat="server" Width="60px" Text="0.00"></asp:TextBox>
                                                            </FooterTemplate>
                                                           </asp:TemplateColumn>

                                                           <asp:TemplateColumn HeaderText="Agosto">
                                                           <HeaderStyle HorizontalAlign="Center" Width="50px" VerticalAlign="Middle"></HeaderStyle>
                                                            <ItemStyle Font-Size="9px"  HorizontalAlign="Center" Width="50px"></ItemStyle>
                                                             <FooterStyle Font-Size="9px" HorizontalAlign="Center" Width="50px"/>
                                                            <ItemTemplate>
		                                                         <asp:Label id="lblImp8" runat="server" Width="60px" Text='<%# String.Format("{0:N}", Container.DataItem("IM_PR08"))%>'></asp:Label>
		                                                    </ItemTemplate>
                                                            <EditItemTemplate>
                                                                <asp:TextBox id="txtImp8" runat="server" Width="60px" Text='<%# DataBinder.Eval(Container, "DataItem.IM_PR08") %>'></asp:TextBox>
                                                            </EditItemTemplate>
                                                            <FooterTemplate>
                                                                 <asp:TextBox id="txtImp8Foot" runat="server" Width="60px" Text="0.00"></asp:TextBox>
                                                            </FooterTemplate>
                                                           </asp:TemplateColumn>

                                                           <asp:TemplateColumn HeaderText="Setiembre">
                                                           <HeaderStyle HorizontalAlign="Center" Width="50px" VerticalAlign="Middle"></HeaderStyle>
                                                            <ItemStyle Font-Size="9px"  HorizontalAlign="Center" Width="50px"></ItemStyle>
                                                             <FooterStyle Font-Size="9px" HorizontalAlign="Center" Width="50px"/>
                                                            <ItemTemplate>
		                                                         <asp:Label id="lblImp9" runat="server" Width="60px" Text='<%# String.Format("{0:N}", Container.DataItem("IM_PR09"))%>'></asp:Label>
		                                                    </ItemTemplate>
                                                            <EditItemTemplate>
                                                                <asp:TextBox id="txtImp9" runat="server" Width="60px" Text='<%# DataBinder.Eval(Container, "DataItem.IM_PR09") %>'></asp:TextBox>
                                                            </EditItemTemplate>
                                                            <FooterTemplate>
                                                                 <asp:TextBox id="txtImp9Foot" runat="server" Width="60px" Text="0.00"></asp:TextBox>
                                                            </FooterTemplate>
                                                           </asp:TemplateColumn>

                                                           <asp:TemplateColumn HeaderText="Octubre">
                                                           <HeaderStyle HorizontalAlign="Center" Width="60px" VerticalAlign="Middle"></HeaderStyle>
                                                            <ItemStyle Font-Size="9px"  HorizontalAlign="Center" Width="50px"></ItemStyle>
                                                            <FooterStyle Font-Size="9px" HorizontalAlign="Center" Width="50px"/>
                                                            <ItemTemplate>
		                                                         <asp:Label id="lblImp10" runat="server" Width="60px" Text='<%# String.Format("{0:N}", Container.DataItem("IM_PR10"))%>'></asp:Label>
		                                                    </ItemTemplate>
                                                            <EditItemTemplate>
                                                                <asp:TextBox id="txtImp10" runat="server" Width="60px" Text='<%# DataBinder.Eval(Container, "DataItem.IM_PR10") %>'></asp:TextBox>
                                                            </EditItemTemplate>
                                                            <FooterTemplate>
                                                                 <asp:TextBox id="txtImp10Foot" runat="server" Width="60px" Text="0.00"></asp:TextBox>
                                                            </FooterTemplate>
                                                           </asp:TemplateColumn>

                                                           <asp:TemplateColumn HeaderText="Noviembre">
                                                           <HeaderStyle HorizontalAlign="Center" Width="60px" VerticalAlign="Middle"></HeaderStyle>
                                                            <ItemStyle Font-Size="9px"  HorizontalAlign="Center" Width="50px"></ItemStyle>
                                                            <FooterStyle Font-Size="9px" HorizontalAlign="Center" Width="50px"/>
                                                            <ItemTemplate>
		                                                         <asp:Label id="lblImp11" runat="server" Width="60px" Text='<%# String.Format("{0:N}", Container.DataItem("IM_PR11"))%>'></asp:Label>
		                                                    </ItemTemplate>
                                                            <EditItemTemplate>
                                                                <asp:TextBox id="txtImp11" runat="server" Width="60px" Text='<%# DataBinder.Eval(Container, "DataItem.IM_PR11") %>'></asp:TextBox>
                                                            </EditItemTemplate>
                                                            <FooterTemplate>
                                                                 <asp:TextBox id="txtImp11Foot" runat="server" Width="60px" Text="0.00"></asp:TextBox>
                                                            </FooterTemplate>
                                                           </asp:TemplateColumn>

                                                           <asp:TemplateColumn HeaderText="Diciembre">
                                                           <HeaderStyle HorizontalAlign="Center" Width="60px" VerticalAlign="Middle"></HeaderStyle>
                                                            <ItemStyle Font-Size="9px"  HorizontalAlign="Center" Width="50px"></ItemStyle>
                                                            <FooterStyle Font-Size="9px" HorizontalAlign="Center" Width="50px"/>
                                                            <ItemTemplate>
		                                                         <asp:Label id="lblImp12" runat="server" Width="60px" Text='<%# String.Format("{0:N}", Container.DataItem("IM_PR12"))%>'></asp:Label>
		                                                    </ItemTemplate>
                                                            <EditItemTemplate>
                                                                <asp:TextBox id="txtImp12" runat="server" Width="60px" Text='<%# DataBinder.Eval(Container, "DataItem.IM_PR12") %>'></asp:TextBox>
                                                            </EditItemTemplate>
                                                            <FooterTemplate>
                                                                 <asp:TextBox id="txtImp12Foot" runat="server" Width="60px" Text="0.00"></asp:TextBox>
                                                            </FooterTemplate>
                                                           </asp:TemplateColumn>

                                                           <asp:TemplateColumn HeaderText="Total">
                                                           <HeaderStyle HorizontalAlign="Center" Width="60px" VerticalAlign="Middle"></HeaderStyle>
                                                            <ItemStyle Font-Size="9px"  HorizontalAlign="Center" Width="50px"></ItemStyle>
                                                            <FooterStyle Font-Size="9px" HorizontalAlign="Center" Width="50px"/>
                                                            <ItemTemplate>
		                                                         <asp:Label id="lblTotal" runat="server" Width="60px" Text='<%# String.Format("{0:N}", Container.DataItem("TOTAL"))%>'></asp:Label>
		                                                    </ItemTemplate>
                                                            <EditItemTemplate>
                                                                <asp:Label id="lblTotalEdit" runat="server" Width="60px" Text='<%# DataBinder.Eval(Container, "DataItem.TOTAL") %>'></asp:Label>
                                                            </EditItemTemplate>
                                                           <%-- <FooterTemplate>
                                                                 <asp:TextBox id="txtImp12Foot" runat="server" Width="50px" Text="0.00"></asp:TextBox>
                                                            </FooterTemplate>--%>
                                                           </asp:TemplateColumn>

                                                           <asp:TemplateColumn HeaderText="Estado">
                                                           <HeaderStyle HorizontalAlign="Center" Width="60px" VerticalAlign="Middle"></HeaderStyle>
                                                            <ItemStyle Font-Size="9px"  HorizontalAlign="Center" Width="50px"></ItemStyle>
                                                            <FooterStyle Font-Size="9px" HorizontalAlign="Center" Width="50px"/>
                                                            <ItemTemplate>
		                                                         <asp:Label id="lblEstado" runat="server" Width="50px" Text='<%# DataBinder.Eval(Container, "DataItem.ES_OT") %>'></asp:Label>
		                                                    </ItemTemplate>
                                                            <EditItemTemplate>
                                                                <asp:Label id="txtlEstadoEdit" runat="server" Width="50px" Text='<%# DataBinder.Eval(Container, "DataItem.ES_OT") %>'></asp:Label>
                                                            </EditItemTemplate>
                                                            <%--<FooterTemplate>
                                                                 <asp:TextBox id="txtImp12Foot" runat="server" Width="50px" Text="0.00"></asp:TextBox>
                                                            </FooterTemplate>--%>
                                                           </asp:TemplateColumn>

                                                           <asp:TemplateColumn HeaderText="Editar">
                                                            <ItemStyle Width="60px"/>
                                                            
                                                            <ItemTemplate>
                                                                <asp:LinkButton ID="lnkEdit" runat="server" CausesValidation="false" Text="<img border=0 src=../images/im_edit.gif alt=editar>" CommandName="Edit"></asp:LinkButton>
                                                            </ItemTemplate>
                                                            <EditItemTemplate>
                                                                <asp:LinkButton ID="lnkUpdate" runat="server" Text="<img border=0 src=../images/save.gif alt=guardar/actualizar>" CommandName="Update">
                                                                    <img border="0" src="../images/save.gif"/ alt="guardar/actualizar" width="16" height="16">
                                                                </asp:LinkButton>&nbsp;
                                                                <asp:LinkButton ID="lnkCancel" runat="server" CausesValidation="false" Text="<img border=0 src=../images/delete.gif alt=cancelar>" CommandName="Cancel">
                                                                </asp:LinkButton>
                                                            </EditItemTemplate>
                                                        </asp:TemplateColumn>

                                                        <asp:TemplateColumn HeaderText="Eliminar" ItemStyle-Width="50px">
                                                            <ItemTemplate>
                                                                <asp:LinkButton ID="lnkDelete" runat="server" CausesValidation="false" Text="<img border=0 src=../images/delete.gif alt=eliminar>" CommandName="Delete">
                                                                    <img border="0" src="../images/delete.gif" alt="eliminar" width="16" height="16"/>
                                                                </asp:LinkButton>    
                                                            </ItemTemplate>
                                                            <FooterTemplate>
                                                                <asp:LinkButton ID="linkAgregar" runat="server" CommandName="AddNewRow">
                                                                    <img border="0" src="../images/save.gif" alt="agregar" width="16" height="16"/>
                                                                </asp:LinkButton>
                                                            </FooterTemplate>
                                                        </asp:TemplateColumn>
                                                    </Columns>
                                                </asp:DataGrid>
                                            </table>
                                        </td>
                                     </tr>
                                 </table>
                            </asp:Panel>
                            <%--<asp:Panel ID="pnlAgregar" runat="server">
                           
                                <table style="width: 960px">
		                               <tr>
		                                    <td class="Descripcion" style="width:60px" align="left">Responsable:</td>
                                            <td class="Descripcion" style="width:160px" align="left">Descripción Orden Trabajo:</td>
		                                    <td class="Descripcion" style="width:30px" align="left">&nbsp;</td>
		                               </tr>
                                       <tr>
                                            <td class="style2">
		                                         <asp:textbox id="txtCodRespo" runat="server" Font-Size="9px" width="60px" MaxLength="20" CssClass="txtHabilitado"></asp:textbox>
                                                    &nbsp;<input id="btnCodResp" style="width: 20px" onclick="javascript:fnc_BuscarResponsable();" type="button" value="..." name="btnCodResp" class="Boton"/>
                                            </td>
                                            <td class="style2">
                                                 <asp:textbox id="txtDescOT" runat="server" Font-Size="9px" width="200px" MaxLength="20" CssClass="txtHabilitado"></asp:textbox>
                                            </td>
                                            <td class="style7">
                                             <asp:imagebutton id="Imagebutton1" runat="server" 
                                                ImageUrl = "../images/save.gif" ToolTip="Guardar Registro en el  Pedido" Height="16px"></asp:imagebutton>
                                            </td>
                                       </tr>
                                       <tr>
                                         <td class="DescripcionResponsable" style="width:140px">
                                            <asp:TextBox ID="txtDesResp" runat="server" BorderStyle="None" Cssclass="Descripcion" Font-Size="8px" width="140px"></asp:TextBox>
                                        </td>
                                        
                                       </tr>
                                </table>
                                        
                                <table style="width: 700px" align="center">
                                    <tr>
                                        <td align="left" class="Etiqueta" height="30px" style="WIDTH: 90px;">Orden de trabajo:</td>
                                            <td align="left" height="30px" style="WIDTH: 180px;">
                                                <asp:Label ID="OT" runat="server" Width="120px"></asp:Label>
                                            </td>
                                    </tr>
                                </table>
                               
                            </asp:Panel>--%>
                        </td>
                    </tr>
                 </table>
         </center>
         
    </form>
</body>
</html>
