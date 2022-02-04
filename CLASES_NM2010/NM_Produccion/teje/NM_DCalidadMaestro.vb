Imports NM_General.NM_BaseDatos
Namespace NM_Tejeduria
    Public Class NM_DCalidadMaestro
        Public CodigoMaestro As String
        Public Descripcion As String
        Public Urdido As Int16 = 0
        Public Engomado As Int16 = 0
        Public TED As Int16 = 0
        Public Usuario As String
        Public Debug As String
        Public dtDetalle As New DataTable()

        Sub New()
            CodigoMaestro = ""
            Descripcion = ""
            Urdido = 0
            Engomado = 0
            TED = 0
        End Sub

        Sub New(ByVal Codigo As String)
            Dim objDet As New NM_DCalidad()
            CodigoMaestro = Codigo
            Seek(CodigoMaestro)
            dtDetalle = objDet.Lista(CodigoMaestro)
        End Sub

        Sub Add()
            Dim sql As String, objConn As New NM_Consulta()
            If CodigoMaestro <> "" And Descripcion <> "" Then
                sql = "Insert into NM_MaestroDCalidad (codigo_maestro_calidad," & _
                "descripcion_maestro, engomado, urdido, ted, " & _
                "usuario_creacion,fecha_creacion) values ('" & _
                CodigoMaestro & "','" & Descripcion & "'," & Engomado & _
                "," & Urdido & "," & TED & ",'" & Usuario & "', getdate())"
                Debug = sql
                objConn.Execute(sql)
            End If
        End Sub

        Sub Update()
            Dim sql As String, objConn As New NM_Consulta()
            If CodigoMaestro <> "" And Descripcion <> "" Then
                sql = "Update NM_MaestroDCalidad set descripcion_maestro ='" & _
                Descripcion & "' usuario_modificacion ='" & Usuario & "', " & _
                "fecha_modificacion=getdate() where codigo_maestro_calidad = '" & CodigoMaestro & "' "
                Debug = sql
                objConn.Execute(sql)
            End If
        End Sub

        Sub Delete(ByVal Codigo As String)
            Dim sql As String, objConn As New NM_Consulta()
            sql = "Delete from NM_MaestroDCalidad where codigo_maestro_calidad='" & _
            Codigo & "' "
            objConn.Execute(sql)
        End Sub

        Public Function Lista()
            Dim sql As String, objConn As New NM_Consulta()
            sql = "Select * from NM_MaestroDCalidad "
            Return objConn.Query(sql)
        End Function

        Public Function Lista(ByVal IsUrdido As Boolean, ByVal IsEngomado As Boolean, ByVal IsTED As Boolean) As DataTable
            Dim sql As String, objConn As New NM_Consulta()
            sql = "Select * from NM_MaestroDCalidad " & _
            " where codigo_detalle_calidad <> NULL "
            If IsUrdido Then sql = sql & " and Urdido = 1 "
            If IsEngomado Then sql = sql & " and engomado = 1 "
            If IsTED Then sql = sql & " and ted = 1 "

            Return objConn.Query(sql)
        End Function

        Sub Seek(ByVal Codigo As String)
            Dim sql As String, objConn As New NM_Consulta()
            Dim dtTable As New DataTable(), fila As DataRow
            sql = "Select * from NM_MaestroDCalidad where " & _
            " codigo_maestro_calidad = '" & Codigo & "' "
            dtTable = objConn.Query(sql)
            For Each fila In dtTable.Rows
                Descripcion = fila.Item("descripcion_maestro")
                Urdido = fila.Item("urdido")
                Engomado = fila.Item("engomado")
                TED = fila.Item("ted")
            Next
        End Sub

    End Class

End Namespace