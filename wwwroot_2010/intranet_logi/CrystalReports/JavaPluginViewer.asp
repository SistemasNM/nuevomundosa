<html>
<head>
<title><%= Session("oRpt").ReportTitle %></title>
</head>
<body bgcolor=C6C6C6 onunload="CallDestroy();">

<P align="center">
<object
	classid="clsid:8AD9C840-044E-11D1-B3E9-00805F499D93"
	width=100%
	height=100%
	codebase="/viewer/JavaPlugIn/Win32/j2re-1_3_1_01-win-i.exe#Version=1,2,2,0" VIEWASTEXT>
<param name=type value="application/x-java-applet;version=1.2.2">
<param name=code value="com.crystaldecisions.ReportViewer.ReportViewer">
<param name=codebase value="/viewer/javaviewer/">
<param name=archive value="ReportViewer.jar">
<param name=Language value="en">
<param name=ReportName value="rptserver.asp">
<param name=HasGroupTree value="true">
<param name=ShowGroupTree value="true">
<param name=HasRefreshButton value="true">
<param name=HasPrintButton value="true">
<param name=HasExportButton value="true">
<param name=HasTextSearchControls value="true">
<param name=CanDrillDown value="true">
<param name=HasZoomControl value="true">
<param name=PromptOnRefresh value="true">
<comment>
<embed
	width=100%
	height=90%
	type="application/x-java-applet;jpi-version=1.3.1_01"
	pluginspage="/viewer9/JavaPlugIn/Win32/j2re-1_3_1_01-win-i.exe"
	java_code="com.crystaldecisions.ReportViewer.ReportViewer"
	java_codebase="/viewer9/javaviewer/"
	java_archive="ReportViewer.jar"
Language="en"
ReportName="rptserver.asp"
ReportParameter=""
HasGroupTree="true"
ShowGroupTree="true"
HasRefreshButton="true"
HasPrintButton="true"
HasExportButton="true"
HasTextSearchControls="true"
CanDrillDown="true"
HasZoomControl="true"
PromptOnRefresh="true"
></embed>
</comment>
</object>
</p>
<script language="javascript">

function CallDestroy()
{
	window.open("Cleanup.asp","cuerpo");
}
</script>
</body>
</html>