Imports NM_General.NM_BaseDatos

Namespace HA_Hilanderia
    Public Class HA_InventarioSpandex

        Public Codigo_Inventario As String
        Public Codigo_Spandex As String
        Public denier As Double
        Public kilos As Double
        Public Usuario As String

        Sub New()
            Codigo_Inventario = ""
            Codigo_Spandex = ""
            denier = 0
        End Sub

        Function Add() As Boolean
            Dim sql As String, objConn As New NM_Consulta(4)
            Try
                sql = "Insert into HA_InventarioSpandex (codigo_inventario,Codigo_Spandex," & _
                "denier, kilos, usuario_creacion, fecha_creacion) " & _
                "values('" & Codigo_Inventario & "','" & Codigo_Spandex & "', " & denier & _
                "," & kilos & ",'" & Usuario & "',getdate() )"
                Return objConn.Execute(sql)
            Catch
                Return False
            End Try
        End Function

        Function Update() As Boolean
            Dim sql As String, objConn As New NM_Consulta(4)
            Try
                sql = "Update HA_InventarioSpandex set denier = " & denier & _
                ", kilos=" & kilos & ", usuario_modificacion='" & Usuario & "', " & _
                "fecha_modificacion=getdate() where codigo_inventario='" & _
                Codigo_Inventario & "' and Codigo_Spandex='" & Codigo_Spandex & "'"
                Return objConn.Execute(sql)
            Catch
                Return False
            End Try

        End Function

        Function Delete(ByVal sCodigoInventario As String, _
        ByVal sCodigoSpandex As String) As Boolean
            Dim sql As String, objConn As New NM_Consulta(4)
            Try
                sql = "Delete from HA_InventarioSpandex where " & _
                " codigo_inventario='" & sCodigoInventario & "' and Codigo_Spandex='" & sCodigoSpandex & "'"
                Return objConn.Execute(sql)
            Catch
                Return False
            End Try

        End Function

        Function Exist(ByVal sCodigoInventario As String, _
        ByVal sCodigoSpandex As String) As Boolean
            Dim sql As String, objConn As New NM_Consulta(4)
            Dim dt As New DataTable()
            Try
                sql = "Select * from HA_InventarioSpandex where " & _
                 " codigo_inventario='" & sCodigoInventario & "' and Codigo_Spandex='" & sCodigoSpandex & "'"
                dt = objConn.Query(sql)
                Return (dt.Rows.Count > 0)
            Catch
                Return False
            End Try
        End Function

        Function List() As DataTable
            Dim sql As String, objConn As New NM_Consulta(4)
            Dim dt As New DataTable()
            sql = "Select * from HA_InventarioSpandex "
            dt = objConn.Query(sql)
            Return dt
        End Function

        Function List(ByVal sCodigoInventario As String) As DataTable
            Dim sql As String, objConn As New NM_Consulta(4)
            Dim dt As New DataTable()
            sql = "Select * from HA_InventarioSpandex where codigo_inventario='" & _
            sCodigoInventario & "' "

            dt = objConn.Query(sql)
            Return dt
        End Function

        Sub Seek(ByVal sCodigoInventario As String, _
        ByVal sCodigoSpandex As String)
            Dim sql As String, objConn As New NM_Consulta(4)
            Dim dt As New DataTable(), fila As DataRow

            sql = "Select * from HA_InventarioSpandex where " & _
            " codigo_inventario='" & sCodigoInventario & "' and Codigo_Spandex='" & sCodigoSpandex & "'"
            dt = objConn.Query(sql)
            For Each fila In dt.Rows
                Me.kilos = fila("kilos")
                Me.Codigo_Inventario = fila("codigo_inventario")
                Me.Codigo_Spandex = fila("Codigo_Spandex")
                Me.denier = fila("denier")
            Next
        End Sub

    End Class

End Namespace