Imports NM_General.NM_BaseDatos

Namespace NM_Tejeduria

    Public Class NM_PiezasSinMedirDetalle
        Public Fecha As Date
        Public CentroCosto As String
        Public CodigoArticulo As String
        Public CodigoPieza As String
        Private objUtil As New NM_General.Util

        Function List(ByVal pFecha As Date, ByVal pCentroCosto As String) As DataTable
            Dim objConn As New NM_Consulta, sql As String = ""
            Dim dtDetalle As New DataTable
            sql = "Select * from NM_InvPiezasSinMedirDet " & _
            " where DATEDIFF(DD, fecha, '" & objUtil.FormatFecha(pFecha) & "') = 0 and centro_costo = '" & pCentroCosto & "' "
            dtDetalle = objConn.Query(sql)
            Return dtDetalle
        End Function

        Function DeleteAll(ByVal pFecha As Date, ByVal pCentroCosto As String) As DataTable
            Dim objConn As New NM_Consulta, sql As String = ""
            sql = "Delete from NM_InvPiezasSinMedirDet " & _
            " where DATEDIFF(DD, fecha, '" & objUtil.FormatFecha(pFecha) & "') = 0 and centro_costo = '" & pCentroCosto & "' "
            objConn.Execute(sql)
        End Function

        Function Add() As Boolean
            Dim sql As String, objConn As New NM_Consulta
            Try
                sql = "Insert into NM_InvPiezasSinMedirDet (" & _
                "fecha, centro_costo, codigo_pieza, codigo_articulo) values(" & _
                "convert(datetime, '" & objUtil.FormatFecha(Fecha) & "'), '" & CentroCosto & _
                "', '" & CodigoPieza & "','" & CodigoArticulo & "')"
                Return objConn.Execute(sql)
            Catch ex As Exception
                Throw ex
                Return False
            End Try
        End Function
    End Class
End Namespace
