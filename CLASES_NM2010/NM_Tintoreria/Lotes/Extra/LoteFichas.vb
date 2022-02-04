Imports NM.AccesoDatos

Namespace Lotes.Extra

    Public Class LoteFichas
#Region "VARIABLES"
        Private mdtbDatos As DataTable
        Private mstrUsuario As String

#End Region

#Region "PROPIEDADES"

        Public Property Datos() As DataTable
            Get
                Return mdtbDatos
            End Get
            Set(ByVal Value As DataTable)
                mdtbDatos = Value
            End Set
        End Property
        Public Property Usuario() As String
            Get
                Return mstrUsuario
            End Get
            Set(ByVal Value As String)
                mstrUsuario = Value
            End Set
        End Property

#End Region

#Region "CONSTRUCTORES"
        Sub New()
            mdtbDatos = EsquemaFichasLote()

        End Sub
#End Region

#Region "   Esquemas"
        Private Function EsquemaFichasLote() As DataTable
            Dim ldtFichas As DataTable
            ldtFichas = New DataTable("Fichas")
            ldtFichas.Columns.Add("codigo_lote", GetType(String))
            ldtFichas.Columns.Add("codigo_orden", GetType(String))
            ldtFichas.Columns.Add("codigo_ficha", GetType(String))
            ldtFichas.Columns.Add("tipo", GetType(String))
            ldtFichas.Columns.Add("secuencia", GetType(Integer))
            ldtFichas.Columns.Add("peso", GetType(Double))
            ldtFichas.Columns.Add("metros", GetType(Double))
            ldtFichas.Columns.Add("velocidad", GetType(Double))
            ldtFichas.Columns.Add("pickup", GetType(Double))
            ldtFichas.Columns.Add("codigo_color", GetType(String))
            ldtFichas.Columns.Add("NOMBRE_COLOR", GetType(String))
            ldtFichas.Columns.Add("codigo_motivo", GetType(String))
            ldtFichas.Columns.Add("secuencia_reproceso", GetType(String))
            ldtFichas.Columns.Add("codigo_tipo_colorante", GetType(String))
            Return ldtFichas
        End Function
#End Region

#Region "METODOS"
        Public Function Buscar(ByVal strCodigoMaquina As String, ByVal strCodigoOperacion As String, _
                            ByVal intFase As Integer, ByVal strColor As String, _
                            ByVal strCodigoFicha As String) As DataTable
            Dim lobjTinto As AccesoDatosSQLServer
            Try
                Dim lstrParametros() As String = {"var_CodigoMaquina", strCodigoMaquina, _
                                "var_CodigoOperacion", strCodigoOperacion, _
                                "sin_CodigoEtapa", intFase, _
                                "var_Color", strColor, _
                                "var_Ficha", strCodigoFicha}
                lobjTinto = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.Tintoreria)
                Return lobjTinto.ObtenerDataTable("usp_TIN_Lote_BuscarFichaLote", lstrParametros)
            Catch ex As Exception
                Throw ex
            End Try
        End Function
#End Region
    End Class

End Namespace
