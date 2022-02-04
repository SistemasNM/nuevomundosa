Imports NM.AccesoDatos

Namespace Lotes.Generico

    Public Class LoteFichas
#Region "VARIABLES"
        Private mdtbDatos As DataTable
        Private mstrUsuario As String
        Private mdtFichas As DataTable
        Private mdtCabecera As DataTable
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
        Public Property Fichas() As DataTable
            Get
                Fichas = mdtFichas
            End Get
            Set(ByVal Value As DataTable)
                Value.TableName = "Fichas"
                mdtFichas = Value
            End Set
        End Property
        Public Property Cabecera() As DataTable
            Get
                Cabecera = mdtCabecera
            End Get
            Set(ByVal Value As DataTable)
                mdtCabecera = Value
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
            ldtFichas.Columns.Add("codigo_ficha", GetType(String))
            ldtFichas.Columns.Add("tipo", GetType(String))
            ldtFichas.Columns.Add("secuencia", GetType(Integer))
            ldtFichas.Columns.Add("peso", GetType(Double))
            ldtFichas.Columns.Add("metros", GetType(Double))
            ldtFichas.Columns.Add("velocidad", GetType(Double))
            ldtFichas.Columns.Add("pickup", GetType(Double))
            ldtFichas.Columns.Add("codigo_color", GetType(String))
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


        Public Function Listar(ByVal strCodigoMaquina As String, ByVal strCodigoOperacion As String, ByVal intTipo As Integer, ByVal intFase As Integer, ByVal strColor As String) As Boolean
            Dim lobjTinto As AccesoDatosSQLServer
            Dim ldsSet As DataSet
            Try
                Dim lstrParametros() As String = {"var_CodigoMaquina", strCodigoMaquina, _
                                "var_CodigoOperacion", strCodigoOperacion, _
                                "sin_CodigoEtapa", intFase, _
                                "sin_Tipo", intTipo, _
                                "var_Color", strColor}
                lobjTinto = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.Tintoreria)
                ldsSet = lobjTinto.ObtenerDataSet("usp_TIN_Lote_ListarFichasLotes", lstrParametros)
                mdtCabecera = ldsSet.Tables(0)
                mdtFichas = ldsSet.Tables(1)
                Return True
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function ListarExtra(ByVal strCodigoMaquina As String, ByVal strCodigoOperacion As String, ByVal intFase As Integer, ByVal strCodigoLote As String) As Boolean
            Dim lobjTinto As AccesoDatosSQLServer
            Dim ldsSet As DataSet
            Try
                Dim lstrParametros() As String = {"var_CodigoMaquina", strCodigoMaquina, _
                                "var_CodigoOperacion", strCodigoOperacion, _
                                "sin_CodigoEtapa", intFase, _
                                "var_CodigoLote", strCodigoLote}
                lobjTinto = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.Tintoreria)
                ldsSet = lobjTinto.ObtenerDataSet("usp_TIN_Lote_ListarFichasExtra", lstrParametros)
                mdtCabecera = ldsSet.Tables(0)
                mdtFichas = ldsSet.Tables(1)
                Return True
            Catch ex As Exception
                Throw ex
            End Try
        End Function
        '------------------------------------------------------------------------------
        'REQSIS201700008 - DG - INI 
        '------------------------------------------------------------------------------
        Public Function ValidarFichaLoteOperacion(ByVal strCodigoOperacion As String, ByVal strCodigoFicha As String) As DataTable
            Dim lobjTinto As AccesoDatosSQLServer
            Try
                Dim lstrParametros() As String = { _
                                "var_CodigoOperacion", strCodigoOperacion, _
                                "var_CodigoFicha", strCodigoFicha}
                lobjTinto = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.Tintoreria)
                Return lobjTinto.ObtenerDataTable("usp_TIN_Validar_Ficha", lstrParametros)
            Catch ex As Exception
                Throw ex
            End Try
        End Function
        Public Function ValidarRecetaPorSecuenciaRutaArticulo(ByVal strCodigoOrden As String, ByVal strFicha As String, ByVal strCodigoArticulo As String, ByVal strSecuencial As String, ByVal strCodigoOperacion As String) As DataTable
            Dim lobjTinto As AccesoDatosSQLServer
            Try
                Dim lstrParametros() As String = { _
                                "var_CodigoOrden", strCodigoOrden, _
                                "var_Ficha", strFicha, _
                                "var_CodigoArticulo", strCodigoArticulo, _
                                "var_Secuencial", strSecuencial, _
                                "var_CodigoOperacion", strCodigoOperacion}
                lobjTinto = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.Tintoreria)
                Return lobjTinto.ObtenerDataTable("usp_Validar_Ficha_Receta_Ruta_Articulo", lstrParametros)
            Catch ex As Exception
                Throw ex
            End Try
        End Function
        '------------------------------------------------------------------------------
        'REQSIS201700008 - DG - FIN
        '------------------------------------------------------------------------------
        '------------------------------------------------------------------------------
        'REQSIS201700014 - DG - INI
        '------------------------------------------------------------------------------
        Public Function ValidarCantidadLotePorMaquinaOperacion(ByVal strCodigoOperacion As String, ByVal strCodigoMaquina As String, ByVal strCodLote As String) As DataTable
            Dim lobjTinto As AccesoDatosSQLServer
            Try
                Dim lstrParametros() As String = {"var_CodigoOperacion", strCodigoOperacion, "var_CodigoMaquina", strCodigoMaquina, "var_CodigoLote", strCodLote}
                lobjTinto = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.Tintoreria)
                Return lobjTinto.ObtenerDataTable("USP_VALIDAR_CANTIDAD_LOTE_OPERACION_MAQUINA", lstrParametros)
            Catch ex As Exception
                Throw ex
            End Try
        End Function
        '------------------------------------------------------------------------------
        'REQSIS201700014 - DG - FIN
        '------------------------------------------------------------------------------
#End Region
    End Class

End Namespace
