Imports OFISIS.OFIPLAN

Public Class PLA20007
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents ddlMoneda As System.Web.UI.WebControls.DropDownList
    Protected WithEvents ddlCuenta As System.Web.UI.WebControls.DropDownList
    Protected WithEvents txtFechaPago As System.Web.UI.WebControls.TextBox
    Protected WithEvents btnGrabar As System.Web.UI.WebControls.Button
    Protected WithEvents ddlPlanilla As System.Web.UI.WebControls.DropDownList
    Protected WithEvents ddlBanco As System.Web.UI.WebControls.DropDownList
    Protected WithEvents txtCorrelativo As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtArchivo As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtTCambio As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtCodEmpresa As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtTC As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtConcepto As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtCTrabI As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtCTrabF As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtAnno As System.Web.UI.WebControls.TextBox
    Protected WithEvents ddlMes As System.Web.UI.WebControls.DropDownList
    Protected WithEvents ddlFMoneda As System.Web.UI.WebControls.DropDownList
    Protected WithEvents rblAbonos As System.Web.UI.WebControls.RadioButtonList

    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object

  Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
    '--INICIO: VERIFICAR LA SESION
    Session("@EMPRESA") = "01"
    'Session("@USUARIO") = "atorresc"

    If (Session("@USUARIO") Is Nothing) OrElse Session("@USUARIO") = "" Then
      If Not Request("Usuario") Is Nothing AndAlso Request("Usuario") <> "" Then
        Dim objRequest As New BLITZ_LOCK.clsRequest
        Session("@Usuario") = objRequest.Decripta(Request("Usuario"))
      End If
      If (Session("@USUARIO") Is Nothing) OrElse Session("@USUARIO") = "" Then
        Response.Redirect("../../intranet/finsesion.htm")
      End If
    End If
    InitializeComponent()
  End Sub

#End Region

  Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
    If Not IsPostBack Then
      Me.txtFechaPago.Text = Format(Now, "dd/MM/yyyy")
      Me.txtTC.Text = 1
      txtFechaPago.Attributes.Add("readonly", "readonly")
    End If
    'Ajax.Utility.RegisterTypeForAjax(GetType(TES22004))
    'btnGrabar.Attributes.Add("Onclick", "javascript:return fValidaPlanilla();")
  End Sub

  Private Sub ddlMoneda_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlMoneda.SelectedIndexChanged
      ddlCuenta.Items.Clear()
    CuentaBanco_Cargar(ddlCuenta, ddlBanco.SelectedValue, ddlMoneda.SelectedValue)
  End Sub
    Private Sub CuentaBanco_Cargar(ByRef ddlRefCuenta As DropDownList, ByVal strCodigoBanco As String, ByVal strCodigoMoneda As String)
        Dim objInterfase As New InterfaseBancos
        ddlRefCuenta.DataSource = objInterfase.ObtenerCuentaBanco(strCodigoBanco, strCodigoMoneda)
        ddlRefCuenta.DataTextField = "var_CodigoCuenta"
        ddlRefCuenta.DataValueField = "var_CodigoCuenta"
        ddlRefCuenta.DataBind()
    End Sub
    Private Sub RangoTrabajadores_Cargar(ByRef strPlanilla As String, ByVal IntAnno As Integer, ByVal IntMes As Integer)
        Dim tabla As New DataTable
        Dim objinterfase As New InterfaseBancos
        If objinterfase.ObtenerRangoTrabajadores(strPlanilla, IntAnno, IntMes, tabla) Then
            With tabla
                txtCTrabI.Text = .Rows(0)(1)
                txtCTrabF.Text = .Rows(0)(2)
            End With
        End If
    End Sub
    Private Sub ddlMes_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ddlMes.SelectedIndexChanged
        txtCTrabI.Text = ""
        txtCTrabF.Text = ""
        RangoTrabajadores_Cargar(ddlPlanilla.SelectedValue, txtAnno.Text, ddlMes.SelectedValue)
    End Sub

    Private Sub ddlFMoneda_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ddlFMoneda.SelectedIndexChanged
        btnGrabar.Enabled = True
    End Sub
 
    Private Sub rblAbonos_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rblAbonos.SelectedIndexChanged
        txtConcepto.Text = ""
        If rblAbonos.SelectedValue = "02" Then
            txtConcepto.Enabled = True
        Else
            txtConcepto.Enabled = False
        End If
    End Sub

    Private Sub btnGrabar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGrabar.Click
        Dim objinterfase As New InterfaseBancos
        Dim strFecha As String
        Dim strPlanilla As String
        strFecha = Right(txtFechaPago.Text, 4) + "-" + Mid(txtFechaPago.Text, 4, 2) + "-" + Left(txtFechaPago.Text, 2)
        strPlanilla = txtArchivo.Text
        objinterfase.sPath = "C:\Inetpub\wwwroot\OFIPLAN\"
        If rblAbonos.SelectedItem.Value = "01" Then
            If ddlBanco.SelectedValue <> "" Then
                'TOTAL PLANILLA
                Select Case ddlBanco.SelectedValue
                    Case "02" 'Banco Continental
                        objinterfase.GeneraFileFMBBVA(ddlPlanilla.SelectedValue, ddlBanco.SelectedValue, ddlMoneda.SelectedValue, ddlCuenta.SelectedValue, txtAnno.Text, ddlMes.SelectedValue, txtCTrabI.Text, txtCTrabF.Text, strFecha, txtArchivo.Text, txtTC.Text, txtCodEmpresa.Text, ddlMoneda.SelectedValue)
                    Case "03" 'Banco Interbank
                        objinterfase.GeneraFileBINB(ddlPlanilla.SelectedValue, ddlBanco.SelectedValue, ddlMoneda.SelectedValue, ddlCuenta.SelectedValue, txtAnno.Text, ddlMes.SelectedValue, txtCTrabI.Text, txtCTrabF.Text, strFecha, txtArchivo.Text, txtTC.Text, txtConcepto.Text)
                    Case Else
                End Select
                If objinterfase.clsError = "" Then
          ClientScript.RegisterStartupScript(Me.[GetType](), "Mensaje", "<script language=javascript>alert('El archivo texto se ha generado correctamente');</script>")
          btnGrabar.Enabled = False
        Else
          Response.Write(objinterfase.clsError)
          Response.End()
        End If
      End If
      objinterfase = Nothing
      'ARCHIVO POR CONCEPTO
    ElseIf rblAbonos.SelectedItem.Value = "02" Then
      If ddlBanco.SelectedValue <> "" Then
        Select Case ddlBanco.SelectedValue
          Case "02" 'Banco Continental
            objinterfase.GeneraFileCOBBVA(ddlPlanilla.SelectedValue, ddlBanco.SelectedValue, ddlMoneda.SelectedValue, ddlCuenta.SelectedValue, txtAnno.Text, ddlMes.SelectedValue, txtCTrabI.Text, txtCTrabF.Text, strFecha, txtArchivo.Text, txtTC.Text, txtCodEmpresa.Text, txtConcepto.Text, ddlMoneda.SelectedValue)
          Case "03" 'Banco Interbank
            objinterfase.GeneraFileBINB(ddlPlanilla.SelectedValue, ddlBanco.SelectedValue, ddlMoneda.SelectedValue, ddlCuenta.SelectedValue, txtAnno.Text, ddlMes.SelectedValue, txtCTrabI.Text, txtCTrabF.Text, strFecha, txtArchivo.Text, txtTC.Text, txtConcepto.Text)
        End Select
        If objinterfase.clsError = "" Then
          ClientScript.RegisterStartupScript(Me.[GetType](), "Mensaje", "<script language=javascript>alert('El archivo texto se ha generado correctamente');</script>")
          btnGrabar.Enabled = False
        Else
          Response.Write(objinterfase.clsError)
          Response.End()
        End If
      End If
      objinterfase = Nothing

        End If

    End Sub
End Class


