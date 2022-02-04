Imports NM.AccesoDatos
Imports NuevoMundo
Imports System.Data
Imports System.Data.SqlClient

Public Class frm_ConsultaEstructuraNM
    Inherits System.Web.UI.Page

    Private Sub frm_ConsultaEstructuraNM_Init(sender As Object, e As System.EventArgs) Handles Me.Init
        Session("@EMPRESA") = "01"
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Titulo()
    End Sub

    Private Sub Consulta()
        Dim clsEstructura As New clsEstructuraNM
        Dim dtbDatos As DataTable

        Dim strNivel As String = ""
        Dim strEmpresa As String = ""
        Dim strCodCenCosGer As String = ""
        Dim strDesCenCosGer As String = ""
        Dim strCodCenCosJef As String = ""
        Dim strDesCenCosJef As String = ""
        Dim strCodCenCosSup As String = ""
        Dim strDesCenCosSup As String = ""

        dtbDatos = Nothing

        Try
            strNivel = Request("pNivel")
            strEmpresa = Session("@EMPRESA")

            If strNivel = "1" Then
                strCodCenCosGer = Trim(txtCodigo.Text)
                strDesCenCosGer = Trim(txtDescripcion.Text)
            End If
            If strNivel = "2" Then
                strCodCenCosGer = Request("pCodCenCosGer")
                strCodCenCosJef = Trim(txtCodigo.Text)
                strDesCenCosJef = Trim(txtDescripcion.Text)
            End If
            If strNivel = "3" Then
                strCodCenCosGer = Request("pCodCenCosGer")
                strCodCenCosJef = Request("pCodCenCosJef")
                strCodCenCosSup = Trim(txtCodigo.Text)
                strDesCenCosSup = Trim(txtDescripcion.Text)
            End If

            dtbDatos = clsEstructura.fncListarEstructura(strNivel, strEmpresa, _
                                                         strCodCenCosGer, strDesCenCosGer, _
                                                         strCodCenCosJef, strDesCenCosJef, _
                                                         strCodCenCosSup, strDesCenCosSup, _
                                                         dtbDatos)
            If Not dtbDatos Is Nothing Then
                If dtbDatos.Rows.Count > 0 Then
                    dgDatos.DataSource = dtbDatos
                    dgDatos.DataBind()
                Else
                    dgDatos.DataSource = Nothing
                    dgDatos.DataBind()
                    lblError.Text = "No existen datos para la consulta. "
                End If
            End If
        Catch ex As Exception
            lblError.Text = "Error en la consulta. " + ex.Message
        End Try

    End Sub

    Private Sub Titulo()
        Select Case Request("pNivel")
            Case "1"
                lblTtulo.Text = "Consultar gerencias"
            Case "2"
                lblTtulo.Text = "Consultar jefaturas"
            Case "3"
                lblTtulo.Text = "Consultar supervisores"
        End Select
        txtCodigo.Focus()

    End Sub

    Protected Sub btnConsultar_Click(sender As Object, e As EventArgs) Handles btnConsultar.Click
        Call Consulta()
    End Sub

    Private Sub dgDatos_ItemDataBound(sender As Object, e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dgDatos.ItemDataBound
        If e.Item.ItemType = ListItemType.AlternatingItem Or e.Item.ItemType = ListItemType.Item Then
            Dim btnEscoger As HtmlControls.HtmlInputButton = CType(e.Item.FindControl("btnEscoger"), HtmlControls.HtmlInputButton)
            Dim drvDatos As DataRowView = CType(e.Item.DataItem, DataRowView)
            Dim strParametros = "'" & drvDatos("vch_Codigo") & "', '" & drvDatos("vch_Descripcion") & "', '" & drvDatos("vch_CodigoEmpleado") & "'"
            btnEscoger.Attributes.Add("onClick", "btnEscoger_Onclick(" + strParametros + ")")
        End If
    End Sub
End Class