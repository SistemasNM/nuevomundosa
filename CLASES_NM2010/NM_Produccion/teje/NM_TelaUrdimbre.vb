Imports NM_General.NM_BaseDatos

Namespace NM_Tejeduria

    Public Class NM_TelaUrdimbre

        Public codigo_tela As String
        Public revision_tela As Integer
        Public codigo_urdimbre As String
        Public revision_urdimbre As String
        Const TIPO As Integer = 0       ' 0: Tipo Orillo
        Public codigo_hilo As String
        Public tipo_orillo As String


        Public Function List(ByVal codigoUrdimbre As String, ByVal revisionUrdimbre As Integer, ByVal codigoTela As String, ByVal revisionTela As Integer) As DataTable
            Dim objGen As New NM_Consulta()

            Dim commandString As New System.Text.StringBuilder()
            commandString.Append("Select ud.*, tu.tipo_orillo ")
            commandString.Append("from nm_urdimbreDetalle ud ")
            commandString.Append("LEFT JOIN NM_TelaUrdimbre tu ")
            commandString.Append("ON ud.codigo_urdimbre = tu.codigo_urdimbre ")
            commandString.Append("AND ud.revision_urdimbre = tu.revision_urdimbre ")
            commandString.Append("AND ud.tipo = tu.tipo ")
            commandString.Append("AND ud.codigo_hilo = tu.codigo_hilo ")
            commandString.Append("AND tu.codigo_tela = '" & codigoTela & "'")
            commandString.Append("AND tu.revision_tela = " & revisionTela & " ")
            commandString.Append("where ud.codigo_urdimbre = '" & codigoUrdimbre & "' ")
            commandString.Append("AND ud.revision_urdimbre = " & revisionUrdimbre & " ")
            commandString.Append("AND ud.tipo = " & TIPO)

            Return objGen.Query(commandString.ToString)
        End Function

        Function Exist() As Boolean
        End Function


        Function Insert() As Boolean
            Dim objGen As New NM_Consulta()

            Dim commandString As New System.Text.StringBuilder()
            commandString.Append("INSERT INTO NM_TelaUrdimbre ")
            commandString.Append("(codigo_tela, revision_tela, codigo_urdimbre, ")
            commandString.Append("revision_urdimbre, tipo, codigo_hilo, tipo_orillo)")
            commandString.Append("VALUES (")
            commandString.Append("'" & codigo_tela & "', ")
            commandString.Append(revision_tela & ", ")
            commandString.Append("'" & codigo_urdimbre & "', ")
            commandString.Append(revision_urdimbre & ", ")
            commandString.Append(TIPO & ", ")
            commandString.Append("'" & codigo_hilo & "', ")
            commandString.Append("'" & tipo_orillo & "')")

            objGen.Query(commandString.ToString)
        End Function


        Function Update() As Boolean
            Dim objGen As New NM_Consulta()

            Dim commandString As New System.Text.StringBuilder()
            commandString.Append("UPDATE NM_TelaUrdimbre SET ")
            commandString.Append("tipo_orillo = '" & tipo_orillo & "' ")
            commandString.Append("WHERE codigo_tela = '" & codigo_tela & "' ")
            commandString.Append("AND revision_tela = " & revision_tela & " ")
            commandString.Append("AND codigo_urdimbre = '" & codigo_urdimbre & "' ")
            commandString.Append("AND revision_urdimbre = " & revision_urdimbre & " ")
            commandString.Append("AND tipo = " & TIPO)
            commandString.Append("AND codigo_hilo = '" & codigo_hilo & "'")

            objGen.Query(commandString.ToString)
        End Function


        Sub Seek(ByVal codigoTela As String, _
                 ByVal revisionTela As Integer, _
                 ByVal codigoUrdimbre As String, _
                 ByVal revisionUrdimbre As Integer, _
                 ByVal codigoHilo As String)

            Dim bd As New NM_Consulta()
            Dim objDT As New DataTable()
            Dim objDR As DataRow
            Dim commandString As New System.Text.StringBuilder()

            commandString.Append("SELECT * ")
            commandString.Append("FROM NM_TelaUrdimbre ")
            commandString.Append("WHERE codigo_tela = '" & codigoTela & "' ")
            commandString.Append("AND revision_tela = " & revisionTela & " ")
            commandString.Append("AND codigo_urdimbre = '" & codigoUrdimbre & "' ")
            commandString.Append("AND revision_urdimbre = " & revisionUrdimbre & " ")
            commandString.Append("AND tipo = " & TIPO)
            commandString.Append("AND codigo_hilo = '" & codigoHilo & "'")

            objDT = bd.Query(commandString.ToString)

            For Each objDR In objDT.Rows
                codigo_tela = objDR("codigo_tela")
                revision_tela = objDR("revision_tela")
                codigo_urdimbre = objDR("codigo_urdimbre")
                revision_urdimbre = objDR("revision_urdimbre")
                codigo_hilo = objDR("codigo_hilo")
                tipo_orillo = objDR("tipo_orillo")
            Next
        End Sub


    End Class

End Namespace