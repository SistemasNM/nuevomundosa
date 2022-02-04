Imports NM.AccesoDatos
Namespace Maestros
    Public Class MotivoReproceso
#Region "   Variables"
        Private mstrCodigo As String
        Private mstrNombre As String
        Private mstrUsuario As String
#End Region
#Region "   Constructor"
        Sub New(ByVal strUsuario)
            mstrUsuario = strUsuario
        End Sub
#End Region
#Region "   Propiedades"
        Public Property Codigo() As String
            Get
                Codigo = mstrCodigo
            End Get
            Set(ByVal Value As String)
                mstrCodigo = Value
            End Set
        End Property
        Public Property Nombre() As String
            Get
                Nombre = mstrNombre
            End Get
            Set(ByVal Value As String)
                mstrNombre = Value
            End Set
        End Property
#End Region
#Region "   Metodos"
        Public Function Listar(Optional ByVal pstrCodigo As String = "", Optional ByVal pstrNombre As String = "") As DataTable
            Dim lobjTintoreria As AccesoDatosSQLServer
            Dim lstrParametros() As String = {"var_Codigo", pstrCodigo, _
                                            "var_Nombre", pstrNombre}
            Dim ldtRes As DataTable
            Try
                lobjTintoreria = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.Tintoreria)
                ldtRes = lobjTintoreria.ObtenerDataTable("usp_TIN_MotivoReproceso_Listar", lstrParametros)
            Catch ex As Exception
            Finally
                lobjTintoreria = Nothing
            End Try
            Return ldtRes
        End Function
        Public Function Buscar()
            Dim lobjTinto As AccesoDatosSQLServer
            Dim lstrParametros() As String = {"var_Codigo", mstrCodigo}
            Dim ldtRes As DataTable
            Try
                lobjTinto = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.Tintoreria)
                ldtRes = lobjTinto.ObtenerDataTable("usp_TIN_MotivoReproceso_Buscar", lstrParametros)
                If ldtRes.Rows.Count = 1 Then
                    mstrCodigo = ldtRes.Rows(0).Item("codigo_motivo_reproceso")
                    mstrNombre = ldtRes.Rows(0).Item("nombre_motivo_reproceso")
                Else
                    mstrCodigo = ""
                    mstrNombre = ""
                End If
            Catch ex As Exception

            Finally
                lobjTinto = Nothing
            End Try
        End Function
#End Region
    End Class
End Namespace
