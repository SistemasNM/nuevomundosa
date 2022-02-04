Imports NM_General.NM_BaseDatos
Namespace NM_Tejeduria
    Public Class NM_DCalidad
        Public CodigoMaestro As String
        Public CodigoDetalle As Integer
        Public Descripcion As String
        Public Usuario As String
        Public Debug As String
        Friend objGen As New NM_Consulta()

        Sub New()
            CodigoMaestro = ""
            CodigoDetalle = ""
            Descripcion = ""
            Usuario = ""
        End Sub

        Sub Add()
            Dim sql As String
            Try
                If CodigoMaestro <> "" AndAlso Descripcion <> "" _
                AndAlso CodigoDetalle <> "" Then
                    sql = "Insert into NM_DCalidad (" & _
                    "codigo_maestro_calidad, descripcion_detalle_calidad, " & _
                    "codigo_detalle_calidad, usuario_creacion, fecha_creacion) values('" & CodigoMaestro & _
                    "','" & Descripcion & "','" & CodigoDetalle & "','" & Usuario & "',getdate())"
                    Debug = sql
                    objGen.Execute(sql)
                End If
            Catch ex As Exception
                Debug = ex.Message
            End Try
        End Sub

        Sub Delete(ByVal CodigoDetalle As Integer, ByVal CodigoMaestro As String)
            Dim sql As String
            Try
                If CodigoDetalle > 0 AndAlso CodigoMaestro <> "" Then
                    sql = "Delete from NM_DCalidad where codigo_detalle_calidad = " & _
                     CodigoDetalle & " and codigo_maestro_calidad = '" & CodigoMaestro & "' "
                    objGen.Execute(sql)
                End If
            Catch ex As Exception

            End Try
        End Sub

        Sub Update()
            Dim sql As String
            Try
                If CodigoMaestro <> "" AndAlso Descripcion <> "" AndAlso CodigoDetalle > 0 Then
                    sql = "Update NM_DCalidad set " & _
                    " descripcion_detalle_calidad = '" & Descripcion & _
                    "',usuario_modificacion='" & Usuario & "',getdate()" & _
                    " where codigo_maestro_calidad='" & CodigoMaestro & "' and " & _
                    " codigo_detalle_calidad = " & CodigoDetalle
                    Debug = sql
                    objGen.Execute(sql)
                End If
            Catch ex As Exception
                Debug = ex.Message
            End Try
        End Sub

        Public Function Lista() As DataTable
            Dim sql As String, objDT As New DataTable()
            sql = "Select * " & _
            " from NM_DCalidad "
            objDT = objGen.Query(sql)
            Return objDT
        End Function

        Public Function Lista(ByVal IsUrdido As Boolean, ByVal IsEngomado As Boolean, ByVal IsTED As Boolean) As DataTable
            Dim sql As String, objDT As New DataTable()
            sql = "Select * " & _
            " from NM_DCalidad D, NM_MaestroDCalidad M " & _
            " where M.codigo_maestro_calidad = D.codigo_maestro_calidad "
            If IsUrdido Then sql = sql & " and Urdido = 1 "
            If IsEngomado Then sql = sql & " and engomado = 1 "
            If IsTED Then sql = sql & " and ted = 1 "
            objDT = objGen.Query(sql)

            Return objDT
        End Function

        Public Function Lista(ByVal pCodigoMaestro As String) As DataTable
            Dim sql As String, objDT As New DataTable()
            sql = "Select * " & _
            " from NM_DCalidad where codigo_maestro_calidad = '" & _
            pCodigoMaestro & "' "
            objDT = objGen.Query(sql)
            Return objDT
        End Function

        Public Function Obtener(ByVal CodigoDetalle As Integer, ByVal CodigoMaestro As String) As DataTable
            Dim sql As String, objDT As New DataTable()
            sql = "Select * " & _
            " from NM_DCalidad where codigo_detalle_calidad = " & _
            CodigoDetalle & "' and codigo_maestro_calidad='" & CodigoMaestro & "' "
            objDT = objGen.Query(sql)
            Return objDT
        End Function

    End Class

End Namespace