Imports OFISIS.OFIPLAN
Imports NuevoMundo

Public Class frm_InterfaceBancos
    Inherits System.Web.UI.Page

        Dim str_CodigoEmpresa As String = ""
        Dim str_CodigoPlanilla As String = ""
        Dim str_CodigoBanco As String = ""
        Dim str_NumeroCuenta As String = ""
        Dim num_Anno As String = ""
        Dim num_Mes As String = ""
        Dim str_CodTrabInicial As String = ""
        Dim str_CodTrabFinal As String = ""
        Dim str_FechaAbono As String = ""
        Dim str_Situacion As String = ""
        Dim str_ConceptoPago As String = ""
        Dim str_Moneda As String = ""
        Dim num_Tipocambio As Double = 0
        Dim strNombreFile As String = ""
        Dim strCorrelativo As String = ""
        Dim strGrupo As String = ""

    ' Init
    Private Sub frm_InterfaceBancos_Init(sender As Object, e As System.EventArgs) Handles Me.Init
        ' Session("@EMPRESA") = "01"
        ' Session("@USUARIO") = "benito"

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

    ' Load
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            prcListarEmpresa()
            txtFechaAbono.Text = Format(Now, "dd/MM/yyyy")
            txtTC.Text = "1.00"
            txtAnno.Text = Year(Now).ToString
            txtArchivo.Text = ""
            txtConcepto.ReadOnly = True
            txtTC.ReadOnly = True
            btnReporteDetalle.Enabled = False
        End If
    End Sub

    ' Obtenemos cuenta de banco
    Private Sub ddlMoneda_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlMoneda.SelectedIndexChanged
        Dim objInterfase As New InterfaseBancos
        Dim strCodigoBanco As String = ""
        Dim strCodigoMoneda As String = ""
        Dim dtbCuenta As New DataTable

        lblmensaje.Text = ""
        dtbCuenta = Nothing
        Try
            strCodigoBanco = ddlBanco.SelectedValue.ToString
            strCodigoMoneda = ddlMoneda.SelectedValue.ToString
            ddlCuenta.Items.Clear()
            dtbCuenta = objInterfase.ObtenerCuentaBanco(strCodigoBanco, strCodigoMoneda)
            If dtbCuenta Is Nothing Or dtbCuenta.Rows.Count = 0 Then
                ddlCuenta.DataSource = Nothing
                lblmensaje.Text = "No existe cuenta asociada."
            Else
                ddlCuenta.DataSource = dtbCuenta
                ddlCuenta.DataTextField = "var_CodigoCuenta"
                ddlCuenta.DataValueField = "var_CodigoCuenta"
                ddlCuenta.DataBind()
            End If
        Catch ex As Exception
           lblmensaje.Text = ex.Message.ToString
        End Try
    End Sub

    ' Obtenemos correlativo de empleados
    Private Sub ddlMes_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ddlMes.SelectedIndexChanged
        Dim objInterfase As New InterfaseBancos
        Dim dtbRango As New DataTable
        Dim strPlanilla As String = ""
        Dim strAnno As String = ""
        Dim strMes As String = ""

        lblmensaje.Text = ""
        txtCTrabI.Text = ""
        txtCTrabF.Text = ""

        dtbRango = Nothing
        strPlanilla = ddlPlanilla.SelectedValue.ToString
        strAnno = Trim(txtAnno.Text)
        strMes = ddlMes.SelectedValue.ToString
        Try
            If objInterfase.ObtenerRangoTrabajadores(strPlanilla, strAnno, strMes, dtbRango) = True Then
                If dtbRango.Rows.Count = 0 Or dtbRango Is Nothing Then
                    lblmensaje.Text = "Verificar planilla aun no procesada."
                Else
                    With dtbRango
                        txtCTrabI.Text = .Rows(0)(1)
                        txtCTrabF.Text = .Rows(0)(2)
                    End With
                End If
            End If
        Catch ex As Exception
            lblmensaje.Text = ex.Message.ToString
        End Try
    End Sub

    ' Opcion concepto
    Private Sub rblAbonos_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles rblAbonos.SelectedIndexChanged
        str_ConceptoPago = rblAbonos.SelectedValue.ToString
        str_Moneda = ddlMoneda.SelectedValue.ToString
        txtConcepto.Text = ""
        txtTC.Text = "1.00"
        txtConcepto.ReadOnly = True
        txtTC.ReadOnly = True

        Select Case str_ConceptoPago
            Case "02" ' Por concepto
                txtConcepto.ReadOnly = False
                txtConcepto.Focus()
            Case "04" ' CTS
                Select Case str_Moneda
                    Case "DOL"
                        txtTC.ReadOnly = False
                        txtTC.Focus()
                End Select
        End Select

        txtArchivo.Text = GeneraNombreFile()
    End Sub

    ' Opcion banco
    Private Sub ddlBanco_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlBanco.SelectedIndexChanged
        If ddlBanco.SelectedValue = "15" Or ddlBanco.SelectedValue = "01" Then
            btnReporteDetalle.Enabled = True
        Else
            btnReporteDetalle.Enabled = False
        End If
    End Sub

    ' Boton reporte - det
    Protected Sub btnReporteDetalle_Click(sender As Object, e As EventArgs) Handles btnReporteDetalle.Click
        If fncValidaDatos() = True Then
            VerInterfasebancoDetalle()
        End If
    End Sub

    ' Boton generar file
    Protected Sub btnGeneraFile_Click(sender As Object, e As EventArgs) Handles btnGeneraFile.Click
        Dim objinterfase As New InterfaseBancos

        lblmensaje.Text = ""
        If fncValidaDatos() = True Then
            ' variables
            str_CodigoEmpresa = Trim(txtCodEmpresa.Text)
            str_CodigoPlanilla = ddlPlanilla.SelectedValue.ToString
            str_CodigoBanco = ddlBanco.SelectedValue.ToString
            str_NumeroCuenta = ddlCuenta.SelectedValue.ToString
            num_Anno = txtAnno.Text
            num_Mes = ddlMes.SelectedValue.ToString
            str_CodTrabInicial = txtCTrabI.Text
            str_CodTrabFinal = txtCTrabF.Text
            str_Situacion = "ACT"
            str_ConceptoPago = rblAbonos.SelectedValue.ToString
            strNombreFile = txtArchivo.Text

            If str_CodigoBanco = "15" Then
                str_FechaAbono = Right(txtFechaAbono.Text, 4) + Mid(txtFechaAbono.Text, 4, 2) + Left(txtFechaAbono.Text, 2)
            Else
                str_FechaAbono = Right(txtFechaAbono.Text, 4) + "-" + Mid(txtFechaAbono.Text, 4, 2) + "-" + Left(txtFechaAbono.Text, 2)
            End If
            str_Moneda = ddlMoneda.SelectedValue.ToString
            num_Tipocambio = Double.Parse(txtTC.Text)
            strCorrelativo = "1"
            strGrupo = "3000"
            objinterfase.sPath = "C:\Inetpub\wwwroot\OFIPLAN\"

            Try
                Select Case str_ConceptoPago
                    Case "00" ' Quincena
                        Select Case str_CodigoBanco
                            Case "02" 'BBVA
                                objinterfase.GeneraFileCOBBVA(str_CodigoPlanilla, str_CodigoBanco, str_Moneda, ddlCuenta.SelectedValue, num_Anno, num_Mes, _
                                    str_CodTrabInicial, str_CodTrabFinal, str_FechaAbono, strNombreFile, num_Tipocambio, str_CodigoEmpresa, "@QUINC", str_Moneda)
                            Case "03" ' Interbank
                                objinterfase.GeneraFileBINB(str_CodigoPlanilla, str_CodigoBanco, str_Moneda, ddlCuenta.SelectedValue, num_Anno, num_Mes, _
                                    str_CodTrabInicial, str_CodTrabFinal, str_FechaAbono, strNombreFile, num_Tipocambio, "@QUINC")
                            Case "15" 'ScotiaBank
                                objinterfase.GeneraFileRemSTK(str_CodigoEmpresa, str_CodigoPlanilla, str_CodigoBanco, num_Anno, num_Mes, str_CodTrabInicial, str_CodTrabFinal, _
                                str_FechaAbono, str_Situacion, str_ConceptoPago, strNombreFile)
                            Case "32" 'BANBIF
                                objinterfase.GeneraFileRemBBIF(str_CodigoEmpresa, str_CodigoPlanilla, str_CodigoBanco, num_Anno, num_Mes, str_CodTrabInicial, str_CodTrabFinal, _
                                str_FechaAbono, str_Situacion, str_ConceptoPago, strNombreFile)
                        End Select

                    Case "01" ' Mensual
                        Select Case str_CodigoBanco
                            Case "02" 'BBVA
                                objinterfase.GeneraFileFMBBVA(str_CodigoPlanilla, str_CodigoBanco, str_Moneda, ddlCuenta.SelectedValue, num_Anno, num_Mes, _
                                    str_CodTrabInicial, str_CodTrabFinal, str_FechaAbono, strNombreFile, num_Tipocambio, str_CodigoEmpresa, str_Moneda)
                            Case "03" 'Interbank
                                objinterfase.GeneraFileBINB(str_CodigoPlanilla, str_CodigoBanco, str_Moneda, ddlCuenta.SelectedValue, num_Anno, num_Mes, str_CodTrabInicial, str_CodTrabFinal, _
                                    str_FechaAbono, strNombreFile, num_Tipocambio, txtConcepto.Text)
                            Case "15" 'ScotiaBank
                                objinterfase.GeneraFileRemSTK(str_CodigoEmpresa, str_CodigoPlanilla, str_CodigoBanco, num_Anno, num_Mes, str_CodTrabInicial, str_CodTrabFinal, _
                                    str_FechaAbono, str_Situacion, str_ConceptoPago, strNombreFile)

                            Case "32" 'BANBIF
                                objinterfase.GeneraFileRemBBIF(str_CodigoEmpresa, str_CodigoPlanilla, str_CodigoBanco, num_Anno, num_Mes, str_CodTrabInicial, str_CodTrabFinal, _
                                str_FechaAbono, str_Situacion, str_ConceptoPago, strNombreFile)
                        End Select

                    Case "02" ' Por Concepto
                        Select Case str_CodigoBanco
                            Case "02" 'BBVA
                                objinterfase.GeneraFileCOBBVA(str_CodigoPlanilla, str_CodigoBanco, str_Moneda, ddlCuenta.SelectedValue, num_Anno, num_Mes, _
                                    str_CodTrabInicial, str_CodTrabFinal, str_FechaAbono, strNombreFile, num_Tipocambio, str_CodigoEmpresa, txtConcepto.Text, str_Moneda)
                            Case "03" ' Interbank
                                objinterfase.GeneraFileBINB(str_CodigoPlanilla, str_CodigoBanco, str_Moneda, ddlCuenta.SelectedValue, num_Anno, num_Mes, _
                                    str_CodTrabInicial, str_CodTrabFinal, str_FechaAbono, strNombreFile, num_Tipocambio, txtConcepto.Text)
                            Case "32" 'BANBIF
                                objinterfase.GeneraFileRemBBIF(str_CodigoEmpresa, str_CodigoPlanilla, str_CodigoBanco, num_Anno, num_Mes, str_CodTrabInicial, str_CodTrabFinal, _
                                str_FechaAbono, str_Situacion, str_ConceptoPago, strNombreFile)
                        End Select

                    Case "03" ' Mov varios

                    Case "04" ' Cts
                        Select Case str_CodigoBanco
                            Case "01" 'BCP
                                objinterfase.GeneraFileCtsBcp(str_CodigoEmpresa, str_CodigoPlanilla, str_CodigoBanco, str_NumeroCuenta, _
                                    num_Anno, num_Mes, strCorrelativo, str_Moneda, num_Tipocambio, strGrupo, "S", str_CodTrabInicial, str_CodTrabFinal, strNombreFile)
                            Case "15" 'ScotiaBank
                                objinterfase.GeneraFileCtsSTK(str_CodigoEmpresa, str_CodigoPlanilla, str_CodigoBanco, num_Anno, num_Mes, str_CodTrabInicial, str_CodTrabFinal, _
                                    str_FechaAbono, str_Situacion, str_ConceptoPago, str_Moneda, num_Tipocambio, strNombreFile)
                            Case "32" 'BANBIF
                                objinterfase.GeneraFileRemBBIF(str_CodigoEmpresa, str_CodigoPlanilla, str_CodigoBanco, num_Anno, num_Mes, str_CodTrabInicial, str_CodTrabFinal, _
                                str_FechaAbono, str_Situacion, str_ConceptoPago, strNombreFile)
                        End Select
                    Case "05" ' cta Cte
                End Select
                If objinterfase.clsError.Length = 0 Then
                    lblmensaje.Text = "Se creo con exito el archivo: " + "C:\Inetpub\wwwroot\OFIPLAN\" + "Interfase\" + strNombreFile + ".txt"
                End If
            Catch ex As Exception
                lblmensaje.Text = objinterfase.clsError
            End Try
        End If
    End Sub


#Region "Metodos"
    ' Listamos las empresas
    Private Sub prcListarEmpresa()
        Dim dtbEmpresa As DataTable
        Dim objEmpresa As New clsEmpresa

        dtbEmpresa = Nothing
        Try
            dtbEmpresa = objEmpresa.fncListarEmpresa(Session("@EMPRESA"), dtbEmpresa)
            If dtbEmpresa Is Nothing Or dtbEmpresa.Rows.Count = 0 Then
                lblmensaje.Text = "No existe cempresas."
            Else
                 With dtbEmpresa
                    txtCodEmpresa.Text = .Rows(0)("co_empr")
                    txtDesEmpresa.Text = .Rows(0)("des_empr")
                End With
            End If
        Catch ex As Exception
            lblmensaje.Text = ex.Message.ToString
        End Try
    End Sub

    ' Validar datos
    Private Function fncValidaDatos() As Boolean
        Dim lblnValidation As Boolean = True
        lblmensaje.Text = ""
        Try
            If ddlPlanilla.SelectedValue = "Seleccionar" Then
                lblmensaje.Text = "Error: Debe elegir un tipo de planilla."
                lblnValidation = False
                Return lblnValidation
                Exit Function
            End If

            If ddlBanco.SelectedValue = "Seleccionar" Then
                lblmensaje.Text = "Error: Debe elegir una entidad bancaria."
                lblnValidation = False
                Return lblnValidation
                Exit Function
            End If

             If ddlMoneda.SelectedValue = "Seleccionar" Then
                lblmensaje.Text = "Error: Debe elegir una moneda."
                lblnValidation = False
                Return lblnValidation
                Exit Function
            End If

            If txtAnno.Text.Trim.Length = 0 Then
                lblmensaje.Text = "Error: Debe ingresar el año para la consulta."
                lblnValidation = False
                Return lblnValidation
                Exit Function
            End If

            If rblAbonos.SelectedItem.Selected = False Then
                lblmensaje.Text = "Error: Debe elegir un tipo de abono."
                lblnValidation = False
                Return lblnValidation
                Exit Function
            End If

            If rblAbonos.SelectedValue.ToString = "04" And ddlMoneda.SelectedValue.ToString = "DOL" And (txtTC.Text.Length = 0 Or Double.Parse(txtTC.Text) <= 0) Then
                lblmensaje.Text = "Error: Debe ingresar un tipo de cambio valido para la consulta."
                lblnValidation = False
                Return lblnValidation
                Exit Function
            End If

            If rblAbonos.SelectedValue.ToString = "02" And txtConcepto.Text.Length = 0 Then
                lblmensaje.Text = "Error: Debe ingresar un concepto de pago valido para la consulta."
                lblnValidation = False
                Return lblnValidation
                Exit Function
            End If

             If txtArchivo.Text.Length = 0 Then
                lblmensaje.Text = "Error: Debe ingresar un nombre para el archivo."
                lblnValidation = False
                Return lblnValidation
                Exit Function
            End If
        Catch ex As Exception
          lblmensaje.Text = ex.Message
        End Try

    Return lblnValidation
    End Function

    ' Listar reporte detalle
    Private Sub VerInterfasebancoDetalle()
        Dim strURL As String
        Dim strPath As String
        Dim strScript As String

        'strPath = "%2fNM_Reportes%2f"
        'strURL = ConfigurationManager.AppSettings("ReporteServer") & strPath
        strURL = System.Web.Configuration.WebConfigurationManager.AppSettings("ReporteServerNM")
        strPath = System.Web.Configuration.WebConfigurationManager.AppSettings("ModuloRecursosHumanos")
        strURL = strURL + strPath
        If (rblAbonos.SelectedValue = "00" Or rblAbonos.SelectedValue = "01") And ddlBanco.SelectedValue = "15" Then
            'strURL = strURL + "rpt_planilla_InterfaseBancosRemSTK"
            strURL = strURL + "pla_interfasebancosremstk"
        End If

        If rblAbonos.SelectedValue = "04" And ddlBanco.SelectedValue = "15" Then
            'strURL = strURL + "rpt_planilla_InterfaseBancosCtsSTK"
            strURL = strURL + "pla_interfasebancosctsstk"
        End If

        If rblAbonos.SelectedValue = "04" And ddlBanco.SelectedValue = "01" Then
            'strURL = strURL + "rpt_planilla_InterfaseBancosCtsBcp"
            strURL = strURL + "pla_interfasebancosctsbcp"
        End If

        If ddlBanco.SelectedValue = "15" Then
            strURL = strURL + "&vch_CodigoEmpresa=" + Trim(txtCodEmpresa.Text)
            strURL = strURL + "&chr_CodigoPlanilla=" + ddlPlanilla.SelectedValue.ToString
            strURL = strURL + "&vch_CodigoBanco=" + ddlBanco.SelectedValue.ToString
            strURL = strURL + "&int_Anno=" + txtAnno.Text
            strURL = strURL + "&int_Mes=" + ddlMes.SelectedValue.ToString
            strURL = strURL + "&vch_CodTrabInicial=" + txtCTrabI.Text
            strURL = strURL + "&vch_CodTrabFinal=" + txtCTrabF.Text
            strURL = strURL + "&vch_FechaAbono=" + Right(txtFechaAbono.Text, 4) + Mid(txtFechaAbono.Text, 4, 2) + Left(txtFechaAbono.Text, 2)
            strURL = strURL + "&chr_Situacion=" + "ACT"
            strURL = strURL + "&vch_ConceptoPago=" + rblAbonos.SelectedValue.ToString

            If rblAbonos.SelectedValue = "04" Then
                strURL = strURL + "&chr_Moneda=" + ddlMoneda.SelectedValue.ToString
                strURL = strURL + "&chr_TipoCambio=" + txtTC.Text
            End If
        End If

        If rblAbonos.SelectedValue = "04" And ddlBanco.SelectedValue = "01" Then
            strURL = strURL + "&ISCO_EMPR=" + Trim(txtCodEmpresa.Text)
            strURL = strURL + "&ISCO_PLAN_INIC=" + ddlPlanilla.SelectedValue.ToString
            strURL = strURL + "&ISCO_BANC_SUEL=" + ddlBanco.SelectedValue.ToString
            strURL = strURL + "&ISCO_CNTA_BANC=" + ddlCuenta.SelectedValue.ToString
            strURL = strURL + "&INNU_ANNO=" + txtAnno.Text
            strURL = strURL + "&INNU_PERI=" + ddlMes.SelectedValue.ToString
            strURL = strURL + "&INNU_CORR_PERI=" + "1"
            strURL = strURL + "&ISCO_MONE=" + ddlMoneda.SelectedValue.ToString
            strURL = strURL + "&INTI_CAMB=" + txtTC.Text
            strURL = strURL + "&ISCO_GRUP=" + "3000"
            strURL = strURL + "&ISTI_SITU_ACTI=" + "S"
            strURL = strURL + "&ISCO_TRAB_INIC=" + txtCTrabI.Text
            strURL = strURL + "&ISCO_TRAB_FINA=" + txtCTrabF.Text
        End If


        strURL = strURL + "&rc:Command=Render"
        strURL = strURL + "&rc:Toolbar=true"
        strScript = "fMostrarReporte('" & strURL & "');"
        ScriptManager.RegisterStartupScript(Me, Page.GetType, "ShowInfo", strScript, True)

    End Sub

    ' Generar nombre archivo
    Private Function GeneraNombreFile() As String
        str_ConceptoPago = rblAbonos.SelectedValue.ToString
        str_CodigoPlanilla = ddlPlanilla.SelectedValue.ToString
        str_CodigoBanco = ddlBanco.SelectedValue.ToString
        str_Moneda = ddlMoneda.SelectedValue.ToString
        num_Anno = txtAnno.Text
        num_Mes = ddlMes.SelectedValue.ToString

        Select Case str_ConceptoPago
            Case "00"
                strNombreFile = strNombreFile + "QUI"
            Case "01"
                strNombreFile = strNombreFile + "MEN"
            Case "02"
                strNombreFile = strNombreFile + "CON"
            Case "03"
                strNombreFile = strNombreFile + "MOV"
            Case "04"
                strNombreFile = strNombreFile + "CTS"
            Case "05"
                strNombreFile = strNombreFile + "CTE"
        End Select

        strNombreFile = strNombreFile + "_" + str_CodigoPlanilla

        Select Case str_CodigoBanco
            Case "01"
                strNombreFile = strNombreFile + "_" + "BCP"
            Case "02"
                strNombreFile = strNombreFile + "_" + "BBVA"
            Case "03"
                strNombreFile = strNombreFile + "_" + "INT"
            Case "15"
                strNombreFile = strNombreFile + "_" + "STK"
        End Select

        strNombreFile = strNombreFile + "_" + str_Moneda + "_" + num_Anno + "-" + num_Mes
        Return strNombreFile
    End Function

#End Region
End Class