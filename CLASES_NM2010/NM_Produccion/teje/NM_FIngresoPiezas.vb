Imports NM_General.NM_BaseDatos
Imports System.Data.SqlClient
Namespace NM_Tejeduria
    Public Class NM_FIngresoPiezas
        Public Function FN_CabFormatoIngPiezas(ByVal intTipo As Integer, ByVal pstrOrden As String) As DataTable
            Dim objDT As New DataTable
            Dim objGen As New NM_Consulta
            Dim strSQL As String
            If intTipo = 1 Then
                strSQL = "SP_CabFichaIngresoPiezasIST '" & pstrOrden & "'" 'Ingreso y Salida de telas
            Else
                strSQL = "SP_CabFichaIngresoPiezasPR '" & pstrOrden & "'" ' Planillas de Revisión
            End If
            objDT = objGen.Query(strSQL)
            Return objDT
        End Function
        Public Function FN_DetFormatoIngPiezas(ByVal intTipo As Integer, ByVal pstrOrden As String) As DataTable
            Dim objDT As New DataTable
            Dim objGen As New NM_Consulta
            Dim strSQL As String

            If intTipo = 1 Then
                strSQL = "SP_DetFichaIngresoPiezasIST '" & pstrOrden & "'" ' Ingreso y Salida de telas
            Else
                strSQL = "SP_DetFichaIngresoPiezasPR '" & pstrOrden & "'" ' Planillas de Revisión
            End If
            objDT = objGen.Query(strSQL)
            Return objDT
        End Function
    End Class
End Namespace