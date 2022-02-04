<%
'<><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><>
'Modulo		 : Modulo de Telares
'Tema		 : Implementacion de Reporte
'Descripcion : Pagina intermediaria para mostrar los reportes en un popUp.
'Fecha		 : 13-07-2004
'<><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><><>

Dim strParametros, strUrlPagina

strUrlPagina = Request("strUrlPagina")
strParametros = Request.QueryString
strParametros = Right( strParametros, len(strParametros) - (12 + 2 + len(strUrlPagina)) )

strUrlPagina = strUrlPagina & "?" & strParametros
%>

<script language="JavaScript1.2">
<!--//Descargamos previamente los controles creados
	window.open("Cleanup.asp");
	// Maximizar Ventana
	window.moveTo(0,0);
	if (document.all) {
		top.window.resizeTo(screen.availWidth,screen.availHeight);
	} else if (document.layers||document.getElementById) {
		if (top.window.outerHeight<screen.availHeight||top.window.outerWidth<screen.availWidth){
			top.window.outerHeight = screen.availHeight;
			top.window.outerWidth = screen.availWidth;
		}
	}
//-->
</script>

<frameset rows=0%,100% border=0 framespacing=0 frameborder=no>
	<frame frameborder=no src="" name="viwerHead"></frame>
	<frame frameborder=no src="<%=strUrlPagina%>" name="viwerCenter"></frame>
</frameset>
