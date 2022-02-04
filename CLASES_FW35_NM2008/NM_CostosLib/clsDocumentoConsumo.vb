Option Explicit On
Imports System.Data
Imports NM.AccesoDatos

Public Class clsDocumentoConsumo

#Region "-- Variables --"

    Protected Friend mstrError As String = ""

    Protected Friend mstrEmpresa As String = ""
    Protected Friend mintPeriodoAno As Integer = 0
    Protected Friend mintPeriodoMes As Integer = 0

    Protected Friend mstrCodigoPlanta As String = ""
    Protected Friend mstrCodigoRecurso As String = ""
    Protected Friend mstrEstado As String = ""
    Protected Friend mstrUsuario As String = ""


#End Region

    '================================= Definición de constructores ===============================


    '================================= Definición de Propiedades =================================
#Region "-- Propiedades --"

    Public ReadOnly Property clsError() As String
        Get
            Return mstrError
        End Get
    End Property

    Public Property Empresa() As String
        Get
            Empresa = mstrEmpresa
        End Get
        Set(ByVal sCad As String)
            mstrEmpresa = sCad
        End Set
    End Property

    Public Property CodigoPlanta() As String
        Get
            CodigoPlanta = mstrCodigoPlanta
        End Get
        Set(ByVal strCad As String)
            mstrCodigoPlanta = strCad
        End Set
    End Property

    Public Property CodigoRecurso() As String
        Get
            CodigoRecurso = mstrCodigoRecurso
        End Get
        Set(ByVal strCad As String)
            mstrCodigoRecurso = strCad
        End Set
    End Property

    Public Property PeriodoAno() As Integer
        Get
            PeriodoAno = mintPeriodoAno
        End Get
        Set(ByVal intNum As Integer)
            mintPeriodoAno = intNum
        End Set
    End Property

    Public Property PeriodoMes() As Integer
        Get
            PeriodoMes = mintPeriodoMes
        End Get
        Set(ByVal intNum As Integer)
            mintPeriodoMes = intNum
        End Set
    End Property

    Public Property Estado() As String
        Get
            Estado = mstrEstado
        End Get
        Set(ByVal strCad As String)
            mstrEstado = strCad
        End Set
    End Property

    Public Property Usuario() As String
        Get
            Usuario = mstrUsuario
        End Get
        Set(ByVal strCad As String)
            mstrUsuario = strCad
        End Set
    End Property

#End Region

    '=================================== Definicion de metodos  ==================================
#Region "-- Metodos --"

    Public Function ListarDocumentoxPeriodo(ByRef pDT As DataSet) As Boolean
        '*******************************************************************************************
        'Creado por:	  Edwin Poma
        'Fecha     :      01-12-2009
        'Proposito :      retorna el documento de proveedor x periodo para guardar o modificar sus datos.
        '*******************************************************************************************

        Dim blnRpta As Boolean
        Dim Conexion As AccesoDatosSQLServer
        Dim objParametro() As Object = {"pchr_codigoempresa", mstrEmpresa, _
                                        "pint_periodoAno", mintPeriodoAno, _
                                        "ptin_periodomes", mintPeriodoMes, _
                                        "pchr_codigoplanta", mstrCodigoPlanta, _
                                        "pchr_codigorecurso", mstrCodigoRecurso}
        Try
            mstrError = ""
            blnRpta = True
            Conexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.CostosReales)
            pDT = Conexion.ObtenerDataSet("usp_cos_docproveedorxperiodo_listar", objParametro)

        Catch ex As Exception
            blnRpta = False
            mstrError = ex.Message
        Finally
            Conexion = Nothing
        End Try

        Return blnRpta

    End Function

    Public Function GuardarDocumentoxPeriodo(ByVal pdtCabecera As DataTable, ByVal pdtDetalle As DataTable) As Boolean
        '*******************************************************************************************
        'Creado por:	  Edwin Poma
        'Fecha     :      19-11-2009
        'Proposito :      guardar el documento de proveedor x periodo
        '*******************************************************************************************

        Dim blnRpta As Boolean, clsUtilitario As New ComunLib.clsUtil
        Dim Conexion As AccesoDatosSQLServer
        pdtCabecera.TableName = "lista"
        pdtDetalle.TableName = "lista"
        Dim objParametro() As Object = {"pchr_codigoempresa", mstrEmpresa, _
                                        "pint_periodoAno", mintPeriodoAno, _
                                        "ptin_periodomes", mintPeriodoMes, _
                                        "pchr_codigoplanta", mstrCodigoPlanta, _
                                        "pchr_codigorecurso", mstrCodigoRecurso, _
                                        "pvch_usuario", mstrUsuario, _
                                        "pnte_cabecera", clsUtilitario.GeneraXml(pdtCabecera), _
                                        "pnte_detalle", clsUtilitario.GeneraXml(pdtDetalle)}
        Try
            mstrError = ""
            blnRpta = True
            Conexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.CostosReales)
            Conexion.EjecutarComando("usp_cos_docproveedorxperiodo_guardar", objParametro)
        Catch ex As Exception
            blnRpta = False
            mstrError = ex.Message
        Finally
            Conexion = Nothing
        End Try

        Return blnRpta

    End Function

    Public Function CerrarDocumento() As Boolean
        '*******************************************************************************************
        'Creado por:	  Edwin Poma
        'Fecha     :      15-12-2009
        'Proposito :      Ejecuta el proceso de cierre del grupo por trabajadores(3,4,5)
        '*******************************************************************************************

        Dim blnRpta As Boolean
        Dim Conexion As AccesoDatosSQLServer

        Dim objParametro() As Object = {"pchr_codigoempresa", mstrEmpresa, _
                                        "pint_periodoAno", mintPeriodoAno, _
                                        "ptin_periodomes", mintPeriodoMes, _
                                        "pchr_codigoplanta", mstrCodigoPlanta, _
                                        "pchr_codigorecurso", mstrCodigoRecurso, _
                                        "pvch_usuario", mstrUsuario}
        Try
            mstrError = ""
            blnRpta = True
            Conexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.CostosReales)
            Conexion.EjecutarComando("usp_cos_docproveedorxperiodo_cerrar", objParametro)
        Catch ex As Exception
            blnRpta = False
            mstrError = ex.Message
        Finally
            Conexion = Nothing
        End Try

        Return blnRpta

    End Function

    Public Function AbrirDocumento() As Boolean
        '*******************************************************************************************
        'Creado por:	  Edwin Poma
        'Fecha     :      15-12-2009
        'Proposito :      Ejecuta el proceso de abrir del grupo por trabajadores(3,4,5)
        '*******************************************************************************************

        Dim blnRpta As Boolean
        Dim Conexion As AccesoDatosSQLServer

        Dim objParametro() As Object = {"pchr_codigoempresa", mstrEmpresa, _
                                        "pint_periodoAno", mintPeriodoAno, _
                                        "ptin_periodomes", mintPeriodoMes, _
                                        "pchr_codigoplanta", mstrCodigoPlanta, _
                                        "pchr_codigorecurso", mstrCodigoRecurso, _
                                        "pvch_usuario", mstrUsuario}
        Try
            mstrError = ""
            blnRpta = True
            Conexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.CostosReales)
            Conexion.EjecutarComando("usp_cos_docproveedorxperiodo_abrir", objParametro)
        Catch ex As Exception
            blnRpta = False
            mstrError = ex.Message
        Finally
            Conexion = Nothing
        End Try

        Return blnRpta

    End Function

    Public Function ObtenerEsquemas(ByRef pdtCabecera As DataTable, ByRef pdtDetalle As DataTable) As Boolean
        '*******************************************************************************************
        'Creado por:	  Edwin Poma
        'Fecha     :      19-11-2009
        'Proposito :      Devuelve los esquemas necesarios donde se va a guardar los datos en xml
        '*******************************************************************************************
        Dim blnRpta As Boolean

        pdtCabecera = New DataTable
        pdtDetalle = New DataTable
        Try
            With pdtCabecera
                .Columns.Add("dtm_FechaRegistro", GetType(Date))
                .Columns.Add("num_TotalDistribuir", GetType(Double))
                .Columns.Add("num_TotalDistribuir2", GetType(Double))
                .Columns.Add("num_TotalDocumento", GetType(Double))
                .Columns.Add("num_TarifaFijo", GetType(Double))
                .Columns.Add("num_TarifaVariable", GetType(Double))
            End With
            With pdtDetalle
                .Columns.Add("int_Secuencial", GetType(Integer))
                .Columns.Add("vch_Concepto", GetType(String))
                .Columns.Add("num_Valor", GetType(Double))
            End With

            mstrError = ""
            blnRpta = True
        Catch ex As Exception
            blnRpta = False
            mstrError = ex.Message
        Finally

        End Try

        Return blnRpta

    End Function

#End Region

End Class
