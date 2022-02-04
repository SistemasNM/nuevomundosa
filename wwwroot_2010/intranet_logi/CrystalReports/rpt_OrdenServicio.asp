<%@ LANGUAGE="VBSCRIPT" %>
	<%
	'CONEXION A BASE DE DATOS
      'CAMBIO JRUIZS - ACTUALIZAR VARIABLES 20211126 - INI
	'Set objConexion = Server.CreateObject("EnlaceNuevoMundo.Conexion")
	'strServidor = CSTR(objConexion.GetServidor("OFILOGI"))
	'strBaseDatos = CSTR(objConexion.GetBaseDatos("OFILOGI"))
	'strUsuario = CSTR(objConexion.GetUsuario("OFILOGI"))
	'strPassword = CSTR(objConexion.GetPassword("OFILOGI"))

	strServidor =  "Servbd02\nmundo02"
	strBaseDatos = "OFILOGI"
	strUsuario =   "ENLACE08"
	strPassword =  "ENLACE08"
    ''CAMBIO JRUIZ -ACTUALIZAR VARIABLES - 20211126 FIN

  reportname = "../Rpts/rptFormtoTerminoOS.rpt"

%>
<!-- #include file="AlwaysRequiredSteps.asp" -->
<% 
' set the oApp to check logons when there is a logon already to 
set Session("options") =  Session("oApp").options
Session("options").MatchLogonInfo = 1

' Create Database object
set ReportDatabase = Session("oRpt").Database

'Database Table collection
Set crdatabasetables = ReportDatabase.tables

' Set the location
set crtable = crdatabasetables.Item(1)
crtable.SetLogonInfo  cstr(strServidor), cstr(strBaseDatos), cstr(strUsuario), cstr(strPassword)
'==================================================================
' WORK WITH STORED PROCEDURE PARAMETERS

'This will disable the Parameter Prompting on the Server
Session("oRpt").ParameterPromptingEnabled = False
		
'This line defines the Parameter Array
set Params = Session("oRpt").ParameterFields

'These are the parameter values that are set
	dim var_Codigo
	
	chr_Empresa = Request("vchCodigoEmpresa")
	var_OrdenCompraServicio = Request("vchCodigoOS")
	
	Params.Item(1).SetCurrentValue(chr_Empresa)
	Params.Item(2).SetCurrentValue(var_OrdenCompraServicio)
	
'==================================================================
'
'  MORE ALWAYS REQUIRED STEPS
'   -  retrieve the records                                         
'   -  create the page engine                                       
'   -  create the smart viewer and point it to rptserver.asp
'
%>
<!-- #include file="MoreRequiredSteps.asp" -->
<% 
'   If it is easier to understand, simply delete the line above,  
'   and replace it with the entire contents of the file 
'   MoreRequiredSteps.asp                                             
'==================================================================
'==================================================================

                                                                      

'==================================================================
'==================================================================
'  DISPLAY THE REPORT
'   -  display the report using a smart viewer
' 
' Include one of the Smart Viewers.
'   -  Smart Viewer ActiveX       =   SmartViewerActiveX.asp
'   -  Smart Viewer JAVA          =   SmartViewerJAVA.asp
'   -  Smart Viewer HTML          =   SmartViewerHTML.asp
'   -  Smart Viewer HTML Frame    =   SmartViewerHTMLFrame.asp
%>
<!-- #include file="SmartViewerActiveX.asp" -->
<% 
'   If it easier for you to conceptualize this code by seeing it  
'   all contained in a single file, simply delete the line above, 
'   and replace it with the entire contents of the file being  
'   included.
'==================================================================
'==================================================================
%>