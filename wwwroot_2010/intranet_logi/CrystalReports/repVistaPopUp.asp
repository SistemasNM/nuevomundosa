<%
'<><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><>
'Modulo		 : Modulo de Telares
'Tema		 : Implementacion de Reporte
'Descripcion : Pagina intermediaria para mostrar los reportes en un popUp.
'Fecha		 : 13-07-2004
'<><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><>

Dim strUrlPagina, strParametros, strUrlViwer

'Pagina q va a contener el reporte.
strUrlViwer = "repVistaPopupFrame.asp"
'Pagina q crea el reporte y es puesta en el Viwer.
strUrlPagina = Request("strUrlPagina")

strParametros = Request.QueryString
%>

<!--Script para el abrir el popup-->
<script language=javascript>	
	//Abrimos la ventana
	popUp("<%=strUrlViwer%>?<%=strParametros%>");
	//Regresamos a la pag. anterior.
	window.history.back();
	
	function popUp(strUrl) {
		var intWidth = screen.width;
		var intHeight = screen.height;
		window.open(strUrl, "", "top=0; left=0; width=" + intWidth + "; height=" + intHeight + "; resizable=1;");
	}
</script>
