Imports OFISIS 
Imports CostosLib

Public Partial Class frm_Test
    Inherits System.Web.UI.Page
    Dim mFila As Integer

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If IsPostBack = False Then
            Session("empresa") = "01"
            Session("Usuario") = Request.QueryString("strUsuario")
            txtNumeroReq.Text = Request.QueryString("strNumeroReq")
            txtNumeroDet.Text = Request.QueryString("strNumeroDet")
            Listar_Requisiciones()
        End If

    End Sub

    Private Sub Listar_Requisiciones()
        '*****************************************************************************************************
        'Objetivo   : Muestra el listado de todas las aprobaciones de cada orden
        'Autor      : Darwin Ccorahua Livon
        'Creado     : 22/06/2010
        'Modificado : 00/00/0000
        '*****************************************************************************************************
        'lblMsgO.Text = ""
        'panActualizaO.Visible = False
        'panListadoO.Visible = True
        Dim lobjPre_Costos As New clsRequisicionPreCosto
        Dim objDS As New DataSet
        Dim intNroSec As Integer

        intNroSec = txtNumeroDet.Text

        If lobjPre_Costos.Listar_AprobacionRequisiciones(intNroSec, Session("Usuario"), objDS) = True Then
            'lblCantidad.Text = objDT.Rows.Count

            grdRequisiciones.DataSource = objDS.Tables(0)
            grdRequisiciones.DataBind()
        Else
            'lblMsgO.ForeColor = Drawing.Color.Red
            'lblMsgO.Text = lobjPre_Costos.clsError
        End If
        'txtRegSel.Text = ""
        objDS = Nothing
        lobjPre_Costos = Nothing
        
    End Sub


    Private Sub grdRequisiciones_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles grdRequisiciones.ItemDataBound

        If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Then

            Dim btn As ImageButton = CType(e.Item.FindControl("btnAprobar"), ImageButton)
            Dim dr As DataRowView = CType(e.Item.DataItem, DataRowView)

            If dr.Row("chr_Activar").ToString() = "S" Then
                btn.Visible = True
                btn.Attributes.Add("OnClick", "javascript: return fConfirma('Actualizar los cambios ?');")
            Else
                btn.Visible = False
            End If

        End If

    End Sub

    Private Sub grdRequisiciones_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles grdRequisiciones.ItemCommand
        Select Case e.CommandName
            Case "APROBAR"

                Dim strNumeroReq As String = txtNumeroReq.Text
                Dim strNumeroDet As String = txtNumeroDet.Text
                Dim strPaso As String = e.Item.Cells(1).Text

                Aprobar_Requisiciones(strNumeroDet, strNumeroReq, Session("Usuario"), strPaso)

        End Select
    End Sub

    Private Sub Aprobar_Requisiciones(ByVal StrNumeroDocumento As String, ByVal StrNumeroSolicitud As String, ByVal StrNumeroRequisicion As String, ByVal strPaso As String)
        Dim ldstResultados As New DataSet

        'Dim ldstResultados As DataSet
        Dim lobjAprobaciones As New OFISIS.OFISEGU.Aprobaciones(Session("empresa"), Session("Usuario"))
        Dim strDatos As String = strPaso & "-" & Session("Usuario")
        lobjAprobaciones.Aprobar_RequisicionPreCostos(strDatos, StrNumeroDocumento, ldstResultados)
        lobjAprobaciones = Nothing
        Listar_Requisiciones()

    End Sub

End Class