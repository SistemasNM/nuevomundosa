Imports NM.AccesoDatos
Imports NM_General.NM_BaseDatos

Namespace NM_Tejeduria

    Public Class NMM_TelaUrdimbreD

        Public CodigoArticulo As String
        Public CodigoUrdimbre As String
        Public Item As Integer
        Public CodigoHilo As String
        Public Tipo As String
        Public TipoOrillo As String
        Public Usuario As String
		Private m_sqlDtProduccion As AccesoDatosSQLServer

        Sub New()
            CodigoUrdimbre = ""
            item = 0
            CodigoHilo = 0
            Tipo = ""
            TipoOrillo = ""
            usuario = ""
			m_sqlDtProduccion = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.Produccion)
        End Sub

        Sub New(ByVal pCodigoUrdimbre As String, ByVal pCodigoArticulo As String, ByVal pItem As String)
            Seek(pCodigoUrdimbre, pCodigoArticulo, pItem)
        End Sub

        Sub Seek(ByVal pCodigoUrdimbre As String, ByVal pCodigoArticulo As String, ByVal pItem As String)
            Dim objGen As New NM_Consulta
            Dim sql As String
            Dim objDT As New DataTable
            Dim objDR As DataRow

            sql = "Select * from NM_MA_UrdimbreDetalle where codigo_urdimbre='" & _
            pCodigoUrdimbre & "' and codigo_articulo='" & pCodigoArticulo & _
            "' and item=" & pItem
            objDT = objGen.Query(sql)

            For Each objDR In objDT.Rows
                CodigoUrdimbre = objDR("codigo_urdimbre")
                CodigoArticulo = objDR("codigo_articulo")
                Item = objDR("item")
                Tipo = objDR("tipo")
                TipoOrillo = objDR("tipo_orillo")
            Next
        End Sub

        Public Function List(ByVal pCodigo_urdimbre As String, ByVal pCodigoArticulo As String) As DataTable
            Dim objConn As New NM_Consulta
            Dim sql As String
            Dim DT As New DataTable
            sql = "Select * from NM_MA_UrdimbreDetalle " & _
            " where codigo_urdimbre='" & pCodigo_urdimbre & "' " & _
            " and codigo_articulo = '" & pCodigoArticulo & "' "
            DT = objConn.Query(sql)
            Return DT
        End Function

        Public Function ListByType(ByVal pCodigoUrdimbre As String, ByVal pTipo As String) As DataTable
            Dim dtblDatos As DataTable

            Try
                Dim objParametros() As Object = {"codigo_urdimbre", pCodigoUrdimbre, "tipo", pTipo, "codigo_articulo", String.Empty}
                dtblDatos = m_sqlDtProduccion.ObtenerDataTable("ListarDetalleUrdimbrePorTipo", objParametros)
            Catch ex As Exception
                Throw ex
            End Try
            Return dtblDatos
        End Function

        Public Function ListByType(ByVal pCodigoUrdimbre As String, ByVal pCodigoArticulo As String, ByVal pTipo As String) As DataTable
            Dim dtblDatos As DataTable

            Try
                Dim objParametros() As Object = {"codigo_urdimbre", pCodigoUrdimbre, "tipo", pTipo, "codigo_articulo", pCodigoArticulo}
                dtblDatos = m_sqlDtProduccion.ObtenerDataTable("ListarDetalleUrdimbrePorTipo", objParametros)
            Catch ex As Exception
                Throw ex
            End Try
            Return dtblDatos
        End Function

        Public Function Delete(ByVal pCodigoUrdimbre As String, ByVal pCodigoArticulo As String, _
        ByVal pTipo As String, ByVal pItem As String) As Boolean
            Dim objConn As New NM_Consulta
            Dim sql As String
            Try
                sql = "Delete from NM_MA_TelarUrdimbreD where codigo_urdimbre='" & _
                pCodigoUrdimbre & "' and item = " & pItem & _
                " and codigo_articulo = '" & pCodigoArticulo & "' and tipo ='" & pTipo & "' "
                Return objConn.Execute(sql)
            Catch ex As Exception
                Throw ex
                Return False
            End Try
        End Function

        Public Function delete(ByVal pCodigoUrdimbre As String, ByVal pCodigoArticulo As String) As Boolean
            Dim objConn As New NM_Consulta
            Dim sql As String
            Try
                sql = "delete from NM_MA_TelaUrdimbreD where codigo_urdimbre='" & _
                pCodigoUrdimbre & "' and codigo_articulo ='" & pCodigoArticulo & "' "
                Return objConn.Execute(sql)
            Catch ex As Exception
                Throw ex
                Return False
            End Try
        End Function

        Public Function delete(ByVal pCodigoArticulo As String) As Boolean
            Dim objConn As New NM_Consulta
            Dim sql As String
            Try
                sql = "delete from NM_MA_TelaUrdimbreD where codigo_articulo ='" & pCodigoArticulo & "' "
                Return objConn.Execute(sql)
            Catch ex As Exception
                Throw ex
                Return False
            End Try
        End Function

        Public Function update() As Boolean
            Dim objConn As New NM_Consulta
            Dim sql As String
            Dim DT As New DataTable
            Dim DR As DataRow
            Try
                sql = "Update NM_MA_TelaUrdimbreD SET " & _
                " tipo_orillo = " & TipoOrillo & _
                " where codigo_urdimbre='" & CodigoUrdimbre & "' and tipo = 0 " & _
                " and item=" & Item & " and codigo_articulo ='" & CodigoArticulo & _
                "' and codigo_hilo ='" & CodigoHilo & "' "
                Return objConn.Execute(sql)
            Catch ex As Exception
                Throw ex
                Return False
            End Try
        End Function

        Public Function Add() As Boolean
            Dim objConn As New NM_Consulta
            Dim sql As String
            Try
                sql = "INSERT INTO NM_MA_UrdimbreDetalle (codigo_urdimbre, " & _
                " codigo_articulo, tipo_orillo, item , tipo,codigo_hilo, usuario_creacion," & _
                "fecha_creacion) VALUES ('" & _
                CodigoUrdimbre & "','" & CodigoArticulo & "','" & TipoOrillo & "', " & _
                Item & "," & Tipo & ",'" & CodigoHilo & "'," & Usuario & ", getdate())"
                Return objConn.Execute(sql)
            Catch ex As Exception
                Return False
            End Try
        End Function

        Function CopyData(ByVal pCodigoArticulo As String, ByVal pUsuario As String) As Boolean
            Dim sql As String, objConn As New NM_Consulta
            sql = "Insert into NM_TelaUrdimbreD (codigo_urdimbre, revision_urdimbre, " & _
            " item, codigo_articulo, revision_articulo, tipo, " & _
            " codigo_hilo, tipo_orillo, usuario_creacion, fecha_creacion ) " & _
            " (select U.codigo_urdimbre, U.revision_urdimbre, D.item, " & _
            " A.codigo_articulo, A.revision_articulo, D.tipo, " & _
            " D.codigo_hilo, D.tipo_orillo, '" & pUsuario & "', getdate() " & _
            " from NM_MA_TelaUrdimbreD D, NM_MA_Urdimbre U, NM_MA_Articulo A " & _
            " where D.codigo_urdimbre = U.codigo_urdimbre " & _
            " and D.codigo_articulo = A.codigo_articulo " & _
            " and D.codigo_articulo ='" & pCodigoArticulo & "') "
            Return objConn.Execute(sql)
        End Function

        Function GetData(ByVal pCodigoArticulo As String, ByVal pCodigoUrdimbre As String, ByVal pUsuario As String) As Boolean
            Dim sql As String, objConn As New NM_Consulta
            sql = "Insert into NM_MA_TelaUrdimbreD (codigo_urdimbre, item, codigo_articulo, tipo, " & _
            " codigo_hilo, tipo_orillo, usuario_creacion, fecha_creacion ) " & _
            " (select codigo_urdimbre, item, '" & pCodigoArticulo & "', tipo, " & _
            " codigo_hilo, '', '" & pUsuario & "', getdate() " & _
            " from NM_MA_UrdimbreDetalle where codigo_urdimbre ='" & pCodigoUrdimbre & "') "
            Return objConn.Execute(sql)
        End Function

    End Class

End Namespace