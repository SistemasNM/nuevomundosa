Imports NM_General.NM_BaseDatos

Namespace NM_Tejeduria

    Public Class NM_ControlInventarioCono
        Public Fecha As Date
        Public CodigoResponsable As String
        Public CentroCosto As String
        Public Usuario As String
        Private objUtil As New NM_General.Util

        Public Function Add() As Boolean
            Dim objConn As New NM_Consulta
            Dim strsql As String
            strsql = "INSERT INTO NM_ControlInventarioCono " & _
            "(fecha, codigo_responsable, centro_costo, " & _
            "usuario_creacion, fecha_creacion) values " & _
            "(convert(datetime, '" & objUtil.FormatFecha(Fecha) & "')," & _
            "'" & CodigoResponsable & "','" & CentroCosto & "'," & _
            "'" & Usuario & "',GetDate())"
            Return objConn.Execute(strsql)
        End Function

        Public Function Update() As Boolean
            Try
                Dim objConn As New NM_Consulta
                Dim strsql As String
                strsql = "UPDATE NM_ControlInventarioCono SET " & _
                "codigo_responsable = '" & CodigoResponsable & "'," & _
                "usuario_modificacion = '" & Usuario & "'," & _
                "fecha_modificacion = getdate()" & _
                " where DATEDIFF(DD, fecha, '" & objUtil.FormatFecha(Fecha) & "') = 0 " & _
                " and centro_costo = '" & CentroCosto & "' "
                Return objConn.Execute(strsql)
            Catch ex As Exception
                Throw ex
                Return False
            End Try
        End Function

        'Public Function delete(ByVal pcodigo_grupo As String, ByVal pcodigo_operario As String) As Boolean
        '    If pcodigo_operario <> "" Then
        '        Dim strsql As String
        '        Dim DB As New NM_Consulta()
        '        strsql = "DELETE FROM NM_ControlInventarioCono where codigo_operario = '" & pcodigo_operario & "' " & _
        '        "AND codigo_grupo = '" & pcodigo_grupo & "'"
        '        Return DB.Execute(strsql)
        '    Else
        '        Return False
        '    End If
        'End Function

        'Public Function list()
        '    Dim bd As New NM_Consulta(4)
        '    Dim sql = "SELECT * FROM NM_Operario where "
        '    Return bd.Query(sql)
        'End Function


        Function getConoNuevos() As DataTable 'Conos nuevos en zona de almacenamiento
            Dim ConosNuevos As New NM_InventarioConoNuevo
            Return ConosNuevos.list(Fecha, CentroCosto)
        End Function
        Function getConoReuso() As DataTable ' Cono de reuso en zona de alamcenamiento
            Dim ConosReuso As New NM_InventarioConoReuso
            Return ConosReuso.List(Fecha, CentroCosto)
        End Function
        Function getConoProceso() As DataTable ' Conos en proceso ubcado castillo
            Dim ConosProceso As New NM_InventarioConoProceso
            Return ConosProceso.list(Fecha, CentroCosto)
        End Function
        Function getConoReusoProc() As DataTable ' Conos de resuso en proceso ubicado castillo
            Dim ConosReusoProc As New NM_InventarioConoReusoProc
            Return ConosReusoProc.List(Fecha, CentroCosto)
        End Function

        Public Function list(ByVal pCodGrupo As String) As DataTable
            Dim bd As New NM_Consulta(0)
            Dim sql = "SELECT * FROM NM_ControlInventarioCono where codigo_grupo='" & pCodGrupo & "'"
            Return bd.Query(sql)
        End Function

        Function Exist(ByVal pfecha As Date, ByVal pCentroCosto As String) As Boolean
            Dim sql As String, objConn As New NM_Consulta
            Dim dt As New DataTable
            Try
                sql = "Select * from NM_ControlInventarioCono where " & _
                              " DATEDIFF(DD, fecha, '" & objUtil.FormatFecha(pfecha) & "') = 0 " & _
                              " and centro_costo='" & pCentroCosto & "' "
                dt = objConn.Query(sql)
                Return (dt.Rows.Count > 0)
            Catch
                Return False
            End Try
        End Function

        Sub Seek(ByVal pFecha As Date, ByVal pCentroCosto As String)
            Dim sql As String, objConn As New NM_Consulta
            Dim dt As New DataTable, fila As DataRow
            sql = "SELECT * FROM NM_ControlInventarioCono " & _
            " where DATEDIFF(DD, fecha, '" & objUtil.FormatFecha(pFecha) & "') = 0 and centro_costo = '" & _
            pCentroCosto & "' "

            dt = objConn.Query(sql)
            For Each fila In dt.Rows
                If Not IsDBNull(fila("fecha")) Then Fecha = fila("fecha")
                If Not IsDBNull(fila("codigo_responsable")) Then CodigoResponsable = fila("codigo_responsable")
                If Not IsDBNull(fila("centro_costo")) Then CentroCosto = fila("centro_costo")
                Exit For
            Next
        End Sub

    End Class

End Namespace

