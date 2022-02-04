Imports NM_General.NM_BaseDatos
Namespace NM_Tejeduria
	Public Class NM_PartidaUrdidoDCalidad
		Public CodigoPartida As String
        Public Carrete As String
        Public CodigoHilo As String
		Public CodigoDetalleCalidad As Integer
		Public CodigoMaestroCalidad As String
		Public ValorDetalleCalidad As String
		Public Debug As String
		Public dtDCalidad As New DataTable()
		Public dtDetalle As New DataTable()
		Public Usuario As String

		Sub New()
			CodigoPartida = ""
			Carrete = ""
			CodigoDetalleCalidad = 0
            ValorDetalleCalidad = ""
            CodigoHilo = ""
			Usuario = ""
		End Sub

		Sub New(ByVal bHeader As Boolean)
			Dim objDet As New NM_DCalidad()
			CodigoPartida = ""
			Carrete = ""
			CodigoDetalleCalidad = 0
            ValorDetalleCalidad = ""
            CodigoHilo = ""
			Usuario = ""
			dtDCalidad = objDet.Lista(True, False, False)
		End Sub

		Public Function List(ByVal sCodigoPartida As String, ByVal bParaGrid As Boolean) As DataTable
			Dim sql As String, objConn As New NM_Consulta()
			Dim dtTable As New DataTable()
			If bParaGrid = True Then
				sql = "EXEC SP_NMCalidadPartidaUrdido '" & sCodigoPartida & "'"
				dtTable = objConn.Query(sql)
			End If
			Return dtTable
		End Function

        Sub Delete(ByVal sCodigoPartida As String, ByVal sCarrete As String, _
        ByVal sCodigoDCalidad As Integer, ByVal sCodigo_maestro As String, _
        ByVal sCodigoHilo As String)
            Dim sql As String, objConn As New NM_Consulta()
            sql = "Delete from NM_PartidaUrdidoDCalidad " & _
            " where codigo_partida_urdido = '" & sCodigoPartida & "' " & _
            " and carrete = '" & sCarrete & "' and codigo_maestro_calidad ='" & _
            sCodigo_maestro & "' and codigo_detalle_calidad = " & _
            sCodigoDCalidad & " and codigo_hilo='" & sCodigoHilo & "' "
            objConn.Execute(sql)
        End Sub

        Public Function List() As DataTable
            Dim sql As String, objConn As NM_Consulta, dtCalidad As New DataTable()
            sql = "Select * from NM_PartidaUrdidoDCalidad "
            dtCalidad = objConn.Query(sql)
            Return dtCalidad
        End Function

        Sub Add()
            Dim sql As String, objConn As New NM_Consulta()
            sql = "Insert into NM_PartidaUrdidoDCalidad (" & _
            "codigo_partida_urdido, carrete, codigo_detalle_calidad, " & _
            " codigo_maestro_calidad,codigo_hilo, valor_detalle_calidad, " & _
            " usuario_creacion " & _
            ", fecha_creacion) values('" & CodigoPartida & "','" & Carrete & "'," & _
            CodigoDetalleCalidad & ",'" & CodigoMaestroCalidad & "','" & _
            CodigoHilo & "', " & _
            ValorDetalleCalidad & ",'" & Usuario & "',getdate())"
            objConn.Execute(sql)
        End Sub

        Sub Update()
            Dim sql As String, objConn As New NM_Consulta()
            sql = "Update NM_PartidaUrdidoDCalidad set " & _
            " valor_detalle_calidad = " & ValorDetalleCalidad & _
            ", usuario_modificacion='" & Usuario & "', fecha_modificacion=getdate()" & _
            " where codigo_partida_urdido = '" & CodigoPartida & "' " & _
            " and carrete ='" & Carrete & "' and codigo_detalle_calidad=" & _
            CodigoDetalleCalidad & " and codigo_maestro_calidad ='" & _
            CodigoMaestroCalidad & "' and codigo_hilo='" & CodigoHilo & "' "
            objConn.Execute(sql)

            Dim objPart As New NM_PartidaUrdido(CodigoPartida)
            objPart.RoturasMillon = GetRoturasMillon(CodigoPartida)
            objPart.RoturasMillonTotal = GetRoturasMillonTotal(CodigoPartida)
            objPart.Usuario = Usuario
            objPart.Update()

        End Sub

        Function GetRoturasMillon(ByVal sCodigoPartida As String) As Double
            Dim sql As String, objConn As New NM_Consulta()
            Dim dt As New DataTable(), fila As DataRow, retorno As Double = 0
            sql = "select case when sum(valor_detalle_calidad) is null then 0 " & _
            " else sum(valor_detalle_calidad) end " & _
            " from NM_PartidaUrdidoDCalidad where(codigo_detalle_calidad <> 12)" & _
            " and codigo_partida_urdido='" & sCodigoPartida & "'"
            dt = objConn.Query(sql)
            For Each fila In dt.Rows
                retorno = fila.Item(0)
            Next
            Return retorno
        End Function

        Function GetRoturasMillonTotal(ByVal sCodigoPartida As String) As Double
            Dim sql As String, objConn As New NM_Consulta()
            Dim dt As New DataTable(), fila As DataRow, retorno As Double = 0
            sql = "select case when sum(valor_detalle_calidad) is null then 0 " & _
            " else sum(valor_detalle_calidad) end " & _
            " from NM_PartidaUrdidoDCalidad where codigo_partida_urdido='" & sCodigoPartida & "'"
            dt = objConn.Query(sql)
            For Each fila In dt.Rows
                retorno = fila.Item(0)
            Next
            Return retorno
        End Function

        Sub Seek(ByVal sCodigoPartida As String, ByVal sCarrete As String, _
        ByVal iCodigoDetalleCalidad As Integer, ByVal sCodigoMaestroCalidad As String, _
        ByVal sCodigoHilo As String)
            Dim sql As String, objConn As New NM_Consulta(), objTable As New DataTable()
            Dim fila As DataRow
            sql = "Select * from NM_PartidaUrdidoDCalidad " & _
            " where codigo_partida_urdido = '" & CodigoPartida & "' " & _
            " and carrete ='" & Carrete & "' and codigo_detalle_calidad=" & _
            CodigoDetalleCalidad & " and codigo_maestro_calidad ='" & _
            CodigoMaestroCalidad & "' and codigo_hilo ='" & sCodigoHilo & "' "
            Debug = sql
            objTable = objConn.Query(sql)
            For Each fila In objTable.Rows
                CodigoPartida = fila.Item("codigo_partida_urdido")
                Carrete = fila.Item("carrete")
                CodigoDetalleCalidad = fila.Item("codigo_detalle_calidad")
                CodigoMaestroCalidad = fila.Item("codigo_maestro_calidad")
                ValorDetalleCalidad = fila.Item("valor_detalle_calidad")
                CodigoHilo = fila.Item("codigo_hilo")
            Next
        End Sub

        Public Function Exist(ByVal sCodigoPartida As String, ByVal sCarrete As String, _
          ByVal iCodigoDetalleCalidad As Integer, ByVal sCodigoMaestroCalidad As String, _
          ByVal sCodigoHilo As String) As Boolean
            Dim sql As String, objConn As New NM_Consulta(), objTable As New DataTable()
            sql = "Select * from NM_PartidaUrdidoDCalidad " & _
            " where codigo_partida_urdido = '" & CodigoPartida & "' " & _
            " and carrete ='" & Carrete & "' and codigo_detalle_calidad=" & _
            CodigoDetalleCalidad & " and codigo_maestro_calidad ='" & _
            CodigoMaestroCalidad & "' and codigo_hilo='" & sCodigoHilo & "' "
            objTable = objConn.Query(sql)
            return (objTable.Rows.Count > 0)
        End Function
    End Class
End Namespace