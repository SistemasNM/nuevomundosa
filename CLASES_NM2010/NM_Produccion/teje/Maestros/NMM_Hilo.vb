Imports NM_General.NM_BaseDatos
Namespace NM_Tejeduria
    Public Class NMM_Hilo
        Friend objConn As New NM_Consulta
        Public Codigo As String
        Public Descripcion As String
        Public Titulo As Double
        Public NeReal As Double

        Sub New()
            Codigo = ""
            Titulo = 0
            Descripcion = ""
            NeReal = 0
        End Sub

        Public Function List() As DataTable
            Dim sql As String, objDT As New DataTable
            Dim objConn As New NM_Consulta(6)
            sql = "Select co_item as codigo_hilo, de_item as descripcion_hilo, " & _
            " convert(integer, left(co_item,4))/10 as titulo " & _
            " from  NM_Hilos "
            objDT = objConn.Query(sql)
            Return objDT
        End Function

        Public Function Seek(ByVal pCodigoHilo As String) As Boolean
            Dim sql As String, objDT As New DataTable, fila As DataRow
            Dim objConn As New NM_Consulta(6)
            sql = "Select * " & _
            " from NM_Hilos where CO_ITEM ='" & pCodigoHilo & "' "
            objDT = objConn.Query(sql)
            For Each fila In objDT.Rows
                Codigo = fila.Item("co_item")
                Titulo = Left(fila.Item("co_item"), 4) / 10
                Descripcion = fila.Item("de_item")
                NeReal = GetNeReal(pCodigoHilo)
            Next
            Return (objDT.Rows.Count > 0)
        End Function

        Public Function Exist(ByVal pCodigoHilo As String) As Boolean
            Dim sql As String, objDT As New DataTable, fila As DataRow
            Dim objConn As New NM_Consulta(6)
            sql = "Select * " & _
            " from NM_Hilos where CO_ITEM ='" & pCodigoHilo & "' "
            objDT = objConn.Query(sql)
            Return (objDT.Rows.Count > 0)
        End Function

        Function GetNeReal(ByVal pCodigoHilo As String) As Double
            Dim sql As String, objConn As New NM_Consulta(4)
            Dim dt As New DataTable, fila As DataRow
            Dim retorno As Double
            sql = "select top 1 estandar from NM_TestHilo " & _
            " where codigo_hilo='" & pCodigoHilo & "' " & _
            " and codigo_testdato = 'STD02' "
            dt = objConn.Query(sql)
            For Each fila In dt.Rows
                retorno = fila("estandar")
            Next
            Return retorno
        End Function

    End Class

End Namespace