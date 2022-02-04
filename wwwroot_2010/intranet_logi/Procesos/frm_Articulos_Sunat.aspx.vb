Public Class frm_Articulos_Sunat
    Inherits System.Web.UI.Page
    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        '-----------------------------------------------------------------------
        '--INICIO: VERIFICAR LA SESION
        '-----------------------------------------------------------------------
        '20120904 EPM Valida que la session este vacio o nula
        'Session("@USUARIO") = "DARWIN"
        If (Session("@USUARIO") Is Nothing) OrElse Session("@USUARIO") = "" Then
            If Not Request("Usuario") Is Nothing AndAlso Request("Usuario") <> "" Then
                Dim objRequest As New BLITZ_LOCK.clsRequest
                Session("@Usuario") = objRequest.Decripta(Request("Usuario"))
            End If

            Session("@EMPRESA") = "01"
            If (Session("@USUARIO") Is Nothing) OrElse Session("@USUARIO") = "" Then
                Response.Redirect("../../intranet/finsesion.htm")
            End If
        End If
    End Sub
    Private Sub sListado_ArticulosSUNAT()
        '*****************************************************************************************************
        'Objetivo   : Muestra listado de Articulos criticos para SUNAT (EX DINANDRO)
        'Autor      : Darwin Ccorahua Livon
        'Creado     : 22/04/2013
        'Modificado : 00/00/0000
        '*****************************************************************************************************
        lblMsg.Text = ""
        Dim lobjArticulos As New NuevoMundo.clsArticulo
        Dim objDT As New DataTable
        'lobjArticulos.CodigoEmpresa = Session("@EMPRESA")
        'lobjOrdenServicio.FechaInicio = Right(Me.WebDatePicker1.Text, 4) + Mid(Me.WebDatePicker1.Text, 4, 2) + Mid(Me.WebDatePicker1.Text, 1, 2)
        'lobjOrdenServicio.FechaFin = Right(Me.WebDatePicker2.Text, 4) + Mid(Me.WebDatePicker2.Text, 4, 2) + Mid(Me.WebDatePicker2.Text, 1, 2)
        'lobjOrdenServicio.CodigoProveedor = Me.TxtCodigoProveedor.Text
        'lobjArticulos.EstadoServicio = Me.cmbOpcion.SelectedValue.ToString
        'lobjArticulos.Listar_ArticulosSUNAT(objDT, Me.cmbAnno.SelectedValue.ToString, Me.cmbMes.SelectedValue.ToString, "")
        Me.dtgLista.DataSource = objDT
        Me.dtgLista.DataBind()
        objDT = Nothing
        lobjArticulos = Nothing
    End Sub
    Protected Sub btnBuscar_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnBuscar.Click
        If fValida() = False Then Exit Sub
        sListado_ArticulosSUNAT()
    End Sub
    Private Function fValida() As Boolean
        '*****************************************************************************************************
        'Objetivo   : Valida el ingreso de datos para la tabla de evaluacion de Proveedores
        'Autor      : Darwin Ccorahua Livon
        'Creado     : 05/09/2011
        'Modificado : 00/00/0000
        '*****************************************************************************************************
        fValida = False

        If Me.cmbAnno.SelectedValue = "00" Then
            Me.cmbAnno.Focus()
            lblMsg.Text = "Por favor seleccione el periodo de búsqueda.. !"
            Exit Function
        End If
        If Me.cmbMes.SelectedValue = "00" Then
            Me.cmbMes.Focus()
            lblMsg.Text = "Por favor seleccione el mes de búsqueda.. !"
            Exit Function
        End If
        fValida = True
    End Function

End Class