Imports NM_General.NM_BaseDatos

Namespace NM_Tejeduria
    Public Class NM_ControlHilosTransitoD
        Public Fecha As Date
        Public CodigoHilo As String
        Public CentroCosto As String
        Public ParoPromedio As Double
        Public NumeroConos As Integer
        Public Usuario As String
        Private objUtil As New NM_General.Util

        Sub New()
            Fecha = Date.Today
            CodigoHilo = ""
            CentroCosto = ""
            ParoPromedio = 0
            NumeroConos = 0
            Usuario = ""
        End Sub

        Function Add() As Boolean
            Dim sql As String, objConn As New NM_Consulta
            Try
                sql = "Insert into NM_ControlHilosTransitoD(" & _
                "fecha, centro_costo, codigo_hilo, paro_promedio, " & _
                " numero_conos, usuario_creacion," & _
                "fecha_creacion) values(convert(datetime,'" & _
                objUtil.FormatFecha(Fecha) & "'),'" & CentroCosto & _
                "', '" & CodigoHilo & "'," & ParoPromedio & "," & _
                NumeroConos & ",'" & Usuario & "',getdate()) "
                Return objConn.Execute(sql)
            Catch ex As Exception
                Throw ex
                Return False
            End Try
        End Function

        Function Update() As Boolean
            Dim sql As String, objConn As New NM_Consulta
            Try
                sql = "Update NM_ControlHilosTransitoD set paro_promedio=" & _
                ParoPromedio & ", numero_conos=" & NumeroConos & ", " & _
                "usuario_modificacion='" & Usuario & "', fecha_modificacion=getdate() " & _
                " where DATEDIFF(DD, fecha, '" & objUtil.FormatFecha(Fecha) & "') = 0 and codigo_hilo='" & CodigoHilo & "' and centro_costo = '" & _
                CentroCosto & "' "
                Return objConn.Execute(sql)
            Catch ex As Exception
                Throw ex
                Return False
            End Try
        End Function

        Function Delete(ByVal pFecha As Date, ByVal pCodHilo As String, ByVal pCentroCosto As String) As Boolean
            Dim sql As String, objConn As New NM_Consulta
            Try
                sql = "Delete from NM_ControlHilosTransitoD " & _
                " where DATEDIFF(DD, fecha, '" & objUtil.FormatFecha(pFecha) & "') = 0 and codigo_hilo='" & pCodHilo & _
                "' and centro_costo = '" & pCentroCosto & "' "
                Return objConn.Execute(sql)
            Catch
                Return False
            End Try
        End Function

        Function Exist(ByVal pFecha As DateTime, ByVal pCodHilo As String, ByVal pCentroCosto As String) As Boolean
            Dim sql As String, objConn As New NM_Consulta
            Dim dt As New DataTable
            Try
                sql = "Select * from NM_ControlHilosTransitoD " & _
                "where DATEDIFF(DD, fecha, '" & objUtil.FormatFecha(pFecha) & "') = 0 and codigo_hilo='" & _
                pCodHilo & "' and centro_costo = '" & pCentroCosto & "' "
                dt = objConn.Query(sql)
                Return (dt.Rows.Count > 0)
            Catch ex As Exception
                Throw ex
                Return False
            End Try
        End Function

        Function List() As DataTable
            Dim sql As String, objConn As New NM_Consulta
            Dim dt As New DataTable
            sql = "Select * from NM_ControlHilosTransitoD "
            dt = objConn.Query(sql)
            Return dt
        End Function

        Function List(ByVal pFecha As Date, ByVal pCentroCosto As String) As DataTable
            Dim sql As String, objConn As New NM_Consulta
            Dim dt As New DataTable
            sql = "Select * from NM_ControlHilosTransitoD " & _
            "where DATEDIFF(DD, fecha, '" & objUtil.FormatFecha(pFecha) & "') = 0 and centro_costo = '" & _
            pCentroCosto & "' order by fecha_creacion"
            dt = objConn.Query(sql)
            Return dt
        End Function

        Sub Seek(ByVal pFecha As Date, ByVal pCodigoHilo As String, ByVal pCentroCosto As String)
            Dim sql As String, objConn As New NM_Consulta
            Dim dt As New DataTable, fila As DataRow
            sql = "Select * from NM_ControlHilosTransitoD " & _
            "where DATEDIFF(DD, fecha, '" & objUtil.FormatFecha(pFecha) & "') = 0 and codigo_hilo='" & _
            pCodigoHilo & "' and centro_costo = '" & pCentroCosto & "' "
            dt = objConn.Query(sql)
            For Each fila In dt.Rows
                Fecha = fila("fecha")
                CodigoHilo = fila("codigo_hilo")
                CentroCosto = fila("centro_costo")
                ParoPromedio = fila("paro_promedio")
                NumeroConos = fila("numero_conos")
            Next
        End Sub

    End Class

End Namespace