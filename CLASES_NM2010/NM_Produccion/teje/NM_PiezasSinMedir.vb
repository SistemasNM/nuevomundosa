Imports NM_General.NM_BaseDatos

Namespace NM_Tejeduria
    Public Class NM_PiezasSinMedir
        Public Fecha As DateTime
        Public Responsable As String
        Public CentroCosto As String
        Public Usuario As String
        Public dtDetalle As New DataTable
        Private objUtil As New NM_General.Util

        Sub New()
            Fecha = Date.Today.Date
            Responsable = ""
            CentroCosto = ""
        End Sub

        Sub Seek(ByVal pFecha As Date, ByVal pCentroCosto As String)
            Dim sql As String = "", objDT As New DataTable
            Dim objBD As New NM_Consulta, fila As DataRow
            sql = "Select * from NM_InvPiezasSinMedir " & _
            " where DATEDIFF(DD, fecha, '" & objUtil.FormatFecha(pFecha) & "') = 0 and centro_costo = '" & _
            pCentroCosto & "' "
            objDT = objBD.Query(sql)
            For Each fila In objDT.Rows
                Fecha = fila.Item("fecha")
                CentroCosto = fila("centro_costo")
                Responsable = fila.Item("persona_responsable")
            Next
        End Sub
        Function Exist(ByVal pFecha As Date, ByVal pCentroCosto As String) As Boolean
            Dim sql As String = "", objDT As New DataTable
            Dim objBD As New NM_Consulta, fila As DataRow
            sql = "Select * from NM_InvPiezasSinMedir " & _
            " where DATEDIFF(DD, fecha, '" & objUtil.FormatFecha(pFecha) & "') = 0 and centro_costo = '" & _
            pCentroCosto & "' "
            objDT = objBD.Query(sql)
            Return (objDT.Rows.Count > 0)
        End Function
        Function Add() As Boolean
            Dim sql As String, objConn As New NM_Consulta
            Try
                sql = "Insert into NM_InvPiezasSinMedir (" & _
                "fecha, persona_responsable, centro_costo)" & _
                " values('" & objUtil.FormatFecha(Fecha) & "','" & Responsable & "','" & CentroCosto & "')"
                Return objConn.Execute(sql)
            Catch ex As Exception
                Throw ex
                Return False
            End Try
        End Function
    End Class
End Namespace
