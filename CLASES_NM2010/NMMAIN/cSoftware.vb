Imports NuevoMundo.Generales.cBaseDatos
Imports System.Data.SqlClient
Namespace Software
    Public Class Temas

#Region "   Variables"
        Private mstrCodigo As String
        Private mstrNombre As String
        Private mintActivo As Integer
        Private mstrEstado As String
        Private mstrDescripcion As String
        Private mstrSoporte As String
        Private mstrAdmin As String
        Private mstrJefe As String
#End Region

#Region "   Propiedades"
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
        Public Property Activo() As Integer
            Get
                Activo = mintActivo
            End Get
            Set(ByVal Value As Integer)
                mintActivo = Value
            End Set
        End Property
        Public Property Estado() As String
            Get
                Estado = mstrEstado
            End Get
            Set(ByVal Value As String)
                mstrEstado = Value
            End Set
        End Property
        Public Property Descripcion() As String
            Get
                Descripcion = mstrDescripcion
            End Get
            Set(ByVal Value As String)
                mstrDescripcion = Value
            End Set
        End Property
        Public Property Soporte() As String
            Get
                Soporte = mstrSoporte
            End Get
            Set(ByVal Value As String)
                mstrSoporte = Value
            End Set
        End Property
        Public Property Admin() As String
            Get
                Admin = mstrAdmin
            End Get
            Set(ByVal Value As String)
                mstrAdmin = Value
            End Set
        End Property
        Public Property Jefe() As String
            Get
                Jefe = mstrJefe
            End Get
            Set(ByVal Value As String)
                mstrJefe = Value
            End Set
        End Property
#End Region

#Region "   Constructor"
        Sub New()
            mstrCodigo = ""
            mstrNombre = ""
            mstrDescripcion = ""
            mintActivo = 1
        End Sub
#End Region

#Region "    Metodos"
        Public Function Consultar() As Boolean
            Dim lobjCon As NuevoMundo.Generales.cBaseDatos
            Dim lstrSQL As String
            Dim ldtRes As DataTable
            Dim ldrRow As DataRow
            Try
                lstrSQL = "Select CHR_CODIGO_PROYECTO As Codigo , VAR_NOMBRE_PROYECTO As Nombre , BIT_ACTIVO As Activo From UTB_PROYECTOS Where CHR_CODIGO_PROYECTO = '" + mstrCodigo + "'"
                lobjCon = New NuevoMundo.Generales.cBaseDatos("INTRANET")
                ldtRes = lobjCon.EjecutarConsulta(lstrSQL)
                lobjCon = Nothing
                ldrRow = ldtRes.Rows(0)
                mstrCodigo = ldrRow("Codigo")
                mstrNombre = ldrRow("Nombre")
                mintActivo = ldrRow("Activo")
                ldrRow = Nothing
                ldtRes = Nothing
                Return True
            Catch ex As Exception
                mstrCodigo = ""
                mstrNombre = ""
                mintActivo = 1
                ldrRow = Nothing
                ldtRes = Nothing
                lobjCon = Nothing
                Return False
            End Try
        End Function
        Public Function ListarRecursos(ByVal pProyecto As String, ByRef pdtRes As DataTable) As Boolean
            Dim lobjCon As NuevoMundo.Generales.cBaseDatos
            Dim lstrSQL As String
            Try
                lobjCon = New NuevoMundo.Generales.cBaseDatos("INTRANET")
                lstrSQL = "Exec USP_PRO_LISTAR_RECURSOS_X_PROYECTO '" + pProyecto + "' "
                pdtRes = lobjCon.EjecutarConsulta(lstrSQL)
                lobjCon = Nothing
                Return True
            Catch ex As Exception
                pdtRes = Nothing
                lobjCon = Nothing
                Return False
            End Try
        End Function
        Public Function Listar(ByVal pintTipo As Integer, ByRef pdtRes As DataTable) As Boolean
            Dim lobjCon As NuevoMundo.Generales.cBaseDatos
            Dim lstrSQL As String
            Try
                lobjCon = New NuevoMundo.Generales.cBaseDatos("INTRANET")
                lstrSQL = "Exec USP_PRO_LISTAR_PROYECTOS "
                Select Case pintTipo
                    Case 1 'ACTIVOS
                        lstrSQL = lstrSQL + " 1 , '' , '' "
                    Case 2 'INACTIVOS
                        lstrSQL = lstrSQL + " 0 , '' , '' "
                    Case 3 'TODOS
                        lstrSQL = lstrSQL + " -1 , '' , '' "
                End Select
                pdtRes = lobjCon.EjecutarConsulta(lstrSQL)
                lobjCon = Nothing
                Return True
            Catch ex As Exception
                pdtRes = Nothing
                lobjCon = Nothing
                Return False
            End Try
        End Function
        Public Function Grabar(ByRef pastrResponsables() As String, ByRef pastrCC() As String, ByRef pastrCO() As String) As Boolean
            Dim lobjCon As NuevoMundo.Generales.cBaseDatos
            Dim lobjConexion As SqlConnection
            Dim lobjTran As SqlTransaction
            Dim lstrSQL As String
            Dim lbooOk As Boolean

            Try
                lobjCon = New NuevoMundo.Generales.cBaseDatos("INTRANET")
                lobjCon.Conexion.Conectar(lobjConexion)
                lstrSQL = "Exec USP_PRO_GRABAR 'OFILOGI' , 'OFILOGI' , 'VBISHARA' "
                lobjTran = lobjConexion.BeginTransaction()
                'lobjConexion.EnlistDistributedTransaction(lobjTran)
                lobjCon.EjecutarConsulta(lstrSQL, lobjConexion)
                lobjTran.Commit()
                lobjTran = Nothing
                lobjCon = Nothing
                Return True
            Catch ex As Exception
                lobjCon = Nothing
                Return False
            End Try
        End Function
#End Region

    End Class
    Public Class Sistemas

#Region "   Constantes"
        Private Const SP_LISTAR = "usp_qry_SistemasListar"
        Private Const SP_GRABAR = "usp_ins_Sistema"
        Private Const SP_ACTUALIZAR = "usp_upd_Sistema"
        Private Const SP_ELIMINAR = "usp_del_Sistema"
        Private Const SP_BUSCAR = "usp_qry_SistemasBuscar"
#End Region

#Region "    Variables"
        Private mstrUsuarioBD As String = ""
        Private mstrCodigoSistema As String = ""
        Private mstrNombreSistema As String = ""
        Private mstrDescripcionSistema As String = ""
        Private mbooActivo As Boolean = True
        Private mdtRecursosSistema As DataTable = New DataTable

        Private mstrError As String = ""
#End Region

#Region "    Propiedades"
        Public Property UsuarioBD() As String
            Get
                UsuarioBD = mstrUsuarioBD
            End Get
            Set(ByVal Value As String)
                mstrUsuarioBD = Value
            End Set
        End Property
        Public Property Codigo() As String
            Get
                Codigo = mstrCodigoSistema
            End Get
            Set(ByVal Value As String)
                mstrCodigoSistema = Value
            End Set
        End Property
        Public Property Nombre() As String
            Get
                Nombre = mstrNombreSistema
            End Get
            Set(ByVal Value As String)
                mstrNombreSistema = Value
            End Set
        End Property
        Public Property Descripcion() As String
            Get
                Descripcion = mstrDescripcionSistema
            End Get
            Set(ByVal Value As String)
                mstrDescripcionSistema = Value
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
        Public Property Recursos() As DataTable
            Get
                Recursos = mdtRecursosSistema
            End Get
            Set(ByVal Value As DataTable)
                mdtRecursosSistema = Value
            End Set
        End Property

        Public ReadOnly Property NMError() As String
            Get
                NMError = mstrError
            End Get
        End Property
#End Region

#Region "    Constructor/Destructor"
        Sub New()

        End Sub
        Protected Overrides Sub Finalize()
            mdtRecursosSistema = Nothing
            MyBase.Finalize()
        End Sub
#End Region

#Region "    Métodos"
        Public Function Grabar() As Boolean
            Dim larrParam() As String
            Dim lstrXML As String
            Dim lobjUtil As New NuevoMundo.Generales.Objetos
            Dim lbooOk As Boolean
            Dim lobjCon As NM.AccesoDatos.AccesoDatosSQLServer
            Dim ldtRes As DataTable

            lobjUtil(mdtRecursosSistema).ToString(NuevoMundo.Generales.Objetos.cDataTableNM.enuFormats.XML, lstrXML)
            lobjUtil = Nothing
            ReDim larrParam(7)
            larrParam(0) = "P_VAR_NOMBRE"
            larrParam(1) = mstrNombreSistema
            larrParam(2) = "P_VAR_DESCRIPCION"
            larrParam(3) = mstrDescripcionSistema
            larrParam(4) = "P_NTX_XML"
            larrParam(5) = lstrXML
            larrParam(6) = "P_VAR_USUARIO"
            larrParam(7) = mstrUsuarioBD

            Try
                lobjCon = New NM.AccesoDatos.AccesoDatosSQLServer(NM.AccesoDatos.GeneradorCadenaConexion.enmBasesDatos.Intranet)
                ldtRes = lobjCon.ObtenerDataTable(SP_GRABAR, larrParam)
                mstrCodigoSistema = ldtRes.Rows(0).Item("Codigo")
                lbooOk = True
            Catch ex As Exception
                lbooOk = False
                mstrError = ex.Message
            Finally
                ldtRes = Nothing
                lobjUtil = Nothing
                lobjCon = Nothing
            End Try
            Return lbooOk
        End Function
        Public Function Actualizar() As Boolean
            Dim larrParam() As String
            Dim lstrXML As String
            Dim lobjUtil As New NuevoMundo.Generales.Objetos
            Dim lbooOk As Boolean
            Dim lobjCon As NM.AccesoDatos.AccesoDatosSQLServer
            Dim ldtRes As DataTable

            lobjUtil(mdtRecursosSistema).ToString(NuevoMundo.Generales.Objetos.cDataTableNM.enuFormats.XML, lstrXML)
            lobjUtil = Nothing
            ReDim larrParam(11)
            larrParam(0) = "P_CHR_CODIGO"
            larrParam(1) = mstrCodigoSistema
            larrParam(2) = "P_VAR_NOMBRE"
            larrParam(3) = mstrNombreSistema
            larrParam(4) = "P_VAR_DESCRIPCION"
            larrParam(5) = mstrDescripcionSistema
            larrParam(6) = "P_INT_ACTIVO"
            larrParam(7) = IIf(mbooActivo, 1, 0)
            larrParam(8) = "P_NTX_XML"
            larrParam(9) = lstrXML
            larrParam(10) = "P_VAR_USUARIO"
            larrParam(11) = mstrUsuarioBD

            Try
                lobjCon = New NM.AccesoDatos.AccesoDatosSQLServer(NM.AccesoDatos.GeneradorCadenaConexion.enmBasesDatos.Intranet)
                ldtRes = lobjCon.ObtenerDataTable(SP_ACTUALIZAR, larrParam)
                mstrCodigoSistema = ldtRes.Rows(0).Item("Codigo")
                lbooOk = True
            Catch ex As Exception
                lbooOk = False
                mstrError = ex.Message
            Finally
                ldtRes = Nothing
                lobjUtil = Nothing
                lobjCon = Nothing
            End Try
            Return lbooOk
        End Function
        Public Function Listar(ByRef Lista As DataTable) As Boolean
            Dim lbooOk As Boolean
            Dim lobjCon As NM.AccesoDatos.AccesoDatosSQLServer
            Try
                lobjCon = New NM.AccesoDatos.AccesoDatosSQLServer(NM.AccesoDatos.GeneradorCadenaConexion.enmBasesDatos.Intranet)
                Lista = lobjCon.ObtenerDataTable(SP_LISTAR)
                lbooOk = True
            Catch ex As Exception
                lbooOk = False
            Finally
                lobjCon = Nothing
            End Try
            Return lbooOk
        End Function
        Public Function Buscar() As Boolean
            Dim lobjCon As NM.AccesoDatos.AccesoDatosSQLServer
            Dim ldtCabecera As DataTable
            Dim ldtDetalle As DataTable
            Dim ldsDataSet As DataSet
            Dim larrParams() As Object
            Dim lbooOk As Boolean

            ReDim larrParams(1)
            larrParams(0) = "P_CHR_CODIGO"
            larrParams(1) = mstrCodigoSistema
            Try
                lobjCon = New NM.AccesoDatos.AccesoDatosSQLServer(NM.AccesoDatos.GeneradorCadenaConexion.enmBasesDatos.Intranet)
                ldsDataSet = lobjCon.ObtenerDataSet(SP_BUSCAR, larrParams)
                ldtCabecera = ldsDataSet.Tables(0)
                ldtDetalle = ldsDataSet.Tables(1)
                mstrNombreSistema = ldtCabecera.Rows(0).Item("Nombre")
                mstrDescripcionSistema = ldtCabecera.Rows(0).Item("Descripcion")
                mbooActivo = ldtCabecera.Rows(0).Item("Activo")
                mdtRecursosSistema = ldtDetalle
                mdtRecursosSistema.TableName = "utbModulosDetalle"
                lbooOk = True
            Catch ex As Exception
                lbooOk = False
                mstrError = ex.Message
            Finally
                ldtCabecera = Nothing
                ldtDetalle = Nothing
                ldsDataSet = Nothing
                lobjCon = Nothing
            End Try
            Return lbooOk
        End Function
        Public Function CrearEsquemaDetalle() As DataTable
            Dim ldcColumn As DataColumn
            Dim ldtTabla As DataTable

            ldtTabla = New DataTable
            ldtTabla.TableName = "utbModulosDetalle"

            ldcColumn = New DataColumn
            ldcColumn.ColumnName = "var_Recurso"
            ldtTabla.Columns.Add(ldcColumn)
            ldcColumn = Nothing

            ldcColumn = New DataColumn
            ldcColumn.ColumnName = "var_Tipo"
            ldtTabla.Columns.Add(ldcColumn)
            ldcColumn = Nothing

            ldcColumn = New DataColumn
            ldcColumn.ColumnName = "var_TipoDescripcion"
            ldtTabla.Columns.Add(ldcColumn)
            ldcColumn = Nothing

            Return ldtTabla
        End Function
#End Region

    End Class
    Public Class Recursos
#Region "    Variables"
        Private mstrCodigo As String
        Private mstrNombres As String
        Private mstrMail As String
        Private mintActivo As Integer
        Private mstrEstado As String
#End Region

#Region "    Constantes"
        Private Const SP_LISTAR = "usp_qry_RecursosListar"
        Private Const SP_BUSCAR = "usp_qry_RecursosBuscar"
        Private Const SP_ACTUALIZAR = "usp_upd_Recursos"
#End Region

#Region "   Propiedades"
        Public Property Codigo() As String
            Get
                Codigo = mstrCodigo
            End Get
            Set(ByVal Value As String)
                mstrCodigo = Value
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
        Public Property Mail() As String
            Get
                Mail = mstrMail
            End Get
            Set(ByVal Value As String)
                mstrMail = Value
            End Set
        End Property
        Public Property Activo() As Integer
            Get
                Activo = mintActivo
            End Get
            Set(ByVal Value As Integer)
                mintActivo = Value
            End Set
        End Property
        Public Property Estado() As String
            Get
                Estado = mstrEstado
            End Get
            Set(ByVal Value As String)
                mstrEstado = Value
            End Set
        End Property
#End Region

#Region "   Constructor"
        Sub New()
            mstrCodigo = ""
            mstrNombres = ""
            mstrMail = ""
            mintActivo = 1
        End Sub
#End Region

#Region "   Metodos"
        Public Function Consultar() As Boolean
            Dim lobjCon As NM.AccesoDatos.AccesoDatosSQLServer
            Dim ldtRes As DataTable
            Dim lbooOk As Boolean
            Dim larrParams() As Object

            ReDim larrParams(1)
            larrParams(0) = "P_VAR_CODIGO"
            larrParams(1) = mstrCodigo
            Try
                lobjCon = New NM.AccesoDatos.AccesoDatosSQLServer(NM.AccesoDatos.GeneradorCadenaConexion.enmBasesDatos.Intranet)
                ldtRes = lobjCon.ObtenerDataTable(SP_BUSCAR, larrParams)
                mstrCodigo = ldtRes.Rows(0).Item("Codigo")
                mstrNombres = ldtRes.Rows(0).Item("Nombres")
                mstrMail = ldtRes.Rows(0).Item("Mail")
                mintActivo = ldtRes.Rows(0).Item("Activo")
                lbooOk = True
            Catch ex As Exception
                mstrCodigo = ""
                mstrNombres = ""
                mstrMail = ""
                mintActivo = 1
                lbooOk = False
            Finally
                ldtRes = Nothing
                lobjCon = Nothing
            End Try
            Return lbooOk
        End Function
        Public Function Listar(ByRef Lista As DataTable) As Boolean
            Dim lobjCon As NM.AccesoDatos.AccesoDatosSQLServer
            Dim lbooOk As Boolean
            Dim larrParams() As Object

            Try
                lobjCon = New NM.AccesoDatos.AccesoDatosSQLServer(NM.AccesoDatos.GeneradorCadenaConexion.enmBasesDatos.Intranet)
                Lista = lobjCon.ObtenerDataTable(SP_LISTAR)
                lbooOk = True
            Catch ex As Exception
                lbooOk = False
            Finally
                lobjCon = Nothing
            End Try
            Return lbooOk
        End Function
        Public Function Actualizar() As Boolean
            Dim lobjCon As NM.AccesoDatos.AccesoDatosSQLServer
            Dim larrParam() As Object
            Dim lbooOk As Boolean

            ReDim larrParam(3)
            larrParam(0) = "IN_CODIGO_RECURSO"
            larrParam(1) = mstrCodigo
            larrParam(2) = "IN_MAIL"
            larrParam(3) = mstrMail
            Try
                lobjCon = New NM.AccesoDatos.AccesoDatosSQLServer(NM.AccesoDatos.GeneradorCadenaConexion.enmBasesDatos.Intranet)
                lbooOk = lobjCon.EjecutarComando(SP_ACTUALIZAR, larrParam)
                lbooOk = True
            Catch ex As Exception
                lbooOk = False
            Finally
                lobjCon = Nothing
            End Try
            Return lbooOk
        End Function
        Public Function ListarRecursosDisponibles() As DataTable
            Dim lobjCon As NuevoMundo.Generales.cBaseDatos
            Dim ldtRes As DataTable
            Dim lstrSQL As String
            Try
                lstrSQL = "Exec USP_REC_LISTAR_RECURSOS 1 , 0 , '' , '' "
                lobjCon = New NuevoMundo.Generales.cBaseDatos("INTRANET")
                ldtRes = lobjCon.EjecutarConsulta(lstrSQL)
                lobjCon = Nothing
                Return ldtRes
            Catch ex As Exception
                Throw ex
            End Try
        End Function
        Public Function AgregarRecurso(ByVal pstrRecurso As String) As Boolean
            Dim lobjCon As NuevoMundo.Generales.cBaseDatos
            Dim ldtRes As DataTable
            Dim lstrSQL As String
            Dim lbooOk As Boolean
            Try
                lstrSQL = "Update UTB_RECURSOS Set BIT_SOPORTE = 1 Where CHR_COD_RECURSO = '" + pstrRecurso + "'"
                lobjCon = New NuevoMundo.Generales.cBaseDatos("INTRANET")
                ldtRes = lobjCon.EjecutarConsulta(lstrSQL)
                lobjCon = Nothing
                Return True
            Catch ex As Exception
                Return False
            End Try
        End Function
        Public Function EliminarRecurso(ByVal pstrRecurso As String) As Boolean
            Dim lobjCon As NuevoMundo.Generales.cBaseDatos
            Dim ldtRes As DataTable
            Dim lstrSQL As String
            Dim lbooOk As Boolean
            Try
                lstrSQL = "Update UTB_RECURSOS Set INT_TIPO = 0 Where CHR_COD_RECURSO = '" + pstrRecurso + "'"
                lobjCon = New NuevoMundo.Generales.cBaseDatos("INTRANET")
                ldtRes = lobjCon.EjecutarConsulta(lstrSQL)
                lobjCon = Nothing
                Return True
            Catch ex As Exception
                Return False
            End Try
        End Function
#End Region
    End Class
    Public Class Incidentes

#Region "    Constantes"
        Private Const SP_GRABAR = "usp_IncidentesSoftware_I"
        Private Const SP_LISTAR = "usp_IncidentesSoftware_L"
        Private Const SP_BUSCAR = "usp_qry_IncidenteDesarrolloBuscar"
        Private Const SP_SOLUCIONAR = "usp_prc_IncidenteSolucionar"
        Private Const SP_APROBAR = "usp_prc_IncidenteAprobar"
        Private Const SP_RECHAZAR = "usp_prc_IncidenteRechazar"
#End Region

#Region "    Variables"
        Private mstrUsuarioBD As String = ""
        Private mstrRecursoCodigo As String = ""
        Private mstrSistemaCodigo As String = ""
        Private mstrTitulo As String = ""
        Private mstrDescripcion As String = ""
        Private mstrID As String = ""
        Private mstrSolucion As String = ""
        Private mstrRechazo As String = ""

        Private mdatSolucion As Date

        Private mdtCorreos As DataTable

        Private mstrFechaRegistro As String
        Private mstrFechaSolucion As String
        Private mstrFechaAprobacion As String
        Private mstrFechaRechazo As String
        Private mintEstado As Integer

        Private mintTipo As Integer

#End Region

#Region "   Propiedades"
        Public Property UsuarioBD() As String
            Get
                UsuarioBD = mstrUsuarioBD
            End Get
            Set(ByVal Value As String)
                mstrUsuarioBD = Value
            End Set
        End Property
        Public Property RecursoCodigo() As String
            Get
                RecursoCodigo = mstrRecursoCodigo
            End Get
            Set(ByVal Value As String)
                mstrRecursoCodigo = Value
            End Set
        End Property
        Public Property SistemaCodigo() As String
            Get
                SistemaCodigo = mstrSistemaCodigo
            End Get
            Set(ByVal Value As String)
                mstrSistemaCodigo = Value
            End Set
        End Property
        Public Property Titulo() As String
            Get
                Titulo = mstrTitulo
            End Get
            Set(ByVal Value As String)
                mstrTitulo = Value
            End Set
        End Property
        Public Property Descripcion() As String
            Get
                Descripcion = mstrDescripcion
            End Get
            Set(ByVal Value As String)
                mstrDescripcion = Value
            End Set
        End Property
        Public Property Id() As String
            Get
                Id = mstrID
            End Get
            Set(ByVal Value As String)
                mstrID = Value
            End Set
        End Property
        Public Property SolucionDesc() As String
            Get
                SolucionDesc = mstrSolucion
            End Get
            Set(ByVal Value As String)
                mstrSolucion = Value
            End Set
        End Property
        Public Property RechazoDesc() As String
            Get
                RechazoDesc = mstrRechazo
            End Get
            Set(ByVal Value As String)
                mstrRechazo = Value
            End Set
        End Property

        Public ReadOnly Property Correos() As DataTable
            Get
                Correos = mdtCorreos
            End Get
        End Property

        Public ReadOnly Property FechaRegistro() As String
            Get
                FechaRegistro = mstrFechaRegistro
            End Get
        End Property
        Public ReadOnly Property FechaSolucion() As String
            Get
                FechaSolucion = mstrFechaSolucion
            End Get
        End Property
        Public ReadOnly Property FechaAprobacion() As String
            Get
                FechaAprobacion = mstrFechaAprobacion
            End Get
        End Property
        Public ReadOnly Property FechaRechazo() As String
            Get
                FechaRechazo = mstrFechaRechazo
            End Get
        End Property
        Public ReadOnly Property Estado() As Integer
            Get
                Estado = mintEstado
            End Get
        End Property
        Public ReadOnly Property Tipo() As Integer
            Get
                Tipo = mintTipo
            End Get
        End Property

        Public WriteOnly Property SolucionFecha() As Date
            Set(ByVal Value As Date)
                mdatSolucion = Value
            End Set
        End Property

#End Region

#Region "    Metodos"
        Public Function Grabar() As Boolean
            Dim lbooOk As Boolean
            Dim lobjCon As NM.AccesoDatos.AccesoDatosSQLServer
            Dim larrParams() As Object
            Dim ldsRes As DataSet
            Dim lstrPara As String = ""
            Dim lstrCC As String = ""

            ReDim larrParams(9)
            larrParams(0) = "P_VAR_CODIGORECURSO"
            larrParams(1) = mstrRecursoCodigo
            larrParams(2) = "P_CHR_CODIGOSISTEMA"
            larrParams(3) = mstrSistemaCodigo
            larrParams(4) = "P_VAR_TITULO"
            larrParams(5) = mstrTitulo
            larrParams(6) = "P_NTX_DESCRIPCION"
            larrParams(7) = mstrDescripcion
            larrParams(8) = "P_VAR_USUARIO"
            larrParams(9) = mstrUsuarioBD

            Try
                lobjCon = New NM.AccesoDatos.AccesoDatosSQLServer(NM.AccesoDatos.GeneradorCadenaConexion.enmBasesDatos.Intranet)
                ldsRes = lobjCon.ObtenerDataSet(SP_GRABAR, larrParams)
                mstrID = ldsRes.Tables(0).Rows(0).Item("var_Id")
                mstrFechaRegistro = ldsRes.Tables(0).Rows(0).Item("FechaRegistro")
                mdtCorreos = ldsRes.Tables(1)
                lbooOk = True
            Catch ex As Exception
                lbooOk = False
            Finally
                lobjCon = Nothing
            End Try
            Return lbooOk
        End Function
        Public Function Listar(ByRef Lista As DataTable, Optional ByVal Usuario As String = "") As Boolean
            Dim lobjCon As NM.AccesoDatos.AccesoDatosSQLServer
            Dim lbooOk As Boolean
            Dim larrParam() As Object

            ReDim larrParam(1)
            larrParam(0) = "P_VAR_USUARIO"
            larrParam(1) = Usuario
            Try
                lobjCon = New NM.AccesoDatos.AccesoDatosSQLServer(NM.AccesoDatos.GeneradorCadenaConexion.enmBasesDatos.Intranet)
                Lista = lobjCon.ObtenerDataTable(SP_LISTAR, larrParam)
                lbooOk = True
            Catch ex As Exception
                lbooOk = False
            Finally
                lobjCon = Nothing
            End Try
            Return lbooOk
        End Function
        Public Function Buscar() As Boolean
            Dim lobjCon As NM.AccesoDatos.AccesoDatosSQLServer
            Dim lbooOk As Boolean
            Dim larrParam() As Object
            Dim ldtRes As DataTable

            ReDim larrParam(7)
            larrParam(0) = "P_VAR_USUARIO"
            larrParam(1) = mstrRecursoCodigo
            larrParam(2) = "P_CHR_SISTEMA"
            larrParam(3) = mstrSistemaCodigo
            larrParam(4) = "P_CHR_ID"
            larrParam(5) = mstrID
            larrParam(6) = "P_VAR_USUARIO_BD"
            larrParam(7) = mstrUsuarioBD
            Try
                lobjCon = New NM.AccesoDatos.AccesoDatosSQLServer(NM.AccesoDatos.GeneradorCadenaConexion.enmBasesDatos.Intranet)
                ldtRes = lobjCon.ObtenerDataTable(SP_BUSCAR, larrParam)
                mstrTitulo = ldtRes.Rows(0).Item("var_Titulo")
                mstrDescripcion = ldtRes.Rows(0).Item("ntx_Descripcion")
                mstrFechaRegistro = ldtRes.Rows(0).Item("FechaRegistro")
                mstrFechaSolucion = ldtRes.Rows(0).Item("FechaSolucion")
                mstrFechaAprobacion = ldtRes.Rows(0).Item("FechaAprobacion")
                mstrFechaRechazo = ldtRes.Rows(0).Item("FechaRechazo")
                mstrRechazo = ldtRes.Rows(0).Item("ntx_Rechazo")
                mstrSolucion = ldtRes.Rows(0).Item("ntx_Solucion")
                mintEstado = ldtRes.Rows(0).Item("int_Estado")
                mintTipo = ldtRes.Rows(0).Item("Tipo")
                lbooOk = True
            Catch ex As Exception
                lbooOk = False
            Finally
                ldtRes = Nothing
                lobjCon = Nothing
            End Try
            Return lbooOk
        End Function
        Public Function Solucionar() As Boolean
            Dim lobjCon As NM.AccesoDatos.AccesoDatosSQLServer
            Dim larrParams() As Object
            Dim ldsRes As DataSet
            Dim lbooOk As Boolean

            ReDim larrParams(9)
            larrParams(0) = "P_VAR_CODIGORECURSO"
            larrParams(1) = mstrRecursoCodigo
            larrParams(2) = "P_CHR_CODIGOSISTEMA"
            larrParams(3) = mstrSistemaCodigo
            larrParams(4) = "P_CHR_ID"
            larrParams(5) = mstrID
            larrParams(6) = "P_NTX_DESCRIPCION"
            larrParams(7) = mstrSolucion
            larrParams(8) = "P_VAR_USUARIO"
            larrParams(9) = mstrUsuarioBD
            Try
                lobjCon = New NM.AccesoDatos.AccesoDatosSQLServer(NM.AccesoDatos.GeneradorCadenaConexion.enmBasesDatos.Intranet)
                ldsRes = lobjCon.ObtenerDataSet(SP_SOLUCIONAR, larrParams)
                mstrID = ldsRes.Tables(0).Rows(0).Item("var_Id")
                mstrFechaRegistro = ldsRes.Tables(0).Rows(0).Item("FechaRegistro")
                mdtCorreos = ldsRes.Tables(1)
                lbooOk = True
            Catch ex As Exception
                lbooOk = False
            Finally
                lobjCon = Nothing
            End Try
            Return lbooOk
        End Function
        Public Function Rechazar() As Boolean
            Dim lobjCon As NM.AccesoDatos.AccesoDatosSQLServer
            Dim larrParams() As Object
            Dim ldsRes As DataSet
            Dim lbooOk As Boolean

            ReDim larrParams(9)
            larrParams(0) = "P_VAR_CODIGORECURSO"
            larrParams(1) = mstrRecursoCodigo
            larrParams(2) = "P_CHR_CODIGOSISTEMA"
            larrParams(3) = mstrSistemaCodigo
            larrParams(4) = "P_CHR_ID"
            larrParams(5) = mstrID
            larrParams(6) = "P_NTX_DESCRIPCION"
            larrParams(7) = mstrRechazo
            larrParams(8) = "P_VAR_USUARIO"
            larrParams(9) = mstrUsuarioBD
            Try
                lobjCon = New NM.AccesoDatos.AccesoDatosSQLServer(NM.AccesoDatos.GeneradorCadenaConexion.enmBasesDatos.Intranet)
                ldsRes = lobjCon.ObtenerDataSet(SP_RECHAZAR, larrParams)
                mstrID = ldsRes.Tables(0).Rows(0).Item("var_Id")
                mstrFechaRegistro = ldsRes.Tables(0).Rows(0).Item("FechaRegistro")
                mdtCorreos = ldsRes.Tables(1)
                lbooOk = True
            Catch ex As Exception
                lbooOk = False
            Finally
                lobjCon = Nothing
            End Try
            Return lbooOk
        End Function
        Public Function Aprobar() As Boolean
            Dim lobjCon As NM.AccesoDatos.AccesoDatosSQLServer
            Dim larrParams() As Object
            Dim ldsRes As DataSet
            Dim lbooOk As Boolean

            ReDim larrParams(7)
            larrParams(0) = "P_VAR_CODIGORECURSO"
            larrParams(1) = mstrRecursoCodigo
            larrParams(2) = "P_CHR_CODIGOSISTEMA"
            larrParams(3) = mstrSistemaCodigo
            larrParams(4) = "P_CHR_ID"
            larrParams(5) = mstrID
            larrParams(6) = "P_VAR_USUARIO"
            larrParams(7) = mstrUsuarioBD
            Try
                lobjCon = New NM.AccesoDatos.AccesoDatosSQLServer(NM.AccesoDatos.GeneradorCadenaConexion.enmBasesDatos.Intranet)
                ldsRes = lobjCon.ObtenerDataSet(SP_APROBAR, larrParams)
                mstrID = ldsRes.Tables(0).Rows(0).Item("var_Id")
                mstrFechaRegistro = ldsRes.Tables(0).Rows(0).Item("FechaRegistro")
                mdtCorreos = ldsRes.Tables(1)
                lbooOk = True
            Catch ex As Exception
                lbooOk = False
            Finally
                lobjCon = Nothing
            End Try
            Return lbooOk
        End Function
#End Region

    End Class
End Namespace
