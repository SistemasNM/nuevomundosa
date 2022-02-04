Imports NM_General.NM_BaseDatos
Imports System.Data.SqlClient

Namespace NM_Tejeduria

    Public Class NM_RecetaParametro
        Public codigo_receta As String
        Public item As String
        Public olla As Single
        Public reserva As Single
        Public tina As Single
        Public usuario_creacion As String
        Public fecha_creacion As String
        Public usuario_modificacion As String
        Public fecha_modificacion As String

        Sub New()
            codigo_receta = ""
            item = 0
            olla = 0
            reserva = 0
            tina = 0
            usuario_creacion = ""
            fecha_creacion = ""
            usuario_modificacion = ""
            fecha_modificacion = ""
        End Sub

        Sub New(ByVal txtcodigo_receta As String)
            codigo_receta = txtcodigo_receta
            Seek()
        End Sub

        Sub Seek()
            Dim objGen As New NM_Consulta()
            Dim sql As String
            Dim objDT As New DataTable()
            Dim objDR As DataRow
            sql = "Select * from NM_RecetaParametro where codigo_receta='" & codigo_receta & "' and " & _
            "item='" & item & "'"
            objDT = objGen.Query(sql)

            For Each objDR In objDT.Rows
                codigo_receta = objDR("codigo_receta")
                item = objDR("item")
                olla = objDR("olla")
                reserva = objDR("reserva")
                tina = objDR("tina")
                usuario_creacion = objDR("usuario_creacion")
                fecha_creacion = objDR("fecha_creacion")
                usuario_modificacion = objDR("usuario_modificacion")
                fecha_modificacion = objDR("fecha_modificacion")
            Next

        End Sub

        Sub Seek(ByVal txtcodigo_receta As String, ByVal strTipo As String)
            Dim objGen As New NM_Consulta()
            Dim sql As String
            Dim objDT As New DataTable()
            Dim objDR As DataRow
            sql = "Select * from NM_RecetaParametro where codigo_receta='" & txtcodigo_receta & "' and " & _
            "tipo='" & strTipo & "'"
            objDT = objGen.Query(sql)

            For Each objDR In objDT.Rows
                codigo_receta = objDR("codigo_receta")
                item = objDR("item")
                olla = objDR("olla")
                reserva = objDR("reserva")
                tina = objDR("tina")
                usuario_creacion = objDR("usuario_creacion")
                fecha_creacion = objDR("fecha_creacion")
                usuario_modificacion = objDR("usuario_modificacion")
                fecha_modificacion = objDR("fecha_modificacion")
            Next

        End Sub

        Public Function loadDT(ByVal txtreceta As String) As DataTable

            Dim objGen As New NM_Consulta()
            Dim sql As String
            Dim objDT As New DataTable()
            sql = " select p.item , " & _
                "p.olla,p.reserva,p.tina " & _
                " from " & _
                " NM_RecetaParametro p," & _
                " nm_receta r " & _
                " where " & _
                " r.codigo_receta  = '" & txtreceta & "' and " & _
                " r.codigo_receta  = p.codigo_receta"
            objDT = objGen.Query(sql)

            Return objDT

        End Function

        Public Sub delete()
            Dim objGen As New NM_Consulta()
            Dim sql As String
            Dim objDT As New DataTable()
            Dim objDR As DataRow
            sql = "delete from NM_RecetaInsumoQuimico where codigo_receta='" & codigo_receta & "' and " & _
            "item='" & item & "'"
            objDT = objGen.Query(sql)
        End Sub

        Public Sub delete(ByVal txtcodigo_receta As String, ByVal txtitem As String)
            Dim objGen As New NM_Consulta()
            Dim sql As String
            Dim objDT As New DataTable()
            Dim objDR As DataRow
            sql = "delete from NM_RecetaParametro where codigo_receta='" & txtcodigo_receta & "' and " & _
            "item=" & txtitem
            objDT = objGen.Query(sql)
        End Sub

        Public Sub update()
            Dim objGen As New NM_Consulta()
            Dim sql As String
            Dim objDT As New DataTable()
            Dim objDR As DataRow

            sql = "Update NM_RecetaParametro Set " & _
            "item = " & item & "," & _
            "olla = " & olla & "," & _
            "reserva = " & reserva & ", " & _
            "tina = " & tina & ", " & _
            "usuario_modificacion = '" & usuario_modificacion & "', " & _
            "Fecha_modificacion = getdate() " & _
            " Where codigo_receta = '" & codigo_receta & "' AND " & _
            " item = " & item

            objDT = objGen.Query(sql)
        End Sub

        Public Sub insert()
            Dim objGen As New NM_Consulta()
            Dim sql As String
            Dim objDT As New DataTable()
            Dim objDR As DataRow

            sql = " select max(item)+1 from NM_RecetaParametro where " & _
            "codigo_receta='" & codigo_receta & "' "


            objDT = objGen.Query(sql)

            If objDT.Rows(0).IsNull(0) Then
                item = 1
            Else
                item = objDT.Rows(0)(0)
            End If

            Dim commandString As New System.Text.StringBuilder()
            commandString.Append("INSERT INTO NM_RecetaParametro (codigo_receta , item, olla ,reserva, tina, usuario_creacion, " & _
            "fecha_creacion) VALUES ('")

            commandString.Append(codigo_receta & "',")
            commandString.Append(item & ",")
            commandString.Append(olla & ",")
            commandString.Append(reserva & ",")
            commandString.Append(tina & ",'")
            commandString.Append("devel02'" & ",")
            commandString.Append(Date.Today & ")")

            objDT = objGen.Query(commandString.ToString)

        End Sub

    End Class

End Namespace