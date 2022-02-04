Imports NM_General.NM_BaseDatos
Namespace NM_Tejeduria
    Public Class NM_InventarioConoNuevo

        Public Fecha As Date
        Public Codigo_Hilo As String
        Public Cantidad_Conos As Integer
        Public CentroCosto As String
        Public Usuario As String
        Private objUtil As New NM_General.Util

        Public Function Add() As Boolean
            Dim objConn As New NM_Consulta
            Dim strsql As String
            strsql = "INSERT INTO NM_InventarioConoNuevo " & _
            "(fecha, centro_costo, codigo_hilo, cantidad_conos, usuario_creacion, fecha_creacion) " & _
            " values(" & _
            "convert(datetime, '" & objUtil.FormatFecha(Fecha) & "')," & _
            "'" & CentroCosto & "', '" & Codigo_Hilo & "','" & Cantidad_Conos & "'," & _
            "'" & Usuario & "', GetDate())"
            Return objConn.Execute(strsql)
        End Function

        Public Function Update() As Boolean
            Dim objConn As New NM_Consulta
            Dim strsql As String
            strsql = "UPDATE NM_InventarioConoNuevo SET " & _
            "cantidad_conos = " & Cantidad_Conos & "," & _
            "usuario_modificacion = '" & Usuario & "'," & _
            "fecha_modificacion = getdate()" & _
            " where DATEDIFF(DD, fecha, '" & objUtil.FormatFecha(Fecha) & "') = 0 and" & _
            " codigo_hilo = '" & Codigo_Hilo & "' and " & _
            " centro_costo = '" & CentroCosto & "' "
            Return objConn.Execute(strsql)
        End Function

        Public Function delete(ByVal pfecha As Date, ByVal pCentroCosto As String, ByVal pcodigo_hilo As String) As Integer
            Dim strsql As String
            Dim DB As New NM_Consulta
            strsql = "DELETE FROM NM_InventarioConoNuevo " & _
            " where DATEDIFF(DD, fecha, '" & objUtil.FormatFecha(pfecha) & "') = 0 and codigo_hilo = '" & _
            pcodigo_hilo & "' and centro_costo = '" & pCentroCosto & "' "
            Return DB.Execute(strsql)
        End Function

        Public Function list(ByVal pfecha As Date, ByVal pCentroCosto As String)
            Dim bd As New NM_Consulta, dt As New DataTable
            Dim sql = "SELECT * FROM NM_InventarioConoNuevo " & _
            " where DATEDIFF(DD, fecha, '" & objUtil.FormatFecha(pfecha) & "') = 0 and centro_costo = '" & _
            pCentroCosto & "' order by fecha_creacion "
            dt = bd.Query(sql)
            Return dt
        End Function

    End Class
End Namespace


