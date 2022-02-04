Imports NM_General.NM_BaseDatos

Namespace HA_Hilanderia
    Public Class HA_InventarioCanillasCoche

        Public Codigo_Inventario As String
        Public Codigo_Hilo As String
        Public titulo As Double
        Public canillas_coche As Integer
        Public kilos_canillas As Double
        Public Usuario As String

        Sub New()
            Codigo_Inventario = ""
            Codigo_Hilo = ""
            titulo = 0
            canillas_coche = 0
        End Sub

        Function Add() As Boolean
            Dim sql As String, objConn As New NM_Consulta(4)
            Try
                sql = "Insert into HA_InventarioCanillasCoche (codigo_inventario,codigo_hilo," & _
                "titulo, canillas_coche, kilos_canillas, usuario_creacion, fecha_creacion, vch_codigomezcla) " & _
                "values('" & Codigo_Inventario & "','" & Codigo_Hilo & "', " & titulo & _
                "," & canillas_coche & "," & kilos_canillas & ",'" & Usuario & "',getdate(), '" & Codigo_Hilo.Substring(11, 3) & "')"
                Return objConn.Execute(sql)
            Catch
                Return False
            End Try
        End Function

        Function Update() As Boolean
            Dim sql As String, objConn As New NM_Consulta(4)
            Try
                sql = "Update HA_InventarioCanillasCoche set titulo = " & titulo & ", canillas_coche=" & _
                canillas_coche & ", kilos_canillas=" & kilos_canillas & ", usuario_modificacion='" & Usuario & "', " & _
                "fecha_modificacion=getdate() where codigo_inventario='" & _
                Codigo_Inventario & "' and codigo_hilo='" & Codigo_Hilo & "'"
                Return objConn.Execute(sql)
            Catch
                Return False
            End Try

        End Function

        Function Delete(ByVal sCodigoInventario As String, _
        ByVal sCodigoHilo As String) As Boolean
            Dim sql As String, objConn As New NM_Consulta(4)
            Try
                sql = "Delete from HA_InventarioCanillasCoche where " & _
                " codigo_inventario='" & sCodigoInventario & "' and codigo_hilo='" & sCodigoHilo & "'"
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
                sql = "Select * from HA_InventarioCanillasCoche where " & _
                 " codigo_inventario='" & sCodigoInventario & "' and codigo_hilo='" & sCodigoHilo & "'"
                dt = objConn.Query(sql)
                Return (dt.Rows.Count > 0)
            Catch
                Return False
            End Try
        End Function

        Function List() As DataTable
            Dim sql As String, objConn As New NM_Consulta(4)
            Dim dt As New DataTable()
            sql = "Select * from HA_InventarioCanillasCoche "
            dt = objConn.Query(sql)
            Return dt
        End Function

        Function List(ByVal sCodigoInventario As String) As DataTable
            Dim sql As String, objConn As New NM_Consulta(4)
            Dim dt As New DataTable()
            sql = "Select * from HA_InventarioCanillasCoche where codigo_inventario='" & _
            sCodigoInventario & "' "

            dt = objConn.Query(sql)
            Return dt
        End Function

        Sub Seek(ByVal sCodigoInventario As String, _
        ByVal sCodigoHilo As String)
            Dim sql As String, objConn As New NM_Consulta(4)
            Dim dt As New DataTable(), fila As DataRow

            sql = "Select * from HA_InventarioCanillasCoche where " & _
            " codigo_inventario='" & sCodigoInventario & "' and codigo_hilo='" & sCodigoHilo & "'"
            dt = objConn.Query(sql)
            For Each fila In dt.Rows
                Me.canillas_coche = fila("canillas_coche")
                Me.kilos_canillas = fila("kilos_canillas")
                Me.Codigo_Inventario = fila("codigo_inventario")
                Me.Codigo_Hilo = fila("codigo_hilo")
                Me.titulo = fila("titulo")
            Next
        End Sub

    End Class

End Namespace