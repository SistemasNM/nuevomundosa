Imports NM_General.NM_BaseDatos
Namespace NM_Hilanderia
    Public Class NM_InventarioAperturaCarda

        Public Codigo_Inventario As String
        Public Codigo_Linea As String
        Public Codigo_MateriaPrima As String
        Public Fardos_Reserva As Double
        Public Kilos_Fardo_Reserva As Double
        Public Kilos_Fardo_Abridora As Double
        Public Kilos_Proceso As Double
        Public Tachos_Cinta_Carda As Double
        Public porcentaje_avance As Double
        Public Tachos_Llenos As Double
        Public Usuario As String
        Private objUtil As New NM_General.Util

        Sub New()
            Codigo_Inventario = ""
            Codigo_Linea = ""
            Codigo_MateriaPrima = ""
            Fardos_Reserva = 0
            Kilos_Fardo_Reserva = 0
            Kilos_Fardo_Abridora = 0
            Kilos_Proceso = 0
            Tachos_Cinta_Carda = 0
            porcentaje_avance = 0
            Tachos_Llenos = 0
            Usuario = ""
        End Sub

        Function Add() As Boolean
            Dim sql As String, objConn As New NM_Consulta(4)
            Try
                sql = "Insert into NM_InventarioAperturaCarda (codigo_inventario," & _
                "codigo_linea, codigo_materia_prima,fardos_reserva, kilos_fardo_reserva, " & _
                "kilos_fardo_abridora,kilos_proceso, tachos_cinta_carda, porcentaje_avance, tachos_llenos, " & _
                "usuario_creacion, fecha_creacion) values('" & Codigo_Inventario & "','" & _
                Codigo_Linea & "','" & Codigo_MateriaPrima & "'," & Fardos_Reserva & "," & _
                Kilos_Fardo_Reserva & "," & Kilos_Fardo_Abridora & "," & Kilos_Proceso & "," & _
                Tachos_Cinta_Carda & "," & porcentaje_avance & "," & Tachos_Llenos & ",'" & Usuario & "', getdate())"
                Return objConn.Execute(sql)
            Catch
                Return False
            End Try
        End Function

        Function Update() As Boolean
            Dim sql As String, objConn As New NM_Consulta(4)
            Try
                sql = "Update NM_InventarioAperturaCarda set fardos_reserva=" & Fardos_Reserva & _
                ",kilos_fardo_reserva=" & Kilos_Fardo_Reserva & ",kilos_fardo_abridora=" & Kilos_Fardo_Abridora & _
                ", kilos_proceso=" & Kilos_Proceso & ", tachos_cinta_carda=" & Tachos_Cinta_Carda & _
                ", porcentaje_avance = " & porcentaje_avance & ", tachos_llenos = " & Tachos_Llenos & ", usuario_modificacion='" & _
                Usuario & "', fecha_modificacion=getdate() where codigo_inventario='" & _
                Codigo_Inventario & "' and codigo_linea='" & Codigo_Linea & "' " & _
                " and codigo_materia_prima='" & Codigo_MateriaPrima & "' "
                Return objConn.Execute(sql)
            Catch
                Return False
            End Try
        End Function

        Function Delete(ByVal sCodigoInventario As String, ByVal sCodigoLinea As String, _
        ByVal sCodigoMateria As String) As Boolean
            Dim sql As String, objConn As New NM_Consulta(4)
            Try
                sql = " Delete from NM_InventarioAperturaCarda where codigo_inventario='" & _
                sCodigoInventario & "' and codigo_linea='" & sCodigoLinea & "' " & _
                " and codigo_materia_prima='" & sCodigoMateria & "' "

                Return objConn.Execute(sql)
            Catch
                Return False
            End Try
        End Function

        Function List() As DataTable
            Dim sql As String, objConn As New NM_Consulta(4)
            Dim dt As New DataTable()
            sql = "Select * from NM_InventarioAperturaCarda "
            dt = objConn.Query(sql)
            Return dt
        End Function

        Function List(ByVal dFecha As Date) As DataTable
            Dim sql As String, objConn As New NM_Consulta(4)
            Dim dt As New DataTable

            'Dim objUtil As New NM_Produccion.NM_Util.NM_Util
            Dim objUtil As New NM_General.Util
            sql = " Select iac.*, mp.descripcion_materia_prima " & _
                " from NM_InventarioAperturaCarda iac, NM_MateriaPrima mp, " & _
                " NM_InventarioHilanderia IH " & _
                " where iac.codigo_materia_prima = mp.codigo_materia_prima " & _
                " and iac.codigo_inventario = IH.codigo_inventario " & _
                " and DATEDIFF(DD, IH.fecha, '" & objUtil.FormatFecha(dFecha) & "') = 0"

            dt = objConn.Query(sql)
            Return dt
        End Function

        Function List(ByVal pFecha As Date, ByVal pCentroCosto As String) As DataTable
            Dim sql As String, objConn As New NM_Consulta(4)
            Dim dt As New DataTable

            'Dim objUtil As New NM_Produccion.NM_Util.NM_Util
            Dim objUtil As New NM_General.Util
            
            sql = " Select iac.*, mp.descripcion_materia_prima " & _
                " from NM_InventarioAperturaCarda iac, NM_MateriaPrima mp, " & _
                " NM_InventarioHilanderia IH " & _
                " where iac.codigo_materia_prima = mp.codigo_materia_prima " & _
                " and iac.codigo_inventario = IH.codigo_inventario " & _
                " and DATEDIFF(DD, IH.fecha, '" & objUtil.FormatFecha(pFecha) & "') = 0 and IH.codigo_centro_costo = '" & _
                pCentroCosto & "' "
            dt = objConn.Query(sql)
            Return dt
        End Function

        Function Exist(ByVal sCodigoInventario As String, ByVal sCodigoLinea As String, _
        ByVal sCodigoMateria As String) As Boolean
            Dim sql As String, objConn As New NM_Consulta(4)
            Dim dt As New DataTable
            Try
                sql = "Select * from NM_InventarioAperturaCarda where codigo_inventario='" & _
                sCodigoInventario & "' and codigo_linea='" & sCodigoLinea & "' " & _
                " and codigo_materia_prima='" & sCodigoMateria & "' "
                dt = objConn.Query(sql)
                Return (dt.Rows.Count > 0)
            Catch
                Return False
            End Try
        End Function

        Sub Seek(ByVal sCodigoInventario As String, ByVal sCodigoLinea As String, _
        ByVal sCodigoMateria As String)
            Dim sql As String, objConn As New NM_Consulta(4)
            Dim dt As New DataTable, fila As DataRow
            sql = "Select * from NM_InventarioAperturaCarda where codigo_inventario='" & _
            sCodigoInventario & "' and codigo_linea = '" & sCodigoLinea & "' " & _
            " and codigo_materia_prima='" & sCodigoMateria & "' "
            dt = objConn.Query(sql)
            For Each fila In dt.Rows
                If IsDBNull(fila("fardos_reserva")) = False Then Fardos_Reserva = fila("fardos_reserva")
                If IsDBNull(fila("kilos_fardo_reserva")) = False Then Kilos_Fardo_Reserva = fila("kilos_fardo_reserva")
                If IsDBNull(fila("kilos_fardo_abridora")) = False Then Kilos_Fardo_Abridora = fila("kilos_fardo_abridora")
                If IsDBNull(fila("kilos_proceso")) = False Then Kilos_Proceso = fila("kilos_proceso")
                If IsDBNull(fila("tachos_cinta_carda")) = False Then Tachos_Cinta_Carda = fila("tachos_cinta_carda")
                If IsDBNull(fila("tachos_llenos")) = False Then Tachos_Llenos = fila("tachos_llenos")
                Codigo_Inventario = sCodigoInventario
                Codigo_Linea = sCodigoLinea
                Codigo_MateriaPrima = sCodigoMateria
            Next
        End Sub

    End Class

End Namespace