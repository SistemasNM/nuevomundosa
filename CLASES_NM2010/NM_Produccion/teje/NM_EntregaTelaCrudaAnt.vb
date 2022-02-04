Imports NM_General.NM_BaseDatos
Imports NM.AccesoDatos

Namespace NM_Tejeduria
    Public Class NM_EntregaTelaCrudaAnt

        Private m_objConnection As AccesoDatosSQLServer

        Public NumeroFicha As String
        Public CodigoMaquina As String
        Public CodigoArticulo As String
        Public NumeroOrdenProduccion As String
        Public Calidad As String
        Public Metraje As Double
        Public Pieza As String
        Public Usuario As String

        Sub New()
            m_objConnection = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.Produccion)

            Me.NumeroFicha = ""
            Me.CodigoArticulo = ""
            Me.CodigoMaquina = ""
            Me.Metraje = 0
            Me.Pieza = ""
            NumeroOrdenProduccion = ""
        End Sub

        Function Add() As Boolean
            Dim sql As String, objConn As New NM_Consulta()
            Try
                sql = "Insert into NM_EntregaTelaCrudaAnt(" & _
                "numero_ficha, orden_produccion, codigo_articulo, pieza, codigo_maquina," & _
                "metraje, calidad, usuario_creacion, fecha_creacion) " & _
                "values('" & Me.NumeroFicha & "','" & NumeroOrdenProduccion & "', '" & _
                CodigoArticulo & "','" & Pieza & "','" & _
                CodigoMaquina & "'," & Metraje & _
                ",'" & Calidad & "', '" & Usuario & "',getdate())"
                Return objConn.Execute(sql)
            Catch
                Return False
            End Try
        End Function

        Function Update() As Boolean
            Dim sql As String, objConn As New NM_Consulta()
            Try
                sql = "Update NM_EntregaTelaCrudaAnt set " & _
                " metraje=" & Metraje & ", " & _
                " calidad = '" & Calidad & "', " & _
                " codigo_maquina='" & CodigoMaquina & "' " & _
                " where numero_ficha = '" & NumeroFicha & "' " & _
                " and pieza='" & Pieza & "' "
                Return objConn.Execute(sql)
            Catch
                Return False
            End Try

        End Function

        Function Delete(ByVal iNumeroFicha As String, ByVal sPieza As String) As Boolean
            Dim sql As String, objConn As New NM_Consulta
            Try
                sql = "Delete from NM_EntregaTelaCrudaAnt " & _
                " where numero_ficha='" & iNumeroFicha & "' " & _
                " and pieza='" & sPieza & "' "
                Return objConn.Execute(sql)
            Catch
                Return False
            End Try
        End Function

        Function Exist(ByVal iNumeroFicha As String, ByVal sPieza As String) As Boolean
            Dim sql As String, objconn As New NM_Consulta
            Dim dt As New DataTable
            sql = "Select * from NM_EntregaTelaCrudaAnt " & _
                " where numero_ficha = '" & iNumeroFicha & "'  " & _
                " and pieza='" & sPieza & "' "
            dt = objconn.Query(sql)
            Return (dt.Rows.Count > 0)
        End Function

        Function Exist(ByVal sPieza As String) As Boolean
            Dim sql As String, objconn As New NM_Consulta
            Dim dt As New DataTable
            sql = "Select * from NM_EntregaTelaCrudaAnt " & _
                " where pieza='" & sPieza & "' "
            dt = objconn.Query(sql)
            Return (dt.Rows.Count > 0)
        End Function

        Sub Seek(ByVal iNumeroFicha As String, ByVal sPieza As String)
            Dim sql As String, objconn As New NM_Consulta
            Dim dt As New DataTable, fila As DataRow
            sql = "Select * from NM_EntregaTelaCrudaAnt " & _
                " where numero_ficha='" & iNumeroFicha & "' " & _
                " and pieza='" & Pieza & "' "
            dt = objconn.Query(sql)
            For Each fila In dt.Rows
                Me.Calidad = fila("calidad")
                Me.CodigoArticulo = fila("codigo_articulo")
                Me.CodigoMaquina = fila("codigo_maquina")
                Me.Metraje = fila("metraje")
                Me.NumeroFicha = fila("numero_ficha")
                Me.Pieza = fila("pieza")
                Me.NumeroOrdenProduccion = fila("orden_produccion")
            Next
            dt = Nothing
        End Sub

        Function List() As DataTable
            Dim sql As String, objconn As New NM_Consulta
            Dim dt As New DataTable
            sql = "Select * from NM_EntregaTelaCrudaAnt "
            dt = objconn.Query(sql)
            Return dt
        End Function

        Function List(ByVal iNumeroFicha As String, ByVal bParaGrid As Boolean) As DataTable
            Dim sql As String, objconn As New NM_Consulta
            Dim dt As New DataTable
            sql = "Select * from NM_EntregaTelaCrudaAnt " & _
            " where numero_ficha = '" & iNumeroFicha & "' "
            dt = objconn.Query(sql)
            Return dt
        End Function

        Function List(ByVal sCodigoArticulo As String) As DataTable
            Dim sql As String, objconn As New NM_Consulta
            Dim dt As New DataTable
            sql = "Select * from NM_EntregaTelaCrudaAnt " & _
            " where codigo_articulo='" & sCodigoArticulo & "' "

            dt = objconn.Query(sql)
            Return dt
        End Function

        Function GetSaldoAlmacen(ByVal sCodigoArticulo As String) As Double
            Dim dt As New DataTable, fila As DataRow, valor As Double = 0
            Dim objparametros() As Object = {"vc_codarticulo", sCodigoArticulo}

            dt = m_objConnection.ObtenerDataTable("NM_ObtieneSaldoAntiguo", objparametros)
            For Each fila In dt.Rows
                If IsDBNull(fila("saldo")) = False Then valor = fila("saldo")
            Next
            Return valor
        End Function

        Function UpdateSaldo() As Boolean
            Try
                Dim sql As String, objConn As New NM_Consulta
                sql = "Update NM_StockAntiguo set saldo=saldo-" & _
                "(select case when sum(metraje) is null then 0 else " & _
                " sum(metraje) end from NM_EntregaTelaCrudaAnt " & _
                " where numero_ficha= '" & NumeroFicha & "' " & _
                " and codigo_articulo='" & CodigoArticulo & "') " & _
                " where codigo_articulo='" & CodigoArticulo & "' "
                Return objConn.Execute(sql)
            Catch
                Return False
            End Try
        End Function

        Function UpdateSaldo(ByVal pMonto As Double, ByVal sCodigoArticulo As String) As Boolean
            Try
                Dim objparametros() As Object = {"nm_metraje", pMonto, "vc_codarticulo", sCodigoArticulo}

                Return (m_objConnection.EjecutarComando("NM_ActualizaSaldoAntiguo", objparametros) > 0)
            Catch ex As Exception
                Throw ex
                Return False
            End Try
        End Function

    End Class

End Namespace
