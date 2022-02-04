Imports NM_General.NM_BaseDatos

Namespace NM_Tejeduria



Public Class NM_PiezaParticion
        Public usuario As String
        Public pieza_origen As String
        Public metraje As Integer
        Public numero_pieza As Integer
        Public calidad As String


        Public Sub Insert(ByVal table As DataTable)
            Dim dt As DataTable = table.Clone()
            Dim bd As New NM_Consulta()
            Dim ds As DataSet
            ds = table.DataSet
            dt.TableName = "NM_PiezaParticion"
            bd.Insert(ds, "NM_PiezaParticion")

            bd = Nothing
            dt = Nothing
            ds = Nothing

        End Sub

        Public Sub Insert()
            Dim bd As New NM_Consulta()

            Dim sql = "INSERT INTO NM_PiezaParticion " & _
                "(pieza_origen, pieza, metraje, calidad, usuario_creacion, fecha_creacion) " & _
                "VALUES ('" & _
                pieza_origen & "', '" & _
                numero_pieza & "', " & _
                metraje & ", '" & _
                calidad & "','" & _
                usuario & "'," & _
                "GetDate())"
            bd.Execute(sql)
        End Sub

        Public Function Exist(ByVal pCodigoPieza As String)

            Dim objGen As New NM_Consulta
            Dim objDT As New DataTable
            Dim sql = "select * from nm_pieza where codigo_pieza='" & pCodigoPieza & "' and " & _
            "tipo<>'H' and tipo<>'P'"
            objDT = objGen.Query(sql)
            If objDT.Rows.Count > 0 Then
                Return True
            Else
                Return False
            End If
        End Function

        Public Sub Update()
            Dim bd As New NM_Consulta

            If Not numero_pieza = "" Then
                Dim strSQL = "UPDATE NM_PiezaParticion " & _
                    "SET " & _
                    "metraje = " & metraje & ", " & _
                    "calidad = '" & calidad & "', " & _
                    "usuario_modificacion = '" & usuario & "', " & _
                    "fecha_modificacion = GetDate() " & _
                    "WHERE pieza_origen = '" & pieza_origen & "' " & _
                    "AND pieza = '" & numero_pieza & "'"
                bd.Execute(strSQL)
            Else
                Throw New Exception("No se puede actualizar porque el código es incorrecto.")
            End If
        End Sub

        Public Sub Delete()
            Dim bd As New NM_Consulta

            If Not numero_pieza = "" Then
                Dim strSQL = "DELETE FROM NM_PiezaParticion " & _
                    "WHERE pieza_origen = '" & pieza_origen & "' " & _
                    "AND pieza = '" & numero_pieza & "' "
                bd.Execute(strSQL)
            Else
                Throw New Exception("No se puede eliminar porque el código no es válido.")
            End If

        End Sub

        Function LoadDT(ByVal codigoPieza As String) As DataTable
            Dim bd As New NM_Consulta
            Dim table As New DataTable

            Dim linea As DataRow
            Dim sql = "SELECT *, '' as codigo_orden FROM NM_PiezaParticion WHERE pieza_origen = '" & codigoPieza & "'"
            table = bd.Query(sql)
            If table.Rows.Count = 0 Then

                'Dim pieza As New NM_Pieza()
                'pieza.Seek(codigoPieza)

                'linea = table.NewRow()
                'linea("codigo_pieza") = codigoPieza
                'linea("pieza_origen") = codigoPieza
                'linea("particion") = "0"
                'linea("metraje") = pieza.metraje
                'linea("calidad") = ""
                'linea("usuario_creacion") = "devel02"
                'linea("fecha_creacion") = Date.Today

                'table.Rows.Add(linea)

            Else

            End If

            Return table

        End Function

    End Class


End Namespace