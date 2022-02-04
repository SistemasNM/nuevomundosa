Imports NuevoMundo

Public Class frm_AproFacturasPendPago_OCOS
    Inherits System.Web.UI.Page

    Private Sub frm_AproFacturasPendPago_OCOS_Init(sender As Object, e As System.EventArgs) Handles Me.Init
        Session("@EMPRESA") = "01"
        'Session("@USUARIO") = "ECASTILL"

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
        If Not Page.IsPostBack Then
            btnAprobarMasivo.Attributes.Add("onclick", "javascript: return confirm('¿Esta seguro que desea aprobar todos los items seleccionados? ');")
            Call ObtenerFacturasPendientesApro()

        End If


    End Sub

    Protected Sub btnActualizar_Click(sender As Object, e As EventArgs) Handles btnActualizar.Click
        Call ObtenerFacturasPendientesApro()

    End Sub

#Region "Metodos"

    Private Sub CargaEstructuraTabla(ByRef dtFacturasSeleccionadas As DataTable)
        'Crea y Establece Campo codigo_rollo como primary key
        Dim keys(1) As DataColumn
        Dim colID As DataColumn = New DataColumn("int_IDFacturaOCOS")
        colID.DataType = System.Type.GetType("System.Int32")
        colID.AllowDBNull = False
        colID.Unique = True
        dtFacturasSeleccionadas.Columns.Add(colID)
        keys(0) = colID
        dtFacturasSeleccionadas.PrimaryKey = keys

        Dim colTipoFactura As DataColumn = New DataColumn("vch_TipoFactura")
        colTipoFactura.DataType = System.Type.GetType("System.String")
        dtFacturasSeleccionadas.Columns.Add(colTipoFactura)

        Dim colNumeroFactura As DataColumn = New DataColumn("vch_NumeroFactura")
        colNumeroFactura.DataType = System.Type.GetType("System.String")
        dtFacturasSeleccionadas.Columns.Add(colNumeroFactura)


    End Sub


    Private Sub ObtenerFacturasPendientesApro()
        Dim objOrdenCompra As New Logistica.OrdenCompra
        Dim dtFacturasPend As DataTable
        Dim strUsuario As String
        Dim strCodEmpresa As String

        strUsuario = Session("@USUARIO")
        strCodEmpresa = Session("@EMPRESA")
        Try
            dtFacturasPend = objOrdenCompra.ufn_ListarFacturasPendApro_OCOS(strCodEmpresa, strUsuario)
            Call ActualizaGrilla(dtFacturasPend)
            lblMsg.Text = ""
        Catch ex As Exception
            lblMsg.Text = ex.Message
        Finally
            dtFacturasPend = Nothing
            objOrdenCompra = Nothing
        End Try



    End Sub

    Private Sub ActualizaGrilla(ByRef dtDatos As DataTable)
        'Obtenemos el total de registros
        lblCantidad.Text = dtDatos.Rows.Count.ToString

        'Actualizamos la grilla
        grvFacturasPendientes.DataSource = dtDatos
        grvFacturasPendientes.DataBind()

    End Sub
#End Region

    Protected Sub btnAprobarMasivo_Click(sender As Object, e As EventArgs) Handles btnAprobarMasivo.Click
        Dim objOrdenCompra As New Logistica.OrdenCompra
        Dim dtFacturasSeleccionadas As New DataTable
        Dim intResult As Integer
        Dim strUsuario As String        

        Try
            Call CargaEstructuraTabla(dtFacturasSeleccionadas)
            For Each row As GridViewRow In grvFacturasPendientes.Rows
                Dim cb As CheckBox = TryCast(row.FindControl("chkFactura"), CheckBox)
                If cb IsNot Nothing AndAlso cb.Checked Then
                    Dim rowFactura As DataRow = dtFacturasSeleccionadas.NewRow()
                    rowFactura("int_IDFacturaOCOS") = Convert.ToInt32(grvFacturasPendientes.DataKeys(row.RowIndex).Value)
                    rowFactura("vch_TipoFactura") = row.Cells(1).Text
                    rowFactura("vch_NumeroFactura") = row.Cells(2).Text
                    dtFacturasSeleccionadas.Rows.Add(rowFactura)
                End If
            Next

            strUsuario = Session("@USUARIO")

            If dtFacturasSeleccionadas.Rows.Count > 0 Then
                intResult = objOrdenCompra.ufn_AprobarFacturasSeleccionadas_OCOS(strUsuario, dtFacturasSeleccionadas)
                Call ObtenerFacturasPendientesApro()
                lblMsg.Text = "Las Facturas fueron aprobadas satisfactoriamente."
            Else
                Throw New Exception("No existe ninguna factura seleccionada.")
            End If
        Catch ex As Exception
            lblMsg.Text = ex.Message
        Finally
            dtFacturasSeleccionadas = Nothing
            objOrdenCompra = Nothing
        End Try



    End Sub
End Class