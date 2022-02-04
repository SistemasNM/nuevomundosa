Imports System.Data.DataTable
Imports CostosLib

Partial Public Class popUpBucarRequeTela
    Inherits System.Web.UI.Page

    Dim objclsAnalisisTela As New clsAnalisisTela

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            Try
                buscarRequerimientosAnalisisTela(Request.QueryString("Mca_estado").ToString, Request.QueryString("Mca_estado2").ToString)
            Catch ex As Exception
                buscarRequerimientosAnalisisTela(Request.QueryString("Mca_estado").ToString, "")
            End Try

        End If
    End Sub

    Sub buscarRequerimientosAnalisisTela(ByVal strEstadoBusqueda As String, ByVal strEstadoBusqueda2 As String)
        objclsAnalisisTela.Accion = "L"
        objclsAnalisisTela.IdRequer = ""
        objclsAnalisisTela.Referencia = ""
        objclsAnalisisTela.Solicitante = ""
        objclsAnalisisTela.IdEstado = ""
        objclsAnalisisTela.Observacion1 = ""
        objclsAnalisisTela.DescripcionTecnica = ""
        objclsAnalisisTela.Ligamento = ""
        objclsAnalisisTela.Peso = ""
        objclsAnalisisTela.Onza = ""
        objclsAnalisisTela.Ancho = ""
        objclsAnalisisTela.TitUrd = ""
        objclsAnalisisTela.TitTrama = ""
        objclsAnalisisTela.MatUrd = ""
        objclsAnalisisTela.MatTrama = ""
        objclsAnalisisTela.Hilos = ""
        objclsAnalisisTela.Pasadas = ""
        objclsAnalisisTela.Color = ""
        objclsAnalisisTela.Elongacion = ""
        objclsAnalisisTela.CostoRef = ""
        objclsAnalisisTela.Observacion2 = ""
        objclsAnalisisTela.URLMuestraTela = ""
        objclsAnalisisTela.Usuario = "AAMPUERO"
        objclsAnalisisTela.Mca1 = strEstadoBusqueda
        objclsAnalisisTela.Mca2 = strEstadoBusqueda2
        objclsAnalisisTela.Mca3 = ""
        objclsAnalisisTela.FecDesde = ""
        objclsAnalisisTela.FecHasta = ""
        objclsAnalisisTela.Response = 0


        dgDatos.DataSource = objclsAnalisisTela.CRUDRequerimientos(objclsAnalisisTela)
        dgDatos.DataBind()
    End Sub

    Private Sub dgDatos_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dgDatos.ItemDataBound
        If e.Item.ItemType = ListItemType.AlternatingItem Or e.Item.ItemType = ListItemType.Item Then
            Dim btnEscoger As HtmlControls.HtmlInputButton = CType(e.Item.FindControl("btnEscoger"), HtmlControls.HtmlInputButton)
            Dim drvDatos As DataRowView = CType(e.Item.DataItem, DataRowView)
            btnEscoger.Attributes.Add("onClick", "btnEscoger_Onclick('" & drvDatos("VCH_ID_REQUER") & "', '" & Replace(drvDatos("VCH_REFERENCIA"), "'", "\'").ToString & "')")
        End If
    End Sub

End Class