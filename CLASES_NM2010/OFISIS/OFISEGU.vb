Imports NuevoMundo.Generales
Namespace OFISEGU
    Public Class Auxiliares
        Inherits Clases.General
        Implements Interfases.IOFISIS

#Region "   Constantes"
        Private CONST_SP_LISTAR = "usp_qry_AuxiliaresListar"
        Private Const CONST_NOMBRE_TABLA_AUXILIARES = "AUXILIARES"
#End Region

#Region "    Variables"
        Private mstrCodigo As String
        Private mstrNombre As String
        Private mbooActivo As Boolean
#End Region

#Region "   Enumeraciones"
        Enum enuTiposAuxiliar
            [Todos] = 0
            [CentrosCosto] = 1
        End Enum
#End Region

#Region "    Propiedades"
        Public Property Codigo() As String
            Get
                Codigo = mstrcodigo
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
        Public Property Activo() As Boolean
            Get
                Activo = mbooActivo
            End Get
            Set(ByVal Value As Boolean)
                mbooActivo = Value
            End Set
        End Property
#End Region

#Region "    Constructor"
        Sub New(ByVal pstrEmpresa As String, ByVal pstrUsuario As String)
            Me.EmpresaCodigo = pstrEmpresa
            Me.UsuarioBD = pstrUsuario
        End Sub
#End Region

#Region "   Metodos"
        Public Function Listar(ByRef pLista As DataTable, ByVal ParamArray Flags() As String) As Boolean Implements Interfases.IOFISIS.Listar
            Dim lobjBD As NM.AccesoDatos.AccesoDatosSQLServer
            Dim lbooOk As Boolean
            Dim larrParams() As Object

            ReDim larrParams(13)
            larrParams(0) = "P_CO_EMPR"
            larrParams(1) = Me.EmpresaCodigo
            larrParams(2) = "P_TIPO"
            larrParams(3) = "K"
            larrParams(4) = "P_LONGITUD"
            larrParams(5) = 7
            larrParams(6) = "P_ESTADO"
            larrParams(7) = 1
            larrParams(8) = "p_Codigo"
            larrParams(9) = Flags(0)
            larrParams(10) = "p_Nombre"
            larrParams(11) = Flags(1)
            larrParams(12) = "var_CuentaGasto"
            larrParams(13) = Flags(2)
            Try
                lobjBD = New NM.AccesoDatos.AccesoDatosSQLServer(NM.AccesoDatos.GeneradorCadenaConexion.enmBasesDatos.SeguridadOfisis)
                pLista = lobjBD.ObtenerDataTable(CONST_SP_LISTAR, larrParams)
                pLista.TableName = CONST_NOMBRE_TABLA_AUXILIARES
                lbooOk = True
            Catch ex As Exception
                lbooOk = False
            Finally
                lobjBD = Nothing
            End Try
            Return lbooOk
        End Function
        Public Function Buscar() As Boolean Implements Interfases.IOFISIS.Buscar
            Dim lobjBD As NM.AccesoDatos.AccesoDatosSQLServer
            Dim lbooOk As Boolean
            Dim larrParams() As String
            Dim ldtRes As DataTable

            ReDim larrParams(11)
            larrParams(0) = "P_CO_EMPR"
            larrParams(1) = Me.EmpresaCodigo
            larrParams(2) = "P_TIPO"
            larrParams(3) = "K"
            larrParams(4) = "P_LONGITUD"
            larrParams(5) = 7
            larrParams(6) = "P_ESTADO"
            larrParams(7) = 1
            larrParams(8) = "p_Codigo"
            larrParams(9) = mstrCodigo
            larrParams(10) = "p_Nombre"
            larrParams(11) = ""

            Try
                lobjBD = New NM.AccesoDatos.AccesoDatosSQLServer(NM.AccesoDatos.GeneradorCadenaConexion.enmBasesDatos.SeguridadOfisis)
                ldtRes = lobjBD.ObtenerDataTable(CONST_SP_LISTAR, larrParams)
                ldtRes.TableName = CONST_NOMBRE_TABLA_AUXILIARES
                If ldtRes.Rows.Count = 1 Then
                    lbooOk = True
                    mstrNombre = ldtRes.Rows(0)("NO_AUXI")
                Else
                    lbooOk = False
                    mstrNombre = ""
                End If
            Catch ex As Exception
                lbooOk = False
                mstrNombre = ""
            Finally
                lobjBD = Nothing
            End Try
            Return lbooOk
        End Function
        Public Sub Dispose() Implements Interfases.IOFISIS.Dispose

        End Sub
#End Region

    End Class
    Public Class Usuarios
        Inherits Clases.General
        Implements Interfases.IOFISIS

#Region "   Constantes"
        Private CONST_SP_BUSCAR = "usp_qry_UsuarioBuscar"
        Private Const CONST_NOMBRE_USUARIO = "USUARIO"
#End Region

#Region "    Variables"
        Private mstrCodigo As String = ""
        Private mstrEmpresaPredeterminadaCodigo As String
        Private mstrCorreoElectronico As String
        Private mstrClave As String
        Private mbooActivo As Boolean
        Private mstrGrupoCodigo As String
        Private mstrNombres As String

        Private mstrError As String = ""
#End Region

#Region "    Constructor"
        Sub New(ByVal pstrEmpresa As String, ByVal pstrUsuario As String)
            Me.EmpresaCodigo = pstrEmpresa
            Me.UsuarioBD = pstrUsuario
        End Sub
#End Region

#Region "   Propiedades"
        Public Property NMError() As String
            Get
                NMError = mstrError
            End Get
            Set(ByVal Value As String)
                mstrError = Value
            End Set
        End Property
        Public Property Codigo() As String
            Get
                Codigo = mstrCodigo
            End Get
            Set(ByVal Value As String)
                mstrCodigo = Value
            End Set
        End Property
        Public Property EmpresaPredeterminadaCodigo() As String
            Get
                EmpresaPredeterminadaCodigo = mstrEmpresaPredeterminadaCodigo
            End Get
            Set(ByVal Value As String)
                mstrEmpresaPredeterminadaCodigo = Value
            End Set
        End Property
        Public Property CorreoElectronico() As String
            Get
                CorreoElectronico = mstrCorreoElectronico
            End Get
            Set(ByVal Value As String)
                mstrCorreoElectronico = Value
            End Set
        End Property
        Public Property Clave() As String
            Get
                Clave = mstrClave
            End Get
            Set(ByVal Value As String)
                mstrClave = Value
            End Set
        End Property
        Public Property Activo() As Boolean
            Get
                Activo = mbooActivo
            End Get
            Set(ByVal Value As Boolean)
                mbooActivo = Value
            End Set
        End Property
        Public Property GrupoCodigo() As String
            Get
                GrupoCodigo = mstrGrupoCodigo
            End Get
            Set(ByVal Value As String)
                mstrGrupoCodigo = Value
            End Set
        End Property
        Public Property Nombres() As String
            Get
                Nombres = mstrNombres
            End Get
            Set(ByVal Value As String)
                mstrNombres = Value
            End Set
        End Property
#End Region

#Region "  Metodos"
        Public Function Buscar() As Boolean Implements Interfases.IOFISIS.Buscar
            Dim lobjBD As NM.AccesoDatos.AccesoDatosSQLServer
            Dim larrParams() As Object = {"P_CO_USUA", mstrCodigo}
            Dim ldsRes As DataSet
            Dim lbooOk As Boolean

            Try
                lobjBD = New NM.AccesoDatos.AccesoDatosSQLServer(NM.AccesoDatos.GeneradorCadenaConexion.enmBasesDatos.SeguridadOfisis)
                ldsRes = lobjBD.ObtenerDataSet(CONST_SP_BUSCAR, larrParams)
                If ldsRes.Tables(0).Rows.Count = 1 Then
                    With ldsRes.Tables(0).Rows(0)
                        mstrNombres = .Item("NO_USUA")
                        mstrClave = .Item("NO_CLAV")
                        mstrGrupoCodigo = .Item("CO_GRUP")
                        mstrCorreoElectronico = .Item("DE_DIRE_MAIL")
                        mbooActivo = IIf(.Item("ST_ACTI") = "S", True, False)
                        mstrEmpresaPredeterminadaCodigo = .Item("CO_EMPR")
                    End With
                    lbooOk = True
                    mstrError = ""
                Else
                    lbooOk = False
                    mstrError = "El usuario " + mstrCodigo + " no existe."
                End If
            Catch ex As Exception
                mstrError = "Error inesperado al consultar, intentelo de nuevo o comuníquese con el administrador del sistema."
                lbooOk = False
            Finally
                ldsRes = Nothing
                lobjBD = Nothing
            End Try
            Return lbooOk
        End Function
        Public Function Listar(ByRef pLista As System.Data.DataTable, ByVal ParamArray Flags() As String) As Boolean Implements Interfases.IOFISIS.Listar
            Dim lobjCon As NM.AccesoDatos.AccesoDatosSQLServer
            Dim larrParams() As Object = {"var_Empresa", Me.EmpresaCodigo, _
                                        "var_Codigo", Flags(0), _
                                        "var_Nombre", Flags(1)}

            Me.LimpiarError()
            Try
                lobjCon = New NM.AccesoDatos.AccesoDatosSQLServer(NM.AccesoDatos.GeneradorCadenaConexion.enmBasesDatos.SeguridadOfisis)
                pLista = lobjCon.ObtenerDataTable("usp_qry_UsuariosListar", larrParams)
                Me.Ok = True
            Catch ex As Exception
                pLista = Nothing
                Me.Ok = False
            Finally
                lobjCon = Nothing
            End Try
            Return Me.Ok
        End Function
        Public Function Mostar_Usuario(ByVal strUsuarios As String) As DataSet
            Dim obj As NM.AccesoDatos.AccesoDatosSQLServer
            Dim larrParams() As Object = {"CO_USUA", strUsuarios}
            Try
                obj = New NM.AccesoDatos.AccesoDatosSQLServer(NM.AccesoDatos.GeneradorCadenaConexion.enmBasesDatos.SeguridadOfisis)
                Return (obj.ObtenerDataSet("usp_seg_firma_usuario", larrParams))
            Catch ex As Exception
                Throw ex
            End Try
            obj = Nothing
        End Function
        Public Sub Dispose() Implements Interfases.IOFISIS.Dispose

        End Sub
#End Region

    End Class
    Public Class Empresas
        Inherits Clases.General
        Implements Interfases.IOFISIS

#Region "   Constantes"
        Private CONST_SP_BUSCAR = "usp_qry_EmpresaBuscar"
        Private Const CONST_NOMBRE_EMPRESA = "EMPRESA"
#End Region

#Region "    Variables"
        Private mstrCodigo As String = ""
        Private mstrNombre As String = ""
        Private mbooEstado As Boolean = True
#End Region

#Region "    Constructor"
        Sub New(ByVal pstrEmpresa As String, ByVal pstrUsuario As String)
            Me.EmpresaCodigo = pstrEmpresa
            Me.UsuarioBD = pstrUsuario
        End Sub
#End Region

#Region "    Metodos"
        Public Function Buscar() As Boolean Implements Interfases.IOFISIS.Buscar

        End Function
        Public Function Listar(ByRef pLista As System.Data.DataTable, ByVal ParamArray Flags() As String) As Boolean Implements Interfases.IOFISIS.Listar

        End Function
        Public Sub Dispose() Implements Interfases.IOFISIS.Dispose

        End Sub
#End Region

    End Class
    Public Class Aprobaciones
        Inherits Clases.General


#Region "    Constructor"
        Sub New(ByVal pstrEmpresa As String, ByVal pstrUsuario As String)
            Me.EmpresaCodigo = pstrEmpresa
            Me.UsuarioBD = pstrUsuario
        End Sub
#End Region

#Region "   Metodos"
        Public Function Generar_Aprobacion(ByVal strCodigoEmpresa As String, ByVal strCodAprobacion As String, _
                                            ByVal strNumeroDocumento As String, ByVal strFecha As String, _
                                            ByVal strObservacion As String, ByVal strEstadoSoli As String, _
                                            ByVal strFechaSolicitud As String, ByVal strTipoAuxiliar As String, _
                                            ByVal strCodigoAuxiliar As String, ByVal strUsuario As String, _
                                            ByVal strFechaCreacion As String, ByVal strUsuarioModi As String, _
                                            ByVal strFechaModi As String, Optional ByRef pdtCorreos As DataTable = Nothing) As Boolean
            Dim lobjCon As NM.AccesoDatos.AccesoDatosSQLServer
            Dim Params() As Object = {"CO_EMPR", strCodigoEmpresa, _
                                      "CO_APRO", strCodAprobacion, _
                                      "NU_DOCU", strNumeroDocumento, _
                                      "FE_DOCU", strFecha, _
                                      "OB_0001", strObservacion, _
                                      "ST_SOLI", strEstadoSoli, _
                                      "FE_STAT_SOLI", strFechaSolicitud, _
                                      "TI_AUXI_EMPR", strTipoAuxiliar, _
                                      "CO_AUXI_EMPR", strCodigoAuxiliar, _
                                      "CO_USUA_CREA", strUsuario, _
                                      "FE_USUA_CREA", strFechaCreacion, _
                                      "CO_USUA_MODI", strUsuarioModi, _
                                      "FE_USUA_MODI", strFechaModi}
            Try
                lobjCon = New NM.AccesoDatos.AccesoDatosSQLServer(NM.AccesoDatos.GeneradorCadenaConexion.enmBasesDatos.SeguridadOfisis)
                pdtCorreos = lobjCon.ObtenerDataTable("usp_qry_Insertar_Soli_Aprobacion_Reqi", Params)
                Me.Ok = True
            Catch ex As Exception
                Me.Ok = False
            Finally
                lobjCon = Nothing
            End Try
            Return Me.Ok
    End Function

    Public Function Generar_AprobacionCTC(ByVal pstrNuSoliSecu As Integer, ByVal strCodigoEmpresa As String, ByVal strCodAprobacion As String, ByVal strNumeroDocumento As String, ByVal strFecha As String, ByVal strObservacion As String, ByVal strEstadoSoli As String, ByVal strFechaSolicitud As String, ByVal strTipoAuxiliar As String, ByVal strCodigoAuxiliar As String, ByVal strUsuario As String, ByVal strFechaCreacion As String, ByVal strUsuarioModi As String, ByVal strFechaModi As String, ByVal pstrAccion As String, ByVal pstrCodInc As String, Optional ByRef pdstResultados As DataSet = Nothing) As Boolean
      Dim lobjCon As NM.AccesoDatos.AccesoDatosSQLServer
      Dim Params() As Object = { _
      "NU_SOLI_SECU", pstrNuSoliSecu, _
      "CO_EMPR", strCodigoEmpresa, _
      "CO_APRO", strCodAprobacion, _
      "NU_DOCU", strNumeroDocumento, _
      "FE_DOCU", strFecha, _
      "OB_0001", strObservacion, _
      "ST_SOLI", strEstadoSoli, _
      "FE_STAT_SOLI", strFechaSolicitud, _
      "TI_AUXI_EMPR", strTipoAuxiliar, _
      "CO_AUXI_EMPR", strCodigoAuxiliar, _
      "CO_USUA_CREA", strUsuario, _
      "FE_USUA_CREA", strFechaCreacion, _
      "CO_USUA_MODI", strUsuarioModi, _
      "FE_USUA_MODI", strFechaModi, _
      "CO_ACCION", pstrAccion, _
      "INT_CODIGOINC", pstrCodInc}

      Try

        lobjCon = New NM.AccesoDatos.AccesoDatosSQLServer(NM.AccesoDatos.GeneradorCadenaConexion.enmBasesDatos.SeguridadOfisis)
        pdstResultados = lobjCon.ObtenerDataSet("usp_seg_ctc_soliaprobacion2", Params)
        Me.Ok = True
      Catch ex As Exception
        Me.Ok = False
      Finally
        lobjCon = Nothing
      End Try
      Return Me.Ok
    End Function
    Public Function Generar_AprobacionPreCostos(ByVal pstrNumeroRequisicion As String, ByVal pstrNuSoliSecu As Integer, ByVal strCodigoEmpresa As String, ByVal strCodAprobacion As String, ByVal strNumeroDocumento As String, ByVal strFecha As String, ByVal strObservacion As String, ByVal strEstadoSoli As String, ByVal strFechaSolicitud As String, ByVal strTipoAuxiliar As String, ByVal strCodigoAuxiliar As String, ByVal strUsuario As String, ByVal strFechaCreacion As String, ByVal strUsuarioModi As String, ByVal strFechaModi As String, ByVal pstrAccion As String, ByVal pstrCodInc As String, Optional ByRef pdstResultados As DataSet = Nothing) As Boolean
            Dim lobjCon As NM.AccesoDatos.AccesoDatosSQLServer
            Dim Params() As Object = { _
            "var_NumeroRequisicion", pstrNumeroRequisicion, _
            "NU_SOLI_SECU", pstrNuSoliSecu, _
            "CO_EMPR", strCodigoEmpresa, _
            "CO_APRO", strCodAprobacion, _
            "NU_DOCU", strNumeroDocumento, _
            "FE_DOCU", strFecha, _
            "OB_0001", strObservacion, _
            "ST_SOLI", strEstadoSoli, _
            "FE_STAT_SOLI", strFechaSolicitud, _
            "TI_AUXI_EMPR", strTipoAuxiliar, _
            "CO_AUXI_EMPR", strCodigoAuxiliar, _
            "CO_USUA_CREA", strUsuario, _
            "FE_USUA_CREA", strFechaCreacion, _
            "CO_USUA_MODI", strUsuarioModi, _
            "FE_USUA_MODI", strFechaModi, _
            "CO_ACCION", pstrAccion, _
            "INT_CODIGOINC", pstrCodInc}

            Try

                lobjCon = New NM.AccesoDatos.AccesoDatosSQLServer(NM.AccesoDatos.GeneradorCadenaConexion.enmBasesDatos.SeguridadOfisis)
                pdstResultados = lobjCon.ObtenerDataSet("USP_SEG_SOLICITAAPROBACIONPRECOSTOS", Params)
                Me.Ok = True
            Catch ex As Exception
                Me.Ok = False
            Finally
                lobjCon = Nothing
            End Try
            Return Me.Ok
            End Function
        Public Function Aprobar_RequisicionPreCostos(ByVal pstrCodigoUsuario As String, ByVal pstrNumeroDocumento As String, Optional ByRef pdstResultados As DataSet = Nothing) As Boolean
            Dim lobjCon As NM.AccesoDatos.AccesoDatosSQLServer
            Dim Params() As Object = { _
            "var_CodigoUsuario", pstrCodigoUsuario, _
            "var_NumeroDocumento", pstrNumeroDocumento}
            Try
                lobjCon = New NM.AccesoDatos.AccesoDatosSQLServer(NM.AccesoDatos.GeneradorCadenaConexion.enmBasesDatos.SeguridadOfisis)
                lobjCon.EjecutarComando("USP_SEG_APROBACIONPRECOSTOS", Params)
                'pdstResultados = lobjCon.ObtenerDataSet("USP_SEG_APROBACIONPRECOSTOS", Params)
                Me.Ok = True
            Catch ex As Exception
                Me.Ok = False
            Finally
                lobjCon = Nothing
            End Try
            Return Me.Ok
        End Function
        Public Function Generar_AprobacionMuestras(ByVal strCodigoEmpresa As String, ByVal strCodAprobacion As String, _
                                            ByVal strNumeroDocumento As String, ByVal strFecha As String, _
                                            ByVal strObservacion As String, ByVal strEstadoSoli As String, _
                                            ByVal strFechaSolicitud As String, ByVal strTipoAuxiliar As String, _
                                            ByVal strCodigoAuxiliar As String, ByVal strUsuario As String, _
                                            ByVal strFechaCreacion As String, ByVal strUsuarioModi As String, _
                                            ByVal strFechaModi As String, Optional ByRef pdtCorreos As DataTable = Nothing) As Boolean
            Dim lobjCon As NM.AccesoDatos.AccesoDatosSQLServer
            Dim Params() As Object = {"CO_EMPR", strCodigoEmpresa, _
                                      "CO_APRO", strCodAprobacion, _
                                      "NU_DOCU", strNumeroDocumento, _
                                      "FE_DOCU", strFecha, _
                                      "OB_0001", strObservacion, _
                                      "ST_SOLI", strEstadoSoli, _
                                      "FE_STAT_SOLI", strFechaSolicitud, _
                                      "TI_AUXI_EMPR", strTipoAuxiliar, _
                                      "CO_AUXI_EMPR", strCodigoAuxiliar, _
                                      "CO_USUA_CREA", strUsuario, _
                                      "FE_USUA_CREA", strFechaCreacion, _
                                      "CO_USUA_MODI", strUsuarioModi, _
                                      "FE_USUA_MODI", strFechaModi}
            Try
                lobjCon = New NM.AccesoDatos.AccesoDatosSQLServer(NM.AccesoDatos.GeneradorCadenaConexion.enmBasesDatos.SeguridadOfisis)
                pdtCorreos = lobjCon.ObtenerDataTable("USP_SEG_APROBACIONMUESTRAS", Params)
                Me.Ok = True
            Catch ex As Exception
                Me.Ok = False
            Finally
                lobjCon = Nothing
            End Try
            Return Me.Ok
        End Function
        Public Function Aprobar_SolicitudMuestras(ByVal pstrCodigoUsuario As String, ByVal pstrNumeroDocumento As String, Optional ByRef pdstResultados As DataSet = Nothing) As Boolean
            Dim lobjCon As NM.AccesoDatos.AccesoDatosSQLServer
            Dim Params() As Object = { _
            "pvar_Usuario", pstrCodigoUsuario, _
            "pVar_NumeroSolicitud", pstrNumeroDocumento}
            Try
                lobjCon = New NM.AccesoDatos.AccesoDatosSQLServer(NM.AccesoDatos.GeneradorCadenaConexion.enmBasesDatos.SeguridadOfisis)
                pdstResultados = lobjCon.ObtenerDataSet("USP_SEG_SOLICITUDMUESTRAS_APROBAR", Params)
                Me.Ok = True
            Catch ex As Exception
                Me.Ok = False
            Finally
                lobjCon = Nothing
            End Try
            Return Me.Ok
        End Function
#End Region

#Region "   Clase Estados"
        Public Class cEstados
            Inherits Clases.General
            Implements Interfases.INM

            Sub New(ByVal pstrEmpresa As String, ByVal pstrUsuario As String)
                Me.EmpresaCodigo = pstrEmpresa
                Me.UsuarioBD = pstrUsuario

                Me.SP_LISTAR = "usp_qry_AprobacionesEstados"
            End Sub


            Public Function Actualizar() As Boolean Implements NuevoMundo.Generales.Interfases.INM.Actualizar

            End Function
            Public Function Consultar() As Boolean Implements NuevoMundo.Generales.Interfases.INM.Consultar

            End Function
            Public Function Eliminar() As Boolean Implements NuevoMundo.Generales.Interfases.INM.Eliminar

            End Function
            Public Function Insertar() As Boolean Implements NuevoMundo.Generales.Interfases.INM.Insertar

            End Function
            Public Function Listar(ByVal ParamArray pParametros() As String) As Boolean Implements NuevoMundo.Generales.Interfases.INM.Listar
                Dim lobjCon As NM.AccesoDatos.AccesoDatosSQLServer

                Try
                    Dim larrParametros() = {"var_Tipo", pParametros(0)}
                    lobjCon = New NM.AccesoDatos.AccesoDatosSQLServer(NM.AccesoDatos.GeneradorCadenaConexion.enmBasesDatos.Intranet)
                    Me.SetDatos = lobjCon.ObtenerDataSet(Me.SP_LISTAR, larrParametros)
                    Me.Ok = True
                Catch ex As Exception
                    Me.Ok = False
                Finally
                    lobjCon = Nothing
                End Try
                Return Me.Ok
            End Function
        End Class

#End Region

    End Class
End Namespace
