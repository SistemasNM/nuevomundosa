Imports NM_General.NM_BaseDatos
Imports NM.AccesoDatos

Namespace NM_Tejeduria

  Public Class NM_ControlTenidoTED

    Dim str_CodigoPartida As String = ""
    Dim str_CodigoArticulo As String = ""
    Dim str_CodigoColor As String = ""
    Dim str_Fecha As String = ""
    Dim str_Auxiliares As String = ""
    Dim dbl_DosifPreRedu As Double = 0
    Dim dbl_PickUp As Double = 0
    Dim str_Neutralizado As String = ""
    Dim dbl_Disel As Double = 0
    Dim str_Usuario As String = ""

    Dim str_Hora As String = ""

    Dim str_Error As String = ""

    Public Property CodigoPartida() As String
      Get
        CodigoPartida = str_CodigoPartida
      End Get
      Set(ByVal strValue As String)
        str_CodigoPartida = strValue
      End Set
    End Property

    Public Property CodigoArticulo() As String
      Get
        CodigoArticulo = str_CodigoArticulo
      End Get
      Set(ByVal strValue As String)
        str_CodigoArticulo = strValue
      End Set
    End Property

    Public Property CodigoColor() As String
      Get
        CodigoColor = str_CodigoColor
      End Get
      Set(ByVal strValue As String)
        str_CodigoColor = strValue
      End Set
    End Property

    Public Property Fecha() As String
      Get
        Fecha = str_Fecha
      End Get
      Set(ByVal strValue As String)
        str_Fecha = strValue
      End Set
    End Property

    Public Property Auxiliares() As String
      Get
        Auxiliares = str_Auxiliares
      End Get
      Set(ByVal strValue As String)
        str_Auxiliares = strValue
      End Set
    End Property

    Public Property DosifPreRedu() As Double
      Get
        DosifPreRedu = dbl_DosifPreRedu
      End Get
      Set(ByVal dblValue As Double)
        dbl_DosifPreRedu = dblValue
      End Set
    End Property

    Public Property PickUp() As Double
      Get
        PickUp = dbl_PickUp
      End Get
      Set(ByVal dblValue As Double)
        dbl_PickUp = dblValue
      End Set
    End Property

    Public Property Neutralizado() As String
      Get
        Neutralizado = str_Neutralizado
      End Get
      Set(ByVal strValue As String)
        str_Neutralizado = strValue
      End Set
    End Property

    Public Property Disel() As Double
      Get
        Disel = dbl_Disel
      End Get
      Set(ByVal dblValue As Double)
        dbl_Disel = dblValue
      End Set
    End Property

    Public Property Hora() As String
      Get
        Hora = str_Hora
      End Get
      Set(ByVal strValue As String)
        str_Hora = strValue
      End Set
    End Property

    Public Property Usuario() As String
      Get
        Usuario = str_Usuario
      End Get
      Set(ByVal strValue As String)
        str_Usuario = strValue
      End Set
    End Property


    Public Property clsError() As String
      Get
        clsError = str_Error
      End Get
      Set(ByVal strValue As String)
        str_Error = strValue
      End Set
    End Property

    '--- Definicion de Metodos ---

    Public Function ListarDatos(ByRef objDS As DataSet) As Boolean
      Dim objConex As AccesoDatosSQLServer
      Try
        str_Error = ""
        objConex = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.Produccion)
        Dim Parametros() As Object = {"pvch_CodigoPartida", str_CodigoPartida}
        objDS = objConex.ObtenerDataSet("usp_PreTej_TenidoTed_Listar", Parametros)
        ListarDatos = True
      Catch ex As Exception
        str_Error = ex.Message
        ListarDatos = False
      Finally
        objConex = Nothing
      End Try
    End Function


    Public Function ListarDatosTemp(ByRef objDT As DataTable) As Boolean
      Dim objConex As AccesoDatosSQLServer
      Try
        str_Error = ""
        objConex = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.Produccion)
        Dim Parametros() As Object = {"pvch_CodigoPartida", str_CodigoPartida}
        objDT = objConex.ObtenerDataTable("usp_PreTej_TenidoTed_ListarTemp", Parametros)
        ListarDatosTemp = True
      Catch ex As Exception
        str_Error = ex.Message
        ListarDatosTemp = False
      Finally
        objConex = Nothing
      End Try
    End Function


    Public Function ActualiarDatos() As Boolean
      Dim objConex As AccesoDatosSQLServer
      Try
        str_Error = ""
        objConex = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.Produccion)
        Dim Parametros() As Object = {"pvch_CodigoPartida", str_CodigoPartida, _
                                      "pvch_CodigoArticulo", str_CodigoArticulo, _
                                      "pvch_CodigoColor", str_CodigoColor, _
                                      "pdtm_Fecha", str_Fecha, _
                                      "pvch_Auxiliares", str_Auxiliares, _
                                      "pnum_DosifPreRedu", dbl_DosifPreRedu, _
                                      "pnum_PickUp", dbl_PickUp, _
                                      "pvch_Neutralizado", str_Neutralizado, _
                                      "pnum_Disel", dbl_Disel, _
                                      "pvch_Usuario", str_Usuario}
        objConex.EjecutarComando("usp_PreTej_TenidoTed_Actualizar", Parametros)
        ActualiarDatos = True
      Catch ex As Exception
        str_Error = ex.Message
        ActualiarDatos = False
      Finally
        objConex = Nothing
      End Try
    End Function



    Public Function ActualiarDatosDet(ByVal pDT As DataTable) As Boolean
      Dim objConex As AccesoDatosSQLServer
      Dim objUtil As New NM_General.Util
      Try
        str_Error = ""
        objConex = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.Produccion)
        Dim Parametros() As Object = {"pvch_CodigoPartida", str_CodigoPartida, _
                                      "pnte_detalle", objUtil.GeneraXml(pDT), _
                                      "pvch_Usuario", str_Usuario}
        objConex.EjecutarComando("usp_PreTej_TenidoTed_ActualizarDet", Parametros)
        ActualiarDatosDet = True
      Catch ex As Exception
        str_Error = ex.Message
        ActualiarDatosDet = False
      Finally
        objConex = Nothing
        objUtil = Nothing
      End Try
    End Function


    Public Function EliminarDatosDet() As Boolean
      Dim objConex As AccesoDatosSQLServer
      Try
        str_Error = ""
        objConex = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.Produccion)
        Dim Parametros() As Object = {"pvch_CodigoPartida", str_CodigoPartida, _
                                      "pvch_Hora", str_Hora}
        objConex.EjecutarComando("usp_PreTej_TenidoTedDet_Eliminar", Parametros)
        EliminarDatosDet = True
      Catch ex As Exception
        str_Error = ex.Message
        EliminarDatosDet = False
      Finally
        objConex = Nothing
      End Try
    End Function

  End Class

End Namespace