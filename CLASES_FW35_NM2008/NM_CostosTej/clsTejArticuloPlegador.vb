Option Explicit On
Imports System.Data
Imports System.Data.SqlClient
Imports NM.AccesoDatos

Public Class clsTejArticuloPlegador
    Dim mstrError As String
    Dim mstrEmpresa As String
    Dim mstrArticuloFinal As String
    Dim mstrPartidaEngomado As String
    Dim mstrPlegador As String
    Dim mstrUsuario As String

#Region "-- Propiedades --"
    Public ReadOnly Property clsError() As String
        Get
            Return mstrError
        End Get
    End Property

    Public Property Empresa() As String
        Get
            Return mstrEmpresa
        End Get
        Set(ByVal value As String)
            mstrEmpresa = value
        End Set
    End Property

    Public Property ArticuloFinal() As String
        Get
            Return mstrArticuloFinal
        End Get
        Set(ByVal value As String)
            mstrArticuloFinal = value
        End Set
    End Property

    Public Property Plegador() As String
        Get
            Return mstrPlegador
        End Get
        Set(ByVal value As String)
            mstrPlegador = value
        End Set
    End Property

    Public Property PartidaEngomado() As String
        Get
            Return mstrPartidaEngomado
        End Get
        Set(ByVal value As String)
            mstrPartidaEngomado = value
        End Set
    End Property

    Public Property Usuario() As String
        Get
            Return mstrUsuario
        End Get
        Set(ByVal value As String)
            mstrUsuario = value
        End Set
    End Property

#End Region

    ' Verifica si se realizo el calculo del Articulo Plegador del periodo
    Public Function fncConsultarArticuloPlegador(ByRef dstArtPlegador As DataSet, ByVal mstrEmpresa As String, _
                                       ByVal mnumPeriodo As Double, ByVal int_Tipo As Integer) As Boolean

        Dim blnRpta As Boolean
        Dim Conexion As AccesoDatosSQLServer
        Dim objParametros() As Object = {"pchr_Empresa", mstrEmpresa, _
                                         "pnum_Periodo", mnumPeriodo, _
                                         "pint_Tipo", int_Tipo}
        Try
            mstrError = ""
            Conexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.CostosTejeduria)
            dstArtPlegador = Conexion.ObtenerDataSet("usp_costej_InvArticuloPlegador_Consultar", objParametros)
            blnRpta = True
        Catch ex As Exception
            blnRpta = False
            mstrError = ex.Message
        Finally
            Conexion = Nothing
        End Try
        Return blnRpta
    End Function

    ' Importar los articulos x periodo
    Public Function fncImportarArticuloPlegador(ByVal mstrEmpresa As String, ByVal mnumPeriodo As Double, _
                                                ByVal mstrUsuario As String) As Boolean
        Dim blnRpta As Boolean
        Dim Conexion As AccesoDatosSQLServer
        Dim objParametros() As Object = {"pchr_Empresa", mstrEmpresa, _
                                         "pnum_Periodo", mnumPeriodo, _
                                         "pint_Proceso", clsTejEstadoProceso.Proceso.HilTel_ArticuloPlegador, _
                                         "pvch_Usuario", mstrUsuario}
        Try
            mstrError = ""
            Conexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.CostosTejeduria)
            Conexion.EjecutarComando("usp_costej_InvArticuloPlegador_Importar", objParametros)
            blnRpta = True
        Catch ex As Exception
            blnRpta = False
            mstrError = ex.Message
        Finally
            Conexion = Nothing
        End Try
        Return blnRpta
    End Function

    'Modifica datos del articulo
    Public Function fncModificarArticuloPlegador(ByVal mnumPeriodo As Double) As Boolean
        Dim blnRpta As Boolean
        Dim Conexion As AccesoDatosSQLServer
        Dim objParametro() As Object = {"pchr_Empresa", mstrEmpresa, _
                                        "pnum_Periodo", mnumPeriodo, _
                                        "pvch_CodigoArticulo", mstrArticuloFinal, _
                                        "pvch_PartidaEngomado", mstrPartidaEngomado, _
                                        "pvch_CodigoPlegador", mstrPlegador, _
                                        "pvch_Usuario", mstrUsuario}
        Try
            mstrError = ""
            Conexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.CostosTejeduria)
            Conexion.EjecutarComando("usp_costej_InvArticuloPlegador_Actualizar", objParametro)
            blnRpta = True
        Catch ex As Exception
            blnRpta = False
            mstrError = ex.Message
        Finally
            Conexion = Nothing
        End Try
        Return blnRpta
    End Function

End Class
