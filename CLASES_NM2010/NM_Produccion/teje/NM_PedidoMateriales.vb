Imports NM_General.NM_BaseDatos
Imports System.Web.Mail

Namespace NM_Tejeduria
	Public Class NM_PedidoMateriales
		Public CodigoPedido As String
		Public CodigoArea As String
        Public Fecha As String
		Public Usuario As String
		Public Debug As String
        Public dtDetalle As New DataTable
        Private objUtil As New NM_General.Util

		Sub New()
			CodigoPedido = ""
			CodigoArea = ""
			Fecha = Date.Today.Date
		End Sub

		Sub New(ByVal sCodigoPedido As String)
			Dim objDetalle As New NM_PedidoMaterialesD()
			Seek(sCodigoPedido)
			dtDetalle = objDetalle.List(sCodigoPedido)
		End Sub

		Public Function Add() As Boolean
			Dim sql As String, objConn As New NM_Consulta()
			Try
                sql = "Insert into NM_PedidoMateriales (codigo_pedido, codigo_area," & _
                "fecha, usuario_creacion, fecha_creacion)values('" & CodigoPedido & _
                "','" & CodigoArea & "',convert(datetime,'" & objUtil.FormatFecha(Fecha) & "'),'" & Usuario & "',getdate())"
                objConn.Execute(sql)
                Return True
			Catch
				Return False
			End Try

		End Function

		Public Function Update() As Boolean
			Dim sql As String, objConn As New NM_Consulta()
			Try
				sql = "Update NM_PedidoMateriales set usuario_modificacion='" & _
				Usuario & "', fecha_modificacion=getdate() where codigo_pedido='" & _
				CodigoPedido & "' "
                objConn.Execute(sql)
                Return True
			Catch
				Return False
			End Try

		End Function

		Public Function CreateId() As String
			Dim sql As String, objConn As New NM_Consulta()
			Dim dt As New DataTable(), fila As DataRow
			Dim codigo As String = "", correlativo As Integer
			sql = "select case when right(Max(codigo_pedido),6) is Null " & _
			" then '000001' else right(Max(codigo_pedido),6) end as correlativo, " & _
			"right(year(getdate()),2) as anno  from NM_PedidoMateriales " & _
			" where Year(Fecha) = Year(getdate()) "
			Debug = sql
			dt = objConn.Query(sql)
			For Each fila In dt.Rows
				correlativo = Val(fila.Item("correlativo")) + 1

				codigo = "P" & fila.Item("anno") & Format(correlativo, "000000")
			Next
			Return codigo
		End Function

		Public Function Delete(ByVal sCodigoPedido As String) As Boolean
			Dim sql As String, objConn As New NM_Consulta()
			Try
				sql = "Delete from NM_PedidoMateriales where codigo_pedido='" & CodigoPedido & "' "
				objConn.Execute(sql)
				Return True
			Catch
				Return False
			End Try
		End Function

		Public Function List() As DataTable
			Dim sql As String, objConn As New NM_Consulta()
			sql = "Select * from NM_PedidoMateriales"
			Return objConn.Query(sql)
		End Function

		Sub Seek(ByVal sCodigoPedido As String)
			Dim sql As String, objConn As New NM_Consulta()
			Dim dt As New DataTable(), fila As DataRow
			sql = "Select * from NM_PedidoMateriales where codigo_pedido='" & sCodigoPedido & "' "
			dt = objConn.Query(sql)
			For Each fila In dt.Rows
				CodigoPedido = fila.Item("codigo_pedido")
				CodigoArea = fila.Item("codigo_area")
				Fecha = fila.Item("fecha")
			Next
		End Sub

		Function GenerarEmail(ByVal Directorio As String) As String
			Dim formatoBody As String
			Dim formatoDetalle As String
            formatoBody = GetFile(Directorio & "\mphl_cab.txt")
            formatoDetalle = GetFile(Directorio & "\mphl_det.txt")

			Dim detalle As String
			Dim item As DataRow

			For Each item In dtDetalle.Rows
				detalle = FormarDetalle(formatoDetalle, item)
				formatoBody = FormarBodyDetalle(formatoBody, detalle)
			Next
			formatoBody = formatoBody.Replace("%detalle%", "")
			formatoBody = FormarBody(formatoBody, CodigoPedido, CodigoArea)
			Return formatoBody
			'SmtpMail.SmtpServer = "devel02"
			'SmtpMail.Send("mborja@itc-peru.com", "uvachado20@hotmail.com", "prueba", "Hola")
		End Function

		Private Function GetFile(ByVal Ruta As String) As String
			' Obtener el formato del detalle
			Dim formato As String
			Dim archivoLectura As Integer = FreeFile()
			FileOpen(archivoLectura, Ruta, OpenMode.Input, OpenAccess.Read, OpenShare.Shared)
			Do While Not EOF(archivoLectura)
				formato += LineInput(archivoLectura) & vbCrLf
			Loop
			FileClose(archivoLectura)
			Return formato
		End Function

		Private Function FormarDetalle(ByVal formatoDetalle As String, ByVal fila As DataRow) As String
			' Formar un detalle con el formato y los valores dados
			Dim resultado As String
			resultado = formatoDetalle.Replace("%codigoHilo%", fila("codigo_hilo"))
			resultado = resultado.Replace("%numeroConos%", fila("numero_conos"))
			resultado = resultado.Replace("%destino%", fila("destino"))
			resultado = resultado.Replace("%conosRecibidos%", fila("numero_real_conos"))
			Return resultado
		End Function

		Private Function FormarBodyDetalle(ByVal strBody As String, ByVal strDetalle As String) As String
			' Formar un detalle con el formato y los valores dados
			Dim resultado As String
			resultado = strBody.Replace("%detalle%", strDetalle & "%detalle%")
			Return resultado
		End Function

		Private Function FormarBody(ByVal strBody As String, ByVal sCodigoPedido As String, ByVal strArea As String) As String
			' Formar un detalle con el formato y los valores dados
			Dim resultado As String
			resultado = strBody.Replace("%codigoPedido%", sCodigoPedido)
			resultado = resultado.Replace("%area%", strArea)
			resultado = resultado.Replace("%fecha%", Date.Today)
			Return resultado
		End Function

	End Class

End Namespace