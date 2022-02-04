Imports NM_General.NM_BaseDatos

Namespace NM_Tejeduria

	Public Class NM_PedidoMaterialesD
		Public CodigoPedido As String
		Public CodigoHilo As String
		Public ConosPedidos As Integer
		Public Destino As String
		Public ConosRecibidos As Integer
		Public Usuario As String
		Public Debug As String

		Sub New()
			CodigoPedido = ""
			CodigoHilo = ""
			ConosPedidos = 0
			ConosRecibidos = 0
		End Sub

		Sub New(ByVal sCodigoPedido As String, ByVal sCodigoHilo As String)
			Seek(sCodigoPedido, sCodigoHilo)
		End Sub

		Public Function Add() As Boolean
			Dim sql As String, objConn As New NM_Consulta()
			Try
				sql = "Insert into NM_PedidoMaterialesD(codigo_pedido, codigo_hilo," & _
				"numero_conos, numero_real_conos,destino, usuario_creacion,fecha_creacion) " & _
				" values('" & CodigoPedido & "','" & CodigoHilo & "'," & ConosPedidos & _
				"," & ConosRecibidos & ",'" & Destino & "','" & Usuario & "',getdate())"
				objConn.Execute(sql)
				Return True
			Catch
				Return False
			End Try
		End Function

		Public Function Update() As Boolean
			Dim sql As String, objConn As New NM_Consulta()
			Try
				sql = "Update NM_PedidoMaterialesD set numero_real_conos=" & ConosRecibidos & _
				" where codigo_pedido='" & CodigoPedido & "' and codigo_hilo='" & CodigoHilo & "'"
				objConn.Execute(sql)
				Return True
			Catch
				Return False
			End Try
		End Function

        Public Function Delete(ByVal sCodigoPedido As String, ByVal sCodigoHilo As String, _
        ByVal sDestino As String) As Boolean
            Try
                Dim sql As String, objConn As New NM_Consulta()
                sql = "Delete from NM_PedidoMaterialesD where codigo_pedido='" & _
                sCodigoPedido & "' and codigo_hilo='" & sCodigoHilo & _
                "' and destino='" & sDestino & "' "
                objConn.Execute(sql)
                Return True
            Catch
                Return False
            End Try
        End Function

        Sub Seek(ByVal sCodigoPedido As String, ByVal sCodigoHilo As String)
            Dim dt As New DataTable(), fila As DataRow
            Dim sql As String, objConn As New NM_Consulta()
            sql = "Select * from NM_PedidoMaterialesD where " & _
            " codigo_pedido='" & CodigoPedido & "' and codigo_hilo='" & CodigoHilo & "' "
            dt = objConn.Query(sql)
            For Each fila In dt.Rows
                CodigoPedido = fila.Item("codigo_pedido")
                CodigoHilo = fila.Item("codigo_hilo")
                ConosPedidos = fila.Item("numero_conos")
                ConosRecibidos = fila.Item("numero_real_conos")
            Next
        End Sub

        Public Function List() As DataTable
            Dim sql As String, objConn As New NM_Consulta()
            sql = "Select * from NM_PedidoMaterialesD "
            Return objConn.Query(sql)
        End Function

        Public Function List(ByVal sCodigoPedido As String) As DataTable
            Dim sql As String, objConn As New NM_Consulta()
            Dim dt As New DataTable()
            sql = "Select * from NM_PedidoMaterialesD where codigo_pedido='" & sCodigoPedido & "' "
            dt = objConn.Query(sql)
            Return dt
        End Function

        Public Function List(ByVal sCodigoPedido As String, ByVal bActivo As Boolean) As DataTable
            Dim sql As String, objConn As New NM_Consulta()
            Dim dt As New DataTable()
            sql = "Select * from NM_PedidoMaterialesD " & _
            " where codigo_pedido='" & sCodigoPedido & "' " & _
            " and numero_conos>numero_real_conos "
            dt = objConn.Query(sql)
            Return dt
        End Function

    End Class

End Namespace