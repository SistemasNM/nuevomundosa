Imports NM_General.NM_BaseDatos
Imports NM.AccesoDatos

Namespace NM_Tejeduria
    Public Class NMM_ArticuloBase

#Region "VARIABLES"


        Private _strAccion As String
        Private _strArticuloBase As String
        Private _intRevisionArticuloBase As Integer
        Private _strTipoArticulo As String
        Private _strEstado As String
        Private _strLinea As String
        Private _strFamiliaCrudo As String
        Private _strLigamento As String
        Private _strCompetitividad As String
        Private _strTipoElongacion As String
        Private _strModa As String
        Private _dblVelocidadEstandar As Double
        Private _dblEficienciaEstandar As Double
        Private _strTipoUrdimbre As String
        Private _strTipoTrama As String
        Private _strTipoComposicion As String
        Private _strFechaLanzamiento As String
        Private _strUsuario As String
        Private _objConexion As AccesoDatosSQLServer

#End Region

#Region "PROPIEDADES"
        Public Property Accion() As String
            Get
                Return _strAccion
            End Get
            Set(ByVal Value As String)
                _strAccion = Value
            End Set
        End Property
        Public Property CodigoArticuloBase() As String
            Get
                Return _strArticuloBase
            End Get
            Set(ByVal Value As String)
                _strArticuloBase = Value
            End Set
        End Property
        Public Property RevisionArticuloBase() As Integer
            Get
                Return _intRevisionArticuloBase
            End Get
            Set(ByVal Value As Integer)
                _intRevisionArticuloBase = Value
            End Set
        End Property
        Public Property TipoArticulo() As String
            Get
                Return _strTipoArticulo
            End Get
            Set(ByVal Value As String)
                _strTipoArticulo = Value
            End Set
        End Property

        Public Property Estado() As String
            Get
                Return _strEstado
            End Get
            Set(ByVal Value As String)
                _strEstado = Value
            End Set
        End Property

        Public Property Linea() As String
            Get
                Return _strLinea
            End Get
            Set(ByVal Value As String)
                _strLinea = Value
            End Set
        End Property

        Public Property FamiliaCrudo() As String
            Get
                Return _strFamiliaCrudo
            End Get
            Set(ByVal Value As String)
                _strFamiliaCrudo = Value
            End Set
        End Property
        Public Property Ligamento() As String
            Get
                Return _strLigamento
            End Get
            Set(ByVal Value As String)
                _strLigamento = Value
            End Set
        End Property
        Public Property Competitividad() As String
            Get
                Return _strCompetitividad
            End Get
            Set(ByVal Value As String)
                _strCompetitividad = Value
            End Set
        End Property
        Public Property TipoElongacion() As String
            Get
                Return _strTipoElongacion
            End Get
            Set(ByVal Value As String)
                _strTipoElongacion = Value
            End Set
        End Property
        Public Property Moda() As String
            Get
                Return _strModa
            End Get
            Set(ByVal Value As String)
                _strModa = Value
            End Set
        End Property
        Public Property VelocidadEstandar() As Double
            Get
                Return _dblVelocidadEstandar
            End Get
            Set(ByVal Value As Double)
                _dblVelocidadEstandar = Value
            End Set
        End Property
        Public Property EficienciaEstandar() As Double
            Get
                Return _dblEficienciaEstandar
            End Get
            Set(ByVal Value As Double)
                _dblEficienciaEstandar = Value
            End Set
        End Property
        Public Property TipoUrdimbre() As String
            Get
                Return _strTipoUrdimbre
            End Get
            Set(ByVal Value As String)
                _strTipoUrdimbre = Value
            End Set
        End Property
        Public Property TipoTrama() As String
            Get
                Return _strTipoTrama
            End Get
            Set(ByVal Value As String)
                _strTipoTrama = Value
            End Set
        End Property
        Public Property TipoComposicion() As String
            Get
                Return _strTipoComposicion
            End Get
            Set(ByVal Value As String)
                _strTipoComposicion = Value
            End Set
        End Property
        Public Property FechaLanzamiento() As String
            Get
                Return _strFechaLanzamiento
            End Get
            Set(ByVal Value As String)
                _strFechaLanzamiento = Value
            End Set
        End Property
        Public Property Usuario() As String
            Get
                Return _strUsuario
            End Get
            Set(ByVal Value As String)
                _strUsuario = Value
            End Set
        End Property


#End Region

        Sub New()
            Accion = ""
            CodigoArticuloBase = ""
            RevisionArticuloBase = 0
            TipoArticulo = ""
            Estado = ""
            Linea = ""
            FamiliaCrudo = ""
            Ligamento = ""
            Competitividad = ""
            TipoElongacion = ""
            Moda = ""
            VelocidadEstandar = 0
            EficienciaEstandar = 0
            TipoUrdimbre = ""
            TipoTrama = ""
            TipoComposicion = ""
            FechaLanzamiento = ""
            Usuario = ""
        End Sub

        Function Registrar_Articulo_Base() As Integer

            _objConexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.Produccion)

            Dim objParametros() As Object = {"pvch_Accion", _strAccion,
                                             "pvch_ArticuloBase", _strArticuloBase,
                                             "pint_RevisionArticuloBase", _intRevisionArticuloBase,
                                             "pvch_TipoArticulo", _strTipoArticulo,
                                             "pvch_Estado", _strEstado,
                                             "pvch_Linea", _strLinea,
                                             "pvch_FamiliaCrudo", _strFamiliaCrudo,
                                             "pvch_Ligamento", _strLigamento,
                                             "pvch_Competitividad", _strCompetitividad,
                                             "pvch_TipoElongacion", _strTipoElongacion,
                                             "pvch_Moda", _strModa,
                                             "pvch_VelocidadEstandar", _dblVelocidadEstandar,
                                             "pvch_EficienciaEstandar", _dblEficienciaEstandar,
                                             "pvch_TipoUrdimbre", _strTipoUrdimbre,
                                             "pvch_TipoTrama", _strTipoTrama,
                                             "pvch_TipoComposicion", _strTipoComposicion,
                                             "pvch_Usuario", _strUsuario}
            Try
                Return _objConexion.EjecutarComando("usp_TEJ_ArticuloBase_Registrar", objParametros)

            Catch ex As Exception
                Throw ex
            Finally
                _objConexion = Nothing
            End Try
        End Function

        Function Modificar_Articulo_Base() As Integer

            _objConexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.Produccion)

            Dim objParametros() As Object = {"pvch_Accion", _strAccion,
                                             "pvch_ArticuloBase", _strArticuloBase,
                                             "pint_RevisionArticuloBase", _intRevisionArticuloBase,
                                             "pvch_TipoArticulo", _strTipoArticulo,
                                             "pvch_Estado", _strEstado,
                                             "pvch_Linea", _strLinea,
                                             "pvch_FamiliaCrudo", _strFamiliaCrudo,
                                             "pvch_Ligamento", _strLigamento,
                                             "pvch_Competitividad", _strCompetitividad,
                                             "pvch_TipoElongacion", _strTipoElongacion,
                                             "pvch_Moda", _strModa,
                                             "pvch_VelocidadEstandar", _dblVelocidadEstandar,
                                             "pvch_EficienciaEstandar", _dblEficienciaEstandar,
                                             "pvch_TipoUrdimbre", _strTipoUrdimbre,
                                             "pvch_TipoTrama", _strTipoTrama,
                                             "pvch_TipoComposicion", _strTipoComposicion,
                                             "pvch_Usuario", _strUsuario}
            Try
                Return _objConexion.EjecutarComando("usp_TEJ_ArticuloBase_Modificar", objParametros)

            Catch ex As Exception
                Throw ex
            Finally
                _objConexion = Nothing
            End Try
        End Function

        Function Buscar_ArticuloBase() As DataTable
            _objConexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.Produccion)

            Dim objParametros() As Object = {"pvch_ArticuloBase", _strArticuloBase}
            Try
                Return _objConexion.ObtenerDataTable("usp_TEJ_ArticuloBase_Buscar", objParametros)

            Catch ex As Exception
                Throw ex
            Finally
                _objConexion = Nothing
            End Try
        End Function


        Function Listar_ArticulosBase() As DataTable
            _objConexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.Produccion)

            Dim objParametros() As Object = {"pvch_ArticuloBase", _strArticuloBase,
                                             "pvch_Estado", _strEstado}
            Try
                Return _objConexion.ObtenerDataTable("usp_TEJ_ArticuloBase_Listar", objParametros)

            Catch ex As Exception
                Throw ex
            Finally
                _objConexion = Nothing
            End Try
        End Function
    End Class


End Namespace
