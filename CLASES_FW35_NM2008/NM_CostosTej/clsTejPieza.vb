Option Explicit On
Imports System.Data
Imports NM.AccesoDatos

Public Class clsTejPieza

#Region "-- Variables --"
    Dim mConexion As AccesoDatosSQLServer
    Dim mstrError As String

    Dim mstrCodigoEmpresa As String = ""
    Dim mstrPeriodo As Double
    Dim mstrCodigoPieza As String = ""
    Dim mstrTipoPieza As String = ""
    Dim mstrCodigoArticulo As String = ""
    Dim mstrCodigoTelar As String = ""
    Dim mdblCantidadMetros As Double
    Dim mstrClasificacion As String = ""
    Dim mstrCodigoPlegador As String = ""
#End Region

#Region "-- Propiedades --"

    Public Property CodigoEmpresa() As String
        Get
            Return mstrCodigoEmpresa
        End Get
        Set(ByVal value As String)
            mstrCodigoEmpresa = value
        End Set
    End Property

    Public Property Periodo() As Double
        Get
            Return mstrPeriodo
        End Get
        Set(ByVal value As Double)
            mstrPeriodo = value
        End Set
    End Property

    Public Property CodigoPieza() As String
        Get
            Return mstrCodigoPieza
        End Get
        Set(ByVal value As String)
            mstrCodigoPieza = value
        End Set
    End Property

    Public Property TipoPieza() As String
        Get
            Return mstrTipoPieza
        End Get
        Set(ByVal value As String)
            mstrTipoPieza = value
        End Set
    End Property

    Public Property CodigoArticulo() As String
        Get
            Return mstrCodigoArticulo
        End Get
        Set(ByVal value As String)
            mstrCodigoArticulo = value
        End Set
    End Property

    Public Property CodigoTelar() As String
        Get
            Return mstrCodigoTelar
        End Get
        Set(ByVal value As String)
            mstrCodigoTelar = value
        End Set
    End Property

    Public Property CantidadMetros() As Double
        Get
            Return mdblCantidadMetros
        End Get
        Set(ByVal value As Double)
            mdblCantidadMetros = value
        End Set
    End Property

    Public Property Clasificacion() As String
        Get
            Return mstrClasificacion
        End Get
        Set(ByVal value As String)
            mstrClasificacion = value
        End Set
    End Property

    Public Property CodigoPlegador() As String
        Get
            Return mstrCodigoPlegador
        End Get
        Set(ByVal value As String)
            mstrCodigoPlegador = value
        End Set
    End Property

    Public ReadOnly Property clsError() As String
        Get
            Return mstrError
        End Get
    End Property
#End Region

#Region "-- Metodos --"
    Public Function fncActualizarPieza(ByVal mstrEmpresa As String, ByVal mnumPeriodo As Double, _
                                     ByVal mstrUsuario As String, ByVal mstrCodigoTelar As String, _
                                     ByVal mstrCodigoPieza As String) As Boolean
        '-----------------------------------------------------
        'Creado por:	  Alexander Torres Cardenas
        'Fecha     :      23-08-2011
        'Proposito :      Actualizar datos de la Pieza
        '-----------------------------------------------------
        Dim blnRpta As Boolean
        Dim Conexion As AccesoDatosSQLServer
        Dim objParametro() As Object = {"pchr_Empresa", mstrEmpresa, _
                                        "pnum_Periodo", mnumPeriodo, _
                                        "pvch_Usuario", mstrCodigoArticulo, _
                                        "pvch_CodigoTelar", mstrCodigoTelar, _
                                        "pvch_CodigoPieza", mstrCodigoPieza}
        Try
            mstrError = ""
            Conexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.CostosTejeduria)
            Conexion.EjecutarComando("usp_costej_Pieza_Actualizar", objParametro)
            blnRpta = True
        Catch ex As Exception
            blnRpta = False
            mstrError = ex.Message
        Finally
            Conexion = Nothing
        End Try
        Return blnRpta
    End Function
#End Region
End Class
