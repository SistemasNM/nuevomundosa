Option Explicit On
Imports System.Data
Imports NM.AccesoDatos

Public Class clsCentroCosto

    '============================== Definicion de variables interna ==============================

    Dim mstrError As String
    Dim mstrEmpresa As String
    Dim mstrCodigo As String
    Dim mstrDescripcion As String
    Dim mintNivel As Integer
    Dim mintTipo As Integer
    Dim mstrEstado As String
    Dim mstrUsuario As String


    '================================= Definición de constructores ===============================

    Public Sub New()
        mstrError = ""
        mstrEmpresa = ""
        mstrCodigo = ""
        mstrDescripcion = ""
        mintNivel = 0
        mintTipo = 0
        mstrEstado = ""
        mstrUsuario = ""
    End Sub

    '================================= Definición de Propiedades =================================

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

    Public Property Codigo() As String
        Get
            Codigo = mstrCodigo
        End Get
        Set(ByVal strCad As String)
            mstrCodigo = strCad
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

    Public Property Nivel() As Integer
        Get
            Nivel = mintNivel
        End Get
        Set(ByVal intVal As Integer)
            mintNivel = intVal
        End Set
    End Property

    Public Property TipoCCosto() As Integer
        Get
            TipoCCosto = mintTipo
        End Get
        Set(ByVal intVal As Integer)
            mintTipo = intVal
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

    '=================================== Definicion de metodos  ==================================

    Public Function Listar(ByRef pDT As DataTable) As Boolean
        '*******************************************************************************************
        'Creado por:	  Carlos Ponce Taype
        'Fecha     :      19-11-2009
        'Proposito :      retorna un listado de C.Costo
        'ptin_tipolista=2--> lista general donde filtra por descripcion o codigo
        '*******************************************************************************************

        Dim blnRpta As Boolean
        Dim Conexion As AccesoDatosSQLServer
        Dim objParametro() As Object = {"ptin_tipolista", 2, _
                                     "pvch_codigocentrocosto", mstrCodigo, _
                                     "pchr_codigoempresa", mstrEmpresa, _
                                     "pvch_descripcion", mstrDescripcion, _
                                     "pint_codigonivel", 0, _
                                     "pint_codigotipo", 0, _
                                     "pvch_estado", ""}

        Try
            mstrError = ""
            blnRpta = True
            Conexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.CostosReales)
            pDT = Conexion.ObtenerDataTable("usp_cos_centrocosto_listar", objParametro)

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
        'Creado por:	  Carlos Ponce Taype
        'Fecha     :      19-11-2009
        'Proposito :      Obtiene los datos de un C.Costo
        '*******************************************************************************************

        Dim Conexion As AccesoDatosSQLServer
        Dim dtbData As DataTable
        Dim objParametro() As Object = {"ptin_tipolista", 1, _
                                        "pvch_codigocentrocosto", mstrCodigo, _
                                        "pchr_codigoempresa", mstrEmpresa, _
                                        "pvch_descripcion", "", _
                                        "pint_codigonivel", 0, _
                                        "pint_codigotipo", 0, _
                                        "pvch_estado", ""}
        Try

            Conexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.CostosReales)
            dtbData = Conexion.ObtenerDataTable("usp_cos_centrocosto_listar", objParametro)

            If (Not (dtbData Is Nothing)) AndAlso dtbData.Rows.Count > 0 Then
                mstrCodigo = dtbData.Rows(0).Item("vch_codigocentrocosto").ToString
                mstrDescripcion = dtbData.Rows(0).Item("vch_descripcion").ToString
                mintNivel = dtbData.Rows(0).Item("int_codigonivel")
                mintTipo = dtbData.Rows(0).Item("int_codigotipo")
                mstrEstado = dtbData.Rows(0).Item("vch_estado").ToString
            Else
                mstrDescripcion = ""
                mintNivel = 0
                mintTipo = ""
                mstrEstado = ""
            End If
        Catch ex As Exception

            mstrError = ex.Message
        Finally
            Conexion = Nothing
        End Try

    End Sub

    Public Function ObtenerDescripcionOfisis(ByRef pDt As DataTable) As Boolean
        '*******************************************************************************************
        'Creado por:	  Edwin Poma Mamani
        'Fecha     :      26-11-2009
        'Proposito :      Obtiene la descripción de la tabla de tmauxi
        '*******************************************************************************************
        Dim blnRpta As Boolean
        Dim Conexion As AccesoDatosSQLServer
        Dim objParametro() As Object = {"ptin_tipolista", 3, _
                                        "pvch_codigocentrocosto", mstrCodigo, _
                                        "pchr_codigoempresa", mstrEmpresa, _
                                        "pvch_descripcion", "", _
                                        "pint_codigonivel", 0, _
                                        "pint_codigotipo", 0, _
                                        "pvch_estado", ""}
        Try
            blnRpta = True
            Conexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.CostosReales)
            pDt = Conexion.ObtenerDataTable("usp_cos_centrocosto_listar", objParametro)

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
        'Creado por:	  Carlos Ponce Taype
        'Fecha     :      19-11-2009
        'Proposito :      Permite registrar un nuevo C.Costo
        '*******************************************************************************************

        Dim blnRpta As Boolean
        Dim Conexion As AccesoDatosSQLServer
        Dim objParametro() As Object = {"ptin_accion", 1, _
                                        "pvch_codigocentrocosto", mstrCodigo, _
                                        "pchr_codigoempresa", mstrEmpresa, _
                                        "pvch_descripcion", mstrDescripcion, _
                                        "pint_codigonivel", mintNivel, _
                                        "pint_codigotipo", mintTipo, _
                                        "pvch_estado", mstrEstado, _
                                        "pvch_usuario", mstrUsuario}
        Try
            mstrError = ""
            blnRpta = True
            Conexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.CostosReales)
            Conexion.EjecutarComando("usp_cos_centrocosto_guardar", objParametro)
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
        'Creado por:	  Carlos Ponce Taype
        'Fecha     :      19-11-2009
        'Proposito :      Permite modificar datos del C.Costo
        '*******************************************************************************************

        Dim blnRpta As Boolean
        Dim Conexion As AccesoDatosSQLServer
        Dim objParametro() As Object = {"ptin_accion", 2, _
                                        "pvch_codigocentrocosto", mstrCodigo, _
                                        "pchr_codigoempresa", mstrEmpresa, _
                                        "pvch_descripcion", mstrDescripcion, _
                                        "pint_codigonivel", mintNivel, _
                                        "pint_codigotipo", mintTipo, _
                                        "pvch_estado", mstrEstado, _
                                        "pvch_usuario", mstrUsuario}

        Try
            mstrError = ""
            blnRpta = True
            Conexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.CostosReales)
            Conexion.EjecutarComando("usp_cos_centrocosto_guardar", objParametro)
        Catch ex As Exception
            blnRpta = False
            mstrError = ex.Message
        Finally
            Conexion = Nothing
        End Try

        Return blnRpta

    End Function

    Public Function Eliminar() As Boolean
        '*******************************************************************************************
        'Creado por:	  Carlos Ponce Taype
        'Fecha     :      19-11-2009
        'Proposito :      Elimina logicamente un C.Costo
        '*******************************************************************************************

        Dim blnRpta As Boolean
        Dim Conexion As AccesoDatosSQLServer
        Dim objParametro() As Object = {"ptin_accion", 3, _
                                        "pvch_codigocentrocosto", mstrCodigo, _
                                        "pchr_codigoempresa", mstrEmpresa, _
                                        "pvch_descripcion", "", _
                                        "pint_codigonivel", 0, _
                                        "pint_codigotipo", 0, _
                                        "pvch_estado", "", _
                                        "pvch_usuario", mstrUsuario}

        Try
            mstrError = ""
            blnRpta = True
            Conexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.CostosReales)
            Conexion.EjecutarComando("usp_cos_centrocosto_guardar", objParametro)
        Catch ex As Exception
            blnRpta = False
            mstrError = ex.Message
        Finally
            Conexion = Nothing
        End Try

        Return blnRpta

    End Function

    Public Function Activar() As Boolean
        '*******************************************************************************************
        'Creado por:	  Carlos Ponce Taype
        'Fecha     :      19-11-2009
        'Proposito :      Elimina logicamente un C.Costo
        '*******************************************************************************************

        Dim blnRpta As Boolean
        Dim Conexion As AccesoDatosSQLServer
        Dim objParametro() As Object = {"ptin_accion", 4, _
                                        "pvch_codigocentrocosto", mstrCodigo, _
                                        "pchr_codigoempresa", mstrEmpresa, _
                                        "pvch_descripcion", "", _
                                        "pint_codigonivel", 0, _
                                        "pint_codigotipo", 0, _
                                        "pvch_estado", "", _
                                        "pvch_usuario", mstrUsuario}

        Try
            mstrError = ""
            blnRpta = True
            Conexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.CostosReales)
            Conexion.EjecutarComando("usp_cos_centrocosto_guardar", objParametro)
        Catch ex As Exception
            blnRpta = False
            mstrError = ex.Message
        Finally
            Conexion = Nothing
        End Try

        Return blnRpta

    End Function

End Class
