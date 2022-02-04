Imports NM.AccesoDatos
Public Class Ficha

#Region "-- Propiedades --"
  Private mstrCodigo As String = ""
  Private mstrCodigoArticuloCorto As String = ""
  Private mstrCodigoOP As String = ""
  Private mobjArticulo As Maestros.Articulo
    Private mdblMetraje As Double = 0
    Private mdblVariacion As Double = 0
  Private mstrUsuario As String = ""
  Private mobjDatosProduccion As DatosProduccion
#End Region

#Region "-- Constructores --"
  Sub New(ByVal strUsuario As String)
    mstrUsuario = strUsuario
  End Sub
  Sub New(ByVal strUsuario As String, ByVal strCodigo As String)
    mstrUsuario = strUsuario
    mstrCodigo = strCodigo
  End Sub
#End Region

#Region "-- Propiedades --"
  Public Property Codigo() As String
    Get
      Codigo = mstrCodigo
    End Get
    Set(ByVal Value As String)
      mstrCodigo = Value
    End Set
  End Property
  Public Property CodigoArticuloCorto() As String
    Get
      CodigoArticuloCorto = mstrCodigoArticuloCorto
    End Get
    Set(ByVal Value As String)
      mstrCodigoArticuloCorto = Value
    End Set
  End Property
  Public Property OP() As String
    Get
      OP = mstrCodigoOP
    End Get
    Set(ByVal Value As String)
      mstrCodigoOP = Value
    End Set
  End Property
  Public ReadOnly Property Articulo() As Maestros.Articulo
    Get
      Articulo = mobjArticulo
    End Get
  End Property
  Public Property Metraje() As Double
    Get
      Metraje = mdblMetraje
    End Get
    Set(ByVal Value As Double)
      mdblMetraje = Value
    End Set
    End Property
    Public Property Variacion() As Double
        Get
            Variacion = mdblVariacion
        End Get
        Set(ByVal Value As Double)
            mdblVariacion = Value
        End Set
    End Property
    Public ReadOnly Property RegistroProduccion() As DatosProduccion
        Get
            RegistroProduccion = mobjDatosProduccion
        End Get
    End Property
#End Region

#Region "-- Metodos --"
  Public Function Buscar() As Boolean
    Dim lobjTinto As AccesoDatosSQLServer
    Dim mdsSet As DataSet
    Dim lbooOk As Boolean
    mobjDatosProduccion = New DatosProduccion(mstrUsuario, mstrCodigo)

    Try
      Dim larrParametros() As String = {"var_CodigoFicha", mstrCodigo}
      lobjTinto = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.Tintoreria)
      mdsSet = lobjTinto.ObtenerDataSet("usp_TIN_Ficha_Buscar1", larrParametros)
      mobjDatosProduccion.Datos = mdsSet.Tables(1)
      With mdsSet.Tables(0).Rows(0)
        mstrCodigoOP = .Item("codigo_orden")
        mstrCodigoArticuloCorto = .Item("codigo_articulo_corto")
        mobjArticulo = New Maestros.Articulo(mstrUsuario, .Item("codigo_articulo_largo"))
        mobjArticulo.Buscar()
                mdblMetraje = .Item("metraje")
                mdblVariacion = .Item("variacion")
      End With

      lbooOk = True
    Catch ex As Exception
      lbooOk = False
    Finally
      lobjTinto = Nothing
      mdsSet = Nothing
    End Try
    Return lbooOk
    End Function

    Public Function Eliminar() As Boolean
        Dim lobjProduccion As AccesoDatosSQLServer
        Dim lbooOk As Boolean
        mobjDatosProduccion = New DatosProduccion(mstrUsuario, mstrCodigo)

        Try
            Dim larrParametros() As String = {"pvch_codigoficha", mstrCodigo}
            lobjProduccion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.Produccion)
            lobjProduccion.EjecutarComando("usp_rec_ficha_eliminar", larrParametros)

            lbooOk = True
        Catch ex As Exception
            Throw ex
            lbooOk = False
        Finally
            lobjProduccion = Nothing
        End Try
        Return lbooOk
    End Function

  Public Function listarFichaTintoreria_disponibles() As DataTable
    Dim lobjTinto As AccesoDatosSQLServer
    lobjTinto = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.Produccion)
    Dim dtIngreso As DataTable = lobjTinto.ObtenerDataTable("SP_ObtenerFichaPrensa_disponibles")

    Dim fp As New NuevoMundo.Tintoreria.FichaPartida
    Dim dtFichaPartida As DataTable = fp.ObtenerTodasFichasHijas

    For Each dr As DataRow In dtFichaPartida.Rows
      dtIngreso.ImportRow(dr)
    Next
    Return dtIngreso
  End Function

#End Region

  Public Class DatosProduccion

#Region "-- Variables --"
    Private mstrCodigoFicha As String = ""
    Private mstrUsuario As String
    Private mdtDatos As DataTable
#End Region

#Region "-- Constructores --"
    Sub New(ByVal strUsuario As String, ByVal strCodigoFicha As String)
      mstrUsuario = strUsuario
      mstrCodigoFicha = strCodigoFicha
    End Sub
#End Region

#Region "-- Propiedades --"
    Public Property Datos() As DataTable
      Get
        Datos = mdtDatos
      End Get
      Set(ByVal Value As DataTable)
        mdtDatos = Value
      End Set
    End Property
#End Region

#Region "-- Metodos --"
    Public Function Grabar(ByVal strXMLDatos As String)
      Dim lobjTinto As AccesoDatosSQLServer
      Dim lstrParametros() As String = {"var_CodigoFicha", mstrCodigoFicha, _
                                          "ntx_XMLDatos", strXMLDatos, _
                                          "var_Usuario", mstrUsuario}
      Try
        lobjTinto = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.Tintoreria)
        lobjTinto.EjecutarComando("usp_TIN_DatosProduccion_Grabar", lstrParametros)
      Catch ex As Exception
        Throw ex
      Finally
        lobjTinto = Nothing
      End Try

    End Function
    Public Function ListarOmisionesXAprobar() As DataTable
      Dim lobjTinto As AccesoDatosSQLServer
      Dim ldtRes As DataTable
      Try
        lobjTinto = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.Tintoreria)
        ldtRes = lobjTinto.ObtenerDataTable("usp_TIN_OmisionSecuencia_ListarXAprobar")
      Catch ex As Exception
        ldtRes = Nothing
        Throw ex
      Finally
        lobjTinto = Nothing
      End Try
      Return ldtRes
    End Function
    Public Function ObtenerDiasPorValidar() As DataTable
      Dim lobjTinto As AccesoDatosSQLServer
      Dim ldtRes As DataTable
      Try
        lobjTinto = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.CalidadTintoreria)
        ldtRes = lobjTinto.ObtenerDataTable("usp_CAL_DiasRegistroProduccion")
      Catch ex As Exception
        ldtRes = Nothing
        Throw ex
      Finally
        lobjTinto = Nothing
      End Try
      Return ldtRes
    End Function
    Public Function AprobarOmision(ByVal strFicha As String, ByVal intSecuencia As Integer)
      Dim lobjTinto As AccesoDatosSQLServer
      Dim lstrParametros() As String = {"var_Ficha", strFicha, _
                  "int_Secuencia", intSecuencia, _
                  "var_Usuario", mstrUsuario}
      Try
        lobjTinto = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.Tintoreria)
        lobjTinto.EjecutarComando("usp_TIN_OmisionSecuencia_Aprobar", lstrParametros)
      Catch ex As Exception
        Throw ex
      Finally
        lobjTinto = Nothing
      End Try

    End Function
    Public Function RechazarOmision(ByVal strFicha As String, ByVal intSecuencia As Integer)
      Dim lobjTinto As AccesoDatosSQLServer
      Dim lstrParametros() As String = {"var_Ficha", strFicha, _
                  "int_Secuencia", intSecuencia, _
                  "var_Usuario", mstrUsuario}
      Try
        lobjTinto = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.Tintoreria)
        lobjTinto.EjecutarComando("usp_TIN_OmisionSecuencia_Rechazar", lstrParametros)
      Catch ex As Exception
        Throw ex
      Finally
        lobjTinto = Nothing
      End Try

    End Function
    Public Function ObtenerUltimaSecuencia(ByVal pintTipoConsulta As Integer, ByVal pstrFicha As String) As DataTable
      Dim lobjTinto As AccesoDatosSQLServer, ldtResultado As DataTable
      Dim lstrParametros() As String = { _
      "pint_tipoconsulta", pintTipoConsulta, _
      "pvch_ficha", pstrFicha _
      }
      Try
        lobjTinto = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.Tintoreria)
        ldtResultado = lobjTinto.ObtenerDataTable("usp_tin_regprod_ultimasecuencia", lstrParametros)

      Catch ex As Exception
        ldtResultado = Nothing
        Throw ex
      Finally
        lobjTinto = Nothing
      End Try
      Return ldtResultado
    End Function
#End Region

  End Class

End Class
