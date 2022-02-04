Imports NM_General.NM_BaseDatos
Imports System.Data.SqlClient
Imports NM.AccesoDatos


Namespace NM_Tejeduria


    Public Class NM_RecetaInsumoQuimico
        Public codigo_receta As String
        Public revision_receta As String
        Public area As String
        Public codigo_insumoquimico As String
        Public be As String
        Public concentracion As Double
        Public usuario_creacion As String
        Public fecha_creacion As String
        Public usuario_modificacion As String
        Public fecha_modificacion As String
        Private _objConexion As AccesoDatosSQLServer


        Sub New()
            codigo_receta = ""
            revision_receta = ""
            codigo_insumoquimico = ""
            be = ""
            concentracion = 0
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
            sql = "Select * from NM_RecetaInsumoQuimico where codigo_receta='" & codigo_receta & "' and " & _
            "codigo_insumoquimico='" & codigo_insumoquimico & "'"
            objDT = objGen.Query(sql)

            For Each objDR In objDT.Rows
                codigo_receta = objDR("codigo_receta")
                revision_receta = objDR("revision_receta")
                codigo_insumoquimico = objDR("codigo_insumoquimico")
                be = objDR("be")
                concentracion = objDR("concentracion")
                usuario_creacion = objDR("usuario_creacion")
                fecha_creacion = objDR("fecha_creacion")
                usuario_modificacion = objDR("usuario_modificacion")
                fecha_modificacion = objDR("fecha_modificacion")
            Next

        End Sub

        Sub Seek(ByVal txtcodigo_receta As String, ByVal txtcodigo_insumoquimico As String, ByVal txtarea As String)
            Dim objGen As New NM_Consulta()
            Dim sql As String
            Dim objDT As New DataTable()
            Dim objDR As DataRow
            sql = "Select * from NM_RecetaInsumoQuimico where codigo_receta='" & txtcodigo_receta & "' and " & _
            "codigo_insumoquimico='" & codigo_insumoquimico & "'" & _
            "and area='" + txtarea + "'"
            objDT = objGen.Query(sql)

            For Each objDR In objDT.Rows
                codigo_receta = objDR("codigo_receta")
                revision_receta = objDR("revision_receta")
                codigo_insumoquimico = objDR("codigo_insumoquimico")
                be = objDR("be")
                concentracion = objDR("concentracion")
                usuario_creacion = objDR("usuario_creacion")
                fecha_creacion = objDR("fecha_creacion")
                usuario_modificacion = objDR("usuario_modificacion")
                fecha_modificacion = objDR("fecha_modificacion")
            Next

        End Sub
        Sub Seek(ByVal txtcodigo_receta As String, ByVal txtrevision_receta As Integer, ByVal txtcodigo_insumoquimico As String, ByVal txtarea As String)
            Dim objGen As New NM_Consulta()
            Dim sql As String
            Dim objDT As New DataTable()
            Dim objDR As DataRow
            sql = "Select * from NM_RecetaInsumoQuimico where codigo_receta='" & txtcodigo_receta & "' and " & _
            "revision_receta =" & txtrevision_receta & " and codigo_insumoquimico='" & Trim(txtcodigo_insumoquimico) & "'" & _
            "and area='" + txtarea + "'"
            objDT = objGen.Query(sql)
            concentracion = 0.0
            For Each objDR In objDT.Rows
                codigo_receta = objDR("codigo_receta")
                revision_receta = objDR("revision_receta")
                codigo_insumoquimico = objDR("codigo_insumoquimico")
                be = objDR("be")
                concentracion = objDR("concentracion")
            Next

        End Sub

        Public Function loadDT(ByVal txtreceta As String, ByVal txtrevision As Integer, ByVal txtarea As String) As DataTable

            Dim objGen As New NM_Consulta()
            Dim sql As String
            Dim objDT As New DataTable()

            sql = " select ri.codigo_insumoquimico , " & _
                "ri.be,ri.concentracion " & _
                " from " & _
                " nm_receta r," & _
                " nm_recetainsumoquimico ri" & _
                " where " & _
                " r.codigo_receta  = '" & txtreceta & "' and " & _
                " r.revision_receta = " & txtrevision & " and " & _
                " r.area = '" & txtarea & "' and " & _
                " r.codigo_receta  = ri.codigo_receta     and" & _
                " r.revision_receta = ri.revision_receta  "

            objDT = objGen.Query(sql)

            Return objDT

        End Function

        Public Function loadDT(ByVal txtreceta As String, ByVal txtrevision As String, ByVal txtestado As String, ByVal txtarea As String) As DataTable

            Dim objGen As New NM_Consulta()
            Dim sql As String
            Dim objDT As New DataTable()
            If txtrevision = "" Then
                txtrevision = "0"
            End If ' " nm_insumoquimico i," & _
            sql = " select ri.codigo_insumoquimico , " & _
                "ri.be,ri.concentracion " & _
                " from " & _
                " nm_receta r," & _
                " nm_recetainsumoquimico ri" & _
                " where " & _
                " r.codigo_receta  = '" & txtreceta & "' and " & _
                " r.revision_receta = " & txtrevision & " and " & _
                " r.flagestado = " & txtestado & " and " & _
                " r.area = '" & txtarea & "' and " & _
                " r.codigo_receta  = ri.codigo_receta     and" & _
                " r.revision_receta = ri.revision_receta  "

            objDT = objGen.Query(sql)

            Return objDT

        End Function

        Public Sub delete()
            Dim objGen As New NM_Consulta()
            Dim sql As String
            Dim objDT As New DataTable()
            Dim objDR As DataRow
            sql = "delete from NM_RecetaInsumoQuimico where codigo_receta='" & codigo_receta & "' and " & _
            "codigo_insumoquimico='" & codigo_insumoquimico & "'"
            objDT = objGen.Query(sql)
        End Sub

        Public Sub delete(ByVal txtcodigo_receta As String, ByVal txtcodigo_insumoquimico As String)
            Dim objGen As New NM_Consulta()
            Dim sql As String
            Dim objDT As New DataTable()
            Dim objDR As DataRow
            sql = "delete from NM_RecetaInsumoQuimico where codigo_receta='" & txtcodigo_receta & "' and " & _
            "codigo_insumoquimico=" & txtcodigo_insumoquimico
            objDT = objGen.Query(sql)
        End Sub

        Public Sub update()
            Dim objGen As New NM_Consulta()
            Dim sql As String
            Dim objDT As New DataTable()
            Dim objDR As DataRow

            sql = "Update NM_RecetaInsumoQuimico Set " & _
            "be = " & be & "," & _
            "concentracion = " & be & " " & _
            "Where codigo_receta = '" & codigo_receta & "' AND " & _
            " codigo_insumoquimico = '" & codigo_insumoquimico & "'"

            objDT = objGen.Query(sql)
        End Sub

        Public Sub insert()
            Dim objGen As New NM_Consulta()
            Dim sql As String
            Dim objDT As New DataTable()
            Dim objDR As DataRow

            Dim commandString As New System.Text.StringBuilder()
            commandString.Append("INSERT INTO NM_RecetaInsumoQuimico (codigo_receta, revision_receta , area , codigo_insumoquimico, be ,concentracion) VALUES ('")
            commandString.Append(codigo_receta & "',")
            commandString.Append(revision_receta & ",'")
            commandString.Append(area & "','")
            commandString.Append(codigo_insumoquimico & "',")
            commandString.Append(be & ",")
            commandString.Append(concentracion & ")")

            objGen.Execute(commandString.ToString)

        End Sub

        Public Sub reserva(ByVal txtreceta As String, ByVal txtrevision As String, ByVal txtarea As String)
            Dim objGen As New NM_Consulta()
            Dim sql As String
            Dim objDT As New DataTable()
            Dim objDR As DataRow

            sql = "INSERT INTO NM_RecetaInsumoQuimico (codigo_receta, revision_receta , area, codigo_insumoquimico, be ,concentracion) "
            sql += "SELECT codigo_receta, " + txtrevision + " , '" + txtarea + "' , codigo_insumoquimico, be ,concentracion from NM_RecetaInsumoQuimico "
            sql += "WHERE codigo_receta='" + txtreceta + "'"
            sql += "and area='" + txtarea + "'"
            sql += " and revision_receta = (select revision_receta from nm_receta where codigo_receta='" + txtreceta + "' and area ='" + txtarea + "' and flagestado = 1 )"

            objGen.Execute(sql)

        End Sub

        Public Function Listar(ByVal strCodigoReceta As String, ByVal strCodigoArea As String) As DataTable
            Try
                _objConexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.Produccion)
                Dim objParametros() As Object = {"var_CodigoReceta", strCodigoReceta, "var_CodigoArea", strCodigoArea}
                Return _objConexion.ObtenerDataTable("usp_PTJ_RecetaInsumos_Obtener", objParametros)
            Catch ex As Exception
                Throw ex
            End Try
        End Function
    End Class

End Namespace