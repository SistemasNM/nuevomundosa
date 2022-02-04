Imports NM_General.NM_BaseDatos

Namespace HA_Hilanderia
    Public Class HA_InventarioConosTerminados

        Public Codigo_Inventario As String
        Public Codigo_Hilo As String
        Public titulo As Double
        Public codigo_materia_prima As String
        Public cantidad_conos As Integer
        Public peso_conos As Double
        Public Usuario As String

        Sub New()
            Codigo_Inventario = ""
            Codigo_Hilo = ""
            titulo = 0
            codigo_materia_prima = ""
            cantidad_conos = 0
        End Sub

        Function Add() As Boolean
            Dim sql As String, objConn As New NM_Consulta(4)
            Try
                sql = "Insert into HA_InventarioConosTerminados (codigo_inventario,codigo_hilo," & _
                "titulo, codigo_materia_prima, cantidad_conos, peso_conos, usuario_creacion, fecha_creacion, vch_codigomezcla) " & _
                "values('" & Codigo_Inventario & "','" & Codigo_Hilo & "', " & titulo & _
                ",'" & codigo_materia_prima & "'," & cantidad_conos & "," & peso_conos & ",'" & Usuario & "', getdate(), '" & Codigo_Hilo.Substring(11, 3) & "')"
                Return objConn.Execute(sql)
            Catch
                Return False
            End Try
        End Function

        Function Update() As Boolean
            Dim sql As String, objConn As New NM_Consulta(4)
            Try
                sql = "Update HA_InventarioConosTerminados set titulo = " & titulo & ", cantidad_conos=" & _
                cantidad_conos & ", peso_conos=" & peso_conos & ", usuario_modificacion='" & Usuario & "', " & _
                "fecha_modificacion=getdate() where codigo_inventario='" & _
                Codigo_Inventario & "' and codigo_hilo=" & Codigo_Hilo & " and " & _
                "codigo_materia_prima='" & codigo_materia_prima & "' "
                Return objConn.Execute(sql)
            Catch
                Return False
            End Try

        End Function

        Function Delete(ByVal sCodigoInventario As String, _
        ByVal sCodigoHilo As String, ByVal sCodigoMateriaPrima As String) As Boolean
            Dim sql As String, objConn As New NM_Consulta(4)
            Try
                sql = "Delete from HA_InventarioConosTerminados where " & _
                " codigo_inventario='" & sCodigoInventario & "' and codigo_hilo='" & sCodigoHilo & "' and " & _
                "codigo_materia_prima='" & sCodigoMateriaPrima & "' "
                Return objConn.Execute(sql)
            Catch
                Return False
            End Try

        End Function

        Function Exist(ByVal sCodigoInventario As String, _
        ByVal sCodigoHilo As String, ByVal sCodigoMateriaPrima As String) As Boolean
            Dim sql As String, objConn As New NM_Consulta(4)
            Dim dt As New DataTable()
            Try
                sql = "Select * from HA_InventarioConosTerminados where " & _
                 " codigo_inventario='" & sCodigoInventario & "' and codigo_hilo='" & sCodigoHilo & "' and " & _
                "codigo_materia_prima='" & sCodigoMateriaPrima & "' "
                dt = objConn.Query(sql)
                Return (dt.Rows.Count > 0)
            Catch
                Return False
            End Try
        End Function

        Function List() As DataTable
            Dim sql As String, objConn As New NM_Consulta(4)
            Dim dt As New DataTable()
            sql = "Select * from HA_InventarioConosTerminados "
            dt = objConn.Query(sql)
            Return dt
        End Function

        Function List(ByVal sCodigoInventario As String) As DataTable
            Dim sql As String, objConn As New NM_Consulta(4)
            Dim dt As New DataTable()
            sql = "Select * from HA_InventarioConosTerminados where codigo_inventario='" & _
            sCodigoInventario & "' "

            dt = objConn.Query(sql)
            Return dt
        End Function

        Sub Seek(ByVal sCodigoInventario As String, _
        ByVal sCodigoHilo As String, ByVal sCodigoMateriaPrima As String)
            Dim sql As String, objConn As New NM_Consulta(4)
            Dim dt As New DataTable(), fila As DataRow

            sql = "Select * from HA_InventarioConosTerminados where " & _
            " codigo_inventario='" & sCodigoInventario & "' and codigo_hilo='" & sCodigoHilo & "' and " & _
            "codigo_materia_prima='" & sCodigoMateriaPrima & "' "
            dt = objConn.Query(sql)
            For Each fila In dt.Rows
                Me.codigo_materia_prima = fila("codigo_materia_prima")
                Me.cantidad_conos = fila("cantidad_conos")
                Me.peso_conos = fila("peso_conos")
                Me.Codigo_Inventario = fila("codigo_inventario")
                Me.Codigo_Hilo = fila("codigo_hilo")
                Me.titulo = fila("titulo")
            Next
        End Sub

        Function Totales(ByVal sCodigoInventario As String) As DataTable
            Dim sql As String, objConn As New NM_Consulta(4)
            Dim dt As New DataTable(), fila As DataRow
            sql = "SELECT SUM(dbo.HA_InventarioConosTerminados.peso_conos) AS Peso_Conos,dbo.NM_MateriaPrima.descripcion_materia_prima  " & _
            "FROM  dbo.HA_InventarioConosTerminados INNER JOIN " & _
            "dbo.NM_MateriaPrima ON dbo.HA_InventarioConosTerminados.codigo_materia_prima = dbo.NM_MateriaPrima.codigo_materia_prima  " & _
            "WHERE HA_InventarioConosTerminados.codigo_inventario = '" & sCodigoInventario & "'" & _
            "GROUP BY dbo.HA_InventarioConosTerminados.codigo_materia_prima, dbo.NM_MateriaPrima.descripcion_materia_prima "
            Try
                dt = objConn.Query(sql)
            Catch ex As System.Exception

            End Try
            Return dt
        End Function

    End Class

End Namespace