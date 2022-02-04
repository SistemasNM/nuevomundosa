Imports NM_General
Imports BLITZ_LOCK
Public Class frm_RegistrarUsuarioPortalProveedor
    Inherits System.Web.UI.Page

    Private Sub frm_RegistrarUsuarioPortalProveedor_Init(sender As Object, e As System.EventArgs) Handles Me.Init
        '--INICIO: VERIFICAR LA SESION
        Session("@EMPRESA") = "01"
        'Session("@USUARIO") = "JCUCHO"

        If (Session("@USUARIO") Is Nothing) OrElse Session("@USUARIO") = "" Then
            If Not Request("Usuario") Is Nothing AndAlso Request("Usuario") <> "" Then
                Dim objRequest As New BLITZ_LOCK.clsRequest
                Session("@Usuario") = objRequest.Decripta(Request("Usuario"))
            End If
            If (Session("@USUARIO") Is Nothing) OrElse Session("@USUARIO") = "" Then
                Response.Redirect("../../intranet/finsesion.htm")
            End If
        End If
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If (Not Page.IsPostBack) Then
            sub_initJavascript()
            sub_LimpiarControles()
            hdnExiste.Value = "N"
            hdnCodigoEmpresa.Value = "01"
        End If
    End Sub
    Sub sub_initJavascript()
        txtNombreProveedor.Attributes.Add("readonly", "readonly")
        txtCorreoElectronicoProveedor.Attributes.Add("readonly", "readonly")
        txtUsuario.Attributes.Add("readonly", "readonly")
        txtPassword.Attributes.Add("readonly", "readonly")
        txtCodigoProveedor.Attributes.Add("onkeypress", "javascript:return fnc_txtCodigoProveedor_onkeyPress();")
        txtNombreProveedor.Attributes.Add("onkeydown", "javascript:return (event.keyCode!=13);")
        txtCorreoElectronicoProveedor.Attributes.Add("onkeydown", "javascript:return (event.keyCode!=13);")
        txtUsuario.Attributes.Add("onkeydown", "javascript:return (event.keyCode!=13);")
        txtPassword.Attributes.Add("onkeydown", "javascript:return (event.keyCode!=13);")
    End Sub
    Sub sub_LimpiarControles()
        txtNombreProveedor.Text = ""
        txtCorreoElectronicoProveedor.Text = ""
        txtUsuario.Text = ""
        txtPassword.Text = ""
        txtCodigoProveedor.Text = ""
        txtNombreProveedor.Text = ""
        txtCorreoElectronicoProveedor.Text = ""
        txtUsuario.Text = ""
        txtPassword.Text = ""
        Session("CO_USUA") = ""
        Session("NO_CLAV") = ""
        Session("NO_CLAV_ENCR") = ""
    End Sub
    Protected Sub btnRegistrarUsuario_Click(sender As Object, e As EventArgs) Handles btnRegistrarUsuario.Click
        Try
            If (Not (txtCodigoProveedor.Text.Trim = "" OrElse
                txtNombreProveedor.Text.Trim = "" OrElse
                txtCorreoElectronicoProveedor.Text.Trim = "" OrElse
                txtUsuario.Text = "" OrElse
                txtPassword.Text = "" OrElse
                Session("CO_USUA") = "" OrElse
                Session("NO_CLAV") = "" OrElse
                Session("NO_CLAV_ENCR") = "")
                ) Then
                Dim objLogistica As New NM_Logistica
                Dim intResult = 0
                Dim strMensaje As String = ""
                Dim dtmfechaModificacionClave As DateTime = DateTime.Now
                Dim strCodigoGrupo As String = "ENLACE"
                Dim strDireccionEmail As String = ""
                Dim strEstadoUsuario As String = "S"
                If (hdnExiste.Value = "N") Then
                    intResult = objLogistica.ufn_CrearUsuarioProveedorPortal(Session("CO_USUA"), txtNombreProveedor.Text.Trim,
                                                               Session("NO_CLAV_ENCR"), dtmfechaModificacionClave,
                                                               strCodigoGrupo, strDireccionEmail,
                                                               strEstadoUsuario)
                    If (intResult > 0) Then
                        intResult = 0
                        intResult = objLogistica.ufn_VincularUsuarioProveedorPortal(txtCodigoProveedor.Text, Session("CO_USUA"),
                                                                                    Session("@USUARIO"), dtmfechaModificacionClave)
                        If (intResult > 0) Then
                            objLogistica.ufn_EnviarCorreoCredenciales(txtCorreoElectronicoProveedor.Text.Trim,
                                                          txtNombreProveedor.Text.Trim,
                                                          txtCodigoProveedor.Text.Trim,
                                                          txtUsuario.Text,
                                                          Session("NO_CLAV"))
                            strMensaje = "Se generarón y se enviarón las credenciales correctamente"
                        Else
                            strMensaje = "Error, No se vinculo usuario con proveedor"
                        End If
                    Else
                        strMensaje = "Error, No se creo el usuario"
                    End If
                Else
                    objLogistica.ufn_EnviarCorreoCredenciales(txtCorreoElectronicoProveedor.Text.Trim,
                                                  txtNombreProveedor.Text.Trim,
                                                  txtCodigoProveedor.Text.Trim,
                                                  txtUsuario.Text,
                                                  Session("NO_CLAV"))
                    intResult = 1
                    strMensaje = "Se enviaron las credenciales correctamente"
                End If

                If (intResult > 0) Then
                    lblMensaje.ForeColor = Drawing.Color.Green
                    lblMensaje.Text = strMensaje
                    sub_LimpiarControles()
                Else
                    lblMensaje.ForeColor = Drawing.Color.Red
                    lblMensaje.Text = strMensaje
                End If

            Else
                lblMensaje.ForeColor = Drawing.Color.Red
                lblMensaje.Text = "Error, Algun dato del proveedor se encuentra vacio"
            End If
        Catch ex As Exception
            lblMensaje.ForeColor = Drawing.Color.Red
            lblMensaje.Text = ex.Message
        End Try
    End Sub

    Protected Sub btnBuscarProveedor_Click(sender As Object, e As EventArgs) Handles btnBuscarProveedor.Click
        If (txtCodigoProveedor.Text.Trim.Length > 0) Then
            Dim objLogistica As New NM_Logistica
            Dim objBlitzLock As New BlitzLock
            Dim strCodigoEmpresa As String = hdnCodigoEmpresa.Value
            Dim strCodigoProveedor As String = txtCodigoProveedor.Text.Trim
            Dim dtDatosProveedor As New DataTable

            Try
                lblMensaje.Text = ""
                dtDatosProveedor = objLogistica.ufn_ObtenerDatosProveedorGeneracionCredenciales(strCodigoEmpresa, strCodigoProveedor)
                If (dtDatosProveedor.Rows.Count = 1) Then
                    For Each dr As DataRow In dtDatosProveedor.Rows
                        txtCodigoProveedor.Text = dr("CO_PROV")
                        txtNombreProveedor.Text = dr("NO_CORT_PROV")
                        txtCorreoElectronicoProveedor.Text = dr("DE_MAIL")
                        hdnSituacionProveedor.Value = dr("ST_PROV")
                        hdnEstadoUsuario.Value = dr("ST_USUA")
                        hdnExiste.Value = dr("ST_EXST")
                        Session("CO_USUA") = dr("CO_USUA")
                        Session("NO_CLAV") = dr("NO_CLAV")
                        txtUsuario.Text = dr("CO_USUA")
                        If (dr("NO_CLAV").ToString.Length > 0) Then
                            Session("NO_CLAV_ENCR") = objBlitzLock.Encripta(dr("NO_CLAV"))
                            txtPassword.Text = "*******"
                        End If
                    Next

                    If (hdnSituacionProveedor.Value <> "ACT") Then
                        btnRegistrarUsuario.Visible = False
                        lblMensaje.Text = "Error, Proveedor no se encuentra activo"
                        lblMensaje.ForeColor = Drawing.Color.Red
                    End If

                    If (hdnEstadoUsuario.Value <> "S") Then
                        btnRegistrarUsuario.Visible = False
                        lblMensaje.Text = "Error, Usuario no se encuentra activo"
                        lblMensaje.ForeColor = Drawing.Color.Red
                    End If

                    If (txtNombreProveedor.Text.Trim = "" OrElse
                       txtCorreoElectronicoProveedor.Text.Trim = "" OrElse
                       txtPassword.Text = "" OrElse
                       txtUsuario.Text = "") Then
                        btnRegistrarUsuario.Visible = False
                        lblMensaje.Text = "Error, Algun dato del proveedor se encuentra vacio"
                    End If

                    If (hdnExiste.Value = "S") Then
                        btnRegistrarUsuario.Text = "Enviar Correo"
                    End If
                Else
                    lblMensaje.Text = "Ocurrio un error, la busqueda devolvio mas de un valor"
                    lblMensaje.ForeColor = Drawing.Color.Red
                End If
            Catch ex As Exception
                lblMensaje.Text = ex.Message
                lblMensaje.ForeColor = Drawing.Color.Red
            End Try
        Else
            lblMensaje.Text = "Ingrese codigo proveedor"
            lblMensaje.ForeColor = Drawing.Color.Red
        End If
    End Sub
End Class