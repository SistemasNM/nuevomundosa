Option Explicit On
Imports System.Data
Imports NM.AccesoDatos

Public Class clsCierreFinal
    '============================== Definicion de variables interna ==============================

    Dim mstrError As String
    Dim mstrEmpresa As String
    Dim mstrCodigo As String
    Dim mstrEstado As String
    Dim mstrUsuario As String
    Dim mintPeriodoAno As Integer
    Dim mintPeriodoMes As Int16
    Dim mintTipoDatoProd As Integer

    '================================= Definición de constructores ===============================

    Public Sub New()
        mstrError = ""
        mstrEmpresa = ""
        mstrCodigo = ""
        mstrEstado = ""
        mstrUsuario = ""
        mintPeriodoAno = 0
        mintPeriodoMes = 0
        mintTipoDatoProd = 0
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

    Public Property PeriodoAno() As Integer
        Get
            PeriodoAno = mintPeriodoAno
        End Get
        Set(ByVal intVal As Integer)
            mintPeriodoAno = intVal
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

    Public Property TipoDatoProd() As Integer
        Get
            TipoDatoprod = mintTipodatoprod
        End Get
        Set(ByVal intVal As Integer)
            mintTipodatoprod = intVal
        End Set
    End Property

    Public Property PeriodoMes() As Int16
        Get
            PeriodoMes = mintPeriodoMes
        End Get
        Set(ByVal intVal As Int16)
            mintPeriodoMes = intVal
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

    Public Function CargarDatos(ByRef pDT As DataTable) As Boolean
        '*******************************************************************************************
        'Creado por:	  Edwin Poma
        'Fecha     :      2010.10.12
        'Proposito :      retorna un listado con los estados de los cierres finales x grupo
        '*******************************************************************************************

        Dim blnRpta As Boolean
        Dim Conexion As AccesoDatosSQLServer
        Dim objParametro() As Object = {"ptin_tipolista", 1, _
                                     "pchr_codigoempresa", mstrEmpresa, _
                                     "pint_periodoano", mintPeriodoAno, _
                                     "ptin_periodomes", mintPeriodoMes}

        Try
            mstrError = ""
            blnRpta = True
            Conexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.CostosReales)
            pDT = Conexion.ObtenerDataTable("usp_cos_cierrefinal_listar", objParametro)

        Catch ex As Exception
            blnRpta = False
            mstrError = ex.Message
        Finally
            Conexion = Nothing
        End Try

        Return blnRpta
    End Function

    Public Function ValidarUsuario() As Boolean
        '*******************************************************************************************
        'Creado por:	  Edwin Poma
        'Fecha     :      2010.10.13
        'Proposito :      actualiza el estado del proceso
        '*******************************************************************************************

        Dim blnRpta As Boolean
        Dim lobjTabla As New ComunLib.clsParametros, ldtbTabla As DataTable

        Try
            mstrError = ""
            blnRpta = False

            lobjTabla.CodigoTabla = "50"

            ldtbTabla = lobjTabla.ListarDatos()
            For Each ldtrFila In ldtbTabla.Rows
                If ldtrFila("vch_usuario") = mstrUsuario Then
                    blnRpta = True
                    Exit For
                End If
            Next

        Catch ex As Exception
            blnRpta = False
            mstrError = ex.Message
        Finally
            ldtbTabla = Nothing
            lobjTabla = Nothing
        End Try

        Return blnRpta
    End Function

    Public Function ActualizarEstado() As Boolean
        '*******************************************************************************************
        'Creado por:	  Edwin Poma
        'Fecha     :      2010.10.13
        'Proposito :      actualiza el estado del proceso
        '*******************************************************************************************

        Dim blnRpta As Boolean
        Dim Conexion As AccesoDatosSQLServer
        Dim objParametro() As Object = {"pchr_codigoempresa", mstrEmpresa, _
                                     "pint_periodoano", mintPeriodoAno, _
                                     "ptin_periodomes", mintPeriodoMes, _
                                     "ptin_tipodatoprod", mintTipoDatoProd, _
                                     "pvch_usuario", mstrUsuario}
        Try
            mstrError = ""
            blnRpta = True
            Conexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.CostosReales)
            If mstrEstado = "ABI" Then 'si esta abierto entonces debe cerrar
                Conexion.EjecutarComando("usp_cos_datosproduccion_cerrar", objParametro)
            Else
                Conexion.EjecutarComando("usp_cos_datosproduccion_abrir", objParametro)
            End If

        Catch ex As Exception
            blnRpta = False
            mstrError = ex.Message
        Finally
            Conexion = Nothing
        End Try

        Return blnRpta
    End Function
End Class
