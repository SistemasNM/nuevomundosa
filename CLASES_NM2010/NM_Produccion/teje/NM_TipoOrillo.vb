Imports NM_General.NM_BaseDatos

Namespace NM_Tejeduria


Public Class NM_TipoOrillo
        Public CodigoTipo As String
        Public DescripcionTipo As String

        Function Lista() As DataTable
            Dim objGen As New NM_Consulta()
            Dim sql = "Select * from NM_TipoOrillo "
            Return objGen.Query(sql)
        End Function

        Function ListaTipoOrillo() As DataTable
            Dim objGen As New NM_Consulta()
            Dim sql = "Select * from NM_TipoOrillo where codigo_tipo > 1"
            Return objGen.Query(sql)
        End Function

        Function ListaTipoHilo() As DataTable
            Dim objGen As New NM_Consulta()
            Dim sql = "Select * from NM_TipoOrillo where codigo_tipo <= 1"
            Return objGen.Query(sql)
        End Function

        Sub Seek(ByVal pCodigoTipo As String)
            Dim objConn As New NM_Consulta, sql As String
            Dim dtTipo As New DataTable, drTipo As DataRow
            sql = "Select * from NM_TipoOrillo where codigo_tipo = " & pCodigoTipo
            dtTipo = objConn.Query(sql)
            For Each drTipo In dtTipo.Rows
                CodigoTipo = drTipo("codigo_tipo")
                DescripcionTipo = drTipo("descripcion_tipo")
            Next

        End Sub

End Class


End Namespace