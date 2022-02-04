Option Explicit On
Imports System.Data
Imports NM.AccesoDatos

Public Class clsFormulaCalculo

    '============================== Definicion de variables interna ==============================

#Region "-- Variables --"

    Dim mstrError As String
    Dim mstrEmpresa As String
    Dim mintCodigo As Integer
    Dim mstrPlanta As String
    Dim mstrRecurso As String
    Dim mintCodigoTabla As Integer
    Dim mintCodigoCampo As Integer
    Dim mstrDescripcion As String
    Dim mstrEstado As String
    Dim mstrUsuario As String
    Dim mstrPseudocodigo As String

#End Region

    '================================= Definición de constructores ===============================

#Region "-- Constructores --"

    Public Sub New()
        mstrError = ""
        mstrEmpresa = ""
        mintCodigo = 0
        mstrPlanta = ""
        mstrRecurso = ""
        mintCodigoTabla = 0
        mintCodigoCampo = 0
        mstrDescripcion = ""
        mstrPseudocodigo = ""
        mstrEstado = ""
        mstrUsuario = ""
    End Sub

#End Region

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
        Set(ByVal strCad As String)
            mstrEmpresa = strCad
        End Set
    End Property

    Public Property Codigo() As Integer
        Get
            Codigo = mintCodigo
        End Get
        Set(ByVal intVal As Integer)
            mintCodigo = intVal
        End Set
    End Property

    Public Property Planta() As String
        Get
            Planta = mstrPlanta
        End Get
        Set(ByVal strCad As String)
            mstrPlanta = strCad
        End Set
    End Property

    Public Property Recurso() As String
        Get
            Recurso = mstrRecurso
        End Get
        Set(ByVal strCad As String)
            mstrRecurso = strCad
        End Set
    End Property

    Public Property CodigoTabla() As Integer
        Get
            CodigoTabla = mintCodigoTabla
        End Get
        Set(ByVal intVal As Integer)
            mintCodigoTabla = intVal
        End Set
    End Property

    Public Property CodigoCampo() As Integer
        Get
            CodigoCampo = mintCodigoCampo
        End Get
        Set(ByVal intVal As Integer)
            mintCodigoCampo = intVal
        End Set
    End Property

    Public Property Descripcion() As String
        Get
            Descripcion = mstrDescripcion
        End Get
        Set(ByVal strCad As String)
            mstrDescripcion = strCad
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

    Public Property Pseudocodigo() As String
        Get
            Pseudocodigo = mstrPseudocodigo
        End Get
        Set(ByVal strCad As String)
            mstrPseudocodigo = strCad
        End Set
    End Property

#End Region

    '=================================== Definicion de metodos  ==================================

    Public Function Listar(ByRef pDT As DataTable) As Boolean
        '*******************************************************************************************
        'Creado por:	  edwin poma
        'Fecha     :      2010.01.14
        'Proposito :      retorna un listado de formulas de calculo
        '*******************************************************************************************

        Dim blnRpta As Boolean
        Dim Conexion As AccesoDatosSQLServer
        Dim objParametro() As Object = {"ptin_tipolista", 2, _
                                     "pint_CodigoFormula", mintCodigo, _
                                     "pchr_CodigoPlanta", mstrPlanta, _
                                     "pvch_CodigoRecurso", mstrRecurso, _
                                     "pint_CodigoTabla", mintCodigoTabla, _
                                     "pint_CodigoCampo", mintCodigoCampo, _
                                     "pvch_Descripcion", mstrDescripcion}
        Try
            mstrError = ""
            blnRpta = True
            Conexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.CostosReales)
            pDT = Conexion.ObtenerDataTable("usp_cos_formulacalculo_listar", objParametro)

        Catch ex As Exception
            blnRpta = False
            mstrError = ex.Message
        Finally
            Conexion = Nothing
        End Try

        Return blnRpta
    End Function

    Public Sub Obtener()
        '*******************************************************************************************
        'Creado por:	  edwin poma
        'Fecha     :      2010.01.15
        'Proposito :      obtiene los datos de la fórmula
        '*******************************************************************************************

        Dim blnRpta As Boolean
        Dim Conexion As AccesoDatosSQLServer
        Dim dtbData As DataTable
        Dim objParametro() As Object = {"ptin_tipolista", 1, _
                                     "pint_CodigoFormula", mintCodigo, _
                                     "pchr_CodigoPlanta", mstrPlanta, _
                                     "pvch_CodigoRecurso", mstrRecurso, _
                                     "pint_CodigoTabla", mintCodigoTabla, _
                                     "pint_CodigoCampo", mintCodigoCampo, _
                                     "pvch_Descripcion", mstrDescripcion}
        Try
            mstrError = ""
            blnRpta = True
            Conexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.CostosReales)
            dtbData = Conexion.ObtenerDataTable("usp_cos_formulacalculo_listar", objParametro)

            If (Not (dtbData Is Nothing)) AndAlso dtbData.Rows.Count > 0 Then
                'mintCodigo = = dtbData.Rows(0).Item("vch_codigocentrocosto").ToString
                'mstrPlanta = dtbData.Rows(0).Item("pchr_CodigoPlanta").ToString
                'mstrRecurso = ""
                mintCodigoTabla = dtbData.Rows(0).Item("int_CodigoTabla").ToString
                mintCodigoCampo = dtbData.Rows(0).Item("int_CodigoCampo").ToString
                mstrDescripcion = dtbData.Rows(0).Item("vch_Descripcion").ToString
                mstrPseudocodigo = dtbData.Rows(0).Item("nvc_Pseudocodigo").ToString
                mstrEstado = dtbData.Rows(0).Item("chr_Estado").ToString
                'mstrUsuario = dtbData.Rows(0).Item("int_CodigoCampo").ToString
            Else
                mstrEmpresa = ""
                mintCodigo = 0
                mintCodigoTabla = 0
                mintCodigoCampo = 0
                mstrDescripcion = ""
                mstrPseudocodigo = ""
                mstrEstado = ""
            End If

        Catch ex As Exception
            blnRpta = False
            mstrError = ex.Message
        Finally
            Conexion = Nothing
        End Try
    End Sub

    Public Function ListarComboDatosFormula(ByRef pDT As DataTable) As Boolean
        '*******************************************************************************************
        'Creado por:	  edwin poma
        'Fecha     :      2010.01.14
        'Proposito :      retorna un listado para los combos de los datos formulas de calculo
        '*******************************************************************************************

        Dim blnRpta As Boolean
        Dim Conexion As AccesoDatosSQLServer
        Dim objParametro() As Object = {"ptin_tipolista", 3, _
                                     "pint_CodigoFormula", mintCodigo, _
                                     "pchr_CodigoPlanta", mstrPlanta, _
                                     "pvch_CodigoRecurso", mstrRecurso, _
                                     "pint_CodigoTabla", mintCodigoTabla, _
                                     "pint_CodigoCampo", mintCodigoCampo, _
                                     "pvch_Descripcion", mstrDescripcion}
        Try
            mstrError = ""
            blnRpta = True
            Conexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.CostosReales)
            pDT = Conexion.ObtenerDataTable("usp_cos_formulacalculo_listar", objParametro)

        Catch ex As Exception
            blnRpta = False
            mstrError = ex.Message
        Finally
            Conexion = Nothing
        End Try

        Return blnRpta
    End Function

    Public Function ListarListboxDatosFormula(ByRef pDT As DataTable) As Boolean
        '*******************************************************************************************
        'Creado por:	  edwin poma
        'Fecha     :      2010.01.14
        'Proposito :      retorna un listado para los combos de los datos formulas de calculo
        '*******************************************************************************************

        Dim blnRpta As Boolean
        Dim Conexion As AccesoDatosSQLServer
        Dim objParametro() As Object = {"ptin_tipolista", 4, _
                                     "pint_CodigoFormula", mintCodigo, _
                                     "pchr_CodigoPlanta", mstrPlanta, _
                                     "pvch_CodigoRecurso", mstrRecurso, _
                                     "pint_CodigoTabla", mintCodigoTabla, _
                                     "pint_CodigoCampo", mintCodigoCampo, _
                                     "pvch_Descripcion", mstrDescripcion}
        Try
            mstrError = ""
            blnRpta = True
            Conexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.CostosReales)
            pDT = Conexion.ObtenerDataTable("usp_cos_formulacalculo_listar", objParametro)

        Catch ex As Exception
            blnRpta = False
            mstrError = ex.Message
        Finally
            Conexion = Nothing
        End Try

        Return blnRpta
    End Function

    Public Function ListarFormulasAgrupadoCampo(ByRef pDT As DataSet) As Boolean
        '*******************************************************************************************
        'Creado por:	  edwin poma
        'Fecha     :      2010.01.18
        'Proposito :      retorna varias datatables para los combos
        '*******************************************************************************************

        Dim blnRpta As Boolean
        Dim Conexion As AccesoDatosSQLServer
        Dim objParametro() As Object = {"ptin_tipolista", 5, _
                                     "pint_CodigoFormula", mintCodigo, _
                                     "pchr_CodigoPlanta", mstrPlanta, _
                                     "pvch_CodigoRecurso", mstrRecurso, _
                                     "pint_CodigoTabla", mintCodigoTabla, _
                                     "pint_CodigoCampo", mintCodigoCampo, _
                                     "pvch_Descripcion", mstrDescripcion}
        Try
            mstrError = ""
            blnRpta = True
            Conexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.CostosReales)
            pDT = Conexion.ObtenerDataSet("usp_cos_formulacalculo_listar", objParametro)

        Catch ex As Exception
            blnRpta = False
            mstrError = ex.Message
        Finally
            Conexion = Nothing
        End Try

        Return blnRpta
    End Function

    Public Function Insertar() As Boolean
        '*******************************************************************************************
        'Creado por:	  edwin poma
        'Fecha     :      2010.01.15
        'Proposito :      Permite registrar una nueva fórmula
        '*******************************************************************************************

        Dim blnRpta As Boolean
        Dim Conexion As AccesoDatosSQLServer
        Dim objParametro() As Object = {"ptin_accion", 1, _
                                        "pint_CodigoFormula", mintCodigo, _
                                        "pchr_CodigoPlanta", mstrPlanta, _
                                        "pvch_CodigoRecurso", mstrRecurso, _
                                        "pint_CodigoTabla", mintCodigoTabla, _
                                        "pint_CodigoCampo", mintCodigoCampo, _
                                        "pvch_Descripcion", mstrDescripcion, _
                                        "pnvc_Pseudocodigo", mstrPseudocodigo, _
                                        "pvch_usuario", mstrUsuario, _
                                        "pchr_Estado", mstrEstado}
        Try
            mstrError = ""
            blnRpta = True
            Conexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.CostosReales)
            Conexion.EjecutarComando("usp_cos_formulacalculo_guardar", objParametro)
        Catch ex As Exception
            blnRpta = False
            mstrError = ex.Message
        Finally
            Conexion = Nothing
        End Try

        Return blnRpta

    End Function

    Public Function Modificar() As Boolean
        '*******************************************************************************************
        'Creado por:	  edwin poma
        'Fecha     :      2010.01.15
        'Proposito :      Permite modificar datos de la fórmula
        '*******************************************************************************************

        Dim blnRpta As Boolean
        Dim Conexion As AccesoDatosSQLServer
        Dim objParametro() As Object = {"ptin_accion", 2, _
                                        "pint_CodigoFormula", mintCodigo, _
                                        "pchr_CodigoPlanta", mstrPlanta, _
                                        "pvch_CodigoRecurso", mstrRecurso, _
                                        "pint_CodigoTabla", mintCodigoTabla, _
                                        "pint_CodigoCampo", mintCodigoCampo, _
                                        "pvch_Descripcion", mstrDescripcion, _
                                        "pnvc_Pseudocodigo", mstrPseudocodigo, _
                                        "pvch_usuario", mstrUsuario, _
                                        "pchr_Estado", mstrEstado}
        Try
            mstrError = ""
            blnRpta = True
            Conexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.CostosReales)
            Conexion.EjecutarComando("usp_cos_formulacalculo_guardar", objParametro)
        Catch ex As Exception
            blnRpta = False
            mstrError = ex.Message
        Finally
            Conexion = Nothing
        End Try

        Return blnRpta

    End Function

End Class
