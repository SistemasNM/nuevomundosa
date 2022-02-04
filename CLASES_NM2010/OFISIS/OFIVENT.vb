Imports NuevoMundo.Generales
Imports NM.AccesoDatos
Namespace OFIVENT
    Public Class Vendedores
        Inherits Clases.General
        Implements Interfases.IOFISIS

#Region "    Constantes"
        Private CONST_SP_LISTAR = "usp_qry_Vendedores"
        Private Const CONST_NOMBRE_TABLA_VENDEDORES = "VENDEDORES"
#End Region

#Region "    Variables de propiedades"
        Private mstrVendedorCodigo As String
        Private mstrVendedorAppPaterno As String
        Private mstrVendedorAppMaterno As String
        Private mstrVendedorNombres As String
#End Region

#Region "    Propiedades"
        Public Property Codigo() As String
            Get
                Codigo = mstrVendedorCodigo
            End Get
            Set(ByVal Value As String)
                mstrVendedorCodigo = Value
            End Set
        End Property
        Public Property AppPaterno() As String
            Get
                AppPaterno = mstrVendedorAppPaterno
            End Get
            Set(ByVal Value As String)
                mstrVendedorAppPaterno = Value
            End Set
        End Property
        Public Property AppMaterno() As String
            Get
                AppMaterno = mstrVendedorAppMaterno
            End Get
            Set(ByVal Value As String)
                mstrVendedorAppMaterno = Value
            End Set
        End Property
        Public Property Nombres() As String
            Get
                Nombres = mstrVendedorNombres
            End Get
            Set(ByVal Value As String)
                mstrVendedorNombres = Value
            End Set
        End Property
#End Region

#Region "    Constructor"
        Sub New(ByVal pstrEmpresa As String, ByVal pstrUsuario As String)
            Me.EmpresaCodigo = pstrEmpresa
            Me.UsuarioBD = pstrUsuario
        End Sub
#End Region

#Region "    Metodos publicos"
        Public Function Buscar() As Boolean Implements Interfases.IOFISIS.Buscar

        End Function
        Public Function Listar(ByRef pLista As System.Data.DataTable, ByVal ParamArray Flags() As String) As Boolean Implements Interfases.IOFISIS.Listar
            Dim lobjBD As NM.AccesoDatos.AccesoDatosSQLServer
            Dim lbooOk As Boolean

            Try
                lobjBD = New NM.AccesoDatos.AccesoDatosSQLServer(NM.AccesoDatos.GeneradorCadenaConexion.enmBasesDatos.VentasOfisis)
                pLista = lobjBD.ObtenerDataTable(CONST_SP_LISTAR)
                pLista.TableName = CONST_NOMBRE_TABLA_VENDEDORES
                lbooOk = True
            Catch ex As Exception
                lbooOk = False
                pLista = Nothing
            Finally
                lobjBD = Nothing
            End Try
            Return lbooOk
        End Function
        Public Sub Dispose() Implements Interfases.IOFISIS.Dispose

        End Sub
#End Region

    End Class
    Public Class Clientes
        Inherits Clases.General
        Implements Interfases.IOFISIS

#Region "    Constantes"
        Private CONST_SP_LISTAR = "usp_qry_ClientesListar"
        Private CONST_SP_BUSCAR = "usp_qry_ClientesBuscar"
        Private Const CONST_NOMBRE_TABLA_CLIENTES = "CLIENTES"
#End Region

#Region "    Variables"
        Private mstrCodigo As String
        Private mstrNombre As String
        Private mstrRUC As String
#End Region

#Region "    Propiedades"



        Public Property Codigo() As String
            Get
                Codigo = mstrCodigo
            End Get
            Set(ByVal Value As String)
                mstrCodigo = Value
            End Set
        End Property
        Public Property Nombre() As String
            Get
                Nombre = mstrNombre
            End Get
            Set(ByVal Value As String)
                mstrNombre = Value
            End Set
        End Property
        Public Property RUC() As String
            Get
                RUC = mstrRUC
            End Get
            Set(ByVal Value As String)
                mstrRUC = Value
            End Set
        End Property
#End Region

#Region "    Constructor"
        Sub New(ByVal pstrEmpresa As String, ByVal pstrUsuario As String)
            Me.EmpresaCodigo = pstrEmpresa
            Me.UsuarioBD = pstrUsuario
        End Sub
#End Region

#Region "   Metodos publicos"
        Public Function Buscar() As Boolean Implements Interfases.IOFISIS.Buscar
            Dim lobjBD As NM.AccesoDatos.AccesoDatosSQLServer
            Dim ldtRes As DataTable
            Dim larrParams() As Object = {"P_CO_EMPR", Me.EmpresaCodigo, "P_CO_CLIE", mstrCodigo}

            Me.LimpiarError()
            Try
                lobjBD = New NM.AccesoDatos.AccesoDatosSQLServer(NM.AccesoDatos.GeneradorCadenaConexion.enmBasesDatos.VentasOfisis)
                ldtRes = lobjBD.ObtenerDataTable(CONST_SP_BUSCAR, larrParams)
                mstrNombre = ldtRes.Rows(0).Item("NO_CLIE")
                mstrRUC = ldtRes.Rows(0).Item("NU_RUCS")
                Me.Ok = True
            Catch ex As Exception
                Me.Ok = False
                'Me.Mensaje = New NuevoMundo.Generales.Clases.NMMensaje _
                '                        ("OFISIS:OFIVENT:Clientes.Buscar", _
                '                        "0001", _
                '                        "Error al consultar", _
                '                        ex.Message, _
                '                        Clases.NMMensaje.enuTiposMensajes.Error)
            Finally
                ldtRes = Nothing
                lobjBD = Nothing
            End Try
            Return Me.Ok
        End Function
        Public Function Listar(ByRef pLista As System.Data.DataTable, ByVal ParamArray Flags() As String) As Boolean Implements Interfases.IOFISIS.Listar
            Dim lobjCon As NM.AccesoDatos.AccesoDatosSQLServer
            Dim larrParams() As Object = {"P_CO_EMPR", Me.EmpresaCodigo}

            Try
                lobjCon = New NM.AccesoDatos.AccesoDatosSQLServer(NM.AccesoDatos.GeneradorCadenaConexion.enmBasesDatos.VentasOfisis)
                pLista = lobjCon.ObtenerDataTable(CONST_SP_LISTAR, larrParams)
                pLista.TableName = CONST_NOMBRE_TABLA_CLIENTES
                Me.Ok = True
            Catch ex As Exception
                pLista = Nothing
                Me.Ok = False
            Finally
                lobjCon = Nothing
            End Try
            Return Me.Ok
        End Function
        Public Sub Dispose() Implements Interfases.IOFISIS.Dispose
            Me.EmpresaCodigo = Nothing
            Me.UsuarioBD = Nothing
            Me.Codigo = Nothing
            Me.Nombre = Nothing
            Me.RUC = Nothing
            'Me.Mensaje = Nothing
        End Sub
#End Region

    End Class
    Public Class Ventas
#Region "Variables"
        Private _objConnexion As AccesoDatosSQLServer
#End Region
#Region "Propiedades"
        Public clsError As String = ""

#End Region
#Region "Constructores"
        Sub New()

        End Sub
#End Region
#Region "Metodos y Funciones"
        Public Function ufn_ObtenerCliente(ByVal pstrCodigoCliente As String, ByVal pstrNombreCliente As String) As DataTable
            Try
                _objConnexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.VentasOfisis)
                Dim objParametros() As Object = {"p_var_Codigocliente", pstrCodigoCliente, "p_var_NombreCliente", pstrNombreCliente}
                Return _objConnexion.ObtenerDataTable("usp_qry_ObtenerCliente", objParametros)
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function PedidoPreVentaListar(ByVal strEmpresa As String, ByVal strNroPedido As String, ByVal strEstDocu As String, ByRef objDT As DataTable) As Boolean
            Dim blnRpt As Boolean = False

            Try
                _objConnexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.VentasOfisis)
                Dim objParametros() As Object = {"chr_CodEmpresa", strEmpresa, "vch_NumPedi", strNroPedido, "vch_EstDocu", strEstDocu}
                objDT = _objConnexion.ObtenerDataTable("usp_venweb_PedidosPendListar", objParametros)
                blnRpt = True
            Catch ex As Exception
                clsError = ex.Message
                Throw ex
            End Try

            PedidoPreVentaListar = blnRpt

    End Function

    Public Function PedidoPreVentaDetListar(ByVal strEmpresa As String, ByVal strNroGuia As String, ByRef objDT As DataTable) As Boolean
      Dim blnRpt As Boolean = False

      Try
        _objConnexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.VentasOfisis)
        Dim objParametros() As Object = {"CO_EMPR", strEmpresa, "NU_GUIA", strNroGuia}
        objDT = _objConnexion.ObtenerDataTable("usp_venweb_PedidosPendListarDet", objParametros)
        blnRpt = True
      Catch ex As Exception
        clsError = ex.Message
        Throw ex
      End Try

      PedidoPreVentaDetListar = blnRpt

    End Function


    Public Function PedidoPreVentaHoraProc(ByVal strEmpresa As String) As String
      Dim strRpt As String = ""
      Dim objDT As DataTable

      Try
        _objConnexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.VentasOfisis)
        Dim objParametros() As Object = {"CO_EMPR", strEmpresa}
        objDT = _objConnexion.ObtenerDataTable("usp_venweb_HoraProcesoBco_Listar", objParametros)
        If objDT.Rows.Count > 0 Then
          strRpt = objDT.Rows(0)("vch_HoraBanco").ToString
        Else
          strRpt = ""
        End If

      Catch ex As Exception
        clsError = ex.Message
        Throw ex
      End Try

      PedidoPreVentaHoraProc = strRpt

    End Function

    Public Function PedidoEstadoAct(ByVal strEmpresa As String, ByVal strNroPedido As String, ByVal strEstDocu As String, ByVal strUsuario As String) As Boolean
      Dim blnRpt As Boolean = False

      Try
        _objConnexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.VentasOfisis)
        Dim objParametros() As Object = {"chr_CodEmpresa", strEmpresa, _
                                         "vch_NumPedi", strNroPedido, _
                                         "vch_EstDocu", strEstDocu,
                                         "vch_UsuModi", strUsuario}
        _objConnexion.EjecutarComando("usp_venweb_PedidosEstadoAct", objParametros)
        blnRpt = True
      Catch ex As Exception
        clsError = ex.Message
        Throw ex
      End Try

      PedidoEstadoAct = blnRpt

    End Function


    Public Function PedidoPreVentaDatos(ByRef objDT As DataTable) As Boolean
      Dim blnRpt As Boolean = False

      Try
        _objConnexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.VentasOfisis)
        Dim objParametros() As Object = {"chr_Empresa", ""}
        objDT = _objConnexion.ObtenerDataTable("usp_venweb_ListaDatoBanco", objParametros)
        blnRpt = True
      Catch ex As Exception
        clsError = ex.Message
        Throw ex
      End Try

      PedidoPreVentaDatos = blnRpt
    End Function


    Public Function ObtenerLoteEnvio(ByVal strEmpresa As String, ByVal NumReg As Integer, ByVal Imptotal As Double, ByVal strUsuario As String, ByRef objDT As DataTable) As Boolean
      Dim blnRpt As Boolean = False

      Try
        _objConnexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.VentasOfisis)
        Dim objParametros() As Object = {"chr_CodEmpresa", strEmpresa, _
                                         "int_NumReg", NumReg, _
                                         "num_ImpTotal", Imptotal, _
                                         "vch_UsuCrea", strUsuario}
        objDT = _objConnexion.ObtenerDataTable("usp_venweb_GenerarLote", objParametros)
        blnRpt = True
      Catch ex As Exception
        clsError = ex.Message
        Throw ex
      End Try

      ObtenerLoteEnvio = blnRpt
    End Function

    Public Function RegistrarPedidoLote(ByVal strEmpresa As String, _
                                        ByVal intLoteEnvio As Integer, _
                                        ByVal strNumPedi As String, _
                                        ByVal strFechaDoc As String, _
                                        ByVal lblImpTotal As Double, _
                                        ByVal strUsuario As String) As Boolean
      Dim blnRpt As Boolean = False

      Try
        _objConnexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.VentasOfisis)
        Dim objParametros() As Object = {"chr_CodEmpresa", strEmpresa, _
                                         "int_LoteEnvio", intLoteEnvio, _
                                         "vch_NumPedi", strNumPedi, _
                                         "dtm_FecEmis", strFechaDoc, _
                                         "num_ImpPedi", lblImpTotal, _
                                         "vch_UsuCrea", strUsuario}
        _objConnexion.EjecutarComando("usp_venweb_RegistrarPedidoLote", objParametros)
        blnRpt = True
      Catch ex As Exception
        clsError = ex.Message
        Throw ex
      End Try

      RegistrarPedidoLote = blnRpt
    End Function


    Public Function ActualizarPedidoLote(ByVal strEmpresa As String, _
                                         ByVal intLoteEnvio As Integer, _
                                         ByVal strNumPedi As String, _
                                         ByVal strFechaPag As String, _
                                         ByVal lblImpTotal As Double, _
                                         ByVal strUsuario As String) As Boolean
      Dim blnRpt As Boolean = False

      Try
        _objConnexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.VentasOfisis)
        Dim objParametros() As Object = {"chr_CodEmpresa", strEmpresa, _
                                         "int_LoteEnvio", intLoteEnvio, _
                                         "vch_NumPedi", strNumPedi, _
                                         "dtm_FecCanc", strFechaPag, _
                                         "num_ImpCanc", lblImpTotal, _
                                         "vch_UsuModi", strUsuario}
        _objConnexion.EjecutarComando("usp_venweb_ActualizaPedidoLote", objParametros)
        blnRpt = True
      Catch ex As Exception
        clsError = ex.Message
        Throw ex
      End Try

      ActualizarPedidoLote = blnRpt

    End Function


#End Region
  End Class

End Namespace
