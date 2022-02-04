Imports NM_General.NM_BaseDatos
Namespace NM_Tejeduria
    Public Class NM_InventarioConoReusoProc
        Public Fecha As Date
        Public CodigoHilo As String
        Public CentroCosto As String
        Public CantidadConos As Integer
        Public PesoMuestreado As Double
        Public Usuario As String
        Private objUtil As New NM_General.Util

        Public Function Add() As Boolean
            Dim DB As New NM_Consulta
            Dim strsql As String
            strsql = "INSERT INTO NM_InventarioConoReusoProc " & _
            " (fecha, centro_costo, codigo_hilo, cantidad_conos, peso_muestreado, " & _
            " usuario_creacion, fecha_creacion) values(" & _
            "convert(datetime, '" & objUtil.FormatFecha(Fecha) & "'), " & _
            "'" & CentroCosto & "', '" & CodigoHilo & "', " & CantidadConos & "," & _
            PesoMuestreado & ", '" & Usuario & "', GetDate())"

            Return DB.Execute(strsql)
        End Function

        Public Function Update() As Boolean
            Dim DB As New NM_Consulta
            Dim strsql As String
            strsql = "UPDATE NM_InventarioConoReusoProc SET " & _
            "cantidad_conos = " & CantidadConos & "," & _
            "peso_muestreado = " & PesoMuestreado & "," & _
            "usuario_modificacion = '" & Usuario & "'," & _
            "fecha_modificacion = getdate()" & _
            " where DATEDIFF(DD, fecha, '" & objUtil.FormatFecha(Fecha) & "') = 0 and" & _
            " codigo_hilo = '" & CodigoHilo & "' and centro_costo ='" & CentroCosto & "' "
            Return DB.Execute(strsql)
        End Function

        Public Function delete(ByVal pfecha As Date, ByVal pCentroCosto As String, ByVal pcodigo_hilo As String) As Integer
            Dim strsql As String
            Dim DB As New NM_Consulta
            strsql = "DELETE FROM NM_InventarioConoReusoProc " & _
            " where DATEDIFF(DD, fecha, '" & objUtil.FormatFecha(pfecha) & "') = 0 and codigo_hilo = '" & _
            pcodigo_hilo & "' and centro_costo = '" & pCentroCosto & "' "
            Return DB.Execute(strsql)
        End Function

        Public Function List(ByVal pFecha As Date, ByVal pCentroCosto As String)
            Dim bd As New NM_Consulta, dt As New DataTable
            Dim sql = "SELECT * FROM NM_InventarioConoReusoProc " & _
            " where DATEDIFF(DD, fecha, '" & objUtil.FormatFecha(pFecha) & "') = 0 and centro_costo = '" & _
            pCentroCosto & "' order by fecha_creacion"
            dt = bd.Query(sql)
            Return dt
        End Function
    End Class
End Namespace
