Imports NM_General.NM_BaseDatos

Namespace NM_Tejeduria

    Public Class NM_TelaUrdimbreD

        Public CodigoArticulo As String
        Public RevisionArticulo As String
        Public CodigoUrdimbre As String
        Public RevisionUrdimbre As String
        Public Item As Integer
        Public CodigoHilo As String
        Public Tipo As String
        Public TipoOrillo As String
        Public Usuario As String

        Sub New()
            CodigoUrdimbre = ""
            RevisionUrdimbre = ""
            Item = 0
            CodigoHilo = 0
            Tipo = ""
            TipoOrillo = ""
            Usuario = ""
        End Sub

        Sub Seek(ByVal pCodigoUrdimbre As String, ByVal pRevisionUrdimbre As String, _
        ByVal pCodigoArticulo As String, ByVal pRevisionArticulo As String, ByVal pItem As String)
            Dim objConn As New NM_Consulta
            Dim sql As String
            Dim objDT As New DataTable
            Dim objDR As DataRow

            sql = "Select * from NM_MA_UrdimbreDetalle where codigo_urdimbre='" & _
            pCodigoUrdimbre & "' and revision_urdimbre = " & pRevisionUrdimbre & " " & _
            " and codigo_articulo='" & pCodigoArticulo & "' and revision_articulo = " & _
            pRevisionArticulo & " and item=" & pItem
            objDT = objConn.Query(sql)

            For Each objDR In objDT.Rows
                CodigoUrdimbre = objDR("codigo_urdimbre")
                RevisionUrdimbre = objDR("revision_urdimbre")
                CodigoArticulo = objDR("codigo_articulo")
                RevisionArticulo = objDR("revision_articulo")
                Item = objDR("item")
                Tipo = objDR("tipo")
                TipoOrillo = objDR("tipo_orillo")
            Next
        End Sub

        Public Function List(ByVal pCodigoPieza As String) As DataTable
            Dim objConn As New NM_Consulta
            Dim sql As String
            Dim DT As New DataTable
            sql = "Select TUD.codigo_hilo, TUD.tipo, TUD.tipo_orillo, " & _
            " case when TUD.tipo = 1 then 'TEJIDO' ELSE 'ORILLO' end as descripcion_tipo " & _
            " from NM_PIEZA P, NM_TELAURDIMBRE TU, NM_TELAURDIMBRED TUD " & _
            " WHERE TU.codigo_articulo = P.codigo_articulo " & _
            " and TU.revision_articulo = P.revision_articulo " & _
            " and TUD.codigo_articulo = TU.codigo_articulo " & _
            " and TUD.revision_articulo = TU.revision_articulo " & _
            " and TUD.codigo_urdimbre = TU.codigo_urdimbre " & _
            " and TUD.revision_urdimbre = TU.revision_urdimbre " & _
            " and P.codigo_pieza like '" & pCodigoPieza & "'"
            DT = objConn.Query(sql)
            Return DT
        End Function

        'Public Function ListByType(ByVal pCodigoUrdimbre As String, ByVal pCodigoArticulo As String, ByVal pTipo As String) As DataTable
        '    Dim objConn As New NM_Consulta
        '    Dim sql As String
        '    Dim DT As New DataTable
        '    Dim DR As DataRow
        '    Dim condicion As String
        '    If UCase(pTipo) = "TEJIDO" Then
        '        condicion = "AND UD.tipo = 1"
        '    End If
        '    If UCase(pTipo) = "ORILLO" Then
        '        condicion = "AND UD.tipo <> 1"
        '    End If
        '    sql = "Select TOR.descripcion_tipo,UD.item, TUD.codigo_articulo, TUD.codigo_hilo, UD.tipo, " & _
        '    " case when TUD.tipo_orillo is null then 0 else TUD.tipo_orillo end as tipo_orillo, UD.numero_hilos " & _
        '        "from NM_MA_Urdimbre U, NM_MA_TelaUrdimbre TU, NM_MA_UrdimbreDetalle UD, " & _
        '        " NM_TipoOrillo TOR, NM_MA_TelaUrdimbreD TUD " & _
        '        "where U.codigo_urdimbre = TU.codigo_urdimbre " & _
        '        " and TU.codigo_articulo = TUD.codigo_articulo " & _
        '        " and TU.codigo_urdimbre= TUD.codigo_urdimbre " & _
        '        " and U.codigo_urdimbre = UD.codigo_urdimbre " & _
        '        " and UD.tipo = TOR.codigo_tipo " & _
        '        " and TUD.codigo_urdimbre = UD.codigo_urdimbre " & _
        '        " and TUD.codigo_hilo = UD.codigo_hilo " & _
        '        " and TUD.item = UD.item and TUD.tipo = UD.tipo " & _
        '        " and UD.codigo_urdimbre='" & pCodigoUrdimbre & "' " & _
        '        " and TUD.codigo_articulo ='" & pCodigoArticulo & "' " & condicion
        '    DT = objConn.Query(sql)
        '    Return DT
        'End Function

        'Public Function Delete(ByVal pCodigoUrdimbre As String, ByVal pCodigoArticulo As String, _
        'ByVal pTipo As String, ByVal pItem As String) As Boolean
        '    Dim objConn As New NM_Consulta
        '    Dim sql As String
        '    Try
        '        sql = "Delete from NM_MA_TelarUrdimbreD where codigo_urdimbre='" & _
        '        pCodigoUrdimbre & "' and item = " & pItem & _
        '        " and codigo_articulo = '" & pCodigoArticulo & "' and tipo ='" & pTipo & "' "
        '        Return objConn.Execute(sql)
        '    Catch ex As Exception
        '        Throw ex
        '        Return False
        '    End Try
        'End Function

        'Public Function delete(ByVal pCodigoUrdimbre As String, ByVal pCodigoArticulo As String) As Boolean
        '    Dim objConn As New NM_Consulta
        '    Dim sql As String
        '    Try
        '        sql = "delete from NM_MA_TelaUrdimbreD where codigo_urdimbre='" & _
        '        pCodigoUrdimbre & "' and codigo_articulo ='" & pCodigoArticulo & "' "
        '        Return objConn.Execute(sql)
        '    Catch ex As Exception
        '        Throw ex
        '        Return False
        '    End Try
        'End Function

        'Public Function delete(ByVal pCodigoArticulo As String) As Boolean
        '    Dim objConn As New NM_Consulta
        '    Dim sql As String
        '    Try
        '        sql = "delete from NM_MA_TelaUrdimbreD where codigo_articulo ='" & pCodigoArticulo & "' "
        '        Return objConn.Execute(sql)
        '    Catch ex As Exception
        '        Throw ex
        '        Return False
        '    End Try
        'End Function

        'Public Function update() As Boolean
        '    Dim objConn As New NM_Consulta
        '    Dim sql As String
        '    Dim DT As New DataTable
        '    Dim DR As DataRow
        '    Try
        '        sql = "Update NM_MA_TelaUrdimbreD SET " & _
        '        " tipo_orillo = " & TipoOrillo & _
        '        " where codigo_urdimbre='" & CodigoUrdimbre & "' and tipo = 0 " & _
        '        " and item=" & Item & " and codigo_articulo ='" & CodigoArticulo & _
        '        "' and codigo_hilo ='" & CodigoHilo & "' "
        '        Return objConn.Execute(sql)
        '    Catch ex As Exception
        '        Throw ex
        '        Return False
        '    End Try
        'End Function

        'Public Function Add() As Boolean
        '    Dim objConn As New NM_Consulta
        '    Dim sql As String
        '    Try
        '        sql = "INSERT INTO NM_MA_UrdimbreDetalle (codigo_urdimbre, " & _
        '        " codigo_articulo, tipo_orillo, item , tipo,codigo_hilo, usuario_creacion," & _
        '        "fecha_creacion) VALUES ('" & _
        '        CodigoUrdimbre & "','" & CodigoArticulo & "','" & TipoOrillo & "', " & _
        '        Item & "," & Tipo & ",'" & CodigoHilo & "'," & Usuario & ", getdate())"
        '        Return objConn.Execute(sql)
        '    Catch ex As Exception
        '        Return False
        '    End Try
        'End Function


    End Class

End Namespace