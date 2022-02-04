Imports NM_General.NM_BaseDatos

Namespace NM_Tejeduria

	Public Class NM_EntregaTelaCrudaD
        Public NumeroFicha As String
		Public NumeroPieza As String
		Public Usuario As String
		
		Sub New()
			Inicializa()
		End Sub

		Function Add() As Boolean
			Dim sql As String, objConn As New NM_Consulta()
			Try
                sql = "Insert into NM_EntregaTelaCrudaD (numero_ficha," & _
                "codigo_pieza, usuario_creacion, fecha_creacion) values ('" & _
                NumeroFicha & "','" & NumeroPieza & "', '" & Usuario & "', getdate())"

				objConn.Execute(sql)
				Return True
            Catch ex As Exception
                Throw ex
                Return False
            End Try
		End Function


        Function Delete(ByVal iNumeroFicha As String, ByVal sNumeroPieza As String) As Boolean
            Dim sql As String, objConn As New NM_Consulta
            Try
                sql = "Delete from NM_EntregaTelaCrudaD where numero_ficha = '" & _
                iNumeroFicha & "' and codigo_pieza='" & sNumeroPieza & "' "
                objConn.Execute(sql)
                Return True
            Catch ex As Exception
                Throw ex
                Return False
            End Try

        End Function

        Function List() As DataTable
            Dim sql As String, objConn As New NM_Consulta
            Dim dt As New DataTable
            sql = "Select * from NM_EntregaTelaCrudaD "
            dt = objConn.Query(sql)
            Return dt
        End Function

        Function List(ByVal iNumeroFicha As String) As DataTable
            Dim sql As String, objConn As New NM_Consulta
            Dim dt As New DataTable
            sql = "Select * from NM_EntregaTelaCrudaD etc " & _
            "JOIN NM_Pieza p ON p.codigo_pieza = etc.codigo_pieza " & _
            " where etc.numero_ficha= '" & iNumeroFicha & "' order by etc.fecha_creacion "
            dt = objConn.Query(sql)
            Return dt
        End Function

        Function Exist(ByVal iNumeroFicha As String, ByVal sNumeroPieza As String) As Boolean
            Dim sql As String, objConn As New NM_Consulta
            Dim dt As New DataTable
            Try
                sql = "Select * from NM_EntregaTelaCrudaD where numero_ficha='" & _
                iNumeroFicha & "' and codigo_pieza='" & sNumeroPieza & "' "
                dt = objConn.Query(sql)
                Return (dt.Rows.Count > 0)
            Catch
                Return False
            End Try

        End Function

        Function Exist(ByVal sNumeroPieza As String) As Boolean
            Dim sql As String, objConn As New NM_Consulta
            Dim dt As New DataTable
            Try
                sql = "Select * from NM_EntregaTelaCrudaD where codigo_pieza='" & sNumeroPieza & "' "
                dt = objConn.Query(sql)
                Return (dt.Rows.Count > 0)
            Catch
                Return False
            End Try

        End Function

        Private Sub Inicializa()
            Me.NumeroFicha = ""
            Me.NumeroPieza = ""
        End Sub

    End Class

End Namespace
