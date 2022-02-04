Imports NM.AccesoDatos
Imports NM_General
Imports System.Data.OleDb
Namespace FichaCosto
    Public Class Ficha

#Region "Declaracion de variables"
        Private lstr_CodigoArticuloLargo As String
        Private lstr_CodigoArticuloCorto As String
        Private lstr_CodigoUrdimbre As String
        Private lstr_CodigoEngomado As String
        Private lstr_TipoEngomado As String
        Private lstr_Usuario As String

        Private lobj_Conexion As AccesoDatosSQLServer
#End Region

#Region "Declaracion de Propiedades"
        Public Property CodigoArticuloLargo() As String
            Get
                CodigoArticuloLargo = lstr_CodigoArticuloLargo
            End Get
            Set(ByVal Value As String)
                lstr_CodigoArticuloLargo = Value
            End Set
        End Property

        Public Property CodigoArticuloCorto() As String
            Get
                CodigoArticuloCorto = lstr_CodigoArticuloCorto
            End Get
            Set(ByVal Value As String)
                lstr_CodigoArticuloCorto = Value
            End Set
        End Property

        Public Property CodigoUrdimbre() As String
            Get
                CodigoUrdimbre = lstr_CodigoUrdimbre
            End Get
            Set(ByVal Value As String)
                lstr_CodigoUrdimbre = Value
            End Set
        End Property

        Public Property CodigoEngomado() As String
            Get
                CodigoEngomado = lstr_CodigoEngomado
            End Get
            Set(ByVal Value As String)
                CodigoEngomado = Value
            End Set
        End Property

        Public Property TipoEngomado() As String
            Get
                TipoEngomado = lstr_TipoEngomado
            End Get
            Set(ByVal Value As String)
                lstr_TipoEngomado = Value
            End Set
        End Property

        Public Property Usuario() As String
            Get
                Usuario = lstr_Usuario
            End Get
            Set(ByVal Value As String)
                lstr_Usuario = Value
            End Set
        End Property

#End Region

#Region "Constructores"
        Sub New()
            lstr_CodigoArticuloLargo = ""
            lstr_CodigoArticuloCorto = ""
            lstr_CodigoUrdimbre = ""
            lstr_CodigoEngomado = ""
            lstr_TipoEngomado = ""
            lstr_Usuario = ""
            lobj_Conexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.FichaCosto)
        End Sub

        Protected Overrides Sub Finalize()
            MyBase.Finalize()
            lobj_Conexion = Nothing
        End Sub

#End Region

#Region "Procedimientos y Funciones"
        Public Function ObtenerCredencialesReportingService() As System.Net.NetworkCredential
            Dim creds As New System.Net.NetworkCredential
            'System.Net.CredentialCache.DefaultCredentials
            creds.Domain = "NUEVOMUNDOSA"
            creds.UserName = "nmsic"
            creds.Password = "Asmrp.159"

            Return creds

        End Function
        Public Function ufn_FichaCostosMasiva_ListarFechas() As DataTable
            Dim lobjCon As NM.AccesoDatos.AccesoDatosSQLServer
            Try
                lobjCon = New NM.AccesoDatos.AccesoDatosSQLServer(NM.AccesoDatos.GeneradorCadenaConexion.enmBasesDatos.FichaCosto)
                Return lobjCon.ObtenerDataTable("usp_cos_FichaCostosMasiva_ListarFechas")
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function ufn_HacerXMLConstrucciones(ByVal pstrArticulo As String) As DataTable
            Dim lobjCon As NM.AccesoDatos.AccesoDatosSQLServer
            Dim lstrParametros() As String = {"var_CodigoArticuloLargo", pstrArticulo}
            Try
                lobjCon = New NM.AccesoDatos.AccesoDatosSQLServer(NM.AccesoDatos.GeneradorCadenaConexion.enmBasesDatos.Produccion)
                Return lobjCon.ObtenerDataTable("usp_cos_FichaCostos_Construcciones", lstrParametros)
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function ufn_ConsultarContribucion(ByVal strXMLDatos As String) As DataTable
            Dim lobjCon As NM.AccesoDatos.AccesoDatosSQLServer

            Dim lstrParametros() As String = {"ntx_Articulos", strXMLDatos}
            Try
                lobjCon = New NM.AccesoDatos.AccesoDatosSQLServer(NM.AccesoDatos.GeneradorCadenaConexion.enmBasesDatos.FichaCosto)
                Return lobjCon.ObtenerDataTable("USP_COS_FICHACOSTOS_CONTRIBUCION", lstrParametros)
            Catch ex As Exception
                Throw ex
            Finally
                lobjCon = Nothing
            End Try
        End Function

        Public Function VerificaPreliminar(ByVal dstDatos As DataSet) As DataTable
            Try
                Dim dtbRetorno As New DataTable, dtrItem As DataRow
                dtbRetorno.Columns.Add("var_Modulo", GetType(String))
                dtbRetorno.Columns.Add("var_Descripcion", GetType(String))

                'CARGANDO ARTICULO
                For Each dtrDatos As DataRow In dstDatos.Tables("Articulo").Rows
                    If dtrDatos("var_CodigoUrdimbre") = "" Then
                        dtrItem = dtbRetorno.NewRow : dtrItem("var_Modulo") = "ARTICULO" : dtrItem("var_Descripcion") = "Falta Codigo de Urdimbre" : dtbRetorno.LoadDataRow(dtrItem.ItemArray, True)
                    End If

                    If dtrDatos("num_EncogimientoUrdimbre") Is Nothing OrElse IsDBNull(dtrDatos("num_EncogimientoUrdimbre")) = True OrElse CType(dtrDatos("num_EncogimientoUrdimbre"), Double) <= 0 Then
                        dtrItem = dtbRetorno.NewRow : dtrItem("var_Modulo") = "ARTICULO" : dtrItem("var_Descripcion") = "Falta Encogimiento de Urdimbre" : dtbRetorno.LoadDataRow(dtrItem.ItemArray, True)
                    End If

                    If dtrDatos("num_EncogimientoTrama") Is Nothing OrElse IsDBNull(dtrDatos("num_EncogimientoTrama")) = True OrElse CType(dtrDatos("num_EncogimientoTrama"), Double) <= 0 Then
                        dtrItem = dtbRetorno.NewRow : dtrItem("var_Modulo") = "ARTICULO" : dtrItem("var_Descripcion") = "Falta Encogimiento de Trama" : dtbRetorno.LoadDataRow(dtrItem.ItemArray, True)
                    End If

                    If dtrDatos("int_AnchoCrudo") Is Nothing OrElse IsDBNull(dtrDatos("int_AnchoCrudo")) = True OrElse CType(dtrDatos("int_AnchoCrudo"), Double) <= 0 Then
                        dtrItem = dtbRetorno.NewRow : dtrItem("var_Modulo") = "ARTICULO" : dtrItem("var_Descripcion") = "Falta Ancho Crudo" : dtbRetorno.LoadDataRow(dtrItem.ItemArray, True)
                    End If

                    If dtrDatos("num_EficienciaTeorica") Is Nothing OrElse IsDBNull(dtrDatos("num_EficienciaTeorica")) = True OrElse CType(dtrDatos("num_EficienciaTeorica"), Double) <= 0 Then
                        dtrItem = dtbRetorno.NewRow : dtrItem("var_Modulo") = "ARTICULO" : dtrItem("var_Descripcion") = "Falta Eficiencia Teorica" : dtbRetorno.LoadDataRow(dtrItem.ItemArray, True)
                    End If

                    If dtrDatos("num_EficienciaReal") Is Nothing OrElse IsDBNull(dtrDatos("num_EficienciaReal")) = True OrElse CType(dtrDatos("num_EficienciaReal"), Double) <= 0 Then
                        dtrItem = dtbRetorno.NewRow : dtrItem("var_Modulo") = "ARTICULO" : dtrItem("var_Descripcion") = "Falta Eficiencia Real" : dtbRetorno.LoadDataRow(dtrItem.ItemArray, True)
                    End If

                    If dtrDatos("num_MermaTejeduria") Is Nothing OrElse IsDBNull(dtrDatos("num_MermaTejeduria")) = True OrElse CType(dtrDatos("num_MermaTejeduria"), Double) <= 0 Then
                        dtrItem = dtbRetorno.NewRow : dtrItem("var_Modulo") = "ARTICULO" : dtrItem("var_Descripcion") = "Falta Merma de Tejeduria" : dtbRetorno.LoadDataRow(dtrItem.ItemArray, True)
                    End If

                    If dtrDatos("num_VariacionDimensional") Is Nothing OrElse CType(dtrDatos("num_VariacionDimensional"), Double) <= 0 Then
                        dtrItem = dtbRetorno.NewRow : dtrItem("var_Modulo") = "ARTICULO" : dtrItem("var_Descripcion") = "Falta Variacion Dimensional" : dtbRetorno.LoadDataRow(dtrItem.ItemArray, True)
                    End If

                    If dtrDatos("num_VelocidadTeorica") Is Nothing OrElse IsDBNull(dtrDatos("num_VelocidadTeorica")) = True OrElse CType(dtrDatos("num_VelocidadTeorica"), Double) <= 0 Then
                        dtrItem = dtbRetorno.NewRow : dtrItem("var_Modulo") = "ARTICULO" : dtrItem("var_Descripcion") = "Falta Velocidad Teorica" : dtbRetorno.LoadDataRow(dtrItem.ItemArray, True)
                    End If

                    If dtrDatos("num_GastoOperativo") Is Nothing OrElse IsDBNull(dtrDatos("num_GastoOperativo")) = True OrElse CType(dtrDatos("num_GastoOperativo"), Double) <= 0 Then
                        dtrItem = dtbRetorno.NewRow : dtrItem("var_Modulo") = "ARTICULO" : dtrItem("var_Descripcion") = "Falta % Gastos Operativos" : dtbRetorno.LoadDataRow(dtrItem.ItemArray, True)
                    End If

                    If dtrDatos("num_PorcentajeCompensacion") Is Nothing OrElse IsDBNull(dtrDatos("num_PorcentajeCompensacion")) = True OrElse IsDBNull(dtrDatos("num_PorcentajeCompensacion")) = True OrElse CType(dtrDatos("num_PorcentajeCompensacion"), Double) <= 0 Then
                        dtrItem = dtbRetorno.NewRow : dtrItem("var_Modulo") = "ARTICULO" : dtrItem("var_Descripcion") = "Falta Porcentaje Compensacion por Calidad" : dtbRetorno.LoadDataRow(dtrItem.ItemArray, True)
                    End If

                    If dtrDatos("num_TipoCambio") Is Nothing OrElse IsDBNull(dtrDatos("num_TipoCambio")) = True OrElse CType(dtrDatos("num_TipoCambio"), Double) <= 0 Then
                        dtrItem = dtbRetorno.NewRow : dtrItem("var_Modulo") = "ARTICULO" : dtrItem("var_Descripcion") = "Falta Tipo de Cambio" : dtbRetorno.LoadDataRow(dtrItem.ItemArray, True)
                    End If

                    If dtrDatos("int_NumeroTelas") Is Nothing OrElse IsDBNull(dtrDatos("int_NumeroTelas")) = True OrElse CType(dtrDatos("int_NumeroTelas"), Double) <= 0 Then
                        dtrItem = dtbRetorno.NewRow : dtrItem("var_Modulo") = "ARTICULO" : dtrItem("var_Descripcion") = "Falta Numero de Telas" : dtbRetorno.LoadDataRow(dtrItem.ItemArray, True)
                    End If

                    If dtrDatos("num_AnchoPeine") Is Nothing OrElse IsDBNull(dtrDatos("num_AnchoPeine")) = True OrElse CType(dtrDatos("num_AnchoPeine"), Double) <= 0 Then
                        dtrItem = dtbRetorno.NewRow : dtrItem("var_Modulo") = "ARTICULO" : dtrItem("var_Descripcion") = "Falta Ancho del peine" : dtbRetorno.LoadDataRow(dtrItem.ItemArray, True)
                    End If

                    dtbRetorno.AcceptChanges()

                Next

                If dstDatos.Tables("Urdimbre").Rows.Count <= 0 Then
                    dtrItem = dtbRetorno.NewRow : dtrItem("var_Modulo") = "URDIMBRE" : dtrItem("var_Descripcion") = "No existen hilos en urdimbre" : dtbRetorno.LoadDataRow(dtrItem.ItemArray, True)
                End If

                'CARGANDO URDIMBRE
                For Each dtrDatos As DataRow In dstDatos.Tables("Urdimbre").Rows

                    If dtrDatos("int_NumeroHilos") Is Nothing OrElse IsDBNull(dtrDatos("int_NumeroHilos")) = True OrElse CType(dtrDatos("int_NumeroHilos"), Double) <= 0 Then
                        dtrItem = dtbRetorno.NewRow : dtrItem("var_Modulo") = "URDIMBRE" : dtrItem("var_Descripcion") = "Falta Numero de Hilos en " & dtrDatos("var_CodigoHilo") : dtbRetorno.LoadDataRow(dtrItem.ItemArray, True)
                    End If

                    If dtrDatos("num_TituloReal") Is Nothing OrElse IsDBNull(dtrDatos("num_TituloReal")) = True OrElse CType(dtrDatos("num_TituloReal"), Double) <= 0 Then
                        dtrItem = dtbRetorno.NewRow : dtrItem("var_Modulo") = "URDIMBRE" : dtrItem("var_Descripcion") = "Falta Titulo real en " & dtrDatos("var_CodigoHilo") : dtbRetorno.LoadDataRow(dtrItem.ItemArray, True)
                    End If

                    If dtrDatos("num_GramosMetro") Is Nothing OrElse IsDBNull(dtrDatos("num_GramosMetro")) = True OrElse CType(dtrDatos("num_GramosMetro"), Double) <= 0 Then
                        dtrItem = dtbRetorno.NewRow : dtrItem("var_Modulo") = "URDIMBRE" : dtrItem("var_Descripcion") = "Falta Gramos/Metro en " & dtrDatos("var_CodigoHilo") : dtbRetorno.LoadDataRow(dtrItem.ItemArray, True)
                    End If

                    If dtrDatos("num_Velocidad") Is Nothing OrElse IsDBNull(dtrDatos("num_Velocidad")) = True OrElse CType(dtrDatos("num_Velocidad"), Double) <= 0 Then
                        dtrItem = dtbRetorno.NewRow : dtrItem("var_Modulo") = "URDIMBRE" : dtrItem("var_Descripcion") = "Falta Velocidad en " & dtrDatos("var_CodigoHilo") : dtbRetorno.LoadDataRow(dtrItem.ItemArray, True)
                    End If

                Next
                dtbRetorno.AcceptChanges()

                If dstDatos.Tables("Trama").Rows.Count <= 0 Then
                    dtrItem = dtbRetorno.NewRow : dtrItem("var_Modulo") = "URDIMBRE" : dtrItem("var_Descripcion") = "No existen hilos en trama" : dtbRetorno.LoadDataRow(dtrItem.ItemArray, True)
                End If
                'CARGANDO TRAMA
                For Each dtrDatos As DataRow In dstDatos.Tables("Trama").Rows

                    If dtrDatos("num_AnchoCrudo") Is Nothing OrElse IsDBNull(dtrDatos("num_AnchoCrudo")) = True OrElse CType(dtrDatos("num_AnchoCrudo"), Double) <= 0 Then
                        dtrItem = dtbRetorno.NewRow : dtrItem("var_Modulo") = "URDIMBRE" : dtrItem("var_Descripcion") = "Falta Ancho Crudo en " & dtrDatos("var_CodigoHilo") : dtbRetorno.LoadDataRow(dtrItem.ItemArray, True)
                    End If

                    If dtrDatos("int_NumeroHilos") Is Nothing OrElse IsDBNull(dtrDatos("int_NumeroHilos")) = True OrElse CType(dtrDatos("int_NumeroHilos"), Double) <= 0 Then
                        dtrItem = dtbRetorno.NewRow : dtrItem("var_Modulo") = "URDIMBRE" : dtrItem("var_Descripcion") = "Falta Numero de Hilos en " & dtrDatos("var_CodigoHilo") : dtbRetorno.LoadDataRow(dtrItem.ItemArray, True)
                    End If

                    If dtrDatos("num_TituloReal") Is Nothing OrElse IsDBNull(dtrDatos("num_TituloReal")) = True OrElse CType(dtrDatos("num_TituloReal"), Double) <= 0 Then
                        dtrItem = dtbRetorno.NewRow : dtrItem("var_Modulo") = "URDIMBRE" : dtrItem("var_Descripcion") = "Falta Titulo real en " & dtrDatos("var_CodigoHilo") : dtbRetorno.LoadDataRow(dtrItem.ItemArray, True)
                    End If

                    If dtrDatos("num_GramosMetro") Is Nothing OrElse IsDBNull(dtrDatos("num_GramosMetro")) = True OrElse CType(dtrDatos("num_GramosMetro"), Double) <= 0 Then
                        dtrItem = dtbRetorno.NewRow : dtrItem("var_Modulo") = "URDIMBRE" : dtrItem("var_Descripcion") = "Falta Gramos/Metro en " & dtrDatos("var_CodigoHilo") : dtbRetorno.LoadDataRow(dtrItem.ItemArray, True)
                    End If
                Next
                dtbRetorno.AcceptChanges()

                'CARGANDO ENGOMADO
                For Each dtrDatos As DataRow In dstDatos.Tables("Engomado").Rows
                    If dtrDatos("var_CodigoEngomado") Is Nothing OrElse IsDBNull(dtrDatos("var_CodigoEngomado")) = True OrElse dtrDatos("var_CodigoEngomado") = "" Then
                        dtrItem = dtbRetorno.NewRow : dtrItem("var_Modulo") = "ENGOMADO" : dtrItem("var_Descripcion") = "Falta Codigo de Engomado" : dtbRetorno.LoadDataRow(dtrItem.ItemArray, True)
                    End If

                    If dtrDatos("var_CodigoTipo") Is Nothing OrElse IsDBNull(dtrDatos("var_CodigoTipo")) = True OrElse dtrDatos("var_CodigoTipo") = "" Then
                        dtrItem = dtbRetorno.NewRow : dtrItem("var_Modulo") = "ENGOMADO" : dtrItem("var_Descripcion") = "Falta Tipo de Engomado" : dtbRetorno.LoadDataRow(dtrItem.ItemArray, True)
                    End If

                    If dtrDatos("num_Estiraje") Is Nothing OrElse IsDBNull(dtrDatos("num_Estiraje")) = True OrElse CType(dtrDatos("num_Estiraje"), Double) <= 0 Then
                        dtrItem = dtbRetorno.NewRow : dtrItem("var_Modulo") = "ENGOMADO" : dtrItem("var_Descripcion") = "Falta Estiraje " : dtbRetorno.LoadDataRow(dtrItem.ItemArray, True)
                    End If

                    If dtrDatos("num_Velocidad") Is Nothing OrElse IsDBNull(dtrDatos("num_Velocidad")) = True OrElse CType(dtrDatos("num_Velocidad"), Double) <= 0 Then
                        dtrItem = dtbRetorno.NewRow : dtrItem("var_Modulo") = "ENGOMADO" : dtrItem("var_Descripcion") = "Falta Velocidad " : dtbRetorno.LoadDataRow(dtrItem.ItemArray, True)
                    End If
                Next
                dtbRetorno.AcceptChanges()

                'CARGANDO FORMULACION
                If dstDatos.Tables("Formulacion").Rows.Count <= 0 Then
                    dtrItem = dtbRetorno.NewRow : dtrItem("var_Modulo") = "ENGOMADO" : dtrItem("var_Descripcion") = "No existen fases de engomado" : dtbRetorno.LoadDataRow(dtrItem.ItemArray, True)
                End If

                For Each dtrDatos As DataRow In dstDatos.Tables("Formulacion").Rows
                    If dtrDatos("var_CodigoReceta") Is Nothing OrElse IsDBNull(dtrDatos("var_CodigoReceta")) = True OrElse dtrDatos("var_CodigoReceta") = "" Then
                        dtrItem = dtbRetorno.NewRow : dtrItem("var_Modulo") = "ENGOMADO" : dtrItem("var_Descripcion") = "Falta Receta en  " & dtrDatos("var_NombreFase") : dtbRetorno.LoadDataRow(dtrItem.ItemArray, True)
                    End If

                    If dtrDatos("num_Dosificacion") Is Nothing OrElse IsDBNull(dtrDatos("num_Dosificacion")) = True OrElse CType(dtrDatos("num_Dosificacion"), Double) <= 0 Then
                        dtrItem = dtbRetorno.NewRow : dtrItem("var_Modulo") = "ENGOMADO" : dtrItem("var_Descripcion") = "Falta Dosificacion en  " & dtrDatos("var_NombreFase") : dtbRetorno.LoadDataRow(dtrItem.ItemArray, True)
                    End If

                    If dtrDatos("num_Pickup") Is Nothing OrElse IsDBNull(dtrDatos("num_Pickup")) = True OrElse CType(dtrDatos("num_Pickup"), Double) <= 0 Then
                        dtrItem = dtbRetorno.NewRow : dtrItem("var_Modulo") = "ENGOMADO" : dtrItem("var_Descripcion") = "Falta Pickup en  " & dtrDatos("var_NombreFase") : dtbRetorno.LoadDataRow(dtrItem.ItemArray, True)
                    End If
                Next
                dtbRetorno.AcceptChanges()

                'CARGANDO IQ RECETA
                For Each dtrDatos As DataRow In dstDatos.Tables("Receta").Rows
                    If dtrDatos("num_Concentracion") Is Nothing OrElse IsDBNull(dtrDatos("num_Concentracion")) = True OrElse CType(dtrDatos("num_Concentracion"), Double) <= 0 Then
                        dtrItem = dtbRetorno.NewRow : dtrItem("var_Modulo") = "RECETA_PTJ" : dtrItem("var_Descripcion") = "Falta Concentracion en  " & dtrDatos("var_CodigoReceta") & "/" & dtrDatos("var_CodigoInsumo") : dtbRetorno.LoadDataRow(dtrItem.ItemArray, True)
                    End If
                Next
                dtbRetorno.AcceptChanges()

                'CARGANDO RUTA TINTORERIA
                For Each dtrDatos As DataRow In dstDatos.Tables("RutaTintoreria").Rows
                    If dtrDatos("var_CodigoMaquina") Is Nothing OrElse IsDBNull(dtrDatos("var_CodigoMaquina")) = True OrElse dtrDatos("var_CodigoMaquina") = "" Then
                        dtrItem = dtbRetorno.NewRow : dtrItem("var_Modulo") = "RUTA_TINTO" : dtrItem("var_Descripcion") = "Falta Codigo de Maquina en #Sec " & dtrDatos("int_Secuencia") : dtbRetorno.LoadDataRow(dtrItem.ItemArray, True)
                    End If

                    If dtrDatos("var_CodigoOperacion") Is Nothing OrElse IsDBNull(dtrDatos("var_CodigoOperacion")) = True OrElse dtrDatos("var_CodigoOperacion") = "" Then
                        dtrItem = dtbRetorno.NewRow : dtrItem("var_Modulo") = "RUTA_TINTO" : dtrItem("var_Descripcion") = "Falta Codigo de Operacion en #Sec " & dtrDatos("int_Secuencia") : dtbRetorno.LoadDataRow(dtrItem.ItemArray, True)
                    End If

                    If dtrDatos("int_Pases") Is Nothing OrElse IsDBNull(dtrDatos("int_Pases")) = True OrElse CType(dtrDatos("int_Pases"), Double) <= 0.0 Then
                        dtrItem = dtbRetorno.NewRow : dtrItem("var_Modulo") = "RUTA_TINTO" : dtrItem("var_Descripcion") = "Falta Pases en #Sec " & dtrDatos("int_Secuencia") : dtbRetorno.LoadDataRow(dtrItem.ItemArray, True)
                    End If
                Next
                dtbRetorno.AcceptChanges()

                'CARGANDO IQ RECETA TINTORERIA
                For Each dtrDatos As DataRow In dstDatos.Tables("RecetaInsumoTinto").Rows
                    If dtrDatos("num_Concentracion") Is Nothing OrElse IsDBNull(dtrDatos("num_Concentracion")) = True OrElse CType(dtrDatos("num_Concentracion"), Double) <= 0 Then
                        dtrItem = dtbRetorno.NewRow : dtrItem("var_Modulo") = "RECETA_PTJ" : dtrItem("var_Descripcion") = "Falta Concentracion en  " & dtrDatos("var_CodigoReceta") & "/" & dtrDatos("var_CodigoInsumo") : dtbRetorno.LoadDataRow(dtrItem.ItemArray, True)
                    End If
                Next
                dtbRetorno.AcceptChanges()

                'CARGANDO PICKUP TINTORERIA
                For Each dtrDatos As DataRow In dstDatos.Tables("PickupTintoreria").Select("var_CodigoReceta<>''")
                    If dtrDatos("num_Pickup") Is Nothing OrElse IsDBNull(dtrDatos("num_Pickup")) = True OrElse CType(dtrDatos("num_Pickup"), Double) <= 0 Then
                        dtrItem = dtbRetorno.NewRow : dtrItem("var_Modulo") = "RECETA_TIN" : dtrItem("var_Descripcion") = "Falta Pickup en  " & dtrDatos("var_CodigoOperacion") & "/" & dtrDatos("var_CodigoReceta") : dtbRetorno.LoadDataRow(dtrItem.ItemArray, True)
                    End If

                    If dtrDatos("num_Peso") Is Nothing OrElse IsDBNull(dtrDatos("num_Peso")) = True OrElse CType(dtrDatos("num_Peso"), Double) <= 0 Then
                        dtrItem = dtbRetorno.NewRow : dtrItem("var_Modulo") = "RECETA_TIN" : dtrItem("var_Descripcion") = "Falta Peso en  " & dtrDatos("var_CodigoOperacion") & "/" & dtrDatos("var_CodigoReceta") : dtbRetorno.LoadDataRow(dtrItem.ItemArray, True)
                    End If
                Next
                dtbRetorno.AcceptChanges()

                Return dtbRetorno
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function fdtbObtenerLogFichaMasiva() As DataTable
            lobj_Conexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.FichaCosto)
            Try
                Return lobj_Conexion.ObtenerDataTable("usp_FCO_GeneracionMasiva_Listar")
            Catch Ex As Exception
                Throw Ex
            Finally
                lobj_Conexion = Nothing
            End Try
        End Function

        Public Function fdtbObtenerResumenCostoVariable(ByVal strCodigoArticulo As String, ByVal strTipoFicha As String, ByVal strCodigoFicha As String) As DataTable
            Dim objParametros() As Object = {"var_CodigoArticuloLargo", strCodigoArticulo, "var_TipoFicha", strTipoFicha, "var_CodigoFicha", strCodigoFicha}
            Dim strRetorno As String = ""
            Try
                Return lobj_Conexion.ObtenerDataTable("usp_FCO_ResumenCostoVariable_Obtener_2", objParametros)
            Catch Ex As Exception
                Throw Ex
            Finally
                objParametros = Nothing
            End Try
        End Function

        Public Function fdtbObtenerResumenCostoVariable_FV(ByVal strCodigoArticulo As String, ByVal strTipoFicha As String, ByVal strCodigoFicha As String) As DataTable
            Dim objParametros() As Object = {"var_CodigoArticuloLargo", strCodigoArticulo, "var_TipoFicha", strTipoFicha, "var_CodigoFicha", strCodigoFicha}
            Dim strRetorno As String = ""
            Try
                Return lobj_Conexion.ObtenerDataTable("usp_FCO_ResumenCostoVariable_Obtener_3", objParametros)
            Catch Ex As Exception
                Throw Ex
            Finally
                objParametros = Nothing
            End Try
        End Function

        Public Function fstrObtenerUltimaFicha(ByVal strCodigoArticulo As String) As String
            Dim objParametros() As Object = {"var_CodigoArticuloLargo", strCodigoArticulo}
            Dim strRetorno As String = ""
            Try
                strRetorno = CType(lobj_Conexion.ObtenerValor("usp_qry_BuscarFicha", objParametros), String)
                Return strRetorno
            Catch Ex As Exception
                Throw Ex
            Finally
                objParametros = Nothing
            End Try
        End Function

        Public Function fObtenerCostoQuintal(ByVal strCodigoFicha As String) As DataTable
            Dim objParametros() As Object = {"sin_Anno", Left(strCodigoFicha, 4), "sin_Mes", Mid(strCodigoFicha, 5, 2)}
            Dim dtblDatos As New DataTable
            Try
                dtblDatos = lobj_Conexion.ObtenerDataTable("usp_qry_ObtenerCostoQuintal_2", objParametros)
                Return dtblDatos
            Catch Ex As Exception
                Throw Ex
            Finally
                objParametros = Nothing
            End Try
        End Function

        ' ------------------------------------------------------------------------
        ' Modificado: Se trae los costos de quintal desde la cabecera de la ficha.
        ' Abril 2014
        ' Alexander Torres Cardenas
        ' ------------------------------------------------------------------------

        Public Function fObtenerCostoQuintal_2(ByVal strCodigoArticuloLargo As String, ByVal strCodigoFicha As String) As DataTable
            Dim objParametros() As Object = {"var_CodigoArticuloLargo", strCodigoArticuloLargo, "var_CodigoFicha", strCodigoFicha}
            Dim dtblDatos As New DataTable
            Try
                dtblDatos = lobj_Conexion.ObtenerDataTable("usp_qry_ObtenerCostoQuintal_2", objParametros)
                Return dtblDatos
            Catch Ex As Exception
                Throw Ex
            Finally
                objParametros = Nothing
            End Try
        End Function

        Public Function fObtenerRentabilidad(ByVal strCodigoArticulo As String, ByVal strCodigoFicha As String) As DataTable
            Dim objParametros() As Object = {"p_var_CodigoArticulo", Left(strCodigoArticulo, 7), "sin_Anno", Left(strCodigoFicha, 4), "sin_Mes", Mid(strCodigoFicha, 5, 2)}
            Dim dtblDatos As New DataTable
            Try
                dtblDatos = lobj_Conexion.ObtenerDataTable("usp_qry_ObtenerRentabilidad", objParametros)
                Return dtblDatos
            Catch Ex As Exception
                Throw Ex
            Finally
                objParametros = Nothing
            End Try
        End Function

        '-------------------------------------------------
        'Agregado: Bloquear generacion de ficha de costos
        'Fecha: Junio 2014
        'Alexander Torres Cardenas
        '-------------------------------------------------
        Public Function fObtenerParametro(ByVal strTabla As String) As DataTable
            Dim objParametros() As Object = {"chr_CodigoTabla", strTabla}
            Dim dtblDatos As New DataTable
            Dim lobj_Conexion2 As AccesoDatosSQLServer

            lobj_Conexion2 = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.Produccion)
            Try
                dtblDatos = lobj_Conexion2.ObtenerDataTable("usp_ConsultaParametroBloqueo", objParametros)
                Return dtblDatos
            Catch Ex As Exception
                Throw Ex
            Finally
                objParametros = Nothing
            End Try
        End Function

        Public Function fActualizarParametro(ByVal strTabla As String, ByVal strDato As String, ByVal strUsuario As String) As Boolean
            Dim objParametros() As Object = {"chr_CodigoTabla", strTabla, _
                                             "ntx_Dato", strDato, _
                                             "var_Usuario", strUsuario}
            Dim blnresultado As Boolean = False
            Dim lobj_Conexion2 As AccesoDatosSQLServer
            lobj_Conexion2 = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.Produccion)
            Try
                If lobj_Conexion2.EjecutarComando("usp_ADM_TablaMaestraColumnaDato_Modificar", objParametros) > 0 Then
                    blnresultado = True
                End If
            Catch Ex As Exception
                Throw Ex
            Finally
                objParametros = Nothing
            End Try
            Return blnresultado
        End Function

        ' --------------------------------------------------------------------
        ' Modificado: Febrero 2016
        ' Alexander Torres
        ' Objetivo: Eliminacion masiva de fichas de costos
        ' --------------------------------------------------------------------
        Public Function fnc_ImportarFichasEliminarMasiva(ByVal strXml As String, ByVal strUsuario As String) As DataTable
            Dim lobjCon As NM.AccesoDatos.AccesoDatosSQLServer
            Dim dtbResultado As DataTable
            dtbResultado = Nothing
            Try
                lobjCon = New NM.AccesoDatos.AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.FichaCosto)
                Dim lstrParametros() As String = {"strxml", strXml, _
                                                  "vch_Usuario", strUsuario}
                dtbResultado = lobjCon.ObtenerDataTable("usp_COS_Ficha_EliminarMasiva", lstrParametros)
            Catch ex As Exception
                Throw ex
            End Try
            Return dtbResultado
        End Function
        'CAMBIO DG - INI
        Public Function fnc_ImportarFichasEliminarMasiva_V2(ByVal strXml As String, ByVal strUsuario As String) As DataTable
            Dim lobjCon As NM.AccesoDatos.AccesoDatosSQLServer
            Dim dtbResultado As DataTable
            dtbResultado = Nothing
            Try
                lobjCon = New NM.AccesoDatos.AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.FichaCosto)
                Dim lstrParametros() As String = {"strxml", strXml, _
                                                  "vch_Usuario", strUsuario}
                dtbResultado = lobjCon.ObtenerDataTable("usp_COS_Ficha_EliminarMasiva_V2", lstrParametros)
            Catch ex As Exception
                Throw ex
            End Try
            Return dtbResultado
        End Function
        'CAMBIO DG - FIN
        Public Function fstrObtenerDatosUltimaFicha(ByVal strCodigoArticulo As String) As DataTable
            Dim objParametros() As Object = {"var_CodigoArticuloLargo", strCodigoArticulo}
            Dim dtblDatos As New DataTable
            Try
                dtblDatos = lobj_Conexion.ObtenerDataTable("usp_qry_BuscarFicha", objParametros)
                Return dtblDatos
            Catch Ex As Exception
                Throw Ex
            Finally
                objParametros = Nothing
            End Try
        End Function

        Public Function fstrObtenerDatosUltimaFicha(ByVal strCodigoArticulo As String, ByVal strTipoFicha As String) As DataTable
            Dim objParametros() As Object = {"p_var_CodigoArticuloLargo", strCodigoArticulo, "p_var_TipoFicha", strTipoFicha}
            Dim dtblDatos As New DataTable
            Try
                dtblDatos = lobj_Conexion.ObtenerDataTable("usp_qry_BuscarFichaTipo_2", objParametros)
                Return dtblDatos
            Catch Ex As Exception
                Throw Ex
            Finally
                objParametros = Nothing
            End Try
        End Function
        Public Function fstrObtenerDatosUltimaFicha_V2(ByVal strCodigoArticulo As String, ByVal strTipoFicha As String) As DataTable
            Dim objParametros() As Object = {"p_var_CodigoArticuloLargo", strCodigoArticulo, "p_var_TipoFicha", strTipoFicha}
            Dim dtblDatos As New DataTable
            Try
                dtblDatos = lobj_Conexion.ObtenerDataTable("usp_qry_BuscarFichaTipo_2_V2", objParametros)
                Return dtblDatos
            Catch Ex As Exception
                Throw Ex
            Finally
                objParametros = Nothing
            End Try
        End Function
        Public Function fstrGenerarFichaRojo(ByVal strCodigoArticuloLargo As String, _
        ByVal strCodigoArticuloCorto As String, ByVal strUsuario As String, ByVal strProceso As String, ByVal strRutaResumen As String) As DataSet
            'strProceso=R ficha roja
            Dim objParametros() As Object = {"var_CodigoArticuloLargo", strCodigoArticuloLargo, _
            "var_CodigoArticuloCorto", strCodigoArticuloCorto, "var_Usuario", strUsuario, "var_Proceso", strProceso, "var_CodigoRutaResumen", strRutaResumen}
            Dim dstRetorno As DataSet
            Try
                dstRetorno = lobj_Conexion.ObtenerDataSet("usp_ins_GenerarFichaRojo_3", objParametros)
                Return dstRetorno
            Catch Ex As Exception
                Throw Ex
            Finally
                objParametros = Nothing
            End Try
        End Function

        Public Function fstrGenerarFichaAzul(ByVal strCodigoArticuloLargo As String, _
        ByVal strCodigoArticuloCorto As String, ByVal strUsuario As String, ByVal strProceso As String, ByVal strRutaResumen As String) As DataSet
            'strProceso=A ficha azul
            Dim objParametros() As Object = {"var_CodigoArticuloLargo", strCodigoArticuloLargo, _
            "var_CodigoArticuloCorto", strCodigoArticuloCorto, "var_Usuario", strUsuario, "var_Proceso", strProceso, "var_GMCodigo", strRutaResumen}
            Dim dstRetorno As DataSet
            Try
                dstRetorno = lobj_Conexion.ObtenerDataSet("usp_ins_GenerarFichaAzul_3", objParametros)
                Return dstRetorno
            Catch Ex As Exception
                Throw Ex
            Finally
                objParametros = Nothing
            End Try
        End Function
        'REQSIS201900060 - INI
        Public Function fstrGenerarFichaRojo_V2(ByVal strCodigoArticuloLargo As String, _
                ByVal strCodigoArticuloCorto As String, ByVal strUsuario As String, ByVal strProceso As String, ByVal strRutaResumen As String) As DataSet
            'strProceso=R ficha roja
            Dim objParametros() As Object = {"var_CodigoArticuloLargo", strCodigoArticuloLargo, _
            "var_CodigoArticuloCorto", strCodigoArticuloCorto, "var_Usuario", strUsuario, "var_Proceso", strProceso, "var_CodigoRutaResumen", strRutaResumen}
            Dim dstRetorno As DataSet
            Try
                dstRetorno = lobj_Conexion.ObtenerDataSet("usp_ins_GenerarFichaRojo_3_V2", objParametros)
                Return dstRetorno
            Catch Ex As Exception
                Throw Ex
            Finally
                objParametros = Nothing
            End Try
        End Function

        Public Function fstrGenerarFichaAzul_V2(ByVal strCodigoArticuloLargo As String, _
        ByVal strCodigoArticuloCorto As String, ByVal strUsuario As String, ByVal strProceso As String, ByVal strRutaResumen As String) As DataSet
            'strProceso=A ficha azul
            Dim objParametros() As Object = {"var_CodigoArticuloLargo", strCodigoArticuloLargo, _
            "var_CodigoArticuloCorto", strCodigoArticuloCorto, "var_Usuario", strUsuario, "var_Proceso", strProceso, "var_GMCodigo", strRutaResumen}
            Dim dstRetorno As DataSet
            Try
                dstRetorno = lobj_Conexion.ObtenerDataSet("usp_ins_GenerarFichaAzul_3_V2", objParametros)
                Return dstRetorno
            Catch Ex As Exception
                Throw Ex
            Finally
                objParametros = Nothing
            End Try
        End Function
        Public Function fdstObtenerFichaGenerada_V2(ByVal strCodigoArticuloLargo As String, ByVal strCodigoFicha As String) As DataSet
            Dim objParametros() As Object = {"var_CodigoArticuloLargo", strCodigoArticuloLargo, "var_CodigoFicha", strCodigoFicha}
            Dim dstDatos As New DataSet
            Try
                dstDatos = lobj_Conexion.ObtenerDataSet("usp_qry_ObtenerFichaGenerada_2_V2", objParametros)
                Return dstDatos
            Catch Ex As Exception
                Throw Ex
            Finally
                objParametros = Nothing
            End Try
        End Function

        'REQSIS201900060 - FIN
        Public Function fdstObtenerFichaGenerada(ByVal strCodigoArticuloLargo As String, ByVal strCodigoFicha As String) As DataSet
            Dim objParametros() As Object = {"var_CodigoArticuloLargo", strCodigoArticuloLargo, "var_CodigoFicha", strCodigoFicha}
            Dim dstDatos As New DataSet
            Try
                dstDatos = lobj_Conexion.ObtenerDataSet("usp_qry_ObtenerFichaGenerada_2", objParametros)
                Return dstDatos
            Catch Ex As Exception
                Throw Ex
            Finally
                objParametros = Nothing
            End Try
        End Function

        Public Function ListarArticulosConFicha(ByVal strCodigoArticuloLargo As String, ByVal strTipoFicha As String) As DataTable
            Dim objParametros() As Object = {"var_CodigoArticuloLargo", strCodigoArticuloLargo, "var_TipoFicha", strTipoFicha}
            Try
                Dim dtbDatos As DataTable = lobj_Conexion.ObtenerDataTable("usp_FCO_ArticuloSimulacion_Listar", objParametros)
                Return dtbDatos
            Catch Ex As Exception
                Throw Ex
            Finally
                objParametros = Nothing
            End Try
        End Function
        Public Function ListarArticulosConFicha_V2(ByVal strCodigoArticuloLargo As String, ByVal strTipoFicha As String) As DataTable
            Dim objParametros() As Object = {"var_CodigoArticuloLargo", strCodigoArticuloLargo, "var_TipoFicha", strTipoFicha}
            Try
                Dim dtbDatos As DataTable = lobj_Conexion.ObtenerDataTable("usp_FCO_ArticuloSimulacion_Listar_V2", objParametros)
                Return dtbDatos
            Catch Ex As Exception
                Throw Ex
            Finally
                objParametros = Nothing
            End Try
        End Function
        Public Function fdstObtenerFichaGeneradaVerde(ByVal strCodigoArticuloLargo As String, ByVal strCodigoFicha As String) As DataSet
            Dim objParametros() As Object = {"var_CodigoArticuloLargo", strCodigoArticuloLargo, "var_CodigoFicha", strCodigoFicha}
            Dim dstDatos As New DataSet
            Try
                dstDatos = lobj_Conexion.ObtenerDataSet("usp_qry_ObtenerFichaGeneradaVerde_2", objParametros)
                Return dstDatos
            Catch Ex As Exception
                Throw Ex
            Finally
                objParametros = Nothing
            End Try
        End Function

        Public Function fdstComparacionCostosVar(ByVal strFecIni As String, ByVal strFecFin As String) As DataTable
            Dim objParametros() As Object = {"FechaIni", strFecIni, "FechaFin", strFecFin}
            Dim dstDatos As New DataTable
            Try
                dstDatos = lobj_Conexion.ObtenerDataTable("USP_COS_ReporteCostoVar", objParametros)
                Return dstDatos
            Catch Ex As Exception
                Throw Ex
            Finally
                objParametros = Nothing
            End Try
        End Function

        Public Function fblnReporteStock(ByVal strFec As String) As DataTable
            Dim objParametros() As Object = {"Fecha", strFec}
            Dim dstDatos As New DataTable

            Try
                dstDatos = lobj_Conexion.ObtenerDataTable("usp_ReporteStockArtic", objParametros)
                Return dstDatos
            Catch Ex As Exception
                Throw Ex
            Finally
                objParametros = Nothing
            End Try

        End Function

        Public Function ActualizaConstruccionArticulos(ByVal strFechaInicio As String, _
        ByVal strFechaFinal As String, ByVal strMes As String, ByVal strAnno As String, ByVal strUsuario As String)
            Try
                Dim objParametros() As Object = {"dtm_Inicio", strFechaInicio, _
                "dtm_Final", strFechaFinal, "sin_Mes", strMes, _
                "sin_Anno", strAnno, "p_var_Usuario", strUsuario}
                Return (lobj_Conexion.EjecutarComando("usp_FCO_ReprocesarConstruccionArticulo", objParametros) > 0)
            Catch Ex As Exception
                Throw Ex
            End Try
        End Function

        Public Function ActualizaInsumoQuimico(ByVal strMes As String, ByVal strAnno As String, ByVal strUsuario As String)
            Try
                Dim objParametros() As Object = {"sin_Anno", strAnno, "sin_Mes", strMes, "var_Usuario", strUsuario}
                Return (lobj_Conexion.EjecutarComando("usp_ins_RegistraCostoInsumoQuimico", objParametros) > 0)
            Catch Ex As Exception
                Throw Ex
            End Try
        End Function

        Public Function ActualizaRecetaPretejido(ByVal strMes As String, ByVal strAnno As String, ByVal strUsuario As String)
            Try
                Dim objParametros() As Object = {"p_sin_Anno", strAnno, "p_sin_Mes", strMes, "p_var_Usuario", strUsuario}
                Return (lobj_Conexion.EjecutarComando("usp_prc_RegistrarCostoIQRecetaPretejido", objParametros) > 0)
            Catch Ex As Exception
                Throw Ex
            End Try
        End Function

        Public Function ActualizaRecetaTintoreria(ByVal strMes As String, ByVal strAnno As String, _
        ByVal strCodigoArticulo As String, ByVal strUsuario As String)
            Try
                Dim objParametros() As Object = {"sin_Anno", strAnno, "sin_Mes", strMes, _
                "var_Usuario", strUsuario, "var_CodigoArticulo", strCodigoArticulo}
                Return (lobj_Conexion.EjecutarComando("usp_FCO_RegistrarCostoRecetaTintoreria_X_Articulo", objParametros) > 0)
            Catch Ex As Exception
                Throw Ex
            End Try
        End Function

        'Generado x EPOMA
        '20120719
        Public Function pstrGeneraRutaResumen(ByVal strCodigoArticulo As String, ByVal strTipo As String) As String
            Try
                Dim objParametros() As Object = {"pvch_codigoproceso", "", "pvch_tipo", strTipo, "pvch_codigoarticuloofisis", strCodigoArticulo}

                Return CType(lobj_Conexion.ObtenerValor("usp_cos_resumenrutaartgenmasivo_proceso", objParametros), String)

            Catch Ex As Exception
                Throw Ex
            End Try
        End Function

        'Generado x EPOMA
        '20120719
        Public Function pstrObtieneRutaResumen(ByVal strCodigoArticulo As String, ByVal strTipo As String) As String
            Try
                Dim objParametros() As Object = {"pvch_tipo", strTipo, "pvch_codigoarticuloofisis", strCodigoArticulo}

                Return CType(lobj_Conexion.ObtenerValor("usp_cos_resumenrutaartgenmasivo_obtener", objParametros), String)

            Catch Ex As Exception
                Throw Ex
            End Try
        End Function
        'REQSIS201700009 - DG - INI
        Public Function pstrValidaEncogimiento(ByVal strCodigoArticulo As String) As String
            Try
                Dim objParametros() As Object = {"pvch_codigoarticuloofisis", strCodigoArticulo}

                Return CType(lobj_Conexion.ObtenerValor("usp_validar_encogimiento_vigente", objParametros), String)
            Catch ex As Exception
                Throw ex
            End Try
        End Function
        'REQSIS201700009 - DG - FIN
#End Region

    End Class

    Public Class Importacion

        Sub New(ByVal pstrUsuario As String)
            Me.Usuario = pstrUsuario
        End Sub

#Region "Variables"

        Private mstrRuta As String = "D:\Interfaz\COSTOS_IMPORTACION.xls"
        Private mintAnio As Integer
        Private mintMes As Integer
        Private mintVersion As Integer
        Private mstrUsuario As String
        Private mstrError As String

#End Region

#Region "Enumeraciones"
        Enum enuTipoCosto
            [Quintal] = 0
            [Produccion] = 1
            [MateriaPrima] = 2
            [Urdido] = 3
            [Engomado] = 4
            [Telares] = 5
            [MaquinasTintoreria] = 6
            [RevisionFinal] = 7
            [Rentabilidad] = 8
            [CostoContable] = 9
            [CostoEstampado] = 10
            [PreEngomado] = 11
            [PreTelar] = 12
            [PreUrdido] = 13
            [PreCosTelar] = 14
            [MateriaPrimaGere] = 15
            [FichaEliminar] = 16
        End Enum

        Enum enuTipoFuente
            [BaseDatos] = 0
            [XLS] = 1
        End Enum

#End Region

#Region "Propiedades"
        Public Property Ruta() As String
            Get
                Ruta = mstrRuta
            End Get
            Set(ByVal Value As String)
                mstrRuta = Value
            End Set
        End Property

        Public Property Anio() As Integer
            Get
                Anio = mintAnio
            End Get
            Set(ByVal Value As Integer)
                mintAnio = Value
            End Set
        End Property

        Public Property Mes() As Integer
            Get
                Mes = mintMes
            End Get
            Set(ByVal Value As Integer)
                mintMes = Value
            End Set
        End Property

        Public Property Version() As Integer
            Get
                Mes = mintVersion
            End Get
            Set(ByVal Value As Integer)
                mintVersion = Value
            End Set
        End Property

        Public Property Usuario() As String
            Get
                Usuario = mstrUsuario
            End Get
            Set(ByVal Value As String)
                mstrUsuario = Value
            End Set
        End Property

        Public Property ErrorDesc() As String
            Get
                ErrorDesc = mstrError
            End Get
            Set(ByVal Value As String)
                mstrError = Value
            End Set
        End Property

#End Region

#Region "Esquemas"
        Public Function Esquema(ByVal penuTipo As enuTipoCosto) As DataTable
            Select Case penuTipo
                Case enuTipoCosto.Quintal
                    Return EsquemaQuintal()
                Case enuTipoCosto.Produccion
                    Return EsquemaProduccion()
                Case enuTipoCosto.MateriaPrima
                    Return EsquemaMateriaPrima()
                Case enuTipoCosto.Urdido
                    Return EsquemaUrdido()
                Case enuTipoCosto.Engomado
                    Return EsquemaEngomado()
                Case enuTipoCosto.Telares
                    Return EsquemaTelar()
                Case enuTipoCosto.MaquinasTintoreria
                    Return EsquemaMaquinaTintoreria()
                Case enuTipoCosto.RevisionFinal
                    Return EsquemaRevisionFinal()
                Case enuTipoCosto.Rentabilidad
                    Return EsquemaRentabilidad()
                Case enuTipoCosto.CostoContable
                    Return EsquemaCostoContableStock()
                Case enuTipoCosto.CostoEstampado
                    Return EsquemaCostoEstampado()
                Case enuTipoCosto.PreEngomado
                    Return EsquemaPreEngomado()
                Case enuTipoCosto.PreTelar
                    Return EsquemaPreTelar()
                Case enuTipoCosto.PreUrdido
                    Return EsquemaPreUrdido()

            End Select

        End Function

        Private Function EsquemaQuintal() As DataTable
            Dim ldtRes As DataTable = New DataTable("CostosQuintal")
            With ldtRes.Columns
                .Add("sin_Anio", GetType(Integer))
                .Add("sin_Mes", GetType(Integer))
                .Add("chr_Linea", GetType(String))
                .Add("mon_CostoSol", GetType(Decimal))
            End With
            Return ldtRes
        End Function

        Private Function EsquemaMateriaPrima() As DataTable
            Dim ldtRes As DataTable = New DataTable("CostosMateriaPrima")
            With ldtRes.Columns
                .Add("sin_Anio", GetType(Integer))
                .Add("sin_Mes", GetType(Integer))
                .Add("int_Procedencia", GetType(String))
                .Add("var_Mezcla", GetType(String))
                .Add("var_Linea", GetType(String))
                .Add("var_CodigoHilo", GetType(String))
                'Cabo
                .Add("var_CaboProcedencia", GetType(Decimal))
                .Add("var_CaboMezcla", GetType(Decimal))
                .Add("var_CaboLinea", GetType(Decimal))
                .Add("var_CaboCodigoHilo", GetType(String))
                'Porcentaje
                .Add("num_PorcentajeAlgodon", GetType(Decimal))
                .Add("num_PorcentajePolyester", GetType(Decimal))
                .Add("num_PorcentajeSpandex", GetType(Decimal))
                'CU
                .Add("num_CUAlgodon", GetType(Decimal))
                .Add("num_CUSpandex", GetType(Decimal))
                .Add("num_CUHiloComprado", GetType(Decimal))
                .Add("num_CUPolyester", GetType(Decimal))
                'Merma Hilanderia Andina
                .Add("num_MermaHAAlgodon", GetType(Decimal))
                .Add("num_MermaHAPolyester", GetType(Decimal))
                .Add("num_MermaHASpandex", GetType(Decimal))
                .Add("num_MermaHAPolycotton", GetType(Decimal))
                .Add("num_MermaRingAlgodon", GetType(Decimal))
                .Add("num_MermaRingPolyester", GetType(Decimal))
                .Add("num_MermaRingSpandex", GetType(Decimal))
                .Add("num_MermaOEAlgodon", GetType(Decimal))
                .Add("num_MermaRecubiertoPolyester", GetType(Decimal))
                .Add("num_MermaRecubiertoSpandex", GetType(Decimal))
                .Add("num_TotalCosto", GetType(Decimal))

            End With
            Return ldtRes
        End Function

        Private Function EsquemaCostoContableStock() As DataTable
            Dim ldtRes As DataTable = New DataTable("CostosContable")
            With ldtRes.Columns
                .Add("int_Anio", GetType(Integer))
                .Add("int_Mes", GetType(Integer))
                .Add("var_CodArticulo", GetType(String))
                .Add("var_DesArticulo", GetType(String))
                .Add("num_CostoTotal", GetType(Decimal))
            End With
            Return ldtRes
        End Function

        Private Function EsquemaCostoEstampado() As DataTable
            Dim ldtRes As DataTable = New DataTable("CostoEstampado")
            With ldtRes.Columns
                .Add("int_Anio", GetType(Integer))
                .Add("int_Mes", GetType(Integer))
                .Add("var_CodArticulo", GetType(String))
                .Add("var_DesArticulo", GetType(String))
                .Add("num_CostoTotal", GetType(Decimal))
            End With
            Return ldtRes
        End Function

        Private Function EsquemaPreEngomado() As DataTable
            Dim ldtRes As DataTable = New DataTable("PreEngomado")
            With ldtRes.Columns
                .Add("int_Anio", GetType(Integer))
                .Add("int_Mes", GetType(Integer))
                .Add("var_Linea", GetType(String))
                .Add("num_CV", GetType(Decimal))
                .Add("num_MOV", GetType(Decimal))
                .Add("num_MOF", GetType(Decimal))
                .Add("num_OtroFijo", GetType(Decimal))
                .Add("num_FactVEngomd", GetType(Decimal))
                .Add("num_FactVTED", GetType(Decimal))
                .Add("num_Vel", GetType(Decimal))
            End With
            Return ldtRes
        End Function

        Private Function EsquemaPreTelar() As DataTable
            Dim ldtRes As DataTable = New DataTable("PreTelar")
            With ldtRes.Columns
                .Add("int_Anio", GetType(Integer))
                .Add("int_Mes", GetType(Integer))
                .Add("var_TipoTelar", GetType(String))
                .Add("var_NomTelar", GetType(String))
                .Add("num_CV", GetType(Decimal))
                .Add("num_CMOV", GetType(Decimal))
                .Add("num_CMOF", GetType(Decimal))
                .Add("num_OtroCF", GetType(Decimal))
            End With
            Return ldtRes
        End Function

        Private Function EsquemaPreUrdido() As DataTable
            Dim ldtRes As DataTable = New DataTable("PreUrdido")
            With ldtRes.Columns
                .Add("int_Anio", GetType(Integer))
                .Add("int_Mes", GetType(Integer))
                .Add("var_Linea", GetType(String))
                .Add("num_CV", GetType(Decimal))
                .Add("num_MOV", GetType(Decimal))
                .Add("num_MOF", GetType(Decimal))
                .Add("num_OtroCF", GetType(Decimal))
                .Add("num_FVS", GetType(Decimal))
                .Add("num_FVHacoba", GetType(Decimal))
                .Add("num_Vel", GetType(Decimal))
            End With
            Return ldtRes
        End Function

        Private Function EsquemaProduccion() As DataTable
            Dim ldtRes As DataTable = New DataTable("CostosProduccion")
            With ldtRes.Columns
                .Add("sin_Anio", GetType(Integer))
                .Add("sin_Mes", GetType(Integer))
                .Add("var_CodigoHilo", GetType(String))
                .Add("mon_CostoPromedio", GetType(Decimal))
                .Add("mon_CostoVariable", GetType(Decimal))
                .Add("num_ManoObraVariable", GetType(Decimal))
                .Add("num_ManoObraFija", GetType(Decimal))
                .Add("mon_OtroCosto", GetType(Decimal))
                .Add("num_FactorFijoVariable", GetType(Decimal))
            End With
            Return ldtRes
        End Function

        Private Function EsquemaUrdido() As DataTable
            Dim ldtRes As DataTable = New DataTable("CostosUrdido")
            With ldtRes.Columns
                .Add("sin_Anio", GetType(Integer))
                .Add("sin_Mes", GetType(Integer))
                .Add("var_CodigoArticulo", GetType(String))
                .Add("mon_CostoVariable", GetType(Decimal))
                .Add("num_ManoObraVariable", GetType(Decimal))
                .Add("num_ManoObraFija", GetType(Decimal))
                .Add("num_OtroGastoFijo", GetType(Decimal))
                .Add("num_FactorVariableSchlaforst", GetType(Decimal))
                .Add("num_FactorVariableHacoba", GetType(Decimal))
            End With
            Return ldtRes
        End Function

        Private Function EsquemaEngomado() As DataTable
            Dim ldtRes As DataTable = New DataTable("CostosEngomado")
            With ldtRes.Columns
                .Add("sin_Anio", GetType(Integer))
                .Add("sin_Mes", GetType(Integer))
                .Add("var_CodigoArticulo", GetType(String))
                .Add("mon_CostoVariable", GetType(Decimal))
                .Add("num_ManoObraVariable", GetType(Decimal))
                .Add("num_ManoObraFija", GetType(Decimal))
                .Add("num_OtroGastoFijo", GetType(Decimal))
                .Add("num_FactorVariableEngomado", GetType(Decimal))
                .Add("num_FactorVariableTED", GetType(Decimal))
            End With
            Return ldtRes
        End Function

        Private Function EsquemaTelar() As DataTable
            Dim ldtRes As DataTable = New DataTable("CostosTelar")
            With ldtRes.Columns
                .Add("sin_Anio", GetType(Integer))
                .Add("sin_Mes", GetType(Integer))
                .Add("var_CodigoArticulo", GetType(String))
                .Add("mon_CostoVariable", GetType(Decimal))
                .Add("num_ManoObraVariable", GetType(Decimal))
                .Add("num_ManoObraFija", GetType(Decimal))
                .Add("num_OtroGastoFijo", GetType(Decimal))
                .Add("num_FactorVariableSulzer", GetType(Decimal))
                .Add("num_FactorVariablePikanol", GetType(Decimal))
                .Add("num_MermaTejeduria", GetType(Decimal))
            End With
            Return ldtRes
        End Function

        Private Function EsquemaMaquinaTintoreria() As DataTable
            Dim ldtRes As DataTable = New DataTable("CostosMaquinaTintoreria")
            With ldtRes.Columns
                .Add("sin_Anio", GetType(Integer))
                .Add("sin_Mes", GetType(Integer))
                .Add("var_CodigoMaquina", GetType(String))
                .Add("mon_CostoVariable", GetType(Decimal))
                .Add("num_ManoObraVariable", GetType(Decimal))
                .Add("num_ManoObraFija", GetType(Decimal))
                .Add("mon_CostoFijo", GetType(Decimal))
                .Add("num_PorcentajeCostoFijo", GetType(Decimal))
                .Add("num_PorcentajeCostoFijoNoVariable", GetType(Decimal))
            End With
            Return ldtRes
        End Function

        Private Function EsquemaRevisionFinal() As DataTable
            Dim ldtRes As DataTable = New DataTable("CostosRevisionFinal")
            With ldtRes.Columns
                .Add("sin_Anio", GetType(Integer))
                .Add("sin_Mes", GetType(Integer))
                .Add("mon_CostoVariable", GetType(Decimal))
                .Add("num_ManoObraVariable", GetType(Decimal))
                .Add("num_ManoObraFija", GetType(Decimal))
                .Add("mon_CostoFijo", GetType(Decimal))
                .Add("mon_CostoFijoVariable", GetType(Decimal))
                .Add("mon_CostoFijoNoVariable", GetType(Decimal))
            End With
            Return ldtRes
        End Function

        Private Function EsquemaRentabilidad() As DataTable
            Dim ldtRes As DataTable = New DataTable("CostosRentabilidad")
            With ldtRes.Columns
                .Add("sin_Anio", GetType(Integer))
                .Add("sin_Mes", GetType(Integer))
                .Add("var_CodigoArticulo", GetType(String))
                .Add("num_MetrosVentas", GetType(Decimal))
                .Add("mon_Rentabilidad", GetType(Decimal))
            End With
            Return ldtRes
        End Function

#End Region

#Region "Obtener Data de XLS"
        Private Function ObtenerCostosQuintalXLS() As DataTable
            Dim lobjCon As New OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source='" & mstrRuta & "';Extended Properties=""Excel 8.0;HDR=Yes;IMEX=1""")
            Dim lobjCom As New OleDbCommand("Select * From [CQ$]", lobjCon)
            Dim ldtQuintal As DataTable = EsquemaQuintal()
            Dim ldrFila As DataRow
            Try
                lobjCon.Open()
                Dim xlReader As OleDbDataReader = lobjCom.ExecuteReader
                While xlReader.Read
                    If Not IsDBNull(xlReader.Item(0)) Then
                        If (xlReader.Item(0) = mintAnio) And (xlReader.Item(1) = mintMes) Then
                            ldrFila = ldtQuintal.NewRow()
                            With ldrFila
                                .Item(0) = xlReader.Item(0)
                                .Item(1) = xlReader.Item(1)
                                .Item(2) = xlReader.Item(2)
                                .Item(3) = xlReader.Item(3)
                            End With
                            ldtQuintal.Rows.Add(ldrFila)
                            ldrFila = Nothing
                        End If
                    End If
                End While
                xlReader.Close()
                Return ldtQuintal
            Catch ex As Exception
                Throw ex
            Finally
                lobjCon.Close()
                lobjCon.Dispose()
                lobjCon = Nothing
            End Try
        End Function

        Private Function ObtenerCostosmateriaprimaXLS() As DataTable

            Dim lobjCon As New OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source='" & mstrRuta & "';Extended Properties=""Excel 8.0;HDR=Yes;IMEX=1""")
            Dim lobjCom As New OleDbCommand("Select * From [CMP$]", lobjCon)
            Dim ldtQuintal As DataTable = EsquemaMateriaPrima()
            Dim ldrFila As DataRow
            Try
                lobjCon.Open()
                Dim xlReader As OleDbDataReader = lobjCom.ExecuteReader
                While xlReader.Read
                    If Not IsDBNull(xlReader.Item(0)) Then
                        If (xlReader.Item(0) = mintAnio) And (xlReader.Item(1) = mintMes) Then
                            ldrFila = ldtQuintal.NewRow()
                            With ldrFila
                                .Item(0) = xlReader.Item(0)
                                .Item(1) = xlReader.Item(1)
                                .Item(2) = xlReader.Item(2)
                                .Item(3) = xlReader.Item(3)
                                .Item(4) = xlReader.Item(4)
                                .Item(5) = xlReader.Item(5)
                                'Cabo
                                .Item(6) = xlReader.Item(6)
                                .Item(7) = xlReader.Item(7)
                                .Item(8) = xlReader.Item(8)
                                .Item(9) = xlReader.Item(9)
                                'Composicion
                                .Item(10) = xlReader.Item(10)
                                .Item(11) = xlReader.Item(11)
                                .Item(12) = xlReader.Item(12)
                                'CU
                                .Item(13) = xlReader.Item(13)
                                .Item(14) = xlReader.Item(14)
                                .Item(15) = xlReader.Item(15)
                                .Item(16) = xlReader.Item(16)
                                'Merma
                                .Item(17) = xlReader.Item(17)
                                .Item(18) = xlReader.Item(18)
                                .Item(19) = xlReader.Item(19)
                                .Item(20) = xlReader.Item(20)
                                .Item(21) = xlReader.Item(21)
                                .Item(22) = xlReader.Item(22)
                                .Item(23) = xlReader.Item(23)
                                .Item(24) = xlReader.Item(24)
                                .Item(25) = xlReader.Item(25)
                                .Item(26) = xlReader.Item(26)
                                'Total Costos
                                .Item(27) = xlReader.Item(27)

                            End With
                            ldtQuintal.Rows.Add(ldrFila)
                            ldrFila = Nothing
                        End If
                    End If
                End While
                xlReader.Close()
                Return ldtQuintal
            Catch ex As Exception
                Throw ex
            Finally
                lobjCon.Close()
                lobjCon.Dispose()
                lobjCon = Nothing
            End Try
        End Function

        Private Function ObtenerCostosContableStockXLS() As DataTable
            Dim lobjCon As New OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source='" & mstrRuta & "';Extended Properties=""Excel 8.0;HDR=YES;IMEX=0""")
            Dim lobjCom As New OleDbCommand("Select * From [CCS$]", lobjCon)
            Dim ldtData As DataTable = EsquemaCostoContableStock()
            Dim ldrFila As DataRow
            Try
                lobjCon.Open()
                Dim xlReader As OleDbDataReader = lobjCom.ExecuteReader
                While xlReader.Read
                    If Not IsDBNull(xlReader.Item(0)) Then
                        If (xlReader.Item(0) = mintAnio) And (xlReader.Item(1) = mintMes) Then
                            ldrFila = ldtData.NewRow()
                            With ldrFila
                                .Item(0) = xlReader.Item(0)
                                .Item(1) = xlReader.Item(1)
                                .Item(2) = xlReader.Item(2)
                                .Item(3) = xlReader.Item(3)
                                .Item(4) = xlReader.Item(4)
                            End With
                            ldtData.Rows.Add(ldrFila)
                            ldrFila = Nothing
                        End If
                    End If
                End While
                xlReader.Close()
                Return ldtData
            Catch ex As Exception
                Throw ex
            Finally
                lobjCon.Close()
                lobjCon.Dispose()
                lobjCon = Nothing
            End Try
        End Function

        Private Function ObtenerCostoEstampadoXLS() As DataTable
            Dim lobjCon As New OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source='" & mstrRuta & "';Extended Properties=""Excel 8.0;HDR=Yes;IMEX=1""")
            Dim lobjCom As New OleDbCommand("Select * From [CESTAMPADO$]", lobjCon)
            Dim ldtData As DataTable = EsquemaCostoEstampado()
            Dim ldrFila As DataRow
            Try
                lobjCon.Open()
                Dim xlReader As OleDbDataReader = lobjCom.ExecuteReader
                While xlReader.Read
                    If Not IsDBNull(xlReader.Item(0)) Then
                        If (xlReader.Item(0) = 0) And (xlReader.Item(1) = 0) Then
                            ldrFila = ldtData.NewRow()
                            With ldrFila
                                .Item(0) = xlReader.Item(0)
                                .Item(1) = xlReader.Item(1)
                                .Item(2) = xlReader.Item(2)
                                .Item(3) = xlReader.Item(3)
                                .Item(4) = xlReader.Item(4)
                            End With
                            ldtData.Rows.Add(ldrFila)
                            ldrFila = Nothing
                        End If
                    End If
                End While
                xlReader.Close()
                Return ldtData
            Catch ex As Exception
                Throw ex
            Finally
                lobjCon.Close()
                lobjCon.Dispose()
                lobjCon = Nothing
            End Try
        End Function

        Private Function ObtenerPreEngomadoXLS() As DataTable
            Dim lobjCon As New OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source='" & mstrRuta & "';Extended Properties=""Excel 8.0;HDR=Yes;IMEX=1""")
            Dim lobjCom As New OleDbCommand("Select * From [ENGOMADO$]", lobjCon)
            Dim ldtData As DataTable = EsquemaPreEngomado()
            Dim ldrFila As DataRow
            Try
                lobjCon.Open()
                Dim xlReader As OleDbDataReader = lobjCom.ExecuteReader
                While xlReader.Read
                    If Not IsDBNull(xlReader.Item(0)) Then
                        If (xlReader.Item(0) = mintAnio) And (xlReader.Item(1) = mintMes) Then
                            ldrFila = ldtData.NewRow()
                            With ldrFila
                                .Item(0) = xlReader.Item(0)
                                .Item(1) = xlReader.Item(1)
                                .Item(2) = xlReader.Item(2)
                                .Item(3) = xlReader.Item(3)
                                .Item(4) = xlReader.Item(4)
                                .Item(5) = xlReader.Item(5)
                                .Item(6) = xlReader.Item(6)
                                .Item(7) = xlReader.Item(7)
                                .Item(8) = xlReader.Item(8)
                                .Item(9) = xlReader.Item(9)
                            End With
                            ldtData.Rows.Add(ldrFila)
                            ldrFila = Nothing
                        End If
                    End If
                End While
                xlReader.Close()
                Return ldtData
            Catch ex As Exception
                Throw ex
            Finally
                lobjCon.Close()
                lobjCon.Dispose()
                lobjCon = Nothing
            End Try
        End Function

        Private Function ObtenerPreTelarXLS() As DataTable
            Dim lobjCon As New OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source='" & mstrRuta & "';Extended Properties=""Excel 8.0;HDR=Yes;IMEX=1""")
            Dim lobjCom As New OleDbCommand("Select * From [Telar$]", lobjCon)
            Dim ldtData As DataTable = EsquemaPreTelar()
            Dim ldrFila As DataRow
            Try
                lobjCon.Open()
                Dim xlReader As OleDbDataReader = lobjCom.ExecuteReader
                While xlReader.Read
                    If Not IsDBNull(xlReader.Item(0)) Then
                        If (xlReader.Item(0) = mintAnio) And (xlReader.Item(1) = mintMes) Then
                            ldrFila = ldtData.NewRow()
                            With ldrFila
                                .Item(0) = xlReader.Item(0)
                                .Item(1) = xlReader.Item(1)
                                .Item(2) = xlReader.Item(2)
                                .Item(3) = xlReader.Item(3)
                                .Item(4) = xlReader.Item(4)
                                .Item(5) = xlReader.Item(5)
                                .Item(6) = xlReader.Item(6)
                                .Item(7) = xlReader.Item(7)
                            End With
                            ldtData.Rows.Add(ldrFila)
                            ldrFila = Nothing
                        End If
                    End If
                End While
                xlReader.Close()
                Return ldtData
            Catch ex As Exception
                Throw ex
            Finally
                lobjCon.Close()
                lobjCon.Dispose()
                lobjCon = Nothing
            End Try
        End Function

        Private Function ObtenerPreUrdidoXLS() As DataTable
            Dim lobjCon As New OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source='" & mstrRuta & "';Extended Properties=""Excel 8.0;HDR=Yes;IMEX=1""")
            Dim lobjCom As New OleDbCommand("Select * From [Urdido$]", lobjCon)
            Dim ldtData As DataTable = EsquemaPreUrdido()
            Dim ldrFila As DataRow
            Try
                lobjCon.Open()
                Dim xlReader As OleDbDataReader = lobjCom.ExecuteReader
                While xlReader.Read
                    If Not IsDBNull(xlReader.Item(0)) Then
                        If (xlReader.Item(0) = mintAnio) And (xlReader.Item(1) = mintMes) Then
                            ldrFila = ldtData.NewRow()
                            With ldrFila
                                .Item(0) = xlReader.Item(0)
                                .Item(1) = xlReader.Item(1)
                                .Item(2) = xlReader.Item(2)
                                .Item(3) = xlReader.Item(3)
                                .Item(4) = xlReader.Item(4)
                                .Item(5) = xlReader.Item(5)
                                .Item(6) = xlReader.Item(6)
                                .Item(7) = xlReader.Item(7)
                                .Item(8) = xlReader.Item(8)
                                .Item(9) = xlReader.Item(9)
                            End With
                            ldtData.Rows.Add(ldrFila)
                            ldrFila = Nothing
                        End If
                    End If
                End While
                xlReader.Close()
                Return ldtData
            Catch ex As Exception
                Throw ex
            Finally
                lobjCon.Close()
                lobjCon.Dispose()
                lobjCon = Nothing
            End Try
        End Function

        Private Function ObtenerCostosProduccionXLS() As DataTable
            Dim lobjCon As New OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source='" & mstrRuta & "';Extended Properties=""Excel 8.0;HDR=Yes;IMEX=1""")
            Dim lobjCom As New OleDbCommand("Select * From [CP$]", lobjCon)
            Dim ldtProduccionHilo As DataTable = EsquemaProduccion()
            Dim ldrFila As DataRow
            Try
                lobjCon.Open()
                Dim xlReader As OleDbDataReader = lobjCom.ExecuteReader
                While xlReader.Read
                    If Not IsDBNull(xlReader.Item(0)) Then
                        If (xlReader.Item(0) = mintAnio) And (xlReader.Item(1) = mintMes) Then
                            ldrFila = ldtProduccionHilo.NewRow()
                            With ldrFila
                                .Item(0) = xlReader.Item(0)
                                .Item(1) = xlReader.Item(1)
                                .Item(2) = xlReader.Item(2)
                                .Item(3) = xlReader.Item(3)
                                .Item(4) = xlReader.Item(4)
                                .Item(5) = xlReader.Item(5)
                                .Item(6) = xlReader.Item(6)
                                .Item(7) = xlReader.Item(7)
                                .Item(8) = xlReader.Item(8)
                            End With
                            ldtProduccionHilo.Rows.Add(ldrFila)
                            ldrFila = Nothing
                        End If
                    End If
                End While
                xlReader.Close()
                Return ldtProduccionHilo
            Catch ex As Exception
                Throw ex
            Finally
                lobjCon.Close()
                lobjCon.Dispose()
                lobjCon = Nothing
            End Try
        End Function

        Private Function ObtenerCostosUrdidoXLS() As DataTable
            Dim lobjCon As New OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source='" & mstrRuta & "';Extended Properties=""Excel 8.0;HDR=Yes;IMEX=1""")
            Dim lobjCom As New OleDbCommand("Select * From [CU$]", lobjCon)
            Dim ldtUrdido As DataTable = EsquemaUrdido()
            Dim ldrFila As DataRow
            Try
                lobjCon.Open()
                Dim xlReader As OleDbDataReader = lobjCom.ExecuteReader
                While xlReader.Read
                    If Not IsDBNull(xlReader.Item(0)) Then
                        If (xlReader.Item(0) = mintAnio) And (xlReader.Item(1) = mintMes) Then
                            ldrFila = ldtUrdido.NewRow()
                            With ldrFila
                                .Item(0) = xlReader.Item(0)
                                .Item(1) = xlReader.Item(1)
                                .Item(2) = xlReader.Item(2)
                                .Item(3) = xlReader.Item(3)
                                .Item(4) = xlReader.Item(4)
                                .Item(5) = xlReader.Item(5)
                                .Item(6) = xlReader.Item(6)
                                .Item(7) = xlReader.Item(7)
                                .Item(8) = xlReader.Item(8)
                            End With
                            ldtUrdido.Rows.Add(ldrFila)
                            ldrFila = Nothing
                        End If
                    End If
                End While
                xlReader.Close()
                Return ldtUrdido
            Catch ex As Exception
                Throw ex
            Finally
                lobjCon.Close()
                lobjCon.Dispose()
                lobjCon = Nothing
            End Try
        End Function

        Private Function ObtenerCostosEngomadoXLS() As DataTable
            Dim lobjCon As New OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source='" & mstrRuta & "';Extended Properties=""Excel 8.0;HDR=Yes;IMEX=1""")
            Dim lobjCom As New OleDbCommand("Select * From [CE$]", lobjCon)
            Dim ldtEngomado As DataTable = EsquemaEngomado()
            Dim ldrFila As DataRow
            Try
                lobjCon.Open()
                Dim xlReader As OleDbDataReader = lobjCom.ExecuteReader
                While xlReader.Read
                    If Not IsDBNull(xlReader.Item(0)) Then
                        If (xlReader.Item(0) = mintAnio) And (xlReader.Item(1) = mintMes) Then
                            ldrFila = ldtEngomado.NewRow()
                            With ldrFila
                                .Item(0) = xlReader.Item(0)
                                .Item(1) = xlReader.Item(1)
                                .Item(2) = xlReader.Item(2)
                                .Item(3) = xlReader.Item(3)
                                .Item(4) = xlReader.Item(4)
                                .Item(5) = xlReader.Item(5)
                                .Item(6) = xlReader.Item(6)
                                .Item(7) = xlReader.Item(7)
                                .Item(8) = xlReader.Item(8)
                            End With
                            ldtEngomado.Rows.Add(ldrFila)
                            ldrFila = Nothing
                        End If
                    End If
                End While
                xlReader.Close()
                Return ldtEngomado
            Catch ex As Exception
                Throw ex
            Finally
                lobjCon.Close()
                lobjCon.Dispose()
                lobjCon = Nothing
            End Try
        End Function

        Private Function ObtenerCostosTelarXLS() As DataTable
            Dim lobjCon As New OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source='" & mstrRuta & "';Extended Properties=""Excel 8.0;HDR=Yes;IMEX=1""")
            Dim lobjCom As New OleDbCommand("Select * From [CT$]", lobjCon)
            Dim ldtTelares As DataTable = EsquemaTelar()
            Dim ldrFila As DataRow
            Try
                lobjCon.Open()
                Dim xlReader As OleDbDataReader = lobjCom.ExecuteReader
                While xlReader.Read
                    If Not IsDBNull(xlReader.Item(0)) Then
                        If (xlReader.Item(0) = mintAnio) And (xlReader.Item(1) = mintMes) Then
                            ldrFila = ldtTelares.NewRow()
                            With ldrFila
                                .Item(0) = xlReader.Item(0)
                                .Item(1) = xlReader.Item(1)
                                .Item(2) = xlReader.Item(2)
                                .Item(3) = xlReader.Item(3)
                                .Item(4) = xlReader.Item(4)
                                .Item(5) = xlReader.Item(5)
                                .Item(6) = xlReader.Item(6)
                                .Item(7) = xlReader.Item(7)
                                .Item(8) = xlReader.Item(8)
                                .Item(9) = xlReader.Item(9)
                            End With
                            ldtTelares.Rows.Add(ldrFila)
                            ldrFila = Nothing
                        End If
                    End If
                End While
                xlReader.Close()
                Return ldtTelares
            Catch ex As Exception
                Throw ex
            Finally
                lobjCon.Close()
                lobjCon.Dispose()
                lobjCon = Nothing
            End Try
        End Function

        Private Function ObtenerCostosMaquinasTintoreriaXLS() As DataTable
            Dim lobjCon As New OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source='" & mstrRuta & "';Extended Properties=""Excel 8.0;HDR=Yes;IMEX=1""")
            Dim lobjCom As New OleDbCommand("Select * From [MT$]", lobjCon)
            Dim ldtMaquinasTintoreria As DataTable = EsquemaMaquinaTintoreria()
            Dim ldrFila As DataRow
            Try
                lobjCon.Open()
                Dim xlReader As OleDbDataReader = lobjCom.ExecuteReader
                While xlReader.Read
                    If Not IsDBNull(xlReader.Item(0)) Then
                        If (xlReader.Item(0) = mintAnio) And (xlReader.Item(1) = mintMes) Then
                            ldrFila = ldtMaquinasTintoreria.NewRow()
                            With ldrFila
                                .Item(0) = xlReader.Item(0)
                                .Item(1) = xlReader.Item(1)
                                .Item(2) = xlReader.Item(2)
                                .Item(3) = xlReader.Item(3)
                                .Item(4) = xlReader.Item(4)
                                .Item(5) = xlReader.Item(5)
                                .Item(6) = xlReader.Item(6)
                                .Item(7) = xlReader.Item(7)
                                .Item(8) = xlReader.Item(8)
                            End With
                            ldtMaquinasTintoreria.Rows.Add(ldrFila)
                            ldrFila = Nothing
                        End If
                    End If
                End While
                xlReader.Close()
                Return ldtMaquinasTintoreria
            Catch ex As Exception
                Throw ex
            Finally
                lobjCon.Close()
                lobjCon.Dispose()
                lobjCon = Nothing
            End Try
        End Function

        Private Function ObtenerCostosRevisionFinalXLS() As DataTable
            Dim lobjCon As New OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source='" & mstrRuta & "';Extended Properties=""Excel 8.0;HDR=Yes;IMEX=1""")
            Dim lobjCom As New OleDbCommand("Select * From [RF$]", lobjCon)
            Dim ldtUrdido As DataTable = EsquemaRevisionFinal()
            Dim ldrFila As DataRow
            Try
                lobjCon.Open()
                Dim xlReader As OleDbDataReader = lobjCom.ExecuteReader
                While xlReader.Read
                    If Not IsDBNull(xlReader.Item(0)) Then
                        If (xlReader.Item(0) = mintAnio) And (xlReader.Item(1) = mintMes) Then
                            ldrFila = ldtUrdido.NewRow()
                            With ldrFila
                                .Item(0) = xlReader.Item(0)
                                .Item(1) = xlReader.Item(1)
                                .Item(2) = xlReader.Item(2)
                                .Item(3) = xlReader.Item(3)
                                .Item(4) = xlReader.Item(4)
                                .Item(5) = xlReader.Item(5)
                                .Item(6) = xlReader.Item(6)
                                .Item(7) = xlReader.Item(7)
                            End With
                            ldtUrdido.Rows.Add(ldrFila)
                            ldrFila = Nothing
                        End If
                    End If
                End While
                xlReader.Close()
                Return ldtUrdido
            Catch ex As Exception
                Throw ex
            Finally
                lobjCon.Close()
                lobjCon.Dispose()
                lobjCon = Nothing
            End Try
        End Function

        Private Function ObtenerCostosRentabilidadXLS() As DataTable
            Dim lobjCon As New OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source='" & mstrRuta & "';Extended Properties=""Excel 8.0;HDR=Yes;IMEX=1""")
            Dim lobjCom As New OleDbCommand("Select * From [R$]", lobjCon)
            Dim ldtRentabilidad As DataTable = EsquemaRentabilidad()
            Dim ldrFila As DataRow
            Try
                lobjCon.Open()
                Dim xlReader As OleDbDataReader = lobjCom.ExecuteReader
                While xlReader.Read
                    If Not IsDBNull(xlReader.Item(0)) Then
                        If (xlReader.Item(0) = mintAnio) And (xlReader.Item(1) = mintMes) Then
                            ldrFila = ldtRentabilidad.NewRow()
                            With ldrFila
                                .Item(0) = xlReader.Item(0)
                                .Item(1) = xlReader.Item(1)
                                .Item(2) = xlReader.Item(2)
                                .Item(3) = xlReader.Item(3)
                                .Item(4) = xlReader.Item(4)
                            End With
                            ldtRentabilidad.Rows.Add(ldrFila)
                            ldrFila = Nothing
                        End If
                    End If
                End While
                xlReader.Close()
                Return ldtRentabilidad
            Catch ex As Exception
                Throw ex
            Finally
                lobjCon.Close()
                lobjCon.Dispose()
                lobjCon = Nothing
            End Try
        End Function
#End Region

#Region "Obtener Data de Base de Datos"

        Public Function ObtenerCostos(ByVal penuFuente As enuTipoFuente, _
                                        ByVal penuTipoCosto As enuTipoCosto, _
                                        Optional ByVal pbooUltimo As Boolean = False) As DataTable
            Dim lstrPeriodo As String = ""
            Dim lintAnioBak As Integer
            Dim lintMesBak As Integer
            If pbooUltimo Then
                lstrPeriodo = ObtenerUltimoPeriodoCargado2(penuTipoCosto)
                If lstrPeriodo.Trim.Length = 6 Then
                    lintAnioBak = mintAnio
                    lintMesBak = mintMes
                    mintAnio = CInt(Left(lstrPeriodo, 4))
                    mintMes = CInt(Right(lstrPeriodo, 2))
                End If
            End If
            Select Case penuTipoCosto
                Case enuTipoCosto.Quintal
                    Select Case penuFuente
                        Case enuTipoFuente.BaseDatos
                            Return ObtenerCostosQuintalBD()
                        Case enuTipoFuente.XLS
                            Return ObtenerCostosQuintalXLS()
                    End Select
                Case enuTipoCosto.MateriaPrima
                    Select Case penuFuente
                        Case enuTipoFuente.BaseDatos
                            Return ObtenerCostosMateriaPrimaBD()
                        Case enuTipoFuente.XLS
                            Return ObtenerCostosmateriaprimaXLS()
                    End Select
                Case enuTipoCosto.MateriaPrimaGere
                    Select Case penuFuente
                        Case enuTipoFuente.BaseDatos
                            Return ObtenerCostosMateriaPrimaGereBD()
                        Case enuTipoFuente.XLS
                    End Select
                Case enuTipoCosto.Produccion
                    Select Case penuFuente
                        Case enuTipoFuente.BaseDatos
                            Return ObtenerCostosProduccionBD()
                        Case enuTipoFuente.XLS
                            Return ObtenerCostosProduccionXLS()
                    End Select
                Case enuTipoCosto.Urdido
                    Select Case penuFuente
                        Case enuTipoFuente.BaseDatos
                            Return ObtenerCostosUrdidoBD()
                        Case enuTipoFuente.XLS
                            Return ObtenerCostosUrdidoXLS()
                    End Select
                Case enuTipoCosto.Engomado
                    Select Case penuFuente
                        Case enuTipoFuente.BaseDatos
                            Return ObtenerCostosEngomadoBD()
                        Case enuTipoFuente.XLS
                            Return ObtenerCostosEngomadoXLS()
                    End Select
                Case enuTipoCosto.Telares
                    Select Case penuFuente
                        Case enuTipoFuente.BaseDatos
                            Return ObtenerCostosTelarBD()
                        Case enuTipoFuente.XLS
                            Return ObtenerCostosTelarXLS()
                    End Select
                Case enuTipoCosto.MaquinasTintoreria
                    Select Case penuFuente
                        Case enuTipoFuente.BaseDatos
                            Return ObtenerCostosMaquinasTintoreriaBD()
                        Case enuTipoFuente.XLS
                            Return ObtenerCostosMaquinasTintoreriaXLS()
                    End Select
                Case enuTipoCosto.RevisionFinal
                    Select Case penuFuente
                        Case enuTipoFuente.BaseDatos
                            Return ObtenerCostosRevisionFinalBD()
                        Case enuTipoFuente.XLS
                            Return ObtenerCostosRevisionFinalXLS()
                    End Select
                Case enuTipoCosto.Rentabilidad
                    Select Case penuFuente
                        Case enuTipoFuente.BaseDatos
                            Return ObtenerCostosRentabilidadBD()
                        Case enuTipoFuente.XLS
                            Return ObtenerCostosRentabilidadXLS()
                    End Select

                Case enuTipoCosto.CostoContable
                    Return ObtenerCostosContableStockXLS()

                Case enuTipoCosto.CostoEstampado
                    Return ObtenerCostoEstampadoXLS()

                Case enuTipoCosto.PreEngomado
                    Return ObtenerPreEngomadoXLS()

                Case enuTipoCosto.PreTelar
                    Return ObtenerPreTelarXLS()

                Case enuTipoCosto.PreUrdido
                    Return ObtenerPreUrdidoXLS()

            End Select
            If lstrPeriodo.Trim.Length = 6 Then
                mintAnio = lintAnioBak
                mintMes = lintMesBak
            End If
        End Function

        Private Function ObtenerCostosQuintalBD() As DataTable
            Dim lobjCon As NM.AccesoDatos.AccesoDatosSQLServer
            Dim ldtRes As DataTable
            Try
                Dim lstrParametros() As String = {"sin_Anno", mintAnio, _
                                                    "sin_Mes", mintMes}
                lobjCon = New NM.AccesoDatos.AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.FichaCosto)
                ldtRes = lobjCon.ObtenerDataTable("usp_COS_CostosQuintal_Listar", lstrParametros)
                ldtRes.TableName = "CostosQuintal"
                Return ldtRes
            Catch ex As Exception
                Throw ex
            Finally
                lobjCon = Nothing
            End Try
        End Function

        Private Function ObtenerCostosMateriaPrimaBD() As DataTable
            Dim lobjCon As NM.AccesoDatos.AccesoDatosSQLServer
            Dim ldtRes As DataTable
            Try
                Dim lstrParametros() As String = {"sin_Anno", mintAnio, _
                                                    "sin_Mes", mintMes}
                lobjCon = New NM.AccesoDatos.AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.FichaCosto)
                ldtRes = lobjCon.ObtenerDataTable("usp_COS_CostosMateriaPrima_Listar", lstrParametros)
                ldtRes.TableName = "CostosMateriaPrima"
                Return ldtRes
            Catch ex As Exception
                Throw ex
            Finally
                lobjCon = Nothing
            End Try
        End Function

        Private Function ObtenerCostosMateriaPrimaGereBD() As DataTable
            Dim lobjCon As NM.AccesoDatos.AccesoDatosSQLServer
            Dim ldtRes As DataTable
            Try
                Dim lstrParametros() As String = {"sin_Anno", mintAnio, _
                                                    "sin_Mes", mintMes}
                lobjCon = New NM.AccesoDatos.AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.FichaCosto)
                ldtRes = lobjCon.ObtenerDataTable("usp_COS_CostosMateriaPrimaGere_Listar", lstrParametros)
                ldtRes.TableName = "CostosMateriaPrimaGere"
                Return ldtRes
            Catch ex As Exception
                Throw ex
            Finally
                lobjCon = Nothing
            End Try
        End Function

        Private Function ObtenerCostosProduccionBD() As DataTable
            Dim lobjCon As NM.AccesoDatos.AccesoDatosSQLServer
            Dim ldtRes As DataTable
            Try
                Dim lstrParametros() As String = {"sin_Anno", mintAnio, _
                                                    "sin_Mes", mintMes}
                lobjCon = New NM.AccesoDatos.AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.FichaCosto)
                ldtRes = lobjCon.ObtenerDataTable("usp_COS_CostosProduccion_Listar", lstrParametros)
                ldtRes.TableName = "CostosProduccion"
                Return ldtRes
            Catch ex As Exception
                Throw ex
            Finally
                lobjCon = Nothing
            End Try
        End Function

        Private Function ObtenerCostosUrdidoBD() As DataTable
            Dim lobjCon As NM.AccesoDatos.AccesoDatosSQLServer
            Dim ldtRes As DataTable
            Try
                Dim lstrParametros() As String = {"sin_Anno", mintAnio, _
                                                    "sin_Mes", mintMes}
                lobjCon = New NM.AccesoDatos.AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.FichaCosto)
                ldtRes = lobjCon.ObtenerDataTable("usp_COS_CostosUrdido_Listar", lstrParametros)
                ldtRes.TableName = "CostosUrdido"
                Return ldtRes
            Catch ex As Exception
                Throw ex
            Finally
                lobjCon = Nothing
            End Try
        End Function

        Private Function ObtenerCostosEngomadoBD() As DataTable
            Dim lobjCon As NM.AccesoDatos.AccesoDatosSQLServer
            Dim ldtRes As DataTable
            Try
                Dim lstrParametros() As String = {"sin_Anno", mintAnio, _
                                                    "sin_Mes", mintMes}
                lobjCon = New NM.AccesoDatos.AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.FichaCosto)
                ldtRes = lobjCon.ObtenerDataTable("usp_COS_CostosEngomado_Listar", lstrParametros)
                ldtRes.TableName = "CostosEngomado"
                Return ldtRes
            Catch ex As Exception
                Throw ex
            Finally
                lobjCon = Nothing
            End Try
        End Function

        Private Function ObtenerCostosTelarBD() As DataTable
            Dim lobjCon As NM.AccesoDatos.AccesoDatosSQLServer
            Dim ldtRes As DataTable
            Try
                Dim lstrParametros() As String = {"sin_Anno", mintAnio, _
                                                    "sin_Mes", mintMes}
                lobjCon = New NM.AccesoDatos.AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.FichaCosto)
                ldtRes = lobjCon.ObtenerDataTable("usp_COS_CostosTelar_Listar", lstrParametros)
                ldtRes.TableName = "CostosTelar"
                Return ldtRes
            Catch ex As Exception
                Throw ex
            Finally
                lobjCon = Nothing
            End Try
        End Function

        Private Function ObtenerCostosMaquinasTintoreriaBD() As DataTable
            Dim lobjCon As NM.AccesoDatos.AccesoDatosSQLServer
            Dim ldtRes As DataTable
            Try
                Dim lstrParametros() As String = {"sin_Anno", mintAnio, _
                                                    "sin_Mes", mintMes}
                lobjCon = New NM.AccesoDatos.AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.FichaCosto)
                ldtRes = lobjCon.ObtenerDataTable("usp_COS_CostosMaquinaTintoreria_Listar", lstrParametros)
                ldtRes.TableName = "CostosMaquinaTintoreria"
                Return ldtRes
            Catch ex As Exception
                Throw ex
            Finally
                lobjCon = Nothing
            End Try
        End Function

        Private Function ObtenerCostosRevisionFinalBD() As DataTable
            Dim lobjCon As NM.AccesoDatos.AccesoDatosSQLServer
            Dim ldtRes As DataTable
            Try
                Dim lstrParametros() As String = {"sin_Anno", mintAnio, _
                                                    "sin_Mes", mintMes}
                lobjCon = New NM.AccesoDatos.AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.FichaCosto)
                ldtRes = lobjCon.ObtenerDataTable("usp_COS_CostosRevisionFinal_Listar", lstrParametros)
                ldtRes.TableName = "CostosRevisionFinal"
                Return ldtRes
            Catch ex As Exception
                Throw ex
            Finally
                lobjCon = Nothing
            End Try
        End Function

        Public Function ObtenerCostosRentabilidadBD() As DataTable
            Dim lobjCon As NM.AccesoDatos.AccesoDatosSQLServer
            Dim ldtRes As DataTable
            Try
                Dim lstrParametros() As String = {"sin_Anno", mintAnio, _
                                                    "sin_Mes", mintMes}
                lobjCon = New NM.AccesoDatos.AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.FichaCosto)
                ldtRes = lobjCon.ObtenerDataTable("usp_COS_CostosRentabilidad_Listar", lstrParametros)
                ldtRes.TableName = "CostosRentabilidad"
                Return ldtRes
            Catch ex As Exception
                Throw ex
            Finally
                lobjCon = Nothing
            End Try
        End Function

        ' --------------------------------------------------------------------
        ' Modificado: Mayo 2013
        ' Alexander Torres
        ' Objetivo: Modificar el calculo e la ficha de costos para los telares
        ' Lista costo promedio de telares
        ' --------------------------------------------------------------------

        Public Function CostosPromedioTelares_Listar() As DataTable
            Dim ldtbCosProTel As New DataTable
            Dim strqry As String = ""
            Dim mobjConexion As NM.AccesoDatos.AccesoDatosSQLServer
            Try
                ldtbCosProTel = Nothing
                strqry = "usp_costos_CostoArticuloTelar_Listar"
                Dim lstrParametros() As String = {"int_Anno", mintAnio, "int_Mes", mintMes}
                mobjConexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.FichaCosto)
                ldtbCosProTel = mobjConexion.ObtenerDataTable(strqry, lstrParametros)
            Catch ex As Exception
                Throw
            End Try
            Return ldtbCosProTel
        End Function

        ' Guardar costo promedio de telares
        Public Function CostosPromedioTelares_Guardar(intPasadasPorPulgada As Double, dblEficPromMensual As Double, _
                                                          dblManoObraVariable As Double, dblCostoVariable As Double, _
                                                          dblOtroGastoFijo As Double, dblManoObraFija As Double) As Boolean
            Dim blnGuardar As Boolean = False
            Dim strqry As String = ""
            Dim mobjConexion As NM.AccesoDatos.AccesoDatosSQLServer
            Try
                Dim lstrParametros() As String = {"int_Anno", mintAnio, _
                                                  "int_Mes", mintMes, _
                                                  "int_PasadasPorPulgada", intPasadasPorPulgada, _
                                                  "num_EficienciaPromMen", dblEficPromMensual, _
                                                  "num_ManoObraVariable", dblManoObraVariable, _
                                                  "num_CostoVariable", dblCostoVariable, _
                                                  "num_OtroGastoFijo", dblOtroGastoFijo, _
                                                  "num_ManoObraFija", dblManoObraFija, _
                                                  "vch_Usuario", Usuario}
                strqry = "usp_costos_CostoArticuloTelar_Agregar_2"
                mobjConexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.FichaCosto)
                mobjConexion.EjecutarComando(strqry, lstrParametros)
                blnGuardar = True
            Catch ex As Exception
                Throw
            End Try
            Return blnGuardar
        End Function

        'REQSIS201900060 -DG - INI
        Public Function CostosPromedioTelares_Guardar_V2(intPasadasPorPulgada As Double, dblEficPromMensual As Double, dblVelSum As Double, _
                                                         dblManoObraVariable As Double, dblCostoVariable As Double, _
                                                         dblOtroGastoFijo As Double, dblManoObraFija As Double, dblFv As Double) As Boolean
            Dim blnGuardar As Boolean = False
            Dim strqry As String = ""
            Dim mobjConexion As NM.AccesoDatos.AccesoDatosSQLServer
            Try
                Dim lstrParametros() As String = {"int_Anno", mintAnio, _
                                                  "int_Mes", mintMes, _
                                                  "int_PasadasPorPulgada", intPasadasPorPulgada, _
                                                  "num_EficienciaPromMen", dblEficPromMensual, _
                                                  "num_VelSum", dblVelSum, _
                                                  "num_ManoObraVariable", dblManoObraVariable, _
                                                  "num_CostoVariable", dblCostoVariable, _
                                                  "num_OtroGastoFijo", dblOtroGastoFijo, _
                                                  "num_ManoObraFija", dblManoObraFija, _
                                                  "vch_Usuario", Usuario, _
                                                  "num_FactorVariable", dblFv}
                strqry = "usp_costos_CostoArticuloTelar_Agregar_2_V2"
                mobjConexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.FichaCosto)
                mobjConexion.EjecutarComando(strqry, lstrParametros)
                blnGuardar = True
            Catch ex As Exception
                Throw
            End Try
            Return blnGuardar
        End Function
        Public Function CostosPromedioTelares_Listar_V2() As DataTable
            Dim ldtbCosProTel As New DataTable
            Dim strqry As String = ""
            Dim mobjConexion As NM.AccesoDatos.AccesoDatosSQLServer
            Try
                ldtbCosProTel = Nothing
                strqry = "usp_costos_CostoArticuloTelar_Listar_V2"
                Dim lstrParametros() As String = {"int_Anno", mintAnio, "int_Mes", mintMes}
                mobjConexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.FichaCosto)
                ldtbCosProTel = mobjConexion.ObtenerDataTable(strqry, lstrParametros)
            Catch ex As Exception
                Throw
            End Try
            Return ldtbCosProTel
        End Function
        
        'REQSIS201900060 -DG - FIN
#End Region

#Region "Obtener Ultimo Periodo Cargado"

        Public Function ObtenerResumenUltimaCarga() As DataTable
            Try
                Dim lobjCon As NM.AccesoDatos.AccesoDatosSQLServer
                lobjCon = New NM.AccesoDatos.AccesoDatosSQLServer(NM.AccesoDatos.GeneradorCadenaConexion.enmBasesDatos.FichaCosto)
                Return lobjCon.ObtenerDataTable("USP_COS_CARGA_RESUMEN")
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function ObtenerUltimoPeriodoCargado2(ByVal pintTipo As enuTipoCosto) As String
            Dim lstrTipo As String
            Dim lobjCon As NM.AccesoDatos.AccesoDatosSQLServer
            Dim lstrPeriodo As String = ""
            Select Case pintTipo
                Case enuTipoCosto.Quintal
                    lstrTipo = "CQ"
                Case enuTipoCosto.MateriaPrima
                    lstrTipo = "MP"
                Case enuTipoCosto.MateriaPrimaGere
                    lstrTipo = "MPG"
                Case enuTipoCosto.Produccion
                    lstrTipo = "CP"
                Case enuTipoCosto.Urdido
                    lstrTipo = "CU"
                Case enuTipoCosto.Engomado
                    lstrTipo = "CE"
                Case enuTipoCosto.Telares
                    lstrTipo = "CT"
                Case enuTipoCosto.MaquinasTintoreria
                    lstrTipo = "MT"
                Case enuTipoCosto.RevisionFinal
                    lstrTipo = "RF"
                Case enuTipoCosto.Rentabilidad
                    lstrTipo = "CR"
            End Select
            Try
                lobjCon = New NM.AccesoDatos.AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.FichaCosto)
                Dim lstrParametros() As String = {"var_Tipo", lstrTipo}
                lstrPeriodo = lobjCon.ObtenerValor("usp_COS_Costos_UltimoPeriodo", lstrParametros)
                Return lstrPeriodo
            Catch ex As Exception

            Finally
                lobjCon = Nothing
            End Try
        End Function

        Public Function ObtenerUltimoPeriodoCargado(ByVal pintTipo As enuTipoCosto) As String
            Dim lstrTipo As String
            Dim lobjCon As NM.AccesoDatos.AccesoDatosSQLServer
            Dim lstrPeriodo As String = ""
            Select Case pintTipo
                Case enuTipoCosto.Quintal
                    lstrTipo = "CQ"
                Case enuTipoCosto.MateriaPrima
                    lstrTipo = "MP"
                Case enuTipoCosto.Produccion
                    lstrTipo = "CP"
                Case enuTipoCosto.Urdido
                    lstrTipo = "CU"
                Case enuTipoCosto.Engomado
                    lstrTipo = "CE"
                Case enuTipoCosto.Telares
                    lstrTipo = "CT"
                Case enuTipoCosto.MaquinasTintoreria
                    lstrTipo = "MT"
                Case enuTipoCosto.RevisionFinal
                    lstrTipo = "RF"
                Case enuTipoCosto.Rentabilidad
                    lstrTipo = "CR"
            End Select
            Try
                lobjCon = New NM.AccesoDatos.AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.FichaCosto)
                Dim lstrParametros() As String = {"var_Tipo", lstrTipo}
                lstrPeriodo = lobjCon.ObtenerValor("usp_FCO_Costos_UltimoCarga", lstrParametros)
                Return lstrPeriodo
            Catch ex As Exception

            Finally
                lobjCon = Nothing
            End Try
        End Function

#End Region

#Region "Insertar data"
        Public Function InsertarCostos(ByRef pdtCostos As DataTable, ByVal pintTipo As enuTipoCosto) As Boolean
            Dim lobjCon As NM.AccesoDatos.AccesoDatosSQLServer
            Dim lstrProcedure As String = ""
            Dim lbooOk As Boolean = False
            Dim lobjUtil As NM_General.Util
            mstrError = ""
            Select Case pintTipo
                Case enuTipoCosto.Quintal
                    lstrProcedure = "usp_COS_CostosQuintal_Cargar"
                Case enuTipoCosto.MateriaPrima
                    lstrProcedure = "usp_COS_CostosMateriaPrima_Cargar"
                Case enuTipoCosto.Produccion
                    lstrProcedure = "usp_COS_CostosProduccion_Cargar"
                Case enuTipoCosto.Urdido
                    lstrProcedure = "usp_COS_CostosUrdido_Cargar"
                Case enuTipoCosto.Engomado
                    lstrProcedure = "usp_COS_CostosEngomado_Cargar"
                Case enuTipoCosto.Telares
                    lstrProcedure = "usp_COS_CostosTelar_Cargar"
                Case enuTipoCosto.MaquinasTintoreria
                    lstrProcedure = "usp_COS_CostosMaquinaTintoreria_Cargar"
                Case enuTipoCosto.RevisionFinal
                    lstrProcedure = "usp_COS_CostosRevisionFinal_Cargar"
                Case enuTipoCosto.Rentabilidad
                    lstrProcedure = "usp_COS_CostosRentabilidad_Cargar"
            End Select
            If lstrProcedure.Trim.Length > 0 Then
                Try
                    lobjCon = New NM.AccesoDatos.AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.FichaCosto)
                    lobjUtil = New NM_General.Util
                    Dim lstrParametros() As String = {"sin_Anio", mintAnio, _
                                                    "sin_Mes", mintMes, _
                                                    "ntx_Costos", lobjUtil.GeneraXml(pdtCostos), _
                                                    "var_Usuario", mstrUsuario} ' "SA"}
                    lobjUtil = Nothing
                    lobjCon.EjecutarComando(lstrProcedure, lstrParametros)
                    lbooOk = True
                Catch ex As Exception
                    mstrError = ex.Message
                    lbooOk = False
                Finally
                    lobjUtil = Nothing
                    lobjCon = Nothing
                End Try
                Return lbooOk
            End If
        End Function

        Public Function InsertarCostoMateriaPrima(ByVal pdtCostos As DataTable) As Boolean
            Dim lobjCon As NM.AccesoDatos.AccesoDatosSQLServer
            Dim lobjUtil As NM_General.Util
            Try
                Dim dtbAuxiliar As DataTable = pdtCostos.Clone
                Dim NumeroFilas As Int16 = pdtCostos.Rows.Count
                dtbAuxiliar.TableName = "CostosMateriaPrima2"
                If pdtCostos.Rows.Count > 50 Then
                    Dim Indice As Int16 = 50

                    For Indice = 50 To NumeroFilas - 1
                        dtbAuxiliar.LoadDataRow(pdtCostos.Rows(Indice).ItemArray, True)
                        dtbAuxiliar.AcceptChanges()
                    Next
                    Indice = 50
                    Do While (Indice < pdtCostos.Rows.Count)
                        pdtCostos.Rows(Indice).Delete()
                        pdtCostos.AcceptChanges()
                    Loop
                End If

                lobjCon = New NM.AccesoDatos.AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.FichaCosto)
                lobjUtil = New NM_General.Util
                Dim lstrParametros() As String = {"sin_Anio", mintAnio, _
                                                "sin_Mes", mintMes, _
                                                "ntx_XMLDatos1", lobjUtil.GeneraXml(pdtCostos), _
                                                "ntx_XMLDatos2", lobjUtil.GeneraXml(dtbAuxiliar), _
                                                "int_Version", mintVersion, _
                                                "var_Usuario", mstrUsuario} ' "SA"}
                lobjUtil = Nothing
                lobjCon.EjecutarComando("usp_COS_CostosMateriaPrima_Registrar", lstrParametros)
                Return True
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function InsertarCostoContableStock(ByVal pdtCostosContable As DataTable) As Boolean
            Dim lobjCon As NM.AccesoDatos.AccesoDatosSQLServer
            Dim lobjUtil As NM_General.Util
            Try
                Dim dtbAuxiliar As DataTable = pdtCostosContable.Clone
                Dim NumeroFilas As Int16 = pdtCostosContable.Rows.Count
                dtbAuxiliar.TableName = "CostosContable2"
                If pdtCostosContable.Rows.Count > 50 Then
                    Dim Indice As Int16 = 50

                    For Indice = 50 To NumeroFilas - 1
                        dtbAuxiliar.LoadDataRow(pdtCostosContable.Rows(Indice).ItemArray, True)
                        dtbAuxiliar.AcceptChanges()
                    Next
                    Indice = 50
                    Do While (Indice < pdtCostosContable.Rows.Count)
                        pdtCostosContable.Rows(Indice).Delete()
                        pdtCostosContable.AcceptChanges()
                    Loop
                End If

                lobjCon = New NM.AccesoDatos.AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.FichaCosto)
                lobjUtil = New NM_General.Util
                Dim ntx_XMLDatos1 As String
                Dim ntx_XMLDatos2 As String

                ntx_XMLDatos1 = lobjUtil.GeneraXml(pdtCostosContable)
                ntx_XMLDatos2 = lobjUtil.GeneraXml(dtbAuxiliar)


                Dim lstrParametros() As String = {"int_Anio", mintAnio, _
                                                "int_Mes", mintMes, _
                                                "ntx_XMLDatos1", ntx_XMLDatos1, _
                                                "ntx_XMLDatos2", ntx_XMLDatos2, _
                                                "var_Usuario", mstrUsuario} ' "SA"}
                lobjUtil = Nothing
                lobjCon.EjecutarComando("usp_COS_CostosContableStock_Registrar", lstrParametros)
                Return True
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function InsertarCostoEstampado(ByVal pdtCostoEstampado As DataTable) As Boolean
            Dim lobjCon As NM.AccesoDatos.AccesoDatosSQLServer
            Dim lobjUtil As NM_General.Util
            Try
                Dim dtbAuxiliar As DataTable = pdtCostoEstampado.Clone
                Dim NumeroFilas As Int16 = pdtCostoEstampado.Rows.Count
                dtbAuxiliar.TableName = "CostoEstampado2"
                If pdtCostoEstampado.Rows.Count > 50 Then
                    Dim Indice As Int16 = 50

                    For Indice = 50 To NumeroFilas - 1
                        dtbAuxiliar.LoadDataRow(pdtCostoEstampado.Rows(Indice).ItemArray, True)
                        dtbAuxiliar.AcceptChanges()
                    Next
                    Indice = 50
                    Do While (Indice < pdtCostoEstampado.Rows.Count)
                        pdtCostoEstampado.Rows(Indice).Delete()
                        pdtCostoEstampado.AcceptChanges()
                    Loop
                End If

                lobjCon = New NM.AccesoDatos.AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.FichaCosto)
                lobjUtil = New NM_General.Util
                Dim ntx_XMLDatos1 As String
                Dim ntx_XMLDatos2 As String

                ntx_XMLDatos1 = lobjUtil.GeneraXml(pdtCostoEstampado)
                ntx_XMLDatos2 = lobjUtil.GeneraXml(dtbAuxiliar)


                Dim lstrParametros() As String = {"int_Anio", mintAnio, _
                                                "int_Mes", mintMes, _
                                                "ntx_XMLDatos1", ntx_XMLDatos1, _
                                                "ntx_XMLDatos2", ntx_XMLDatos2, _
                                                "var_Usuario", mstrUsuario} ' "SA"}
                lobjUtil = Nothing
                lobjCon.EjecutarComando("utb_CostosEstampado_Registrar", lstrParametros)
                Return True
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function InsertarPreEngomado(ByVal pdtPreEngomado As DataTable) As Boolean
            Dim lobjCon As NM.AccesoDatos.AccesoDatosSQLServer
            Dim lobjUtil As NM_General.Util
            Try
                Dim dtbAuxiliar As DataTable = pdtPreEngomado.Clone
                Dim NumeroFilas As Int16 = pdtPreEngomado.Rows.Count
                dtbAuxiliar.TableName = "PreEngomado2"
                If pdtPreEngomado.Rows.Count > 50 Then
                    Dim Indice As Int16 = 50

                    For Indice = 50 To NumeroFilas - 1
                        dtbAuxiliar.LoadDataRow(pdtPreEngomado.Rows(Indice).ItemArray, True)
                        dtbAuxiliar.AcceptChanges()
                    Next
                    Indice = 50
                    Do While (Indice < pdtPreEngomado.Rows.Count)
                        pdtPreEngomado.Rows(Indice).Delete()
                        pdtPreEngomado.AcceptChanges()
                    Loop
                End If

                lobjCon = New NM.AccesoDatos.AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.FichaCosto)
                lobjUtil = New NM_General.Util
                Dim ntx_XMLDatos1 As String
                Dim ntx_XMLDatos2 As String

                ntx_XMLDatos1 = lobjUtil.GeneraXml(pdtPreEngomado)
                ntx_XMLDatos2 = lobjUtil.GeneraXml(dtbAuxiliar)


                Dim lstrParametros() As String = {"int_Anio", mintAnio, _
                                                "int_Mes", mintMes, _
                                                "ntx_XMLDatos1", ntx_XMLDatos1, _
                                                "ntx_XMLDatos2", ntx_XMLDatos2, _
                                                "var_Usuario", mstrUsuario} ' "SA"}
                lobjUtil = Nothing
                lobjCon.EjecutarComando("usp_PreCos_Engomado_Registrar", lstrParametros)
                Return True
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function InsertarPreTelar(ByVal pdtPreTelar As DataTable) As Boolean
            Dim lobjCon As NM.AccesoDatos.AccesoDatosSQLServer
            Dim lobjUtil As NM_General.Util
            Try
                Dim dtbAuxiliar As DataTable = pdtPreTelar.Clone
                Dim NumeroFilas As Int16 = pdtPreTelar.Rows.Count
                dtbAuxiliar.TableName = "PreTelar2"
                If pdtPreTelar.Rows.Count > 50 Then
                    Dim Indice As Int16 = 50

                    For Indice = 50 To NumeroFilas - 1
                        dtbAuxiliar.LoadDataRow(pdtPreTelar.Rows(Indice).ItemArray, True)
                        dtbAuxiliar.AcceptChanges()
                    Next
                    Indice = 50
                    Do While (Indice < pdtPreTelar.Rows.Count)
                        pdtPreTelar.Rows(Indice).Delete()
                        pdtPreTelar.AcceptChanges()
                    Loop
                End If

                lobjCon = New NM.AccesoDatos.AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.FichaCosto)
                lobjUtil = New NM_General.Util
                Dim ntx_XMLDatos1 As String
                Dim ntx_XMLDatos2 As String

                ntx_XMLDatos1 = lobjUtil.GeneraXml(pdtPreTelar)
                ntx_XMLDatos2 = lobjUtil.GeneraXml(dtbAuxiliar)


                Dim lstrParametros() As String = {"int_Anio", mintAnio, _
                                                "int_Mes", mintMes, _
                                                "ntx_XMLDatos1", ntx_XMLDatos1, _
                                                "ntx_XMLDatos2", ntx_XMLDatos2, _
                                                "var_Usuario", mstrUsuario} ' "SA"}
                lobjUtil = Nothing
                lobjCon.EjecutarComando("usp_PreCos_Telar_Registrar", lstrParametros)
                Return True
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function InsertarPreUrdido(ByVal pdtPreUrdido As DataTable) As Boolean
            Dim lobjCon As NM.AccesoDatos.AccesoDatosSQLServer
            Dim lobjUtil As NM_General.Util
            Try
                Dim dtbAuxiliar As DataTable = pdtPreUrdido.Clone
                Dim NumeroFilas As Int16 = pdtPreUrdido.Rows.Count
                dtbAuxiliar.TableName = "PreUrdido2"
                If pdtPreUrdido.Rows.Count > 50 Then
                    Dim Indice As Int16 = 50

                    For Indice = 50 To NumeroFilas - 1
                        dtbAuxiliar.LoadDataRow(pdtPreUrdido.Rows(Indice).ItemArray, True)
                        dtbAuxiliar.AcceptChanges()
                    Next
                    Indice = 50
                    Do While (Indice < pdtPreUrdido.Rows.Count)
                        pdtPreUrdido.Rows(Indice).Delete()
                        pdtPreUrdido.AcceptChanges()
                    Loop
                End If

                lobjCon = New NM.AccesoDatos.AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.FichaCosto)
                lobjUtil = New NM_General.Util
                Dim ntx_XMLDatos1 As String
                Dim ntx_XMLDatos2 As String

                ntx_XMLDatos1 = lobjUtil.GeneraXml(pdtPreUrdido)
                ntx_XMLDatos2 = lobjUtil.GeneraXml(dtbAuxiliar)


                Dim lstrParametros() As String = {"int_Anio", mintAnio, _
                                                "int_Mes", mintMes, _
                                                "ntx_XMLDatos1", ntx_XMLDatos1, _
                                                "ntx_XMLDatos2", ntx_XMLDatos2, _
                                                "var_Usuario", mstrUsuario} ' "SA"}
                lobjUtil = Nothing
                lobjCon.EjecutarComando("usp_PreCos_Urdido_Registrar", lstrParametros)
                Return True
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function fValidaCostoMateriaPrima() As Boolean
            Dim lobjCon As NM.AccesoDatos.AccesoDatosSQLServer
            Dim lobjUtil As NM_General.Util
            Dim objDT As New DataTable
            Try

                'Verifica si hay registros con version 1 (Produccion)

                lobjCon = New NM.AccesoDatos.AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.FichaCosto)
                lobjUtil = New NM_General.Util
                Dim lstrParametros() As String = {"sin_Anio", mintAnio, _
                                                  "sin_Mes", mintMes}
                lobjUtil = Nothing
                objDT = lobjCon.ObtenerDataTable("usp_CostosMateriaPrima_Validar", lstrParametros)

                If objDT.Rows.Count > 0 Then
                    fValidaCostoMateriaPrima = True
                Else
                    fValidaCostoMateriaPrima = False

                End If

            Catch ex As Exception
                Throw ex
            End Try
        End Function

        'Importación de costos calculados en mpodulo de hilandería
        Public Function ImportarCostosQuintal() As Boolean
            Dim lobjCon As NM.AccesoDatos.AccesoDatosSQLServer
            Try


                lobjCon = New NM.AccesoDatos.AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.FichaCosto)

                Dim lstrParametros() As String = {"int_PeriodoAno", mintAnio, _
                                                  "int_PeriodoMes", mintMes, _
                                                  "vch_Usuario", mstrUsuario}
                lobjCon.EjecutarComando("usp_COS_CostosQuintal_Importar", lstrParametros)
                Return True
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function ImportarCostosMP() As Boolean
            Dim lobjCon As NM.AccesoDatos.AccesoDatosSQLServer
            Try


                lobjCon = New NM.AccesoDatos.AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.FichaCosto)

                Dim lstrParametros() As String = {"int_PeriodoAno", mintAnio, _
                                                  "int_PeriodoMes", mintMes, _
                                                  "vch_Usuario", mstrUsuario}
                lobjCon.EjecutarComando("usp_COS_CostosMateriaPrima_Importar", lstrParametros)
                Return True
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function ImportarCostosProduccion() As Boolean
            Dim lobjCon As NM.AccesoDatos.AccesoDatosSQLServer
            Try


                lobjCon = New NM.AccesoDatos.AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.FichaCosto)

                Dim lstrParametros() As String = {"int_PeriodoAno", mintAnio, _
                                                  "int_PeriodoMes", mintMes, _
                                                  "vch_Usuario", mstrUsuario}
                lobjCon.EjecutarComando("usp_COS_CostosProduccion_Importar", lstrParametros)
                Return True
            Catch ex As Exception
                Throw ex
            End Try
        End Function

#End Region

    End Class

End Namespace
