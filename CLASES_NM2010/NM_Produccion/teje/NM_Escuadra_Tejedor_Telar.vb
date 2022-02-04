Imports NM_General.NM_BaseDatos
Namespace NM_Tejeduria
    Public Class NM_Escuadra_Tejedor_Telar
        Inherits NM_Escuadra

        Private objUtil As New NM_General.Util

        Sub New()
            MyBase.New("NM_Escuadra_Tejedor_Telar")
        End Sub

        Public Sub seek(ByVal scodigo_escuadra As String, ByVal sturno As String)
            Dim strsql As String
            Dim tabla As DataTable
            Dim fila As DataRow
            strsql = "Select top 1 * from " & Nombre_Tabla & _
            " where codigo_escuadra_tejedor = '" & scodigo_escuadra & "'" & _
            " and turno = " & sturno & " order by fecha_inicio desc"
            tabla = DB.Query(strsql)
            For Each fila In tabla.Rows
                codigo_escuadra = fila("codigo_escuadra_tejedor")
                codigo_trabajador = fila("codigo_tejedor")
                fecha_inicio = fila("fecha_inicio")
                planta = fila("codigo_planta")
                turno = fila("turno")
                If IsDBNull(fila("fecha_final")) = False Then fecha_final = fila("fecha_final")
            Next
            Dim Escuadra_Detail As New NM_Escuadra_Tejedor_TelarD
            Detalle = Escuadra_Detail.listturno(scodigo_escuadra, turno, fecha_inicio, planta, codigo_trabajador)
        End Sub

        Public Function Exist(ByVal scodigo_escuadra As String, ByVal sturno As String) As Boolean
            Dim strsql As String
            Dim tabla As DataTable
            Dim fila As DataRow
            strsql = "Select * from " & Nombre_Tabla & _
            " where codigo_escuadra_tejedor = '" & scodigo_escuadra & "'" & _
            " and turno = " & sturno
            tabla = DB.Query(strsql)
            Return tabla.Rows.Count > 0
        End Function

        Public Function Exist(ByVal scodigo_escuadra As String, ByVal sturno As String, ByVal pFecha As Date, ByVal pCodigoTejedor As String) As Boolean
            Dim strsql As String
            Dim tabla As DataTable
            Dim fila As DataRow
            strsql = "Select * from " & Nombre_Tabla & _
            " where codigo_escuadra_tejedor = '" & scodigo_escuadra & "'" & _
            " and turno = " & sturno & " and fecha_inicio='" & objUtil.FormatFecha(pFecha) & "' " & _
            "and codigo_tejedor='" & pCodigoTejedor & "'"
            tabla = DB.Query(strsql)
            Return tabla.Rows.Count > 0
        End Function

        Public Overloads Function Update() As Boolean
            Dim strsql As String, objConn As New NM_Consulta

            strsql = "UPDATE NM_Escuadra_Tejedor_Telar SET" & _
            " codigo_planta = '" & planta & "'," & _
            " codigo_tejedor = '" & codigo_trabajador & "'," & _
            " fecha_inicio = convert(datetime, '" & objUtil.FormatFecha(fecha_inicio) & "'), " & _
            " fecha_final  = convert(datetime, '" & objUtil.FormatFecha(fecha_final) & "'), " & _
            " usuario_modificacion = '" & usuario & "'," & _
            " fecha_modificacion = getdate()" & _
            " where codigo_escuadra_tejedor = '" & codigo_escuadra & "'" & _
            " and turno=" & turno & " and fecha_inicio='" & objUtil.FormatFecha(fecha_inicio) & "'"

            Try
                Return objConn.Execute(strsql)
            Catch ex As Exception

            End Try
        End Function

    End Class
End Namespace