Option Explicit On
Imports System.Data
Imports NM.AccesoDatos

Public Class clsTejArticulo

#Region "-- Variables --"
    Dim mConexion As AccesoDatosSQLServer
    Dim mstrError As String

    Dim mstrCodigoArticulo As String = ""
    Dim mintRevisionArticulo As Integer
    Dim mstrCodigoHilo As String = ""
    Dim mstrDescripcionHilo As String
    Dim mnumPasadasPulgada As Double = 0
    Dim mnumPorcEngTrama As Double = 0
    Dim mnumAnchoCrudo As Double = 0
    Dim mnumPorcEngUrdimbre As Double = 0
#End Region

#Region "-- Propiedades --"
    
    Public Property CodigoArticulo() As String
        Get
            Return mstrCodigoArticulo
        End Get
        Set(ByVal value As String)
            mstrCodigoArticulo = value
        End Set
    End Property

    Public Property RevisionArticulo() As Integer
        Get
            Return mintRevisionArticulo
        End Get
        Set(ByVal value As Integer)
            mintRevisionArticulo = value
        End Set
    End Property

    Public Property CodigoHilo() As String
        Get
            Return mstrCodigoHilo
        End Get
        Set(ByVal value As String)
            mstrCodigoHilo = value
        End Set
    End Property

    Public Property DescripcionHilo() As String
        Get
            Return mstrDescripcionHilo
        End Get
        Set(ByVal value As String)
            mstrDescripcionHilo = value
        End Set
    End Property

    Public Property PasadasPulgada() As Double
        Get
            Return mnumPasadasPulgada
        End Get
        Set(ByVal value As Double)
            mnumPasadasPulgada = value
        End Set
    End Property

    Public Property PorcEngTrama() As Double
        Get
            Return mnumPorcEngTrama
        End Get
        Set(ByVal value As Double)
            mnumPorcEngTrama = value
        End Set
    End Property

    Public Property AnchoCrudo() As Double
        Get
            Return mnumAnchoCrudo
        End Get
        Set(ByVal value As Double)
            mnumAnchoCrudo = value
        End Set
    End Property

    Public Property PorcEngUrdimbre() As Double
        Get
            Return mnumPorcEngUrdimbre
        End Get
        Set(ByVal value As Double)
            mnumPorcEngUrdimbre = value
        End Set
    End Property

    Public ReadOnly Property clsError() As String
        Get
            Return mstrError
        End Get
    End Property
#End Region

    '-------------------- Definicion de Metodos/Funciones  --------------------

    Public Function fncListarArticulos(ByRef dstArticulo As DataSet, ByVal mstrEmpresa As String, _
                                       ByVal mnumPeriodo As Double, ByVal mstrUsuario As String, _
                                       ByVal mintTipoConsulta As Integer, ByVal mstrArticulo As String) As Boolean
        '-----------------------------------------------------
        'Creado por:	  Alexander Torres Cardenas
        'Fecha     :      18-08-2011
        'Proposito :      Retorna listado de articulos x periodo
        '-----------------------------------------------------
        Dim blnRpta As Boolean
        Dim Conexion As AccesoDatosSQLServer
        Dim objParametros() As Object = {"pchr_Empresa", mstrEmpresa, _
                                         "pnum_Periodo", mnumPeriodo, _
                                         "pvch_Usuario", mstrUsuario, _
                                         "pint_TipoConsulta", mintTipoConsulta, _
                                         "pvch_CodigoArticulo", mstrArticulo, _
                                         "pint_Proceso", clsTejEstadoProceso.Proceso.HilTel_DatosArticulo}
        Try
            mstrError = ""
            Conexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.CostosTejeduria)
            dstArticulo = Conexion.ObtenerDataSet("usp_costej_Articulos_Consultar", objParametros)
            blnRpta = True
        Catch ex As Exception
            blnRpta = False
            mstrError = ex.Message
        Finally
            Conexion = Nothing
        End Try
        Return blnRpta
    End Function

    Public Function fncModificarArticulo(ByVal mstrEmpresa As String, ByVal mnumPeriodo As Double, _
                                         ByVal mstrHiloAnterior As String, ByVal mstrUsuario As String) As Boolean
        '-----------------------------------------------------
        'Creado por:	  Alexander Torres Cardenas
        'Fecha     :      19-08-2011
        'Proposito :      Modifica datos del articulo
        '-----------------------------------------------------
        Dim blnRpta As Boolean
        Dim Conexion As AccesoDatosSQLServer
        Dim objParametro() As Object = {"pchr_Empresa", mstrEmpresa, _
                                        "pnum_Periodo", mnumPeriodo, _
                                        "pvch_CodigoArticulo", mstrCodigoArticulo, _
                                        "pint_RevisionArticulo", mintRevisionArticulo, _
                                        "pvch_CodigoHilo", mstrCodigoHilo, _
                                        "pnum_PasadasPulgada", mnumPasadasPulgada, _
                                        "pnum_PorcEngTrama", mnumPorcEngTrama, _
                                        "pnum_PorcEngUrdimbre", mnumPorcEngUrdimbre, _
                                        "pnum_AnchoCrudo", mnumAnchoCrudo, _
                                        "pvch_Usuario", mstrUsuario, _
                                        "pvch_HiloAnterior", mstrHiloAnterior}
        Try
            mstrError = ""
            Conexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.CostosTejeduria)
            Conexion.EjecutarComando("usp_costej_Articulos_Actualizar", objParametro)
            blnRpta = True
        Catch ex As Exception
            blnRpta = False
            mstrError = ex.Message
        Finally
            Conexion = Nothing
        End Try
        Return blnRpta
    End Function

    Public Function fncImportarArticulos(ByVal mstrEmpresa As String, ByVal mnumPeriodo As Double, ByVal mstrUsuario As String) As Boolean
        '-----------------------------------------------------
        'Creado por:	  Alexander Torres Cardenas
        'Fecha     :      19-08-2011
        'Proposito :      importar los articulos x periodo
        '-----------------------------------------------------
        Dim blnRpta As Boolean
        Dim Conexion As AccesoDatosSQLServer
        Dim objParametros() As Object = {"pchr_Empresa", mstrEmpresa, _
                                         "pnum_Periodo", mnumPeriodo, _
                                         "pint_Proceso", clsTejEstadoProceso.Proceso.HilTel_DatosArticulo, _
                                         "pvch_Usuario", mstrUsuario}
        Try
            mstrError = ""
            Conexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.CostosTejeduria)
            Conexion.EjecutarComando("usp_costej_Articulos_Importar", objParametros)
            blnRpta = True
        Catch ex As Exception
            blnRpta = False
            mstrError = ex.Message
        Finally
            Conexion = Nothing
        End Try
        Return blnRpta
    End Function

    Public Function fncValdarHiloArticulo(ByRef dtbHilosArticulo As DataTable, ByVal mstrEmpresa As String, ByVal mnumPeriodo As Double) As Boolean
        '---------------------------------------------------------------------------
        'Creado por:	  Alexander Torres Cardenas
        'Fecha     :      24-08-2011
        'Proposito :      Validar la existencia de las trama en el maestro de hilos
        '---------------------------------------------------------------------------
        Dim blnRpta As Boolean
        Dim Conexion As AccesoDatosSQLServer
        Dim objParametros() As Object = {"pchr_Empresa", mstrEmpresa, "pnum_Periodo", mnumPeriodo}
        Try
            mstrError = ""
            Conexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.CostosTejeduria)
            dtbHilosArticulo = Conexion.ObtenerDataTable("usp_costej_Articulo_Validar", objParametros)
            blnRpta = True
        Catch ex As Exception
            blnRpta = False
            mstrError = ex.Message
            dtbHilosArticulo = Nothing
        Finally
            Conexion = Nothing
        End Try
        Return blnRpta
    End Function
End Class
