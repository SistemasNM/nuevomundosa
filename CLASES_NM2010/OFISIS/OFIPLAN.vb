Imports NuevoMundo.Generales
Imports NM.AccesoDatos
Imports System.IO
Imports System.Data
Namespace OFIPLAN
    ' trabajador
    Public Class Trabajador
        Inherits Clases.General
        Implements Interfases.IOFISIS

        Private bCreate As Boolean

#Region "    Variables"
        Private mstrCodigo As String
        Private mstrApellidoPaterno As String
        Private mstrApellidoMaterno As String
        Private mstrNombres As String
        Private mstrNombreCompleto As String
        Private mstrCentroCostoCodigo As String
        Private mstrCentroCostoNombre As String
#End Region

#Region "    Propiedades"
        Public Property Codigo() As String
            Get
                Codigo = mstrCodigo
            End Get
            Set(ByVal Value As String)
                mstrCodigo = Value
            End Set
        End Property
        Public Property NombreCompleto() As String
            Get
                NombreCompleto = mstrNombreCompleto
            End Get
            Set(ByVal Value As String)
                mstrNombreCompleto = Value
            End Set
        End Property
        Public Property CentroCostoCodigo() As String
            Get
                CentroCostoCodigo = mstrCentroCostoCodigo
            End Get
            Set(ByVal Value As String)
                mstrCentroCostoCodigo = Value
            End Set
        End Property
        Public Property CentroCostoNombre() As String
            Get
                CentroCostoNombre = mstrCentroCostoNombre
            End Get
            Set(ByVal Value As String)
                mstrCentroCostoNombre = Value
            End Set
        End Property
#End Region

#Region "    Constructor"
        Sub New(ByVal pstrEmpresa As String, ByVal pstrUsuario As String)
            Me.EmpresaCodigo = pstrEmpresa
            Me.UsuarioBD = pstrUsuario
            Me.SP_LISTAR = "usp_qry_TrabajadorListar"

        End Sub
#End Region

        Public Function Buscar() As Boolean Implements NuevoMundo.Generales.Interfases.IOFISIS.Buscar
            Dim lobjBD As NM.AccesoDatos.AccesoDatosSQLServer
            Dim ldtRes As DataTable
            Dim larrParams() As String = {"P_EMPRESA", Me.EmpresaCodigo, "P_TRABAJADOR", Me.Codigo}

            Try
                lobjBD = New NM.AccesoDatos.AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.Intranet)
                ldtRes = lobjBD.ObtenerDataTable("usp_qry_BuscarTrabajadores", larrParams)
                mstrApellidoPaterno = ldtRes.Rows(0).Item("no_apel_pate")
                mstrApellidoMaterno = ldtRes.Rows(0).Item("no_apel_mate")
                mstrNombres = ldtRes.Rows(0).Item("no_trab")
                mstrNombreCompleto = ldtRes.Rows(0).Item("Nombre")
                mstrCentroCostoCodigo = ldtRes.Rows(0).Item("co_cent_cost")
                mstrCentroCostoNombre = ldtRes.Rows(0).Item("no_auxi")
                Me.Ok = True
            Catch ex As Exception
                mstrApellidoPaterno = ""
                mstrApellidoMaterno = ""
                mstrNombres = ""
                mstrNombreCompleto = ""
                mstrCentroCostoCodigo = ""
                mstrCentroCostoNombre = ""
                Me.Ok = False
            Finally
                lobjBD = Nothing
            End Try
            Return Me.Ok
        End Function

        Public Sub Dispose() Implements NuevoMundo.Generales.Interfases.IOFISIS.Dispose

        End Sub

        Public Function Listar(ByRef pLista As System.Data.DataTable, ByVal ParamArray Flags() As String) As Boolean Implements NuevoMundo.Generales.Interfases.IOFISIS.Listar
            Dim lobjPlan As NM.AccesoDatos.AccesoDatosSQLServer
            Dim larrParams() As String = {"p_var_Empresa", Me.EmpresaCodigo, _
                                "p_var_TipoPlanilla", Flags(0), _
                                "p_var_Codigo", Flags(1), _
                                "p_var_Nombre", Flags(2)}
            Try
                lobjPlan = New NM.AccesoDatos.AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.PlanOfisis)
                pLista = lobjPlan.ObtenerDataTable(SP_LISTAR, larrParams)
            Catch ex As Exception

            Finally
                lobjPlan.Dispose()
                lobjPlan = Nothing
            End Try
        End Function

    End Class

    ' cts
    Public Class CTS
        Inherits Clases.General

        Sub New(ByVal pstrEmpresa As String, ByVal pstrUsuario As String)
            Me.EmpresaCodigo = pstrEmpresa
            Me.UsuarioBD = pstrUsuario
        End Sub

        Public Function getDatoEmpleado(ByVal vCodigo As String, ByRef dtRes As DataTable) As Boolean
            Dim lobjConexion As NM.AccesoDatos.AccesoDatosSQLServer
            Dim lstrParametros() As String = {"Co_Empr", Me.EmpresaCodigo, "Co_Trab", vCodigo}
            Dim lbooOk As Boolean = False
            Try
                lobjConexion = New NM.AccesoDatos.AccesoDatosSQLServer(NM.AccesoDatos.GeneradorCadenaConexion.enmBasesDatos.PlanOfisis)
                dtRes = lobjConexion.ObtenerDataTable("USP_PLA_CTSPEND_DATOS", lstrParametros)
                lbooOk = True
            Catch ex As Exception
                dtRes = Nothing
                Throw ex
            Finally
                lobjConexion = Nothing
            End Try
            Return lbooOk
        End Function

        Public Function getDetalleCts(ByVal vCodigo As String, ByRef dtRes As DataTable) As Boolean
            Dim lobjConexion As NM.AccesoDatos.AccesoDatosSQLServer
            Dim lstrParametros() As String = {"Co_Empr", Me.EmpresaCodigo, "Co_Trab", vCodigo}
            Dim lbooOk As Boolean = False
            Try
                lobjConexion = New NM.AccesoDatos.AccesoDatosSQLServer(NM.AccesoDatos.GeneradorCadenaConexion.enmBasesDatos.PlanOfisis)
                dtRes = lobjConexion.ObtenerDataTable("USP_PLA_CTSPEND_LISTA_CODIGO", lstrParametros)
                lbooOk = True
            Catch ex As Exception
                dtRes = Nothing
                Throw ex
            Finally
                lobjConexion = Nothing
            End Try
            Return lbooOk
        End Function

        Public Function setDetalleCts(ByVal vCodigo As String) As Boolean
            Dim lobjconexion As NM.AccesoDatos.AccesoDatosSQLServer
            Dim lstrParametros() As String = {"Co_empr", Me.EmpresaCodigo, "Co_Usua", Me.UsuarioBD, "Co_Trab", vCodigo}
            Dim lbooOk As Boolean = False
            Dim Retorno As Integer

            Try
                lobjconexion = New NM.AccesoDatos.AccesoDatosSQLServer(NM.AccesoDatos.GeneradorCadenaConexion.enmBasesDatos.PlanOfisis)
                Retorno = lobjconexion.EjecutarComando("USP_PLA_CTSPEND_CANCELAR", lstrParametros)
                lbooOk = True
            Catch ex As Exception
                lbooOk = False
            Finally
                lobjconexion = Nothing
            End Try
            Return lbooOk

        End Function

        Public Function ListarAnnos(ByRef dtRes As DataTable) As Boolean
            Dim lobjconexion As NM.AccesoDatos.AccesoDatosSQLServer
            Dim lstrParametros() As String = {"Co_empr", Me.EmpresaCodigo}
            Dim lbooOk As Boolean = False
            Try
                lobjconexion = New NM.AccesoDatos.AccesoDatosSQLServer(NM.AccesoDatos.GeneradorCadenaConexion.enmBasesDatos.PlanOfisis)
                dtRes = lobjconexion.ObtenerDataTable("USP_PLA_CTSPEND_LISTA_AA", lstrParametros)
                lbooOk = True
            Catch ex As Exception
                dtRes = Nothing
                Throw ex
            Finally
                lobjconexion = Nothing
            End Try
            Return lbooOk
        End Function

        Public Function ListarMeses(ByRef VAnno As Integer, ByRef dtRes As DataTable) As Boolean
            Dim lobjconexion As NM.AccesoDatos.AccesoDatosSQLServer
            Dim lstrParametros() As String = {"Co_empr", Me.EmpresaCodigo, "Nu_Anno", VAnno}
            Dim lbooOk As Boolean = False
            Try
                lobjconexion = New NM.AccesoDatos.AccesoDatosSQLServer(NM.AccesoDatos.GeneradorCadenaConexion.enmBasesDatos.PlanOfisis)
                dtRes = lobjconexion.ObtenerDataTable("USP_PLA_CTSPEND_LISTA_MM", lstrParametros)
                lbooOk = True
            Catch ex As Exception
                dtRes = Nothing
                Throw ex
            Finally
                lobjconexion = Nothing
            End Try
            Return lbooOk
        End Function

        Public Function ListarBancos(ByRef VAnno As Integer, ByRef Vmes As Integer, ByRef dtRes As DataTable) As Boolean
            Dim lobjconexion As NM.AccesoDatos.AccesoDatosSQLServer
            Dim lstrParametros() As String = {"Co_empr", Me.EmpresaCodigo, "Nu_Anno", VAnno, "Nu_Mese", Vmes}
            Dim lbooOk As Boolean = False
            Try
                lobjconexion = New NM.AccesoDatos.AccesoDatosSQLServer(NM.AccesoDatos.GeneradorCadenaConexion.enmBasesDatos.PlanOfisis)
                dtRes = lobjconexion.ObtenerDataTable("USP_PLA_CTSPEND_LISTA_BCO", lstrParametros)
                lbooOk = True
            Catch ex As Exception
                dtRes = Nothing
                Throw ex
            Finally
                lobjconexion = Nothing
            End Try
            Return lbooOk
        End Function

        Public Function ListarMonedas(ByRef VAnno As Integer, ByRef Vmes As Integer, ByRef vBanco As String, ByRef dtRes As DataTable) As Boolean
            Dim lobjconexion As NM.AccesoDatos.AccesoDatosSQLServer
            Dim lstrParametros() As String = {"Co_empr", Me.EmpresaCodigo, "Nu_Anno", VAnno, "Nu_Mese", Vmes, "Co_Banc", vBanco}
            Dim lbooOk As Boolean = False
            Try
                lobjconexion = New NM.AccesoDatos.AccesoDatosSQLServer(NM.AccesoDatos.GeneradorCadenaConexion.enmBasesDatos.PlanOfisis)
                dtRes = lobjconexion.ObtenerDataTable("USP_PLA_CTSPEND_LISTA_MON", lstrParametros)
                lbooOk = True
            Catch ex As Exception
                dtRes = Nothing
                Throw ex
            Finally
                lobjconexion = Nothing
            End Try
            Return lbooOk
        End Function

        Public Function ListarPeriodo(ByRef VAnno As Integer, ByRef Vmes As Integer, ByRef vBanco As String, ByRef vMoneda As String, ByRef dtRes As DataTable) As Boolean
            Dim lobjconexion As NM.AccesoDatos.AccesoDatosSQLServer
            Dim lstrParametros() As String = {"Co_empr", Me.EmpresaCodigo, "Nu_Anno", VAnno, "Nu_Mese", Vmes, "Co_Banc", vBanco, "Co_Mone", vMoneda}
            Dim lbooOk As Boolean = False
            Try
                lobjconexion = New NM.AccesoDatos.AccesoDatosSQLServer(NM.AccesoDatos.GeneradorCadenaConexion.enmBasesDatos.PlanOfisis)
                dtRes = lobjconexion.ObtenerDataTable("USP_PLA_CTSPEND_LISTA_PERIODO", lstrParametros)
                lbooOk = True
            Catch ex As Exception
                dtRes = Nothing
                Throw ex
            Finally
                lobjconexion = Nothing
            End Try
            Return lbooOk
        End Function

        Public Function TotalPeriodo(ByRef VAnno As Integer, ByRef Vmes As Integer, ByRef vBanco As String, ByRef vMoneda As String, ByRef dtRes As DataTable) As Boolean
            Dim lobjconexion As NM.AccesoDatos.AccesoDatosSQLServer
            Dim lstrParametros() As String = {"Co_empr", Me.EmpresaCodigo, "Nu_Anno", VAnno, "Nu_Mese", Vmes, "Co_Banc", vBanco, "Co_Mone", vMoneda}
            Dim lbooOk As Boolean = False
            Try
                lobjconexion = New NM.AccesoDatos.AccesoDatosSQLServer(NM.AccesoDatos.GeneradorCadenaConexion.enmBasesDatos.PlanOfisis)
                dtRes = lobjconexion.ObtenerDataTable("USP_PLA_CTSPEND_TOTAL_PERIODO", lstrParametros)
                lbooOk = True
            Catch ex As Exception
                dtRes = Nothing
                Throw ex
            Finally
                lobjconexion = Nothing
            End Try
            Return lbooOk
        End Function

        Public Function CancelaPeriodo(ByRef VAnno As Integer, ByRef Vmes As Integer, ByRef vBanco As String, ByRef vMoneda As String) As Boolean
            Dim lobjconexion As NM.AccesoDatos.AccesoDatosSQLServer
            Dim lstrParametros() As String = {"Co_empr", Me.EmpresaCodigo, "Co_Usua", Me.UsuarioBD, "Nu_Anno", VAnno, "Nu_Mese", Vmes, "Co_Banc", vBanco, "Co_Mone", vMoneda}
            Dim lbooOk As Boolean = False
            Dim Retorno As Integer
            Try
                lobjconexion = New NM.AccesoDatos.AccesoDatosSQLServer(NM.AccesoDatos.GeneradorCadenaConexion.enmBasesDatos.PlanOfisis)
                Retorno = lobjconexion.EjecutarComando("USP_PLA_CTSPEND_CANCELA_PERIODO", lstrParametros)
                lbooOk = True
            Catch ex As Exception
                lbooOk = False
            Finally
                lobjconexion = Nothing
            End Try
            Return lbooOk
        End Function

        Function ListarDetalle(ByVal VAnno As Integer, ByVal Vmes As Integer, ByVal vBanco As String, ByVal vMoneda As String) As DataTable
            Dim objConexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.PlanOfisis)
            Try
                Dim objParametros() As Object = {"Co_empr", Me.EmpresaCodigo, "Nu_Anno", VAnno, "Nu_Mese", Vmes, "Co_Banc", vBanco, "Co_Mone", vMoneda}
                Return objConexion.ObtenerDataTable("USP_PLA_CTSPEND_LISTA_PERIODO", objParametros)
            Catch ex As Exception
                Throw ex
            End Try
        End Function

    End Class

    ' vacaciones
    Public Class Vacaciones
        Inherits Clases.General
        Sub New(ByVal pstrEmpresa As String, ByVal pstrUsuario As String)
            Me.EmpresaCodigo = pstrEmpresa
            Me.UsuarioBD = pstrUsuario
        End Sub

        Public Function CalculaAdelantoVacaciones(ByRef VAnno As Integer, ByRef Vmes As Integer, ByRef vPlanilla As String, ByRef vFinicial As String, ByRef vFFinal As String) As Boolean
            Dim lobjconexion As NM.AccesoDatos.AccesoDatosSQLServer
            Dim lstrParametros() As String = {"Co_empr", Me.EmpresaCodigo, "Nu_Anno", VAnno, "Nu_Mese", Vmes, "Co_Plan", vPlanilla, "Fe_Inic", vFinicial, "Fe_Fina", vFFinal}
            Dim lbooOk As Boolean = False
            Dim Retorno As Integer
            Try
                lobjconexion = New NM.AccesoDatos.AccesoDatosSQLServer(NM.AccesoDatos.GeneradorCadenaConexion.enmBasesDatos.PlanOfisis)
                Retorno = lobjconexion.EjecutarComando("USP_PLA_VACAPREV_CALC", lstrParametros)
                lbooOk = True
            Catch ex As Exception
                lbooOk = False
            Finally
                lobjconexion = Nothing
            End Try
            Return lbooOk
        End Function
    End Class

    ' SCA
    Public Class SCA
        Inherits Clases.General
        Sub New(ByVal pstrEmpresa As String, ByVal pstrUsuario As String)
            Me.EmpresaCodigo = pstrEmpresa
            Me.UsuarioBD = pstrUsuario

        End Sub

        Public Function ProcesaMarcas(ByRef VPlanilla As String, ByRef vFinicial As String, ByRef vFFinal As String) As Boolean
            Dim lobjconexion As NM.AccesoDatos.AccesoDatosSQLServer
            Dim lstrParametros() As String = {"CODPLA", VPlanilla, "FECHAINI", vFinicial, "FECHAINI", vFFinal}
            Dim lbooOk As Boolean = False
            Dim Retorno As Integer
            Try
                lobjconexion = New NM.AccesoDatos.AccesoDatosSQLServer(NM.AccesoDatos.GeneradorCadenaConexion.enmBasesDatos.AuxiliarNM04)
                Retorno = lobjconexion.EjecutarComando("SCA_GEN_DATAA", lstrParametros)
                lbooOk = True
            Catch ex As Exception
                lbooOk = False
            Finally
                lobjconexion = Nothing
            End Try
            Return lbooOk
        End Function

        Public Function ListarPersonal(ByRef vPlan As String, ByRef Vcod As String, ByVal vNom As String, ByRef dtRes As DataTable) As Boolean
            Dim lobjconexion As NM.AccesoDatos.AccesoDatosSQLServer
            Dim lstrParametros() As String = {"p_var_Empresa", Me.EmpresaCodigo, "p_var_TipoPlanilla", vPlan, "p_var_Codigo", Vcod, "p_var_Nombre", vNom}
            Dim lbooOk As Boolean = False
            Try
                lobjconexion = New NM.AccesoDatos.AccesoDatosSQLServer(NM.AccesoDatos.GeneradorCadenaConexion.enmBasesDatos.PlanOfisis)
                dtRes = lobjconexion.ObtenerDataTable("USP_PLA_PERSONAL_LISTAR1", lstrParametros)
                lbooOk = True
            Catch ex As Exception
                dtRes = Nothing
                Throw ex
            Finally
                lobjconexion = Nothing
            End Try
            Return lbooOk
        End Function
    End Class

    ' Interfase bancaria
    Public Class InterfaseBancos
        Dim lobjconexion As NM.AccesoDatos.AccesoDatosSQLServer
        Private sError As String
        Private sRuta As String

        Public ReadOnly Property clsError() As String
            Get
                clsError = sError
            End Get
        End Property

        ' Ruta donde se graba el archivo
        Public WriteOnly Property sPath() As String
            Set(ByVal sCad As String)
                sRuta = sCad
            End Set

        End Property

        ' Obtenemos cuenta de banco destino, segun moneda
        Function ObtenerCuentaBanco(ByVal strBanco As String, ByVal pstr_TipoMoneda As String) As DataTable
            lobjconexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.PlanOfisis)
            Try
                Dim objParametros() As Object = {"var_CodigoBanco", strBanco, "var_TipoMoneda", pstr_TipoMoneda}
                lobjconexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.PlanOfisis)
                Return lobjconexion.ObtenerDataTable("Usp_Pla_InterBancos_CuentasListar", objParametros)
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        ' Obtenemos rango de trabajadores para el periodo
        Public Function ObtenerRangoTrabajadores(ByRef strPlanilla As String, ByRef Int_anno As Integer, ByRef int_Mes As Integer, ByRef dtRes As DataTable) As Boolean
            Dim lobjconexion As NM.AccesoDatos.AccesoDatosSQLServer
            Dim lstrParametros() As String = {"var_Planilla", strPlanilla, "int_Anno", Int_anno, "int_Mes", int_Mes}
            Dim lbooOk As Boolean = False
            Try
                lobjconexion = New NM.AccesoDatos.AccesoDatosSQLServer(NM.AccesoDatos.GeneradorCadenaConexion.enmBasesDatos.PlanOfisis)
                dtRes = lobjconexion.ObtenerDataTable("Usp_Pla_InterBancos_RangoTrabs", lstrParametros)
                lbooOk = True
            Catch ex As Exception
                dtRes = Nothing
                Throw ex
            Finally
                lobjconexion = Nothing
            End Try
            Return lbooOk
        End Function

        ' BCP?
        Public Sub GenerateFileBCP(ByVal strPlanilla As String, ByVal strBanco As String, ByVal strMoneda As String, ByVal strCuenta As String, ByVal intAnno As Integer, ByVal intMes As Integer, ByVal strCTrabI As String, ByVal strCTrabF As String, ByVal strFecha As String, ByVal strArchivo As String, ByVal numTC As Double, ByVal strCodEmp As String, ByVal strConcepto As String, ByVal strMonedaF As String)
            Dim lobjconexion As New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.PlanOfisis)
            Dim clsDT As New DataTable
            ' Objetos de archivos
            Dim strFile As Stream
            Dim strWriter As StreamWriter
            Dim sFilePath As String = sRuta & "Interfase\" & strArchivo & ".txt"
            Dim ApePaterno As String
            Dim ApeMaterno As String
            Dim strTipDocIde As String
            Dim strNumDocIde As String
            Dim strNroLetra As String
            Dim strFechaVenc As String
            Dim strImporte As String

            Dim intFila As Integer

            Dim lobjParametros() As Object = {"ISCO_EMPR", "01", "ISCO_UNID_INIC", "001", "ISCO_UNID_FINA", "001", "ISCO_PLAN_INIC", strPlanilla, "ISCO_PLAN_FINA", strPlanilla, "ISCO_TRAB_INIC", strCTrabI, "ISCO_TRAB_FINA", strCTrabI, "ISCO_CADE_TRAB", "", "ISCO_BANC_SUEL", strBanco, "ISCO_CNTA_BANC", strCuenta, "INNU_ANNO", intAnno, "INNU_PERI", intMes, "INNU_CORR_PERI", 1, "ISCO_MONE", strMoneda, "ISTI_ABON", "MEN", "ISCO_CPTO_FORM", "", "ISFI_MONE", "N", "INTI_CAMB", numTC, "IDFE_ABON", strFecha, "ISNU_CNTA_SUE2", strCodEmp, "IDFE_MOV1", strFecha, "IDFE_MOV2", strFecha, "ISCO_GRUP", "3000", "ISTI_CESA_MESE", "N", "ISTI_CESA_FMES", "N", "ISST_CNTA_DEST", "N"}


            Try

                sError = ""

                If File.Exists(sFilePath) = True Then
                    File.Delete(sFilePath)
                End If

                clsDT = lobjconexion.ObtenerDataTable("SP_TELE_CRED_I01", lobjParametros)

                '    strFile = File.OpenWrite(sFilePath)
                '    strWriter = New StreamWriter(strFile, System.Text.Encoding.ASCII)

                '    For intFila = 0 To clsDT.Rows.Count - 1


                '        With clsDT.Rows(intFila)

                '            If CType(.Item("TI_NATU_JURI"), String) = "J" Then
                '                NomCliente = LSet(fsLeter(CType(.Item("NombreAcep"), String)), 72)
                '                ApePaterno = LSet("", 24)
                '                ApeMaterno = LSet("", 24)
                '            Else
                '                NomCliente = LSet(fsLeter(CType(.Item("NomCliente"), String)), 72)
                '                ApePaterno = LSet(fsLeter(CType(.Item("ApePaterno"), String)), 24)
                '                ApeMaterno = LSet(fsLeter(CType(.Item("ApeMaterno"), String)), 24)
                '            End If

                '            strTipDocIde = CType(.Item("TipDocIde"), String)

                '            strNumDocIde = LSet(CType(.Item("NumDocIde"), String), 15)
                '            strNroLetra = LSet(fsChar(CType(.Item("NroCedente"), String)), 12)
                '            strFechaVenc = CType(.Item("FechaVenc"), String)
                '            strFechaVenc = Right(strFechaVenc, 2) & Mid(strFechaVenc, 5, 2) & Mid(strFechaVenc, 3, 2)
                '            strImporte = RSet(CType(.Item("Importe"), String), 14)

                '        End With

                '        strWriter.WriteLine(NomCliente & _
                '                            ApePaterno & _
                '                            ApeMaterno & _
                '                            strTipDocIde & _
                '                            strNumDocIde & _
                '                            strNroLetra & _
                '                            strFechaVenc & _
                '                            strImporte)

                '    Next


                '    strWriter.Close()
                '    strWriter = Nothing

                '    Exit Sub

            Catch ex As Exception
                sError = ex.Message

            End Try

        End Sub

        ' Genera archivo mensual BBVA
        Public Sub GeneraFileFMBBVA(ByVal strPlanilla As String, ByVal strBanco As String, ByVal strMoneda As String, ByVal strCuenta As String, ByVal intAnno As Integer, ByVal intMes As Integer, ByVal strCTrabI As String, ByVal strCTrabF As String, ByVal strFecha As String, ByVal strArchivo As String, ByVal numTC As Double, ByVal strCodEmp As String, ByVal strMonedaF As String)
            Dim lobjconexion As New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.PlanOfisis)
            Dim clsDT As New DataTable
            ' Objetos de archivos
            Dim strFile As Stream
            Dim strWriter As StreamWriter
            Dim sFilePath As String = sRuta & "Interfase\" & strArchivo & ".txt"

            Dim strCodAgencia As String
            Dim strCodCtaBco As String
            Dim strCodMoneda As String
            Dim strTotImporte As String
            Dim strFechaProc As String
            Dim strNroTRab As String
            Dim strTipoRegistro As String
            Dim strDOI As String
            Dim strNroDOI As String
            Dim strTiAbono As String
            Dim strNuCtaAbono As String
            Dim strNoTrab As String
            Dim strImporte As String
            Dim strRefe As String
            Dim strBlancos As String = Space(68)

            Dim intFila As Integer

            Dim lobjParametros() As Object = {"ISCO_PLAN_INIC", strPlanilla, "ISCO_TRAB_INIC", strCTrabI, "ISCO_TRAB_FINA", strCTrabF, "ISCO_BANC_SUEL", strBanco, "ISCO_CNTA_BANC", strCuenta, "INNU_ANNO", intAnno, "INNU_PERI", intMes, "INNU_CORR_PERI", 1, "ISCO_MONE", strMoneda, "ISTI_ABON", "MEN", "ISFI_MONE", "S", "INTI_CAMB", numTC, "IDFE_ABON", strFecha, "ISNU_CNTA_SUE2", strCodEmp, "IDFE_MOV1", strFecha, "IDFE_MOV2", strFecha, "ISCO_GRUP", "3000", "ISTI_CESA_MESE", "N", "ISTI_CESA_FMES", "N", "ISST_CNTA_DEST", "N"}
            Try
                sError = ""
                If File.Exists(sFilePath) = True Then
                    File.Delete(sFilePath)
                End If
                clsDT = lobjconexion.ObtenerDataTable("Usp_Pla_InterBancos_GeneraFileFM", lobjParametros)
                strFile = File.OpenWrite(sFilePath)
                strWriter = New StreamWriter(strFile, System.Text.Encoding.ASCII)

                For intFila = 0 To clsDT.Rows.Count - 1
                    With clsDT.Rows(intFila)
                        'Cabecera
                        If intFila = 0 Then
                            strCodAgencia = CType(.Item("CO_AGEN_BANC"), String)
                            strCodCtaBco = CType(.Item("CO_CNTA_BANC"), String)
                            strCodMoneda = LSet(fsChar(CType(.Item("CO_MONE"), String)), 3)
                            strTotImporte = CType(.Item("SU_MONT"), String)
                            strFechaProc = CType(.Item("FE_PAGO"), String)
                            strRefe = LSet(fsChar(CType(.Item("DE_REFE"), String)), 26)
                            strNroTRab = fsNumCad(CType(.Item("NU_REGI"), String))

                            strWriter.WriteLine("7000011" & _
                                                strCodAgencia & _
                                                "636" & _
                                                strCodCtaBco & _
                                                strCodMoneda & _
                                                strTotImporte & _
                                                strNuCtaAbono & _
                                                "F" & _
                                                strFechaProc & _
                                                strRefe & _
                                                strNroTRab & _
                                                "N" & _
                                                 strBlancos)

                        End If
                        'Detalle
                        strTipoRegistro = CType(.Item("TI_CNTA_BANC"), String)
                        strDOI = CType(.Item("TI_DOCU_IDEN"), String)
                        strNroDOI = LSet(fsChar(CType(.Item("NU_DOCU_IDEN"), String)), 12)
                        strTiAbono = CType(.Item("TI_FORM_PAGO"), String)
                        strNuCtaAbono = CType(.Item("NU_CNTA"), String)
                        strNoTrab = LSet(fsLeter(CType(.Item("APE_NOM"), String)), 40)
                        strImporte = fsNumDec(CType(.Item("TO_ABON"), String))
                        strRefe = LSet(fsChar(CType(.Item("DE_REFE"), String)), 141)
                        strWriter.WriteLine(strTipoRegistro & _
                                            strDOI & _
                                            strNroDOI & _
                                            strTiAbono & _
                                            strNuCtaAbono & _
                                            strNoTrab & _
                                            strImporte & _
                                            strRefe)
                    End With

                Next
                strWriter.Close()
                strWriter = Nothing
                Exit Sub

            Catch ex As Exception
                sError = ex.Message

            End Try

        End Sub

        ' Genera archivo mensual/por concepto interBank
        Public Sub GeneraFileBINB(ByVal strPlanilla As String, ByVal strBanco As String, ByVal strMoneda As String, ByVal strCuenta As String, ByVal intAnno As Integer, ByVal intMes As Integer, ByVal strCTrabI As String, ByVal strCTrabF As String, ByVal strFecha As String, ByVal strArchivo As String, ByVal numTC As Double, ByVal strCodCpto As String)
            Dim lobjconexion As New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.PlanOfisis)
            Dim clsDT As New DataTable
            ' Objetos de archivos
            Dim strFile As Stream
            Dim strWriter As StreamWriter
            Dim sFilePath As String = sRuta & "Interfase\" & strArchivo & ".txt"
            Dim strCodRegi As String
            Dim strCodBene As String
            Dim strCodMone As String
            Dim strImpAbon As String
            Dim strIndBnco As String
            Dim strTipAbon As String
            Dim strTipProd As String
            Dim strCodOfic As String
            Dim strNroCnta As String
            Dim strTipPers As String
            Dim strTipDocu As String
            Dim strNroDocu As String
            Dim strNomBene As String
            Dim strBlancos As String = Space(29)
            Dim strBlancos1 As String = Space(23)
            Dim intFila As Integer

            Dim lobjParametros() As Object = {"CO_EMPR", "01", "CO_UNID", "001", "CO_BANC", strBanco, "CO_MONE", strMoneda, "CO_PLAN", strPlanilla, _
             "CO_TRAB_INI", strCTrabI, "CO_TRAB_FIN", strCTrabF, "CO_CPTO", strCodCpto, "NU_ANNO", intAnno, "NU_MESE", intMes, "TI_CAMB", numTC}
            Try
                sError = ""
                If File.Exists(sFilePath) = True Then
                    File.Delete(sFilePath)
                End If
                clsDT = lobjconexion.ObtenerDataTable("Usp_Pla_InterBancos_Interbank_GeneraFile", lobjParametros)
                strFile = File.OpenWrite(sFilePath)
                strWriter = New StreamWriter(strFile, System.Text.Encoding.ASCII)

                For intFila = 0 To clsDT.Rows.Count - 1
                    With clsDT.Rows(intFila)
                        'Detalle
                        strCodRegi = LSet(fsChar(CType(.Item("COD_REG"), String)), 2)
                        strCodBene = LSet(fsChar(CType(.Item("NU_DOCU_IDEN"), String)), 20)
                        strCodMone = LSet(fsChar(CType(.Item("CO_MONE_SUEL"), String)), 2)
                        strImpAbon = fsNumDec(CType(.Item("IM_ABON"), String))
                        strIndBnco = CType(.Item("IND_BNCO"), String)
                        strTipAbon = CType(.Item("TIP_ABON"), String)
                        strTipProd = CType(.Item("TIP_PROD"), String)
                        strCodOfic = CType(.Item("COD_OFIC"), String)
                        strNroCnta = LSet(fsChar(CType(.Item("NU_CNTA_SUEL"), String)), 20)
                        strTipPers = CType(.Item("TIP_PERS"), String)
                        strTipDocu = CType(.Item("TI_DOCU_IDEN"), String)
                        strNroDocu = LSet(fsChar(CType(.Item("NU_DOCU_IDEN"), String)), 15)
                        strNomBene = LSet(fsLeter(CType(.Item("NO_APEL_PATE"), String)), 20) + LSet(fsLeter(CType(.Item("NO_APEL_MATE"), String)), 20) + LSet(fsLeter(CType(.Item("NO_TRAB"), String)), 20)

                        strWriter.WriteLine(strCodRegi & _
                                            strCodBene & _
                                            strBlancos & _
                                            strCodMone & _
                                            strImpAbon & _
                                            strIndBnco & _
                                            strTipAbon & _
                                            strTipProd & _
                                            strCodMone & _
                                            strCodOfic & _
                                            strNroCnta & _
                                            strTipPers & _
                                            strTipDocu & _
                                            strNroDocu & _
                                            strNomBene & _
                                            strBlancos1)
                    End With
                Next
                strWriter.Close()
                strWriter = Nothing
                Exit Sub
            Catch ex As Exception
                sError = ex.Message
            End Try

        End Sub

        ' Genera archivo por concepto BBVA
        Public Sub GeneraFileCOBBVA(ByVal strPlanilla As String, ByVal strBanco As String, ByVal strMoneda As String, ByVal strCuenta As String, ByVal intAnno As Integer, ByVal intMes As Integer, ByVal strCTrabI As String, ByVal strCTrabF As String, ByVal strFecha As String, ByVal strArchivo As String, ByVal numTC As Double, ByVal strCodEmp As String, ByVal strConcepto As String, ByVal strMonedaF As String)
            Dim lobjconexion As New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.PlanOfisis)
            Dim clsDT As New DataTable
            ' Objetos de archivos
            Dim strFile As Stream
            Dim strWriter As StreamWriter
            Dim sFilePath As String = sRuta & "Interfase\" & strArchivo & ".txt"

            Dim strCodAgencia As String
            Dim strCodCtaBco As String
            Dim strCodMoneda As String
            Dim strTotImporte As String
            Dim strFechaProc As String
            Dim strNroTRab As String
            Dim strTipoRegistro As String
            Dim strDOI As String
            Dim strNroDOI As String
            Dim strTiAbono As String
            Dim strNuCtaAbono As String
            Dim strNoTrab As String
            Dim strImporte As String
            Dim strRefe As String
            Dim strIndAviso As String
            Dim strDeMail As String
            Dim strBlancos As String = Space(68)

            Dim intFila As Integer

            Dim lobjParametros() As Object = {"ISCO_PLAN_INIC", strPlanilla, "ISCO_TRAB_INIC", strCTrabI, "ISCO_TRAB_FINA", strCTrabF, "ISCO_BANC_SUEL", strBanco, "ISCO_CNTA_BANC", strCuenta, "INNU_ANNO", intAnno, "INNU_PERI", intMes, "INNU_CORR_PERI", 1, "ISCO_MONE", strMoneda, "ISTI_ABON", "CON", "ISCO_CPTO_FORM", strConcepto, "ISFI_MONE", "S", "INTI_CAMB", numTC, "IDFE_ABON", strFecha, "ISNU_CNTA_SUE2", strCodEmp, "IDFE_MOV1", strFecha, "IDFE_MOV2", strFecha, "ISCO_GRUP", "3000", "ISTI_CESA_MESE", "N", "ISTI_CESA_FMES", "N", "ISST_CNTA_DEST", "N"}
            Try
                sError = ""
                If File.Exists(sFilePath) = True Then
                    File.Delete(sFilePath)
                End If
                clsDT = lobjconexion.ObtenerDataTable("Usp_Pla_InterBancos_GeneraFileCO", lobjParametros)
                strFile = File.OpenWrite(sFilePath)
                strWriter = New StreamWriter(strFile, System.Text.Encoding.ASCII)

                For intFila = 0 To clsDT.Rows.Count - 1
                    With clsDT.Rows(intFila)
                        'Cabecera
                        If intFila = 0 Then
                            strCodAgencia = CType(.Item("CO_AGEN_BANC"), String)
                            strCodCtaBco = CType(.Item("CO_CNTA_BANC"), String)
                            strCodMoneda = LSet(fsChar(CType(.Item("CO_MONE"), String)), 3)
                            strTotImporte = CType(.Item("SU_MONT"), String)
                            strFechaProc = CType(.Item("FE_PAGO"), String)
                            strRefe = LSet(fsChar(CType(.Item("DE_REFE"), String)), 26)
                            strNroTRab = fsNumCad(CType(.Item("NU_REGI"), String))

                            strWriter.WriteLine("7000011" & _
                                                strCodAgencia & _
                                                "636" & _
                                                strCodCtaBco & _
                                                strCodMoneda & _
                                                strTotImporte & _
                                                strNuCtaAbono & _
                                                "F" & _
                                                strFechaProc & _
                                                strRefe & _
                                                strNroTRab & _
                                                "N" & _
                                                 strBlancos)

                        End If
                        'Detalle
                        strTipoRegistro = CType(.Item("TI_CNTA_BANC"), String)
                        strDOI = CType(.Item("TI_DOCU_IDEN"), String)
                        strNroDOI = LSet(fsChar(CType(.Item("NU_DOCU_IDEN"), String)), 12)
                        strTiAbono = CType(.Item("TI_FORM_PAGO"), String)
                        strNuCtaAbono = CType(.Item("NU_CNTA"), String)
                        strNoTrab = LSet(fsLeter(CType(.Item("APE_NOM"), String)), 40)
                        strImporte = fsNumDec(CType(.Item("TO_ABON"), String))
                        strRefe = LSet(fsChar(CType(.Item("DE_REFE"), String)), 141)
                        strWriter.WriteLine(strTipoRegistro & _
                                         strDOI & _
                                         strNroDOI & _
                                         strTiAbono & _
                                         strNuCtaAbono & _
                                         strNoTrab & _
                                         strImporte & _
                                         strRefe)
                    End With

                Next
                strWriter.Close()
                strWriter = Nothing
                Exit Sub

            Catch ex As Exception
                sError = ex.Message
            End Try

        End Sub

        ' Genera archivo quincenal, mensual ScotiaBank
        Public Sub GeneraFileRemSTK(ByVal strCodEmp As String, ByVal strPlanilla As String, ByVal strBanco As String, ByVal intAnno As Integer, ByVal intMes As Integer, _
                 ByVal strCodTrabIni As String, ByVal strCodTrabFin As String, ByVal strFechaAbono As String, strSituacion As String, _
                 strConcepto As String, ByVal strArchivo As String)

            Dim lobjconexion As New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.PlanOfisis)
            Dim strFile As Stream
            Dim strWriter As StreamWriter
            Dim dtbTabla As New DataTable
            Dim strlinea As String = ""
            Dim intFila As Integer = 0
            Dim sFilePath As String = sRuta & "Interfase\" & strArchivo & ".txt"

            dtbTabla = Nothing
            Dim lobjParametros() As Object = {"vch_CodigoEmpresa", strCodEmp, "chr_CodigoPlanilla", strPlanilla, "vch_CodigoBanco", strBanco, "int_Anno", intAnno, "int_Mes", intMes, _
            "vch_CodTrabInicial", strCodTrabIni, "vch_CodTrabFinal", strCodTrabFin, "vch_FechaAbono", strFechaAbono, "chr_Situacion", strSituacion, "vch_ConceptoPago", strConcepto}

            Try
                sError = ""
                If File.Exists(sFilePath) = True Then
                    File.Delete(sFilePath)
                End If
                dtbTabla = lobjconexion.ObtenerDataTable("usp_pla_InterBancos_ScotiaBank_REM_GeneraArchivo", lobjParametros)
                If Not dtbTabla Is Nothing Or dtbTabla.Rows.Count = 0 Then
                    strFile = File.OpenWrite(sFilePath)
                    strWriter = New StreamWriter(strFile, System.Text.Encoding.ASCII)

                    For intFila = 0 To dtbTabla.Rows.Count - 1
                        With dtbTabla.Rows(intFila)
                            strlinea = LSet(fsChar(CType(.Item("vch_linea"), String)), 96)
                            strWriter.WriteLine(strlinea)
                        End With
                    Next
                    strWriter.Close()
                    strWriter = Nothing
                End If
            Catch ex As Exception
                sError = ex.Message
            End Try
        End Sub
        ' Genera archivo quincenal, mensual ScotiaBank
        Public Sub GeneraFileRemBBIF(ByVal strCodEmp As String, ByVal strPlanilla As String, ByVal strBanco As String, ByVal intAnno As Integer, ByVal intMes As Integer, _
                 ByVal strCodTrabIni As String, ByVal strCodTrabFin As String, ByVal strFechaAbono As String, strSituacion As String, _
                 strConcepto As String, ByVal strArchivo As String)

            Dim lobjconexion As New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.PlanOfisis)
            Dim strFile As Stream
            Dim strWriter As StreamWriter
            Dim dtbTabla As New DataTable
            Dim strlinea As String = ""
            Dim intFila As Integer = 0
            Dim sFilePath As String = sRuta & "Interfase\" & strArchivo & ".txt"

            dtbTabla = Nothing
            Dim lobjParametros() As Object = {"vch_CodigoEmpresa", strCodEmp, "chr_CodigoPlanilla", strPlanilla, "vch_CodigoBanco", strBanco, "int_Anno", intAnno, "int_Mes", intMes, _
            "vch_CodTrabInicial", strCodTrabIni, "vch_CodTrabFinal", strCodTrabFin, "vch_FechaAbono", strFechaAbono, "chr_Situacion", strSituacion, "vch_ConceptoPago", strConcepto}

            Try
                sError = ""
                If File.Exists(sFilePath) = True Then
                    File.Delete(sFilePath)
                End If
                dtbTabla = lobjconexion.ObtenerDataTable("usp_pla_InterBancos_ScotiaBank_REM_GeneraArchivo", lobjParametros)
                If Not dtbTabla Is Nothing Or dtbTabla.Rows.Count = 0 Then
                    strFile = File.OpenWrite(sFilePath)
                    strWriter = New StreamWriter(strFile, System.Text.Encoding.ASCII)

                    For intFila = 0 To dtbTabla.Rows.Count - 1
                        With dtbTabla.Rows(intFila)
                            strlinea = LSet(fsChar(CType(.Item("vch_linea"), String)), 96)
                            strWriter.WriteLine(strlinea)
                        End With
                    Next
                    strWriter.Close()
                    strWriter = Nothing
                End If
            Catch ex As Exception
                sError = ex.Message
            End Try
        End Sub

        ' Genera archivo cts ScotiaBank
        Public Sub GeneraFileCtsSTK(ByVal strCodEmp As String, ByVal strPlanilla As String, ByVal strBanco As String, ByVal intAnno As Integer, ByVal intMes As Integer, _
                 ByVal strCodTrabIni As String, ByVal strCodTrabFin As String, ByVal strFechaAbono As String, strSituacion As String, _
                 strConcepto As String, strMoneda As String, numTipoCambio As Double, ByVal strArchivo As String)

            Dim lobjconexion As New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.PlanOfisis)
            Dim strFile As Stream
            Dim strWriter As StreamWriter
            Dim dtbTabla As New DataTable
            Dim strlinea As String = ""
            Dim intFila As Integer = 0
            Dim sFilePath As String = sRuta & "Interfase\" & strArchivo & ".txt"

            dtbTabla = Nothing
            Dim lobjParametros() As Object = {"vch_CodigoEmpresa", strCodEmp, "chr_CodigoPlanilla", strPlanilla, "vch_CodigoBanco", strBanco, "int_Anno", intAnno, "int_Mes", intMes, _
            "vch_CodTrabInicial", strCodTrabIni, "vch_CodTrabFinal", strCodTrabFin, "vch_FechaAbono", strFechaAbono, "chr_Situacion", strSituacion, "vch_ConceptoPago", strConcepto,
            "chr_Moneda", strMoneda, "chr_TipoCambio", numTipoCambio}

            Try
                sError = ""
                If File.Exists(sFilePath) = True Then
                    File.Delete(sFilePath)
                End If
                dtbTabla = lobjconexion.ObtenerDataTable("usp_pla_InterBancos_ScotiaBank_CTS_GeneraArchivo", lobjParametros)
                If Not dtbTabla Is Nothing Or dtbTabla.Rows.Count = 0 Then
                    strFile = File.OpenWrite(sFilePath)
                    strWriter = New StreamWriter(strFile, System.Text.Encoding.ASCII)

                    For intFila = 0 To dtbTabla.Rows.Count - 1
                        With dtbTabla.Rows(intFila)
                            strlinea = LSet(fsChar(CType(.Item("vch_linea"), String)), 96)
                            strWriter.WriteLine(strlinea)
                        End With
                    Next
                    strWriter.Close()
                    strWriter = Nothing
                End If
            Catch ex As Exception
                sError = ex.Message
            End Try
        End Sub

        ' Genera archivo cts BCP
        Public Sub GeneraFileCtsBcp(ByVal strCodEmp As String, ByVal strPlanilla As String, ByVal strBanco As String, ByVal strNumeroCta As String, _
                 ByVal intAnno As Integer, ByVal intMes As Integer, strCorrelativo As String, _
                 ByVal strMoneda As String, ByVal numTipoCambio As Double, ByVal strGrupo As String, _
                 ByVal strSituacion As String, ByVal strCodTrabIni As String, ByVal strCodTrabFin As String, ByVal strArchivo As String)

            Dim lobjconexion As New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.PlanOfisis)
            Dim strFile As Stream
            Dim strWriter As StreamWriter
            Dim dtbTabla As New DataTable
            Dim strlinea As String = ""
            Dim intFila As Integer = 0
            Dim sFilePath As String = sRuta & "Interfase\" & strArchivo & ".txt"

            dtbTabla = Nothing
            Dim lobjParametros() As Object = {"ISCO_EMPR", strCodEmp, "ISCO_PLAN_INIC", strPlanilla, "ISCO_BANC_SUEL", strBanco, "ISCO_CNTA_BANC", strNumeroCta, _
                "INNU_ANNO", intAnno, "INNU_PERI", intMes, "INNU_CORR_PERI", strCorrelativo, _
                "ISCO_MONE", strMoneda, "INTI_CAMB", numTipoCambio, "ISCO_GRUP", strGrupo, _
                "ISTI_SITU_ACTI", strSituacion, "ISCO_TRAB_INIC", strCodTrabIni, "ISCO_TRAB_FINA", strCodTrabFin}

            Try
                sError = ""
                If File.Exists(sFilePath) = True Then
                    File.Delete(sFilePath)
                End If
                dtbTabla = lobjconexion.ObtenerDataTable("usp_pla_InterBancos_Bcp_CTS_GeneraArchivo", lobjParametros)
                If Not dtbTabla Is Nothing Or dtbTabla.Rows.Count = 0 Then
                    strFile = File.OpenWrite(sFilePath)
                    strWriter = New StreamWriter(strFile, System.Text.Encoding.ASCII)

                    For intFila = 0 To dtbTabla.Rows.Count - 1
                        With dtbTabla.Rows(intFila)
                            strlinea = LSet(fsChar(CType(.Item("vch_linea"), String)), 217)
                            strWriter.WriteLine(strlinea)
                        End With
                    Next
                    strWriter.Close()
                    strWriter = Nothing
                End If
            Catch ex As Exception
                sError = ex.Message
            End Try

        End Sub

        Private Function fsNumDec(ByVal sValor As String) As String
            Dim sCade As String = ""
            Dim Pos As Integer
            If InStr(1, CStr(sValor), ".") > 0 Then
                Pos = InStr(1, CStr(sValor), ".") - 1
                sCade = "000000000000000" & Left(sValor, Pos) & Right(sValor, 2)
            End If
            fsNumDec = Right(sCade, 15)
        End Function

        Private Function fsLeter(ByVal sValor As Object) As String
            Dim sCade As String = ""
            Dim sDato As String = ""

            If (CType(sValor, String) = "") Then
                fsLeter = ""
                Exit Function
            End If

            sCade = UCase(CStr(sValor))

            sDato = Replace(sCade, "Ñ", "N")
            sDato = Replace(sDato, "Á", "A")
            sDato = Replace(sDato, "É", "E")
            sDato = Replace(sDato, "Í", "I")
            sDato = Replace(sDato, "Ó", "O")
            sDato = Replace(sDato, "Ú", "U")

            sDato = Replace(sDato, "À", "A")
            sDato = Replace(sDato, "È", "E")
            sDato = Replace(sDato, "Ì", "I")
            sDato = Replace(sDato, "Ò", "O")
            sDato = Replace(sDato, "Ú", "U")
            sDato = Replace(sDato, "´´", "")
            sDato = Replace(sDato, "´", "'")


            fsLeter = CStr(sDato)
        End Function

        Private Function fsNumCad(ByVal sValor As String) As String
            Dim sCade As String = ""
            sCade = "000000" & (sValor)
            fsNumCad = Right(sCade, 6)
        End Function

        Private Function fsChar(ByVal sValor As Object) As String
            Dim sCade As String = ""
            Dim sDato As String = ""

            If (CType(sValor, String) = "") Then
                fsChar = ""
                Exit Function
            End If
            sCade = UCase(CStr(sValor))
            sDato = Replace(sCade, "-", "")
            fsChar = CStr(sDato)
        End Function
    End Class

    ' requisicion personal
    Public Class RequisicionPersonal
        Dim lobjconexion As NM.AccesoDatos.AccesoDatosSQLServer
        Function ObtenerGerencias() As DataTable
            lobjconexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.PlanOfisis)
            Try
                Dim objParametros() As Object = {}
                lobjconexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.PlanOfisis)
                Return lobjconexion.ObtenerDataTable("Usp_Pla_RequisicionPersonal_GerenciasListar", objParametros)
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Function ObtenerAreas() As DataTable
            lobjconexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.PlanOfisis)
            Try
                Dim objParametros() As Object = {}
                lobjconexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.PlanOfisis)
                Return lobjconexion.ObtenerDataTable("Usp_Pla_RequisicionPersonal_AreasListar", objParametros)
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Function ObtenerCCosto() As DataTable
            lobjconexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.PlanOfisis)
            Try
                Dim objParametros() As Object = {}
                lobjconexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.PlanOfisis)
                Return lobjconexion.ObtenerDataTable("Usp_Pla_RequisicionPersonal_CCostoListar", objParametros)
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Function ObtenerPlanilla() As DataTable
            lobjconexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.PlanOfisis)
            Try
                Dim objParametros() As Object = {}
                lobjconexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.PlanOfisis)
                Return lobjconexion.ObtenerDataTable("Usp_Pla_RequisicionPersonal_PLanillaListar", objParametros)
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Function ObtenerPuesto() As DataTable
            lobjconexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.PlanOfisis)
            Try
                Dim objParametros() As Object = {}
                lobjconexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.PlanOfisis)
                Return lobjconexion.ObtenerDataTable("Usp_Pla_RequisicionPersonal_PuestosListar", objParametros)
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Function ObtenerTiCont() As DataTable
            lobjconexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.PlanOfisis)
            Try
                Dim objParametros() As Object = {}
                lobjconexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.PlanOfisis)
                Return lobjconexion.ObtenerDataTable("Usp_Pla_RequisicionPersonal_TiContListar", objParametros)
            Catch ex As Exception
                Throw ex
            End Try
        End Function

    End Class
End Namespace