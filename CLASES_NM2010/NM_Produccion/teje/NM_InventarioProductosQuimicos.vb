Imports NM_General.NM_BaseDatos

Namespace NM_Tejeduria

    Public Class NM_InventarioProductosQuimicos
        Public Fecha As Date
        Public PersonaResponsable As String
        Public CentroCosto As String
        Public CuentaGasto As String
        Public dtProductosQuimicos As DataTable
        Public Usuario As String
        Private objUtil As New NM_General.Util

        Sub New()
            fecha = Date.Today
            CuentaGasto = ""
            PersonaResponsable = ""
            CentroCosto = ""
        End Sub

        Sub New(ByVal pFecha As Date, ByVal pCentroCosto As String)
            Seek(pFecha, pCentroCosto)
        End Sub

        Sub Seek(ByVal pFecha As Date, ByVal pCentroCosto As String)
            Dim objConn As New NM_Consulta
            Dim sql As String
            Dim objDT As New DataTable
            Dim objDR As DataRow
            sql = "Select * from NM_InventarioProductosQuimicos " & _
            " where DATEDIFF(DD, fecha, '" & objUtil.FormatFecha(pFecha) & "') = 0 and centro_costo = '" & pCentroCosto & "' "
            objDT = objConn.Query(sql)

            For Each objDR In objDT.Rows
                Fecha = objDR("fecha")
                CuentaGasto = objDR("cuenta_gasto")
                PersonaResponsable = objDR("persona_responsable")
                CentroCosto = objDR("centro_costo")
            Next

            ' Cargar el detalle
            Dim detalleInventario As New NM_InventarioProductosQuimicosD
            dtProductosQuimicos = detalleInventario.LoadDT(pFecha, pCentroCosto)
        End Sub


        Function Exist(ByVal pFecha As Date, ByVal pCentroCosto As String) As Boolean
            Dim objConn As New NM_Consulta
            Dim sql As String
            Dim objDT As New DataTable
            sql = "Select * from NM_InventarioProductosQuimicos " & _
            " where DATEDIFF(DD, fecha, '" & objUtil.FormatFecha(Fecha) & "') = 0"
            objDT = objConn.Query(sql)
            Return (objDT.Rows.Count > 0)
        End Function
        'Public Sub Insertar(ByVal fecha As String, ByVal horaInicio As String, ByVal horaFin As String, ByVal personaResponsable As String, ByVal codigoCentroCosto As String)
        '    Dim sql As String
        '    Try
        '        If Not (fecha Is Nothing) Then
        '            sql = "INSERT INTO NM_InventarioProductosQuimicos " & _
        '                "(fecha, hora_inicio, hora_fin, persona_responsable, codigo_centro_costo) " & _
        '                "VALUES ('" & _
        '                fecha & "','" & _
        '                horaInicio & "','" & _
        '                horaFin & "','" & _
        '                personaResponsable & "','" & _
        '                areaResponsable & "')"
        '            BD.Execute(sql)
        '        Else
        '            Throw New Exception("No se puede insertar porque la fecha no es válida.")
        '        End If
        '    Catch ex As Exception
        '        Throw ex
        '    End Try
        'End Sub

        Public Function Insertar() As Boolean
            Dim sql As String, objConn As New NM_Consulta
            Try
                If IsDate(Fecha) = True AndAlso CentroCosto <> "" Then
                    sql = "INSERT INTO NM_InventarioProductosQuimicos " & _
                        "(fecha, cuenta_gasto, persona_responsable, centro_costo, " & _
                        " usuario_creacion, fecha_creacion) " & _
                        "VALUES (convert(datetime,'" & _
                        objUtil.FormatFecha(Fecha) & "'),'" & _
                        CuentaGasto & "','" & _
                        PersonaResponsable & "', '" & CentroCosto & "', '" & _
                        Usuario & "', GetDate())"
                    Return objConn.Execute(sql)
                Else
                    Throw New Exception("No se puede insertar porque la fecha no es válida.")
                End If
            Catch ex As Exception
                Throw ex
            End Try
        End Function

    End Class

    Public Class NM_InventarioProductosQuimicosD
        Dim BD As New NM_Consulta
        Public Usuario As String
        Public Fecha As Date
        Public CodigoInsumoQuimico As String
        Public CentroCosto As String
        Public Valor As Double
        Private objUtil As New NM_General.Util

        Public Function Insertar() As Boolean
            Dim objConn As New NM_Consulta
            Try
                If IsDate(Fecha) = True Then
                    Dim sql = "INSERT INTO NM_InventarioProductosQuimicosD " & _
                        "(fecha, centro_costo, codigo_insumo_quimico, valor, usuario_creacion, fecha_creacion) " & _
                        "VALUES (convert(datetime,'" & objUtil.FormatFecha(Fecha) & "'),'" & _
                        CentroCosto & "', '" & CodigoInsumoQuimico & "','" & _
                        Valor & "','" & Usuario & "',GetDate())"
                    Return objConn.Execute(sql)
                Else
                    Throw New Exception("No se puede insertar porque la fecha no es válida.")
                End If
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Function LoadDT(ByVal pFecha As Date, ByVal pCentroCosto As String) As DataTable
            Dim objConn As New NM_Consulta
            Dim sql = "SELECT codigo_insumo_quimico, valor " & _
                "FROM NM_InventarioProductosQuimicosD " & _
                "WHERE DATEDIFF(DD, fecha, '" & objUtil.FormatFecha(pFecha) & "') = 0 and centro_costo = '" & pCentroCosto & "' "
            Return objConn.Query(sql)
        End Function

        Function DeleteAll(ByVal pFecha As Date, ByVal pCentroCosto As String) As Boolean
            Dim objConn As New NM_Consulta
            Dim sql = "delete FROM NM_InventarioProductosQuimicosD " & _
                "WHERE DATEDIFF(DD, fecha, '" & objUtil.FormatFecha(pFecha) & "') = 0 and centro_costo = '" & pCentroCosto & "' "
            Return objConn.Execute(sql)
        End Function

    End Class

End Namespace