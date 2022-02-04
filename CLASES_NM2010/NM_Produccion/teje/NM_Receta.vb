Imports NM_General.NM_BaseDatos
Imports System.Data.SqlClient
Imports NM.AccesoDatos

Namespace NM_Tejeduria

    Public Class NM_Receta
        Public codigo_receta As String
        Public revision_receta As String
        Public area As String
        Public fase As String
        Public tipo_receta As String
        Public dosificacion As String
        Public indigo_peso_fibra As String
        Public usuario_creacion As String
        Public fecha_creacion As String
        Public usuario_modificacion As String
        Public fecha_modificacion As String

        Public olla1 As String
        Public olla2 As String
        Public reserva1 As String
        Public reserva2 As String
        Public tina1 As String
        Public tina2 As String
        Public batea1 As String
        Public batea2 As String
        Public tinated1 As String
        Public tinated2 As String

        Public insumosquimicos As DataTable
        Public parametros As DataTable

        Private _objConexion As AccesoDatosSQLServer


        Sub New()
            codigo_receta = ""
            revision_receta = ""
            area = ""
            fase = ""
            tipo_receta = ""
            dosificacion = ""
            indigo_peso_fibra = ""
            usuario_creacion = ""
            fecha_creacion = ""
            usuario_modificacion = ""
            fecha_modificacion = ""

            olla1 = ""
            olla2 = ""
            reserva1 = ""
            reserva2 = ""
            tina1 = ""
            tina2 = ""
            batea1 = ""
            batea2 = ""
            tinated1 = ""
            tinated2 = ""

        End Sub


        Sub New(ByVal txtcodigo_receta As String, ByVal txtcodigo_area As String)

            Seek(txtcodigo_receta, txtcodigo_area)
            Dim objIQ As New NM_RecetaInsumoQuimico()
            insumosquimicos = objIQ.loadDT(txtcodigo_receta, Me.revision_receta, "1", txtcodigo_area)
            Dim objParametros As New NM_RecetaParametro()
            parametros = objParametros.loadDT(txtcodigo_receta)

        End Sub

        Sub New(ByVal txtcodigo_receta As String, ByVal txtcodigo_area As String, ByVal txtflagestado As String)

            Seek(txtcodigo_receta, txtcodigo_area, txtflagestado)
            Dim objIQ As New NM_RecetaInsumoQuimico()
            insumosquimicos = objIQ.loadDT(txtcodigo_receta, Me.revision_receta, txtflagestado, txtcodigo_area)
            Dim objParametros As New NM_RecetaParametro()
            parametros = objParametros.loadDT(txtcodigo_receta)

        End Sub

        Sub Seek(ByVal txtcodigo_receta As String, ByVal txtcodigo_area As String)
            Dim objGen As New NM_Consulta()
            Dim sql As String
            Dim objDT As New DataTable()
            Dim objDR As DataRow
            sql = " Select * from NM_Receta "
            sql += " Where codigo_receta='" & txtcodigo_receta & "'"
            sql += " and area ='" & txtcodigo_area & "'"
            sql += " and flagestado = 1 "
            objDT = objGen.Query(sql)

            If objDT.Rows.Count > 0 Then

                For Each objDR In objDT.Rows

                    If Not IsDBNull(objDR("codigo_receta")) Then
                        codigo_receta = objDR("codigo_receta")
                    End If
                    If Not IsDBNull(objDR("revision_receta")) Then
                        revision_receta = objDR("revision_receta")
                    End If
                    If Not IsDBNull(objDR("area")) Then
                        area = objDR("area")
                    End If
                    If Not IsDBNull(objDR("codigo_fase")) Then
                        fase = objDR("codigo_fase")
                    End If
                    If Not IsDBNull(objDR("tipo_receta")) Then
                        tipo_receta = objDR("tipo_receta")
                    End If
                    If Not IsDBNull(objDR("dosificacion")) Then
                        dosificacion = objDR("dosificacion")
                    Else
                        dosificacion = "0"
                    End If
                    If Not IsDBNull(objDR("indigo_peso_fibra")) Then
                        indigo_peso_fibra = objDR("indigo_peso_fibra")
                    Else
                        indigo_peso_fibra = "0"
                    End If
                    If Not IsDBNull(objDR("olla1")) Then
                        olla1 = objDR("olla1")
                    Else
                        olla1 = "0"
                    End If
                    If Not IsDBNull(objDR("olla2")) Then
                        olla2 = objDR("olla2")
                    Else
                        olla2 = "0"
                    End If
                    If Not IsDBNull(objDR("reserva1")) Then
                        reserva1 = objDR("reserva1")
                    Else
                        reserva1 = "0"
                    End If
                    If Not IsDBNull(objDR("reserva2")) Then
                        reserva2 = objDR("reserva2")
                    Else
                        reserva2 = "0"
                    End If
                    If Not IsDBNull(objDR("tina1")) Then
                        tina1 = objDR("tina1")
                    Else
                        tina1 = "0"
                    End If
                    If Not IsDBNull(objDR("tina2")) Then
                        tina2 = objDR("tina2")
                    Else
                        tina2 = "0"
                    End If
                    If Not IsDBNull(objDR("batea1")) Then
                        batea1 = objDR("batea1")
                    Else
                        batea1 = "0"
                    End If
                    If Not IsDBNull(objDR("batea2")) Then
                        batea2 = objDR("batea2")
                    Else
                        batea2 = "0"
                    End If
                    If Not IsDBNull(objDR("tinated1")) Then
                        tinated1 = objDR("tinated1")
                    Else
                        tinated1 = "0"
                    End If
                    If Not IsDBNull(objDR("tinated2")) Then
                        tinated2 = objDR("tinated2")
                    Else
                        tinated2 = "0"
                    End If
                Next
            End If

        End Sub

        Sub SeekByRevision(ByVal txtcodigo_receta As String, ByVal revisionReceta As Integer, ByVal txtcodigo_area As String)
            Dim objGen As New NM_Consulta()
            Dim sql As String
            Dim objDT As New DataTable()
            Dim objDR As DataRow
            sql = " Select * from NM_Receta "
            sql += " Where codigo_receta='" & txtcodigo_receta & "'"
            sql += " and revision_receta = " & revisionReceta
            sql += " and area ='" & txtcodigo_area & "'"

            objDT = objGen.Query(sql)
            If objDT.Rows.Count > 0 Then

                For Each objDR In objDT.Rows

                    If Not IsDBNull(objDR("codigo_receta")) Then
                        codigo_receta = objDR("codigo_receta")
                    End If
                    If Not IsDBNull(objDR("revision_receta")) Then
                        revision_receta = objDR("revision_receta")
                    End If
                    If Not IsDBNull(objDR("area")) Then
                        area = objDR("area")
                    End If
                    If Not IsDBNull(objDR("codigo_fase")) Then
                        fase = objDR("codigo_fase")
                    End If
                    If Not IsDBNull(objDR("tipo_receta")) Then
                        tipo_receta = objDR("tipo_receta")
                    End If
                    If Not IsDBNull(objDR("dosificacion")) Then
                        dosificacion = objDR("dosificacion")
                    Else
                        dosificacion = "0"
                    End If
                    If Not IsDBNull(objDR("indigo_peso_fibra")) Then
                        indigo_peso_fibra = objDR("indigo_peso_fibra")
                    Else
                        indigo_peso_fibra = "0"
                    End If
                    If Not IsDBNull(objDR("olla1")) Then
                        olla1 = objDR("olla1")
                    Else
                        olla1 = "0"
                    End If
                    If Not IsDBNull(objDR("olla2")) Then
                        olla2 = objDR("olla2")
                    Else
                        olla2 = "0"
                    End If
                    If Not IsDBNull(objDR("reserva1")) Then
                        reserva1 = objDR("reserva1")
                    Else
                        reserva1 = "0"
                    End If
                    If Not IsDBNull(objDR("reserva2")) Then
                        reserva2 = objDR("reserva2")
                    Else
                        reserva2 = "0"
                    End If
                    If Not IsDBNull(objDR("tina1")) Then
                        tina1 = objDR("tina1")
                    Else
                        tina1 = "0"
                    End If
                    If Not IsDBNull(objDR("tina2")) Then
                        tina2 = objDR("tina2")
                    Else
                        tina2 = "0"
                    End If
                    If Not IsDBNull(objDR("batea1")) Then
                        batea1 = objDR("batea1")
                    Else
                        batea1 = "0"
                    End If
                    If Not IsDBNull(objDR("batea2")) Then
                        batea2 = objDR("batea2")
                    Else
                        batea2 = "0"
                    End If
                    If Not IsDBNull(objDR("tinated1")) Then
                        tinated1 = objDR("tinated1")
                    Else
                        tinated1 = "0"
                    End If
                    If Not IsDBNull(objDR("tinated2")) Then
                        tinated2 = objDR("tinated2")
                    Else
                        tinated2 = "0"
                    End If

                    Dim recetaDet As New NM_RecetaInsumoQuimico()
                    insumosquimicos = recetaDet.loadDT(txtcodigo_receta, revisionReceta, txtcodigo_area)

                Next
            End If

        End Sub
        Sub Seek(ByVal txtcodigo_receta As String, ByVal txtcodigo_area As String, ByVal txtflagestado As String)
            Dim objGen As New NM_Consulta()
            Dim sql As String
            Dim objDT As New DataTable()
            Dim objDR As DataRow
            sql = " Select * from NM_Receta "
            sql += " Where codigo_receta='" & txtcodigo_receta & "'"
            sql += " and area ='" & txtcodigo_area & "'"
            sql += " and flagestado = " & txtflagestado
            objDT = objGen.Query(sql)
            If objDT.Rows.Count > 0 Then

                For Each objDR In objDT.Rows

                    If Not IsDBNull(objDR("codigo_receta")) Then
                        codigo_receta = objDR("codigo_receta")
                    End If
                    If Not IsDBNull(objDR("revision_receta")) Then
                        revision_receta = objDR("revision_receta")
                    End If
                    If Not IsDBNull(objDR("area")) Then
                        area = objDR("area")
                    End If
                    If Not IsDBNull(objDR("codigo_fase")) Then
                        fase = objDR("codigo_fase")
                    End If
                    If Not IsDBNull(objDR("tipo_receta")) Then
                        tipo_receta = objDR("tipo_receta")
                    End If
                    If Not IsDBNull(objDR("dosificacion")) Then
                        dosificacion = objDR("dosificacion")
                    Else
                        dosificacion = "0"
                    End If
                    If Not IsDBNull(objDR("indigo_peso_fibra")) Then
                        indigo_peso_fibra = objDR("indigo_peso_fibra")
                    Else
                        indigo_peso_fibra = "0"
                    End If
                    If Not IsDBNull(objDR("olla1")) Then
                        olla1 = objDR("olla1")
                    Else
                        olla1 = "0"
                    End If
                    If Not IsDBNull(objDR("olla2")) Then
                        olla2 = objDR("olla2")
                    Else
                        olla2 = "0"
                    End If
                    If Not IsDBNull(objDR("reserva1")) Then
                        reserva1 = objDR("reserva1")
                    Else
                        reserva1 = "0"
                    End If
                    If Not IsDBNull(objDR("reserva2")) Then
                        reserva2 = objDR("reserva2")
                    Else
                        reserva2 = "0"
                    End If
                    If Not IsDBNull(objDR("tina1")) Then
                        tina1 = objDR("tina1")
                    Else
                        tina1 = "0"
                    End If
                    If Not IsDBNull(objDR("tina2")) Then
                        tina2 = objDR("tina2")
                    Else
                        tina2 = "0"
                    End If
                    If Not IsDBNull(objDR("batea1")) Then
                        batea1 = objDR("batea1")
                    Else
                        batea1 = "0"
                    End If
                    If Not IsDBNull(objDR("batea2")) Then
                        batea2 = objDR("batea2")
                    Else
                        batea2 = "0"
                    End If
                    If Not IsDBNull(objDR("tinated1")) Then
                        tinated1 = objDR("tinated1")
                    Else
                        tinated1 = "0"
                    End If
                    If Not IsDBNull(objDR("tinated2")) Then
                        tinated2 = objDR("tinated2")
                    Else
                        tinated2 = "0"
                    End If
                Next
            End If

        End Sub

        Function List(ByVal sarea As String) As DataTable
            Dim objGen As New NM_Consulta()
            Dim sql As String
            Dim objDT As New DataTable()
            sql = "Select F.descripcion_fase, R.* " & _
            " from NM_Receta R, NM_FASE F " & _
            " where R.codigo_fase = F.codigo_fase " & _
            " and R.area ='" & sarea & "' and flagestado=1 "
            objDT = objGen.Query(sql)
            Return objDT
        End Function

        Function List(ByVal sarea As String, ByVal sCodigoFase As Integer) As DataTable
            Dim objGen As New NM_Consulta()
            Dim sql As String
            Dim objDT As New DataTable()
            sql = "Select * from NM_Receta where area = '" & sarea & "' and codigo_fase=" & sCodigoFase & " and flagestado = 1 "
            objDT = objGen.Query(sql)
            Return objDT
        End Function

        Public Function loadDT(ByVal txtcodigo_urdimbre As String) As DataTable

            Dim objGen As New NM_Consulta()
            Dim sql As String
            Dim objDT As New DataTable()
            sql = "Select * from NM_Recetainsumoquimico where codigo_urdimbre='" & txtcodigo_urdimbre & "'"

            objDT = objGen.Query(sql)

            Return objDT

        End Function

        Sub Update()
            Dim objGen As New NM_Consulta()
            Dim sql As String, codErr As Integer = 0
            Try
                If codigo_receta <> "" AndAlso Val(revision_receta) >= 0 AndAlso area <> "" AndAlso _
                Val(fase) >= 0 AndAlso Val(tipo_receta) >= 0 AndAlso dosificacion >= 0 AndAlso _
                indigo_peso_fibra >= 0 Then
                    sql = "UPDATE NM_Receta SET " & _
                    " codigo_fase = " & fase & ", tipo_receta = " & tipo_receta & " " & _
                    ", dosificacion = " & dosificacion & ", indigo_peso_fibra = " & indigo_peso_fibra & " " & _
                    ", olla1 = " & olla1 & ", olla2 = " & olla2 & _
                    ", reserva1 = " & reserva1 & ", reserva2 = " & reserva2 & _
                    ", tina1 = " & tina1 & ", tina2 = " & tina2 & _
                    ", batea1 = " & batea1 & ", batea2 = " & batea2 & _
                    ", tinated1 = " & tinated1 & ", tinated2 = " & tinated2 & _
                    "  where codigo_receta = '" & codigo_receta & "' and revision_receta = " & revision_receta & _
                    " and area = '" & area & "'"
                    codErr = objGen.Execute(sql)
                Else
                    codErr = 0
                End If
            Catch ex As Exception
                codErr = 0
            End Try
        End Sub

        Public Sub Delete(ByVal Codigo As String, ByVal Revision As Integer)
            Dim sql As String
            Dim objGen As New NM_Consulta()
            Try
                If Codigo <> "" And Val(Revision) >= 0 Then
                    sql = "Delete from NM_Receta where " & _
                    "codigo_receta = '" & Codigo & "' and revision_receta=" & Revision
                    objGen.Execute(sql)
                Else
                    Throw New Exception("No se puedo borrar")
                End If
            Catch ex As Exception

            End Try
        End Sub

        Public Sub insert()
            Dim objGen As New NM_Consulta()
            Dim sql As String
            Dim objDT As New DataTable()
            Dim objDR As DataRow

            'sql = " UPDATE NM_Receta SET "
            'sql += " flagestado=0 "
            'sql += " Where codigo_receta='" & codigo_receta & "'"
            'sql += " and flagestado = 1  and area='" & Me.area & "'"
            'objGen.Execute(sql)

            Dim commandString As New System.Text.StringBuilder()
            commandString.Append("INSERT INTO NM_Receta (codigo_receta,revision_receta,area,codigo_fase,tipo_receta,dosificacion,indigo_peso_fibra,")
            commandString.Append("olla1,olla2,reserva1,reserva2,tina1,tina2,batea1,batea2,tinated1,tinated2,flagestado) VALUES ('")

            commandString.Append(codigo_receta & "',")
            commandString.Append(revision_receta & ",'")
            commandString.Append(area & "',")
            commandString.Append(fase & ",")
            commandString.Append(tipo_receta & ",")
            commandString.Append(dosificacion & ",")   'dosificacion
            commandString.Append(indigo_peso_fibra & ",")

            commandString.Append(olla1 & ",")
            commandString.Append(olla2 & ",")
            commandString.Append(reserva1 & ",")
            commandString.Append(reserva2 & ",")
            commandString.Append(tina1 & ",")
            commandString.Append(tina2 & ",")
            commandString.Append(batea1 & ",")
            commandString.Append(batea2 & ",")
            commandString.Append(tinated1 & ",")
            commandString.Append(tinated2 & ",")
            commandString.Append("1)")

            objDT = objGen.Query(commandString.ToString)

        End Sub

        Public Sub reserva()
            Dim objGen As New NM_Consulta()
            Dim sql As String
            Dim objDT As New DataTable()
            Dim objDR As DataRow

            Dim commandString As New System.Text.StringBuilder()
            commandString.Append("INSERT INTO NM_Receta (codigo_receta,revision_receta,area,codigo_fase,tipo_receta,dosificacion,indigo_peso_fibra,")
            commandString.Append("olla1,olla2,reserva1,reserva2,tina1,tina2,batea1,batea2,tinated1,tinated2,flagestado) VALUES ('")

            commandString.Append(codigo_receta & "',")
            commandString.Append(revision_receta & ",'")
            commandString.Append(area & "',")
            commandString.Append(fase & ",")
            commandString.Append(tipo_receta & ",")
            commandString.Append(dosificacion & ",")   'dosificacion
            commandString.Append(indigo_peso_fibra & ",")

            commandString.Append(olla1 & ",")
            commandString.Append(olla2 & ",")
            commandString.Append(reserva1 & ",")
            commandString.Append(reserva2 & ",")
            commandString.Append(tina1 & ",")
            commandString.Append(tina2 & ",")
            commandString.Append(batea1 & ",")
            commandString.Append(batea2 & ",")
            commandString.Append(tinated1 & ",")
            commandString.Append(tinated2 & ",")
            commandString.Append("2)")

            objDT = objGen.Query(commandString.ToString)

        End Sub

        Public Sub paseRevision()
            Dim objGen As New NM_Consulta()
            Dim sql As String
            Dim objDT As New DataTable()
            Dim objDR As DataRow

            sql = " UPDATE NM_Receta SET "
            sql += " flagestado=0 "
            sql += " Where codigo_receta='" & codigo_receta & "'"
            sql += " and area='" & area & "'"
            sql += " and flagestado = 1  "
            objGen.Execute(sql)

            sql = " UPDATE NM_Receta SET "
            sql += " flagestado=1 "
            sql += " Where codigo_receta='" & codigo_receta & "'"
            sql += " and area='" & area & "'"
            sql += " and flagestado = 2  "
            objGen.Execute(sql)

            Me.Update()

        End Sub

        Public Function Listar(ByVal strCodigoReceta As String, ByVal strCodigoFase As String) As DataTable
            Try
                _objConexion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.Produccion)
                Dim objParametros() = {"var_CodigoReceta", strCodigoReceta, "var_CodigoFase", strCodigoFase}
                Return _objConexion.ObtenerDataTable("usp_PTJ_Receta_Obtener", objParametros)
            Catch ex As Exception
                Throw ex
            End Try
        End Function
    End Class


End Namespace