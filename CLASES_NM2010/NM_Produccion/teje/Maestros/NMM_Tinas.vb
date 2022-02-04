Imports NM_General.NM_BaseDatos
Imports NM.AccesoDatos
Namespace NM_Tejeduria
  Public Class NMM_Tinas

#Region "-- Variables --"

    Dim mConexion As AccesoDatosSQLServer
    Dim mstrError As String

#End Region


#Region "-- Propiedades --"

    Public ReadOnly Property clsError() As String
      Get
        Return mstrError
      End Get
    End Property

#End Region

    Public Function TinasListar(ByVal strCodTed As String, ByRef pDT As DataTable) As Boolean
      '************************************************  
      'Creado por:	 CPT
      'Fecha     :   22-08-2013
      'Proposito :   Lista las tinas para el proceso TED
      '************************************************  
      Dim blnRpta As Boolean
      Dim Conexion As AccesoDatosSQLServer
      Dim objParametro() As Object = {"vch_codigo_ted", strCodTed}
      Try
        mstrError = ""
        blnRpta = True
        Conexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.Produccion)
        pDT = Conexion.ObtenerDataTable("usp_PreTej_Tinas_Listar", objParametro)

      Catch ex As Exception
        blnRpta = False
        mstrError = ex.Message
      Finally
        Conexion = Nothing
      End Try
      Return blnRpta
    End Function


    Public Function TinasGenerar(ByVal strCodigoTed As String, ByVal strUsuario As String) As Boolean
      '************************************************  
      'Creado por:	  CPT
      'Fecha     :    22-08-2013
      'Proposito :    Genera las tinas para el proceso TED
      '************************************************  
      Dim blnRpta As Boolean
      Dim Conexion As AccesoDatosSQLServer
      Dim objParametro() As Object = {"vch_codigo_ted", strCodigoTed, _
                                      "vch_usuario", strUsuario}
      Try
        mstrError = ""
        blnRpta = True
        Conexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.Produccion)
        Conexion.EjecutarComando("usp_PreTej_Tinas_Generar", objParametro)

      Catch ex As Exception
        blnRpta = False
        mstrError = ex.Message
      Finally
        Conexion = Nothing
      End Try
      Return blnRpta
    End Function

    Public Function TinasActualizar(ByVal str_codigo_ted As String, _
                                    ByVal int_numero As Integer, _
                                    ByVal dbl_presion_rodillo As Double, _
                                    ByVal dbl_tension As Double, _
                                    ByVal dbl_temperatura As Double, _
                                    ByVal str_usuario As String, _
                                    ByVal int_codigo_tipo As Integer, _
                                    ByVal str_codigo_condicion As String, _
                                    ByVal str_codigo_oxidacion As String) As Boolean

      '*************************************************************
      'Creado por:	  CPT
      'Fecha     :    22-08-2013
      'Proposito :    Actualiza las configuraciones de las tinas 
      '**************************************************************
      Dim blnRpta As Boolean
      Dim Conexion As AccesoDatosSQLServer
      Dim objParametro() As Object = {"vch_codigo_ted      ", str_codigo_ted, _
                                      "int_numero          ", int_numero, _
                                      "num_presion_rodillo ", dbl_presion_rodillo, _
                                      "num_tension         ", dbl_tension, _
                                      "num_temperatura     ", dbl_temperatura, _
                                      "vch_usuario         ", str_usuario, _
                                      "int_codigo_tipo     ", int_codigo_tipo, _
                                      "vch_codigo_condicion", str_codigo_condicion, _
                                      "vch_codigo_oxidacion", str_codigo_oxidacion}
      Try
        mstrError = ""
        blnRpta = True
        Conexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.Produccion)
        Conexion.EjecutarComando("usp_PreTej_Tinas_Actualizar", objParametro)

      Catch ex As Exception
        blnRpta = False
        mstrError = ex.Message
      Finally
        Conexion = Nothing
      End Try
      Return blnRpta
    End Function



    Public Function CondicionListar(ByRef pDT As DataTable) As Boolean
      '************************************************  
      'Creado por:	 CPT
      'Fecha     :   23-08-2013
      'Proposito :   Lista las condiciones para las tinas
      '************************************************  
      Dim blnRpta As Boolean
      Dim Conexion As AccesoDatosSQLServer
      Dim objParametro() As Object = {"vch_codigo_ted", ""}
      Try
        mstrError = ""
        blnRpta = True
        Conexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.Produccion)
        pDT = Conexion.ObtenerDataTable("usp_PreTej_Tinas_CondicionListar", objParametro)

      Catch ex As Exception
        blnRpta = False
        mstrError = ex.Message
      Finally
        Conexion = Nothing
      End Try
      Return blnRpta
    End Function

    Public Function OxidacionListar(ByRef pDT As DataTable) As Boolean
      '************************************************  
      'Creado por:	 CPT
      'Fecha     :   23-08-2013
      'Proposito :   Lista las oxidaciones para las tinas
      '************************************************  
      Dim blnRpta As Boolean
      Dim Conexion As AccesoDatosSQLServer
      Dim objParametro() As Object = {"vch_codigo_ted", ""}
      Try
        mstrError = ""
        blnRpta = True
        Conexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.Produccion)
        pDT = Conexion.ObtenerDataTable("usp_PreTej_Tinas_OxidacionListar", objParametro)

      Catch ex As Exception
        blnRpta = False
        mstrError = ex.Message
      Finally
        Conexion = Nothing
      End Try
      Return blnRpta
    End Function

    Public Function TipoProcesoListar(ByRef pDT As DataTable) As Boolean
      '************************************************  
      'Creado por:	 CPT
      'Fecha     :   23-08-2013
      'Proposito :   Lista los tipos de proceso que puede tener la tina
      '************************************************  
      Dim blnRpta As Boolean
      Dim Conexion As AccesoDatosSQLServer
      Dim objParametro() As Object = {"vch_codigo_ted", ""}
      Try
        mstrError = ""
        blnRpta = True
        Conexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.Produccion)
        pDT = Conexion.ObtenerDataTable("usp_PreTej_Tinas_TipoProcListar", objParametro)

      Catch ex As Exception
        blnRpta = False
        mstrError = ex.Message
      Finally
        Conexion = Nothing
      End Try
      Return blnRpta
    End Function

    Public Function CopyData(ByVal strCodigoTed As String, ByVal strUsuario As String) As Boolean

      '*************************************************************
      'Creado por:	  CPT
      'Fecha     :    23-08-2013
      'Proposito :    Permite guardar las versiones
      '**************************************************************
      Dim blnRpta As Boolean
      Dim Conexion As AccesoDatosSQLServer
      Dim objParametro() As Object = {"var_CodigoTed", strCodigoTed, "Var_Usuario_creacion", strUsuario}
      Try
        mstrError = ""
        blnRpta = True
        Conexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.Produccion)
        Conexion.EjecutarComando("USP_TEJ_NM_TINAS_INSERTAR", objParametro)

      Catch ex As Exception
        blnRpta = False
        mstrError = ex.Message
      Finally
        Conexion = Nothing
      End Try
      Return blnRpta
    End Function


  End Class

End Namespace