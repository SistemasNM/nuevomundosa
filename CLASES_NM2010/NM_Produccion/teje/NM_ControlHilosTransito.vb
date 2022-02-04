Imports NM_General.NM_BaseDatos

Namespace NM_Tejeduria

    Public Class NM_ControlHilosTransito
        Public Fecha As Date
        Public CentroCosto As String
        Public Responsable As String
        Public Usuario As String
        Public dtDetalle As DataTable
        Private objUtil As New NM_General.Util

        Sub New()
            Fecha = Date.Today.Date
            CentroCosto = ""
            Responsable = ""
            Usuario = ""
        End Sub

        Function Add() As Boolean
            Dim sql As String, objConn As New NM_Consulta
            Try
                sql = "Insert into NM_ControlHilosTransito (fecha, " & _
                "centro_costo, responsable, usuario_creacion, fecha_creacion) " & _
                " values(convert(datetime, '" & _
                objUtil.FormatFecha(Fecha) & "'), '" & _
                CentroCosto & "','" & Responsable & "','" & Usuario & "',getdate())"

                Return objConn.Execute(sql)
            Catch ex As Exception
                Throw ex
                Return False
            End Try
        End Function

        Function Update() As Boolean
            Dim sql As String, objConn As New NM_Consulta
            Try
                sql = "Update NM_ControlHilosTransito SET  responsable='" & Responsable & _
                "', usuario_modificacion='" & Usuario & "', fecha_modificacion=getdate() " & _
                " where DATEDIFF(DD, fecha, '" & objUtil.FormatFecha(Fecha) & "') = 0 and centro_costo ='" & CentroCosto & "' "
                Return objConn.Execute(sql)
            Catch ex As Exception
                Throw ex
                Return False
            End Try
        End Function

        Function Delete(ByVal pFecha As Date, ByVal pCentroCosto As String) As Boolean
            Dim sql As String, objConn As New NM_Consulta
            Try
                sql = "Delete from NM_ControlHilosTransito " & _
                " where DATEDIFF(DD, fecha, '" & objUtil.FormatFecha(pFecha) & "') = 0 and centro_costo ='" & _
                pCentroCosto & "' "
                Return objConn.Execute(sql)
            Catch ex As Exception
                Throw ex
                Return False
            End Try
        End Function

        Function List() As DataTable
            Dim sql As String, dt As New DataTable, objConn As New NM_Consulta
            sql = "Select * from NM_ControlHilosTransito order by fecha "
            dt = objConn.Query(sql)
            Return dt
        End Function

        Sub Seek(ByVal dFecha As Date, ByVal pCentroCosto As String)
            Dim sql As String, dt As New DataTable, objConn As New NM_Consulta
            Dim fila As DataRow
            sql = "Select * from NM_ControlHilosTransito " & _
            " where DATEDIFF(DD, fecha, '" & objUtil.FormatFecha(dFecha) & "') = 0 and centro_costo = '" & _
            pCentroCosto & "' "
            dt = objConn.Query(sql)
            If dt.Rows.Count > 0 Then
                For Each fila In dt.Rows
                    Fecha = fila("fecha")
                    Responsable = fila("responsable")
                    CentroCosto = fila("centro_costo")
                Next
                Dim detailControlHilo As New NM_ControlHilosTransitoD
                dtDetalle = detailControlHilo.List(dFecha, pCentroCosto)
            End If
        End Sub

        Function Exist(ByVal pFecha As Date, ByVal pCentroCosto As String) As Boolean
            Dim sql As String, objConn As New NM_Consulta
            Dim dt As New DataTable
            Try
                sql = "Select * from NM_ControlHilosTransito " & _
                " where DATEDIFF(DD, fecha, '" & objUtil.FormatFecha(pFecha) & "') = 0 and centro_costo = '" & _
                pCentroCosto & "' "
                dt = objConn.Query(sql)
                Return (dt.Rows.Count > 0)
            Catch
                Return False
            End Try
        End Function

    End Class
End Namespace

