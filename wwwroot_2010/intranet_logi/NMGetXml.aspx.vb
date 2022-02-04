Public Class NMGetXml
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub

    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Put user code to initialize the page here
    End Sub

    Public Function GetXMLTipoAprobacion(ByVal pstrCodigo As String) As String
        Dim lobjCon As NM.AccesoDatos.AccesoDatosSQLServer
        Dim ldtRes As DataTable
        Dim lstrRetorno As String
        Try
            Dim lstrParams() As String = {"var_Codigo", pstrCodigo}
            lobjCon = New NM.AccesoDatos.AccesoDatosSQLServer(NM.AccesoDatos.GeneradorCadenaConexion.enmBasesDatos.SeguridadOfisis)
            ldtRes = lobjCon.ObtenerDataTable("usp_LOG_Requisiciones_TiposAprobacionListar", lstrParams)
            If ldtRes.Rows.Count = 1 Then
                lstrRetorno = "<Datos TipoAprobacionCodigo='" + ldtRes.Rows(0).Item("CO_APRO") + "' TipoAprobacionNombre='" + ldtRes.Rows(0).Item("DE_APRO") + "' />"
            Else
                lstrRetorno = "<Datos TipoAprobacionCodigo='' TipoAprobacionNombre='' />"
            End If
        Catch ex As Exception
            lstrRetorno = "<Datos TipoAprobacionCodigo='' TipoAprobacionNombre='' />"
        Finally
            ldtRes = Nothing
            lobjCon = Nothing
        End Try
        Return lstrRetorno
    End Function

    Public Function GetXMLProveedor(ByVal pstrCodigo As String) As String
        Dim lobjLogistica As OFISIS.OFILOGI.Proveedores
        Dim ldtDatos As DataTable
        Dim lstrRetorno As String = ""
        Try
            lobjLogistica = New OFISIS.OFILOGI.Proveedores(Session("@EMPRESA"), Session("@USUARIO"))
            lobjLogistica.Listar(ldtDatos, pstrCodigo, "")
            If ldtDatos.Rows.Count = 1 Then
                lstrRetorno = "<Datos ProveedorCodigo='" + ldtDatos.Rows(0)("CO_PROV") + "' ProveedorNombre='" + ldtDatos.Rows(0)("NO_CORT_PROV") + "' />"
            Else
                lstrRetorno = "<Datos ProveedorCodigo='' ProveedorNombre='' />"
            End If
        Catch ex As Exception
            lstrRetorno = "<Datos ProveedorCodigo='' ProveedorNombre='' />"
        Finally
            lobjLogistica = Nothing
        End Try
        Return lstrRetorno
    End Function

    Public Function GetXMLUsuario(ByVal pstrCodigo As String) As String
        Dim lobjLogistica As OFISIS.OFISEGU.Usuarios
        Dim ldtDatos As DataTable
        Dim lstrRetorno As String = ""
        Try
            lobjLogistica = New OFISIS.OFISEGU.Usuarios(Session("@EMPRESA"), Session("@USUARIO"))
            lobjLogistica.Listar(ldtDatos, pstrCodigo, "")
            If ldtDatos.Rows.Count = 1 Then
                lstrRetorno = "<Datos UsuarioCodigo='" + ldtDatos.Rows(0)("CO_USUA") + "' UsuarioNombreCompleto='" + ldtDatos.Rows(0)("NO_USUA") + "' />"
            Else
                lstrRetorno = "<Datos UsuarioCodigo='' UsuarioNombreCompleto='' />"
            End If
        Catch ex As Exception
            lstrRetorno = "<Datos UsuarioCodigo='' UsuarioNombreCompleto='' />"
        Finally
            lobjLogistica = Nothing
        End Try
        Return lstrRetorno
    End Function

    Public Function GetXMLCentroCosto(ByVal pstrCodigo As String) As String
        Dim lobjCon As OFISIS.OFISEGU.Auxiliares
        Dim ldtRes As DataTable
        Dim lstrRetorno As String
        Try
            Dim lstrParams() As String = {"var_Codigo", pstrCodigo}
            lobjCon = New OFISIS.OFISEGU.Auxiliares(Session("@EMPRESA"), Session("@USUARIO"))
            lobjCon.Codigo = pstrCodigo
            lobjCon.Buscar()
            If lobjCon.Buscar Then
                lstrRetorno = "<Datos CentroCostoCodigo='" + lobjCon.Codigo.Trim + "' CentroCostoNombre='" + lobjCon.Nombre.Trim + "' />"
            Else
                lstrRetorno = "<Datos CentroCostoCodigo='' CentroCostoNombre='' />"
            End If
        Catch ex As Exception
            lstrRetorno = "<Datos CentroCostoCodigo='' CentroCostoNombre='' />"
        Finally
            ldtRes = Nothing
            lobjCon = Nothing
        End Try
        Return lstrRetorno
    End Function

    Public Function GetXMLJefatura(ByVal pstrCodigo As String) As String
        Dim lobjCon As New NM_General.NM_Logistica
        Dim ldtDatos As DataTable
        Dim lstrRetorno As String
        Try
            Dim lstrParams() As String = {"var_Codigo", pstrCodigo}

            ldtDatos = lobjCon.ufn_BuscarJefaturaSolicitante(pstrCodigo, "")

            If ldtDatos.Rows.Count = 1 Then
                lstrRetorno = "<Datos JefaturaCodigo='" + ldtDatos.Rows(0)("CO_APRO").ToString.Trim + "' JefaturaNombre='" + ldtDatos.Rows(0)("DE_APRO").ToString.Trim + "' />"
            Else
                lstrRetorno = "<Datos JefaturaCodigo='' JefaturaNombre='' />"
            End If
        Catch ex As Exception
            lstrRetorno = "<Datos JefaturaCodigo='' JefaturaNombre='' />"
        Finally
            ldtDatos = Nothing
            lobjCon = Nothing
        End Try
        Return lstrRetorno
    End Function
End Class
