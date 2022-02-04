Imports NM_General.NM_BaseDatos

Namespace NM_Tejeduria

    Public Class NMM_TelaUrdimbre
        Public CodigoArticulo As String
        Public CodigoUrdimbre As String
        Public Encogimiento As String
        Public Usuario As String
        Public objTelaUrdimbreD As New NMM_TelaUrdimbreD

        Sub New()
            CodigoArticulo = ""
            CodigoUrdimbre = ""
            encogimiento = ""
            Usuario = ""
        End Sub

        'Public Function List(ByVal codigoUrdimbre As String, ByVal codigoTela As String) As DataTable
        '    Dim objGen As New NM_Consulta, sql As String
        '    sql = "Select ud.*, tu.tipo_orillo " & _
        '    "from nm_urdimbreDetalle ud " & _
        '    "LEFT JOIN NM_TelaUrdimbre tu " & _
        '    "ON ud.codigo_urdimbre = tu.codigo_urdimbre " & _
        '    "AND ud.tipo = tu.tipo " & _
        '    "AND ud.codigo_hilo = tu.codigo_hilo " & _
        '    "AND tu.codigo_articulo = '" & codigoTela & "'" & _
        '    "where ud.codigo_urdimbre = '" & codigoUrdimbre & "' " & _
        '    "AND ud.tipo = " & TIPO
        '    Return objGen.Query(sql)
        'End Function

        Public Function List(ByVal pCodigoArticulo As String) As DataTable
            Dim objConn As New NM_Consulta, sql As String
            sql = "Select * " & _
            "from NM_MA_TelaUrdimbre " & _
            "where codigo_articulo = '" & pCodigoArticulo & "'"
            Return objConn.Query(sql)
        End Function

        Function Exist(ByVal pCodigoArticulo As String, ByVal pCodigoUrdimbre As String) As Boolean
            Dim sql As String, objConn As New NM_Consulta, DT As New DataTable
            sql = "Select * from NM_MA_TelaUrdimbre where codigo_articulo ='" & _
            pCodigoArticulo & "' and codigo_urdimbre='" & pCodigoUrdimbre & "' "
            DT = objConn.Query(sql)
            Return (DT.Rows.Count > 0)
        End Function

        Function Delete(ByVal pCodigoArticulo As String, ByVal pCodigoUrdimbre As String) As Boolean
            Dim sql As String, objConn As New NM_Consulta, DT As New DataTable
            Try
                sql = "Delete from NM_MA_TelaUrdimbre where codigo_articulo ='" & _
                pCodigoArticulo & "' and codigo_urdimbre='" & pCodigoUrdimbre & "' "
                Return objConn.Execute(sql)
            Catch ex As Exception
                Return False
            End Try
        End Function

        Function Delete(ByVal pCodigoArticulo As String) As Boolean
            Dim sql As String, objConn As New NM_Consulta, DT As New DataTable
            Try
                sql = "Delete from NM_MA_TelaUrdimbre where codigo_articulo ='" & _
                pCodigoArticulo & "' "
                Return objConn.Execute(sql)
            Catch ex As Exception
                Return False
            End Try
        End Function

        Function Add() As Boolean
            Dim objConn As New NM_Consulta, sql As String
            Try
                sql = "INSERT INTO NM_MA_TelaUrdimbre " & _
                "(codigo_articulo, codigo_urdimbre, " & _
                "encogimiento_urdimbre, usuario_creacion, fecha_creacion)" & _
                "VALUES (" & _
                "'" & CodigoArticulo & "', " & _
                "'" & CodigoUrdimbre & "', " & Encogimiento & ", " & _
                "'" & Usuario & "', " & _
                "getdate())"
                Return objConn.Execute(sql)
            Catch ex As Exception
                Return False
            End Try
        End Function

        Function Update() As Boolean
            Dim objConn As New NM_Consulta, sql As String
            Try
                sql = "UPDATE NM_MA_TelaUrdimbre SET " & _
                "encogimiento_urdimbre = " & Encogimiento & ", usuario_modificacion='" & Usuario & _
                "', fecha_modificacion=getdate() " & _
                "WHERE codigo_articulo = '" & CodigoArticulo & "' " & _
                "AND codigo_urdimbre = '" & CodigoUrdimbre & "' "
                Return objConn.Execute(sql)
            Catch ex As Exception
                Return False
            End Try
        End Function

        Sub Seek(ByVal pCodigoTela As String, ByVal pCodigoUrdimbre As String, ByVal pCodigoHilo As String)
            Dim objConn As New NM_Consulta, sql As String
            Dim dt As New DataTable
            Dim dr As DataRow

            sql = "SELECT * " & _
            "FROM NM_MA_TelaUrdimbre " & _
            "WHERE codigo_articulo = '" & pCodigoTela & "' " & _
            "AND codigo_urdimbre = '" & pCodigoUrdimbre & "' "
            dt = objConn.Query(sql)
            For Each dr In dt.Rows
                CodigoArticulo = dr("codigo_articulo")
                CodigoUrdimbre = dr("codigo_urdimbre")
                Encogimiento = dr("encogimiento_urdimbre")
            Next
        End Sub

        Sub Seek(ByVal pCodigoTela As String)
            Dim objConn As New NM_Consulta, sql As String
            Dim dt As New DataTable
            Dim dr As DataRow

            sql = "SELECT * " & _
            "FROM NM_MA_TelaUrdimbre " & _
            "WHERE codigo_articulo = '" & pCodigoTela & "' "
            dt = objConn.Query(sql)
            For Each dr In dt.Rows
                CodigoArticulo = dr("codigo_articulo")
                CodigoUrdimbre = dr("codigo_urdimbre")
                If IsDBNull(dr("encogimiento_urdimbre")) = False Then Encogimiento = dr("encogimiento_urdimbre")
            Next
        End Sub

        Function CopyData(ByVal pCodigoArticulo As String, ByVal pUsuario As String) As Boolean
            Dim sql As String, objConn As New NM_Consulta
            sql = "Insert into NM_TelaUrdimbre (codigo_urdimbre, revision_urdimbre, " & _
            " codigo_articulo, revision_articulo, " & _
            " encogimiento_urdimbre, usuario_creacion, fecha_creacion ) " & _
            "(select U.codigo_urdimbre, U.revision_urdimbre, A.codigo_articulo, A.revision_articulo, " & _
            " TU.encogimiento_urdimbre, '" & pUsuario & "', getdate() " & _
            " from NM_MA_TelaUrdimbre TU, NM_MA_Articulo A, NM_MA_Urdimbre U " & _
            " where TU.codigo_articulo = A.codigo_articulo " & _
            " and TU.codigo_urdimbre = U.codigo_urdimbre " & _
            " and TU.codigo_articulo ='" & pCodigoArticulo & "') "
            Return objConn.Execute(sql)
        End Function
    End Class

End Namespace