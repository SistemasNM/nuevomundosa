Imports NuevoMundo.Generales
Imports NM.AccesoDatos


Namespace OFITESO

  Public Class clsPlazoVentas

    Dim strCO_CLIE As String = ""
    Dim strNO_CLIE As String
    Dim intNU_DIAS As Integer = 0

    Dim str_Usuario As String = ""
    Dim str_Error As String = ""

    Public Property CodigoCliente() As String
      Get
        CodigoCliente = strCO_CLIE
      End Get
      Set(ByVal strValue As String)
        strCO_CLIE = strValue
      End Set
    End Property

    Public Property NombreCliente() As String
      Get
        NombreCliente = strNO_CLIE
      End Get
      Set(ByVal strValue As String)
        strNO_CLIE = strValue
      End Set
    End Property

    Public Property NumeroDias() As Integer
      Get
        NumeroDias = intNU_DIAS
      End Get
      Set(ByVal intValue As Integer)
        intNU_DIAS = intValue
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

    Public Function ListarDatos(ByRef objDT As DataTable) As Boolean
      Dim objConex As AccesoDatosSQLServer
      Try
        str_Error = ""
        objConex = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.TesoreriaOfisis)
        Dim Parametros() As Object = {"pCO_CLIE", strCO_CLIE, _
                                      "pNO_CLIE", strNO_CLIE}
        objDT = objConex.ObtenerDataTable("usp_ofi_ClientesPlazo_Listar", Parametros)
        ListarDatos = True
      Catch ex As Exception
        str_Error = ex.Message
        ListarDatos = False
      Finally
        objConex = Nothing
      End Try
    End Function


    Public Function AgregarClientes() As Boolean
      Dim objConex As AccesoDatosSQLServer
      Try
        str_Error = ""
        objConex = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.TesoreriaOfisis)
        Dim Parametros() As Object = {"pCO_CLIE", strCO_CLIE, _
                                      "pNU_DIAS", intNU_DIAS, _
                                      "pvch_Usuario", str_Usuario}
        objConex.EjecutarComando("usp_ofi_ClientesPlazo_Agregar", Parametros)
        AgregarClientes = True
      Catch ex As Exception
        str_Error = ex.Message
        AgregarClientes = False
      Finally
        objConex = Nothing
      End Try
    End Function

    Public Function ActualizarClientes() As Boolean
      Dim objConex As AccesoDatosSQLServer
      Try
        str_Error = ""
        objConex = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.TesoreriaOfisis)
        Dim Parametros() As Object = {"pCO_CLIE", strCO_CLIE, _
                                      "pNU_DIAS", intNU_DIAS, _
                                      "pvch_Usuario", str_Usuario}
        objConex.EjecutarComando("usp_ofi_ClientesPlazo_Actualizar", Parametros)
        ActualizarClientes = True
      Catch ex As Exception
        str_Error = ex.Message
        ActualizarClientes = False
      Finally
        objConex = Nothing
      End Try
    End Function

    Public Function EliminarCliente() As Boolean
      Dim objConex As AccesoDatosSQLServer
      Try
        str_Error = ""
        objConex = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.TesoreriaOfisis)
        Dim Parametros() As Object = {"pCO_CLIE", strCO_CLIE, _
                                      "pvch_Usuario", str_Usuario}

        objConex.EjecutarComando("usp_ofi_ClientesPlazo_Eliminar", Parametros)
        EliminarCliente = True
      Catch ex As Exception
        str_Error = ex.Message
        EliminarCliente = False
      Finally
        objConex = Nothing
      End Try
    End Function

    Public Function ProcesarData(ByVal strEmpresa As String, ByVal intAnio As Integer) As Boolean
      Dim objConex As AccesoDatosSQLServer
      Try
        str_Error = ""
        objConex = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.TesoreriaOfisis)
        Dim Parametros() As Object = {"var_Empresa", strEmpresa, _
                                      "int_Anio", intAnio, _
                                      "var_Usuario", str_Usuario}

        objConex.EjecutarComando("usp_TES_PlazoVenta_Proceso", Parametros)
        ProcesarData = True
      Catch ex As Exception
        str_Error = ex.Message
        ProcesarData = False
      Finally
        objConex = Nothing
      End Try
    End Function


  End Class
End Namespace
