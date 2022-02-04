Imports NuevoMundo.Generales
Imports NM.AccesoDatos
Imports Scripting
Namespace Soporte
#Region "Controlador de servidores"
    Public Class Servidor

        Private mstrPartes() As String = {"El documento ", _
                                                    ", ", _
                                                    " que pertenece a ", _
                                                    " se imprimió en ", _
                                                    " por el puerto ", _
                                                    "  Tamaño en bytes: ", _
                                                    "; páginas impresas: "}
        Private mstrPartesEnglish() As String = {"Document ", _
                                                    ", ", _
                                                    " owned by ", _
                                                    " was printed on ", _
                                                    " via port ", _
                                                    "  Size in bytes: ", _
                                                    "; pages printed: "}

#Region "    Constantes"
        Private mstrNombre As String
        Private mstrIP As String
#End Region

#Region "    Propiedades"
        Public Property Nombre() As String
            Get
                Nombre = mstrNombre
            End Get
            Set(ByVal Value As String)
                mstrNombre = Value
            End Set
        End Property
        Public Property IP() As String
            Get
                IP = mstrIP
            End Get
            Set(ByVal Value As String)
                mstrIP = Value
            End Set
        End Property
#End Region

#Region "    Constructor"
        Sub New(ByVal pstrNombre As String)
            mstrNombre = pstrNombre
        End Sub
#End Region

#Region "    Metodos"
        Public Function LeerRegistro(ByRef pdtLista As DataTable, ByVal pstrPath As String) As Boolean
            Dim lstrPath As String
            Dim lfsoManejadorArchivos As FileSystemObject
            Dim ltsArchivo As TextStream
            Dim lstrLinea As String
            Dim lstrPartes() As String
            Dim lbooErrorFecha As Boolean

            Dim ldtmFecha As DateTime
            Dim lstrTipo1 As String
            Dim lstrTipo2 As String
            Dim lstrNone As String
            Dim lintNumero As Integer
            Dim lstrMaquina As String
            Dim lstrServidor As String
            Dim lstrDescripcion As String

            Dim i As Integer
            Dim ldrFila As DataRow

            Dim lbooOk As Boolean

            pdtLista = CrearRecordsetImpresiones()
            lstrPath = pstrPath
            lfsoManejadorArchivos = New FileSystemObject
            If lfsoManejadorArchivos.FileExists(lstrPath) Then
                Try
                    ltsArchivo = lfsoManejadorArchivos.OpenTextFile(lstrPath, IOMode.ForReading, False)
                    Do While Not ltsArchivo.AtEndOfStream
                        lstrLinea = ltsArchivo.ReadLine
                        lstrPartes = Split(lstrLinea, ",")
                        If UBound(lstrPartes, 1) > 7 Then
                            lbooErrorFecha = False
                            Try
                                ldtmFecha = CDate(lstrPartes(0) + " " + lstrPartes(1))
                            Catch ex As Exception
                                lbooErrorFecha = True
                            End Try
                            If lstrPartes(0) = "24/11/2005" And lstrPartes(1) = "08:11:44 p.m." Then
                                i = 0
                            End If
                            If lbooErrorFecha = False Then
                                lstrTipo1 = lstrPartes(2) 'Tipo
                                lstrTipo2 = lstrPartes(3) 'Tipo2
                                lstrNone = lstrPartes(4) 'None
                                lintNumero = CInt(lstrPartes(5))
                                lstrMaquina = lstrPartes(6) 'Maquina
                                lstrServidor = lstrPartes(7) 'Servidor
                                If lintNumero = 10 Then
                                    For i = 8 To UBound(lstrPartes, 1)
                                        lstrPartes(i - 8) = lstrPartes(i)
                                    Next
                                    ReDim Preserve lstrPartes(UBound(lstrPartes, 1) - 8)
                                    lstrDescripcion = Join(lstrPartes, ",")
                                    lstrDescripcion = Replace(lstrDescripcion.Trim, """", "")
                                    lstrPartes = DividirTexto(lstrDescripcion)
                                    If lstrPartes(0) = Nothing Then
                                        lstrPartes = DividirTextoEnglish(lstrDescripcion)
                                    End If
                                    If lstrPartes(0) <> Nothing Then
                                        If IsNumeric(lstrPartes(5)) Then
                                            If CInt(lstrPartes(5)) > 0 Then
                                                ldrFila = pdtLista.NewRow

                                                ldrFila.Item("Servidor") = lstrServidor
                                                ldrFila.Item("Source") = lstrTipo1
                                                ldrFila.Item("Id") = CStr(lintNumero)
                                                ldrFila.Item("Tipo") = lstrTipo2

                                                ldrFila.Item("Documento") = lstrPartes(0)
                                                ldrFila.Item("Usuario") = lstrPartes(1)
                                                ldrFila.Item("Impresora") = lstrPartes(2)
                                                ldrFila.Item("Puerto") = lstrPartes(3)
                                                ldrFila.Item("Tamanio") = lstrPartes(4)
                                                ldrFila.Item("Paginas") = lstrPartes(5)

                                                ldrFila.Item("Mensaje") = lstrDescripcion
                                                ldrFila.Item("Fecha") = ldtmFecha
                                                pdtLista.Rows.Add(ldrFila)
                                            End If
                                        End If
                                    End If
                                End If
                            End If
                        End If
                    Loop
                    lbooOk = True
                Catch ex As Exception
                    lbooOk = False
                Finally
                    ltsArchivo.Close()
                End Try
            End If
            lfsoManejadorArchivos = Nothing
            Return lbooOk
        End Function

        Private Function CrearRecordsetImpresiones() As DataTable
            Dim ldtRes As DataTable
            ldtRes = New DataTable
            ldtRes.Columns.Add("Servidor")
            ldtRes.Columns.Add("Source")
            ldtRes.Columns.Add("Fecha")
            ldtRes.Columns.Add("Id")
            ldtRes.Columns.Add("Tipo")
            ldtRes.Columns.Add("Documento")
            ldtRes.Columns.Add("Usuario")
            ldtRes.Columns.Add("Impresora")
            ldtRes.Columns.Add("Puerto")
            ldtRes.Columns.Add("Tamanio")
            ldtRes.Columns.Add("Paginas")
            ldtRes.Columns.Add("Mensaje")
            Return ldtRes
        End Function
        Public Function LeerRegistro(ByRef pdtLista As DataTable) As Boolean
            Dim lobjEvent As EventLog
            Dim lobjRegistro As EventLogEntry
            Dim i As Integer
            Dim ldrFila As DataRow
            Dim lstrTexto As String
            Dim lstrPartes() As String

            pdtLista = CrearRecordsetImpresiones()

            lobjEvent = New EventLog("System", "[SERVBD03\NMUNDO03]")
            For i = 0 To lobjEvent.Entries.Count - 1
                lobjRegistro = lobjEvent.Entries(i)
                If lobjRegistro.EventID.ToString = 10 Then
                    ldrFila = pdtLista.NewRow
                    ldrFila.Item("Servidor") = lobjRegistro.MachineName.ToString
                    ldrFila.Item("Source") = lobjRegistro.Source.ToString
                    ldrFila.Item("Id") = lobjRegistro.EventID.ToString
                    ldrFila.Item("Tipo") = lobjRegistro.EntryType.ToString
                    lstrTexto = lobjRegistro.Message.ToString
                    lstrPartes = DividirTexto(lstrTexto)

                    ldrFila.Item("Documento") = lstrPartes(0)
                    ldrFila.Item("Usuario") = lstrPartes(1)
                    ldrFila.Item("Impresora") = lstrPartes(2)
                    ldrFila.Item("Puerto") = lstrPartes(3)
                    ldrFila.Item("Tamanio") = lstrPartes(4)
                    ldrFila.Item("Paginas") = lstrPartes(5)

                    ldrFila.Item("Mensaje") = lobjRegistro.Message.ToString
                    ldrFila.Item("Fecha") = lobjRegistro.TimeGenerated()
                    pdtLista.Rows.Add(ldrFila)
                End If
            Next i
            ldrFila = Nothing
            lobjRegistro = Nothing
            lobjEvent = Nothing
            Return True
        End Function
        Public Function GrabarRegistroImpresiones(ByRef pdtLista As DataTable) As Boolean
            Dim lobjBD As NM.AccesoDatos.AccesoDatosSQLServer
            Dim i As Integer
            Dim larrParams(17) As Object

            larrParams(0) = "P_SERVIDOR"
            larrParams(2) = "P_IMPRESORA"
            larrParams(4) = "P_FECHA"
            larrParams(6) = "P_DOCUMENTO"
            larrParams(8) = "P_USUARIO"
            larrParams(10) = "P_PUERTO"
            larrParams(12) = "P_TAMANIO"
            larrParams(14) = "P_PAGINAS"
            larrParams(16) = "P_USUARIOBD"
            Try
                lobjBD = New NM.AccesoDatos.AccesoDatosSQLServer(NM.AccesoDatos.GeneradorCadenaConexion.enmBasesDatos.Intranet)
                For i = 0 To pdtLista.Rows.Count - 1
                    If CInt(pdtLista.Rows(i).Item("Paginas")) > 0 Then
                        larrParams(1) = pdtLista.Rows(i).Item("Servidor")
                        larrParams(3) = pdtLista.Rows(i).Item("Impresora")
                        larrParams(5) = CDate(pdtLista.Rows(i).Item("Fecha"))
                        larrParams(7) = pdtLista.Rows(i).Item("Documento")
                        larrParams(9) = pdtLista.Rows(i).Item("Usuario")
                        larrParams(11) = pdtLista.Rows(i).Item("Puerto")
                        larrParams(13) = CInt(pdtLista.Rows(i).Item("Tamanio"))
                        larrParams(15) = CInt(pdtLista.Rows(i).Item("Paginas"))
                        larrParams(17) = "VBISHARA"
                        lobjBD.EjecutarComando("usp_Impresiones_IU", larrParams)
                    End If
                Next i

            Catch ex As Exception

            Finally
                lobjBD = Nothing
            End Try
        End Function
        Private Function DividirTexto(ByVal pstrTexto As String) As String()
            Dim lstrPartes(5) As String
            Dim lstrTexto As String
            Dim lstrTemp() As String
            Dim i As Integer
            Dim lstrTem As String

            lstrTexto = pstrTexto
            If Left(lstrTexto, Len(mstrPartes(0))) = mstrPartes(0) Then
                lstrTemp = Split(lstrTexto, mstrPartes(0))
                lstrTexto = lstrTemp(1)
                lstrTemp = Split(lstrTexto, mstrPartes(1))
                If UBound(lstrTemp, 1) > 1 Then
                    For i = 0 To UBound(lstrTemp, 1) - 1
                        lstrTemp(i) = lstrTemp(i + 1)
                    Next i
                    ReDim Preserve lstrTemp(UBound(lstrTemp) - 1)
                    lstrTexto = Join(lstrTemp, mstrPartes(1))
                Else
                    lstrTexto = lstrTemp(1)
                End If

                lstrTemp = Split(lstrTexto, mstrPartes(2))
                lstrPartes(0) = lstrTemp(0)
                lstrTexto = lstrTemp(1)
                lstrTemp = Split(lstrTexto, mstrPartes(3))
                lstrPartes(1) = lstrTemp(0)
                lstrTexto = lstrTemp(1)
                lstrTemp = Split(lstrTexto, mstrPartes(4))
                lstrPartes(2) = lstrTemp(0)
                lstrTexto = lstrTemp(1)
                lstrTemp = Split(lstrTexto, mstrPartes(5))
                lstrPartes(3) = lstrTemp(0)
                lstrTexto = lstrTemp(1)
                lstrTemp = Split(lstrTexto, mstrPartes(6))
                lstrPartes(4) = lstrTemp(0)
                lstrTexto = lstrTemp(1)
                lstrPartes(5) = lstrTexto
            End If
            Return lstrPartes
        End Function
        Private Function DividirTextoEnglish(ByVal pstrTexto As String) As String()
            Dim lstrPartes(5) As String
            Dim lstrTexto As String
            Dim lstrTemp() As String
            Dim i As Integer
            Dim lstrTem As String

            lstrTexto = pstrTexto
            If Left(lstrTexto, Len(mstrPartesEnglish(0))) = mstrPartesEnglish(0) Then
                lstrTemp = Split(lstrTexto, mstrPartesEnglish(0))
                If UBound(lstrTemp, 1) > 1 Then
                    For i = 1 To UBound(lstrTemp, 1)
                        lstrTemp(i - 1) = lstrTemp(i)
                    Next
                    ReDim Preserve lstrTemp(UBound(lstrTemp, 1) - 1)
                    lstrTexto = Join(lstrTemp, mstrPartesEnglish(0))
                Else
                    lstrTexto = lstrTemp(1)
                End If

                lstrTemp = Split(lstrTexto, mstrPartesEnglish(1))
                If UBound(lstrTemp, 1) > 1 Then
                    For i = 0 To UBound(lstrTemp, 1) - 1
                        lstrTemp(i) = lstrTemp(i + 1)
                    Next i
                    ReDim Preserve lstrTemp(UBound(lstrTemp, 1) - 1)
                    lstrTexto = Join(lstrTemp, mstrPartesEnglish(1))
                Else
                    lstrTexto = lstrTemp(1)
                End If

                lstrTemp = Split(lstrTexto, mstrPartesEnglish(2))
                lstrPartes(0) = lstrTemp(0)
                lstrTexto = lstrTemp(1)
                lstrTemp = Split(lstrTexto, mstrPartesEnglish(3))
                lstrPartes(1) = lstrTemp(0)
                lstrTexto = lstrTemp(1)
                lstrTemp = Split(lstrTexto, mstrPartesEnglish(4))
                lstrPartes(2) = lstrTemp(0)
                lstrTexto = lstrTemp(1)
                lstrTemp = Split(lstrTexto, mstrPartesEnglish(5))
                lstrPartes(3) = lstrTemp(0)
                lstrTexto = lstrTemp(1)
                lstrTemp = Split(lstrTexto, mstrPartesEnglish(6))
                lstrPartes(4) = lstrTemp(0)
                lstrTexto = lstrTemp(1)
                lstrPartes(5) = lstrTexto
            End If
            Return lstrPartes
        End Function
#End Region

    End Class
#End Region
#Region "Controlador de equipos"
    Public Class Equipos
        Inherits Clases.General
        Implements Interfases.IOFISIS


#Region "    Variables"
        Private mstrNombre As String
        Private mstrDescripcion As String
        Private mstrIP As String
        Private mobjSO As SistemasOperativos
        Private mobjCC As OFISIS.OFISEGU.Auxiliares
        Private mstrCodigoInterno As String
        Private mbooActivo As Boolean
#End Region

#Region "    Propiedades"
        Public Property Nombre() As String
            Get
                Nombre = mstrNombre
            End Get
            Set(ByVal Value As String)
                mstrNombre = Value
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
        Public Property IP() As String
            Get
                IP = mstrIP
            End Get
            Set(ByVal Value As String)
                mstrIP = Value
            End Set
        End Property
        Public Property CodigoInterno() As String
            Get
                CodigoInterno = mstrCodigoInterno
            End Get
            Set(ByVal Value As String)
                mstrCodigoInterno = Value
            End Set
        End Property
        Public ReadOnly Property SistemaOperativo() As SistemasOperativos
            Get
                SistemaOperativo = mobjSO
            End Get
        End Property
        Public ReadOnly Property CentroCosto() As OFISIS.OFISEGU.Auxiliares
            Get
                CentroCosto = mobjCC
            End Get
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
            mobjCC = New OFISIS.OFISEGU.Auxiliares(pstrEmpresa, pstrUsuario)
            mobjSO = New SistemasOperativos(pstrEmpresa, pstrUsuario)
            Me.SP_BUSCAR = "usp_qry_EquiposBuscar"
            Me.SP_LISTAR = "usp_qry_MaquinasListar"
        End Sub
#End Region

        Public Function Buscar() As Boolean Implements Generales.Interfases.IOFISIS.Buscar
            Dim lobjBD As NM.AccesoDatos.AccesoDatosSQLServer
            Dim larrParams() As String = {"P_EMPRESA", Me.EmpresaCodigo, "P_EQUIPO", mstrNombre}
            Dim ldtRes As DataTable

            Try
                lobjBD = New NM.AccesoDatos.AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.Intranet)
                ldtRes = lobjBD.ObtenerDataTable(SP_BUSCAR, larrParams)
                If ldtRes.Rows.Count = 1 Then
                    mstrDescripcion = ldtRes.Rows(0).Item("var_Descripcion")
                    mstrIP = ldtRes.Rows(0).Item("var_IP")
                    mobjSO.Codigo = ldtRes.Rows(0).Item("var_CodigoSO")
                    mobjSO.Nombre = ldtRes.Rows(0).Item("var_NombreSO")
                    mobjCC.Codigo = ldtRes.Rows(0).Item("var_CodigoCentroCosto")
                    mobjCC.Nombre = ldtRes.Rows(0).Item("var_NombreCentroCosto")
                    mstrCodigoInterno = ldtRes.Rows(0).Item("var_CodigoInterno")
                    mbooActivo = ldtRes.Rows(0).Item("bit_Estado")
                    Me.Ok = True
                Else
                    Me.Ok = False
                End If
            Catch ex As Exception
                Me.Ok = False
            Finally
                ldtRes = Nothing
                lobjBD = Nothing
            End Try
            Return Me.Ok
        End Function
        Public Sub Dispose() Implements Generales.Interfases.IOFISIS.Dispose
            mobjSO.Dispose()
            mobjCC.Dispose()
            mobjSO = Nothing
            mobjCC = Nothing
        End Sub
        Public Function Listar(ByRef pLista As System.Data.DataTable, ByVal ParamArray Flags() As String) As Boolean Implements Generales.Interfases.IOFISIS.Listar
            Dim lobjIntra As NM.AccesoDatos.AccesoDatosSQLServer
            Dim larrParams() As String = {"p_var_Empresa", Me.EmpresaCodigo, _
                                "p_var_Nombre", Flags(0), _
                                "p_var_Descripcion", Flags(1)}
            Try
                lobjIntra = New NM.AccesoDatos.AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.Intranet)
                pLista = lobjIntra.ObtenerDataTable(SP_LISTAR, larrParams)
            Catch ex As Exception

            Finally
                lobjIntra.Dispose()
                lobjIntra = Nothing
            End Try
        End Function
    End Class
#End Region
#Region "Controlador de Sistemas operativos"
    Public Class SistemasOperativos
        Inherits Clases.General
        Implements Interfases.IOFISIS

#Region "    Variables"
        Private mstrCodigo As String
        Private mstrNombre As String
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
#End Region

#Region "    Constructor"
        Sub New(ByVal pstrEmpresa As String, ByVal pstrUsuario As String)
            Me.EmpresaCodigo = pstrEmpresa
            Me.UsuarioBD = pstrUsuario
        End Sub
#End Region

        Public Function Buscar() As Boolean Implements Generales.Interfases.IOFISIS.Buscar

        End Function
        Public Sub Dispose() Implements Generales.Interfases.IOFISIS.Dispose

        End Sub
        Public Function Listar(ByRef pLista As System.Data.DataTable, ByVal ParamArray Flags() As String) As Boolean Implements Generales.Interfases.IOFISIS.Listar

        End Function
    End Class
#End Region
#Region "Controlador de repuestos"
    Public Class Repuestos
        Inherits Clases.General
        Implements Interfases.IOFISIS

#Region "    Constantes"
#End Region

#Region "    Variables"
        Private mstrCodigo As String
        Private mstrNombre As String
        Private mbooActivo As Boolean
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
            Me.SP_BUSCAR = "usp_qry_RepuestoBuscar"
            Me.SP_LISTAR = "usp_qry_RepuestoListar"

        End Sub
#End Region

        Public Function Buscar() As Boolean Implements Generales.Interfases.IOFISIS.Buscar
            Dim lobjBD As NM.AccesoDatos.AccesoDatosSQLServer
            Dim larrParams() As String = {"P_EMPRESA", Me.EmpresaCodigo, "P_REPUESTO", mstrCodigo}
            Dim ldtRes As DataTable

            Try
                lobjBD = New NM.AccesoDatos.AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.Intranet)
                ldtRes = lobjBD.ObtenerDataTable(SP_BUSCAR, larrParams)
                If ldtRes.Rows.Count = 1 Then
                    mstrNombre = ldtRes.Rows(0).Item("var_Nombre")
                    mbooActivo = ldtRes.Rows(0).Item("bit_Estado")
                    Me.Ok = True
                Else
                    Me.Ok = False
                End If
            Catch ex As Exception

                Me.Ok = False
            Finally
                ldtRes = Nothing
                lobjBD = Nothing
            End Try
            Return Me.Ok
        End Function

        Public Sub Dispose() Implements Generales.Interfases.IOFISIS.Dispose

        End Sub

        Public Function Listar(ByRef pLista As System.Data.DataTable, ByVal ParamArray Flags() As String) As Boolean Implements Generales.Interfases.IOFISIS.Listar
            Dim lobjIntra As NM.AccesoDatos.AccesoDatosSQLServer
            Dim larrParams() As String = {"p_chr_Codigo", Flags(0), _
                                "p_var_Nombre", Flags(1)}
            Try
                lobjIntra = New NM.AccesoDatos.AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.Intranet)
                pLista = lobjIntra.ObtenerDataTable(SP_LISTAR, larrParams)
            Catch ex As Exception

            Finally
                lobjIntra.Dispose()
                lobjIntra = Nothing
            End Try
        End Function
    End Class
#End Region
#Region "Controlador de incidentes"
    Public Class Incidentes
        Inherits Clases.General
        Implements Interfases.IMantenimiento


#Region "    Variables"
        Private mstrCodigoIncidente As String
        Private mobjTrabajador As OFISIS.OFIPLAN.Trabajador
        Private mobjEquipo As NuevoMundo.ControlIncidentes.Soporte.Equipos
        Private mobjCentroCosto As OFISIS.OFISEGU.Auxiliares
        Private mstrNivel As String
        Private mstrSituacion As String
        Private mstrClasificacion As String
        Private mstrProblema As String
        Private mstrReporteTecnico As String
        Private mdtFechas As DataTable
        Private mstrObservaciones As String
        Private mstrCalificacion As String
        Private mdtRepuestos As DataTable
        Private mbooActivo As Boolean
        Private mdtmInicio As DateTime
        Private mdtmFin As DateTime
#End Region

#Region "    Propiedades"
        Public Property Codigo() As String
            Get
                Codigo = mstrCodigoIncidente
            End Get
            Set(ByVal Value As String)
                mstrCodigoIncidente = Value
            End Set
        End Property
        Public ReadOnly Property Trabajador() As OFISIS.OFIPLAN.Trabajador
            Get
                Trabajador = mobjTrabajador
            End Get
        End Property
        Public ReadOnly Property Equipo() As NuevoMundo.ControlIncidentes.Soporte.Equipos
            Get
                Equipo = mobjEquipo
            End Get
        End Property
        Public ReadOnly Property CentroCosto() As OFISIS.OFISEGU.Auxiliares
            Get
                CentroCosto = mobjCentroCosto
            End Get
        End Property
        Public Property Nivel() As String
            Get
                Nivel = mstrNivel
            End Get
            Set(ByVal Value As String)
                mstrNivel = Value
            End Set
        End Property
        Public Property Situacion() As String
            Get
                Situacion = mstrSituacion
            End Get
            Set(ByVal Value As String)
                mstrSituacion = Value
            End Set
        End Property
        Public Property Clasificacion() As String
            Get
                Clasificacion = mstrClasificacion
            End Get
            Set(ByVal Value As String)
                mstrClasificacion = Value
            End Set
        End Property
        Public Property Problema() As String
            Get
                Problema = mstrProblema
            End Get
            Set(ByVal Value As String)
                mstrProblema = Value
            End Set
        End Property
        Public Property ReporteTecnico() As String
            Get
                ReporteTecnico = mstrReporteTecnico
            End Get
            Set(ByVal Value As String)
                mstrReporteTecnico = Value
            End Set
        End Property
        Public Property Fechas() As DataTable
            Get
                Fechas = mdtFechas
            End Get
            Set(ByVal Value As DataTable)
                mdtFechas = Value
            End Set
        End Property
        Public Property Observaciones() As String
            Get
                Observaciones = mstrObservaciones
            End Get
            Set(ByVal Value As String)
                mstrObservaciones = Value
            End Set
        End Property
        Public Property Calificacion() As String
            Get
                Calificacion = mstrCalificacion
            End Get
            Set(ByVal Value As String)
                mstrCalificacion = Value
            End Set
        End Property
        Public Property Repuestos() As DataTable
            Get
                Repuestos = mdtRepuestos
            End Get
            Set(ByVal Value As DataTable)
                mdtRepuestos = Value
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
        Public Property Inicio() As DateTime
            Get
                Inicio = mdtmInicio
            End Get
            Set(ByVal Value As DateTime)
                mdtmInicio = Value
            End Set
        End Property
        Public Property Fin() As DateTime
            Get
                Fin = mdtmFin
            End Get
            Set(ByVal Value As DateTime)
                mdtmFin = Value
            End Set
        End Property
#End Region

#Region "    Constructor"
        Sub New(ByVal pstrEmpresa As String, ByVal pstrUsuario As String)
            Me.EmpresaCodigo = pstrEmpresa
            Me.UsuarioBD = pstrUsuario
            mobjTrabajador = New OFISIS.OFIPLAN.Trabajador(Me.EmpresaCodigo, Me.UsuarioBD)
            mobjCentroCosto = New OFISIS.OFISEGU.Auxiliares(Me.EmpresaCodigo, Me.UsuarioBD)
            mobjEquipo = New NuevoMundo.ControlIncidentes.Soporte.Equipos(Me.EmpresaCodigo, Me.UsuarioBD)
            Me.SP_LISTAR = "usp_qry_IncidenteSoporteTecnicoListar"
            Me.SP_BUSCAR = "usp_qry_IncidenteSoporteTecnicoBuscar"
            Me.SP_INSERTAR = "usp_ins_IncidenteSoporteTecnico"
            Me.SP_ACTUALIZAR = "usp_upd_IncidenteSoporteTecnico"
            Me.SP_ELIMINAR = "usp_del_IncidenteSoporteTecnico"

        End Sub
#End Region

#Region "   Metodos"
        Public Function Buscar() As Boolean Implements Generales.Interfases.IMantenimiento.Buscar
            Dim lobjIntranet As NM.AccesoDatos.AccesoDatosSQLServer
            Dim ldsRes As DataSet
            Dim lstrParams() As String = {"p_chr_CodigoIncidente", mstrCodigoIncidente}
            Dim ldtIncidente As DataTable
            Dim ldtHoras As DataTable
            Dim ldtRepuestos As DataTable
            Try
                lobjIntranet = New NM.AccesoDatos.AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.Intranet)
                ldsRes = lobjIntranet.ObtenerDataSet(SP_BUSCAR, lstrParams)
                ldtIncidente = ldsRes.Tables(0)
                mdtFechas = ldsRes.Tables(1)
                mdtRepuestos = ldsRes.Tables(2)
                With ldtIncidente.Rows(0)
                    mobjTrabajador.Codigo = .Item("chr_Usuario")
                    mobjTrabajador.NombreCompleto = .Item("var_UsuarioNombre")
                    mobjEquipo.Nombre = .Item("var_Maquina")
                    mobjEquipo.Descripcion = .Item("var_NombreMaquina")
                    mobjCentroCosto.Codigo = .Item("chr_CentroCosto")
                    mobjCentroCosto.Nombre = .Item("var_CentroCosto")
                    mstrNivel = .Item("chr_Nivel")
                    mstrClasificacion = .Item("chr_Calificacion")
                    mstrSituacion = .Item("chr_Estado")
                    mstrProblema = .Item("var_Problema")
                    mstrReporteTecnico = .Item("var_Reporte")
                    mstrObservaciones = .Item("var_Comentarios")
                    mstrCalificacion = .Item("chr_CodigoClasificacion")
                    mbooActivo = IIf(.Item("bit_Estado") = 1, True, False)
                    mdtmInicio = .Item("dtm_FechaInicio")
                    mdtmFin = .Item("dtm_FechaFin")
                End With
                Me.Ok = True
            Catch ex As Exception
                Me.Ok = False
            Finally
                lobjIntranet.Dispose()
                lobjIntranet = Nothing
            End Try
            Return Me.Ok
        End Function

        Public Sub Dispose() Implements Generales.Interfases.IMantenimiento.Dispose

        End Sub

        Public Function Eliminar() As Boolean Implements Generales.Interfases.IMantenimiento.Eliminar
            Dim lobjBD As NM.AccesoDatos.AccesoDatosSQLServer
            Dim larrParametros(3) As String
            Dim ldtRes As DataTable

            larrParametros(0) = "p_var_CodigoIncidente"
            larrParametros(1) = mstrCodigoIncidente
            larrParametros(2) = "p_var_Usuario"
            larrParametros(3) = Me.UsuarioBD

            Try
                lobjBD = New NM.AccesoDatos.AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.Intranet)
                ldtRes = lobjBD.ObtenerDataTable(SP_ELIMINAR, larrParametros)
                mstrCodigoIncidente = ldtRes.Rows(0).Item("CodigoIncidente")
                Me.Ok = True
            Catch ex As Exception
                Me.Ok = False
            Finally
                ldtRes = Nothing
                lobjBD = Nothing
            End Try
            Return Me.Ok
        End Function

        Public Function Insertar() As Boolean Implements Generales.Interfases.IMantenimiento.Insertar
            Dim lobjBD As NM.AccesoDatos.AccesoDatosSQLServer
            Dim larrParametros(33) As String
            Dim ldtRes As DataTable
            Dim lobjUtil As New NuevoMundo.Generales.Objetos
            Dim lstrXMLFechas As String
            Dim lstrXMLRepuestos As String

            lobjUtil(mdtFechas).ToString(Objetos.cDataTableNM.enuFormats.XML, lstrXMLFechas)
            lobjUtil(mdtRepuestos).ToString(Objetos.cDataTableNM.enuFormats.XML, lstrXMLRepuestos)

            lobjUtil = Nothing

            larrParametros(0) = "p_var_CodigoIncidente"
            larrParametros(1) = ""
            larrParametros(2) = "p_var_CodigoTrabajador"
            larrParametros(3) = mobjTrabajador.Codigo
            larrParametros(4) = "p_var_CodigoEquipo"
            larrParametros(5) = mobjEquipo.Nombre
            larrParametros(6) = "p_var_CodigoCentroCosto"
            larrParametros(7) = mobjCentroCosto.Codigo
            larrParametros(8) = "p_var_Nivel"
            larrParametros(9) = mstrNivel
            larrParametros(10) = "p_var_Situacion"
            larrParametros(11) = mstrSituacion
            larrParametros(12) = "p_var_Clasificacion"
            larrParametros(13) = mstrClasificacion
            larrParametros(14) = "p_var_Problema"
            larrParametros(15) = mstrProblema
            larrParametros(16) = "p_var_ReporteTecnico"
            larrParametros(17) = mstrReporteTecnico
            larrParametros(18) = "p_ntx_Fechas  "
            larrParametros(19) = lstrXMLFechas
            larrParametros(20) = "p_var_Observaciones"
            larrParametros(21) = mstrObservaciones
            larrParametros(22) = "p_var_Calificacion"
            larrParametros(23) = mstrCalificacion
            larrParametros(24) = "p_ntx_Repuestos"
            larrParametros(25) = lstrXMLRepuestos
            larrParametros(26) = "p_dtm_FechaInicial"
            larrParametros(27) = Format(mdtmInicio, "MM/dd/yyyy HH:mm")
            larrParametros(28) = "p_dtm_FechaFinal"
            larrParametros(29) = Format(mdtmFin, "MM/dd/yyyy HH:mm")
            larrParametros(30) = "p_bit_Estado"
            larrParametros(31) = 1
            larrParametros(32) = "p_var_Usuario"
            larrParametros(33) = Me.UsuarioBD
            Try
                lobjBD = New NM.AccesoDatos.AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.Intranet)
                ldtRes = lobjBD.ObtenerDataTable(SP_INSERTAR, larrParametros)
                mstrCodigoIncidente = ldtRes.Rows(0).Item("CodigoIncidente")
                Me.Ok = True
            Catch ex As Exception
                Me.Ok = False
            Finally
                ldtRes = Nothing
                lobjBD = Nothing
            End Try
            Return Me.Ok
        End Function

        Public Function Listar(ByRef pLista As System.Data.DataTable, ByVal ParamArray Flags() As String) As Boolean Implements Generales.Interfases.IMantenimiento.Listar
            Dim lobjIntranet As NM.AccesoDatos.AccesoDatosSQLServer
            Dim larrParams() As String = {"p_var_UsuarioSoporte", Flags(0), _
                                                    "p_chr_CentroCosto", Flags(1), _
                                                    "p_var_NombreCentroCosto", Flags(2), _
                                                    "p_dtm_FechaIni", Flags(3), _
                                                    "p_dtm_FechaFin", Flags(4), _
                                                    "p_chr_Situacion", Flags(5)}

            Try
                lobjIntranet = New NM.AccesoDatos.AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.Intranet)
                pLista = lobjIntranet.ObtenerDataTable(SP_LISTAR, larrParams)
                Me.Ok = True
            Catch ex As Exception
                Me.Ok = False
            Finally
                lobjIntranet = Nothing
            End Try
            Return Me.Ok
        End Function

        Public Function Modificar() As Boolean Implements Generales.Interfases.IMantenimiento.Modificar
            Dim lobjBD As NM.AccesoDatos.AccesoDatosSQLServer
            Dim larrParametros(29) As String
            Dim ldtRes As DataTable
            Dim lobjUtil As New NuevoMundo.Generales.Objetos
            Dim lstrXMLFechas As String
            Dim lstrXMLRepuestos As String

            lobjUtil(mdtFechas).ToString(Objetos.cDataTableNM.enuFormats.XML, lstrXMLFechas)
            lobjUtil(mdtRepuestos).ToString(Objetos.cDataTableNM.enuFormats.XML, lstrXMLRepuestos)

            lobjUtil = Nothing

            larrParametros(0) = "p_var_CodigoIncidente"
            larrParametros(1) = mstrCodigoIncidente
            larrParametros(2) = "p_var_CodigoTrabajador"
            larrParametros(3) = mobjTrabajador.Codigo
            larrParametros(4) = "p_var_CodigoEquipo"
            larrParametros(5) = mobjEquipo.Nombre
            larrParametros(6) = "p_var_CodigoCentroCosto"
            larrParametros(7) = mobjCentroCosto.Codigo
            larrParametros(8) = "p_var_Nivel"
            larrParametros(9) = mstrNivel
            larrParametros(10) = "p_var_Situacion"
            larrParametros(11) = mstrSituacion
            larrParametros(12) = "p_var_Clasificacion"
            larrParametros(13) = mstrClasificacion
            larrParametros(14) = "p_var_Problema"
            larrParametros(15) = mstrProblema
            larrParametros(16) = "p_var_ReporteTecnico"
            larrParametros(17) = mstrReporteTecnico
            larrParametros(18) = "p_ntx_Fechas  "
            larrParametros(19) = lstrXMLFechas
            larrParametros(20) = "p_var_Observaciones"
            larrParametros(21) = mstrObservaciones
            larrParametros(22) = "p_var_Calificacion"
            larrParametros(23) = mstrCalificacion
            larrParametros(24) = "p_ntx_Repuestos"
            larrParametros(25) = lstrXMLRepuestos
            larrParametros(26) = "p_bit_Estado"
            larrParametros(27) = 1
            larrParametros(28) = "p_var_Usuario"
            larrParametros(29) = Me.UsuarioBD
            Try
                lobjBD = New NM.AccesoDatos.AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.Intranet)
                ldtRes = lobjBD.ObtenerDataTable(Me.SP_ACTUALIZAR, larrParametros)
                mstrCodigoIncidente = ldtRes.Rows(0).Item("CodigoIncidente")
                Me.Ok = True
            Catch ex As Exception
                Me.Ok = False
            Finally
                ldtRes = Nothing
                lobjBD = Nothing
            End Try
            Return Me.Ok
        End Function
#End Region

    End Class
#End Region
End Namespace
