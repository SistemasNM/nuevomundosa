Namespace NM_Tejeduria
    Public Class NM_Escuadra_Mecanico_Telar
        Inherits NM_Escuadra

        Sub New()
            MyBase.New("NM_Escuadra_Mecanico_Telar")
        End Sub

        Public Sub seek(ByVal scodigo_escuadra As String)
            Dim strsql As String
            Dim tabla As New DataTable()
            Dim fila As DataRow
            Dim commandString As New System.Text.StringBuilder()
            commandString.Append("Select  * from  " & Nombre_Tabla)
            commandString.Append(" where codigo_escuadra_mecanico = '" & scodigo_escuadra & "'")
            tabla = DB.Query(commandString.ToString)
            For Each fila In tabla.Rows
                codigo_escuadra = fila("codigo_escuadra_mecanico")
                codigo_trabajador = fila("codigo_mecanico")
                fecha_inicio = fila("fecha_inicio")
                planta = fila("planta")
                If IsDBNull(fila("fecha_final")) = False Then fecha_final = fila("fecha_final")
            Next
            Dim Escuadra_Detail As New NM_Escuadra_Mecanico_TelarD()
            Detalle = Escuadra_Detail.list(scodigo_escuadra)
        End Sub

    End Class
End Namespace