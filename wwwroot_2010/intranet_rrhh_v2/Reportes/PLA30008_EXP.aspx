<%@ Page Language="vb" AutoEventWireup="false" Codebehind="PLA30008_EXP.aspx.vb" Inherits="intranet_rrhh.PLA30008_EXP"%>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html>
 <head>
 <title>PLA30008_EXP</title>
 <meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR" />
 <meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE" />
 <meta content="JavaScript" name="vs_defaultClientScript" />
 <meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema" />
 <%--<link href="../../intranet/Estilos/NM0001.css" type="text/css" rel="stylesheet" />--%>
 <script type ="text/javascript" language="javascript" src="../../intranet/JS/jsCalendario_N3.js"></script>
 <script type="text/javascript" language="javascript">
  // exporta excel
  function popUp(strUrl) {
   var intWidth = screen.width;
   var intHeight = screen.height;
   window.open(strUrl, "", "top=0; left=0; width=" + intWidth + "; height=" + intHeight + "; resizable=1;");
  }	
 </script>
<style >
.txtcabecera1{
  vertical-align:top;
  padding-left:2pt;
  padding-top:2pt;
  padding-right:2pt;
  padding-bottom:2pt;
  border:0.5pt solid #7292cc;
  background-color:#4c68a2;
  text-align:center;
  direction:ltr;
  vertical-align:top;
  font-style:normal;
  font-family:Arial;
  font-size:7pt;
  font-weight:normal;
  text-decoration:none;
  unicode-bidi:normal;
  color:White;
  width:70px;
}
.txttexto1{
  vertical-align:top;
  padding-left:2pt;
  padding-top:2pt;
  padding-right:2pt;
  padding-bottom:2pt;
  border:0.5pt solid #e5e5e5;
  background-color:Transparent;
  text-align:left;
  direction:ltr;
  font-style:normal;
  font-family:Arial;
  font-size:6pt;
  font-weight:400;
  text-decoration:none;
  unicode-bidi:normal;
  color:#4d4d4d;
}
</style>
</head>
<body >
 <form id="Form1" method="post" runat="server">
  <asp:table id="tblmaestro" style="Z-INDEX: 101; LEFT: 8px; POSITION: absolute; TOP: 8px" runat="server"></asp:table>
  <asp:table id="tblresumen" style="Z-INDEX: 101; LEFT: 8px; POSITION: absolute; TOP: 8px" runat="server"></asp:table>
 </form>
</body>
</html>
