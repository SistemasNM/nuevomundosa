<%@ Page Language="vb" AutoEventWireup="false" Codebehind="NMGetXML.aspx.vb" Inherits="intranet_logi.NMGetXml" %>
<?xml version="1.0" encoding="iso-8859-1" ?>
<xml>
	<%Select Case ucase(Request("Opcion"))%>
	<% Case "USUARIO" %>
	<%=GetXMLUsuario(Request("Codigo")) %>
	<% Case "PROVEEDOR" %>
	<%=GetXMLProveedor(Request("Codigo")) %>
	<% Case "TIPOAPROBACION"%>
	<%=GetXMLTipoAprobacion(Request("Codigo"))%>
	<% Case "CENTROCOSTO"%>
	<%=GetXMLCentroCosto(Request("Codigo"))%>
    <% Case "JEFATURA"%>
	<%=GetXMLJefatura(Request("Codigo"))%>
	<%end select %>
</xml>