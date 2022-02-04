Imports NM_General.NM_BaseDatos
Imports System.Data.SqlClient
Namespace NM_Tejeduria
    Public Class NM_FSalidaPiezas
        Public Function FN_CabFormatoSalPiezas(ByVal intTipo As Integer, ByVal pstrOrden As String, ByVal pArticulo As String, ByVal pintFicha As Integer) As DataTable
            Dim objDT As New DataTable
            Dim objGen As New NM_Consulta
            Dim strSQL As String
            If intTipo = 1 Then
                strSQL = "SP_CabFichaSalidaPiezasIST '" & pstrOrden & "','" & pArticulo & "'," & pintFicha
            Else
                strSQL = "SP_CabFichaSalidaPiezas '" & pstrOrden & "','" & pArticulo & "'," & pintFicha
            End If
            objDT = objGen.Query(strSQL)
            Return objDT
        End Function
        Public Function FN_DetFormatoSalPiezas(ByVal intTipo As Integer, ByVal pstrOrden As String, ByVal pArticulo As String, ByVal pintFicha As Integer) As DataTable
            Dim objDT As New DataTable
            Dim objGen As New NM_Consulta
            Dim strSQL As String

            If intTipo = 1 Then
                strSQL = "SP_DetFichaSalidaPiezasIST '" & pstrOrden & "','" & pArticulo & "'," & pintFicha
            Else
                strSQL = "SP_DetFichaSalidaPiezas '" & pstrOrden & "','" & pArticulo & "'," & pintFicha
            End If
            objDT = objGen.Query(strSQL)
            Return objDT
        End Function
        Public Function FN_ObtenerInformacionEngomado(ByVal pintFicha As Integer) As DataTable

            Dim objDT As New DataTable
            Dim objGen As New NM_Consulta
            Dim strSQL As String
            strSQL = "USP_REVCRUDOS_REPORTE_FICHA_TINTORERIA_ENGOMADO '" & pintFicha & "'"
            objDT = objGen.Query(strSQL)
            Return objDT
        End Function
        Public Function FN_ObtenerInformacionPiezas(ByVal pinArticulo As String) As DataTable

            Dim objDT As New DataTable
            Dim objGen As New NM_Consulta
            Dim strSQL As String
            strSQL = "USP_REVCRUDOS_REPORTE_FICHA_TINTORERIA_PIEZA '" & pinArticulo & "'"
            objDT = objGen.Query(strSQL)
            Return objDT
        End Function
    End Class
End Namespace