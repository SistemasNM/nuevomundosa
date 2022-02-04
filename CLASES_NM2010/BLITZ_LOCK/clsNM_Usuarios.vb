Imports NM.AccesoDatos


Public Class clsNM_Usuarios

    Private _strUsuario As String
    Private _dtmFechaExpiracion As DateTime
    Private _strEstado As String    
    Private _CodigoOperario As String
    Private _codigomodulo As Integer

    Private connProd As AccesoDatosSQLServer


#Region "-- Propiedades --"
    Public Property Usuario() As String
        Get
            Return _strUsuario
        End Get
        Set(ByVal Value As String)
            _strUsuario = Value
        End Set
    End Property
    Public Property CodigoOperario() As String
        Get
            Return _CodigoOperario
        End Get
        Set(ByVal Value As String)
            _CodigoOperario = Value
        End Set
    End Property
    Public ReadOnly Property NombreOperario() As String
        Get
            If _CodigoOperario <> "" Then
                Return ObtenerDatosTrabajador(_CodigoOperario)
            Else
                Return ""
            End If
        End Get
    End Property
    Public Property FechaExpiracion() As DateTime
        Get
            Return _dtmFechaExpiracion
        End Get
        Set(ByVal Value As DateTime)
            _dtmFechaExpiracion = Value
        End Set
    End Property
    Public Property Estado() As String
        Get
            Return _strEstado
        End Get
        Set(ByVal Value As String)
            _strEstado = Value
        End Set
    End Property


#End Region

    Public Function Exist(ByVal sUsuario As String, ByVal sClave As String) As Boolean
        Try
            connProd = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.Produccion)
            Dim objParametros() As String = {"usuario", sUsuario, "clave", sClave}
            Dim dtUsuario As New DataTable
            dtUsuario = connProd.ObtenerDataTable("SP_NM_LOGIN_USUARIO_V2", objParametros)
            If Not dtUsuario Is Nothing Then
                Return (dtUsuario.Rows.Count > 0)
            Else
                Return False
            End If
        Catch Ex As Exception
            Throw Ex
        Finally
            connProd = Nothing
        End Try
    End Function

    Public Sub Seek(ByVal sUsuario As String, ByVal sClave As String)
        Dim objParametros() As String = {"usuario", sUsuario, "clave", sClave}
        Dim dtUsuario As New DataTable
        Dim fila As DataRow
        connProd = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.Produccion)
        dtUsuario = connProd.ObtenerDataTable("SP_NM_LOGIN_USUARIO_V2", objParametros)
        For Each fila In dtUsuario.Rows
            _strUsuario = fila("login_usuario")
            _strEstado = fila("estado")
            If IsDBNull(fila("fecha_expiracion")) = False Then _dtmFechaExpiracion = fila("fecha_expiracion")
            If IsDBNull(fila("codigo_operario")) = False Then _CodigoOperario = fila("codigo_operario")
        Next
        connProd = Nothing
    End Sub
    Public Function ObtenerDatosTrabajador(ByVal codigoTrabajador As String) As String
        Dim ldtbResultado As DataTable
        Dim m_sqlDtAccPlanilla As AccesoDatosSQLServer
        Dim strResult As String

        strResult = ""
        Dim lobjParametros() As Object = {"pvch_Codigo_Trabajador", codigoTrabajador}
        Try
            m_sqlDtAccPlanilla = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.PlanOfisis)

            ldtbResultado = m_sqlDtAccPlanilla.ObtenerDataTable("usp_pla_trabajador_obtener", lobjParametros)
            If ldtbResultado.Rows.Count > 0 Then
                strResult = CType(ldtbResultado.Rows(0).Item("NO_APEL_PATE"), String) + " " +
                            CType(ldtbResultado.Rows(0).Item("NO_APEL_MATE"), String) + " " +
                            CType(ldtbResultado.Rows(0).Item("NO_TRAB"), String)
            End If
        Catch ex As Exception

        Finally
            m_sqlDtAccPlanilla = Nothing
            ldtbResultado = Nothing
        End Try

        Return strResult

    End Function
    'INGRESO DE USUARIO EXCLUSIVO OCTO - DG - INI 
    Public Function ExistUsuario(ByVal sUsuario As String, ByVal sClave As String) As Boolean
        Try
            connProd = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.Produccion)
            Dim objParametros() As String = {"usuario", sUsuario, "clave", sClave}
            Dim dtUsuario As New DataTable
            dtUsuario = connProd.ObtenerDataTable("SP_NM_LOGIN_USUARIO_OCTO", objParametros)
            If Not dtUsuario Is Nothing Then
                Return (dtUsuario.Rows.Count > 0)
            Else
                Return False
            End If
        Catch Ex As Exception
            Throw Ex
        Finally
            connProd = Nothing
        End Try
    End Function
    Public Sub SeekUsuario(ByVal sUsuario As String, ByVal sClave As String)
        Dim objParametros() As String = {"usuario", sUsuario, "clave", sClave}
        Dim dtUsuario As New DataTable
        Dim fila As DataRow
        connProd = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.Produccion)
        dtUsuario = connProd.ObtenerDataTable("SP_NM_LOGIN_USUARIO_OCTO", objParametros)
        For Each fila In dtUsuario.Rows
            _strUsuario = fila("login_usuario")
            _strEstado = fila("estado")
            If IsDBNull(fila("fecha_expiracion")) = False Then _dtmFechaExpiracion = fila("fecha_expiracion")
            'If IsDBNull(fila("codigo_operario")) = False Then _CodigoOperario = fila("codigo_operario")
        Next
        connProd = Nothing
    End Sub
    'INGRESO DE USUARIO EXCLUSIVO OCTO - DG - FIN
End Class
