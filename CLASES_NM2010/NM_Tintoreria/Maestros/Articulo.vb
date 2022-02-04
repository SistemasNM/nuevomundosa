Imports NM.AccesoDatos
Namespace Maestros
    Public Class Articulo
        Implements IDisposable

        Structure stuItem
            Public Codigo As String
            Public Descripcion As String
        End Structure
        Structure stuFiltroBusqueda
            Public Articulo As stuItem
            Public Colorante As stuItem
            Public Color As stuItem
            Public Disenio As stuItem
            Public Combinacion As stuItem
            Public ColoranteEstampado As stuItem
        End Structure

#Region "   Variables"
        Private mstrCodigoArticulo As String
        Private mstrCodigoLargo As String
        Private mstrCodigoOFISIS As String
        Private mobjColor As Color
        Private mobjTipoColorante As TipoColorante
        Private mobjAcabado As Acabado
        Private mobjRuta As Ruta
        Private mstrNombre As String
        Private mstrUsuario As String
        Private mstrMarca As String
#End Region

#Region "   Constructor"
        Sub New(ByVal strUsuario As String)
            mstrUsuario = strUsuario
        End Sub
        Sub New(ByVal strUsuario As String, ByVal strCodigoArticulo As String)
            mstrUsuario = strUsuario
            mstrCodigoArticulo = strCodigoArticulo
        End Sub
#End Region

#Region "   Propiedades"
        Public Property Codigo() As String
            Get
                Codigo = mstrCodigoArticulo
            End Get
            Set(ByVal Value As String)
                mstrCodigoArticulo = Value
            End Set
        End Property
        Public ReadOnly Property CodigoLargo() As String
            Get
                CodigoLargo = mstrCodigoLargo
            End Get
        End Property
        Public ReadOnly Property CodigoOFISIS() As String
            Get
                CodigoOFISIS = mstrCodigoOFISIS
            End Get
        End Property
        Public Property Nombre() As String
            Get
                Nombre = mstrNombre
            End Get
            Set(ByVal Value As String)
                mstrNombre = Value
            End Set
        End Property
        Public Property Marca() As String
            Get
                Marca = mstrMarca
            End Get
            Set(ByVal Value As String)
                mstrMarca = Value
            End Set
        End Property
        Public ReadOnly Property Color() As Color
            Get
                Color = mobjColor
            End Get
        End Property
        Public ReadOnly Property TipoColorante() As TipoColorante
            Get
                TipoColorante = mobjTipoColorante
            End Get
        End Property
        Public ReadOnly Property Acabado() As Acabado
            Get
                Acabado = mobjAcabado
            End Get
        End Property
        Public ReadOnly Property Ruta() As Ruta
            Get
                Ruta = mobjRuta
            End Get

        End Property
        Public Property Usuario() As String
            Get
                Usuario = mstrUsuario
            End Get
            Set(ByVal Value As String)
                mstrUsuario = Value
            End Set
        End Property
#End Region

#Region "   Metodos"
        Public Function Listar(ByVal stuFiltros As stuFiltroBusqueda) As DataTable
            Dim ldtDatos As DataTable
            Dim lobjTinto As New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.Tintoreria)
            Try
                With stuFiltros
                    Dim lstrParametros() As String = { _
                            "var_CodigoArticulo", .Articulo.Codigo, _
                            "var_NombreArticulo", .Articulo.Descripcion, _
                            "var_CodigoColorante", .Colorante.Codigo, _
                            "var_NombreColorante", .Colorante.Descripcion, _
                            "var_CodigoColor", .Color.Codigo, _
                            "var_NombreColor", .Color.Descripcion, _
                            "var_CodigoDiseno", .Disenio.Codigo, _
                            "var_NombreDiseno", .Disenio.Descripcion, _
                            "var_CodigoCombinacion", .Combinacion.Codigo, _
                            "var_NombreCombinacion", .Combinacion.Descripcion, _
                            "var_CodigoColoranteEstampado", .ColoranteEstampado.Codigo, _
                            "var_NombreColoranteEstampado", .ColoranteEstampado.Descripcion}
                    ldtDatos = lobjTinto.ObtenerDataTable("usp_TIN_Articulos_Lista", lstrParametros)
                End With
            Catch ex As Exception
                Throw ex
            End Try

            Return ldtDatos
        End Function
        Public Function Buscar()
            Dim lobjTinto As New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.Tintoreria)
            Dim ldsDatos As DataSet
            Try
                Dim lstrParametros() As String = {"var_CodigoArticulo", mstrCodigoArticulo}
                ldsDatos = lobjTinto.ObtenerDataSet("usp_TIN_Articulo_Buscar", lstrParametros)
                With ldsDatos.Tables(0).Rows(0)
                    mstrCodigoLargo = .Item("codigo_articulo_largo")
                    mstrCodigoOFISIS = .Item("codigo_articulo_ofisis")

                    mobjColor = New Color(mstrUsuario)
                    mobjColor.Codigo = .Item("codigo_color")
                    mobjColor.Nombre = .Item("descripcion_color")

                    mobjTipoColorante = New TipoColorante(mstrUsuario)
                    mobjTipoColorante.Codigo = .Item("codigo_tipo_colorante")
                    mobjTipoColorante.Nombre = .Item("descripcion_tipo_colorante")

                    mobjAcabado = New Acabado(mstrUsuario)
                    mobjAcabado.Codigo = .Item("codigo_acabado")
                    mobjAcabado.Nombre = .Item("descripcion_acabado")
                    mstrMarca = .Item("marca")
                    mstrNombre = .Item("descripcion_articulo")
                End With
                mobjRuta = New Ruta(mstrUsuario, mstrCodigoLargo)
                mobjRuta.Detalle = ldsDatos.Tables(1)
            Catch ex As Exception
                Throw ex
            Finally
                lobjTinto = Nothing
                ldsDatos = Nothing
            End Try
        End Function

        Public Function Listar() As DataTable
            Dim ldtDatos As DataTable
            Dim lobjTinto As New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.Tintoreria)
            Try
                ldtDatos = lobjTinto.ObtenerDataTable("pr_NM_ArticulosOfisisMaestroRutas_Select")
            Catch ex As Exception
                Throw ex
            End Try

            Return ldtDatos
        End Function
        Public Function Listar_Encogimientos(ByVal strArticulo As String) As DataTable
            Dim ldtDatos As DataTable
            Dim lobjTinto As New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.Tintoreria)
            Dim objParametros() As Object = {"Var_CodigoArticulo", strArticulo}
            Try
                ldtDatos = lobjTinto.ObtenerDataTable("USP_TIN_LISTADO_ENCOGIMIENTOS", objParametros)
            Catch ex As Exception
                Throw ex
            End Try
            Return ldtDatos
        End Function
        Public Function Listar(ByVal pArticulo As String, ByVal pCodigoSubproceso As String) As DataTable
            Dim dtDatos As DataTable
            Dim lobjTinto As New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.Tintoreria)
            Dim objParametros() As Object = {"codigo_articulo", pArticulo, "codigo_subproceso", pCodigoSubproceso}
            Try
                dtDatos = lobjTinto.ObtenerDataTable("pr_NM_RutaProduccion_Select", objParametros)
            Catch ex As Exception
                Throw ex
            End Try
            Return dtDatos
        End Function
        Public Function ObtenerNumeroRevision(ByVal strArticulo As String) As String
            Dim pResultado As String
            Dim lobjTinto As New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.Tintoreria)

            Dim objParametros() As Object = {"codigo_articulo", strArticulo}
            Try
                pResultado = lobjTinto.ObtenerValor("pr_NM_RutaProduccion_Revision", objParametros).ToString
            Catch ex As Exception
                Throw ex
            End Try

            Return pResultado
        End Function
        Public Function ActualizaEstado(ByVal pArticulo As String, ByVal strEstado As String, ByVal strUsuario As String) As Boolean
            Dim objParametros() As Object = {"codigo_articulo", pArticulo, "estado_articulo", strEstado, "usuario_modificacion", strUsuario}
            Dim lobjTinto As New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.Tintoreria)
            Try
                lobjTinto.EjecutarComando("usp_TINT_Actualiza_EstadoArticulo", objParametros).ToString()
            Catch ex As Exception
                Throw ex
            End Try

            Return True
        End Function
        Public Function Agregar(ByVal pArticulo As String, ByVal pSubproceso As String, ByVal pRevisionSubproceso As Integer, ByVal pRevisionRuta As Integer, ByVal pUsuario As String) As Boolean
            Dim lobjTinto As New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.Tintoreria)
            Dim objParametros() As Object = {"codigo_articulo", pArticulo, "codigo_subproceso", pSubproceso, "revision_subproceso", pRevisionSubproceso, "revision_ruta", pRevisionRuta, "usuario_creacion", pUsuario}
            Try
                lobjTinto.EjecutarComando("pr_NM_RutaProduccion_Insert", objParametros)
            Catch ex As Exception
                Throw ex
            End Try

            Return True
        End Function
        Public Function ListarNuevosDatos(ByVal pArticulo As String, ByVal pSubproceso As String) As DataTable
            Dim dtDatos As DataTable
            Dim lobjTinto As New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.Tintoreria)

            Dim objParametros() As Object = {"codigo_articulo", pArticulo, "codigo_subproceso", pSubproceso}
            Try
                dtDatos = lobjTinto.ObtenerDataTable("pr_NM_Subproceso_Detalle_PorSubProceso", objParametros)
            Catch ex As Exception
                Throw ex
            End Try

            Return dtDatos
        End Function
        Public Function ListarRutas_CodSubprocesos_byArticulo(ByVal pArticulo As String) As DataTable
            Dim dtDatos As DataTable
            Dim lobjTinto As New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.Tintoreria)

            Dim objParametros() As Object = {"codigo_articulo", pArticulo}
            Try

                dtDatos = lobjTinto.ObtenerDataTable("pr_NM_RutaProduccion_CodSubprocesos_ByArticulo", objParametros)
            Catch ex As Exception
                Throw ex
            End Try

            Return dtDatos
        End Function
#End Region
        Public Sub Dispose() Implements System.IDisposable.Dispose

        End Sub
    End Class
End Namespace
