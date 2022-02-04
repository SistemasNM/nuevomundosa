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

%>                                                                     

<% 
'   This is the name of the report being used in this example. 
'   This variable is being used in the AlwaysRequiredSteps.asp 
'   To use a different report, change it here.

Set objConexion = Server.CreateObject("EnlaceNuevoMundo.Conexion")
strServidor = CSTR(objConexion.GetServidor("OFIPLAN"))
strBaseDatos = CSTR(objConexion.GetBaseDatos("OFIPLAN"))
strUsuario = CSTR(objConexion.GetUsuario("OFIPLAN"))
strPassword = CSTR(objConexion.GetPassword("OFIPLAN"))


Dim strEmpresa,strPlanilla,strAnio,strMes,strCorrelativo,strCCosto,strCodInicio,strCodFin,strObservacion

strEmpresa=cstr(request.QueryString("strEmpresa"))
strPlanilla=cstr(request.QueryString("strPlanilla"))
strAnio=request.QueryString("strAnio")
strMes=request.QueryString("strMes")
strCorrelativo = request.QueryString("strCorrelativo")
strCCosto = cstr(request.QueryString("strCCosto"))
strCodInicio = request.QueryString("strCodInicio")
strCodFin = request.QueryString("strCodFin")
strObservacion = cstr(request.QueryString("strObservacion"))

if strPlanilla="EMP" then
	reportname = "../Rpts/Rpt_Boleta_Pago_Emp.rpt"
end if

if strPlanilla="OBM" then
	reportname = "../Rpts/Rpt_Boleta_Pago_Obm.rpt"
end if


%>

<!-- #include file="AlwaysRequiredSteps.asp" -->                       

<% 
'   If it is easier to understand, simply delete the line above,  
'   and replace it with the entire contents of the file  
'   AlwaysRequiredSteps.asp                                             
'==================================================================
'==================================================================
                                                                     

' set the oApp to check logons when there is a logon already to 
' 
set Session("options") =  Session("oApp").options
Session("options").MatchLogonInfo = 1

' Create Database object
set ReportDatabase = Session("oRpt").Database

'Database Table collection
Set crdatabasetables = ReportDatabase.tables

' Set the location
set crtable = crdatabasetables.Item(1)
crtable.SetLogonInfo  CSTR(strServidor), CSTR(strBaseDatos), CSTR(strUsuario), CSTR(strPassword)


'==================================================================
' WORK WITH STORED PROCEDURE PARAMETERS

'This will disable the Parameter Prompting on the Server
Session("oRpt").ParameterPromptingEnabled = False

'This line defines the Parameter Array
set Params = Session("oRpt").ParameterFields

'These are the parameter values that are set



Params.Item(1).SetCurrentValue (cstr(strEmpresa))
Params.Item(2).SetCurrentValue (cstr(strPlanilla))
Params.Item(3).SetCurrentValue (cint(strAnio))
Params.Item(4).SetCurrentValue (cint(strMes))
Params.Item(5).SetCurrentValue (cint(strCorrelativo))
Params.Item(6).SetCurrentValue (cstr(strCCosto))
Params.Item(7).SetCurrentValue (cdbl(strCodInicio))
Params.Item(8).SetCurrentValue (cdbl(strCodFin))
Params.Item(9).SetCurrentValue (cstr(strObservacion))

'response.write strEmpresa+" "+strPlanilla + " " + strAnio +"  "+ strMes+"  "+strCorrelativo+" "+strCCosto+"  "+strCodInicio+" "+strCodFin+" "+strObservacion
'response.write strServidor + " " + strBaseDatos + " " + strUsuario + " " + strPassword
'response.end

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

