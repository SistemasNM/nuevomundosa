Imports BLITZ_LOCK
Imports NM_General.NM_BaseDatos
Imports NM.AccesoDatos

Namespace NM_Tejeduria
	Public Class NM_Usuario
		Public codigo_usuario As String
		Public Clave As String
		Public Login As String
		Public Identidad As String
		Public codigo_perfil As String
		Public Codigo_Area As String
    Public Nivel As Integer
    Private _strUsuario As String
    Private _dtmFechaExpiracion As DateTime
    Private _strEstado As String
		Public Debug As String
    Public dtPermisos As New DataTable
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
          Dim objUtil As New NM_Util.NM_Trabajador
          objUtil.Seek(_CodigoOperario)
          Return objUtil.NombreCompleto
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
    Public Property CodigoModulo() As Integer
      Get
        Return _codigomodulo
      End Get
      Set(ByVal Value As Integer)
        _codigomodulo = Value
      End Set
    End Property

#End Region

    Sub New()
      codigo_usuario = ""
      Clave = ""
      Login = ""
      Identidad = ""
      codigo_perfil = ""
      Codigo_Area = ""
      Nivel = 0
      _CodigoOperario = ""
      _codigomodulo = -1
    End Sub

    Sub New(ByVal sUsuario As String, ByVal sClave As String)
      Dim objDet As New NM_PermisoPerfil
      Seek(sUsuario, sClave)
      dtPermisos = objDet.List(codigo_perfil)
    End Sub

    Sub New(ByVal sCodigo As String)
      Dim objDet As New NM_PermisoPerfil
      Seek(sCodigo)
      dtPermisos = objDet.List(codigo_perfil)
    End Sub

    Public Sub Seek(ByVal sCodigo As String)
      Dim objParametros() = {"CODIGO", sCodigo}
      Dim dtUsuario As New DataTable
      Dim fila As DataRow
      connProd = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.Produccion)
      dtUsuario = connProd.ObtenerDataTable("SP_NM_GET_USUARIO_V2", objParametros)
      For Each fila In dtUsuario.Rows
        codigo_usuario = fila("codigo_usuario")
        Clave = fila("passwd_usuario")
        Login = fila("login_usuario")
        Identidad = fila("identidad_usuario")
        codigo_perfil = fila("codigo_perfil")
        Codigo_Area = fila("codigo_area")
        Nivel = fila("nivel")
        _strEstado = fila("estado")
        If IsDBNull(fila("fecha_expiracion")) = False Then _dtmFechaExpiracion = fila("fecha_expiracion")
        If IsDBNull(fila("codigo_operario")) = False Then _CodigoOperario = fila("codigo_operario")
      Next
      connProd = Nothing
    End Sub


    Public Sub Seek(ByVal sUsuario As String, ByVal sClave As String)
      Dim objParametros() = {"usuario", sUsuario, "clave", sClave}
      Dim dtUsuario As New DataTable
      Dim fila As DataRow
      connProd = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.Produccion)
      dtUsuario = connProd.ObtenerDataTable("SP_NM_LOGIN_USUARIO_V2", objParametros)
      For Each fila In dtUsuario.Rows
        codigo_usuario = fila("codigo_usuario")
        Clave = fila("passwd_usuario")
        Login = fila("login_usuario")
        Identidad = fila("identidad_usuario")
        codigo_perfil = fila("codigo_perfil")
        Codigo_Area = fila("codigo_area")
        Nivel = fila("nivel")
        _strEstado = fila("estado")
        If IsDBNull(fila("fecha_expiracion")) = False Then _dtmFechaExpiracion = fila("fecha_expiracion")
        If IsDBNull(fila("codigo_operario")) = False Then _CodigoOperario = fila("codigo_operario")
      Next
      connProd = Nothing
    End Sub

    Public Function Exist(ByVal sUsuario As String, ByVal sClave As String) As Boolean
      Try
        connProd = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.Produccion)
        Dim objParametros() = {"usuario", sUsuario, "clave", sClave}
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

    Public Function List() As DataTable
      Dim sql As String, objConn As New NM_Consulta, dtUsuario As New DataTable
      sql = "Select * from NM_Usuario "
      dtUsuario = objConn.Query(sql)
      Return dtUsuario
    End Function

    Public Function List(ByVal bParagrid As Boolean) As DataTable
      Dim sql As String, objConn As New NM_Consulta, dtUsuario As New DataTable
      If bParagrid = True Then
        sql = "Select U.codigo_usuario, U.identidad_usuario, " & _
        " U.login_usuario, U.passwd_usuario, P.descripcion_perfil, " & _
        " P.codigo_perfil, U.codigo_area, A.nombre_area, U.Nivel, " & _
        " U.codigo_operario, dbo.FN_NM_GetNombreOperario(U.codigo_operario) as nombre_operario " & _
        " from NM_Usuario U, NM_PerfilUsuario P, NM_Area A " & _
        " where P.codigo_perfil = U.codigo_perfil " & _
        " and U.codigo_area*=A.codigo_area "
        dtUsuario = objConn.Query(sql)
      End If
      Return dtUsuario
    End Function

  End Class

End Namespace