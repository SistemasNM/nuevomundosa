'Fecha creación: 07-12-2004
Imports NM.AccesoDatos
Namespace NM_Tejeduria
    Public Class NM_TipoTela
#Region "VARIABLE"
        Private _objConexion As AccesoDatosSQLServer
        Private _strUsuario As String
        Private _strCodigo As String
        Private _strDescripcion As String
#End Region

#Region "ANTIGUO"
        Private strCodigo As String
        Private strDescripcion As String
        Private dblMaxPtsxM21ra As Double
        Private dblMaxPtsxM22da As Double
        Private dblMaxPtsxM2Obs As Double
        Private strUsuarioCreacion As String
        Private strUsuarioModificacion As String

        'Constructores
        Sub New()

        End Sub

        Sub New(ByVal strCodigo As String, ByVal strDescripcion As String, ByVal dblMaxPtsxM21ra As Double, ByVal dblMaxPtsxM22da As Double)
            Me.strCodigo = strCodigo
            Me.strDescripcion = strDescripcion
            Me.dblMaxPtsxM21ra = dblMaxPtsxM21ra
            Me.dblMaxPtsxM22da = dblMaxPtsxM22da
        End Sub

        'Métodos de Asignación y Recuperación de Datos
        Public Function setCodigo(ByVal strCodigo As String)
            Me.strCodigo = strCodigo
        End Function
        Public Function getCodigo() As String
            Return strCodigo
        End Function

        Public Function setDescripcion(ByVal strDescripcion As String)
            Me.strDescripcion = strDescripcion
        End Function
        Public Function getDescripcion() As String
            Return strDescripcion
        End Function

        Public Function setMaxPtsxM21ra(ByVal dblMaxPtsxM21ra As Double)
            Me.dblMaxPtsxM21ra = dblMaxPtsxM21ra
        End Function
        Public Function getMaxPtsxM21ra() As Double
            Return dblMaxPtsxM21ra
        End Function

        Public Function setMaxPtsxM2Obs(ByVal dblMaxPtsxM2Obs As Double)
            Me.dblMaxPtsxM2Obs = dblMaxPtsxM2Obs
        End Function
        Public Function getMaxPtsxM2Obs() As Double
            Return dblMaxPtsxM2Obs
        End Function

        Public Function setMaxPtsxM22da(ByVal dblMaxPtsxM22da As Double)
            Me.dblMaxPtsxM22da = dblMaxPtsxM22da
        End Function
        Public Function getMaxPtsxM22da() As Double
            Return strDescripcion
        End Function

        Public Function setUsuarioCreacion(ByVal strUsuarioCreacion As String)
            Me.strUsuarioCreacion = strUsuarioCreacion
        End Function
        Public Function getUsuarioCreacion() As String
            Return strUsuarioCreacion
        End Function

        Public Function setUsuarioModificacion(ByVal strUsuarioModificacion As String)
            Me.strUsuarioModificacion = strUsuarioModificacion
        End Function
        Public Function getUsuarioModificacion() As String
            Return strUsuarioModificacion
        End Function
#End Region

#Region "METODOS Y FUNCIONES"
        Function List() As DataTable
            Try
                _objConexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.Produccion)
                Return _objConexion.ObtenerDataTable("usp_TEJ_TipoTela_Listar")
            Catch ex As Exception
                Throw ex
            End Try

        End Function
#End Region
    End Class

End Namespace
