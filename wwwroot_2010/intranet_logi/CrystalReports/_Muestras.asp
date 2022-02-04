<%@ LANGUAGE="VBSCRIPT" %>

<%
'==================================================================
'   WORKING WITH STORED PROCEDURE PARAMETERS
'==================================================================
'
' CONCEPT                                                             
'   The Application object (oApp) is needed so that we can create the 
'   report object.
'   Once we have created the report object (oRpt), we can then  
'   gain access to such things the "DatabaseParameters" in that  
'   report.   
'                                                            
'  ALWAYS REQUIRED STEPS (contained in AlwaysRequiredSteps.asp)
'   -  create the application object                                
'   -  create the report object                                     
'   -  open the report                                              
'
'  WORK WITH STORED PROCEDURE PARAMETERS
'   -  get the database in the report
'   -  get the database's Stored Procedure Parameters
'   -  get the specific Store Procedure Parameter
'   -  save the new value to the Stored Proc Param
'
'  MORE ALWAYS REQUIRED STEPS
'   -  retrieve the records                                         
'   -  create the page engine 
'
'  DISPLAY THE REPORT
'   -  display the report using a smart viewer
' = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = 



'==================================================================
'==================================================================
' ALWAYS REQUIRED STEPS
'
' Include the file AlwaysRequiredSteps.asp which contains the code    
' for steps:
'   -  create the application object  
'   -  create the report object and open the report
'CAMBIO JRUIZS - ACTUALIZAR VARIABLES 20211126 - INI
''Set objConexion = Server.CreateObject("EnlaceNuevoMundo.Conexion")
''strServidor = CSTR(objConexion.GetServidor("OFIVENT"))
''strBaseDatos = CSTR(objConexion.GetBaseDatos("OFIVENT"))
''strUsuario = CSTR(objConexion.GetUsuario("OFIVENT"))
''strPassword = CSTR(objConexion.GetPassword("OFIVENT"))

    strServidor =  "Servbd02\nmundo02"
	strBaseDatos = "OFIVENT"
	strUsuario =   "ENLACE08"
	strPassword =  "ENLACE08"
    ''CAMBIO JRUIZ -ACTUALIZAR VARIABLES - 20211126 FIN

%>                                                                     

<% 
'   This is the name of the report being used in this example. 
'   This variable is being used in the AlwaysRequiredSteps.asp 
'   To use a different report, change it here.
Dim lstrTipo
lstrTipo=Request("strTipo")
'response.Write lstrTipo
'response.End 
'response.Write Len(lstrtipo)
If Trim(lstrTipo) = "3" Then
	'reportname = "../Opciones/logistica/Rpts/rpt_MuestrasClientes.rpt"
	reportname = "../Rpts/MuestrasClienteGral.rpt"
End If
'reportname	  = "rptComprasClientes.rpt"
%>

<!-- #include file="AlwaysRequiredSteps.asp" -->                       

<% 
'   If it is easier to understand, simply delete the line above,  
'   and replace it with the entire contents of the file  
'   AlwaysRequiredSteps.asp                                             
'==================================================================
'==================================================================
                                                                     
userid = user
password = passw


' set the oApp to check logons when there is a logon already to 
' 
set Session("options") =  Session("oApp").options
Session("options").MatchLogonInfo = 1

' Create Database object
set ReportDatabase = Session("oRpt").Database

'Database Table collection
Set crdatabasetables = ReportDatabase.tables

' Set the location
' Set the location
set crtable = crdatabasetables.Item(1)
crtable.SetLogonInfo  CSTR(strServidor), CSTR(strBaseDatos), CSTR(strUsuario), CSTR(strPassword)



'==================================================================
' WORK WITH STORED PROCEDURE PARAMETERS

'This will disable the Parameter Prompting on the Server
Session("oRpt").ParameterPromptingEnabled = true
Session("oRpt").Discardsaveddata=true

'This line defines the Parameter Array
set Params = Session("oRpt").ParameterFields

'These are the parameter values that are set
Dim lstrEmpresa,lstrFechaIni, lstrFechaFin, lstrCodigoVendedor, lstrCodigoCliente,lstrTipoMuestra
'Dim lstrEmpresa,lstrTipoMuestra,lstrPeriodo
dim lstrCliente,lstrArticulo
'lstrPeriodo=Request("strPeriodo")
lstrEmpresa=Request("strEmpresa")

lstrFechaIni=request("strFechaIni")
lstrFechaFin=request("strFechaFin")
lstrCodigoVendedor=request("strCodigoVendedor")
lstrCodigoCliente=request("strCodigoCliente")
lstrTipoMuestra="24"

'response.Write lstrFechaIni
'response.Write lstrFechaFin
'response.Write lstrCodigoVendedor
'response.Write lstrCodigoCliente

'response.End 


If Trim(lstrTipo) = "3" Then
    Params.Item(1).SetCurrentValue (lstrEmpresa)
	Params.Item(2).SetCurrentValue (lstrFechaIni)
	Params.Item(3).SetCurrentValue (lstrFechaFin)	
	Params.Item(4).SetCurrentValue (lstrCodigoVendedor)
	Params.Item(5).SetCurrentValue (lstrCodigoCliente)	
end if

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

