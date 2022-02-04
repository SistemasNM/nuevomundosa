<html>
<head>
<title><%= Session("oRpt").ReportTitle %></title>
</head>
<body bgcolor=C6C6C6 onunload="CallDestroy();">
<P align="center">
<script language="javascript">
document.writeln('<EMBED name="startup"');
document.writeln('type=application/x-crreportviewer-plugin;version=9.0.0.0');
document.writeln('Pluginspage="/viewer/ActiveXViewer/en/get-npviewer.htm"');
document.writeln('Width=100% ');
document.writeln('Height=100% ');
document.writeln('Param_URL="rptserver.asp" ');
document.writeln('Param_EnableExportButton="true" ');
document.writeln('Param_EnableHelpButton="false" ');
document.writeln('Param_DisplayGroupTree="true" ');
document.writeln('Param_DisplayToolbar="true" ');
document.writeln('Param_EnableGroupTree="true" ');
document.writeln('Param_EnablePrintButton="true" ');
document.writeln('Param_EnableRefreshButton="true" ');
document.writeln('Param_EnableZoomControl="true" ');
document.writeln('>');
document.writeln('</EMBED>');
</script>

<script language="javascript">
function CallDestroy()
{
	window.open("Cleanup.asp");
}
</script>
</p>
</body>
</html>