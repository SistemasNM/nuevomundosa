Imports NM.AccesoDatos

Public Class NM_Logistica
#Region "Variables"
    Private _objConexion As AccesoDatosSQLServer
#End Region

    Function SolicitarReproPedido(ByVal strNupedi As String, ByVal strFechaDesp As String, ByVal strMotivo As String) As DataTable
        Try
            _objConexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.VentasOfisis)
            Dim objParametros() As Object = {"NU_PEDI", strNupedi, "FE_ENTR", strFechaDesp, "COD_MOTIVO", strMotivo}
            Return _objConexion.ObtenerDataTable("USP_VENT_REG_REPRO_PEDIDO", objParametros)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Function ListarHilos(ByVal strCodigo As String, ByVal strNombre As String) As DataTable
        Try
            _objConexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.LogisticaOfisis)
            Dim objParametros() As Object = {"var_CodigoHilo", strCodigo, "var_NombreHilo", strNombre}
            Return _objConexion.ObtenerDataTable("usp_LOG_Hilos_Listar", objParametros)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

  Public Function PedidoObservaciones_Mante(ByVal strOpcion As String, ByVal strPedido As String, ByVal int_Correlativo As Integer, ByVal strObs As String, ByVal strTipObs As String) As Object
    Dim Conexion As AccesoDatosSQLServer
    Dim objParametro() As Object = {"cOpcion", strOpcion, _
                                    "NU_PEDI", strPedido, _
                                    "INT_CORRELATIVO", int_Correlativo, _
                                    "VCH_OBSERVACIONES", strObs, _
                                    "VCH_TIPOBS", strTipObs}
    Dim dtR As Object

    Try
      Conexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.VentasOfisis)

      If strOpcion = "Lis" Or strOpcion = "Sel" Then
        dtR = Conexion.ObtenerDataTable("Usp_ObservacionesPedido_Mante", objParametro)
      Else
        dtR = Conexion.EjecutarComando("Usp_ObservacionesPedido_Mante", objParametro)
      End If

    Catch ex As Exception
      Throw
    Finally
      Conexion = Nothing
    End Try

    Return dtR

    End Function
    Public Function ObtenerTipoProductos() As DataTable
        Try
            _objConexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.LogisticaOfisis)
            Dim objParametros() As Object = {}
            Return _objConexion.ObtenerDataTable("USP_OBTENER_TIPOS_PRODUCTO", objParametros)
        Catch ex As Exception
            Throw ex
        End Try

    End Function
    Public Function ObtenerRubroPorTipo(ByVal strTipo As String) As DataTable
        Try
            _objConexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.LogisticaOfisis)
            Dim objParametros() As Object = {"TIPO_ITEM", strTipo}
            Return _objConexion.ObtenerDataTable("USP_OBTENER_RUBRO_POR_TIPO", objParametros)
        Catch ex As Exception
            Throw ex
        End Try

    End Function
    Public Function ObtenerFamiliaPorRubro(ByVal strTipo As String, ByVal strRubro As String) As DataTable
        Try
            _objConexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.LogisticaOfisis)
            Dim objParametros() As Object = {"CO_RUBRO", strRubro, "TIPO_ITEM", strTipo}
            Return _objConexion.ObtenerDataTable("USP_OBTENER_FAMILIA_POR_RUBRO", objParametros)
        Catch ex As Exception
            Throw ex
        End Try

    End Function
    Public Function ObtenerProductosPorTipo(ByVal strTipo As String, ByVal strRubro As String, ByVal strFamilia As String, ByVal strCodigo As String, ByVal strDesc As String) As DataTable
        Try
            _objConexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.LogisticaOfisis)
            Dim objParametros() As Object = {"TIPO_ITEM", strTipo, "CO_RUBRO", strRubro, "CO_FAMI", strFamilia, "CODIGO", strCodigo, "DESCRIPCION", strDesc}
            Return _objConexion.ObtenerDataTable("USP_OBTENER_PRODUCTOR_POR_TIPO", objParametros)
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function ObtenerPeriodoMaestrosProd(ByVal strAnio As String, ByVal strMes As String, strTipo As String) As DataTable
        Try
            _objConexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.Produccion)
            Dim objParametros() As Object = {"vch_anio", strAnio, "vch_mes", strMes, "vch_tipo", strTipo}
            Return _objConexion.ObtenerDataTable("USP_OBTENER_PERIODO_M_P", objParametros)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

  Function GuiaEnDespacho_ValidarExistencia(ByVal int_NumDespacho As Integer, ByVal strNroPedido As String, ByVal strCodArticulo As String) As String
    Try
      _objConexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.LogisticaOfisis)
      Dim objParametros() As Object = {"int_NumDespacho", int_NumDespacho, _
                                       "vch_NroPedido", strNroPedido, _
                                       "vch_CodArticulo", strCodArticulo}
      Dim strReturn As String = ""
      Dim Dt As DataTable = _objConexion.ObtenerDataTable("Usp_GuiaDespacho_ValidarExistencia", objParametros)
      For Each row As DataRow In Dt.Rows
        strReturn = strReturn + row("NU_GUIA") & vbCrLf
      Next
      Return strReturn
    Catch ex As Exception
      Throw ex
    End Try

  End Function

  Function ObtenerGuiasPedido(ByVal int_NroSalida As Integer, ByVal str_NroPedido As String) As DataTable
    Try
      _objConexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.LogisticaOfisis)
      Dim objParametros() As Object = {"pint_NroSalida", int_NroSalida, _
                                       "pvch_NroPedido", str_NroPedido}
      Dim Dt As DataTable = _objConexion.ObtenerDataTable("Usp_ListarGuiasItemProgramacion", objParametros)
      Return Dt
    Catch ex As Exception
      Throw ex
    End Try
  End Function

  Sub ActualizarEstadoGuias(ByVal int_NroSalida As Integer, ByVal str_NroPedido As String, ByVal str_NroGuia As String, ByVal str_EstadoGuia As String)
    Try
      _objConexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.LogisticaOfisis)
      Dim objParametros() As Object = {"pint_NroSalida", int_NroSalida, _
                                       "pvch_NroPedido", str_NroPedido, _
                                       "pvch_NroGuia", str_NroGuia, _
                                       "pvch_EstadoGuia", str_EstadoGuia}
      _objConexion.EjecutarComando("Usp_ActualizarEstadoGuias", objParametros)

    Catch ex As Exception
      Throw ex
    End Try
    End Sub

    Sub ActualizarEstadoGuiasMasivo(ByVal int_NroSalida As Integer, ByVal str_NroPedido As String)
        Try
            _objConexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.LogisticaOfisis)
            Dim objParametros() As Object = {"pint_NroSalida", int_NroSalida, _
                                             "pvch_NroPedido", str_NroPedido}
            _objConexion.EjecutarComando("Usp_ActualizarEstadoGuias_Masivo", objParametros)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub


  Sub ProgramaDespachoEliminar(ByVal int_NroSalida As Integer)
    Try
      _objConexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.LogisticaOfisis)
      Dim objParametros() As Object = {"pint_NroSalida", int_NroSalida}
      _objConexion.EjecutarComando("Usp_ProgramacionEliminar", objParametros)

    Catch ex As Exception
      Throw ex
    End Try
  End Sub

  Sub ProgramaDespacho_Grabar(ByVal str_Accion As String, ByRef int_NroSalida As Integer, ByVal vch_FecPrograma As String, ByVal vch_CodTurno As String, ByVal vch_CodPlaca As String, ByVal vch_ComentarioProg As String, ByVal vch_EstadoProg As String, ByVal vch_UsuarioReg As String, ByVal xml_Pedido As String, ByVal xml_DetPedido As String, ByVal xml_DetPedidoRollo As String)
    Try
      _objConexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.LogisticaOfisis)
      Dim objParametros() As Object = {"vch_Accion", str_Accion, _
                                       "int_NroSalida", int_NroSalida, _
                                       "vch_FecPrograma", vch_FecPrograma, _
                                       "vch_CodTurno", vch_CodTurno, _
                                       "vch_CodPlaca", vch_CodPlaca, _
                                       "vch_ComentarioProg", vch_ComentarioProg, _
                                       "vch_EstadoProg", vch_EstadoProg, _
                                       "vch_UsuarioReg", vch_UsuarioReg, _
                                       "xml_Pedido", xml_Pedido, _
                                       "xml_DetPedido", xml_DetPedido, _
                                       "xml_DetPedidoRollo", xml_DetPedidoRollo}

      int_NroSalida = _objConexion.ObtenerValor("Usp_ProgramaDespacho_Grabar", objParametros)

    Catch ex As Exception
      Throw ex
    End Try
  End Sub

  Sub PedidosPendientes_Buscar(ByRef dsPedido As DataSet, ByVal strCodEmpresa As String, ByVal strCodCliente As String, ByVal strCodTipo As String, ByVal strNroPedido As String, ByVal strCodArticulo As String, ByVal strFecIniPed As String, ByVal strFecFinPed As String, ByVal strFecProg As String)
    Try
      _objConexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.VentasOfisis)
      Dim objParametros() As Object = {"vch_CodEmpresa", strCodEmpresa, _
                                       "vch_CodCliente", strCodCliente, _
                                       "vch_CodTipo", strCodTipo, _
                                       "vch_NroPedido", strNroPedido, _
                                       "vch_CodArticulo", strCodArticulo, _
                                       "vch_FecIniPed", strFecIniPed, _
                                       "vch_FecfinPed", strFecFinPed, _
                                       "vch_FechaPrograma", strFecProg}

      dsPedido = _objConexion.ObtenerDataSet("Usp_PedidosProgramacion_Buscar", objParametros)

    Catch ex As Exception
      Throw ex
    End Try
    End Sub

    Sub PedidosPendientes_Buscar_v2(ByRef dsPedido As DataSet, ByVal strCodEmpresa As String, ByVal strCodCliente As String, ByVal strCodTipo As String, ByVal strNroPedido As String, ByVal strCodArticulo As String, ByVal strFecIniPed As String, ByVal strFecFinPed As String, ByVal strFecProg As String)
        Try
            _objConexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.VentasOfisis)
            Dim objParametros() As Object = {"vch_CodEmpresa", strCodEmpresa, _
                                             "vch_CodCliente", strCodCliente, _
                                             "vch_CodTipo", strCodTipo, _
                                             "vch_NroPedido", strNroPedido, _
                                             "vch_CodArticulo", strCodArticulo, _
                                             "vch_FecIniPed", strFecIniPed, _
                                             "vch_FecfinPed", strFecFinPed, _
                                             "vch_FechaPrograma", strFecProg}

            dsPedido = _objConexion.ObtenerDataSet("Usp_PedidosProgramacion_Buscar_v2", objParametros)

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

  'INICIO: 20150605
  Sub RollosPendientes_Buscar(ByRef dsRollo As DataSet, ByVal strCodEmpresa As String, ByVal strNroPedido As String, ByVal strCodArticulo As String, ByVal intNroSalida As Integer)
    Try
      _objConexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.LogisticaOfisis)
      Dim objParametros() As Object = {"vch_CodEmpresa", strCodEmpresa, _
                                       "vch_NroPedido", strNroPedido, _
                                       "vch_CodArticulo", strCodArticulo, _
                                       "int_NroSalida", intNroSalida _
                                      }

      dsRollo = _objConexion.ObtenerDataSet("Usp_BuscarRollos_x_Programa", objParametros)

    Catch ex As Exception
      Throw ex
    End Try
  End Sub
  'FINAL: 20150605

  Sub ProgramaDespacho_Listar(ByRef dsPrograma As DataSet, ByVal strCodEmpresa As String, ByVal intNroDespacho As String, ByVal strFecIni As String, ByVal strFecFin As String, ByVal strCodTurno As String, ByVal strCodPlaca As String)
    Try
      _objConexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.LogisticaOfisis)
      Dim objParametros() As Object = {"pvch_CodEmpresa", strCodEmpresa, _
                                       "pvch_NroDespacho", intNroDespacho, _
                                       "vch_FecIniProg", strFecIni, _
                                       "vch_FecfinProg", strFecFin, _
                                       "pvch_CodTurno", strCodTurno, _
                                       "pvch_CodPlaca", strCodPlaca}

      dsPrograma = _objConexion.ObtenerDataSet("Usp_ProgramaDespacho_Obtener", objParametros)

    Catch ex As Exception
      Throw ex
    End Try
  End Sub

    ''' <summary>
    ''' Busca lista de Jefaturas solicitantes (Requisiciones)
    ''' </summary>
    ''' <param name="strCodigo"></param>
    ''' <param name="strDescripcion"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ufn_BuscarJefaturaSolicitante(ByVal strCodigo As String, ByVal strDescripcion As String) As DataTable
        Try
            _objConexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.LogisticaOfisis)

            Dim objParametros() As Object = {"pstrCodigo", strCodigo,
                                             "pstrDescripcion", strDescripcion}

            Return _objConexion.ObtenerDataTable("usp_LOG_Requisicion_BuscarJefaturas", objParametros)
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    '*****************************************************************************************************
    'Objetivo   : Obtener datos programacion por rollo
    'Autor      : Juan Cucho Antunez
    'Creado     : 20/09/2016
    'Modificado : //
    '*****************************************************************************************************
    Public Function ufn_obtenerDatosProgramacionPorRollo(ByVal strRollo As String) As DataTable
        Try
            _objConexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.LogisticaOfisis)
            Dim objParametros() As Object = {
                                                "pvch_Rollo", strRollo
                                            }
            Return _objConexion.ObtenerDataTable("USP_LOG_OBTENER_DATOS_PROGRAMACION_POR_ROLLO", objParametros)

        Catch ex As Exception
            Throw ex
        End Try
    End Function
    '*****************************************************************************************************
    'Objetivo   : Registrar Rollos Programados Off Line
    'Autor      : Juan Cucho Antunez
    'Creado     : 28/09/2016
    'Modificado : //
    '*****************************************************************************************************
    Public Function ufn_RegistrarRollosProgramadosOffLine(ByVal dtb_DatosRollosProgramados As DataTable) As Integer
        Try
            _objConexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.LogisticaOfisis)
            Dim lobjUtil As New NM_General.Util
            Dim DatosRollosProgramados_XML As String
            DatosRollosProgramados_XML = lobjUtil.GeneraXml(dtb_DatosRollosProgramados)
            Dim objParametros() As Object = {"pvch_DatosRollosProgramados_XML", DatosRollosProgramados_XML}

            Return (_objConexion.EjecutarComando("USP_LOG_REGISTRAR_ROLLOS_PROGRAMADOS_OFFLINE", objParametros))
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    '*****************************************************************************************************
    'Objetivo   : Eliminar Rollos Programados Off Line
    'Autor      : Juan Cucho Antunez
    'Creado     : 28/09/2016
    'Modificado : //
    '*****************************************************************************************************
    Public Function ufn_EliminarRollosProgramadosOffLine() As Integer
        Try
            _objConexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.LogisticaOfisis)
            Return (_objConexion.EjecutarComando("USP_LOG_ELIMINAR_ROLLOS_PROGRAMADOS_OFFLINE"))
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    '*****************************************************************************************************
    'Objetivo   : Eliminar Rollos de articulo programados
    'Autor      : Juan Cucho Antunez
    'Creado     : 12/10/2016
    'Modificado : //
    '*****************************************************************************************************
    Public Function ufn_EliminarRollosArticuloProgramado(ByVal strNroSalida As Integer _
                                                         , ByVal strNroPedido As String _
                                                         , ByVal strCodArticulo As String) As String
        Try
            _objConexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.LogisticaOfisis)
            Dim objParametros() As Object = {
                                                "pint_NroSalida", strNroSalida,
                                                "pvch_NroPedido", strNroPedido,
                                                "pvch_CodArticulo", strCodArticulo
                                            }
            Return (_objConexion.EjecutarComando("USP_LOG_ELIMINAR_ROLLOS_ARTICULO_PROGRAMADO", objParametros))
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    '*****************************************************************************************************
    'Objetivo   : Quitar Rollos Programados Off Line
    'Autor      : Juan Cucho Antunez
    'Creado     : 13/10/2016
    'Modificado : //
    '*****************************************************************************************************
    Public Function ufn_QuitarRollosProgramadosOffLine(ByVal dtb_DatosRollosProgramados As DataTable) As Integer
        Try
            _objConexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.LogisticaOfisis)
            Dim lobjUtil As New NM_General.Util
            Dim DatosRollosProgramados_XML As String
            DatosRollosProgramados_XML = lobjUtil.GeneraXml(dtb_DatosRollosProgramados)
            Dim objParametros() As Object = {"pvch_DatosRollosProgramados_XML", DatosRollosProgramados_XML}

            Return (_objConexion.EjecutarComando("USP_LOG_QUITAR_ROLLOS_PROGRAMADOS_OFFLINE", objParametros))
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function ufn_ObtenerCliente(ByVal pstrCodigoCliente As String, ByVal pstrNombreCliente As String) As DataTable
        Try
            _objConexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.VentasOfisis)
            Dim objParametros() As Object = {"p_var_Codigocliente", pstrCodigoCliente, "p_var_NombreCliente", pstrNombreCliente}
            Return _objConexion.ObtenerDataTable("usp_qry_ObtenerCliente", objParametros)
        Catch ex As Exception
            Throw ex
        End Try
    End Function
#Region "GENERACION USUARIO PROVEEDOR"
    '*****************************************************************************************************
    'Objetivo   : Obtener datos proveedor para la generacion de credenciales
    'Autor      : Juan Cucho Antunez
    'Creado     : 05/07/2017
    'Modificado : //
    '*****************************************************************************************************
    Public Function ufn_ObtenerDatosProveedorGeneracionCredenciales(ByVal strCodigoEmpresa As String, ByVal strCodigoProveedor As String) As DataTable
        Try
            _objConexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.LogisticaOfisis)
            Dim objParametros() As Object = {"pvch_codigoEmpresa", strCodigoEmpresa,
                                            "pvch_codigoProveedor", strCodigoProveedor}

            Return (_objConexion.ObtenerDataTable("USP_LOG_OBTENER_DATOS_PROVEEDOR_GENERACION_CREDENCIALES", objParametros))
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    '*****************************************************************************************************
    'Objetivo   : Enviar correo credenciales proveedor
    'Autor      : Juan Cucho Antunez
    'Creado     : 05/07/2017
    'Modificado : //
    '*****************************************************************************************************
    Public Function ufn_EnviarCorreoCredenciales(ByVal strEmailTo As String,
                                                 ByVal strNombreProveedor As String,
                                                 ByVal strCodigoProveedor As String,
                                                 ByVal strUsuario As String,
                                                 ByVal strClave As String) As Integer
        Try
            _objConexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.LogisticaOfisis)
            Dim objParametros() As Object = {"pvch_email_to", strEmailTo,
                                            "pvch_nombreProveedor", strNombreProveedor,
                                            "pvch_codigoProveedor", strCodigoProveedor,
                                            "pvch_usuario", strUsuario,
                                            "pvch_clave", strClave}

            Return (_objConexion.EjecutarComando("USP_LOG_ENVIAR_CORREO_CREDENCIALES_V2", objParametros))
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    '*****************************************************************************************************
    'Objetivo   : Enviar correo credenciales proveedor
    'Autor      : Juan Cucho Antunez
    'Creado     : 07/07/2017
    'Modificado : //
    '*****************************************************************************************************
    Public Function ufn_CrearUsuarioProveedorPortal(ByVal strUsuario As String,
                                      ByVal strNombreUsuario As String,
                                      ByVal strClaveEncriptada As String,
                                      ByVal dtmFechaModificacionClave As DateTime,
                                      ByVal strCodigoGrupo As String,
                                      ByVal strDireccionEmail As String,
                                      ByVal strEstadoUsuario As String) As Integer
        Try
            _objConexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.SeguridadOfisis)
            Dim objParametros() As Object = {"pvch_Usuario", strUsuario,
                                            "pvch_NombreUsuario", strNombreUsuario,
                                            "pvch_ClaveEncriptada", strClaveEncriptada,
                                            "pdtm_FechaModificacionClave", dtmFechaModificacionClave,
                                            "pvch_CodigoGrupo", strCodigoGrupo,
                                            "pvch_DireccionEmail", strDireccionEmail,
                                            "pvch_Estado", strEstadoUsuario}

            Return (_objConexion.EjecutarComando("USP_SEG_CREAR_USUARIO_PROVEEDOR_PORTAL", objParametros))
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    '*****************************************************************************************************
    'Objetivo   : Vincular usuario proveedor del portal
    'Autor      : Juan Cucho Antunez
    'Creado     : 07/07/2017
    'Modificado : //
    '*****************************************************************************************************
    Public Function ufn_VincularUsuarioProveedorPortal(ByVal strCodigoProveedor As String,
                                      ByVal strCodigoUsuario As String,
                                      ByVal strUsuarioModificacion As String,
                                      ByVal dtmFechaModificacion As DateTime) As Integer
        Try
            _objConexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.SeguridadOfisis)
            Dim objParametros() As Object = {"pvch_codigoProveedor", strCodigoProveedor,
                                            "pvch_CodigoUsuario", strCodigoUsuario,
                                            "pvch_UsuarioModificacion", strUsuarioModificacion,
                                            "pdtm_FechaModificacion", dtmFechaModificacion}

            Return (_objConexion.EjecutarComando("USP_SEG_VINCULAR_USUARIO_PROVEEDOR_PORTAL", objParametros))
        Catch ex As Exception
            Throw ex
        End Try
    End Function
#End Region
#Region "EXTRANET"
    '*****************************************************************************************************
    'Objetivo   : Obtener Lista de Ordenes Pendientes Proveedor - Extranet
    'Autor      : Luis Alanoca J.
    'Creado     : 20/03/2017
    'Modificado : //
    '*****************************************************************************************************
    Public Function ufn_ObtenerOrdenesPendientesProveedor(ByVal strCodigoProveedor As String) As DataTable
        Try
            _objConexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.LogisticaOfisis)
            Dim objParametros() As Object = {
                                                "pvch_CodProveedor", strCodigoProveedor
                                            }
            Return _objConexion.ObtenerDataTable("USP_LOG_EXTRANET_ORDENES_PENDIENTE_PROVEEDOR", objParametros)

        Catch ex As Exception
            Throw ex
        End Try
    End Function

    '*****************************************************************************************************
    'Objetivo   : Obtener Lista de Ordenes Pendientes Proveedor - Extranet
    'Autor      : Luis Alanoca J.
    'Creado     : 25/03/2017
    'Modificado : //
    '*****************************************************************************************************
    Public Function ufn_ObtenerFacturasOCOSProveedor(ByVal strCodigoProveedor As String, ByVal strNumeroOCOS As String, ByVal strTipoOCOS As String) As DataTable
        Try
            _objConexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.LogisticaOfisis)
            Dim objParametros() As Object = {
                                                "pvch_CodProveedor", strCodigoProveedor,
                                                "pvch_NumeroOCOS", strNumeroOCOS,
                                                "pvch_TipoOCOS", strTipoOCOS
                                            }
            Return _objConexion.ObtenerDataTable("USP_LOG_EXTRANET_FACTURAS_OCOS_PROVEEDOR", objParametros)

        Catch ex As Exception
            Throw ex
        End Try
    End Function


    '*****************************************************************************************************
    'Objetivo   : Obtener Datos Proveedor - Extranet
    'Autor      : Luis Alanoca J.
    'Creado     : 27/03/2017
    'Modificado : //
    '*****************************************************************************************************
    Public Function ufn_ObtenerDatosProveedor(ByVal strCodigoUsuario As String) As DataTable
        Try
            _objConexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.LogisticaOfisis)
            Dim objParametros() As Object = {
                                                "pvch_Usuario", strCodigoUsuario
                                            }
            Return _objConexion.ObtenerDataTable("USP_LOG_EXTRANET_DATOS_PROVEEDOR", objParametros)

        Catch ex As Exception
            Throw ex
        End Try
    End Function

    '*****************************************************************************************************
    'Objetivo   : Registrar Facturas de OCOS del Proveedor - Extranet
    'Autor      : Luis Alanoca J.
    'Creado     : 27/03/2017
    'Modificado : //
    '*****************************************************************************************************
    Public Function ufn_RegistrarFacturasOCOSProveedor(ByVal strCodigoProveedor As String,
                                                       ByVal strNumeroOCOS As String,
                                                       ByVal strTipoOCOS As String,
                                                       ByVal strNumDocumentoLog As String,
                                                       ByVal strTipDocumentoLog As String,
                                                       ByVal strTipoFactura As String,
                                                       ByVal strNumeroFactura As String,
                                                       ByVal dtmFechaEmision As Date,
                                                       ByVal strObservaciones As String,
                                                       ByVal strDetraccion As String,
                                                       ByVal strMoneda As String,
                                                       ByVal dblBaseImponible As Double,
                                                       ByVal dblBaseInafecta As Double,
                                                       ByVal dblImpuesto As Double,
                                                       ByVal dblMontoFactura As Double,
                                                       ByVal strUsuario As String,
                                                       ByVal strIsFpr As String) As Integer
        Try
            _objConexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.LogisticaOfisis)
            Dim objParametros() As Object = {"pvch_CodigoProveedor", strCodigoProveedor,
                                                "pvch_NumeroOCOS", strNumeroOCOS,
                                                "pvch_TipoOCOS", strTipoOCOS,
                                                "pvch_NumDocumentoLog", strNumDocumentoLog,
                                                "pvch_TipDocumentoLog", strTipDocumentoLog,
                                                "pvch_TipoFactura", strTipoFactura,
                                                "pvch_NumeroFactura", strNumeroFactura,
                                                "pdtm_FechaEmision", dtmFechaEmision,
                                                "pvch_Observaciones", strObservaciones,
                                                "pvch_Detraccion", strDetraccion,
                                                "pvch_Moneda", strMoneda,
                                                "pnum_BaseImponible", dblBaseImponible,
                                                "pnum_BaseInafecta", dblBaseInafecta,
                                                "pnum_Impuesto", dblImpuesto,
                                                "pnum_MontoFactura", dblMontoFactura,
                                                "pvch_Usuario", strUsuario,
                                                "pvch_Is_Fpr", strIsFpr
                                            }
            Return (_objConexion.EjecutarComando("USP_LOG_EXTRANET_REGISTRAR_FACTURAS_PROVEEDOR", objParametros))
        Catch ex As Exception
            Throw ex
        End Try
    End Function


    '*****************************************************************************************************
    'Objetivo   : Obtener Datos Factura OCOS - Extranet
    'Autor      : Luis Alanoca J.
    'Creado     : 28/03/2017
    'Modificado : //
    '*****************************************************************************************************
    Public Function ufn_ObtenerDatosFacturaOCOS(ByVal int_IDFacturaOCOS As Integer) As DataTable
        Try
            _objConexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.LogisticaOfisis)
            Dim objParametros() As Object = {
                                                "pint_IDFacturaOCOS", int_IDFacturaOCOS
                                            }

            Return _objConexion.ObtenerDataTable("USP_LOG_EXTRANET_OBTENER_DATOS_FACTURA_OCOS", objParametros)

        Catch ex As Exception
            Throw ex
        End Try
    End Function

    '*****************************************************************************************************
    'Objetivo   : Obtener Datos Factura OCOS - Extranet por tipo y numero factura
    'Autor      : Juan Cucho
    'Creado     : 29/03/2017
    'Modificado : //
    '*****************************************************************************************************
    Public Function ufn_ObtenerDocumentosOcosProveedorExtraxFacturaOProveedor(Optional ByVal strCodigoProveedor As String = "", _
                                                                    Optional ByVal strTipoFactura As String = "", _
                                                                    Optional ByVal strNumeroFactura As String = "", _
                                                                    Optional ByVal strNombreProveedor As String = "", _
                                                                    Optional ByVal strEstado As String = "") As DataTable
        Try
            _objConexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.LogisticaOfisis)
            Dim objParametros() As Object = {"pvch_CodigoProveedor", strCodigoProveedor,
                                            "pvch_TipoFactura", strTipoFactura,
                                            "pvch_NumeroFactura", strNumeroFactura,
                                            "pvch_NombreProveedor", strNombreProveedor,
                                            "pvch_Estado", strEstado
                                            }

            Return _objConexion.ObtenerDataTable("USP_LOG_OBTENER_DOCUMENTOS_OCOS_PROVEEDOR_EXTRA_X_FACTURA", objParametros)

        Catch ex As Exception
            Throw ex
        End Try
    End Function

    '*****************************************************************************************************
    'Objetivo   : Registrar Facturas de OCOS del Proveedor - Extranet
    'Autor      : Luis Alanoca J.
    'Creado     : 27/03/2017
    'Modificado : //
    '*****************************************************************************************************
    Public Function ufn_ActualizarFacturasOCOSProveedor(ByVal intIDFacturaOCOS As Integer,
                                                        ByVal strCodigoProveedor As String,
                                                        ByVal strNumeroOCOS As String,
                                                        ByVal strTipoOCOS As String,
                                                        ByVal strNumDocumentoLog As String,
                                                        ByVal strTipDocumentoLog As String,
                                                        ByVal strTipoFactura As String,
                                                        ByVal strNumeroFactura As String,
                                                        ByVal dtmFechaEmision As Date,
                                                        ByVal strObservaciones As String,
                                                        ByVal strDetraccion As String,
                                                        ByVal strMoneda As String,
                                                        ByVal dblBaseImponible As Double,
                                                        ByVal dblBaseInafecta As Double,
                                                        ByVal dblImpuesto As Double,
                                                        ByVal dblMontoFactura As Double,
                                                        ByVal strUsuario As String) As Integer
        Try
            _objConexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.LogisticaOfisis)
            Dim objParametros() As Object = {"pint_IDFacturaOCOS", intIDFacturaOCOS,
                                             "pvch_CodigoProveedor", strCodigoProveedor,
                                             "pvch_NumeroOCOS", strNumeroOCOS,
                                             "pvch_TipoOCOS", strTipoOCOS,
                                             "pvch_NumDocumentoLog", strNumDocumentoLog,
                                             "pvch_TipDocumentoLog", strTipDocumentoLog,
                                             "pvch_TipoFactura", strTipoFactura,
                                             "pvch_NumeroFactura", strNumeroFactura,
                                             "pdtm_FechaEmision", dtmFechaEmision,
                                             "pvch_Observaciones", strObservaciones,
                                             "pvch_Detraccion", strDetraccion,
                                             "pvch_Moneda", strMoneda,
                                             "pnum_BaseImponible", dblBaseImponible,
                                             "pnum_BaseInafecta", dblBaseInafecta,
                                             "pnum_Impuesto", dblImpuesto,
                                             "pnum_MontoFactura", dblMontoFactura,
                                             "pvch_Usuario", strUsuario
                                            }
            Return (_objConexion.EjecutarComando("USP_LOG_EXTRANET_ACTUALIZAR_FACTURAS_PROVEEDOR", objParametros))
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    '*****************************************************************************************************
    'Objetivo   : Obtener Datos OS - Extranet por codigo proveedor y numero factura
    'Autor      : Juan Cucho
    'Creado     : 29/03/2017
    'Modificado : //
    '*****************************************************************************************************
    Public Function ufn_ObtenerDatosOcosExtranetxNumeroOcos(ByVal strCodigoProveedor As String, _
                                                                    ByVal strTipoOCOS As String, _
                                                                     ByVal strTipDocumentoLog As String, _
                                                                    ByVal strNumDocumentoLog As String) As DataSet
        Try
            _objConexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.LogisticaOfisis)
            Dim objParametros() As Object = {"pvch_codigoProveedor", strCodigoProveedor,
                                             "pvch_TipoOCOS", strTipoOCOS,
                                             "pvch_TipDocumentoLog", strTipDocumentoLog,
                                             "pvch_NumDocumentoLog", strNumDocumentoLog
                                            }

            Return _objConexion.ObtenerDataSet("USP_LOG_OBTENER_DATOS_OCOS_EXTRANET_X_NUMERO_OCOS_V2", objParametros)

        Catch ex As Exception
            Throw ex
        End Try
    End Function

    '*****************************************************************************************************
    'Objetivo   : ObtenerInformacion de OCOS del Proveedor - Extranet
    'Autor      : Luis Alanoca J.
    'Creado     : 30/03/2017
    'Modificado : //
    '*****************************************************************************************************
    Public Function ufn_ObtenerInformacionOCOSProveedor(ByVal strCodigoProveedor As String, ByVal strNumeroOCOS As String, ByVal strTipoOCOS As String) As DataTable
        Try
            _objConexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.LogisticaOfisis)
            Dim objParametros() As Object = {
                                                "pvch_CodProveedor", strCodigoProveedor,
                                                "pvch_NumeroOCOS", strNumeroOCOS,
                                                "pvch_TipoOCOS", strTipoOCOS
                                            }
            Return _objConexion.ObtenerDataTable("USP_LOG_EXTRANET_OBTENER_INFORMACION_OCOS_PROVEEDOR", objParametros)

        Catch ex As Exception
            Throw ex
        End Try
    End Function

    '*****************************************************************************************************
    'Objetivo   : obtener base imponible,porcentaje igv e igv de una orden de servicio
    'Autor      : Juan Cucho
    'Creado     : 31/03/2017
    'Modificado : //
    '*****************************************************************************************************
    Public Function ufn_ObtenerBaseIgvImponibleOS(ByVal strCodigoEmpresa As String, _
                                                  ByVal strCodigoProveedor As String, _
                                                  ByVal strnumeroOS As String, _
                                                  ByVal strNumeroConformidad As String) As DataTable
        Try
            _objConexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.LogisticaOfisis)
            Dim objParametros() As Object = {"pvar_coempr", strCodigoEmpresa,
                                            "pvar_codigoProveedor", strCodigoProveedor,
                                            "pvch_numeroOS", strnumeroOS,
                                             "pvar_NumeroConformidad", strNumeroConformidad
                                            }

            Return _objConexion.ObtenerDataTable("USP_LOG_OBTENER_BASE_IGV_IMPONIBLE_OS", objParametros)

        Catch ex As Exception
            Throw ex
        End Try
    End Function
    '*****************************************************************************************************
    'Objetivo   : Actualizar validacion por parte de mesa de parte
    'Autor      : Juan Cucho
    'Creado     : 03/04/2017
    'Modificado : //
    '*****************************************************************************************************
    Public Function ufn_ValidarOcOsMesaParte(ByVal strCodigoProveedor As String, _
                                                  ByVal strTipoFactura As String, _
                                                  ByVal strNumeroFactura As String, _
                                                  ByVal strUsuarioValidacion As String, _
                                                  ByVal strEstado As String) As Integer
        Try
            _objConexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.LogisticaOfisis)
            Dim objParametros() As Object = {"pvar_codigoProveedor", strCodigoProveedor,
                                            "pvch_TipoFactura", strTipoFactura,
                                            "pvch_NumeroFactura", strNumeroFactura,
                                            "pvch_Usua_ValidacionMP", strUsuarioValidacion,
                                            "pvch_Estado", strEstado
                                            }

            Return _objConexion.EjecutarComando("USP_LOG_VALIDAR_OC_OS_MESA_PARTE", objParametros)

        Catch ex As Exception
            Throw ex
        End Try
    End Function

    '*****************************************************************************************************
    'Objetivo   : obtener base imponible,porcentaje igv e igv de una nota de ingreso
    'Autor      : Juan Cucho
    'Creado     : 11/04/2017
    'Modificado : //
    '*****************************************************************************************************
    Public Function ufn_ObtenerBaseIgvImponibleOC(ByVal strCodigoEmpresa As String, _
                                                  ByVal strTipDocumentoLog As String, _
                                                  ByVal strNumDocumentoLog As String) As DataTable
        Try
            _objConexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.LogisticaOfisis)
            Dim objParametros() As Object = {"pvar_coempr", strCodigoEmpresa,
                                             "pvar_TipDocumentoLog", strTipDocumentoLog,
                                             "pvar_NumDocumentoLog", strNumDocumentoLog
                                            }

            Return _objConexion.ObtenerDataTable("USP_LOG_OBTENER_BASE_IGV_IMPONIBLE_OC", objParametros)

        Catch ex As Exception
            Throw ex
        End Try
    End Function

    '*****************************************************************************************************
    'Objetivo   : Obtener listado documento oc/os por numero de documento
    'Autor      : Juan Cucho
    'Creado     : 20/04/2017
    'Modificado : //
    '*****************************************************************************************************
    Public Function ufn_ObtenerListadoDocumentosOCOSXNumeroDocumento(ByVal strCodigoEmpresa As String, _
                                                  ByVal strCodigoMonedaNacional As String,
                                                  ByVal strCodigoMonedaExtranjera As String,
                                                  ByVal strTipoCambio As String,
                                                  ByVal strCambioExtranjero As String,
                                                  ByVal strCodigoProveedor As String,
                                                  ByVal strCodigoMoneda As String,
                                                  ByVal strTipoConsulta As String,
                                                  ByVal strTipoDocumento As String,
                                                  ByVal strNumeroDocumento As String) As DataTable
        Try
            _objConexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.LogisticaOfisis)
            Dim objParametros() As Object = {"ISCO_EMPR", strCodigoEmpresa,
                                             "ISCO_MONE_NACI", strCodigoMonedaNacional,
                                             "ISCO_MONE_EXTR", strCodigoMonedaExtranjera,
                                             "INFA_TIPO_CAMB", strTipoCambio,
                                             "INFA_CAMB_EXTR", strCambioExtranjero,
                                             "ISCO_PROV", strCodigoProveedor,
                                             "ISCO_MONE", strCodigoMoneda,
                                             "ISTI_CONS", strTipoConsulta,
                                             "pvch_TipoDocumento", strTipoDocumento,
                                             "pvch_NumeroDocumento", strNumeroDocumento
                                            }

            Return _objConexion.ObtenerDataTable("USP_LOG_OBTENER_LISTADO_DOCUMENTOS_OCOS_X_NUMERO_DOCUMENTO", objParametros)

        Catch ex As Exception
            Throw ex
        End Try
    End Function

    '*****************************************************************************************************
    'Objetivo   : Obtener listado documento oc/os por numero de documento
    'Autor      : Juan Cucho
    'Creado     : 20/04/2017
    'Modificado : //
    '*****************************************************************************************************
    Public Function ufn_ObtenerMaestroBusqueda(ByVal strCodigoEmpresa As String, _
                                                  ByVal strTipoBusqueda As String,
                                                  Optional ByVal strParametro As String = "",
                                                  Optional ByVal strCodigo As String = "",
                                                  Optional ByVal strDescripcion As String = "") As DataTable
        Try
            _objConexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.TesoreriaOfisis)
            Dim objParametros() As Object = {"pvar_CodigoEmpresa", strCodigoEmpresa,
                                             "pvar_Tipobusqueda", strTipoBusqueda,
                                             "pvar_parametro", strParametro,
                                             "pvar_Codigo", strCodigo,
                                             "pvar_Descripcion", strDescripcion
                                            }

            Return _objConexion.ObtenerDataTable("USP_TES_OBTENER_MAESTRO_BUSQUEDA", objParametros)

        Catch ex As Exception
            Throw ex
        End Try
    End Function

    '*****************************************************************************************************
    'Objetivo   : Obtener datos documento Interno
    'Autor      : Luis Alanoca
    'Creado     : 25/04/2017
    'Modificado : //
    '*****************************************************************************************************
    Public Function ufn_ObtenerDatosDocumentoInterno(ByVal strTipoDocuInterno As String,
                                                     ByVal strNumDocuInterno As String,
                                                     ByVal strNumeroOCOS As String,
                                                     ByVal strOpcion As String) As DataTable

        Try
            _objConexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.LogisticaOfisis)
            Dim objParametros() As Object = {"pvch_TipoDocuInterno", strTipoDocuInterno,
                                             "pvch_NumeroDocuInterno", strNumDocuInterno,
                                             "pvch_NumeroOCOS", strNumeroOCOS,
                                             "pvch_Opcion", strOpcion
                                            }

            Return _objConexion.ObtenerDataTable("USP_LOG_EXTRANET_OBTENER_DATOS_DOCUMENTO_INTERNO_V2", objParametros)

        Catch ex As Exception
            Throw ex
        End Try
    End Function

    '*****************************************************************************************************
    'Objetivo   : Obtener datos periodo de cierre contable
    'Autor      : Juan Cucho
    'Creado     : 26/04/2017
    'Modificado : //
    '*****************************************************************************************************
    Public Function ufn_ObtenerDatosPeriodoActivoContable(ByVal strCodigoEmpresa As String, _
                                                  ByVal intAnioPeriodo As Integer,
                                                  ByVal intMesPeriodo As Integer) As DataTable
        Try
            _objConexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.TesoreriaOfisis)
            Dim objParametros() As Object = {"pvch_codigoEmpresa", strCodigoEmpresa,
                                             "pint_anioPeriodo", intAnioPeriodo,
                                             "pint_mesPeriodo", intMesPeriodo
                                            }

            Return _objConexion.ObtenerDataTable("USP_TES_OBTENER_DATOS_PERIODO_ACTIVO_CONTABLE", objParametros)

        Catch ex As Exception
            Throw ex
        End Try
    End Function

    '*****************************************************************************************************
    'Objetivo   : Obtener datos documento provisionado
    'Autor      : Juan Cucho
    'Creado     : 26/04/2017
    'Modificado : //
    '*****************************************************************************************************
    Public Function ufn_ObtenerDatosDocumentoProvisionados(ByVal strCodigoEmpresa As String, _
                                                          ByVal strCodigoProveedor As String, _
                                                          ByVal strcodigoTipoDocumento As String,
                                                          ByVal strNumeroDocumento As String) As DataTable
        Try
            _objConexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.TesoreriaOfisis)
            Dim objParametros() As Object = {"pvch_codigoEmpresa", strCodigoEmpresa,
                                             "pvch_codigoProveedor", strCodigoProveedor,
                                             "pvch_codigoTipoDocumento", strcodigoTipoDocumento,
                                             "pvch_NumeroDocumento", strNumeroDocumento
                                            }

            Return _objConexion.ObtenerDataTable("USP_TES_OBTENER_DATOS_DOCUMENTOS_PROVISIONADOS", objParametros)

        Catch ex As Exception
            Throw ex
        End Try
    End Function

    '*****************************************************************************************************
    'Objetivo   : Obtener datos periodo de cierre contable
    'Autor      : Juan Cucho
    'Creado     : 27/04/2017
    'Modificado : //
    '*****************************************************************************************************
    Public Function ufn_ObtenerDatosPeriodoCierreContable(ByVal strCodigoEmpresa As String, _
                                                  ByVal strCodigoTipoOperacion As String,
                                                  ByVal strcodigoUnidadContable As String,
                                                  ByVal intAnioPeriodo As Integer,
                                                  ByVal intMesPeriodo As Integer) As DataTable
        Try
            _objConexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.TesoreriaOfisis)
            Dim objParametros() As Object = {"pvch_codigoEmpresa", strCodigoEmpresa,
                                             "pvch_codigoTipoOperacion", strCodigoTipoOperacion,
                                             "pchr_codigoUnidadContable", strcodigoUnidadContable,
                                             "pint_anioPeriodo", intAnioPeriodo,
                                             "pint_mesPeriodo", intMesPeriodo
                                            }

            Return _objConexion.ObtenerDataTable("USP_TES_OBTENER_DATOS_PERIODO_CIERRE_CONTABLE", objParametros)

        Catch ex As Exception
            Throw ex
        End Try
    End Function

    '*****************************************************************************************************
    'Objetivo   : Generar documento NM_TMDOCU_PROV_I03
    'Autor      : Juan Cucho
    'Creado     : 29/04/2017
    'Modificado : //
    '*****************************************************************************************************
    Public Function ufn_NmTmdocuProvI03(ByVal strCodigoEmpresa As String, _
                                                  ByVal strCodigoProveedor As String,
                                                  ByVal strTipoDocumento As String,
                                                  ByVal strNumeroDocumentoProveedor As String,
                                                  ByVal strCodigoMoneda As String,
                                                  ByVal strCodigoTipoOperacion As String,
                                                  ByVal strCodigoCondicionPago As String,
                                                  ByVal dtmFechaEmision As DateTime,
                                                  ByVal strCodigoUnidadContable As String,
                                                  ByVal dtmFechaVencimiento As DateTime,
                                                  ByVal decTipoCambio As Decimal,
                                                  ByVal decImpuestoBrutoAfecto As Decimal,
                                                  ByVal decImpuestoBrutoInafecto As Decimal,
                                                  ByVal decImpuestoGastosFinancieros As Decimal,
                                                  ByVal decImpuestoMora As Decimal,
                                                  ByVal decImpuestoFlete As Decimal,
                                                  ByVal strEstadoAfectoGastosFinancieros As String,
                                                  ByVal strEstadoAfectoMora As String,
                                                  ByVal strEstadoAfectoFlete As String,
                                                  ByVal strEstadoDct1Braf As String,
                                                  ByVal strEstadoDct1Brin As String,
                                                  ByVal strEstadoDct1Gafi As String,
                                                  ByVal strEstadoDct1Mora As String,
                                                  ByVal strEstadoDct1Flet As String,
                                                  ByVal strEstadoDct2Braf As String,
                                                  ByVal strEstadoDct2Brin As String,
                                                  ByVal strEstadoDct2Gafi As String,
                                                  ByVal strEstadoDct2Mora As String,
                                                  ByVal strEstadoDct2Flet As String,
                                                  ByVal decPorcentajeDescuento1 As Decimal,
                                                  ByVal decMontoDescuento1 As Decimal,
                                                  ByVal decPorcentajeDescuento2 As Decimal,
                                                  ByVal decMontoDescuento2 As Decimal,
                                                  ByVal strCodigoImpuesto1 As String,
                                                  ByVal decPorcentajeImpuesto1 As Decimal,
                                                  ByVal decMontoImpuesto1 As Decimal,
                                                  ByVal strCodigoImpuesto2 As String,
                                                  ByVal decPorcentajeImpuesto2 As Decimal,
                                                  ByVal decMontoImpuesto2 As Decimal,
                                                  ByVal strCodigoImpuesto3 As String,
                                                  ByVal decPorcentajeImpuesto3 As Decimal,
                                                  ByVal decMontoImpuesto3 As Decimal,
                                                  ByVal decPorcentajeScrf As Decimal,
                                                  ByVal decImpuestoScrf As Decimal,
                                                  ByVal decImpuestoScf1 As Decimal,
                                                  ByVal decImpuestoScf2 As Decimal,
                                                  ByVal decImpuestoScf3 As Decimal,
                                                  ByVal decMontoTotal As Decimal,
                                                  ByVal decMontoPagaRend As Decimal,
                                                  ByVal strCodigoTipoDocumentoOrigen As String,
                                                  ByVal strNumeroDocumentoOrigen As String,
                                                  ByVal dtmFechaDocumentoOrigen As String,
                                                  ByVal strObservacion As String,
                                                  ByVal strNumeroRendicionGasto As String,
                                                  ByVal dtmFechaProgramacionPago As DateTime,
                                                  ByVal strCodigoEstadoRend As String,
                                                  ByVal dtmFechaRegistroCompra As DateTime,
                                                  ByVal strNumeroCompServ As String,
                                                  ByVal strCodigoModoDistribucion As String,
                                                  ByVal strEstadoRendComp As String,
                                                  ByVal strNumeroSrenGasto As String,
                                                  ByVal dtmFechaRendGasto As DateTime,
                                                  ByVal strEstadoAstoResu As String,
                                                  ByVal strNumeroImportacion As String,
                                                  ByVal decFactorCambioExtranjero As Decimal,
                                                  ByVal strEstadoSujetoDetraccion As String,
                                                  ByVal strCodigoActivoDetraccion As String,
                                                  ByVal strTipoOperacionDetraccion As String,
                                                  ByVal strEstadoGravNgra As String) As Integer
        Try
            _objConexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.TesoreriaOfisis)
            Dim objParametros() As Object = {"ISCO_EMPR", strCodigoEmpresa,
                                             "ISCO_PROV", strCodigoProveedor,
                                             "ISCO_TIPO_DOCU", strTipoDocumento,
                                             "ISNU_DOCU_PROV", strNumeroDocumentoProveedor,
                                             "ISCO_MONE", strCodigoMoneda,
                                             "ISCO_TIPO_OPER", strCodigoTipoOperacion,
                                             "ISCO_COND_PAGO", strCodigoCondicionPago,
                                             "IDFE_EMIS", dtmFechaEmision,
                                             "ISCO_UNID_CNTB", strCodigoUnidadContable,
                                             "IDFE_VENC", dtmFechaVencimiento,
                                             "INFA_TIPO_CAMB", decTipoCambio,
                                             "INIM_BRUT_AFEC", decImpuestoBrutoAfecto,
                                             "INIM_BRUT_INAF", decImpuestoBrutoInafecto,
                                             "INIM_GAFI", decImpuestoGastosFinancieros,
                                             "INIM_MORA", decImpuestoMora,
                                             "INIM_FLET", decImpuestoFlete,
                                             "ISST_AFEC_GAFI", strEstadoAfectoGastosFinancieros,
                                             "ISST_AFEC_MORA", strEstadoAfectoMora,
                                             "ISST_AFEC_FLET", strEstadoAfectoFlete,
                                             "ISST_DCT1_BRAF", strEstadoDct1Braf,
                                             "ISST_DCT1_BRIN", strEstadoDct1Brin,
                                             "ISST_DCT1_GAFI", strEstadoDct1Gafi,
                                             "ISST_DCT1_MORA", strEstadoDct1Mora,
                                             "ISST_DCT1_FLET", strEstadoDct1Flet,
                                             "ISST_DCT2_BRAF", strEstadoDct2Braf,
                                             "ISST_DCT2_BRIN", strEstadoDct2Brin,
                                             "ISST_DCT2_GAFI", strEstadoDct2Gafi,
                                             "ISST_DCT2_MORA", strEstadoDct2Mora,
                                             "ISST_DCT2_FLET", strEstadoDct2Flet,
                                             "INPC_DCT1", decPorcentajeDescuento1,
                                             "INIM_DCT1", decMontoDescuento1,
                                             "INPC_DCT2", decPorcentajeDescuento2,
                                             "INIM_DCT2", decMontoDescuento2,
                                             "ISCO_IMP1", strCodigoImpuesto1,
                                             "INPC_IMP1", decPorcentajeImpuesto1,
                                             "INIM_IMP1", decMontoImpuesto1,
                                             "ISCO_IMP2", DBNull.Value,
                                             "INPC_IMP2", decPorcentajeImpuesto2,
                                             "INIM_IMP2", decMontoImpuesto2,
                                             "ISCO_IMP3", DBNull.Value,
                                             "INPC_IMP3", decPorcentajeImpuesto3,
                                             "INIM_IMP3", decMontoImpuesto3,
                                             "INPC_SCRF", DBNull.Value,
                                             "INIM_SCRF", DBNull.Value,
                                             "INIM_SCF1", DBNull.Value,
                                             "INIM_SCF2", DBNull.Value,
                                             "INIM_SCF3", DBNull.Value,
                                             "INIM_TOTA", decMontoTotal,
                                             "INIM_PAGA_REND", decMontoPagaRend,
                                             "ISCO_TIDO_ORIG", strCodigoTipoDocumentoOrigen,
                                             "ISNU_DOCU_ORIG", strNumeroDocumentoOrigen,
                                             "IDFE_DOCU_ORIG", DBNull.Value,
                                             "ISDE_OBSE", strObservacion,
                                             "ISNU_REND_GAST", strNumeroRendicionGasto,
                                             "IDFE_PROG_PAGO", dtmFechaProgramacionPago,
                                             "ISCO_ESTA_REND", strCodigoEstadoRend,
                                             "IDFE_REGI_COMP", dtmFechaRegistroCompra,
                                             "ISNU_COMP_SERV", strNumeroCompServ,
                                             "ISCO_MODE_DIST", strCodigoModoDistribucion,
                                             "ISST_REND_COMP", strEstadoRendComp,
                                             "ISNU_SREN_GAST", DBNull.Value,
                                             "IDFE_REND_GAST", DBNull.Value,
                                             "ISST_ASTO_RESU", strEstadoAstoResu,
                                             "ISNU_IMPO", strNumeroImportacion,
                                             "INFA_CAMB_EXTR", decFactorCambioExtranjero,
                                             "ISST_SUJE_DETR", strEstadoSujetoDetraccion,
                                             "ISCO_ACTI_DETR", strCodigoActivoDetraccion,
                                             "ISCO_TIOP_DETR", strTipoOperacionDetraccion,
                                             "ISST_GRAV_NGRA", strEstadoGravNgra
                                            }
            Return _objConexion.EjecutarComando("NM_TMDOCU_PROV_I03", objParametros)
            'Return 1
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    '*****************************************************************************************************
    'Objetivo   : Consulta de saldo de partidas SP_TDORDE_FACT_Q01
    'Autor      : Juan Cucho
    'Creado     : 29/04/2017
    'Modificado : //
    '*****************************************************************************************************
    Public Function ufn_SpTdOrdeFactQ01(ByVal strCodigoEmpresa As String, _
                                                  ByVal strCodigoUsuarioModificacion As String,
                                                  ByVal strCodigoProveedor As String,
                                                  ByVal strCodigoTipoDocumento As String,
                                                  ByVal strNumeroDocumentoProveedor As String,
                                                  ByVal strNumeroDocumentoOrigen As String,
                                                  ByVal strCodigoMoneda As String,
                                                  ByVal strNumDocuGuin As String,
                                                  ByVal decFaTipoCamb As Decimal) As Integer
        Try
            _objConexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.TesoreriaOfisis)
            Dim objParametros() As Object = {"ISCO_EMPR", strCodigoEmpresa,
                                             "ISCO_USUA_MODI", strCodigoUsuarioModificacion,
                                             "ISCO_PROV", strCodigoProveedor,
                                             "ISCO_TIPO_DOCU", strCodigoTipoDocumento,
                                             "ISNU_DOCU_PROV", strNumeroDocumentoProveedor,
                                             "ISNU_DOCU_ORIG", strNumeroDocumentoOrigen,
                                             "ISCO_MONE", strCodigoMoneda,
                                             "ISNU_DOCU_GUIN", strNumDocuGuin,
                                             "INFA_TIPO_CAMB", decFaTipoCamb
                                            }
            Return _objConexion.EjecutarComando("SP_TDORDE_FACT_Q01", objParametros)
            'Return 1
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    '*****************************************************************************************************
    'Objetivo   : Consulta de saldo de partidas NM_TDORDE_FACT_I01
    'Autor      : Juan Cucho
    'Creado     : 29/04/2017
    'Modificado : //
    '*****************************************************************************************************
    Public Function ufn_SpTdOrdeFactI01(ByVal strCodigoEmpresa As String, _
                                                  ByVal strCodigoUsuarioModificacion As String,
                                                  ByVal strCodigoProveedor As String,
                                                  ByVal strCodigoTipoDocumento As String,
                                                  ByVal strNumeroDocumentoProveedor As String,
                                                  ByVal strTipoDocumentoOrigen As String,
                                                  ByVal strNumeroDocumentoOrigen As String,
                                                  ByVal strCodigoMoneda As String,
                                                  ByVal strTipDocuGuin As String,
                                                  ByVal strNumDocuGuin As String,
                                                  ByVal decImDocuOrig As Decimal,
                                                  ByVal decImConv As Decimal,
                                                  ByVal decImConvOrig As Decimal,
                                                  ByVal decImSaldConv As Decimal,
                                                  ByVal intNumeroCorrelativo As Integer,
                                                  ByVal strCodigoUnidad As String,
                                                  ByVal strCodigoAlmacen As String,
                                                  ByVal decFaTipoCamb As Decimal,
                                                  ByVal decFaCambBase As Decimal,
                                                  ByVal decImConvTota As Decimal,
                                                  ByVal intNuSecu As Integer,
                                                  ByVal intNuSecuAnte As Integer) As Integer
        Try
            _objConexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.TesoreriaOfisis)
            Dim objParametros() As Object = {"ISCO_EMPR", strCodigoEmpresa,
                                             "ISCO_USUA_MODI", strCodigoUsuarioModificacion,
                                             "ISCO_PROV", strCodigoProveedor,
                                             "ISCO_TIPO_DOCU", strCodigoTipoDocumento,
                                             "ISNU_DOCU_PROV", strNumeroDocumentoProveedor,
                                             "ISTI_DOCU_ORIG", strTipoDocumentoOrigen,
                                             "ISNU_DOCU_ORIG", strNumeroDocumentoOrigen,
                                             "ISCO_MONE", strCodigoMoneda,
                                             "ISTI_DOCU_GUIN", strTipDocuGuin,
                                             "ISNU_DOCU_GUIN", strNumDocuGuin,
                                             "INIM_DOCU_ORIG", decImDocuOrig,
                                             "INIM_CONV", decImConv,
                                             "INIM_CONV_ORIG", decImConvOrig,
                                             "INIM_SALD_CONV", decImSaldConv,
                                             "INNU_CORR", DBNull.Value,
                                             "ISCO_UNID", strCodigoUnidad,
                                             "ISCO_ALMA", strCodigoAlmacen,
                                             "INFA_TIPO_CAMB", decFaTipoCamb,
                                             "INFA_CAMB_BASE", decFaTipoCamb,
                                             "INIM_CONV_TOTA", decImConvTota,
                                             "INNU_SECU", intNuSecu,
                                             "INNU_SECU_ANTE", intNuSecuAnte
                                            }
            Return _objConexion.EjecutarComando("NM_TDORDE_FACT_I01", objParametros)
            'Return 1
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    '*****************************************************************************************************
    'Objetivo   : Consulta de saldo de partidas SP_TMDOCU_PROV_Q09
    'Autor      : Juan Cucho
    'Creado     : 02/05/2017
    'Modificado : //
    '*****************************************************************************************************
    Public Function ufn_SpTmdocuProvQ09(ByVal strCodigoEmpresa As String, _
                                                  ByVal strCodigoProveedor As String,
                                                  ByVal strCodigoTipoDocumento As String,
                                                  ByVal strNumeroDocumentoProveedor As String) As Integer
        Try
            _objConexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.TesoreriaOfisis)
            Dim objParametros() As Object = {"ISCO_EMPR", strCodigoEmpresa,
                                             "ISCO_PROV", strCodigoProveedor,
                                             "ISCO_TIPO_DOCU", strCodigoTipoDocumento,
                                             "ISNU_DOCU_PROV", strNumeroDocumentoProveedor
                                            }
            Return _objConexion.EjecutarComando("SP_TMDOCU_PROV_Q09", objParametros)
            'Return 1
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    '*****************************************************************************************************
    'Objetivo   : Borrar Tipo Distribucion Centro Costo
    'Autor      : Juan Cucho
    'Creado     : 29/04/2017
    'Modificado : //
    '*****************************************************************************************************
    Public Function ufn_BorrarTipoDistribucionCentroCosto(ByVal strCodigoEmpresa As String, _
                                                          ByVal strCodigoProveedor As String, _
                                                          ByVal strcodigoTipoDocumento As String,
                                                          ByVal strNumeroDocumento As String) As Integer
        Try
            _objConexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.TesoreriaOfisis)
            Dim objParametros() As Object = {"pvch_codigoEmpresa", strCodigoEmpresa,
                                             "pvch_codigoProveedor", strCodigoProveedor,
                                             "pvch_codigoTipoDocumento", strcodigoTipoDocumento,
                                             "pvch_NumeroDocumento", strNumeroDocumento
                                            }

            Return _objConexion.EjecutarComando("USP_TES_BORRAR_TIPO_DISTRIBUCION_CENTRO_COSTO", objParametros)
            'Return 1
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    '*****************************************************************************************************
    'Objetivo   : Actualizar Usuario Fecha Documento Provision
    'Autor      : Juan Cucho
    'Creado     : 29/04/2017
    'Modificado : //
    '*****************************************************************************************************
    Public Function ufn_ActualizarUsuarioFechaDocumentoProvision(ByVal strCodigoUsuario As String, _
                                                                 ByVal strCodigoEmpresa As String,
                                                          ByVal strCodigoProveedor As String, _
                                                          ByVal strcodigoTipoDocumento As String,
                                                          ByVal strNumeroDocumento As String) As Integer
        Try
            _objConexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.TesoreriaOfisis)
            Dim objParametros() As Object = {"pvch_codigoUsuario", strCodigoUsuario,
                                             "pvch_codigoEmpresa", strCodigoEmpresa,
                                             "pvch_codigoProveedor", strCodigoProveedor,
                                             "pvch_codigoTipoDocumento", strcodigoTipoDocumento,
                                             "pvch_NumeroDocumento", strNumeroDocumento
                                            }

            Return _objConexion.EjecutarComando("USP_TES_ACTUALIZAR_USUARIO_FECHA_DOCUMENTO_PROVISION", objParametros)
            'Return 1
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    '*****************************************************************************************************
    'Objetivo   : Registrar distribucion de costo
    'Autor      : Juan Cucho
    'Creado     : 05/03/2017
    'Modificado : //
    '*****************************************************************************************************
    Public Function ufn_RegistrarDistribucionCosto(ByVal strCodigoEmpresa As String,
                                                          ByVal strCodigoProveedor As String,
                                                          ByVal strcodigoTipoDocumento As String,
                                                          ByVal strNumeroDocumento As String,
                                                          ByVal intNumeroCorrelativo As Integer,
                                                          ByVal strCodigoCentroCosto As String,
                                                          ByVal strCodigoCuentaEmpresa As String,
                                                          ByVal strEstadoDistribucion As String,
                                                          ByVal decPorcentajeDistribucion As Decimal,
                                                          ByVal decMontoDistribucion As Decimal,
                                                          ByVal strCodigoOrdenServicio As String,
                                                          ByVal strTipoAuxiliarEmpresa As String,
                                                          ByVal strObservacion As String,
                                                          ByVal strCodigoUsuario As String) As Integer
        Try
            _objConexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.TesoreriaOfisis)
            Dim objParametros() As Object = {"pvch_codigoEmpresa", strCodigoEmpresa,
                                             "pvch_codigoProveedor", strCodigoProveedor,
                                             "pvch_codigoTipoDocumento", strcodigoTipoDocumento,
                                             "pvch_NumeroDocumento", strNumeroDocumento,
                                             "pint_numeroCorrelativo", intNumeroCorrelativo,
                                             "pvch_codigoCentroCosto", strCodigoCentroCosto,
                                             "pvch_codigoCuentaEmpresa", strCodigoCuentaEmpresa,
                                             "pvch_estadoDistribucion", strEstadoDistribucion,
                                             "pnum_PorcentajeDistribucion", decPorcentajeDistribucion,
                                             "pnum_MontoDistribucion", decMontoDistribucion,
                                             "pvch_codigoOrdenServicio", strCodigoOrdenServicio,
                                             "pvch_tipoAuxiliarEmpresa", strTipoAuxiliarEmpresa,
                                             "pvch_observacion", strObservacion,
                                             "pvch_codigoUsuario", strCodigoUsuario
                                            }

            Return _objConexion.EjecutarComando("USP_TES_REGISTRAR_DISTRIBUCION_COSTO", objParametros)
            'Return 1
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    '*****************************************************************************************************
    'Objetivo   : Obtener datos documento con cuenta
    'Autor      : Juan Cucho
    'Creado     : 03/05/2017
    'Modificado : //
    '*****************************************************************************************************
    Public Function ufn_ObtenerDatosDocumentoConCuenta(ByVal strCodigoEmpresa As String, _
                                                          ByVal strCodigoProveedor As String, _
                                                          ByVal strcodigoTipoDocumento As String,
                                                          ByVal strNumeroDocumento As String) As DataTable
        Try
            _objConexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.TesoreriaOfisis)
            Dim objParametros() As Object = {"pvch_codigoEmpresa", strCodigoEmpresa,
                                             "pvch_codigoProveedor", strCodigoProveedor,
                                             "pvch_codigoTipoDocumento", strcodigoTipoDocumento,
                                             "pvch_NumeroDocumento", strNumeroDocumento
                                            }

            'Return _objConexion.ObtenerDataTable("USP_TES_OBTENER_DATOS_DOCUMENTO_CON_CUENTA", objParametros)
            Return _objConexion.ObtenerDataTable("USP_TES_OBTENER_DATOS_DOCUMENTO_CON_CUENTA_V2", objParametros)


        Catch ex As Exception
            Throw ex
        End Try
    End Function
    '*****************************************************************************************************
    'Objetivo   : Registrar distribucion de costo automatico
    'Autor      : Juan Cucho
    'Creado     : 04/03/2017
    'Modificado : //
    '*****************************************************************************************************
    Public Function ufn_RegistrarDistribucionCostoAutomatico(ByVal strCodigoEmpresa As String,
                                                          ByVal strCodigoProveedor As String,
                                                          ByVal strcodigoTipoDocumento As String,
                                                          ByVal strNumeroDocumento As String,
                                                          ByVal strTipoGuiaIngreso As String,
                                                          ByVal strNumeroGuiaIngreso As String,
                                                          ByVal strCodigoAlmacen As String,
                                                          ByVal decOperacionesGravadas As Decimal,
                                                          ByVal decInafecta As Decimal,
                                                          ByVal decOperacionesNoGravadas As Decimal,
                                                          ByVal decOperacionesComunes As Decimal,
                                                          ByVal decFlete As Decimal,
                                                          ByVal strDescripcionAlmacen As String,
                                                          ByVal strCodigoUsuario As String) As Integer
        Try
            _objConexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.TesoreriaOfisis)
            Dim objParametros() As Object = {"pvch_codigoEmpresa", strCodigoEmpresa,
                                             "pvch_codigoProveedor", strCodigoProveedor,
                                             "pvch_codigoTipoDocumento", strcodigoTipoDocumento,
                                             "pvch_NumeroDocumento", strNumeroDocumento,
                                             "pvch_TipoGuiaIngreso", strTipoGuiaIngreso,
                                             "pvch_numeroGuiaIngreso", strNumeroGuiaIngreso,
                                             "pvch_codigoAlmacen", strCodigoAlmacen,
                                             "pnum_OperacionesGravadas", decOperacionesGravadas,
                                             "pnum_Inafecta", decInafecta,
                                             "pnum_OperacionesNoGravadas", decOperacionesNoGravadas,
                                             "pnum_OperacionesComunes", decOperacionesComunes,
                                             "pnum_Flete", decFlete,
                                             "pvch_DescripcionObservacion", strDescripcionAlmacen,
                                             "pvch_codigoUsuario", strCodigoUsuario
                                            }

            Return _objConexion.EjecutarComando("USP_TES_REGISTRAR_DISTRIBUCION_COSTO_AUTOMATICA", objParametros)
            'Return 1
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    '*****************************************************************************************************
    'Objetivo   : Obtener observacion glosa guia ingreso
    'Autor      : Juan Cucho
    'Creado     : 04/03/2017
    'Modificado : //
    '*****************************************************************************************************
    Public Function ufn_ObtenerObservacionGlosaGuiaIngreso(ByVal strCodigoEmpresa As String,
                                                          ByVal strTipoGuiaIngreso As String,
                                                          ByVal strNumeroGuiaIngreso As String,
                                                          ByVal strCodigoAlmacen As String) As String
        Try
            _objConexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.LogisticaOfisis)
            Dim objParametros() As Object = {"pvch_codigoEmpresa", strCodigoEmpresa,
                                             "pvch_TipoGuiaIngreso", strTipoGuiaIngreso,
                                             "pvch_numeroGuiaIngreso", strNumeroGuiaIngreso,
                                             "pvch_codigoAlmacen", strCodigoAlmacen
                                            }

            Return _objConexion.ObtenerDataTable("USP_LOG_OBTENER_OBSERVACION_GLOSA_GUIA_INGRESO", objParametros).Rows(0)("DE_OBSE").ToString()
            'Return 1
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    '*****************************************************************************************************
    'Objetivo   : Borrar asiento contable
    'Autor      : Juan Cucho
    'Creado     : 04/03/2017
    'Modificado : //
    '*****************************************************************************************************
    Public Function ufn_BorrarAsientoContable(ByVal strCodigoEmpresa As String,
                                                          ByVal strCodigoUnidadContable As String,
                                                          ByVal strCodigoOperacionContable As String,
                                                          ByVal intAnioPeriodo As Integer,
                                                          ByVal intMesPeriodo As Integer,
                                                          ByVal strNumeroAsientoContable As String) As Integer
        Try
            _objConexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.ContabilidadOfisis)
            Dim objParametros() As Object = {"pvch_codigoEmpresa", strCodigoEmpresa,
                                             "pvch_codigoUnidadContable", strCodigoUnidadContable,
                                             "pvch_codigoOperacionContable", strCodigoOperacionContable,
                                             "pint_anioPeriodo", intAnioPeriodo,
                                             "pint_mesPeriodo", intMesPeriodo,
                                             "pvch_numeroAsientoContable", strNumeroAsientoContable
                                            }

            Return _objConexion.EjecutarComando("USP_CON_BORRAR_ASIENTO_CONTABLE", objParametros)
            'Return 1
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    '*****************************************************************************************************
    'Objetivo   : Generar Asiento SP_TTPROC_INTE_I01
    'Autor      : Juan Cucho
    'Creado     : 04/03/2017
    'Modificado : //
    '*****************************************************************************************************
    Public Function ufn_SpTtprocInteI01(ByVal strCodigoEmpresa As String,
                                        ByVal strCodigoProveedor As String,
                                        ByVal strcodigoTipoDocumento As String,
                                        ByVal strNumeroDocumento As String,
                                        ByVal intAnioPeriodo As Integer,
                                        ByVal intMesPeriodo As Integer,
                                        ByVal dtmFechaRegistroCompra As DateTime,
                                        ByVal strNumeroAsientoContable As String,
                                        ByVal strEstadoFlag As String) As Integer
        Try
            _objConexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.TesoreriaOfisis)
            Dim objParametros() As Object = {"ISCO_EMPR", strCodigoEmpresa,
                                             "ISPA_0001", strCodigoProveedor,
                                             "ISPA_0002", strcodigoTipoDocumento,
                                             "ISPA_0003", strNumeroDocumento,
                                             "INPA_ANNO", intAnioPeriodo,
                                             "INPA_MESE", intMesPeriodo,
                                             "IDPA_FECH", dtmFechaRegistroCompra,
                                             "ISNU_ASTO", strNumeroAsientoContable,
                                             "ISST_FLAG", strEstadoFlag
                                            }

            Return _objConexion.EjecutarComando("SP_TTPROC_INTE_I01", objParametros)
            'Return 1
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    '*****************************************************************************************************
    'Objetivo   : Generar Asiento SP_TXMVTO_INTE_I01
    'Autor      : Juan Cucho
    'Creado     : 04/03/2017
    'Modificado : //
    '*****************************************************************************************************
    Public Function ufn_SpTxmvtoInteI01(ByVal strCodigoEmpresa As String,
                                                          ByVal strNumeroAsientoContable As String,
                                                          ByVal strCodigoUnidadContable As String,
                                                          ByVal strCodigoOperacionContable As String,
                                                          ByVal intAnioPeriodo As Integer,
                                                          ByVal intMesPeriodo As Integer) As Integer
        Try
            _objConexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.TesoreriaOfisis)
            Dim objParametros() As Object = {"ISCO_EMPR", strCodigoEmpresa,
                                             "ISNU_ASTO", strNumeroAsientoContable,
                                             "ISCO_UNID_CNTB", strCodigoUnidadContable,
                                             "ISCO_OPRC_CNTB", strCodigoOperacionContable,
                                             "INNU_ANNO", intAnioPeriodo,
                                             "INNU_MESE", intMesPeriodo
                                            }

            Return _objConexion.EjecutarComando("SP_TXMVTO_INTE_I01", objParametros)
            'Return 1
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    '*****************************************************************************************************
    'Objetivo   : Generar Asiento SP_TXMVTO_INTE_I01
    'Autor      : Juan Cucho
    'Creado     : 04/03/2017
    'Modificado : //
    '*****************************************************************************************************
    Public Function ufn_ActualizarEstadoDocumentoProvisionAsiento(ByVal strCodigoEmpresa As String,
                                                          ByVal strCodigoProveedor As String,
                                                          ByVal strcodigoTipoDocumento As String,
                                                          ByVal strNumeroDocumento As String) As Integer
        Try
            _objConexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.TesoreriaOfisis)
            Dim objParametros() As Object = {"pvch_codigoEmpresa", strCodigoEmpresa,
                                             "pvch_codigoProveedor", strCodigoProveedor,
                                             "pvch_codigoTipoDocumento", strcodigoTipoDocumento,
                                             "pvch_NumeroDocumento", strNumeroDocumento
                                            }

            Return _objConexion.EjecutarComando("USP_TES_ACTUALIZAR_ESTADO_DOCUMENTO_PROVISION_ASIENTO", objParametros)
            'Return 1
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    '*****************************************************************************************************
    'Objetivo   : Actualizar datos tabla extranet con provision
    'Autor      : Juan Cucho
    'Creado     : 04/05/2017
    'Modificado : //
    '*****************************************************************************************************
    Public Function ufn_ActualizarEstadoProvisionarOcOsExtranet(ByVal strCodigoProveedor As String, _
                                                  ByVal strTipoFactura As String, _
                                                  ByVal strNumeroFactura As String, _
                                                  ByVal strUsuario As String, _
                                                  ByVal strEstado As String) As Integer
        Try
            _objConexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.LogisticaOfisis)
            Dim objParametros() As Object = {"pvar_codigoProveedor", strCodigoProveedor,
                                            "pvch_TipoFactura", strTipoFactura,
                                            "pvch_NumeroFactura", strNumeroFactura,
                                            "pvch_Usua", strUsuario,
                                            "pvch_Estado", strEstado
                                            }

            Return _objConexion.EjecutarComando("USP_LOG_ACTUALIZAR_ESTADO_PROVISIIONAR_OC_OS_EXTRANET", objParametros)

        Catch ex As Exception
            Throw ex
        End Try
    End Function

    '*****************************************************************************************************
    'Objetivo   : Obtener datos documento provisionado dataset
    'Autor      : Juan Cucho
    'Creado     : 10/05/2017
    'Modificado : //
    '*****************************************************************************************************
    Public Function ufn_ObtenerDatosDocumentoProvisionadosDs(ByVal strCodigoEmpresa As String, _
                                                          ByVal strCodigoProveedor As String, _
                                                          ByVal strcodigoTipoDocumento As String,
                                                          ByVal strNumeroDocumento As String) As DataSet
        Try
            _objConexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.TesoreriaOfisis)
            Dim objParametros() As Object = {"pvch_codigoEmpresa", strCodigoEmpresa,
                                             "pvch_codigoProveedor", strCodigoProveedor,
                                             "pvch_codigoTipoDocumento", strcodigoTipoDocumento,
                                             "pvch_NumeroDocumento", strNumeroDocumento
                                            }

            Return _objConexion.ObtenerDataSet("USP_TES_OBTENER_DATOS_DOCUMENTOS_PROVISIONADOS_DS", objParametros)

        Catch ex As Exception
            Throw ex
        End Try
    End Function

    '*****************************************************************************************************
    'Objetivo   : Borrar datos documentos provisionado sin asiento
    'Autor      : Juan Cucho
    'Creado     : 11/05/2017
    'Modificado : //
    '*****************************************************************************************************
    Public Function ufn_BorrarDatosDocumentosProvisionadosSinAsiento(ByVal strCodigoEmpresa As String, _
                                                          ByVal strCodigoProveedor As String, _
                                                          ByVal strcodigoTipoDocumento As String,
                                                          ByVal strNumeroDocumento As String) As Integer
        Try
            _objConexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.TesoreriaOfisis)
            Dim objParametros() As Object = {"pvch_codigoEmpresa", strCodigoEmpresa,
                                             "pvch_codigoProveedor", strCodigoProveedor,
                                             "pvch_codigoTipoDocumento", strcodigoTipoDocumento,
                                             "pvch_NumeroDocumento", strNumeroDocumento
                                            }

            Return _objConexion.EjecutarComando("USP_TES_BORRAR_DATOS_DOCUMENTOS_PROVISIONADOS_SIN_ASIENTO", objParametros)
            'Return 1
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    '*****************************************************************************************************
    'Objetivo   : Obtener factor de cambio por fecha emision
    'Autor      : Juan Cucho
    'Creado     : 18/05/2017
    'Modificado : //
    '*****************************************************************************************************
    Public Function ufn_ObtenerFactorCambioPorFechaEmision(ByVal strCodigoMoneda As String, _
                                                          ByVal dtmFechaEmision As DateTime) As DataTable
        Try
            _objConexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.TesoreriaOfisis)
            Dim objParametros() As Object = {"pvch_codigoMoneda", strCodigoMoneda,
                                             "pdtm_fechaEmision", dtmFechaEmision
                                            }

            Return _objConexion.ObtenerDataTable("USP_TES_OBTENER_FACTOR_CAMBIO_POR_FECHA_EMISION", objParametros)
            'Return 1
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    '*****************************************************************************************************
    'Objetivo   : Obtener Datos Factura OCOS - Extranet
    'Autor      : Luis Alanoca J.
    'Creado     : 21/05/2017
    'Modificado : //
    '*****************************************************************************************************
    Public Function ufn_EliminarDatosFacturaOCOS(ByVal int_IDFacturaOCOS As Integer, ByVal strCodigoProveedor As String, ByVal strNumeroOCOS As String, ByVal strUsuario As String) As DataTable
        Try
            _objConexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.LogisticaOfisis)
            Dim objParametros() As Object = {
                                                "pint_IDFacturaOCOS", int_IDFacturaOCOS,
                                                "pvch_CodProveedor", strCodigoProveedor,
                                                "pvch_NumeroOCOS", strNumeroOCOS,
                                                "pvch_Usuario", strUsuario
                                            }

            Return _objConexion.ObtenerDataTable("USP_LOG_EXTRANET_ELIMINAR_DATOS_FACTURA_OCOS", objParametros)

        Catch ex As Exception
            Throw ex
        End Try
    End Function
    '*****************************************************************************************************
    'Objetivo   : Registrar Facturas de OCOS del Proveedor - Extranet (con ordenes de compra y servicio)  
    'Autor Mod  : Juan Cucho
    'Modificado : 24/05/2017
    'Modificado : //
    '*****************************************************************************************************
    Public Function ufn_RegistrarFacturasOCOSProveedor(ByVal strCodigoProveedor As String,
                                                       ByVal strNumeroOCOS As String,
                                                       ByVal strTipoOCOS As String,
                                                       ByVal strNumDocumentoLog As String,
                                                       ByVal strTipDocumentoLog As String,
                                                       ByVal strTipoFactura As String,
                                                       ByVal strNumeroFactura As String,
                                                       ByVal dtmFechaEmision As Date,
                                                       ByVal strObservaciones As String,
                                                       ByVal strDetraccion As String,
                                                       ByVal strMoneda As String,
                                                       ByVal dblBaseImponible As Double,
                                                       ByVal dblBaseInafecta As Double,
                                                       ByVal dblImpuesto As Double,
                                                       ByVal dblMontoFactura As Double,
                                                       ByVal strUsuario As String,
                                                       ByVal strIsFpr As String,
                                                       ByVal strCodigoActividad As String) As Integer
        Try
            _objConexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.LogisticaOfisis)
            Dim objParametros() As Object = {"pvch_CodigoProveedor", strCodigoProveedor,
                                                "pvch_NumeroOCOS", strNumeroOCOS,
                                                "pvch_TipoOCOS", strTipoOCOS,
                                                "pvch_NumDocumentoLog", strNumDocumentoLog,
                                                "pvch_TipDocumentoLog", strTipDocumentoLog,
                                                "pvch_TipoFactura", strTipoFactura,
                                                "pvch_NumeroFactura", strNumeroFactura,
                                                "pdtm_FechaEmision", dtmFechaEmision,
                                                "pvch_Observaciones", strObservaciones,
                                                "pvch_Detraccion", strDetraccion,
                                                "pvch_Moneda", strMoneda,
                                                "pnum_BaseImponible", dblBaseImponible,
                                                "pnum_BaseInafecta", dblBaseInafecta,
                                                "pnum_Impuesto", dblImpuesto,
                                                "pnum_MontoFactura", dblMontoFactura,
                                                "pvch_Usuario", strUsuario,
                                                "pvch_Is_Fpr", strIsFpr,
                                                "pvch_CodigoActividad", strCodigoActividad
                                            }
            Return (_objConexion.EjecutarComando("USP_LOG_EXTRANET_REGISTRAR_FACTURAS_PROVEEDOR_V2", objParametros))
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    '*****************************************************************************************************
    'Objetivo   : Actualizar Facturas de OCOS del Proveedor - Extranet (con ordenes de compra y servicio)
    'Autor      : Juan Cucho
    'Creado     : 27/03/2017
    'Modificado : //
    '*****************************************************************************************************
    Public Function ufn_ActualizarFacturasOCOSProveedor(ByVal intIDFacturaOCOS As Integer,
                                                        ByVal strCodigoProveedor As String,
                                                        ByVal strNumeroOCOS As String,
                                                        ByVal strTipoOCOS As String,
                                                        ByVal strNumDocumentoLog As String,
                                                        ByVal strTipDocumentoLog As String,
                                                        ByVal strTipoFactura As String,
                                                        ByVal strNumeroFactura As String,
                                                        ByVal dtmFechaEmision As Date,
                                                        ByVal strObservaciones As String,
                                                        ByVal strDetraccion As String,
                                                        ByVal strMoneda As String,
                                                        ByVal dblBaseImponible As Double,
                                                        ByVal dblBaseInafecta As Double,
                                                        ByVal dblImpuesto As Double,
                                                        ByVal dblMontoFactura As Double,
                                                        ByVal strUsuario As String,
                                                        ByVal strIsFpr As String,
                                                        ByVal strCodigoActividad As String) As Integer
        Try
            _objConexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.LogisticaOfisis)
            Dim objParametros() As Object = {"pint_IDFacturaOCOS", intIDFacturaOCOS,
                                             "pvch_CodigoProveedor", strCodigoProveedor,
                                             "pvch_NumeroOCOS", strNumeroOCOS,
                                             "pvch_TipoOCOS", strTipoOCOS,
                                             "pvch_NumDocumentoLog", strNumDocumentoLog,
                                             "pvch_TipDocumentoLog", strTipDocumentoLog,
                                             "pvch_TipoFactura", strTipoFactura,
                                             "pvch_NumeroFactura", strNumeroFactura,
                                             "pdtm_FechaEmision", dtmFechaEmision,
                                             "pvch_Observaciones", strObservaciones,
                                             "pvch_Detraccion", strDetraccion,
                                             "pvch_Moneda", strMoneda,
                                             "pnum_BaseImponible", dblBaseImponible,
                                             "pnum_BaseInafecta", dblBaseInafecta,
                                             "pnum_Impuesto", dblImpuesto,
                                             "pnum_MontoFactura", dblMontoFactura,
                                             "pvch_Usuario", strUsuario,
                                             "pvch_Is_Fpr", strIsFpr,
                                             "pvch_CodigoActividad", strCodigoActividad
                                            }
            Return (_objConexion.EjecutarComando("USP_LOG_EXTRANET_ACTUALIZAR_FACTURAS_PROVEEDOR_V2", objParametros))
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    '*****************************************************************************************************
    'Objetivo   : Registrar atención logística de las facturas registradas portal web NM
    'Autor      : Juan Cucho
    'Creado     : 03/04/2017
    'Modificado : //
    '*****************************************************************************************************
    Public Function ufn_ExtranetRegistrarAtencionLogistica(ByVal strCodigoProveedor As String, _
                                                  ByVal strTipoFactura As String, _
                                                  ByVal strNumeroFactura As String, _
                                                  ByVal strOrdenServicio As String, _
                                                  ByVal strNroConformidad As String,
                                                  ByVal strCoUsuaModi As String) As Integer
        Try
            _objConexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.LogisticaOfisis)
            Dim objParametros() As Object = {"pvch_CodigoProveedor", strCodigoProveedor,
                                            "pvch_TipoFactura", strTipoFactura,
                                            "pvch_NumFactura", strNumeroFactura,
                                            "pvch_OrdenServicio", strOrdenServicio,
                                            "pvch_NroConformidad", strNroConformidad,
                                            "pvch_CoUsuaModi", strCoUsuaModi
                                            }

            Return _objConexion.EjecutarComando("USP_LOG_EXTRANET_REGISTRAR_ATENCION_LOGISTICA", objParametros)

        Catch ex As Exception
            Throw ex
        End Try
    End Function

    '*****************************************************************************************************
    'Objetivo   : Registrar atención logística de las facturas registradas portal web NM
    'Autor      : Juan Cucho
    'Creado     : 03/04/2017
    'Modificado : //
    '*****************************************************************************************************
    Public Function ufn_ExtranetRegistrarAtencionLogistica_V2(ByVal strCodigoProveedor As String, _
                                                  ByVal strTipoFactura As String, _
                                                  ByVal strNumeroFactura As String, _
                                                  ByVal strOrdenServicio As String, _
                                                  ByVal strNroConformidad As String,
                                                  ByVal strCoUsuaModi As String) As Integer
        Try
            _objConexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.LogisticaOfisis)
            Dim objParametros() As Object = {"pvch_CodigoProveedor", strCodigoProveedor,
                                            "pvch_TipoFactura", strTipoFactura,
                                            "pvch_NumFactura", strNumeroFactura,
                                            "pvch_OrdenServicio", strOrdenServicio,
                                            "pvch_NroConformidad", strNroConformidad,
                                            "pvch_CoUsuaModi", strCoUsuaModi
                                            }

            Return _objConexion.EjecutarComando("USP_LOG_EXTRANET_REGISTRAR_ATENCION_LOGISTICA_v2", objParametros)

        Catch ex As Exception
            Throw ex
        End Try
    End Function

    '*****************************************************************************************************
    'Objetivo   : Obtener observacion glosa orden de servicio
    'Autor      : Juan Cucho
    'Creado     : 29/05/2017
    'Modificado : //
    '*****************************************************************************************************
    Public Function ufn_ObtenerObservacionGlosaOrdenesServicio(ByVal strCodigoEmpresa As String,
                                                          ByVal strNumeroDocumentoOrigen As String) As String
        Try
            _objConexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.LogisticaOfisis)
            Dim objParametros() As Object = {"pvch_codigoEmpresa", strCodigoEmpresa,
                                             "pvch_numeroDocumentoOrigen ", strNumeroDocumentoOrigen
                                            }

            Return _objConexion.ObtenerDataTable("USP_LOG_OBTENER_OBSERVACION_GLOSA_ORDENES_SERVICIO", objParametros).Rows(0)("DE_OBSE").ToString()
            'Return 1
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    '*****************************************************************************************************
    'Objetivo   : Obtener listado os por numero de orden
    'Autor      : Juan Cucho
    'Creado     : 29/05/2017
    'Modificado : //
    '*****************************************************************************************************
    Public Function ufn_ObtenerListadoOsxNumeroOrden(ByVal strCodigoEmpresa As String, _
                                                  ByVal strcodigoMonedaDocumento As String,
                                                  ByVal strCodigoMonedaNacional As String,
                                                  ByVal strCodigoMonedaExtranjera As String,
                                                  ByVal numFactorCambio As Decimal,
                                                  ByVal strCodigoProveedor As String,
                                                  ByVal strNumeroOrdenServicio As String) As DataTable
        Try
            _objConexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.TesoreriaOfisis)
            Dim objParametros() As Object = {"pvch_codigoEmpresa", strCodigoEmpresa,
                                             "pvch_codigoMonedaDocumento", strcodigoMonedaDocumento,
                                             "pvch_codigoMonedaNacional", strCodigoMonedaNacional,
                                             "pvch_codigoMonedaExtranjera", strCodigoMonedaExtranjera,
                                             "pnum_factorCambio", numFactorCambio,
                                             "pvch_codigoProveedor", strCodigoProveedor,
                                             "pvch_numeroOrdenServicio", strNumeroOrdenServicio
                                            }

            Return _objConexion.ObtenerDataTable("USP_TES_OBTENER_LISTADO_OS_X_NUMERO_ORDEN", objParametros)

        Catch ex As Exception
            Throw ex
        End Try
    End Function
    '*****************************************************************************************************
    'Objetivo   : Registrar distribucion costo automatico para ordenes de servicio
    'Autor      : Juan Cucho
    'Creado     : 01/06/2017
    'Modificado : //
    '*****************************************************************************************************
    Public Function ufn_RegistrarDistribucionCostoAutomaticoOs(ByVal strCodigoEmpresa As String, _
                                                  ByVal strCodigoProveedor As String,
                                                  ByVal strCodigoTipoDocumento As String,
                                                  ByVal strNumeroDocumento As String,
                                                  ByVal strNumeroOrdenServicio As String,
                                                  ByVal strGlosaNumeroOrdenServicio As String,
                                                  ByVal strTipoOperacion As String,
                                                  ByVal strCodigoUsuario As String) As Integer
        Try
            _objConexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.TesoreriaOfisis)
            Dim objParametros() As Object = {"pvch_codigoEmpresa", strCodigoEmpresa,
                                             "pvch_codigoProveedor", strCodigoProveedor,
                                             "pvch_codigoTipoDocumento", strCodigoTipoDocumento,
                                             "pvch_numeroDocumento", strNumeroDocumento,
                                             "pvch_NumeroOrdenServicio", strNumeroOrdenServicio,
                                             "pvch_GlosaNumeroOrdenServicio", strGlosaNumeroOrdenServicio,
                                             "pvch_TipoOperacion", strTipoOperacion,
                                             "pvch_codigoUsuario", strCodigoUsuario
                                            }

            Return _objConexion.EjecutarComando("USP_TES_REGISTRAR_DISTRIBUCION_COSTO_AUTOMATICA_OS", objParametros)
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    '*****************************************************************************************************
    'Objetivo   : obtener listado de modo de distribucion
    'Autor      : Juan Cucho
    'Creado     : 07/06/2017
    'Modificado : //
    '*****************************************************************************************************
    Public Function ufn_ObtenerListadoModoDistribucion(ByVal strCodigoEmpresa As String, _
                                                  ByVal strCodigoModoDistribucion As String) As DataTable
        Try
            _objConexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.TesoreriaOfisis)
            Dim objParametros() As Object = {"pvch_codigoEmpresa", strCodigoEmpresa,
                                             "pvch_codigoModoDistribucion", strCodigoModoDistribucion
                                            }

            Return _objConexion.ObtenerDataTable("USP_TES_OBTENER_LISTADO_MODO_DISTRIBUCION", objParametros)
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    '*****************************************************************************************************
    'Objetivo   : Registrar distribucion costo automatico para ordenes de servicio con plantilla de distribucion
    'Autor      : Juan Cucho
    'Creado     : 01/06/2017
    'Modificado : //
    '*****************************************************************************************************
    Public Function ufn_RegistrarDistribucionCostoAutomaticoOsPlantDistr(ByVal strCodigoEmpresa As String, _
                                                  ByVal strCodigoProveedor As String,
                                                  ByVal strCodigoTipoDocumento As String,
                                                  ByVal strNumeroDocumento As String,
                                                  ByVal strCodigoModoDistribucion As String,
                                                  ByVal numBaseImponible As Decimal,
                                                  ByVal strGlosaObservacion As String,
                                                  ByVal strCodigoUsuario As String) As Integer
        Try
            _objConexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.TesoreriaOfisis)
            Dim objParametros() As Object = {"pvch_codigoEmpresa", strCodigoEmpresa,
                                             "pvch_codigoProveedor", strCodigoProveedor,
                                             "pvch_codigoTipoDocumento", strCodigoTipoDocumento,
                                             "pvch_numeroDocumento", strNumeroDocumento,
                                             "pvch_codigoModoDistribucion", strCodigoModoDistribucion,
                                             "pnum_baseImponible", numBaseImponible,
                                             "pvch_glosaObservacion", strGlosaObservacion,
                                             "pvch_codigoUsuario", strCodigoUsuario
                                            }

            Return _objConexion.EjecutarComando("USP_TES_REGISTRAR_DISTRIBUCION_COSTO_AUTOMATICA_OS_PLANT_DISTR", objParametros)
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    '*****************************************************************************************************
    'Objetivo   : Actualizar validacion por parte de mesa de parte
    'Autor      : Juan Cucho
    'Creado     : 03/04/2017
    'Modificado : //
    '*****************************************************************************************************
    Public Function ufn_AnularFacturaProveedorExtranet(ByVal strCodigoProveedor As String, _
                                                  ByVal strTipoFactura As String, _
                                                  ByVal strNumeroFactura As String, _
                                                  ByVal strUsuario As String, _
                                                  ByVal strEstado As String) As Integer
        Try
            _objConexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.LogisticaOfisis)
            Dim objParametros() As Object = {"pvar_codigoProveedor", strCodigoProveedor,
                                            "pvch_TipoFactura", strTipoFactura,
                                            "pvch_NumeroFactura", strNumeroFactura,
                                            "pvch_Usuario", strUsuario,
                                            "pvch_Estado", strEstado
                                            }

            Return _objConexion.EjecutarComando("USP_LOG_ANULAR_FACTURA_PROVEEDOR_EXTRANET", objParametros)

        Catch ex As Exception
            Throw ex
        End Try
    End Function
    '*****************************************************************************************************
    'Objetivo   : Obtener observacion numero ctc
    'Autor      : Juan Cucho
    'Creado     : 29/05/2017
    'Modificado : //
    '*****************************************************************************************************
    Public Function ufn_ObtenerCodigoOrdenServicioCtc(ByVal strCodigoEmpresa As String,
                                                      ByVal strCodigoProveedor As String,
                                                          ByVal strNumeroOrdenServicio As String) As String
        Try
            _objConexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.LogisticaOfisis)
            Dim objParametros() As Object = {"pvch_codigoEmpresa", strCodigoEmpresa,
                                             "pvch_codigoProveedor", strCodigoProveedor,
                                             "pvch_numeroOrdenServicio ", strNumeroOrdenServicio
                                            }

            Return _objConexion.ObtenerDataTable("USP_LOG_OBTENER_CODIGO_ORDEN_SERVICIO_CTC", objParametros).Rows(0)("CO_ORDE_SERV").ToString()
            'Return 1
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    '*****************************************************************************************************
    'Objetivo   : Obtener documentos pagados y pendientes proveedor
    'Autor      : Juan Cucho
    'Creado     : /05/2017
    'Modificado : //
    '*****************************************************************************************************
    Public Function ufn_ObtenerDocumentosPagadosPendientesProveedor(ByVal strCodigoEmpresa As String,
                                                      ByVal strCodigoProveedor As String,
                                                      ByVal dtmFechaInicial As DateTime,
                                                      ByVal dtmFechaFinal As DateTime,
                                                      ByVal strCodigoFormaPago As String,
                                                      ByVal strCodigoTipoDocumento As String,
                                                      ByVal strNumeroDocumento As String) As DataTable
        Try
            _objConexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.TesoreriaOfisis)
            Dim objParametros() As Object = {"pvch_codigoEmpresa", strCodigoEmpresa,
                                             "pvch_codigoProveedor", strCodigoProveedor,
                                             "pdtm_FechaInicial", dtmFechaInicial,
                                             "pdtm_FechaFinal", dtmFechaFinal,
                                             "pvch_CodigoFormaPago", strCodigoFormaPago,
                                             "pvch_CodigoTipoDocumento", strCodigoTipoDocumento,
                                             "pvch_NumeroDocumento ", strNumeroDocumento
                                            }

            Return _objConexion.ObtenerDataTable("USP_TES_OBTENER_DOCUMENTOS_PAGADOS_PENDIENTES_PROVEEDOR", objParametros)
            'Return 1
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    '*****************************************************************************************************
    'Objetivo   : Obtener tipo de documentos pagados y pendientes proveedor
    'Autor      : Juan Cucho
    'Creado     : 28/05/2017
    'Modificado : //
    '*****************************************************************************************************
    Public Function ufn_ObtenerTipoDocumentosPagadosPendientesProveedor() As DataTable
        Try
            _objConexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.TesoreriaOfisis)
            Dim objParametros() As Object = {}

            Return _objConexion.ObtenerDataTable("USP_TES_OBTENER_TIPO_DOCUMENTOS_PAGADOS_PENDIENTES_PROVEEDOR", objParametros)
            'Return 1
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    '*****************************************************************************************************
    'Objetivo   : Obtener datos documento con cuenta y OT
    'Autor      : Alessandro Ampuero
    'Creado     : 01/03/2017
    'Modificado : //
    '*****************************************************************************************************
    Public Function ufn_ObtenerDatosDocumentoConCuentaOT(ByVal strCodigoEmpresa As String, _
                                                          ByVal strCodigoProveedor As String, _
                                                          ByVal strcodigoTipoDocumento As String,
                                                          ByVal strNumeroDocumento As String,
                                                          ByVal intNumSecu As Integer) As DataTable
        Try
            _objConexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.TesoreriaOfisis)
            Dim objParametros() As Object = {"pvch_codigoEmpresa", strCodigoEmpresa,
                                             "pvch_codigoProveedor", strCodigoProveedor,
                                             "pvch_codigoTipoDocumento", strcodigoTipoDocumento,
                                             "pvch_NumeroDocumento", strNumeroDocumento,
                                             "pint_numSecuencia", intNumSecu
                                            }

            'Return _objConexion.ObtenerDataTable("USP_TES_OBTENER_DATOS_DOCUMENTO_CON_CUENTA", objParametros)
            Return _objConexion.ObtenerDataTable("USP_TES_OBTENER_DATOS_DOCUMENTO_CON_CUENTA_V3", objParametros)


        Catch ex As Exception
            Throw ex
        End Try
    End Function


    '*****************************************************************************************************
    'Objetivo   : Obtener datos documento con cuenta y OT
    'Autor      : Alessandro Ampuero
    'Creado     : 04/03/2017
    'Modificado : //
    '*****************************************************************************************************
    Public Function ufn_InsertarDatosDistribucionCuentaOT(ByVal strCodigoEmpresa As String, _
                                                          ByVal strCodigoProveedor As String, _
                                                          ByVal strcodigoTipoDocumento As String, _ 
                                                          ByVal strNumeroDocumento As String, _
                                                          ByVal intNumSecu As Integer, _
                                                          ByVal dblMontoImporte As Double, _ 
                                                          ByVal dblMontoInput as Double, _ 
                                                          ByVal strTipAuxi As String, _ 
                                                          ByVal strCentCosto as String, _ 
                                                          ByVal strCoCntaEmpr as String, _ 
                                                          ByVal strCoOrdServ As String, _
                                                          ByVal strStDist As String, _ 
                                                          ByVal strDeObse As String, _
                                                          ByVal strNuOrtr As String, _
                                                          ByVal strUsuario As String                                                          
                                                          ) As DataTable
        Try
            _objConexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.TesoreriaOfisis)
            Dim objParametros() As Object = {"pvch_codigoEmpresa", strCodigoEmpresa,
                                             "pvch_codigoProveedor", strCodigoProveedor,
                                             "pvch_codigoTipoDocumento", strcodigoTipoDocumento,
                                             "pvch_NumeroDocumento", strNumeroDocumento,
                                             "pint_numSecuencia", intNumSecu,
                                             "pnum_MontoImporte", dblMontoImporte,
                                             "pnum_MontoInput", dblMontoInput,
                                             "pvchTipAuxiEmpr", strTipAuxi,
                                             "pvchCoCentCosto", strCentCosto,
                                             "pvchCoCntaEmpr", strCoCntaEmpr,
                                             "pvchCoOrdServ", strCoOrdServ,
                                             "pvchStDist", strStDist,
                                             "pvchDeObse", strDeObse,
                                             "pvchNuOrtr", strNuOrtr,
                                             "pvchUsuario", strUsuario
                                            }
            Return _objConexion.ObtenerDataTable("USP_TES_INSERTAR_REGISTRO_DISTRIBUCION", objParametros)


        Catch ex As Exception
            Throw ex
        End Try
    End Function

    '*****************************************************************************************************
    'Objetivo   : Obtener datos documento con cuenta y OT
    'Autor      : Alessandro Ampuero
    'Creado     : 04/03/2017
    'Modificado : //
    '*****************************************************************************************************
    Public Function ufn_EliminarDatosDistribucionCuentaOT(ByVal strCodigoEmpresa As String, _
                                                          ByVal strCodigoProveedor As String, _
                                                          ByVal strcodigoTipoDocumento As String, _
                                                          ByVal strNumeroDocumento As String, _
                                                          ByVal intNumSecu As Integer
                                                          ) As DataTable
        Try
            _objConexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.TesoreriaOfisis)
            Dim objParametros() As Object = {"pvch_codigoEmpresa", strCodigoEmpresa,
                                             "pvch_codigoProveedor", strCodigoProveedor,
                                             "pvch_codigoTipoDocumento", strcodigoTipoDocumento,
                                             "pvch_NumeroDocumento", strNumeroDocumento,
                                             "pint_numSecuencia", intNumSecu
                                            }
            Return _objConexion.ObtenerDataTable("USP_TES_ELIMINAR_REGISTRO_DISTRIBUCION", objParametros)


        Catch ex As Exception
            Throw ex
        End Try
    End Function

    '*****************************************************************************************************
    'Objetivo   : Anular atención
    'Autor      : Juan Cucho
    'Creado     : 28/05/2017
    'Modificado : //
    '*****************************************************************************************************
    Public Function ufn_SpTdatenOrcoD01(ByVal strCodigoEmpresa As String, _
                                                  ByVal strCodigoUsuario As String,
                                                  ByVal strNumeroOrdenServicio As String,
                                                  ByVal intNuSecu As Integer,
                                                  ByVal intNuSecuAten As Integer,
                                                  ByVal decCaAten As Decimal) As Integer
        Try
            _objConexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.LogisticaOfisis)
            Dim objParametros() As Object = {"ISCO_EMPR", strCodigoEmpresa,
                                             "ISCO_USUA", strCodigoUsuario,
                                             "ISNU_ORCO", strNumeroOrdenServicio,
                                             "INNU_SECU", intNuSecu,
                                             "INNU_SECU_ATEN", intNuSecuAten,
                                             "INCA_ATEN", decCaAten
                                            }

            Return _objConexion.EjecutarComando("SP_TDATEN_ORCO_D01", objParametros)
        Catch ex As Exception
            Throw ex
        End Try
    End Function
#End Region
#Region "DESPERDICIOS"
    '*****************************************************************************************************
    'Objetivo   : Obtener Lista de Articulos de Desperdicios - Desperdicios Tejeduria
    'Autor      : Luis Alanoca J.
    'Creado     : 25/09/2017
    'Modificado : //
    '*****************************************************************************************************
    Public Function ufn_BusquedaArticulosDesperdicios(ByVal strCodigo As String, ByVal strDescripcion As String) As DataTable
        Try
            _objConexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.LogisticaOfisis)
            Dim objParametros() As Object = {"pvch_Codigo", strCodigo,
                                             "pvch_Descripcion", strDescripcion}

            Return _objConexion.ObtenerDataTable("USP_LOG_BUSQUEDA_ARTICULOS_DESPERDICIOS", objParametros)

        Catch ex As Exception
            Throw ex
        End Try
    End Function

    '*****************************************************************************************************
    'Objetivo   : Obtener Lista de Proceso Origen - Desperdicios Tejeduria
    'Autor      : Luis Alanoca J.
    'Creado     : 25/09/2017
    'Modificado : //
    '*****************************************************************************************************
    Public Function ufn_BusquedaProcesoOrigen(ByVal strCodigo As String, ByVal strDescripcion As String) As DataTable
        Try
            _objConexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.LogisticaOfisis)
            Dim objParametros() As Object = {"pvch_Codigo", strCodigo,
                                             "pvch_Descripcion", strDescripcion}

            Return _objConexion.ObtenerDataTable("USP_LOG_BUSQUEDA_PROCESO_ORIGEN", objParametros)

        Catch ex As Exception
            Throw ex
        End Try
    End Function

    '*****************************************************************************************************
    'Objetivo   : Obtener Lista de Centro de Costo - Tejeduria
    'Autor      : Luis Alanoca J.
    'Creado     : 25/09/2017
    'Modificado : //
    '*****************************************************************************************************
    Public Function ufn_BusquedaCentroCostosTejeduria(ByVal strCodigo As String, ByVal strDescripcion As String) As DataTable
        Try
            _objConexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.LogisticaOfisis)
            Dim objParametros() As Object = {"pvch_Codigo", strCodigo,
                                             "pvch_Descripcion", strDescripcion}

            Return _objConexion.ObtenerDataTable("USP_LOG_BUSQUEDA_CENTROCOSTOS_TEJEDURIA", objParametros)

        Catch ex As Exception
            Throw ex
        End Try
    End Function


    '*****************************************************************************************************
    'Objetivo   : Registrar Asociacion Proceso Desperdicios - Tejeduria
    'Autor      : Luis Alanoca J.
    'Creado     : 26/09/2017
    'Modificado : //
    '*****************************************************************************************************
    Public Function ufn_RegistrarAsociacionProcesoTejeduria(ByVal strArticuloDesperdicio As String,
                                                            ByVal strCentroCostoTeje As String,
                                                            ByVal strProcesoOrigen As String,
                                                            ByVal strArticuloDesperdicioORI As String,
                                                            ByVal strCentroCostoTejeORI As String,
                                                            ByVal strProcesoOrigenORI As String,
                                                            ByVal strOpcion As String,
                                                            ByVal strUsuario As String) As Integer
        Try
            _objConexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.LogisticaOfisis)
            Dim objParametros() As Object = {"pvch_ArticuloDesperdicio", strArticuloDesperdicio,
                                             "pvch_CentroCostoTeje", strCentroCostoTeje,
                                             "pvch_ProcesoOrigen", strProcesoOrigen,
                                             "pvch_ArticuloDesperdicioORI", strArticuloDesperdicioORI,
                                             "pvch_CentroCostoTejeORI", strCentroCostoTejeORI,
                                             "pvch_ProcesoOrigenORI", strProcesoOrigenORI,
                                             "pvch_Opcion", strOpcion,
                                             "pvch_Usuario", strUsuario
                                            }

            Return (_objConexion.EjecutarComando("USP_LOG_REGISTRA_ASOCIACION_PROCESO_DESPERDICIO_TEJE", objParametros))
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    '*****************************************************************************************************
    'Objetivo   : Obtener Lista de Centro de Costo - Tejeduria
    'Autor      : Luis Alanoca J.
    'Creado     : 25/09/2017
    'Modificado : //
    '*****************************************************************************************************
    Public Function ufn_Listado_Procesos_Desperdicios_Teje() As DataTable
        Try
            _objConexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.LogisticaOfisis)

            Return _objConexion.ObtenerDataTable("USP_LOG_LISTADO_PROCESOS_DESPERDICIOS_TEJEDURIA")

        Catch ex As Exception
            Throw ex
        End Try
    End Function

    '*****************************************************************************************************
    'Objetivo   : Registrar Asociacion Proceso Desperdicios - Tejeduria
    'Autor      : Luis Alanoca J.
    'Creado     : 26/09/2017
    'Modificado : //
    '*****************************************************************************************************
    Public Function ufn_EliminarAsociacionProcesoTejeduria(ByVal strArticuloDesperdicio As String,
                                                           ByVal strCentroCostoTeje As String,
                                                           ByVal strProcesoOrigen As String,
                                                           ByVal strUsuario As String) As Integer
        Try
            _objConexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.LogisticaOfisis)
            Dim objParametros() As Object = {"pvch_ArticuloDesperdicio", strArticuloDesperdicio,
                                             "pvch_CentroCostoTeje", strCentroCostoTeje,
                                             "pvch_ProcesoOrigen", strProcesoOrigen,
                                             "pvch_Usuario", strUsuario
                                            }

            Return (_objConexion.EjecutarComando("USP_LOG_ELIMINAR_ASOCIACION_PROCESO_DESPERDICIO_TEJE", objParametros))
        Catch ex As Exception
            Throw ex
        End Try
    End Function


    '*****************************************************************************************************
    'Objetivo   : Obtener Lista de Cuenta de Gastos
    'Autor      : Luis Alanoca J.
    'Creado     : 18/01/2018
    'Modificado : //
    '*****************************************************************************************************
    Public Function ufn_BusquedaCuentaGastos(ByVal strCodigo As String, ByVal strDescripcion As String, ByVal strCentroCosto As String, ByVal strOpcion As String) As DataTable
        Try
            _objConexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.LogisticaOfisis)
            Dim objParametros() As Object = {"vch_Codigo", strCodigo,
                                             "vch_Descripcion", strDescripcion,
                                             "vch_CentroCosto", strCentroCosto,
                                             "vch_Opcion", strOpcion}

            Return _objConexion.ObtenerDataTable("USP_LOG_BUSQUEDA_CUENTASGASTOS", objParametros)

        Catch ex As Exception
            Throw ex
        End Try
    End Function

    '*****************************************************************************************************
    'Objetivo   : Obtener Lista de Centro de Costos
    'Autor      : Luis Alanoca J.
    'Creado     : 18/01/2018
    'Modificado : //
    '*****************************************************************************************************
    Public Function ufn_BusquedaCentrodeCostos(ByVal strCodigo As String, ByVal strDescripcion As String) As DataTable
        Try
            _objConexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.SeguridadOfisis)
            Dim objParametros() As Object = {"vch_Codigo", strCodigo,
                                             "vch_Descripcion", strDescripcion}

            Return _objConexion.ObtenerDataTable("USP_LOG_BUSQUEDA_CENTRO_DE_COSTO", objParametros)

        Catch ex As Exception
            Throw ex
        End Try
    End Function

    '*****************************************************************************************************
    'Objetivo   : Obtener Lista de Articulos de Desperdicios - Desperdicios Tejeduria
    'Autor      : Luis Alanoca J.
    'Creado     : 25/09/2017
    'Modificado : //
    '*****************************************************************************************************
    Public Function ufn_BusquedaArticulosDesperdiciosAlgodon(ByVal strCodigo As String, ByVal strDescripcion As String) As DataTable
        Try
            _objConexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.LogisticaOfisis)
            Dim objParametros() As Object = {"pvch_Codigo", strCodigo,
                                             "pvch_Descripcion", strDescripcion}

            Return _objConexion.ObtenerDataTable("USP_LOG_BUSQUEDA_ARTICULOS_DESPERDICIOS_ALGODON", objParametros)

        Catch ex As Exception
            Throw ex
        End Try
    End Function

    '*****************************************************************************************************
    'Objetivo   : Obtener Lista de Cuenta de Gastos
    'Autor      : Luis Alanoca J.
    'Creado     : 18/01/2018
    'Modificado : //
    '*****************************************************************************************************
    Public Function ufn_BusquedaOrdenTrabajo(ByVal strCodigo As String, ByVal strDescripcion As String, ByVal strCentroCosto As String, ByVal strOpcion As String) As DataTable
        Try
            _objConexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.LogisticaOfisis)
            Dim objParametros() As Object = {"vch_Codigo", strCodigo,
                                             "vch_Descripcion", strDescripcion,
                                             "vch_CentroCosto", strCentroCosto,
                                             "vch_Opcion", strOpcion}

            Return _objConexion.ObtenerDataTable("USP_LOG_BUSQUEDA_ORDEN_TRABAJO", objParametros)

        Catch ex As Exception
            Throw ex
        End Try
    End Function

    '*****************************************************************************************************
    'Objetivo   : Obtener Lista de Almacen - LOGISTICA
    'Autor      : Luis Alanoca J.
    'Creado     : 09/01/2019
    'Modificado : //
    '*****************************************************************************************************
    Public Function ufn_BusquedaAlmacen(ByVal strCodigo As String, ByVal strDescripcion As String) As DataTable
        Try
            _objConexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.LogisticaOfisis)
            Dim objParametros() As Object = {"pvch_Codigo", strCodigo,
                                             "pvch_Descripcion", strDescripcion}

            Return _objConexion.ObtenerDataTable("USP_LOG_BUSQUEDA_ALMACEN", objParametros)

        Catch ex As Exception
            Throw ex
        End Try
    End Function


    '*****************************************************************************************************
    'Objetivo   : Obtener Lista de Almacen - LOGISTICA
    'Autor      : Luis Alanoca J.
    'Creado     : 09/01/2019
    'Modificado : //
    '*****************************************************************************************************
    Public Function ufn_BusquedaResponsableOT(ByVal strCodigo As String, ByVal strDescripcion As String) As DataTable
        Try
            _objConexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.LogisticaOfisis)
            Dim objParametros() As Object = {"pvch_Codigo", strCodigo,
                                             "pvch_Descripcion", strDescripcion}

            Return _objConexion.ObtenerDataTable("USP_LOG_BUSQUEDA_RESPONSABLE_OT", objParametros)

        Catch ex As Exception
            Throw ex
        End Try
    End Function

    '*****************************************************************************************************
    'Objetivo   : Obtener Lista de Orden de Trabjo
    'Autor      : Luis Alanoca J.
    'Creado     : 31/01/2019
    'Modificado : //
    '*****************************************************************************************************
    Public Function ufn_BusquedaOrdenTrabajo_V2(ByVal strResponsable As String, ByVal strCentroCosto As String, ByVal strCodigo As String, ByVal strDescripcion As String, ByVal strAno As String) As DataTable
        Try
            _objConexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.LogisticaOfisis)
            Dim objParametros() As Object = {"vch_Responsable", strResponsable,
                                             "vch_CentroCosto", strCentroCosto,
                                             "vch_Codigo", strCodigo,
                                             "vch_Descripcion", strDescripcion,
                                             "vch_Ano", strAno}


            Return _objConexion.ObtenerDataTable("USP_LOG_BUSQUEDA_ORDEN_TRABAJO_V2", objParametros)

        Catch ex As Exception
            Throw ex
        End Try
    End Function

    '*****************************************************************************************************
    'Objetivo   : Obtener Lista de Centro de Costo asociados al Responsable de la orden de trabajo
    'Autor      : Luis Alanoca J.
    'Creado     : 31/01/2019
    'Modificado : //
    '*****************************************************************************************************
    Public Function ufn_Listado_CentroCostos_OrdenTrabajo(ByVal strResponsable As String) As DataTable
        Try
            _objConexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.LogisticaOfisis)
            Dim objParametros() As Object = {"vch_Responsable", strResponsable}


            Return _objConexion.ObtenerDataTable("USP_LOG_BUSQUEDA_CENTROCOSTOS_ORDENTRABAJO", objParametros)

        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region
#Region "MAESTRO DIAS LABORABLES"

    '*****************************************************************************************************
    'Objetivo   : Obtener Lista de Dias No Laborables x Modulo
    'Autor      : Luis Alanoca J.
    'Creado     : 24/10/2017
    'Modificado : //
    '*****************************************************************************************************
    Public Function ufn_ListadoDiasNoLaborablesxModulo(ByVal strFechaDesde As String,
                                                       ByVal strFechaHasta As String,
                                                       ByVal strCodigoPlanta As String,
                                                       ByVal strCodigoMaquina As String,
                                                       ByVal strTurno As String,
                                                       ByVal strModulo As String) As DataTable
        Try
            _objConexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.LogisticaOfisis)

            Dim objParametros() As Object = {"pdtm_FechaDesde", strFechaDesde,
                                             "pdtm_FechaHasta", strFechaHasta,
                                             "pvch_CodigPlanta", strCodigoPlanta,
                                             "pvch_CodigoMaquina", strCodigoMaquina,
                                             "pvch_Turno", strTurno,
                                             "pvch_Modulo", strModulo}

            Return _objConexion.ObtenerDataTable("USP_LOG_LISTADO_MAESTRO_DIAS_NO_LABORABLES", objParametros)

        Catch ex As Exception
            Throw ex
        End Try
    End Function

    '*****************************************************************************************************
    'Objetivo   : Registrar Dias No Laborables x Modulo
    'Autor      : Luis Alanoca J.
    'Creado     : 26/10/2017
    'Modificado : //
    '*****************************************************************************************************
    Public Function ufn_RegistrarDiaNoLaboradoxModulo(ByVal strCodigoPlanta As String,
                                                      ByVal strCodigoMaquina As String,
                                                      ByVal strFechaCorta As String,
                                                      ByVal strTurno As String,
                                                      ByVal strHorasNoTrabajadas As String,
                                                      ByVal strCodigoPlantaORI As String,
                                                      ByVal strCodigoMaquinaORI As String,
                                                      ByVal strTurnoORI As String,
                                                      ByVal strFechaNoLaborableORI As String,
                                                      ByVal strOpcion As String,
                                                      ByVal strUsuario As String,
                                                      ByVal strModulo As String) As Integer
        Try
            _objConexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.LogisticaOfisis)
            Dim objParametros() As Object = {"pvch_CodigoPlanta", strCodigoPlanta,
                                             "pvch_CodigoMaquina", strCodigoMaquina,
                                             "pdtm_Fecha", strFechaCorta,
                                             "pvch_Turno", strTurno,
                                             "pvch_HorasNoTrabajadas", strHorasNoTrabajadas,
                                             "pvch_CodigoPlantaORI", strCodigoPlantaORI,
                                             "pvch_CodigoMaquinaORI", strCodigoMaquinaORI,
                                             "pvch_TurnoORI", strTurnoORI,
                                             "pdtm_FechaNoLaborableORI", strFechaNoLaborableORI,
                                             "pvch_Opcion", strOpcion,
                                             "pvch_Usuario", strUsuario,
                                             "pvch_Modulo", strModulo}

            Return (_objConexion.EjecutarComando("USP_LOG_REGISTRA_MAESTRO_DIAS_NO_LABORABLES", objParametros))
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    '*****************************************************************************************************
    'Objetivo   : Eliminar Dias No Laborables x Modulo
    'Autor      : Luis Alanoca J.
    'Creado     : 27/10/2017
    'Modificado : //
    '*****************************************************************************************************
    Public Function ufn_EliminarRegistroDiaNoLaboradoxModulo(ByVal strCodigoPlanta As String,
                                                             ByVal strCodigoMaquina As String,
                                                             ByVal strFechaNoLaborable As String,
                                                             ByVal strTurno As String,
                                                             ByVal strUsuario As String,
                                                             ByVal strModulo As String) As Integer
        Try
            _objConexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.LogisticaOfisis)
            Dim objParametros() As Object = {"pvch_CodigoPlanta", strCodigoPlanta,
                                             "pvch_CodigoMaquina", strCodigoMaquina,
                                             "pvch_FechaNoLaborable", strFechaNoLaborable,
                                             "pvch_Turno", strTurno,
                                             "pvch_Usuario", strUsuario,
                                             "pvch_Modulo", strModulo
                                            }

            Return (_objConexion.EjecutarComando("USP_LOG_ELIMINAR_REGISTRO_DIAS_NO_LABORABLES", objParametros))
        Catch ex As Exception
            Throw ex
        End Try
    End Function
#End Region
#Region "OCTO"
    Public Function ObtenerItemPorDocumento(ByVal strDocu As String, ByVal strEmpr As String) As DataTable
        Try
            _objConexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.VentasOfisis)
            Dim objParametros() As Object = {"ISNU_DOCU", strDocu, "ISCO_EMPR", strEmpr}
            Return _objConexion.ObtenerDataTable("USP_OBTENER_PRODUCTOS_POR_DOCUMENTO", objParametros)
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function GrabarDocumento(ByVal strDocu As String, ByVal dt As DataTable, ByVal strDescuento As String, ByVal strObservaciones As String, ByVal strUsuario As String, ByVal strEmpr As String, ByVal strTipo As String) As String
        Try
            Dim clsUtilitario As New NM_General.Util
            _objConexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.VentasOfisis)
            Dim objParametros() As Object = {"ISNU_DOCU", strDocu, "PVCH_DT", clsUtilitario.GeneraXml(dt), "ISDESC", strDescuento, "ISOBS", strObservaciones, "ISUSUARIO", strUsuario, "ISEMPR", strEmpr, "ISTIPO", strTipo}
            Return _objConexion.ObtenerDataTable("USP_GENERAR_DOCUMENTO_NCR_NDE", objParametros).Rows(0)("RESULTADO").ToString()
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function ValidarDocumento(ByVal strDocu As String) As String
        Try
            Dim clsUtilitario As New NM_General.Util
            _objConexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.VentasOfisis)
            Dim objParametros() As Object = {"ISNU_DOCU", strDocu}
            Return _objConexion.ObtenerDataTable("USP_VALIDAR_DOCUMENTO_GENERADO_NCR", objParametros).Rows(0)("DOCUMENTO").ToString()
        Catch ex As Exception
            Throw ex
        End Try
    End Function
#End Region
#Region "MaestroOT"
    'ObtenerDatosOrdenTrabajo
    Public Function ObtenerDatosOrdenTrabajo(ByVal strOT As String, ByVal strCodResp As String, ByVal strAno As String) As DataTable
        Try
            _objConexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.LogisticaOfisis)
            Dim objParametros() As Object = {"NU_ORTR", strOT, "CO_RES", strCodResp, "ANIO", strAno}
            Return _objConexion.ObtenerDataTable("USP_LISTAR_ORDEN_TRABAJO_AUXI_CNTA", objParametros)
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function ActualizarDatosOrdenTrabajo(ByVal Num_OT As String, ByVal strDescOT As String, ByVal Costo As String, ByVal Gasto As String, ByVal Obser As String, ByVal Imp1 As Decimal, ByVal Imp2 As Decimal, _
                                                ByVal Imp3 As Decimal, ByVal Imp4 As Decimal, ByVal Imp5 As Decimal, ByVal Imp6 As Decimal, ByVal Imp7 As Decimal, ByVal Imp8 As Decimal, _
                                                ByVal Imp9 As Decimal, ByVal Imp10 As Decimal, ByVal Imp11 As Decimal, ByVal Imp12 As Decimal, ByVal strUsuario As String) As DataTable
        Try
            _objConexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.LogisticaOfisis)
            Dim objParametros() As Object = {"NUM_OT", Num_OT, "DESC_OT", strDescOT, "CE_CO", Costo, "CU_GA", Gasto, "OBS_OT", Obser, "IMP1", Imp1, "IMP2", Imp2, "IMP3", Imp3, "IMP4", Imp4, "IMP5", Imp5, "IMP6", Imp6, "IMP7", Imp7, "IMP8", Imp8, _
                                             "IMP9", Imp9, "IMP10", Imp10, "IMP11", Imp11, "IMP12", Imp12, "CO_USUA", strUsuario}
            Return _objConexion.ObtenerDataTable("USP_ACTUALIZA_DATOS_ORDEN_TRABAJO", objParametros)
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function EliminarDatosOrdenTrabajo(ByVal Num_OT As String) As DataTable
        Try
            _objConexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.LogisticaOfisis)
            Dim objParametros() As Object = {"NUM_OT", Num_OT}
            Return _objConexion.ObtenerDataTable("USP_ELIMINA_DATOS_ORDEN_TRABAJO", objParametros)
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function AgregarDatosOrdenTrabajo(ByVal StrOT As String, ByVal strCodRes As String, ByVal DescOT As String, ByVal Costo As String, ByVal Gasto As String, ByVal Obser As String, ByVal Imp1 As Decimal, ByVal Imp2 As Decimal, _
                                                ByVal Imp3 As Decimal, ByVal Imp4 As Decimal, ByVal Imp5 As Decimal, ByVal Imp6 As Decimal, ByVal Imp7 As Decimal, ByVal Imp8 As Decimal, _
                                                ByVal Imp9 As Decimal, ByVal Imp10 As Decimal, ByVal Imp11 As Decimal, ByVal Imp12 As Decimal, ByVal strUsuario As String) As DataTable
        Try
            _objConexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.LogisticaOfisis)
            Dim objParametros() As Object = {"CO_OT", StrOT, "COD_RESP", strCodRes, "DESC_OT", DescOT, "CE_CO", Costo, "CU_GA", Gasto, "OBS_OT", Obser, "IMP1", Imp1, "IMP2", Imp2, "IMP3", Imp3, "IMP4", Imp4, "IMP5", Imp5, "IMP6", Imp6, "IMP7", Imp7, "IMP8", Imp8, _
                                             "IMP9", Imp9, "IMP10", Imp10, "IMP11", Imp11, "IMP12", Imp12, "CO_USUA", strUsuario}
            Return _objConexion.ObtenerDataTable("USP_AGREGAR_DATOS_ORDEN_TRABAJO", objParametros)
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function ValidarDescripcionOT(ByVal strOT As String) As String
        Try
            _objConexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.LogisticaOfisis)
            Dim objParametros() As Object = {"CO_OT", strOT}
            Return _objConexion.ObtenerDataTable("USP_VALIDAR_DESCRIPCION_OT", objParametros).Rows(0).Item("RESULTADO").ToString()
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    'REQSIS201900032 - DG - INI
    Public Function ObtenerSalidasEstados(ByVal strFecIni As String, ByVal strFecFin As String, ByVal strSalida As String) As DataTable
        Try
            _objConexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.LogisticaOfisis)
            Dim objParametros() As Object = {"FEC_INI", strFecIni, "FEC_FIN", strFecFin, "SALIDA", strSalida}
            Return _objConexion.ObtenerDataTable("USP_OBTENER_SALIDAS_ESTADO", objParametros)
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function ObtenerSalidasEstadosPorSalida(ByVal strSalida As String) As DataTable
        Try
            _objConexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.LogisticaOfisis)
            Dim objParametros() As Object = {"SALIDA", strSalida}
            Return _objConexion.ObtenerDataTable("USP_OBTENER_ESTADOS_POR_SALIDA", objParametros)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function ActualizarEstadiosSalida(ByVal strSalida As String, ByVal strFlg As String, ByVal strUsuario As String, ByVal intPiso As Integer) As Boolean
        Dim flg As Boolean = False
        Try
            _objConexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.LogisticaOfisis)

            Dim objParametros() As Object = {"SALIDA", strSalida, "FLG", strFlg, "USUARIO", strUsuario, "PISO", intPiso}
            _objConexion.EjecutarComando("USP_ACTUALIZAR_ESTADO_SALIDA", objParametros)
            Return flg = True
        Catch ex As Exception
            flg = False
            Throw ex
        End Try
    End Function
    'REQSIS201900032 - DG - FIN
    Public Function ListarAnioOT() As DataTable
        Try
            _objConexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.LogisticaOfisis)
            Dim objParametros() As Object = {}
            Return _objConexion.ObtenerDataTable("USP_LISTAR_ANIO_OT", objParametros)
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function ObtenerListadoPersonalEscolaridad(ByVal strAnio As String) As DataTable
        Try
            _objConexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.LogisticaOfisis)
            Dim objParametros() As Object = {"vch_Anio", strAnio}
            Return _objConexion.ObtenerDataTable("USP_LISTADO_PERSONAL_ESCOLARIDAD", objParametros)
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function ObtenerListadoPersonalPrestamo(ByVal strAnio As String) As DataTable
        Try
            _objConexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.LogisticaOfisis)
            Dim objParametros() As Object = {"vch_Anio", strAnio}
            Return _objConexion.ObtenerDataTable("USP_LISTADO_PERSONAL_PRESTAMO", objParametros)
        Catch ex As Exception
            Throw ex
        End Try
    End Function
#End Region
End Class
