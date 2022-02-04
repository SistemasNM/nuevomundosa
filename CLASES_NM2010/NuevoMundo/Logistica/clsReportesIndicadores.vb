Imports NM.AccesoDatos

Public Class clsReportesIndicadores

#Region "Propiedades"

    Protected Friend varTipoArticulo As String
    Protected Friend varAlmacen As String

    Public Property TipoArticulo() As String
        Get
            TipoArticulo = varTipoArticulo
        End Get
        Set(ByVal value As String)
            varTipoArticulo = value
        End Set
    End Property

    Public Property Almacen() As String
        Get
            Almacen = varAlmacen
        End Get
        Set(ByVal value As String)
            varAlmacen = value
        End Set
    End Property

#End Region

#Region "Metodos"
    Public Function fListarTipoArticulos() As DataTable
        Try
            Return New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.LogisticaOfisis).ObtenerDataSet("usp_LOG_DiasStockxArticulosFiltros").Tables(0)
        Catch ex As Exception
            Throw ex
            Return Nothing
        End Try
    End Function

    Public Function fListarAlmacen() As DataTable
        Try
            Return New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.LogisticaOfisis).ObtenerDataSet("usp_LOG_DiasStockxArticulosFiltros").Tables(1)
        Catch ex As Exception
            Throw ex
            Return Nothing
        End Try
    End Function

#End Region

End Class
