Imports NM_General.NM_BaseDatos
Namespace NM_Hilanderia
    Public Class NM_InventarioHilanderia
        Public Codigo_Inventario As String
        Public Codigo_Tipo As String
        Public Codigo_Centro_Costo As String
        Public Codigo_Responsable As String
        Public Fecha As DateTime
        Public Usuario As String
        Private objUtil As New NM_General.Util

        Sub New()
            Me.Codigo_Inventario = ""
            Me.Codigo_Centro_Costo = ""
            Me.Codigo_Responsable = ""
            Me.Codigo_Tipo = ""
            Me.Fecha = Nothing
            
        End Sub

        Function Add() As Boolean
            Dim sql As String, objConn As New NM_Consulta(4)
            Try
                sql = "Insert into NM_inventarioHilanderia (codigo_inventario, " & _
                "codigo_tipo, fecha, codigo_centro_costo, " & _
                "codigo_responsable, usuario_creacion, fecha_creacion) values('" & _
                Me.Codigo_Inventario & "','" & Codigo_Tipo & "',convert(datetime,'" & _
                objUtil.FormatFecha(Fecha) & "'),'" & Codigo_Centro_Costo & "','" & _
                Codigo_Responsable & "','" & Usuario & "', getdate())"

                Return objConn.Execute(sql)
            Catch
                Throw
                Return False
            End Try
        End Function


        Function Update() As Boolean
            Dim sql As String, objConn As New NM_Consulta(4)
            Try
                sql = "Update NM_InventarioHilanderia set codigo_tipo='" & Codigo_Tipo & _
                "', fecha=convert(datetime, '" & objUtil.FormatFecha(Fecha) & "'), " & _
                "codigo_centro_costo='" & Codigo_Centro_Costo & "', " & _
                "codigo_responsable='" & Codigo_Responsable & "', usuario_modificacion='" & _
                Usuario & "', fecha_modificacion = getdate() where codigo_inventario='" & _
                Codigo_Inventario & "' "
                Return objConn.Execute(sql)
            Catch
                Return False
            End Try
        End Function

        Function Delete(ByVal sCodigoInventario As String) As Boolean
            Dim sql As String, objConn As New NM_Consulta(4)
            Try
                sql = "Delete from NM_InventarioHilanderia " & _
                " where codigo_inventario='" & sCodigoInventario & "' "

                Return objConn.Execute(sql)
            Catch
                Return False
            End Try

        End Function

        Function List() As DataTable
            Dim sql As String, objConn As New NM_Consulta(4), dt As New DataTable()
            sql = "Select * from NM_InventarioHilanderia "
            dt = objConn.Query(sql)
            Return dt
        End Function

        Function List(ByVal sCodigoTipo As String) As DataTable
            Dim sql As String, objConn As New NM_Consulta(4), dt As New DataTable()
            sql = "Select * from NM_InventarioHilanderia where codigo_tipo ='" & sCodigoTipo & "' "
            dt = objConn.Query(sql)
            Return dt
        End Function

        Function Exist(ByVal sCodigoInventario As String) As Boolean
            Dim sql As String, objConn As New NM_Consulta(4)
            Dim dt As New DataTable()
            Try
                sql = "Select * from NM_InventarioHilanderia " & _
                " where codigo_inventario='" & sCodigoInventario & "' "
                dt = objConn.Query(sql)
                Return (dt.Rows.Count > 0)
            Catch
                Return False
            End Try
        End Function

        Sub Seek(ByVal sCodigoInventario As String)
            Dim sql As String, objConn As New NM_Consulta(4)
            Dim dt As New DataTable(), fila As DataRow
            Try
                sql = "Select * from NM_InventarioHilanderia " & _
                " where codigo_inventario='" & sCodigoInventario & "' "
                dt = objConn.Query(sql)
                For Each fila In dt.Rows
                    Me.Codigo_Inventario = fila("codigo_inventario")
                    Me.Codigo_Centro_Costo = fila("codigo_centro_costo")
                    Me.Codigo_Responsable = fila("codigo_responsable")
                    Me.Codigo_Tipo = fila("codigo_tipo")
                    Me.Fecha = fila("fecha")
                Next
            Catch
            End Try
        End Sub

        Sub SeekByDate(ByVal pFecha As Date)
            Dim sql As String, objConn As New NM_Consulta(4)
            Dim dt As New DataTable, fila As DataRow
            Try
                sql = "Select * from NM_InventarioHilanderia " & _
                " where DATEDIFF(DD, fecha, '" & objUtil.FormatFecha(pFecha) & "') = 0"
                dt = objConn.Query(sql)
                For Each fila In dt.Rows
                    Me.Codigo_Inventario = fila("codigo_inventario")
                    Me.Codigo_Centro_Costo = fila("codigo_centro_costo")
                    Me.Codigo_Responsable = fila("codigo_responsable")
                    Me.Codigo_Tipo = fila("codigo_tipo")
                    Me.Fecha = fila("fecha")
                Next
            Catch
            End Try
        End Sub

        Function GeneraCodigo(ByVal pCodigoTipo As String, ByVal pFecha As Date) As String
            Dim sql As String, objConn As New NM_Consulta(4)
            Dim dt As New DataTable, fila As DataRow, id As String
            Dim objTipo As New NM_TipoInventario
            objTipo.Seek(pCodigoTipo)
            id = UCase(objTipo.Codigo_Tipo) & Format(pFecha, "yy/MM/dd")
            Return id
        End Function

        Function GeneraCodigo() As String
            Dim sql As String, objConn As New NM_Consulta(4)
            Dim dt As New DataTable, fila As DataRow, id As String
            sql = "Select max(right(codigo_inventario,6)) from NM_InventarioHilanderia " & _
            " where left(codigo_inventario,4)=datepart(yyyy,getdate())"
            dt = objConn.Query(sql)
            For Each fila In dt.Rows
                If IsDBNull(fila(0)) = False Then
                    id = Year(Date.Today) & Format(fila(0) + 1, "000000")
                Else
                    id = Year(Date.Today) & "000001"
                End If
            Next


            Return id
        End Function

    End Class

End Namespace
