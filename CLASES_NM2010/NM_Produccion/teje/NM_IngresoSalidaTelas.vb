Imports NM_General.NM_BaseDatos
Namespace NM_Tejeduria

    Public Class NM_IngresoSalidaTelas
        Public codigo_pieza As String
        Public fecha_entrega As Date
        Public hora_entrega As String
        Public codigo_articulo As String
        Public codigo_color As String
        Public codigo_combinacion As String
        Public codigo_diseno As String
        Public esultima_ficha As String
        Public codigo_articulo_largo As String
        Public revision_articulo As Integer
        Public codigo_telar As String
        Public revision_telar As Integer
        Public orden_produccion As String
        Public metraje As Double
        Public numero_caballete As String
        Public usuario As String
        Private objUtil As New NM_General.Util

        Sub New()
            codigo_pieza = ""
            fecha_entrega = Date.Today
            codigo_color = ""
            codigo_combinacion = ""
            hora_entrega = ""
            codigo_articulo = ""
            codigo_diseno = ""
            esultima_ficha = "0"
            codigo_articulo_largo = ""
            codigo_telar = ""
            orden_produccion = ""
            metraje = 0
            numero_caballete = ""
            usuario = ""
        End Sub

        Public Function insertar() As Boolean
            Dim objConn As New NM_Consulta
            Dim strsql As String
            numero_caballete = getNumFicha()
            strsql = "INSERT INTO NM_IngresoSalidaTelas" & _
            "(codigo_pieza,fecha_entrega, hora_entrega, codigo_articulo,revision_articulo, " & _
            "codigo_color, codigo_combinacion, codigo_diseno, esultima_ficha, codigo_articulo_largo, " & _
            "codigo_telar, revision_telar, orden_produccion, metraje, numero_caballete, " & _
            " usuario_creacion, fecha_creacion) values(" & _
            "'" & codigo_pieza & "',convert(datetime, '" & objUtil.FormatFecha(fecha_entrega) & "')," & _
            "'" & hora_entrega & "','" & codigo_articulo & "'," & _
            "" & revision_articulo & ",'" & codigo_color & "','" & Me.codigo_combinacion & "', '" & _
            codigo_diseno & "', '" & esultima_ficha & "', '" & codigo_articulo_largo & "','" & codigo_telar & "'," & _
            "'" & revision_telar & "','" & orden_produccion & "'," & _
            metraje & ",'" & numero_caballete & "','" & usuario & "',getdate())"
            Return objConn.Execute(strsql)
        End Function

        Public Function update() As Boolean
            Dim objConn As New NM_Consulta
            Try
                If codigo_pieza <> "" Then
                    Dim strsql As String
                    strsql = "UPDATE NM_IngresoSalidaTelas SET " & _
                    " fecha_entrega = convert(datetime, '" & objUtil.FormatFecha(fecha_entrega) & "')," & _
                    "hora_entrega = '" & hora_entrega & "'," & _
                    "codigo_articulo = '" & codigo_articulo & "'," & _
                    "codigo_telar = '" & codigo_telar & "'," & _
                    "codigo_color = '" & codigo_color & "', " & _
                    "codigo_combinacion = '" & codigo_combinacion & "', " & _
                    "codigo_diseno = '" & codigo_diseno & "', " & _
                    "esultima_ficha = '" & esultima_ficha & "', " & _
                    " codigo_articulo_largo = '" & codigo_articulo_largo & "' " & _
                    "orden_produccion = '" & orden_produccion & "'," & _
                    "metraje = " & metraje & "," & _
                    "usuario_modificacion = '" & usuario & "'," & _
                    "fecha_modificacion = getdate()" & _
                    " where codigo_pieza = '" & codigo_pieza & "'"
                    Return objConn.Execute(strsql)
                Else
                    Return False
                End If
            Catch
                Return False
            End Try
        End Function

        Private Function getNumFicha() As String
            Dim tabla As New DataTable
            Dim fila As DataRow
            tabla = getlastFicha()
            For Each fila In tabla.Rows
                If IsDBNull(fila("caballete")) Then
                    Return "400000"
                Else
                    Return CStr(CInt(fila("caballete")) + 1)
                End If
                Exit For
            Next
        End Function

        Public Function Exist(ByVal pcodigo_pieza As String) As Boolean
            Dim strsql As String
            Dim Db As New NM_Consulta
            Dim tabla As New DataTable
            strsql = "SELECT * FROM NM_IngresoSalidaTelas where codigo_pieza = '" & pcodigo_pieza & "'"
            tabla = Db.Query(strsql)
            Return (tabla.Rows.Count > 0)
        End Function

        Public Sub SeekXCodPieza(ByVal pcodigo_pieza As String)
            Dim strsql As String
            Dim objConn As New NM_Consulta
            Dim tabla As New DataTable
            Dim fila As DataRow
            strsql = "SELECT * FROM NM_IngresoSalidaTelas where codigo_pieza = '" & pcodigo_pieza & "'"
            tabla = objConn.Query(strsql)
            For Each fila In tabla.Rows
                If Not IsDBNull(fila("codigo_pieza")) Then codigo_pieza = fila("codigo_pieza")
                If Not IsDBNull(fila("fecha_entrega")) Then fecha_entrega = fila("fecha_entrega")
                If Not IsDBNull(fila("hora_entrega")) Then hora_entrega = fila("hora_entrega")
                If Not IsDBNull(fila("codigo_articulo")) Then codigo_articulo = fila("codigo_articulo")
                If Not IsDBNull(fila("codigo_telar")) Then codigo_telar = fila("codigo_telar")
                If Not IsDBNull(fila("orden_produccion")) Then orden_produccion = fila("orden_produccion")
                If Not IsDBNull(fila("metraje")) Then metraje = fila("metraje")
                If Not IsDBNull(fila("numero_caballete")) Then numero_caballete = fila("numero_caballete")
                If Not IsDBNull(fila("codigo_color")) Then codigo_color = fila("codigo_color")
                If Not IsDBNull(fila("codigo_combinacion")) Then codigo_combinacion = fila("codigo_combinacion")
                If Not IsDBNull(fila("codigo_diseno")) Then codigo_diseno = fila("codigo_diseno")
                If Not IsDBNull(fila("esultima_ficha")) Then esultima_ficha = fila("esultima_ficha")
                If Not IsDBNull(fila("codigo_articulo_largo")) Then codigo_articulo_largo = fila("codigo_articulo_largo")
            Next
        End Sub

        Public Function listar() As DataTable
            Dim objConn As New NM_Consulta
            Return objConn.Query("Select * from NM_IngresoSalidaTelas order by 8, 10") ' ordena por el numero de ficha y por la fecha de creacion
        End Function

        Private Function getlastFicha() As DataTable ' Obtiene el ultimo numero de caballete ingresado. caballete=ficha
            Dim objConn As New NM_Consulta
            Dim strSql As String
            strSql = "SELECT MAX(convert(integer, numero_caballete)) Caballete FROM NM_IngresoSalidaTelas where numero_caballete like '4%' "
            Return objConn.Query(strSql) ' ordena por el numero de ficha y por la fecha de creacion
        End Function

    End Class
End Namespace